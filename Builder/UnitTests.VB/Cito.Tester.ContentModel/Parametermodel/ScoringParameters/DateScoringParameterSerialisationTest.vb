
Imports Cito.Tester.ContentModel

<TestClass>
Public Class DateScoringParameterSerialisationTest
    Inherits SerializationTestBase

    <TestMethod()> <TestCategory("ContentModel"), TestCategory("ScoringParameter")>
    Public Sub CompareWithPreviouslyKnowState()
        Dim scorePrm = New DateScoringParameter() With {.ControllerId = "DatPrm", .DateFormat = "dd/MM/yyyy"}

        Dim result = DoSerialize(Of DateScoringParameter)(scorePrm)

        Assert.AreEqual(<DateScoringParameter
                            xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
                            xmlns:xsd="http://www.w3.org/2001/XMLSchema"
                            ControllerId="DatPrm"
                            dateFormat="dd/MM/yyyy"
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
                                  <dateScoringParameter dateFormat="dd/MM/yyyy"/>
                              </parameterSet>
                          </parameters>
                      </assessmentItem>

        Dim result = Deserialize(Of AssessmentItem)(xmlData)

        Assert.IsInstanceOfType(result.Parameters(0).InnerParameters(0), GetType(DateScoringParameter))
        Assert.AreEqual("dd/MM/yyyy", DirectCast(result.Parameters(0).InnerParameters(0), DateScoringParameter).DateFormat)
    End Sub
End Class
