Imports Questify.Builder.Security
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic

Public Class General_TestSetPropertiesEditor
    Implements ITestSetEditorPropertyEditor

    Private _testSet As GeneralTestSet
    Private _testSetModel As TestSet



    Public Overrides ReadOnly Property FrameTitle() As String
        Get
            Return My.Resources.General_TestSetPropertiesEditor_FrameTitle
        End Get
    End Property

    Public Overrides ReadOnly Property HasFieldsToFillByUser() As Boolean
        Get
            Return True
        End Get
    End Property

    Public Property TestSet() As TestSet Implements ITestSetEditorPropertyEditor.CurrentDataSource
        Get
            Return _testSetModel
        End Get
        Set(value As TestSet)
            _testSetModel = value

            If _testSetModel IsNot Nothing Then
                _testSet = New GeneralTestSet(_testSetModel)
            End If

            ControlBindingSource.DataSource = _testSet
        End Set
    End Property


    Public Overrides Sub HandleTestPackageDesignPermissionChange(permission As TestDesignPermission)
        For Each ctrl As Control In DetailTableLayoutPanel.Controls
            If permission = TestDesignPermission.Minimal Then
                ctrl.Enabled = ctrl Is TitleLabel OrElse ctrl Is TitleTextBox
            End If
        Next
    End Sub
End Class