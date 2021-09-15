
using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Questify.Builder.UnitTests
{
    /// <summary>
    /// This class is here to ensure that any test that Debugs.Asserts gets treated like a failed test.
    /// </summary>
    public class FailOnAssert : TraceListener
    {
        [ThreadStatic]
        private static bool _disable;

        private static FailOnAssert _instance = null;

        private static object _lock = new object();

        private FailOnAssert()
        {

        }

        public static FailOnAssert GetInstance()
        {
            if (_instance != null)
            {
                return _instance;
            }

            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new FailOnAssert();
                }
            }
            return _instance;
        }

        public static bool Disable
        {
            get
            {
                return _disable;
            }
            set
            {
                _disable = value;
            }
        }

        public override void Fail(string message)
        {
            if (!Disable)
            {
                Assert.Fail("Product raised an assert: " + message);
            }
        }

        public override void Fail(string message, string detailMessage)
        {
            if (!Disable)
            {
                Assert.Fail("Product raised an assert: " + message + "\n" + detailMessage);
            }
        }

        public override void Write(string message)
        {
        }

        public override void WriteLine(string message)
        {
        }
    }
}
