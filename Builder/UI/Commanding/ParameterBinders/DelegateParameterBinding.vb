

Namespace Commanding

    Public Class DelegateParameterBinding
        Inherits ParameterBinding


        Private ReadOnly _parameter As GetCommandParameterDelegate



        Public Sub New(parameter As GetCommandParameterDelegate)
            If parameter Is Nothing Then
                Throw New ArgumentNullException("parameter")
            End If
            Me._parameter = parameter
        End Sub



        Public Delegate Function GetCommandParameterDelegate(source As Object) As Object



        Overrides Function GetCommandParameter(source As Object) As Object
            Return _parameter.Invoke(source)
        End Function


    End Class

End Namespace

