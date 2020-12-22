Imports System.Globalization
Imports System.Linq
Imports System.Xml.Linq
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.QTI.Helpers.QTI_Base

Namespace QTI.Converters.Processing.QTI22

    Public Class ConceptResponseProcessingOrder
        Inherits ResponseProcessingPerTypeBase

        Public Sub New(responseIndex As Integer, Optional responseSubIndex As Integer = 0)
            MyBase.New(responseIndex, responseSubIndex)
        End Sub

        Protected Overrides Function GetProcessingForFact(fact As KeyFact, Optional addNotMemberOfElement As Boolean = False, Optional outcomeIdentifier As String = "") As XElement
            Dim processing As XElement = <match></match>
            Dim responseId = QTIScoringHelper.GetResponseId(ResponseIndex)
            Dim indexVariable = <index n="{0}">
                                    <variable identifier="{1}"/>
                                </index>

            If fact.Values.Count > 0 Then
                Dim keyValue As KeyValue = DirectCast(fact.Values.First(), KeyValue)
                If keyValue.Values.Count = 1 Then
                    Dim value = keyValue.Values.First()
                    If TypeOf value Is CatchAllValue Then Return Nothing
                    Dim keyIndex As Integer = Asc(fact.Id.Substring(0, 1)) - 64
                    processing.Add(XElement.Parse(String.Format(indexVariable.ToString, keyIndex, responseId)))
                    processing.Add(GetProcessingForValue(keyValue.Values.First))
                End If
            End If

            Return processing
        End Function

        Private Function GetProcessingForValue(value As BaseValue) As XElement
            Dim correctResponse = AlphabeticIdentifierHelper.GetAlphabeticIdentifier(CInt(CType(value, IntegerValue).Value.ToString(CultureInfo.InvariantCulture.NumberFormat)))
            Dim correctValue As XElement = <baseValue baseType="identifier"><%= correctResponse %></baseValue>
            Return correctValue
        End Function

    End Class
End Namespace