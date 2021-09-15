Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.QTI.Converters.ScoringConverter.QTI30
Imports Questify.Builder.UnitTests.Publication.QTI30

Namespace QTI30

    <TestClass()>
    Public Class ResponseProcessingCustomInteractionTests

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
        Public Sub GetResponseProcessingForCombinationOfFactSetsAndFactsOnFinding_Polytomous_Test()
            Dim scoringPrms As HashSet(Of ScoringParameter) = GetCustomInteractionScoringParams()
            PublicationTestHelper.RunResponseProcessingTest(_itemBody1, _finding1, _responseProcessing1, scoringPrms, New CombinedScoringConverter(scoringPrms))
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
        Public Sub GetResponseProcessingForCombinationOfFactSetsAndFactsOnFinding_Dichotomous_Test()
            Dim scoringPrms As HashSet(Of ScoringParameter) = GetCustomInteractionScoringParams()
            PublicationTestHelper.RunResponseProcessingTest(_itemBody1, _finding5, _responseProcessing5, scoringPrms, New CombinedScoringConverter(scoringPrms))
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
        Public Sub GetResponseProcessingForMultipleFactSets_Polytomous_Test()
            Dim scoringPrms As HashSet(Of ScoringParameter) = GetCustomInteractionScoringParams()
            PublicationTestHelper.RunResponseProcessingTest(_itemBody1, _finding2, _responseProcessing2, scoringPrms, New CombinedScoringConverter(scoringPrms))
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
        Public Sub GetResponseProcessingForMultipleFactSets_Dichotomous_Test()
            Dim scoringPrms As HashSet(Of ScoringParameter) = GetCustomInteractionScoringParams()
            PublicationTestHelper.RunResponseProcessingTest(_itemBody1, _finding6, _responseProcessing6, scoringPrms, New CombinedScoringConverter(scoringPrms))
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
        Public Sub GetResponseProcessingForFactSetTest()
            Dim scoringPrms As HashSet(Of ScoringParameter) = GetCustomInteractionScoringParams()
            PublicationTestHelper.RunResponseProcessingTest(_itemBody1, _finding3, _responseProcessing3, scoringPrms, New CombinedScoringConverter(scoringPrms))
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
        Public Sub GetResponseProcessingForFactsOnFinding_Polytomous_Test()
            Dim scoringPrms As HashSet(Of ScoringParameter) = GetCustomInteractionScoringParams()
            PublicationTestHelper.RunResponseProcessingTest(_itemBody1, _finding4, _responseProcessing4, scoringPrms, New CombinedScoringConverter(scoringPrms))
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
        Public Sub GetResponseProcessingForFactsOnFinding_Dichotomous_Test()
            Dim scoringPrms As HashSet(Of ScoringParameter) = GetCustomInteractionScoringParams()
            PublicationTestHelper.RunResponseProcessingTest(_itemBody1, _finding7, _responseProcessing7, scoringPrms, New CombinedScoringConverter(scoringPrms))
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
        Public Sub GetResponseProcessingForType3_Test()
            Dim scoringPrms As HashSet(Of ScoringParameter) = GetCustomInteractionScoringParams_Type3And4()
            PublicationTestHelper.RunResponseProcessingTest(_itemBody_Type3, _finding_Type3_1, _responseProcessing_Type3, scoringPrms, New CombinedScoringConverter(scoringPrms))
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
        Public Sub GetResponseProcessingForType4_Test()
            Dim scoringPrms As HashSet(Of ScoringParameter) = GetCustomInteractionScoringParams_Type3And4()
            PublicationTestHelper.RunResponseProcessingTest(_itemBody_Type4, _finding_Type4_1, _responseProcessing_Type4, scoringPrms, New CombinedScoringConverter(scoringPrms))
        End Sub


        Private Function GetCustomInteractionScoringParams() As HashSet(Of ScoringParameter)
            Dim scoreParams As New HashSet(Of ScoringParameter)
            Dim scoreParam1 As ScoringParameter = New IntegerScoringParameter() With {.Label = "coordinate 1", .ControllerId = "CI_SP0", .FindingOverride = "CustomInteractions", .MaxLength = 5}.AddSubParameters("1")
            scoreParams.Add(scoreParam1)
            Dim scoreParam2 As ScoringParameter = New MultiChoiceScoringParameter() With {.Label = "coordinate 2", .ControllerId = "CI_SP1", .FindingOverride = "CustomInteractions", .MinChoices = 0, .MaxChoices = 1}.AddSubParameters("A", "B", "C", "D", "E")
            scoreParams.Add(scoreParam2)
            Dim scoreParam3 As ScoringParameter = New InlineChoiceScoringParameter() With {.Label = "coordinate 3", .ControllerId = "CI_SP2", .FindingOverride = "CustomInteractions", .MinChoices = 0, .MaxChoices = 1}.AddSubParameters("A", "B", "C")
            scoreParams.Add(scoreParam3)
            Dim scoreParam4 As ScoringParameter = New InlineChoiceScoringParameter() With {.Label = "coordinate 4", .ControllerId = "CI_SP3", .FindingOverride = "CustomInteractions", .MinChoices = 0, .MaxChoices = 1}.AddSubParameters("A", "B", "C", "D", "E", "F")
            scoreParams.Add(scoreParam4)

            Return scoreParams
        End Function

        Private Function GetCustomInteractionScoringParams_Type3And4() As HashSet(Of ScoringParameter)
            Dim scoreParams As New HashSet(Of ScoringParameter)
            Dim scoreParam1 As ScoringParameter = New DecimalScoringParameter() With {.Label = "coordinate 1 - x (A)", .Name = "CI_SP0", .ControllerId = "CI_SP0", .FindingOverride = "CustomInteractions", .IntegerPartMaxLength = 10, .FractionPartMaxLength = 2}.AddSubParameters("A")
            scoreParams.Add(scoreParam1)
            Dim scoreParam2 As ScoringParameter = New DecimalScoringParameter() With {.Label = "coordinate 1 - y (B)", .Name = "CI_SP1", .ControllerId = "CI_SP1", .FindingOverride = "CustomInteractions", .IntegerPartMaxLength = 10, .FractionPartMaxLength = 2}.AddSubParameters("A")
            scoreParams.Add(scoreParam2)
            Dim scoreParam3 As ScoringParameter = New DecimalScoringParameter() With {.Label = "coordinate 2 - x (C)", .Name = "CI_SP2", .ControllerId = "CI_SP2", .FindingOverride = "CustomInteractions", .IntegerPartMaxLength = 10, .FractionPartMaxLength = 2}.AddSubParameters("A")
            scoreParams.Add(scoreParam3)
            Dim scoreParam4 As ScoringParameter = New DecimalScoringParameter() With {.Label = "coordinate 2 - y (D)", .Name = "CI_SP3", .ControllerId = "CI_SP3", .FindingOverride = "CustomInteractions", .IntegerPartMaxLength = 10, .FractionPartMaxLength = 2}.AddSubParameters("A")
            scoreParams.Add(scoreParam4)
            Dim scoreParam5 As ScoringParameter = New DecimalScoringParameter() With {.Label = "coordinate 3 - x (E)", .Name = "CI_SP4", .ControllerId = "CI_SP4", .FindingOverride = "CustomInteractions", .IntegerPartMaxLength = 10, .FractionPartMaxLength = 2}.AddSubParameters("A")
            scoreParams.Add(scoreParam5)
            Dim scoreParam6 As ScoringParameter = New DecimalScoringParameter() With {.Label = "coordinate 3 - y (F)", .Name = "CI_SP5", .ControllerId = "CI_SP5", .FindingOverride = "CustomInteractions", .IntegerPartMaxLength = 10, .FractionPartMaxLength = 2}.AddSubParameters("A")
            scoreParams.Add(scoreParam6)
            Dim scoreParam7 As ScoringParameter = New DecimalScoringParameter() With {.Label = "coordinate 4 - x (G)", .Name = "CI_SP6", .ControllerId = "CI_SP6", .FindingOverride = "CustomInteractions", .IntegerPartMaxLength = 10, .FractionPartMaxLength = 2}.AddSubParameters("A")
            scoreParams.Add(scoreParam7)
            Dim scoreParam8 As ScoringParameter = New DecimalScoringParameter() With {.Label = "coordinate 4 - y (H)", .Name = "CI_SP7", .ControllerId = "CI_SP7", .FindingOverride = "CustomInteractions", .IntegerPartMaxLength = 10, .FractionPartMaxLength = 2}.AddSubParameters("A")
            scoreParams.Add(scoreParam8)
            Return scoreParams
        End Function



        Private _itemBody1 As XElement =
        <wrapper>
            <stylesheet href="resource://package/itemstyle.css" type="text/css"/>
            <stylesheet href="resource://package/userstyle.css" type="text/css"/>
            <qti-item-body class="defaultBody">
                <div class="content">
                    <qti-custom-interaction response-identifier="CustomInteractions">
                        <prompt>Hieronder zou de Custom Interaction moeten staan...</prompt>
                        <object xmlns:html="http://www.w3.org/1999/xhtml" type="application/javascript" data="../ref/json/test.manifest.json" height="680" width="680">
                            <param name="responseLength" value="4" valuetype="DATA"/>
                        </object>
                    </qti-custom-interaction>
                </div>
            </qti-item-body>
        </wrapper>

        Private _itemBody_Type3 As XElement =
        <wrapper>
            <stylesheet href="resource://package/itemstyle.css" type="text/css"/>
            <stylesheet href="resource://package/userstyle.css" type="text/css"/>
            <qti-item-body class="defaultBody">
                <div class="content">
                    <div id="question">
                        <p id="c1-id-11">Gegeven ...</p>
                        <p id="c1-id-12">
                            <strong id="c1-id-14">Teken ...</strong>
                        </p>
                        <p id="c1-id-13"> </p>
                    </div>
                    <qti-custom-interaction response-identifier="CustomInteractions">
                        <object xmlns:html="http://www.w3.org/1999/xhtml" type="application/javascript" height="500" width="530" data="resource://package/DWOitemtype3JH1806.ci">
                            <!-- + 1 because first entry is reserved to save state -->
                            <param name="responseLength" value="1" valuetype="DATA"/>
                        </object>
                    </qti-custom-interaction>
                </div>
            </qti-item-body>
        </wrapper>

        Private _itemBody_Type4 As XElement =
        <wrapper>
            <stylesheet href="resource://package/itemstyle.css" type="text/css"/>
            <stylesheet href="resource://package/userstyle.css" type="text/css"/>
            <qti-item-body class="defaultBody">
                <div class="content">
                    <div id="question">
                        <p id="c1-id-11">Gegeven ...</p>
                        <p id="c1-id-12">
                            <strong id="c1-id-14">Teken ...</strong>
                        </p>
                        <p id="c1-id-13"> </p>
                    </div>
                    <qti-custom-interaction response-identifier="CustomInteractions">
                        <object xmlns:html="http://www.w3.org/1999/xhtml" type="application/javascript" height="500" width="530" data="resource://package/DWOitemtype4aJH1806.ci">
                            <!-- + 1 because first entry is reserved to save state -->
                            <param name="responseLength" value="1" valuetype="DATA"/>
                        </object>
                    </qti-custom-interaction>
                </div>
            </qti-item-body>
        </wrapper>



        Private _finding1 As XElement =
        <keyFinding id="CustomInteractions" scoringMethod="Polytomous">
            <keyFact id="D-CI_SP1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="CI_SP1" occur="1">
                    <stringValue>
                        <typedValue>D</typedValue>
                    </stringValue>
                </keyValue>
            </keyFact>
            <keyFact id="F-CI_SP3" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="CI_SP3" occur="1">
                    <stringValue>
                        <typedValue>F</typedValue>
                    </stringValue>
                </keyValue>
            </keyFact>
            <keyFactSet>
                <keyFact id="1-CI_SP0" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="CI_SP0" occur="1">
                        <integerValue>
                            <typedValue>12345</typedValue>
                        </integerValue>
                    </keyValue>
                </keyFact>
                <keyFact id="B-CI_SP2" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="CI_SP2" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
            <keyFactSet>
                <keyFact id="1-CI_SP0" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="CI_SP0" occur="1">
                        <integerValue>
                            <typedValue>54321</typedValue>
                        </integerValue>
                    </keyValue>
                </keyFact>
                <keyFact id="B-CI_SP2" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="CI_SP2" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
        </keyFinding>

        Private _finding2 As XElement =
        <keyFinding id="CustomInteractions" scoringMethod="Polytomous">
            <keyFactSet>
                <keyFact id="D-CI_SP1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="CI_SP1" occur="1">
                        <stringValue>
                            <typedValue>D</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
                <keyFact id="F-CI_SP3" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="CI_SP3" occur="1">
                        <stringValue>
                            <typedValue>F</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
            <keyFactSet>
                <keyFact id="1-CI_SP0" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="CI_SP0" occur="1">
                        <integerValue>
                            <typedValue>12345</typedValue>
                        </integerValue>
                    </keyValue>
                </keyFact>
                <keyFact id="B-CI_SP2" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="CI_SP2" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
            <keyFactSet>
                <keyFact id="1-CI_SP0" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="CI_SP0" occur="1">
                        <integerValue>
                            <typedValue>54321</typedValue>
                        </integerValue>
                    </keyValue>
                </keyFact>
                <keyFact id="B-CI_SP2" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="CI_SP2" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
            <keyFactSet>
                <keyFact id="C-CI_SP1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="CI_SP1" occur="1">
                        <stringValue>
                            <typedValue>C</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
                <keyFact id="E-CI_SP3" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="CI_SP3" occur="1">
                        <stringValue>
                            <typedValue>E</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
            <keyFactSet>
                <keyFact id="B-CI_SP1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="CI_SP1" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
                <keyFact id="D-CI_SP3" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="CI_SP3" occur="1">
                        <stringValue>
                            <typedValue>D</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
        </keyFinding>

        Private _finding3 As XElement =
        <keyFinding id="CustomInteractions" scoringMethod="Polytomous">
            <keyFactSet>
                <keyFact id="1-CI_SP0" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="CI_SP0" occur="1">
                        <integerValue>
                            <typedValue>12345</typedValue>
                        </integerValue>
                    </keyValue>
                </keyFact>
                <keyFact id="D-CI_SP1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="CI_SP1" occur="1">
                        <stringValue>
                            <typedValue>D</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
                <keyFact id="F-CI_SP3" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="CI_SP3" occur="1">
                        <stringValue>
                            <typedValue>F</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
                <keyFact id="B-CI_SP2" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="CI_SP2" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
            <keyFactSet>
                <keyFact id="D-CI_SP1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="CI_SP1" occur="1">
                        <stringValue>
                            <typedValue>D</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
                <keyFact id="B-CI_SP2" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="CI_SP2" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
                <keyFact id="E-CI_SP3" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="CI_SP3" occur="1">
                        <stringValue>
                            <typedValue>E</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
                <keyFact id="1-CI_SP0" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="CI_SP0" occur="1">
                        <integerValue>
                            <typedValue>54321</typedValue>
                        </integerValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
        </keyFinding>

        Private _finding4 As XElement =
       <keyFinding id="CustomInteractions" scoringMethod="Polytomous">
           <keyFact id="1-CI_SP0" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
               <keyValue domain="CI_SP0" occur="1">
                   <integerValue>
                       <typedValue>12345</typedValue>
                   </integerValue>
               </keyValue>
           </keyFact>
           <keyFact id="D-CI_SP1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
               <keyValue domain="CI_SP1" occur="1">
                   <stringValue>
                       <typedValue>D</typedValue>
                   </stringValue>
               </keyValue>
           </keyFact>
           <keyFact id="F-CI_SP3" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
               <keyValue domain="CI_SP3" occur="1">
                   <stringValue>
                       <typedValue>F</typedValue>
                   </stringValue>
               </keyValue>
           </keyFact>
           <keyFact id="B-CI_SP2" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
               <keyValue domain="CI_SP2" occur="1">
                   <stringValue>
                       <typedValue>B</typedValue>
                   </stringValue>
               </keyValue>
           </keyFact>
       </keyFinding>

        Private _finding5 As XElement =
        <keyFinding id="CustomInteractions" scoringMethod="Dichotomous">
            <keyFact id="D-CI_SP1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="CI_SP1" occur="1">
                    <stringValue>
                        <typedValue>D</typedValue>
                    </stringValue>
                </keyValue>
            </keyFact>
            <keyFact id="F-CI_SP3" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="CI_SP3" occur="1">
                    <stringValue>
                        <typedValue>F</typedValue>
                    </stringValue>
                </keyValue>
            </keyFact>
            <keyFactSet>
                <keyFact id="1-CI_SP0" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="CI_SP0" occur="1">
                        <integerValue>
                            <typedValue>12345</typedValue>
                        </integerValue>
                    </keyValue>
                </keyFact>
                <keyFact id="B-CI_SP2" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="CI_SP2" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
            <keyFactSet>
                <keyFact id="1-CI_SP0" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="CI_SP0" occur="1">
                        <integerValue>
                            <typedValue>54321</typedValue>
                        </integerValue>
                    </keyValue>
                </keyFact>
                <keyFact id="B-CI_SP2" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="CI_SP2" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
        </keyFinding>

        Private _finding6 As XElement =
        <keyFinding id="CustomInteractions" scoringMethod="Dichotomous">
            <keyFactSet>
                <keyFact id="D-CI_SP1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="CI_SP1" occur="1">
                        <stringValue>
                            <typedValue>D</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
                <keyFact id="F-CI_SP3" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="CI_SP3" occur="1">
                        <stringValue>
                            <typedValue>F</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
            <keyFactSet>
                <keyFact id="1-CI_SP0" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="CI_SP0" occur="1">
                        <integerValue>
                            <typedValue>12345</typedValue>
                        </integerValue>
                    </keyValue>
                </keyFact>
                <keyFact id="B-CI_SP2" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="CI_SP2" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
            <keyFactSet>
                <keyFact id="1-CI_SP0" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="CI_SP0" occur="1">
                        <integerValue>
                            <typedValue>54321</typedValue>
                        </integerValue>
                    </keyValue>
                </keyFact>
                <keyFact id="B-CI_SP2" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="CI_SP2" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
            <keyFactSet>
                <keyFact id="C-CI_SP1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="CI_SP1" occur="1">
                        <stringValue>
                            <typedValue>C</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
                <keyFact id="E-CI_SP3" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="CI_SP3" occur="1">
                        <stringValue>
                            <typedValue>E</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
            <keyFactSet>
                <keyFact id="B-CI_SP1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="CI_SP1" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
                <keyFact id="D-CI_SP3" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="CI_SP3" occur="1">
                        <stringValue>
                            <typedValue>D</typedValue>
                        </stringValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
        </keyFinding>

        Private _finding7 As XElement =
       <keyFinding id="CustomInteractions" scoringMethod="Dichotomous">
           <keyFact id="1-CI_SP0" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
               <keyValue domain="CI_SP0" occur="1">
                   <integerValue>
                       <typedValue>12345</typedValue>
                   </integerValue>
               </keyValue>
           </keyFact>
           <keyFact id="D-CI_SP1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
               <keyValue domain="CI_SP1" occur="1">
                   <stringValue>
                       <typedValue>D</typedValue>
                   </stringValue>
               </keyValue>
           </keyFact>
           <keyFact id="F-CI_SP3" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
               <keyValue domain="CI_SP3" occur="1">
                   <stringValue>
                       <typedValue>F</typedValue>
                   </stringValue>
               </keyValue>
           </keyFact>
           <keyFact id="B-CI_SP2" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
               <keyValue domain="CI_SP2" occur="1">
                   <stringValue>
                       <typedValue>B</typedValue>
                   </stringValue>
               </keyValue>
           </keyFact>
       </keyFinding>


        Private _finding_Type3_1 As XElement =
        <keyFinding id="CustomInteractions" scoringMethod="Dichotomous">
            <keyFactSet>
                <keyFact id="A-CI_SP0" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="CI_SP0" occur="1">
                        <decimalRangeValue rangeEnd="1.1" rangeStart="0.9"/>
                    </keyValue>
                </keyFact>
                <keyFact id="A-CI_SP1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="CI_SP1" occur="1">
                        <decimalRangeValue rangeEnd="1.1" rangeStart="0.9"/>
                    </keyValue>
                </keyFact>
            </keyFactSet>
            <keyFactSet>
                <keyFact id="A-CI_SP2" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="CI_SP2" occur="1">
                        <decimalRangeValue rangeEnd="2.1" rangeStart="1.9"/>
                    </keyValue>
                </keyFact>
                <keyFact id="A-CI_SP3" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="CI_SP3" occur="1">
                        <decimalRangeValue rangeEnd="2.1" rangeStart="1.9"/>
                    </keyValue>
                </keyFact>
            </keyFactSet>
            <keyFactSet>
                <keyFact id="A-CI_SP4" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="CI_SP4" occur="1">
                        <decimalRangeValue rangeEnd="3.1" rangeStart="2.9"/>
                    </keyValue>
                </keyFact>
                <keyFact id="A-CI_SP5" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="CI_SP5" occur="1">
                        <decimalRangeValue rangeEnd="3.1" rangeStart="2.9"/>
                    </keyValue>
                </keyFact>
            </keyFactSet>
            <keyFactSet>
                <keyFact id="A-CI_SP6" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="CI_SP6" occur="1">
                        <decimalRangeValue rangeEnd="4.1" rangeStart="3.9"/>
                    </keyValue>
                </keyFact>
                <keyFact id="A-CI_SP7" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="CI_SP7" occur="1">
                        <decimalRangeValue rangeEnd="4.1" rangeStart="3.9"/>
                    </keyValue>
                </keyFact>
            </keyFactSet>
        </keyFinding>

        Private _finding_Type4_1 As XElement =
        <keyFinding id="CustomInteractions" scoringMethod="Dichotomous">
            <keyFactSet>
                <keyFact id="A-CI_SP0" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="CI_SP0" occur="1">
                        <decimalValue>
                            <typedValue>1</typedValue>
                        </decimalValue>
                    </keyValue>
                </keyFact>
                <keyFact id="A-CI_SP1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="CI_SP1" occur="1">
                        <decimalValue>
                            <typedValue>3</typedValue>
                        </decimalValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
            <keyFactSet>
                <keyFact id="A-CI_SP2" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="CI_SP2" occur="1">
                        <decimalValue>
                            <typedValue>2</typedValue>
                        </decimalValue>
                    </keyValue>
                </keyFact>
                <keyFact id="A-CI_SP3" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="CI_SP3" occur="1">
                        <decimalValue>
                            <typedValue>6</typedValue>
                        </decimalValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
            <keyFactSet>
                <keyFact id="A-CI_SP4" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="CI_SP4" occur="1">
                        <decimalValue>
                            <typedValue>3</typedValue>
                        </decimalValue>
                    </keyValue>
                </keyFact>
                <keyFact id="A-CI_SP5" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="CI_SP5" occur="1">
                        <decimalValue>
                            <typedValue>9</typedValue>
                        </decimalValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
            <keyFactSet>
                <keyFact id="A-CI_SP6" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="CI_SP6" occur="1">
                        <decimalValue>
                            <typedValue>4</typedValue>
                        </decimalValue>
                    </keyValue>
                </keyFact>
                <keyFact id="A-CI_SP7" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="CI_SP7" occur="1">
                        <decimalValue>
                            <typedValue>12</typedValue>
                        </decimalValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
        </keyFinding>



        Private _responseProcessing1 As XElement =
