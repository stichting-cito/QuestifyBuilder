
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports SD.LLBLGen.Pro.ORMSupportClasses

Public Class UserEntityFake
    Inherits UserEntity

    Protected Overrides Function CreateValidator() As IValidator
        Return New UserValidatorFake()
    End Function

    Public Sub New(id As Int32)
        MyBase.New(id)
    End Sub
End Class
