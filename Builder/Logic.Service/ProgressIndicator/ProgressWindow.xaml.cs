using System;
using System.Windows;
using System.Windows.Interop;

namespace Questify.Builder.Logic.Service.ProgressIndicator
{
    public partial class ProgressWindow : Window
    {
        private ProgressWindow()
        {
            InitializeComponent();

            progressBar.IsIndeterminate = true;
        }

        public ProgressWindow(System.Windows.Forms.Form owner, int nrOfSteps)
            : this()
        {
            if (nrOfSteps < 0 || nrOfSteps > 100) throw new ArgumentException("nOfSteps must be between 0 and 100!");

            var helper = new WindowInteropHelper(this);

            helper.Owner = owner.Handle;

            Initialize(nrOfSteps);
        }

        public ProgressWindow(System.Windows.Window owner, int nrOfSteps)
            : this()
        {
            if (nrOfSteps < 0 || nrOfSteps > 100) throw new ArgumentException("nOfSteps must be between 0 and 100!");

            this.Owner = owner;

            Initialize(nrOfSteps);
        }

        private void Initialize(int nrOfSteps)
        {
            if (nrOfSteps > 0)
            {
                progressBar.Maximum = nrOfSteps;
                progressBar.IsIndeterminate = false;
            }
        }

        internal double ProgressBarValue
        {
            get { return progressBar.Value; }
            set { progressBar.Value = value; }
        }

        public string TaskText
        {
            get { return taskTextTextBlock.Text; }
            set { taskTextTextBlock.Text = value; }
        }

        public bool IsIndeterminate
        {
            get { return progressBar.IsIndeterminate; }
        }

    }
}