<qti-response-processing>
    <qti-response-condition>
        <qti-response-if>
            <qti-or>
                <qti-and>
                    <qti-equal tolerance-mode="exact">
                        <qti-index n="2">
                            <qti-variable identifier="RESPONSE"/>
                        </qti-index>
                        <qti-base-value base-type="integer">12345</qti-base-value>
                    </qti-equal>
                    <qti-match>
                        <qti-index n="4">
                            <qti-variable identifier="RESPONSE"/>
                        </qti-index>
                        <qti-base-value base-type="identifier">B</qti-base-value>
                    </qti-match>
                </qti-and>
                <qti-and>
                    <qti-equal tolerance-mode="exact">
                        <qti-index n="2">
                            <qti-variable identifier="RESPONSE"/>
                        </qti-index>
                        <qti-base-value base-type="integer">54321</qti-base-value>
                    </qti-equal>
                    <qti-match>
                        <qti-index n="4">
                            <qti-variable identifier="RESPONSE"/>
                        </qti-index>
                        <qti-base-value base-type="identifier">B</qti-base-value>
                    </qti-match>
                </qti-and>
            </qti-or>
            <qti-set-outcome-value identifier="SCORE">
                <qti-sum>
                    <qti-base-value base-type="float">1</qti-base-value>
                    <qti-variable identifier="SCORE"/>
                </qti-sum>
            </qti-set-outcome-value>
        </qti-response-if>
    </qti-response-condition>
    <qti-response-condition>
        <qti-response-if>
            <qti-member>
                <qti-base-value base-type="identifier">D</qti-base-value>
                <qti-index n="3">
                    <qti-variable identifier="RESPONSE"/>
                </qti-index>
            </qti-member>
            <qti-set-outcome-value identifier="SCORE">
                <qti-sum>
                    <qti-base-value base-type="float">1</qti-base-value>
                    <qti-variable identifier="SCORE"/>
                </qti-sum>
            </qti-set-outcome-value>
        </qti-response-if>
    </qti-response-condition>
    <qti-response-condition>
        <qti-response-if>
            <qti-match>
                <qti-index n="5">
                    <qti-variable identifier="RESPONSE"/>
                </qti-index>
                <qti-base-value base-type="identifier">F</qti-base-value>
            </qti-match>
            <qti-set-outcome-value identifier="SCORE">
                <qti-sum>
                    <qti-base-value base-type="float">1</qti-base-value>
                    <qti-variable identifier="SCORE"/>
                </qti-sum>
            </qti-set-outcome-value>
        </qti-response-if>
    </qti-response-condition>
