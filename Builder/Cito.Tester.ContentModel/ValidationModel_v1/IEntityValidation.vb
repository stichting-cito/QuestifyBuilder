Public Interface IEntityValidation


    Function ValidateEntityFieldValue(entity As ValidatingEntityBase, fieldName As String) As String


    ReadOnly Property FriendlyEntityName As String

End Interface
