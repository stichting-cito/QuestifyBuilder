

Namespace Controls.Canvas.LabelGenerator
    Public Class AlphabeticIdentifierGenerator
        Implements IIdentifierGenerator(Of String)

        Private _lastUsedIdentifier As Integer = 1

        Public Sub New()
            _lastUsedIdentifier = 1
        End Sub

        Public Sub New(startIndex As Integer)
            If startIndex <= 0 Then
                _lastUsedIdentifier = 1
            End If

            _lastUsedIdentifier = startIndex
        End Sub

        Public Function GetNewIdentifier() As String Implements IIdentifierGenerator(Of String).GetNewIdentifier
            Dim newId As String = AlphabeticIdentifierHelper.GetAlphabeticIdentifier(_lastUsedIdentifier)

            _lastUsedIdentifier += 1

            Return newId
        End Function

        Public Function GetLastGeneratedIdentifier() As String Implements IIdentifierGenerator(Of String).GetLastGeneratedIdentifier
            Return AlphabeticIdentifierHelper.GetAlphabeticIdentifier(_lastUsedIdentifier)
        End Function

        Public Sub Reset() Implements IIdentifierGenerator(Of String).Reset
            _lastUsedIdentifier = 1
        End Sub

    End Class

End Namespace
