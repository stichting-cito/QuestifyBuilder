﻿
Imports Cito.Tester.ContentModel

<TestClass()>
Public Class MultiChoiceScoringParameterSerialisationTest
    Inherits SerializationTestBase

    <TestMethod()> <TestCategory("ContentModel"), TestCategory("ScoringParameter")>
    Public Sub CompareWithPreviouslyKnowState()
        'Arrange
        Dim multiPrm = New MultiChoiceScoringParameter() With {.MaxChoices = 2, .MinChoices = 1, .MultiChoice = MultiChoiceType.Check, .ControllerId = "mc_1"}
    
        'Act
        Dim result = DoSerialize(Of MultiChoiceScoringParameter)(multiPrm)
    
        'Assert
        'Compare with previously known result 
        Assert.AreEqual(<MultiChoiceScoringParameter
                            xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
                            xmlns:xsd="http://www.w3.org/2001/XMLSchema"
                            ControllerId="mc_1"
                            minChoices="1"
                            maxChoices="2"
                            multiChoice="Check"/>.ToString(), result.ToString())
    End Sub

    <TestMethod()> <TestCategory("ContentModel"), TestCategory("ScoringParameter")>
    Public Sub Deserialize_Test()
        'Arrange
        Dim xmlData = <MultiChoiceScoringParameter
                          xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
                          xmlns:xsd="http://www.w3.org/2001/XMLSchema"
                          ControllerId="mc_1"
                          minChoices="1"
                          maxChoices="2"
                          multiChoice="Radio"/>
      
        'Act
        Dim result = Deserialize(Of MultiChoiceScoringParameter)(xmlData)
      
        'Assert
        Assert.AreEqual(result.ControllerId, "mc_1")
        Assert.AreEqual(result.MinChoices, 1)
        Assert.AreEqual(result.MaxChoices, 2)
        Assert.AreEqual(result.MultiChoice, MultiChoiceType.Radio)
    End Sub

    <TestMethod()> <TestCategory("ContentModel"), TestCategory("ScoringParameter")>
    Public Sub SerializeMultiChoice_inAssessmentItem_CompareWithPreviouslyKnownResult_Test()
        'Arrange
        Dim a = New AssessmentItem With
                {.Title = "someTitle", .Identifier = "someIdentifier", .LayoutTemplateSourceName = "someIlt"}
        Dim p = a.Parameters.AddNew()
        p.Id = "id_1"
        p.InnerParameters.Add(New MultiChoiceScoringParameter() With {.MaxChoices = 2, .MinChoices = 1, .MultiChoice = MultiChoiceType.Check, .ControllerId = "mc_1"})
       
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
                                    <multichoicescoringparameter ControllerId="mc_1" minChoices="1" maxChoices="2" multiChoice="Check"/>
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
                                  <multichoicescoringparameter ControllerId="mc_1" minChoices="1" maxChoices="2" multiChoice="Check"/>
                              </parameterSet>
                          </parameters>
                      </assessmentItem>
     
        'Act
        Dim result = Deserialize(Of AssessmentItem)(xmlData)
        
        'Assert
        Assert.IsInstanceOfType(result.Parameters(0).InnerParameters(0), GetType(MultiChoiceScoringParameter))
    End Sub

End Class
