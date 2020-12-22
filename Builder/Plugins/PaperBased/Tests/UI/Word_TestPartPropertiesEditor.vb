Imports Questify.Builder.Security
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic

Public Class Word_TestPartPropertiesEditor
    Implements ITestPartPropertyEditor


    Private _testPart As WordTestPart
    Private _testPartModel As TestPart2
    Private _assessmentTest As AssessmentTest2



    Public Sub New(ByVal assessmentTest As AssessmentTest2)
        InitializeComponent()

        _assessmentTest = assessmentTest
    End Sub



    Public Property CurrentDataSource() As TestPart2 Implements ITestPartPropertyEditor.CurrentDataSource
        Get
            Return _testPartModel
        End Get
        Set(ByVal value As TestPart2)
            _testPartModel = value
            If value IsNot Nothing Then
                _testPart = New WordTestPart(value, _assessmentTest)
                ControlBindingSource.DataSource = _testPart
            Else
                ControlBindingSource.DataSource = Nothing
            End If
        End Set
    End Property

    Public Overrides ReadOnly Property FrameTitle() As String
        Get
            Return My.Resources.Word_TestPartPropertiesEditor_FrameTitle
        End Get
    End Property

    Public Overrides ReadOnly Property HasFieldsToFillByUser() As Boolean
        Get
            Return True
        End Get
    End Property



    Public Overrides Sub HandleTestDesignPermissionChange(ByVal permission As TestDesignPermission)
        For Each ctrl As System.Windows.Forms.Control In DetailTableLayoutPanel.Controls
            ctrl.Enabled = Not (permission = TestDesignPermission.Minimal)
        Next
    End Sub


End Class