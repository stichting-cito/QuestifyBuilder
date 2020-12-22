
Imports System.Linq
Imports System.Xml.Linq
Imports FakeItEasy
Imports Questify.Builder.IoC
Imports Questify.Builder.Logic
Imports Questify.Builder.Logic.PluginExtensibility.Html.Converters
Imports Questify.Builder.UnitTests.Framework

<TestClass()>
Public Class HtmlConverter_MathML_Tests

    <TestMethod(), TestCategory("UILogic")>
    Public Sub EditorToParamTest()
        Dim mathMlEditorPlugin = A.Fake(Of IMathMlEditorPlugin)()

        IoCHelper.Init(New List(Of IMathMlEditorPlugin) From {mathMlEditorPlugin})
        PluginHelper.MathMlPlugin = IoCHelper.GetInstances(Of IMathMlEditorPlugin).FirstOrDefault()

        A.CallTo(Function() mathMlEditorPlugin.RenderPng(A(Of String).Ignored, A(Of Dictionary(Of String, String)).Ignored, A(Of Double).Ignored)).ReturnsLazily(Function() GetMathMlImg())

        Dim expected = XDocument.Parse(mathImgHtml.ToString())
        Dim converter = New HtmlConverter_MathMLToMathImage(PluginHelper.MathMlPlugin)

        Dim result = XDocument.Parse(converter.ConvertHtml(mathFormulaHtml.ToString()))
        UnitTestHelper.SetFixedIdsToCompare(result)

        Dim areEqual = UnitTestHelper.AreSame(result, expected)
        Assert.IsTrue(areEqual)
    End Sub

    <TestMethod, TestCategory("UILogic")>
    Public Sub ParamToEditorTest()
        Dim expected = XDocument.Parse(mathFormulaHtml.ToString())
        Dim converter = New HtmlConverter_MathImageToMathML()

        Dim result = XDocument.Parse(converter.ConvertHtml(mathImgHtml.ToString()))

        Dim areEqual = UnitTestHelper.AreSame(result, expected)
        Assert.IsTrue(areEqual)
    End Sub

    Private Function GetMathMlImg() As Byte()
        Return Convert.FromBase64String("iVBORw0KGgoAAAANSUhEUgAAACEAAAASCAYAAADVCrdsAAAACXBIWXMAAA7EAAAOxAGVKw4bAAABt0lEQVRIie3Uz4uNYRQH8M+duQwbTBaEEWEyG8WKFFlMZmMoNUUp2zFFs7GQUjKyUpqGspHEZv4FC4WmpFlQLCxs2Ew0RWH8mGvxnDtz7zvvO/Pemh2nTs+v73nP9/me87z8t2SVFvEj2L3MHMarLYB3oYZLy0zicyvgm+jM7G3Lwa3EPnQrVroprqwSW/EF09iAMziMLZGwFrhNQfY1eoPEiYjrwikcweoYa9BWksQQbsf8J57jmYU3vYw3Uu/0YTuuxdkMnmAy+/EyJDbiD6ZiPY0JfMvB/sbRmP+IhHtjPYUXsd9kZcoxhLESOLhgXp2K1BePC7D1Es4pUcUN7M8A16MDH0uSqGE25gNYhasF2LlS1pW4joPowfEG4DncWSRpUff34DwO4dNScXUlLkrv/5gkIazBOrxvkcRmXEE/PuBsWRLwFC8xHOtB3F2EQB6JTtzDOA5IJektEddkA/iOHRgtwHThPl7hKx7hdJw9kPqi0UfibGfEvZVe2EOczEtQxbvwPQUkKmiPsSKp2djgKzLetlRceybBLH5hLW4VkKDheZm/cT0+61lsXtwC65B+zf+W/QU0JFY71brlJwAAAABJRU5ErkJggg==")
    End Function

    Private mathFormulaHtml As XElement = <html xmlns="http://www.w3.org/1999/xhtml">
                                              <body style="padding: 2px; margin: 0; box-sizing: border-box; height: auto; width: 100%;" id="fixedid">
                                                  <p id="fixedid">Formula <m:math xmlns:m="http://www.w3.org/1998/Math/MathML"><m:mi mathvariant="normal">π</m:mi><m:mo>+</m:mo><m:mo>-</m:mo><m:mo>&lt;</m:mo><m:mo>&gt;</m:mo><m:mo>≤</m:mo><m:mo>≥</m:mo></m:math> ... and text</p>
                                              </body>
                                          </html>

    Private mathImgHtml As XElement = <html xmlns="http://www.w3.org/1999/xhtml">
                                          <body style="padding: 2px; margin: 0; box-sizing: border-box; height: auto; width: 100%;" id="fixedid">
                                              <p id="fixedid">Formula <img xmlns="" cito_value="" mathml_value="&amp;lt;m:math xmlns:m=&amp;quot;http://www.w3.org/1998/Math/MathML&amp;quot;&amp;gt;&amp;lt;m:mi mathvariant=&amp;quot;normal&amp;quot;&amp;gt;π&amp;lt;/m:mi&amp;gt;&amp;lt;m:mo&amp;gt;+&amp;lt;/m:mo&amp;gt;&amp;lt;m:mo&amp;gt;-&amp;lt;/m:mo&amp;gt;&amp;lt;m:mo&amp;gt;&amp;amp;lt;&amp;lt;/m:mo&amp;gt;&amp;lt;m:mo&amp;gt;&amp;amp;gt;&amp;lt;/m:mo&amp;gt;&amp;lt;m:mo&amp;gt;≤&amp;lt;/m:mo&amp;gt;&amp;lt;m:mo&amp;gt;≥&amp;lt;/m:mo&amp;gt;&amp;lt;/m:math&amp;gt;" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAACEAAAASCAYAAADVCrdsAAAACXBIWXMAAA7EAAAOxAGVKw4bAAABt0lEQVRIie3Uz4uNYRQH8M+duQwbTBaEEWEyG8WKFFlMZmMoNUUp2zFFs7GQUjKyUpqGspHEZv4FC4WmpFlQLCxs2Ew0RWH8mGvxnDtz7zvvO/Pemh2nTs+v73nP9/me87z8t2SVFvEj2L3MHMarLYB3oYZLy0zicyvgm+jM7G3Lwa3EPnQrVroprqwSW/EF09iAMziMLZGwFrhNQfY1eoPEiYjrwikcweoYa9BWksQQbsf8J57jmYU3vYw3Uu/0YTuuxdkMnmAy+/EyJDbiD6ZiPY0JfMvB/sbRmP+IhHtjPYUXsd9kZcoxhLESOLhgXp2K1BePC7D1Es4pUcUN7M8A16MDH0uSqGE25gNYhasF2LlS1pW4joPowfEG4DncWSRpUff34DwO4dNScXUlLkrv/5gkIazBOrxvkcRmXEE/PuBsWRLwFC8xHOtB3F2EQB6JTtzDOA5IJektEddkA/iOHRgtwHThPl7hKx7hdJw9kPqi0UfibGfEvZVe2EOczEtQxbvwPQUkKmiPsSKp2djgKzLetlRceybBLH5hLW4VkKDheZm/cT0+61lsXtwC65B+zf+W/QU0JFY71brlJwAAAABJRU5ErkJggg==" id="fixedid" ismathmlimage="true" style="vertical-align:1px;" alt=""/> ... and text</p>
                                          </body>
                                      </html>
End Class
