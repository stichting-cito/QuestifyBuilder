
Imports Cito.Tester.ContentModel
Imports Questify.Builder.UnitTests.Publication.QTI30

Namespace QTI30

    <TestClass()>
    Public Class ResponseDeclarationHottextCorrectionsTests

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesresponseDeclaration")>
        Public Sub GetResponseDeclarationForFactsOnFindingTest()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution1, GetHottextCorrectionScoringParams, _result1, 12)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesresponseDeclaration")>
        Public Sub GetResponseDeclarationForCombinationOfFactSetsAndFactsOnFindingTest()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution2, GetHottextCorrectionScoringParams, _result2, 12)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesresponseDeclaration")>
        Public Sub GetResponseDeclarationForMultipleFactSetsTest()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution3, GetHottextCorrectionScoringParams, _result3, 12)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesresponseDeclaration")>
        Public Sub GetResponseDeclarationForFactSetsTest()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution4, GetHottextCorrectionScoringParams, _result4, 12)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesresponseDeclaration")>
        Public Sub GetEmptyResponseDeclarationTest()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution5, GetHottextCorrectionScoringParams, _result5, 12)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesresponseDeclaration")>
        Public Sub GetEmptyResponseDeclarationTest_II()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution6, GetHottextCorrectionScoringParams, _result6, 12)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesresponseDeclaration")>
        Public Sub GetResponseDeclarationForFactSetsTest_II()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody2, _solution7, GetHottextCorrectionScoringParams_II, _result7, 5)
        End Sub

