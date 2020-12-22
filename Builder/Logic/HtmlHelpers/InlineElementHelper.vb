

Public Class InlineElementHelper

    Public Shared Function GetNewInlineElementIdentifier() As String
        Return String.Concat("I", Guid.NewGuid().ToString)
    End Function

End Class
