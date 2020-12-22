
Namespace Commanding

    Public Class ButtonBaseCommandBinding(Of T As ButtonBase)
        Inherits CommandBinding(Of T)
        Implements IDisposable

        Private disposedValue As Boolean
        Private _commands As New List(Of ICommand)
        Private _sources As New List(Of ButtonBase)
        Private _parameter As New List(Of ParameterBinding)


        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub

        Protected Overloads Overrides Sub Bind(command As ICommand, source As T)
            AddBinding(command, New ControlTagParameterBinding, source)
        End Sub

        Protected Overloads Overrides Sub Bind(command As ICommand, parameter As ParameterBinding, source As T)
            AddBinding(command, parameter, source)
        End Sub


        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                End If

            End If
            Me.disposedValue = True
        End Sub

        Private Sub AddBinding(command As ICommand, parameter As ParameterBinding, source As T)
            _commands.Add(command)
            _sources.Add(source)
            _parameter.Add(parameter)

            AddHandler source.Click, AddressOf buttonBase_Click
            AddHandler command.CanExecuteChanged, AddressOf command_CanExecuteChanged

            source.Text = command.NameLocalized
            source.Image = command.Image
        End Sub

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

        Private Function GetCommandParameter(sender As Object) As Object
            Dim sourceIndex As Integer = _sources.IndexOf(CType(sender, ButtonBase))
            Dim paramBinding As ParameterBinding = _parameter(sourceIndex)

            Return paramBinding.GetCommandParameter(sender)
        End Function

        Private Sub command_CanExecuteChanged(sender As Object, e As EventArgs)
            For i As Integer = 0 To Me._commands.Count - 1
                Dim commandType As Type = DirectCast(Me._commands(i), Object).GetType
                If sender.GetType.Equals(commandType) Then
                    Dim parameter As Object = GetCommandParameter(Me._sources(i))
                    Me._sources(i).Enabled = _commands(i).CanExecute(parameter)
                End If
            Next
        End Sub

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

