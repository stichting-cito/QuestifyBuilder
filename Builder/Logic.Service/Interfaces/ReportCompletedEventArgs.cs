using System;

namespace Questify.Builder.Logic.Service.Interfaces
{
    public class ReportCompletedEventArgs : EventArgs
    {
        public ReportCompletedEventArgs(bool result) : base()
        {
            Result = result;
        }

        public bool Result { get; set; }
    }
}
