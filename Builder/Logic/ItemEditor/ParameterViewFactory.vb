Imports System.Linq
Imports Questify.Builder.Logic.Annotations
Imports Questify.Builder.Logic.ContentModel
Imports Cito.Tester.ContentModel

Namespace ItemEditor
    Public Class ParameterViewFactory
        Private ReadOnly _parameterSetCollection As ParameterSetCollection
        Private ReadOnly _groups As Lazy(Of List(Of Group))
        Private ReadOnly _parameters As Lazy(Of List(Of IParameterEvaluator))
        Private ReadOnly _groupDictionary As Dictionary(Of String, Group)
        Private ReadOnly _groupInfluencers As Dictionary(Of String, List(Of IGroupInfluence))

        Public Sub New(parameterSetCollection As ParameterSetCollection)
            _parameterSetCollection = parameterSetCollection
            _groups = New Lazy(Of List(Of Group))(AddressOf EnsureGroups)
            _parameters = New Lazy(Of List(Of IParameterEvaluator))(AddressOf EnsureParameters)
            _groupDictionary = New Dictionary(Of String, Group)(StringComparer.InvariantCultureIgnoreCase)
            _groupInfluencers = New Dictionary(Of String, List(Of IGroupInfluence))(StringComparer.InvariantCultureIgnoreCase)
        End Sub

        Public Function GetGroups() As IEnumerable(Of IGroup)
            Return _groups.Value.Where(AddressOf CanBePresented)
        End Function

        Private Function CanBePresented(g As Group) As Boolean
            Return g.HasVisibleParameters
        End Function

        Private Function EnsureGroups() As List(Of Group)
            For Each parameter As IParameterEvaluator In _parameters.Value
                Dim group As Group = GetGroupFor(parameter)
                group.AddParameter(parameter)
            Next
            Return New List(Of Group)(_groupDictionary.Values)
        End Function

        <NotNull> _
        Private Function GetGroupFor(parameter As IParameterEvaluator) As Group
            Dim group As Group
            Dim groupName As String = parameter.OwningGroupName

            If Not _groupDictionary.TryGetValue(groupName, group) Then
                group = CreateGroup(groupName)
                _groupDictionary.Add(groupName, group)
            End If

            Return group
        End Function

        Private Function CreateGroup(groupName As String) As Group
            Dim group As Group = New Group(groupName)

            Dim influences As List(Of IGroupInfluence) = New List(Of IGroupInfluence)
            If _groupInfluencers.TryGetValue(groupName, influences) Then
                group.AddInfluencers(influences)
            End If

            Return group
        End Function

        Private Function EnsureParameters() As List(Of IParameterEvaluator)
            Dim parameters as List(Of IParameterEvaluator) = _parameterSetCollection.AsListOfParameters().Select(Function(x) ParameterEvaluatorFactory.Create(x)).ToList()
            For Each par As IParameterEvaluator In parameters
                RegisterGroupInfluencers(par)
            Next
            Return parameters.ToList()
        End Function

        Private Sub RegisterGroupInfluencers(ByRef parameterEvaluator As IParameterEvaluator)
            Dim hasGroupInfluence as IHasGroupInfluence = TryCast(parameterEvaluator, IHasGroupInfluence)
            If hasGroupInfluence IsNot Nothing Then
                Dim influencers As IEnumerable(Of IGroupInfluence) = hasGroupInfluence.GetGroupInfluence(Of IGroupInfluence)()
                For Each influencer As IGroupInfluence In influencers
                    RegisterGroupInfluence(influencer.InfluencedGroup, influencer)
                Next
            End If
        End Sub

        Private Sub RegisterGroupInfluence(<NotNull> influencedGroup As String, influencer As IGroupInfluence)
            If Not _groupInfluencers.ContainsKey(influencedGroup) Then
                _groupInfluencers.Add(influencedGroup, New List(Of IGroupInfluence)())
            End If

            _groupInfluencers(influencedGroup).Add(influencer)
        End Sub

    End Class
End Namespace
