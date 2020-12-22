
Imports Cito.Tester.ContentModel
Imports System.Linq
Imports System.Xml.Linq
Imports Cito.Tester.Common

<TestClass>
Public Class GapMatchScoringParameterTest
    Inherits SerializationTestBase

    <TestMethod()> <TestCategory("ContentModel"), TestCategory("ScoringParameter")>
    Public Sub Deserialize_InAssessmentItem_Test()
        Dim xmlData = <assessmentItem xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" identifier="someIdentifier" title="someTitle" layoutTemplateSrc="someIlt">
                          <solution>
                              <keyFindings/>
                              <aspectReferences/>
                          </solution>
                          <parameters>
                              <parameterSet id="id_1">
                                  <gapMatchScoringParameter name="scoreParam" findingOverride="gapMatchController"/>
                              </parameterSet>
                          </parameters>
                      </assessmentItem>

        Dim result = Deserialize(Of AssessmentItem)(xmlData)
        Dim param = CType(result.Parameters(0).InnerParameters(0), GapMatchScoringParameter)

        Assert.IsInstanceOfType(result.Parameters(0).InnerParameters(0), GetType(GapMatchScoringParameter))
        Assert.AreEqual("gapMatchController", param.FindingOverride)
        Assert.AreEqual("scoreParam", param.Name)
    End Sub

    <TestMethod()> <TestCategory("ContentModel"), TestCategory("ScoringParameter")>
    Public Sub Deserialize_InRealWorldAssessmentItem_Test()

        Dim result = Deserialize(Of AssessmentItem)(_serializedGapMatchItem)

        Assert.IsInstanceOfType(result.Parameters(0).InnerParameters(0), GetType(GapMatchScoringParameter))

        Dim result2 = DoSerialize(result)
        Assert.AreEqual(_serializedGapMatchItem.ToString(), result2.ToString())
    End Sub

    <TestMethod()> <TestCategory("ContentModel"), TestCategory("ScoringParameter")>
    Public Sub Serialize_Test()
        Dim gm = New GapMatchScoringParameter() With {.ControllerId = "gm1"}

        gm.BluePrint = New ParameterCollection()
        gm.BluePrint.InnerParameters.Add(New GapTextParameter() With {.Name = "choice"})

        Dim subparam1 = New ParameterCollection() With {.Id = "1"}
        subparam1.InnerParameters.Add(New GapTextParameter() With {.Name = "choice", .Value = "Aap", .MatchMax = 2, .Id = "1"})

        Dim subparam2 = New ParameterCollection() With {.Id = "2"}
        subparam2.InnerParameters.Add(New GapTextParameter() With {.Name = "choice", .Value = "Giraffe", .MatchMax = 1, .Id = "2"})

        Dim subparam3 = New ParameterCollection() With {.Id = "3"}
        subparam3.InnerParameters.Add(New GapTextParameter() With {.Name = "choice", .Value = "Panter", .MatchMax = 1, .Id = "3"})

        gm.Value = New ParameterSetCollection()
        gm.Value.Add(subparam1)
        gm.Value.Add(subparam2)
        gm.Value.Add(subparam3)

        gm.GapXhtmlParameter = New XHtmlParameter() With {.Name = "itemInlineInput"}
        gm.GapXhtmlParameter.DesignerSettings.Add(New DesignerSetting() With {.Key = "required", .Value = "true"})

        Dim result = DoSerialize(Of GapMatchScoringParameter)(gm)

        Assert.AreEqual(_serializedGapMatchScoringParameter.ToString(), result.ToString())
    End Sub

    <TestMethod()> <TestCategory("ContentModel"), TestCategory("ScoringParameter")>
    Public Sub SerializeInParameterSet_Test()
        Dim parameterset As New ParameterSetCollection()
        parameterset.Add(New ParameterCollection())
        parameterset(0).InnerParameters.Add(Deserialize(Of GapMatchScoringParameter)(_serializedGapMatchScoringParameter))

        Dim result = DoSerialize(parameterset)

        Assert.AreEqual(_serializedParameterSet.ToString(), result.ToString())
    End Sub

    <TestMethod()> <TestCategory("ContentModel"), TestCategory("ScoringParameter")>
    Public Sub GetGapIds_Test()
        Dim gapXhtmlParameter = Deserialize(Of XHtmlParameter)(_gapXhtmlParameter)
        Dim gmScoringParameter = Deserialize(Of GapMatchScoringParameter)(_serializedGapMatchScoringParameter)

        gmScoringParameter.GapXhtmlParameter = gapXhtmlParameter

        Dim gapInlines = gmScoringParameter.Gaps

        Dim expected As New List(Of String)()
        expected.Add("I23a0653b-b574-4d5e-ad66-e05af1a169da")
        expected.Add("I47a1295a-c729-49d5-9da0-bac0799a019e")
        expected.Add("I27b8eca6-ae70-4f7b-bc23-b904b2e9045f")

        Dim indexer = 0
        For Each keyValuePair As KeyValuePair(Of String, Dictionary(Of String, String)) In gapInlines
            Assert.AreEqual(expected(indexer), keyValuePair.Key)
            indexer += 1
        Next
    End Sub

    <TestMethod()> <TestCategory("ContentModel"), TestCategory("ScoringParameter")>
    Public Sub TransformViaParameterSet_Test()
        Dim expected As New List(Of String)()
        expected.Add("I123d5b20-b6e9-409d-a0aa-6da1effdcb13")
        expected.Add("I05fb9f7d-6bf5-4ba3-90b1-f6d7642bcbc2")
        Dim item = Deserialize(Of AssessmentItem)(_serializedGapMatchItem)

        Dim params = item.Parameters.DeepFetchInlineScoringParameters()
        Dim scoreParam As GapMatchScoringParameter = CType(params.First(), GapMatchScoringParameter)

        Assert.IsTrue(scoreParam.Gaps.ContainsKey(1.ToString()))
        Assert.IsTrue(scoreParam.Gaps.ContainsKey(2.ToString()))
        Assert.IsTrue(scoreParam.Value(0).Id = expected(0))
        Assert.IsTrue(scoreParam.Value(1).Id = expected(1))
        Assert.IsTrue(CType(scoreParam.Value(0).InnerParameters(0), GapTextParameter).Value = "vak0")
    End Sub

    <TestMethod()> <TestCategory("ContentModel"), TestCategory("ScoringParameter")>
    Public Sub Transform_Test()
        Dim gmScoringParameter = Deserialize(Of GapMatchScoringParameter)(_serializedGapMatchScoringParameterWithGaps)

        gmScoringParameter = gmScoringParameter.Transform()

        Assert.IsTrue(gmScoringParameter.Gaps.ContainsKey(1.ToString()))
        Assert.IsTrue(gmScoringParameter.Gaps.ContainsKey(2.ToString()))
        Assert.AreEqual(gmScoringParameter.Gaps("2")("Value"), "Giraffe")
        Assert.IsTrue(gmScoringParameter.Value(0).Id = "I123d5b20-b6e9-409d-a0aa-6da1effdcb13")
        Assert.IsTrue(gmScoringParameter.Value(1).Id = "I05fb9f7d-6bf5-4ba3-90b1-f6d7642bcbc2")
        Assert.AreEqual("vak1", DirectCast(gmScoringParameter.Value(1).InnerParameters(0), GapTextParameter).Value)
    End Sub

    <TestMethod()> <TestCategory("ContentModel"), TestCategory("ScoringParameter")>
    Public Sub GetAlternativesCount_Test()

        Dim gmScoringParameter = Deserialize(Of GapMatchScoringParameter)(_serializedGapMatchScoringParameter)

        Assert.AreEqual(3, gmScoringParameter.AlternativesCount)
    End Sub


    <TestMethod()> <TestCategory("ContentModel"), TestCategory("ScoringParameter")>
    Public Sub Default_GapMatchScoringParameter_HasInitializedXHtmlParameter()

        Dim gmScoringParameter = New GapMatchScoringParameter()

        Assert.IsNotNull(gmScoringParameter.GapXhtmlParameter)
    End Sub

    <TestMethod()> <TestCategory("ContentModel"), TestCategory("ScoringParameter")>
    Public Sub WhenTransformingGM_XhtmlInlineNeeds_inlineGapMatchLabel()
        Dim gmScoringParameter = New GapMatchScoringParameter()
        gmScoringParameter.Value = New ParameterSetCollection()
        Dim collection = New ParameterCollection() With {.Id = "A"}
        collection.InnerParameters.Add(New GapTextParameter() With {.Value = "aaa"})
        gmScoringParameter.Value.Add(collection)

        gmScoringParameter.GapXhtmlParameter.Value = <p>﻿
                                                        <cito:InlineElement id="1" layoutTemplateSourceName="" xmlns:cito="http://www.cito.nl/citotester">
                                                             <cito:parameters>
                                                                 <cito:parameterSet id="">
                                                                     <cito:plaintextparameter name="inlineGapMatchId">_1</cito:plaintextparameter>
                                                                     <cito:plaintextparameter name="inlineGapMatchLabel">one</cito:plaintextparameter>
                                                                 </cito:parameterSet>
                                                             </cito:parameters>
                                                         </cito:InlineElement>
                                                     </p>.ToString()

        Dim result = gmScoringParameter.Transform

        Assert.AreEqual(1, result.Value.Count())
    End Sub

    <TestMethod()> <TestCategory("ContentModel"), TestCategory("ScoringParameter")>
    <ExpectedException(GetType(ContentModelException))>
    Public Sub WhenTransformingGM_XhtmlInlineNeeds_inlineGapMatchLabel_ProofThatItThrows()
        Dim gmScoringParameter = New GapMatchScoringParameter()
        gmScoringParameter.Value = New ParameterSetCollection()
        Dim collection = New ParameterCollection() With {.Id = "A"}
        collection.InnerParameters.Add(New GapTextParameter() With {.Value = "aaa"})
        gmScoringParameter.Value.Add(collection)

        gmScoringParameter.GapXhtmlParameter.Value = <p>﻿
                                                        <cito:InlineElement id="1" layoutTemplateSourceName="" xmlns:cito="http://www.cito.nl/citotester">
                                                             <cito:parameters>
                                                                 <cito:parameterSet id="">
                                                                     <cito:plaintextparameter name="inlineGapMatchId">_1</cito:plaintextparameter>
                                                                 </cito:parameterSet>
                                                             </cito:parameters>
                                                         </cito:InlineElement>
                                                     </p>.ToString()

        Dim result = gmScoringParameter.Transform

    End Sub

    Private ReadOnly _serializedGapMatchItem As XElement = <assessmentItem xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" identifier="2001" title="2001" layoutTemplateSrc="ilt.gapmatch">
                                                               <solution>
                                                                   <keyFindings/>
                                                                   <aspectReferences/>
                                                               </solution>
                                                               <parameters>
                                                                   <parameterSet id="invoer">
                                                                       <gapMatchScoringParameter name="domainx">
                                                                           <subparameterset id="1">
                                                                               <gapTextParameter name="gapText" matchMax="1">Giraffe</gapTextParameter>
                                                                           </subparameterset>
                                                                           <subparameterset id="2">
                                                                               <gapTextParameter name="gapText" matchMax="1">Panter</gapTextParameter>
                                                                           </subparameterset>
                                                                           <definition id="">
                                                                               <gapTextParameter name="gapText" matchMax="1"/>
                                                                           </definition>
                                                                           <xhtmlParameter name="itemInlineInput">
                                                                               <p id="c1-id-8" xmlns="http://www.w3.org/1999/xhtml">Een <cito:InlineElement id="I123d5b20-b6e9-409d-a0aa-6da1effdcb13" layoutTemplateSourceName="InlineGapMatchLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                                                       <cito:parameters>
                                                                                           <cito:parameterSet id="entireItem">
                                                                                               <cito:plaintextparameter name="inlineGapMatchId">I123d5b20-b6e9-409d-a0aa-6da1effdcb13</cito:plaintextparameter>
                                                                                               <cito:plaintextparameter name="inlineGapMatchLabel">vak0</cito:plaintextparameter>
                                                                                           </cito:parameterSet>
                                                                                       </cito:parameters>
                                                                                   </cito:InlineElement> is langzamer dan een <cito:InlineElement id="I05fb9f7d-6bf5-4ba3-90b1-f6d7642bcbc2" layoutTemplateSourceName="InlineGapMatchLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                                                       <cito:parameters>
                                                                                           <cito:parameterSet id="entireItem">
                                                                                               <cito:plaintextparameter name="inlineGapMatchId">I05fb9f7d-6bf5-4ba3-90b1-f6d7642bcbc2</cito:plaintextparameter>
                                                                                               <cito:plaintextparameter name="inlineGapMatchLabel">vak1</cito:plaintextparameter>
                                                                                           </cito:parameterSet>
                                                                                       </cito:parameters>
                                                                                   </cito:InlineElement> </p>
                                                                           </xhtmlParameter>
                                                                       </gapMatchScoringParameter>
                                                                   </parameterSet>
                                                               </parameters>
                                                           </assessmentItem>

    Private ReadOnly _serializedGapMatchScoringParameter As XElement = <GapMatchScoringParameter xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" ControllerId="gm1">
                                                                           <subparameterset id="1">
                                                                               <gapTextParameter name="choice" id="1" matchMax="2">Aap</gapTextParameter>
                                                                           </subparameterset>
                                                                           <subparameterset id="2">
                                                                               <gapTextParameter name="choice" id="2" matchMax="1">Giraffe</gapTextParameter>
                                                                           </subparameterset>
                                                                           <subparameterset id="3">
                                                                               <gapTextParameter name="choice" id="3" matchMax="1">Panter</gapTextParameter>
                                                                           </subparameterset>
                                                                           <definition id="">
                                                                               <gapTextParameter name="choice" matchMax="1"/>
                                                                           </definition>
                                                                           <xhtmlParameter name="itemInlineInput"/>
                                                                       </GapMatchScoringParameter>

    Private ReadOnly _serializedGapMatchScoringParameterWithGaps As XElement = <GapMatchScoringParameter xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" ControllerId="gm1">
                                                                                   <subparameterset id="1">
                                                                                       <gapTextParameter name="choice" id="1" matchMax="2">Aap</gapTextParameter>
                                                                                   </subparameterset>
                                                                                   <subparameterset id="2">
                                                                                       <gapTextParameter name="choice" id="2" matchMax="1">Giraffe</gapTextParameter>
                                                                                   </subparameterset>
                                                                                   <subparameterset id="3">
                                                                                       <gapTextParameter name="choice" id="3" matchMax="1">Panter</gapTextParameter>
                                                                                   </subparameterset>
                                                                                   <definition id="">
                                                                                       <gapTextParameter name="choice" matchMax="0"/>
                                                                                   </definition>
                                                                                   <xhtmlParameter name="itemInlineInput">
                                                                                       <p id="c1-id-8" xmlns="http://www.w3.org/1999/xhtml">Een <cito:InlineElement id="I123d5b20-b6e9-409d-a0aa-6da1effdcb13" layoutTemplateSourceName="InlineGapMatchLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                                                               <cito:parameters>
                                                                                                   <cito:parameterSet id="entireItem">
                                                                                                       <cito:plaintextparameter name="inlineGapMatchId">I123d5b20-b6e9-409d-a0aa-6da1effdcb13</cito:plaintextparameter>
                                                                                                       <cito:plaintextparameter name="inlineGapMatchLabel">vak0</cito:plaintextparameter>
                                                                                                   </cito:parameterSet>
                                                                                               </cito:parameters>
                                                                                           </cito:InlineElement> is langzamer dan een <cito:InlineElement id="I05fb9f7d-6bf5-4ba3-90b1-f6d7642bcbc2" layoutTemplateSourceName="InlineGapMatchLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                                                               <cito:parameters>
                                                                                                   <cito:parameterSet id="entireItem">
                                                                                                       <cito:plaintextparameter name="inlineGapMatchId">I05fb9f7d-6bf5-4ba3-90b1-f6d7642bcbc2</cito:plaintextparameter>
                                                                                                       <cito:plaintextparameter name="inlineGapMatchLabel">vak1</cito:plaintextparameter>
                                                                                                   </cito:parameterSet>
                                                                                               </cito:parameters>
                                                                                           </cito:InlineElement> </p>
                                                                                   </xhtmlParameter>
                                                                               </GapMatchScoringParameter>

    Private ReadOnly _serializedParameterSet As XElement = <ArrayOfParameterCollection xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                                               <ParameterCollection id="">
                                                                   <gapMatchScoringParameter ControllerId="gm1">
                                                                       <subparameterset id="1">
                                                                           <gapTextParameter name="choice" id="1" matchMax="2">Aap</gapTextParameter>
                                                                       </subparameterset>
                                                                       <subparameterset id="2">
                                                                           <gapTextParameter name="choice" id="2" matchMax="1">Giraffe</gapTextParameter>
                                                                       </subparameterset>
                                                                       <subparameterset id="3">
                                                                           <gapTextParameter name="choice" id="3" matchMax="1">Panter</gapTextParameter>
                                                                       </subparameterset>
                                                                       <definition id="">
                                                                           <gapTextParameter name="choice" matchMax="1"/>
                                                                       </definition>
                                                                       <xhtmlParameter name="itemInlineInput"/>
                                                                   </gapMatchScoringParameter>
                                                               </ParameterCollection>
                                                           </ArrayOfParameterCollection>

    Private ReadOnly _gapXhtmlParameter As XElement = <XHtmlParameter name="itemInlineInput" xmlns:cito="http://www.cito.nl/citotester">
                                                          <cito:InlineElement id="I23a0653b-b574-4d5e-ad66-e05af1a169da" layoutTemplateSourceName="InlineGapMatchLayoutTemplate">
                                                              <cito:parameters>
                                                                  <cito:parameterSet id="entireItem">
                                                                      <cito:plaintextparameter name="inlineGapMatchId">I23a0653b-b574-4d5e-ad66-e05af1a169da</cito:plaintextparameter>
                                                                      <cito:plaintextparameter name="inlineGapMatchLabel">vak1</cito:plaintextparameter>
                                                                  </cito:parameterSet>
                                                              </cito:parameters>
                                                          </cito:InlineElement>
                                                          <cito:InlineElement id="I47a1295a-c729-49d5-9da0-bac0799a019e" layoutTemplateSourceName="InlineGapMatchLayoutTemplate">
                                                              <cito:parameters>
                                                                  <cito:parameterSet id="entireItem">
                                                                      <cito:plaintextparameter name="inlineGapMatchId">I47a1295a-c729-49d5-9da0-bac0799a019e</cito:plaintextparameter>
                                                                      <cito:plaintextparameter name="inlineGapMatchLabel">vak2</cito:plaintextparameter>
                                                                  </cito:parameterSet>
                                                              </cito:parameters>
                                                          </cito:InlineElement> maar een <cito:InlineElement id="I27b8eca6-ae70-4f7b-bc23-b904b2e9045f" layoutTemplateSourceName="InlineGapMatchLayoutTemplate">
                                                              <cito:parameters>
                                                                  <cito:parameterSet id="entireItem">
                                                                      <cito:plaintextparameter name="inlineGapMatchId">I27b8eca6-ae70-4f7b-bc23-b904b2e9045f</cito:plaintextparameter>
                                                                      <cito:plaintextparameter name="inlineGapMatchLabel">vak3</cito:plaintextparameter>
                                                                  </cito:parameterSet>
                                                              </cito:parameters>
                                                          </cito:InlineElement>
                                                      </XHtmlParameter>

End Class
