Imports Questify.Builder.Security
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic

Public Class General_TestPartPropertiesEditor
    Implements ITestPartPropertyEditor

    Private _testPartModel As TestPart2
    Private _testPart As GeneralTestPart
    Private _assessmentTest As AssessmentTest2

    Public Sub New(ByVal assessmentTest As AssessmentTest2)
        InitializeComponent()

        _assessmentTest = assessmentTest
    End Sub

    Public Property CurrentDataSource As TestPart2 Implements ITestPartPropertyEditor.CurrentDataSource
        Get
            Return _testPartModel
        End Get
        Set
            _testPartModel = Value

            If Value IsNot Nothing Then
                _testPart = New GeneralTestPart(Value, _assessmentTest)
            Else
                _testPart = Nothing
            End If

            ControlBindingSource.DataSource = _testPart
        End Set
    End Property

    Public Overrides ReadOnly Property FrameTitle As String
        Get
            Return My.Resources.General_TestPartPropertiesEditor_FrameTitle
        End Get
    End Property

    Public Overrides ReadOnly Property HasFieldsToFillByUser As Boolean
        Get
            Return True
        End Get
    End Property

    Public Overrides Sub HandleTestDesignPermissionChange(permission As TestDesignPermission)
        For Each ctrl As Control In DetailTableLayoutPanel.Controls
            If permission = TestDesignPermission.Minimal Then
                ctrl.Enabled = ctrl Is TitleLabel OrElse ctrl Is TitleTextBox
            Else
                ctrl.Enabled = True
            End If
        Next
    End Sub

End Class