</qti-response-processing>

        Private _responseProcessing2 As XElement =
<qti-response-processing>
    <qti-response-condition>
        <qti-response-if>
            <qti-or>
                <qti-and>
                    <qti-equal tolerance-mode="exact">
                        <qti-index n="2">
                            <qti-variable identifier="RESPONSE"/>
                        </qti-index>
                        <qti-base-value base-type="integer">12345</qti-base-value>
                    </qti-equal>
                    <qti-match>
                        <qti-index n="4">
                            <qti-variable identifier="RESPONSE"/>
                        </qti-index>
                        <qti-base-value base-type="identifier">B</qti-base-value>
                    </qti-match>
                </qti-and>
                <qti-and>
                    <qti-equal tolerance-mode="exact">
                        <qti-index n="2">
                            <qti-variable identifier="RESPONSE"/>
                        </qti-index>
                        <qti-base-value base-type="integer">54321</qti-base-value>
                    </qti-equal>
                    <qti-match>
                        <qti-index n="4">
                            <qti-variable identifier="RESPONSE"/>
                        </qti-index>
                        <qti-base-value base-type="identifier">B</qti-base-value>
                    </qti-match>
                </qti-and>
            </qti-or>
            <qti-set-outcome-value identifier="SCORE">
                <qti-sum>
                    <qti-base-value base-type="float">1</qti-base-value>
                    <qti-variable identifier="SCORE"/>
                </qti-sum>
            </qti-set-outcome-value>
        </qti-response-if>
    </qti-response-condition>
    <qti-response-condition>
        <qti-response-if>
            <qti-or>
                <qti-and>
                    <qti-member>
                        <qti-base-value base-type="identifier">D</qti-base-value>
                        <qti-index n="3">
                            <qti-variable identifier="RESPONSE"/>
                        </qti-index>
                    </qti-member>
                    <qti-match>
                        <qti-index n="5">
                            <qti-variable identifier="RESPONSE"/>
                        </qti-index>
                        <qti-base-value base-type="identifier">F</qti-base-value>
                    </qti-match>
                </qti-and>
                <qti-and>
                    <qti-member>
                        <qti-base-value base-type="identifier">C</qti-base-value>
                        <qti-index n="3">
                            <qti-variable identifier="RESPONSE"/>
                        </qti-index>
                    </qti-member>
                    <qti-match>
                        <qti-index n="5">
                            <qti-variable identifier="RESPONSE"/>
                        </qti-index>
                        <qti-base-value base-type="identifier">E</qti-base-value>
                    </qti-match>
                </qti-and>
                <qti-and>
                    <qti-member>
                        <qti-base-value base-type="identifier">B</qti-base-value>
                        <qti-index n="3">
                            <qti-variable identifier="RESPONSE"/>
                        </qti-index>
                    </qti-member>
                    <qti-match>
                        <qti-index n="5">
                            <qti-variable identifier="RESPONSE"/>
                        </qti-index>
                        <qti-base-value base-type="identifier">D</qti-base-value>
                    </qti-match>
                </qti-and>
            </qti-or>
            <qti-set-outcome-value identifier="SCORE">
                <qti-sum>
                    <qti-base-value base-type="float">1</qti-base-value>
                    <qti-variable identifier="SCORE"/>
                </qti-sum>
            </qti-set-outcome-value>
        </qti-response-if>
    </qti-response-condition>
