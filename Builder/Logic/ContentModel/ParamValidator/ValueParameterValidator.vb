Imports Cito.Tester.ContentModel
Imports System.Linq

Namespace ContentModel.ParamValidator

    Friend MustInherit Class ValueParameterValidator(Of T As ParameterBase) : Inherits baseParamValidator

        Sub New(param As ParameterBase)
            MyBase.New(param)
        End Sub

        Public Overrides Function GetError() As IEnumerable(Of String)
            If Not IfRequiredValidateContent() Then
                Dim label = Param.GetDesignerValue("label", String.Empty)
                If String.IsNullOrEmpty(label) Then label = Param.GetDesignerValue("group", String.Empty)
                If String.IsNullOrEmpty(label) Then label = Param.Name
                Return New String() {String.Format(My.Resources.MandatoryEmptyParameterMessage, label)}.ToList()
            Else
                Return New List(Of String)
            End If
        End Function

        Public Overrides Function DoCheckIsValid() As Boolean
            Return IfRequiredValidateContent()
        End Function

        Protected ReadOnly Property Param As T
            Get
                Return DirectCast(Parameter, T)
            End Get
        End Property

        Protected Function IfRequiredValidateContent() As Boolean

            If Param.GetDesignerValue("required", False) Then
                Return ParamHasValue(Param)
            Else
                Return True
            End If
        End Function

        Protected MustOverride Function ParamHasValue(p As T) As Boolean

    End Class

End Namespace
