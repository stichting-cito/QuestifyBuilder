
Imports Questify.Builder.Model.ContentModel.EntityClasses

Namespace Faketory.impl

    Friend Class AddAsDependency
        Inherits AddBase
        
        Public Sub New(resource As ResourceEntity)
            MyBase.New(resource)
        End Sub

        Protected Overrides Sub HookupResource(ret As ResourceEntity)
            'the resource is hookedup as a child. 
            'EG. after creating an item, you want to create a image,.. the image is a dependency of the item.
            MyBase._resource.DependentResourceCollection.Add(New DependentResourceEntity With {.DependentResource = ret})
        End Sub
    End Class
End NameSpace