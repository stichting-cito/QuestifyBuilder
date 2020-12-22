
Imports System.Linq
Imports System.Xml.Linq
Imports Cito.Tester.ContentModel

Namespace QTI.Converters.Processing.QTI22

    Public Class ResponseProcessingChoice
        Inherits ResponseProcessingPerTypeBase

        Public Sub New(responseIndex As Integer, Optional responseSubIndex As Integer = 0)
            MyBase.New(responseIndex, responseSubIndex)
        End Sub

        Protected Overrides Function GetProcessingForFact(fact As KeyFact, Optional addNotMemberOfElement As Boolean = False, Optional outcomeIdentifier As String = "") As XElement
            Dim keyValue As KeyValue = DirectCast(fact.Values.First(), KeyValue)
            Dim processing As XElement = Nothing

            If keyValue.Values.Count = 1 Then
                Dim value = keyValue.Values.First()
                processing = GetProcessingForValue(value)
            End If

            Return processing
        End Function

        Private Function GetProcessingForValue(value As BaseValue) As XElement

            Dim processing As XElement = <match></match>
            processing.Add(GetProcessingForVariable())

            Dim correctResponse = CType(value, StringValue).Value
            Dim correctValue As XElement = <baseValue baseType="identifier"><%= correctResponse %></baseValue>
            processing.Add(correctValue)

            Return processing
        End Function

    End Class

End Namespace
