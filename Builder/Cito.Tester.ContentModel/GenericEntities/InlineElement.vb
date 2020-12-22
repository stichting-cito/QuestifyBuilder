Imports System.Xml
Imports System.Diagnostics.CodeAnalysis
Imports System.Drawing
Imports System.IO
Imports System.Text
Imports Cito.Tester.Common
Imports System.Web
Imports System.Xml.Serialization

<Serializable,
XmlRoot(Namespace:="http://www.cito.nl/citotester")>
Public Class InlineElement
    Inherits ValidatingEntityBase


    Private _identifier As String
    Private _layoutTemplateSourceName As String
    Private _inlineFindingOverride As String
    Private _parameters As ParameterSetCollection
    Private _imageConverter As InlineElementImageConverter



    <SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")>
    Public Sub New()
        Me._parameters = New ParameterSetCollection()

        Me.Identifier = String.Empty
        Me.LayoutTemplateSourceName = String.Empty

    End Sub



    Protected Overrides ReadOnly Property Validator As IEntityValidation
        Get
            Return New InlineElementValidator
        End Get
    End Property

    Public Function DeepClone() As InlineElement
        Dim clone As New InlineElement()
        clone.Identifier = Guid.NewGuid().ToString()
        clone.LayoutTemplateSourceName = Me.LayoutTemplateSourceName
        clone.Parameters.Clear()
        clone.Parameters.AddRange(ParameterSetCollection.DeepClone(Me.Parameters))

        Return clone
    End Function

    Public Function GetResourcesFromResourceParameter() As List(Of String)
        Dim returnList As New List(Of String)
        For Each parameter As ParameterBase In Me.Parameters.GetParameters()
            GetResourcesFromParameter(parameter, returnList)
        Next
        Return returnList
    End Function
    Private Sub GetResourcesFromParameter(parameter As ParameterBase, ByRef resourceCollection As List(Of String))
        If TypeOf parameter Is ResourceParameter Then
            Dim resourceParameter As ResourceParameter = DirectCast(parameter, ResourceParameter)
            If Not String.IsNullOrEmpty(resourceParameter.Value.Trim) AndAlso Not resourceCollection.Contains(resourceParameter.Value) Then
                resourceCollection.Add(resourceParameter.Value())
            End If
        ElseIf TypeOf parameter Is CollectionParameter Then
            Dim collectionParameter As CollectionParameter = DirectCast(parameter, CollectionParameter)
            For Each innerparameter As ParameterBase In collectionParameter.BluePrint.InnerParameters
                GetResourcesFromParameter(innerparameter, resourceCollection)
            Next
        End If
    End Sub




    <XmlIgnore>
    Public Property ImageConverter As InlineElementImageConverter
        Get
            Return Me._imageConverter
        End Get
        Set
            Me._imageConverter = Value
        End Set
    End Property

    <XmlAttribute("id", Namespace:="")>
    Public Property Identifier As String
        Get
            Return Me._identifier
        End Get
        Set
            Me._identifier = Value
            Me.Validate("Identifier")
        End Set
    End Property

    <XmlAttribute("layoutTemplateSourceName", Namespace:="")>
    Public Property LayoutTemplateSourceName As String
        Get
            Return Me._layoutTemplateSourceName
        End Get
        Set
            Me._layoutTemplateSourceName = Value
            Me.Validate("LayoutTemplateSourceName")
        End Set
    End Property

    <XmlArray("parameters"),
