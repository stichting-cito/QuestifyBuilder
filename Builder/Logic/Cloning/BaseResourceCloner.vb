Imports Questify.Builder.Model.ContentModel.EntityClasses
Namespace Cloning

    Public MustInherit Class BaseResourceCloner
        Implements IResourceCloner

        Public Property Resource As ResourceEntity
        Private Shared ReadOnly _dict As Dictionary(Of Type, Func(Of ResourceEntity, IResourceCloner))

        Shared Sub New()
            _dict = New Dictionary(Of Type, Func(Of ResourceEntity, IResourceCloner))
            _dict.Add(GetType(ItemResourceEntity), Function(p) New ItemResourceCloner(p))
            _dict.Add(GetType(AssessmentTestResourceEntity), Function(p) New AssessmentTestResourceCloner(p))
            _dict.Add(GetType(TestPackageResourceEntity), Function(p) New TestPackageResourceCloner(p))
        End Sub

        Sub New(resource As ResourceEntity)
            Me.Resource = resource
        End Sub

        Friend Shared Function GetResourceCloner(ByVal resource As ResourceEntity) As IResourceCloner

            Dim ret As Func(Of ResourceEntity, IResourceCloner) = Nothing
            If (_dict.TryGetValue(resource.GetType(), ret)) Then
                Return ret.Invoke(resource)
            Else
                Return New DefaultResourceCloner(resource)
            End If
        End Function

        Public MustOverride Sub DoSpecificClone(originalResource As ResourceEntity) Implements IResourceCloner.DoSpecificClone

    End Class
End Namespace