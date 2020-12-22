Imports System.Linq
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Logic.Service.Model.Entities

Public Class SimpleSearchControl
    Private Sub FillTestList(ByVal tests As IEnumerable(Of AssessmentTestResourceDto))
        TestComboBox.Items.Clear()
        TestComboBox.Items.Add(New TestInListBox(Guid.Empty, My.Resources.SimpleSearchControl_SearchInAllTestsDropDownListOption))
        For Each testInBank In tests
            Dim displayString = String.Format("{0} - [{1}]", testInBank.name, testInBank.title)
            TestComboBox.Items.Add(New TestInListBox(testInBank.resourceId, displayString))
        Next
        TestComboBox.SelectedIndex = 0
    End Sub

    Private Sub SearchButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SearchButton.Click
        Dim selectedTestResourceId As Guid = Guid.Empty
        If TestComboBox.SelectedItem IsNot Nothing Then
            selectedTestResourceId = DirectCast(TestComboBox.SelectedItem, TestInListBox).ResourceId
        End If

        If Not String.IsNullOrEmpty(SearchForTextBox.Text) OrElse (String.IsNullOrEmpty(SearchForTextBox.Text) AndAlso selectedTestResourceId <> Guid.Empty) Then

            Dim maxRecords As Integer = 0
            If SearchForTextBox.Text.Contains("*") Then
                maxRecords = Integer.Parse(New System.Configuration.AppSettingsReader().GetValue("MaxRecordsToReturnFromWildcardSearch", GetType(String)).ToString())
            End If

            Dim args As New FastSearchInitiatedEventArgs(true,
                                                         true,
                                                         SearchForTextBox.Text,
                                                         selectedTestResourceId,
                                                         maxRecords,
                                                         chkIncludeSubBanks.Checked)
            OnFastSearchInitiated(args)
        Else
            MessageBox.Show(My.Resources.SimpleSearchControl_NoSearchTermProvidedError, My.Resources.SimpleSearchControl_NoSearchTermProvidedErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    Private Sub ClearButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearButton.Click
        Reset()

        OnClearedSearchStatement(New EventArgs())
    End Sub

    Private Sub SearchForTextBox_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles SearchForTextBox.KeyDown
        If e.KeyCode = Keys.Enter Then
            SearchButton_Click(sender, New EventArgs())
            e.Handled = True
        End If
    End Sub

    Private Sub SimpleSearchControl_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    End Sub

    Protected Sub OnFastSearchInitiated(ByVal e As FastSearchInitiatedEventArgs)
        RaiseEvent FastSearchInitiated(Me, e)
    End Sub

    Protected Sub OnClearedSearchStatement(ByVal e As EventArgs)
        RaiseEvent ClearedSearchStatement(Me, e)
    End Sub


    Public Sub Initialize(ByVal bankId As Integer)
        Dim tests = DtoFactory.Test.GetResourcesForBank(bankId)
        tests = tests.ToList.OrderBy(Function(t) t.title).ToList()
        FillTestList(tests)
    End Sub

    Public Sub Reset()
        SearchForTextBox.Text = String.Empty
        If TestComboBox.Items.Count > 0 Then
            TestComboBox.SelectedIndex = 0
        Else
            TestComboBox.SelectedIndex = -1
        End If
    End Sub

    Public Event FastSearchInitiated As EventHandler(Of FastSearchInitiatedEventArgs)

    Public Event ClearedSearchStatement As EventHandler

    Private Class TestInListBox
        Public Sub New(ByVal rId As Guid, ByVal name As String)
            ResourceId = rId
            TestName = name
        End Sub

        Public ReadOnly Property ResourceId As Guid

        Public ReadOnly Property TestName As String
    End Class
End Class