using System;
using Questify.Builder.Logic.Service.Domain.TransManager;

namespace Questify.Builder.Logic.Service.Domain.AppServices
{
    public class GlobalContext
         : IGlobalContext
    {
        static readonly Object LocatorLock = new object();
        private static GlobalContext InternalInstance;

        private GlobalContext() { }

        public static GlobalContext Instance()
        {
            if (InternalInstance != null)
            {
                return InternalInstance;
            }

            lock (LocatorLock)
            {
                if (InternalInstance == null)
                {
                    InternalInstance = new GlobalContext(); ;
                }
            }
            return InternalInstance;
        }


        public ITransFactory TransFactory { get; set; }

    }
}
