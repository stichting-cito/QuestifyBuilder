Imports Cito.Tester.ContentModel

Namespace ContentModel.Scoring

    Friend Class ChoiceScoringManipulatorDecorator : Inherits ScoringManipulatorDecorator(Of ChoiceScoringParameter) : Implements IChoiceScoringManipulator

        Private ReadOnly _decoree As IChoiceScoringManipulator

        Public Sub New(param As ChoiceScoringParameter, decoree As IChoiceScoringManipulator)
            MyBase.New(param, decoree)
            _decoree = decoree
        End Sub


        Protected Overridable Sub Clear() Implements IChoiceScoringManipulator.Clear
            _decoree.Clear()
        End Sub

        Protected Overridable Function GetKeyStatus() As IDictionary(Of String, Boolean) Implements IChoiceScoringManipulator.GetKeyStatus
            Return _decoree.GetKeyStatus()
        End Function

        Protected Overridable Sub RemoveKey(key As String) Implements IChoiceScoringManipulator.RemoveKey
            _decoree.RemoveKey(key)
        End Sub

        Protected Overridable Sub SetKey(key As String) Implements IChoiceScoringManipulator.SetKey
            _decoree.SetKey(key)
        End Sub

        Protected Overridable Sub SetKeyWithDefaultValue(key As String) Implements IChoiceScoringManipulator.SetKeyWithDefaultValue
            _decoree.SetKeyWithDefaultValue(key)
        End Sub

        Protected Overridable Function IsValid(value As String) As Boolean Implements IChoiceScoringManipulator.IsValid
            Throw New NotImplementedException()
        End Function


    End Class

End Namespace