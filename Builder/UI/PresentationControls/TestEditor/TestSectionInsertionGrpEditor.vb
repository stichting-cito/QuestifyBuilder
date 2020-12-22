Imports Cito.Tester.ContentModel

Public Class TestSectionInsertionGrpEditor


    Private Sub PopulateDropdownListSectionType()
        Dim InsertionMethodList As New List(Of KeyValuePair(Of enumInsertionMethod, String))

        InsertionMethodList.Add(New KeyValuePair(Of enumInsertionMethod, String)(enumInsertionMethod.linear, enumInsertionMethod.linear.ToString))
        InsertionMethodList.Add(New KeyValuePair(Of enumInsertionMethod, String)(enumInsertionMethod.random, enumInsertionMethod.random.ToString))

        InsertionMethodComboBox.DisplayMember = "value"
        InsertionMethodComboBox.ValueMember = "key"
        InsertionMethodComboBox.DataSource = InsertionMethodList
    End Sub

    Private Sub TestSectionInsertionGrpEditor_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        PopulateDropdownListSectionType()
    End Sub


End Class