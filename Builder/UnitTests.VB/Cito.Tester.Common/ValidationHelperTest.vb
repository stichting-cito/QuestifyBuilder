
Imports Cito.Tester.Common

'''<summary>
'''This is a test class for Cito.Tester.Common.ValidationHelper and is intended
'''to contain all Cito.Tester.Common.ValidationHelper Unit Tests
'''</summary>
<TestClass()> _
Public Class ValidationHelperTest

    '''<summary>
    '''Gets or sets the test context which provides
    '''information about and functionality for the current test run.
    '''</summary>
    Public Property TestContext() As TestContext

    '''<summary>
    '''A test for DoesntContainsCharacters(ByVal String, ByVal String)
    '''</summary>
    <TestMethod()> _
    Public Sub ValidationHelper_DoesntContainsCharactersTest()
        Dim falseValidation As String = "Th&sIs@FalseV@lid@tion"
        Dim illegalChars As String = "%!@#$&*()"
        Dim test1result As Boolean = ValidationHelper.DoesntContainsCharacters(falseValidation, illegalChars)
        Assert.IsFalse(test1result, "The first test is expected to be false")

        Dim trueValidation As String = "ThisIsATrueValidation999"
        Dim test2result As Boolean = ValidationHelper.DoesntContainsCharacters(trueValidation, illegalChars)
        Assert.IsTrue(test2result, "The second test is expected to be true")
    End Sub

    '''<summary>
    '''A test for IsBetweenNumericValues(ByVal Decimal, ByVal Decimal, ByVal Decimal)
    '''</summary>
    <TestMethod()> _
    Public Sub ValidationHelper_IsBetweenDecimalNumericValuesTest()
        Dim validation As Decimal = 1203
        Dim test1Result As Boolean = ValidationHelper.IsBetweenNumericValues(validation, 1000, 1300)
        Assert.IsTrue(test1Result, "The first test is expected to return 'true'")

        Dim test2Result As Boolean = ValidationHelper.IsBetweenNumericValues(validation, 1000, 1200)
        Assert.IsFalse(test2Result, "The second test is expected to return 'false'")
    End Sub

    '''<summary>
    '''A test for IsBetweenNumericValues(ByVal Double, ByVal Double, ByVal Double)
    '''</summary>
    <TestMethod()> _
    Public Sub ValidationHelper_IsBetweenDoubleNumericValuesTest()
        Dim validation As Double = 10.487
        Dim test1Result As Boolean = ValidationHelper.IsBetweenNumericValues(validation, 10, 11)
        Assert.IsTrue(test1Result, "The first test is expected to return 'true'")

        Dim test2Result As Boolean = ValidationHelper.IsBetweenNumericValues(validation, 10, 10.4)
        Assert.IsFalse(test2Result, "The second test is expected to return 'false'")
    End Sub

    '''<summary>
    '''A test for IsBetweenNumericValues(ByVal Integer, ByVal Integer, ByVal Integer)
    '''</summary>
    <TestMethod()> _
    Public Sub ValidationHelper_IsBetweenIntegerNumericValuesTest()
        Dim validation As Integer = 1203
        Dim test1Result As Boolean = ValidationHelper.IsBetweenNumericValues(validation, 1000, 1300)
        Assert.IsTrue(test1Result, "The first test is expected to return 'true'")

        Dim test2Result As Boolean = ValidationHelper.IsBetweenNumericValues(validation, 1000, 1200)
        Assert.IsFalse(test2Result, "The second test is expected to return 'false'")
    End Sub

    '''<summary>
    '''A test for IsBetweenNumericValues(ByVal Single, ByVal Single, ByVal Single)
    '''</summary>
    <TestMethod()> _
    Public Sub ValidationHelper_IsBetweenSingleNumericValuesTest()
        Dim validation As Single = 1203
        Dim test1Result As Boolean = ValidationHelper.IsBetweenNumericValues(validation, 1000, 1300)
        Assert.IsTrue(test1Result, "The first test is expected to return 'true'")

        Dim test2Result As Boolean = ValidationHelper.IsBetweenNumericValues(validation, 1000, 1200)
        Assert.IsFalse(test2Result, "The second test is expected to return 'false'")
    End Sub

    '''<summary>
    '''A test for IsFollowingRegexRule(ByVal String, ByVal String)
    '''</summary>
    <TestMethod()> _
    Public Sub ValidationHelper_IsFollowingRegexRuleTest()
        'Regex expression: only numbers and letters
        Dim regexExpression As String = "^[\d\w\s]+$"

        'Strings to validate
        Dim validationTest1 As String = "231232ThisStringShouldReturnTrue23213"
        Dim validationTest2 As String = "ThisStringShouldReturnF@lse"

        Dim test1Result As Boolean = ValidationHelper.IsFollowingRegexRule(validationTest1, regexExpression)
        Assert.IsTrue(test1Result, "The first test is expected to return 'true'")

        Dim test2Result As Boolean = ValidationHelper.IsFollowingRegexRule(validationTest2, regexExpression)
        Assert.IsFalse(test2Result, "The second test is expected to return 'false'")
    End Sub

    '''<summary>
    '''A test for IsNotEmpty(ByVal Object)
    '''</summary>
    <TestMethod()> _
    Public Sub ValidationHelper_IsNotEmptyTest()
        Dim validationTest1 As String = String.Empty
        Dim validationTest2 As String = "NotEmpty"
        Dim emptyObject As Object = Nothing

        Dim test1Result As Boolean = ValidationHelper.IsNotEmpty(validationTest1)
        Assert.IsFalse(test1Result, "First test is expected to return false")

        Dim test2Result As Boolean = ValidationHelper.IsNotEmpty(validationTest2)
        Assert.IsTrue(test2Result, "Second test is expected to return true")

        Dim test3Result As Boolean = ValidationHelper.IsNotEmpty(emptyObject)
        Assert.IsFalse(test3Result, "Third test is expected to return false")
    End Sub

    '''<summary>
    '''A test for IsNumeric(ByVal String)
    '''</summary>
    <TestMethod()> _
    Public Sub ValidationHelper_IsNumericTest()
        Dim validationTest1 As String = "12F"
        Dim validationTest2 As String = "12"

        Dim test1Result As Boolean = ValidationHelper.IsNumeric(validationTest1)
        Assert.IsFalse(test1Result, "First test is expected to return false")

        Dim test2Result As Boolean = ValidationHelper.IsNumeric(validationTest2)
        Assert.IsTrue(test2Result, "Second test is expected to return true")
    End Sub

    <TestMethod>
    Public Sub ValidationHelper_IsValidNCNameTest()
        Dim message As String = String.Empty
        Dim validNCName As String = "valid_123"
        Dim validNCNameWhenPrefixed As String = "123_validWhenPrefixed"
        Dim validWhenSpacesReplaced As String = "123 valid When Spaces Replaced"
        Dim invalidNCName As String = "¡invalid"

        Dim validNCNameResult As Boolean = ValidationHelper.IsValidNCName(validNCName, message)
        Assert.IsTrue(validNCNameResult, "'valid_123' is expected to return true")
        Assert.IsTrue(String.IsNullOrEmpty(message), message)

        Dim validNCNameWhenPrefixedResult As Boolean = ValidationHelper.IsValidNCName(validNCNameWhenPrefixed, message)
        Assert.IsTrue(validNCNameWhenPrefixedResult, "'123_validWhenPrefixed' is expected to return true when prefixed, as is done in the test")
        Assert.IsTrue(String.IsNullOrEmpty(message), message)

        Dim validNCNameWhenSpacesReplacedResult As Boolean = ValidationHelper.IsValidNCName(validWhenSpacesReplaced, message)
        Assert.IsTrue(validNCNameWhenSpacesReplacedResult, "'123 valid When Spaces Replaced' is expected to return true when spaces are replaced, as is done in the test")
        Assert.IsTrue(String.IsNullOrEmpty(message), message)

        Dim invalidNCNameResult As Boolean = ValidationHelper.IsValidNCName(invalidNCName, message)
        Assert.IsFalse(invalidNCNameResult, "'¡invalid' is expected to return false")
        Assert.IsFalse(String.IsNullOrEmpty(message), message)
    End Sub

End Class
