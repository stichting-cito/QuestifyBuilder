
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports System.ComponentModel

<TestClass()>
Public Class CustomBankPropertyEntityTest
    Inherits GenericTestBase(Of CustomBankPropertyEntity)

    Private Const _replacedCharacter As String = "[_]"

    'CustomBankPropertyEntity
    Protected Overrides Function CreateTheObject() As CustomBankPropertyEntity
        Return New CustomBankPropertyEntity
    End Function

    Protected Overrides Sub SetErroneousParam(obj As CustomBankPropertyEntity)
        obj.Name = ""
    End Sub

    Protected Overrides Sub SetCorrectParam(obj As CustomBankPropertyEntity)
        obj.Name = "abc"
    End Sub

    <TestMethod()> <TestCategory("ContentModel")> 
    Public Sub ValidateNameField_Valid_Test()
        'Arrange
        Dim entity As New CustomBankPropertyEntity()
        Dim validNameValue As String = "Deze string is valide"

        'Act
        entity.Name = validNameValue

        'Assert
        Assert.AreSame(validNameValue, entity.Name)
    End Sub

    <TestMethod()> <TestCategory("ContentModel")> 
    Public Sub ValidateNameField_IllegalCharacters_Test1()
        'Arrange
        Dim entity As New CustomBankPropertyEntity()
        Dim illegalNameValue As String = "Deze string @ bevat# illegale karakters."

        'Act
        entity.Name = illegalNameValue

        'Assert
        Assert.IsTrue(String.IsNullOrEmpty(entity.Name))
        Assert.IsFalse(String.IsNullOrEmpty(CType(entity, IDataErrorInfo).Error))
    End Sub

    <TestMethod()> <TestCategory("ContentModel")> 
    Public Sub ValidateNameField_IllegalCharacters_Test2()
        'Arrange
        Dim entity As New CustomBankPropertyEntity()
        Dim illegalNameValue As String = "\Deze <string> bevat % 'illegale karakters."

        'Act
        entity.Name = illegalNameValue

        'Assert
        Assert.IsTrue(String.IsNullOrEmpty(entity.Name))
        Assert.IsFalse(String.IsNullOrEmpty(CType(entity, IDataErrorInfo).Error))
    End Sub

    <TestMethod()> <TestCategory("ContentModel")> 
    Public Sub ValidateNameField_IllegalCharacters_Test3()
        'Arrange
        Dim entity As New CustomBankPropertyEntity()
        Dim illegalNameValue As String = "Deze string $bevat >< (illegale) karakters;"

        'Act
        entity.Name = illegalNameValue

        'Assert
        Assert.IsTrue(String.IsNullOrEmpty(entity.Name))
        Assert.IsFalse(String.IsNullOrEmpty(CType(entity, IDataErrorInfo).Error))
    End Sub

    <TestMethod()> <TestCategory("ContentModel")> 
    Public Sub ValidateNameField_IllegalCharacters_Test4()
        'Arrange
        Dim entity As New CustomBankPropertyEntity()
        Dim illegalNameValue As String = "Deze {string} bevat ""illegale"" karakters?"

        'Act
        entity.Name = illegalNameValue

        'Assert
        Assert.IsTrue(String.IsNullOrEmpty(entity.Name))
        Assert.IsFalse(String.IsNullOrEmpty(CType(entity, IDataErrorInfo).Error))
    End Sub

    <TestMethod()> <TestCategory("ContentModel")> 
    Public Sub ValidateNameField_IllegalCharactersTest5()
        'Arrange
        Dim entity As New CustomBankPropertyEntity()
        Dim illegalNameValue As String = "Deze {string} ""bevat"" == illegale karakters!!"

        'Act
        entity.Name = illegalNameValue

        'Assert
        Assert.IsTrue(String.IsNullOrEmpty(entity.Name))
        Assert.IsFalse(String.IsNullOrEmpty(CType(entity, IDataErrorInfo).Error))
    End Sub

    <TestMethod()> <TestCategory("ContentModel")> 
    Public Sub TwoSequentialDots_ValidateNameFieldTest()
        'Arrange
        Dim entity As New CustomBankPropertyEntity()
        Dim illegalNameValue As String = "Een name met daarin twee .. achter elkaar"

        'Act
        entity.Name = illegalNameValue

        'Assert
        Assert.IsTrue(String.IsNullOrEmpty(entity.Name))
        Assert.IsFalse(String.IsNullOrEmpty(CType(entity, IDataErrorInfo).Error))
    End Sub

    <TestMethod()> <TestCategory("ContentModel")> 
    Public Sub MultipleSequentialDots_ValidateNameFieldTest()
        'Arrange
        Dim entity As New CustomBankPropertyEntity()
        Dim illegalNameValue As String = "Een name met daarin meerdere .... achter elkaar"

        'Act
        entity.Name = illegalNameValue

        'Assert
        Assert.IsTrue(String.IsNullOrEmpty(entity.Name))
        Assert.IsFalse(String.IsNullOrEmpty(CType(entity, IDataErrorInfo).Error))
    End Sub

    <TestMethod()> <TestCategory("ContentModel")> 
    <ExpectedException(GetType(ArgumentOutOfRangeException))>
    Public Sub StringTooLong_ValidateNameFieldTest()
        'Arrange
        Dim entity As New CustomBankPropertyEntity()
        Dim illegalNameValue As String = "Ja zo zijn we niet getrouwd natuurlijk want deze waarde is echt veel te lang."

        'Act
        entity.Name = illegalNameValue

        'Assert
        Assert.IsTrue(String.IsNullOrEmpty(entity.Name))
        Assert.IsFalse(String.IsNullOrEmpty(CType(entity, IDataErrorInfo).Error))
    End Sub

    <TestMethod()> <TestCategory("ContentModel")> 
    Public Sub SetNameEmpty_ValidateNameFieldTest()
        'Arrange
        Dim entity As New CustomBankPropertyEntity()
        Dim illegalNameValue As String = ""

        'Act
        entity.Name = illegalNameValue

        'Assert
        Assert.IsTrue(String.IsNullOrEmpty(entity.Name))
        Assert.IsFalse(String.IsNullOrEmpty(CType(entity, IDataErrorInfo).Error))
    End Sub

    <TestMethod()> <TestCategory("ContentModel")> 
    Public Sub SetNameOfOnlyWhitespaces_ValidateNameFieldTest()
        'Arrange
        Dim entity As New CustomBankPropertyEntity()
        Dim illegalNameValue As String = "      "

        'Act
        entity.Name = illegalNameValue

        'Assert
        Assert.IsTrue(String.IsNullOrEmpty(entity.Name))
        Assert.IsFalse(String.IsNullOrEmpty(CType(entity, IDataErrorInfo).Error))
    End Sub

End Class

