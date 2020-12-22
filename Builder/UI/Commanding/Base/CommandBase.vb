Namespace Commanding

    Public MustInherit Class CommandBase
        Implements ICommand

        Private _canExecute As Nullable(Of Boolean)

        Public Event CanExecuteChanged(sender As Object, e As EventArgs) Implements ICommand.CanExecuteChanged

        Public MustOverride ReadOnly Property Image() As Image Implements ICommand.Image

        Public MustOverride ReadOnly Property Name() As String Implements ICommand.Name

        Public MustOverride ReadOnly Property NameLocalized() As String Implements ICommand.NameLocalized

        Public Function CanExecute(parameter As Object) As Boolean Implements ICommand.CanExecute
            Dim current As Boolean = GetCanExecuteState(parameter)

            If Not _canExecute.HasValue OrElse _canExecute.Value <> current Then
                _canExecute = current
                RaiseEvent CanExecuteChanged(Me, New EventArgs)
            End If

            Return _canExecute.Value
        End Function

        Public MustOverride Sub Execute(parameter As Object) Implements ICommand.Execute

        Public Overridable Sub ExecuteDoubleClick(parameter As Object) Implements ICommand.ExecuteDoubleClick
        End Sub

        Protected MustOverride Function GetCanExecuteState(parameter As Object) As Boolean

    End Class

End Namespace

