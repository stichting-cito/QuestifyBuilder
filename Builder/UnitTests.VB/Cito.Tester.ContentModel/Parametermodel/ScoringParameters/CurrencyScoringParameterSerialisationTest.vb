
Imports System.Xml.Linq
Imports Cito.Tester.ContentModel
Imports Questify.Builder.UnitTests.Framework

<TestClass>
Public Class CurrencyScoringParameterSerialisationTest
    Inherits SerializationTestBase

    <TestMethod()> <TestCategory("ContentModel"), TestCategory("ScoringParameter")>
    Public Sub CompareWithPreviouslyKnowState()
        'Arrange
        Dim scorePrm = New CurrencyScoringParameter() With {.IntegerPartMaxLength = 3, .FractionPartMaxLength = 2, .CurrencyCulture = "nl-NL", .ControllerId = "CurPrm"}
      
        'Act
        Dim result = DoSerialize(Of CurrencyScoringParameter)(scorePrm)
       
        'Assert
        'Compare with previously known result 
        Dim expected As XElement = <CurrencyScoringParameter
                            xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
                            xmlns:xsd="http://www.w3.org/2001/XMLSchema"
                            ControllerId="CurPrm"
                            currencyCulture="nl-NL"
                            integerPartMaxLength="3"
                            fractionPartMaxLength="2"
                        />

        UnitTestHelper.AreSame(expected, result)
    End Sub

    <TestMethod()> <TestCategory("ContentModel"), TestCategory("ScoringParameter")>
    Public Sub Deserialize_InAssessmentItem_Test()
        'Arrange
        Dim xmlData = <assessmentItem xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" identifier="someIdentifier" title="someTitle" layoutTemplateSrc="someIlt">
                          <solution>
                              <keyFindings/>
                              <aspectReferences/>
                          </solution>
                          <parameters>
                              <parameterSet id="id_1">
                                  <currencyScoringParameter integerPartMaxLength="3" fractionPartMaxLength="2" currencyCulture="nl-NL"/>
                              </parameterSet>
                          </parameters>
                      </assessmentItem>
     
        'Act
        Dim result = Deserialize(Of AssessmentItem)(xmlData)
       
        'Assert
        Assert.IsInstanceOfType(result.Parameters(0).InnerParameters(0), GetType(CurrencyScoringParameter))
        Assert.AreEqual(3, DirectCast(result.Parameters(0).InnerParameters(0), CurrencyScoringParameter).IntegerPartMaxLength)
        Assert.AreEqual(2, DirectCast(result.Parameters(0).InnerParameters(0), CurrencyScoringParameter).FractionPartMaxLength)
        Assert.AreEqual("nl-NL", DirectCast(result.Parameters(0).InnerParameters(0), CurrencyScoringParameter).CurrencyCulture)
    End Sub

End Class
