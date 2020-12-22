Imports System.Linq
Imports Cito.Tester.ContentModel

Namespace ContentModel.Scoring

    <ScoreEditorFor(GetType(MatrixScoringParameter))>
    Friend Class MatrixScoringManipulator : Implements IChoiceArrayScoringManipulator

        Private ReadOnly _scoreParameter As MatrixScoringParameter
        Private ReadOnly _adaptee As IGapScoringManipulator(Of String)
        Private _currentKeyId As String = String.Empty

        Public Sub New(manipulator As IFindingManipulator, param As MatrixScoringParameter)
            _adaptee = New MatrixRowScoreManipulator(manipulator, param)
            manipulator.SetDomainOverride(Function(id) GetDomainForKey(id))
            _scoreParameter = param
        End Sub


        Private Sub Clear() Implements IChoiceArrayScoringManipulator.Clear
            _currentKeyId = String.Empty
            _adaptee.Clear()
        End Sub

        Private Sub RemoveKey(key As String) Implements IChoiceArrayScoringManipulator.RemoveKey
            _adaptee.RemoveKey(key)
        End Sub

        Private Function CanBeRemovedFromFactSet(parameterCollectionId As String) As Boolean Implements IScoreManipulator.CanBeRemovedFromFactSet
            Return _adaptee.CanBeRemovedFromFactSet(parameterCollectionId)
        End Function

        Private Function CreateFactSetTarget() As Integer Implements IScoreManipulator.CreateFactSetTarget
            Return _adaptee.CreateFactSetTarget()
        End Function

        Private Sub CreateFindingTarget(parameterCollectionId As String) Implements IScoreManipulator.CreateFindingTarget
            _adaptee.CreateFindingTarget(parameterCollectionId)
        End Sub

        Private ReadOnly Property FactSetTarget As Integer? Implements IScoreManipulator.FactSetTarget
            Get
                Return _adaptee.FactSetTarget
            End Get
        End Property

        Private Function GetFactIdForKey(key As String) As String Implements IScoreManipulator.GetFactIdForKey
            Return _adaptee.GetFactIdForKey(key)
        End Function

        Private Function GetHowToScoreMethod() As EnumScoringMethod Implements IScoreManipulator.GetHowToScoreMethod
            Return _adaptee.GetHowToScoreMethod()
        End Function

        Private Function GetKeysAlreadyManipulated() As IEnumerable(Of String) Implements IScoreManipulator.GetKeysAlreadyManipulated
            Return _adaptee.GetKeysAlreadyManipulated()
        End Function

        Private Function GetManipulatableKeys() As IEnumerable(Of String) Implements IScoreManipulator.GetManipulatableKeys
            Return _adaptee.GetManipulatableKeys()
        End Function

        Private Sub RemoveFactSetTarget(factSetNumber As Integer) Implements IScoreManipulator.RemoveFactSetTarget
            _adaptee.RemoveFactSetTarget(factSetNumber)
        End Sub

        Private Sub SetFactSetTarget(factSetNumber As Integer?) Implements IScoreManipulator.SetFactSetTarget
            _adaptee.SetFactSetTarget(factSetNumber)
        End Sub

        Private Sub SetHowToScoreMethod(e As EnumScoringMethod) Implements IScoreManipulator.SetHowToScoreMethod
            _adaptee.SetHowToScoreMethod(e)
        End Sub


        Private Function GetFactSetNumbers(parameterCollectionId As String) As IEnumerable(Of Integer) Implements IChoiceArrayScoringManipulator.GetFactSetNumbers
            Return _adaptee.GetFactSetNumbers(parameterCollectionId)
        End Function

        Private Function GetkeyStatus() As IDictionary(Of String, NoValueType(Of String)) Implements IChoiceArrayScoringManipulator.GetKeyStatus
            Dim gapkeys = _adaptee.GetKeyStatus()

            Dim keys As New Dictionary(Of String, NoValueType(Of String))

            gapkeys.Keys.ToList().ForEach(Sub(key) keys.Add(key, gapkeys(key).First.Value))

            Return keys
        End Function

        Private Sub SetKey(key As String, value As NoValueType(Of String)) Implements IChoiceArrayScoringManipulator.SetKey

            _currentKeyId = key

            If Not _scoreParameter.Value.Any(Function(p) p.Id = key) Then Throw New KeyNotFoundException(
                $"Matrix row {key} not found")
            If Not _scoreParameter.MatrixColumnsDefinition.Value.Any(Function(p) p.Id = value) Then Throw New KeyNotFoundException(
                $"Matrix column {value} not found")

            _adaptee.RemoveKey(key)
            _adaptee.SetKey(key, New GapValue(Of String)(value))

        End Sub

        Public Function GetDisplayValueForKey(key As String) As String Implements IScoreManipulator.GetDisplayValueForKey
            Return _adaptee.GetDisplayValueForKey(key)
        End Function

        Public Function GetBaseValueForKey(key As String) As BaseValue Implements IScoreManipulator.GetBaseValueForKey
            Return _adaptee.GetBaseValueForKey(key)
        End Function

        Public Function GetValueForKey(key As String) As String Implements IScoreManipulator.GetValueForKey
            Return _adaptee.GetValueForKey(key)
        End Function

        Public Function GetDomainForKey(ByVal key As String) As String Implements IScoreManipulator.GetDomainForKey
            Return FactValueDomainNameGenerators.GetDomainByScoringParamPlusPostFix(_scoreParameter, _currentKeyId, key)
        End Function


        Private Class MatrixRowScoreManipulator
            Inherits BaseGapScoringManipulator(Of String)

            Sub New(findingManipulator As IFindingManipulator, param As ScoringParameter)
                MyBase.New(findingManipulator, param)
            End Sub

            Protected Overrides Function GetDefaultValue() As GapValue(Of String)
                Return New GapValue(Of String)("")
            End Function

            Protected Overrides Function GetValues(lst As IEnumerable(Of BaseValue)) As IEnumerable(Of GapValue(Of String))
                Return lst.Select(Function(e) New GapValue(Of String)(DirectCast(e, StringValue).Value))
            End Function


        End Class


    End Class

End Namespace
