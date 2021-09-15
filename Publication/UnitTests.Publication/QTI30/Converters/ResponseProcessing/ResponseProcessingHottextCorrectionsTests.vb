
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.QTI.Converters.ScoringConverter.QTI30
Imports Questify.Builder.UnitTests.Publication.QTI30

Namespace QTI30

    <TestClass()>
    Public Class ResponseProcessingHottextCorrectionsTests

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
        Public Sub GetResponseProcessingForCombinationOfFactSetsAndFactsOnFinding_Polytomous_Test()
            GetResponseProcessingTest(_finding1, _responseProcessing1)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
        Public Sub GetResponseProcessingForCombinationOfFactSetsAndFactsOnFinding_Dichotomous_Test()
            GetResponseProcessingTest(_finding5, _responseProcessing5)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
        Public Sub GetResponseProcessingForMultipleFactSets_Polytomous_Test()
            GetResponseProcessingTest(_finding2, _responseProcessing2)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
        Public Sub GetResponseProcessingForMultipleFactSets_Dichotomous_Test()
            GetResponseProcessingTest(_finding6, _responseProcessing6)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
        Public Sub GetResponseProcessingForFactSetTest()
            GetResponseProcessingTest(_finding3, _responseProcessing3)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
        Public Sub GetResponseProcessingForFactsOnFinding_Polytomous_Test()
            GetResponseProcessingTest(_finding4, _responseProcessing4)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessing")>
        Public Sub GetResponseProcessingForFactsOnFinding_Dichotomous_Test()
            GetResponseProcessingTest(_finding7, _responseProcessing7)
        End Sub

        Public Sub GetResponseProcessingTest(findingElement As XElement, responseProcessingElement As XElement)
            Dim scoringPrms As HashSet(Of ScoringParameter) = GetHottextCorrectionScoringParams()
            PublicationTestHelper.RunResponseProcessingTest(_itemBody1, findingElement, responseProcessingElement, scoringPrms, New CombinedScoringConverter(scoringPrms))
        End Sub


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
                                             <p id="c1-id-11" xmlns="http://www.w3.org/1999/xhtml">Op ..<cito:InlineElement id="Ice5853d6-5a4b-4fc6-9298-7bc4047d0e26" layoutTemplateSourceName="InlineHottextCorrectionLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                     <cito:parameters>
                                                         <cito:parameterSet id="entireItem">
                                                             <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                             <cito:plaintextparameter name="controlId">Ice5853d6-5a4b-4fc6-9298-7bc4047d0e26</cito:plaintextparameter>
                                                             <cito:plaintextparameter name="controlLabel">een</cito:plaintextparameter>
                                                             <cito:booleanparameter name="addHottextCorrection">True</cito:booleanparameter>
                                                             <cito:plaintextparameter name="hottextValue"/>
                                                             <cito:hotTextCorrectionScoringParameter name="hotTextCorrectionScoring" ControllerId="hottextCorrectionController" findingOverride="hottextController" expectedLength="0" correctionIsApplicable="true">
                                                                 <cito:subparameterset id="Input"/>
                                                                 <cito:definition id=""/>
                                                                 <cito:relatedControlLabel name="controlLabel">een</cito:relatedControlLabel>
                                                             </cito:hotTextCorrectionScoringParameter>
                                                         </cito:parameterSet>
                                                     </cito:parameters>
                                                 </cito:InlineElement><span id="SIce5853d6-5a4b-4fc6-9298-7bc4047d0e26" style="background-color: #C7B8CE;">een</span> bing <cito:InlineElement id="I1752ec64-4652-4723-b3b0-0404b15a0e6d" layoutTemplateSourceName="InlineHottextCorrectionLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                     <cito:parameters>
                                                         <cito:parameterSet id="entireItem">
                                                             <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                             <cito:plaintextparameter name="controlId">I1752ec64-4652-4723-b3b0-0404b15a0e6d</cito:plaintextparameter>
                                                             <cito:plaintextparameter name="controlLabel">twee</cito:plaintextparameter>
                                                             <cito:booleanparameter name="addHottextCorrection">True</cito:booleanparameter>
                                                             <cito:plaintextparameter name="hottextValue"/>
                                                             <cito:hotTextCorrectionScoringParameter name="hotTextCorrectionScoring" ControllerId="hottextCorrectionController" findingOverride="hottextController" expectedLength="0" correctionIsApplicable="true">
                                                                 <cito:subparameterset id="Input"/>
                                                                 <cito:definition id=""/>
                                                                 <cito:relatedControlLabel name="controlLabel">twee</cito:relatedControlLabel>
                                                             </cito:hotTextCorrectionScoringParameter>
                                                         </cito:parameterSet>
                                                     </cito:parameters>
                                                 </cito:InlineElement><span id="SI1752ec64-4652-4723-b3b0-0404b15a0e6d" style="background-color: #C7B8CE;">twee</span> bada.. <cito:InlineElement id="Iff799b19-6c0e-406a-ad0d-63f470eac66f" layoutTemplateSourceName="InlineHottextCorrectionLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                     <cito:parameters>
                                                         <cito:parameterSet id="entireItem">
                                                             <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                             <cito:plaintextparameter name="controlId">Iff799b19-6c0e-406a-ad0d-63f470eac66f</cito:plaintextparameter>
                                                             <cito:plaintextparameter name="controlLabel">drie</cito:plaintextparameter>
                                                             <cito:booleanparameter name="addHottextCorrection">True</cito:booleanparameter>
                                                             <cito:plaintextparameter name="hottextValue"/>
                                                             <cito:hotTextCorrectionScoringParameter name="hotTextCorrectionScoring" ControllerId="hottextCorrectionController" findingOverride="hottextController" expectedLength="0" correctionIsApplicable="true">
                                                                 <cito:subparameterset id="Input"/>
                                                                 <cito:definition id=""/>
                                                                 <cito:relatedControlLabel name="controlLabel">drie</cito:relatedControlLabel>
                                                             </cito:hotTextCorrectionScoringParameter>
                                                         </cito:parameterSet>
                                                     </cito:parameters>
                                                 </cito:InlineElement><span id="SIff799b19-6c0e-406a-ad0d-63f470eac66f" style="background-color: #C7B8CE;">drie</span>. Met ..<cito:InlineElement id="I88bb2ba0-543d-44e5-977a-1754f8a1d505" layoutTemplateSourceName="InlineHottextCorrectionLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                     <cito:parameters>
                                                         <cito:parameterSet id="entireItem">
                                                             <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                             <cito:plaintextparameter name="controlId">I88bb2ba0-543d-44e5-977a-1754f8a1d505</cito:plaintextparameter>
                                                             <cito:plaintextparameter name="controlLabel">vier</cito:plaintextparameter>
                                                             <cito:booleanparameter name="addHottextCorrection">True</cito:booleanparameter>
                                                             <cito:plaintextparameter name="hottextValue"/>
                                                             <cito:hotTextCorrectionScoringParameter name="hotTextCorrectionScoring" ControllerId="hottextCorrectionController" findingOverride="hottextController" expectedLength="0" correctionIsApplicable="true">
                                                                 <cito:subparameterset id="Input"/>
                                                                 <cito:definition id=""/>
                                                                 <cito:relatedControlLabel name="controlLabel">vier</cito:relatedControlLabel>
                                                             </cito:hotTextCorrectionScoringParameter>
                                                         </cito:parameterSet>
                                                     </cito:parameters>
                                                 </cito:InlineElement><span id="SI88bb2ba0-543d-44e5-977a-1754f8a1d505" style="background-color: #C7B8CE;">vier</span> Foo.. <cito:InlineElement id="I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a" layoutTemplateSourceName="InlineHottextCorrectionLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                     <cito:parameters>
                                                         <cito:parameterSet id="entireItem">
                                                             <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                             <cito:plaintextparameter name="controlId">I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a</cito:plaintextparameter>
                                                             <cito:plaintextparameter name="controlLabel">vijf</cito:plaintextparameter>
                                                             <cito:booleanparameter name="addHottextCorrection">True</cito:booleanparameter>
                                                             <cito:plaintextparameter name="hottextValue"/>
                                                             <cito:hotTextCorrectionScoringParameter name="hotTextCorrectionScoring" ControllerId="hottextCorrectionController" findingOverride="hottextController" expectedLength="0" correctionIsApplicable="true">
                                                                 <cito:subparameterset id="Input"/>
                                                                 <cito:definition id=""/>
                                                                 <cito:relatedControlLabel name="controlLabel">vijf</cito:relatedControlLabel>
                                                             </cito:hotTextCorrectionScoringParameter>
                                                         </cito:parameterSet>
                                                     </cito:parameters>
                                                 </cito:InlineElement><span id="SI9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a" style="background-color: #C7B8CE;">vijf</span> bar.. <cito:InlineElement id="I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b" layoutTemplateSourceName="InlineHottextCorrectionLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                     <cito:parameters>
                                                         <cito:parameterSet id="entireItem">
                                                             <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                             <cito:plaintextparameter name="controlId">I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b</cito:plaintextparameter>
                                                             <cito:plaintextparameter name="controlLabel">zes</cito:plaintextparameter>
                                                             <cito:booleanparameter name="addHottextCorrection">True</cito:booleanparameter>
                                                             <cito:plaintextparameter name="hottextValue"/>
                                                             <cito:hotTextCorrectionScoringParameter name="hotTextCorrectionScoring" ControllerId="hottextCorrectionController" findingOverride="hottextController" expectedLength="0" correctionIsApplicable="true">
                                                                 <cito:subparameterset id="Input"/>
                                                                 <cito:definition id=""/>
                                                                 <cito:relatedControlLabel name="controlLabel">zes</cito:relatedControlLabel>
                                                             </cito:hotTextCorrectionScoringParameter>
                                                         </cito:parameterSet>
                                                     </cito:parameters>
                                                 </cito:InlineElement><span id="SI0f93467a-f5fd-4b54-a0dc-5afd4bf5475b" style="background-color: #C7B8CE;">zes</span>. Waarschijnlijk ..<cito:InlineElement id="I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea" layoutTemplateSourceName="InlineHottextCorrectionLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                     <cito:parameters>
                                                         <cito:parameterSet id="entireItem">
                                                             <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                             <cito:plaintextparameter name="controlId">I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea</cito:plaintextparameter>
                                                             <cito:plaintextparameter name="controlLabel">Heer</cito:plaintextparameter>
                                                             <cito:booleanparameter name="addHottextCorrection">True</cito:booleanparameter>
                                                             <cito:plaintextparameter name="hottextValue"/>
                                                             <cito:hotTextCorrectionScoringParameter name="hotTextCorrectionScoring" ControllerId="hottextCorrectionController" findingOverride="hottextController" expectedLength="0" correctionIsApplicable="true">
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
                                                             <cito:hotTextCorrectionScoringParameter name="hotTextCorrectionScoring" ControllerId="hottextCorrectionController" findingOverride="hottextController" expectedLength="0" correctionIsApplicable="true">
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
                                                             <cito:hotTextCorrectionScoringParameter name="hotTextCorrectionScoring" ControllerId="hottextCorrectionController" findingOverride="hottextController" expectedLength="0" correctionIsApplicable="true">
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
                                                             <cito:hotTextCorrectionScoringParameter name="hotTextCorrectionScoring" ControllerId="hottextCorrectionController" findingOverride="hottextController" expectedLength="0" correctionIsApplicable="true">
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
                                                             <cito:hotTextCorrectionScoringParameter name="hotTextCorrectionScoring" ControllerId="hottextCorrectionController" findingOverride="hottextController" expectedLength="0" correctionIsApplicable="true">
                                                                 <cito:subparameterset id="Input"/>
                                                                 <cito:definition id=""/>
                                                                 <cito:relatedControlLabel name="controlLabel">elf</cito:relatedControlLabel>
                                                             </cito:hotTextCorrectionScoringParameter>
                                                         </cito:parameterSet>
                                                     </cito:parameters>
                                                 </cito:InlineElement><span id="SIad84d52a-4b1e-45b3-8067-96811e46ff7e" style="background-color: #C7B8CE;">elf</span> mee ...</p>
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
                scoreParams.Add(correctionScoreParam)
            Next

            Return scoreParams
        End Function



        Private _itemBody1 As XElement =
            <wrapper>
                <qti-item-body class="defaultBody">
                    <div class="content">
                        <div>
                            <div class="div_left" xmlns="http://www.w3.org/1999/xhtml">
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
                                                <span id="SIce5853d6-5a4b-4fc6-9298-7bc4047d0e26" style="background-color: #C7B8CE;">een</span> twee <span>
                                                    <qti-hottext id="I1752ec64-4652-4723-b3b0-0404b15a0e6d" identifier="I1752ec64-4652-4723-b3b0-0404b15a0e6d"/>
                                                </span>
                                                <span id="SI1752ec64-4652-4723-b3b0-0404b15a0e6d" style="background-color: #C7B8CE;">twee</span> hoppa <span>
                                                    <qti-hottext id="Iff799b19-6c0e-406a-ad0d-63f470eac66f" identifier="Iff799b19-6c0e-406a-ad0d-63f470eac66f"/>
                                                </span>
                                                <span id="SIff799b19-6c0e-406a-ad0d-63f470eac66f" style="background-color: #C7B8CE;">drie</span>. Met of zonder<span>
                                                    <qti-hottext id="I88bb2ba0-543d-44e5-977a-1754f8a1d505" identifier="I88bb2ba0-543d-44e5-977a-1754f8a1d505"/>
                                                </span>
                                                <span id="SI88bb2ba0-543d-44e5-977a-1754f8a1d505" style="background-color: #C7B8CE;">vier</span> gaan <span>
                                                    <qti-hottext id="I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a" identifier="I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a"/>
                                                </span>
                                                <span id="SI9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a" style="background-color: #C7B8CE;">vijf</span> banaan <span>
                                                    <qti-hottext id="I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b" identifier="I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b"/>
                                                </span>
                                                <span id="SI0f93467a-f5fd-4b54-a0dc-5afd4bf5475b" style="background-color: #C7B8CE;">zes</span>. foo <span>
                                                    <qti-hottext id="I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea" identifier="I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea"/>
                                                </span>
                                                <span id="SI43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea" style="background-color: #C7B8CE;">Lord</span>
                                                <span>
                                                    <qti-hottext id="I941e9a9a-fd22-43a4-acf2-aa862f943cfc" identifier="I941e9a9a-fd22-43a4-acf2-aa862f943cfc"/>
                                                </span>
                                                <span id="SI941e9a9a-fd22-43a4-acf2-aa862f943cfc" style="background-color: #C7B8CE;">spik</span> en <span>
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
                            <div class="div_right" xmlns="http://www.w3.org/1999/xhtml">
                                <!-- div_right_inner is needed to create the 50px margin between the left and right columns in dual column mode -->
                                <div class="div_right_inner">
                                    <div id="body">
                                        <p id="c1-id-11">Doe iets.</p>
                                    </div>
                                    <div id="question">
                                        <p id="c1-id-11">
                                            <strong id="c1-id-12">Of niet?</strong>
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
                </qti-item-body>
            </wrapper>



        Private _finding1 As XElement =
            <keyFinding id="hottextController" scoringMethod="Polytomous">
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

        Private _finding2 As XElement =
            <keyFinding id="hottextController" scoringMethod="Polytomous">
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

        Private _finding3 As XElement =
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
                                <typedValue>drie</typedValue>
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

        Private _finding4 As XElement =
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

        Private _finding5 As XElement =
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

        Private _finding6 As XElement =
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

        Private _finding7 As XElement =
            <keyFinding id="hottextController" scoringMethod="Dichotomous">
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



        Private _responseProcessing1 As XElement =
    <qti-response-processing>
        <qti-response-condition>
            <qti-response-if>
                <qti-or>
                    <qti-and>
                        <qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">Ice5853d6-5a4b-4fc6-9298-7bc4047d0e26</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                        </qti-not>
                        <qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">I1752ec64-4652-4723-b3b0-0404b15a0e6d</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                        </qti-not>
                        <qti-member>
                            <qti-base-value base-type="identifier">Iff799b19-6c0e-406a-ad0d-63f470eac66f</qti-base-value>
                            <qti-variable identifier="RESPONSE"/>
                        </qti-member>
                        <qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">I88bb2ba0-543d-44e5-977a-1754f8a1d505</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                        </qti-not>
                        <qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                        </qti-not>
                        <qti-member>
                            <qti-base-value base-type="identifier">I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b</qti-base-value>
                            <qti-variable identifier="RESPONSE"/>
                        </qti-member>
                        <qti-member>
                            <qti-base-value base-type="identifier">I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea</qti-base-value>
                            <qti-variable identifier="RESPONSE"/>
                        </qti-member>
                        <qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">I941e9a9a-fd22-43a4-acf2-aa862f943cfc</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                        </qti-not>
                        <qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">I6eb7f951-31d1-46e2-89fb-c7eca3849f9d</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                        </qti-not>
                        <qti-member>
                            <qti-base-value base-type="identifier">I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4</qti-base-value>
                            <qti-variable identifier="RESPONSE"/>
                        </qti-member>
                        <qti-member>
                            <qti-base-value base-type="identifier">Iad84d52a-4b1e-45b3-8067-96811e46ff7e</qti-base-value>
                            <qti-variable identifier="RESPONSE"/>
                        </qti-member>
                    </qti-and>
                    <qti-and>
                        <qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">Ice5853d6-5a4b-4fc6-9298-7bc4047d0e26</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                        </qti-not>
                        <qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">I1752ec64-4652-4723-b3b0-0404b15a0e6d</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                        </qti-not>
                        <qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">Iff799b19-6c0e-406a-ad0d-63f470eac66f</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                        </qti-not>
                        <qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">I88bb2ba0-543d-44e5-977a-1754f8a1d505</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                        </qti-not>
                        <qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                        </qti-not>
                        <qti-member>
                            <qti-base-value base-type="identifier">I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b</qti-base-value>
                            <qti-variable identifier="RESPONSE"/>
                        </qti-member>
                        <qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                        </qti-not>
                        <qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">I941e9a9a-fd22-43a4-acf2-aa862f943cfc</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                        </qti-not>
                        <qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">I6eb7f951-31d1-46e2-89fb-c7eca3849f9d</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                        </qti-not>
                        <qti-member>
                            <qti-base-value base-type="identifier">I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4</qti-base-value>
                            <qti-variable identifier="RESPONSE"/>
                        </qti-member>
                        <qti-member>
                            <qti-base-value base-type="identifier">Iad84d52a-4b1e-45b3-8067-96811e46ff7e</qti-base-value>
                            <qti-variable identifier="RESPONSE"/>
                        </qti-member>
                    </qti-and>
                    <qti-and>
                        <qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">Ice5853d6-5a4b-4fc6-9298-7bc4047d0e26</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                        </qti-not>
                        <qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">I1752ec64-4652-4723-b3b0-0404b15a0e6d</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                        </qti-not>
                        <qti-member>
                            <qti-base-value base-type="identifier">Iff799b19-6c0e-406a-ad0d-63f470eac66f</qti-base-value>
                            <qti-variable identifier="RESPONSE"/>
                        </qti-member>
                        <qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">I88bb2ba0-543d-44e5-977a-1754f8a1d505</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                        </qti-not>
                        <qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                        </qti-not>
                        <qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                        </qti-not>
                        <qti-member>
                            <qti-base-value base-type="identifier">I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea</qti-base-value>
                            <qti-variable identifier="RESPONSE"/>
                        </qti-member>
                        <qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">I941e9a9a-fd22-43a4-acf2-aa862f943cfc</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                        </qti-not>
                        <qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">I6eb7f951-31d1-46e2-89fb-c7eca3849f9d</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                        </qti-not>
                        <qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                        </qti-not>
                        <qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">Iad84d52a-4b1e-45b3-8067-96811e46ff7e</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                        </qti-not>
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
                <qti-string-match case-sensitive="true">
                    <qti-variable identifier="RESPONSE4"/>
                    <qti-base-value base-type="string">drie</qti-base-value>
                </qti-string-match>
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
                <qti-string-match case-sensitive="true">
                    <qti-variable identifier="RESPONSE7"/>
                    <qti-base-value base-type="string">zes</qti-base-value>
                </qti-string-match>
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
                <qti-string-match case-sensitive="true">
                    <qti-variable identifier="RESPONSE8"/>
                    <qti-base-value base-type="string">heer</qti-base-value>
                </qti-string-match>
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
                <qti-string-match case-sensitive="true">
                    <qti-variable identifier="RESPONSE11"/>
                    <qti-base-value base-type="string">tien</qti-base-value>
                </qti-string-match>
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
                <qti-string-match case-sensitive="true">
                    <qti-variable identifier="RESPONSE12"/>
                    <qti-base-value base-type="string">elf</qti-base-value>
                </qti-string-match>
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
                        <qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">Ice5853d6-5a4b-4fc6-9298-7bc4047d0e26</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                        </qti-not>
                        <qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">I1752ec64-4652-4723-b3b0-0404b15a0e6d</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                        </qti-not>
                        <qti-member>
                            <qti-base-value base-type="identifier">Iff799b19-6c0e-406a-ad0d-63f470eac66f</qti-base-value>
                            <qti-variable identifier="RESPONSE"/>
                        </qti-member>
                        <qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">I88bb2ba0-543d-44e5-977a-1754f8a1d505</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                        </qti-not>
                        <qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                        </qti-not>
                        <qti-member>
                            <qti-base-value base-type="identifier">I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b</qti-base-value>
                            <qti-variable identifier="RESPONSE"/>
                        </qti-member>
                        <qti-member>
                            <qti-base-value base-type="identifier">I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea</qti-base-value>
                            <qti-variable identifier="RESPONSE"/>
                        </qti-member>
                        <qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">I941e9a9a-fd22-43a4-acf2-aa862f943cfc</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                        </qti-not>
                        <qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">I6eb7f951-31d1-46e2-89fb-c7eca3849f9d</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                        </qti-not>
                        <qti-member>
                            <qti-base-value base-type="identifier">I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4</qti-base-value>
                            <qti-variable identifier="RESPONSE"/>
                        </qti-member>
                        <qti-member>
                            <qti-base-value base-type="identifier">Iad84d52a-4b1e-45b3-8067-96811e46ff7e</qti-base-value>
                            <qti-variable identifier="RESPONSE"/>
                        </qti-member>
                    </qti-and>
                    <qti-and>
                        <qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">Ice5853d6-5a4b-4fc6-9298-7bc4047d0e26</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                        </qti-not>
                        <qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">I1752ec64-4652-4723-b3b0-0404b15a0e6d</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                        </qti-not>
                        <qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">Iff799b19-6c0e-406a-ad0d-63f470eac66f</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                        </qti-not>
                        <qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">I88bb2ba0-543d-44e5-977a-1754f8a1d505</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                        </qti-not>
                        <qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                        </qti-not>
                        <qti-member>
                            <qti-base-value base-type="identifier">I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b</qti-base-value>
                            <qti-variable identifier="RESPONSE"/>
                        </qti-member>
                        <qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                        </qti-not>
                        <qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">I941e9a9a-fd22-43a4-acf2-aa862f943cfc</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                        </qti-not>
                        <qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">I6eb7f951-31d1-46e2-89fb-c7eca3849f9d</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                        </qti-not>
                        <qti-member>
                            <qti-base-value base-type="identifier">I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4</qti-base-value>
                            <qti-variable identifier="RESPONSE"/>
                        </qti-member>
                        <qti-member>
                            <qti-base-value base-type="identifier">Iad84d52a-4b1e-45b3-8067-96811e46ff7e</qti-base-value>
                            <qti-variable identifier="RESPONSE"/>
                        </qti-member>
                    </qti-and>
                    <qti-and>
                        <qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">Ice5853d6-5a4b-4fc6-9298-7bc4047d0e26</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                        </qti-not>
                        <qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">I1752ec64-4652-4723-b3b0-0404b15a0e6d</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                        </qti-not>
                        <qti-member>
                            <qti-base-value base-type="identifier">Iff799b19-6c0e-406a-ad0d-63f470eac66f</qti-base-value>
                            <qti-variable identifier="RESPONSE"/>
                        </qti-member>
                        <qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">I88bb2ba0-543d-44e5-977a-1754f8a1d505</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                        </qti-not>
                        <qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                        </qti-not>
                        <qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                        </qti-not>
                        <qti-member>
                            <qti-base-value base-type="identifier">I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea</qti-base-value>
                            <qti-variable identifier="RESPONSE"/>
                        </qti-member>
                        <qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">I941e9a9a-fd22-43a4-acf2-aa862f943cfc</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                        </qti-not>
                        <qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">I6eb7f951-31d1-46e2-89fb-c7eca3849f9d</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                        </qti-not>
                        <qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                        </qti-not>
                        <qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">Iad84d52a-4b1e-45b3-8067-96811e46ff7e</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                        </qti-not>
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
                        <qti-string-match case-sensitive="true">
                            <qti-variable identifier="RESPONSE4"/>
                            <qti-base-value base-type="string">drie</qti-base-value>
                        </qti-string-match>
                        <qti-string-match case-sensitive="true">
                            <qti-variable identifier="RESPONSE7"/>
                            <qti-base-value base-type="string">zes</qti-base-value>
                        </qti-string-match>
                        <qti-string-match case-sensitive="true">
                            <qti-variable identifier="RESPONSE8"/>
                            <qti-base-value base-type="string">heer</qti-base-value>
                        </qti-string-match>
                        <qti-string-match case-sensitive="true">
                            <qti-variable identifier="RESPONSE11"/>
                            <qti-base-value base-type="string">tien</qti-base-value>
                        </qti-string-match>
                        <qti-string-match case-sensitive="true">
                            <qti-variable identifier="RESPONSE12"/>
                            <qti-base-value base-type="string">elf</qti-base-value>
                        </qti-string-match>
                    </qti-and>
                    <qti-and>
                        <qti-string-match case-sensitive="true">
                            <qti-variable identifier="RESPONSE7"/>
                            <qti-base-value base-type="string">Bzes</qti-base-value>
                        </qti-string-match>
                        <qti-string-match case-sensitive="true">
                            <qti-variable identifier="RESPONSE11"/>
                            <qti-base-value base-type="string">Ver kaak</qti-base-value>
                        </qti-string-match>
                        <qti-string-match case-sensitive="true">
                            <qti-variable identifier="RESPONSE12"/>
                            <qti-base-value base-type="string">elf</qti-base-value>
                        </qti-string-match>
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
                        <qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">Ice5853d6-5a4b-4fc6-9298-7bc4047d0e26</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                        </qti-not>
                        <qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">I1752ec64-4652-4723-b3b0-0404b15a0e6d</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                        </qti-not>
                        <qti-member>
                            <qti-base-value base-type="identifier">Iff799b19-6c0e-406a-ad0d-63f470eac66f</qti-base-value>
                            <qti-variable identifier="RESPONSE"/>
                        </qti-member>
                        <qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">I88bb2ba0-543d-44e5-977a-1754f8a1d505</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                        </qti-not>
                        <qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                        </qti-not>
                        <qti-member>
                            <qti-base-value base-type="identifier">I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b</qti-base-value>
                            <qti-variable identifier="RESPONSE"/>
                        </qti-member>
                        <qti-member>
                            <qti-base-value base-type="identifier">I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea</qti-base-value>
                            <qti-variable identifier="RESPONSE"/>
                        </qti-member>
                        <qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">I941e9a9a-fd22-43a4-acf2-aa862f943cfc</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                        </qti-not>
                        <qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">I6eb7f951-31d1-46e2-89fb-c7eca3849f9d</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                        </qti-not>
                        <qti-member>
                            <qti-base-value base-type="identifier">I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4</qti-base-value>
                            <qti-variable identifier="RESPONSE"/>
                        </qti-member>
                        <qti-member>
                            <qti-base-value base-type="identifier">Iad84d52a-4b1e-45b3-8067-96811e46ff7e</qti-base-value>
                            <qti-variable identifier="RESPONSE"/>
                        </qti-member>
                        <qti-string-match case-sensitive="true">
                            <qti-variable identifier="RESPONSE4"/>
                            <qti-base-value base-type="string">drie</qti-base-value>
                        </qti-string-match>
                        <qti-string-match case-sensitive="true">
                            <qti-variable identifier="RESPONSE7"/>
                            <qti-base-value base-type="string">zes</qti-base-value>
                        </qti-string-match>
                        <qti-string-match case-sensitive="true">
                            <qti-variable identifier="RESPONSE8"/>
                            <qti-base-value base-type="string">heer</qti-base-value>
                        </qti-string-match>
                        <qti-string-match case-sensitive="true">
                            <qti-variable identifier="RESPONSE11"/>
                            <qti-base-value base-type="string">tien</qti-base-value>
                        </qti-string-match>
                        <qti-string-match case-sensitive="true">
                            <qti-variable identifier="RESPONSE12"/>
                            <qti-base-value base-type="string">elf</qti-base-value>
                        </qti-string-match>
                    </qti-and>
                    <qti-and>
                        <qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">Ice5853d6-5a4b-4fc6-9298-7bc4047d0e26</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                        </qti-not>
                        <qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">I1752ec64-4652-4723-b3b0-0404b15a0e6d</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                        </qti-not>
                        <qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">Iff799b19-6c0e-406a-ad0d-63f470eac66f</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                        </qti-not>
                        <qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">I88bb2ba0-543d-44e5-977a-1754f8a1d505</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                        </qti-not>
                        <qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                        </qti-not>
                        <qti-member>
                            <qti-base-value base-type="identifier">I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b</qti-base-value>
                            <qti-variable identifier="RESPONSE"/>
                        </qti-member>
                        <qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                        </qti-not>
                        <qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">I941e9a9a-fd22-43a4-acf2-aa862f943cfc</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                        </qti-not>
                        <qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">I6eb7f951-31d1-46e2-89fb-c7eca3849f9d</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                        </qti-not>
                        <qti-member>
                            <qti-base-value base-type="identifier">I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4</qti-base-value>
                            <qti-variable identifier="RESPONSE"/>
                        </qti-member>
                        <qti-member>
                            <qti-base-value base-type="identifier">Iad84d52a-4b1e-45b3-8067-96811e46ff7e</qti-base-value>
                            <qti-variable identifier="RESPONSE"/>
                        </qti-member>
                        <qti-string-match case-sensitive="true">
                            <qti-variable identifier="RESPONSE7"/>
                            <qti-base-value base-type="string">zes</qti-base-value>
                        </qti-string-match>
                        <qti-string-match case-sensitive="true">
                            <qti-variable identifier="RESPONSE11"/>
                            <qti-base-value base-type="string">tien</qti-base-value>
                        </qti-string-match>
                        <qti-string-match case-sensitive="true">
                            <qti-variable identifier="RESPONSE12"/>
                            <qti-base-value base-type="string">elf</qti-base-value>
                        </qti-string-match>
                    </qti-and>
                    <qti-and>
                        <qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">Ice5853d6-5a4b-4fc6-9298-7bc4047d0e26</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                        </qti-not>
                        <qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">I1752ec64-4652-4723-b3b0-0404b15a0e6d</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                        </qti-not>
                        <qti-member>
                            <qti-base-value base-type="identifier">Iff799b19-6c0e-406a-ad0d-63f470eac66f</qti-base-value>
                            <qti-variable identifier="RESPONSE"/>
                        </qti-member>
                        <qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">I88bb2ba0-543d-44e5-977a-1754f8a1d505</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                        </qti-not>
                        <qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                        </qti-not>
                        <qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                        </qti-not>
                        <qti-member>
                            <qti-base-value base-type="identifier">I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea</qti-base-value>
                            <qti-variable identifier="RESPONSE"/>
                        </qti-member>
                        <qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">I941e9a9a-fd22-43a4-acf2-aa862f943cfc</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                        </qti-not>
                        <qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">I6eb7f951-31d1-46e2-89fb-c7eca3849f9d</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                        </qti-not>
                        <qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                        </qti-not>
                        <qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">Iad84d52a-4b1e-45b3-8067-96811e46ff7e</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                        </qti-not>
                        <qti-string-match case-sensitive="true">
                            <qti-variable identifier="RESPONSE4"/>
                            <qti-base-value base-type="string">drie</qti-base-value>
                        </qti-string-match>
                        <qti-string-match case-sensitive="true">
                            <qti-variable identifier="RESPONSE8"/>
                            <qti-base-value base-type="string">heer</qti-base-value>
                        </qti-string-match>
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

        Private _responseProcessing4 As XElement =
    <qti-response-processing>
        <qti-response-condition>
            <qti-response-if>
                <qti-member>
                    <qti-base-value base-type="identifier">Iff799b19-6c0e-406a-ad0d-63f470eac66f</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
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
                <qti-member>
                    <qti-base-value base-type="identifier">I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
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
                <qti-member>
                    <qti-base-value base-type="identifier">I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
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
                <qti-member>
                    <qti-base-value base-type="identifier">I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
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
                <qti-member>
                    <qti-base-value base-type="identifier">Iad84d52a-4b1e-45b3-8067-96811e46ff7e</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
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
                <qti-string-match case-sensitive="true">
                    <qti-variable identifier="RESPONSE4"/>
                    <qti-base-value base-type="string">drie</qti-base-value>
                </qti-string-match>
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
                <qti-string-match case-sensitive="true">
                    <qti-variable identifier="RESPONSE7"/>
                    <qti-base-value base-type="string">zes</qti-base-value>
                </qti-string-match>
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
                <qti-string-match case-sensitive="true">
                    <qti-variable identifier="RESPONSE8"/>
                    <qti-base-value base-type="string">heer</qti-base-value>
                </qti-string-match>
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
                <qti-string-match case-sensitive="true">
                    <qti-variable identifier="RESPONSE11"/>
                    <qti-base-value base-type="string">tien</qti-base-value>
                </qti-string-match>
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
                <qti-string-match case-sensitive="true">
                    <qti-variable identifier="RESPONSE12"/>
                    <qti-base-value base-type="string">elf</qti-base-value>
                </qti-string-match>
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
                            <qti-not>
                                <qti-member>
                                    <qti-base-value base-type="identifier">Ice5853d6-5a4b-4fc6-9298-7bc4047d0e26</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                            </qti-not>
                            <qti-not>
                                <qti-member>
                                    <qti-base-value base-type="identifier">I1752ec64-4652-4723-b3b0-0404b15a0e6d</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                            </qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">Iff799b19-6c0e-406a-ad0d-63f470eac66f</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                            <qti-not>
                                <qti-member>
                                    <qti-base-value base-type="identifier">I88bb2ba0-543d-44e5-977a-1754f8a1d505</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                            </qti-not>
                            <qti-not>
                                <qti-member>
                                    <qti-base-value base-type="identifier">I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                            </qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                            <qti-member>
                                <qti-base-value base-type="identifier">I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                            <qti-not>
                                <qti-member>
                                    <qti-base-value base-type="identifier">I941e9a9a-fd22-43a4-acf2-aa862f943cfc</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                            </qti-not>
                            <qti-not>
                                <qti-member>
                                    <qti-base-value base-type="identifier">I6eb7f951-31d1-46e2-89fb-c7eca3849f9d</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                            </qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                            <qti-member>
                                <qti-base-value base-type="identifier">Iad84d52a-4b1e-45b3-8067-96811e46ff7e</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                        </qti-and>
                        <qti-and>
                            <qti-not>
                                <qti-member>
                                    <qti-base-value base-type="identifier">Ice5853d6-5a4b-4fc6-9298-7bc4047d0e26</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                            </qti-not>
                            <qti-not>
                                <qti-member>
                                    <qti-base-value base-type="identifier">I1752ec64-4652-4723-b3b0-0404b15a0e6d</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                            </qti-not>
                            <qti-not>
                                <qti-member>
                                    <qti-base-value base-type="identifier">Iff799b19-6c0e-406a-ad0d-63f470eac66f</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                            </qti-not>
                            <qti-not>
                                <qti-member>
                                    <qti-base-value base-type="identifier">I88bb2ba0-543d-44e5-977a-1754f8a1d505</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                            </qti-not>
                            <qti-not>
                                <qti-member>
                                    <qti-base-value base-type="identifier">I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                            </qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                            <qti-not>
                                <qti-member>
                                    <qti-base-value base-type="identifier">I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                            </qti-not>
                            <qti-not>
                                <qti-member>
                                    <qti-base-value base-type="identifier">I941e9a9a-fd22-43a4-acf2-aa862f943cfc</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                            </qti-not>
                            <qti-not>
                                <qti-member>
                                    <qti-base-value base-type="identifier">I6eb7f951-31d1-46e2-89fb-c7eca3849f9d</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                            </qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                            <qti-member>
                                <qti-base-value base-type="identifier">Iad84d52a-4b1e-45b3-8067-96811e46ff7e</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                        </qti-and>
                        <qti-and>
                            <qti-not>
                                <qti-member>
                                    <qti-base-value base-type="identifier">Ice5853d6-5a4b-4fc6-9298-7bc4047d0e26</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                            </qti-not>
                            <qti-not>
                                <qti-member>
                                    <qti-base-value base-type="identifier">I1752ec64-4652-4723-b3b0-0404b15a0e6d</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                            </qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">Iff799b19-6c0e-406a-ad0d-63f470eac66f</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                            <qti-not>
                                <qti-member>
                                    <qti-base-value base-type="identifier">I88bb2ba0-543d-44e5-977a-1754f8a1d505</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                            </qti-not>
                            <qti-not>
                                <qti-member>
                                    <qti-base-value base-type="identifier">I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                            </qti-not>
                            <qti-not>
                                <qti-member>
                                    <qti-base-value base-type="identifier">I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                            </qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                            <qti-not>
                                <qti-member>
                                    <qti-base-value base-type="identifier">I941e9a9a-fd22-43a4-acf2-aa862f943cfc</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                            </qti-not>
                            <qti-not>
                                <qti-member>
                                    <qti-base-value base-type="identifier">I6eb7f951-31d1-46e2-89fb-c7eca3849f9d</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                            </qti-not>
                            <qti-not>
                                <qti-member>
                                    <qti-base-value base-type="identifier">I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                            </qti-not>
                            <qti-not>
                                <qti-member>
                                    <qti-base-value base-type="identifier">Iad84d52a-4b1e-45b3-8067-96811e46ff7e</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                            </qti-not>
                        </qti-and>
                    </qti-or>
                    <qti-string-match case-sensitive="true">
                        <qti-variable identifier="RESPONSE4"/>
                        <qti-base-value base-type="string">drie</qti-base-value>
                    </qti-string-match>
                    <qti-string-match case-sensitive="true">
                        <qti-variable identifier="RESPONSE7"/>
                        <qti-base-value base-type="string">zes</qti-base-value>
                    </qti-string-match>
                    <qti-string-match case-sensitive="true">
                        <qti-variable identifier="RESPONSE8"/>
                        <qti-base-value base-type="string">heer</qti-base-value>
                    </qti-string-match>
                    <qti-string-match case-sensitive="true">
                        <qti-variable identifier="RESPONSE11"/>
                        <qti-base-value base-type="string">tien</qti-base-value>
                    </qti-string-match>
                    <qti-string-match case-sensitive="true">
                        <qti-variable identifier="RESPONSE12"/>
                        <qti-base-value base-type="string">elf</qti-base-value>
                    </qti-string-match>
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
                            <qti-not>
                                <qti-member>
                                    <qti-base-value base-type="identifier">Ice5853d6-5a4b-4fc6-9298-7bc4047d0e26</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                            </qti-not>
                            <qti-not>
                                <qti-member>
                                    <qti-base-value base-type="identifier">I1752ec64-4652-4723-b3b0-0404b15a0e6d</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                            </qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">Iff799b19-6c0e-406a-ad0d-63f470eac66f</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                            <qti-not>
                                <qti-member>
                                    <qti-base-value base-type="identifier">I88bb2ba0-543d-44e5-977a-1754f8a1d505</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                            </qti-not>
                            <qti-not>
                                <qti-member>
                                    <qti-base-value base-type="identifier">I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                            </qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                            <qti-member>
                                <qti-base-value base-type="identifier">I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                            <qti-not>
                                <qti-member>
                                    <qti-base-value base-type="identifier">I941e9a9a-fd22-43a4-acf2-aa862f943cfc</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                            </qti-not>
                            <qti-not>
                                <qti-member>
                                    <qti-base-value base-type="identifier">I6eb7f951-31d1-46e2-89fb-c7eca3849f9d</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                            </qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                            <qti-member>
                                <qti-base-value base-type="identifier">Iad84d52a-4b1e-45b3-8067-96811e46ff7e</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                        </qti-and>
                        <qti-and>
                            <qti-not>
                                <qti-member>
                                    <qti-base-value base-type="identifier">Ice5853d6-5a4b-4fc6-9298-7bc4047d0e26</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                            </qti-not>
                            <qti-not>
                                <qti-member>
                                    <qti-base-value base-type="identifier">I1752ec64-4652-4723-b3b0-0404b15a0e6d</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                            </qti-not>
                            <qti-not>
                                <qti-member>
                                    <qti-base-value base-type="identifier">Iff799b19-6c0e-406a-ad0d-63f470eac66f</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                            </qti-not>
                            <qti-not>
                                <qti-member>
                                    <qti-base-value base-type="identifier">I88bb2ba0-543d-44e5-977a-1754f8a1d505</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                            </qti-not>
                            <qti-not>
                                <qti-member>
                                    <qti-base-value base-type="identifier">I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                            </qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                            <qti-not>
                                <qti-member>
                                    <qti-base-value base-type="identifier">I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                            </qti-not>
                            <qti-not>
                                <qti-member>
                                    <qti-base-value base-type="identifier">I941e9a9a-fd22-43a4-acf2-aa862f943cfc</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                            </qti-not>
                            <qti-not>
                                <qti-member>
                                    <qti-base-value base-type="identifier">I6eb7f951-31d1-46e2-89fb-c7eca3849f9d</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                            </qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                            <qti-member>
                                <qti-base-value base-type="identifier">Iad84d52a-4b1e-45b3-8067-96811e46ff7e</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                        </qti-and>
                        <qti-and>
                            <qti-not>
                                <qti-member>
                                    <qti-base-value base-type="identifier">Ice5853d6-5a4b-4fc6-9298-7bc4047d0e26</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                            </qti-not>
                            <qti-not>
                                <qti-member>
                                    <qti-base-value base-type="identifier">I1752ec64-4652-4723-b3b0-0404b15a0e6d</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                            </qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">Iff799b19-6c0e-406a-ad0d-63f470eac66f</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                            <qti-not>
                                <qti-member>
                                    <qti-base-value base-type="identifier">I88bb2ba0-543d-44e5-977a-1754f8a1d505</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                            </qti-not>
                            <qti-not>
                                <qti-member>
                                    <qti-base-value base-type="identifier">I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                            </qti-not>
                            <qti-not>
                                <qti-member>
                                    <qti-base-value base-type="identifier">I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                            </qti-not>
                            <qti-member>
                                <qti-base-value base-type="identifier">I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea</qti-base-value>
                                <qti-variable identifier="RESPONSE"/>
                            </qti-member>
                            <qti-not>
                                <qti-member>
                                    <qti-base-value base-type="identifier">I941e9a9a-fd22-43a4-acf2-aa862f943cfc</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                            </qti-not>
                            <qti-not>
                                <qti-member>
                                    <qti-base-value base-type="identifier">I6eb7f951-31d1-46e2-89fb-c7eca3849f9d</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                            </qti-not>
                            <qti-not>
                                <qti-member>
                                    <qti-base-value base-type="identifier">I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                            </qti-not>
                            <qti-not>
                                <qti-member>
                                    <qti-base-value base-type="identifier">Iad84d52a-4b1e-45b3-8067-96811e46ff7e</qti-base-value>
                                    <qti-variable identifier="RESPONSE"/>
                                </qti-member>
                            </qti-not>
                        </qti-and>
                    </qti-or>
                    <qti-or>
                        <qti-and>
                            <qti-string-match case-sensitive="true">
                                <qti-variable identifier="RESPONSE4"/>
                                <qti-base-value base-type="string">drie</qti-base-value>
                            </qti-string-match>
                            <qti-string-match case-sensitive="true">
                                <qti-variable identifier="RESPONSE7"/>
                                <qti-base-value base-type="string">zes</qti-base-value>
                            </qti-string-match>
                            <qti-string-match case-sensitive="true">
                                <qti-variable identifier="RESPONSE8"/>
                                <qti-base-value base-type="string">heer</qti-base-value>
                            </qti-string-match>
                            <qti-string-match case-sensitive="true">
                                <qti-variable identifier="RESPONSE11"/>
                                <qti-base-value base-type="string">tien</qti-base-value>
                            </qti-string-match>
                            <qti-string-match case-sensitive="true">
                                <qti-variable identifier="RESPONSE12"/>
                                <qti-base-value base-type="string">elf</qti-base-value>
                            </qti-string-match>
                        </qti-and>
                        <qti-and>
                            <qti-string-match case-sensitive="true">
                                <qti-variable identifier="RESPONSE7"/>
                                <qti-base-value base-type="string">Bzes</qti-base-value>
                            </qti-string-match>
                            <qti-string-match case-sensitive="true">
                                <qti-variable identifier="RESPONSE11"/>
                                <qti-base-value base-type="string">Ver kaak</qti-base-value>
                            </qti-string-match>
                            <qti-string-match case-sensitive="true">
                                <qti-variable identifier="RESPONSE12"/>
                                <qti-base-value base-type="string">elf</qti-base-value>
                            </qti-string-match>
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
                    <qti-not>
                        <qti-member>
                            <qti-base-value base-type="identifier">Ice5853d6-5a4b-4fc6-9298-7bc4047d0e26</qti-base-value>
                            <qti-variable identifier="RESPONSE"/>
                        </qti-member>
                    </qti-not>
                    <qti-not>
                        <qti-member>
                            <qti-base-value base-type="identifier">I1752ec64-4652-4723-b3b0-0404b15a0e6d</qti-base-value>
                            <qti-variable identifier="RESPONSE"/>
                        </qti-member>
                    </qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">Iff799b19-6c0e-406a-ad0d-63f470eac66f</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                    <qti-not>
                        <qti-member>
                            <qti-base-value base-type="identifier">I88bb2ba0-543d-44e5-977a-1754f8a1d505</qti-base-value>
                            <qti-variable identifier="RESPONSE"/>
                        </qti-member>
                    </qti-not>
                    <qti-not>
                        <qti-member>
                            <qti-base-value base-type="identifier">I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a</qti-base-value>
                            <qti-variable identifier="RESPONSE"/>
                        </qti-member>
                    </qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                    <qti-member>
                        <qti-base-value base-type="identifier">I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                    <qti-not>
                        <qti-member>
                            <qti-base-value base-type="identifier">I941e9a9a-fd22-43a4-acf2-aa862f943cfc</qti-base-value>
                            <qti-variable identifier="RESPONSE"/>
                        </qti-member>
                    </qti-not>
                    <qti-not>
                        <qti-member>
                            <qti-base-value base-type="identifier">I6eb7f951-31d1-46e2-89fb-c7eca3849f9d</qti-base-value>
                            <qti-variable identifier="RESPONSE"/>
                        </qti-member>
                    </qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                    <qti-member>
                        <qti-base-value base-type="identifier">Iad84d52a-4b1e-45b3-8067-96811e46ff7e</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                    <qti-string-match case-sensitive="true">
                        <qti-variable identifier="RESPONSE4"/>
                        <qti-base-value base-type="string">drie</qti-base-value>
                    </qti-string-match>
                    <qti-string-match case-sensitive="true">
                        <qti-variable identifier="RESPONSE7"/>
                        <qti-base-value base-type="string">zes</qti-base-value>
                    </qti-string-match>
                    <qti-string-match case-sensitive="true">
                        <qti-variable identifier="RESPONSE8"/>
                        <qti-base-value base-type="string">heer</qti-base-value>
                    </qti-string-match>
                    <qti-string-match case-sensitive="true">
                        <qti-variable identifier="RESPONSE11"/>
                        <qti-base-value base-type="string">tien</qti-base-value>
                    </qti-string-match>
                    <qti-string-match case-sensitive="true">
                        <qti-variable identifier="RESPONSE12"/>
                        <qti-base-value base-type="string">elf</qti-base-value>
                    </qti-string-match>
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
