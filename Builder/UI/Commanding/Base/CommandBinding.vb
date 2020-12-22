Imports System.ComponentModel

Namespace Commanding

    Public MustInherit Class CommandBinding(Of T As IComponent)
        Implements ICommandBinding


        Public ReadOnly Property SourceType() As Type Implements ICommandBinding.SourceType
            Get
                Return GetType(T)
            End Get
        End Property

        Public Sub Bind(command As ICommand, parameter As ParameterBinding, source As Object) Implements ICommandBinding.Bind
            Bind(command, parameter, DirectCast(source, T))
        End Sub

        Protected MustOverride Sub Bind(command As ICommand, source As T)

        Protected MustOverride Sub Bind(command As ICommand, parameter As ParameterBinding, source As T)

        Protected MustOverride Sub UpdateCommandState() Implements ICommandBinding.UpdateCommandState

    End Class

End Namespace

