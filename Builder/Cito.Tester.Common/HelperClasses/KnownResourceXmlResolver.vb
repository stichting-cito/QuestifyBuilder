Imports System.Xml
Imports System.IO
Imports System.Net

Public NotInheritable Class KnownResourceXmlResolver
    Inherits XmlResolver
    Private _knownResources As ArrayList = New ArrayList

    Public Sub New()
    End Sub

    Public Sub AddResource(xmlUri As Uri, resourceNamespaceType As Type, resourceName As String)
        If xmlUri Is Nothing Then
            Throw New ArgumentNullException("xmlUri")
        End If
        If resourceNamespaceType Is Nothing Then
            Throw New ArgumentNullException("resourceNamespaceType")
        End If
        If resourceName Is Nothing Then
            Throw New ArgumentNullException("resourceName")
        End If
        _knownResources.Add(New XmlResource(xmlUri, resourceNamespaceType, resourceName))
    End Sub

    Public Overloads Overrides WriteOnly Property Credentials() As ICredentials
        Set(value As ICredentials)
            Throw New NotSupportedException
        End Set
    End Property


    Public Overloads Overrides Function GetEntity(absoluteUri As Uri, role As String, ofObjectToReturn As Type) As Object
        If absoluteUri IsNot Nothing Then
            For Each resource As XmlResource In Me._knownResources
                If resource.XmlUri.Equals(absoluteUri) Then
                    Trace.WriteLine(String.Format(My.Resources.Trace_XHtmlDocument_GetEntity_StreamReturnedSuccesfully, absoluteUri.ToString))
                    Return resource.GetResourceStream
                End If
            Next
            Trace.WriteLine(String.Format(My.Resources.Trace_XHtmlDocument_GetEntity_NoEntityAvailable, absoluteUri.ToString))
            Return Nothing
        Else
            Throw New TesterException(My.Resources.Error_XHtmlDocument_GetEntity_CannotGetEntity)
        End If
    End Function

    Public Overloads Overrides Function ResolveUri(baseUri As Uri, relativeUri As String) As Uri
        If Not String.IsNullOrEmpty(relativeUri) Then
            For Each resource As XmlResource In Me._knownResources
                Dim relUri As Uri
                If relativeUri.Contains("urn:") Then
                    relUri = New Uri(relativeUri)
                Else
                    relUri = New Uri("urn:" + relativeUri)
                End If

                If resource.XmlUri.AbsolutePath.EndsWith(relUri.AbsolutePath) Then
                    Trace.WriteLine(String.Format(My.Resources.Trace_XHtmlDocument_ResolveUri_ResolvedSuccessFully, relativeUri))
                    Return resource.XmlUri
                End If
            Next
            Trace.WriteLine(String.Format(My.Resources.Trace_XHtmlDocument_ResolveUri_Unresolved, relativeUri))
        Else
            Throw New TesterException(My.Resources.Error_XHtmlDocument_ResolveUri_URIisNothing)
        End If

        Return Nothing
    End Function

    Private Structure XmlResource
        Public ReadOnly XmlUri As Uri
        Private ReadOnly NamespaceType As Type
        Private ReadOnly ResourceName As String

        Public Sub New(xmlUri As Uri, namespaceType As Type, resourceName As String)
            Me.XmlUri = xmlUri
            Me.NamespaceType = namespaceType
            Me.ResourceName = resourceName
        End Sub


        Public Function GetResourceStream() As Stream
            Return Me.NamespaceType.Assembly.GetManifestResourceStream(Me.NamespaceType, Me.ResourceName)
        End Function
    End Structure
End Class

