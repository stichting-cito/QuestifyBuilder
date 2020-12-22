Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.QTI.Xsd.QTI22_Final

Namespace QTI.Converters.Declaration.QTI22

    Public Class ResponseDeclarationGraphicGapMatch
        Inherits ResponseDeclarationGapMatch

        Private _isCategorizableGraphicGapMatch As Boolean = False

        Public Sub New(isCategorizableGraphicGapMatch As Boolean)
            _isCategorizableGraphicGapMatch = isCategorizableGraphicGapMatch
        End Sub

        Protected Overrides Function GetCorrectResponse(baseValue As BaseValue, domain As String) As ValueType
            If baseValue IsNot Nothing Then
                Dim domainValue = domain
                Dim correctResponse As String
                Dim value As String = baseValue.ToString
                If Not TypeOf baseValue Is NoValue Then
                    value = AlphabeticIdentifierHelper.GetAlphabeticIdentifier(baseValue.ToString)
                    If _isCategorizableGraphicGapMatch Then value = $"HS{value}"
                End If
                If domainValue.IndexOf("-") > 0 Then domainValue = domainValue.Substring(0, domainValue.IndexOf("-"))
                If _isCategorizableGraphicGapMatch Then
                    correctResponse = $"{AlphabeticIdentifierHelper.GetAlphabeticIdentifier(domainValue)} {value}"
                Else
                    correctResponse = $"{value} HS{AlphabeticIdentifierHelper.GetAlphabeticIdentifier(domainValue)}"
                End If
                Return New ValueType() With {.Value = correctResponse}
            End If
            Return Nothing
        End Function

    End Class

End Namespace