</qti-response-processing>

        Private _responseProcessing3 As XElement =
<qti-response-processing>
    <qti-response-condition>
        <qti-response-if>
            <qti-or>
                <qti-and>
                    <qti-equal tolerance-mode="exact">
                        <qti-index n="2">
                            <qti-variable identifier="RESPONSE"/>
                        </qti-index>
                        <qti-base-value base-type="integer">12345</qti-base-value>
                    </qti-equal>
                    <qti-member>
                        <qti-base-value base-type="identifier">D</qti-base-value>
                        <qti-index n="3">
                            <qti-variable identifier="RESPONSE"/>
                        </qti-index>
                    </qti-member>
                    <qti-match>
                        <qti-index n="4">
                            <qti-variable identifier="RESPONSE"/>
                        </qti-index>
                        <qti-base-value base-type="identifier">B</qti-base-value>
                    </qti-match>
                    <qti-match>
                        <qti-index n="5">
                            <qti-variable identifier="RESPONSE"/>
                        </qti-index>
                        <qti-base-value base-type="identifier">F</qti-base-value>
                    </qti-match>
                </qti-and>
                <qti-and>
                    <qti-equal tolerance-mode="exact">
                        <qti-index n="2">
                            <qti-variable identifier="RESPONSE"/>
                        </qti-index>
                        <qti-base-value base-type="integer">54321</qti-base-value>
                    </qti-equal>
                    <qti-member>
                        <qti-base-value base-type="identifier">D</qti-base-value>
                        <qti-index n="3">
                            <qti-variable identifier="RESPONSE"/>
                        </qti-index>
                    </qti-member>
                    <qti-match>
                        <qti-index n="4">
                            <qti-variable identifier="RESPONSE"/>
                        </qti-index>
                        <qti-base-value base-type="identifier">B</qti-base-value>
                    </qti-match>
                    <qti-match>
                        <qti-index n="5">
                            <qti-variable identifier="RESPONSE"/>
                        </qti-index>
                        <qti-base-value base-type="identifier">E</qti-base-value>
                    </qti-match>
                </qti-and>
            </qti-or>
            <qti-set-outcome-value identifier="SCORE">
                <qti-sum>
                    <qti-base-value base-type="float">1</qti-base-value>
                    <qti-variable identifier="SCORE"/>
                </qti-sum>
            </qti-set-outcome-value>
        </qti-response-if>
    </qti-response-condition>
