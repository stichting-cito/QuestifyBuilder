Imports System.Collections.Generic

Namespace Questify.Builder.Model.ContentModel.ResourceProperties

    Public Class ResourcePropertyListValueDefinition


        Private _key As Guid
        Private _name As String
        Private _title As String
        Private _children As List(Of ResourcePropertyListValueDefinition)
        Private _parent As Guid?



        Public Sub New()
            _children = New List(Of ResourcePropertyListValueDefinition)
        End Sub

        Public Sub New(ByVal key As Guid, ByVal name As String, ByVal title As String)
            _key = key
            _name = name
            _title = title
            _children = New List(Of ResourcePropertyListValueDefinition)
        End Sub



        Public ReadOnly Property Key() As Guid
            Get
                Return _key
            End Get
        End Property

        Public ReadOnly Property Name() As String
            Get
                Return _name
            End Get
        End Property

        Public Property Title() As String
            Get
                Return _title
            End Get
            Set(ByVal value As String)
                _title = value
            End Set
        End Property

        Public Property Parent() As Guid?
            Get
                Return _parent
            End Get
            Set(ByVal value As Guid?)
                _parent = value
            End Set
        End Property

        Public Property Children As List(Of ResourcePropertyListValueDefinition)
            Get
                Return _children
            End Get
            Set(ByVal value As List(Of ResourcePropertyListValueDefinition))
                _children = value
            End Set
        End Property


    End Class
End Namespace
