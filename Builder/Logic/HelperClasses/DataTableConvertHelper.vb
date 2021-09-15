
Imports System.Linq
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports Questify.Builder.Logic.ResourceManager
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports Enums
Imports Questify.Builder.Configuration
Imports Questify.Builder.Logic.ContentModel
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Logic.Service.Model.Entities
Imports Questify.Builder.Model.ContentModel.FactoryClasses

Public Class DataTableConvertHelper
    Implements IDisposable

    Public Sub New(bankId As Integer)
        Me._resourceManager = New DataBaseResourceManager(bankId)
        If MultiLanguageController.CurrentLanguageSetting = DUTCH Then
            _stateFieldName = "Status"
        End If
    End Sub

    Private ReadOnly _bankDictionary As New Dictionary(Of Integer, String)
    Private ReadOnly _templateCache As New Dictionary(Of String, ParameterSetCollection)
    Private ReadOnly _customBankPropertyDictionary As New Dictionary(Of Guid, String)
    Private ReadOnly _stateFieldName As String = "State"

    Private Const DUTCH As String = "NL"

#Region "IDisposable Support"

    Protected Overridable Sub Dispose(disposing As Boolean)
        If disposing AndAlso Me._resourceManager IsNot Nothing Then
            Dim rm As DataBaseResourceManager = Me._resourceManager
            Me._resourceManager = Nothing
            rm.Dispose()
        End If
    End Sub

    'This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        'Do not change this code. Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Me.Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

#Region " Public Events  "

    ''' <summary>
    ''' Occurs when [start publication].
    ''' </summary>
    Public Event StartCreateTable As EventHandler(Of StartEventArgs)

    ''' <summary>
    ''' Raises the <see cref="StartCreateTable" /> event.
    ''' </summary>
    ''' <param name="e">The <see cref="StartEventArgs"/> instance containing the event data.</param>
    Private Sub OnStartCreateTable(ByVal e As StartEventArgs)
        RaiseEvent StartCreateTable(Me, e)
    End Sub

    ''' <summary>
    ''' Occurs when progress in the creation of the table van be reported.
    ''' </summary>
    Public Event CreateProgress As EventHandler(Of ProgressEventArgs)

    ''' <summary>
    ''' Raises the <see cref="CreateProgress" /> event.
    ''' </summary>
    ''' <param name="e">The <see cref="Cito.Tester.Common.ProgressEventArgs" /> instance containing the event data.</param>
    Private Sub OnCreateProgress(ByVal e As ProgressEventArgs)
        RaiseEvent CreateProgress(Me, e)
    End Sub

