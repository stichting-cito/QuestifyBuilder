Imports System.Collections.Generic

Namespace Questify.Builder.Model.ContentModel.ResourceProperties

    Public Class ResourcePropertyValueCollection
        Inherits Dictionary(Of ResourcePropertyDefinition, IList(Of Object))


        Public Function GetResourcePropertyDefinitionAndValuesByKey(ByVal key As Guid) As KeyValuePair(Of ResourcePropertyDefinition, IList(Of Object))
            For Each kvp As KeyValuePair(Of ResourcePropertyDefinition, IList(Of Object)) In Me
                If (kvp.Key.Key = key) Then
                    Return kvp
                End If
            Next

            Return Nothing
        End Function
        Public Function GetResourcePropertyDefinitionAndValuesByCode(ByVal code As Guid) As KeyValuePair(Of ResourcePropertyDefinition, IList(Of Object))
            For Each kvp As KeyValuePair(Of ResourcePropertyDefinition, IList(Of Object)) In Me
                If (kvp.Key.Code = code) Then
                    Return kvp
                End If
            Next

            Return Nothing
        End Function

        Public Function GetResourcePropertyByKey(ByVal key As Guid) As ResourcePropertyDefinition
            For Each resourcePropertyDefinition As ResourcePropertyDefinition In Me.Keys
                If resourcePropertyDefinition.Key = key Then
                    Return resourcePropertyDefinition
                End If
            Next

            Return Nothing
        End Function

        Public Function GetResourcePropertyByName(ByVal name As String) As IList(Of Object)
            Dim result As IList(Of Object) = New List(Of Object)

            For Each resourcePropertyDefinition As ResourcePropertyDefinition In Me.Keys
                If resourcePropertyDefinition.Name = name Then
                    result.Add(resourcePropertyDefinition)
                End If
            Next

            Return result
        End Function

        Public Function GetResourcePropertyByTitle(ByVal title As String) As IList(Of Object)
            Dim result As IList(Of Object) = New List(Of Object)

            For Each resourcePropertyDefinition As ResourcePropertyDefinition In Me.Keys
                If resourcePropertyDefinition.Title = title Then
                    result.Add(resourcePropertyDefinition)
                End If
            Next

            Return result
        End Function


    End Class
End Namespace
