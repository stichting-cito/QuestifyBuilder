Imports System.Collections.Concurrent
Imports System.IO
Imports System.Linq
Imports System.Xml
Imports Questify.Builder.Logic.QTI.Helpers
Imports Questify.Builder.Logic.QTI.Helpers.QTI_Base.QTICompliantHelper.Interfaces
Imports Questify.Builder.Logic.QTI.Helpers.QTI30
Imports Questify.Builder.Logic.QTI.PackageCreators.QTI30
Imports Questify.Builder.Logic.QTI.Requests.QTI30
Imports Questify.Builder.Logic.QTI.Xsd.QTI30
Imports ResourceType = Questify.Builder.Logic.QTI.Xsd.QTI30.ResourceType

Namespace QTI.Model.QTI30

    Public Class QtixHtml

        Private _xHtml As String
        Private _qti As String
        Private _dependencies As New List(Of String)
        Private _css As String
        Private _itemCode As String
        Private _isConverted As Boolean = False
        Private _isPreview As Boolean = False
        Private _styleSheets As List(Of StyleSheetType)
        Private _resources As ConcurrentDictionary(Of String, Dictionary(Of ResourceType, List(Of String)))
        Private _resourceMimeTypeDictionary As ConcurrentDictionary(Of String, String)
        Private _resourceHelper As ResourceHelper
        Private _packageCreator As PackageCreator

        Public Sub New(xHtml As String, itemCode As String, isPreview As Boolean, resources As ConcurrentDictionary(Of String, Dictionary(Of ResourceType, List(Of String))), resourceMimeTypeDictionary As ConcurrentDictionary(Of String, String), css As String, resourceHelper As ResourceHelper, packageCreator As PackageCreator)
            _xHtml = xHtml
            _itemCode = itemCode
            _isPreview = isPreview
            _resources = resources
            _resourceMimeTypeDictionary = resourceMimeTypeDictionary
            _css = css
            _resourceHelper = resourceHelper
            _packageCreator = packageCreator
        End Sub

        Public Property Dependencies() As List(Of String)
            Get
                If Not _isConverted Then
                    Me.Convert()
                End If
                Return _dependencies
            End Get
            Set(ByVal value As List(Of String))
                _dependencies = value
            End Set
        End Property

        Public Property StyleSheets() As List(Of StyleSheetType)
            Get
                If Not _isConverted Then
                    Me.Convert()
                End If
                Return _styleSheets
            End Get
            Set(ByVal value As List(Of StyleSheetType))
                _styleSheets = value
            End Set
        End Property

        Public Property Css() As String
            Get
                If Not _isConverted Then
                    Me.Convert()
                End If
                Return _css
            End Get
            Set(ByVal value As String)
                _css = value
            End Set
        End Property

        Public Sub Convert()
            If Not _isConverted Then
                Dim ch = New ConverterHelper(Of PublicationRequest)
                Dim converter = ch.GetXhtmlConverter(_itemCode)

                Dim xmlDoc = Me.CreateDocument(_xHtml)
                Dim docHelper = New QTI30DocumentHelper()
                docHelper.ModifyXmlDocument(Of IModifyItemDocument)(xmlDoc)

                _xHtml = converter.ConvertXhtmlToQti(xmlDoc.DocumentElement.InnerXml, True)
                _dependencies = Me.GetAndProcessDepenciesFromTemplate(_xHtml)

                xmlDoc = Me.CreateDocument(_xHtml)
                TemplateHelper.ConvertInlineStylesToCss(xmlDoc, _css, converter)

                _styleSheets = Me.ExtractStylesheets(xmlDoc)
                _qti = xmlDoc.DocumentElement.SelectSingleNode("qti-item-body").OuterXml

                _isConverted = True
            End If
        End Sub

        Private Function CreateDocument(qtiString As String) As XmlDocument
            Dim xmlDoc As New XmlDocument
            xmlDoc.PreserveWhitespace = True
            xmlDoc.LoadXml($"<wrapper>{qtiString}</wrapper>")
            Return xmlDoc
        End Function

        Private Function ExtractStylesheets(xmlDoc As XmlDocument) As List(Of StyleSheetType)
            Dim stylesheetList As New List(Of StyleSheetType)
            Dim xmlnamespaceManager As New XmlNamespaceManager(xmlDoc.NameTable)
            Dim styleSheetNodes As XmlNodeList = xmlDoc.SelectNodes("//qti-stylesheet", xmlnamespaceManager)
            If styleSheetNodes IsNot Nothing Then
                Dim helper As New QTI30StyleSheetHelper

                For Each stylesheetNode As XmlNode In styleSheetNodes
                    If stylesheetNode.Attributes("href") IsNot Nothing Then
                        stylesheetList.Add(helper.GetStylesheetType(Path.GetFileName(stylesheetNode.Attributes("href").Value), _packageCreator.RelativePathToResources))
                    End If
                Next

                For i As Integer = styleSheetNodes.Count - 1 To 0 Step -1
                    styleSheetNodes(i).ParentNode.RemoveChild(styleSheetNodes(i))
                Next
            End If

            Return stylesheetList
        End Function

        Private Function GetAndProcessDepenciesFromTemplate(ByRef content As String) As List(Of String)
            Return If(String.IsNullOrWhiteSpace(content), _dependencies, _dependencies.Union(_resourceHelper.ProcessResources(content, _resources, _resourceMimeTypeDictionary, _isPreview, _itemCode)).ToList())
        End Function

        Public Overrides Function ToString() As String
            If Not _isConverted Then
                Me.Convert()
            End If
            Return _qti
        End Function

    End Class
End Namespace