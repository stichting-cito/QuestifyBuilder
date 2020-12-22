Imports Questify.Builder.Logic.ContentModel
Imports Cito.Tester.ContentModel

Namespace ItemEditor
    Friend Class GroupConditionalEnabled
        Implements IGroupConditionalEnabled
        Private ReadOnly _parameter As ParameterBase
        Private ReadOnly _value As String

        Public Sub New(parameter As ParameterBase)
            _parameter = parameter
            InfluencedGroup = parameter.GroupConditionalEnabledSwitch()
            _value = parameter.GroupConditionalEnabledWhenValue()

            Debug.Assert(InfluencedGroup <> String.Empty)
            Debug.Assert(_value <> String.Empty)
        End Sub

        Public Function IsEnabled() As Boolean Implements IGroupConditionalEnabled.IsEnabled
            Return _parameter.ToString().Equals(_value, StringComparison.CurrentCultureIgnoreCase)
        End Function

        Public Event NeedsReEvaluation As EventHandler Implements IGroupConditionalEnabled.NeedsReEvaluation

        Public Property InfluencedGroup As String Implements IGroupInfluence.InfluencedGroup

    End Class
End Namespace
