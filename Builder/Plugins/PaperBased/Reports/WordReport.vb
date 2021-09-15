
Imports System.Collections.ObjectModel
Imports System.Drawing
Imports System.IO
Imports System.Text
Imports System.Windows.Forms
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports DocumentFormat.OpenXml.Packaging
Imports DocumentFormat.OpenXml.Wordprocessing
Imports Questify.Builder.Configuration
Imports Questify.Builder.Logic
Imports Questify.Builder.Logic.ContentModel.Scoring
Imports Questify.Builder.Logic.HelperClasses
Imports Questify.Builder.Logic.ResourceManager
Imports Questify.Builder.Logic.Service.Classes
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Logic.Service.HelperFunctions
Imports Questify.Builder.Logic.Service.Interfaces
Imports Questify.Builder.Logic.Service.Model.Entities
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Model.ContentModel.FactoryClasses
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports Questify.Builder.UI
Imports SD.LLBLGen.Pro.ORMSupportClasses

Public Class WordReport
    Inherits ReportBase
    Implements IReportHandlerAsync

#Region " Private Fields "

    Private _bankId As Integer
    Private _assessmentTest As AssessmentTest2
    Private _resourceManager As DataBaseResourceManager
    Private _itemCollection As EntityCollectionBase2(Of ItemResourceEntity)
    Private _itemCodeList As List(Of String)
    Private _imageTempPath As String
    Private _previewName As String = String.Empty
    Private _screenshotHelper As ScreenshotHelper = Nothing
    Private _isCompleted As Boolean = False
    Private _previousHandler As IItemPreviewHandler
    Private _publicationProperties As List(Of PublicationProperty)

    Private ReadOnly _defaultImageSizePortrait As New Size(580, 435)
    Private ReadOnly _defaultImageSizeLandscape As New Size(666, 500)

#End Region

#Region " Constructor "

    Public Sub New()
        _optionValidatorBase = New OptionValidatorWordExport
    End Sub

#End Region

#Region " Public Properties "

    Public Overrides ReadOnly Property Name() As String Implements IReportHandler.Name
        Get
            Dim resource = Me.Collection.FirstOrDefault
            If resource IsNot Nothing Then
                Select Case resource.GetType
                    Case GetType(AssessmentTestResourceDto)
                        Return My.Resources.ReportNameTest
                    Case GetType(ItemResourceDto)
                        Return My.Resources.ReportNameItem
                End Select
            End If
            Return String.Empty
        End Get
    End Property

    ''' <summary>
    ''' Sets the selected bank. Which is needed in some cases when
    ''' you need to get all parent banks to retrieve items that are in
    ''' other banks than the selected bank (parent).
    ''' </summary>
    Public Overrides Property BankId As Integer Implements IReportValidationBase.BankId
        Set(ByVal value As Integer)
            _bankId = value
        End Set
        Get
            Return _bankId
        End Get
    End Property

    Public Overrides ReadOnly Property Overview() As String Implements IReportHandler.Overview
        Get
            If OptionValidator IsNot Nothing Then
                Dim selectedInformationBlocks As String = GetSelectedInformationBlocks()
                Dim itemOnNewPage As String

                If OptionValidator.ItemOnNewPage Then
                    itemOnNewPage = My.Resources.Yes
                Else
                    itemOnNewPage = My.Resources.No
                End If

                Dim pageOrientationLandscape As String
                If OptionValidator.PageOrientationLandscape Then
                    pageOrientationLandscape = My.Resources.Yes
                Else
                    pageOrientationLandscape = My.Resources.No
                End If

                Return String.Format(My.Resources.OverviewGridToWord, vbCrLf, Me.Name, OptionValidator.ExportPath, selectedInformationBlocks, itemOnNewPage, pageOrientationLandscape)
            End If

            Return String.Empty
        End Get
    End Property

    ''' <summary>
    ''' Gets the selected information blocks.
    ''' </summary>
    Private Function GetSelectedInformationBlocks() As String
        Dim sb As New StringBuilder(String.Empty)
        If OptionValidator.ShouldItemInformationBeAddedToTheReport Then
            sb.Append(String.Concat("- ", My.Resources.ItemInformation, vbCrLf))
        End If

        If OptionValidator.ShouldItemCustomPropertiesBeAddedToTheReport Then
            sb.Append(String.Concat("- ", My.Resources.ItemCustomProperties, vbCrLf))
        End If

        If OptionValidator.ShouldDependenciesBeAddedToTheReport Then
            sb.Append(String.Concat("- ", My.Resources.ItemDependencies, vbCrLf))
        End If

        If OptionValidator.ShouldReferencesBeAddedToTheReport Then
            sb.Append(String.Concat("- ", My.Resources.ItemReferences, vbCrLf))
        End If

        If OptionValidator.ShouldItemSolutionBeAddedToTheReport Then
            sb.Append(String.Concat("- ", My.Resources.ItemSolution, vbCrLf))
        End If

        If OptionValidator.ShouldAddItemContent Then
            sb.Append(String.Concat("- ", My.Resources.ItemContent, vbCrLf))
        End If

        If OptionValidator.ShouldShowPreprocessorRules Then
            sb.Append(String.Concat("- ", My.Resources.ItemKeysWithPreprocessorRules, vbCrLf))
        End If

        If OptionValidator.SelectedHandler IsNot Nothing Then
            sb.Append(String.Concat("      - ", OptionValidator.SelectedHandler.UserFriendlyName, vbCrLf))
        End If

        If Not String.IsNullOrEmpty(OptionValidator.Size) Then
            sb.Append(String.Concat("      - ", String.Format(My.Resources.Dimension, OptionValidator.Size.Replace("|", "x")), vbCrLf))
        End If

        Return sb.ToString
    End Function

    ''' <summary>
    ''' Gets the exported report location.
    ''' </summary>
    Public Overrides ReadOnly Property ExportedReportLocation() As String Implements IReportHandler.ExportedReportLocation
        Get
            If OptionValidator IsNot Nothing AndAlso Not String.IsNullOrEmpty(OptionValidator.ExportPath) Then
                Return OptionValidator.ExportPath
            End If

            Return String.Empty
        End Get
    End Property

    Public Overrides ReadOnly Property IsInitDataGeneratedAsynchronous As Boolean Implements IReportHandler.IsInitDataGeneratedAsynchronous
        Get
            Return True
        End Get
    End Property

    Protected Overridable ReadOnly Property OptionValidator As OptionValidatorWordExport
        Get
            Return CType(_optionValidatorBase, OptionValidatorWordExport)
        End Get
    End Property

#End Region

#Region " Public Events "

    Protected Sub OnStartCollectGeneratingReport(ByVal e As StartEventArgs)
        RaiseEvent StartReportProgress(Me, e)
    End Sub


    ''' <summary>
    ''' Occurs when [progress collect columns].
    ''' </summary>
    Public Event ReportProgress(ByVal sender As Object, ByVal e As Cito.Tester.Common.ProgressEventArgs) Implements IReportHandler.Progress

    ''' <summary>
    ''' Occurs when [start collect columns].
    ''' </summary>
    Public Event StartReportProgress(ByVal sender As Object, ByVal e As StartEventArgs) Implements IReportHandler.StartProgress


    Protected Sub OnProgressCollectGeneratingReport(ByVal e As Cito.Tester.Common.ProgressEventArgs)
        RaiseEvent ReportProgress(Me, e)
    End Sub

    Protected Sub OnReportCompleted(ByVal e As ReportCompletedEventArgs)
        RaiseEvent ReportCompleted(Me, e)
    End Sub

    ''' <summary>
    ''' Occurs when [on report completed].
    ''' </summary>
    Public Event ReportCompleted(ByVal sender As Object, ByVal e As ReportCompletedEventArgs) Implements IReportHandler.ReportCompleted

