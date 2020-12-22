Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports Cito.Tester.ContentModel
Imports Cito.Tester.Common
Imports System.Xml
Imports System.Linq
Imports System.IO
Imports System.Drawing.Imaging
Imports System.Text.RegularExpressions

Public Class PlaceHolderHelper
    Implements IDisposable

    Private Const SHOWDIMENSIONS_MINIMUM_WIDTH As Integer = 80
    Private Const SHOWDIMENSIONS_MINIMUM_HEIGHT As Integer = 30

    Private _binaryData As Dictionary(Of String, Byte()) = Nothing

    Public Sub Dispose() Implements IDisposable.Dispose
        If _binaryData IsNot Nothing Then
            _binaryData.Clear()
        End If
    End Sub


    Public Function InlineElementToPlaceHolderImage(ByVal inlineElement As InlineElement, defaultNamespace As String, Optional isPopupInlineElement As Boolean = False) As XmlElement
        Return InlineElementToPlaceHolderImage(inlineElement, defaultNamespace, Nothing, isPopupInlineElement)
    End Function

    Public Function InlineElementToPlaceHolderImage(ByVal inlineElement As InlineElement, defaultNamespace As String, resourceManager As ResourceManagerBase, Optional isPopupInlineElement As Boolean = False) As XmlElement
        Dim flattenedParameters = inlineElement.Parameters.FlattenParameters().ToList()
        Dim sources = GetSources(flattenedParameters)

        Dim width As Integer? = DetermineWidth(sources, flattenedParameters)
        Dim height As Integer? = DetermineHeight(sources, flattenedParameters)

        Dim tempDoc = New XmlDocument()
        Dim imgElement = tempDoc.CreateElement("img", defaultNamespace)
        imgElement.Attributes.Append(tempDoc.CreateAttribute("isinlineelement")).Value = "true"
        imgElement.Attributes.Append(tempDoc.CreateAttribute("id")).Value = inlineElement.Identifier

        Dim inlineControlImage As String = GetPlaceHolderImage(inlineElement)

        If sources.Any() OrElse Not String.IsNullOrEmpty(inlineControlImage) OrElse isPopupInlineElement Then
            imgElement = SetDimensions(imgElement, tempDoc, width, height)

            If sources.Any(Function(s) IsImage(s.ToString, resourceManager)) Then
                Dim sourceName As String = sources.First(Function(s) IsImage(s.ToString, resourceManager)).ToString()
                imgElement.Attributes.Append(tempDoc.CreateAttribute("alt")).Value = sourceName
                imgElement.Attributes.Append(tempDoc.CreateAttribute("src")).Value = $"resource://package/{sourceName}"
            ElseIf Not String.IsNullOrEmpty(inlineControlImage) Then
                imgElement.Attributes.Append(tempDoc.CreateAttribute("alt")).Value = ""
                imgElement.Attributes.Append(tempDoc.CreateAttribute("style")).Value = "vertical-align: middle;"
                imgElement.Attributes.Append(tempDoc.CreateAttribute("isinlinecontrol")).Value = "true"
                imgElement.Attributes.Append(tempDoc.CreateAttribute("src")).Value =
                    $"data:image/png;base64,{inlineControlImage}"
            Else
                imgElement = CreatePlaceHolder(imgElement, sources, resourceManager, tempDoc, width, height, isPopupInlineElement)
            End If
        Else
            imgElement = CreatePlaceHolder(imgElement, sources, resourceManager, tempDoc, width, height, isPopupInlineElement)
        End If

        Return imgElement
    End Function



    Private Function GetSources(flattenedParameters As List(Of ParameterBase)) As List(Of ParameterBase)
        Dim sources As New List(Of ParameterBase)

        If flattenedParameters.OfType(Of ResourceParameter).Any() Then
            sources = flattenedParameters.Where(Function(p) TypeOf p Is ResourceParameter AndAlso Not String.IsNullOrEmpty(p.ToString())).ToList
        End If

        Return sources
    End Function

    Private Function DetermineWidth(sources As List(Of ParameterBase), flattenedParameters As List(Of ParameterBase)) As Integer?
        Dim source = sources.OfType(Of ResourceParameter).FirstOrDefault(Function(s) Not String.IsNullOrEmpty(s.Value))
        If source IsNot Nothing AndAlso source.Width > 0 Then
            Return source.Width
        Else
            Dim imageWidth = TryCast(flattenedParameters.FirstOrDefault(Function(p) p.Name.Equals("Width", StringComparison.OrdinalIgnoreCase)), IntegerParameter)
            If imageWidth IsNot Nothing AndAlso imageWidth.Value > 0 Then
                Return imageWidth.Value
            End If
        End If

        Return Nothing
    End Function

    Private Function DetermineHeight(sources As List(Of ParameterBase), flattenedParameters As List(Of ParameterBase)) As Integer?
        Dim source = sources.OfType(Of ResourceParameter).FirstOrDefault(Function(s) Not String.IsNullOrEmpty(s.Value))
        If source IsNot Nothing AndAlso source.Height > 0 Then
            Return source.Height
        Else
            Dim imageHeight = TryCast(flattenedParameters.FirstOrDefault(Function(p) p.Name.Equals("Height", StringComparison.OrdinalIgnoreCase)), IntegerParameter)
            If imageHeight IsNot Nothing AndAlso imageHeight.Value > 0 Then
                Return imageHeight.Value
            End If
        End If

        Return Nothing
    End Function

    Private Function SetDimensions(imgElement As XmlElement,
                                   tempDoc As XmlDocument,
                                   width As Integer?,
                                   height As Integer?) As XmlElement
        If width.HasValue AndAlso width.Value > 0 Then
            imgElement.Attributes.Append(tempDoc.CreateAttribute("width")).Value = width.Value.ToString
        End If
        If height.HasValue AndAlso height.Value > 0 Then
            imgElement.Attributes.Append(tempDoc.CreateAttribute("height")).Value = height.Value.ToString
        End If

        Return imgElement
    End Function

    Private Function CreatePlaceHolder(imgElement As XmlElement,
                                       sources As List(Of ParameterBase),
                                       resourceManager As ResourceManagerBase,
                                       tempDoc As XmlDocument,
                                       width As Integer?,
                                       height As Integer?,
                                       isPopupInlineElement As Boolean) As XmlElement
        Dim placeHolderText As String = String.Empty

        Dim w = 200
        Dim h = 150

        If width.HasValue AndAlso width.Value > 0 Then
            w = width.Value
        End If
        If height.HasValue AndAlso height.Value > 0 Then
            h = height.Value
        End If

        Dim showDimensions = HasMinimumSizeToShowDimensions(w, h)
        If sources.Any() Then
            sources.ForEach(Sub(s)
                                placeHolderText = String.Concat(placeHolderText, $"'{s.ToString}' {vbNewLine}")
                            End Sub)

            Dim containsAudio As Boolean = sources.Any(Function(s) IsAudio(s.ToString, resourceManager))
            showDimensions = showDimensions AndAlso Not containsAudio
        End If

        If String.IsNullOrEmpty(placeHolderText) AndAlso Not isPopupInlineElement Then
            placeHolderText = My.Resources.PlaceHolderHelper_NoSourceSelectedText
        End If

        imgElement.Attributes.Append(tempDoc.CreateAttribute("alt")).Value = "placeHolder"
        imgElement.Attributes.Append(tempDoc.CreateAttribute("src")).Value =
            $"data:image/jpg;base64,{GenerateBase64PlaceHolder(w, h, placeHolderText, showDimensions)}"

        Return imgElement
    End Function

    Private Function IsImage(fileName As String, Optional resourceManager As ResourceManagerBase = Nothing) As Boolean
        Dim binaryData As Byte() = Nothing
        If (resourceManager IsNot Nothing) Then
            Dim mediaType = resourceManager.GetGenericResourceMimeType(fileName)
            If Not String.IsNullOrEmpty(mediaType) Then
                Return mediaType.Equals("image", StringComparison.InvariantCultureIgnoreCase)
            End If
            binaryData = GetResourceData(resourceManager, fileName)
        End If
        Return FileHelper.IsImage(fileName, binaryData)
    End Function

    Private Function IsAudio(fileName As String, Optional resourceManager As ResourceManagerBase = Nothing) As Boolean
        Dim binaryData As Byte() = Nothing
        If (resourceManager IsNot Nothing) Then
            Dim mediaType = resourceManager.GetGenericResourceMimeType(fileName)
            If Not String.IsNullOrEmpty(mediaType) Then
                Return mediaType.Equals("audio", StringComparison.InvariantCultureIgnoreCase)
            End If
            binaryData = GetResourceData(resourceManager, fileName)
        End If
        Return FileHelper.IsAudio(fileName, binaryData)
    End Function

    Private Function GetResourceData(ByVal resourceManager As ResourceManagerBase, fileName As String) As Byte()
        If _binaryData Is Nothing Then
            _binaryData = New Dictionary(Of String, Byte())
        End If
        If Not _binaryData.ContainsKey(fileName) Then
            Dim resourceStream = resourceManager.GetResource(fileName)
            If resourceStream IsNot Nothing AndAlso resourceStream.ResourceObject IsNot Nothing Then
                Using ms As New MemoryStream()
                    resourceStream.ResourceObject.CopyTo(ms)
                    _binaryData.Add(fileName, ms.ToArray())
                End Using
            End If
        End If

        Return _binaryData(fileName)
    End Function

    Private Function HasMinimumSizeToShowDimensions(width As Integer, height As Integer) As Boolean
        Return (width >= SHOWDIMENSIONS_MINIMUM_WIDTH AndAlso height >= SHOWDIMENSIONS_MINIMUM_HEIGHT)
    End Function

    Private Function GetPlaceHolderImage(inlineElement As InlineElement) As String
        Dim image As String = GetPlaceHolderImageByScoringParameter(inlineElement)

        If String.IsNullOrEmpty(image) Then
            image = GetPlaceHolderImageByControlType(inlineElement)
        End If

        Return image
    End Function

    Private Function GetPlaceHolderImageByScoringParameter(inlineElement As InlineElement) As String

        If Not inlineElement.Parameters.Any(Function(p) p.InnerParameters.OfType(Of ScoringParameter).Any()) Then Return String.Empty

        Dim paramCollection = inlineElement.Parameters.First(Function(p) p.InnerParameters.OfType(Of ScoringParameter).Any())
        Dim scoringParameter = paramCollection.InnerParameters.OfType(Of ScoringParameter).First()

        Select Case scoringParameter.GetType().ToString()

            Case GetType(InlineChoiceScoringParameter).ToString
                Return Convert.ToBase64String(ImageToByte2(My.Resources.InlineChoice))

            Case GetType(HotTextScoringParameter).ToString(),
                GetType(HotTextCorrectionScoringParameter).ToString()
                Return Convert.ToBase64String(ImageToByte2(My.Resources.InlineHTSC))

            Case GetType(IntegerScoringParameter).ToString(),
                GetType(DecimalScoringParameter).ToString(),
                GetType(CurrencyScoringParameter).ToString(),
                GetType(DateScoringParameter).ToString(),
                GetType(TimeScoringParameter).ToString(),
                GetType(MathScoringParameter).ToString(),
                GetType(StringScoringParameter).ToString()
                Return Convert.ToBase64String(ImageToByte2(My.Resources.InlineGap))

            Case GetType(GapMatchScoringParameter).ToString()
                Return Convert.ToBase64String(ImageToByte2(My.Resources.InlineGapMatch))

            Case GetType(AspectScoringParameter).ToString()
                Return Convert.ToBase64String(ImageToByte2(My.Resources.InlineEssay))

            Case Else
                Return String.Empty
        End Select

    End Function

    Private Function GetPlaceHolderImageByControlType(inlineElement As InlineElement) As String

        Dim flattenParameters = inlineElement.Parameters.FlattenParameters().ToList()
        Dim fp = flattenParameters.Where(Function(p) p.Name.Equals("controlType", StringComparison.OrdinalIgnoreCase))
        If fp.Count > 0 Then
            Dim controlType = fp(0).ToString
            Select Case controlType.ToLower
                Case "choice"
                    Return Convert.ToBase64String(ImageToByte2(My.Resources.InlineChoice))
                Case "input"
                    Return Convert.ToBase64String(ImageToByte2(My.Resources.InlineGap))
                Case "gapmatch"
                    Return Convert.ToBase64String(ImageToByte2(My.Resources.InlineGapMatch))
                Case "sc", "hottext"
                    Return Convert.ToBase64String(ImageToByte2(My.Resources.InlineHTSC))
                Case "mc"
                    Return Convert.ToBase64String(ImageToByte2(My.Resources.InlineMC))
                Case "mr"
                    Return Convert.ToBase64String(ImageToByte2(My.Resources.InlineMR))
                Case "tabular"
                    Return Convert.ToBase64String(ImageToByte2(My.Resources.InlineTabular))
                Case "essay", "signature"
                    Return Convert.ToBase64String(ImageToByte2(My.Resources.InlineEssay))
                Case Else
                    Debug.Assert(False, "unknown controlType")
                    Return String.Empty
            End Select
        End If

        Return String.Empty
    End Function

    Private Function ImageToByte2(img As Image) As Byte()
        Dim byteArray As Byte() = New Byte(-1) {}
        Using stream As New MemoryStream()
            img.Save(stream, System.Drawing.Imaging.ImageFormat.Png)
            stream.Close()

            byteArray = stream.ToArray()
        End Using
        Return byteArray
    End Function

    Private Function GenerateBase64PlaceHolder(ByVal width As Integer, ByVal height As Integer, ByVal text As String, showDimensions As Boolean) As String
        If width <= 0 Then Throw New ArgumentOutOfRangeException("width", "Width must be larger than 0")
        If height <= 0 Then Throw New ArgumentOutOfRangeException("height", "Height must be larger than 0")
        Dim returnValue = String.Empty
        Dim drawformat As StringFormat = New StringFormat()
        Dim font As Font = New Font("Arial", 8)

        Using bitmap As Bitmap = New Bitmap(width, height, PixelFormat.Format32bppArgb)
            Using graphics As Graphics = Graphics.FromImage(bitmap)
                Dim result As Byte()
                Dim borderColor = New SolidBrush(Color.FromArgb(255, 103, 126, 153))
                Dim bgColor = New SolidBrush(Color.FromArgb(255, 240, 245, 250))
                Dim txtcolor = borderColor
                Dim borderPen = New Pen(borderColor, 1)
                borderPen.DashStyle = DashStyle.Dash
                graphics.Clear(Color.White)

                Dim border = New Rectangle(0, 0, width - CInt(borderPen.Width), height - CInt(borderPen.Width))
                graphics.DrawRectangle(borderPen, border)

                Dim background = New Rectangle(3, 3, width - 6, height - 6)
                graphics.FillRectangle(bgColor, background)

                Dim textDrawingRectangle As RectangleF
                If showDimensions Then
                    drawformat.Alignment = StringAlignment.Center
                    textDrawingRectangle = New RectangleF(5, 5, width - 10, height - 10)
                    graphics.DrawString(width & "px", font, txtcolor, textDrawingRectangle, drawformat)

                    drawformat.Alignment = StringAlignment.Far
                    textDrawingRectangle = New RectangleF(5, CType(height / 2, Single) - font.Size, width - 10, 20)
                    graphics.DrawString(height & "px", font, txtcolor, textDrawingRectangle, drawformat)
                End If

                drawformat.Alignment = StringAlignment.Center
                Dim linesOfText = Regex.Matches(text, Regex.Escape($"{vbNewLine}")).Count + 1
                textDrawingRectangle = New RectangleF(5, height - (15 * linesOfText), width - 10, (20 * linesOfText))
                graphics.DrawString(text, font, txtcolor, textDrawingRectangle, drawformat)

                Using memoryStream As New IO.MemoryStream()
                    bitmap.Save(memoryStream, ImageFormat.Png)
                    result = memoryStream.GetBuffer()
                End Using
                returnValue = Convert.ToBase64String(result)
            End Using
        End Using
        Return returnValue
    End Function


End Class
