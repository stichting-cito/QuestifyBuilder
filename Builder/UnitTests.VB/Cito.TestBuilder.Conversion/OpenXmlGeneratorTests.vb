
Imports System.Linq
Imports System.Xml.Linq
Imports FluentAssertions
Imports Questify.Builder.Plugins.PaperBased

<TestClass()>
Public Class OpenXmlGeneratorTests

    <TestMethod(), TestCategory("OpenXmlHelper")>
    Public Sub RemoveTtsStyles_WithTtsStyles_ShouldRemoveTtsSpans()
        Dim openXmlGenerator As New OpenXmlGenerator
        Dim input As String = "<w:customXml w:uri=""http://www.cito.nl/citotester"" w:element=""xhtmlElementToOpenXml"" xml:space=""preserve"" xmlns:w=""http://schemas.openxmlformats.org/wordprocessingml/2006/main"">									<p id=""c1-id-10"" xmlns=""http://www.w3.org/1999/xhtml"" xml:space=""preserve"">Mag de chauffeur <span class=""UserSRttsTonen"" id=""c1-id-11"">al</span> <span class=""UserSRttsFonetisch"" id=""c1-id-12"">al</span><span class=""UserSRttsPauze"" id=""c1-id-13""> </span><span class=""UserSRttsTonen"" id=""c1-id-14"">rijden?</span> <span class=""UserSRttsFonetisch"" id=""c1-id-15"">rijden</span></p>								</w:customXml>"
        Dim expectedResult As String = "<w:customXml w:uri=""http://www.cito.nl/citotester"" w:element=""xhtmlElementToOpenXml"" xml:space=""preserve"" xmlns:w=""http://schemas.openxmlformats.org/wordprocessingml/2006/main"">									<p id=""c1-id-10"" xmlns=""http://www.w3.org/1999/xhtml"" xml:space=""preserve"">Mag de chauffeur al rijden? </p>								</w:customXml>"

        openXmlGenerator.RemoveTtsStyles(input)

        Assert.AreEqual(input, expectedResult)
        Assert.AreEqual(0, HtmlContainsTag(input, "span", "ttsFonetisch"))
        Assert.AreEqual(0, HtmlContainsTag(input, "span", "ttsPauze"))
        Assert.AreEqual(0, HtmlContainsTag(input, "span", "ttsTonen"))
    End Sub

    <TestMethod(), TestCategory("OpenXmlHelper")>
    Public Sub RemoveTtsStyles_WithTtsStyles_ShouldOnlyRemoveTtsSpans()
        Dim openXmlGenerator As New OpenXmlGenerator
        Dim input As String = "<w:customXml w:uri=""http://www.cito.nl/citotester"" w:element=""xhtmlElementToOpenXml"" xml:space=""preserve"" xmlns:w=""http://schemas.openxmlformats.org/wordprocessingml/2006/main"">									<p id=""c1-id-10"" xmlns=""http://www.w3.org/1999/xhtml"" xml:space=""preserve"">Mag de chauffeur <span style=""text-decoration:underline"" class=""UserSRttsTonen"" id=""c1-id-11"">al</span><span class=""UserSRttsFonetisch"" id=""c1-id-12""> <span style=""text-decoration:underline"" id=""c1-id-13"">al</span></span><span class=""UserSRttsPauze"" id=""c1-id-14""> </span><span class=""UserSRttsTonen"" id=""c1-id-15""> <span class=""UserSRVetOnderstreept"" id=""c1-id-16"">rijden?</span></span><span class=""UserSRttsFonetisch"" id=""c1-id-17""> <span class=""UserSRVetOnderstreept"" id=""c1-id-18"">rijden</span></span></p>								</w:customXml>"
        Dim expectedResult As String = "<w:customXml w:uri=""http://www.cito.nl/citotester"" w:element=""xhtmlElementToOpenXml"" xml:space=""preserve"" xmlns:w=""http://schemas.openxmlformats.org/wordprocessingml/2006/main"">									<p id=""c1-id-10"" xmlns=""http://www.w3.org/1999/xhtml"" xml:space=""preserve"">Mag de chauffeur <span style=""text-decoration:underline"" id=""c1-id-11"">al</span> <span class=""UserSRVetOnderstreept"" id=""c1-id-16"">rijden?</span></p>								</w:customXml>"

        openXmlGenerator.RemoveTtsStyles(input)

        Assert.AreEqual(input, expectedResult)
        Assert.AreEqual(2, HtmlContainsTag(input, "span"))
        Assert.AreEqual(0, HtmlContainsTag(input, "span", "ttsFonetisch"))
        Assert.AreEqual(0, HtmlContainsTag(input, "span", "ttsPauze"))
        Assert.AreEqual(0, HtmlContainsTag(input, "span", "ttsTonen"))
    End Sub

    <TestMethod(), TestCategory("OpenXmlHelper")>
    Public Sub RemoveTtsStyles_NoTtsStyles_ShouldNotHaveChanged()
        Dim openXmlGenerator As New OpenXmlGenerator
        Dim input As String = "<w:customXml w:uri=""http://www.cito.nl/citotester"" w:element=""xhtmlElementToOpenXml"" xml:space=""preserve"" xmlns:w=""http://schemas.openxmlformats.org/wordprocessingml/2006/main"">									<p id=""c1-id-10"" xmlns=""http://www.w3.org/1999/xhtml"" xml:space=""preserve"">Mag de chauffeur <strong id=""c1-id-11"">al</strong> <span style=""text-decoration:underline"" id=""c1-id-12"">al</span> <span class=""UserSRDoorstrepen"" id=""c1-id-13""> rijden</span>?<span class=""UserSRVetOnderstreept"" id=""c1-id-14""> rijden</span></p>								</w:customXml>"
        Dim expectedResult As String = input

        openXmlGenerator.RemoveTtsStyles(input)

        Assert.AreEqual(input, expectedResult)
        Assert.AreEqual(3, HtmlContainsTag(input, "span"))
    End Sub

    <TestMethod(), TestCategory("OpenXmlHelper")>
    Public Sub RemoveTtsStyles_WithTtsStyles_ShouldRemoveAllElementsWithTtsStyle()
        Dim openXmlGenerator As New OpenXmlGenerator
        Dim input As String = "<w:customXml w:uri=""http://www.cito.nl/citotester"" w:element=""xhtmlElementToOpenXml"" xml:space=""preserve"" xmlns:w=""http://schemas.openxmlformats.org/wordprocessingml/2006/main"">									<p id=""c1-id-10"" xmlns=""http://www.w3.org/1999/xhtml"" xml:space=""preserve"">Mag de chauffeur <span id=""c1-id-21"" class=""UserSRttsTonen"">al</span> <span id=""c1-id-22"" class=""UserSRttsFonetisch"">al</span> <span id=""c1-id-23"" class=""UserSRttsTonen"">rijden?</span> <span id=""c1-id-24"" class=""UserSRttsFonetisch"">rijden</span></p><p id=""c1-id-20"" class=""UserSRttsPauze"" xmlns=""http://www.w3.org/1999/xhtml"" xml:space=""preserve"">Nou?</p><p id=""c1-id-19"" class=""UserSRttsFonetisch"" xmlns=""http://www.w3.org/1999/xhtml"" xml:space=""preserve"">Nee dat mag hij niet.</p>								</w:customXml>"
        Dim expectedResult As String = "<w:customXml w:uri=""http://www.cito.nl/citotester"" w:element=""xhtmlElementToOpenXml"" xml:space=""preserve"" xmlns:w=""http://schemas.openxmlformats.org/wordprocessingml/2006/main"">									<p id=""c1-id-10"" xmlns=""http://www.w3.org/1999/xhtml"" xml:space=""preserve"">Mag de chauffeur al  rijden? </p>								</w:customXml>"

        openXmlGenerator.RemoveTtsStyles(input)

        Assert.AreEqual(input, expectedResult)
        Assert.AreEqual(1, HtmlContainsTag(input, "p"))
        Assert.AreEqual(0, HtmlContainsTag(input, "span"))
        Assert.AreEqual(0, HtmlContainsTag(input, "span", "ttsFonetisch"))
        Assert.AreEqual(0, HtmlContainsTag(input, "span", "ttsPauze"))
        Assert.AreEqual(0, HtmlContainsTag(input, "p", "ttsFonetisch"))
        Assert.AreEqual(0, HtmlContainsTag(input, "p", "ttsPauze"))
        Assert.AreEqual(0, HtmlContainsTag(input, "span", "ttsTonen"))
    End Sub

    <TestMethod(), TestCategory("OpenXmlHelper")>
    Public Sub RemoveTtsStyles_ParagraphWithTtsTonen_ShouldOnlyRemoveTtsClassName()
        Dim openXmlGenerator As New OpenXmlGenerator
        Dim input As String = "<w:customXml w:uri=""http://www.cito.nl/citotester"" w:element=""xhtmlElementToOpenXml"" xml:space=""preserve"" xmlns:w=""http://schemas.openxmlformats.org/wordprocessingml/2006/main"">									<p class=""UserSRttsTonen"" id=""c1-id-10"" xmlns=""http://www.w3.org/1999/xhtml"" xml:space=""preserve"">Mag de chauffeur al rijden?</p>								</w:customXml>"
        Dim expectedResult As String = "<w:customXml w:uri=""http://www.cito.nl/citotester"" w:element=""xhtmlElementToOpenXml"" xml:space=""preserve"" xmlns:w=""http://schemas.openxmlformats.org/wordprocessingml/2006/main"">									<p id=""c1-id-10"" xmlns=""http://www.w3.org/1999/xhtml"" xml:space=""preserve"">Mag de chauffeur al rijden?</p>								</w:customXml>"

        openXmlGenerator.RemoveTtsStyles(input)

        Assert.AreEqual(input, expectedResult)
        Assert.AreEqual(1, HtmlContainsTag(input, "p"))
        Assert.AreEqual(0, HtmlContainsTag(input, "p", "ttsTonen"))
    End Sub

    <TestMethod(), TestCategory("OpenXmlHelper")>
    Public Sub RemoveTtsStyles_ParagraphWithTtsTonen_AndOtherClass_ShouldOnlyRemoveTtsClassName()
        Dim openXmlGenerator As New OpenXmlGenerator
        Dim input As String = "<w:customXml w:uri=""http://www.cito.nl/citotester"" w:element=""xhtmlElementToOpenXml"" xml:space=""preserve"" xmlns:w=""http://schemas.openxmlformats.org/wordprocessingml/2006/main"">									<p class=""TestClassName UserSRttsTonen"" id=""c1-id-10"" xmlns=""http://www.w3.org/1999/xhtml"" xml:space=""preserve"">Mag de chauffeur al rijden?</p>								</w:customXml>"
        Dim expectedResult As String = "<w:customXml w:uri=""http://www.cito.nl/citotester"" w:element=""xhtmlElementToOpenXml"" xml:space=""preserve"" xmlns:w=""http://schemas.openxmlformats.org/wordprocessingml/2006/main"">									<p class=""TestClassName"" id=""c1-id-10"" xmlns=""http://www.w3.org/1999/xhtml"" xml:space=""preserve"">Mag de chauffeur al rijden?</p>								</w:customXml>"

        openXmlGenerator.RemoveTtsStyles(input)

        Assert.AreEqual(input, expectedResult)
        Assert.AreEqual(1, HtmlContainsTag(input, "p"))
        Assert.AreEqual(0, HtmlContainsTag(input, "p", "ttsTonen"))
    End Sub

    <TestMethod(), TestCategory("OpenXmlHelper")>
    Public Sub RemoveTtsStyles_SpanWithTtsTonen_AndStyleAttribute_ShouldOnlyRemoveTtsClassName()
        Dim openXmlGenerator As New OpenXmlGenerator
        Dim input As String = "<w:customXml w:uri=""http://www.cito.nl/citotester"" w:element=""xhtmlElementToOpenXml"" xml:space=""preserve"" xmlns:w=""http://schemas.openxmlformats.org/wordprocessingml/2006/main"">									<p id=""c1-id-10"" xmlns=""http://www.w3.org/1999/xhtml"" xml:space=""preserve"">Mag de chauffeur al <span style=""text-decoration:underline"" class=""UserSRttsTonen"" id=""c1-id-11"">rijden?</span></p>								</w:customXml>"
        Dim expectedResult As String = "<w:customXml w:uri=""http://www.cito.nl/citotester"" w:element=""xhtmlElementToOpenXml"" xml:space=""preserve"" xmlns:w=""http://schemas.openxmlformats.org/wordprocessingml/2006/main"">									<p id=""c1-id-10"" xmlns=""http://www.w3.org/1999/xhtml"" xml:space=""preserve"">Mag de chauffeur al <span style=""text-decoration:underline"" id=""c1-id-11"">rijden?</span></p>								</w:customXml>"

        openXmlGenerator.RemoveTtsStyles(input)

        Assert.AreEqual(input, expectedResult)
        Assert.AreEqual(1, HtmlContainsTag(input, "span"))
        Assert.AreEqual(0, HtmlContainsTag(input, "span", "ttsTonen"))
    End Sub

    <TestMethod(), TestCategory("OpenXmlHelper")>
    Public Sub RemoveTtsStyles_SpanWithTtsTonen_AndOtherClass_ShouldOnlyRemoveTtsClassName()
        Dim openXmlGenerator As New OpenXmlGenerator
        Dim input As String = "<w:customXml w:uri=""http://www.cito.nl/citotester"" w:element=""xhtmlElementToOpenXml"" xml:space=""preserve"" xmlns:w=""http://schemas.openxmlformats.org/wordprocessingml/2006/main"">									<p id=""c1-id-10"" xmlns=""http://www.w3.org/1999/xhtml"" xml:space=""preserve"">Mag de chauffeur al <span class=""TestClassName UserSRttsTonen"" id=""c1-id-11"">rijden?</span></p>								</w:customXml>"
        Dim expectedResult As String = "<w:customXml w:uri=""http://www.cito.nl/citotester"" w:element=""xhtmlElementToOpenXml"" xml:space=""preserve"" xmlns:w=""http://schemas.openxmlformats.org/wordprocessingml/2006/main"">									<p id=""c1-id-10"" xmlns=""http://www.w3.org/1999/xhtml"" xml:space=""preserve"">Mag de chauffeur al <span class=""TestClassName"" id=""c1-id-11"">rijden?</span></p>								</w:customXml>"

        openXmlGenerator.RemoveTtsStyles(input)

        Assert.AreEqual(input, expectedResult)
        Assert.AreEqual(1, HtmlContainsTag(input, "span"))
        Assert.AreEqual(0, HtmlContainsTag(input, "span", "ttsTonen"))
        Assert.AreEqual(1, HtmlContainsTag(input, "span", "TestClassName"))
    End Sub

    <TestMethod>
    Public Sub RemoveVerklankingParagraphsThatIncludeBreaksKeepsBreaks()
        Dim openXmlGenerator As New OpenXmlGenerator
        Dim input As String = "<w:customXml w:uri=""http://www.cito.nl/citotester"" w:element=""xhtmlElementToOpenXml"" xml:space=""preserve"" xmlns:w=""http://schemas.openxmlformats.org/wordprocessingml/2006/main""> <p id=""c1-id-10"" xmlns=""http://www.w3.org/1999/xhtml"" xml:space=""preserve"">Mag de chauffeur al <span class=""TestClassName UserSRttsTonen"" id=""c1-id-11"">rijden?</span><span class=""UserSRttsFonetisch"" id=""c1-id-24""><br id=""c1-id-16"" /></span></p></w:customXml>"

        openXmlGenerator.RemoveTtsStyles(input)

        input.Should().Contain("<br ")
    End Sub

    <TestMethod(), TestCategory("OpenXmlHelper")>
    Public Sub RemoveRemarksFromSpanTest()
        Dim openXmlGenerator As New OpenXmlGenerator
        Dim input As String = "<w:customXml w:uri=""http://www.cito.nl/citotester"" w:element=""xhtmlElementToOpenXml"" xml:space=""preserve"" xmlns:w=""http://schemas.openxmlformats.org/wordprocessingml/2006/main"">									<p id=""c1-id-10"" xmlns=""http://www.w3.org/1999/xhtml"" xml:space=""preserve"">Mag de chauffeur al <span class=""UserSROpmerkingNietInAfname"" id=""c1-id-11"">rijden?</span></p>								</w:customXml>"
        Dim expectedResult As String = "<w:customXml w:uri=""http://www.cito.nl/citotester"" w:element=""xhtmlElementToOpenXml"" xml:space=""preserve"" xmlns:w=""http://schemas.openxmlformats.org/wordprocessingml/2006/main"">									<p id=""c1-id-10"" xmlns=""http://www.w3.org/1999/xhtml"" xml:space=""preserve"">Mag de chauffeur al </p>								</w:customXml>"

        Dim result As String = openXmlGenerator.RemoveRemarks(input)

        Assert.AreEqual(expectedResult, result)
    End Sub

    <TestMethod(), TestCategory("OpenXmlHelper")>
    Public Sub RemoveRemarksFromParagraphTest()
        Dim openXmlGenerator As New OpenXmlGenerator
        Dim input As String = "<w:customXml w:uri=""http://www.cito.nl/citotester"" w:element=""xhtmlElementToOpenXml"" xml:space=""preserve"" xmlns:w=""http://schemas.openxmlformats.org/wordprocessingml/2006/main"">									<p id=""c1-id-10"" xmlns=""http://www.w3.org/1999/xhtml"" xml:space=""preserve"">Mag de chauffeur al <p class=""UserSROpmerkingNietInAfname"" id=""c1-id-11"">rijden?</p></p>								</w:customXml>"
        Dim expectedResult As String = "<w:customXml w:uri=""http://www.cito.nl/citotester"" w:element=""xhtmlElementToOpenXml"" xml:space=""preserve"" xmlns:w=""http://schemas.openxmlformats.org/wordprocessingml/2006/main"">									<p id=""c1-id-10"" xmlns=""http://www.w3.org/1999/xhtml"" xml:space=""preserve"">Mag de chauffeur al </p>								</w:customXml>"

        Dim result As String = openXmlGenerator.RemoveRemarks(input)

        Assert.AreEqual(expectedResult, result)
    End Sub


    Private Function HtmlContainsTag(xhtmlString As String, searchForTag As String, Optional searchForClass As String = "") As Integer
        Dim result As Integer
        Dim doc As XDocument = XDocument.Parse(xhtmlString)
        Dim namesp As XNamespace = "http://www.w3.org/1999/xhtml"

        If String.IsNullOrEmpty(searchForClass) Then
            result = doc.Descendants(namesp + searchForTag).Count
        Else
            doc.Descendants(namesp + searchForTag).ToList().ForEach(Sub(d)
                                                                        If d.Attributes IsNot Nothing AndAlso d.Attributes.Any(Function(a) a.Name = "class" AndAlso Not String.IsNullOrEmpty(a.Value) AndAlso a.Value.IndexOf(searchForClass, StringComparison.InvariantCultureIgnoreCase) > -1) Then
                                                                            result += 1
                                                                        End If
                                                                    End Sub)
        End If

        Return result
    End Function


End Class

