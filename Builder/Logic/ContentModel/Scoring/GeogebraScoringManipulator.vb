
Imports Cito.Tester.ContentModel

Namespace ContentModel.Scoring

    <ScoreEditorFor(GetType(GeogebraScoringParameter))>
    Friend Class GeogebraScoringManipulator : Inherits StringScoringManipulator


        Private ReadOnly _param As GeogebraScoringParameter

        Public Sub New(manipulator As IFindingManipulator, param As GeogebraScoringParameter)
            MyBase.New(manipulator, param)
            _param = param
        End Sub

        Protected Overrides Function GetDefaultValue() As GapValue(Of String)
            If _param.GeogebraKey IsNot Nothing Then
                Return New GapValue(Of String)(_param.GeogebraKey)
            Else
                Return MyBase.GetDefaultValue()
            End If
        End Function

    End Class

End Namespace