#Region "Scoring parameters"

        Private Function GetHottextCorrectionScoringParams() As HashSet(Of ScoringParameter)
            Dim scoreParams As New HashSet(Of ScoringParameter)
            Dim scoreParam As HotTextScoringParameter = New HotTextScoringParameter() With {.ControllerId = "hottextController", .FindingOverride = "hottextController", .MinChoices = 1, .MaxChoices = 10, .IsCorrectionVariant = True}.AddSubParameters("Ice5853d6-5a4b-4fc6-9298-7bc4047d0e26", "I1752ec64-4652-4723-b3b0-0404b15a0e6d", "Iff799b19-6c0e-406a-ad0d-63f470eac66f", "I88bb2ba0-543d-44e5-977a-1754f8a1d505", "I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a", "I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b", "I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea", "I941e9a9a-fd22-43a4-acf2-aa862f943cfc", "I6eb7f951-31d1-46e2-89fb-c7eca3849f9d", "I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4", "Iad84d52a-4b1e-45b3-8067-96811e46ff7e")

            Dim correctionScoreParamSubSet As New Dictionary(Of String, String)
            correctionScoreParamSubSet.Add("Ice5853d6-5a4b-4fc6-9298-7bc4047d0e26", "een")
            correctionScoreParamSubSet.Add("I1752ec64-4652-4723-b3b0-0404b15a0e6d", "twee")
            correctionScoreParamSubSet.Add("Iff799b19-6c0e-406a-ad0d-63f470eac66f", "drie")
            correctionScoreParamSubSet.Add("I88bb2ba0-543d-44e5-977a-1754f8a1d505", "vier")
            correctionScoreParamSubSet.Add("I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a", "vijf")
            correctionScoreParamSubSet.Add("I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b", "zes")
            correctionScoreParamSubSet.Add("I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea", "Heer")
            correctionScoreParamSubSet.Add("I941e9a9a-fd22-43a4-acf2-aa862f943cfc", "acht")
            correctionScoreParamSubSet.Add("I6eb7f951-31d1-46e2-89fb-c7eca3849f9d", "negen")
            correctionScoreParamSubSet.Add("I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4", "tien")
            correctionScoreParamSubSet.Add("Iad84d52a-4b1e-45b3-8067-96811e46ff7e", "elf")

            Dim xhtmlValue As XElement = <xhtmlparameter name="hottexttext">
                                             <p id="c1-id-11" xmlns="http://www.w3.org/1999/xhtml">Op vrijwel alle <cito:InlineElement id="Ice5853d6-5a4b-4fc6-9298-7bc4047d0e26" layoutTemplateSourceName="InlineHottextCorrectionLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                     <cito:parameters>
                                                         <cito:parameterSet id="entireItem">
                                                             <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                             <cito:plaintextparameter name="controlId">Ice5853d6-5a4b-4fc6-9298-7bc4047d0e26</cito:plaintextparameter>
                                                             <cito:plaintextparameter name="controlLabel">een</cito:plaintextparameter>
                                                             <cito:booleanparameter name="addHottextCorrection">True</cito:booleanparameter>
                                                             <cito:plaintextparameter name="hottextValue"/>
                                                             <cito:hotTextCorrectionScoringParameter name="hotTextCorrectionScoring" ControllerId="hottextCorrectionController" findingOverride="hottextController" expected-length="0" correctionIsApplicable="true">
                                                                 <cito:subparameterset id="Input"/>
                                                                 <cito:definition id=""/>
                                                                 <cito:relatedControlLabel name="controlLabel">een</cito:relatedControlLabel>
                                                             </cito:hotTextCorrectionScoringParameter>
                                                         </cito:parameterSet>
                                                     </cito:parameters>
                                                 </cito:InlineElement><span id="SIce5853d6-5a4b-4fc6-9298-7bc4047d0e26" style="background-color: #C7B8CE;">een</span> meren is in de zomer veel te doen: ze zijn <cito:InlineElement id="I1752ec64-4652-4723-b3b0-0404b15a0e6d" layoutTemplateSourceName="InlineHottextCorrectionLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                     <cito:parameters>
                                                         <cito:parameterSet id="entireItem">
                                                             <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                             <cito:plaintextparameter name="controlId">I1752ec64-4652-4723-b3b0-0404b15a0e6d</cito:plaintextparameter>
                                                             <cito:plaintextparameter name="controlLabel">twee</cito:plaintextparameter>
                                                             <cito:booleanparameter name="addHottextCorrection">True</cito:booleanparameter>
                                                             <cito:plaintextparameter name="hottextValue"/>
                                                             <cito:hotTextCorrectionScoringParameter name="hotTextCorrectionScoring" ControllerId="hottextCorrectionController" findingOverride="hottextController" expected-length="0" correctionIsApplicable="true">
                                                                 <cito:subparameterset id="Input"/>
                                                                 <cito:definition id=""/>
                                                                 <cito:relatedControlLabel name="controlLabel">twee</cito:relatedControlLabel>
                                                             </cito:hotTextCorrectionScoringParameter>
                                                         </cito:parameterSet>
                                                     </cito:parameters>
                                                 </cito:InlineElement><span id="SI1752ec64-4652-4723-b3b0-0404b15a0e6d" style="background-color: #C7B8CE;">twee</span> geschikt voor <cito:InlineElement id="Iff799b19-6c0e-406a-ad0d-63f470eac66f" layoutTemplateSourceName="InlineHottextCorrectionLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                     <cito:parameters>
                                                         <cito:parameterSet id="entireItem">
                                                             <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                             <cito:plaintextparameter name="controlId">Iff799b19-6c0e-406a-ad0d-63f470eac66f</cito:plaintextparameter>
                                                             <cito:plaintextparameter name="controlLabel">drie</cito:plaintextparameter>
                                                             <cito:booleanparameter name="addHottextCorrection">True</cito:booleanparameter>
                                                             <cito:plaintextparameter name="hottextValue"/>
                                                             <cito:hotTextCorrectionScoringParameter name="hotTextCorrectionScoring" ControllerId="hottextCorrectionController" findingOverride="hottextController" expected-length="0" correctionIsApplicable="true">
                                                                 <cito:subparameterset id="Input"/>
                                                                 <cito:definition id=""/>
                                                                 <cito:relatedControlLabel name="controlLabel">drie</cito:relatedControlLabel>
                                                             </cito:hotTextCorrectionScoringParameter>
                                                         </cito:parameterSet>
                                                     </cito:parameters>
                                                 </cito:InlineElement><span id="SIff799b19-6c0e-406a-ad0d-63f470eac66f" style="background-color: #C7B8CE;">drie</span>. Met de bovenbouw van de <cito:InlineElement id="I88bb2ba0-543d-44e5-977a-1754f8a1d505" layoutTemplateSourceName="InlineHottextCorrectionLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                     <cito:parameters>
                                                         <cito:parameterSet id="entireItem">
                                                             <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                             <cito:plaintextparameter name="controlId">I88bb2ba0-543d-44e5-977a-1754f8a1d505</cito:plaintextparameter>
                                                             <cito:plaintextparameter name="controlLabel">vier</cito:plaintextparameter>
                                                             <cito:booleanparameter name="addHottextCorrection">True</cito:booleanparameter>
                                                             <cito:plaintextparameter name="hottextValue"/>
                                                             <cito:hotTextCorrectionScoringParameter name="hotTextCorrectionScoring" ControllerId="hottextCorrectionController" findingOverride="hottextController" expected-length="0" correctionIsApplicable="true">
                                                                 <cito:subparameterset id="Input"/>
                                                                 <cito:definition id=""/>
                                                                 <cito:relatedControlLabel name="controlLabel">vier</cito:relatedControlLabel>
                                                             </cito:hotTextCorrectionScoringParameter>
                                                         </cito:parameterSet>
                                                     </cito:parameters>
                                                 </cito:InlineElement><span id="SI88bb2ba0-543d-44e5-977a-1754f8a1d505" style="background-color: #C7B8CE;">vier</span> gaan we daarom in de <cito:InlineElement id="I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a" layoutTemplateSourceName="InlineHottextCorrectionLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                     <cito:parameters>
                                                         <cito:parameterSet id="entireItem">
                                                             <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                             <cito:plaintextparameter name="controlId">I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a</cito:plaintextparameter>
                                                             <cito:plaintextparameter name="controlLabel">vijf</cito:plaintextparameter>
                                                             <cito:booleanparameter name="addHottextCorrection">True</cito:booleanparameter>
                                                             <cito:plaintextparameter name="hottextValue"/>
                                                             <cito:hotTextCorrectionScoringParameter name="hotTextCorrectionScoring" ControllerId="hottextCorrectionController" findingOverride="hottextController" expected-length="0" correctionIsApplicable="true">
                                                                 <cito:subparameterset id="Input"/>
                                                                 <cito:definition id=""/>
                                                                 <cito:relatedControlLabel name="controlLabel">vijf</cito:relatedControlLabel>
                                                             </cito:hotTextCorrectionScoringParameter>
                                                         </cito:parameterSet>
                                                     </cito:parameters>
                                                 </cito:InlineElement><span id="SI9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a" style="background-color: #C7B8CE;">vijf</span> een weekje naar het <cito:InlineElement id="I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b" layoutTemplateSourceName="InlineHottextCorrectionLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                     <cito:parameters>
                                                         <cito:parameterSet id="entireItem">
                                                             <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                             <cito:plaintextparameter name="controlId">I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b</cito:plaintextparameter>
                                                             <cito:plaintextparameter name="controlLabel">zes</cito:plaintextparameter>
                                                             <cito:booleanparameter name="addHottextCorrection">True</cito:booleanparameter>
                                                             <cito:plaintextparameter name="hottextValue"/>
                                                             <cito:hotTextCorrectionScoringParameter name="hotTextCorrectionScoring" ControllerId="hottextCorrectionController" findingOverride="hottextController" expected-length="0" correctionIsApplicable="true">
                                                                 <cito:subparameterset id="Input"/>
                                                                 <cito:definition id=""/>
                                                                 <cito:relatedControlLabel name="controlLabel">zes</cito:relatedControlLabel>
                                                             </cito:hotTextCorrectionScoringParameter>
                                                         </cito:parameterSet>
                                                     </cito:parameters>
                                                 </cito:InlineElement><span id="SI0f93467a-f5fd-4b54-a0dc-5afd4bf5475b" style="background-color: #C7B8CE;">zes</span>. Waarschijnlijk gaan de <cito:InlineElement id="I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea" layoutTemplateSourceName="InlineHottextCorrectionLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                     <cito:parameters>
                                                         <cito:parameterSet id="entireItem">
                                                             <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                             <cito:plaintextparameter name="controlId">I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea</cito:plaintextparameter>
                                                             <cito:plaintextparameter name="controlLabel">Heer</cito:plaintextparameter>
                                                             <cito:booleanparameter name="addHottextCorrection">True</cito:booleanparameter>
                                                             <cito:plaintextparameter name="hottextValue"/>
                                                             <cito:hotTextCorrectionScoringParameter name="hotTextCorrectionScoring" ControllerId="hottextCorrectionController" findingOverride="hottextController" expected-length="0" correctionIsApplicable="true">
                                                                 <cito:subparameterset id="Input"/>
                                                                 <cito:definition id=""/>
                                                                 <cito:relatedControlLabel name="controlLabel">Heer</cito:relatedControlLabel>
                                                             </cito:hotTextCorrectionScoringParameter>
                                                         </cito:parameterSet>
                                                     </cito:parameters>
                                                 </cito:InlineElement><span id="SI43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea" style="background-color: #C7B8CE;">Heer</span><cito:InlineElement id="I941e9a9a-fd22-43a4-acf2-aa862f943cfc" layoutTemplateSourceName="InlineHottextCorrectionLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                     <cito:parameters>
                                                         <cito:parameterSet id="entireItem">
                                                             <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                             <cito:plaintextparameter name="controlId">I941e9a9a-fd22-43a4-acf2-aa862f943cfc</cito:plaintextparameter>
                                                             <cito:plaintextparameter name="controlLabel">acht</cito:plaintextparameter>
                                                             <cito:booleanparameter name="addHottextCorrection">True</cito:booleanparameter>
                                                             <cito:plaintextparameter name="hottextValue"/>
                                                             <cito:hotTextCorrectionScoringParameter name="hotTextCorrectionScoring" ControllerId="hottextCorrectionController" findingOverride="hottextController" expected-length="0" correctionIsApplicable="true">
                                                                 <cito:subparameterset id="Input"/>
                                                                 <cito:definition id=""/>
                                                                 <cito:relatedControlLabel name="controlLabel">acht</cito:relatedControlLabel>
                                                             </cito:hotTextCorrectionScoringParameter>
                                                         </cito:parameterSet>
                                                     </cito:parameters>
                                                 </cito:InlineElement><span id="SI941e9a9a-fd22-43a4-acf2-aa862f943cfc" style="background-color: #C7B8CE;">acht</span> en <cito:InlineElement id="I6eb7f951-31d1-46e2-89fb-c7eca3849f9d" layoutTemplateSourceName="InlineHottextCorrectionLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                     <cito:parameters>
                                                         <cito:parameterSet id="entireItem">
                                                             <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                             <cito:plaintextparameter name="controlId">I6eb7f951-31d1-46e2-89fb-c7eca3849f9d</cito:plaintextparameter>
                                                             <cito:plaintextparameter name="controlLabel">negen</cito:plaintextparameter>
                                                             <cito:booleanparameter name="addHottextCorrection">True</cito:booleanparameter>
                                                             <cito:plaintextparameter name="hottextValue"/>
                                                             <cito:hotTextCorrectionScoringParameter name="hotTextCorrectionScoring" ControllerId="hottextCorrectionController" findingOverride="hottextController" expected-length="0" correctionIsApplicable="true">
                                                                 <cito:subparameterset id="Input"/>
                                                                 <cito:definition id=""/>
                                                                 <cito:relatedControlLabel name="controlLabel">negen</cito:relatedControlLabel>
                                                             </cito:hotTextCorrectionScoringParameter>
                                                         </cito:parameterSet>
                                                     </cito:parameters>
                                                 </cito:InlineElement><span id="SI6eb7f951-31d1-46e2-89fb-c7eca3849f9d" style="background-color: #C7B8CE;">negen</span><cito:InlineElement id="I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4" layoutTemplateSourceName="InlineHottextCorrectionLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                     <cito:parameters>
                                                         <cito:parameterSet id="entireItem">
                                                             <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                             <cito:plaintextparameter name="controlId">I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4</cito:plaintextparameter>
                                                             <cito:plaintextparameter name="controlLabel">tien</cito:plaintextparameter>
                                                             <cito:booleanparameter name="addHottextCorrection">True</cito:booleanparameter>
                                                             <cito:plaintextparameter name="hottextValue"/>
                                                             <cito:hotTextCorrectionScoringParameter name="hotTextCorrectionScoring" ControllerId="hottextCorrectionController" findingOverride="hottextController" expected-length="0" correctionIsApplicable="true">
                                                                 <cito:subparameterset id="Input"/>
                                                                 <cito:definition id=""/>
                                                                 <cito:relatedControlLabel name="controlLabel">tien</cito:relatedControlLabel>
                                                             </cito:hotTextCorrectionScoringParameter>
                                                         </cito:parameterSet>
                                                     </cito:parameters>
                                                 </cito:InlineElement><span id="SI7ac4361f-5aae-4a8f-9850-a93bb81ad3a4" style="background-color: #C7B8CE;">tien</span> - <cito:InlineElement id="Iad84d52a-4b1e-45b3-8067-96811e46ff7e" layoutTemplateSourceName="InlineHottextCorrectionLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                     <cito:parameters>
                                                         <cito:parameterSet id="entireItem">
                                                             <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                             <cito:plaintextparameter name="controlId">Iad84d52a-4b1e-45b3-8067-96811e46ff7e</cito:plaintextparameter>
                                                             <cito:plaintextparameter name="controlLabel">elf</cito:plaintextparameter>
                                                             <cito:booleanparameter name="addHottextCorrection">True</cito:booleanparameter>
                                                             <cito:plaintextparameter name="hottextValue"/>
                                                             <cito:hotTextCorrectionScoringParameter name="hotTextCorrectionScoring" ControllerId="hottextCorrectionController" findingOverride="hottextController" expected-length="0" correctionIsApplicable="true">
                                                                 <cito:subparameterset id="Input"/>
                                                                 <cito:definition id=""/>
                                                                 <cito:relatedControlLabel name="controlLabel">elf</cito:relatedControlLabel>
                                                             </cito:hotTextCorrectionScoringParameter>
                                                         </cito:parameterSet>
                                                     </cito:parameters>
                                                 </cito:InlineElement><span id="SIad84d52a-4b1e-45b3-8067-96811e46ff7e" style="background-color: #C7B8CE;">elf</span> mee als begeleider.</p>
                                         </xhtmlparameter>

            Dim xhtmlPrm As New XHtmlParameter() With {.Name = "hottexttext", .Value = xhtmlValue.ToString}
            scoreParam.HotTextText = xhtmlPrm
            scoreParams.Add(scoreParam)

            Dim pair As KeyValuePair(Of String, String)
            For Each pair In correctionScoreParamSubSet
                Dim correctionScoreParam As HotTextCorrectionScoringParameter = New HotTextCorrectionScoringParameter() With {.ControllerId = "hottextCorrectionController", .FindingOverride = "hottextController", .InlineId =
                        $"Input_{pair.Key}", .ExpectedLength = 0, .CorrectionIsApplicable = True}
                correctionScoreParam.AddSubParameters("Input")
                correctionScoreParam.RelatedControlLabelParameter = New PlainTextParameter() With {.Name = "controlLabel", .Value = pair.Value}
                scoreParams.Add(correctionScoreParam)   'For testpurposes... add to collection as well... during a normal publication the scoring parameters are being retrieved from the item using DeepFetchInlineScoringParameters
            Next

            Return scoreParams
        End Function

        Private Function GetHottextCorrectionScoringParams_II() As HashSet(Of ScoringParameter)
            Dim scoreParams As New HashSet(Of ScoringParameter)
            Dim scoreParam As HotTextScoringParameter = New HotTextScoringParameter() With {.ControllerId = "hottextController", .FindingOverride = "hottextController", .MinChoices = 1, .MaxChoices = 4, .IsCorrectionVariant = True}.AddSubParameters("Iddc904d4-b814-436f-ac08-7a68086f48f1", "I155c6ad9-ec1a-4023-9986-d8b43fd362ff", "I70bd825f-db6f-4c1a-b1cc-04e3cb28a50f", "Ibaa80230-ea88-4c26-80e3-b7fb8143abba")

            Dim correctionScoreParamSubSet As New Dictionary(Of String, String)
            correctionScoreParamSubSet.Add("Iddc904d4-b814-436f-ac08-7a68086f48f1", "woord1")
            correctionScoreParamSubSet.Add("I155c6ad9-ec1a-4023-9986-d8b43fd362ff", "woord2")
            correctionScoreParamSubSet.Add("I70bd825f-db6f-4c1a-b1cc-04e3cb28a50f", "woord3")
            correctionScoreParamSubSet.Add("Ibaa80230-ea88-4c26-80e3-b7fb8143abba", "woord4")

            Dim xhtmlValue As XElement = <xhtmlparameter name="hottextInput">
                                             <p id="c1-id-11" xmlns="http://www.w3.org/1999/xhtml">
                                                 <cito:InlineElement id="Iddc904d4-b814-436f-ac08-7a68086f48f1" layoutTemplateSourceName="InlineHottextCorrectionLayoutTemplate" inlineFO="" xmlns:cito="http://www.cito.nl/citotester">
                                                     <cito:parameters>
                                                         <cito:parameterSet id="entireItem">
                                                             <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                             <cito:plaintextparameter name="controlId">Iddc904d4-b814-436f-ac08-7a68086f48f1</cito:plaintextparameter>
                                                             <cito:plaintextparameter name="controlLabel">woord1</cito:plaintextparameter>
                                                             <cito:booleanparameter name="addHottextCorrection">True</cito:booleanparameter>
                                                             <cito:plaintextparameter name="hottextValue"/>
                                                             <cito:hotTextCorrectionScoringParameter name="hotTextCorrectionScoring" ControllerId="hottextCorrectionController" findingOverride="hottextController" expected-length="0" correctionIsApplicable="true">
                                                                 <cito:subparameterset id="1"/>
                                                                 <cito:definition id=""/>
                                                                 <cito:relatedControlLabel name="controlLabel">woord1</cito:relatedControlLabel>
                                                             </cito:hotTextCorrectionScoringParameter>
                                                         </cito:parameterSet>
                                                     </cito:parameters>
                                                 </cito:InlineElement>
                                                 <span id="SIddc904d4-b814-436f-ac08-7a68086f48f1" style="background-color: #C7B8CE;">woord1</span>
                                                 <cito:InlineElement id="I155c6ad9-ec1a-4023-9986-d8b43fd362ff" layoutTemplateSourceName="InlineHottextCorrectionLayoutTemplate" inlineFO="" xmlns:cito="http://www.cito.nl/citotester">
                                                     <cito:parameters>
                                                         <cito:parameterSet id="entireItem">
                                                             <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                             <cito:plaintextparameter name="controlId">I155c6ad9-ec1a-4023-9986-d8b43fd362ff</cito:plaintextparameter>
                                                             <cito:plaintextparameter name="controlLabel">woord2</cito:plaintextparameter>
                                                             <cito:booleanparameter name="addHottextCorrection">True</cito:booleanparameter>
                                                             <cito:plaintextparameter name="hottextValue"/>
                                                             <cito:hotTextCorrectionScoringParameter name="hotTextCorrectionScoring" ControllerId="hottextCorrectionController" findingOverride="hottextController" expected-length="0" correctionIsApplicable="true">
                                                                 <cito:subparameterset id="1"/>
                                                                 <cito:definition id=""/>
                                                                 <cito:relatedControlLabel name="controlLabel">woord2</cito:relatedControlLabel>
                                                             </cito:hotTextCorrectionScoringParameter>
                                                         </cito:parameterSet>
                                                     </cito:parameters>
                                                 </cito:InlineElement>
                                                 <span id="SI155c6ad9-ec1a-4023-9986-d8b43fd362ff" style="background-color: #C7B8CE;">woord2</span>
                                                 <cito:InlineElement id="I70bd825f-db6f-4c1a-b1cc-04e3cb28a50f" layoutTemplateSourceName="InlineHottextCorrectionLayoutTemplate" inlineFO="" xmlns:cito="http://www.cito.nl/citotester">
                                                     <cito:parameters>
                                                         <cito:parameterSet id="entireItem">
                                                             <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                             <cito:plaintextparameter name="controlId">I70bd825f-db6f-4c1a-b1cc-04e3cb28a50f</cito:plaintextparameter>
                                                             <cito:plaintextparameter name="controlLabel">woord3</cito:plaintextparameter>
                                                             <cito:booleanparameter name="addHottextCorrection">True</cito:booleanparameter>
                                                             <cito:plaintextparameter name="hottextValue"/>
                                                             <cito:hotTextCorrectionScoringParameter name="hotTextCorrectionScoring" ControllerId="hottextCorrectionController" findingOverride="hottextController" expected-length="0" correctionIsApplicable="true">
                                                                 <cito:subparameterset id="1"/>
                                                                 <cito:definition id=""/>
                                                                 <cito:relatedControlLabel name="controlLabel">woord3</cito:relatedControlLabel>
                                                             </cito:hotTextCorrectionScoringParameter>
                                                         </cito:parameterSet>
                                                     </cito:parameters>
                                                 </cito:InlineElement>
                                                 <span id="SI70bd825f-db6f-4c1a-b1cc-04e3cb28a50f" style="background-color: #C7B8CE;">woord3</span>
                                                 <cito:InlineElement id="Ibaa80230-ea88-4c26-80e3-b7fb8143abba" layoutTemplateSourceName="InlineHottextCorrectionLayoutTemplate" inlineFO="" xmlns:cito="http://www.cito.nl/citotester">
                                                     <cito:parameters>
                                                         <cito:parameterSet id="entireItem">
                                                             <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                             <cito:plaintextparameter name="controlId">Ibaa80230-ea88-4c26-80e3-b7fb8143abba</cito:plaintextparameter>
                                                             <cito:plaintextparameter name="controlLabel">woord4</cito:plaintextparameter>
                                                             <cito:booleanparameter name="addHottextCorrection">True</cito:booleanparameter>
                                                             <cito:plaintextparameter name="hottextValue"/>
                                                             <cito:hotTextCorrectionScoringParameter name="hotTextCorrectionScoring" ControllerId="hottextCorrectionController" findingOverride="hottextController" expected-length="0" correctionIsApplicable="true">
                                                                 <cito:subparameterset id="1"/>
                                                                 <cito:definition id=""/>
                                                                 <cito:relatedControlLabel name="controlLabel">woord4</cito:relatedControlLabel>
                                                             </cito:hotTextCorrectionScoringParameter>
                                                         </cito:parameterSet>
                                                     </cito:parameters>
                                                 </cito:InlineElement>
                                                 <span id="SIbaa80230-ea88-4c26-80e3-b7fb8143abba" style="background-color: #C7B8CE;">woord4</span>
                                             </p>
                                         </xhtmlparameter>

            Dim xhtmlPrm As New XHtmlParameter() With {.Name = "hottexttext", .Value = xhtmlValue.ToString}
            scoreParam.HotTextText = xhtmlPrm
            scoreParams.Add(scoreParam)

            Dim pair As KeyValuePair(Of String, String)
            For Each pair In correctionScoreParamSubSet
                Dim correctionScoreParam As HotTextCorrectionScoringParameter = New HotTextCorrectionScoringParameter() With {.ControllerId = "hottextCorrectionController", .FindingOverride = "hottextController", .InlineId =
                        $"Input_{pair.Key}", .ExpectedLength = 0, .CorrectionIsApplicable = True}
                correctionScoreParam.AddSubParameters("Input")
                correctionScoreParam.RelatedControlLabelParameter = New PlainTextParameter() With {.Name = "controlLabel", .Value = pair.Value}
                scoreParams.Add(correctionScoreParam)   'For testpurposes... add to collection as well... during a normal publication the scoring parameters are being retrieved from the item using DeepFetchInlineScoringParameters
            Next

            Return scoreParams
        End Function

