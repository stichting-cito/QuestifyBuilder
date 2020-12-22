

Namespace Commanding

    Public Class SelectedNodeTagParameterBinding
        Inherits ParameterBinding


        Private _tree As TreeView



        Public Sub New(tree As TreeView)
            If tree Is Nothing Then
                Throw New ArgumentNullException("tree")
            End If
            Me.Tree = tree
        End Sub



        Public Property Tree() As TreeView
            Get
                Return _tree
            End Get
            Set(value As TreeView)
                _tree = value
            End Set
        End Property



        Public Overrides Function GetCommandParameter(source As Object) As Object
            If Tree.SelectedNode IsNot Nothing Then
                Return Tree.SelectedNode.Tag
            Else
                Return Nothing
            End If
        End Function


    End Class

End Namespace

