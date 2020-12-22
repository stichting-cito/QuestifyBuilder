Imports System.IO
Imports System.Windows.Forms
Imports Questify.Builder.Configuration
Imports Cito.Tester.ContentModel
Imports Cito.Tester.Common
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Logic.ResourceManager
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Logic.Service.Interfaces
Imports Questify.Builder.Logic
Imports Questify.Builder.Plugins.PaperBased.Publication.UI

Public NotInheritable Class WordPublicationHandler
    Implements IPublicationHandler

    Private _progressIndex As Integer = 0
    Private _printFormType As PrintFormType
    Private _optionsControl As SelectPublicationOptions
    Private _resourceManager As DataBaseResourceManager


    Public ReadOnly Property Urls As Dictionary(Of String, String) Implements IPublicationHandler.Urls
        Get
            Return New Dictionary(Of String, String)
        End Get
    End Property

    Public ReadOnly Property FileExtension() As String Implements IPublicationHandler.FileExtension
        Get
            Return ".docx"
        End Get
    End Property

    Public Property ExportedFiles() As Dictionary(Of String, String) Implements IPublicationHandler.ExportedFiles

    Public ReadOnly Property ProgressMessage() As String Implements IPublicationHandler.ProgressMessage
        Get
            Return My.Resources.PublishingDocumentToSpecifiedLocation
        End Get
    End Property

    Public ReadOnly Property UserFriendlyName() As String Implements IPublicationHandler.UserFriendlyName
        Get
            Return My.Resources.PublishToWord

        End Get
    End Property

    Public ReadOnly Property ShowTargetFileLocation As Boolean Implements IPublicationHandler.ShowTargetFileLocation
        Get
            Return True
        End Get
    End Property
    Public ReadOnly Property ShowPublicationOptions As Boolean Implements IPublicationHandler.ShowPublicationOptions
        Get
            Return True
        End Get
    End Property

    Public ReadOnly Property ShowFileResultsAsUrl As Boolean Implements IPublicationHandler.ShowFileResultsAsUrl
        Get
            Return True
        End Get
    End Property

    Public Property ConfigurationOptions As Dictionary(Of String, String) Implements IPublicationHandler.ConfigurationOptions

    Public Property PublicationPath As String Implements IPublicationHandler.PublicationPath
        Get
            Return _optionsControl.PublicationPath
        End Get
        Set()
        End Set
    End Property

    Public Property PublicationSelection As IPublicationSelection Implements IPublicationHandler.PublicationSelection


    Public Property PublicationOptionsControl As UserControl Implements IPublicationHandler.PublicationOptionsControl
        Get
            If _optionsControl Is Nothing Then
                _optionsControl = New SelectPublicationOptions(Me.PublicationSelection, ConfigurationOptions)
            End If
            Return _optionsControl
        End Get
        Set
        End Set
    End Property

    Public ReadOnly Property CanHandleMultipleTests() As Boolean Implements IPublicationHandler.CanHandleMultipleTests
        Get
            Return False
        End Get
    End Property

    Public ReadOnly Property CanHandleSingleTests() As Boolean Implements IPublicationHandler.CanHandleSingleTests
        Get
            Return True
        End Get
    End Property

    Public ReadOnly Property CanHandleMultipleTestPackages As Boolean Implements IPublicationHandler.CanHandleMultipleTestPackages
        Get
            Return False
        End Get
    End Property

    Public ReadOnly Property CanHandleSingleTestPackages As Boolean Implements IPublicationHandler.CanHandleSingleTestPackages
        Get
            Return False
        End Get
    End Property

    Public ReadOnly Property CanHandleBanks As Boolean Implements IPublicationHandler.CanHandleBanks
        Get
            Return False
        End Get
    End Property

    Public ReadOnly Property SupportedViews() As List(Of String) Implements IPublicationHandler.SupportedViews
        Get
            Return New List(Of String)(New String() {PaperBasedTestPlugin.PLUGIN_NAME})
        End Get
    End Property

    Public ReadOnly Property Errors As String = String.Empty Implements IPublicationHandler.Errors

    Public ReadOnly Property Warnings As String = String.Empty Implements IPublicationHandler.Warnings

    Public Property HandlerConfig As PluginHandlerConfigCollection Implements IPublicationHandler.HandlerConfig
    Public Property FilePath As String Implements IPublicationHandler.FilePath
        Get
            Return _optionsControl?.PackageName
        End Get
        Set(value As String)
        End Set
    End Property


    Public Event Progress(ByVal sender As Object, ByVal e As Cito.Tester.Common.ProgressEventArgs) Implements IPublicationHandler.Progress
    Public Event StartProgress(ByVal sender As Object, ByVal e As StartEventArgs) Implements IPublicationHandler.StartProgress



    Public Function IsValid() As Boolean Implements IPublicationHandler.IsValid
        Return _optionsControl.IsValid()
    End Function

    Public Function CheckItemsSupportedViews() As Boolean Implements IPublicationHandler.CheckItemsSupportedViews
        Return False
    End Function

    Function GetConfigurationOptions(
                                    bankId As Integer,
                                    testNames As IList(Of String),
                                    testPackageNames As IList(Of String)) As Dictionary(Of String, String) Implements IPublicationHandler.GetConfigurationOptions

        Dim configurationOptions As Dictionary(Of String, String) = New Dictionary(Of String, String)

        For Each testResource As AssessmentTestResourceEntity In ResourceFactory.Instance.GetResourcesByNamesWithOption(bankId, testNames.ToList(), New ResourceRequestDTO())
            Dim serializedTest As Byte() = ResourceFactory.Instance.GetResourceData(testResource).BinData
            Dim deserializedTest As ReturnedAssessmentTestModelInfo = AssessmentTestv2Factory.ReturnAssessmentTestv2ModelFromByteArray(serializedTest, True)
            Dim wordAssessmentTest As WordAssessmentTest = AssessmentTestv2Factory.CreateView(Of WordAssessmentTest)(deserializedTest.AssessmentTestv2)
            If wordAssessmentTest IsNot Nothing Then
                configurationOptions(PublicationHandlerConfigurationOptions.PrintForms) = String.Join(","c, wordAssessmentTest.PrintForm.Select(Function(pf) pf.Type.ToString() + "|" + pf.Id.ToString() + "|" + pf.TypeLabel).ToArray())
            End If
        Next

        Return configurationOptions
    End Function

    Public Function Publish(
                           configOptions As Dictionary(Of String, String),
                           bankId As Integer,
                           testNames As IList(Of String),
                           testPackageNames As IList(Of String),
                           publicationFolder As String,
                           isForPreview As Boolean,
                           customName As String) As Boolean Implements IPublicationHandler.Publish

        Dim openXmlGenerator As New OpenXmlGenerator()
        _resourceManager = New DataBaseResourceManager(bankId)
        _resourceManager.IncludeMetaData = ResourceManager.MetaDataType.Publishable
        ExportedFiles = New Dictionary(Of String, String)()

        AddHandler openXmlGenerator.HandlerProgress, AddressOf HandlerProgress
        Dim publicationDirectoryInfo As DirectoryInfo = IO.Directory.CreateDirectory(publicationFolder)

        Dim testName As String = testNames.First()
        Dim testResource As AssessmentTestResourceEntity = CType(ResourceFactory.Instance.GetResourceByNameWithOption(bankId, testName, New ResourceRequestDTO()), AssessmentTestResourceEntity)
        Dim serializedTest As Byte() = ResourceFactory.Instance.GetResourceData(testResource).BinData
        Dim deserializedTest As ReturnedAssessmentTestModelInfo = AssessmentTestv2Factory.ReturnAssessmentTestv2ModelFromByteArray(serializedTest, True)
        Dim wordAssessmentTest As WordAssessmentTest = AssessmentTestv2Factory.CreateView(Of WordAssessmentTest)(deserializedTest.AssessmentTestv2)

        Dim newDictionary As New Dictionary(Of PrintForm, String)
        Dim selectedPrintForms As String = If(configOptions.ContainsKey(PublicationHandlerConfigurationOptions.PrintForms), configOptions(PublicationHandlerConfigurationOptions.PrintForms), String.Empty)
        If String.IsNullOrEmpty(selectedPrintForms) Then
            Dim firstPrintForm As PrintForm
            firstPrintForm = wordAssessmentTest.PrintForm.FirstOrDefault(Function(f) f.Type = PrintFormType.QuestionBooklet)
            If (firstPrintForm Is Nothing) Then
                firstPrintForm = wordAssessmentTest.PrintForm.FirstOrDefault()
            End If
            If firstPrintForm IsNot Nothing Then
                selectedPrintForms = $"{firstPrintForm.Type.ToString()}|{firstPrintForm.Id}"
            End If
        End If
        For Each printForm As PrintForm In GetPrintForms(wordAssessmentTest, selectedPrintForms)
            Dim publicationName As String = $"{testName}_{printForm.TypeLabel}.docx"
            Dim newBooklet As String = Path.Combine(publicationFolder, publicationName)
            Dim resourceName As String = printForm.ResourceName
            Dim eventArgs As New ResourceNeededEventArgs(resourceName, AddressOf StreamConverters.ConvertStreamToByteArray)
            ResourceNeeded(Me, eventArgs)
            If eventArgs.BinaryResource IsNot Nothing AndAlso eventArgs.BinaryResource.ResourceObject IsNot Nothing Then
                If FileHelper.MakeFileFromByteArray(newBooklet, DirectCast(eventArgs.BinaryResource.ResourceObject, Byte())) Then
                    newDictionary.Add(printForm, newBooklet)
                    ExportedFiles.Add(publicationDirectoryInfo.Name + "/" + publicationName, newBooklet)
                End If
            End If
        Next

        openXmlGenerator.LoadAndProcessTest(_resourceManager, testName, newDictionary)
        _resourceManager.Dispose()

        Return True
    End Function

    Public Function PublishItem(ByVal assessmentItem As AssessmentItem, ByVal resourceManager As ResourceManagerBase) As String Implements IPublicationHandler.PublishItem
        Throw New NotImplementedException()
    End Function

    Public Function PublishItem(ByVal packageFile As IO.FileInfo, ByVal isEncryptedPackage As Boolean, ByVal assessmentItem As AssessmentItem) As Boolean Implements IPublicationHandler.PublishItem
        Throw New NotImplementedException()
    End Function



    Private Function GetPrintForms(wordAssessmentTest As WordAssessmentTest, selectedPrintFormsString As String) As List(Of PrintForm)

        Dim printforms As List(Of PrintForm) = New List(Of PrintForm)
        Dim selectedPrintForms As String() = selectedPrintFormsString.Split(","c)

        Dim selectedPrintFormTypes As String() = selectedPrintForms.Select(Function(x) x.Split("|"c)(0)).ToArray()
        Dim selectedPrintFormIds As String() = selectedPrintForms.Where(Function(x) x.Split("|"c).Length > 1).Select(Function(x) x.Split("|"c)(1)).ToArray()

        For Each printForm As PrintForm In wordAssessmentTest.PrintForm
            If (printForm.Type() = _printFormType And selectedPrintForms.Length = 0) OrElse
                (printForm.Type <> PrintFormType.UserDefinedBooklet AndAlso selectedPrintFormTypes.Contains(printForm.Type.ToString)) OrElse
                (printForm.Type = PrintFormType.UserDefinedBooklet AndAlso selectedPrintFormIds.Contains(printForm.Id.ToString())) Then

                printforms.Add(printForm)
            End If
        Next

        Return printforms
    End Function

    Private Sub ResourceNeeded(ByVal sender As Object, ByVal e As ResourceNeededEventArgs)
        Dim resource As BinaryResource = Nothing
        Dim request = New ResourceRequestDTO()
        If (e.Command And ResourceNeededCommand.Resource) = ResourceNeededCommand.Resource Then
            If e.TypedResourceType IsNot Nothing Then
                resource = _resourceManager.GetTypedResource(e.ResourceName, e.TypedResourceType, request)
            Else
                resource = _resourceManager.GetResource(e.ResourceName, e.StreamProcessingDelegate, request)
            End If
            e.BinaryResource = resource
        End If
    End Sub

    Private Sub HandlerProgress(ByVal sender As System.Object, ByVal e As ProgressEventArgs)
        If _progressIndex = 0 Then
            RaiseEvent StartProgress(Me, New StartEventArgs(e.Maximum))
            Application.DoEvents()
        End If
        RaiseEvent Progress(Me, New Cito.Tester.Common.ProgressEventArgs(e.Message, _progressIndex))
        _progressIndex += 1
    End Sub

End Class
