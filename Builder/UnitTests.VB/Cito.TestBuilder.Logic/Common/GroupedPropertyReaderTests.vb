
Imports Questify.Builder.Logic.Common
Imports System.Linq

<TestClass>
Public Class GroupedPropertyReaderTests

    <TestMethod(), TestCategory("Logic")>
    <ExpectedException(GetType(ArgumentNullException))>
    Public Sub ParseNullSingleString()
        Dim parser As New GroupedPropertyReader(Nothing)

    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub ParseEmptySingleString()
        Dim parser As New GroupedPropertyReader("")

        Dim result = parser.GetAsList()

        Assert.AreEqual(0, result.Count)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub ParseSingleGroup_1Key_String_HasKeys()
        Dim parser As New GroupedPropertyReader("(a=1)")

        Dim result = parser.GetAsList()

        Assert.AreEqual(1, result.First().Keys.Count)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub ParseSingleGroup_1Key_String()
        Dim parser As New GroupedPropertyReader("(a=1)")

        Dim result = parser.GetAsList()

        Assert.AreEqual(1, result.Count)
    End Sub


    <TestMethod(), TestCategory("Logic")>
    Public Sub ParseSingleGroup_2Key_String()
        Dim parser As New GroupedPropertyReader("(a=2;x=3)")

        Dim result = parser.GetAsList()

        Assert.AreEqual(1, result.Count)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub ParseSingleGroup_2Key_String_Has2Keys()
        Dim parser As New GroupedPropertyReader("(a=2;x=3)")

        Dim result = parser.GetAsList()

        Assert.AreEqual(2, result.First().Keys.Count)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub Parse_2Groups_1Key_String_HasKeys()
        Dim parser As New GroupedPropertyReader("(a=1)(a=2)")

        Dim result = parser.GetAsList()

        Assert.AreEqual(2, result.Count)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub Parse_2Groups_1Key_String_HasKeys_AsDictionary()
        Dim parser As New GroupedPropertyReader("(a=1)(a=2)")

        Dim result = parser.GetAsDictionary("a")

        Assert.AreEqual(2, result.Count)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub Parse_2Groups_1Key_String_HasKeys_AsDictionary_CaseInsensitive()
        Dim parser As New GroupedPropertyReader("(a=1)(A=2)")

        Dim result = parser.GetAsDictionary("A")

        Assert.AreEqual(2, result.Count)
    End Sub


    <TestMethod(), TestCategory("Logic")>
    Public Sub TestWithSample_GetAsDictionary_Has2Entires()
        Dim parser As New GroupedPropertyReader("(template=tmp.inline.word;icon=Icon1;selection=required;text=Woordkeytemplate)(template=tmp.inline.sentence;icon=Icon2;selection=required;text=Zin)")

        Dim result = parser.GetAsDictionary("template")

        Assert.AreEqual(2, result.Keys.Count)
        Assert.AreEqual("tmp.inline.word", result.Keys(0))
        Assert.AreEqual("tmp.inline.sentence", result.Keys(1))
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub TestWithSample_GetAsDictionary_Sentence_Has3Properties()
        Dim parser As New GroupedPropertyReader("(template=tmp.inline.word;icon=Icon1;selection=required;text=Woordkeytemplate)(template=tmp.inline.sentence;icon=Icon2;selection=required;text=Zin)")

        Dim result = parser.GetAsDictionary("template")("tmp.inline.sentence")

        Assert.AreEqual(4, result.Keys.Count)
        Assert.AreEqual("tmp.inline.sentence", result("template"))
        Assert.AreEqual("Icon2", result("icon"))
        Assert.AreEqual("required", result("selection"))
        Assert.AreEqual("Zin", result("Text"))
    End Sub

End Class
