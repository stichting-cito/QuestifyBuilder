Imports Cito.Tester.ContentModel

Namespace ContentModel.ParamValidator
    Friend Class ResourceParameterValidator : Inherits ValueParameterValidator(Of ResourceParameter)

        Sub New(param As ParameterBase)
            MyBase.New(param)
        End Sub

        Protected Overrides Sub SetKnownDesignerSettings(ByRef knownParameters As List(Of String))
            MyBase.SetKnownDesignerSettings(knownParameters)
            knownParameters.Add("required")
        End Sub

        Protected Overrides Function ParamHasValue(p As ResourceParameter) As Boolean
            Return Not String.IsNullOrEmpty(p.Value)
        End Function

    End Class

End Namespace

