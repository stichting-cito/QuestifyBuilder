Imports Questify.Builder.Security
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic
Imports System.Windows.Forms

Public Class Word_AssessmentTestPropertiesEditor
    Implements IAssessmentTestPropertyEditor


    Private _assessmentTestModel As AssessmentTest2
    Private _assessmentTest As WordAssessmentTest



    Public Sub New()
        InitializeComponent()

    End Sub



    Public Property CurrentDataSource() As AssessmentTest2 Implements IAssessmentTestPropertyEditor.CurrentDataSource
        Get
            Return _assessmentTestModel
        End Get
        Set(ByVal value As AssessmentTest2)
            _assessmentTestModel = value
            If value IsNot Nothing Then
                _assessmentTest = New WordAssessmentTest(value)
                ControlBindingSource.DataSource = _assessmentTest
            Else
                ControlBindingSource.DataSource = Nothing
                _assessmentTest = Nothing
            End If
        End Set
    End Property

    Public Overrides ReadOnly Property FrameTitle() As String
        Get
            Return My.Resources.Word_AssessmentTestPropertiesEditor_FrameTitle
        End Get
    End Property

    Public Overrides ReadOnly Property HasFieldsToFillByUser() As Boolean
        Get
            Return True
        End Get
    End Property


    Private Sub Word_AssessmentTestPropertiesEditor_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.PrintFormEditor1.TestEntity = Me.TestEntity
        AddHandler Me.PrintFormEditor1.DependentResourceAdded, AddressOf DependentResourceIsAdded
        AddHandler Me.PrintFormEditor1.DependentResourceRemoved, AddressOf DependentResourceIsRemoved
    End Sub
    Private Sub DependentResourceIsRemoved(ByVal sender As Object, ByVal e As ResourceNameEventArgs)
        OnDependentResourceRemoved(e)
    End Sub

    Private Sub DependentResourceIsAdded(ByVal sender As Object, ByVal e As ResourceEventArgs)
        OnDependentResourceAdded(e)
    End Sub



    Public Overrides Sub HandleTestDesignPermissionChange(ByVal permission As TestDesignPermission)
        For Each ctrl As Control In Me.Controls
            ctrl.Enabled = Not (permission = TestDesignPermission.Minimal)
        Next
    End Sub


End Class