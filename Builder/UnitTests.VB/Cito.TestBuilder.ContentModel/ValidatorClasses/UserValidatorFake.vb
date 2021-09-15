﻿
Imports Questify.Builder.Model.ContentModel.ValidatorClasses
Imports SD.LLBLGen.Pro.ORMSupportClasses

''' <summary>
''' UserValidatorFake - used by unit tests
''' </summary>
Public Class UserValidatorFake
	Inherits UserValidator

	Public Overrides Function ValidateFieldValue(ByVal involvedEntity As IEntityCore, ByVal fieldIndex As Integer, ByVal value As Object) As Boolean
		Return True
	End Function
End Class
