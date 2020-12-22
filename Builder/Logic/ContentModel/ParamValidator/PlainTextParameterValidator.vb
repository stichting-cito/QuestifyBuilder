Imports Cito.Tester.ContentModel
Imports System.Text
Imports System.Linq

Namespace ContentModel.ParamValidator

    Friend Class PlainTextParameterValidator : Inherits ValueParameterValidator(Of PlainTextParameter)
        Private Const ValidationRegEx = "validationRegEx"

        Sub New(p As ParameterBase)
            MyBase.New(p)
        End Sub

        Public Overrides Function GetError() As IEnumerable(Of String)
            Dim errors = MyBase.GetError.ToList()
            If Not IsRegexValid() Then
                Dim message = "Text doens't meet regex specification"
                If Not String.IsNullOrEmpty(Param.ValidationRegExMessage) Then
                    message = Param.ValidationRegExMessage
                End If
                errors.Add(String.Format(message))
            End If
            Return errors
        End Function

        Protected Overrides Sub SetKnownDesignerSettings(ByRef knownParameters As List(Of String))
            MyBase.SetKnownDesignerSettings(knownParameters)
            knownParameters.Add(validationRegEx)
            knownParameters.Add("required")
        End Sub

        Public Overrides Function DoCheckIsValid() As Boolean
            Return MyBase.DoCheckIsValid() AndAlso IsRegexValid()
        End Function

        Private Function IsRegexValid() As Boolean
            Dim isValue = true
            Dim regex = Param.GetDesignerValue(validationRegEx, String.Empty)
            If Not String.IsNullOrEmpty(regex) Then
                Dim regExEngineInstance As New RegularExpressions.Regex(regex)
                Dim m As RegularExpressions.Match = regExEngineInstance.Match(Param.Value)
                isValue = m.Success
            End If
            Return isValue
        End Function

        Protected Overrides Function ParamHasValue(p As PlainTextParameter) As Boolean
            Return Not String.IsNullOrEmpty(p.Value)
        End Function
    End Class

End Namespace