#End Region

#Region " Public Functions "

    Public Function GenerateData() As Boolean Implements IReportHandler.GenerateData
        Dim t As Task(Of Boolean) = DoGenerateDataAsync()

        t.Wait()
        Return t.Result
    End Function

    Public Async Function DoGenerateDataAsync() As Task(Of Boolean) Implements IReportHandlerAsync.GenerateDataAsync
        'get a list of item codes
        Dim returnValue As Boolean = False
        Try
            Dim resource = Me.Collection.FirstOrDefault
            If resource IsNot Nothing Then
                Select Case resource.GetType
                    Case GetType(AssessmentTestResourceDto)
                        GetAssessmentTest(resource)
                        Dim allItemsInTestCollection As ReadOnlyCollection(Of ItemReference2) = _assessmentTest.GetAllItemReferencesInTest()
                        _itemCodeList = allItemsInTestCollection.OfType(Of ItemReference2).Select(Function(i) i.SourceName).ToList
                    Case GetType(ItemResourceDto)
                        _itemCodeList = _selectedEntities.Select(Function(i) i.Name).ToList
                        _itemCodeList.Sort()
                End Select

                If _itemCodeList.Any() Then
                    Dim request = New ItemResourceRequestDTO() With {.WithDependencies = OptionValidator.ShouldDependenciesBeAddedToTheReport OrElse OptionValidator.ShouldAddItemContent, .WithCustomProperties = OptionValidator.ShouldItemCustomPropertiesBeAddedToTheReport, .WithReportData = True}
                    _itemCollection = ResourceFactory.Instance.GetItemsByCodes(_itemCodeList, _bankId, request)
                    Await CreateReportFromItemCollection()

                    returnValue = True
                End If
            End If
        Catch ex As Exception
            _resultText = String.Format(My.Resources.ErrorOccured & " ({1})", ex.Message, ResultText)

            If Not _isCompleted Then
                OnReportCompleted(New ReportCompletedEventArgs(False))
                _isCompleted = True
            End If
        End Try

        Return returnValue
    End Function

    ''' <summary>
    ''' Gets the assessment test.
    ''' </summary>
    ''' <param name="resource">The test resource.</param>
    Protected Overrides Function GetAssessmentTest(resource As ResourceDto) As AssessmentTest2
        If _assessmentTest Is Nothing Then
            Dim assessmentTest = TryCast(ResourceFactory.Instance.GetResourceByIdWithOption(resource.ResourceId, New AssessmentTestResourceEntityFactory(), New ResourceRequestDTO()), AssessmentTestResourceEntity)
            If assessmentTest IsNot Nothing Then
                If (_helper Is Nothing) Then
                    _helper = New ReportHelperClass(resource.BankId)
                End If
                Dim fullTest As AssessmentTestResourceEntity = _helper.GetFullTestEntity(assessmentTest)
                If fullTest IsNot Nothing Then
                    _assessmentTest = _helper.GetTestFromResource(fullTest)
                End If
            End If
        End If
        Return _assessmentTest
    End Function

    ''' <summary>
    ''' Gets the export location UI.
    ''' </summary>
    Public Overrides Function GetExportLocationUI() As UserControl Implements IReportHandler.GetExportLocationUI
        OptionValidator.ClearErrors()
        Dim selectLocationForm As New SelectReportLocation
        Dim first = _selectedEntities.FirstOrDefault
        If first IsNot Nothing Then
            Dim name As String = String.Empty

            If OptionValidator.ShouldAddItemContent AndAlso OptionValidator.SelectedHandler IsNot Nothing Then
                name = String.Concat("_", OptionValidator.SelectedHandler.UserFriendlyName)
            End If

            Select Case first.GetType
                Case GetType(AssessmentTestResourceDto)
                    OptionValidator.Filename = String.Concat(first.Name, name)
                Case GetType(ItemResourceDto)
                    OptionValidator.Filename = String.Format("Items_Bank_{0}{1}", first.BankName, name)
            End Select

            If String.IsNullOrEmpty(OptionValidator.ExportPath) OrElse _previousHandler IsNot Nothing OrElse (_previousHandler IsNot Nothing AndAlso Not _previousHandler.UserFriendlyName = OptionValidator.SelectedHandler.UserFriendlyName) Then
                If String.IsNullOrEmpty(ReportSettings.WordReport) Then ReportSettings.WordReport = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                OptionValidator.ExportPath = Path.Combine(ReportSettings.WordReport, String.Format("{0}.docx", OptionValidator.Filename))
            End If

            selectLocationForm.OptionValidatorWordExportBindingSource.DataSource = OptionValidator
            _previousHandler = OptionValidator.SelectedHandler

        End If
        Return selectLocationForm
    End Function

    ''' <summary>
    ''' Gets the extra options UI.
    ''' </summary>
    Public Overrides Function GetExtraOptionsUI() As UserControl Implements IReportHandler.GetExtraOptionsUI
        OptionValidator.ClearErrors()
        Dim first = _selectedEntities.FirstOrDefault
        If first IsNot Nothing Then

            Dim selectedItemIdentifiers As New List(Of String)()

            Select Case first.GetType
                Case GetType(AssessmentTestResourceDto)
                    GetAssessmentTest(first)
                    OptionValidator.Handlers = _helper.GetPreviewMethodsByTest(_assessmentTest)
                    selectedItemIdentifiers.AddRange(From itemReference2 In _assessmentTest.GetAllItemReferencesInTest() Select itemReference2.SourceName)
                Case GetType(ItemResourceDto)
                    OptionValidator.Filename = String.Format("Items_Bank_{0}", first.BankName)
                    OptionValidator.Handlers = _helper.GetPreviewMethodsByItems(_selectedEntities)
                    For Each r In _selectedEntities
                        selectedItemIdentifiers.Add(r.Name)
                    Next
            End Select

            If OptionValidator.Handlers IsNot Nothing AndAlso OptionValidator.Handlers.Any() Then
                OptionValidator.SelectedHandler = OptionValidator.Handlers(0)
                ' reset extra options
                OptionValidator.Size = String.Empty

                If OptionValidator.SelectedHandler.Dimensions IsNot Nothing Then
                    Dim screenDimensions As Size = OptionValidator.SelectedHandler.Dimensions(OptionValidator.SelectedHandler.Dimensions.Keys.First)
                    OptionValidator.Size = String.Format("{0}x{1}", screenDimensions.Width, screenDimensions.Height)
                End If

            End If
            Return New SelectInformationBlocks(OptionValidator, _bankId, selectedItemIdentifiers, OptionValidator.Handlers, _publicationProperties)
        End If
        Return Nothing
    End Function

