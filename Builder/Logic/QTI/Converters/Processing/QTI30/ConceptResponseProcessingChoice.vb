Imports System.Linq
Imports System.Xml.Linq
Imports Cito.Tester.ContentModel

Namespace QTI.Converters.Processing.QTI30

    Public Class ConceptResponseProcessingChoice
        Inherits ResponseProcessingPerTypeBase

        Public Sub New(responseIndex As Integer, Optional responseSubIndex As Integer = 0)
            MyBase.New(responseIndex, responseSubIndex)
        End Sub

        Protected Overrides Function GetProcessingForFact(fact As KeyFact, Optional addNotMemberOfElement As Boolean = False, Optional outcomeIdentifier As String = "") As XElement
            Dim processing As XElement = Nothing
            Dim keyValue As KeyValue = DirectCast(fact.Values.First(), KeyValue)

            If keyValue.Values.Count = 1 Then
                Dim value = keyValue.Values.First()
                If Not TypeOf value Is CatchAllValue Then
                    processing = GetProcessingForValue(value)
                End If
            End If

            Return processing
        End Function

        Private Function GetProcessingForValue(value As BaseValue) As XElement
            Dim processing As XElement = <qti-match></qti-match>
            Dim correctResponse = CType(value, StringValue).Value
            Dim correctValue As XElement = <qti-base-value base-type="identifier"><%= correctResponse %></qti-base-value>
            processing.Add(correctValue)
            processing.Add(GetProcessingForVariable())

            Return processing
        End Function

    End Class
End Namespace