using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Windows;
using System.Windows.Forms.Integration;
using Cinch;
using MEFedMVVM.ViewModelLocator;

namespace Questify.Builder.UI.Wpf.Presentation.WinformsInterop
{
    [PartCreationPolicy(CreationPolicy.Shared)]
    [ExportService(ServiceType.Both, typeof(IWPF2WinVisualizerService))]
    class WPFUIVisualizerService : IWPF2WinVisualizerService
    {
        private readonly Dictionary<string, Type> _registeredWindows;
        private readonly int _owningThread;

        public WPFUIVisualizerService()
        {
            _registeredWindows = new Dictionary<string, Type>();
            _owningThread = Environment.CurrentManagedThreadId;
        }

        public void Register(Dictionary<string, Type> startupData)
        {
            GuardThreadAccess();
            foreach (var entry in startupData)
                Register(entry.Key, entry.Value);
        }

        public void Register(string key, Type winType)
        {
            GuardThreadAccess();
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (winType == null)
                throw new ArgumentNullException(nameof(winType));
            if (!typeof(Window).IsAssignableFrom(winType))
                throw new ArgumentException("winType must be of type Window");

            lock (_registeredWindows)
            {
                _registeredWindows.Add(key, winType);
            }
        }

        public bool Unregister(string key)
        {
            GuardThreadAccess();
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            lock (_registeredWindows)
            {
                return _registeredWindows.Remove(key);
            }
        }

        public bool Show(string key, object state, bool setOwner,
    EventHandler<UICompletedEventArgs> completedProc)
        {
            GuardThreadAccess();
            Window win = CreateWindow(key, state, setOwner, completedProc, false);
            if (win != null)
            {
                win.Show();
                return true;
            }

            Debug.Assert(false, "Windows not found");

            return false;
        }

        public bool? ShowDialog(string key, object state, bool setQBMainWindowAsOwner)
        {
            Window win = CreateWindow(key, state, setQBMainWindowAsOwner, null, true);
            if (win != null)
                return win.ShowDialog();

            return false;
        }

        public List<object> GetOpenWindows()
        {
            GuardThreadAccess();
            List<object> openWindows = new List<object>();

            foreach (var openForm in System.Windows.Forms.Application.OpenForms)
                openWindows.Add(openForm);

            foreach (var openWindow in Application.Current.Windows)
                openWindows.Add(openWindow);

            return openWindows;
        }



        private Window CreateWindow(string key, object dataContext, bool setOwner,
    EventHandler<UICompletedEventArgs> completedProc, bool isModal)
        {
            GuardThreadAccess();
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            Type winType;
            lock (_registeredWindows)
            {
                if (!_registeredWindows.TryGetValue(key, out winType))
                    return null;
            }

            var win = (Window)Activator.CreateInstance(winType);
            ElementHost.EnableModelessKeyboardInterop(win);

            if (dataContext is IViewStatusAwareInjectionAware)
            {
                IViewAwareStatus viewAwareStatus =
                    ViewModelRepository.Instance.Resolver.Container.GetExport<IViewAwareStatus>().Value;
                viewAwareStatus.InjectContext((FrameworkElement)win);
                ((IViewStatusAwareInjectionAware)dataContext).InitialiseViewAwareService(viewAwareStatus);
            }

            if (dataContext is IViewStatusAwareWindowInjectionAware)
            {
                IViewAwareStatusWindow viewAwareStatusWindow =
                    ViewModelRepository.Instance.Resolver.Container.GetExport<IViewAwareStatusWindow>().Value;
                viewAwareStatusWindow.InjectContext((FrameworkElement)win);
                ((IViewStatusAwareWindowInjectionAware)dataContext).InitialiseViewAwareWindowService(viewAwareStatusWindow);
            }

            if (setOwner)
            {
                System.Windows.Forms.Form main = GetMainWindow();

                Debug.Assert(main != null);
                new System.Windows.Interop.WindowInteropHelper(win).Owner = main.Handle;
            }

            win.DataContext = dataContext;

            if (dataContext != null)
            {
                var bvm = dataContext as ViewModelBase;
                if (bvm != null)
                {
                    if (isModal)
                    {
                        bvm.CloseRequest += ((EventHandler<CloseRequestEventArgs>)((s, e) =>
                        {
                            try
                            {
                                win.DialogResult = e.Result;
                            }
                            catch (InvalidOperationException)
                            {
                                win.Close();
                            }
                        })).MakeWeak(eh => bvm.CloseRequest -= eh);


                    }
                    else
                    {
                        bvm.CloseRequest += ((EventHandler<CloseRequestEventArgs>)((s, e) => win.Close()))
                                .MakeWeak(eh => bvm.CloseRequest -= eh);
                    }
                    bvm.ActivateRequest += ((EventHandler<EventArgs>)((s, e) => win.Activate()))
                        .MakeWeak(eh => bvm.ActivateRequest -= eh);
                }
            }

            win.Closed += (s, e) =>
            {
                var disposableContext = win.DataContext as ICinchDisposable;
                if (disposableContext != null)
                    disposableContext.Dispose();

                win.DataContext = null;
                if (completedProc != null)
                {
                    completedProc(this, new UICompletedEventArgs()
                    {
                        State = dataContext,
                        Result = (isModal) ? win.DialogResult : null
                    });
                }

                var main = GetMainWindow();
                if (main != null) main.BringToFront();
            };

            return win;
        }

        private System.Windows.Forms.Form GetMainWindow()
        {
            GuardThreadAccess();
            System.Windows.Forms.Form ret = null;
            foreach (System.Windows.Forms.Form f in System.Windows.Forms.Application.OpenForms)
            {
                if (f.GetType().Name.Contains("MainForm"))
                {
                    ret = f;
                    break;
                }
            }
            return ret;
        }

        void GuardThreadAccess()
        {
            if (_owningThread != Environment.CurrentManagedThreadId)
            {
                var stack = new StackTrace();

                for (int stackFrame = 0; stackFrame < stack.FrameCount; stackFrame++)
                    Debug.WriteLine(stack.GetFrame(stackFrame).ToString());

                Debug.Assert(false, "Cross Thread Access Detected");
            }
        }
    }
}
