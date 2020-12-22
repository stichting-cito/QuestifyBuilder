Imports System.Collections.Generic
Imports Questify.Builder.Model.ContentModel.EntityClasses

Namespace CustomClasses

    Public Class ResourceEntityWithOptions

        Public Sub New(resourceEntity As ResourceEntity, options As List(Of Int16))
            resource = resourceEntity
            hasOptions = options
        End Sub

        Public Property resource As ResourceEntity

        Public Property hasOptions As List(Of Int16)

    End Class

End Namespace
