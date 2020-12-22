Imports Cito.Tester.ContentModel

Namespace ContentModel.ParamValidator

    Friend Class XHtmlParameterValidator : Inherits ValueParameterValidator(Of XHtmlParameter)

        Private ReadOnly _htmlContentValidator As New HtmlContentValidator

        Sub New(param As ParameterBase)
            MyBase.New(param)
        End Sub

        Protected Overrides Sub SetKnownDesignerSettings(ByRef knownParameters As List(Of String))
            MyBase.SetKnownDesignerSettings(knownParameters)
            knownParameters.Add("required")
            knownParameters.Add("visible")
            knownParameters.Add("conditionalEnabled")
            knownParameters.Add("conditionalEnabledSwitchParameter")
            knownParameters.Add("conditionalEnabledWhenValue")
        End Sub

        Protected Overrides Function ParamHasValue(p As XHtmlParameter) As Boolean
            Return _htmlContentValidator.HtmlContainsValue(Param.Value)
        End Function
    End Class

End Namespace

