
Namespace Commanding

    Public Interface ICommandBinding

        ''' <summary>
        ''' Gets the type of the source.
        ''' </summary>
        ''' <value>
        ''' The type of the source.
        ''' </value>
        ReadOnly Property SourceType() As Type

        ''' <summary>
        ''' Binds the specified command.
        ''' </summary>
        ''' <param name="command">The command.</param>
        ''' <param name="parameter">The parameter.</param>
        ''' <param name="source">The source.</param>
        Sub Bind(command As ICommand, parameter As ParameterBinding, source As Object)

        Sub UpdateCommandState()

    End Interface

End Namespace

