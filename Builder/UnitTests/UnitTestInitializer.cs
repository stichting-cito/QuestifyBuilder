
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Questify.Builder.UnitTests
{
    /// <summary>
    /// This class is here for a construct to ensure that when a test Debug.Asserts the test fails.
    /// </summary>
    [TestClass]
    public class UnitTestInitializer
    {
        private static bool _initialized = false;
        private static object _lock = new object();

        [AssemblyInitialize]
        public static void Initialize(TestContext context)
        {
            lock (_lock)
            {
                if (_initialized)
                {
                    return;
                }

                TraceListener removeListener = null;
                foreach (TraceListener listener in Debug.Listeners)
                {
                    if (listener is DefaultTraceListener)
                    {
                        removeListener = listener;
                        break;
                    }
                }
                Debug.Listeners.Remove(removeListener);
                Debug.Listeners.Add(FailOnAssert.GetInstance());
            }
        }
    }

}