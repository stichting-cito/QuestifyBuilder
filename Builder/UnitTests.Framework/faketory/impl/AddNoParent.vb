Imports Questify.Builder.Model.ContentModel.EntityClasses

Namespace Faketory.impl

    Friend Class AddNoParent
        Inherits AddBase

        Public Sub New()
            MyBase.New(Nothing)
        End Sub

        Protected Overrides Sub HookupResource(ret As ResourceEntity)
        End Sub
    End Class
End NameSpace