Imports Questify.Builder.Model.ContentModel.EntityClasses

<TestClass()>
Public Class ListValueCustomBankPropertyEntityTest
    Inherits GenericTestBase(Of ListValueCustomBankPropertyEntity)


    Protected Overrides Function CreateTheObject() As ListValueCustomBankPropertyEntity
        Return New ListValueCustomBankPropertyEntity
    End Function

    Protected Overrides Sub SetErroneousParam(obj As ListValueCustomBankPropertyEntity)
        obj.Name = ""
    End Sub

    Protected Overrides Sub SetCorrectParam(obj As ListValueCustomBankPropertyEntity)
        obj.Name = "abc"
    End Sub
End Class
