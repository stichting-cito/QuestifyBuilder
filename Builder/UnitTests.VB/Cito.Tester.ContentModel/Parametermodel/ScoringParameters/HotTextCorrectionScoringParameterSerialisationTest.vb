
Imports Cito.Tester.ContentModel

<TestClass()>
Public Class HotTextCorrectionScoringParameterSerialisationTest
    Inherits SerializationTestBase

    <TestMethod()> <TestCategory("ContentModel"), TestCategory("ScoringParameter")>
    Public Sub CompareWithPreviouslyKnowState()
        'Arrange
        Dim hotTextCorrectionPrm = New HotTextCorrectionScoringParameter() With {.ControllerId = "hottextcorrectioncontroller_1"}
        hotTextCorrectionPrm.Value = New ParameterSetCollection()
        hotTextCorrectionPrm.BluePrint = New ParameterCollection()

        Dim subParamCollection As New ParameterCollection() With {.Id = "guid123"}
        hotTextCorrectionPrm.Value.Add(subParamCollection)

        'Act
        Dim result = DoSerialize(Of HotTextCorrectionScoringParameter)(hotTextCorrectionPrm)
        
        'Assert
        'Compare with previously known result 
        Assert.AreEqual(<HotTextCorrectionScoringParameter
                            xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
                            xmlns:xsd="http://www.w3.org/2001/XMLSchema"
                            ControllerId="hottextcorrectioncontroller_1"
                            expectedLength="0"
                            correctionIsApplicable="false">
                            <subparameterset id="guid123"/>
                            <definition id=""/>
                        </HotTextCorrectionScoringParameter>.ToString(), result.ToString())
    End Sub

    <TestMethod()> <TestCategory("ContentModel"), TestCategory("ScoringParameter")>
    Public Sub Deserialize_Test()
        'Arrange
        Dim xmlData = <HotTextCorrectionScoringParameter
                          xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
                          xmlns:xsd="http://www.w3.org/2001/XMLSchema"
                          ControllerId="hottextcorrectioncontroller_1"
                          expectedLength="1"
                          >
                          <subparameterset id="Id213789c-6ee7-45ee-a024-0b10a10f6375">
                              <plaintextparameter name="controlLabel">Ruim 100 jaar lang werden alle huizen en kantoren ...</plaintextparameter>
                          </subparameterset>
                          <subparameterset id="Ia1384693-9aac-4a5d-bb43-9c2b262e864a">
                              <plaintextparameter name="controlLabel"> De lampen waren niet duur en waren gemakkelijk in...</plaintextparameter>
                          </subparameterset>
                          <definition id=""/>
                      </HotTextCorrectionScoringParameter>
        
        'Act
        Dim result = Deserialize(Of HotTextCorrectionScoringParameter)(xmlData)
       
        'Assert
        Assert.AreEqual("hottextcorrectioncontroller_1", result.ControllerId)
        Assert.AreEqual(1, result.ExpectedLength)
        Assert.AreEqual("Ia1384693-9aac-4a5d-bb43-9c2b262e864a", result.Value(1).Id)
        Assert.AreEqual(" De lampen waren niet duur en waren gemakkelijk in...", DirectCast(result.Value(1).InnerParameters(0), PlainTextParameter).Value)
    End Sub


    <TestMethod()> <TestCategory("ContentModel"), TestCategory("ScoringParameter")>
    Public Sub SerializeHotTextCorrectionScoringParameter_inAssessmentItem_CompareWithPreviouslyKnownResult_Test()
        'Arrange
        Dim a = New AssessmentItem With
                {.Title = "someTitle", .Identifier = "someIdentifier", .LayoutTemplateSourceName = "someIlt"}
        Dim p = a.Parameters.AddNew()
        p.Id = "id_1"

        Dim newHotTextXhtmlParam As New XHtmlParameter() With {.Name = "hottext"}
        newHotTextXhtmlParam.Value = <p id="c1-id-11" xmlns="http://www.w3.org/1999/xhtml">
                                         <cito:InlineElement id="I53054a26-3fe9-429e-8e02-53236a83f969" layoutTemplateSourceName="InlineHottextCorrectionLayoutTemplate" inlineFO="" xmlns:cito="http://www.cito.nl/citotester">
                                             <cito:parameters>
                                                 <cito:parameterSet id="entireItem">
                                                     <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                     <cito:plaintextparameter name="controlId">I53054a26-3fe9-429e-8e02-53236a83f969</cito:plaintextparameter>
                                                     <cito:plaintextparameter name="controlLabel">woord1</cito:plaintextparameter>
                                                     <cito:booleanparameter name="addHottextCorrection">True</cito:booleanparameter>
                                                     <cito:plaintextparameter name="hottextValue"/>
                                                     <cito:hotTextCorrectionScoringParameter name="hotTextCorrectionScoring" ControllerId="hottextCorrectionController" findingOverride="hottextController" expectedLength="0" correctionIsApplicable="true">
                                                         <cito:subparameterset id="Input"/>
                                                         <cito:definition id=""/>
                                                         <cito:relatedControlLabel name="controlLabel">woord1</cito:relatedControlLabel>
                                                     </cito:hotTextCorrectionScoringParameter>
                                                 </cito:parameterSet>
                                             </cito:parameters>
                                         </cito:InlineElement>woord1</p>.ToString()

        Dim newHTParam As New HotTextScoringParameter() With {.MaxChoices = 2, .MinChoices = 1, .ControllerId = "hottextcontroller_1", .HotTextText = newHotTextXhtmlParam, .IsCorrectionVariant = True}
        Dim newHTCParam As New HotTextCorrectionScoringParameter() With {.Name = "thecorrectionparam", .ControllerId = "hottextcorrectioncontroller_1", .FindingOverride = "hottextfindingoverride"}

        p.InnerParameters.Add(newHotTextXhtmlParam)
        p.InnerParameters.Add(newHTParam)

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
                                    <xhtmlparameter name="hottext">
                                        <p id="c1-id-11" xmlns="http://www.w3.org/1999/xhtml">
                                            <cito:InlineElement id="I53054a26-3fe9-429e-8e02-53236a83f969" layoutTemplateSourceName="InlineHottextCorrectionLayoutTemplate" inlineFO="" xmlns:cito="http://www.cito.nl/citotester">
                                                <cito:parameters>
                                                    <cito:parameterSet id="entireItem">
                                                        <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                        <cito:plaintextparameter name="controlId">I53054a26-3fe9-429e-8e02-53236a83f969</cito:plaintextparameter>
                                                        <cito:plaintextparameter name="controlLabel">woord1</cito:plaintextparameter>
                                                        <cito:booleanparameter name="addHottextCorrection">True</cito:booleanparameter>
                                                        <cito:plaintextparameter name="hottextValue"/>
                                                        <cito:hotTextCorrectionScoringParameter name="hotTextCorrectionScoring" ControllerId="hottextCorrectionController" findingOverride="hottextController" expectedLength="0" correctionIsApplicable="true">
                                                            <cito:subparameterset id="Input"/>
                                                            <cito:definition id=""/>
                                                            <cito:relatedControlLabel name="controlLabel">woord1</cito:relatedControlLabel>
                                                        </cito:hotTextCorrectionScoringParameter>
                                                    </cito:parameterSet>
                                                </cito:parameters>
                                            </cito:InlineElement>woord1</p>
                                    </xhtmlparameter>
                                    <hotTextScoringParameter ControllerId="hottextcontroller_1" minChoices="1" maxChoices="2" multiChoice="Check" isCorrectionVariant="true">
                                        <subparameterset id="I53054a26-3fe9-429e-8e02-53236a83f969">
                                            <plaintextparameter name="contentLabel">woord1</plaintextparameter>
                                        </subparameterset>
                                    </hotTextScoringParameter>
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
                                  <hotTextCorrectionScoringParameter ControllerId="hottextcorrectioncontroller_1" expectedLength="2"/>
                              </parameterSet>
                          </parameters>
                      </assessmentItem>
        
        'Act
        Dim result = Deserialize(Of AssessmentItem)(xmlData)
        
        'Assert
        Assert.IsInstanceOfType(result.Parameters(0).InnerParameters(0), GetType(HotTextCorrectionScoringParameter))
    End Sub

End Class
