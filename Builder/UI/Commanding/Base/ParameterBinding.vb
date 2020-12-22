

Namespace Commanding

    Public MustInherit Class ParameterBinding
        Implements IParameterBinding

        MustOverride Function GetCommandParameter(source As Object) As Object Implements IParameterBinding.GetCommandParameter

    End Class

End Namespace