Imports Cito.Tester.ContentModel
Imports System.Linq

Namespace ContentModel.ParamValidator
    Friend Class CollectionParameterValidator : Inherits ValueParameterValidator(Of CollectionParameter)

        Sub New(param As ParameterBase)
            MyBase.New(param)
        End Sub

        Protected Overrides Sub SetKnownDesignerSettings(ByRef knownParameters As List(Of String))
            MyBase.SetKnownDesignerSettings(knownParameters)
            knownParameters.Add("visible")
        End Sub

        Public Overrides Function GetError() As IEnumerable(Of String)
            Dim baseResult = MyBase.GetError()
            If baseResult.Count = 0 Then
                baseResult = New String() {Param.Value.GetValidateHierarchicalErrors()}.ToList
            End If
            Return baseResult
        End Function

        Public Overrides Function DoCheckIsValid() As Boolean
            Dim baseResult = MyBase.DoCheckIsValid()

            baseResult = baseResult AndAlso Param.Value IsNot Nothing AndAlso Param.Value.ValidateHierarchical()

            Return baseResult
        End Function

        Protected Overrides Function ParamHasValue(p As CollectionParameter) As Boolean
            Return (p.Value IsNot Nothing AndAlso p.Value.Count > 0)
        End Function

    End Class

End Namespace
