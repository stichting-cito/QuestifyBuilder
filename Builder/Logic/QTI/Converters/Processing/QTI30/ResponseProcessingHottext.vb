Imports System.Linq
Imports System.Xml.Linq
Imports Cito.Tester.ContentModel

Namespace QTI.Converters.Processing.QTI30

    Public Class ResponseProcessingHottext
        Inherits ResponseProcessingPerTypeBase

        Public Sub New(responseIndex As Integer, Optional responseSubIndex As Integer = 0)
            MyBase.New(responseIndex, responseSubIndex)
        End Sub

        Protected Overrides Function GetProcessingForFact(fact As KeyFact, Optional addNotMemberOfElement As Boolean = False, Optional outcomeIdentifier As String = "") As XElement
            Dim processing As XElement = Nothing
            Dim keyValue As KeyValue = DirectCast(fact.Values.First(), KeyValue)

            If keyValue.Values.Count >= 1 Then
                Dim processingForValue As String = String.Empty
                Dim value = keyValue.Values.First()
                If TypeOf value Is BooleanValue Then
                    If DirectCast(value, BooleanValue).Value OrElse addNotMemberOfElement Then
                        processingForValue = GetCorrectResponse(fact)
                    End If
                End If
                If Not String.IsNullOrEmpty(processingForValue) Then
                    processing = <qti-member></qti-member>
                    processing.Add(GetProcessingForValue(processingForValue))
                    processing.Add(GetProcessingForVariable())
                    If addNotMemberOfElement AndAlso TypeOf value Is BooleanValue AndAlso DirectCast(value, BooleanValue).Value = False Then
                        processing = XElement.Parse($"<qti-not>{processing.ToString}</qti-not>")
                    End If
                End If
            End If

            Return processing
        End Function

        Private Function GetProcessingForValue(value As String) As XElement
            Return <qti-base-value base-type="identifier"><%= value %></qti-base-value>
        End Function

        Private Function GetCorrectResponse(fact As KeyFact) As String
            Return fact.Id.Substring(0, fact.Id.LastIndexOf("-"))
        End Function

    End Class

End Namespace