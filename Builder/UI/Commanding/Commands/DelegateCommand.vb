

Namespace Commanding

    Public Class DelegateCommand(Of T)
        Inherits CommandBase


        Private _canExecute As Predicate(Of T)
        Private _canExecuteValue As Boolean = True
        Private _execute As Action(Of Object)
        Private _executeDoubleClick As Action(Of Object)
        Private _image As Image = Nothing
        Private _name As String = String.Empty
        Private _nameLocalized As String = String.Empty



        Public Sub New(name As String, execute As Action(Of Object))
            _name = name
            _execute = execute
        End Sub

        Public Sub New(name As String, execute As Action(Of Object), canExecute As Predicate(Of T))
            Me.New(name, execute)
            If (canExecute Is Nothing) Then Throw New ArgumentNullException("canExecute")
            _canExecute = canExecute
        End Sub

        Public Sub New(name As String, execute As Action(Of Object), executeDoubleClick As Action(Of Object), canExecute As Predicate(Of T))
            Me.New(name, execute)
            _executeDoubleClick = executeDoubleClick
            If (canExecute Is Nothing) Then Throw New ArgumentNullException("canExecute")
            _canExecute = canExecute
        End Sub

        Public Sub New(name As String, nameLocalized As String, image As Image, execute As Action(Of Object), canExecute As Predicate(Of T))
            Me.New(name, execute, canExecute)
            Me._nameLocalized = nameLocalized
            Me._image = image
        End Sub



        Public Overrides ReadOnly Property Image() As Image
            Get
                Return Me._image
            End Get
        End Property

        Public Overrides ReadOnly Property Name() As String
            Get
                Return _name
            End Get
        End Property

        Public Overrides ReadOnly Property NameLocalized() As String
            Get
                If String.IsNullOrEmpty(_nameLocalized) Then
                    Return Me.Name
                Else
                    Return _nameLocalized
                End If
            End Get
        End Property



        Public Overrides Sub Execute(parameter As Object)
            _execute(parameter)
        End Sub

        Public Overrides Sub ExecuteDoubleClick(parameter As Object)
            If _executeDoubleClick IsNot Nothing Then _executeDoubleClick(parameter)
        End Sub

        Protected Overloads Overrides Function GetCanExecuteState(parameter As Object) As Boolean
            If (_canExecute Is Nothing) Then
                Return True
            Else
                Return _canExecute(parameter)
            End If
        End Function


    End Class

End Namespace

