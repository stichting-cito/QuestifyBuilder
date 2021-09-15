
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
    /// <summary>
    /// Helper class for all viewmodels that use the ViewModel
    /// 
    /// This class acts af if the ItemEditorViewmodel is present for your VM. 
    /// 
    /// You might want to use this class as a base class for your test.
    /// 
    /// use the following attribute for your class:
    ///     [AddParameter(typeof(MultiChoiceScoringParameter))] adds Parameters to the ParameterSet.
    /// 
    /// </summary>
    [TestClass]
    public abstract class UsesTheItemEditorVM : MVVMTestBase
    {
        private IItemEditorViewModel _fake;
        private TestViewAwareStatus _viewAwareStatus;

        //Handler
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

            _parameterAttributes.Add( att);
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
            
            _parameterAttributes.Sort((p1, p2) => p1.Order.CompareTo(p2.Order)); // Sort the list on the ordering parameter.
            _parameterAttributes.ForEach(p => _fakeParamSetHandler.DealWith(p)); // Add the parameters in the desired order.

            if (_fakeParamSetHandler != null)
                _fakeParamSetHandler.GetResult(_fake.ParameterSetCollection);


            if (_fakeConceptHandler != null)
                _fakeConceptHandler.SetFakeBehavior(_fake);

            if (_fakeTreeHandler != null)
                _fakeTreeHandler.SetFakeBehavior(_fake);


            _fakeAssessmentItem.InitializeFakes(_fake);


            _viewAwareStatus = new TestViewAwareStatus();
            var fakeView = A.Fake<IWorkSpaceAware>();

            //Init data for view
            SetFakeViewDataContext(ref fakeView, _fake);
            Debug.Assert(fakeView.WorkSpaceContextualData != null, "You should had correctly initialized the view");

            _viewAwareStatus.View = fakeView;

            // and one of these too! 
            LocatorBootstrapper.ApplyComposer(
                new MyComposer(GetTypesForInjection())
                );

            //Dark Magic :: We need to clear the instance by reflection because there is no other way. This instance has to be cleared because it conflicts with other instances.
            typeof(ViewModelRepository).GetField("instance", BindingFlags.Static | BindingFlags.NonPublic).SetValue(null, null);

            //Needed for resources.      
            if (Application.Current!=null) Application.Current.Resources = new ResourceDictionary();
            Bootstrapper.InitLanguageAndResources();
        }

        /// <summary>
        /// Sets the fake view data context.
        /// So your ViewModel uses the ItemEditorVM,.. ok but it might also use other stuff,.. so maybe you want to add this
        /// 
        /// example:
        /// fakeView.WorkSpaceContextualData.DataValue = itemEditorViewModel; //For plain vanilla using the ItemEditorVM
        /// or
        /// fakeView.WorkSpaceContextualData.DataValue = Tuple<IItemEditorViewModel, integer>(itemEditorViewModel, someint); //your specialized version.
        /// </summary>
        /// <param name="fakeView">The fake view.</param>
        /// <param name="itemEditorViewModel">The itemEditor view model.</param>
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

        /// <summary>
        /// Get the ViewAwareStatus. If your VM is loaded by a view that is loaded by WorkspaceData. This is the method to get th ItemEditorVM
        /// 
        /// Call the ".imulateViewIsLoadedEvent" and your viewVM should react on it. \
        /// 
        /// See : http://www.codeproject.com/Articles/96175/CinchV2-Version2-of-my-Cinch-MVVM-framework-part-3#Workspaces
        /// </summary>
        public Cinch.TestViewAwareStatus ViewAwareStatus { get { return _viewAwareStatus; } }


        /// <summary>
        /// Gets the fake concept handler.
        /// </summary>
        /// <value>The fake concept handler.</value>
        internal HandleFakeConcept FakeConceptHandler
        {
            get { return _fakeConceptHandler; }
        }


        protected enum ResourceEntityType { Item, ItemTemplate, ControlTemplate, Stylesheet, Image };


        protected void AddResource(string name, string data, ResourceEntityType type )
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
