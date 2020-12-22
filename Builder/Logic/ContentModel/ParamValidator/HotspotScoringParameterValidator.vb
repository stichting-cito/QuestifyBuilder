Imports Cito.Tester.ContentModel
Imports System.Linq

Namespace ContentModel.ParamValidator
    Friend Class HotspotScoringParameterValidator : Inherits CollectionParameterValidator

        Sub New(param As ParameterBase)
            MyBase.New(param)
        End Sub

        Public Overrides Function GetError() As IEnumerable(Of String)
            Dim valueBag As New Dictionary(Of String, String)(StringComparison.InvariantCultureIgnoreCase)
            Dim baseResult = {DirectCast(Param, HotspotScoringParameter).Area.GetError(valueBag)}.ToList()
            Return baseResult
        End Function

        Public Overrides Function DoCheckIsValid() As Boolean
            Return DirectCast(Param, HotspotScoringParameter).Area.IsValid()
        End Function

    End Class

End Namespace
