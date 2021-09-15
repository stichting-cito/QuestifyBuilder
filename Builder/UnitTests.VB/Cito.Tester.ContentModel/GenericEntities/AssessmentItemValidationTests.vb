
Imports System.ComponentModel
Imports Cito.Tester.ContentModel

<TestClass>
Public Class AssessmentItemValidationTests

#Region "Identifier"

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub Validate_empty_Identifier()
        'Arrange
        Dim assmtItm As New AssessmentItem
        Dim iErr = DirectCast(assmtItm, IDataErrorInfo)
        
        'Act
        assmtItm.Validate("Identifier")
        
        'Assert
        Assert.IsFalse(String.IsNullOrEmpty(iErr.Item("Identifier")))
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub Validate_valid_Identifier()
        'Arrange
        Dim assmtItm As New AssessmentItem
        assmtItm.Identifier = "xyz"
        Dim iErr = DirectCast(assmtItm, IDataErrorInfo)
        
        'Act
        assmtItm.Validate("Identifier")
        
        'Assert
        Assert.IsTrue(String.IsNullOrEmpty(iErr.Item("Identifier")))
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub Validate_OnlyWitheSpace_Identifier()
        'Arrange
        Dim assmtItm As New AssessmentItem
        assmtItm.Identifier = "   "
        Dim iErr = DirectCast(assmtItm, IDataErrorInfo)
       
        'Act
        assmtItm.Validate("Identifier")
       
        'Assert
        Assert.AreNotEqual(0, iErr.Item("Identifier").Length)
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub Validate_IllegalChars_Identifier()
        'Arrange
        Dim illegalChars = "<>""#%{}|\/^~[]';?:@=&^$+()!,`*"

        Dim assmtItm As AssessmentItem

        'Act & Assert
        For i = 0 To illegalChars.Length - 1
            assmtItm = New AssessmentItem With {.Identifier = illegalChars(i)}
            assmtItm.Validate("Identifier")
            'Error should NOT be empty.
            Assert.AreNotEqual(0, assmtItm.Item("Identifier").Length)
        Next
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub Validate_MaxLengthOf_Identifier()
        'Arrange
        Dim assmtItm As New AssessmentItem

        assmtItm.Identifier = New String("a"c, 255)
        Dim iErr = DirectCast(assmtItm, IDataErrorInfo)
        
        'Act
        assmtItm.Validate("Identifier")
        
        'Assert
        Assert.IsTrue(String.IsNullOrEmpty(iErr.Item("Identifier")))
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub Validate_Max_PlusOne_LengthOf_Identifier()
        'Arrange
        Dim assmtItm As New AssessmentItem

        assmtItm.Identifier = New String("a"c, 256)
        Dim iErr = DirectCast(assmtItm, IDataErrorInfo)
       
        'Act
        assmtItm.Validate("Identifier")
       
        'Assert
        Assert.IsFalse(String.IsNullOrEmpty(iErr.Item("Identifier")))
    End Sub

#End Region

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub Validate_empty_null_Title()
        'Arrange
        Dim assmtItm As New AssessmentItem
        assmtItm.Title = Nothing
      
        'Act
        assmtItm.Validate("Title")
     
        'Assert
        Assert.AreNotEqual(0, assmtItm.Item("Title").Length)
    End Sub

#Region "Title"

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub Validate_empty_Title()
        'Arrange
        Dim assmtItm As New AssessmentItem
        assmtItm.Title = String.Empty
      
        'Act
        assmtItm.Validate("Title")
       
        'Assert
        Assert.AreNotEqual(0, assmtItm.Item("Title").Length)
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub Validate_Whitespace_isvalid_Title()
        'Arrange
        Dim assmtItm As New AssessmentItem
        assmtItm.Title = " "
    
        'Act
        assmtItm.Validate("Title")
     
        'Assert
        Assert.AreEqual(0, assmtItm.Item("Title").Length)
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub Validate_Notempty_Title()
        'Arrange
        Dim assmtItm As New AssessmentItem
        assmtItm.Title = "Xyz"
      
        'Act
        assmtItm.Validate("Title")
      
        'Assert
        Assert.AreEqual(0, assmtItm.Item("Title").Length)
    End Sub

#End Region

#Region "LayoutTemplateSourceName"

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub Validate_empty_null_LayoutTemplateSourceName()
        'Arrange
        Dim assmtItm As New AssessmentItem
        assmtItm.LayoutTemplateSourceName = Nothing
       
        'Act
        assmtItm.Validate("LayoutTemplateSourceName")
      
        'Assert
        Assert.AreNotEqual(0, assmtItm.Item("LayoutTemplateSourceName").Length)
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub Validate_empty_emptyString_LayoutTemplateSourceName()
        'Arrange
        Dim assmtItm As New AssessmentItem
        assmtItm.LayoutTemplateSourceName = String.Empty
     
        'Act
        assmtItm.Validate("LayoutTemplateSourceName")
    
        'Assert
        Assert.AreNotEqual(0, assmtItm.Item("LayoutTemplateSourceName").Length)
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub Validate_SomeValue_LayoutTemplateSourceName()
        'Arrange
        Dim assmtItm As New AssessmentItem
        assmtItm.LayoutTemplateSourceName = "xyz" 'Just needs to be set.
       
        'Act
        assmtItm.Validate("LayoutTemplateSourceName")
      
        'Assert
        Assert.AreEqual(0, assmtItm.Item("LayoutTemplateSourceName").Length)
    End Sub

#End Region

#Region "General"
    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub Validate_AllPropertiesSetToValids()
        'Arrange
        Dim assmtItm As New AssessmentItem
        assmtItm.Identifier = "xyz"
        assmtItm.Title = "xyz"
        assmtItm.LayoutTemplateSourceName = "xyz" 'Just needs to be set.
       
        'Act
        assmtItm.ValidateAllProperties()
      
        'Assert
        Assert.AreEqual(0, assmtItm.Error.Length)
    End Sub

#End Region

End Class
