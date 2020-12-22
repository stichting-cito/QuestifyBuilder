
Imports Cito.Tester.ContentModel
Imports System.Linq

Namespace ContentModel.Scoring
    <ScoreEditorFor(GetType(ChoiceScoringParameter))>
    Friend Class MultiResponseScoringManipulator
        Inherits BaseGapScoringManipulator(Of Boolean)
        Implements IChoiceScoringManipulator

        Private _currentKeyId As String = String.Empty

        Public Sub New(manipulator As IFindingManipulator, param As ChoiceScoringParameter)
            MyBase.New(manipulator, param)
            Debug.Assert(param.IsSingleChoice = False, "This score editor is used for MR not MC")
        End Sub
        Protected Overrides Function GetValues(lst As IEnumerable(Of BaseValue)) As IEnumerable(Of GapValue(Of Boolean))
            Dim result As New List(Of GapValue(Of Boolean))
            For Each val As BaseValue In lst
                result.Add(New GapValue(Of Boolean)(DirectCast(val, BooleanValue).Value))
            Next
            Return result
        End Function

        Protected Overrides Function GetDefaultValue() As GapValue(Of Boolean)
            Return New GapValue(Of Boolean)(False)
        End Function

        Public Sub IChoiceScoringManipulator_Clear() Implements IChoiceScoringManipulator.Clear
            Clear()
        End Sub

        Public Function IChoiceScoringManipulator_GetKeyStatus1() As IDictionary(Of String, Boolean) Implements IChoiceScoringManipulator.GetKeyStatus
            Dim ret As New Dictionary(Of String, Boolean)

            For Each key In GetkeyStatus()
                Dim value As Boolean = False
                If (key.Value.Any()) Then
                    value = key.Value.First.Value
                End If
                ret.Add(key.Key, value)
            Next

            Return ret
        End Function

        Public Sub IChoiceScoringManipulator_RemoveKey(key As String) Implements IChoiceScoringManipulator.RemoveKey
            ReplaceKeyValueAt(key, New GapValue(Of Boolean)(False), 0)
        End Sub

        Public Sub IChoiceScoringManipulator_SetKey(key As String) Implements IChoiceScoringManipulator.SetKey
            ReplaceKeyValueAt(key, New GapValue(Of Boolean)(True), 0)
        End Sub

        Public Sub IChoiceScoringManipulator_SetKeyWithDefaultValue(key As String) Implements IChoiceScoringManipulator.SetKeyWithDefaultValue
            ReplaceKeyValueAt(key, GetDefaultValue(), 0)
        End Sub

        Protected Overrides Function BaseValueToDisplayString(key As String, baseValue As BaseValue) As String
            Dim booleanValue As BooleanValue = TryCast(baseValue, BooleanValue)
            If booleanValue IsNot Nothing Then
                Return booleanValue.ToString()
            End If
            Return MyBase.BaseValueToDisplayString(key, baseValue)
        End Function

        Public Overrides Sub ReplaceKeyValueAt(key As String, value As Boolean, index As Integer)
            _currentKeyId = key
            Manipulator.ReplaceKeyWithSpecificOptionals(GetFactIdentfier(key), New GapValue(Of Boolean)(value), index)
        End Sub

        Public Overrides Sub ReplaceKeyValueAt(key As String, value As GapValue(Of Boolean), index As Integer)
            _currentKeyId = key
            Manipulator.ReplaceKeyWithSpecificOptionals(GetFactIdentfier(key), value, index, If(value.Value, 1, 0))
        End Sub

        Public Overrides Sub SetKey(key As String, ParamArray values() As Boolean)
            _currentKeyId = key
            Manipulator.SetKeyWithOptionals(GetFactIdentfier(key), values.Select(Function(v) New GapValue(Of Boolean)(v)).ToArray())
        End Sub

        Public Overrides Sub SetKey(key As String, ParamArray values() As GapValue(Of Boolean))
            _currentKeyId = key
            If values(0) IsNot Nothing Then
                Manipulator.SetKeyWithOptionals(If(values(0).Value, 1, 0), GetFactIdentfier(key), values)
            Else
                Manipulator.SetKeyWithOptionals(GetFactIdentfier(key), values)
            End If
        End Sub

        Public Overrides Function GetDomainForKey(ByVal key As String) As String
            Return FactValueDomainNameGenerators.GetDomainByScoringParamPlusPrefix(ScoreParameter, _currentKeyId, key)
        End Function

        Public Function IsValid(value As String) As Boolean Implements IChoiceScoringManipulator.IsValid
            Throw New NotImplementedException()
        End Function
    End Class

End Namespace