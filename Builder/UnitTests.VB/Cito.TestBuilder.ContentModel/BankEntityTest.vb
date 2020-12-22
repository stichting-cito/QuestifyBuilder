Imports Questify.Builder.Model.ContentModel.EntityClasses

<TestClass()>
Public Class BankEntityTest
    Inherits GenericTestBase(Of BankEntity)


    Protected Overrides Function CreateTheObject() As BankEntity
        Return New BankEntity()
    End Function

    Protected Overrides Sub SetErroneousParam(obj As BankEntity)
        obj.Name = ""
    End Sub

    Protected Overrides Sub SetCorrectParam(obj As BankEntity)
        obj.Name = "abc"
    End Sub

End Class
