
Option Strict On
Option Explicit On

Imports System

Namespace My.Resources

    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0"), _
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
                    Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("Questify.Builder.Plugins.Reports.Excel.Resources", GetType(Resources).Assembly)
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

        Friend ReadOnly Property AlternativesCount() As String
            Get
                Return ResourceManager.GetString("AlternativesCount", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ApplicableToMask() As String
            Get
                Return ResourceManager.GetString("ApplicableToMask", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property AspectGrid() As String
            Get
                Return ResourceManager.GetString("AspectGrid", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Aspects() As String
            Get
                Return ResourceManager.GetString("Aspects", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property BankId() As String
            Get
                Return ResourceManager.GetString("BankId", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property BankName() As String
            Get
                Return ResourceManager.GetString("BankName", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property CannotStartExport() As String
            Get
                Return ResourceManager.GetString("CannotStartExport", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ConceptScoringReportColumnCaption_AdditionalKeyValuesAndConceptScores() As String
            Get
                Return ResourceManager.GetString("ConceptScoringReportColumnCaption_AdditionalKeyValuesAndConceptScores", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ConceptScoringReportColumnCaption_AttributeLevelConceptResponseCount() As String
            Get
                Return ResourceManager.GetString("ConceptScoringReportColumnCaption_AttributeLevelConceptResponseCount", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ConceptScoringReportColumnCaption_BankName() As String
            Get
                Return ResourceManager.GetString("ConceptScoringReportColumnCaption_BankName", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ConceptScoringReportColumnCaption_ConceptCode() As String
            Get
                Return ResourceManager.GetString("ConceptScoringReportColumnCaption_ConceptCode", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ConceptScoringReportColumnCaption_ConceptResponseLabel() As String
            Get
                Return ResourceManager.GetString("ConceptScoringReportColumnCaption_ConceptResponseLabel", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ConceptScoringReportColumnCaption_GroupElementCount() As String
            Get
                Return ResourceManager.GetString("ConceptScoringReportColumnCaption_GroupElementCount", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ConceptScoringReportColumnCaption_InteractionCount() As String
            Get
                Return ResourceManager.GetString("ConceptScoringReportColumnCaption_InteractionCount", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ConceptScoringReportColumnCaption_IsGrouped() As String
            Get
                Return ResourceManager.GetString("ConceptScoringReportColumnCaption_IsGrouped", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ConceptScoringReportColumnCaption_ItemCode() As String
            Get
                Return ResourceManager.GetString("ConceptScoringReportColumnCaption_ItemCode", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ConceptScoringReportColumnCaption_ItemId() As String
            Get
                Return ResourceManager.GetString("ConceptScoringReportColumnCaption_ItemId", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ConceptScoringReportColumnCaption_Itemlayouttemplate() As String
            Get
                Return ResourceManager.GetString("ConceptScoringReportColumnCaption_Itemlayouttemplate", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ConceptScoringReportColumnCaption_ItemTitle() As String
            Get
                Return ResourceManager.GetString("ConceptScoringReportColumnCaption_ItemTitle", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ConceptScoringReportColumnCaption_KeyValuesAndConceptScores() As String
            Get
                Return ResourceManager.GetString("ConceptScoringReportColumnCaption_KeyValuesAndConceptScores", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ConceptScoringReportColumnCaption_SubAttributLevelConceptResponseCount() As String
            Get
                Return ResourceManager.GetString("ConceptScoringReportColumnCaption_SubAttributLevelConceptResponseCount", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ControlTemplateGrid() As String
            Get
                Return ResourceManager.GetString("ControlTemplateGrid", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property CreatedBy() As String
            Get
                Return ResourceManager.GetString("CreatedBy", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property CreationDate() As String
            Get
                Return ResourceManager.GetString("CreationDate", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property CSVFileFilter() As String
            Get
                Return ResourceManager.GetString("CSVFileFilter", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property CustomPropertiesCount() As String
            Get
                Return ResourceManager.GetString("CustomPropertiesCount", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property CustomPropertyName() As String
            Get
                Return ResourceManager.GetString("CustomPropertyName", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property CustomPropertyTitle() As String
            Get
                Return ResourceManager.GetString("CustomPropertyTitle", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property CustomPropertyType() As String
            Get
                Return ResourceManager.GetString("CustomPropertyType", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property DataSourceGrid() As String
            Get
                Return ResourceManager.GetString("DataSourceGrid", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property DatatableColumn_AssessmentTestCode() As String
            Get
                Return ResourceManager.GetString("DatatableColumn_AssessmentTestCode", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property DatatableColumn_AssessmentTestTitle() As String
            Get
                Return ResourceManager.GetString("DatatableColumn_AssessmentTestTitle", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property DatatableColumn_Code() As String
            Get
                Return ResourceManager.GetString("DatatableColumn_Code", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property DatatableColumn_FileHeight() As String
            Get
                Return ResourceManager.GetString("DatatableColumn_FileHeight", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property DatatableColumn_FileHeightInItem() As String
            Get
                Return ResourceManager.GetString("DatatableColumn_FileHeightInItem", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property DatatableColumn_Filesize() As String
            Get
                Return ResourceManager.GetString("DatatableColumn_Filesize", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property DatatableColumn_FileWidth() As String
            Get
                Return ResourceManager.GetString("DatatableColumn_FileWidth", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property DatatableColumn_FileWidthInItem() As String
            Get
                Return ResourceManager.GetString("DatatableColumn_FileWidthInItem", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property DatatableColumn_Itemcode() As String
            Get
                Return ResourceManager.GetString("DatatableColumn_Itemcode", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property DatatableColumn_ItemId() As String
            Get
                Return ResourceManager.GetString("DatatableColumn_ItemId", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property DatatableColumn_Itemtitle() As String
            Get
                Return ResourceManager.GetString("DatatableColumn_Itemtitle", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property DatatableColumn_MaxScore() As String
            Get
                Return ResourceManager.GetString("DatatableColumn_MaxScore", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property DatatableColumn_Mediacode() As String
            Get
                Return ResourceManager.GetString("DatatableColumn_Mediacode", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property DatatableColumn_Mediatitle() As String
            Get
                Return ResourceManager.GetString("DatatableColumn_Mediatitle", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property DatatableColumn_Mediatype() As String
            Get
                Return ResourceManager.GetString("DatatableColumn_Mediatype", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property DatatableColumn_References() As String
            Get
                Return ResourceManager.GetString("DatatableColumn_References", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property DatatableColumn_Title() As String
            Get
                Return ResourceManager.GetString("DatatableColumn_Title", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property DataTableKey1() As String
            Get
                Return ResourceManager.GetString("DataTableKey1", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Description() As String
            Get
                Return ResourceManager.GetString("Description", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ErrorOccured() As String
            Get
                Return ResourceManager.GetString("ErrorOccured", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ErrorWhileExportingToCsvFileFileMightBeInUse() As String
            Get
                Return ResourceManager.GetString("ErrorWhileExportingToCsvFileFileMightBeInUse", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ErrorWhileExportingToExcelFileMightBeInUse() As String
            Get
                Return ResourceManager.GetString("ErrorWhileExportingToExcelFileMightBeInUse", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ExportCustomPropertyReportDescription() As String
            Get
                Return ResourceManager.GetString("ExportCustomPropertyReportDescription", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ExportGridDescription() As String
            Get
                Return ResourceManager.GetString("ExportGridDescription", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ExportGridName() As String
            Get
                Return ResourceManager.GetString("ExportGridName", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ExtraOptionConceptcodesTask() As String
            Get
                Return ResourceManager.GetString("ExtraOptionConceptcodesTask", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ExtraOptionConceptcodesTaskDescription() As String
            Get
                Return ResourceManager.GetString("ExtraOptionConceptcodesTaskDescription", resourceCulture)
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

        Friend ReadOnly Property FieldEmpty() As String
            Get
                Return ResourceManager.GetString("FieldEmpty", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property FileExists() As String
            Get
                Return ResourceManager.GetString("FileExists", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property FileNameCustomReport() As String
            Get
                Return ResourceManager.GetString("FileNameCustomReport", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property FilenameNotValid() As String
            Get
                Return ResourceManager.GetString("FilenameNotValid", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Index() As String
            Get
                Return ResourceManager.GetString("Index", resourceCulture)
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

        Friend ReadOnly Property ItemCode() As String
            Get
                Return ResourceManager.GetString("ItemCode", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ItemGrid() As String
            Get
                Return ResourceManager.GetString("ItemGrid", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ItemLayoutTemplateGrid() As String
            Get
                Return ResourceManager.GetString("ItemLayoutTemplateGrid", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ItemLayoutTemplateUsedName() As String
            Get
                Return ResourceManager.GetString("ItemLayoutTemplateUsedName", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property KeyValues() As String
            Get
                Return ResourceManager.GetString("KeyValues", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property MaxScore() As String
            Get
                Return ResourceManager.GetString("MaxScore", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property MediaReferencedByItemsExcelReport_Description() As String
            Get
                Return ResourceManager.GetString("MediaReferencedByItemsExcelReport_Description", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property MediaReferencedByItemsExcelReport_Name() As String
            Get
                Return ResourceManager.GetString("MediaReferencedByItemsExcelReport_Name", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property MediaReferencedByTestsExcelReport_Description() As String
            Get
                Return ResourceManager.GetString("MediaReferencedByTestsExcelReport_Description", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property MediaReferencedByTestsExcelReport_Name() As String
            Get
                Return ResourceManager.GetString("MediaReferencedByTestsExcelReport_Name", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ModifiedBy() As String
            Get
                Return ResourceManager.GetString("ModifiedBy", resourceCulture)
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

        Friend ReadOnly Property NavigationNumber() As String
            Get
                Return ResourceManager.GetString("NavigationNumber", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property No() As String
            Get
                Return ResourceManager.GetString("No", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property NoInfo() As String
            Get
                Return ResourceManager.GetString("NoInfo", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property NoProgrammInstalled() As String
            Get
                Return ResourceManager.GetString("NoProgrammInstalled", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property OptionValidatorResourceExposureLog_The__date_from__is_not_earlier_than__date_to_() As String
            Get
                Return ResourceManager.GetString("OptionValidatorResourceExposureLog_The__date_from__is_not_earlier_than__date_to_", resourceCulture)
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

        Friend ReadOnly Property Overview() As String
            Get
                Return ResourceManager.GetString("Overview", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property OverviewConceptScoring() As String
            Get
                Return ResourceManager.GetString("OverviewConceptScoring", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property OverviewGridToExcel() As String
            Get
                Return ResourceManager.GetString("OverviewGridToExcel", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property PathNotValid() As String
            Get
                Return ResourceManager.GetString("PathNotValid", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property PathOrFilenameNotValid() As String
            Get
                Return ResourceManager.GetString("PathOrFilenameNotValid", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ProgressMessage() As String
            Get
                Return ResourceManager.GetString("ProgressMessage", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ProgressMessageConceptScoringReport() As String
            Get
                Return ResourceManager.GetString("ProgressMessageConceptScoringReport", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Publishable() As String
            Get
                Return ResourceManager.GetString("Publishable", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property RawScore() As String
            Get
                Return ResourceManager.GetString("RawScore", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ReferencedMediaExcelExport_Description() As String
            Get
                Return ResourceManager.GetString("ReferencedMediaExcelExport_Description", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ReferencedMediaExcelExport_Name() As String
            Get
                Return ResourceManager.GetString("ReferencedMediaExcelExport_Name", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ReportDescriptionAssessmentTestMaxScore() As String
            Get
                Return ResourceManager.GetString("ReportDescriptionAssessmentTestMaxScore", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ReportDescriptionConceptScoring() As String
            Get
                Return ResourceManager.GetString("ReportDescriptionConceptScoring", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ReportDescriptionDatasource() As String
            Get
                Return ResourceManager.GetString("ReportDescriptionDatasource", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ReportDescriptionItem() As String
            Get
                Return ResourceManager.GetString("ReportDescriptionItem", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ReportDescriptionItemWithoutParameter() As String
            Get
                Return ResourceManager.GetString("ReportDescriptionItemWithoutParameter", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ReportDescriptionMedia() As String
            Get
                Return ResourceManager.GetString("ReportDescriptionMedia", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ReportDescriptionTest() As String
            Get
                Return ResourceManager.GetString("ReportDescriptionTest", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ReportDescriptionTestItems() As String
            Get
                Return ResourceManager.GetString("ReportDescriptionTestItems", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ReportDescriptionTestWithoutParameter() As String
            Get
                Return ResourceManager.GetString("ReportDescriptionTestWithoutParameter", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ReportNameAssessmentTestMaxScore() As String
            Get
                Return ResourceManager.GetString("ReportNameAssessmentTestMaxScore", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ReportNameConceptScoring() As String
            Get
                Return ResourceManager.GetString("ReportNameConceptScoring", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ReportNameDatasource() As String
            Get
                Return ResourceManager.GetString("ReportNameDatasource", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ReportNameItem() As String
            Get
                Return ResourceManager.GetString("ReportNameItem", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ReportNameItemWithoutParameters() As String
            Get
                Return ResourceManager.GetString("ReportNameItemWithoutParameters", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ReportNameKenmerkenOverzicht() As String
            Get
                Return ResourceManager.GetString("ReportNameKenmerkenOverzicht", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ReportNameMedia() As String
            Get
                Return ResourceManager.GetString("ReportNameMedia", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ReportNameTest() As String
            Get
                Return ResourceManager.GetString("ReportNameTest", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ReportNameTestItems() As String
            Get
                Return ResourceManager.GetString("ReportNameTestItems", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ReportNameTestWithoutParameters() As String
            Get
                Return ResourceManager.GetString("ReportNameTestWithoutParameters", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ReportSuccessful() As String
            Get
                Return ResourceManager.GetString("ReportSuccessful", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ReportUnSuccessful() As String
            Get
                Return ResourceManager.GetString("ReportUnSuccessful", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ResourceExposureLogReport_ChooseDates() As String
            Get
                Return ResourceManager.GetString("ResourceExposureLogReport_ChooseDates", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ResourceId() As String
            Get
                Return ResourceManager.GetString("ResourceId", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ResourceReferenceBy_ReportName() As String
            Get
                Return ResourceManager.GetString("ResourceReferenceBy_ReportName", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ResourcesReferencedBy_Description() As String
            Get
                Return ResourceManager.GetString("ResourcesReferencedBy_Description", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ResourcesReferencedBy_Name() As String
            Get
                Return ResourceManager.GetString("ResourcesReferencedBy_Name", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ResponseCount() As String
            Get
                Return ResourceManager.GetString("ResponseCount", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Scorable() As String
            Get
                Return ResourceManager.GetString("Scorable", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Section() As String
            Get
                Return ResourceManager.GetString("Section", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property SelectDates_FromDateLabelText() As String
            Get
                Return ResourceManager.GetString("SelectDates_FromDateLabelText", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property SelectDates_ToDateLabelText() As String
            Get
                Return ResourceManager.GetString("SelectDates_ToDateLabelText", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property State() As String
            Get
                Return ResourceManager.GetString("State", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property TestGrid() As String
            Get
                Return ResourceManager.GetString("TestGrid", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property TestPart() As String
            Get
                Return ResourceManager.GetString("TestPart", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property TestTemplateGrid() As String
            Get
                Return ResourceManager.GetString("TestTemplateGrid", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Title() As String
            Get
                Return ResourceManager.GetString("Title", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Version() As String
            Get
                Return ResourceManager.GetString("Version", resourceCulture)
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
