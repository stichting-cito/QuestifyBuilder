

Namespace Commanding

    Public Interface ICommand


        Event CanExecuteChanged As EventHandler



        ReadOnly Property Image() As Image

        ReadOnly Property Name() As String

        ReadOnly Property NameLocalized() As String



        Sub Execute(parameter As Object)

        Sub ExecuteDoubleClick(parameter As Object)

        Function CanExecute(parameter As Object) As Boolean


    End Interface

End Namespace

