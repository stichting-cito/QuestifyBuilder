
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Cito.Tester.ContentModel
Imports System.Collections.ObjectModel
Imports System.Linq
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports Questify.Builder.UI.PublicationService
Imports Questify.Builder.Logic.Service.Factories

Public Class SelectTestPreviewDialog


    Private _factoryResult As ReturnedAssessmentTestModelInfo
    Private ReadOnly _testPackage As TestPackage
    Private _shouldChooseHandler As Boolean
    Private _shouldChooseTest As Boolean
    Private ReadOnly _bankId As Integer
    Private ReadOnly _handlers As TestPreviewHandlerIdentifier()


    Public Sub New()
        InitializeComponent()
    End Sub

    Public Sub New(ByVal factoryResult As ReturnedAssessmentTestModelInfo, handlers As TestPreviewHandlerIdentifier())
        Me.New()
        _factoryResult = factoryResult
        _handlers = handlers
    End Sub

    Public Sub New(ByVal testPackage As TestPackage, bankId As Integer, handlers As TestPreviewHandlerIdentifier())
        Me.New()
        _testPackage = testPackage
        _bankId = bankId
        _handlers = handlers
    End Sub




    Private Function GetTests(testRefCollection As ReadOnlyCollection(Of TestReference), bankId As Integer) As List(Of KeyValuePair(Of String, String))
        Dim returnValue As New List(Of KeyValuePair(Of String, String))
        Dim listOfTest As New List(Of String)
        For Each testRef As TestReference In testRefCollection
            listOfTest.Add(testRef.Title)
        Next
        Using testcollection As EntityCollection(Of AssessmentTestResourceEntity) = ResourceFactory.Instance.GetTestsByCodes(listOfTest, bankId, False)
            For Each test As AssessmentTestResourceEntity In testcollection
                returnValue.Add(New KeyValuePair(Of String, String)(test.Name, test.Title))
            Next
        End Using
        Return returnValue
    End Function

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub ButtonOK_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonOK.Click
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub SelectTestPreviewDialog_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        FillComboBoxWithHandlers()
        InitialiseTestReference()
        InitialiseTestForm()
    End Sub



    Private Sub InitialiseTestForm()
        SelectTestPanel.Visible = _shouldChooseTest
        SelectPreviewMethodPanel.Visible = _shouldChooseHandler
        If Not _shouldChooseHandler OrElse Not _shouldChooseTest Then
            Me.Height -= 45
        End If
    End Sub


    Private Sub InitialiseTestReference()
        If _testPackage IsNot Nothing Then
            Dim testRefCollection As ReadOnlyCollection(Of TestReference) = _testPackage.GetAllTestReferencesInTestPackage
            If testRefCollection.Count > 1 Then
                _shouldChooseTest = True
                SelectTestComboBox.DataSource = New BindingSource(GetTests(testRefCollection, _bankId), Nothing)
                SelectTestComboBox.DisplayMember = "Value"
                SelectTestComboBox.ValueMember = "Key"
            ElseIf testRefCollection.Count = 1 Then
                _shouldChooseTest = False
                _SelectedTestId = testRefCollection(0).SourceName
            Else
                Throw New Exception($"Unexpected number of testreferences '{testRefCollection.Count}; ")
            End If
        End If
    End Sub

    Private Sub FillComboBoxWithHandlers()
        _shouldChooseHandler = _handlers.Count > 1
        If (_handlers.Count = 1) Then SelectedTestPreviewHandler = _handlers(0)

        For Each h In _handlers
            SelectTestPreviewComboBox.Items.Add(h.UserFriendlyName)
        Next
    End Sub

    Private Sub SelectTestPreviewComboBox_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles SelectTestPreviewComboBox.SelectionChangeCommitted
        If SelectTestPreviewComboBox.SelectedItem IsNot Nothing Then
            SelectedTestPreviewHandler = _handlers.Where(Function(h) h.UserFriendlyName = SelectTestPreviewComboBox.SelectedItem.ToString)(0)
            ReEvaluateButtonStatus()
        End If
    End Sub

    Private Sub SelectTestComboBox_SelectedValueChanged(sender As Object, e As EventArgs) Handles SelectTestComboBox.SelectedValueChanged
        If SelectTestComboBox.SelectedValue IsNot Nothing Then
            SelectedTestId = SelectTestComboBox.SelectedValue.ToString
            ReEvaluateButtonStatus()
        End If
    End Sub



    Public Property SelectedTestPreviewHandler As TestPreviewHandlerIdentifier
    Public Property SelectedTestId As String


    Private Sub ReEvaluateButtonStatus()
        ButtonOK.Enabled = (Not _shouldChooseHandler OrElse SelectedTestPreviewHandler IsNot Nothing) AndAlso ((Not _shouldChooseTest) OrElse (_shouldChooseTest AndAlso Not String.IsNullOrEmpty(SelectedTestId)))
    End Sub


End Class