
Imports System.Linq
Imports System.Xml.Linq
Imports Cito.Tester.ContentModel

<TestClass>
Public Class MultipleInlineChoiceAndAspectTests
    Inherits ScoringTestBase

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub MultipleInlineChoiceAndAspect_poly()
        Dim solution = toSolution(sol_A)
        Dim response = toResponse(resp_A)

        Dim result = solution.ScoreSolution(response)

        Assert.AreEqual(13, result, "Score should be 13")
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub MultipleInlineChoiceAndAspect()
        Dim solution = toSolution(sol_A)
        solution.Findings.First().Method = EnumScoringMethod.Dichotomous

        Dim response = toResponse(resp_A)

        Dim result = solution.ScoreSolution(response)

        Assert.AreEqual(1, result, "Score should be 1")
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub MultipleInlineChoiceAndAspect_responeHas1Error_poly_Expects12()
        Dim solution = toSolution(sol_A)
        Dim response = toResponse(resp_1Error)

        Dim result = solution.ScoreSolution(response)

        Assert.AreEqual(12, result, "Score should be 12")
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub MultipleInlineChoiceAndAspect_responeHas1Error_Expects0()
        Dim solution = toSolution(sol_A)
        solution.Findings.First().Method = EnumScoringMethod.Dichotomous

        Dim response = toResponse(resp_1Error)

        Dim result = solution.ScoreSolution(response)

        Assert.AreEqual(0, result, "Score should be 0")
    End Sub

    Private sol_A As XElement = <solution>
                                    <keyFindings>
                                        <keyFinding id="textEntryController" scoringMethod="Polytomous">
                                            <keyFact id="298e779c-859f-4268-817e-98d545283ed1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                <keyValue domain="298e779c-859f-4268-817e-98d545283ed1" occur="1">
                                                    <stringValue>
                                                        <typedValue>S</typedValue>
                                                    </stringValue>
                                                </keyValue>
                                            </keyFact>
                                            <keyFact id="bf7dfccb-132e-49c7-a598-cf3fd721f0ce" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                <keyValue domain="bf7dfccb-132e-49c7-a598-cf3fd721f0ce" occur="1">
                                                    <integerValue>
                                                        <typedValue>0</typedValue>
                                                    </integerValue>
                                                </keyValue>
                                            </keyFact>
                                            <keyFact id="51b71779-5df6-458e-ad01-93e16a24894c" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                <keyValue domain="51b71779-5df6-458e-ad01-93e16a24894c" occur="1">
                                                    <integerValue>
                                                        <typedValue>1</typedValue>
                                                    </integerValue>
                                                </keyValue>
                                            </keyFact>
                                            <keyFact id="bca821f8-15c3-4a94-b67a-49f350b11f4c" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                <keyValue domain="bca821f8-15c3-4a94-b67a-49f350b11f4c" occur="1">
                                                    <integerValue>
                                                        <typedValue>0</typedValue>
                                                    </integerValue>
                                                </keyValue>
                                            </keyFact>
                                            <keyFact id="100001bc-d48c-436b-9199-063282e726ab" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                <keyValue domain="100001bc-d48c-436b-9199-063282e726ab" occur="1">
                                                    <integerValue>
                                                        <typedValue>1</typedValue>
                                                    </integerValue>
                                                </keyValue>
                                            </keyFact>
                                            <keyFact id="a14c5a60-e72b-459c-80ee-f0acb3f3d974" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                <keyValue domain="a14c5a60-e72b-459c-80ee-f0acb3f3d974" occur="1">
                                                    <integerValue>
                                                        <typedValue>2</typedValue>
                                                    </integerValue>
                                                </keyValue>
                                            </keyFact>
                                            <keyFact id="b4439c3d-82a9-4c47-8fae-6ced2a0f03ef" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                <keyValue domain="b4439c3d-82a9-4c47-8fae-6ced2a0f03ef" occur="1">
                                                    <integerValue>
                                                        <typedValue>0</typedValue>
                                                    </integerValue>
                                                </keyValue>
                                            </keyFact>
                                            <keyFact id="ea260e34-f1a5-4d11-a2cf-b355a8c6f1ce" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                <keyValue domain="ea260e34-f1a5-4d11-a2cf-b355a8c6f1ce" occur="1">
                                                    <integerValue>
                                                        <typedValue>1</typedValue>
                                                    </integerValue>
                                                </keyValue>
                                            </keyFact>
                                            <keyFact id="64732446-c285-44c6-bb64-84cd56cdc283" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                <keyValue domain="64732446-c285-44c6-bb64-84cd56cdc283" occur="1">
                                                    <integerValue>
                                                        <typedValue>3</typedValue>
                                                    </integerValue>
                                                </keyValue>
                                            </keyFact>
                                            <keyFact id="25ab7a67-1111-41bb-aac8-984bfeed0124" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                <keyValue domain="25ab7a67-1111-41bb-aac8-984bfeed0124" occur="1">
                                                    <integerValue>
                                                        <typedValue>1</typedValue>
                                                    </integerValue>
                                                </keyValue>
                                            </keyFact>
                                            <keyFact id="41e11cbc-3f28-447d-af4c-57f82f103417" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                <keyValue domain="41e11cbc-3f28-447d-af4c-57f82f103417" occur="1">
                                                    <integerValue>
                                                        <typedValue>4</typedValue>
                                                    </integerValue>
                                                </keyValue>
                                            </keyFact>
                                            <keyFact id="3b71d25c-7682-4ece-a523-9f789d8bace9" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                <keyValue domain="3b71d25c-7682-4ece-a523-9f789d8bace9" occur="1">
                                                    <integerValue>
                                                        <typedValue>0</typedValue>
                                                    </integerValue>
                                                </keyValue>
                                            </keyFact>
                                            <keyFact id="a7330bda-bee2-4609-b6fb-834c3dda7564" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                <keyValue domain="a7330bda-bee2-4609-b6fb-834c3dda7564" occur="1">
                                                    <integerValue>
                                                        <typedValue>0</typedValue>
                                                    </integerValue>
                                                </keyValue>
                                            </keyFact>
                                        </keyFinding>
                                    </keyFindings>
                                    <aspectReferences>
                                        <aspectReferenceSet id="extendedTextEntryController">
                                            <aspectReference maxScore="2" src="Inhoud">&lt;p class="UserSRTitelGroot" id="c1-id-11" xmlns="http://www.w3.org/1999/xhtml"&gt;Scorevoorschrift&lt;/p&gt;&lt;p id="c1-id-12" xmlns="http://www.w3.org/1999/xhtml"&gt;&lt;br id="c1-id-13" /&gt;Verbalisantnummer:&lt;br id="c1-id-14" /&gt;Wordt niet beoordeelt.wwww&lt;/p&gt;</aspectReference>
                                        </aspectReferenceSet>
                                    </aspectReferences>
                                </solution>

    Private resp_A As XElement = <Response xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" active="true" sessionId="35417" id="CB01031" responseNr="11" translatedScore="5" rawScore="5" navigatedToIndex="11">
                                     <ResponseProperties xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                     <ApplicationId xmlns="http://Cito.Tester.Server/xml/serialization">TR1</ApplicationId>
                                     <responseFindings xmlns="http://Cito.Tester.Server/xml/serialization">
                                         <responseFinding id="textEntryController">
                                             <responseFact id="298e779c-859f-4268-817e-98d545283ed1">
                                                 <responseValue domain="298e779c-859f-4268-817e-98d545283ed1" occur="1">
                                                     <stringValue>
                                                         <typedValue>S</typedValue>
                                                     </stringValue>
                                                 </responseValue>
                                             </responseFact>
                                             <responseFact id="bf7dfccb-132e-49c7-a598-cf3fd721f0ce">
                                                 <responseValue domain="bf7dfccb-132e-49c7-a598-cf3fd721f0ce" occur="1">
                                                     <integerValue>
                                                         <typedValue>0</typedValue>
                                                     </integerValue>
                                                 </responseValue>
                                             </responseFact>
                                             <responseFact id="51b71779-5df6-458e-ad01-93e16a24894c">
                                                 <responseValue domain="51b71779-5df6-458e-ad01-93e16a24894c" occur="1">
                                                     <integerValue>
                                                         <typedValue>1</typedValue>
                                                     </integerValue>
                                                 </responseValue>
                                             </responseFact>
                                             <responseFact id="bca821f8-15c3-4a94-b67a-49f350b11f4c">
                                                 <responseValue domain="bca821f8-15c3-4a94-b67a-49f350b11f4c" occur="1">
                                                     <integerValue>
                                                         <typedValue>0</typedValue>
                                                     </integerValue>
                                                 </responseValue>
                                             </responseFact>
                                             <responseFact id="100001bc-d48c-436b-9199-063282e726ab">
                                                 <responseValue domain="100001bc-d48c-436b-9199-063282e726ab" occur="1">
                                                     <integerValue>
                                                         <typedValue>1</typedValue>
                                                     </integerValue>
                                                 </responseValue>
                                             </responseFact>
                                             <responseFact id="a14c5a60-e72b-459c-80ee-f0acb3f3d974">
                                                 <responseValue domain="a14c5a60-e72b-459c-80ee-f0acb3f3d974" occur="1">
                                                     <integerValue>
                                                         <typedValue>2</typedValue>
                                                     </integerValue>
                                                 </responseValue>
                                             </responseFact>
                                             <responseFact id="b4439c3d-82a9-4c47-8fae-6ced2a0f03ef">
                                                 <responseValue domain="b4439c3d-82a9-4c47-8fae-6ced2a0f03ef" occur="1">
                                                     <integerValue>
                                                         <typedValue>0</typedValue>
                                                     </integerValue>
                                                 </responseValue>
                                             </responseFact>
                                             <responseFact id="ea260e34-f1a5-4d11-a2cf-b355a8c6f1ce">
                                                 <responseValue domain="ea260e34-f1a5-4d11-a2cf-b355a8c6f1ce" occur="1">
                                                     <integerValue>
                                                         <typedValue>1</typedValue>
                                                     </integerValue>
                                                 </responseValue>
                                             </responseFact>
                                             <responseFact id="64732446-c285-44c6-bb64-84cd56cdc283">
                                                 <responseValue domain="64732446-c285-44c6-bb64-84cd56cdc283" occur="1">
                                                     <integerValue>
                                                         <typedValue>3</typedValue>
                                                     </integerValue>
                                                 </responseValue>
                                             </responseFact>
                                             <responseFact id="25ab7a67-1111-41bb-aac8-984bfeed0124">
                                                 <responseValue domain="25ab7a67-1111-41bb-aac8-984bfeed0124" occur="1">
                                                     <integerValue>
                                                         <typedValue>1</typedValue>
                                                     </integerValue>
                                                 </responseValue>
                                             </responseFact>
                                             <responseFact id="41e11cbc-3f28-447d-af4c-57f82f103417">
                                                 <responseValue domain="41e11cbc-3f28-447d-af4c-57f82f103417" occur="1">
                                                     <integerValue>
                                                         <typedValue>4</typedValue>
                                                     </integerValue>
                                                 </responseValue>
                                             </responseFact>
                                             <responseFact id="3b71d25c-7682-4ece-a523-9f789d8bace9">
                                                 <responseValue domain="3b71d25c-7682-4ece-a523-9f789d8bace9" occur="1">
                                                     <integerValue>
                                                         <typedValue>0</typedValue>
                                                     </integerValue>
                                                 </responseValue>
                                             </responseFact>
                                             <responseFact id="a7330bda-bee2-4609-b6fb-834c3dda7564">
                                                 <responseValue domain="a7330bda-bee2-4609-b6fb-834c3dda7564" occur="1">
                                                     <integerValue>
                                                         <typedValue>0</typedValue>
                                                     </integerValue>
                                                 </responseValue>
                                             </responseFact>
                                         </responseFinding>
                                         <responseFinding id="extendedTextEntryController">
                                             <responseFact id="3aeed2d8-62ea-400c-a006-5eea2b673fb1">
                                                 <responseValue domain="3aeed2d8-62ea-400c-a006-5eea2b673fb1" occur="1">
                                                     <stringValue>
                                                         <typedValue>1</typedValue>
                                                     </stringValue>
                                                 </responseValue>
                                             </responseFact>
                                             <responseFact id="a84a7545-580d-4c24-8b00-c3897fe36a6b">
                                                 <responseValue domain="a84a7545-580d-4c24-8b00-c3897fe36a6b" occur="1">
                                                     <stringValue>
                                                         <typedValue>2</typedValue>
                                                     </stringValue>
                                                 </responseValue>
                                             </responseFact>
                                             <responseFact id="ec761093-a840-4294-879e-cc853ae1e4a1">
                                                 <responseValue domain="ec761093-a840-4294-879e-cc853ae1e4a1" occur="1">
                                                     <stringValue>
                                                         <typedValue>3</typedValue>
                                                     </stringValue>
                                                 </responseValue>
                                             </responseFact>
                                             <responseFact id="731bcd2b-a3d0-43be-a462-42c7c91e6801">
                                                 <responseValue domain="731bcd2b-a3d0-43be-a462-42c7c91e6801" occur="1">
                                                     <stringValue>
                                                         <typedValue>4</typedValue>
                                                     </stringValue>
                                                 </responseValue>
                                             </responseFact>
                                             <responseFact id="2d27caa2-e49d-4a50-8424-2b29ffcae10f">
                                                 <responseValue domain="2d27caa2-e49d-4a50-8424-2b29ffcae10f" occur="1">
                                                     <stringValue>
                                                         <typedValue>5</typedValue>
                                                     </stringValue>
                                                 </responseValue>
                                             </responseFact>
                                             <responseFact id="c26d716d-9d84-42aa-baea-686e3152f783">
                                                 <responseValue domain="c26d716d-9d84-42aa-baea-686e3152f783" occur="1">
                                                     <stringValue>
                                                         <typedValue>6</typedValue>
                                                     </stringValue>
                                                 </responseValue>
                                             </responseFact>
                                             <responseFact id="228208fb-e3ad-4cc0-b8a6-e335ecba18c4">
                                                 <responseValue domain="228208fb-e3ad-4cc0-b8a6-e335ecba18c4" occur="1">
                                                     <stringValue>
                                                         <typedValue>7</typedValue>
                                                     </stringValue>
                                                 </responseValue>
                                             </responseFact>
                                         </responseFinding>
                                     </responseFindings>
                                     <ItemIndexInTest xmlns="http://Cito.Tester.Server/xml/serialization">10</ItemIndexInTest>
                                 </Response>

    Private resp_1Error As XElement = <Response xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" active="true" sessionId="35417" id="CB01031" responseNr="11" translatedScore="5" rawScore="5" navigatedToIndex="11">
                                          <ResponseProperties xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                          <ApplicationId xmlns="http://Cito.Tester.Server/xml/serialization">TR1</ApplicationId>
                                          <responseFindings xmlns="http://Cito.Tester.Server/xml/serialization">
                                              <responseFinding id="textEntryController">
                                                  <responseFact id="298e779c-859f-4268-817e-98d545283ed1">
                                                      <responseValue domain="298e779c-859f-4268-817e-98d545283ed1" occur="1">
                                                          <stringValue>
                                                              <typedValue>WRONG WRONG WR0NG</typedValue>
                                                          </stringValue>
                                                      </responseValue>
                                                  </responseFact>
                                                  <responseFact id="bf7dfccb-132e-49c7-a598-cf3fd721f0ce">
                                                      <responseValue domain="bf7dfccb-132e-49c7-a598-cf3fd721f0ce" occur="1">
                                                          <integerValue>
                                                              <typedValue>0</typedValue>
                                                          </integerValue>
                                                      </responseValue>
                                                  </responseFact>
                                                  <responseFact id="51b71779-5df6-458e-ad01-93e16a24894c">
                                                      <responseValue domain="51b71779-5df6-458e-ad01-93e16a24894c" occur="1">
                                                          <integerValue>
                                                              <typedValue>1</typedValue>
                                                          </integerValue>
                                                      </responseValue>
                                                  </responseFact>
                                                  <responseFact id="bca821f8-15c3-4a94-b67a-49f350b11f4c">
                                                      <responseValue domain="bca821f8-15c3-4a94-b67a-49f350b11f4c" occur="1">
                                                          <integerValue>
                                                              <typedValue>0</typedValue>
                                                          </integerValue>
                                                      </responseValue>
                                                  </responseFact>
                                                  <responseFact id="100001bc-d48c-436b-9199-063282e726ab">
                                                      <responseValue domain="100001bc-d48c-436b-9199-063282e726ab" occur="1">
                                                          <integerValue>
                                                              <typedValue>1</typedValue>
                                                          </integerValue>
                                                      </responseValue>
                                                  </responseFact>
                                                  <responseFact id="a14c5a60-e72b-459c-80ee-f0acb3f3d974">
                                                      <responseValue domain="a14c5a60-e72b-459c-80ee-f0acb3f3d974" occur="1">
                                                          <integerValue>
                                                              <typedValue>2</typedValue>
                                                          </integerValue>
                                                      </responseValue>
                                                  </responseFact>
                                                  <responseFact id="b4439c3d-82a9-4c47-8fae-6ced2a0f03ef">
                                                      <responseValue domain="b4439c3d-82a9-4c47-8fae-6ced2a0f03ef" occur="1">
                                                          <integerValue>
                                                              <typedValue>0</typedValue>
                                                          </integerValue>
                                                      </responseValue>
                                                  </responseFact>
                                                  <responseFact id="ea260e34-f1a5-4d11-a2cf-b355a8c6f1ce">
                                                      <responseValue domain="ea260e34-f1a5-4d11-a2cf-b355a8c6f1ce" occur="1">
                                                          <integerValue>
                                                              <typedValue>1</typedValue>
                                                          </integerValue>
                                                      </responseValue>
                                                  </responseFact>
                                                  <responseFact id="64732446-c285-44c6-bb64-84cd56cdc283">
                                                      <responseValue domain="64732446-c285-44c6-bb64-84cd56cdc283" occur="1">
                                                          <integerValue>
                                                              <typedValue>3</typedValue>
                                                          </integerValue>
                                                      </responseValue>
                                                  </responseFact>
                                                  <responseFact id="25ab7a67-1111-41bb-aac8-984bfeed0124">
                                                      <responseValue domain="25ab7a67-1111-41bb-aac8-984bfeed0124" occur="1">
                                                          <integerValue>
                                                              <typedValue>1</typedValue>
                                                          </integerValue>
                                                      </responseValue>
                                                  </responseFact>
                                                  <responseFact id="41e11cbc-3f28-447d-af4c-57f82f103417">
                                                      <responseValue domain="41e11cbc-3f28-447d-af4c-57f82f103417" occur="1">
                                                          <integerValue>
                                                              <typedValue>4</typedValue>
                                                          </integerValue>
                                                      </responseValue>
                                                  </responseFact>
                                                  <responseFact id="3b71d25c-7682-4ece-a523-9f789d8bace9">
                                                      <responseValue domain="3b71d25c-7682-4ece-a523-9f789d8bace9" occur="1">
                                                          <integerValue>
                                                              <typedValue>0</typedValue>
                                                          </integerValue>
                                                      </responseValue>
                                                  </responseFact>
                                                  <responseFact id="a7330bda-bee2-4609-b6fb-834c3dda7564">
                                                      <responseValue domain="a7330bda-bee2-4609-b6fb-834c3dda7564" occur="1">
                                                          <integerValue>
                                                              <typedValue>0</typedValue>
                                                          </integerValue>
                                                      </responseValue>
                                                  </responseFact>
                                              </responseFinding>
                                              <responseFinding id="extendedTextEntryController">
                                                  <responseFact id="3aeed2d8-62ea-400c-a006-5eea2b673fb1">
                                                      <responseValue domain="3aeed2d8-62ea-400c-a006-5eea2b673fb1" occur="1">
                                                          <stringValue>
                                                              <typedValue>1</typedValue>
                                                          </stringValue>
                                                      </responseValue>
                                                  </responseFact>
                                                  <responseFact id="a84a7545-580d-4c24-8b00-c3897fe36a6b">
                                                      <responseValue domain="a84a7545-580d-4c24-8b00-c3897fe36a6b" occur="1">
                                                          <stringValue>
                                                              <typedValue>2</typedValue>
                                                          </stringValue>
                                                      </responseValue>
                                                  </responseFact>
                                                  <responseFact id="ec761093-a840-4294-879e-cc853ae1e4a1">
                                                      <responseValue domain="ec761093-a840-4294-879e-cc853ae1e4a1" occur="1">
                                                          <stringValue>
                                                              <typedValue>3</typedValue>
                                                          </stringValue>
                                                      </responseValue>
                                                  </responseFact>
                                                  <responseFact id="731bcd2b-a3d0-43be-a462-42c7c91e6801">
                                                      <responseValue domain="731bcd2b-a3d0-43be-a462-42c7c91e6801" occur="1">
                                                          <stringValue>
                                                              <typedValue>4</typedValue>
                                                          </stringValue>
                                                      </responseValue>
                                                  </responseFact>
                                                  <responseFact id="2d27caa2-e49d-4a50-8424-2b29ffcae10f">
                                                      <responseValue domain="2d27caa2-e49d-4a50-8424-2b29ffcae10f" occur="1">
                                                          <stringValue>
                                                              <typedValue>5</typedValue>
                                                          </stringValue>
                                                      </responseValue>
                                                  </responseFact>
                                                  <responseFact id="c26d716d-9d84-42aa-baea-686e3152f783">
                                                      <responseValue domain="c26d716d-9d84-42aa-baea-686e3152f783" occur="1">
                                                          <stringValue>
                                                              <typedValue>6</typedValue>
                                                          </stringValue>
                                                      </responseValue>
                                                  </responseFact>
                                                  <responseFact id="228208fb-e3ad-4cc0-b8a6-e335ecba18c4">
                                                      <responseValue domain="228208fb-e3ad-4cc0-b8a6-e335ecba18c4" occur="1">
                                                          <stringValue>
                                                              <typedValue>7</typedValue>
                                                          </stringValue>
                                                      </responseValue>
                                                  </responseFact>
                                              </responseFinding>
                                          </responseFindings>
                                          <ItemIndexInTest xmlns="http://Cito.Tester.Server/xml/serialization">10</ItemIndexInTest>
                                      </Response>

End Class
