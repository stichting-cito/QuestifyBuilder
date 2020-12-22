Imports Questify.Builder.Model.ContentModel.EntityClasses

Namespace Faketory.impl

    Friend Class AddIsUsedBy
        Inherits AddBase

        Public Sub New(resource As ResourceEntity)
            MyBase.New(resource)
        End Sub

        Protected Overrides Sub HookupResource(ret As ResourceEntity)
            ret.DependentResourceCollection.Add(New DependentResourceEntity With {.DependentResource = _resource})
        End Sub

    End Class
End NameSpace