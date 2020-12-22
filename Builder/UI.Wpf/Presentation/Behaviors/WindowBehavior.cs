using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;
using Cinch;

namespace Questify.Builder.UI.Wpf.Presentation.Behaviors
{
    public class WindowBehavior : Behavior<Window>
    {
        protected override void OnAttached()
        {
            AssociatedObject.Closed += AssociatedObjectClosed;
            AssociatedObject.PreviewKeyUp += AssociatedObjectPreviewKeyUp;

            base.OnAttached();
        }

        private void AssociatedObjectPreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.S && Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                var args = new EventToCommandArgs(AssociatedObject, null, null, e);
                SaveCommand.Execute(args);
            }
        }

        void AssociatedObjectClosed(object sender, EventArgs e)
        {
            var args = new EventToCommandArgs(AssociatedObject, null, null, e);
            ClosedCommand.Execute(args);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            AssociatedObject.Closed -= AssociatedObjectClosed;
            AssociatedObject.PreviewKeyUp -= AssociatedObjectPreviewKeyUp;
        }

        public static readonly DependencyProperty ClosedCommandProperty = DependencyProperty.Register(
    "ClosedCommand", typeof(ICommand), typeof(WindowBehavior),
    new PropertyMetadata(null));


        public ICommand ClosedCommand
        {
            get { return (ICommand)GetValue(ClosedCommandProperty); }
            set { SetValue(ClosedCommandProperty, value); }
        }

        public static readonly DependencyProperty SaveCommandProperty = DependencyProperty.Register(
    "SaveCommand", typeof(ICommand), typeof(WindowBehavior),
    new PropertyMetadata(null));


        public ICommand SaveCommand
        {
            get { return (ICommand)GetValue(SaveCommandProperty); }
            set { SetValue(SaveCommandProperty, value); }
        }
    }
}
