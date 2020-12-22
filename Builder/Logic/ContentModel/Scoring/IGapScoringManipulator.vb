Namespace ContentModel.Scoring

    Public Interface IGapScoringManipulator : Inherits IScoreManipulator
        Sub Clear()

        Function GetPreProcessingMethods(key As String) As IEnumerable(Of String)

        Function GetValuePrefixes(key As String) As IEnumerable(Of String)

        Sub SetPreProcessingMethods(key As String, preProcessing As IEnumerable(Of String))

        Sub RemoveKey(key As String)

        Sub SetKeyWithDefaultValue(key As String)
    End Interface

    Public Interface IGapScoringManipulator(Of T) : Inherits IGapScoringManipulator
        Function GetValue(key As String, index As Integer) As GapValue(Of T)

        Sub SetKey(key As String, ParamArray values As T())

        Sub SetKey(key As String, ParamArray values As GapValue(Of T)())

        Sub SetKeys(key As String, values As IEnumerable(Of T))

        Sub SetKeys(key As String, values As IEnumerable(Of GapValue(Of T)))

        Sub ReplaceKeyValueAt(key As String, value As T, index As Integer)

        Sub ReplaceKeyValueAt(key As String, value As GapValue(Of T), index As Integer)

        Function GetKeyStatus() As IDictionary(Of String, IEnumerable(Of GapValue(Of T)))
    End Interface
End Namespace