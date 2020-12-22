Imports Questify.Builder.Model.ContentModel.EntityClasses

<TestClass>
Public Class AspectMetaDataGenerationTest : Inherits GenericMetaDataGenerationTest(Of AspectResourceEntity)

    Protected Overrides Function GetEntity() As AspectResourceEntity
        Return New AspectResourceEntity(Guid.NewGuid())
    End Function

End Class

<TestClass>
Public Class AssessmentMetaDataGenerationTest : Inherits GenericMetaDataGenerationTest(Of AssessmentTestResourceEntity)
    Protected Overrides Function GetEntity() As AssessmentTestResourceEntity
        Return New AssessmentTestResourceEntity(Guid.NewGuid())
    End Function
End Class

<TestClass>
Public Class ControlTemplateMetaDataGenerationTest : Inherits GenericMetaDataGenerationTest(Of ControlTemplateResourceEntity)
    Protected Overrides Function GetEntity() As ControlTemplateResourceEntity
        Return New ControlTemplateResourceEntity(Guid.NewGuid())
    End Function
End Class

<TestClass>
Public Class DataSourceMetaDataGenerationTest : Inherits GenericMetaDataGenerationTest(Of DataSourceResourceEntity)
    Protected Overrides Function GetEntity() As DataSourceResourceEntity
        Return New DataSourceResourceEntity(Guid.NewGuid())
    End Function
End Class

<TestClass>
Public Class GenericResourceMetaDataGenerationTest : Inherits GenericMetaDataGenerationTest(Of GenericResourceEntity)
    Protected Overrides Function GetEntity() As GenericResourceEntity
        Return New GenericResourceEntity(Guid.NewGuid())
    End Function
End Class

<TestClass>
Public Class ResourceEntityMetaDataGenerationTest : Inherits GenericMetaDataGenerationTest(Of ResourceEntity)
    Protected Overrides Function GetEntity() As ResourceEntity
        Return New ResourceEntity(Guid.NewGuid())
    End Function
End Class

<TestClass>
Public Class ItemLayoutMetaDataGenerationTest : Inherits GenericMetaDataGenerationTest(Of ItemLayoutTemplateResourceEntity)
    Protected Overrides Function GetEntity() As ItemLayoutTemplateResourceEntity
        Return New ItemLayoutTemplateResourceEntity(Guid.NewGuid())
    End Function
End Class

<TestClass>
Public Class ItemMetaDataGenerationTest : Inherits GenericMetaDataGenerationTest(Of ItemResourceEntity)
    Protected Overrides Function GetEntity() As ItemResourceEntity
        Return New ItemResourceEntity(Guid.NewGuid())
    End Function
End Class

<TestClass>
Public Class PackageMetaDataGenerationTest : Inherits GenericMetaDataGenerationTest(Of PackageResourceEntity)
    Protected Overrides Function GetEntity() As PackageResourceEntity
        Return New PackageResourceEntity(Guid.NewGuid())
    End Function
End Class

<TestClass>
Public Class TestPackageMetaDataGenerationTest : Inherits GenericMetaDataGenerationTest(Of TestPackageResourceEntity)
    Protected Overrides Function GetEntity() As TestPackageResourceEntity
        Return New TestPackageResourceEntity(Guid.NewGuid())
    End Function
End Class