using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Controls
{
    [TestClass]
    public class AssemblyTests
    {
        public void ThereShouldBeNo_Our_Own_Assemblies_referenced_Test()
        {
            var QB_ControlsAssembly = typeof(global::Questify.Builder.UI.Wpf.Controls.BlockGrid).Assembly;


            if (QB_ControlsAssembly == null) Assert.Inconclusive("");
            var referencedAssemblies = new HashSet<string>(QB_ControlsAssembly.GetReferencedAssemblies().Select(a => a.Name));

            HashSet<string> allowed = new HashSet<string>(Approved());

            referencedAssemblies.ExceptWith(allowed);

            foreach (var e in referencedAssemblies)
                Debug.WriteLine(e);

            Assert.IsTrue(referencedAssemblies.Count == 0);
        }


        public List<string> Approved()
        {

            var approved = new List<string>();
            approved.Add("mscorlib");
            approved.Add("Microsoft.CSharp");
            approved.Add("PresentationCore");
            approved.Add("PresentationFramework");
            approved.Add("System");
            approved.Add("System.Core");
            approved.Add("System.Data");
            approved.Add("System.Data.DataSetExtensions");
            approved.Add("System.Xaml");
            approved.Add("System.Xml.Linq");
            approved.Add("WindowsBase");

            approved.Add("Microsoft.VisualStudio.CodeCoverage.Shim");
            return approved;
        }
    }
}
