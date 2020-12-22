Imports System.Linq
Imports Cito.Tester.ContentModel

Namespace ContentModel.Scoring

    <ScoreEditorFor(GetType(ChoiceScoringParameter))>
    <ScoreEditorFor(GetType(MultiChoiceScoringParameter))>
    <ScoreEditorFor(GetType(InlineChoiceScoringParameter))>
    Friend Class ChoiceScoringManipulator
        Inherits BaseScoringManipulator(Of ChoiceScoringParameter)
        Implements IChoiceScoringManipulator

        Public Sub New(manipulator As IFindingManipulator, param As ChoiceScoringParameter)
            MyBase.New(manipulator, param)
            Debug.Assert(param.IsSingleChoice = True, "This score editor is used for MC not MR")
            manipulator.SetDomainOverride(Function(id) GetDomainForKey(id))
        End Sub

        Public Sub Clear() Implements IChoiceScoringManipulator.Clear
            MyBase.RemoveAllManipulatableFacts()
        End Sub

        Public Function GetkeyStatus() As IDictionary(Of String, Boolean) Implements IChoiceScoringManipulator.GetKeyStatus
            Dim ret As New Dictionary(Of String, Boolean)

            Dim keys As New HashSet(Of String)

            Dim ids = GetKeysAlreadyManipulated()

            For Each id In ids

                If (Not ScoreParameter.Value.Any(Function(parameterColl) parameterColl.Id = id)) Then
                    Continue For
                ElseIf (Not String.IsNullOrEmpty(ScoreParameter.CollectionIdx) AndAlso Not id.EndsWith(ScoreParameter.CollectionIdx)) Then
                    Continue For
                End If
                Dim result = Manipulator.GetKeys(GetFactIdentfier(id))
                For Each e In result
                    For Each v In e
                        If TypeOf v Is StringValue Then
                            Dim val = DirectCast(v, StringValue)
                            Debug.Assert(id = val.Value, "These should match, this is how MC works")
                            keys.Add(val.Value)
                        Else
                            Dim val = DirectCast(v, BooleanValue)
                            If (val.Value = True) Then
                                keys.Add(id)
                            End If
                        End If

                    Next
                Next
            Next

            If (ScoreParameter.Value IsNot Nothing) Then
                For Each p In ScoreParameter.Value
                    ret.Add(p.Id, keys.Contains(p.Id))
                Next
            End If

            Return ret
        End Function

        Public Sub SetKey(key As String) Implements IChoiceScoringManipulator.SetKey

            If Not ScoreParameter.Value.Any(Function(p) p.Id = key) Then Throw New KeyNotFoundException()

            If ScoreParameter.IsSingleChoice Then
                If (Not Manipulator.IsConceptManipulator) AndAlso ScoreParameter.IsSingleValue Then
                    Clear()
                Else
                    RemoveKey(key)
                End If
                Manipulator.SetKey(GetFactIdentfier(key), key)
            Else
                Manipulator.ReplaceKeyWithSpecificOptionals(GetFactIdentfier(key), True, 0)
            End If

        End Sub


        Public Sub RemoveKey(key As String) Implements IChoiceScoringManipulator.RemoveKey
            Manipulator.RemoveFact(GetFactIdentfier(key))
        End Sub

        Public Sub SetKeyWithDefaultValue(key As String) Implements IChoiceScoringManipulator.SetKeyWithDefaultValue
            Manipulator.SetKey(GetFactIdentfier(key), key)
        End Sub

        Public Overrides Function GetFactSetNumbers(scoreKey As String) As IEnumerable(Of Integer)
            Dim factSetNumbers = New HashSet(Of Integer)()
            For Each choiceValue In ScoreParameter.Value
                factSetNumbers.UnionWith(MyBase.GetFactSetNumbers(choiceValue.Id))
            Next
            Return factSetNumbers
        End Function

        Public Overrides Function GetDomainForKey(ByVal key As String) As String
            Return FactValueDomainNameGenerators.GetDomainByScoringParam(ScoreParameter)
        End Function

        Protected Overrides Function GetFactIdentfier(key As String) As String
            If TypeOf ScoreParameter Is SelectPointScoringParameter Then
                Return $"A{ScoreParameter.IdentifierPostFix()}"
            Else
                Return MyBase.GetFactIdentfier(key)
            End If
        End Function

        Public Function IsValid(value As String) As Boolean Implements IChoiceScoringManipulator.IsValid
            If String.IsNullOrEmpty(value) Then
                Return True
            End If

            If TypeOf ScoreParameter Is MultiChoiceScoringParameter AndAlso ScoreParameter.IsSingleChoice AndAlso Not ScoreParameter.IsSingleValue Then
                Dim countOfThisKey As Integer = GetkeyStatus().Where(Function(k) k.Key = value).Count()
                If countOfThisKey > 1 Then
                    Return False
                End If
            End If

            Return True
        End Function
    End Class

End Namespace
