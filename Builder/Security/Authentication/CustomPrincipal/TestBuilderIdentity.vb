Imports System.Xml.Serialization
Imports System.Security.Principal

<XmlRoot("TestBuilderIdentity", [Namespace]:="http://Cito.TestBuilder.Security/xml/serialization", IsNullable:=True), _
 Serializable()> _
Public Class TestBuilderIdentity
    Inherits GenericIdentity

    Private _userId As Integer
    Private _userName As String


    Public Property UserId() As Integer
        Get
            Return _userId
        End Get
        Set(ByVal value As Integer)
            _userId = value
        End Set
    End Property

    Public Property UserName() As String
        Get
            Return _userName
        End Get
        Set(ByVal value As String)
            _userName = value
        End Set
    End Property

    Public Overrides ReadOnly Property IsAuthenticated() As Boolean
        Get
            Return (_userId > 0)
        End Get
    End Property

    Public Overrides ReadOnly Property Name() As String
        Get
            Return _userName
        End Get
    End Property



    Public Sub New(ByVal userId As Integer, ByVal userName As String, ByVal type As String)
        MyBase.New(String.Empty, type)

        _userId = userId
        _userName = userName
    End Sub

    Public Sub New(ByVal type As String)
        MyBase.New(String.Empty, type)
    End Sub

    Public Sub New()
        MyBase.New(String.Empty, "Webservice")
    End Sub


End Class

