
Imports System.Xml.Serialization
Imports Cito.Tester.Common

<Serializable()> _
Public Class PackageSet


    Private _packageSetRoot As Uri

    Private _packageSetEntryCollection As New PackageSetEntryCollection



    <XmlElement("PackageSetEntryCollection")> _
    Public ReadOnly Property PackageSetEntryCollection() As PackageSetEntryCollection
        Get
            Return _packageSetEntryCollection
        End Get
    End Property

    <XmlIgnore()> _
    Public Property PackageSetRoot() As Uri
        Get
            Return _packageSetRoot
        End Get
        Friend Set(value As Uri)
            _packageSetRoot = value
        End Set
    End Property



    Public Shared Function LoadFromFile(path As String) As PackageSet
        Dim packageSetToReturn As PackageSet = CType(SerializeHelper.XmlDeserializeFromFile(path, GetType(PackageSet)), PackageSet)
        packageSetToReturn.PackageSetRoot = New Uri(IO.Path.GetDirectoryName(path) + "/")
        Return packageSetToReturn
    End Function

    Public Shared Sub SaveToFile(path As String, packageSetObject As PackageSet)
        SerializeHelper.XmlSerializeToFile(path, packageSetObject)
    End Sub


End Class