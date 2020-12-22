Imports System.Xml
Imports System.Xml.Serialization
Namespace Versioning
    <Serializable()>
    <Serialization.XmlRoot(ElementName:="PropertyEntity")> _
    Public Class PropertyEntityMetaData
        Inherits MetaDataBase

        Private _value As Object

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal id As Guid, ByVal name As String, ByVal value As Object, ByVal version As String)
            Me.New(id, name, version)

            _value = value
        End Sub

        Private Sub New(ByVal id As Guid, ByVal name As String, ByVal version As String)
            MyBase.New(id, name, version)
        End Sub

        Public Property Value As Object
            Get
                Return _value
            End Get
            Set(value As Object)
                _value = value
            End Set
        End Property

        <XmlIgnore()> _
        Public Overrides ReadOnly Property Category As String
            Get
                Return My.Resources.Category_PropertyEntity
            End Get
        End Property


    End Class
End Namespace