</qti-response-processing>

        Private _responseProcessing4 As XElement =
<qti-response-processing>
    <qti-response-condition>
        <qti-response-if>
            <qti-equal tolerance-mode="exact">
                <qti-index n="2">
                    <qti-variable identifier="RESPONSE"/>
                </qti-index>
                <qti-base-value base-type="integer">12345</qti-base-value>
            </qti-equal>
            <qti-set-outcome-value identifier="SCORE">
                <qti-sum>
                    <qti-base-value base-type="float">1</qti-base-value>
                    <qti-variable identifier="SCORE"/>
                </qti-sum>
            </qti-set-outcome-value>
        </qti-response-if>
    </qti-response-condition>
    <qti-response-condition>
        <qti-response-if>
            <qti-member>
                <qti-base-value base-type="identifier">D</qti-base-value>
                <qti-index n="3">
                    <qti-variable identifier="RESPONSE"/>
                </qti-index>
            </qti-member>
            <qti-set-outcome-value identifier="SCORE">
                <qti-sum>
                    <qti-base-value base-type="float">1</qti-base-value>
                    <qti-variable identifier="SCORE"/>
                </qti-sum>
            </qti-set-outcome-value>
        </qti-response-if>
    </qti-response-condition>
    <qti-response-condition>
        <qti-response-if>
            <qti-match>
                <qti-index n="4">
                    <qti-variable identifier="RESPONSE"/>
                </qti-index>
                <qti-base-value base-type="identifier">B</qti-base-value>
            </qti-match>
            <qti-set-outcome-value identifier="SCORE">
                <qti-sum>
                    <qti-base-value base-type="float">1</qti-base-value>
                    <qti-variable identifier="SCORE"/>
                </qti-sum>
            </qti-set-outcome-value>
        </qti-response-if>
    </qti-response-condition>
    <qti-response-condition>
        <qti-response-if>
            <qti-match>
                <qti-index n="5">
                    <qti-variable identifier="RESPONSE"/>
                </qti-index>
                <qti-base-value base-type="identifier">F</qti-base-value>
            </qti-match>
            <qti-set-outcome-value identifier="SCORE">
                <qti-sum>
                    <qti-base-value base-type="float">1</qti-base-value>
                    <qti-variable identifier="SCORE"/>
                </qti-sum>
            </qti-set-outcome-value>
        </qti-response-if>
    </qti-response-condition>
