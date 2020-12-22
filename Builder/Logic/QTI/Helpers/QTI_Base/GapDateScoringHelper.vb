Imports System.Linq
Imports Cito.Tester.ContentModel

Namespace QTI.Helpers.QTI_Base

    Public Class GapDateScoringHelper


        Private Const dateSeparator As Char = "/"c

        Enum DateSubType
            dutch
            american
            scandinavian
        End Enum



        Public Shared Function FactContainsDateValues(ByVal fact As KeyFact) As Boolean
            Dim returnValue As Boolean = False
            If TypeOf fact Is ConceptFact Then
                If fact.Values IsNot Nothing Then
                    For Each value As ConceptValue In fact.Values
                        returnValue = ValueContainsDateValues(value.Values)
                        If returnValue = True Then Exit For
                    Next
                End If
            Else
                If fact.Values IsNot Nothing Then
                    For Each value As KeyValue In fact.Values
                        returnValue = ValueContainsDateValues(value.Values)
                        If returnValue = True Then Exit For
                    Next
                End If
            End If
            Return returnValue
        End Function

        Public Shared Function ValueContainsDateValues(ByVal keyValueCollection As Cito.Tester.ContentModel.KeyValueCollection) As Boolean
            For Each baseValue As BaseValue In keyValueCollection
                If BaseValueIsDateValue(baseValue) = True Then Return True
            Next
            Return False
        End Function

        Public Shared Function BaseValueIsDateValue(ByVal baseValue As BaseValue) As Boolean
            If (TypeOf baseValue Is StringValue) AndAlso baseValue.ToString.Contains(dateSeparator) Then

                Dim countDateParts As Integer = baseValue.ToString.Split(dateSeparator).Count
                If Not (countDateParts = 3) Then Return False

                For Each partResponse As String In baseValue.ToString.Split(dateSeparator)
                    If Not IsNumeric(partResponse) Then Return False
                Next

                If Convert.ToInt32(baseValue.ToString.Split(dateSeparator)(0)) < 1 OrElse Convert.ToInt32(baseValue.ToString.Split(dateSeparator)(0)) > 12 Then Return False

                If Convert.ToInt32(baseValue.ToString.Split(dateSeparator)(1)) < 1 OrElse Convert.ToInt32(baseValue.ToString.Split(dateSeparator)(1)) > 31 Then Return False

                Dim lengthOfThirdPart As Integer = baseValue.ToString.Split(dateSeparator)(2).Length
                If Not (lengthOfThirdPart = 4) Then Return False

                If Convert.ToInt32(baseValue.ToString.Split(dateSeparator)(2)) < 0 Then Return False
            Else
                Return False
            End If
            Return True
        End Function

        Public Shared Function GetPartOfDateValue(ByVal baseValue As BaseValue, ByVal dateSubIndex As Integer) As String
            Dim result As String = String.Empty
            Dim index As Integer = 1

            For Each partResponse As String In baseValue.ToString.Split(dateSeparator)
                If index = dateSubIndex Then
                    If IsNumeric(partResponse) Then
                        Return partResponse
                    End If
                End If
                index += 1
            Next

            Return result
        End Function

        Friend Shared Function NrOfDateParts(ByVal keyValueCollection As Cito.Tester.ContentModel.KeyValueCollection) As Integer
            Dim result As Integer = 0
            Dim tmp As Integer = 0
            For Each baseValue As BaseValue In keyValueCollection
                tmp = NrOfDateParts(baseValue)
                If tmp > result Then result = tmp
            Next
            Return result
        End Function

        Friend Shared Function NrOfDateParts(ByVal baseValue As BaseValue) As Integer
            Return baseValue.ToString.Split(dateSeparator).Count
        End Function

    End Class
End NameSpace