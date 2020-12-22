
Imports System.Xml.Serialization

Public Class PublicationMetaData


    Private _publicationDateAndTime As DateTime
    Private _publicationGUID As Guid
    Private _publishedByUser As String
    Private _publishedWithApplication As String
    Private _publishedWithApplicationVersion As String



    Public Sub New()
        Me.PublicationGUID = Guid.NewGuid
    End Sub

    Public Sub New(publicationDateAndTime As DateTime, publishedByUser As String, publishedWithApplication As String, publishedWithApplicationVersion As String)
        Me.New()
        Me.PublicationDateAndTime = publicationDateAndTime
        Me.PublishedByUser = publishedByUser
        Me.PublishedWithApplication = publishedWithApplication
        Me.PublishedWithApplicationVersion = publishedWithApplicationVersion
    End Sub



    <XmlAttribute("publicationDateAndTime")> _
    Public Property PublicationDateAndTime() As DateTime
        Get
            Return _publicationDateAndTime
        End Get
        Set(value As DateTime)
            _publicationDateAndTime = value
        End Set
    End Property

    <XmlAttribute("publicationGUID")> _
    Public Property PublicationGUID() As Guid
        Get
            Return _publicationGUID
        End Get
        Set(value As Guid)
            _publicationGUID = value
        End Set
    End Property

    <XmlAttribute("publishedByUser")> _
    Public Property PublishedByUser() As String
        Get
            Return _publishedByUser
        End Get
        Set(value As String)
            _publishedByUser = value
        End Set
    End Property

    <XmlAttribute("publishedWithApplication")> _
    Public Property PublishedWithApplication() As String
        Get
            Return _publishedWithApplication
        End Get
        Set(value As String)
            _publishedWithApplication = value
        End Set
    End Property

    <XmlAttribute("publishedWithApplicationVersion")> _
    Public Property PublishedWithApplicationVersion() As String
        Get
            Return _publishedWithApplicationVersion
        End Get
        Set(value As String)
            _publishedWithApplicationVersion = value
        End Set
    End Property



    Public Shared Function CreatePublicationMetaData() As PublicationMetaData
        Dim metaDateToReturn As New PublicationMetaData(DateTime.Now, My.User.Name, My.Application.Info.ProductName, My.Application.Info.Version.ToString)
        Return metaDateToReturn
    End Function


End Class