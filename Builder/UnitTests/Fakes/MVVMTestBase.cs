using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Questify.Builder.UnitTests.Fakes
{
    [TestClass]
    public abstract class MVVMTestBase
    {

        private TestContext testContextInstance;
        private Dictionary<Type, Action<Attribute>> _Initializer;

        public MVVMTestBase()
        {
            _Initializer = new Dictionary<Type, Action<Attribute>>();
        }

        public void AddAttributteInitializer<T>(Action<Attribute> init) where T : Attribute
        {
            _Initializer.Add(typeof(T), init);
        }

        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext) { }

        [ClassCleanup()]
        public static void MyClassCleanup() { }

        [TestInitialize()]
        public void TestInitialize() { RunInitializers(); }

        [TestCleanup()]
        public void TestCleanup() { }


        private void RunInitializers()
        {
            var meth = this.GetType().GetMethod(testContextInstance.TestName);
            foreach (var a in meth.GetCustomAttributes(false))
            {
                Action<Attribute> doInit4Att = null;
                if (_Initializer.TryGetValue(a.GetType(), out doInit4Att))
                {
                    doInit4Att(a as Attribute);
                }
            }

        }
    }
}
