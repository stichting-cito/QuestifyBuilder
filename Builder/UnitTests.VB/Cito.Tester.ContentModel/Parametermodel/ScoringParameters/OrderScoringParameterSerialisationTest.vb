
Imports Cito.Tester.ContentModel

<TestClass()>
Public Class OrderScoringParameterSerialisationTest
    Inherits SerializationTestBase

    <TestMethod()> <TestCategory("ContentModel"), TestCategory("ScoringParameter")>
    Public Sub CompareWithPreviouslyKnowState()
        'Arrange
        Dim orderPrm = New OrderScoringParameter() With {.MaxChoices = 2, .MinChoices = 1, .ControllerId = "ordercontroller_1"}
      
        'Act
        Dim result = DoSerialize(Of OrderScoringParameter)(orderPrm)
     
        'Assert
        'Compare with previously known result 
        Assert.AreEqual(<OrderScoringParameter
                            xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
                            xmlns:xsd="http://www.w3.org/2001/XMLSchema"
                            ControllerId="ordercontroller_1"
                            minChoices="1"
                            maxChoices="2"/>.ToString(), result.ToString())
    End Sub

    <TestMethod()> <TestCategory("ContentModel"), TestCategory("ScoringParameter")>
    Public Sub Deserialize_Test()
        'Arrange
        Dim xmlData = <OrderScoringParameter
                          xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
                          xmlns:xsd="http://www.w3.org/2001/XMLSchema"
                          ControllerId="ordercontroller_1"
                          minChoices="1"
                          maxChoices="2"
                      />
      
        'Act
        Dim result = Deserialize(Of OrderScoringParameter)(xmlData)
       
        'Assert
        Assert.AreEqual(result.ControllerId, "ordercontroller_1")
        Assert.AreEqual(result.MinChoices, 1)
        Assert.AreEqual(result.MaxChoices, 2)
    End Sub


    <TestMethod()> <TestCategory("ContentModel"), TestCategory("ScoringParameter")>
    Public Sub SerializeOrderScoringParameter_inAssessmentItem_CompareWithPreviouslyKnownResult_Test()
        'Arrange
        Dim a = New AssessmentItem With
                {.Title = "someTitle", .Identifier = "someIdentifier", .LayoutTemplateSourceName = "someIlt"}
        Dim p = a.Parameters.AddNew()
        p.Id = "id_1"
        p.InnerParameters.Add(New OrderScoringParameter() With {.MaxChoices = 2, .MinChoices = 1, .ControllerId = "ordercontroller_1"})
      
        'Act
        Dim result = DoSerialize(Of AssessmentItem)(a)
      
        'Assert
        Assert.AreEqual(<assessmentItem xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" identifier="someIdentifier" title="someTitle" layoutTemplateSrc="someIlt">
                            <solution>
                                <keyFindings/>
                                <aspectReferences/>
                            </solution>
                            <parameters>
                                <parameterSet id="id_1">
                                    <orderScoringParameter ControllerId="ordercontroller_1" minChoices="1" maxChoices="2"/>
                                </parameterSet>
                            </parameters>
                        </assessmentItem>.ToString(), result.ToString())
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
                                  <orderScoringParameter ControllerId="ordercontroller_1" minChoices="1" maxChoices="2"/>
                              </parameterSet>
                          </parameters>
                      </assessmentItem>
      
        'Act
        Dim result = Deserialize(Of AssessmentItem)(xmlData)
      
        'Assert
        Assert.IsInstanceOfType(result.Parameters(0).InnerParameters(0), GetType(OrderScoringParameter))
    End Sub

End Class
