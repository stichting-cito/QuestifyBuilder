Namespace ContentModel.ParamValidator

    Interface IParamValidator

        Property ValueBag As IDictionary(Of String, String)

        Function isValid() As Boolean


        Function GetError() As IEnumerable(Of String)

        ReadOnly Property ValidDesignerSettings As IEnumerable(Of String)

    End Interface

End Namespace
