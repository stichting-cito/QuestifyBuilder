
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Cinch;
using Cito.Tester.ContentModel;
using Enums;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.Logic.Service.Interfaces;
using Questify.Builder.Logic.Service.Model.Entities;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.Views;
using Questify.Builder.UnitTests.Fakes;
using Questify.Builder.UnitTests.Framework.FakeAppTemplate;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Presentation.ItemEditor
{
    [TestClass]
    public class ItemEditor_TemplateSwitchingTests : UsesTheItemEditorVM
    {
        [TestMethod, TestCategory("ViewModel"), TestCategory("ItemProcessing"), TestCategory("Logic")]
        [Description("Testing if SaveNeeded stays false after canceling a template switch. Test made for TFS #26272")]
        [AddParameter(typeof(BooleanParameter), Name="dualColumnLayout")]
        [AddParameter(typeof(MultiChoiceScoringParameter), Name="multiChoiceScoring")]
        [AddXhtmlParameter(XHtmlParamLeftBody)]
        [AddXhtmlParameter(XHtmlParamItemBody)]
        public void NoSaveNeeded_AfterCancelSwitching()
        {
            // Arrange
            var msgBoxService = A.Fake<IMessageBoxService>();
            var selectDlgFactory = FakeSelectDialogFactory.MakeNewFake();
            var selectIltDialog = A.Fake<ISelectIltDialog>();
            var iltSCId = Guid.NewGuid();
                
            
            // Source template for switching.
            FakeDal.Add.ItemTemplate("Cito.Generic.MC.DC", ilt => SetXmlAsBinData(ilt, XElement.Parse(Properties.Resources.CitoGenericMcDc)))
                .DependsOn.ControlTemplate("Cito.Generic.Interaction.MC", ct => SetXmlAsBinData(ct, XElement.Parse(Properties.Resources.CitoGenericInteractionMc)));
            FakeDal.Add.ItemTemplate("Cito.Generic.MC.SC", ilt => { SetXmlAsBinData(ilt, XElement.Parse(Properties.Resources.CitoGenericMcSc)); ilt.ResourceId = iltSCId; })
                .DependsOn.ControlTemplate("Cito.Generic.Interaction.MC");

            A.CallTo(() => selectDlgFactory.GetSelectItemLayoutTemplate(A<int>.Ignored, A<List<ItemTypeEnum>>.Ignored, A<bool>.Ignored, A<string>.Ignored)).
                ReturnsLazily(args => selectIltDialog);
            A.CallTo(() => selectIltDialog.SelectedEntity).ReturnsLazily(args => 
                new ItemLayoutTemplateResourceDto
                {
                    ResourceId = iltSCId,
                    Name = "Cito.Generic.MC.SC"
                }
            );

            A.CallTo(() => selectIltDialog.ShowDialog()).ReturnsLazily(args => System.Windows.Forms.DialogResult.OK);
            
            var item = CreateItem("item1", XElement.Parse(Properties.Resources.item_1), "Cito.Generic.MC.DC");
            FakeItemEditorVM.ItemResourceEntity.DataValue = item;
            FakeItemEditorVM.AssessmentItem.DataValue = item.GetAssessmentFromItemResource();

            var metaDataVM = new MetaDataViewModel(ViewAwareStatus, msgBoxService, selectDlgFactory);
            ViewAwareStatus.SimulateViewIsLoadedEvent();

            // Act
            metaDataVM.SwitchItemLayoutTemplate();

            // Assert
            Assert.AreEqual(false, FakeItemEditorVM.SaveNeeded.DataValue);
        }

        internal override void SetFakeViewDataContext(ref IWorkSpaceAware fakeView, IItemEditorViewModel itemEditorViewModel)
        {
            fakeView = A.Fake<IMetaDataControl>();
            fakeView.WorkSpaceContextualData.DataValue = itemEditorViewModel;
        }

        protected override IEnumerable<System.ComponentModel.Composition.Primitives.ComposablePartCatalog> GetTypesForInjection()
        {
            return new[] { MyComposer.GetCustomUITypes(), MyComposer.GetTestTypesForCinch() };
        }

        private ItemResourceEntity CreateItem(string name, XElement assessment, string iltName)
        {
            // Get Itemlayout
            var col = FakeDal.FakeServices.FakeResourceService.GetItemLayoutTemplatesForBank(0);
            var ilt = col.OfType<ItemLayoutTemplateResourceEntity>().FirstOrDefault(i => i.Name.Equals(iltName, StringComparison.InvariantCultureIgnoreCase));
            if (ilt == null) Assert.Fail("Itemlayouttemplate not found");

            ItemResourceEntity newItem = null;
            FakeDal.Add.Item(name, i => 
                {
                    i.ResourceId = Guid.NewGuid();
                    i.IsNew = false;
                    newItem = i;
                }).DependsOn.ItemTemplate("Cito.Generic.MC.DC");

            SetXmlAsBinData(newItem, assessment);

            return newItem;
        }

        public const string XHtmlParamLeftBody = @"<XHtmlParameter xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" name=""leftBody"">
	                                                            <p id=""c1-id-8"">some text</p>
                                                            </XHtmlParameter>";

        public const string XHtmlParamItemBody = @"<XHtmlParameter xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" name=""itemBody"">
	                                                            <p id=""c1-id-8"">some text</p>
                                                            </XHtmlParameter>";
    }
}
