Imports System.IO
Imports System.Xml

Namespace QTI.Helpers

    Public Class XmlStreamResolver
        Inherits XmlUrlResolver

        Private ReadOnly _resources As Dictionary(Of String, Byte())

        Public Sub New(ByVal resources As Dictionary(Of String, Byte()))
            MyBase.New()

            _resources = resources
        End Sub

        Public Overrides Function GetEntity(absoluteUri As Uri, role As String, ofObjectToReturn As Type) As Object
            Return New MemoryStream(_resources(Path.GetFileName(absoluteUri.LocalPath)))
        End Function

    End Class
End NameSpace