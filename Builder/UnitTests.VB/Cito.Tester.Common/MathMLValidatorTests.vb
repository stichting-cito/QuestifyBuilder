
Imports System.Xml.Linq
Imports Cito.Tester.Common
Imports System.Linq

<TestClass()>
Public Class MathMLValidatorTests

    <TestMethod()>
    Public Sub ValidMatML_ValidatesWithoutErrorAndWarnings()
        'Arrange
        Dim mathML As String = _validMathML.ToString()

        'Act
        Dim result As IEnumerable(Of String) = MathMLValidator.ValidateMathML(mathML)

        'Assert
        Assert.AreEqual(0, result.Count())
    End Sub

    <TestMethod()>
    Public Sub ValidMatML_IsValid()
        'Arrange
        Dim mathML As String = _validMathML.ToString()

        'Act
        Dim result As Boolean = MathMLValidator.IsValidMathML(mathML)

        'Assert
        Assert.IsTrue(result)
    End Sub

    <TestMethod()>
    Public Sub ValidMatMLWithUnicode_IsValid()
        'Arrange
        Dim mathML As String = _validMathMLWithUnicode.ToString()

        'Act
        Dim result As Boolean = MathMLValidator.IsValidMathML(mathML)

        'Assert
        Assert.IsTrue(result)
    End Sub

    <TestMethod()>
    Public Sub InValidMatML_GivesErrorAndWarnings()
        'Arrange
        Dim mathML As String = _invalidMathML.ToString()

        'Act
        Dim result As IEnumerable(Of String) = MathMLValidator.ValidateMathML(mathML)

        'Assert
        Assert.AreEqual(1, result.Count())
    End Sub

    <TestMethod()>
    Public Sub InValidMatMLWithSchemaViolation_GivesErrorAndWarnings()
        'Arrange
        Dim mathML As String = _invalidMathMLViolatesSchema.ToString()

        'Act
        Dim result As IEnumerable(Of String) = MathMLValidator.ValidateMathML(mathML)

        'Assert
        Assert.AreEqual(1, result.Count())
    End Sub

    <TestMethod()>
    Public Sub NotMatMLWithNamespace_GivesErrorAndWarnings()
        'Arrange
        Dim mathML As String = _bookXMLWithMathMLNamespace.ToString()

        'Act
        Dim result As IEnumerable(Of String) = MathMLValidator.ValidateMathML(mathML)

        'Assert
        Assert.AreEqual(1, result.Count())
    End Sub

    <TestMethod()>
    Public Sub NotMatMLWithoutNamespace_GivesErrorAndWarnings()
        'Arrange
        Dim mathML As String = _bookXMLWithoutMathMLNamespace.ToString()

        'Act
        Dim result As IEnumerable(Of String) = MathMLValidator.ValidateMathML(mathML)

        'Assert
        Assert.AreEqual(1, result.Count())
    End Sub

    <TestMethod()>
    Public Sub SomeText_GivesErrorAndWarnings()
        'Arrange
        Dim mathML As String = "some text"

        'Act
        Dim result As IEnumerable(Of String) = MathMLValidator.ValidateMathML(mathML)

        'Assert
        Assert.AreEqual(1, result.Count())
    End Sub

    ReadOnly _validMathML As XElement =
        <math xmlns="http://www.w3.org/1998/Math/MathML">
            <msqrt>
                <mn>4</mn>
            </msqrt>
            <mo>-</mo>
            <mn>2</mn>
        </math>

    ReadOnly _invalidMathMLViolatesSchema As XElement =
       <math xmlns="http://www.w3.org/1998/Math/MathML">
           <math></math>
           <msqrt>
               <mn>4</mn>
           </msqrt>
           <mo>-</mo>
           <mn>2</mn>
       </math>

    ReadOnly _validMathMLWithUnicode As XElement =
        <math xmlns="http://www.w3.org/1998/Math/MathML">
            <mrow>
                <mi>a</mi>
                <mo>&#x2062;</mo>
                <msup>
                    <mi>x</mi>
                    <mn>2</mn>
                </msup>
                <mo>+</mo>
                <mi>b</mi>
                <mo>&#x2062;</mo>
                <mi>x</mi>
                <mo>+</mo>
                <mi>c</mi>
            </mrow>
        </math>

    ReadOnly _bookXMLWithMathMLNamespace As XElement =
       <book xmlns="http://www.w3.org/1998/Math/MathML">
           <title name="title"/>
           <author name="someone"/>
       </book>

    ReadOnly _bookXMLWithoutMathMLNamespace As XElement =
       <book>
           <title name="title"/>
           <author name="someone"/>
       </book>

    ReadOnly _invalidMathML As String = _
        "<math xmlns=""http://www.w3.org/1998/Math/MathML"">" + _
        "  <mrow>" + _
        "    <mi>a</mi>" + _
        "    <mo>&InvisibleTimes;</mo>" + _
        "    <msup>" + _
        "      <mi>x</mi>" + _
        "      <mn>2</mn>" + _
        "    </msup>" + _
        "    <mo>+</mo>" + _
        "    <mi>b</mi>" + _
        "    <mo>&InvisibleTimes;</mo>" + _
        "    <mi>x</mi>" + _
        "    <mo>+</mo>" + _
        "    <mi>c</mi>" + _
        "  </mrow>" + _
        "</math>"

End Class
