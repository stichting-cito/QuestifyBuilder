
Imports System.ComponentModel
Imports Cito.Tester.ContentModel

<TestClass>
Public Class AssessmentItemValidationTests


    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub Validate_empty_Identifier()
        Dim assmtItm As New AssessmentItem
        Dim iErr = DirectCast(assmtItm, IDataErrorInfo)

        assmtItm.Validate("Identifier")

        Assert.IsFalse(String.IsNullOrEmpty(iErr.Item("Identifier")))
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub Validate_valid_Identifier()
        Dim assmtItm As New AssessmentItem
        assmtItm.Identifier = "xyz"
        Dim iErr = DirectCast(assmtItm, IDataErrorInfo)

        assmtItm.Validate("Identifier")

        Assert.IsTrue(String.IsNullOrEmpty(iErr.Item("Identifier")))
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub Validate_OnlyWitheSpace_Identifier()
        Dim assmtItm As New AssessmentItem
        assmtItm.Identifier = "   "
        Dim iErr = DirectCast(assmtItm, IDataErrorInfo)

        assmtItm.Validate("Identifier")

        Assert.AreNotEqual(0, iErr.Item("Identifier").Length)
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub Validate_IllegalChars_Identifier()
        Dim illegalChars = "<>""#%{}|\/^~[]';?:@=&^$+()!,`*"

        Dim assmtItm As AssessmentItem

        For i = 0 To illegalChars.Length - 1
            assmtItm = New AssessmentItem With {.Identifier = illegalChars(i)}
            assmtItm.Validate("Identifier")
            Assert.AreNotEqual(0, assmtItm.Item("Identifier").Length)
        Next
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub Validate_MaxLengthOf_Identifier()
        Dim assmtItm As New AssessmentItem

        assmtItm.Identifier = New String("a"c, 255)
        Dim iErr = DirectCast(assmtItm, IDataErrorInfo)

        assmtItm.Validate("Identifier")

        Assert.IsTrue(String.IsNullOrEmpty(iErr.Item("Identifier")))
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub Validate_Max_PlusOne_LengthOf_Identifier()
        Dim assmtItm As New AssessmentItem

        assmtItm.Identifier = New String("a"c, 256)
        Dim iErr = DirectCast(assmtItm, IDataErrorInfo)

        assmtItm.Validate("Identifier")

        Assert.IsFalse(String.IsNullOrEmpty(iErr.Item("Identifier")))
    End Sub


    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub Validate_empty_null_Title()
        Dim assmtItm As New AssessmentItem
        assmtItm.Title = Nothing

        assmtItm.Validate("Title")

        Assert.AreNotEqual(0, assmtItm.Item("Title").Length)
    End Sub


    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub Validate_empty_Title()
        Dim assmtItm As New AssessmentItem
        assmtItm.Title = String.Empty

        assmtItm.Validate("Title")

        Assert.AreNotEqual(0, assmtItm.Item("Title").Length)
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub Validate_Whitespace_isvalid_Title()
        Dim assmtItm As New AssessmentItem
        assmtItm.Title = " "

        assmtItm.Validate("Title")

        Assert.AreEqual(0, assmtItm.Item("Title").Length)
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub Validate_Notempty_Title()
        Dim assmtItm As New AssessmentItem
        assmtItm.Title = "Xyz"

        assmtItm.Validate("Title")

        Assert.AreEqual(0, assmtItm.Item("Title").Length)
    End Sub



    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub Validate_empty_null_LayoutTemplateSourceName()
        Dim assmtItm As New AssessmentItem
        assmtItm.LayoutTemplateSourceName = Nothing

        assmtItm.Validate("LayoutTemplateSourceName")

        Assert.AreNotEqual(0, assmtItm.Item("LayoutTemplateSourceName").Length)
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub Validate_empty_emptyString_LayoutTemplateSourceName()
        Dim assmtItm As New AssessmentItem
        assmtItm.LayoutTemplateSourceName = String.Empty

        assmtItm.Validate("LayoutTemplateSourceName")

        Assert.AreNotEqual(0, assmtItm.Item("LayoutTemplateSourceName").Length)
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub Validate_SomeValue_LayoutTemplateSourceName()
        Dim assmtItm As New AssessmentItem
        assmtItm.LayoutTemplateSourceName = "xyz"

        assmtItm.Validate("LayoutTemplateSourceName")

        Assert.AreEqual(0, assmtItm.Item("LayoutTemplateSourceName").Length)
    End Sub


    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub Validate_AllPropertiesSetToValids()
        Dim assmtItm As New AssessmentItem
        assmtItm.Identifier = "xyz"
        assmtItm.Title = "xyz"
        assmtItm.LayoutTemplateSourceName = "xyz"

        assmtItm.ValidateAllProperties()

        Assert.AreEqual(0, assmtItm.Error.Length)
    End Sub


End Class
