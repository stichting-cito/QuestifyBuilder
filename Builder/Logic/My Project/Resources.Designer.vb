
Option Strict On
Option Explicit On

Imports System

Namespace My.Resources

    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0"), _
Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), _
Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute(), _
Global.Microsoft.VisualBasic.HideModuleNameAttribute()> _
    Public Module Resources

        Private resourceMan As Global.System.Resources.ResourceManager

        Private resourceCulture As Global.System.Globalization.CultureInfo

        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)> _
        Public ReadOnly Property ResourceManager() As Global.System.Resources.ResourceManager
            Get
                If Object.ReferenceEquals(resourceMan, Nothing) Then
                    Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("Questify.Builder.Logic.Resources", GetType(Resources).Assembly)
                    resourceMan = temp
                End If
                Return resourceMan
            End Get
        End Property

        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)> _
        Public Property Culture() As Global.System.Globalization.CultureInfo
            Get
                Return resourceCulture
            End Get
            Set
                resourceCulture = value
            End Set
        End Property

        Public ReadOnly Property AlternativesCount() As String
            Get
                Return ResourceManager.GetString("AlternativesCount", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ApplicableToMask() As String
            Get
                Return ResourceManager.GetString("ApplicableToMask", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property AspectGrid() As String
            Get
                Return ResourceManager.GetString("AspectGrid", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Aspects() As String
            Get
                Return ResourceManager.GetString("Aspects", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property BankId() As String
            Get
                Return ResourceManager.GetString("BankId", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property BankName() As String
            Get
                Return ResourceManager.GetString("BankName", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property BusyEncryptingDocument() As String
            Get
                Return ResourceManager.GetString("BusyEncryptingDocument", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property CannotAddExtensionFileAlreadyExists() As String
            Get
                Return ResourceManager.GetString("CannotAddExtensionFileAlreadyExists", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property CannotCopyPasteInlineControls() As String
            Get
                Return ResourceManager.GetString("CannotCopyPasteInlineControls", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property CannotPasteImagesOutsideBankHierarchy() As String
            Get
                Return ResourceManager.GetString("CannotPasteImagesOutsideBankHierarchy", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property CannotStartExport() As String
            Get
                Return ResourceManager.GetString("CannotStartExport", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property CannotStartExport1() As String
            Get
                Return ResourceManager.GetString("CannotStartExport1", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property CannotStartPublication() As String
            Get
                Return ResourceManager.GetString("CannotStartPublication", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Category_AssessmentItem() As String
            Get
                Return ResourceManager.GetString("Category_AssessmentItem", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Category_Solution() As String
            Get
                Return ResourceManager.GetString("Category_Solution", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property CodeNotFound() As String
            Get
                Return ResourceManager.GetString("CodeNotFound", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ConceptScoringReportColumnCaption_AdditionalKeyValuesAndConceptScores() As String
            Get
                Return ResourceManager.GetString("ConceptScoringReportColumnCaption_AdditionalKeyValuesAndConceptScores", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ConceptScoringReportColumnCaption_AttributeLevelConceptResponseCount() As String
            Get
                Return ResourceManager.GetString("ConceptScoringReportColumnCaption_AttributeLevelConceptResponseCount", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ConceptScoringReportColumnCaption_BankName() As String
            Get
                Return ResourceManager.GetString("ConceptScoringReportColumnCaption_BankName", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ConceptScoringReportColumnCaption_ConceptCode() As String
            Get
                Return ResourceManager.GetString("ConceptScoringReportColumnCaption_ConceptCode", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ConceptScoringReportColumnCaption_ConceptResponseLabel() As String
            Get
                Return ResourceManager.GetString("ConceptScoringReportColumnCaption_ConceptResponseLabel", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ConceptScoringReportColumnCaption_GroupElementCount() As String
            Get
                Return ResourceManager.GetString("ConceptScoringReportColumnCaption_GroupElementCount", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ConceptScoringReportColumnCaption_InteractionCount() As String
            Get
                Return ResourceManager.GetString("ConceptScoringReportColumnCaption_InteractionCount", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ConceptScoringReportColumnCaption_IsGrouped() As String
            Get
                Return ResourceManager.GetString("ConceptScoringReportColumnCaption_IsGrouped", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ConceptScoringReportColumnCaption_ItemCode() As String
            Get
                Return ResourceManager.GetString("ConceptScoringReportColumnCaption_ItemCode", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ConceptScoringReportColumnCaption_Itemlayouttemplate() As String
            Get
                Return ResourceManager.GetString("ConceptScoringReportColumnCaption_Itemlayouttemplate", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ConceptScoringReportColumnCaption_ItemTitle() As String
            Get
                Return ResourceManager.GetString("ConceptScoringReportColumnCaption_ItemTitle", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ConceptScoringReportColumnCaption_KeyValuesAndConceptScores() As String
            Get
                Return ResourceManager.GetString("ConceptScoringReportColumnCaption_KeyValuesAndConceptScores", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ConceptScoringReportColumnCaption_SubAttributLevelConceptResponseCount() As String
            Get
                Return ResourceManager.GetString("ConceptScoringReportColumnCaption_SubAttributLevelConceptResponseCount", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ControlTemplateGrid() As String
            Get
                Return ResourceManager.GetString("ControlTemplateGrid", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property CoordinateCorrectResponseNotValid() As String
            Get
                Return ResourceManager.GetString("CoordinateCorrectResponseNotValid", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property CorrectResponseNotInChoiceCollection() As String
            Get
                Return ResourceManager.GetString("CorrectResponseNotInChoiceCollection", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property CreatedBy() As String
            Get
                Return ResourceManager.GetString("CreatedBy", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property CreatedByFullName() As String
            Get
                Return ResourceManager.GetString("CreatedByFullName", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property CreatingScreenshot() As String
            Get
                Return ResourceManager.GetString("CreatingScreenshot", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property CreationDate() As String
            Get
                Return ResourceManager.GetString("CreationDate", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property CSVFileFilter() As String
            Get
                Return ResourceManager.GetString("CSVFileFilter", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property CustomPropertiesCount() As String
            Get
                Return ResourceManager.GetString("CustomPropertiesCount", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property CustomPropertyName() As String
            Get
                Return ResourceManager.GetString("CustomPropertyName", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property CustomPropertyTitle() As String
            Get
                Return ResourceManager.GetString("CustomPropertyTitle", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property CustomPropertyType() As String
            Get
                Return ResourceManager.GetString("CustomPropertyType", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property DataSourceGrid() As String
            Get
                Return ResourceManager.GetString("DataSourceGrid", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property DataTableKey1() As String
            Get
                Return ResourceManager.GetString("DataTableKey1", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property DataTableKey2() As String
            Get
                Return ResourceManager.GetString("DataTableKey2", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property DecimalScoringNotValid() As String
            Get
                Return ResourceManager.GetString("DecimalScoringNotValid", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Description() As String
            Get
                Return ResourceManager.GetString("Description", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property DescriptionItemKeyValidator() As String
            Get
                Return ResourceManager.GetString("DescriptionItemKeyValidator", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property DescriptionResourceStateValidator() As String
            Get
                Return ResourceManager.GetString("DescriptionResourceStateValidator", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property DuplicateKey() As String
            Get
                Return ResourceManager.GetString("DuplicateKey", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Error_DependentResourceNotFound() As String
            Get
                Return ResourceManager.GetString("Error_DependentResourceNotFound", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Error_ManifestResourceHandler_ClearCache_CacheDirectoryError() As String
            Get
                Return ResourceManager.GetString("Error_ManifestResourceHandler_ClearCache_CacheDirectoryError", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Error_ManifestResourceManager_CannotGetResource() As String
            Get
                Return ResourceManager.GetString("Error_ManifestResourceManager_CannotGetResource", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Error_ManifestResourceManager_CopyMediaToCache_ErrorWhileWritingToCache() As String
            Get
                Return ResourceManager.GetString("Error_ManifestResourceManager_CopyMediaToCache_ErrorWhileWritingToCache", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Error_ManifestResourceManager_EmptyStream() As String
            Get
                Return ResourceManager.GetString("Error_ManifestResourceManager_EmptyStream", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Error_PackageManager_GetStream_UriNotRelative() As String
            Get
                Return ResourceManager.GetString("Error_PackageManager_GetStream_UriNotRelative", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Error_PackageManager_GetStreamRelativeUri_UriNotSet() As String
            Get
                Return ResourceManager.GetString("Error_PackageManager_GetStreamRelativeUri_UriNotSet", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Error_ResourceEntriesInCacheCollection_itemParNotSet() As String
            Get
                Return ResourceManager.GetString("Error_ResourceEntriesInCacheCollection_itemParNotSet", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Error_ResourceEntry_Constructor_DependentResources_Empty() As String
            Get
                Return ResourceManager.GetString("Error_ResourceEntry_Constructor_DependentResources_Empty", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Error_ResourceEntry_Constructor_Name_Empty() As String
            Get
                Return ResourceManager.GetString("Error_ResourceEntry_Constructor_Name_Empty", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Error_ResourceEntry_Constructor_Type_Empty() As String
            Get
                Return ResourceManager.GetString("Error_ResourceEntry_Constructor_Type_Empty", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Error_ResourceEntry_Constructor_Uri_Empty() As String
            Get
                Return ResourceManager.GetString("Error_ResourceEntry_Constructor_Uri_Empty", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Error_ResourceManifest_Load_StreamNotSet() As String
            Get
                Return ResourceManager.GetString("Error_ResourceManifest_Load_StreamNotSet", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Error_UriNotSet() As String
            Get
                Return ResourceManager.GetString("Error_UriNotSet", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ErrorMessage() As String
            Get
                Return ResourceManager.GetString("ErrorMessage", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ErrorOccured() As String
            Get
                Return ResourceManager.GetString("ErrorOccured", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ErrorOccurred() As String
            Get
                Return ResourceManager.GetString("ErrorOccurred", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ErrorWhileExportingToExcelFileMightBeInUse() As String
            Get
                Return ResourceManager.GetString("ErrorWhileExportingToExcelFileMightBeInUse", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ErrorWhilePreviewIsPrepared() As String
            Get
                Return ResourceManager.GetString("ErrorWhilePreviewIsPrepared", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ErrorWhileProcessingMetadataFile() As String
            Get
                Return ResourceManager.GetString("ErrorWhileProcessingMetadataFile", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ExclusionGroupCode() As String
            Get
                Return ResourceManager.GetString("ExclusionGroupCode", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Export() As String
            Get
                Return ResourceManager.GetString("Export", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ExportAlreadyExistsSelectOtherExportLocation() As String
            Get
                Return ResourceManager.GetString("ExportAlreadyExistsSelectOtherExportLocation", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ExportCustomPropertyReportDescription() As String
            Get
                Return ResourceManager.GetString("ExportCustomPropertyReportDescription", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ExportGridDescription() As String
            Get
                Return ResourceManager.GetString("ExportGridDescription", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ExportGridName() As String
            Get
                Return ResourceManager.GetString("ExportGridName", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ExtensionShouldBeZip() As String
            Get
                Return ResourceManager.GetString("ExtensionShouldBeZip", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ExtraOptionConceptcodesTask() As String
            Get
                Return ResourceManager.GetString("ExtraOptionConceptcodesTask", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ExtraOptionConceptcodesTaskDescription() As String
            Get
                Return ResourceManager.GetString("ExtraOptionConceptcodesTaskDescription", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ExtraOptionTask() As String
            Get
                Return ResourceManager.GetString("ExtraOptionTask", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ExtraOptionTaskDescription() As String
            Get
                Return ResourceManager.GetString("ExtraOptionTaskDescription", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property FieldEmpty() As String
            Get
                Return ResourceManager.GetString("FieldEmpty", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property File() As String
            Get
                Return ResourceManager.GetString("File", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property FileAlreadyExists() As String
            Get
                Return ResourceManager.GetString("FileAlreadyExists", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property FileExists() As String
            Get
                Return ResourceManager.GetString("FileExists", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property FileNameCustomReport() As String
            Get
                Return ResourceManager.GetString("FileNameCustomReport", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property FilenameNotValid() As String
            Get
                Return ResourceManager.GetString("FilenameNotValid", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property FolderContainsInvalidCharacters() As String
            Get
                Return ResourceManager.GetString("FolderContainsInvalidCharacters", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property FormatTitle() As String
            Get
                Return ResourceManager.GetString("FormatTitle", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Formula() As String
            Get
                Return ResourceManager.GetString("Formula", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Ie9Required() As String
            Get
                Return ResourceManager.GetString("Ie9Required", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ImageReportNameItem() As String
            Get
                Return ResourceManager.GetString("ImageReportNameItem", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ImageReportNameTest() As String
            Get
                Return ResourceManager.GetString("ImageReportNameTest", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property InclusionGroupCode() As String
            Get
                Return ResourceManager.GetString("InclusionGroupCode", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Index() As String
            Get
                Return ResourceManager.GetString("Index", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Index1() As String
            Get
                Return ResourceManager.GetString("Index1", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property InlineChoice() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("InlineChoice", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property InlineElementRemovedFromParameter() As String
            Get
                Return ResourceManager.GetString("InlineElementRemovedFromParameter", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property InlineEssay() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("InlineEssay", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property InlineGap() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("InlineGap", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property InlineGapMatch() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("InlineGapMatch", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property InlineHTSC() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("InlineHTSC", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property InlineImageParameterSet() As String
            Get
                Return ResourceManager.GetString("InlineImageParameterSet", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property InlineMC() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("InlineMC", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property InlineMR() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("InlineMR", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property InlineTabular() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("InlineTabular", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property IntegerScoringNotValid() As String
            Get
                Return ResourceManager.GetString("IntegerScoringNotValid", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property IsAnchorItem() As String
            Get
                Return ResourceManager.GetString("IsAnchorItem", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property IsEnteredMessage() As String
            Get
                Return ResourceManager.GetString("IsEnteredMessage", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property IsSystemItem() As String
            Get
                Return ResourceManager.GetString("IsSystemItem", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Item() As String
            Get
                Return ResourceManager.GetString("Item", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ItemActive() As String
            Get
                Return ResourceManager.GetString("ItemActive", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ItemCode() As String
            Get
                Return ResourceManager.GetString("ItemCode", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ItemFunctionalType() As String
            Get
                Return ResourceManager.GetString("ItemFunctionalType", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ItemGrid() As String
            Get
                Return ResourceManager.GetString("ItemGrid", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ItemLayoutTemplateGrid() As String
            Get
                Return ResourceManager.GetString("ItemLayoutTemplateGrid", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ItemLayoutTemplateInlineMultimediaMissing() As String
            Get
                Return ResourceManager.GetString("ItemLayoutTemplateInlineMultimediaMissing", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ItemLayoutTemplateUsedName() As String
            Get
                Return ResourceManager.GetString("ItemLayoutTemplateUsedName", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ItemTemplateSwitchingUnableToLoadTemplate() As String
            Get
                Return ResourceManager.GetString("ItemTemplateSwitchingUnableToLoadTemplate", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ItemTypeFromItemLayoutTemplateString() As String
            Get
                Return ResourceManager.GetString("ItemTypeFromItemLayoutTemplateString", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property KeyValues() As String
            Get
                Return ResourceManager.GetString("KeyValues", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property LogicFileHelper_CodeMissingOrIncorrectExtension() As String
            Get
                Return ResourceManager.GetString("LogicFileHelper_CodeMissingOrIncorrectExtension", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property LogicFileHelper_ImageSizeTooLarge() As String
            Get
                Return ResourceManager.GetString("LogicFileHelper_ImageSizeTooLarge", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property MandatoryEmptyParameterMessage() As String
            Get
                Return ResourceManager.GetString("MandatoryEmptyParameterMessage", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property MandatoryEmptyShapelistMessage() As String
            Get
                Return ResourceManager.GetString("MandatoryEmptyShapelistMessage", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property MathFormulaImage() As String
            Get
                Return ResourceManager.GetString("MathFormulaImage", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property MathFormulaImageOfResource() As String
            Get
                Return ResourceManager.GetString("MathFormulaImageOfResource", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property MathML_NoPlugin() As String
            Get
                Return ResourceManager.GetString("MathML_NoPlugin", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property MathMlCorrectResponseNotValid() As String
            Get
                Return ResourceManager.GetString("MathMlCorrectResponseNotValid", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property MaxChoicesGreaterThanChoices() As String
            Get
                Return ResourceManager.GetString("MaxChoicesGreaterThanChoices", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property MaximumNumberSymbolreferencesReached() As String
            Get
                Return ResourceManager.GetString("MaximumNumberSymbolreferencesReached", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property MaxScore() As String
            Get
                Return ResourceManager.GetString("MaxScore", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Message_CreatingProposal_Error() As String
            Get
                Return ResourceManager.GetString("Message_CreatingProposal_Error", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Message_TestConstruction_AlreadyInItemContext() As String
            Get
                Return ResourceManager.GetString("Message_TestConstruction_AlreadyInItemContext", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Message_TestConstruction_DuplicateDataSource() As String
            Get
                Return ResourceManager.GetString("Message_TestConstruction_DuplicateDataSource", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Message_TestPackageConstruction_AlreadyInTestContext() As String
            Get
                Return ResourceManager.GetString("Message_TestPackageConstruction_AlreadyInTestContext", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Message_ValidateItemRelationShip_Conflicts() As String
            Get
                Return ResourceManager.GetString("Message_ValidateItemRelationShip_Conflicts", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Message_ValidateItemRelationShip_UnknownRequestType() As String
            Get
                Return ResourceManager.GetString("Message_ValidateItemRelationShip_UnknownRequestType", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property MinchoicesGreaterThanChoices() As String
            Get
                Return ResourceManager.GetString("MinchoicesGreaterThanChoices", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property MinchoicesGreaterThanMaxChoices() As String
            Get
                Return ResourceManager.GetString("MinchoicesGreaterThanMaxChoices", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property MissingGeogebraFile() As String
            Get
                Return ResourceManager.GetString("MissingGeogebraFile", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property MissingJsonManifest() As String
            Get
                Return ResourceManager.GetString("MissingJsonManifest", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property MissingJsonManifest1() As String
            Get
                Return ResourceManager.GetString("MissingJsonManifest1", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ModifiedBy() As String
            Get
                Return ResourceManager.GetString("ModifiedBy", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ModifiedByFullName() As String
            Get
                Return ResourceManager.GetString("ModifiedByFullName", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ModifiedDate() As String
            Get
                Return ResourceManager.GetString("ModifiedDate", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property MostRecentVersion() As String
            Get
                Return ResourceManager.GetString("MostRecentVersion", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property MultipleJsonManifest() As String
            Get
                Return ResourceManager.GetString("MultipleJsonManifest", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Name() As String
            Get
                Return ResourceManager.GetString("Name", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property NameItemKeyValidator() As String
            Get
                Return ResourceManager.GetString("NameItemKeyValidator", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property NameResourceStateValidator() As String
            Get
                Return ResourceManager.GetString("NameResourceStateValidator", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property NavigationNumber() As String
            Get
                Return ResourceManager.GetString("NavigationNumber", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property No() As String
            Get
                Return ResourceManager.GetString("No", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property NoCommonTargetFound() As String
            Get
                Return ResourceManager.GetString("NoCommonTargetFound", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property NoDependenciesAvailable() As String
            Get
                Return ResourceManager.GetString("NoDependenciesAvailable", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property NoGenericTestFound() As String
            Get
                Return ResourceManager.GetString("NoGenericTestFound", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property NoInfo() As String
            Get
                Return ResourceManager.GetString("NoInfo", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property nonqtischemas() As Byte()
            Get
                Dim obj As Object = ResourceManager.GetObject("nonqtischemas", resourceCulture)
                Return CType(obj, Byte())
            End Get
        End Property

        Public ReadOnly Property nonqtischemas_qti22() As Byte()
            Get
                Dim obj As Object = ResourceManager.GetObject("nonqtischemas_qti22", resourceCulture)
                Return CType(obj, Byte())
            End Get
        End Property

        Public ReadOnly Property NoProgrammInstalled() As String
            Get
                Return ResourceManager.GetString("NoProgrammInstalled", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property NoScoringAvailable() As String
            Get
                Return ResourceManager.GetString("NoScoringAvailable", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property NoThemeAvailable() As String
            Get
                Return ResourceManager.GetString("NoThemeAvailable", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property OnlyAValueWithinTheRangeOfTillIsAllowed() As String
            Get
                Return ResourceManager.GetString("OnlyAValueWithinTheRangeOfTillIsAllowed", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property OnlyImagesSupportCopyPaste() As String
            Get
                Return ResourceManager.GetString("OnlyImagesSupportCopyPaste", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property OriginalName() As String
            Get
                Return ResourceManager.GetString("OriginalName", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property OriginalVersion() As String
            Get
                Return ResourceManager.GetString("OriginalVersion", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Overview() As String
            Get
                Return ResourceManager.GetString("Overview", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property OverviewConceptScoring() As String
            Get
                Return ResourceManager.GetString("OverviewConceptScoring", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property OverviewGridToExcel() As String
            Get
                Return ResourceManager.GetString("OverviewGridToExcel", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ParameterWillBeDeletedMessage() As String
            Get
                Return ResourceManager.GetString("ParameterWillBeDeletedMessage", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property PerformCreationOfZipFile() As String
            Get
                Return ResourceManager.GetString("PerformCreationOfZipFile", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property PerformXsdValidation() As String
            Get
                Return ResourceManager.GetString("PerformXsdValidation", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property PerformXsdValidationOnFile() As String
            Get
                Return ResourceManager.GetString("PerformXsdValidationOnFile", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property PerformXslTransformation() As String
            Get
                Return ResourceManager.GetString("PerformXslTransformation", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property PlaceHolderHelper_NoSourceSelectedText() As String
            Get
                Return ResourceManager.GetString("PlaceHolderHelper_NoSourceSelectedText", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property PlaceHolderSpacer() As Byte()
            Get
                Dim obj As Object = ResourceManager.GetObject("PlaceHolderSpacer", resourceCulture)
                Return CType(obj, Byte())
            End Get
        End Property

        Public ReadOnly Property PleaseSelectAnExistingPath() As String
            Get
                Return ResourceManager.GetString("PleaseSelectAnExistingPath", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ProcessingItem() As String
            Get
                Return ResourceManager.GetString("ProcessingItem", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ProgressMessage() As String
            Get
                Return ResourceManager.GetString("ProgressMessage", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ProgressMessage1() As String
            Get
                Return ResourceManager.GetString("ProgressMessage1", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ProgressMessageConceptScoringReport() As String
            Get
                Return ResourceManager.GetString("ProgressMessageConceptScoringReport", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Properties() As String
            Get
                Return ResourceManager.GetString("Properties", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Property_AspectReference() As String
            Get
                Return ResourceManager.GetString("Property_AspectReference", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Property_Concept() As String
            Get
                Return ResourceManager.GetString("Property_Concept", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Property_Description() As String
            Get
                Return ResourceManager.GetString("Property_Description", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Property_Findings() As String
            Get
                Return ResourceManager.GetString("Property_Findings", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Property_Index() As String
            Get
                Return ResourceManager.GetString("Property_Index", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Property_MaxScore() As String
            Get
                Return ResourceManager.GetString("Property_MaxScore", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Property_MaxSolutionRawScore() As String
            Get
                Return ResourceManager.GetString("Property_MaxSolutionRawScore", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Property_Method() As String
            Get
                Return ResourceManager.GetString("Property_Method", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Property_ModifiedBy() As String
            Get
                Return ResourceManager.GetString("Property_ModifiedBy", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Property_Name() As String
            Get
                Return ResourceManager.GetString("Property_Name", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Property_RawScore() As String
            Get
                Return ResourceManager.GetString("Property_RawScore", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Property_StateName() As String
            Get
                Return ResourceManager.GetString("Property_StateName", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Property_Title() As String
            Get
                Return ResourceManager.GetString("Property_Title", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Property_TranslatedScore() As String
            Get
                Return ResourceManager.GetString("Property_TranslatedScore", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ProposalCreator_DuplicateItemsInMultipleDataSources() As String
            Get
                Return ResourceManager.GetString("ProposalCreator_DuplicateItemsInMultipleDataSources", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ProposalCreator_FailedToAddItems() As String
            Get
                Return ResourceManager.GetString("ProposalCreator_FailedToAddItems", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ProposalCreator_ItemsAlreadyInTest() As String
            Get
                Return ResourceManager.GetString("ProposalCreator_ItemsAlreadyInTest", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property PublicationLocationText() As String
            Get
                Return ResourceManager.GetString("PublicationLocationText", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property PublicationToFile() As String
            Get
                Return ResourceManager.GetString("PublicationToFile", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property PublicationToFileAndServer() As String
            Get
                Return ResourceManager.GetString("PublicationToFileAndServer", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property PublicationToServer() As String
            Get
                Return ResourceManager.GetString("PublicationToServer", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Publishable() As String
            Get
                Return ResourceManager.GetString("Publishable", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property PublishingPackageToSpecifiedLocation() As String
            Get
                Return ResourceManager.GetString("PublishingPackageToSpecifiedLocation", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property RawScore() As String
            Get
                Return ResourceManager.GetString("RawScore", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ReferenceCount() As String
            Get
                Return ResourceManager.GetString("ReferenceCount", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ReferencedFileFromJsonManifestNotFound() As String
            Get
                Return ResourceManager.GetString("ReferencedFileFromJsonManifestNotFound", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ReferencedFileNotFoundInImsManifest() As String
            Get
                Return ResourceManager.GetString("ReferencedFileNotFoundInImsManifest", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ReferencedFileNotFoundInJsonManifest() As String
            Get
                Return ResourceManager.GetString("ReferencedFileNotFoundInJsonManifest", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ReferenceTitle() As String
            Get
                Return ResourceManager.GetString("ReferenceTitle", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property RefreshUsingDataSources_CreateProposalsFromDataSourceList_ProposalPrefix() As String
            Get
                Return ResourceManager.GetString("RefreshUsingDataSources_CreateProposalsFromDataSourceList_ProposalPrefix", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Report() As String
            Get
                Return ResourceManager.GetString("Report", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ReportDescriptionConceptScoring() As String
            Get
                Return ResourceManager.GetString("ReportDescriptionConceptScoring", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ReportDescriptionDatasource() As String
            Get
                Return ResourceManager.GetString("ReportDescriptionDatasource", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ReportDescriptionItem() As String
            Get
                Return ResourceManager.GetString("ReportDescriptionItem", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ReportDescriptionLinkedAspects() As String
            Get
                Return ResourceManager.GetString("ReportDescriptionLinkedAspects", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ReportDescriptionTest() As String
            Get
                Return ResourceManager.GetString("ReportDescriptionTest", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ReportNameConceptScoring() As String
            Get
                Return ResourceManager.GetString("ReportNameConceptScoring", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ReportNameDatasource() As String
            Get
                Return ResourceManager.GetString("ReportNameDatasource", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ReportNameItem() As String
            Get
                Return ResourceManager.GetString("ReportNameItem", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ReportNameKenmerkenOverzicht() As String
            Get
                Return ResourceManager.GetString("ReportNameKenmerkenOverzicht", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ReportNameLinkedAspects() As String
            Get
                Return ResourceManager.GetString("ReportNameLinkedAspects", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ReportNameTest() As String
            Get
                Return ResourceManager.GetString("ReportNameTest", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ReportSuccesful() As String
            Get
                Return ResourceManager.GetString("ReportSuccesful", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ReportUnSuccesful() As String
            Get
                Return ResourceManager.GetString("ReportUnSuccesful", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property RequestContainsItemsValidationHandler_DoesNotContainItems() As String
            Get
                Return ResourceManager.GetString("RequestContainsItemsValidationHandler_DoesNotContainItems", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property RequestContainsTestsUsedAsRetry() As String
            Get
                Return ResourceManager.GetString("RequestContainsTestsUsedAsRetry", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property RequestContainsTestValidationHandler_DoesNotContainTest() As String
            Get
                Return ResourceManager.GetString("RequestContainsTestValidationHandler_DoesNotContainTest", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ResourceAndCustomBankPropertyMover_HierarchyAlreadyContainsCustomBankProperty() As String
            Get
                Return ResourceManager.GetString("ResourceAndCustomBankPropertyMover_HierarchyAlreadyContainsCustomBankProperty", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ResourceAndCustomBankPropertyMover_HierarchyAlreadyContainsResource() As String
            Get
                Return ResourceManager.GetString("ResourceAndCustomBankPropertyMover_HierarchyAlreadyContainsResource", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ResourceAndCustomBankPropertyMover_InsufficientPermissionsInDestinationBank() As String
            Get
                Return ResourceManager.GetString("ResourceAndCustomBankPropertyMover_InsufficientPermissionsInDestinationBank", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ResourceAndCustomBankPropertyMover_InsufficientPermissionsInSourceBank() As String
            Get
                Return ResourceManager.GetString("ResourceAndCustomBankPropertyMover_InsufficientPermissionsInSourceBank", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ResourceAndCustomBankPropertyMover_Moved() As String
            Get
                Return ResourceManager.GetString("ResourceAndCustomBankPropertyMover_Moved", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ResourceAndCustomBankPropertyMover_PropertyAlreadyExistsInBank() As String
            Get
                Return ResourceManager.GetString("ResourceAndCustomBankPropertyMover_PropertyAlreadyExistsInBank", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ResourceAndCustomBankPropertyMover_ResourceAlreadyExistsInBank() As String
            Get
                Return ResourceManager.GetString("ResourceAndCustomBankPropertyMover_ResourceAlreadyExistsInBank", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ResourceAndCustomBankPropertyMover_ResourceNotFound() As String
            Get
                Return ResourceManager.GetString("ResourceAndCustomBankPropertyMover_ResourceNotFound", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ResourceAndCustomBankPropertyMover_ResourceReferencedFromOtherSubBank() As String
            Get
                Return ResourceManager.GetString("ResourceAndCustomBankPropertyMover_ResourceReferencedFromOtherSubBank", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ResourceDoesNotExist() As String
            Get
                Return ResourceManager.GetString("ResourceDoesNotExist", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ResourceId() As String
            Get
                Return ResourceManager.GetString("ResourceId", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ResourcesCanBePublished() As String
            Get
                Return ResourceManager.GetString("ResourcesCanBePublished", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ResourcesCouldNotBeFound() As String
            Get
                Return ResourceManager.GetString("ResourcesCouldNotBeFound", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ResponseCount() As String
            Get
                Return ResourceManager.GetString("ResponseCount", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property schema_qti22() As Byte()
            Get
                Dim obj As Object = ResourceManager.GetObject("schema_qti22", resourceCulture)
                Return CType(obj, Byte())
            End Get
        End Property

        Public ReadOnly Property schema_qti30() As Byte()
            Get
                Dim obj As Object = ResourceManager.GetObject("schema_qti30", resourceCulture)
                Return CType(obj, Byte())
            End Get
        End Property

        Public ReadOnly Property ScoringEmpty() As String
            Get
                Return ResourceManager.GetString("ScoringEmpty", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Section() As String
            Get
                Return ResourceManager.GetString("Section", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property SolutionWillBeDeletedNoMatchPossible() As String
            Get
                Return ResourceManager.GetString("SolutionWillBeDeletedNoMatchPossible", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Sort() As String
            Get
                Return ResourceManager.GetString("Sort", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property State() As String
            Get
                Return ResourceManager.GetString("State", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property StateName() As String
            Get
                Return ResourceManager.GetString("StateName", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property SwitchingTemplateConsequencesWarning() As String
            Get
                Return ResourceManager.GetString("SwitchingTemplateConsequencesWarning", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TestConstructionValidator_Description() As String
            Get
                Return ResourceManager.GetString("TestConstructionValidator_Description", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TestConstructionValidator_Name() As String
            Get
                Return ResourceManager.GetString("TestConstructionValidator_Name", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TestConstructionValidator_TestContainsValidationErrors() As String
            Get
                Return ResourceManager.GetString("TestConstructionValidator_TestContainsValidationErrors", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TestEditor_InvalidAdd() As String
            Get
                Return ResourceManager.GetString("TestEditor_InvalidAdd", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TestGrid() As String
            Get
                Return ResourceManager.GetString("TestGrid", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TestPart() As String
            Get
                Return ResourceManager.GetString("TestPart", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TestPart1() As String
            Get
                Return ResourceManager.GetString("TestPart1", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TestTemplateGrid() As String
            Get
                Return ResourceManager.GetString("TestTemplateGrid", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TheFollowingResourcesPreventYouFromPublishingThisPackage() As String
            Get
                Return ResourceManager.GetString("TheFollowingResourcesPreventYouFromPublishingThisPackage", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TheFollowingResourcesResultInAWarning() As String
            Get
                Return ResourceManager.GetString("TheFollowingResourcesResultInAWarning", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ThisVersion() As String
            Get
                Return ResourceManager.GetString("ThisVersion", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Title() As String
            Get
                Return ResourceManager.GetString("Title", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Trace_ItemSelectorManager_GetItemReferenceAtIndex_NewItemPicked() As String
            Get
                Return ResourceManager.GetString("Trace_ItemSelectorManager_GetItemReferenceAtIndex_NewItemPicked", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Trace_ManifestResourceManager_ClearCache_CacheCleared() As String
            Get
                Return ResourceManager.GetString("Trace_ManifestResourceManager_ClearCache_CacheCleared", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Trace_ManifestResourceManager_ClearCache_CachedirNotFoundOrNoEntriesInCache() As String
            Get
                Return ResourceManager.GetString("Trace_ManifestResourceManager_ClearCache_CachedirNotFoundOrNoEntriesInCache", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Trace_ManifestResourceManager_CopyMediaToCache_AddedToCache() As String
            Get
                Return ResourceManager.GetString("Trace_ManifestResourceManager_CopyMediaToCache_AddedToCache", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Trace_ManifestResourceManager_CopyMediaToCache_CopyResources() As String
            Get
                Return ResourceManager.GetString("Trace_ManifestResourceManager_CopyMediaToCache_CopyResources", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Trace_ManifestResourceManager_GetFromCache_GetResourceStream() As String
            Get
                Return ResourceManager.GetString("Trace_ManifestResourceManager_GetFromCache_GetResourceStream", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Trace_ManifestResourceManager_GetFromCache_ProcessingStream() As String
            Get
                Return ResourceManager.GetString("Trace_ManifestResourceManager_GetFromCache_ProcessingStream", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Trace_ManifestResourceManager_GetResource_FoundInCache() As String
            Get
                Return ResourceManager.GetString("Trace_ManifestResourceManager_GetResource_FoundInCache", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Trace_ManifestResourceManager_GetResource_GetResource() As String
            Get
                Return ResourceManager.GetString("Trace_ManifestResourceManager_GetResource_GetResource", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Trace_ManifestResourceManager_GetResource_NotFoundInCache() As String
            Get
                Return ResourceManager.GetString("Trace_ManifestResourceManager_GetResource_NotFoundInCache", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Trace_ManifestResourceManager_GetResourceAndProcessStream_GetResourceStream() As String
            Get
                Return ResourceManager.GetString("Trace_ManifestResourceManager_GetResourceAndProcessStream_GetResourceStream", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Trace_ManifestResourceManager_GetResourceAndProcessStream_ProcessingStream() As String
            Get
                Return ResourceManager.GetString("Trace_ManifestResourceManager_GetResourceAndProcessStream_ProcessingStream", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Trace_ManifestResourceManager_GetTypedResource_GetResource() As String
            Get
                Return ResourceManager.GetString("Trace_ManifestResourceManager_GetTypedResource_GetResource", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property UploadPackage() As String
            Get
                Return ResourceManager.GetString("UploadPackage", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ValidatingResource() As String
            Get
                Return ResourceManager.GetString("ValidatingResource", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ValidationError() As String
            Get
                Return ResourceManager.GetString("ValidationError", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Version() As String
            Get
                Return ResourceManager.GetString("Version", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Warning() As String
            Get
                Return ResourceManager.GetString("Warning", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property WarningItemNoKey() As String
            Get
                Return ResourceManager.GetString("WarningItemNoKey", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property WarningItemsWithoutKeys() As String
            Get
                Return ResourceManager.GetString("WarningItemsWithoutKeys", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Weight() As String
            Get
                Return ResourceManager.GetString("Weight", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property WouldYouLikeToOpen() As String
            Get
                Return ResourceManager.GetString("WouldYouLikeToOpen", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property XsdValidationFile() As String
            Get
                Return ResourceManager.GetString("XsdValidationFile", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Yes() As String
            Get
                Return ResourceManager.GetString("Yes", resourceCulture)
            End Get
        End Property
    End Module
End Namespace
