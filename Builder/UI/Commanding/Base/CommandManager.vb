
Imports System.ComponentModel

Namespace Commanding

    Public Class CommandManager

        Public Delegate Function GetCommandParameter(context As Object) As Object

        Private ReadOnly _binders As IList(Of ICommandBinding) = New List(Of ICommandBinding)
        Private ReadOnly _commands As IList(Of ICommand) = New List(Of ICommand)

        Public Sub New()
            MyBase.New()

            InitializeComponent()

            Binders.Add(New ButtonBaseCommandBinding(Of Button)())
            Binders.Add(New MenuItemCommandBinding())

        End Sub

        Public ReadOnly Property Binders() As IList(Of ICommandBinding)
            Get
                Return _binders
            End Get
        End Property

        Private ReadOnly Property Commands() As IList(Of ICommand)
            Get
                Return _commands
            End Get
        End Property

        Public Function Bind(command As ICommand, component As IComponent) As CommandManager
            If Not Commands.Contains(command) Then
                Commands.Add(command)
            End If

            FindBinder(component).Bind(command, New NullParameterBinding, component)

            Return Me
        End Function

        Public Function Bind(command As ICommand, parameter As ParameterBinding, component As IComponent) As CommandManager
            If Not Commands.Contains(command) Then
                Commands.Add(command)
            End If

            FindBinder(component).Bind(command, parameter, component)

            Return Me
        End Function


        Protected Function FindBinder(component As IComponent) As ICommandBinding
            Dim binder As ICommandBinding = GetBinderFor(component)

            If binder Is Nothing Then
                Dim componentType As Type = DirectCast(component, Object).GetType
                Throw New Exception(String.Format("No binding found for component of type {0}", componentType.Name))
            End If

            Return binder
        End Function

        Private Function GetBinderFor(component As IComponent) As ICommandBinding
            Dim componentType As Type = DirectCast(component, Object).GetType

            While componentType IsNot Nothing
                Dim binder As ICommandBinding = Nothing

                For Each available As ICommandBinding In Binders
                    If componentType.Equals(available.SourceType) Then
                        binder = available
                        Exit For
                    End If
                Next

                If binder IsNot Nothing Then
                    Return binder
                End If

                componentType = componentType.BaseType
            End While

            Return Nothing
        End Function

        Public Sub UpdateCommandState(sender As Object, e As EventArgs)
            If Not Me.DesignMode Then
                For Each b As ICommandBinding In Me._binders
                    b.UpdateCommandState()
                Next
            End If
        End Sub

    End Class

End Namespace

