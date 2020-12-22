Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ContentModel

Namespace ItemEditor
    Class CollectionParameterEvaluator
        Inherits ParameterEvaluatorBase
        Implements IHasGroups
        Private ReadOnly _parameter As CollectionParameter
        Private ReadOnly _namingStrategy As ICollectionParameterNaming
        Private ReadOnly _factory As SubParameterViewFactory
        Private ReadOnly _groups As New List(Of IGroup)()

        Public Sub New(parameter As CollectionParameter)
            MyBase.New(parameter)
            _parameter = parameter
            Dim strategy As String = parameter.SubSetIdentifierStrategy()
            _namingStrategy = CollectionParameterNamingFactory.Create(strategy, parameter)
            _factory = New SubParameterViewFactory(parameter)

            ReadGroups()
        End Sub

        Public ReadOnly Property Groups() As IEnumerable(Of IGroup) Implements IHasGroups.Groups
            Get
                Return _groups
            End Get
        End Property

        Public Function AddGroup() As IGroup Implements IHasGroups.AddGroup
            Dim newSet As ParameterCollection = CreateNewSet(_namingStrategy.GetId())
            Dim newGroup As IGroup = _factory.CreateGroup(newSet)
            _groups.Add(newGroup)
            Return newGroup
        End Function

        Private Function CreateNewSet(id As String) As ParameterCollection
            Dim newSet As ParameterCollection = _parameter.BluePrint.DeepCloneWithDesignerSettingsAndAttributeReferences()
            For Each p As ParameterBase In newSet.InnerParameters
                SetDefaultValue(p)
            Next
            newSet.Id = id
            Return newSet
        End Function

        Private Sub ReadGroups()
            _groups.Clear()
            _groups.AddRange(_factory.GetGroups())
        End Sub

    End Class
End Namespace
