
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports System.ComponentModel

<TestClass()>
Public Class CustomBankPropertyEntityTest
    Inherits GenericTestBase(Of CustomBankPropertyEntity)

    Private Const _replacedCharacter As String = "[_]"

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
        Dim entity As New CustomBankPropertyEntity()
        Dim validNameValue As String = "Deze string is valide"

        entity.Name = validNameValue

        Assert.AreSame(validNameValue, entity.Name)
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub ValidateNameField_IllegalCharacters_Test1()
        Dim entity As New CustomBankPropertyEntity()
        Dim illegalNameValue As String = "Deze string @ bevat# illegale karakters."

        entity.Name = illegalNameValue

        Assert.IsTrue(String.IsNullOrEmpty(entity.Name))
        Assert.IsFalse(String.IsNullOrEmpty(CType(entity, IDataErrorInfo).Error))
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub ValidateNameField_IllegalCharacters_Test2()
        Dim entity As New CustomBankPropertyEntity()
        Dim illegalNameValue As String = "\Deze <string> bevat % 'illegale karakters."

        entity.Name = illegalNameValue

        Assert.IsTrue(String.IsNullOrEmpty(entity.Name))
        Assert.IsFalse(String.IsNullOrEmpty(CType(entity, IDataErrorInfo).Error))
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub ValidateNameField_IllegalCharacters_Test3()
        Dim entity As New CustomBankPropertyEntity()
        Dim illegalNameValue As String = "Deze string $bevat >< (illegale) karakters;"

        entity.Name = illegalNameValue

        Assert.IsTrue(String.IsNullOrEmpty(entity.Name))
        Assert.IsFalse(String.IsNullOrEmpty(CType(entity, IDataErrorInfo).Error))
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub ValidateNameField_IllegalCharacters_Test4()
        Dim entity As New CustomBankPropertyEntity()
        Dim illegalNameValue As String = "Deze {string} bevat ""illegale"" karakters?"

        entity.Name = illegalNameValue

        Assert.IsTrue(String.IsNullOrEmpty(entity.Name))
        Assert.IsFalse(String.IsNullOrEmpty(CType(entity, IDataErrorInfo).Error))
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub ValidateNameField_IllegalCharactersTest5()
        Dim entity As New CustomBankPropertyEntity()
        Dim illegalNameValue As String = "Deze {string} ""bevat"" == illegale karakters!!"

        entity.Name = illegalNameValue

        Assert.IsTrue(String.IsNullOrEmpty(entity.Name))
        Assert.IsFalse(String.IsNullOrEmpty(CType(entity, IDataErrorInfo).Error))
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub TwoSequentialDots_ValidateNameFieldTest()
        Dim entity As New CustomBankPropertyEntity()
        Dim illegalNameValue As String = "Een name met daarin twee .. achter elkaar"

        entity.Name = illegalNameValue

        Assert.IsTrue(String.IsNullOrEmpty(entity.Name))
        Assert.IsFalse(String.IsNullOrEmpty(CType(entity, IDataErrorInfo).Error))
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub MultipleSequentialDots_ValidateNameFieldTest()
        Dim entity As New CustomBankPropertyEntity()
        Dim illegalNameValue As String = "Een name met daarin meerdere .... achter elkaar"

        entity.Name = illegalNameValue

        Assert.IsTrue(String.IsNullOrEmpty(entity.Name))
        Assert.IsFalse(String.IsNullOrEmpty(CType(entity, IDataErrorInfo).Error))
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    <ExpectedException(GetType(ArgumentOutOfRangeException))>
    Public Sub StringTooLong_ValidateNameFieldTest()
        Dim entity As New CustomBankPropertyEntity()
        Dim illegalNameValue As String = "Ja zo zijn we niet getrouwd natuurlijk want deze waarde is echt veel te lang."

        entity.Name = illegalNameValue

        Assert.IsTrue(String.IsNullOrEmpty(entity.Name))
        Assert.IsFalse(String.IsNullOrEmpty(CType(entity, IDataErrorInfo).Error))
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub SetNameEmpty_ValidateNameFieldTest()
        Dim entity As New CustomBankPropertyEntity()
        Dim illegalNameValue As String = ""

        entity.Name = illegalNameValue

        Assert.IsTrue(String.IsNullOrEmpty(entity.Name))
        Assert.IsFalse(String.IsNullOrEmpty(CType(entity, IDataErrorInfo).Error))
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub SetNameOfOnlyWhitespaces_ValidateNameFieldTest()
        Dim entity As New CustomBankPropertyEntity()
        Dim illegalNameValue As String = "      "

        entity.Name = illegalNameValue

        Assert.IsTrue(String.IsNullOrEmpty(entity.Name))
        Assert.IsFalse(String.IsNullOrEmpty(CType(entity, IDataErrorInfo).Error))
    End Sub

End Class