#End Region

#Region "Itembodies"

        Private _itemBody1 As XElement =
            <wrapper>
                <itemBody class="defaultBody">
                    <div class="content">
                        <div>
                            <div class="div_left">
                                <!-- div_left_inner is needed to create the 50px margin between the left and right columns in dual column mode -->
                                <div class="div_left_inner">
                                    <div id="leftBody">
                                        <p id="c1-id-11"> </p>
                                    </div>
                                    <div id="questionwithinlinecontrol">
                                        <qti-hottext-interaction response-identifier="hottextController" max-choices="0" class="markCorrect">
                                            <p id="c1-id-11">Op vrijwel alle <span>
                                                    <qti-hottext id="Ice5853d6-5a4b-4fc6-9298-7bc4047d0e26" identifier="Ice5853d6-5a4b-4fc6-9298-7bc4047d0e26"/>
                                                </span>
                                                <span id="SIce5853d6-5a4b-4fc6-9298-7bc4047d0e26" style="background-color: #C7B8CE;">een</span> meren is in de zomer veel te doen: ze zijn <span>
                                                    <qti-hottext id="I1752ec64-4652-4723-b3b0-0404b15a0e6d" identifier="I1752ec64-4652-4723-b3b0-0404b15a0e6d"/>
                                                </span>
                                                <span id="SI1752ec64-4652-4723-b3b0-0404b15a0e6d" style="background-color: #C7B8CE;">twee</span> geschikt voor <span>
                                                    <qti-hottext id="Iff799b19-6c0e-406a-ad0d-63f470eac66f" identifier="Iff799b19-6c0e-406a-ad0d-63f470eac66f"/>
                                                </span>
                                                <span id="SIff799b19-6c0e-406a-ad0d-63f470eac66f" style="background-color: #C7B8CE;">drie</span>. Met de bovenbouw van de <span>
                                                    <qti-hottext id="I88bb2ba0-543d-44e5-977a-1754f8a1d505" identifier="I88bb2ba0-543d-44e5-977a-1754f8a1d505"/>
                                                </span>
                                                <span id="SI88bb2ba0-543d-44e5-977a-1754f8a1d505" style="background-color: #C7B8CE;">vier</span> gaan we daarom in de <span>
                                                    <qti-hottext id="I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a" identifier="I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a"/>
                                                </span>
                                                <span id="SI9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a" style="background-color: #C7B8CE;">vijf</span> een weekje naar het <span>
                                                    <qti-hottext id="I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b" identifier="I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b"/>
                                                </span>
                                                <span id="SI0f93467a-f5fd-4b54-a0dc-5afd4bf5475b" style="background-color: #C7B8CE;">zes</span>. Waarschijnlijk gaan de <span>
                                                    <qti-hottext id="I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea" identifier="I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea"/>
                                                </span>
                                                <span id="SI43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea" style="background-color: #C7B8CE;">Heer</span>
                                                <span>
                                                    <qti-hottext id="I941e9a9a-fd22-43a4-acf2-aa862f943cfc" identifier="I941e9a9a-fd22-43a4-acf2-aa862f943cfc"/>
                                                </span>
                                                <span id="SI941e9a9a-fd22-43a4-acf2-aa862f943cfc" style="background-color: #C7B8CE;">acht</span> en <span>
                                                    <qti-hottext id="I6eb7f951-31d1-46e2-89fb-c7eca3849f9d" identifier="I6eb7f951-31d1-46e2-89fb-c7eca3849f9d"/>
                                                </span>
                                                <span id="SI6eb7f951-31d1-46e2-89fb-c7eca3849f9d" style="background-color: #C7B8CE;">negen</span>
                                                <span>
                                                    <qti-hottext id="I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4" identifier="I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4"/>
                                                </span>
                                                <span id="SI7ac4361f-5aae-4a8f-9850-a93bb81ad3a4" style="background-color: #C7B8CE;">tien</span> - <span>
                                                    <qti-hottext id="Iad84d52a-4b1e-45b3-8067-96811e46ff7e" identifier="Iad84d52a-4b1e-45b3-8067-96811e46ff7e"/>
                                                </span>
                                                <span id="SIad84d52a-4b1e-45b3-8067-96811e46ff7e" style="background-color: #C7B8CE;">elf</span> mee als begeleider.</p>
                                        </qti-hottext-interaction>
                                    </div>
                                </div>
                            </div>
                            <div class="div_right">
                                <!-- div_right_inner is needed to create the 50px margin between the left and right columns in dual column mode -->
                                <div class="div_right_inner">
                                    <div id="body">
                                        <p id="c1-id-11">Selecteer de fout gespelde woorden en verbeter deze vervolgens.</p>
                                    </div>
                                    <div id="question">
                                        <p id="c1-id-11">
                                            <strong id="c1-id-12">Welke woorden zijn fout gespeld ?</strong>
                                        </p>
                                    </div>
                                    <div id="answer">
                                        <qti-extended-text-interaction id="HT_A-ti" response-identifier="Input_Ice5853d6-5a4b-4fc6-9298-7bc4047d0e26" expected-length="140" expected-lines="2" hottext-id="Ice5853d6-5a4b-4fc6-9298-7bc4047d0e26"/>
                                        <qti-extended-text-interaction id="HT_B-ti" response-identifier="Input_I1752ec64-4652-4723-b3b0-0404b15a0e6d" expected-length="140" expected-lines="2" hottext-id="I1752ec64-4652-4723-b3b0-0404b15a0e6d"/>
                                        <qti-extended-text-interaction id="HT_C-ti" response-identifier="Input_Iff799b19-6c0e-406a-ad0d-63f470eac66f" expected-length="140" expected-lines="2" hottext-id="Iff799b19-6c0e-406a-ad0d-63f470eac66f"/>
                                        <qti-extended-text-interaction id="HT_D-ti" response-identifier="Input_I88bb2ba0-543d-44e5-977a-1754f8a1d505" expected-length="140" expected-lines="2" hottext-id="I88bb2ba0-543d-44e5-977a-1754f8a1d505"/>
                                        <qti-extended-text-interaction id="HT_E-ti" response-identifier="Input_I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a" expected-length="140" expected-lines="2" hottext-id="I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a"/>
                                        <qti-extended-text-interaction id="HT_F-ti" response-identifier="Input_I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b" expected-length="140" expected-lines="2" hottext-id="I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b"/>
                                        <qti-extended-text-interaction id="HT_G-ti" response-identifier="Input_I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea" expected-length="140" expected-lines="2" hottext-id="I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea"/>
                                        <qti-extended-text-interaction id="HT_H-ti" response-identifier="Input_I941e9a9a-fd22-43a4-acf2-aa862f943cfc" expected-length="140" expected-lines="2" hottext-id="I941e9a9a-fd22-43a4-acf2-aa862f943cfc"/>
                                        <qti-extended-text-interaction id="HT_I-ti" response-identifier="Input_I6eb7f951-31d1-46e2-89fb-c7eca3849f9d" expected-length="140" expected-lines="2" hottext-id="I6eb7f951-31d1-46e2-89fb-c7eca3849f9d"/>
                                        <qti-extended-text-interaction id="HT_J-ti" response-identifier="Input_I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4" expected-length="140" expected-lines="2" hottext-id="I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4"/>
                                        <qti-extended-text-interaction id="HT_K-ti" response-identifier="Input_Iad84d52a-4b1e-45b3-8067-96811e46ff7e" expected-length="140" expected-lines="2" hottext-id="Iad84d52a-4b1e-45b3-8067-96811e46ff7e"/>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </itemBody>
            </wrapper>

        Private _itemBody2 As XElement =
            <wrapper>
                <itemBody class="defaultBody">
                    <div class="content">
                        <div class="div_left">
                            <!-- div_left_inner is needed to create the 50px margin between the left and right columns in dual column mode -->
                            <div class="div_left_inner">
                                <div id="questionwithinlinecontrol">
                                    <qti-hottext-interaction response-identifier="hottextController" max-choices="0" class="markCorrect">
                                        <p id="c1-id-11">
                                            <span>
                                                <qti-hottext id="Iddc904d4-b814-436f-ac08-7a68086f48f1" identifier="Iddc904d4-b814-436f-ac08-7a68086f48f1"/>
                                            </span>
                                            <span id="SIddc904d4-b814-436f-ac08-7a68086f48f1" style="background-color: #C7B8CE;">woord1</span>
                                            <span>
                                                <qti-hottext id="I155c6ad9-ec1a-4023-9986-d8b43fd362ff" identifier="I155c6ad9-ec1a-4023-9986-d8b43fd362ff"/>
                                            </span>
                                            <span id="SI155c6ad9-ec1a-4023-9986-d8b43fd362ff" style="background-color: #C7B8CE;">woord2</span>
                                            <span>
                                                <qti-hottext id="I70bd825f-db6f-4c1a-b1cc-04e3cb28a50f" identifier="I70bd825f-db6f-4c1a-b1cc-04e3cb28a50f"/>
                                            </span>
                                            <span id="SI70bd825f-db6f-4c1a-b1cc-04e3cb28a50f" style="background-color: #C7B8CE;">woord3</span>
                                            <span>
                                                <qti-hottext id="Ibaa80230-ea88-4c26-80e3-b7fb8143abba" identifier="Ibaa80230-ea88-4c26-80e3-b7fb8143abba"/>
                                            </span>
                                            <span id="SIbaa80230-ea88-4c26-80e3-b7fb8143abba" style="background-color: #C7B8CE;">woord4</span>
                                        </p>
                                    </qti-hottext-interaction>
                                </div>
                            </div>
                        </div>
                        <div class="div_right">
                            <!-- div_right_inner is needed to create the 50px margin between the left and right columns in dual column mode -->
                            <div class="div_right_inner">
                                <div id="question">
                                    <p id="c1-id-11">Corrrigeer maar !</p>
                                </div>
                                <div>
                                    <qti-extended-text-interaction id="HT_A-ti" response-identifier="Input_Iddc904d4-b814-436f-ac08-7a68086f48f1" expected-length="140" expected-lines="2" hottext-id="Iddc904d4-b814-436f-ac08-7a68086f48f1" patternMask="^.*$"/>
                                    <qti-extended-text-interaction id="HT_B-ti" response-identifier="Input_I155c6ad9-ec1a-4023-9986-d8b43fd362ff" expected-length="140" expected-lines="2" hottext-id="I155c6ad9-ec1a-4023-9986-d8b43fd362ff" patternMask="^.*$"/>
                                    <qti-extended-text-interaction id="HT_C-ti" response-identifier="Input_I70bd825f-db6f-4c1a-b1cc-04e3cb28a50f" expected-length="140" expected-lines="2" hottext-id="I70bd825f-db6f-4c1a-b1cc-04e3cb28a50f" patternMask="^.*$"/>
                                    <qti-extended-text-interaction id="HT_D-ti" response-identifier="Input_Ibaa80230-ea88-4c26-80e3-b7fb8143abba" expected-length="140" expected-lines="2" hottext-id="Ibaa80230-ea88-4c26-80e3-b7fb8143abba" patternMask="^.*$"/>
                                </div>
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
                    <keyFinding id="hottextController" scoringMethod="Polytomous">
                        <keyFact id="Ice5853d6-5a4b-4fc6-9298-7bc4047d0e26-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="Ice5853d6-5a4b-4fc6-9298-7bc4047d0e26-hottextController" occur="1">
                                <booleanValue>
                                    <typedValue>false</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="I1752ec64-4652-4723-b3b0-0404b15a0e6d-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I1752ec64-4652-4723-b3b0-0404b15a0e6d-hottextController" occur="1">
                                <booleanValue>
                                    <typedValue>false</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="Iff799b19-6c0e-406a-ad0d-63f470eac66f-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="Iff799b19-6c0e-406a-ad0d-63f470eac66f-hottextController" occur="1">
                                <booleanValue>
                                    <typedValue>true</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="I88bb2ba0-543d-44e5-977a-1754f8a1d505-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I88bb2ba0-543d-44e5-977a-1754f8a1d505-hottextController" occur="1">
                                <booleanValue>
                                    <typedValue>false</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a-hottextController" occur="1">
                                <booleanValue>
                                    <typedValue>false</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b-hottextController" occur="1">
                                <booleanValue>
                                    <typedValue>true</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea-hottextController" occur="1">
                                <booleanValue>
                                    <typedValue>true</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="I941e9a9a-fd22-43a4-acf2-aa862f943cfc-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I941e9a9a-fd22-43a4-acf2-aa862f943cfc-hottextController" occur="1">
                                <booleanValue>
                                    <typedValue>false</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="I6eb7f951-31d1-46e2-89fb-c7eca3849f9d-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I6eb7f951-31d1-46e2-89fb-c7eca3849f9d-hottextController" occur="1">
                                <booleanValue>
                                    <typedValue>false</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4-hottextController" occur="1">
                                <booleanValue>
                                    <typedValue>true</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="Iad84d52a-4b1e-45b3-8067-96811e46ff7e-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="Iad84d52a-4b1e-45b3-8067-96811e46ff7e-hottextController" occur="1">
                                <booleanValue>
                                    <typedValue>true</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="1-Input_Iff799b19-6c0e-406a-ad0d-63f470eac66f" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="Input_Iff799b19-6c0e-406a-ad0d-63f470eac66f" occur="1">
                                <stringValue>
                                    <typedValue>drie</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="1-Input_I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="Input_I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b" occur="1">
                                <stringValue>
                                    <typedValue>zes</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="1-Input_I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="Input_I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea" occur="1">
                                <stringValue>
                                    <typedValue>heer</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="1-Input_I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="Input_I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4" occur="1">
                                <stringValue>
                                    <typedValue>tien</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="1-Input_Iad84d52a-4b1e-45b3-8067-96811e46ff7e" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="Input_Iad84d52a-4b1e-45b3-8067-96811e46ff7e" occur="1">
                                <stringValue>
                                    <typedValue>elf</typedValue>
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

        Private _solution2 As XElement =
            <solution>
                <keyFindings>
                    <keyFinding id="hottextController" scoringMethod="Dichotomous">
                        <keyFact id="1-Input_Iff799b19-6c0e-406a-ad0d-63f470eac66f" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="Input_Iff799b19-6c0e-406a-ad0d-63f470eac66f" occur="1">
                                <stringValue>
                                    <typedValue>drie</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="1-Input_I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="Input_I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b" occur="1">
                                <stringValue>
                                    <typedValue>zes</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="1-Input_I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="Input_I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea" occur="1">
                                <stringValue>
                                    <typedValue>heer</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="1-Input_I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="Input_I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4" occur="1">
                                <stringValue>
                                    <typedValue>tien</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="1-Input_Iad84d52a-4b1e-45b3-8067-96811e46ff7e" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="Input_Iad84d52a-4b1e-45b3-8067-96811e46ff7e" occur="1">
                                <stringValue>
                                    <typedValue>elf</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFactSet>
                            <keyFact id="Ice5853d6-5a4b-4fc6-9298-7bc4047d0e26-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="Ice5853d6-5a4b-4fc6-9298-7bc4047d0e26-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I1752ec64-4652-4723-b3b0-0404b15a0e6d-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I1752ec64-4652-4723-b3b0-0404b15a0e6d-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="Iff799b19-6c0e-406a-ad0d-63f470eac66f-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="Iff799b19-6c0e-406a-ad0d-63f470eac66f-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>true</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I88bb2ba0-543d-44e5-977a-1754f8a1d505-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I88bb2ba0-543d-44e5-977a-1754f8a1d505-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>true</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>true</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I941e9a9a-fd22-43a4-acf2-aa862f943cfc-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I941e9a9a-fd22-43a4-acf2-aa862f943cfc-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I6eb7f951-31d1-46e2-89fb-c7eca3849f9d-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I6eb7f951-31d1-46e2-89fb-c7eca3849f9d-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>true</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="Iad84d52a-4b1e-45b3-8067-96811e46ff7e-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="Iad84d52a-4b1e-45b3-8067-96811e46ff7e-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>true</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                        </keyFactSet>
                        <keyFactSet>
                            <keyFact id="Ice5853d6-5a4b-4fc6-9298-7bc4047d0e26-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="Ice5853d6-5a4b-4fc6-9298-7bc4047d0e26-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I1752ec64-4652-4723-b3b0-0404b15a0e6d-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I1752ec64-4652-4723-b3b0-0404b15a0e6d-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="Iff799b19-6c0e-406a-ad0d-63f470eac66f-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="Iff799b19-6c0e-406a-ad0d-63f470eac66f-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I88bb2ba0-543d-44e5-977a-1754f8a1d505-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I88bb2ba0-543d-44e5-977a-1754f8a1d505-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>true</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I941e9a9a-fd22-43a4-acf2-aa862f943cfc-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I941e9a9a-fd22-43a4-acf2-aa862f943cfc-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I6eb7f951-31d1-46e2-89fb-c7eca3849f9d-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I6eb7f951-31d1-46e2-89fb-c7eca3849f9d-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>true</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="Iad84d52a-4b1e-45b3-8067-96811e46ff7e-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="Iad84d52a-4b1e-45b3-8067-96811e46ff7e-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>true</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                        </keyFactSet>
                        <keyFactSet>
                            <keyFact id="Ice5853d6-5a4b-4fc6-9298-7bc4047d0e26-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="Ice5853d6-5a4b-4fc6-9298-7bc4047d0e26-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I1752ec64-4652-4723-b3b0-0404b15a0e6d-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I1752ec64-4652-4723-b3b0-0404b15a0e6d-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="Iff799b19-6c0e-406a-ad0d-63f470eac66f-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="Iff799b19-6c0e-406a-ad0d-63f470eac66f-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>true</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I88bb2ba0-543d-44e5-977a-1754f8a1d505-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I88bb2ba0-543d-44e5-977a-1754f8a1d505-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>true</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I941e9a9a-fd22-43a4-acf2-aa862f943cfc-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I941e9a9a-fd22-43a4-acf2-aa862f943cfc-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I6eb7f951-31d1-46e2-89fb-c7eca3849f9d-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I6eb7f951-31d1-46e2-89fb-c7eca3849f9d-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="Iad84d52a-4b1e-45b3-8067-96811e46ff7e-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="Iad84d52a-4b1e-45b3-8067-96811e46ff7e-hottextController" occur="1">
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
                    <keyFinding id="hottextController" scoringMethod="Dichotomous">
                        <keyFactSet>
                            <keyFact id="Ice5853d6-5a4b-4fc6-9298-7bc4047d0e26-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="Ice5853d6-5a4b-4fc6-9298-7bc4047d0e26-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I1752ec64-4652-4723-b3b0-0404b15a0e6d-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I1752ec64-4652-4723-b3b0-0404b15a0e6d-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="Iff799b19-6c0e-406a-ad0d-63f470eac66f-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="Iff799b19-6c0e-406a-ad0d-63f470eac66f-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>true</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I88bb2ba0-543d-44e5-977a-1754f8a1d505-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I88bb2ba0-543d-44e5-977a-1754f8a1d505-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>true</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>true</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I941e9a9a-fd22-43a4-acf2-aa862f943cfc-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I941e9a9a-fd22-43a4-acf2-aa862f943cfc-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I6eb7f951-31d1-46e2-89fb-c7eca3849f9d-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I6eb7f951-31d1-46e2-89fb-c7eca3849f9d-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>true</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="Iad84d52a-4b1e-45b3-8067-96811e46ff7e-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="Iad84d52a-4b1e-45b3-8067-96811e46ff7e-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>true</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                        </keyFactSet>
                        <keyFactSet>
                            <keyFact id="Ice5853d6-5a4b-4fc6-9298-7bc4047d0e26-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="Ice5853d6-5a4b-4fc6-9298-7bc4047d0e26-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I1752ec64-4652-4723-b3b0-0404b15a0e6d-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I1752ec64-4652-4723-b3b0-0404b15a0e6d-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="Iff799b19-6c0e-406a-ad0d-63f470eac66f-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="Iff799b19-6c0e-406a-ad0d-63f470eac66f-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I88bb2ba0-543d-44e5-977a-1754f8a1d505-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I88bb2ba0-543d-44e5-977a-1754f8a1d505-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>true</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I941e9a9a-fd22-43a4-acf2-aa862f943cfc-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I941e9a9a-fd22-43a4-acf2-aa862f943cfc-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I6eb7f951-31d1-46e2-89fb-c7eca3849f9d-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I6eb7f951-31d1-46e2-89fb-c7eca3849f9d-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>true</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="Iad84d52a-4b1e-45b3-8067-96811e46ff7e-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="Iad84d52a-4b1e-45b3-8067-96811e46ff7e-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>true</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                        </keyFactSet>
                        <keyFactSet>
                            <keyFact id="Ice5853d6-5a4b-4fc6-9298-7bc4047d0e26-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="Ice5853d6-5a4b-4fc6-9298-7bc4047d0e26-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I1752ec64-4652-4723-b3b0-0404b15a0e6d-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I1752ec64-4652-4723-b3b0-0404b15a0e6d-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="Iff799b19-6c0e-406a-ad0d-63f470eac66f-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="Iff799b19-6c0e-406a-ad0d-63f470eac66f-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>true</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I88bb2ba0-543d-44e5-977a-1754f8a1d505-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I88bb2ba0-543d-44e5-977a-1754f8a1d505-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>true</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I941e9a9a-fd22-43a4-acf2-aa862f943cfc-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I941e9a9a-fd22-43a4-acf2-aa862f943cfc-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I6eb7f951-31d1-46e2-89fb-c7eca3849f9d-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I6eb7f951-31d1-46e2-89fb-c7eca3849f9d-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="Iad84d52a-4b1e-45b3-8067-96811e46ff7e-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="Iad84d52a-4b1e-45b3-8067-96811e46ff7e-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                        </keyFactSet>
                        <keyFactSet>
                            <keyFact id="1-Input_Iff799b19-6c0e-406a-ad0d-63f470eac66f" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="Input_Iff799b19-6c0e-406a-ad0d-63f470eac66f" occur="1">
                                    <stringValue>
                                        <typedValue>drie</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="1-Input_I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="Input_I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b" occur="1">
                                    <stringValue>
                                        <typedValue>zes</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="1-Input_I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="Input_I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea" occur="1">
                                    <stringValue>
                                        <typedValue>heer</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="1-Input_I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="Input_I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4" occur="1">
                                    <stringValue>
                                        <typedValue>tien</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="1-Input_Iad84d52a-4b1e-45b3-8067-96811e46ff7e" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="Input_Iad84d52a-4b1e-45b3-8067-96811e46ff7e" occur="1">
                                    <stringValue>
                                        <typedValue>elf</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                        </keyFactSet>
                        <keyFactSet>
                            <keyFact id="1-Input_I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="Input_I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4" occur="1">
                                    <stringValue>
                                        <typedValue>Ver kaak</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="1-Input_Iad84d52a-4b1e-45b3-8067-96811e46ff7e" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="Input_Iad84d52a-4b1e-45b3-8067-96811e46ff7e" occur="1">
                                    <stringValue>
                                        <typedValue>elf</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="1-Input_I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="Input_I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b" occur="1">
                                    <stringValue>
                                        <typedValue>Bzes</typedValue>
                                    </stringValue>
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
                    <keyFinding id="hottextController" scoringMethod="Dichotomous">
                        <keyFactSet>
                            <keyFact id="Ice5853d6-5a4b-4fc6-9298-7bc4047d0e26-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="Ice5853d6-5a4b-4fc6-9298-7bc4047d0e26-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I1752ec64-4652-4723-b3b0-0404b15a0e6d-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I1752ec64-4652-4723-b3b0-0404b15a0e6d-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="Iff799b19-6c0e-406a-ad0d-63f470eac66f-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="Iff799b19-6c0e-406a-ad0d-63f470eac66f-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>true</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I88bb2ba0-543d-44e5-977a-1754f8a1d505-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I88bb2ba0-543d-44e5-977a-1754f8a1d505-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>true</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>true</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I941e9a9a-fd22-43a4-acf2-aa862f943cfc-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I941e9a9a-fd22-43a4-acf2-aa862f943cfc-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I6eb7f951-31d1-46e2-89fb-c7eca3849f9d-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I6eb7f951-31d1-46e2-89fb-c7eca3849f9d-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>true</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="Iad84d52a-4b1e-45b3-8067-96811e46ff7e-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="Iad84d52a-4b1e-45b3-8067-96811e46ff7e-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>true</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="1-Input_Iff799b19-6c0e-406a-ad0d-63f470eac66f" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="Input_Iff799b19-6c0e-406a-ad0d-63f470eac66f" occur="1">
                                    <stringValue>
                                        <typedValue>drie</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="1-Input_I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="Input_I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b" occur="1">
                                    <stringValue>
                                        <typedValue>zes</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="1-Input_I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="Input_I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea" occur="1">
                                    <stringValue>
                                        <typedValue>heer</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="1-Input_I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="Input_I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4" occur="1">
                                    <stringValue>
                                        <typedValue>tien</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="1-Input_Iad84d52a-4b1e-45b3-8067-96811e46ff7e" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="Input_Iad84d52a-4b1e-45b3-8067-96811e46ff7e" occur="1">
                                    <stringValue>
                                        <typedValue>elf</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                        </keyFactSet>
                        <keyFactSet>
                            <keyFact id="Ice5853d6-5a4b-4fc6-9298-7bc4047d0e26-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="Ice5853d6-5a4b-4fc6-9298-7bc4047d0e26-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I1752ec64-4652-4723-b3b0-0404b15a0e6d-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I1752ec64-4652-4723-b3b0-0404b15a0e6d-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="Iff799b19-6c0e-406a-ad0d-63f470eac66f-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="Iff799b19-6c0e-406a-ad0d-63f470eac66f-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I88bb2ba0-543d-44e5-977a-1754f8a1d505-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I88bb2ba0-543d-44e5-977a-1754f8a1d505-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>true</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I941e9a9a-fd22-43a4-acf2-aa862f943cfc-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I941e9a9a-fd22-43a4-acf2-aa862f943cfc-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I6eb7f951-31d1-46e2-89fb-c7eca3849f9d-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I6eb7f951-31d1-46e2-89fb-c7eca3849f9d-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>true</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="Iad84d52a-4b1e-45b3-8067-96811e46ff7e-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="Iad84d52a-4b1e-45b3-8067-96811e46ff7e-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>true</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="1-Input_I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="Input_I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b" occur="1">
                                    <stringValue>
                                        <typedValue>zes</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="1-Input_I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="Input_I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4" occur="1">
                                    <stringValue>
                                        <typedValue>tien</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="1-Input_Iad84d52a-4b1e-45b3-8067-96811e46ff7e" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="Input_Iad84d52a-4b1e-45b3-8067-96811e46ff7e" occur="1">
                                    <stringValue>
                                        <typedValue>elf</typedValue>
                                    </stringValue>
                                    <stringValue>
                                        <typedValue>elf</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                        </keyFactSet>
                        <keyFactSet>
                            <keyFact id="Ice5853d6-5a4b-4fc6-9298-7bc4047d0e26-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="Ice5853d6-5a4b-4fc6-9298-7bc4047d0e26-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I1752ec64-4652-4723-b3b0-0404b15a0e6d-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I1752ec64-4652-4723-b3b0-0404b15a0e6d-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="Iff799b19-6c0e-406a-ad0d-63f470eac66f-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="Iff799b19-6c0e-406a-ad0d-63f470eac66f-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>true</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I88bb2ba0-543d-44e5-977a-1754f8a1d505-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I88bb2ba0-543d-44e5-977a-1754f8a1d505-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>true</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I941e9a9a-fd22-43a4-acf2-aa862f943cfc-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I941e9a9a-fd22-43a4-acf2-aa862f943cfc-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I6eb7f951-31d1-46e2-89fb-c7eca3849f9d-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I6eb7f951-31d1-46e2-89fb-c7eca3849f9d-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="Iad84d52a-4b1e-45b3-8067-96811e46ff7e-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="Iad84d52a-4b1e-45b3-8067-96811e46ff7e-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="1-Input_Iff799b19-6c0e-406a-ad0d-63f470eac66f" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="Input_Iff799b19-6c0e-406a-ad0d-63f470eac66f" occur="1">
                                    <stringValue>
                                        <typedValue>water sport</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="1-Input_I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="Input_I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea" occur="1">
                                    <stringValue>
                                        <typedValue>heer</typedValue>
                                    </stringValue>
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

        Private _solution5 As XElement =
            <solution>
                <keyFindings/>
                <aspectReferences/>
                <ItemScoreTranslationTable>
                    <ItemScoreTranslationTableEntry rawScore="0" translatedScore="0"/>
                    <ItemScoreTranslationTableEntry rawScore="1" translatedScore="1"/>
                </ItemScoreTranslationTable>
            </solution>

        Private _solution6 As XElement =
            <solution>
                <keyFindings>
                    <keyFinding id="hottextController" scoringMethod="Dichotomous">
                        <keyFact id="I2194c3c9-d10d-4127-9b81-a0aed1f35846-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I2194c3c9-d10d-4127-9b81-a0aed1f35846-hottextController" occur="1">
                                <booleanValue>
                                    <typedValue>false</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="Ibe9aa083-5565-4f52-9be0-6cc5c15d65d9-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="Ibe9aa083-5565-4f52-9be0-6cc5c15d65d9-hottextController" occur="1">
                                <booleanValue>
                                    <typedValue>false</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="I8fed8938-beec-4a82-9ec1-40a593fc2d45-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I8fed8938-beec-4a82-9ec1-40a593fc2d45-hottextController" occur="1">
                                <booleanValue>
                                    <typedValue>false</typedValue>
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

        Private _solution7 As XElement =
            <solution>
                <keyFindings>
                    <keyFinding id="hottextController" scoringMethod="Dichotomous">
                        <keyFact id="I155c6ad9-ec1a-4023-9986-d8b43fd362ff-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I155c6ad9-ec1a-4023-9986-d8b43fd362ff-hottextController" occur="1">
                                <booleanValue>
                                    <typedValue>false</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="Ibaa80230-ea88-4c26-80e3-b7fb8143abba-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="Ibaa80230-ea88-4c26-80e3-b7fb8143abba-hottextController" occur="1">
                                <booleanValue>
                                    <typedValue>false</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                        <keyFactSet>
                            <keyFact id="Iddc904d4-b814-436f-ac08-7a68086f48f1-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="Iddc904d4-b814-436f-ac08-7a68086f48f1-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>true</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I70bd825f-db6f-4c1a-b1cc-04e3cb28a50f-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I70bd825f-db6f-4c1a-b1cc-04e3cb28a50f-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="1-Input_Iddc904d4-b814-436f-ac08-7a68086f48f1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="Input_Iddc904d4-b814-436f-ac08-7a68086f48f1" occur="1">
                                    <stringValue>
                                        <typedValue>woordje1</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                        </keyFactSet>
                        <keyFactSet>
                            <keyFact id="Iddc904d4-b814-436f-ac08-7a68086f48f1-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="Iddc904d4-b814-436f-ac08-7a68086f48f1-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I70bd825f-db6f-4c1a-b1cc-04e3cb28a50f-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I70bd825f-db6f-4c1a-b1cc-04e3cb28a50f-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>true</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="1-Input_I70bd825f-db6f-4c1a-b1cc-04e3cb28a50f" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="Input_I70bd825f-db6f-4c1a-b1cc-04e3cb28a50f" occur="1">
                                    <stringValue>
                                        <typedValue>woordje3</typedValue>
                                    </stringValue>
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

