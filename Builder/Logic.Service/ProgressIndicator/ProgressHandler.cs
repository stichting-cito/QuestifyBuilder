using System;
using System.ComponentModel;
using System.Windows.Threading;

namespace Questify.Builder.Logic.Service.ProgressIndicator
{
    public static class ProgressHandler
    {
        public static bool IsTaskBusy { get; private set; } = false;

        public static bool ProgressWindowVisible { get; private set; } = false;

        public static void DoWorkWithModal(Action<IProgress<string>> work, System.Windows.Forms.Form owner, int openWindowDelayInMs, int closeWindowTimeoutInSeconds, int nrOfProgressSteps)
        {
            if (openWindowDelayInMs > (closeWindowTimeoutInSeconds > 0 ? closeWindowTimeoutInSeconds * 1000 : int.MaxValue)) throw new ArgumentException("openWindowDelay cannot exceed timeOutInSeconds!");

            var progressWindow = new ProgressWindow(owner, nrOfProgressSteps);
            var worker = new BackgroundWorker();

            SetCenterOfProgressWindow(owner, progressWindow);

            worker.WorkerSupportsCancellation = true;
            worker.WorkerReportsProgress = true;

            var progress = new Progress<string>(data => progressWindow.TaskText = data);

            progress.ProgressChanged += (s, e) =>
                {
                    progressWindow.ProgressBarValue += 1;
                };

            worker.DoWork += (s, e) =>
                {
                    IsTaskBusy = true;

                    if (!e.Cancel)
                        work(progress);
                };

            worker.ProgressChanged += (s, e) =>
                {
                    progressWindow.ProgressBarValue = e.ProgressPercentage;
                };

            worker.RunWorkerCompleted += (s, e) =>
                {
                    IsTaskBusy = false;
                    progressWindow.Close();
                };

            progressWindow.Loaded += (s, e) => ProgressWindowVisible = true;
            progressWindow.Closed += (s, e) => ProgressWindowVisible = false;

            worker.RunWorkerAsync();

            if (openWindowDelayInMs > 0)
                CreateOpenDelayTimer(progressWindow, openWindowDelayInMs);
            else
                progressWindow.ShowDialog();

            if (closeWindowTimeoutInSeconds > 0)
                CreateCloseWindowTimeoutTimer(progressWindow, closeWindowTimeoutInSeconds);
        }

        private static void SetCenterOfProgressWindow(System.Windows.Forms.Form owner, ProgressWindow progressWindow)
        {
            progressWindow.Left = owner.Left + (owner.Width - progressWindow.Width) / 2;
            progressWindow.Top = owner.Top + (owner.Height - progressWindow.Height) / 2;
        }

        private static void CreateOpenDelayTimer(ProgressWindow progressWindow, int openWindowDelayInMs)
        {
            var openWindowDelayTimer = new System.Timers.Timer(openWindowDelayInMs);

            openWindowDelayTimer.Elapsed += (s1, e1) =>
            {
                openWindowDelayTimer.Stop();

                if (ProgressWindowVisible)
                    progressWindow.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => progressWindow.ShowDialog()));
            };

            openWindowDelayTimer.Start();
        }

        private static void CreateCloseWindowTimeoutTimer(ProgressWindow progressWindow, int timeoutInSeconds)
        {
            var openWindowTimoutTimer = new System.Timers.Timer(timeoutInSeconds * 1000);

            openWindowTimoutTimer.Elapsed += (s1, e1) =>
            {
                openWindowTimoutTimer.Stop();

                if (ProgressWindowVisible)
                    progressWindow.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => progressWindow.Close()));
            };

            openWindowTimoutTimer.Start();
        }
    }
}
