Public Interface IValidatingEntity

    Function GetValidationErrors(includeChildren As Boolean) As ValidationValueCollection

End Interface
