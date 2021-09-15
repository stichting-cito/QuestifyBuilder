
Imports System.Xml.Linq
Imports Cito.Tester.ContentModel

<TestClass()>
Public Class HotTextScoringParameterSerialisationTest
    Inherits SerializationTestBase

    <TestMethod()> <TestCategory("ContentModel"), TestCategory("ScoringParameter")>
    Public Sub CompareWithPreviouslyKnowState()
        'Arrange
        Dim hotTextPrm = New HotTextScoringParameter() With {.MaxChoices = 2, .MinChoices = 1, .ControllerId = "hottextcontroller_1", .HotTextText = New XHtmlParameter() With {.Name = "hottexttext", .Value = "<p>What's this?</p>"}}
      
        'Act
        Dim result = DoSerialize(Of HotTextScoringParameter)(hotTextPrm)
      
        'Assert
        'Compare with previously known result 
        Assert.AreEqual(<HotTextScoringParameter
                            xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
                            xmlns:xsd="http://www.w3.org/2001/XMLSchema"
                            ControllerId="hottextcontroller_1"
                            minChoices="1"
                            maxChoices="2"
                            multiChoice="Check"
                            isCorrectionVariant="false"/>.ToString(), result.ToString())
    End Sub

    <TestMethod()> <TestCategory("ContentModel"), TestCategory("ScoringParameter")>
    Public Sub Deserialize_Test()
        'Arrange
        Dim xmlData = <HotTextScoringParameter
                          xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
                          xmlns:xsd="http://www.w3.org/2001/XMLSchema"
                          ControllerId="hottextcontroller_1"
                          minChoices="1"
                          maxChoices="2"
                          >
                          <xhtmlparameter name="hottexttext">
                              <p>
                                  <cito:InlineElement id="Id213789c-6ee7-45ee-a024-0b10a10f6375" layoutTemplateSourceName="InlineHottextLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                      <cito:parameters>
                                          <cito:parameterSet id="entireItem">
                                              <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                              <cito:plaintextparameter name="controlId">Id213789c-6ee7-45ee-a024-0b10a10f6375</cito:plaintextparameter>
                                              <cito:plaintextparameter name="controlLabel">Ruim 100 jaar lang werden alle huizen en kantoren ...</cito:plaintextparameter>
                                              <cito:booleanparameter name="addHottextCorrection">False</cito:booleanparameter>
                                              <cito:plaintextparameter name="hottextValue"/>
                                          </cito:parameterSet>
                                      </cito:parameters>
                                  </cito:InlineElement>
                                  <cito:InlineElement id="Ia1384693-9aac-4a5d-bb43-9c2b262e864a" layoutTemplateSourceName="InlineHottextLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                      <cito:parameters>
                                          <cito:parameterSet id="entireItem">
                                              <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                              <cito:plaintextparameter name="controlId">Ia1384693-9aac-4a5d-bb43-9c2b262e864a</cito:plaintextparameter>
                                              <cito:plaintextparameter name="controlLabel"> De lampen waren niet duur en waren gemakkelijk in...</cito:plaintextparameter>
                                              <cito:booleanparameter name="addHottextCorrection">False</cito:booleanparameter>
                                              <cito:plaintextparameter name="hottextValue"/>
                                          </cito:parameterSet>
                                      </cito:parameters>
                                  </cito:InlineElement>
                              </p>
                          </xhtmlparameter>

                          <subparameterset id="Id213789c-6ee7-45ee-a024-0b10a10f6375">
                              <plaintextparameter name="controlLabel">Ruim 100 jaar lang werden alle huizen en kantoren ...</plaintextparameter>
                          </subparameterset>
                          <subparameterset id="Ia1384693-9aac-4a5d-bb43-9c2b262e864a">
                              <plaintextparameter name="controlLabel"> De lampen waren niet duur en waren gemakkelijk in...</plaintextparameter>
                          </subparameterset>
                          <definition id="">
                              <plaintextparameter name="controlLabel"/>
                          </definition>

                      </HotTextScoringParameter>
        
        'Act
        Dim result = Deserialize(Of HotTextScoringParameter)(xmlData)
        
        'Assert
        Assert.AreEqual("hottextcontroller_1", result.ControllerId)
        Assert.AreEqual(1, result.MinChoices)
        Assert.AreEqual(2, result.MaxChoices)
        Assert.AreEqual("Ia1384693-9aac-4a5d-bb43-9c2b262e864a", result.Value(1).Id)
        Assert.AreEqual(" De lampen waren niet duur en waren gemakkelijk in...", DirectCast(result.Value(1).InnerParameters(0), PlainTextParameter).Value)
    End Sub


    <TestMethod()> <TestCategory("ContentModel"), TestCategory("ScoringParameter")>
    Public Sub Serialize_Test()
        'Arrange
        Dim scorePar As New HotTextScoringParameter() With {.HotTextText = New XHtmlParameter() With {.Name = "hottexttext"}}

        'Act
        Dim result = DoSerialize(Of HotTextScoringParameter)(scorePar)

        'Assert
        Assert.AreEqual(<HotTextScoringParameter xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" minChoices="0" maxChoices="0" multiChoice="Check" isCorrectionVariant="false"/>.ToString(), result.ToString())
    End Sub

    <TestMethod()> <TestCategory("ContentModel"), TestCategory("ScoringParameter")>
    Public Sub SerializeHotTextScoringParameter_inAssessmentItem_CompareWithPreviouslyKnownResult_Test()
        'Arrange
        Dim a = New AssessmentItem With
                {.Title = "someTitle", .Identifier = "someIdentifier", .LayoutTemplateSourceName = "someIlt"}
        Dim p = a.Parameters.AddNew()
        p.Id = "id_1"
        p.InnerParameters.Add(New HotTextScoringParameter() With {.MaxChoices = 2, .MinChoices = 1, .ControllerId = "hottextcontroller_1", .HotTextText = New XHtmlParameter() With {.Value = "<p>What's this?</p>"}, .IsCorrectionVariant = False})

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
                                    <hotTextScoringParameter ControllerId="hottextcontroller_1" minChoices="1" maxChoices="2" multiChoice="Check" isCorrectionVariant="false"/>
                                </parameterSet>
                            </parameters>
                        </assessmentItem>.ToString(), result.ToString())
    End Sub

    <TestMethod()> <TestCategory("ContentModel"), TestCategory("ScoringParameter")>
    Public Sub Deserialize_InAssessmentItem_Test()
        'Arrange  
        
        'Act
        Dim result = Deserialize(Of AssessmentItem)(xmlData)
        
        'Assert
        Assert.IsInstanceOfType(result.Parameters(0).InnerParameters(0), GetType(HotTextScoringParameter))
    End Sub

    <TestMethod()> <TestCategory("ContentModel"), TestCategory("ScoringParameter")>
    Public Sub GetLabelNoPlainTextPrm_ShouldBeEmpty()
        'Arrange
        Dim assessment = Deserialize(Of AssessmentItem)(xmlData)
        Dim sp = DirectCast(assessment.Parameters(0).InnerParameters(0), HotTextScoringParameter)

        'Act        
        Dim result = sp.GetLabelFor("test")
     
        'Assert                            
        Assert.AreEqual(String.Empty, result)
    End Sub

    <TestMethod()> <TestCategory("ContentModel"), TestCategory("ScoringParameter")>
    Public Sub GetLabelPlainTextPrm_ShouldBeEmpty()
        'Arrange
        Dim assessment = Deserialize(Of AssessmentItem)(xmlData2)
        Dim sp = DirectCast(assessment.Parameters(0).InnerParameters(0), HotTextScoringParameter)

        'Act        
        Dim result = sp.GetLabelFor("test")
       
        'Assert                            
        Assert.AreEqual("test.", result)
    End Sub

    Dim xmlData As XElement = <assessmentItem xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" identifier="someIdentifier" title="someTitle" layoutTemplateSrc="someIlt">
                                  <solution>
                                      <keyFindings/>
                                      <aspectReferences/>
                                  </solution>
                                  <parameters>
                                      <parameterSet id="id_1">
                                          <hotTextScoringParameter ControllerId="hottextcontroller_1" minChoices="1" maxChoices="2" multiChoice="Check">
                                              <subparameterset id="test">
                                              </subparameterset>
                                          </hotTextScoringParameter>
                                      </parameterSet>
                                  </parameters>
                              </assessmentItem>

    Dim xmlData2 As XElement = <assessmentItem xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" identifier="someIdentifier" title="someTitle" layoutTemplateSrc="someIlt">
                                   <solution>
                                       <keyFindings/>
                                       <aspectReferences/>
                                   </solution>
                                   <parameters>
                                       <parameterSet id="id_1">
                                           <hotTextScoringParameter ControllerId="hottextcontroller_1" minChoices="1" maxChoices="2" multiChoice="Check">
                                               <subparameterset id="test">
                                                   <plaintextparameter name="contentLabel">test.</plaintextparameter>
                                               </subparameterset>
                                           </hotTextScoringParameter>
                                       </parameterSet>
                                   </parameters>
                               </assessmentItem>

End Class
