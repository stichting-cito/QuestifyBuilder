Imports Cito.Tester.ContentModel

Namespace ItemEditor
    Friend Class ParameterEvaluator
        Inherits ParameterEvaluatorBase
        Implements IHasGroupInfluence
        Private ReadOnly _groupInfluences As New List(Of IGroupInfluence)()

        Public Sub New(parameter As ParameterBase)
            MyBase.New(parameter)
            _groupInfluences.AddRange(GroupInfluenceFactory.CreateFor(parameter))
        End Sub

        Public Function GetGroupInfluence(Of T As IGroupInfluence)() As IEnumerable(Of IGroupInfluence) Implements IHasGroupInfluence.GetGroupInfluence
            Return _groupInfluences
        End Function
    End Class
End Namespace
