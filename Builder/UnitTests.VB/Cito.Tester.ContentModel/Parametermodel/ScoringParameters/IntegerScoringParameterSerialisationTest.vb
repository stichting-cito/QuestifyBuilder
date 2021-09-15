
Imports Cito.Tester.ContentModel

<TestClass>
Public Class IntegerScoringParameterSerialisationTest
    Inherits SerializationTestBase

    <TestMethod()> <TestCategory("ContentModel"), TestCategory("ScoringParameter")>
    Public Sub CompareWithPreviouslyKnowState()
        'Arrange
        Dim scorePrm = New IntegerScoringParameter() With {.MaxLength = 5, .ControllerId = "IntPrm"}
        
        'Act
        Dim result = DoSerialize(Of IntegerScoringParameter)(scorePrm)
       
        'Assert
        'Compare with previously known result 
        Assert.AreEqual(<IntegerScoringParameter
                            xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
                            xmlns:xsd="http://www.w3.org/2001/XMLSchema"
                            ControllerId="IntPrm"
                            maxLength="5"
                        />.ToString(), result.ToString())
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
                                  <integerScoringParameter maxLength="5"/>
                              </parameterSet>
                          </parameters>
                      </assessmentItem>
       
        'Act
        Dim result = Deserialize(Of AssessmentItem)(xmlData)
       
        'Assert
        Assert.IsInstanceOfType(result.Parameters(0).InnerParameters(0), GetType(IntegerScoringParameter))
        Assert.AreEqual(5, DirectCast(result.Parameters(0).InnerParameters(0), IntegerScoringParameter).MaxLength)
    End Sub
End Class
