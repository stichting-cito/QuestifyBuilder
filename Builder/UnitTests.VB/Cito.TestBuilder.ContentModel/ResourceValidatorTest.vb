Imports Questify.Builder.Model.ContentModel.ValidatorClasses

<TestClass()>
Public Class ResourceValidatorTest

    Private Const _replacedCharacter As String = "[_]"

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub ReplaceIllegalCharactersTest1()
        Dim illegalNameValue As String = "Deze string @ bevat# illegale karakters."

        Dim result As String = ResourceValidator.ReplaceIllegalCharacters(illegalNameValue)

        Assert.IsTrue(result.IndexOfAny("_".ToCharArray()) > -1)
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub ReplaceIllegalCharactersTest2()
        Dim illegalNameValue As String = "\Deze <string> bevat % 'illegale karakters."

        Dim result As String = ResourceValidator.ReplaceIllegalCharacters(illegalNameValue)

        Assert.IsTrue(result.IndexOfAny("_".ToCharArray()) > -1)
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub ReplaceIllegalCharactersTest3()
        Dim illegalNameValue As String = "Deze string $bevat >< (illegale) karakters;"

        Dim result As String = ResourceValidator.ReplaceIllegalCharacters(illegalNameValue)

        Assert.IsTrue(result.IndexOfAny("_".ToCharArray()) > -1)
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub ReplaceIllegalCharactersTest4()
        Dim illegalNameValue As String = "Deze {string} bevat ""illegale"" karakters?"

        Dim result As String = ResourceValidator.ReplaceIllegalCharacters(illegalNameValue)

        Assert.IsTrue(result.IndexOfAny("_".ToCharArray()) > -1)
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub ReplaceIllegalCharactersTest5()
        Dim illegalNameValue As String = "Deze {string} ""bevat"" == illegale karakters!!"

        Dim result As String = ResourceValidator.ReplaceIllegalCharacters(illegalNameValue)

        Assert.IsTrue(result.IndexOfAny("_".ToCharArray()) > -1)
    End Sub

End Class
