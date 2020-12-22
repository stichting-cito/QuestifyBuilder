Imports System.Linq
Imports Cito.Tester.ContentModel

Namespace QTI.Helpers.QTI_Base

    Public Class GapTimeScoringHelper

        Private Const timeSeparator As Char = ":"c


        Public Shared Function FactContainsTimeValues(ByVal fact As KeyFact) As Boolean
            Dim returnValue As Boolean = False
            If TypeOf fact Is ConceptFact Then
                If fact.Values IsNot Nothing Then
                    For Each value As ConceptValue In fact.Values
                        returnValue = ValueContainsTimeValues(value.Values)
                        If returnValue = True Then
                            Return returnValue
                        End If
                    Next
                End If
            Else
                If fact.Values IsNot Nothing Then
                    For Each value As KeyValue In fact.Values
                        returnValue = ValueContainsTimeValues(value.Values)
                        If returnValue = True Then
                            Return returnValue
                        End If
                    Next
                End If
            End If
            Return returnValue
        End Function

        Public Shared Function ValueContainsTimeValues(ByVal keyValueCollection As Cito.Tester.ContentModel.KeyValueCollection) As Boolean
            For Each baseValue As BaseValue In keyValueCollection
                If BaseValueIsTimeValue(baseValue) = True Then
                    Return True
                End If
            Next
            Return False
        End Function

        Public Shared Function BaseValueIsTimeValue(ByVal baseValue As BaseValue) As Boolean
            If (TypeOf baseValue Is StringValue) AndAlso baseValue.ToString.Contains(timeSeparator) Then

                Dim countTimeParts As Integer = NrOfTimeParts(baseValue)
                If Not (countTimeParts >= 2 AndAlso countTimeParts <= 3) Then
                    Return False
                End If

                For Each partResponse As String In baseValue.ToString.Split(timeSeparator)
                    If Not IsNumeric(partResponse) Then
                        Return False
                    End If
                Next

                For Each partResponse As String In baseValue.ToString.Split(timeSeparator)
                    If Convert.ToInt32(partResponse) < 0 OrElse Convert.ToInt32(partResponse) > 59 Then
                        Return False
                    End If
                Next

                If countTimeParts = 3 Then
                    If Convert.ToInt32(baseValue.ToString.Split(timeSeparator)(0)) > 23 Then
                        Return False
                    End If
                End If
            Else
                Return False
            End If
            Return True
        End Function

        Friend Shared Function NrOfTimeParts(ByVal keyValueCollection As Cito.Tester.ContentModel.KeyValueCollection) As Integer
            Dim result As Integer = 0
            Dim tmp As Integer = 0
            For Each baseValue As BaseValue In keyValueCollection
                tmp = NrOfTimeParts(baseValue)
                If tmp > result Then result = tmp
            Next
            Return result
        End Function

        Friend Shared Function NrOfTimeParts(ByVal baseValue As BaseValue) As Integer
            Return baseValue.ToString.Split(timeSeparator).Count
        End Function

        Public Shared Function GetPartOfTimeValue(ByVal baseValue As BaseValue, ByVal timeSubIndex As Integer) As String
            Dim result As String = String.Empty
            Dim index As Integer = 1

            For Each partResponse As String In baseValue.ToString.Split(timeSeparator)
                If index = timeSubIndex Then
                    If IsNumeric(partResponse) Then
                        Return partResponse
                    End If
                End If
                index += 1
            Next

            Return result
        End Function

    End Class
End NameSpace