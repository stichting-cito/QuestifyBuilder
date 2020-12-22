Imports Questify.Builder.Logic.Annotations
Imports Questify.Builder.Logic.ContentModel
Imports Cito.Tester.ContentModel


Namespace ItemEditor
    MustInherit Class ParameterEvaluatorBase
        Implements IParameterEvaluator
        Private ReadOnly _parameter As ParameterBase

        Public Sub New(<NotNull> parameter As ParameterBase)
            _parameter = parameter
            OwningGroupName = parameter.Group()
            IsVisible = parameter.IsVisible()
            SetDefaultValue(_parameter)
        End Sub

        <NotNull> _
        Public Property OwningGroupName As String Implements IParameterEvaluator.OwningGroupName


        <NotNull> _
        Public Property IsVisible As Boolean Implements IParameterEvaluator.IsVisible


        <NotNull> _
        Public ReadOnly Property Parameter() As ParameterBase Implements IParameterEvaluator.Parameter
            Get
                Return _parameter
            End Get
        End Property

        Protected Sub SetDefaultValue(param As ParameterBase)
            Dim value As String = param.DefaultValue()
            If Not String.IsNullOrEmpty(value) Then
                param.SetValue(value)
            End If
        End Sub

    End Class
End Namespace
