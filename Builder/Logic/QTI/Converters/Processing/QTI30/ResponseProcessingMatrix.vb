Imports System.Linq
Imports System.Xml.Linq
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.QTI.Helpers.QTI30

Namespace QTI.Converters.Processing.QTI30

    Public Class ResponseProcessingMatrix
        Inherits ResponseProcessingPerTypeBase

        Public Sub New(responseIndex As Integer, Optional responseSubIndex As Integer = 0)
            MyBase.New(responseIndex, responseSubIndex)
        End Sub

        Protected Overrides Function GetProcessingForFact(fact As KeyFact, Optional addNotMemberOfElement As Boolean = False, Optional outcomeIdentifier As String = "") As XElement
            Dim processing As XElement = <qti-member></qti-member>
            Dim keyValue As KeyValue = DirectCast(fact.Values.First(), KeyValue)

            If keyValue.Values.Count = 1 Then
                Dim value = keyValue.Values.First()
                processing.Add(GetProcessingForValue(value, GetMatrixRowIndexFromFact(fact)))
                processing.Add(GetProcessingForVariable())
            End If

            Return processing
        End Function

        Private Function GetProcessingForValue(value As BaseValue, valueY As Integer) As XElement
            Dim y = $"y_{AlphabeticIdentifierHelper.GetAlphabeticIdentifier(valueY)}"
            Dim x = $"x_{(AscW(value.ToString) - 64)}"
            Return ScoringHelper.GetDirectPairProcessingForValue(y, x)
        End Function

        Private Function GetMatrixRowIndexFromFact(fact As KeyFact) As Integer
            Return CInt(fact.Id.Substring(0, fact.Id.IndexOf("-")))
        End Function

    End Class

End Namespace