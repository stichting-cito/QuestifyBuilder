Imports Cito.Tester.ContentModel
Imports System.Linq

Namespace ContentModel.ParamValidator
    Friend Class MatrixScoringParameterValidator : Inherits CollectionParameterValidator

        Sub New(param As ParameterBase)
            MyBase.New(param)
        End Sub

        Public Overrides Function GetError() As IEnumerable(Of String)
            Dim baseResult = MyBase.GetError()
            Dim valueBag As New Dictionary(Of String, String)(StringComparison.InvariantCultureIgnoreCase)
            baseResult = baseResult.ToArray.Concat({DirectCast(Param, MatrixScoringParameter).MatrixColumnsDefinition.GetError(valueBag)}.ToList())
            baseResult = baseResult.ToArray.Concat({DirectCast(Param, MatrixScoringParameter).LineLabelColumnWidth.GetError(valueBag)}.ToList())
            Return baseResult
        End Function

        Public Overrides Function DoCheckIsValid() As Boolean
            Dim baseResult = MyBase.DoCheckIsValid()
            baseResult = baseResult AndAlso DirectCast(Param, MatrixScoringParameter).MatrixColumnsDefinition.IsValid() AndAlso DirectCast(Param, MatrixScoringParameter).LineLabelColumnWidth.IsValid()
            Return baseResult
        End Function

    End Class
End Namespace
