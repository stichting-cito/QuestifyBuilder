Imports System.ComponentModel
Imports Questify.Builder.Security
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic

Public Class Word_TestSectionPropertiesEditor
    Implements ITestSectionPartPropertyEditor

    Private _testSection As WordTestSection
    Private _testSectionModel As TestSection2

    Public Property CurrentDataSource() As TestSection2 Implements ITestSectionPartPropertyEditor.CurrentDataSource
        Get
            Return _testSectionModel
        End Get
        Set(ByVal value As TestSection2)
            _testSectionModel = value
            If value IsNot Nothing Then
                _testSection = New WordTestSection(value)

                ControlBindingSource.DataSource = _testSection
            Else
                _testSection = Nothing
                ControlBindingSource.DataSource = Nothing
            End If
        End Set
    End Property

    Public Overrides ReadOnly Property FrameTitle() As String
        Get
            Return My.Resources.Word_TestSectionPropertiesEditor_FrameTitle
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

    <Description("This event will be raised when this control needs the 'select resource' dialog."), Category("SectionLogicSettingsControl events")>
    Public Event ResourceDialogRequest As EventHandler(Of ResourceDialogRequestEventArgs) Implements ITestSectionPartPropertyEditor.ResourceDialogRequest

    <Description("This event will be raised when a dependent resource is changed in a section logic settings control."), Category("TestSectionPropertiesEditor events")>
    Public Event SectionItemDatasourceDependentResourceChanged As EventHandler(Of SectionLogicSettingsDependencyChangedEventArgs) Implements ITestSectionPartPropertyEditor.SectionItemDatasourceDependentResourceChanged

End Class