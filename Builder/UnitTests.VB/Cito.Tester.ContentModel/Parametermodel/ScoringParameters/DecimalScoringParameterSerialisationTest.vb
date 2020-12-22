
Imports Cito.Tester.ContentModel

<TestClass>
Public Class DecimalScoringParameterSerialisationTest
    Inherits SerializationTestBase

    <TestMethod()> <TestCategory("ContentModel"), TestCategory("ScoringParameter")>
    Public Sub CompareWithPreviouslyKnowState()
        Dim scorePrm = New DecimalScoringParameter() With {.IntegerPartMaxLength = 3, .FractionPartMaxLength = 2, .ControllerId = "DecPrm"}

        Dim result = DoSerialize(Of DecimalScoringParameter)(scorePrm)

        Assert.AreEqual(<DecimalScoringParameter
                            xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
                            xmlns:xsd="http://www.w3.org/2001/XMLSchema"
                            ControllerId="DecPrm"
                            integerPartMaxLength="3"
                            fractionPartMaxLength="2"
                        />.ToString(), result.ToString())
    End Sub

    <TestMethod()> <TestCategory("ContentModel"), TestCategory("ScoringParameter")>
    Public Sub Deserialize_InAssessmentItem_Test()
        Dim xmlData = <assessmentItem xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" identifier="someIdentifier" title="someTitle" layoutTemplateSrc="someIlt">
                          <solution>
                              <keyFindings/>
                              <aspectReferences/>
                          </solution>
                          <parameters>
                              <parameterSet id="id_1">
                                  <decimalScoringParameter integerPartMaxLength="3" fractionPartMaxLength="2"/>
                              </parameterSet>
                          </parameters>
                      </assessmentItem>

        Dim result = Deserialize(Of AssessmentItem)(xmlData)

        Assert.IsInstanceOfType(result.Parameters(0).InnerParameters(0), GetType(DecimalScoringParameter))
        Assert.AreEqual(3, DirectCast(result.Parameters(0).InnerParameters(0), DecimalScoringParameter).IntegerPartMaxLength)
        Assert.AreEqual(2, DirectCast(result.Parameters(0).InnerParameters(0), DecimalScoringParameter).FractionPartMaxLength)
    End Sub

End Class