#End Region

    ''' <summary>
    ''' Gets the datatable.
    ''' </summary>
    ''' <param name="collection">The collection.</param>
    Public Function CreateTable(ByVal collection As IList(Of ResourceDto), includedItemParameters As Boolean) As DataTable
        Dim returnDatatable As DataTable = Nothing

        Dim rte = DetermineResourceTypeEnum(collection.FirstOrDefault())

        Dim customProperties As EntityCollection = BankFactory.Instance.GetCustomBankPropertiesForBranchById(collection.FirstOrDefault.BankId, rte)
        customProperties.OfType(Of CustomBankPropertyEntity).ToList.ForEach(Sub(c)
                                                                                If Not TypeOf c Is RichTextValueCustomBankPropertyEntity Then
                                                                                    _customBankPropertyDictionary.Add(c.CustomBankPropertyId, c.Name)
                                                                                End If
                                                                            End Sub)

        Dim entityDataTable As DataTable = Nothing

        If collection.OfType(Of ItemResourceDto).Any() OrElse
           collection.OfType(Of GenericResourceDto).Any() OrElse
           collection.OfType(Of AssessmentTestResourceDto).Any() Then
            'We'll process the whole collection at once so we can get the full item in one DB call
            entityDataTable = Me.CreateDatatable(collection.OfType(Of ResourceDto).ToList, includedItemParameters)
            If entityDataTable IsNot Nothing Then
                Dim keys = {entityDataTable.Columns(My.Resources.ResourceManager.GetResourceStringByName("DataTableKey2"))}
                entityDataTable.PrimaryKey = keys
                returnDatatable = GetReturnDataTable(returnDatatable)
                returnDatatable.Merge(entityDataTable)
            End If
        End If

        Return GetReturnDataTable(returnDatatable)
    End Function

    Public Function CreateTableForTestItems(ByVal collection As IList(Of ResourceDto), includedItemParameters As Boolean) As DataTable
        Dim returnDatatable As DataTable = Nothing
        Dim customProperties As EntityCollection = BankFactory.Instance.GetCustomBankPropertiesForBranchById(collection.FirstOrDefault.BankId, ResourceTypeEnum.ItemResource)
        customProperties.OfType(Of CustomBankPropertyEntity).ToList.ForEach(Sub(c)
                                                                                If Not TypeOf c Is RichTextValueCustomBankPropertyEntity Then
                                                                                    _customBankPropertyDictionary.Add(c.CustomBankPropertyId, c.Name)
                                                                                End If
                                                                            End Sub)

        Dim entityDataTable As DataTable = Nothing

        Dim assessmentTestResources = ResourceFactory.Instance.GetResourcesByIdsWithOption(collection.OfType(Of AssessmentTestResourceDto).Select(Function(r) r.ResourceId).ToList(), New AssessmentTestResourceEntityFactory(), New ResourceRequestDTO())

        For Each entity In collection.OfType(Of AssessmentTestResourceDto).ToList
            If entity IsNot Nothing Then
                entityDataTable = Nothing

                Dim assessmentResource = DirectCast(assessmentTestResources.Items.Cast(Of ResourceEntity).FirstOrDefault(Function(t) t.ResourceId = entity.ResourceId), AssessmentTestResourceEntity)
                If (assessmentResource) IsNot Nothing Then
                    entityDataTable = Me.CreateDataTableForTest(assessmentResource, includedItemParameters)
                End If

                If entityDataTable IsNot Nothing Then
                    Dim keys = {entityDataTable.Columns(My.Resources.ResourceManager.GetResourceStringByName("DataTableKey1")),
                        entityDataTable.Columns(My.Resources.ResourceManager.GetResourceStringByName("DataTableKey2"))}
                    entityDataTable.PrimaryKey = keys
                    returnDatatable = GetReturnDataTable(returnDatatable, True)
                    returnDatatable.Merge(entityDataTable)
                End If
            End If
        Next

        Return GetReturnDataTable(returnDatatable, True)
    End Function

    Private Function DetermineResourceTypeEnum(resource As ResourceDto) As ResourceTypeEnum
        Select Case resource.GetType
            Case GetType(AssessmentTestResourceDto)
                Return ResourceTypeEnum.AssessmentTestResource
            Case GetType(ItemResourceDto)
                Return ResourceTypeEnum.ItemResource
            Case GetType(GenericResourceDto)
                Return ResourceTypeEnum.GenericResource
            Case Else
                Return ResourceTypeEnum.None
        End Select
    End Function

    Private Function GetReturnDataTable(returnDatatable As DataTable, Optional forTestItems As Boolean = False) As DataTable
        Dim table = returnDatatable
        If table Is Nothing Then
            table = New DataTable()
            Dim keys = {table.Columns(My.Resources.ResourceManager.GetResourceStringByName("DataTableKey2"))}

            If forTestItems Then
                keys = {table.Columns(My.Resources.ResourceManager.GetResourceStringByName("DataTableKey1")),
                    table.Columns(My.Resources.ResourceManager.GetResourceStringByName("DataTableKey2"))}
            End If

            table.PrimaryKey = keys
        End If
        Return table
    End Function

    Private _resourceManager As DataBaseResourceManager

    Private ReadOnly Property BankId As Integer
        Get
            Return Me._resourceManager.BankId
        End Get
    End Property

    Private Function CreateDatatable(ByVal collection As IList(Of ResourceDto), ByVal includeParameters As Boolean) As DataTable
        Dim dataTable As DataTable = New DataTable()
        Me.OnStartCreateTable(New StartEventArgs(collection.Count))
        'Don't loop through the collection because we want to keep the sequence
        Dim index As Integer = 0
        For Each resource In collection
            dataTable.NewRow()
            Me.OnCreateProgress(New ProgressEventArgs(String.Format(My.Resources.ProgressMessage, resource.Name), index))
            Me.CreateRow(dataTable, resource, index, includeParameters)
            index += 1
        Next
        Return dataTable
    End Function

    Private Sub CreateRow(ByRef table As DataTable, ByRef resource As ResourceDto, ByRef index As Integer, withParameters As Boolean)
        Dim newRow As DataRow = table.NewRow
        table.Rows.Add(newRow)
        'Add default value for first key-column (necessary to specify test code when creating reports for multiple tests at once)
        Me.FillRowWithResourceDto(newRow, resource)
        If withParameters AndAlso TypeOf resource Is ItemResourceDto Then
            Dim item = DirectCast(resource, ItemResourceDto)
            Dim assesmentItem As AssessmentItem = item.GetAssessmentItem()
            If assesmentItem IsNot Nothing Then
                Me.FillRowWithAssessmentItem(newRow, assesmentItem, index)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Fills the data row for item.
    ''' </summary>
    ''' <param name="row">The DataRow to fill</param>
    ''' <param name="resourceDto">The resource.</param>
    Private Sub FillRowWithResourceDto(ByRef row As DataRow, ByVal resourceDto As ResourceDto)
        row.AddField(My.Resources.ResourceManager.GetResourceStringByName("DataTableKey2"), resourceDto.Name, 1)
        row.FillWithEntity(resourceDto, _customBankPropertyDictionary)

        'Add State field manually
        row.AddField(_stateFieldName, resourceDto.StateName)
    End Sub

    ''' <summary>
    ''' Handles the ResourceNeeded event of the ItemParameterGrid control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Cito.Tester.ContentModel.ResourceNeededEventArgs" /> instance containing the event data.</param>
    Private Sub ItemParameterGrid_ResourceNeeded(ByVal sender As Object, ByVal e As ResourceNeededEventArgs)
        _resourceManager.HandleResourceNeeded(e, New ResourceRequestDTO())
    End Sub

    ''' <summary>
    ''' Gets the name of the parameter.
    ''' </summary>
    ''' <param name="parameter">The parameter.</param>

    Public Shared Function GetLocalizedParameterLabel(ByVal parameter As ParameterBase) As String

        Const DEFAULTLANGUAGE As String = "EN"
        Dim language As String = DEFAULTLANGUAGE
        Dim localizedLabel As String = String.Empty
        Const LABEL As String = "label"

        language = MultiLanguageController.CurrentLanguageSetting
        If Not language.Equals(DEFAULTLANGUAGE, StringComparison.InvariantCultureIgnoreCase) Then
            localizedLabel = $"{LABEL}-{language}"
        End If

        Dim returnValue As String = String.Empty

        If Not String.IsNullOrEmpty(parameter.DesignerSettings.GetSettingValueByKey(localizedLabel)) Then
            returnValue = parameter.DesignerSettings.GetSettingValueByKey(localizedLabel)
        ElseIf Not String.IsNullOrEmpty(parameter.DesignerSettings.GetSettingValueByKey(LABEL)) Then
            returnValue = parameter.DesignerSettings.GetSettingValueByKey(LABEL)
        End If

        Return returnValue
    End Function

    ''' <summary>
    ''' Fills the data row for item.
    ''' </summary>
    ''' <param name="row">The DataTable to add the row to</param>
    ''' <param name="assessmentItem">The item resource.</param>
    ''' <param name="index">The index of the added row</param>
    Private Sub FillRowWithAssessmentItem(ByVal row As DataRow, ByVal assessmentItem As AssessmentItem, ByRef index As Integer)
        If assessmentItem.Solution.AspectReferenceSetCollection IsNot Nothing AndAlso assessmentItem.Solution.AspectReferenceSetCollection.Any() Then
            'Add the 'Aspects' column (if it doesn't exist)
            row.AddField("Aspects", assessmentItem.Solution.GetAspectsCSV(), index)
        End If

        'Get parameters from itemtemplate
        If assessmentItem.Solution.AspectReferenceSetCollection IsNot Nothing AndAlso assessmentItem.Solution.AspectReferenceSetCollection.Count > 0 Then
            'Add the 'Aspects' column if it doesn't exist
            If MultiLanguageController.CurrentLanguageSetting = DUTCH Then
                row.AddField("Aspecten", assessmentItem.Solution.GetAspectsCSV())
            Else
                row.AddField("Aspects", assessmentItem.Solution.GetAspectsCSV())
            End If
        End If

        'Get parameters from itemtemplate
        If Not _templateCache.ContainsKey(assessmentItem.LayoutTemplateSourceName) Then
            Dim adapter As New ItemLayoutAdapter(assessmentItem.LayoutTemplateSourceName, Nothing, AddressOf ItemParameterGrid_ResourceNeeded)
            Dim extractedParameterSets = adapter.CreateParameterSetsFromItemTemplate()
            _templateCache.Add(assessmentItem.LayoutTemplateSourceName, extractedParameterSets)
        End If

        For Each parameterSet As ParameterCollection In _templateCache(assessmentItem.LayoutTemplateSourceName)
            If parameterSet IsNot Nothing Then
                Dim setIndex As Integer = 1
                row.FillWithParameterSet(parameterSet, assessmentItem.Parameters, setIndex)
            End If
        Next
    End Sub


    ''' <summary>
    ''' Fills the datatable for test.
    ''' </summary>
    ''' <param name="assessmenttestResource">The assessmentest resource.</param>
    Private Function CreateDataTableForTest(ByVal assessmenttestResource As AssessmentTestResourceEntity, ByVal includeParameters As Boolean) As DataTable
        Const ERRORITEM As String = "e"
        Const PAUSEITEM As String = "p"
        Const INFOITEM As String = "i"
        Const SYSTEMITEM As String = "s"

        Dim dataTable As DataTable = New DataTable()

        Dim allItemsInTestCollection As New List(Of ItemReference2)
        Dim fullTest As AssessmentTestResourceEntity = ResourceFactory.Instance.GetAssessmentTest(assessmenttestResource)
        Dim assessmentTest As AssessmentTest2 = fullTest.GetTest()
        Dim sectionDictionary As New Dictionary(Of String, String)
        Dim testpartDictionary As New Dictionary(Of String, String)

        For Each part As TestPart2 In assessmentTest.TestParts
            For Each section As TestSection2 In part.Sections
                section.AddToDictionaries(String.Empty, part.Title, sectionDictionary, testpartDictionary, allItemsInTestCollection)
            Next
        Next

        'Get item
        Dim request = New ItemResourceRequestDTO() With {.WithCustomProperties = True, .WithDependencies = True, .WithReportData=True}
        Dim items = DtoFactory.Item.GetItemsByCode(allItemsInTestCollection.Select(Function(i) i.SourceName), BankId, request)
        Dim index As Integer = 0
        Dim navNr As Integer = 0
        Dim navRef As String = String.Empty

        Me.OnStartCreateTable(New StartEventArgs(allItemsInTestCollection.Count))

        'Don't loop through the itemCollection because we want to keep the sequence
        For Each itemReference As ItemReference2 In allItemsInTestCollection
            Me.OnCreateProgress(New ProgressEventArgs(String.Format(My.Resources.ProgressMessage, itemReference.SourceName)))
            Dim item = items.FirstOrDefault(Function(i) i.Name = itemReference.SourceName)
            If item Is Nothing Then
                Continue For
            End If

            Dim newDataRow As DataRow = dataTable.NewRow
            dataTable.Rows.Add(newDataRow)

            'Add test identifier for first key-column (necessary when creating reports for multiple tests at once)
            newDataRow.AddField(My.Resources.ResourceManager.GetResourceStringByName("DataTableKey1"), assessmentTest.Identifier)

            'Add itemreference fields to the newly created row
            newDataRow.AddField("ReferenceTitle", itemReference.Title)
            newDataRow.AddField("ItemActive", If(itemReference.Active, My.Resources.Yes, My.Resources.No))
            newDataRow.AddField("IsAnchorItem", If(itemReference.IsAnchorItem, My.Resources.Yes, My.Resources.No))
            newDataRow.AddField("ItemFunctionalType", ResourceEnumConverter.ConvertToString(itemReference.ItemFunctionalType), index)
            newDataRow.AddField("Weight", itemReference.Weight.ToString)

            Me.FillRowWithResourceDto(newDataRow, item)
            If includeParameters Then
                Dim assesmentItem As AssessmentItem = item.GetAssessmentItem()
                If Not assesmentItem Is Nothing Then
                    Me.FillRowWithAssessmentItem(newDataRow, assesmentItem, index)
                End If
            End If

            Select Case item.ItemTypeFromItemLayoutTemplate
                Case "Error"
                    navRef = ERRORITEM
                Case "Informational"
                    navRef = INFOITEM
                Case "Pause"
                    navRef = PAUSEITEM
                Case "System"
                    navRef = SYSTEMITEM
                Case Else
                    navNr += 1
                    navRef = navNr.ToString()
            End Select

            If sectionDictionary.ContainsKey(itemReference.SourceName) AndAlso testpartDictionary.ContainsKey(itemReference.SourceName) Then
                newDataRow.AddField("Index", (index + 1).ToString)
                newDataRow.AddField("NavigationNumber", navRef)
                newDataRow.AddField("TestPart", testpartDictionary.Item(itemReference.SourceName))
                newDataRow.AddField("Section", sectionDictionary.Item(itemReference.SourceName))
            End If

            index += 1
        Next

        Return dataTable
    End Function

End Class