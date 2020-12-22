Imports Cito.Tester.ContentModel

Namespace ContentModel.Scoring
    Friend Class GapScoringManipulatorDecorator(Of TScoringParameter As ScoringParameter, T)
        Inherits ScoringManipulatorDecorator(Of TScoringParameter)
        Implements IGapScoringManipulator(Of T)

        Private ReadOnly _decoree As IGapScoringManipulator(Of T)

        Public Sub New(param As TScoringParameter, decoree As IGapScoringManipulator(Of T))
            MyBase.New(param, decoree)
            _decoree = decoree
        End Sub


        Protected Overridable Sub Clear() Implements IGapScoringManipulator(Of T).Clear
            _decoree.Clear()
        End Sub

        Protected Overridable Function GetKeyStatus() As IDictionary(Of String, IEnumerable(Of GapValue(Of T))) Implements IGapScoringManipulator(Of T).GetKeyStatus
            Return _decoree.GetKeyStatus()
        End Function

        Protected Overridable Function GetPreProcessingMethods(key As String) As IEnumerable(Of String) Implements IGapScoringManipulator(Of T).GetPreProcessingMethods
            Return _decoree.GetPreProcessingMethods(key)
        End Function

        Protected Overridable Function GetValuePrefixes(key As String) As IEnumerable(Of String) Implements IGapScoringManipulator(Of T).GetValuePrefixes
            Return _decoree.GetValuePrefixes(key)
        End Function

        Protected Overridable Function GetValue(key As String, index As Integer) As GapValue(Of T) Implements IGapScoringManipulator(Of T).GetValue
            Return _decoree.GetValue(key, index)
        End Function

        Protected Overridable Sub RemoveKey(key As String) Implements IGapScoringManipulator(Of T).RemoveKey
            _decoree.RemoveKey(key)
        End Sub

        Protected Overridable Sub ReplaceKeyValueAt(key As String, value As GapValue(Of T), index As Integer) Implements IGapScoringManipulator(Of T).ReplaceKeyValueAt
            _decoree.ReplaceKeyValueAt(key, value, index)
        End Sub

        Protected Overridable Sub ReplaceKeyValueAt(key As String, value As T, index As Integer) Implements IGapScoringManipulator(Of T).ReplaceKeyValueAt
            _decoree.ReplaceKeyValueAt(key, value, index)
        End Sub

        Protected Overridable Sub SetKey(key As String, ParamArray values() As GapValue(Of T)) Implements IGapScoringManipulator(Of T).SetKey
            _decoree.SetKey(key, values)
        End Sub

        Protected Overridable Sub SetKey(key As String, ParamArray values() As T) Implements IGapScoringManipulator(Of T).SetKey
            _decoree.SetKey(key, values)
        End Sub

        Protected Overridable Sub SetKeys(key As String, values As IEnumerable(Of GapValue(Of T))) Implements IGapScoringManipulator(Of T).SetKeys
            _decoree.SetKeys(key, values)
        End Sub

        Protected Overridable Sub SetKeys(key As String, values As IEnumerable(Of T)) Implements IGapScoringManipulator(Of T).SetKeys
            _decoree.SetKeys(key, values)
        End Sub

        Protected Overridable Sub SetKeyWithDefaultValue(key As String) Implements IGapScoringManipulator(Of T).SetKeyWithDefaultValue
            _decoree.SetKeyWithDefaultValue(key)
        End Sub

        Protected Overridable Sub SetPreProcessingMethods(key As String, preProcessing As IEnumerable(Of String)) Implements IGapScoringManipulator(Of T).SetPreProcessingMethods
            _decoree.SetPreProcessingMethods(key, preProcessing)
        End Sub


    End Class
End Namespace