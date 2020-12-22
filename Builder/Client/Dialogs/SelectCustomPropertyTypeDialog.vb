
Imports Enums

Public Class SelectCustomPropertyTypeDialog

    Private ReadOnly _types As New List(Of CustomBankPropertyType)

    Public ReadOnly Property Types As List(Of CustomBankPropertyType)
        Get
            Return _types
        End Get
    End Property

    Private Sub Cancel_Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub OK_Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles OK_Button.Click
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub SelectCustomPropertyTypeDialog_Load(sender As Object, e As EventArgs) Handles Me.Load

        Dim index As Integer
        For Each type As CustomBankPropertyType In [Enum].GetValues(GetType(CustomBankPropertyType))
            Dim choice As New RadioButton()
            choice.Tag = CInt(type)
            choice.Text = LocalizedEnumConverter.ConvertToString(type)
            choice.Margin = New Padding(0)
            choice.Width = 200

            If index = 0 Then
                choice.Checked = True
            End If
            index += 1
            FlowLayoutPanelTypes.Controls.Add(choice)

        Next

    End Sub

    Public ReadOnly Property SelectedType() As CustomBankPropertyType
        Get
            For Each choice As RadioButton In FlowLayoutPanelTypes.Controls
                If choice.Checked Then
                    Return CType(CInt(choice.Tag), CustomBankPropertyType)
                End If
            Next

            Return Nothing
        End Get
    End Property
End Class