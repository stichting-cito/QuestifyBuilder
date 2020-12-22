Imports System.Collections.Generic
Imports System.Linq
Imports System.Xml.Serialization

Namespace Versioning

    <Serializable()>
    Public Class TreeStructureMetaData
        Inherits MetaDataBase

        Private _values As List(Of String)

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal id As Guid, ByVal name As String, ByVal version As String)
            MyBase.New(id, name, version)
        End Sub

        Public Sub New(ByVal id As Guid, ByVal name As String, ByVal values As List(Of String), ByVal version As String)
            MyBase.New(id, name, version)
            _values = values
        End Sub

        Public Property Values As List(Of String)
            Get
                Return _values
            End Get
            Set(value As List(Of String))
                _values = value
            End Set
        End Property

        <XmlIgnore()>
        Default Public ReadOnly Property Item(ByVal code As Guid) As String
            Get
                Dim value = Values.FirstOrDefault(Function(v) v.StartsWith(code.ToString()))
                Return If(value IsNot Nothing, value.Substring(37), String.Empty)
            End Get
        End Property

        <XmlIgnore()>
        Default Public ReadOnly Property Item(ByVal index As Integer) As Guid
            Get
                Dim result As Guid
                If Values.Count > index AndAlso Guid.TryParse(Values(index).Substring(0, 36), result) Then
                    Return result
                Else
                    Return Guid.Empty
                End If
            End Get
        End Property

        <XmlIgnore()>
        Public ReadOnly Property ConcatenatedValues As String
            Get
                Return String.Join(",", Values.Select(Function(v) v.Substring(37)))
            End Get
        End Property

        <XmlIgnore()> _
        Public Overrides ReadOnly Property Category As String
            Get
                Return My.Resources.Category_ConceptStructure
            End Get
        End Property
    End Class

End Namespace