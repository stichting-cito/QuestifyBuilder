
Namespace Commanding

    ''' <summary>
    ''' Allows binding of commands to a UI Component inherited from ButtonBase
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    Public Class ButtonBaseCommandBinding(Of T As ButtonBase)
        Inherits CommandBinding(Of T)
        Implements IDisposable

        Private disposedValue As Boolean 'To detect redundant calls
        Private _commands As New List(Of ICommand)
        Private _sources As New List(Of ButtonBase)
        Private _parameter As New List(Of ParameterBinding)


        ''' <summary>
        ''' Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        ''' </summary>
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub

        ''' <summary>
        ''' Binds the specified command.
        ''' </summary>
        ''' <param name="command">The command.</param>
        ''' <param name="source">The source.</param>
        Protected Overloads Overrides Sub Bind(command As ICommand, source As T)
            AddBinding(command, New ControlTagParameterBinding, source)
        End Sub

        ''' <summary>
        ''' Binds the specified command.
        ''' </summary>
        ''' <param name="command">The command.</param>
        ''' <param name="parameter">The parameter.</param>
        ''' <param name="source">The source.</param>
        Protected Overloads Overrides Sub Bind(command As ICommand, parameter As ParameterBinding, source As T)
            AddBinding(command, parameter, source)
        End Sub


        ''' <summary>
        ''' Releases unmanaged and - optionally - managed resources
        ''' </summary>
        ''' <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: dispose managed state (managed objects).
                End If
               
            End If
            Me.disposedValue = True
        End Sub

        ''' <summary>
        ''' Adds the binding.
        ''' </summary>
        ''' <param name="command">The command.</param>
        ''' <param name="source">The source.</param>
        Private Sub AddBinding(command As ICommand, parameter As ParameterBinding, source As T)
            _commands.Add(command)
            _sources.Add(source)
            _parameter.Add(parameter)

            AddHandler source.Click, AddressOf buttonBase_Click
            AddHandler command.CanExecuteChanged, AddressOf command_CanExecuteChanged

            source.Text = command.NameLocalized
            source.Image = command.Image
        End Sub

        ''' <summary>
        ''' Handles the Click event of the buttonBase control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub buttonBase_Click(sender As Object, e As EventArgs)
            If TypeOf sender Is ButtonBase Then
                Dim sourceIndex As Integer = _sources.IndexOf(CType(sender, ButtonBase))
                Dim linkedCommand As ICommand = _commands(sourceIndex)
                Dim parameter As Object = GetCommandParameter(sender)

                If linkedCommand.CanExecute(parameter) Then
                    linkedCommand.Execute(parameter)
                End If
            End If
        End Sub

        ''' <summary>
        ''' Gets the command parameter.
        ''' </summary>
        ''' <param name="sender">The sender.</param><returns></returns>
        Private Function GetCommandParameter(sender As Object) As Object
            Dim sourceIndex As Integer = _sources.IndexOf(CType(sender, ButtonBase))
            Dim paramBinding As ParameterBinding = _parameter(sourceIndex)

            Return paramBinding.GetCommandParameter(sender)
        End Function

        ''' <summary>
        ''' Handles the CanExecuteChanged event of the command control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub command_CanExecuteChanged(sender As Object, e As EventArgs)
            For i As Integer = 0 To Me._commands.Count - 1
                Dim commandType As Type = DirectCast(Me._commands(i), Object).GetType  ' vb.net workaround for [interface].gettype()
                If sender.GetType.Equals(commandType) Then
                    Dim parameter As Object = GetCommandParameter(Me._sources(i))
                    Me._sources(i).Enabled = _commands(i).CanExecute(parameter)
                End If
            Next
        End Sub

        ''' <summary>
        ''' Updates the state of the bound commands.
        ''' </summary>
        Protected Overrides Sub UpdateCommandState()
            For sourceIndex As Integer = 0 To Me._sources.Count - 1
                Dim source As Object = _sources(sourceIndex)
                Dim linkedCommand As ICommand = _commands(sourceIndex)
                Dim parameter As Object = GetCommandParameter(source)
                linkedCommand.CanExecute(parameter)
            Next
        End Sub

     

    End Class

End Namespace

