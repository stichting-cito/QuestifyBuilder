using System;

namespace Questify.Builder.Services.TasksService
{
    internal class BuilderTaskExecutionParamsBase
    {
        public Boolean LogTheActions { get; set; }

        public string TempLogFileName { get; set; }
        public System.IO.StreamWriter TempLogFileWriter { get; set; }

        public Boolean CancellationPending { get; set; }
    }
}