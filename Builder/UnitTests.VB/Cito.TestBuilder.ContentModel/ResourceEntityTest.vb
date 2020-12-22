Imports Questify.Builder.Model.ContentModel.EntityClasses

<TestClass()>
Public Class ResourceEntityTest
    Inherits GenericTestBase(Of DataSourceResourceEntity)



    Protected Overrides Function CreateTheObject() As DataSourceResourceEntity
        Return New DataSourceResourceEntity()
    End Function

    Protected Overrides Sub SetErroneousParam(obj As DataSourceResourceEntity)
        obj.Name = "()"
    End Sub

    Protected Overrides Sub SetCorrectParam(obj As DataSourceResourceEntity)
        obj.Name = "abc"
    End Sub

End Class