XmlArrayItem("parameterSet", GetType(ParameterCollection))>
    Public ReadOnly Property Parameters As ParameterSetCollection
        Get
            Return _parameters
        End Get
    End Property

    <XmlAttribute("inlineFO", Namespace:="")>
    Public Property InlineFindingOverride As String
        Get
            Return _inlineFindingOverride
        End Get
        Set
            _inlineFindingOverride = Value
        End Set
    End Property

    <XmlIgnore>
    Public Overrides ReadOnly Property ValidationEntityIdentifier As String
        Get
            Return Me.Identifier
        End Get
    End Property



    Public Overrides Sub ValidateAllProperties()
        Me.Validate("Identifier")
        Me.Validate("LayoutTemplateSourceName")
    End Sub




    Public Class InlineElementImageConverter

        Private ReadOnly _popupParAttributes As Dictionary(Of String, String) = New Dictionary(Of String, String)
        Private ReadOnly _namespaceManager As XmlNamespaceManager
        Private ReadOnly _resourceProtocolPrefix As String
        Private _actualSizes As Dictionary(Of String, Size) = Nothing


        <SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")>
        Public Sub New(namespaceManager As XmlNamespaceManager, resourceProtocolPrefix As String, isDefault As Boolean)
            _namespaceManager = namespaceManager
            _resourceProtocolPrefix = resourceProtocolPrefix

            CreatePopupParAttributes(isDefault)
        End Sub


        Private Sub CreatePopupParAttributes(isDefault As Boolean)
            _popupParAttributes.Add("resource", "")
            _popupParAttributes.Add("title", "")
            _popupParAttributes.Add("modal", "true")
            _popupParAttributes.Add("top", "0")
            _popupParAttributes.Add("left", "0")
            _popupParAttributes.Add("width", "0")
            _popupParAttributes.Add("height", "0")
            _popupParAttributes.Add("version", "1.0")
            _popupParAttributes.Add("resizable", "true")
            _popupParAttributes.Add("popupid", Guid.NewGuid().ToString())
        End Sub

        Private Sub SaveAttributesFromHtml(aElement As XmlElement)
            Dim keyValuePairs As String() = Split(aElement.GetAttribute("popuppar"), ";")

            For Each param As String In keyValuePairs
                Dim keyValuePair As String() = Split(param, "=")

                _popupParAttributes(keyValuePair(0).Trim()) = keyValuePair(1).Trim("'".ToCharArray())
            Next
        End Sub

        Private Function GetActualSize(fileName As String) As Size
            If _actualSizes Is Nothing Then
                _actualSizes = New Dictionary(Of String, Size)
            End If
            If _actualSizes.ContainsKey(fileName) Then
                Return _actualSizes(fileName)
            Else
                Dim actualSize As Size
                Try
                    actualSize = FileHelper.GetSizeFromByteArray(DirectCast(TestSessionContext.GetResourceObject(fileName, AddressOf StreamConverters.ConvertStreamToByteArray), Byte()))
                    _actualSizes.Add(fileName, actualSize)
                Catch ex As Exception
                End Try

                Return actualSize
            End If
        End Function

        Public Sub ConvertHtmlBasedOnOldItemLayoutToInlineElementLayout(htmlBasedOnOldItemLayoutTemplate As XmlElement, ByRef inlineElement As InlineElement)
            If (htmlBasedOnOldItemLayoutTemplate.Name.ToLower() = "a") Then
                SaveAttributesFromHtml(htmlBasedOnOldItemLayoutTemplate)

                If inlineElement.Parameters(0).GetParameterByName("largeYpos", False) IsNot Nothing Then DirectCast(inlineElement.Parameters(0).GetParameterByName("largeYpos"), IntegerParameter).Value = CInt(_popupParAttributes("top"))
                If inlineElement.Parameters(0).GetParameterByName("largeXpos", False) IsNot Nothing Then DirectCast(inlineElement.Parameters(0).GetParameterByName("largeXpos"), IntegerParameter).Value = CInt(_popupParAttributes("left"))
                If inlineElement.Parameters(0).GetParameterByName("largeImage", False) IsNot Nothing Then
                    Dim largeImg As ResourceParameter = DirectCast(inlineElement.Parameters(0).GetParameterByName("largeImage"), ResourceParameter)
                    largeImg.Value = Path.GetFileName(_popupParAttributes("resource"))
                    largeImg.Width = CInt(_popupParAttributes("width"))
                    largeImg.Height = CInt(_popupParAttributes("height"))

                    Dim actualSize = GetActualSize(Path.GetFileName(DirectCast(inlineElement.Parameters(0).GetParameterByName("largeImage"), ResourceParameter).Value))
                    largeImg.EditSize = Not (largeImg.Width = actualSize.Width AndAlso largeImg.Height = actualSize.Height)
                    actualSize = Nothing
                End If
                If inlineElement.Parameters(0).GetParameterByName("showPopup", False) IsNot Nothing Then DirectCast(inlineElement.Parameters(0).GetParameterByName("showPopup"), BooleanParameter).Value = True

                If inlineElement.Parameters(0).GetParameterByName("editLargePosition", False) IsNot Nothing Then DirectCast(inlineElement.Parameters(0).GetParameterByName("editLargePosition"), BooleanParameter).Value = CInt(_popupParAttributes("top")) > 0 AndAlso CInt(_popupParAttributes("left")) > 0
                If inlineElement.Parameters(0).GetParameterByName("popupDescription", False) IsNot Nothing Then DirectCast(inlineElement.Parameters(0).GetParameterByName("popupDescription"), PlainTextParameter).Value = _popupParAttributes("title")
                If inlineElement.Parameters(0).GetParameterByName("largeImageModal", False) IsNot Nothing Then DirectCast(inlineElement.Parameters(0).GetParameterByName("largeImageModal"), BooleanParameter).Value = CBool(_popupParAttributes("modal"))
                If inlineElement.Parameters(0).GetParameterByName("largeImageResizable", False) IsNot Nothing Then DirectCast(inlineElement.Parameters(0).GetParameterByName("largeImageResizable"), BooleanParameter).Value = CBool(_popupParAttributes("resizable"))

                Dim imageElement As XmlElement = DirectCast(htmlBasedOnOldItemLayoutTemplate.SelectSingleNode("def:img", _namespaceManager), XmlElement)
                If inlineElement.Parameters(0).GetParameterByName("source", False) IsNot Nothing Then
                    Dim image As ResourceParameter = DirectCast(inlineElement.Parameters(0).GetParameterByName("source"), ResourceParameter)
                    image.Value = HttpUtility.UrlDecode(Path.GetFileName(imageElement.GetAttribute("src")))
                    image.Width = If(imageElement.HasAttribute("width"), CInt(imageElement.GetAttribute("width")), 0)
                    image.Height = If(imageElement.HasAttribute("height"), CInt(imageElement.GetAttribute("height")), 0)

                    Dim actualSize As Size = GetActualSize(Path.GetFileName(DirectCast(inlineElement.Parameters(0).GetParameterByName("source"), ResourceParameter).Value))
                    image.EditSize = Not (image.Width = actualSize.Width AndAlso image.Height = actualSize.Height)
                    actualSize = Nothing
                End If
            ElseIf (htmlBasedOnOldItemLayoutTemplate.Name.ToLower() = "img") Then
                If inlineElement.Parameters(0).GetParameterByName("source", False) IsNot Nothing Then
                    Dim image As ResourceParameter = DirectCast(inlineElement.Parameters(0).GetParameterByName("source"), ResourceParameter)
                    image.Value = HttpUtility.UrlDecode(Path.GetFileName(htmlBasedOnOldItemLayoutTemplate.GetAttribute("src")))
                    image.Width = If(htmlBasedOnOldItemLayoutTemplate.HasAttribute("width"), CInt(htmlBasedOnOldItemLayoutTemplate.GetAttribute("width")), 0)
                    image.Height = If(htmlBasedOnOldItemLayoutTemplate.HasAttribute("height"), CInt(htmlBasedOnOldItemLayoutTemplate.GetAttribute("height")), 0)

                    Dim actualSize As Size = GetActualSize(Path.GetFileName(DirectCast(inlineElement.Parameters(0).GetParameterByName("source"), ResourceParameter).Value))
                    image.EditSize = Not (image.Width = actualSize.Width AndAlso image.Height = actualSize.Height)
                    actualSize = Nothing
                End If
            Else
                Throw New ArgumentException($"oldImage has an unsupported name: {htmlBasedOnOldItemLayoutTemplate.Name}")
            End If
        End Sub

        Public Sub ConvertInlineElementLayoutToHtmlBasedOnOldItemLayout(ByRef htmlBasedOnInlineElementLayout As XmlElement, inlineElement As InlineElement)
            Dim size As Size = Nothing
            Dim image As ResourceParameter = Nothing

            If inlineElement.Parameters(0).GetParameterByName("source", False) IsNot Nothing Then
                image = DirectCast(inlineElement.Parameters(0).GetParameterByName("source"), ResourceParameter)
            End If

            If htmlBasedOnInlineElementLayout.Name.ToLower() = "a" Then
                Dim largeImg As ResourceParameter = Nothing

                If image IsNot Nothing AndAlso inlineElement.Parameters(0).GetParameterByName("largeImage") IsNot Nothing Then
                    largeImg = DirectCast(inlineElement.Parameters(0).GetParameterByName("largeImage"), ResourceParameter)
                    _popupParAttributes("resource") = Path.GetFileName(largeImg.Value)

                    If (CInt(_popupParAttributes("width")) = 0 AndAlso CInt(_popupParAttributes("height")) = 0) AndAlso Not largeImg.EditSize Then
                        size = FileHelper.GetSizeFromByteArray(DirectCast(TestSessionContext.GetResourceObject(Path.GetFileName(largeImg.Value), AddressOf StreamConverters.ConvertStreamToByteArray), Byte()))
                        _popupParAttributes("width") = size.Width.ToString()
                        _popupParAttributes("height") = size.Height.ToString()
                    Else
                        _popupParAttributes("width") = CStr(largeImg.Width)
                        _popupParAttributes("height") = CStr(largeImg.Height)
                    End If

                    If DirectCast(inlineElement.Parameters(0).GetParameterByName("editLargePosition"), BooleanParameter).Value Then
                        _popupParAttributes("left") = CStr(DirectCast(inlineElement.Parameters(0).GetParameterByName("largeXpos"), IntegerParameter).Value)
                        _popupParAttributes("top") = CStr(DirectCast(inlineElement.Parameters(0).GetParameterByName("largeYpos"), IntegerParameter).Value)
                    Else
                        _popupParAttributes("left") = "0"
                        _popupParAttributes("top") = "0"
                    End If

                    If inlineElement.Parameters(0).GetParameterByName("popupDescription", False) IsNot Nothing Then _popupParAttributes("title") = DirectCast(inlineElement.Parameters(0).GetParameterByName("popupDescription"), PlainTextParameter).Value
                    If inlineElement.Parameters(0).GetParameterByName("largeImageModal", False) IsNot Nothing Then _popupParAttributes("modal") = DirectCast(inlineElement.Parameters(0).GetParameterByName("largeImageModal"), BooleanParameter).Value.ToString()
                    If inlineElement.Parameters(0).GetParameterByName("largeImageResizable", False) IsNot Nothing Then _popupParAttributes("resizable") = DirectCast(inlineElement.Parameters(0).GetParameterByName("largeImageResizable"), BooleanParameter).Value.ToString()
                    htmlBasedOnInlineElementLayout.SetAttribute("popuppar", CreatePopupParAttributeString())

                    Dim imgElement As XmlElement = DirectCast(htmlBasedOnInlineElementLayout.SelectSingleNode("def:img", _namespaceManager), XmlElement)
                    imgElement.SetAttribute("src", _resourceProtocolPrefix & image.Value)
                    imgElement.SetAttribute("id", inlineElement.Identifier)
                    imgElement.SetAttribute("alt", "")

                    If Not image.EditSize Then
                        If image.WidthSpecified AndAlso image.HeightSpecified Then
                            size = FileHelper.GetSizeFromByteArray(DirectCast(TestSessionContext.GetResourceObject(Path.GetFileName(image.Value), AddressOf StreamConverters.ConvertStreamToByteArray), Byte()))
                            imgElement.SetAttribute("width", size.Width.ToString())
                            imgElement.SetAttribute("height", size.Height.ToString())
                        End If
                    Else
                        imgElement.SetAttribute("width", CStr(image.Width))
                        imgElement.SetAttribute("height", CStr(image.Height))
                    End If
                End If

            ElseIf htmlBasedOnInlineElementLayout.Name.ToLower() = "img" Then

                If image IsNot Nothing Then
                    htmlBasedOnInlineElementLayout.SetAttribute("src", _resourceProtocolPrefix & image.Value)
                    htmlBasedOnInlineElementLayout.SetAttribute("id", inlineElement.Identifier)
                    htmlBasedOnInlineElementLayout.SetAttribute("alt", "")

                    If Not image.EditSize Then
                        If image.WidthSpecified AndAlso image.HeightSpecified Then
                            size = FileHelper.GetSizeFromByteArray(DirectCast(TestSessionContext.GetResourceObject(Path.GetFileName(image.Value), AddressOf StreamConverters.ConvertStreamToByteArray), Byte()))
                            htmlBasedOnInlineElementLayout.SetAttribute("width", size.Width.ToString())
                            htmlBasedOnInlineElementLayout.SetAttribute("height", size.Height.ToString())
                        End If
                    Else
                        htmlBasedOnInlineElementLayout.SetAttribute("width", CStr(image.Width))
                        htmlBasedOnInlineElementLayout.SetAttribute("height", CStr(image.Height))
                    End If
                End If
            Else
                Throw New ArgumentException($"oldImage has an unsupported name: {htmlBasedOnInlineElementLayout.Name}")
            End If
            size = Nothing
        End Sub

        Private Function CreatePopupParAttributeString() As String
            Dim builder As New StringBuilder()

            For Each kvp As KeyValuePair(Of String, String) In _popupParAttributes
                builder.Append(kvp.Key)
                builder.Append("=")
                builder.Append("'")
                builder.Append(kvp.Value)
                builder.Append("'")
                builder.Append("; ")
            Next

            Return builder.ToString().Trim("; ".ToCharArray())
        End Function
    End Class


End Class