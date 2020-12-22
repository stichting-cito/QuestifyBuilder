Imports Cito.Tester.ContentModel

Namespace ContentModel.Scoring
    Friend Class ValidatingChoiceArrayScoringManipulator(Of TScoringParameter As ScoringParameter) : Inherits ScoringManipulatorDecorator(Of TScoringParameter) : Implements IValidatingChoiceArrayScoringManipulator(Of String)

        Private ReadOnly _decoree As IValidatingChoiceArrayScoringManipulator(Of String)

        Public Sub New(param As TScoringParameter, decoree As IValidatingChoiceArrayScoringManipulator(Of String))
            MyBase.New(param, decoree)
            _decoree = decoree
        End Sub

        Protected Overridable Sub Clear() Implements IChoiceArrayScoringManipulator.Clear
            _decoree.Clear()
        End Sub

        Protected Overridable Function GetKeyStatus() As IDictionary(Of String, NoValueType(Of String)) Implements IChoiceArrayScoringManipulator.GetKeyStatus
            Return _decoree.GetKeyStatus()
        End Function

        Protected Overridable Function IsValid(value As String) As Boolean Implements IValidatingChoiceArrayScoringManipulator(Of String).IsValid
            Return _decoree.IsValid(value)
        End Function

        Protected Overridable Sub RemoveKey(key As String) Implements IChoiceArrayScoringManipulator.RemoveKey
            _decoree.RemoveKey(key)
        End Sub

        Protected Overridable Sub SetKey(key As String, value As NoValueType(Of String)) Implements IChoiceArrayScoringManipulator.SetKey
            _decoree.SetKey(key, value)
        End Sub

    End Class
End Namespace