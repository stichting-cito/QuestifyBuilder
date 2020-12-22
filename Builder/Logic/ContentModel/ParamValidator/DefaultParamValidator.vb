Imports Cito.Tester.ContentModel

Namespace ContentModel.ParamValidator
    Friend Class DefaultParamValidator
        Inherits baseParamValidator

        Public Sub New(param As ParameterBase)
            MyBase.New(param)
        End Sub

        Public Overrides Function GetError() As IEnumerable(Of String)
            Return New List(Of String)
        End Function

        Public Overrides Function DoCheckIsValid() As Boolean
            Return True
        End Function

    End Class

End Namespace
