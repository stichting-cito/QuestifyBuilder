Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.UnitTests.Framework.Faketory.interface

Namespace Faketory.impl

    Friend Class AddKeyword
        Implements IAddResourcesKeyword
        Implements IActionsAfter

        Private ReadOnly _resource As ResourceEntity

        Public Sub New(resource As ResourceEntity)
            _resource = resource
        End Sub

        Private ReadOnly Property AddDependency As IAddResources Implements IAddResourcesKeyword.AddResource
            Get
                Return New AddAsDependency(_resource)
            End Get
        End Property

        Private ReadOnly Property DependsOn As IAddRootObjects Implements IActionsAfter.DependsOn
            Get
                Return New AddAsDependency(_resource)
            End Get
        End Property

        Private ReadOnly Property IsUsedBy As IAddRootObjects Implements IActionsAfter.IsUsedBy
            Get
                Return New AddIsUsedBy(_resource)
            End Get
        End Property

    End Class
End NameSpace