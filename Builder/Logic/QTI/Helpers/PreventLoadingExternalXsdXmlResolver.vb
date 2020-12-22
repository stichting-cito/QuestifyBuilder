Imports System.Xml

Namespace QTI.Helpers

    Public Class PreventLoadingExternalXsdXmlResolver
        Inherits XmlUrlResolver

        Public Overrides Function GetEntity(absoluteUri As Uri, role As String, ofObjectToReturn As Type) As Object
            If Not absoluteUri.Scheme = "http" AndAlso Not absoluteUri.Scheme = "https" Then
                Return MyBase.GetEntity(absoluteUri, role, ofObjectToReturn)
            Else
                Return Nothing
            End If
        End Function
    End Class
End NameSpace