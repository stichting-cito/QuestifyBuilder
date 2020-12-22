

Namespace Commanding

    Public Class NullParameterBinding
        Inherits ParameterBinding


        Public Overrides Function GetCommandParameter(source As Object) As Object
            Return Nothing
        End Function


    End Class

End Namespace

