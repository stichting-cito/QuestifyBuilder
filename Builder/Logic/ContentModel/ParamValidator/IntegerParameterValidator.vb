Imports Cito.Tester.ContentModel
Imports System.Linq

Namespace ContentModel.ParamValidator
    Friend Class IntegerParameterValidator : Inherits ValueParameterValidator(Of IntegerParameter)

        Sub New(param As ParameterBase)
            MyBase.New(param)
        End Sub

        Protected Overrides Sub SetKnownDesignerSettings(ByRef knownParameters As List(Of String))
            MyBase.SetKnownDesignerSettings(knownParameters)
            knownParameters.Add("required")
        End Sub

        Public Overrides Function GetError() As IEnumerable(Of String)
            Dim errors = MyBase.GetError.ToList()
            If Not IsInRange Then
                Errors.Add(String.Format(My.Resources.OnlyAValueWithinTheRangeOfTillIsAllowed, Param.RangeFrom.ToString, Param.RangeTo.ToString))
            End If
            Return Errors
        End Function

        Public Overrides Function DoCheckIsValid() As Boolean
            Dim baseResult = MyBase.DoCheckIsValid()
            If baseResult Then baseResult = IsInRange()
            Return baseResult
        End Function

        Protected Overrides Function ParamHasValue(p As IntegerParameter) As Boolean
            Return True
        End Function

        Private Function IsInRange() As Boolean
            Dim result = True
            Dim rangeFrom = Param.RangeFrom
            Dim rangeTo = Param.RangeTo
            If rangeFrom.HasValue AndAlso rangeTo.HasValue AndAlso (Param.Value < rangeFrom.Value OrElse Param.Value > rangeTo.Value) OrElse
                rangeFrom.HasValue AndAlso Param.Value < rangeFrom.Value OrElse
                rangeTo.HasValue AndAlso Param.Value > rangeTo.Value Then
                result = False
            End If
            Return result
        End Function

    End Class

End Namespace
