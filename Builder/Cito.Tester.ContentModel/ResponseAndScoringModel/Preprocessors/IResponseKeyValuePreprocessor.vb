Public Interface IResponseKeyValuePreprocessor

    ReadOnly Property Id As PreProcessingRuleId

    ReadOnly Property DisplayName As String

    Function PreprocessValue(keyValue As BaseValue) As BaseValue

End Interface
