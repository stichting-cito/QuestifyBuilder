Imports System.Linq
Imports Cito.Tester.ContentModel

Namespace ItemEditor
    Public Class SubParameterViewFactory
        Private ReadOnly _collectionParameter As CollectionParameter
        Private ReadOnly _groupDictionary As Dictionary(Of String, Group)

        Public Sub New(collectionParameter As CollectionParameter)
            _collectionParameter = collectionParameter
            _groupDictionary = New Dictionary(Of String, Group)(StringComparer.InvariantCultureIgnoreCase)
        End Sub

        Public Function GetGroups() As IEnumerable(Of IGroup)
            Return _collectionParameter.Value.[Select](Function(parameterSet) CreateGroup(parameterSet))
        End Function

        Public Function CreateGroup(parameterSet As ParameterCollection) As IGroup
            Dim newGroup As Group = New Group(parameterSet.Id)
            parameterSet.InnerParameters.ForEach(Sub(prm) newGroup.AddParameter(ParameterEvaluatorFactory.Create(prm)))
            _groupDictionary.Add(newGroup.Name, newGroup)
            Return newGroup
        End Function
    End Class
End Namespace
