Imports System.Diagnostics.CodeAnalysis
Imports System.Xml
Imports System.IO


<SuppressMessage("Microsoft.Design", "CA1058:TypesShouldNotExtendCertainBaseTypes", MessageId:="System.Xml.XmlDocument"), _
SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix"), _
SuppressMessage("Microsoft.Design", "CA1010:CollectionsShouldImplementGenericInterface")> _
Public Class XHtmlDocument
    Inherits XmlDocument


    Const Xhtml11 As String = "xhtml11-flat.dtd"
    Const XhtmlStrict As String = "xhtml1-strict.dtd"
    Const XhtmlTransitional As String = "xhtml1-transitional.dtd"
    Const XhtmlLat1 As String = "xhtml-lat1.ent"
    Const XhtmlSpecial As String = "xhtml-special.ent"
    Const XhtmlSymbol As String = "xhtml-symbol.ent"

    Const Html4Loose As String = "loose.dtd"
    Const Html4Strict As String = "strict.dtd"
    Const Html4Frameset As String = "frameset.dtd"
    Const Html4Lat1 As String = "HTMLlat1.ent"
    Const Html4Special As String = "HTMLspecial.ent"
    Const Html4Symbol As String = "HTMLsymbol.ent"



    Public Overrides Sub LoadXml(xml As String)
        Try
            If Not String.IsNullOrEmpty(xml) Then
                Me.LoadXml(xml, False)
            Else
                Throw New TesterException(My.Resources.Error_XHtmlDocument_LoadXML_DocumentIsNothing)
            End If
        Catch ex As Exception
            Throw New TesterException(My.Resources.Error_XHtmlDocument_LoadXML_CannotLoadXML, ex)
        Finally
            xml = Nothing
        End Try
    End Sub


    <SuppressMessage("Microsoft.Reliability", "CA2000:DisposeObjectsBeforeLosingScope")> _
    Public Overloads Sub LoadXml(xml As String, useDefaultDocType As Boolean)
        Dim ex As Exception = Nothing
        If Not TryLoadXml(xml, useDefaultDocType, ex) Then
            xml = StringManipulationHelper.FixEncodingXmlString(xml)
            If Not TryLoadXml(xml, useDefaultDocType, ex) AndAlso ex IsNot Nothing Then
                Throw New TesterException(My.Resources.Error_XHtmlDocument_LoadXML_CannotLoadXML, ex)
            End If
        End If
    End Sub

    Private Function TryLoadXml(xml As String, useDefaultDocType As Boolean, ByRef e As Exception) As Boolean
        Dim returnValue As Boolean = True
        Dim rdr As XmlReader = Nothing
        Dim stringReader As StringReader = Nothing
        Try
            Dim xhtmlContext As XmlParserContext = CreateXhtmlContext(useDefaultDocType)
            Dim settings As New XmlReaderSettings
            settings.ValidationType = ValidationType.None
            settings.XmlResolver = CreateXhtmlResolver()
            settings.ProhibitDtd = False

            stringReader = New StringReader(xml)
            rdr = XmlReader.Create(stringReader, settings, xhtmlContext)
            If rdr IsNot Nothing Then
                Me.Load(rdr)
            End If
            If Me Is Nothing OrElse (String.IsNullOrEmpty(Me.OuterXml.Trim) AndAlso Not String.IsNullOrEmpty(xml.Trim)) Then
                returnValue = False
            End If
        Catch ex As Exception
            e = ex
            returnValue = False
        Finally
            If rdr IsNot Nothing Then
                rdr.Close()
                rdr = Nothing
            End If
            If stringReader IsNot Nothing Then
                stringReader.Close()
                stringReader.Dispose()
                stringReader = Nothing
            End If
        End Try
        Return returnValue
    End Function




    Private Shared Function CreateXhtmlContext(useDefaultDocType As Boolean) As XmlParserContext
        Dim nt As XmlNameTable = New NameTable()
        Dim nsmgr As XmlNamespaceManager = New XmlNamespaceManager(nt)
        Dim context As XmlParserContext = New XmlParserContext(Nothing, nsmgr, Nothing, XmlSpace.None)

        If useDefaultDocType Then
            context.DocTypeName = "html"
            context.PublicId = "-//W3C//DTD XHTML 1.0 Strict//EN"
            context.SystemId = "xhtml1-strict.dtd"
        End If

        Return context
    End Function


    Private Shared Function CreateXhtmlResolver() As XmlResolver
        Dim resolver As KnownResourceXmlResolver = New KnownResourceXmlResolver()

        resolver.AddResource(New Uri("urn:" + "-//W3C//DTD XHTML 1.0 Strict//EN"), GetType(XHtmlDocument), XhtmlStrict)

        resolver.AddResource(New Uri("urn:" + XhtmlStrict), GetType(XHtmlDocument), XhtmlStrict)
        resolver.AddResource(New Uri("urn:" + XhtmlLat1), GetType(XHtmlDocument), XhtmlLat1)
        resolver.AddResource(New Uri("urn:" + XhtmlSpecial), GetType(XHtmlDocument), XhtmlSpecial)
        resolver.AddResource(New Uri("urn:" + XhtmlSymbol), GetType(XHtmlDocument), XhtmlSymbol)

        resolver.AddResource(New Uri("urn:" + "-//W3C//DTD XHTML 1.0 Transitional//EN"), GetType(XHtmlDocument), XhtmlTransitional)

        resolver.AddResource(New Uri("urn:" + "-//W3C//DTD XHTML 1.1//EN"), GetType(XHtmlDocument), Xhtml11)

        resolver.AddResource(New Uri("urn:" + "-//W3C//DTD HTML 4.0 Transitional//EN"), GetType(XHtmlDocument), Html4Loose)
        resolver.AddResource(New Uri("urn:" + "-//W3C//DTD HTML 4.01//EN"), GetType(XHtmlDocument), Html4Strict)
        resolver.AddResource(New Uri("urn:" + "-//W3C//DTD HTML 4.01 Frameset//EN"), GetType(XHtmlDocument), Html4Frameset)


        resolver.AddResource(New Uri("urn:" + Html4Lat1), GetType(XHtmlDocument), Html4Lat1)
        resolver.AddResource(New Uri("urn:" + Html4Special), GetType(XHtmlDocument), Html4Special)
        resolver.AddResource(New Uri("urn:" + Html4Symbol), GetType(XHtmlDocument), Html4Symbol)

        Return resolver
    End Function


    Private Shared Function CreateXhtmlNamespaceManager() As XmlNamespaceManager
        Dim nsMgr As XmlNamespaceManager = New XmlNamespaceManager(New NameTable)
        nsMgr.AddNamespace("xhtml", "http://www.w3.org/1999/xhtml")
        Return nsMgr
    End Function



    Public Sub New()
    End Sub


End Class
