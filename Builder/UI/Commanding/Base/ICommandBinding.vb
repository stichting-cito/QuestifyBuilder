
Namespace Commanding

    Public Interface ICommandBinding

        ReadOnly Property SourceType() As Type

        Sub Bind(command As ICommand, parameter As ParameterBinding, source As Object)

        Sub UpdateCommandState()

    End Interface

End Namespace

