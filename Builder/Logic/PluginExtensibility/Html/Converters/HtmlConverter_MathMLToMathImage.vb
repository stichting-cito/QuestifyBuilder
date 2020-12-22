Imports System.Globalization
Imports System.IO
Imports System.Linq
Imports System.Net
Imports System.Windows.Forms
Imports System.Xml.Linq
Imports Questify.Builder.Logic.HelperClasses
Imports Questify.Builder.Logic.Service.HelperFunctions

Namespace PluginExtensibility.Html.Converters

    Friend Class HtmlConverter_MathMLToMathImage
        Inherits HtmlConverterBase

        Private ReadOnly _mml As XNamespace = "http://www.w3.org/1998/Math/MathML"
        Private _mathMlEditorPlugin As IMathMlEditorPlugin
        Private _processedMathMlImages As Dictionary(Of Integer, String)
        Private _equalityComparer As XNodeEqualityComparer = New XNodeEqualityComparer()

        Sub New(mathMlEditorPlugin As IMathMlEditorPlugin)
            _mathMlEditorPlugin = mathMlEditorPlugin
        End Sub

        Protected Overrides Function DoConvert(html As String) As String
            Dim xDoc As XDocument
            Using strReader As New StringReader("<root>" + html + "</root>")
                xDoc = XDocument.Load(strReader, LoadOptions.PreserveWhitespace)
            End Using

            ProcessMathFormulaTags(xDoc)

            Return xDoc.Descendants("root").First().InnerXml()
        End Function

        Private Sub ProcessMathFormulaTags(xDoc As XDocument)
            Dim elementsToRemove As New List(Of XElement)
            Dim mathMlTags As IEnumerable(Of XElement) = xDoc.Descendants(_mml + "math")
            For Each mathMlTag As XElement In mathMlTags
                If _processedMathMlImages Is Nothing Then
                    _processedMathMlImages = New Dictionary(Of Integer, String)
                End If
                ProcessMathFormulaTag(mathMlTag)
                elementsToRemove.Add(mathMlTag)
            Next

            elementsToRemove.ForEach(Sub(e)
                                         e.Remove()
                                     End Sub)
        End Sub

        Private Sub ProcessMathFormulaTag(mathElement As XElement)
            Dim hash = _equalityComparer.GetHashCode(mathElement)
            If _processedMathMlImages.ContainsKey(hash) Then
                mathElement.AddAfterSelf(XElement.Parse(_processedMathMlImages(hash)))
            Else
                Dim mathMl = mathElement.OuterXml()
                Dim verticalAlignValue As Double

                Dim mathImage = _mathMlEditorPlugin.RenderPng(mathMl, MathMLHelper.CreateImageOptions(mathMl, MathMLHelper.GetBaseFont()), verticalAlignValue)
                Dim mathImgTag As XElement =
                        <img cito_value="" mathml_value=<%= WebUtility.HtmlEncode(mathMl) %> src=<%= String.Format("data:image/png;base64,{0}", Convert.ToBase64String(mathImage)) %> id=<%= Guid.NewGuid().ToString() %> ismathmlimage="true" style=<%= String.Format("vertical-align:{0}px;", (verticalAlignValue - 1) * -1).ToString(CultureInfo.InvariantCulture) %> alt=""/>

                _processedMathMlImages.Add(hash, mathImgTag.ToString())
                mathElement.AddAfterSelf(mathImgTag)
            End If
        End Sub

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                If _processedMathMlImages IsNot Nothing Then
                    _processedMathMlImages.Clear()
                End If
                _mathMlEditorPlugin = Nothing
            End If

            MyBase.Dispose(disposing)
        End Sub

    End Class

End Namespace