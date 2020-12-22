
Option Strict On
Option Explicit On

Imports System

Namespace My.Resources

    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0"), _
Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), _
Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute(), _
Global.Microsoft.VisualBasic.HideModuleNameAttribute()> _
    Friend Module Resources

        Private resourceMan As Global.System.Resources.ResourceManager

        Private resourceCulture As Global.System.Globalization.CultureInfo

        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)> _
        Friend ReadOnly Property ResourceManager() As Global.System.Resources.ResourceManager
            Get
                If Object.ReferenceEquals(resourceMan, Nothing) Then
                    Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("Questify.Builder.Plugins.PaperBased.Resources", GetType(Resources).Assembly)
                    resourceMan = temp
                End If
                Return resourceMan
            End Get
        End Property

        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)> _
        Friend Property Culture() As Global.System.Globalization.CultureInfo
            Get
                Return resourceCulture
            End Get
            Set
                resourceCulture = value
            End Set
        End Property

        Friend ReadOnly Property _Default() As Byte()
            Get
                Dim obj As Object = ResourceManager.GetObject("_Default", resourceCulture)
                Return CType(obj, Byte())
            End Get
        End Property

        Friend ReadOnly Property add_icon_16() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("add_icon_16", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Friend ReadOnly Property AlternativesCount() As String
            Get
                Return ResourceManager.GetString("AlternativesCount", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Aspect() As String
            Get
                Return ResourceManager.GetString("Aspect", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property AssessmentTestViewBase_FriendlyEntityName() As String
            Get
                Return ResourceManager.GetString("AssessmentTestViewBase_FriendlyEntityName", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property AssessmentTestViewType_Word() As String
            Get
                Return ResourceManager.GetString("AssessmentTestViewType_Word", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Bank() As String
            Get
                Return ResourceManager.GetString("Bank", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property BankId() As String
            Get
                Return ResourceManager.GetString("BankId", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property CannotStartExport() As String
            Get
                Return ResourceManager.GetString("CannotStartExport", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property CannotStartPublication() As String
            Get
                Return ResourceManager.GetString("CannotStartPublication", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property CES() As String
            Get
                Return ResourceManager.GetString("CES", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ChoiceAlternatives() As String
            Get
                Return ResourceManager.GetString("ChoiceAlternatives", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ChoosePrintFormControl_LabelIsRequired() As String
            Get
                Return ResourceManager.GetString("ChoosePrintFormControl_LabelIsRequired", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ChoosePrintFormControl_LabelMaxLength() As String
            Get
                Return ResourceManager.GetString("ChoosePrintFormControl_LabelMaxLength", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ChoosePrintFormControl_LabelMayNotContainFollowingCharacters() As String
            Get
                Return ResourceManager.GetString("ChoosePrintFormControl_LabelMayNotContainFollowingCharacters", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ChoosePrintFormControl_TemplateIsRequired() As String
            Get
                Return ResourceManager.GetString("ChoosePrintFormControl_TemplateIsRequired", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Config() As String
            Get
                Return ResourceManager.GetString("Config", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Content() As String
            Get
                Return ResourceManager.GetString("Content", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ControlTemplate() As String
            Get
                Return ResourceManager.GetString("ControlTemplate", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property CreatedBy() As String
            Get
                Return ResourceManager.GetString("CreatedBy", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property CreatingScreenshot() As String
            Get
                Return ResourceManager.GetString("CreatingScreenshot", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property CreationDate() As String
            Get
                Return ResourceManager.GetString("CreationDate", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Description() As String
            Get
                Return ResourceManager.GetString("Description", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Dimension() As String
            Get
                Return ResourceManager.GetString("Dimension", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property DiskIsFull() As String
            Get
                Return ResourceManager.GetString("DiskIsFull", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ErrorOccured() As String
            Get
                Return ResourceManager.GetString("ErrorOccured", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ErrorsFoundInCollectionparameter() As String
            Get
                Return ResourceManager.GetString("ErrorsFoundInCollectionparameter", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ErrorWhileParsingTemplate() As String
            Get
                Return ResourceManager.GetString("ErrorWhileParsingTemplate", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ExtraOptionTask() As String
            Get
                Return ResourceManager.GetString("ExtraOptionTask", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ExtraOptionTaskDescription() As String
            Get
                Return ResourceManager.GetString("ExtraOptionTaskDescription", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property FACET() As String
            Get
                Return ResourceManager.GetString("FACET", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property FieldEmpty() As String
            Get
                Return ResourceManager.GetString("FieldEmpty", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property FieldMustBeFilled() As String
            Get
                Return ResourceManager.GetString("FieldMustBeFilled", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property FileAlreadyExist() As String
            Get
                Return ResourceManager.GetString("FileAlreadyExist", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property FileExists() As String
            Get
                Return ResourceManager.GetString("FileExists", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property FilenameNotValid() As String
            Get
                Return ResourceManager.GetString("FilenameNotValid", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property FolderContainsInvalidCharacters() As String
            Get
                Return ResourceManager.GetString("FolderContainsInvalidCharacters", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Formula() As String
            Get
                Return ResourceManager.GetString("Formula", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property IdentifierIsARequiredField() As String
            Get
                Return ResourceManager.GetString("IdentifierIsARequiredField", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ImageCouldNotBeFound() As String
            Get
                Return ResourceManager.GetString("ImageCouldNotBeFound", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ImageReportDescriptionItem() As String
            Get
                Return ResourceManager.GetString("ImageReportDescriptionItem", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ImageReportDescriptionTest() As String
            Get
                Return ResourceManager.GetString("ImageReportDescriptionTest", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ImageReportNameItem() As String
            Get
                Return ResourceManager.GetString("ImageReportNameItem", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ImageReportNameTest() As String
            Get
                Return ResourceManager.GetString("ImageReportNameTest", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property InvisibleParameterHasAChangedValue() As String
            Get
                Return ResourceManager.GetString("InvisibleParameterHasAChangedValue", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property IsSystemItem() As String
            Get
                Return ResourceManager.GetString("IsSystemItem", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Item() As String
            Get
                Return ResourceManager.GetString("Item", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ItemContent() As String
            Get
                Return ResourceManager.GetString("ItemContent", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ItemCustomProperties() As String
            Get
                Return ResourceManager.GetString("ItemCustomProperties", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ItemDependencies() As String
            Get
                Return ResourceManager.GetString("ItemDependencies", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ItemEditor_ValidateItem_ValidationErrors() As String
            Get
                Return ResourceManager.GetString("ItemEditor_ValidateItem_ValidationErrors", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ItemFunctionalTypeIsARequiredField() As String
            Get
                Return ResourceManager.GetString("ItemFunctionalTypeIsARequiredField", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ItemInformation() As String
            Get
                Return ResourceManager.GetString("ItemInformation", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ItemIsAdded() As String
            Get
                Return ResourceManager.GetString("ItemIsAdded", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ItemKeysWithPreprocessorRules() As String
            Get
                Return ResourceManager.GetString("ItemKeysWithPreprocessorRules", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ItemLayoutTemplate() As String
            Get
                Return ResourceManager.GetString("ItemLayoutTemplate", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ItemLayoutTemplateUsedName() As String
            Get
                Return ResourceManager.GetString("ItemLayoutTemplateUsedName", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ItemPlusCode() As String
            Get
                Return ResourceManager.GetString("ItemPlusCode", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ItemReferences() As String
            Get
                Return ResourceManager.GetString("ItemReferences", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ItemReferenceViewBase_FriendlyEntityName() As String
            Get
                Return ResourceManager.GetString("ItemReferenceViewBase_FriendlyEntityName", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ItemSolution() As String
            Get
                Return ResourceManager.GetString("ItemSolution", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property KeyValues() As String
            Get
                Return ResourceManager.GetString("KeyValues", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Label() As String
            Get
                Return ResourceManager.GetString("Label", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property MaxScore() As String
            Get
                Return ResourceManager.GetString("MaxScore", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Media() As String
            Get
                Return ResourceManager.GetString("Media", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ModifiedBy() As String
            Get
                Return ResourceManager.GetString("ModifiedBy", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ModifiedByFullName() As String
            Get
                Return ResourceManager.GetString("ModifiedByFullName", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ModifiedDate() As String
            Get
                Return ResourceManager.GetString("ModifiedDate", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Name() As String
            Get
                Return ResourceManager.GetString("Name", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Name1() As String
            Get
                Return ResourceManager.GetString("Name1", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property NameValue() As String
            Get
                Return ResourceManager.GetString("NameValue", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property NavigationModeIsARequiredField() As String
            Get
                Return ResourceManager.GetString("NavigationModeIsARequiredField", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property No() As String
            Get
                Return ResourceManager.GetString("No", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property NoCommonTargetFound() As String
            Get
                Return ResourceManager.GetString("NoCommonTargetFound", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property None() As String
            Get
                Return ResourceManager.GetString("None", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property NoPrintFormsAvailable() As String
            Get
                Return ResourceManager.GetString("NoPrintFormsAvailable", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property NoProgrammInstalled() As String
            Get
                Return ResourceManager.GetString("NoProgrammInstalled", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property NotApplicable() As String
            Get
                Return ResourceManager.GetString("NotApplicable", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property OrginalName() As String
            Get
                Return ResourceManager.GetString("OrginalName", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property OrginalVersion() As String
            Get
                Return ResourceManager.GetString("OrginalVersion", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property OriginalName() As String
            Get
                Return ResourceManager.GetString("OriginalName", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property OriginalVersion() As String
            Get
                Return ResourceManager.GetString("OriginalVersion", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property OverviewGridImage() As String
            Get
                Return ResourceManager.GetString("OverviewGridImage", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property OverviewGridToWord() As String
            Get
                Return ResourceManager.GetString("OverviewGridToWord", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property PackagedImageReportDescriptionItem() As String
            Get
                Return ResourceManager.GetString("PackagedImageReportDescriptionItem", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property PackagedImageReportDescriptionTest() As String
            Get
                Return ResourceManager.GetString("PackagedImageReportDescriptionTest", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property PackagedImageReportNameItem() As String
            Get
                Return ResourceManager.GetString("PackagedImageReportNameItem", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property PackagedImageReportNameTest() As String
            Get
                Return ResourceManager.GetString("PackagedImageReportNameTest", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property PathOrFilenameNotValid() As String
            Get
                Return ResourceManager.GetString("PathOrFilenameNotValid", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property PleaseEnterACompleteAndValidPath() As String
            Get
                Return ResourceManager.GetString("PleaseEnterACompleteAndValidPath", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property PleaseEnterAValidExportFilename() As String
            Get
                Return ResourceManager.GetString("PleaseEnterAValidExportFilename", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property PleaseSelectAnExistingPath() As String
            Get
                Return ResourceManager.GetString("PleaseSelectAnExistingPath", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property PleaseSelectAPrintForm() As String
            Get
                Return ResourceManager.GetString("PleaseSelectAPrintForm", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property PleaseSelectAtLeastOneInformationBlock() As String
            Get
                Return ResourceManager.GetString("PleaseSelectAtLeastOneInformationBlock", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property PleaseSelectAtLeatOneInfomationBlock() As String
            Get
                Return ResourceManager.GetString("PleaseSelectAtLeatOneInfomationBlock", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Plugin() As String
            Get
                Return ResourceManager.GetString("Plugin", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property PreprocessingRule() As String
            Get
                Return ResourceManager.GetString("PreprocessingRule", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property PrintFormIsARequiredField() As String
            Get
                Return ResourceManager.GetString("PrintFormIsARequiredField", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property PrintFormLabelsAreRequired() As String
            Get
                Return ResourceManager.GetString("PrintFormLabelsAreRequired", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property PrintFormLabelsMaxLength() As String
            Get
                Return ResourceManager.GetString("PrintFormLabelsMaxLength", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property PrintFormLabelsMayNotContainFollowingCharacters() As String
            Get
                Return ResourceManager.GetString("PrintFormLabelsMayNotContainFollowingCharacters", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property PrintFormLabelsMustBeUnique() As String
            Get
                Return ResourceManager.GetString("PrintFormLabelsMustBeUnique", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property PrintFormType_CorrectionBooklet() As String
            Get
                Return ResourceManager.GetString("PrintFormType_CorrectionBooklet", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property PrintFormType_MultiMediaInstructionBooklet() As String
            Get
                Return ResourceManager.GetString("PrintFormType_MultiMediaInstructionBooklet", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property PrintFormType_QuestionBooklet() As String
            Get
                Return ResourceManager.GetString("PrintFormType_QuestionBooklet", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property PrintFormType_SourceBooklet() As String
            Get
                Return ResourceManager.GetString("PrintFormType_SourceBooklet", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property PrintFormWordTemplatesAreRequired() As String
            Get
                Return ResourceManager.GetString("PrintFormWordTemplatesAreRequired", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ProcessingItem() As String
            Get
                Return ResourceManager.GetString("ProcessingItem", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ProgressMessage() As String
            Get
                Return ResourceManager.GetString("ProgressMessage", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Publication() As String
            Get
                Return ResourceManager.GetString("Publication", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property PublicationFolder() As String
            Get
                Return ResourceManager.GetString("PublicationFolder", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property PublicationLocationText() As String
            Get
                Return ResourceManager.GetString("PublicationLocationText", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property PublicationOptions() As String
            Get
                Return ResourceManager.GetString("PublicationOptions", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property PublishingDocumentToSpecifiedLocation() As String
            Get
                Return ResourceManager.GetString("PublishingDocumentToSpecifiedLocation", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property PublishToWord() As String
            Get
                Return ResourceManager.GetString("PublishToWord", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property RawScore() As String
            Get
                Return ResourceManager.GetString("RawScore", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property remove16() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("remove16", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Friend ReadOnly Property ReportDescriptionItem() As String
            Get
                Return ResourceManager.GetString("ReportDescriptionItem", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ReportDescriptionTest() As String
            Get
                Return ResourceManager.GetString("ReportDescriptionTest", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ReportNameItem() As String
            Get
                Return ResourceManager.GetString("ReportNameItem", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ReportNameTest() As String
            Get
                Return ResourceManager.GetString("ReportNameTest", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ResourceId() As String
            Get
                Return ResourceManager.GetString("ResourceId", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ResponseCount() As String
            Get
                Return ResourceManager.GetString("ResponseCount", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property SelectATarget() As String
            Get
                Return ResourceManager.GetString("SelectATarget", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property SelectedAnalysesFiles() As String
            Get
                Return ResourceManager.GetString("SelectedAnalysesFiles", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property SelectedFolderDoesNotExist() As String
            Get
                Return ResourceManager.GetString("SelectedFolderDoesNotExist", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property SelectedTarget() As String
            Get
                Return ResourceManager.GetString("SelectedTarget", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Selection() As String
            Get
                Return ResourceManager.GetString("Selection", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property SelectResourceDialog_CannotSelectBecauseOfStatus() As String
            Get
                Return ResourceManager.GetString("SelectResourceDialog_CannotSelectBecauseOfStatus", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property SelectResourceEntityDialogBase_OkButton_NoResourceSelectedMessage() As String
            Get
                Return ResourceManager.GetString("SelectResourceEntityDialogBase_OkButton_NoResourceSelectedMessage", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property SourceNameIsARequiredField() As String
            Get
                Return ResourceManager.GetString("SourceNameIsARequiredField", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property SpecifyPackageName() As String
            Get
                Return ResourceManager.GetString("SpecifyPackageName", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property State() As String
            Get
                Return ResourceManager.GetString("State", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property SuccessfullyGenerated() As String
            Get
                Return ResourceManager.GetString("SuccessfullyGenerated", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Test() As String
            Get
                Return ResourceManager.GetString("Test", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property TestEditorPlugin_Description() As String
            Get
                Return ResourceManager.GetString("TestEditorPlugin_Description", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property TestPartViewBase_FriendlyEntityName() As String
            Get
                Return ResourceManager.GetString("TestPartViewBase_FriendlyEntityName", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property TestPlayer() As String
            Get
                Return ResourceManager.GetString("TestPlayer", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property TestPluginError() As String
            Get
                Return ResourceManager.GetString("TestPluginError", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property TestPluginErrorCaption() As String
            Get
                Return ResourceManager.GetString("TestPluginErrorCaption", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property TestPreviewToWord() As String
            Get
                Return ResourceManager.GetString("TestPreviewToWord", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property TestSectionViewBase_FriendlyEntityName() As String
            Get
                Return ResourceManager.GetString("TestSectionViewBase_FriendlyEntityName", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property TheFilenameCannotContainIllegalCharacters() As String
            Get
                Return ResourceManager.GetString("TheFilenameCannotContainIllegalCharacters", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Theme() As String
            Get
                Return ResourceManager.GetString("Theme", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Title() As String
            Get
                Return ResourceManager.GetString("Title", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Title1() As String
            Get
                Return ResourceManager.GetString("Title1", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property TitleIsARequiredField() As String
            Get
                Return ResourceManager.GetString("TitleIsARequiredField", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Type() As String
            Get
                Return ResourceManager.GetString("Type", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ValidateParametersCollectionParameterWillShrink() As String
            Get
                Return ResourceManager.GetString("ValidateParametersCollectionParameterWillShrink", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ValidateParametersCollectionWillBeDeleted() As String
            Get
                Return ResourceManager.GetString("ValidateParametersCollectionWillBeDeleted", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ValidateParametersParameterHasADifferentType() As String
            Get
                Return ResourceManager.GetString("ValidateParametersParameterHasADifferentType", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ValidateParametersParameterWillBeDeleted() As String
            Get
                Return ResourceManager.GetString("ValidateParametersParameterWillBeDeleted", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Version() As String
            Get
                Return ResourceManager.GetString("Version", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property WarningsFoundInCollectionparameter() As String
            Get
                Return ResourceManager.GetString("WarningsFoundInCollectionparameter", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property WithoutNavigationbar() As String
            Get
                Return ResourceManager.GetString("WithoutNavigationbar", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Word() As String
            Get
                Return ResourceManager.GetString("Word", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Word_AssessmentTestPropertiesEditor_FrameTitle() As String
            Get
                Return ResourceManager.GetString("Word_AssessmentTestPropertiesEditor_FrameTitle", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Word_ItemReferencePropertiesEditor_FrameTitle() As String
            Get
                Return ResourceManager.GetString("Word_ItemReferencePropertiesEditor_FrameTitle", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Word_TestOutcomePropertiesEditor_FrameTitle() As String
            Get
                Return ResourceManager.GetString("Word_TestOutcomePropertiesEditor_FrameTitle", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Word_TestPackagePropertiesEditor_FrameTitle() As String
            Get
                Return ResourceManager.GetString("Word_TestPackagePropertiesEditor_FrameTitle", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Word_TestPartPropertiesEditor_FrameTitle() As String
            Get
                Return ResourceManager.GetString("Word_TestPartPropertiesEditor_FrameTitle", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Word_TestReferencePropertiesEditor_FrameTitle() As String
            Get
                Return ResourceManager.GetString("Word_TestReferencePropertiesEditor_FrameTitle", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Word_TestSectionPropertiesEditor_FrameTitle() As String
            Get
                Return ResourceManager.GetString("Word_TestSectionPropertiesEditor_FrameTitle", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Word_TestSetPropertiesEditor_FrameTitle() As String
            Get
                Return ResourceManager.GetString("Word_TestSetPropertiesEditor_FrameTitle", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property WouldYouLikeToOpen() As String
            Get
                Return ResourceManager.GetString("WouldYouLikeToOpen", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Yes() As String
            Get
                Return ResourceManager.GetString("Yes", resourceCulture)
            End Get
        End Property
    End Module
End Namespace
