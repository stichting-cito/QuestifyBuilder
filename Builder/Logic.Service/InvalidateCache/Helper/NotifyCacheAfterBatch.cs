using System;

namespace Questify.Builder.Logic.Service.InvalidateCache.Helper
{
    public class NotifyCacheAfterBatch : IDisposable
    {

        public NotifyCacheAfterBatch()
        {
            InvalidateCacheBankService.StartUsingBatchNotifier();
            InvalidateCacheResourceService.StartUsingBatchNotifier();
        }


        public void Dispose()
        {
            InvalidateCacheBankService.StopUsingBatchNotifier();
            InvalidateCacheResourceService.StopUsingBatchNotifier();
            InvalidateCacheBankService.ExecuteBatchInvalidation();
            InvalidateCacheResourceService.ExecuteBatchInvalidation();
        }
    }
}
