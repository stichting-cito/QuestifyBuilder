﻿
Imports System.Xml.Linq
Imports Cito.Tester.ContentModel

<TestClass>
Public Class HotspotScoringParameterSerialisationTest : Inherits SerializationTestBase

    <TestMethod()> <TestCategory("ContentModel"), TestCategory("ScoringParameter")>
    Public Sub HotspotScoringParameterShouldBeMultiChoice()
        'Hotspot scoringparameter should always be treating its scores as a multiple response item (in other words: boolean keyvalues).
        'That's because users have the ability to change the number of alternatives, which could cause a change from a MC-item (having strings as keyvalues)
        'to a MR-item (having booleans as keyvalues). That would also change the UI of the scoring editor in QB.

        'Arrange
        Dim hotSpotSp = New HotspotScoringParameter() With {.Name = "HotSpot1", .FindingOverride = "finding", .MinChoices = 1, .MaxChoices = 1}
        hotSpotSp.Value = New ParameterSetCollection()
        hotSpotSp.Value.Add(New ParameterCollection() With {.Id = "A"})
        hotSpotSp.Value.Add(New ParameterCollection() With {.Id = "B"})
       
        'Act
        Dim result = hotSpotSp.IsSingleChoice
       
        'Assert
        Assert.IsFalse(result, "Hotspot scoringparameter should always be treated as a multiple response item")
    End Sub

    <TestMethod()> <TestCategory("ContentModel"), TestCategory("ScoringParameter")>
    Public Sub CompareWithPreviouslyKnowState()
        'Arrange
        Dim hsPrm = New HotspotScoringParameter() With {.MaxChoices = 2, .MinChoices = 1, .ControllerId = "hs_1"}
       
        'Act
        Dim result = DoSerialize(Of HotspotScoringParameter)(hsPrm)
      
        'Assert
        'Compare with previously known result 
        Assert.AreEqual(<HotspotScoringParameter
                            xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
                            xmlns:xsd="http://www.w3.org/2001/XMLSchema"
                            ControllerId="hs_1"
                            minChoices="1"
                            maxChoices="2"/>.ToString(), result.ToString())
    End Sub

    <TestMethod()> <TestCategory("ContentModel"), TestCategory("ScoringParameter")>
    Public Sub Deserialize_Test()
        'Arrange

        'Act
        Dim result = Deserialize(Of HotspotScoringParameter)(_serializedHotspotParameter)

        'Assert
        'Image
        Assert.AreEqual("clickableImage.jpg", CType(result.Area.Value(0).InnerParameters(0), ResourceParameter).Value)

        'Hotspots
        Assert.AreEqual(2, result.Area.ShapeList.Count)
        Assert.AreEqual("A", result.Area.ShapeList(0).Identifier)
        Assert.AreEqual("A", result.Area.ShapeList(0).Label)

        'Alternatives
        Assert.AreEqual(2, result.Value.Count) 'Must be equal to the number of shapes in the shapelist of the area parameter.
    End Sub


    <TestMethod()> <TestCategory("ContentModel"), TestCategory("ScoringParameter")>
    Public Sub SerializeHotspot_inAssessmentItem_CompareWithPreviouslyKnownResult_Test()
        'Arrange
        Dim a = New AssessmentItem With
                {.Title = "someTitle", .Identifier = "someIdentifier", .LayoutTemplateSourceName = "someIlt"}
        Dim p = a.Parameters.AddNew()
        p.Id = "id_1"
        p.InnerParameters.Add(New HotspotScoringParameter() With {.MaxChoices = 2, .MinChoices = 1, .ControllerId = "hs_1", .Area = New AreaParameter() With {.Name = "areaparameter"}})
       
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
                                    <hotspotScoringParameter ControllerId="hs_1" minChoices="1" maxChoices="2">
                                        <areaparameter name="areaparameter">
                                            <Shapes/>
                                        </areaparameter>
                                    </hotspotScoringParameter>
                                </parameterSet>
                            </parameters>
                        </assessmentItem>.ToString(), result.ToString())
    End Sub

    <TestMethod()> <TestCategory("ContentModel"), TestCategory("ScoringParameter")>
    Public Sub Deserialize_InAssessmentItem_Test()
        'Arrange

        'Act
        Dim result = Deserialize(Of AssessmentItem)(_serializedHotspotItem)
        
        'Assert
        Assert.IsInstanceOfType(result.Parameters(0).InnerParameters(0), GetType(HotspotScoringParameter))
    End Sub


    Private ReadOnly _serializedHotspotParameter As XElement = <HotspotScoringParameter xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" minChoices="1" maxChoices="2" ControllerId="hs_1">
                                                                   <subparameterset id="A"/>
                                                                   <subparameterset id="B"/>
                                                                   <subparameterset id="C"/>
                                                                   <definition id="">
                                                                   </definition>
                                                                   <areaparameter>
                                                                       <subparameterset id="A">
                                                                           <resourceparameter name="clickableImage">clickableImage.jpg</resourceparameter>
                                                                       </subparameterset>
                                                                       <definition id="">
                                                                           <resourceparameter name="clickableImage"/>
                                                                       </definition>
                                                                       <Shapes>
                                                                           <Rectangle id="A" label="A">
                                                                               <TopLeft>
                                                                                   <X>33</X>
                                                                                   <Y>46</Y>
                                                                               </TopLeft>
                                                                               <BottomRight>
                                                                                   <X>155</X>
                                                                                   <Y>85</Y>
                                                                               </BottomRight>
                                                                           </Rectangle>
                                                                           <Rectangle id="B" label="B">
                                                                               <TopLeft>
                                                                                   <X>352</X>
                                                                                   <Y>47</Y>
                                                                               </TopLeft>
                                                                               <BottomRight>
                                                                                   <X>473</X>
                                                                                   <Y>85</Y>
                                                                               </BottomRight>
                                                                           </Rectangle>
                                                                       </Shapes>
                                                                   </areaparameter>
                                                               </HotspotScoringParameter>


    Private ReadOnly _serializedHotspotItem As XElement = <assessmentItem xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" identifier="TI_GapMatch" title="TI_GapMatch" layoutTemplateSrc="Cito.Generic.GraphicGapMatch.SC">
                                                              <solution>
                                                                  <keyFindings>
                                                                      <keyFinding id="hs_1" scoringMethod="Dichotomous">
                                                                          <keyFact id="A" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                              <keyValue occur="1">
                                                                                  <stringValue>
                                                                                      <typedValue>A</typedValue>
                                                                                  </stringValue>
                                                                              </keyValue>
                                                                          </keyFact>
                                                                      </keyFinding>
                                                                  </keyFindings>
                                                                  <aspectReferences/>
                                                                  <ItemScoreTranslationTable>
                                                                      <ItemScoreTranslationTableEntry rawScore="0" translatedScore="0"/>
                                                                      <ItemScoreTranslationTableEntry rawScore="1" translatedScore="1"/>
                                                                  </ItemScoreTranslationTable>
                                                              </solution>
                                                              <parameters>
                                                                  <parameterSet id="entireItem">
                                                                      <hotspotScoringParameter ControllerId="hs_1">
                                                                          <subparameterset id="A"/>
                                                                          <subparameterset id="B"/>
                                                                          <subparameterset id="C"/>
                                                                          <definition id="">
                                                                          </definition>
                                                                          <areaparameter>
                                                                              <subparameterset id="A">
                                                                                  <resourceparameter name="clickableImage">clickableImage.jpg</resourceparameter>
                                                                              </subparameterset>
                                                                              <definition id="">
                                                                                  <resourceparameter name="clickableImage"/>
                                                                              </definition>
                                                                              <Shapes>
                                                                                  <Rectangle id="A" label="A">
                                                                                      <TopLeft>
                                                                                          <X>33</X>
                                                                                          <Y>46</Y>
                                                                                      </TopLeft>
                                                                                      <BottomRight>
                                                                                          <X>155</X>
                                                                                          <Y>85</Y>
                                                                                      </BottomRight>
                                                                                  </Rectangle>
                                                                                  <Rectangle id="B" label="B">
                                                                                      <TopLeft>
                                                                                          <X>352</X>
                                                                                          <Y>47</Y>
                                                                                      </TopLeft>
                                                                                      <BottomRight>
                                                                                          <X>473</X>
                                                                                          <Y>85</Y>
                                                                                      </BottomRight>
                                                                                  </Rectangle>
                                                                                  <Rectangle id="C" label="C">
                                                                                      <TopLeft>
                                                                                          <X>500</X>
                                                                                          <Y>85</Y>
                                                                                      </TopLeft>
                                                                                      <BottomRight>
                                                                                          <X>625</X>
                                                                                          <Y>125</Y>
                                                                                      </BottomRight>
                                                                                  </Rectangle>
                                                                              </Shapes>
                                                                          </areaparameter>
                                                                      </hotspotScoringParameter>
                                                                  </parameterSet>
                                                              </parameters>
                                                          </assessmentItem>

End Class
