Imports Cito.Tester.ContentModel
Imports System.Linq
Imports System.Globalization

Namespace ContentModel.Scoring

    <ScoreEditorFor(GetType(DateScoringParameter))>
    Friend Class DateScoringManipulator : Inherits BaseGapScoringManipulator(Of String)

        Private _param As DateScoringParameter

        Public Sub New(manipulator As IFindingManipulator, param As DateScoringParameter)
            MyBase.New(manipulator, param)
            _param = param
        End Sub

        Private Function BaseValueToGapValue(baseValue As BaseValue) As GapValue(Of String)
            Dim dtm As DateTime
            If DateTime.TryParseExact(baseValue.ToString, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, dtm) Then
                Return New GapValue(Of String)(New StringValue(dtm.ToString("d", CultureInfo.CurrentCulture)).Value)
            End If
            Return New GapValue(Of String)(DirectCast(baseValue, StringValue).Value)
        End Function

        Private Function GapValueToBaseValue(gapValue As GapValue(Of String)) As GapValue(Of String)
            Return New GapValue(Of String)(GapValueToBaseValue(gapValue.Value))
        End Function

        Private Function GapValueToBaseValue(gapValue As String) As String
            Dim dtm As DateTime
            If DateTime.TryParseExact(gapValue, "d", CultureInfo.CurrentCulture, DateTimeStyles.None, dtm) Then
                Return dtm.ToString("d", CultureInfo.InvariantCulture)
            End If
            Return gapValue
        End Function

        Protected Overrides Function GetValues(lst As IEnumerable(Of BaseValue)) As IEnumerable(Of GapValue(Of String))
            Return lst.Select(Function(e) BaseValueToGapValue(e))
        End Function

        Protected Overrides Function GetDefaultValue() As GapValue(Of String)
            Return New GapValue(Of String)("")
        End Function

        Public Overrides Sub SetKey(key As String, ParamArray values() As String)
            MyBase.SetKey(key, values.Select(Function(x) GapValueToBaseValue(x)).ToArray())
        End Sub

        Public Overrides Sub SetKey(key As String, ParamArray values() As GapValue(Of String))
            MyBase.SetKey(key, values.Select(Function(x) GapValueToBaseValue(x)).ToArray())
        End Sub

        Public Overrides Sub SetKeys(key As String, values As IEnumerable(Of String))
            Dim dateValues As IEnumerable(Of String) = values.Select(Function(x) GapValueToBaseValue(x))
            MyBase.SetKeys(key, dateValues)
        End Sub

        Public Overrides Sub SetKeys(key As String, values As IEnumerable(Of GapValue(Of String)))
            Dim dateValues As IEnumerable(Of GapValue(Of String)) = values.Select(Function(x) GapValueToBaseValue(x))
            MyBase.SetKeys(key, dateValues)
        End Sub

        Public Overrides Sub ReplaceKeyValueAt(key As String, value As String, index As Integer)
            MyBase.ReplaceKeyValueAt(key, GapValueToBaseValue(value), index)
        End Sub

        Public Overrides Sub ReplaceKeyValueAt(key As String, values As GapValue(Of String), index As Integer)
            MyBase.ReplaceKeyValueAt(key, GapValueToBaseValue(values), index)
        End Sub

    End Class
End Namespace