</qti-response-processing>

        Private _responseProcessing5 As XElement =
<qti-response-processing>
    <qti-response-condition>
        <qti-response-if>
            <qti-and>
                <qti-or>
                    <qti-and>
                        <qti-equal tolerance-mode="exact">
                            <qti-index n="2">
                                <qti-variable identifier="RESPONSE"/>
                            </qti-index>
                            <qti-base-value base-type="integer">12345</qti-base-value>
                        </qti-equal>
                        <qti-match>
                            <qti-index n="4">
                                <qti-variable identifier="RESPONSE"/>
                            </qti-index>
                            <qti-base-value base-type="identifier">B</qti-base-value>
                        </qti-match>
                    </qti-and>
                    <qti-and>
                        <qti-equal tolerance-mode="exact">
                            <qti-index n="2">
                                <qti-variable identifier="RESPONSE"/>
                            </qti-index>
                            <qti-base-value base-type="integer">54321</qti-base-value>
                        </qti-equal>
                        <qti-match>
                            <qti-index n="4">
                                <qti-variable identifier="RESPONSE"/>
                            </qti-index>
                            <qti-base-value base-type="identifier">B</qti-base-value>
                        </qti-match>
                    </qti-and>
                </qti-or>
                <qti-member>
                    <qti-base-value base-type="identifier">D</qti-base-value>
                    <qti-index n="3">
                        <qti-variable identifier="RESPONSE"/>
                    </qti-index>
                </qti-member>
                <qti-match>
                    <qti-index n="5">
                        <qti-variable identifier="RESPONSE"/>
                    </qti-index>
                    <qti-base-value base-type="identifier">F</qti-base-value>
                </qti-match>
            </qti-and>
            <qti-set-outcome-value identifier="SCORE">
                <qti-sum>
                    <qti-base-value base-type="float">1</qti-base-value>
                    <qti-variable identifier="SCORE"/>
                </qti-sum>
            </qti-set-outcome-value>
        </qti-response-if>
    </qti-response-condition>
    <qti-response-condition>
        <qti-response-if>
            <qti-gte>
                <qti-variable identifier="SCORE"/>
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-gte>
            <qti-set-outcome-value identifier="SCORE">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
        <qti-response-else>
            <qti-set-outcome-value identifier="SCORE">
                <qti-base-value base-type="float">0</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else>
    </qti-response-condition>
</qti-response-processing>

        Private _responseProcessing6 As XElement =
