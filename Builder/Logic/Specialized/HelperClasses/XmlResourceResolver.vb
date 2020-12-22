Imports System.Text
Imports System.Xml

Public Class XmlResourceResolver
    Inherits XmlUrlResolver

    Public Overrides Function GetEntity(absoluteUri As Uri, role As String, ofObjectToReturn As Type) As Object
        Dim entityName As String = IO.Path.GetFileNameWithoutExtension(absoluteUri.LocalPath)
        Return New IO.MemoryStream(ASCIIEncoding.UTF8.GetBytes(My.Resources.ResourceManager.GetObject(entityName).ToString))
    End Function
End Class
