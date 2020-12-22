Imports System.Xml.Serialization
Imports System.Diagnostics.CodeAnalysis

<DebuggerDisplay("ResourceEntry {Name}")>
Public Class ResourceEntry

    <XmlAttribute("name")>
    Public Property Name() As String

    <XmlAttribute("originalname")>
    Public Property OriginalName() As String = String.Empty

    <XmlAttribute("originalversion")>
    Public Property OriginalVersion() As Integer = 0

    <XmlAttribute("version")>
    Public Property Version() As Integer = 1

    <XmlAttribute("uri")>
    Public Property Uri() As String

    <XmlAttribute("type")>
    Public Property Type() As String

    <XmlAttribute("cacheLocal")>
    Public Property CacheLocal() As Boolean

    <XmlElement("DependentResource", GetType(DependentResource))>
    Public ReadOnly Property DependentResources() As DependentResourceCollection

    <XmlElement("metaData", GetType(MetaData))>
    <XmlElement("metaDataMultiValue", GetType(MetaDataMultiValue))>
    Public ReadOnly Property MetaData() As New MetaDataCollection

    <XmlAttribute("state")>
    Public Property State As Integer

    Public Sub New()
        _DependentResources = New DependentResourceCollection
        _MetaData = New MetaDataCollection
    End Sub

    Public Sub New(name As String,
               version As Integer,
               resourceLocation As String,
               type As String,
               cacheLocal As Boolean,
               dependentResources As DependentResourceCollection)

        If String.IsNullOrEmpty(name) Then
            Throw New ResourceManagerException(My.Resources.Error_ResourceEntry_Constructor_Name_Empty)
        End If

        If String.IsNullOrEmpty(resourceLocation) Then
            Throw New ResourceManagerException(My.Resources.Error_ResourceEntry_Constructor_Uri_Empty)
        End If

        If String.IsNullOrEmpty(type) Then
            Throw New ResourceManagerException(My.Resources.Error_ResourceEntry_Constructor_Type_Empty)
        End If

        If dependentResources Is Nothing Then
            Throw New ResourceManagerException(My.Resources.Error_ResourceEntry_Constructor_DependentResources_Empty)
        End If

        Me.Name = name
        Me.Version = version
        Uri = resourceLocation
        Me.Type = type
        Me.CacheLocal = cacheLocal
        Me.DependentResources = dependentResources
    End Sub


    Public Sub New(name As String,
               version As Integer,
               resourceLocation As String,
               type As String,
               cacheLocal As Boolean,
               dependentResources As DependentResourceCollection,
               originalName As String,
               originalVersion As Integer,
               state As Integer)

        Me.New(name, version, resourceLocation, type, cacheLocal, dependentResources)

        Me.OriginalVersion = originalVersion
        Me.OriginalName = originalName
        Me.State = state
    End Sub


    Public Sub New(name As String)
        Me.New(name, 1)
    End Sub

    Public Sub New(name As String, version As Integer)
        If String.IsNullOrEmpty(name) Then
            Throw New ResourceManagerException(My.Resources.Error_ResourceEntry_Constructor_Name_Empty)
        End If
        Me.Version = version
        Me.Name = name
        Uri = ""
        Type = ""
        DependentResources = New DependentResourceCollection
    End Sub
End Class
