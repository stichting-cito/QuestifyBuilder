using System;
using Questify.Builder.Services.TasksService.TaskClasses;

namespace Questify.Builder.Services.TasksService
{
    internal abstract class QuestifyBuilderTaskBase : IDisposable
    {
        internal BuilderTaskExecutionParamsBase ExecutionParams;
        internal BuilderTaskProgress Progress;
        internal BuilderTaskResult TaskResult;

        ~QuestifyBuilderTaskBase()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        protected virtual void Dispose(Boolean disposing)
        {
            if (ExecutionParams != null && !string.IsNullOrEmpty(ExecutionParams.TempLogFileName))
            {
                if (disposing)
                {
                    if (ExecutionParams.TempLogFileWriter != null)
                    {
                        ExecutionParams.TempLogFileWriter.Dispose();
                        ExecutionParams.TempLogFileWriter = null;
                    }
                }

                if (!string.IsNullOrEmpty(ExecutionParams.TempLogFileName) && System.IO.File.Exists(ExecutionParams.TempLogFileName))
                {
                    System.IO.File.Delete(ExecutionParams.TempLogFileName);
                    ExecutionParams.TempLogFileName = null;
                }
            }
        }

    }
}