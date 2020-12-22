Imports Cito.Tester.ContentModel
Imports System.Linq

Namespace ContentModel.Scoring
    Friend MustInherit Class BaseScoringManipulator(Of TScoreParameter As {ScoringParameter})
        Implements IScoreManipulator


        Private Shared ReadOnly EmptyIdArray() As String = New String() {}
        Protected ReadOnly _manipulator As IFindingManipulator
        Private ReadOnly _param As TScoreParameter




        Public Sub New(manipulator As IFindingManipulator, param As TScoreParameter)
            _manipulator = manipulator
            _param = param

            _manipulator.FactIdPostFix = GetFactFilter()
        End Sub


        Public ReadOnly Property FactsetTarget As Integer? Implements IScoreManipulator.FactSetTarget
            Get
                Return Manipulator.FactsetTarget
            End Get
        End Property

        Public Overridable Function GetKeysAlreadyManipulated() As IEnumerable(Of String) _
            Implements IScoreManipulator.GetKeysAlreadyManipulated

            Dim filter As String = GetFactFilter()

            Return _
    Manipulator.GetIds().Where(Function(s) s.EndsWith(filter)).Select(
        Function(s) s.Remove(s.Length - filter.Length, filter.Length))
        End Function

        Public Function GetManipulatableKeys() As IEnumerable(Of String) _
    Implements IScoreManipulator.GetManipulatableKeys
            If (_param.Value Is Nothing) Then Return EmptyIdArray
            Return From paramSet In _param.Value Select paramSet.Id
        End Function

        Public Function GetHowToScoreMethod() As EnumScoringMethod Implements IScoreManipulator.GetHowToScoreMethod
            Return Manipulator.GetScoringMethod()
        End Function

        Public Sub SetHowToScoreMethod(e As EnumScoringMethod) Implements IScoreManipulator.SetHowToScoreMethod
            Manipulator.SetScoringMethod(e)
        End Sub

        Public Function GetFactIdForKey(key As String) As String Implements IScoreManipulator.GetFactIdForKey
            Return GetFactIdentfier(key)
        End Function

        Public Overridable Function GetDisplayValueForKey(key As String) As String Implements IScoreManipulator.GetDisplayValueForKey
            Return GetValueForKey(key)
        End Function

        Public Overridable Function GetBaseValueForKey(key As String) As BaseValue Implements IScoreManipulator.GetBaseValueForKey
            Dim keys As IEnumerable(Of IEnumerable(Of BaseValue)) = Manipulator.GetKeys(GetFactIdentfier(Key)).ToList()
            If (keys.Count() = 1) Then
                return keys.First().FirstOrDefault
            End If
            Return Nothing
        End Function

        Public Function GetValueForKey(Key As String) As String Implements IScoreManipulator.GetValueForKey
            Dim keys As IEnumerable(Of IEnumerable(Of BaseValue)) = Manipulator.GetKeys(GetFactIdentfier(Key)).ToList()
            Debug.Assert(keys.Count <= 1, "Unable to deal with multiple KeyFacts")

            If (keys.Count() = 1) Then

                Dim stringValueList = keys.First().Select(Function(baseValue) BaseValueToDisplayString(Key, baseValue))

                Return String.Join("#", stringValueList)
            End If

            Return String.Empty
        End Function

        Public Overridable Function GetFactSetNumbers(scoreKey As String) As IEnumerable(Of Integer) _
            Implements IScoreManipulator.GetFactSetNumbers
            If Manipulator.HasSets Then
                Return Manipulator.GetFactSetNumbers(scoreKey)
            End If
            Return Enumerable.Empty(Of Integer)()
        End Function

        Public Sub SetFactSetTarget(factSetNumber As Integer?) Implements IScoreManipulator.SetFactSetTarget
            Manipulator.SetFactSetTarget(factSetNumber)
        End Sub

        Public Function CreateFactSetTarget() As Integer Implements IScoreManipulator.CreateFactSetTarget
            Return Manipulator.CreateNewFactSet()
        End Function

        Public Sub CreateFindingTarget(parameterCollectionId As String) Implements IScoreManipulator.CreateFindingTarget
            Manipulator.CreateFindingTarget(GetFactIdForKey(parameterCollectionId))
        End Sub

        Public Sub RemoveFactSetTarget(factSetNumber As Integer) Implements IScoreManipulator.RemoveFactSetTarget
            Manipulator.RemoveFactSetTarget(factSetNumber)
        End Sub


        Public MustOverride Function GetDomainForKey(key As String) As String Implements IScoreManipulator.GetDomainForKey



        Protected Function GetFactFilter() As String
            If (Not String.IsNullOrEmpty(_param.InlineId) OrElse Not String.IsNullOrEmpty(_param.ControllerId)) Then
                Return ScoreParameter.IdentifierPostFix()
            End If
            Return String.Empty
        End Function



        Protected Friend ReadOnly Property Manipulator As IFindingManipulator
            Get
                Return _manipulator
            End Get
        End Property

        Protected Friend ReadOnly Property ScoreParameter As TScoreParameter
            Get
                Return _param
            End Get
        End Property


        Protected Sub RemoveAllManipulatableFacts()
            Dim toRemove = GetKeysAlreadyManipulated().ToList()
            For Each factId As String In toRemove
                Manipulator.RemoveFact(GetFactIdentfier(factId))
            Next
        End Sub

        Protected Overridable Function GetFactIdentfier(key As String) As String
            Return $"{key}{ScoreParameter.IdentifierPostFix()}"
        End Function


        Public Function CanBeRemovedFromFactSet(parameterCollectionId As String) As Boolean _
            Implements IScoreManipulator.CanBeRemovedFromFactSet
            Dim factSetNumbers = GetFactSetNumbers(parameterCollectionId)
            If factSetNumbers Is Nothing OrElse factSetNumbers.Count < 1 Then Return False
            Manipulator.SetFactSetTarget(factSetNumbers(0))
            Return Manipulator.CanFactBeRemovedFromTarget()
        End Function

        Protected Overridable Function BaseValueToDisplayString(key As String, baseValue As BaseValue) As String
            If (baseValue Is Nothing) Then Return String.Empty
            Return baseValue.ToString()
        End Function

    End Class
End Namespace
