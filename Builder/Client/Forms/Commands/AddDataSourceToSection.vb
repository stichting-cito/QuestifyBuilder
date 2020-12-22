Imports Cito.Tester.ContentModel
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Logic
Imports Cito.Tester.ContentModel.Datasources
Imports System.Linq
Imports Cito.Tester.Common
Imports Questify.Builder.Logic.ResourceManager
Imports Questify.Builder.Logic.TestConstruction.Helpers
Imports Questify.Builder.Logic.TestConstruction
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Model.ContentModel.FactoryClasses
Imports Questify.Builder.UI
Imports Questify.Builder.UI.Commanding

Friend Class AddDataSourceToSection
    Inherits CommandBase

    Const CInclusion As String = "inclusion"
    Const CNormal As String = "normal"

    Private ReadOnly _bankId As Integer
    Private ReadOnly _displayText As String
    Private ReadOnly _assessment As AssessmentTest2
    Private ReadOnly _testResourceEntity As AssessmentTestResourceEntity
    Private ReadOnly _resourceManager As DataBaseResourceManager
    Private ReadOnly _facade As TestConstructionFacade
    Private ReadOnly _refreshControlAction As MethodInvoker

    Public Sub New(ByVal bankId As Integer,
                   ByVal displayText As String,
                   ByVal assessment As AssessmentTest2,
                   ByVal testResourceEntity As AssessmentTestResourceEntity,
                   ByVal resourceManager As DataBaseResourceManager,
                   ByVal facade As TestConstructionFacade,
                   ByVal refreshControlAction As MethodInvoker
                   )
        If (String.IsNullOrEmpty(displayText)) Then Throw New ArgumentException("displayText")
        If (assessment Is Nothing) Then Throw New ArgumentException("assessment")
        If (testResourceEntity Is Nothing) Then Throw New ArgumentException("testResourceEntity")
        If (resourceManager Is Nothing) Then Throw New ArgumentException("resourceManager")
        If (facade Is Nothing) Then Throw New ArgumentException("facade")
        If (refreshControlAction Is Nothing) Then Throw New ArgumentException("facade")

        _bankId = bankId
        _displayText = displayText
        _assessment = assessment
        _testResourceEntity = testResourceEntity
        _resourceManager = resourceManager
        _facade = facade
        _refreshControlAction = refreshControlAction
    End Sub

    Public Overrides Sub Execute(parameter As Object)
        If (TypeOf parameter Is TestSection2) Then
            DoCommand(DirectCast(parameter, TestSection2))
        Else
            DoCommand(DirectCast(DirectCast(parameter, ItemReference2).Parent, TestSection2))
        End If
    End Sub


    Protected Overrides Function GetCanExecuteState(parameter As Object) As Boolean
        Return (TypeOf parameter Is TestSection2 AndAlso Not DirectCast(parameter, TestSection2).LockedForEdit) OrElse (TypeOf parameter Is ItemReference2 AndAlso Not DirectCast(parameter, ItemReference2).Parent.LockedForEdit)
    End Function

    Public Overrides ReadOnly Property Image As Image
        Get
            Return My.Resources.AddItem24x24_Image
        End Get
    End Property

    Public Overrides ReadOnly Property Name As String
        Get
            Return _displayText
        End Get
    End Property

    Public Overrides ReadOnly Property NameLocalized As String
        Get
            Return _displayText
        End Get
    End Property


    Sub DoCommand(ByVal section As TestSection2)
        Dim dialog As New SelectDataSourceResourceDialog(_bankId, _assessment.TestParts.SelectMany(function(tp) tp.Sections.Where(function(s) s IsNot section AndAlso Not String.IsNullOrEmpty(s.ItemDataSource)).Select(function(s) s.ItemDataSource)).ToList(), False, CNormal, CInclusion)
        dialog.AllowMultiSelect = False

        Select Case dialog.ShowDialog()
            Case DialogResult.OK
                If Not dialog.EntitiesProhibitedToSelect.Contains(dialog.SelectedEntity.resourceId) Then
                    Dim entity = ResourceFactory.Instance.GetResourceByIdWithOption(dialog.SelectedEntity.resourceId, New DataSourceResourceEntityFactory(), New ResourceRequestDTO())
                    Dim settings As DataSourceSettings = Parsers.ParseItemDataSourceSettingsFromResourceEntity(entity)
                    If settings IsNot Nothing Then
                        Dim items As IEnumerable(Of ResourceRef) = CType(settings.CreateGetDataSource(), ItemDataSource).Get(_resourceManager)
                        If TestConstructionOp.AddItemsToTest(_assessment, _testResourceEntity, _resourceManager, items.ToList(), section, 0, _facade) Then
                            section.ItemDataSource = entity.Name
                            section.ItemDataSourceBehaviour = settings.Behaviour
                        End If
                        _refreshControlAction()
                    Else
                        Debug.Assert(False, "Settings not found in datasource")
                    End If
                Else
                    MessageBox.Show(My.Resources.SelectResourceDialog_CannotSelectBecauseOfStatus, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
        End Select
    End Sub

End Class
