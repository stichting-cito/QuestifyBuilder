using System;

namespace Questify.Builder.Services.TasksService.ItemHarmonization
{
    internal class ItemHarmonizationExecutionParams : BuilderTaskExecutionParamsBase
    {
        internal ItemHarmonizationExecutionParams(Boolean logTheActions)
        {
            LogTheActions = logTheActions;
        }

    }
}