Imports System.Collections.Generic

Namespace Questify.Builder.Model.ContentModel.ResourceProperties

    Public Class ResourcePropertyDefinitionCollection
        Inherits List(Of ResourcePropertyDefinition)


        Public Sub New()
            MyBase.New()
        End Sub



        Public Function GetResourcePropertyDefinitionByKey(ByVal key As Guid) As ResourcePropertyDefinition
            For Each resourcePropertyDefinition As ResourcePropertyDefinition In Me
                If (key = resourcePropertyDefinition.Key) Then
                    Return resourcePropertyDefinition
                End If
            Next

            Return Nothing
        End Function

        Public Function GetResourcePropertyDefinitionByName(ByVal name As String) As ResourcePropertyDefinition
            For Each resourcePropertyDefinition As ResourcePropertyDefinition In Me
                If (name = resourcePropertyDefinition.Name) Then
                    Return resourcePropertyDefinition
                End If
            Next

            Return Nothing
        End Function


    End Class
End Namespace
