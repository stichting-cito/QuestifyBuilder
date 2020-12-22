Imports System.Xml
Imports System.Xml.Serialization
Imports System.Collections.Generic
Namespace Versioning
    <Serializable()>
    <Serialization.XmlRoot(ElementName:="CustomBankProperty")> _
    Public Class CustomBankPropertyMetaData
        Inherits MetaDataBase

        Private _values As List(Of String)

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal id As Guid, ByVal name As String, ByVal values As List(Of String), ByVal version As String)
            MyBase.New(id, name, version)

            _values = values
        End Sub

        Public Sub New(ByVal id As Guid, ByVal name As String, ByVal value As String, ByVal version As String)
            Me.New(id, name, New List(Of String)(), version)

            Values.Add(value)
        End Sub

        Public Property Values As List(Of String)
            Get
                Return _values
            End Get
            Set(value As List(Of String))
                _values = value
            End Set
        End Property

        <XmlIgnore()> _
        Public Overrides ReadOnly Property Category As String
            Get
                Return My.Resources.Category_CustomBankProperties
            End Get
        End Property
    End Class
End Namespace