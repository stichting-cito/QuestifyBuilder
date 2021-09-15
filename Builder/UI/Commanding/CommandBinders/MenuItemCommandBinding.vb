
Imports System
Imports System.Linq
Imports System.Windows.Threading

Namespace Commanding

    ''' <summary>
    ''' Allows binding of commands to a toolstripitem (menu, toolbar button, etc.)
    ''' </summary>
    Public Class MenuItemCommandBinding
        Inherits CommandBinding(Of ToolStripItem)

        Private ReadOnly _commands As New Dictionary(Of ICommand, List(Of String))
        Private ReadOnly _parameters As New Dictionary(Of ParameterBinding, List(Of String))
        Private ReadOnly _sources As New List(Of ToolStripItem)
        Private _currentClickedToolStripItem As ToolStripItem
        Private _isWaiting As Boolean = False
        Private ReadOnly _clickWaitTimer As New DispatcherTimer(New TimeSpan(0, 0, 0, 0, 500), DispatcherPriority.Background, Sub() WaitTimer_Tick(Nothing, Nothing), Dispatcher.CurrentDispatcher)


        ''' <summary>
        ''' Binds the specified command.
        ''' </summary>
        ''' <param name="command">The command.</param>
        ''' <param name="source">The source.</param>
        Protected Overrides Sub Bind(command As ICommand, source As ToolStripItem)
            AddBinding(command, New ControlTagParameterBinding(), source)
        End Sub

        ''' <summary>
        ''' Binds the specified command.
        ''' </summary>
        ''' <param name="command">The command.</param>
        ''' <param name="parameter">The parameter.</param>
        ''' <param name="source">The source.</param>
        Protected Overloads Overrides Sub Bind(command As ICommand, parameter As ParameterBinding, source As ToolStripItem)
            AddBinding(command, parameter, source)
        End Sub

        ''' <summary>
        ''' Adds the binding.
        ''' </summary>
        ''' <param name="command">The command.</param>
        ''' <param name="source">The source.</param>
        Private Sub AddBinding(command As ICommand, parameter As ParameterBinding, source As ToolStripItem)
            If _sources.Any(Function(s) s.Name.Equals(source.Name, StringComparison.OrdinalIgnoreCase)) Then
                removeBinding(source.Name)
            End If

            _sources.Add(source)
            AddHandler source.Click, AddressOf menuitem_Click

            Dim listOfSources As New List(Of String)

            If _commands.ContainsKey(command) Then
                If Not _commands(command).Contains(source.Name, StringComparer.OrdinalIgnoreCase) Then _commands(command).Add(source.Name)
            Else
                listOfSources.Add(source.Name)
                _commands.Add(command, listOfSources)
                AddHandler command.CanExecuteChanged, AddressOf command_CanExecuteChanged
            End If

            If _parameters.ContainsKey(parameter) Then
                If Not _parameters(parameter).Contains(source.Name, StringComparer.OrdinalIgnoreCase) Then _parameters(parameter).Add(source.Name)
            Else
                listOfSources.Add(source.Name)
                _parameters.Add(parameter, listOfSources)
            End If

            source.Text = command.NameLocalized
            'Only set image when command has an image.
            If (command.Image IsNot Nothing) Then source.Image = command.Image
        End Sub

        Private Sub WaitTimer_Tick(sender As Object, e As EventArgs)
            _clickWaitTimer.Stop()
            _isWaiting = False
            ' Handle Single Click Actions
            ClickHandler(_currentClickedToolStripItem)
        End Sub

        ''' <summary>
        ''' Handles the CanExecuteChanged event of the command control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub command_CanExecuteChanged(sender As Object, e As EventArgs)
            _commands.Where(Function(c) c.Key IsNot Nothing AndAlso sender.Equals(c.Key)).ToList() _
            .ForEach(Sub(c)
                           c.Value.ForEach(Sub(cs)
                                               Dim source As ToolStripItem = _sources.FirstOrDefault(Function(s) s.Name.Equals(cs, StringComparison.OrdinalIgnoreCase))
                                               If source IsNot Nothing Then
                                                   Dim parameter As Object = GetCommandParameter(source)
                                                   source.Enabled = c.Key.CanExecute(parameter)
                                               End If
                                           End Sub)
                       End Sub)
        End Sub

        ''' <summary>
        ''' Updates the state of the bound commands.
        ''' </summary>
        Protected Overrides Sub UpdateCommandState()
            Dim temp As Boolean
            _commands.Where(Function(c) c.Key IsNot Nothing AndAlso c.Value IsNot Nothing AndAlso c.Value.Count > 0).ToList() _
                .ForEach(Sub(c)
                              Dim source As ToolStripItem = _sources.FirstOrDefault(Function(s) s.Name.Equals(c.Value(0), StringComparison.OrdinalIgnoreCase))
                              If source IsNot Nothing Then
                                  Dim parameter As Object = GetCommandParameter(source)
                                  temp = c.Key.CanExecute(parameter)
                              End If
                          End Sub)
        End Sub

        ''' <summary>
        ''' Handles the Click event of the menuitem control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub menuitem_Click(sender As Object, e As EventArgs)
            If Not _isWaiting Then
                _isWaiting = True
                _currentClickedToolStripItem = TryCast(sender, ToolStripItem)
                _clickWaitTimer.Start()
            Else
                'Handle DoubleClick
                _isWaiting = False
                _clickWaitTimer.Stop()
                ClickHandler(_currentClickedToolStripItem, True)
            End If
        End Sub

        Private Sub ClickHandler(toolStripItem As ToolStripItem, Optional executeDoubleClick As Boolean = False)
            If toolStripItem Is Nothing Then
                Return
            End If

            Dim source As ToolStripItem = _sources.FirstOrDefault(Function(s) s.Name.Equals(toolStripItem.Name, StringComparison.OrdinalIgnoreCase))
            If source Is Nothing Then
                Return
            End If

            Dim parameter As Object = GetCommandParameter(source)
            Dim linkedCommand As ICommand = _commands.FirstOrDefault(Function(c) c.Value.Contains(source.Name, StringComparer.OrdinalIgnoreCase)).Key
            If Not linkedCommand.CanExecute(parameter) Then
                Return
            End If

            If Not executeDoubleClick Then
                linkedCommand.Execute(parameter)
            Else
                linkedCommand.ExecuteDoubleClick(parameter)
            End If
        End Sub

        ''' <summary>
        ''' Gets the command parameter.
        ''' </summary>
        ''' <param name="sender">The sender.</param><returns></returns>
        Private Function GetCommandParameter(sender As Object) As Object
            Dim paramBinding As ParameterBinding = _parameters.FirstOrDefault(Function(p) p.Value.Contains(DirectCast(sender, ToolStripItem).Name, StringComparer.OrdinalIgnoreCase)).Key
            Return If(paramBinding Is Nothing, Nothing, paramBinding.GetCommandParameter(sender))
        End Function

        Private Sub RemoveBinding(sourceName As String)

            _sources.Where(Function(s) s.Name.Equals(sourceName, StringComparison.OrdinalIgnoreCase)).ToList.ForEach(Sub(s)
                                                                                                                         _parameters.Where(Function(p) p.Value.Contains(s.Name, StringComparer.OrdinalIgnoreCase)).ToList.ForEach(Sub(p)
                                                                                                                                                                                                                                      p.Value.Remove(s.Name)
                                                                                                                                                                                                                                  End Sub)

                                                                                                                         _commands.Where(Function(c) c.Value.Contains(s.Name, StringComparer.OrdinalIgnoreCase)).ToList.ForEach(Sub(c)
                                                                                                                                                                                                                                    c.Value.Remove(s.Name)
                                                                                                                                                                                                                                End Sub)

                                                                                                                         RemoveHandler s.Click, AddressOf menuitem_Click
                                                                                                                         _sources.Remove(s)
                                                                                                                     End Sub)

            Dim prmsToRemove As New List(Of ParameterBinding)
            Dim cmdsToRemove As New List(Of ICommand)

            _parameters.Where(Function(p) p.Value Is Nothing OrElse p.Value.Count = 0).ToList.ForEach(Sub(p) prmsToRemove.Add(p.Key))
            _commands.Where(Function(c) c.Value Is Nothing OrElse c.Value.Count = 0).ToList.ForEach(Sub(c) cmdsToRemove.Add(c.Key))

            prmsToRemove.ForEach(Sub(p)
                                     _parameters.Remove(p)
                                 End Sub)

            cmdsToRemove.ForEach(Sub(c)
                                     RemoveHandler c.CanExecuteChanged, AddressOf command_CanExecuteChanged
                                     _commands.Remove(c)
                                 End Sub)

        End Sub
        
    End Class

End Namespace

