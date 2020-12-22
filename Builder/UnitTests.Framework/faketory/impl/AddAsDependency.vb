
Imports Questify.Builder.Model.ContentModel.EntityClasses

Namespace Faketory.impl

    Friend Class AddAsDependency
        Inherits AddBase

        Public Sub New(resource As ResourceEntity)
            MyBase.New(resource)
        End Sub

        Protected Overrides Sub HookupResource(ret As ResourceEntity)
            MyBase._resource.DependentResourceCollection.Add(New DependentResourceEntity With {.DependentResource = ret})
        End Sub
    End Class
End NameSpace