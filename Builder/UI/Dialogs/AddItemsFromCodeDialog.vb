Public Class AddItemsFromCodeDialog

    Private _itemCodes As New List(Of String)



    Public ReadOnly Property ListOfItemCodes() As List(Of String)
        Get
            Return _itemCodes
        End Get
    End Property


    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub AddItemsButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddItemsButton.Click
        FillItemCodeList()
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub FillItemCodeList()
        Dim itemcodeArray As String() = ItemCodeTextBox.Text.Split(Chr(13))
        For Each itemcode As String In itemcodeArray
            If Not String.IsNullOrEmpty(itemcode) AndAlso Not itemcode = Chr(10) Then
                _itemCodes.Add(itemcode.Trim)
            End If
        Next
    End Sub
End Class