<qti-response-processing>
    <qti-response-condition>
        <qti-response-if>
            <qti-and>
                <qti-or>
                    <qti-and>
                        <qti-equal tolerance-mode="exact">
                            <qti-index n="2">
                                <qti-variable identifier="RESPONSE"/>
                            </qti-index>
                            <qti-base-value base-type="integer">12345</qti-base-value>
                        </qti-equal>
                        <qti-match>
                            <qti-index n="4">
                                <qti-variable identifier="RESPONSE"/>
                            </qti-index>
                            <qti-base-value base-type="identifier">B</qti-base-value>
                        </qti-match>
                    </qti-and>
                    <qti-and>
                        <qti-equal tolerance-mode="exact">
                            <qti-index n="2">
                                <qti-variable identifier="RESPONSE"/>
                            </qti-index>
                            <qti-base-value base-type="integer">54321</qti-base-value>
                        </qti-equal>
                        <qti-match>
                            <qti-index n="4">
                                <qti-variable identifier="RESPONSE"/>
                            </qti-index>
                            <qti-base-value base-type="identifier">B</qti-base-value>
                        </qti-match>
                    </qti-and>
                </qti-or>
                <qti-or>
                    <qti-and>
                        <qti-member>
                            <qti-base-value base-type="identifier">D</qti-base-value>
                            <qti-index n="3">
                                <qti-variable identifier="RESPONSE"/>
                            </qti-index>
                        </qti-member>
                        <qti-match>
                            <qti-index n="5">
                                <qti-variable identifier="RESPONSE"/>
                            </qti-index>
                            <qti-base-value base-type="identifier">F</qti-base-value>
                        </qti-match>
                    </qti-and>
                    <qti-and>
                        <qti-member>
                            <qti-base-value base-type="identifier">C</qti-base-value>
                            <qti-index n="3">
                                <qti-variable identifier="RESPONSE"/>
                            </qti-index>
                        </qti-member>
                        <qti-match>
                            <qti-index n="5">
                                <qti-variable identifier="RESPONSE"/>
                            </qti-index>
                            <qti-base-value base-type="identifier">E</qti-base-value>
                        </qti-match>
                    </qti-and>
                    <qti-and>
                        <qti-member>
                            <qti-base-value base-type="identifier">B</qti-base-value>
                            <qti-index n="3">
                                <qti-variable identifier="RESPONSE"/>
                            </qti-index>
                        </qti-member>
                        <qti-match>
                            <qti-index n="5">
                                <qti-variable identifier="RESPONSE"/>
                            </qti-index>
                            <qti-base-value base-type="identifier">D</qti-base-value>
                        </qti-match>
                    </qti-and>
                </qti-or>
            </qti-and>
            <qti-set-outcome-value identifier="SCORE">
                <qti-sum>
                    <qti-base-value base-type="float">1</qti-base-value>
                    <qti-variable identifier="SCORE"/>
                </qti-sum>
            </qti-set-outcome-value>
        </qti-response-if>
    </qti-response-condition>
    <qti-response-condition>
        <qti-response-if>
            <qti-gte>
                <qti-variable identifier="SCORE"/>
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-gte>
            <qti-set-outcome-value identifier="SCORE">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
        <qti-response-else>
            <qti-set-outcome-value identifier="SCORE">
                <qti-base-value base-type="float">0</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else>
    </qti-response-condition>
</qti-response-processing>

        Private _responseProcessing7 As XElement =
<qti-response-processing>
    <qti-response-condition>
        <qti-response-if>
            <qti-and>
                <qti-equal tolerance-mode="exact">
                    <qti-index n="2">
                        <qti-variable identifier="RESPONSE"/>
                    </qti-index>
                    <qti-base-value base-type="integer">12345</qti-base-value>
                </qti-equal>
                <qti-member>
                    <qti-base-value base-type="identifier">D</qti-base-value>
                    <qti-index n="3">
                        <qti-variable identifier="RESPONSE"/>
                    </qti-index>
                </qti-member>
                <qti-match>
                    <qti-index n="4">
                        <qti-variable identifier="RESPONSE"/>
                    </qti-index>
                    <qti-base-value base-type="identifier">B</qti-base-value>
                </qti-match>
                <qti-match>
                    <qti-index n="5">
                        <qti-variable identifier="RESPONSE"/>
                    </qti-index>
                    <qti-base-value base-type="identifier">F</qti-base-value>
                </qti-match>
            </qti-and>
            <qti-set-outcome-value identifier="SCORE">
                <qti-sum>
                    <qti-base-value base-type="float">1</qti-base-value>
                    <qti-variable identifier="SCORE"/>
                </qti-sum>
            </qti-set-outcome-value>
        </qti-response-if>
    </qti-response-condition>
    <qti-response-condition>
        <qti-response-if>
            <qti-gte>
                <qti-variable identifier="SCORE"/>
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-gte>
            <qti-set-outcome-value identifier="SCORE">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
        <qti-response-else>
            <qti-set-outcome-value identifier="SCORE">
                <qti-base-value base-type="float">0</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else>
    </qti-response-condition>
</qti-response-processing>

        Private _responseProcessing_Type3 As XElement =
