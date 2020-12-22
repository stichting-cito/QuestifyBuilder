Public Class GridSettings


    Public Sub New()
    End Sub

    Public Sub New(gridIdentifier As String)
        _GridIdentifier = gridIdentifier
    End Sub


    Public Property GridIdentifier As String
    Public Property SelectedRowIdentifier As String
    Public Property ColumnSettings As List(Of ColumnSettings)
    Public Property ScrollBarPosition As Integer

    Public ReadOnly Property IsFiltered As Boolean
        Get
            Dim hasColumnFilter = False

            If ColumnSettings IsNot Nothing Then
                For Each columnSetting As ColumnSettings In ColumnSettings
                    If Not String.IsNullOrWhiteSpace(columnSetting.Filter) Then
                        hasColumnFilter = True
                        Exit For
                    End If
                Next
            End If

            Return hasColumnFilter
        End Get
    End Property

End Class
