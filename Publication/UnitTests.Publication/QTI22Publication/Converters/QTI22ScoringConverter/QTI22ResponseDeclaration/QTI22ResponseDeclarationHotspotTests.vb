
Imports Cito.Tester.ContentModel

<TestClass()>
Public Class QTI22ResponseDeclarationHotspotTests
    Inherits QTI22ResponseDeclarationTests_Base

    'Hotspot items are (scoring-wise) MR-items, so the response declaration is build using ResponseDeclarationMultipleResponse (for mc and mr items).
    'This functionality may already be tested in other unittests, but still it's useful to check if it also correctly works for hotspot items.

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub GetResponseDeclarationForCombinationOfFactSetsAndFactsOnFindingTest()
        RunResponseDeclarationTest(_itemBody1, _solution1, GetHotspotScoringParams(), _result1, 1)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub GetResponseDeclarationForMultipleFactSetsTest()
        RunResponseDeclarationTest(_itemBody1, _solution2, GetHotspotScoringParams(), _result2, 1)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub GetResponseDeclarationForFactSetsTest()
        RunResponseDeclarationTest(_itemBody1, _solution3, GetHotspotScoringParams(), _result3, 1)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub GetResponseDeclarationForFactsOnFindingTest()
        RunResponseDeclarationTest(_itemBody1, _solution4, GetHotspotScoringParams(), _result4, 1)
    End Sub

    <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
    Public Sub GetEmptyResponseDeclarationTest()
        RunResponseDeclarationTest(_itemBody1, _solution5, GetHotspotScoringParams(), _result5, 1)
    End Sub

#Region "Scoring parameters"

    Private Function GetHotspotScoringParams() As HashSet(Of ScoringParameter)
        Dim scoreParams As New HashSet(Of ScoringParameter)

        Dim scoreParam As HotspotScoringParameter = New HotspotScoringParameter() With {.ControllerId = "areaInteractionController", .FindingOverride = "areaInteractionController", .MinChoices = 1, .MaxChoices = 5}.AddSubParameters("A", "B", "C", "D", "E")
        Dim area As New AreaParameter With {.Name = "clickableArea"}
        scoreParam.Area = area
        scoreParam.Area.ShapeList.Add(New CircleShape() With {.Identifier = "A", .Radius = 35})
        scoreParam.Area.ShapeList.Add(New CircleShape() With {.Identifier = "B", .Radius = 35})
        scoreParam.Area.ShapeList.Add(New CircleShape() With {.Identifier = "C", .Radius = 35})
        scoreParam.Area.ShapeList.Add(New CircleShape() With {.Identifier = "D", .Radius = 35})
        scoreParam.Area.ShapeList.Add(New CircleShape() With {.Identifier = "E", .Radius = 35})

        scoreParams.Add(scoreParam)
        Return scoreParams
    End Function

#End Region

#Region "Itembodies"

    Private _itemBody1 As XElement =
       <wrapper>
           <itemBody class="defaultBody">
               <div class="content">
                   <div>
                       <div>
                           <hotspotInteraction responseIdentifier="areaInteractionController" minChoices="1" maxChoices="5">
                               <object type="image/jpeg" data="resource://package/UK.jpg" width="197" height="256"/>
                               <hotspotChoice identifier="A" coords="44,45,40" shape="circle"/>
                               <hotspotChoice identifier="B" coords="148,45,40" shape="circle"/>
                               <hotspotChoice identifier="C" coords="45,133,40" shape="circle"/>
                               <hotspotChoice identifier="D" coords="149,129,40" shape="circle"/>
                               <hotspotChoice identifier="E" coords="150,214,40" shape="circle"/>
                           </hotspotInteraction>
                       </div>
                   </div>
               </div>
           </itemBody>
       </wrapper>

#End Region