<qti-response-processing>
    <qti-response-condition>
        <qti-response-if>
            <qti-and>
                <qti-and>
                    <qti-and>
                        <qti-gte>
                            <qti-index n="2">
                                <qti-variable identifier="RESPONSE"/>
                            </qti-index>
                            <qti-base-value base-type="float">0.9</qti-base-value>
                        </qti-gte>
                        <qti-lte>
                            <qti-index n="2">
                                <qti-variable identifier="RESPONSE"/>
                            </qti-index>
                            <qti-base-value base-type="float">1.1</qti-base-value>
                        </qti-lte>
                    </qti-and>
                    <qti-and>
                        <qti-gte>
                            <qti-index n="3">
                                <qti-variable identifier="RESPONSE"/>
                            </qti-index>
                            <qti-base-value base-type="float">0.9</qti-base-value>
                        </qti-gte>
                        <qti-lte>
                            <qti-index n="3">
                                <qti-variable identifier="RESPONSE"/>
                            </qti-index>
                            <qti-base-value base-type="float">1.1</qti-base-value>
                        </qti-lte>
                    </qti-and>
                </qti-and>
                <qti-and>
                    <qti-and>
                        <qti-gte>
                            <qti-index n="4">
                                <qti-variable identifier="RESPONSE"/>
                            </qti-index>
                            <qti-base-value base-type="float">1.9</qti-base-value>
                        </qti-gte>
                        <qti-lte>
                            <qti-index n="4">
                                <qti-variable identifier="RESPONSE"/>
                            </qti-index>
                            <qti-base-value base-type="float">2.1</qti-base-value>
                        </qti-lte>
                    </qti-and>
                    <qti-and>
                        <qti-gte>
                            <qti-index n="5">
                                <qti-variable identifier="RESPONSE"/>
                            </qti-index>
                            <qti-base-value base-type="float">1.9</qti-base-value>
                        </qti-gte>
                        <qti-lte>
                            <qti-index n="5">
                                <qti-variable identifier="RESPONSE"/>
                            </qti-index>
                            <qti-base-value base-type="float">2.1</qti-base-value>
                        </qti-lte>
                    </qti-and>
                </qti-and>
                <qti-and>
                    <qti-and>
                        <qti-gte>
                            <qti-index n="6">
                                <qti-variable identifier="RESPONSE"/>
                            </qti-index>
                            <qti-base-value base-type="float">2.9</qti-base-value>
                        </qti-gte>
                        <qti-lte>
                            <qti-index n="6">
                                <qti-variable identifier="RESPONSE"/>
                            </qti-index>
                            <qti-base-value base-type="float">3.1</qti-base-value>
                        </qti-lte>
                    </qti-and>
                    <qti-and>
                        <qti-gte>
                            <qti-index n="7">
                                <qti-variable identifier="RESPONSE"/>
                            </qti-index>
                            <qti-base-value base-type="float">2.9</qti-base-value>
                        </qti-gte>
                        <qti-lte>
                            <qti-index n="7">
                                <qti-variable identifier="RESPONSE"/>
                            </qti-index>
                            <qti-base-value base-type="float">3.1</qti-base-value>
                        </qti-lte>
                    </qti-and>
                </qti-and>
                <qti-and>
                    <qti-and>
                        <qti-gte>
                            <qti-index n="8">
                                <qti-variable identifier="RESPONSE"/>
                            </qti-index>
                            <qti-base-value base-type="float">3.9</qti-base-value>
                        </qti-gte>
                        <qti-lte>
                            <qti-index n="8">
                                <qti-variable identifier="RESPONSE"/>
                            </qti-index>
                            <qti-base-value base-type="float">4.1</qti-base-value>
                        </qti-lte>
                    </qti-and>
                    <qti-and>
                        <qti-gte>
                            <qti-index n="9">
                                <qti-variable identifier="RESPONSE"/>
                            </qti-index>
                            <qti-base-value base-type="float">3.9</qti-base-value>
                        </qti-gte>
                        <qti-lte>
                            <qti-index n="9">
                                <qti-variable identifier="RESPONSE"/>
                            </qti-index>
                            <qti-base-value base-type="float">4.1</qti-base-value>
                        </qti-lte>
                    </qti-and>
                </qti-and>
            </qti-and>
            <qti-set-outcome-value identifier="SCORE">
                <qti-sum>
                    <qti-base-value base-type="float">1</qti-base-value>
                    <qti-variable identifier="SCORE"/>
                </qti-sum>
            </qti-set-outcome-value>
        </qti-response-if>
    </qti-response-condition>
    <qti-response-condition>
        <qti-response-if>
            <qti-gte>
                <qti-variable identifier="SCORE"/>
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-gte>
            <qti-set-outcome-value identifier="SCORE">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
        <qti-response-else>
            <qti-set-outcome-value identifier="SCORE">
                <qti-base-value base-type="float">0</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else>
    </qti-response-condition>
</qti-response-processing>

        Private _responseProcessing_Type4 As XElement =
<qti-response-processing>
    <qti-response-condition>
        <qti-response-if>
            <qti-and>
                <qti-and>
                    <qti-equal tolerance-mode="exact">
                        <qti-index n="2">
                            <qti-variable identifier="RESPONSE"/>
                        </qti-index>
                        <qti-base-value base-type="float">1</qti-base-value>
                    </qti-equal>
                    <qti-equal tolerance-mode="exact">
                        <qti-index n="3">
                            <qti-variable identifier="RESPONSE"/>
                        </qti-index>
                        <qti-base-value base-type="float">3</qti-base-value>
                    </qti-equal>
                </qti-and>
                <qti-and>
                    <qti-equal tolerance-mode="exact">
                        <qti-index n="4">
                            <qti-variable identifier="RESPONSE"/>
                        </qti-index>
                        <qti-base-value base-type="float">2</qti-base-value>
                    </qti-equal>
                    <qti-equal tolerance-mode="exact">
                        <qti-index n="5">
                            <qti-variable identifier="RESPONSE"/>
                        </qti-index>
                        <qti-base-value base-type="float">6</qti-base-value>
                    </qti-equal>
                </qti-and>
                <qti-and>
                    <qti-equal tolerance-mode="exact">
                        <qti-index n="6">
                            <qti-variable identifier="RESPONSE"/>
                        </qti-index>
                        <qti-base-value base-type="float">3</qti-base-value>
                    </qti-equal>
                    <qti-equal tolerance-mode="exact">
                        <qti-index n="7">
                            <qti-variable identifier="RESPONSE"/>
                        </qti-index>
                        <qti-base-value base-type="float">9</qti-base-value>
                    </qti-equal>
                </qti-and>
                <qti-and>
                    <qti-equal tolerance-mode="exact">
                        <qti-index n="8">
                            <qti-variable identifier="RESPONSE"/>
                        </qti-index>
                        <qti-base-value base-type="float">4</qti-base-value>
                    </qti-equal>
                    <qti-equal tolerance-mode="exact">
                        <qti-index n="9">
                            <qti-variable identifier="RESPONSE"/>
                        </qti-index>
                        <qti-base-value base-type="float">12</qti-base-value>
                    </qti-equal>
                </qti-and>
            </qti-and>
            <qti-set-outcome-value identifier="SCORE">
                <qti-sum>
                    <qti-base-value base-type="float">1</qti-base-value>
                    <qti-variable identifier="SCORE"/>
                </qti-sum>
            </qti-set-outcome-value>
        </qti-response-if>
    </qti-response-condition>
    <qti-response-condition>
        <qti-response-if>
            <qti-gte>
                <qti-variable identifier="SCORE"/>
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-gte>
            <qti-set-outcome-value identifier="SCORE">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
        <qti-response-else>
            <qti-set-outcome-value identifier="SCORE">
                <qti-base-value base-type="float">0</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else>
    </qti-response-condition>
</qti-response-processing>


    End Class

End Namespace