#End Region

#Region "Expected results"

        Private _result1 As XElement =
    <root>
        <qti-response-declaration identifier="RESPONSE" cardinality="multiple" base-type="identifier">
            <qti-correct-response interpretation="Iff799b19-6c0e-406a-ad0d-63f470eac66f&amp;I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b&amp;I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea&amp;I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4&amp;Iad84d52a-4b1e-45b3-8067-96811e46ff7e">
                <qti-value>Iff799b19-6c0e-406a-ad0d-63f470eac66f</qti-value>
                <qti-value>I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b</qti-value>
                <qti-value>I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea</qti-value>
                <qti-value>I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4</qti-value>
                <qti-value>Iad84d52a-4b1e-45b3-8067-96811e46ff7e</qti-value>
            </qti-correct-response>
        </qti-response-declaration>
        <qti-response-declaration identifier="RESPONSE2" cardinality="single" base-type="string"/>
        <qti-response-declaration identifier="RESPONSE3" cardinality="single" base-type="string"/>
        <qti-response-declaration identifier="RESPONSE4" cardinality="single" base-type="string">
            <qti-correct-response interpretation="drie">
                <qti-value>drie</qti-value>
            </qti-correct-response>
        </qti-response-declaration>
        <qti-response-declaration identifier="RESPONSE5" cardinality="single" base-type="string"/>
        <qti-response-declaration identifier="RESPONSE6" cardinality="single" base-type="string"/>
        <qti-response-declaration identifier="RESPONSE7" cardinality="single" base-type="string">
            <qti-correct-response interpretation="zes">
                <qti-value>zes</qti-value>
            </qti-correct-response>
        </qti-response-declaration>
        <qti-response-declaration identifier="RESPONSE8" cardinality="single" base-type="string">
            <qti-correct-response interpretation="heer">
                <qti-value>heer</qti-value>
            </qti-correct-response>
        </qti-response-declaration>
        <qti-response-declaration identifier="RESPONSE9" cardinality="single" base-type="string"/>
        <qti-response-declaration identifier="RESPONSE10" cardinality="single" base-type="string"/>
        <qti-response-declaration identifier="RESPONSE11" cardinality="single" base-type="string">
            <qti-correct-response interpretation="tien">
                <qti-value>tien</qti-value>
            </qti-correct-response>
        </qti-response-declaration>
        <qti-response-declaration identifier="RESPONSE12" cardinality="single" base-type="string">
            <qti-correct-response interpretation="elf">
                <qti-value>elf</qti-value>
            </qti-correct-response>
        </qti-response-declaration>
    </root>

        Private _result2 As XElement =
    <root>
        <qti-response-declaration identifier="RESPONSE" cardinality="multiple" base-type="identifier">
            <qti-correct-response interpretation="(Iff799b19-6c0e-406a-ad0d-63f470eac66f&amp;I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b&amp;I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea&amp;I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4&amp;Iad84d52a-4b1e-45b3-8067-96811e46ff7e)|(I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b&amp;I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4&amp;Iad84d52a-4b1e-45b3-8067-96811e46ff7e)|(Iff799b19-6c0e-406a-ad0d-63f470eac66f&amp;I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea)">
                <qti-value>Iff799b19-6c0e-406a-ad0d-63f470eac66f</qti-value>
                <qti-value>I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b</qti-value>
                <qti-value>I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea</qti-value>
                <qti-value>I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4</qti-value>
                <qti-value>Iad84d52a-4b1e-45b3-8067-96811e46ff7e</qti-value>
            </qti-correct-response>
        </qti-response-declaration>
        <qti-response-declaration identifier="RESPONSE2" cardinality="single" base-type="string"/>
        <qti-response-declaration identifier="RESPONSE3" cardinality="single" base-type="string"/>
        <qti-response-declaration identifier="RESPONSE4" cardinality="single" base-type="string">
            <qti-correct-response interpretation="drie">
                <qti-value>drie</qti-value>
            </qti-correct-response>
        </qti-response-declaration>
        <qti-response-declaration identifier="RESPONSE5" cardinality="single" base-type="string"/>
        <qti-response-declaration identifier="RESPONSE6" cardinality="single" base-type="string"/>
        <qti-response-declaration identifier="RESPONSE7" cardinality="single" base-type="string">
            <qti-correct-response interpretation="zes">
                <qti-value>zes</qti-value>
            </qti-correct-response>
        </qti-response-declaration>
        <qti-response-declaration identifier="RESPONSE8" cardinality="single" base-type="string">
            <qti-correct-response interpretation="heer">
                <qti-value>heer</qti-value>
            </qti-correct-response>
        </qti-response-declaration>
        <qti-response-declaration identifier="RESPONSE9" cardinality="single" base-type="string"/>
        <qti-response-declaration identifier="RESPONSE10" cardinality="single" base-type="string"/>
        <qti-response-declaration identifier="RESPONSE11" cardinality="single" base-type="string">
            <qti-correct-response interpretation="tien">
                <qti-value>tien</qti-value>
            </qti-correct-response>
        </qti-response-declaration>
        <qti-response-declaration identifier="RESPONSE12" cardinality="single" base-type="string">
            <qti-correct-response interpretation="elf">
                <qti-value>elf</qti-value>
            </qti-correct-response>
        </qti-response-declaration>
    </root>

        Private _result3 As XElement =
    <root>
        <qti-response-declaration identifier="RESPONSE" cardinality="multiple" base-type="identifier">
            <qti-correct-response interpretation="(Iff799b19-6c0e-406a-ad0d-63f470eac66f&amp;I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b&amp;I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea&amp;I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4&amp;Iad84d52a-4b1e-45b3-8067-96811e46ff7e)|(I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b&amp;I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4&amp;Iad84d52a-4b1e-45b3-8067-96811e46ff7e)|(Iff799b19-6c0e-406a-ad0d-63f470eac66f&amp;I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea)">
                <qti-value>Iff799b19-6c0e-406a-ad0d-63f470eac66f</qti-value>
                <qti-value>I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b</qti-value>
                <qti-value>I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea</qti-value>
                <qti-value>I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4</qti-value>
                <qti-value>Iad84d52a-4b1e-45b3-8067-96811e46ff7e</qti-value>
            </qti-correct-response>
        </qti-response-declaration>
        <qti-response-declaration identifier="RESPONSE2" cardinality="single" base-type="string"/>
        <qti-response-declaration identifier="RESPONSE3" cardinality="single" base-type="string"/>
        <qti-response-declaration identifier="RESPONSE4" cardinality="single" base-type="string">
            <qti-correct-response interpretation="(drie&amp;zes&amp;heer&amp;tien&amp;elf)|(Bzes&amp;Ver kaak&amp;elf)">
                <qti-value>drie</qti-value>
            </qti-correct-response>
        </qti-response-declaration>
        <qti-response-declaration identifier="RESPONSE5" cardinality="single" base-type="string"/>
        <qti-response-declaration identifier="RESPONSE6" cardinality="single" base-type="string"/>
        <qti-response-declaration identifier="RESPONSE7" cardinality="single" base-type="string">
            <qti-correct-response>
                <qti-value>zes</qti-value>
            </qti-correct-response>
        </qti-response-declaration>
        <qti-response-declaration identifier="RESPONSE8" cardinality="single" base-type="string">
            <qti-correct-response>
                <qti-value>heer</qti-value>
            </qti-correct-response>
        </qti-response-declaration>
        <qti-response-declaration identifier="RESPONSE9" cardinality="single" base-type="string"/>
        <qti-response-declaration identifier="RESPONSE10" cardinality="single" base-type="string"/>
        <qti-response-declaration identifier="RESPONSE11" cardinality="single" base-type="string">
            <qti-correct-response>
                <qti-value>tien</qti-value>
            </qti-correct-response>
        </qti-response-declaration>
        <qti-response-declaration identifier="RESPONSE12" cardinality="single" base-type="string">
            <qti-correct-response>
                <qti-value>elf</qti-value>
            </qti-correct-response>
        </qti-response-declaration>
    </root>

        Private _result4 As XElement =
    <root>
        <qti-response-declaration identifier="RESPONSE" cardinality="multiple" base-type="identifier">
            <qti-correct-response interpretation="(Iff799b19-6c0e-406a-ad0d-63f470eac66f&amp;I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b&amp;I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea&amp;I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4&amp;Iad84d52a-4b1e-45b3-8067-96811e46ff7e&amp;drie&amp;zes&amp;heer&amp;tien&amp;elf)|(I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b&amp;I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4&amp;Iad84d52a-4b1e-45b3-8067-96811e46ff7e&amp;zes&amp;tien&amp;elf#elf)|(Iff799b19-6c0e-406a-ad0d-63f470eac66f&amp;I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea&amp;water sport&amp;heer)">
                <qti-value>Iff799b19-6c0e-406a-ad0d-63f470eac66f</qti-value>
                <qti-value>I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b</qti-value>
                <qti-value>I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea</qti-value>
                <qti-value>I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4</qti-value>
                <qti-value>Iad84d52a-4b1e-45b3-8067-96811e46ff7e</qti-value>
            </qti-correct-response>
        </qti-response-declaration>
        <qti-response-declaration identifier="RESPONSE2" cardinality="single" base-type="string"/>
        <qti-response-declaration identifier="RESPONSE3" cardinality="single" base-type="string"/>
        <qti-response-declaration identifier="RESPONSE4" cardinality="single" base-type="string">
            <qti-correct-response>
                <qti-value>drie</qti-value>
            </qti-correct-response>
        </qti-response-declaration>
        <qti-response-declaration identifier="RESPONSE5" cardinality="single" base-type="string"/>
        <qti-response-declaration identifier="RESPONSE6" cardinality="single" base-type="string"/>
        <qti-response-declaration identifier="RESPONSE7" cardinality="single" base-type="string">
            <qti-correct-response>
                <qti-value>zes</qti-value>
            </qti-correct-response>
        </qti-response-declaration>
        <qti-response-declaration identifier="RESPONSE8" cardinality="single" base-type="string">
            <qti-correct-response>
                <qti-value>heer</qti-value>
            </qti-correct-response>
        </qti-response-declaration>
        <qti-response-declaration identifier="RESPONSE9" cardinality="single" base-type="string"/>
        <qti-response-declaration identifier="RESPONSE10" cardinality="single" base-type="string"/>
        <qti-response-declaration identifier="RESPONSE11" cardinality="single" base-type="string">
            <qti-correct-response>
                <qti-value>tien</qti-value>
            </qti-correct-response>
        </qti-response-declaration>
        <qti-response-declaration identifier="RESPONSE12" cardinality="single" base-type="string">
            <qti-correct-response>
                <qti-value>elf</qti-value>
            </qti-correct-response>
        </qti-response-declaration>
    </root>

        Private _result5 As XElement =
    <root>
        <qti-response-declaration identifier="RESPONSE" cardinality="multiple" base-type="string"/>
        <qti-response-declaration identifier="RESPONSE2" cardinality="single" base-type="string"/>
        <qti-response-declaration identifier="RESPONSE3" cardinality="single" base-type="string"/>
        <qti-response-declaration identifier="RESPONSE4" cardinality="single" base-type="string"/>
        <qti-response-declaration identifier="RESPONSE5" cardinality="single" base-type="string"/>
        <qti-response-declaration identifier="RESPONSE6" cardinality="single" base-type="string"/>
        <qti-response-declaration identifier="RESPONSE7" cardinality="single" base-type="string"/>
        <qti-response-declaration identifier="RESPONSE8" cardinality="single" base-type="string"/>
        <qti-response-declaration identifier="RESPONSE9" cardinality="single" base-type="string"/>
        <qti-response-declaration identifier="RESPONSE10" cardinality="single" base-type="string"/>
        <qti-response-declaration identifier="RESPONSE11" cardinality="single" base-type="string"/>
        <qti-response-declaration identifier="RESPONSE12" cardinality="single" base-type="string"/>
    </root>

        Private _result6 As XElement =
    <root>
        <qti-response-declaration identifier="RESPONSE" cardinality="multiple" base-type="string"/>
        <qti-response-declaration identifier="RESPONSE2" cardinality="single" base-type="string"/>
        <qti-response-declaration identifier="RESPONSE3" cardinality="single" base-type="string"/>
        <qti-response-declaration identifier="RESPONSE4" cardinality="single" base-type="string"/>
        <qti-response-declaration identifier="RESPONSE5" cardinality="single" base-type="string"/>
        <qti-response-declaration identifier="RESPONSE6" cardinality="single" base-type="string"/>
        <qti-response-declaration identifier="RESPONSE7" cardinality="single" base-type="string"/>
        <qti-response-declaration identifier="RESPONSE8" cardinality="single" base-type="string"/>
        <qti-response-declaration identifier="RESPONSE9" cardinality="single" base-type="string"/>
        <qti-response-declaration identifier="RESPONSE10" cardinality="single" base-type="string"/>
        <qti-response-declaration identifier="RESPONSE11" cardinality="single" base-type="string"/>
        <qti-response-declaration identifier="RESPONSE12" cardinality="single" base-type="string"/>
    </root>

        Private _result7 As XElement =
    <root>
        <qti-response-declaration identifier="RESPONSE" cardinality="multiple" base-type="identifier">
            <qti-correct-response interpretation="(Iddc904d4-b814-436f-ac08-7a68086f48f1&amp;woordje1)|(I70bd825f-db6f-4c1a-b1cc-04e3cb28a50f&amp;woordje3)">
                <qti-value>Iddc904d4-b814-436f-ac08-7a68086f48f1</qti-value>
            </qti-correct-response>
        </qti-response-declaration>
        <qti-response-declaration identifier="RESPONSE2" cardinality="single" base-type="string">
            <qti-correct-response>
                <qti-value>woordje1</qti-value>
            </qti-correct-response>
        </qti-response-declaration>
        <qti-response-declaration identifier="RESPONSE3" cardinality="single" base-type="string"/>
        <qti-response-declaration identifier="RESPONSE4" cardinality="single" base-type="string"/>
        <qti-response-declaration identifier="RESPONSE5" cardinality="single" base-type="string"/>
    </root>

#End Region

    End Class

End Namespace