#Region "Solutions"

    Private _solution1 As XElement =
        <solution>
            <keyFindings>
                <keyFinding id="areaInteractionController" scoringMethod="Dichotomous">
                    <keyFact id="D-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="D-areaInteractionController" occur="1">
                            <booleanValue>
                                <typedValue>false</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="E-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="E-areaInteractionController" occur="1">
                            <booleanValue>
                                <typedValue>true</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                    <keyFactSet>
                        <keyFact id="C-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="C-areaInteractionController" occur="1">
                                <booleanValue>
                                    <typedValue>false</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="B-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="B-areaInteractionController" occur="1">
                                <booleanValue>
                                    <typedValue>true</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="A-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="A-areaInteractionController" occur="1">
                                <booleanValue>
                                    <typedValue>true</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                    </keyFactSet>
                    <keyFactSet>
                        <keyFact id="A-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="A-areaInteractionController" occur="1">
                                <booleanValue>
                                    <typedValue>false</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="B-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="B-areaInteractionController" occur="1">
                                <booleanValue>
                                    <typedValue>true</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="C-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="C-areaInteractionController" occur="1">
                                <booleanValue>
                                    <typedValue>true</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                    </keyFactSet>
                </keyFinding>
            </keyFindings>
            <aspectReferences/>
            <ItemScoreTranslationTable>
                <ItemScoreTranslationTableEntry rawScore="0" translatedScore="0"/>
                <ItemScoreTranslationTableEntry rawScore="1" translatedScore="1"/>
            </ItemScoreTranslationTable>
        </solution>

    Private _solution2 As XElement =
        <solution>
            <keyFindings>
                <keyFinding id="areaInteractionController" scoringMethod="Dichotomous">
                    <keyFactSet>
                        <keyFact id="C-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="C-areaInteractionController" occur="1">
                                <booleanValue>
                                    <typedValue>false</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="B-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="B-areaInteractionController" occur="1">
                                <booleanValue>
                                    <typedValue>true</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="A-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="A-areaInteractionController" occur="1">
                                <booleanValue>
                                    <typedValue>true</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                    </keyFactSet>
                    <keyFactSet>
                        <keyFact id="A-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="A-areaInteractionController" occur="1">
                                <booleanValue>
                                    <typedValue>false</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="B-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="B-areaInteractionController" occur="1">
                                <booleanValue>
                                    <typedValue>true</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="C-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="C-areaInteractionController" occur="1">
                                <booleanValue>
                                    <typedValue>true</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                    </keyFactSet>
                    <keyFactSet>
                        <keyFact id="D-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="D-areaInteractionController" occur="1">
                                <booleanValue>
                                    <typedValue>false</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="E-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="E-areaInteractionController" occur="1">
                                <booleanValue>
                                    <typedValue>true</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                    </keyFactSet>
                    <keyFactSet>
                        <keyFact id="D-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="D-areaInteractionController" occur="1">
                                <booleanValue>
                                    <typedValue>true</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="E-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="E-areaInteractionController" occur="1">
                                <booleanValue>
                                    <typedValue>false</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                    </keyFactSet>
                </keyFinding>
            </keyFindings>
            <aspectReferences/>
            <ItemScoreTranslationTable>
                <ItemScoreTranslationTableEntry rawScore="0" translatedScore="0"/>
                <ItemScoreTranslationTableEntry rawScore="1" translatedScore="1"/>
            </ItemScoreTranslationTable>
        </solution>

    Private _solution3 As XElement =
        <solution>
            <keyFindings>
                <keyFinding id="areaInteractionController" scoringMethod="Dichotomous">
                    <keyFactSet>
                        <keyFact id="C-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="C-areaInteractionController" occur="1">
                                <booleanValue>
                                    <typedValue>false</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="B-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="B-areaInteractionController" occur="1">
                                <booleanValue>
                                    <typedValue>true</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="A-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="A-areaInteractionController" occur="1">
                                <booleanValue>
                                    <typedValue>true</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="D-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="D-areaInteractionController" occur="1">
                                <booleanValue>
                                    <typedValue>false</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="E-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="E-areaInteractionController" occur="1">
                                <booleanValue>
                                    <typedValue>true</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                    </keyFactSet>
                    <keyFactSet>
                        <keyFact id="A-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="A-areaInteractionController" occur="1">
                                <booleanValue>
                                    <typedValue>false</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="B-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="B-areaInteractionController" occur="1">
                                <booleanValue>
                                    <typedValue>true</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="C-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="C-areaInteractionController" occur="1">
                                <booleanValue>
                                    <typedValue>true</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="D-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="D-areaInteractionController" occur="1">
                                <booleanValue>
                                    <typedValue>false</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="E-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="E-areaInteractionController" occur="1">
                                <booleanValue>
                                    <typedValue>false</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                    </keyFactSet>
                </keyFinding>
            </keyFindings>
            <aspectReferences/>
            <ItemScoreTranslationTable>
                <ItemScoreTranslationTableEntry rawScore="0" translatedScore="0"/>
                <ItemScoreTranslationTableEntry rawScore="1" translatedScore="1"/>
            </ItemScoreTranslationTable>
        </solution>

    Private _solution4 As XElement =
        <solution>
            <keyFindings>
                <keyFinding id="areaInteractionController" scoringMethod="Dichotomous">
                    <keyFact id="A-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="A-areaInteractionController" occur="1">
                            <booleanValue>
                                <typedValue>true</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="B-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="B-areaInteractionController" occur="1">
                            <booleanValue>
                                <typedValue>true</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="C-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="C-areaInteractionController" occur="1">
                            <booleanValue>
                                <typedValue>false</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="D-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="D-areaInteractionController" occur="1">
                            <booleanValue>
                                <typedValue>false</typedValue>
                            </booleanValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="E-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="E-areaInteractionController" occur="1">
                            <booleanValue>
                                <typedValue>true</typedValue>
                            </booleanValue>
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

    Private _solution5 As XElement =
        <solution>
            <keyFindings/>
            <aspectReferences/>
            <ItemScoreTranslationTable>
                <ItemScoreTranslationTableEntry rawScore="0" translatedScore="0"/>
                <ItemScoreTranslationTableEntry rawScore="1" translatedScore="1"/>
            </ItemScoreTranslationTable>
        </solution>

#End Region

#Region "Expected results"

    Private _result1 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="multiple" baseType="identifier">
                <correctResponse interpretation="(A&amp;B)|(B&amp;C)&amp;E">
                    <value>A</value>
                    <value>B</value>
                    <value>E</value>
                </correctResponse>
            </responseDeclaration>
        </responseDeclarations>

    Private _result2 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="multiple" baseType="identifier">
                <correctResponse interpretation="(A&amp;B)|(B&amp;C)&amp;E|D">
                    <value>A</value>
                    <value>B</value>
                    <value>E</value>
                </correctResponse>
            </responseDeclaration>
        </responseDeclarations>

    Private _result3 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="multiple" baseType="identifier">
                <correctResponse interpretation="(A&amp;B&amp;E)|(B&amp;C)">
                    <value>A</value>
                    <value>B</value>
                    <value>E</value>
                </correctResponse>
            </responseDeclaration>
        </responseDeclarations>

    Private _result4 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="multiple" baseType="identifier">
                <correctResponse interpretation="A&amp;B&amp;E">
                    <value>A</value>
                    <value>B</value>
                    <value>E</value>
                </correctResponse>
            </responseDeclaration>
        </responseDeclarations>

    Private _result5 As XElement =
        <responseDeclarations>
            <responseDeclaration identifier="RESPONSE" cardinality="multiple" baseType="string"/>
        </responseDeclarations>

#End Region

End Class
