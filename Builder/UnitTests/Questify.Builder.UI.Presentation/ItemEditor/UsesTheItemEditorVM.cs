
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Xml.Linq;
using Cinch;
using Cito.Tester.Common;
using FakeItEasy;
using MEFedMVVM.ViewModelLocator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.UI.Wpf.Presentation;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels;
using Questify.Builder.UnitTests.Fakes;
using Questify.Builder.UnitTests.Framework.FakeAppTemplate;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.ItemEditor
{
    [TestClass]
    public abstract class UsesTheItemEditorVM : MVVMTestBase
    {
        private IItemEditorViewModel _fake;
        private TestViewAwareStatus _viewAwareStatus;

        private HandleFakeParameterSet _fakeParamSetHandler = new HandleFakeParameterSet();
        private readonly HandleFakeAssessmentItem _fakeAssessmentItem = new HandleFakeAssessmentItem();
        private HandleFakeConcept _fakeConceptHandler;
        private HandleFakeTree _fakeTreeHandler;
        private readonly Dictionary<string, string> _resources = new Dictionary<string, string>();
        private readonly List<OrderedAttribute> _parameterAttributes = new List<OrderedAttribute>();

        public UsesTheItemEditorVM()
        {
            AddAttributteInitializer<AddParameterAttribute>(att => DealWith_AddParameter(att as AddParameterAttribute));
            AddAttributteInitializer<AddXhtmlParameterAttribute>(att => DealWith_AddXhtmlParameter(att as AddXhtmlParameterAttribute));
            AddAttributteInitializer<AddAspectScoringParameterAttribute>(att => DealWith_AddAspectScoringParameter(att as AddAspectScoringParameterAttribute));
            AddAttributteInitializer<AddConceptAttribute>(att => DealWith_AddConcept(att as AddConceptAttribute));
            AddAttributteInitializer<AddTreeAttribute>(att => DealWith_AddTree(att as AddTreeAttribute));
        }

        private void DealWith_AddParameter(AddParameterAttribute att)
        {
            if (_fakeParamSetHandler == null)
                _fakeParamSetHandler = new HandleFakeParameterSet();

            _parameterAttributes.Add(att);
        }

        private void DealWith_AddXhtmlParameter(AddXhtmlParameterAttribute att)
        {
            if (_fakeParamSetHandler == null)
                _fakeParamSetHandler = new HandleFakeParameterSet();

            _parameterAttributes.Add(att);
        }

        private void DealWith_AddAspectScoringParameter(AddAspectScoringParameterAttribute att)
        {
            if (_fakeParamSetHandler == null)
                _fakeParamSetHandler = new HandleFakeParameterSet();

            _parameterAttributes.Add(att);
        }

        private void DealWith_AddConcept(AddConceptAttribute att)
        {
            if (_fakeConceptHandler == null)
                _fakeConceptHandler = new HandleFakeConcept();
            _fakeConceptHandler.DealWith(att);
        }

        private void DealWith_AddTree(AddTreeAttribute att)
        {
            if (_fakeTreeHandler == null)
                _fakeTreeHandler = new HandleFakeTree();
            _fakeTreeHandler.DealWith(att);
        }

        public void InjectStuff(object o)
        {
            LocatorBootstrapper.EnsureLocatorBootstrapper().ComposeParts(o);
        }


        [TestInitialize]
        public virtual void Initialize()
        {
            FakeDal.Init();
            _fake = A.Fake<IItemEditorViewModel>();
            _fake.ItemResourceEntity.DataValue = new ItemResourceEntity();
            _fake.ResourceManager.DataValue = FakeDal.GetFakeResourceManager();

            _parameterAttributes.Sort((p1, p2) => p1.Order.CompareTo(p2.Order)); _parameterAttributes.ForEach(p => _fakeParamSetHandler.DealWith(p));
            if (_fakeParamSetHandler != null)
                _fakeParamSetHandler.GetResult(_fake.ParameterSetCollection);


            if (_fakeConceptHandler != null)
                _fakeConceptHandler.SetFakeBehavior(_fake);

            if (_fakeTreeHandler != null)
                _fakeTreeHandler.SetFakeBehavior(_fake);


            _fakeAssessmentItem.InitializeFakes(_fake);


            _viewAwareStatus = new TestViewAwareStatus();
            var fakeView = A.Fake<IWorkSpaceAware>();

            SetFakeViewDataContext(ref fakeView, _fake);
            Debug.Assert(fakeView.WorkSpaceContextualData != null, "You should had correctly initialized the view");

            _viewAwareStatus.View = fakeView;

            LocatorBootstrapper.ApplyComposer(
    new MyComposer(GetTypesForInjection())
    );

            typeof(ViewModelRepository).GetField("instance", BindingFlags.Static | BindingFlags.NonPublic).SetValue(null, null);

            if (Application.Current != null) Application.Current.Resources = new ResourceDictionary();
            Bootstrapper.InitLanguageAndResources();
        }

        internal abstract void SetFakeViewDataContext(ref IWorkSpaceAware fakeView, IItemEditorViewModel itemEditorViewModel);

        protected abstract IEnumerable<System.ComponentModel.Composition.Primitives.ComposablePartCatalog> GetTypesForInjection();

        [TestCleanup]
        public virtual void Clean()
        {
            _fake = null;
            _fakeParamSetHandler = null;
            _fakeConceptHandler = null;
            _fakeTreeHandler = null;
            FakeDal.Deinit();
        }

        internal IItemEditorViewModel FakeItemEditorVM { get { return _fake; } }

        public Cinch.TestViewAwareStatus ViewAwareStatus { get { return _viewAwareStatus; } }


        internal HandleFakeConcept FakeConceptHandler
        {
            get { return _fakeConceptHandler; }
        }


        protected enum ResourceEntityType { Item, ItemTemplate, ControlTemplate, Stylesheet, Image };


        protected void AddResource(string name, string data, ResourceEntityType type)
        {
            if (!_resources.ContainsKey(name))
            {
                _resources.Add(name, data);
                switch (type)
                {
                    case ResourceEntityType.Item:
                        FakeDal.Add.Item(name, SetData);
                        break;
                    case ResourceEntityType.ItemTemplate:
                        FakeDal.Add.ItemTemplate(name, SetData);
                        break;
                    case ResourceEntityType.ControlTemplate:
                        FakeDal.Add.ItemTemplate(name, SetData);
                        break;
                    case ResourceEntityType.Image:
                        FakeDal.Add.Image(name, SetData);
                        break;
                    case ResourceEntityType.Stylesheet:
                        FakeDal.Add.StyleSheet(name, data);
                        break;
                }

            }
            else
            {
                throw new Exception("entity already in dictionary!");
            }
        }

        protected void SetData(ResourceEntity r)
        {
            Debug.Assert(!string.IsNullOrEmpty(r.Name));
            Debug.Assert(_resources.ContainsKey(r.Name), "Name should be the same as file in data. Is file set to Copy always????");
            var xml = XElement.Parse(_resources[r.Name]);
            SetXmlAsBinData(r, xml);
        }

        protected void SetXmlAsBinData(ResourceEntity resource, XElement data)
        {
            if ((resource.ResourceData == null))
                resource.ResourceData = new ResourceDataEntity();

            using (var memStream = new MemoryStream())
            {
                SerializeHelper.XmlSerializeToStream(memStream, data);
                resource.ResourceData.BinData = memStream.ToArray();
            }
        }
    }
}
