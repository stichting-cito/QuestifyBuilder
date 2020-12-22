Imports Cito.Tester.ContentModel

Namespace ContentModel.ParamValidator
    Friend Class GapImageParameterValidator : Inherits ResourceParameterValidator

        Sub New(param As ParameterBase)
            MyBase.New(param)
        End Sub

        Protected Overrides Function ParamHasValue(p As ResourceParameter) As Boolean
            If TypeOf p Is GapImageParameter Then
                If DirectCast(p, GapImageParameter).ContentType = GapImageParameter.GapImageParameterContentType.Text Then
                    Return Not String.IsNullOrEmpty(DirectCast(p, GapImageParameter).EnteredText)
                Else
                    Return Not String.IsNullOrEmpty(p.Value)
                End If
            End If
            Return True
        End Function

    End Class

End Namespace

