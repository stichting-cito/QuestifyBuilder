Public Class ColumnSettings

    Public Sub New()
    End Sub

    Public Sub New(columnIdentifier As String)
        _ColumnIdentifier = columnIdentifier
    End Sub


    Public Property ColumnIdentifier As String
    Public Property Width As Integer
    Public Property Visible As Boolean
    Public Property IsGrouped As Boolean
    Public Property SortGroupAscending As Boolean
    Public Property IsSorted As Boolean
    Public Property SortAscending As Boolean
    Public Property Filter As String
    Public Property GroupPosition As Integer

End Class