#End Region

#Region " Private Functions "

    ''' <summary>
    ''' Handles the Progress event of the ItemPreviewer control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Cito.Tester.Common.ProgressEventArgs" /> instance containing the event data.</param>
    Private Sub ItemPreviewer_Progress(ByVal sender As Object, ByVal e As Cito.Tester.Common.ProgressEventArgs)
        OnProgressCollectGeneratingReport(e)
    End Sub


    ''' <summary>
    ''' Handles the AllScreenshotsCompleted event of the ItemPreviewer control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Private Sub ItemPreviewer_AllScreenshotsCompleted(ByVal sender As Object, ByVal e As EventArgs)
        StartGeneratingReport()
    End Sub

    ''' <summary>
    ''' Creates the report form item collection.
    ''' </summary>
    Private Async Function CreateReportFromItemCollection() As Task
        If _itemCollection IsNot Nothing Then
            'dont loop through the itemCollection because we want to keep the sequence
            OnStartCollectGeneratingReport(New StartEventArgs(_itemCollection.Count))

            If OptionValidator.ShouldAddItemContent AndAlso (OptionValidator.SelectedHandler IsNot Nothing AndAlso OptionValidator.SelectedHandler.PreviewTarget <> PaperBasedTestPlugin.PLUGIN_NAME) Then
                _publicationProperties = New List(Of PublicationProperty)
                Dim previewer As IItemPreviewer
                If OptionValidator.SelectedHandler.PreviewTarget = "Word" Then
                    previewer = New Word_ItemPreviewer
                Else
                    previewer = New Chromium_ItemPreviewer
                End If
                _screenshotHelper = New ScreenshotHelper(_bankId, OptionValidator.SelectedHandler, _publicationProperties, previewer)
                AddHandler _screenshotHelper.ItemPreviewer_Progress, AddressOf ItemPreviewer_Progress
                AddHandler _screenshotHelper.AllScreenshotsCompleted, AddressOf ItemPreviewer_AllScreenshotsCompleted

                _imageTempPath = _screenshotHelper.InitScreenshots
                _previewName = _screenshotHelper.PreviewName

                Dim size As New Size(1024, 768)

                If Not String.IsNullOrEmpty(OptionValidator.Size) AndAlso Not OptionValidator.Size.IndexOf("|"c) = -1 Then
                    size = New Size(CInt(OptionValidator.Size.Split("|"c)(0)), CInt(OptionValidator.Size.Split("|"c)(1)))
                End If

                Await _screenshotHelper.CreateScreenShotsAsync(_itemCollection, size)
            Else
                StartGeneratingReport()
            End If
        End If
    End Function

    Private Shared Function IsDiskFull(ex As Exception) As Boolean
        Const ERROR_HANDLE_DISK_FULL As Integer = &H27
        Const ERROR_DISK_FULL As Integer = &H70

        Dim win32ErrorCode As Integer = ex.HResult And &HFFFF
        Return win32ErrorCode = ERROR_HANDLE_DISK_FULL OrElse win32ErrorCode = ERROR_DISK_FULL
    End Function

    Private Sub StartGeneratingReport()
        Dim returnValue As Boolean = False

        Try

            Dim retry As Boolean = True
            Dim originalExportpath As String = OptionValidator.ExportPath
            Dim i As Integer = 1

            'If the file is open, generate a new name.
            Do
                Try
                    Using fileStream As New FileStream(OptionValidator.ExportPath, FileMode.Create)
                        fileStream.Write(My.Resources._Default, 0, My.Resources._Default.Length)
                        retry = False
                    End Using
                Catch ex As IOException
                    If IsDiskFull(ex) Then
                        returnValue = False
                        _resultText = String.Format(My.Resources.DiskIsFull)
                        retry = False
                    Else
                        Dim extension As String = Path.GetExtension(originalExportpath)
                        Dim filename As String = Path.GetFileNameWithoutExtension(originalExportpath)
                        Dim directoryname As String = Path.GetDirectoryName(originalExportpath)

                        OptionValidator.ExportPath = String.Format("{0}\{1}_{2}{3}", directoryname, filename, i, extension)

                        i += 1
                    End If
                End Try
            Loop Until Not retry

            'reset the progressbar
            OnStartCollectGeneratingReport(New StartEventArgs(_itemCollection.Count))

            Dim openXmlHelper As New OpenXmlGenerator

            Dim wordDoc As WordprocessingDocument = openXmlHelper.OpenDocument(OptionValidator.ExportPath)
            wordDoc.MainDocumentPart.Document.Body.RemoveAllChildren()

            'Set landscape page orientation if required
            If OptionValidator.PageOrientationLandscape Then openXmlHelper.SetPageOrientationLandscape(wordDoc)

            Dim itemIndex As Integer = 0
            For Each itemCode As String In _itemCodeList
                wordDoc = ProcessItemCode(openXmlHelper, wordDoc, itemCode, itemIndex)
                itemIndex += 1
            Next

            wordDoc.MainDocumentPart.Document.Save()
            wordDoc.Dispose()

            returnValue = True
            _resultText = String.Format(My.Resources.SuccessfullyGenerated, Me.Name)
        Catch ex As Exception
            _resultText = _resultText & Environment.NewLine & String.Format(My.Resources.ErrorOccured, ex.Message)
        Finally
            _itemCollection.Dispose()

            If OptionValidator.ShouldAddItemContent AndAlso (OptionValidator.SelectedHandler IsNot Nothing AndAlso OptionValidator.SelectedHandler.PreviewTarget <> "Word") Then
                _screenshotHelper.Dispose()
            End If

            If Not _isCompleted Then
                OnReportCompleted(New ReportCompletedEventArgs(returnValue))
                _isCompleted = True
            End If

            If _resourceManager IsNot Nothing Then
                _resourceManager.Dispose()
                _resourceManager = Nothing
            End If
        End Try
    End Sub

    Private Function ProcessItemCode(
                                    openXmlHelper As OpenXmlGenerator,
                                    ByRef wordDoc As WordprocessingDocument,
                                    itemCode As String,
                                    itemIndex As Integer) As WordprocessingDocument

        _resultText = "Item: " + itemCode 'Keep track of the item being processed so this information is available in the resultoverview.

        OnProgressCollectGeneratingReport(New Cito.Tester.Common.ProgressEventArgs(String.Format(My.Resources.ProcessingItem, itemCode)))

        Dim filter As New FieldCompareValuePredicate(ItemResourceFields.Name, Nothing, ComparisonOperator.Equal, itemCode)
        Dim indexList As List(Of Integer) = _itemCollection.FindMatches(filter)
        Dim item As ItemResourceEntity = _itemCollection.Item(indexList(0))
        Dim headerOnePresent As Boolean = False

        If indexList.Count = 1 Then
            'Add item information
            If OptionValidator.ShouldItemInformationBeAddedToTheReport Then
                If Not headerOnePresent Then
                    openXmlHelper.AddHeaderOne(wordDoc, String.Format(My.Resources.ItemPlusCode, itemCode), If(itemIndex = 0 OrElse OptionValidator.ItemOnNewPage, True, False))
                    headerOnePresent = True
                End If

                openXmlHelper.AddHeaderTwo(wordDoc, My.Resources.ItemInformation)

                Dim itemInformationTable As Table = openXmlHelper.AddTable(wordDoc, openXmlHelper.GetDefaultTableWidth(OptionValidator.PageOrientationLandscape))
                AddRowsForEntity(item, openXmlHelper, itemInformationTable)
            End If

            'Add table for answer alternatives for inline items
            If OptionValidator.ShouldShowPreprocessorRules Then
                AddFirstHeader(openXmlHelper, wordDoc, itemCode, headerOnePresent)
                openXmlHelper.AddHeaderTwo(wordDoc, My.Resources.ItemKeysWithPreprocessorRules)

                AddRowsForItemKeysWithPreprocessorRules(item, openXmlHelper, wordDoc)
            End If

            'Add custom properties
            If OptionValidator.ShouldItemCustomPropertiesBeAddedToTheReport Then
                If Not headerOnePresent Then
                    openXmlHelper.AddHeaderOne(wordDoc, String.Format(My.Resources.ItemPlusCode, itemCode), If(itemIndex = 0 OrElse OptionValidator.ItemOnNewPage, True, False))
                    headerOnePresent = True
                End If

                openXmlHelper.AddHeaderTwo(wordDoc, My.Resources.ItemCustomProperties)

                Dim itemCustomPropertyTable As Table = openXmlHelper.AddTable(wordDoc, openXmlHelper.GetDefaultTableWidth(OptionValidator.PageOrientationLandscape))
                AddCustomPropertiesRowsForEntity(item, openXmlHelper, itemCustomPropertyTable, My.Resources.Item, wordDoc)
            End If

            'Add References
            If OptionValidator.ShouldReferencesBeAddedToTheReport Then
                If Not headerOnePresent Then
                    openXmlHelper.AddHeaderOne(wordDoc, String.Format(My.Resources.ItemPlusCode, itemCode), If(itemIndex = 0 OrElse OptionValidator.ItemOnNewPage, True, False))
                    headerOnePresent = True
                End If

                openXmlHelper.AddHeaderTwo(wordDoc, My.Resources.ItemReferences)

                Dim itemReferenceTable As Table = openXmlHelper.AddTable(wordDoc, openXmlHelper.GetDefaultTableWidth(OptionValidator.PageOrientationLandscape))
                Dim referenceCollection As EntityCollection = ResourceFactory.Instance.GetReferencesForResource(item)
                AddRowsForCollection(referenceCollection, openXmlHelper, itemReferenceTable, wordDoc)
            End If

            'Add Dependencies
            If OptionValidator.ShouldDependenciesBeAddedToTheReport Then
                If Not headerOnePresent Then
                    openXmlHelper.AddHeaderOne(wordDoc, String.Format(My.Resources.ItemPlusCode, itemCode), If(itemIndex = 0 OrElse OptionValidator.ItemOnNewPage, True, False))
                    headerOnePresent = True
                End If

                openXmlHelper.AddHeaderTwo(wordDoc, My.Resources.ItemDependencies)

                Dim itemDependenciesTable As Table = openXmlHelper.AddTable(wordDoc, openXmlHelper.GetDefaultTableWidth(OptionValidator.PageOrientationLandscape))
                Dim dependencyCollection As EntityCollection = ResourceFactory.Instance.GetDependenciesForResource(item)
                AddRowsForCollection(dependencyCollection, openXmlHelper, itemDependenciesTable, wordDoc)
            End If

            'Add Key
            If OptionValidator.ShouldItemSolutionBeAddedToTheReport Then
                If Not headerOnePresent Then
                    openXmlHelper.AddHeaderOne(wordDoc, String.Format(My.Resources.ItemPlusCode, itemCode), If(itemIndex = 0 OrElse OptionValidator.ItemOnNewPage, True, False))
                    headerOnePresent = True
                End If

                openXmlHelper.AddHeaderTwo(wordDoc, My.Resources.ItemSolution)

                If _resourceManager Is Nothing Then
                    _resourceManager = New DataBaseResourceManager(_bankId)
                End If

                openXmlHelper.AddSolutionToWordDoc(wordDoc, _resourceManager, itemCode, item.KeyValues)
            End If

            'Add Content 
            If OptionValidator.ShouldAddItemContent Then
                If Not headerOnePresent Then
                    openXmlHelper.AddHeaderOne(wordDoc, String.Format(My.Resources.ItemPlusCode, itemCode), If(itemIndex = 0 OrElse OptionValidator.ItemOnNewPage, True, False))
                    headerOnePresent = True
                End If

                openXmlHelper.AddHeaderTwo(wordDoc, My.Resources.ItemContent)

                If OptionValidator.SelectedHandler IsNot Nothing AndAlso OptionValidator.SelectedHandler.PreviewTarget <> "Word" Then
                    Dim imagePath As String = IO.Path.Combine(_imageTempPath, String.Format("{2}_{0}_{1}.jpg", _previewName.Replace(Chr(32), String.Empty), item.Name, itemIndex + 1))

                    If IO.File.Exists(imagePath) Then
                        openXmlHelper.AddImageToDocument(imagePath, wordDoc.MainDocumentPart, item.Name, True, If(OptionValidator.PageOrientationLandscape = True, _defaultImageSizeLandscape, _defaultImageSizePortrait))
                    Else
                        openXmlHelper.AddText(wordDoc, My.Resources.ImageCouldNotBeFound)
                    End If
                Else
                    If _resourceManager Is Nothing Then
                        _resourceManager = New DataBaseResourceManager(_bankId)
                    End If
                    If item IsNot Nothing Then
                        openXmlHelper.AddItemToWordDoc(wordDoc, _resourceManager, item.GetAssessmentItem())
                    Else
                        openXmlHelper.AddItemToWordDoc(wordDoc, _resourceManager, itemCode)
                    End If
                End If
            End If

            'Add Alternatives
            If OptionValidator.ShouldAddChoiceAlternatives AndAlso OptionValidator.AddChoiceAlternativesOptionVisible Then
                If Not headerOnePresent Then
                    openXmlHelper.AddHeaderOne(wordDoc, String.Format(My.Resources.ItemPlusCode, itemCode), If(itemIndex = 0 OrElse OptionValidator.ItemOnNewPage, True, False))
                    headerOnePresent = True
                End If

                If _resourceManager Is Nothing Then
                    _resourceManager = New DataBaseResourceManager(_bankId)
                End If

                openXmlHelper.AddInlineChoiceAlternativesToWordDoc(wordDoc, _resourceManager, itemCode)
            End If

        End If

        If OptionValidator.ItemOnNewPage AndAlso Not (itemIndex + 1 = _itemCodeList.Count) Then 'dont add a break when we are at the end
            'add break
            openXmlHelper.AddPageBreak(wordDoc)
        End If

        Return wordDoc
    End Function

    ''' <summary>
    ''' Adds the first header, with the itemCode, if it has not been added yet
    ''' </summary>
    Private Sub AddFirstHeader(ByRef openXmlHelper As OpenXmlGenerator, ByRef wordDoc As WordprocessingDocument, itemCode As String, ByRef headerOnePresent As Boolean)
        If Not headerOnePresent Then
            openXmlHelper.AddHeaderOne(wordDoc, String.Format(My.Resources.ItemPlusCode, itemCode))
            headerOnePresent = True
        End If
    End Sub

    ''' <summary>
    ''' Adds the rows for entity.
    ''' </summary>
    ''' <param name="entity">The entity.</param>
    ''' <param name="openXmlHelper">The openxml helper.</param>
    ''' <param name="table">The table.</param>
    Private Sub AddRowsForEntity(ByVal entity As ResourceEntity, ByRef openXmlHelper As OpenXmlGenerator, ByVal table As Table)
        ' Create a list of columns to exclude from the report.
        Dim listOfExcludedColumns As New List(Of String)
        listOfExcludedColumns.Add("ResourceId")

        For Each field As IEntityField2 In entity.Fields
            If Not listOfExcludedColumns.Contains(field.Name) Then
                Dim fieldName As String = GetResourceStringByName(field.Name)
                Dim fieldValue As String = String.Empty

                Select Case field.Name
                    Case "StateId"
                        ' StateId is not very meaningful for the end user, so we translate it to StateName
                        If Not String.IsNullOrEmpty(entity.StateName) Then
                            fieldName = My.Resources.State
                            fieldValue = entity.StateName
                        End If
                    Case "BankId"
                        ' BankId is not very meaningful for the end user, so we translate it to BankName
                        If Not String.IsNullOrEmpty(entity.BankName) Then
                            fieldName = My.Resources.Bank
                            fieldValue = entity.BankName
                        End If
                    Case "CreatedBy"
                        ' CreatedBy (user id) is not very meaningful for the end user, so we translate it to the full name of the user.
                        If Not String.IsNullOrEmpty(entity.CreatedByFullName) Then
                            fieldName = My.Resources.CreatedBy
                            fieldValue = entity.CreatedByFullName
                        End If
                    Case "ModifiedBy"
                        ' ModiefiedBy (user id) is not very meaningful for the end user, so we translate it to the full name of the user.
                        If Not String.IsNullOrEmpty(entity.ModifiedByFullName) Then
                            fieldName = My.Resources.ModifiedBy
                            fieldValue = entity.ModifiedByFullName
                        End If
                    Case "IsSystemItem"
                        ' True/False is not for a end user, so translate it to yes/no.
                        If field.DbValue IsNot Nothing Then
                            Dim isSystemItem As Boolean
                            If Boolean.TryParse(field.DbValue.ToString(), isSystemItem) AndAlso isSystemItem Then
                                fieldValue = My.Resources.Yes
                            Else
                                fieldValue = My.Resources.No
                            End If
                        End If
                    Case "keyValues"
                        'If value is empty then check if there are aspects
                        If Not OptionValidator.ShouldItemSolutionBeAddedToTheReport Then
                            If field.DbValue IsNot Nothing AndAlso Not String.IsNullOrEmpty(field.DbValue.ToString) Then
                                fieldValue = field.DbValue.ToString()
                            End If
                        End If
                    Case "ItemAutoId"
                        'Ignore
                    Case "ItemId"
                        If ItemIdHelper.UseItemId Then fieldValue = If(field.DbValue IsNot Nothing, field.DbValue.ToString(), String.Empty)
                    Case Else
                        ' In all other cases, just get the value.
                        fieldValue = If(field.DbValue IsNot Nothing, field.DbValue.ToString(), String.Empty)
                End Select

                ' Only add the entity property to the table if there is a value.
                If Not String.IsNullOrEmpty(fieldValue) Then
                    AddRowToTable(fieldName, fieldValue, openXmlHelper, table)
                End If
            End If
        Next
    End Sub

    Private Sub AddRowsForItemKeysWithPreprocessorRules(ByVal itemResource As ItemResourceEntity, ByRef openXmlHelper As OpenXmlGenerator, ByRef wordDoc As WordprocessingDocument)
        Dim data As ResourceDataEntity = ResourceFactory.Instance.GetResourceData(itemResource)
        itemResource.ResourceData = data

        Dim item As AssessmentItem = DirectCast(SerializeHelper.XmlDeserializeFromByteArray(itemResource.ResourceData.BinData, GetType(AssessmentItem)), AssessmentItem)

        If item.Solution Is Nothing Then
            openXmlHelper.AddText(wordDoc, My.Resources.NotApplicable)
            Return
        End If

        Dim scoringParameters = item.Parameters.DeepFetchScoringParameters()

        If Not CanHavePreprocessorRules(scoringParameters) Then
            openXmlHelper.AddText(wordDoc, My.Resources.NotApplicable)
            Return
        End If

        Dim facts As New List(Of BaseFact)
        For Each keyFinding In item.Solution.Findings
            facts.AddRange(keyFinding.Facts)
            For Each KeyFactSet As KeyFactSet In keyFinding.KeyFactsets
                facts.AddRange(KeyFactSet.Facts)
            Next
        Next

        Dim itemSettingsTable As Table = openXmlHelper.AddTable(wordDoc)

        Dim rowContents = New List(Of String)
        rowContents.Add(My.Resources.Label)
        rowContents.Add(My.Resources.Content)
        rowContents.Add(My.Resources.PreprocessingRule)

        openXmlHelper.AddRow(itemSettingsTable, rowContents)
        rowContents.Clear()

        For Each fact As KeyFact In facts
            Dim domains = fact.Values.Select(Function(v) DirectCast(v, KeyValue).Domain)
            Dim rules = ScoringPropertiesCalculator.GetResponsePreprocessorRules(fact)
            Dim spm = scoringParameters.Where(Function(sp) sp.InlineId IsNot Nothing AndAlso domains.Contains(sp.InlineId)).FirstOrDefault
            Dim label As String

            If spm IsNot Nothing AndAlso spm.Label IsNot Nothing Then
                label = spm.Label
            Else
                label = String.Empty
            End If

            rowContents.Add(label)
            rowContents.Add(String.Join("#", fact.Values.Select(Function(v)
                                                                    If MathMLHelper.IsValidMathMlExpression(DirectCast(v, KeyValue).ToString(True)) Then
                                                                        Return My.Resources.Formula
                                                                    Else
                                                                        Return DirectCast(v, KeyValue).ToString(True)
                                                                    End If
                                                                End Function)))
            rowContents.Add(String.Join("&", rules.Select(Function(r) r.DisplayName).ToArray()))

            openXmlHelper.AddRow(itemSettingsTable, rowContents)
            rowContents.Clear()
        Next
    End Sub

    Private Function CanHavePreprocessorRules(scoringParameters As HashSet(Of ScoringParameter)) As Boolean
        For Each scoringParameter As ScoringParameter In scoringParameters
            If scoringParameter.GetType = GetType(StringScoringParameter) Then
                Return True
            End If
        Next

        Return False
    End Function

    ''' <summary>
    ''' Adds a new row to the table.
    ''' </summary>
    ''' <param name="fieldName">The name of the field (value for column 1).</param>
    ''' <param name="fieldValue">The value of the field (value for column 2).</param>
    ''' <param name="openXmlHelper">The OpenXmlGenerator to use.</param>
    ''' <param name="table">The table to add the row to.</param>
    Private Sub AddRowToTable(ByVal fieldName As String, ByVal fieldValue As String, ByRef openXmlHelper As OpenXmlGenerator, ByVal table As Table)
        Dim newList As New List(Of String)
        newList.Add(fieldName)
        newList.Add(fieldValue)
        openXmlHelper.AddRow(table, newList)
    End Sub

    ''' <summary>
    ''' Gets the custom properties for entity.
    ''' </summary>
    ''' <param name="entity">The entity.</param>
    Private Sub AddCustomPropertiesRowsForEntity(ByVal entity As ResourceEntity, ByRef openXmlHelper As OpenXmlGenerator, ByVal table As Table, ByVal entityName As String, ByVal worddoc As WordprocessingDocument)
        'get test custom properties
        If entity.CustomBankPropertyValueCollection.Any() Then
            For Each customBankPropertyValue As CustomBankPropertyValueEntity In entity.CustomBankPropertyValueCollection
                Dim customBankValueProperty As CustomBankPropertyValueEntity = GetCustomPropertyValue(customBankPropertyValue.CustomBankPropertyId, entity)
                If customBankValueProperty IsNot Nothing Then
                    'TODO: get the correct value (customBankValueProperty.ToString is not it!) - Check in the item editor how the value is retrieved. There are 3 possible value types (list, free value, ??).
                    Dim name As String = customBankPropertyValue.CustomBankProperty.Name
                    Dim fieldName As String = String.Format("{0}_{1}", entityName, name)
                    Dim value As String = String.Empty

                    Select Case customBankPropertyValue.CustomBankProperty.GetType.ToString
                        Case GetType(FreeValueCustomBankPropertyEntity).ToString
                            Dim freeValue As FreeValueCustomBankPropertyValueEntity = DirectCast(customBankPropertyValue, FreeValueCustomBankPropertyValueEntity)
                            value = freeValue.Value

                        Case GetType(ListCustomBankPropertyEntity).ToString
                            Dim listValue As ListCustomBankPropertyEntity = DirectCast(customBankValueProperty.CustomBankProperty, ListCustomBankPropertyEntity)
                            value = GetDisplayTextForSelectedValues(customBankValueProperty)
                    End Select

                    If (Not String.IsNullOrEmpty(value)) Then
                        AddRowToTable(fieldName, value, openXmlHelper, table)
                    End If
                End If
            Next
        Else
            table.Remove()
            openXmlHelper.AddText(worddoc, My.Resources.None)
        End If
    End Sub

    ''' <summary>
    ''' Gets the display text for selected values.
    ''' </summary>
    Private Function GetDisplayTextForSelectedValues(ByVal customBankPropertyValue As CustomBankPropertyValueEntity) As String
        Dim selectedValues As New StringBuilder

        Dim value As ListCustomBankPropertyValueEntity
        value = DirectCast(customBankPropertyValue, ListCustomBankPropertyValueEntity)

        For Each selectedValue As ListValueCustomBankPropertyEntity In value.ListValueCustomBankPropertyCollectionViaListCustomBankPropertySelectedValue
            ' If there already is a value in selectedValues, append a semicolumn to seperate the values.
            If selectedValues.Length > 0 Then
                selectedValues.Append(";")
            End If
            selectedValues.Append(selectedValue.ToString())
        Next

        Return selectedValues.ToString
    End Function

    ''' <summary>
    ''' Adds the reference rows for entity.
    ''' </summary>
    ''' <param name="collection">The  collection.</param>
    ''' <param name="openXmlHelper">The openxml helper.</param>
    ''' <param name="table">The item reference table.</param>
    Private Sub AddRowsForCollection(ByRef collection As EntityCollection, ByRef openXmlHelper As OpenXmlGenerator, ByRef table As Table, ByVal wordDoc As WordprocessingDocument)
        If collection.Any() Then
            Dim headerList As New List(Of String)
            headerList.Add(My.Resources.Type)
            headerList.Add(My.Resources.NameValue)
            headerList.Add(My.Resources.Title)

            openXmlHelper.AddRow(table, headerList, True)

            For Each reference As EntityBase2 In collection
                AddRowForRelatedEntity(reference, table, openXmlHelper)
            Next
        Else
            table.Remove()

            openXmlHelper.AddText(wordDoc, My.Resources.None)
        End If
    End Sub

    ''' <summary>
    ''' Adds the row for related entity.
    ''' </summary>
    ''' <param name="relatedEntity">The related entity.</param>
    ''' <param name="table">The table.</param>
    ''' <param name="openXmlHelper">The openxml helper.</param>
    Private Sub AddRowForRelatedEntity(ByRef relatedEntity As EntityBase2, ByRef table As Table, ByRef openXmlHelper As OpenXmlGenerator)
        Dim newList As New List(Of String)
        Dim type As String = GetTypeNameFromEntityBase(relatedEntity)
        Dim name As String = DirectCast(relatedEntity, ResourceEntity).Name
        Dim title As String = DirectCast(relatedEntity, ResourceEntity).Title
        newList.Add(type)
        newList.Add(name)
        newList.Add(title)
        openXmlHelper.AddRow(table, newList)
    End Sub


    ''' <summary>
    ''' Gets the type name from entity base.
    ''' </summary>
    ''' <param name="entity">The entity.</param><returns></returns>
    Private Function GetTypeNameFromEntityBase(ByVal entity As EntityBase2) As String
        Dim type As String = String.Empty
        Select Case entity.GetType.ToString
            Case GetType(ItemResourceEntity).ToString
                type = My.Resources.Item
            Case GetType(AssessmentTestResourceEntity).ToString
                type = My.Resources.Test
            Case GetType(GenericResourceEntity).ToString
                type = My.Resources.Media
            Case GetType(DataSourceResourceEntity).ToString
                type = My.Resources.Selection
            Case GetType(ItemLayoutTemplateResourceEntity).ToString
                type = My.Resources.ItemLayoutTemplate
            Case GetType(ControlTemplateResourceEntity).ToString
                type = My.Resources.ControlTemplate
        End Select
        Return type
    End Function

    ''' <summary>
    ''' Gets the custom property value.
    ''' </summary>
    ''' <param name="CustomBankPropertyId">The custom bank property id.</param>

    Private Function GetCustomPropertyValue(ByVal CustomBankPropertyId As Guid, ByVal entity As ResourceEntity) As CustomBankPropertyValueEntity
        Dim filter As IPredicate
        Dim indexes As List(Of Integer)

        ' Try to locate value for custombankproperty
        filter = (CustomBankPropertyValueFields.CustomBankPropertyId = CustomBankPropertyId)
        indexes = entity.CustomBankPropertyValueCollection.FindMatches(filter)

        ' Return found entity or nothing
        If indexes.Count = 1 Then
            Return entity.CustomBankPropertyValueCollection(indexes(0))
        Else
            Return Nothing
        End If
    End Function


    ''' <summary>
    ''' Gets the name of the resource string by resource.
    ''' </summary>
    ''' <param name="resourceName">Name of the resource.</param><returns></returns>
    Private Function GetResourceStringByName(ByVal resourceName As String) As String
        Dim returnValue As String = String.Empty
        Try
            returnValue = My.Resources.ResourceManager.GetString(resourceName)
        Catch ex As Exception
            returnValue = resourceName
        End Try
        If returnValue Is Nothing Then
            returnValue = resourceName
        End If
        Return returnValue
    End Function

    Public Sub CancelReportGeneration() Implements IReportHandlerAsync.CancelReportGeneration
        If (Me._screenshotHelper IsNot Nothing) Then
            Me._screenshotHelper.Cancel()
        End If
    End Sub

#End Region
End Class
