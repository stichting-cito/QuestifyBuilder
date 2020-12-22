Imports System.Linq
Imports Questify.Builder.IoC
Imports Questify.Builder.Logic

Public Class SelectSupportedViewsForAssessmentTestDialog

    Private ReadOnly _initialCheckedViewTypes As List(Of String)
    Private ReadOnly _isTest As Boolean = False
    Private ReadOnly _mode As DialogDisplayMode

    Public Enum DialogDisplayMode
        NewTestTemplate
        AddOrDeleteViewTypesOnTest
    End Enum

    Private Sub SelectSupportedViewsForAssessmentTestDialog_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If _isTest Then
            Me.Text = My.Resources.SelectTargetsForTests
        Else
            Me.Text = My.Resources.SelectTargetsForTestPackages
        End If
        Dim viewTypes As New List(Of String)
        viewTypes.Add(My.Resources.GenericTest_SupportedView)
        If _isTest Then
            viewTypes.AddRange(IoCHelper.GetInstances(Of ITestEditorPlugin)().Select(Function(p) p.Description).ToList())
        Else
            viewTypes.AddRange(IoCHelper.GetInstances(Of ITestPackageEditorPlugin)().Select(Function(p) p.Description).ToList())
        End If
        ViewTypesCheckedListBox.DataSource = viewTypes

        SetInitialViewTypesInListBox()

        If _mode = DialogDisplayMode.NewTestTemplate Then
            If _isTest Then
                ContentLabel.Text = My.Resources.SelectSupportedViewsForAssessmentTestDialog_ContentLabel_NewTest
            Else
                ContentLabel.Text = My.Resources.SelectSupportedViewsForTestPackageDialog_ContentLabel_NewTestPackage
            End If
        Else
            If _isTest Then
                ContentLabel.Text = My.Resources.SelectSupportedViewsForAssessmentTestDialog_ContentLabel_AddRemoveViewTypes
            Else
                ContentLabel.Text = My.Resources.SelectSupportedViewsForTestPackageDialog_ContentLabel_AddRemoveViewTypes
            End If
        End If
    End Sub

    Private Sub SetInitialViewTypesInListBox()
        ViewTypesCheckedListBox.ClearSelected()
        ViewTypesCheckedListBox.SetItemChecked(ViewTypesCheckedListBox.Items.IndexOf(My.Resources.GenericTest_SupportedView), True)

        If _initialCheckedViewTypes Is Nothing Then
            Return
        End If

        If _isTest Then
            Dim toBeChecked = IoCHelper.GetInstances(Of ITestEditorPlugin)().Where(Function(p) _initialCheckedViewTypes.Contains(p.Name)).ToList()

            For Each selectViewType In toBeChecked
                Dim index As Int32 = ViewTypesCheckedListBox.Items.IndexOf(selectViewType.Description)

                If index > -1 Then
                    ViewTypesCheckedListBox.SetItemChecked(index, True)
                End If
            Next
        Else
            Dim toBeChecked = IoCHelper.GetInstances(Of ITestPackageEditorPlugin)().Where(Function(p) _initialCheckedViewTypes.Contains(p.Name)).ToList()

            For Each selectViewType In toBeChecked
                Dim index As Int32 = ViewTypesCheckedListBox.Items.IndexOf(selectViewType.Description)

                If index > -1 Then
                    ViewTypesCheckedListBox.SetItemChecked(index, True)
                End If
            Next
        End If
    End Sub

    Public ReadOnly Property SelectedViewTypes() As List(Of String)
        Get
            Dim returnValue As New List(Of String)

            If _isTest Then
                returnValue.Add(GenericTestModelPlugin.PLUGIN_NAME)
                Dim available = IoCHelper.GetInstances(Of ITestEditorPlugin)().ToList()
                For Each checkedViewType As String In ViewTypesCheckedListBox.CheckedItems
                    Dim plugin = available.FirstOrDefault((Function(p) p.Description = checkedViewType))
                    If plugin IsNot Nothing Then
                        returnValue.Add(plugin.Name)
                    End If
                Next
            Else
                Dim available = IoCHelper.GetInstances(Of ITestPackageEditorPlugin)().ToList()
                For Each checkedViewType As String In ViewTypesCheckedListBox.CheckedItems
                    Dim plugin = available.FirstOrDefault((Function(p) p.Description = checkedViewType))
                    If plugin IsNot Nothing Then
                        returnValue.Add(plugin.Name)
                    End If
                Next
            End If

            Return returnValue
        End Get
    End Property

    Private Sub Cancel_Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub OK_Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles OK_Button.Click
        If ViewTypesCheckedListBox.CheckedItems.Count > 0 Then
            Me.DialogResult = DialogResult.OK
            Me.Close()
        Else
            MessageBox.Show(My.Resources.SelectSupportedViewsForAssessmentTestDialog_PleaseSelectViews_Message, My.Resources.SelectSupportedViewsForAssessmentTestDialog_PleaseSelectViews_Title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    Public Sub New(ByVal mode As DialogDisplayMode, isTest As Boolean)

        InitializeComponent()

        _mode = mode
        _isTest = isTest
    End Sub

    Public Sub New(ByVal mode As DialogDisplayMode, ByVal initialCheckedViewTypes As List(Of String), isTest As Boolean)
        Me.New(mode, isTest)
        _isTest = isTest
        _initialCheckedViewTypes = initialCheckedViewTypes
    End Sub

    Private Sub ViewTypesCheckedListBox_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles ViewTypesCheckedListBox.ItemCheck
        If (e.Index = 0 andalso e.CurrentValue = CheckState.Checked) Then
            e.NewValue = e.CurrentValue
        End If
    End Sub
End Class