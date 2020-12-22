Imports SD.LLBLGen.Pro.ORMSupportClasses

Namespace Questify.Builder.Model.ContentModel.EntityClasses

    Partial Public Class UserEntity

        Protected Overrides Function CreateValidator() As IValidator
            Return New ValidatorClasses.UserValidator()
        End Function

    End Class

End Namespace
