Imports System.Collections.Generic
Imports SD.LLBLGen.Pro.ORMSupportClasses

Namespace Questify.Builder.Model.ContentModel.HelperClasses

    Partial Public Class EntityCollection(Of TEntity As {EntityBase2, IEntity2})

        Public Overridable Function GetMatches(ByVal filter As IPredicate) As List(Of TEntity)

            Dim list As New List(Of TEntity)(Me.Count)

            Dim expression As IPredicateExpression = TryCast(filter, IPredicateExpression)
            If ((filter Is Nothing) OrElse ((Not expression Is Nothing) AndAlso (expression.Count <= 0))) Then
                list = DirectCast(Me.Items, List(Of TEntity))
            Else

                Dim interpret As IPredicateInterpret = TryCast(filter, IPredicateInterpret)
                If (interpret Is Nothing) Then
                    Throw New ORMInterpretationException("The passed in filter doesn't implement IPredicateInterpret")
                End If

                For Each entity As TEntity In Me.Items
                    If interpret.Interpret(entity) Then
                        list.Add(entity)
                    End If
                Next

            End If

            Return list
        End Function

        Public Overridable Function GetFirstMatch(ByVal filter As IPredicate) As TEntity

            Dim result As TEntity = Nothing

            Dim expression As IPredicateExpression = TryCast(filter, IPredicateExpression)
            If ((filter Is Nothing) OrElse ((Not expression Is Nothing) AndAlso (expression.Count <= 0))) Then
                If Me.Count > 0 Then
                    result = Me.Items(0)
                End If

            Else

                Dim interpret As IPredicateInterpret = TryCast(filter, IPredicateInterpret)
                If (interpret Is Nothing) Then
                    Throw New ORMInterpretationException("The passed in filter doesn't implement IPredicateInterpret")
                End If

                For Each entity As TEntity In Me.Items
                    If interpret.Interpret(entity) Then
                        result = entity
                        Exit For
                    End If
                Next
            End If

            Return result
        End Function

    End Class

End Namespace
