
Imports Cito.Tester.ContentModel

<TestClass>
Public Class MathScoringSerialisationParameterTest
    Inherits SerializationTestBase

    <TestMethod()> <TestCategory("ContentModel"), TestCategory("ScoringParameter")>
    Public Sub CompareWithPreviouslyKnowState()
        Dim scorePrm = New MathScoringParameter() With {.ControllerId = "MathPrm"}

        Dim result = DoSerialize(Of MathScoringParameter)(scorePrm)

        Assert.AreEqual(<MathScoringParameter
                            xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
                            xmlns:xsd="http://www.w3.org/2001/XMLSchema"
                            ControllerId="MathPrm"
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
                                  <mathScoringParameter/>
                              </parameterSet>
                          </parameters>
                      </assessmentItem>

        Dim result = Deserialize(Of AssessmentItem)(xmlData)

        Assert.IsInstanceOfType(result.Parameters(0).InnerParameters(0), GetType(MathScoringParameter))
    End Sub
End Class
