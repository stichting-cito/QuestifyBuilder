Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.QTI.Converters.ScoringConverter.QTI22

<TestClass()>
Public Class QTI22ResponseProcessingHottextTests
    Inherits QTI22ResponseProcessingTests_Base

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
        Dim scoringPrms As HashSet(Of ScoringParameter) = GetHottextScoringParams()
        RunResponseProcessingTest(_itemBody1, findingElement, responseProcessingElement, scoringPrms, New QTI22CombinedScoringConverter(scoringPrms))
    End Sub

    Private Function GetHottextScoringParams() As HashSet(Of ScoringParameter)
        Dim scoreParams As New HashSet(Of ScoringParameter)

        Dim scoreParam As HotTextScoringParameter = New HotTextScoringParameter() With {.ControllerId = "hottextController", .FindingOverride = "hottextController", .MinChoices = 1, .MaxChoices = 25}.AddSubParameters("Iba0772fe-878f-457c-b652-a0e922626082", "I5493e55f-fae9-499f-bd3e-16df52c028e1", "I7134c778-4641-40b5-8eae-1a80013cf777", "I3cb9edb6-551e-474a-9122-338b8c6a396d", "I41d5e633-d892-46fe-8a0c-ceba54c167f3", "I498a96cd-114d-4299-b27c-639e41559e29", "I19b64b54-f7f4-4284-af37-37999bd24935", "I7ea195a7-b0ec-465e-ada3-31c9e940f0dc", "Ibf683ce7-ac85-4610-b057-dd0578f3b71f", "Ie6f1fe1f-fe23-4f29-85ec-27d483138cfa", "I2b4ed71d-8984-4ffd-a95a-126efbb12e48", "I0778d3a8-11c3-41f5-aeb7-4efd94338296", "I3a1bf3a7-f9b4-472d-ac19-c6d27f671543", "I01f17f48-6da9-44a0-bb7f-82e325f2f233", "Ifdbe8e9c-0eeb-4019-b8a7-5599265834fb", "I0dd2d1e9-84b7-4b3f-a144-a2ec2c13061b", "I41e6c74d-787a-4620-889f-fae84be71cdb", "I1f639c54-57ee-428e-93ed-77d786d5a2d1", "Ib580dda7-0717-4cfc-b70e-34059b4c78f1", "I834d38a6-dac5-4235-9cf4-d323dd89f1d2", "Ic858ebcf-6f25-46ee-be0b-afbee1f41971", "If6a1b756-b293-4baf-a3c0-3332221e3ea8", "I80cb35ef-89d7-4ada-aef4-eda2b963a606", "Ia2c49323-f160-4dfa-8f79-5abea876efa6", "I8ecdc331-d565-4fcc-9f1f-a83e6e6bab9d", "I4142aba3-f108-41dc-b537-ab44fd8cb5ae", "If9dc68d4-33ec-4176-adb2-eda10373f1a0", "Ice8f6ba6-0b24-4234-8e1c-ea2d6caf18f6", "Ia64d8e75-aa61-481b-bd40-df7270053448", "I2a787ac6-adce-419f-b230-293302ab6e2b", "I562dc41e-1fcd-45b2-ad63-e346a9b12ed3", "I2ff56de9-53fd-4be6-adc3-b9d48cbac0bd", "I8c49dcaf-eea6-48ad-bcf0-5c00ccde0415", "I7b431d95-a30e-403f-9b35-7575b30d31a3", "I46f0651d-a9f2-4b96-8172-8104b0938213", "I1e40e819-c8a2-42dd-89de-dd29e070b99b", "I67b6042e-7444-46be-b169-53ec421f9eda", "I18248834-8320-4342-967c-2007195ba14a", "I0bf4350c-9288-4cec-9e37-1b0978b057ba", "Ifb1bfeea-20a0-4d29-bb87-d502ff355522", "Ie1f531cd-79c9-4b87-a90c-76766792b183", "I66faad9a-d004-4cdc-a275-6f9ff736bb14", "I665f2702-e926-4266-bc3c-3f04ea9a6c62", "I7e8b004a-0223-432a-bb9a-536918190017", "I217873b0-4970-405f-a80b-b2a9f42bbd85", "I46521597-f1f4-432c-96c2-8e3defb05950", "Ifbea86b5-20bc-4dde-a4e4-4a14153c8ead", "I279d3c47-95a8-42d1-9440-26fadf909090", "I6d80a45f-5042-4f42-99fe-151229268a43")

        Dim xhtmlValue As XElement = <xhtmlparameter name="hottextInput">
                                         <p id="c1-id-11" xmlns="http://www.w3.org/1999/xhtml">
                                             <cito:InlineElement id="Iba0772fe-878f-457c-b652-a0e922626082" layoutTemplateSourceName="InlineHottextLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                 <cito:parameters>
                                                     <cito:parameterSet id="entireItem">
                                                         <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                         <cito:plaintextparameter name="controlId">Iba0772fe-878f-457c-b652-a0e922626082</cito:plaintextparameter>
                                                         <cito:plaintextparameter name="controlLabel">Op</cito:plaintextparameter>
                                                         <cito:booleanparameter name="addHottextCorrection">False</cito:booleanparameter>
                                                         <cito:plaintextparameter name="hottextValue"/>
                                                     </cito:parameterSet>
                                                 </cito:parameters>
                                             </cito:InlineElement>
                                             <span id="SIba0772fe-878f-457c-b652-a0e922626082" style="background-color: #C7B8CE;">Op</span><cito:InlineElement id="I5493e55f-fae9-499f-bd3e-16df52c028e1" layoutTemplateSourceName="InlineHottextLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                 <cito:parameters>
                                                     <cito:parameterSet id="entireItem">
                                                         <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                         <cito:plaintextparameter name="controlId">I5493e55f-fae9-499f-bd3e-16df52c028e1</cito:plaintextparameter>
                                                         <cito:plaintextparameter name="controlLabel">vrijwel</cito:plaintextparameter>
                                                         <cito:booleanparameter name="addHottextCorrection">False</cito:booleanparameter>
                                                         <cito:plaintextparameter name="hottextValue"/>
                                                     </cito:parameterSet>
                                                 </cito:parameters>
                                             </cito:InlineElement><span id="SI5493e55f-fae9-499f-bd3e-16df52c028e1" style="background-color: #C7B8CE;">vrijwel</span><cito:InlineElement id="I7134c778-4641-40b5-8eae-1a80013cf777" layoutTemplateSourceName="InlineHottextLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                 <cito:parameters>
                                                     <cito:parameterSet id="entireItem">
                                                         <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                         <cito:plaintextparameter name="controlId">I7134c778-4641-40b5-8eae-1a80013cf777</cito:plaintextparameter>
                                                         <cito:plaintextparameter name="controlLabel">alle</cito:plaintextparameter>
                                                         <cito:booleanparameter name="addHottextCorrection">False</cito:booleanparameter>
                                                         <cito:plaintextparameter name="hottextValue"/>
                                                     </cito:parameterSet>
                                                 </cito:parameters>
                                             </cito:InlineElement><span id="SI7134c778-4641-40b5-8eae-1a80013cf777" style="background-color: #C7B8CE;">alle</span><cito:InlineElement id="I3cb9edb6-551e-474a-9122-338b8c6a396d" layoutTemplateSourceName="InlineHottextLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                 <cito:parameters>
                                                     <cito:parameterSet id="entireItem">
                                                         <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                         <cito:plaintextparameter name="controlId">I3cb9edb6-551e-474a-9122-338b8c6a396d</cito:plaintextparameter>
                                                         <cito:plaintextparameter name="controlLabel">een</cito:plaintextparameter>
                                                         <cito:booleanparameter name="addHottextCorrection">False</cito:booleanparameter>
                                                         <cito:plaintextparameter name="hottextValue"/>
                                                     </cito:parameterSet>
                                                 </cito:parameters>
                                             </cito:InlineElement><span id="SI3cb9edb6-551e-474a-9122-338b8c6a396d" style="background-color: #C7B8CE;">een</span><cito:InlineElement id="I41d5e633-d892-46fe-8a0c-ceba54c167f3" layoutTemplateSourceName="InlineHottextLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                 <cito:parameters>
                                                     <cito:parameterSet id="entireItem">
                                                         <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                         <cito:plaintextparameter name="controlId">I41d5e633-d892-46fe-8a0c-ceba54c167f3</cito:plaintextparameter>
                                                         <cito:plaintextparameter name="controlLabel">meren</cito:plaintextparameter>
                                                         <cito:booleanparameter name="addHottextCorrection">False</cito:booleanparameter>
                                                         <cito:plaintextparameter name="hottextValue"/>
                                                     </cito:parameterSet>
                                                 </cito:parameters>
                                             </cito:InlineElement><span id="SI41d5e633-d892-46fe-8a0c-ceba54c167f3" style="background-color: #C7B8CE;">meren</span><cito:InlineElement id="I498a96cd-114d-4299-b27c-639e41559e29" layoutTemplateSourceName="InlineHottextLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                 <cito:parameters>
                                                     <cito:parameterSet id="entireItem">
                                                         <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                         <cito:plaintextparameter name="controlId">I498a96cd-114d-4299-b27c-639e41559e29</cito:plaintextparameter>
                                                         <cito:plaintextparameter name="controlLabel">is</cito:plaintextparameter>
                                                         <cito:booleanparameter name="addHottextCorrection">False</cito:booleanparameter>
                                                         <cito:plaintextparameter name="hottextValue"/>
                                                     </cito:parameterSet>
                                                 </cito:parameters>
                                             </cito:InlineElement><span id="SI498a96cd-114d-4299-b27c-639e41559e29" style="background-color: #C7B8CE;">is</span><cito:InlineElement id="I19b64b54-f7f4-4284-af37-37999bd24935" layoutTemplateSourceName="InlineHottextLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                 <cito:parameters>
                                                     <cito:parameterSet id="entireItem">
                                                         <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                         <cito:plaintextparameter name="controlId">I19b64b54-f7f4-4284-af37-37999bd24935</cito:plaintextparameter>
                                                         <cito:plaintextparameter name="controlLabel">in</cito:plaintextparameter>
                                                         <cito:booleanparameter name="addHottextCorrection">False</cito:booleanparameter>
                                                         <cito:plaintextparameter name="hottextValue"/>
                                                     </cito:parameterSet>
                                                 </cito:parameters>
                                             </cito:InlineElement><span id="SI19b64b54-f7f4-4284-af37-37999bd24935" style="background-color: #C7B8CE;">in</span><cito:InlineElement id="I7ea195a7-b0ec-465e-ada3-31c9e940f0dc" layoutTemplateSourceName="InlineHottextLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                 <cito:parameters>
                                                     <cito:parameterSet id="entireItem">
                                                         <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                         <cito:plaintextparameter name="controlId">I7ea195a7-b0ec-465e-ada3-31c9e940f0dc</cito:plaintextparameter>
                                                         <cito:plaintextparameter name="controlLabel">de</cito:plaintextparameter>
                                                         <cito:booleanparameter name="addHottextCorrection">False</cito:booleanparameter>
                                                         <cito:plaintextparameter name="hottextValue"/>
                                                     </cito:parameterSet>
                                                 </cito:parameters>
                                             </cito:InlineElement><span id="SI7ea195a7-b0ec-465e-ada3-31c9e940f0dc" style="background-color: #C7B8CE;">de</span><cito:InlineElement id="Ibf683ce7-ac85-4610-b057-dd0578f3b71f" layoutTemplateSourceName="InlineHottextLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                 <cito:parameters>
                                                     <cito:parameterSet id="entireItem">
                                                         <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                         <cito:plaintextparameter name="controlId">Ibf683ce7-ac85-4610-b057-dd0578f3b71f</cito:plaintextparameter>
                                                         <cito:plaintextparameter name="controlLabel">zomer</cito:plaintextparameter>
                                                         <cito:booleanparameter name="addHottextCorrection">False</cito:booleanparameter>
                                                         <cito:plaintextparameter name="hottextValue"/>
                                                     </cito:parameterSet>
                                                 </cito:parameters>
                                             </cito:InlineElement><span id="SIbf683ce7-ac85-4610-b057-dd0578f3b71f" style="background-color: #C7B8CE;">zomer</span><cito:InlineElement id="Ie6f1fe1f-fe23-4f29-85ec-27d483138cfa" layoutTemplateSourceName="InlineHottextLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                 <cito:parameters>
                                                     <cito:parameterSet id="entireItem">
                                                         <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                         <cito:plaintextparameter name="controlId">Ie6f1fe1f-fe23-4f29-85ec-27d483138cfa</cito:plaintextparameter>
                                                         <cito:plaintextparameter name="controlLabel">veel</cito:plaintextparameter>
                                                         <cito:booleanparameter name="addHottextCorrection">False</cito:booleanparameter>
                                                         <cito:plaintextparameter name="hottextValue"/>
                                                     </cito:parameterSet>
                                                 </cito:parameters>
                                             </cito:InlineElement><span id="SIe6f1fe1f-fe23-4f29-85ec-27d483138cfa" style="background-color: #C7B8CE;">veel</span><cito:InlineElement id="I2b4ed71d-8984-4ffd-a95a-126efbb12e48" layoutTemplateSourceName="InlineHottextLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                 <cito:parameters>
                                                     <cito:parameterSet id="entireItem">
                                                         <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                         <cito:plaintextparameter name="controlId">I2b4ed71d-8984-4ffd-a95a-126efbb12e48</cito:plaintextparameter>
                                                         <cito:plaintextparameter name="controlLabel">te</cito:plaintextparameter>
                                                         <cito:booleanparameter name="addHottextCorrection">False</cito:booleanparameter>
                                                         <cito:plaintextparameter name="hottextValue"/>
                                                     </cito:parameterSet>
                                                 </cito:parameters>
                                             </cito:InlineElement><span id="SI2b4ed71d-8984-4ffd-a95a-126efbb12e48" style="background-color: #C7B8CE;">te</span><cito:InlineElement id="I0778d3a8-11c3-41f5-aeb7-4efd94338296" layoutTemplateSourceName="InlineHottextLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                 <cito:parameters>
                                                     <cito:parameterSet id="entireItem">
                                                         <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                         <cito:plaintextparameter name="controlId">I0778d3a8-11c3-41f5-aeb7-4efd94338296</cito:plaintextparameter>
                                                         <cito:plaintextparameter name="controlLabel">doen</cito:plaintextparameter>
                                                         <cito:booleanparameter name="addHottextCorrection">False</cito:booleanparameter>
                                                         <cito:plaintextparameter name="hottextValue"/>
                                                     </cito:parameterSet>
                                                 </cito:parameters>
                                             </cito:InlineElement><span id="SI0778d3a8-11c3-41f5-aeb7-4efd94338296" style="background-color: #C7B8CE;">doen</span>: <cito:InlineElement id="I3a1bf3a7-f9b4-472d-ac19-c6d27f671543" layoutTemplateSourceName="InlineHottextLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                 <cito:parameters>
                                                     <cito:parameterSet id="entireItem">
                                                         <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                         <cito:plaintextparameter name="controlId">I3a1bf3a7-f9b4-472d-ac19-c6d27f671543</cito:plaintextparameter>
                                                         <cito:plaintextparameter name="controlLabel">ze</cito:plaintextparameter>
                                                         <cito:booleanparameter name="addHottextCorrection">False</cito:booleanparameter>
                                                         <cito:plaintextparameter name="hottextValue"/>
                                                     </cito:parameterSet>
                                                 </cito:parameters>
                                             </cito:InlineElement><span id="SI3a1bf3a7-f9b4-472d-ac19-c6d27f671543" style="background-color: #C7B8CE;">ze</span><cito:InlineElement id="I01f17f48-6da9-44a0-bb7f-82e325f2f233" layoutTemplateSourceName="InlineHottextLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                 <cito:parameters>
                                                     <cito:parameterSet id="entireItem">
                                                         <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                         <cito:plaintextparameter name="controlId">I01f17f48-6da9-44a0-bb7f-82e325f2f233</cito:plaintextparameter>
                                                         <cito:plaintextparameter name="controlLabel">zijn</cito:plaintextparameter>
                                                         <cito:booleanparameter name="addHottextCorrection">False</cito:booleanparameter>
                                                         <cito:plaintextparameter name="hottextValue"/>
                                                     </cito:parameterSet>
                                                 </cito:parameters>
                                             </cito:InlineElement><span id="SI01f17f48-6da9-44a0-bb7f-82e325f2f233" style="background-color: #C7B8CE;">zijn</span><cito:InlineElement id="Ifdbe8e9c-0eeb-4019-b8a7-5599265834fb" layoutTemplateSourceName="InlineHottextLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                 <cito:parameters>
                                                     <cito:parameterSet id="entireItem">
                                                         <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                         <cito:plaintextparameter name="controlId">Ifdbe8e9c-0eeb-4019-b8a7-5599265834fb</cito:plaintextparameter>
                                                         <cito:plaintextparameter name="controlLabel">twee</cito:plaintextparameter>
                                                         <cito:booleanparameter name="addHottextCorrection">False</cito:booleanparameter>
                                                         <cito:plaintextparameter name="hottextValue"/>
                                                     </cito:parameterSet>
                                                 </cito:parameters>
                                             </cito:InlineElement><span id="SIfdbe8e9c-0eeb-4019-b8a7-5599265834fb" style="background-color: #C7B8CE;">twee</span><cito:InlineElement id="I0dd2d1e9-84b7-4b3f-a144-a2ec2c13061b" layoutTemplateSourceName="InlineHottextLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                 <cito:parameters>
                                                     <cito:parameterSet id="entireItem">
                                                         <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                         <cito:plaintextparameter name="controlId">I0dd2d1e9-84b7-4b3f-a144-a2ec2c13061b</cito:plaintextparameter>
                                                         <cito:plaintextparameter name="controlLabel">geschikt</cito:plaintextparameter>
                                                         <cito:booleanparameter name="addHottextCorrection">False</cito:booleanparameter>
                                                         <cito:plaintextparameter name="hottextValue"/>
                                                     </cito:parameterSet>
                                                 </cito:parameters>
                                             </cito:InlineElement><span id="SI0dd2d1e9-84b7-4b3f-a144-a2ec2c13061b" style="background-color: #C7B8CE;">geschikt</span><cito:InlineElement id="I41e6c74d-787a-4620-889f-fae84be71cdb" layoutTemplateSourceName="InlineHottextLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                 <cito:parameters>
                                                     <cito:parameterSet id="entireItem">
                                                         <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                         <cito:plaintextparameter name="controlId">I41e6c74d-787a-4620-889f-fae84be71cdb</cito:plaintextparameter>
                                                         <cito:plaintextparameter name="controlLabel">voor</cito:plaintextparameter>
                                                         <cito:booleanparameter name="addHottextCorrection">False</cito:booleanparameter>
                                                         <cito:plaintextparameter name="hottextValue"/>
                                                     </cito:parameterSet>
                                                 </cito:parameters>
                                             </cito:InlineElement><span id="SI41e6c74d-787a-4620-889f-fae84be71cdb" style="background-color: #C7B8CE;">voor</span><cito:InlineElement id="I1f639c54-57ee-428e-93ed-77d786d5a2d1" layoutTemplateSourceName="InlineHottextLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                 <cito:parameters>
                                                     <cito:parameterSet id="entireItem">
                                                         <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                         <cito:plaintextparameter name="controlId">I1f639c54-57ee-428e-93ed-77d786d5a2d1</cito:plaintextparameter>
                                                         <cito:plaintextparameter name="controlLabel">drie</cito:plaintextparameter>
                                                         <cito:booleanparameter name="addHottextCorrection">False</cito:booleanparameter>
                                                         <cito:plaintextparameter name="hottextValue"/>
                                                     </cito:parameterSet>
                                                 </cito:parameters>
                                             </cito:InlineElement><span id="SI1f639c54-57ee-428e-93ed-77d786d5a2d1" style="background-color: #C7B8CE;">drie</span>. <cito:InlineElement id="Ib580dda7-0717-4cfc-b70e-34059b4c78f1" layoutTemplateSourceName="InlineHottextLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                 <cito:parameters>
                                                     <cito:parameterSet id="entireItem">
                                                         <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                         <cito:plaintextparameter name="controlId">Ib580dda7-0717-4cfc-b70e-34059b4c78f1</cito:plaintextparameter>
                                                         <cito:plaintextparameter name="controlLabel">Met</cito:plaintextparameter>
                                                         <cito:booleanparameter name="addHottextCorrection">False</cito:booleanparameter>
                                                         <cito:plaintextparameter name="hottextValue"/>
                                                     </cito:parameterSet>
                                                 </cito:parameters>
                                             </cito:InlineElement><span id="SIb580dda7-0717-4cfc-b70e-34059b4c78f1" style="background-color: #C7B8CE;">Met</span><cito:InlineElement id="I834d38a6-dac5-4235-9cf4-d323dd89f1d2" layoutTemplateSourceName="InlineHottextLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                 <cito:parameters>
                                                     <cito:parameterSet id="entireItem">
                                                         <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                         <cito:plaintextparameter name="controlId">I834d38a6-dac5-4235-9cf4-d323dd89f1d2</cito:plaintextparameter>
                                                         <cito:plaintextparameter name="controlLabel">de</cito:plaintextparameter>
                                                         <cito:booleanparameter name="addHottextCorrection">False</cito:booleanparameter>
                                                         <cito:plaintextparameter name="hottextValue"/>
                                                     </cito:parameterSet>
                                                 </cito:parameters>
                                             </cito:InlineElement><span id="SI834d38a6-dac5-4235-9cf4-d323dd89f1d2" style="background-color: #C7B8CE;">de</span><cito:InlineElement id="Ic858ebcf-6f25-46ee-be0b-afbee1f41971" layoutTemplateSourceName="InlineHottextLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                 <cito:parameters>
                                                     <cito:parameterSet id="entireItem">
                                                         <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                         <cito:plaintextparameter name="controlId">Ic858ebcf-6f25-46ee-be0b-afbee1f41971</cito:plaintextparameter>
                                                         <cito:plaintextparameter name="controlLabel">bovenbouw</cito:plaintextparameter>
                                                         <cito:booleanparameter name="addHottextCorrection">False</cito:booleanparameter>
                                                         <cito:plaintextparameter name="hottextValue"/>
                                                     </cito:parameterSet>
                                                 </cito:parameters>
                                             </cito:InlineElement><span id="SIc858ebcf-6f25-46ee-be0b-afbee1f41971" style="background-color: #C7B8CE;">bovenbouw</span><cito:InlineElement id="If6a1b756-b293-4baf-a3c0-3332221e3ea8" layoutTemplateSourceName="InlineHottextLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                 <cito:parameters>
                                                     <cito:parameterSet id="entireItem">
                                                         <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                         <cito:plaintextparameter name="controlId">If6a1b756-b293-4baf-a3c0-3332221e3ea8</cito:plaintextparameter>
                                                         <cito:plaintextparameter name="controlLabel">van</cito:plaintextparameter>
                                                         <cito:booleanparameter name="addHottextCorrection">False</cito:booleanparameter>
                                                         <cito:plaintextparameter name="hottextValue"/>
                                                     </cito:parameterSet>
                                                 </cito:parameters>
                                             </cito:InlineElement><span id="SIf6a1b756-b293-4baf-a3c0-3332221e3ea8" style="background-color: #C7B8CE;">van</span><cito:InlineElement id="I80cb35ef-89d7-4ada-aef4-eda2b963a606" layoutTemplateSourceName="InlineHottextLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                 <cito:parameters>
                                                     <cito:parameterSet id="entireItem">
                                                         <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                         <cito:plaintextparameter name="controlId">I80cb35ef-89d7-4ada-aef4-eda2b963a606</cito:plaintextparameter>
                                                         <cito:plaintextparameter name="controlLabel">de</cito:plaintextparameter>
                                                         <cito:booleanparameter name="addHottextCorrection">False</cito:booleanparameter>
                                                         <cito:plaintextparameter name="hottextValue"/>
                                                     </cito:parameterSet>
                                                 </cito:parameters>
                                             </cito:InlineElement><span id="SI80cb35ef-89d7-4ada-aef4-eda2b963a606" style="background-color: #C7B8CE;">de</span><cito:InlineElement id="Ia2c49323-f160-4dfa-8f79-5abea876efa6" layoutTemplateSourceName="InlineHottextLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                 <cito:parameters>
                                                     <cito:parameterSet id="entireItem">
                                                         <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                         <cito:plaintextparameter name="controlId">Ia2c49323-f160-4dfa-8f79-5abea876efa6</cito:plaintextparameter>
                                                         <cito:plaintextparameter name="controlLabel">vier</cito:plaintextparameter>
                                                         <cito:booleanparameter name="addHottextCorrection">False</cito:booleanparameter>
                                                         <cito:plaintextparameter name="hottextValue"/>
                                                     </cito:parameterSet>
                                                 </cito:parameters>
                                             </cito:InlineElement><span id="SIa2c49323-f160-4dfa-8f79-5abea876efa6" style="background-color: #C7B8CE;">vier</span><cito:InlineElement id="I8ecdc331-d565-4fcc-9f1f-a83e6e6bab9d" layoutTemplateSourceName="InlineHottextLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                 <cito:parameters>
                                                     <cito:parameterSet id="entireItem">
                                                         <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                         <cito:plaintextparameter name="controlId">I8ecdc331-d565-4fcc-9f1f-a83e6e6bab9d</cito:plaintextparameter>
                                                         <cito:plaintextparameter name="controlLabel">gaan</cito:plaintextparameter>
                                                         <cito:booleanparameter name="addHottextCorrection">False</cito:booleanparameter>
                                                         <cito:plaintextparameter name="hottextValue"/>
                                                     </cito:parameterSet>
                                                 </cito:parameters>
                                             </cito:InlineElement><span id="SI8ecdc331-d565-4fcc-9f1f-a83e6e6bab9d" style="background-color: #C7B8CE;">gaan</span><cito:InlineElement id="I4142aba3-f108-41dc-b537-ab44fd8cb5ae" layoutTemplateSourceName="InlineHottextLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                 <cito:parameters>
                                                     <cito:parameterSet id="entireItem">
                                                         <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                         <cito:plaintextparameter name="controlId">I4142aba3-f108-41dc-b537-ab44fd8cb5ae</cito:plaintextparameter>
                                                         <cito:plaintextparameter name="controlLabel">we</cito:plaintextparameter>
                                                         <cito:booleanparameter name="addHottextCorrection">False</cito:booleanparameter>
                                                         <cito:plaintextparameter name="hottextValue"/>
                                                     </cito:parameterSet>
                                                 </cito:parameters>
                                             </cito:InlineElement><span id="SI4142aba3-f108-41dc-b537-ab44fd8cb5ae" style="background-color: #C7B8CE;">we</span><cito:InlineElement id="If9dc68d4-33ec-4176-adb2-eda10373f1a0" layoutTemplateSourceName="InlineHottextLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                 <cito:parameters>
                                                     <cito:parameterSet id="entireItem">
                                                         <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                         <cito:plaintextparameter name="controlId">If9dc68d4-33ec-4176-adb2-eda10373f1a0</cito:plaintextparameter>
                                                         <cito:plaintextparameter name="controlLabel">daarom</cito:plaintextparameter>
                                                         <cito:booleanparameter name="addHottextCorrection">False</cito:booleanparameter>
                                                         <cito:plaintextparameter name="hottextValue"/>
                                                     </cito:parameterSet>
                                                 </cito:parameters>
                                             </cito:InlineElement><span id="SIf9dc68d4-33ec-4176-adb2-eda10373f1a0" style="background-color: #C7B8CE;">daarom</span><cito:InlineElement id="Ice8f6ba6-0b24-4234-8e1c-ea2d6caf18f6" layoutTemplateSourceName="InlineHottextLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                 <cito:parameters>
                                                     <cito:parameterSet id="entireItem">
                                                         <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                         <cito:plaintextparameter name="controlId">Ice8f6ba6-0b24-4234-8e1c-ea2d6caf18f6</cito:plaintextparameter>
                                                         <cito:plaintextparameter name="controlLabel">in</cito:plaintextparameter>
                                                         <cito:booleanparameter name="addHottextCorrection">False</cito:booleanparameter>
                                                         <cito:plaintextparameter name="hottextValue"/>
                                                     </cito:parameterSet>
                                                 </cito:parameters>
                                             </cito:InlineElement><span id="SIce8f6ba6-0b24-4234-8e1c-ea2d6caf18f6" style="background-color: #C7B8CE;">in</span><cito:InlineElement id="Ia64d8e75-aa61-481b-bd40-df7270053448" layoutTemplateSourceName="InlineHottextLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                 <cito:parameters>
                                                     <cito:parameterSet id="entireItem">
                                                         <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                         <cito:plaintextparameter name="controlId">Ia64d8e75-aa61-481b-bd40-df7270053448</cito:plaintextparameter>
                                                         <cito:plaintextparameter name="controlLabel">de</cito:plaintextparameter>
                                                         <cito:booleanparameter name="addHottextCorrection">False</cito:booleanparameter>
                                                         <cito:plaintextparameter name="hottextValue"/>
                                                     </cito:parameterSet>
                                                 </cito:parameters>
                                             </cito:InlineElement><span id="SIa64d8e75-aa61-481b-bd40-df7270053448" style="background-color: #C7B8CE;">de</span><cito:InlineElement id="I2a787ac6-adce-419f-b230-293302ab6e2b" layoutTemplateSourceName="InlineHottextLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                 <cito:parameters>
                                                     <cito:parameterSet id="entireItem">
                                                         <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                         <cito:plaintextparameter name="controlId">I2a787ac6-adce-419f-b230-293302ab6e2b</cito:plaintextparameter>
                                                         <cito:plaintextparameter name="controlLabel">vijf</cito:plaintextparameter>
                                                         <cito:booleanparameter name="addHottextCorrection">False</cito:booleanparameter>
                                                         <cito:plaintextparameter name="hottextValue"/>
                                                     </cito:parameterSet>
                                                 </cito:parameters>
                                             </cito:InlineElement><span id="SI2a787ac6-adce-419f-b230-293302ab6e2b" style="background-color: #C7B8CE;">vijf</span><cito:InlineElement id="I562dc41e-1fcd-45b2-ad63-e346a9b12ed3" layoutTemplateSourceName="InlineHottextLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                 <cito:parameters>
                                                     <cito:parameterSet id="entireItem">
                                                         <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                         <cito:plaintextparameter name="controlId">I562dc41e-1fcd-45b2-ad63-e346a9b12ed3</cito:plaintextparameter>
                                                         <cito:plaintextparameter name="controlLabel">een</cito:plaintextparameter>
                                                         <cito:booleanparameter name="addHottextCorrection">False</cito:booleanparameter>
                                                         <cito:plaintextparameter name="hottextValue"/>
                                                     </cito:parameterSet>
                                                 </cito:parameters>
                                             </cito:InlineElement><span id="SI562dc41e-1fcd-45b2-ad63-e346a9b12ed3" style="background-color: #C7B8CE;">een</span><cito:InlineElement id="I2ff56de9-53fd-4be6-adc3-b9d48cbac0bd" layoutTemplateSourceName="InlineHottextLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                 <cito:parameters>
                                                     <cito:parameterSet id="entireItem">
                                                         <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                         <cito:plaintextparameter name="controlId">I2ff56de9-53fd-4be6-adc3-b9d48cbac0bd</cito:plaintextparameter>
                                                         <cito:plaintextparameter name="controlLabel">weekje</cito:plaintextparameter>
                                                         <cito:booleanparameter name="addHottextCorrection">False</cito:booleanparameter>
                                                         <cito:plaintextparameter name="hottextValue"/>
                                                     </cito:parameterSet>
                                                 </cito:parameters>
                                             </cito:InlineElement><span id="SI2ff56de9-53fd-4be6-adc3-b9d48cbac0bd" style="background-color: #C7B8CE;">weekje</span><cito:InlineElement id="I8c49dcaf-eea6-48ad-bcf0-5c00ccde0415" layoutTemplateSourceName="InlineHottextLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                 <cito:parameters>
                                                     <cito:parameterSet id="entireItem">
                                                         <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                         <cito:plaintextparameter name="controlId">I8c49dcaf-eea6-48ad-bcf0-5c00ccde0415</cito:plaintextparameter>
                                                         <cito:plaintextparameter name="controlLabel">naar</cito:plaintextparameter>
                                                         <cito:booleanparameter name="addHottextCorrection">False</cito:booleanparameter>
                                                         <cito:plaintextparameter name="hottextValue"/>
                                                     </cito:parameterSet>
                                                 </cito:parameters>
                                             </cito:InlineElement><span id="SI8c49dcaf-eea6-48ad-bcf0-5c00ccde0415" style="background-color: #C7B8CE;">naar</span><cito:InlineElement id="I7b431d95-a30e-403f-9b35-7575b30d31a3" layoutTemplateSourceName="InlineHottextLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                 <cito:parameters>
                                                     <cito:parameterSet id="entireItem">
                                                         <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                         <cito:plaintextparameter name="controlId">I7b431d95-a30e-403f-9b35-7575b30d31a3</cito:plaintextparameter>
                                                         <cito:plaintextparameter name="controlLabel">het</cito:plaintextparameter>
                                                         <cito:booleanparameter name="addHottextCorrection">False</cito:booleanparameter>
                                                         <cito:plaintextparameter name="hottextValue"/>
                                                     </cito:parameterSet>
                                                 </cito:parameters>
                                             </cito:InlineElement><span id="SI7b431d95-a30e-403f-9b35-7575b30d31a3" style="background-color: #C7B8CE;">het</span><cito:InlineElement id="I46f0651d-a9f2-4b96-8172-8104b0938213" layoutTemplateSourceName="InlineHottextLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                 <cito:parameters>
                                                     <cito:parameterSet id="entireItem">
                                                         <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                         <cito:plaintextparameter name="controlId">I46f0651d-a9f2-4b96-8172-8104b0938213</cito:plaintextparameter>
                                                         <cito:plaintextparameter name="controlLabel">zes</cito:plaintextparameter>
                                                         <cito:booleanparameter name="addHottextCorrection">False</cito:booleanparameter>
                                                         <cito:plaintextparameter name="hottextValue"/>
                                                     </cito:parameterSet>
                                                 </cito:parameters>
                                             </cito:InlineElement><span id="SI46f0651d-a9f2-4b96-8172-8104b0938213" style="background-color: #C7B8CE;">zes</span>. <cito:InlineElement id="I1e40e819-c8a2-42dd-89de-dd29e070b99b" layoutTemplateSourceName="InlineHottextLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                 <cito:parameters>
                                                     <cito:parameterSet id="entireItem">
                                                         <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                         <cito:plaintextparameter name="controlId">I1e40e819-c8a2-42dd-89de-dd29e070b99b</cito:plaintextparameter>
                                                         <cito:plaintextparameter name="controlLabel">Waarschijnlijk</cito:plaintextparameter>
                                                         <cito:booleanparameter name="addHottextCorrection">False</cito:booleanparameter>
                                                         <cito:plaintextparameter name="hottextValue"/>
                                                     </cito:parameterSet>
                                                 </cito:parameters>
                                             </cito:InlineElement><span id="SI1e40e819-c8a2-42dd-89de-dd29e070b99b" style="background-color: #C7B8CE;">Waarschijnlijk</span><cito:InlineElement id="I67b6042e-7444-46be-b169-53ec421f9eda" layoutTemplateSourceName="InlineHottextLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                 <cito:parameters>
                                                     <cito:parameterSet id="entireItem">
                                                         <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                         <cito:plaintextparameter name="controlId">I67b6042e-7444-46be-b169-53ec421f9eda</cito:plaintextparameter>
                                                         <cito:plaintextparameter name="controlLabel">gaan</cito:plaintextparameter>
                                                         <cito:booleanparameter name="addHottextCorrection">False</cito:booleanparameter>
                                                         <cito:plaintextparameter name="hottextValue"/>
                                                     </cito:parameterSet>
                                                 </cito:parameters>
                                             </cito:InlineElement><span id="SI67b6042e-7444-46be-b169-53ec421f9eda" style="background-color: #C7B8CE;">gaan</span><cito:InlineElement id="I18248834-8320-4342-967c-2007195ba14a" layoutTemplateSourceName="InlineHottextLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                 <cito:parameters>
                                                     <cito:parameterSet id="entireItem">
                                                         <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                         <cito:plaintextparameter name="controlId">I18248834-8320-4342-967c-2007195ba14a</cito:plaintextparameter>
                                                         <cito:plaintextparameter name="controlLabel">de</cito:plaintextparameter>
                                                         <cito:booleanparameter name="addHottextCorrection">False</cito:booleanparameter>
                                                         <cito:plaintextparameter name="hottextValue"/>
                                                     </cito:parameterSet>
                                                 </cito:parameters>
                                             </cito:InlineElement><span id="SI18248834-8320-4342-967c-2007195ba14a" style="background-color: #C7B8CE;">de</span><cito:InlineElement id="I0bf4350c-9288-4cec-9e37-1b0978b057ba" layoutTemplateSourceName="InlineHottextLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                 <cito:parameters>
                                                     <cito:parameterSet id="entireItem">
                                                         <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                         <cito:plaintextparameter name="controlId">I0bf4350c-9288-4cec-9e37-1b0978b057ba</cito:plaintextparameter>
                                                         <cito:plaintextparameter name="controlLabel">Heer</cito:plaintextparameter>
                                                         <cito:booleanparameter name="addHottextCorrection">False</cito:booleanparameter>
                                                         <cito:plaintextparameter name="hottextValue"/>
                                                     </cito:parameterSet>
                                                 </cito:parameters>
                                             </cito:InlineElement><span id="SI0bf4350c-9288-4cec-9e37-1b0978b057ba" style="background-color: #C7B8CE;">Heer</span><cito:InlineElement id="Ifb1bfeea-20a0-4d29-bb87-d502ff355522" layoutTemplateSourceName="InlineHottextLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                 <cito:parameters>
                                                     <cito:parameterSet id="entireItem">
                                                         <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                         <cito:plaintextparameter name="controlId">Ifb1bfeea-20a0-4d29-bb87-d502ff355522</cito:plaintextparameter>
                                                         <cito:plaintextparameter name="controlLabel">acht</cito:plaintextparameter>
                                                         <cito:booleanparameter name="addHottextCorrection">False</cito:booleanparameter>
                                                         <cito:plaintextparameter name="hottextValue"/>
                                                     </cito:parameterSet>
                                                 </cito:parameters>
                                             </cito:InlineElement><span id="SIfb1bfeea-20a0-4d29-bb87-d502ff355522" style="background-color: #C7B8CE;">acht</span><cito:InlineElement id="Ie1f531cd-79c9-4b87-a90c-76766792b183" layoutTemplateSourceName="InlineHottextLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                 <cito:parameters>
                                                     <cito:parameterSet id="entireItem">
                                                         <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                         <cito:plaintextparameter name="controlId">Ie1f531cd-79c9-4b87-a90c-76766792b183</cito:plaintextparameter>
                                                         <cito:plaintextparameter name="controlLabel">en</cito:plaintextparameter>
                                                         <cito:booleanparameter name="addHottextCorrection">False</cito:booleanparameter>
                                                         <cito:plaintextparameter name="hottextValue"/>
                                                     </cito:parameterSet>
                                                 </cito:parameters>
                                             </cito:InlineElement><span id="SIe1f531cd-79c9-4b87-a90c-76766792b183" style="background-color: #C7B8CE;">en</span><cito:InlineElement id="I66faad9a-d004-4cdc-a275-6f9ff736bb14" layoutTemplateSourceName="InlineHottextLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                 <cito:parameters>
                                                     <cito:parameterSet id="entireItem">
                                                         <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                         <cito:plaintextparameter name="controlId">I66faad9a-d004-4cdc-a275-6f9ff736bb14</cito:plaintextparameter>
                                                         <cito:plaintextparameter name="controlLabel">negen</cito:plaintextparameter>
                                                         <cito:booleanparameter name="addHottextCorrection">False</cito:booleanparameter>
                                                         <cito:plaintextparameter name="hottextValue"/>
                                                     </cito:parameterSet>
                                                 </cito:parameters>
                                             </cito:InlineElement><span id="SI66faad9a-d004-4cdc-a275-6f9ff736bb14" style="background-color: #C7B8CE;">negen</span><cito:InlineElement id="I665f2702-e926-4266-bc3c-3f04ea9a6c62" layoutTemplateSourceName="InlineHottextLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                 <cito:parameters>
                                                     <cito:parameterSet id="entireItem">
                                                         <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                         <cito:plaintextparameter name="controlId">I665f2702-e926-4266-bc3c-3f04ea9a6c62</cito:plaintextparameter>
                                                         <cito:plaintextparameter name="controlLabel">tien</cito:plaintextparameter>
                                                         <cito:booleanparameter name="addHottextCorrection">False</cito:booleanparameter>
                                                         <cito:plaintextparameter name="hottextValue"/>
                                                     </cito:parameterSet>
                                                 </cito:parameters>
                                             </cito:InlineElement><span id="SI665f2702-e926-4266-bc3c-3f04ea9a6c62" style="background-color: #C7B8CE;">tien</span> - <cito:InlineElement id="I7e8b004a-0223-432a-bb9a-536918190017" layoutTemplateSourceName="InlineHottextLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                 <cito:parameters>
                                                     <cito:parameterSet id="entireItem">
                                                         <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                         <cito:plaintextparameter name="controlId">I7e8b004a-0223-432a-bb9a-536918190017</cito:plaintextparameter>
                                                         <cito:plaintextparameter name="controlLabel">van</cito:plaintextparameter>
                                                         <cito:booleanparameter name="addHottextCorrection">False</cito:booleanparameter>
                                                         <cito:plaintextparameter name="hottextValue"/>
                                                     </cito:parameterSet>
                                                 </cito:parameters>
                                             </cito:InlineElement><span id="SI7e8b004a-0223-432a-bb9a-536918190017" style="background-color: #C7B8CE;">van</span><cito:InlineElement id="I217873b0-4970-405f-a80b-b2a9f42bbd85" layoutTemplateSourceName="InlineHottextLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                 <cito:parameters>
                                                     <cito:parameterSet id="entireItem">
                                                         <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                         <cito:plaintextparameter name="controlId">I217873b0-4970-405f-a80b-b2a9f42bbd85</cito:plaintextparameter>
                                                         <cito:plaintextparameter name="controlLabel">der</cito:plaintextparameter>
                                                         <cito:booleanparameter name="addHottextCorrection">False</cito:booleanparameter>
                                                         <cito:plaintextparameter name="hottextValue"/>
                                                     </cito:parameterSet>
                                                 </cito:parameters>
                                             </cito:InlineElement><span id="SI217873b0-4970-405f-a80b-b2a9f42bbd85" style="background-color: #C7B8CE;">der</span><cito:InlineElement id="I46521597-f1f4-432c-96c2-8e3defb05950" layoutTemplateSourceName="InlineHottextLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                 <cito:parameters>
                                                     <cito:parameterSet id="entireItem">
                                                         <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                         <cito:plaintextparameter name="controlId">I46521597-f1f4-432c-96c2-8e3defb05950</cito:plaintextparameter>
                                                         <cito:plaintextparameter name="controlLabel">wielen</cito:plaintextparameter>
                                                         <cito:booleanparameter name="addHottextCorrection">False</cito:booleanparameter>
                                                         <cito:plaintextparameter name="hottextValue"/>
                                                     </cito:parameterSet>
                                                 </cito:parameters>
                                             </cito:InlineElement><span id="SI46521597-f1f4-432c-96c2-8e3defb05950" style="background-color: #C7B8CE;">wielen</span><cito:InlineElement id="Ifbea86b5-20bc-4dde-a4e4-4a14153c8ead" layoutTemplateSourceName="InlineHottextLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                 <cito:parameters>
                                                     <cito:parameterSet id="entireItem">
                                                         <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                         <cito:plaintextparameter name="controlId">Ifbea86b5-20bc-4dde-a4e4-4a14153c8ead</cito:plaintextparameter>
                                                         <cito:plaintextparameter name="controlLabel">mee</cito:plaintextparameter>
                                                         <cito:booleanparameter name="addHottextCorrection">False</cito:booleanparameter>
                                                         <cito:plaintextparameter name="hottextValue"/>
                                                     </cito:parameterSet>
                                                 </cito:parameters>
                                             </cito:InlineElement><span id="SIfbea86b5-20bc-4dde-a4e4-4a14153c8ead" style="background-color: #C7B8CE;">mee</span><cito:InlineElement id="I279d3c47-95a8-42d1-9440-26fadf909090" layoutTemplateSourceName="InlineHottextLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                 <cito:parameters>
                                                     <cito:parameterSet id="entireItem">
                                                         <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                         <cito:plaintextparameter name="controlId">I279d3c47-95a8-42d1-9440-26fadf909090</cito:plaintextparameter>
                                                         <cito:plaintextparameter name="controlLabel">als</cito:plaintextparameter>
                                                         <cito:booleanparameter name="addHottextCorrection">False</cito:booleanparameter>
                                                         <cito:plaintextparameter name="hottextValue"/>
                                                     </cito:parameterSet>
                                                 </cito:parameters>
                                             </cito:InlineElement><span id="SI279d3c47-95a8-42d1-9440-26fadf909090" style="background-color: #C7B8CE;">als</span><cito:InlineElement id="I6d80a45f-5042-4f42-99fe-151229268a43" layoutTemplateSourceName="InlineHottextLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                 <cito:parameters>
                                                     <cito:parameterSet id="entireItem">
                                                         <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                         <cito:plaintextparameter name="controlId">I6d80a45f-5042-4f42-99fe-151229268a43</cito:plaintextparameter>
                                                         <cito:plaintextparameter name="controlLabel">begeleider</cito:plaintextparameter>
                                                         <cito:booleanparameter name="addHottextCorrection">False</cito:booleanparameter>
                                                         <cito:plaintextparameter name="hottextValue"/>
                                                     </cito:parameterSet>
                                                 </cito:parameters>
                                             </cito:InlineElement><span id="SI6d80a45f-5042-4f42-99fe-151229268a43" style="background-color: #C7B8CE;">begeleider</span>.</p>
                                     </xhtmlparameter>

        Dim xhtmlPrm As New XHtmlParameter() With {.Name = "hottextInput", .Value = xhtmlValue.ToString}
        scoreParam.HotTextText = xhtmlPrm

        scoreParams.Add(scoreParam)
        Return scoreParams
    End Function

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
                                   <hottextInteraction responseIdentifier="hottextController" maxChoices="25">
                                       <p id="c1-id-11">
                                           <span>
                                               <hottext identifier="Iba0772fe-878f-457c-b652-a0e922626082"/>
                                           </span>
                                           <span id="SIba0772fe-878f-457c-b652-a0e922626082" style="background-color: #C7B8CE;">Op</span>
                                           <span>
                                               <hottext identifier="I5493e55f-fae9-499f-bd3e-16df52c028e1"/>
                                           </span>
                                           <span id="SI5493e55f-fae9-499f-bd3e-16df52c028e1" style="background-color: #C7B8CE;">vrijwel</span>
                                           <span>
                                               <hottext identifier="I7134c778-4641-40b5-8eae-1a80013cf777"/>
                                           </span>
                                           <span id="SI7134c778-4641-40b5-8eae-1a80013cf777" style="background-color: #C7B8CE;">alle</span>
                                           <span>
                                               <hottext identifier="I3cb9edb6-551e-474a-9122-338b8c6a396d"/>
                                           </span>
                                           <span id="SI3cb9edb6-551e-474a-9122-338b8c6a396d" style="background-color: #C7B8CE;">een</span>
                                           <span>
                                               <hottext identifier="I41d5e633-d892-46fe-8a0c-ceba54c167f3"/>
                                           </span>
                                           <span id="SI41d5e633-d892-46fe-8a0c-ceba54c167f3" style="background-color: #C7B8CE;">meren</span>
                                           <span>
                                               <hottext identifier="I498a96cd-114d-4299-b27c-639e41559e29"/>
                                           </span>
                                           <span id="SI498a96cd-114d-4299-b27c-639e41559e29" style="background-color: #C7B8CE;">is</span>
                                           <span>
                                               <hottext identifier="I19b64b54-f7f4-4284-af37-37999bd24935"/>
                                           </span>
                                           <span id="SI19b64b54-f7f4-4284-af37-37999bd24935" style="background-color: #C7B8CE;">in</span>
                                           <span>
                                               <hottext identifier="I7ea195a7-b0ec-465e-ada3-31c9e940f0dc"/>
                                           </span>
                                           <span id="SI7ea195a7-b0ec-465e-ada3-31c9e940f0dc" style="background-color: #C7B8CE;">de</span>
                                           <span>
                                               <hottext identifier="Ibf683ce7-ac85-4610-b057-dd0578f3b71f"/>
                                           </span>
                                           <span id="SIbf683ce7-ac85-4610-b057-dd0578f3b71f" style="background-color: #C7B8CE;">zomer</span>
                                           <span>
                                               <hottext identifier="Ie6f1fe1f-fe23-4f29-85ec-27d483138cfa"/>
                                           </span>
                                           <span id="SIe6f1fe1f-fe23-4f29-85ec-27d483138cfa" style="background-color: #C7B8CE;">veel</span>
                                           <span>
                                               <hottext identifier="I2b4ed71d-8984-4ffd-a95a-126efbb12e48"/>
                                           </span>
                                           <span id="SI2b4ed71d-8984-4ffd-a95a-126efbb12e48" style="background-color: #C7B8CE;">te</span>
                                           <span>
                                               <hottext identifier="I0778d3a8-11c3-41f5-aeb7-4efd94338296"/>
                                           </span>
                                           <span id="SI0778d3a8-11c3-41f5-aeb7-4efd94338296" style="background-color: #C7B8CE;">doen</span>: <span>
                                               <hottext identifier="I3a1bf3a7-f9b4-472d-ac19-c6d27f671543"/>
                                           </span>
                                           <span id="SI3a1bf3a7-f9b4-472d-ac19-c6d27f671543" style="background-color: #C7B8CE;">ze</span>
                                           <span>
                                               <hottext identifier="I01f17f48-6da9-44a0-bb7f-82e325f2f233"/>
                                           </span>
                                           <span id="SI01f17f48-6da9-44a0-bb7f-82e325f2f233" style="background-color: #C7B8CE;">zijn</span>
                                           <span>
                                               <hottext identifier="Ifdbe8e9c-0eeb-4019-b8a7-5599265834fb"/>
                                           </span>
                                           <span id="SIfdbe8e9c-0eeb-4019-b8a7-5599265834fb" style="background-color: #C7B8CE;">twee</span>
                                           <span>
                                               <hottext identifier="I0dd2d1e9-84b7-4b3f-a144-a2ec2c13061b"/>
                                           </span>
                                           <span id="SI0dd2d1e9-84b7-4b3f-a144-a2ec2c13061b" style="background-color: #C7B8CE;">geschikt</span>
                                           <span>
                                               <hottext identifier="I41e6c74d-787a-4620-889f-fae84be71cdb"/>
                                           </span>
                                           <span id="SI41e6c74d-787a-4620-889f-fae84be71cdb" style="background-color: #C7B8CE;">voor</span>
                                           <span>
                                               <hottext identifier="I1f639c54-57ee-428e-93ed-77d786d5a2d1"/>
                                           </span>
                                           <span id="SI1f639c54-57ee-428e-93ed-77d786d5a2d1" style="background-color: #C7B8CE;">drie</span>. <span>
                                               <hottext identifier="Ib580dda7-0717-4cfc-b70e-34059b4c78f1"/>
                                           </span>
                                           <span id="SIb580dda7-0717-4cfc-b70e-34059b4c78f1" style="background-color: #C7B8CE;">Met</span>
                                           <span>
                                               <hottext identifier="I834d38a6-dac5-4235-9cf4-d323dd89f1d2"/>
                                           </span>
                                           <span id="SI834d38a6-dac5-4235-9cf4-d323dd89f1d2" style="background-color: #C7B8CE;">de</span>
                                           <span>
                                               <hottext identifier="Ic858ebcf-6f25-46ee-be0b-afbee1f41971"/>
                                           </span>
                                           <span id="SIc858ebcf-6f25-46ee-be0b-afbee1f41971" style="background-color: #C7B8CE;">bovenbouw</span>
                                           <span>
                                               <hottext identifier="If6a1b756-b293-4baf-a3c0-3332221e3ea8"/>
                                           </span>
                                           <span id="SIf6a1b756-b293-4baf-a3c0-3332221e3ea8" style="background-color: #C7B8CE;">van</span>
                                           <span>
                                               <hottext identifier="I80cb35ef-89d7-4ada-aef4-eda2b963a606"/>
                                           </span>
                                           <span id="SI80cb35ef-89d7-4ada-aef4-eda2b963a606" style="background-color: #C7B8CE;">de</span>
                                           <span>
                                               <hottext identifier="Ia2c49323-f160-4dfa-8f79-5abea876efa6"/>
                                           </span>
                                           <span id="SIa2c49323-f160-4dfa-8f79-5abea876efa6" style="background-color: #C7B8CE;">vier</span>
                                           <span>
                                               <hottext identifier="I8ecdc331-d565-4fcc-9f1f-a83e6e6bab9d"/>
                                           </span>
                                           <span id="SI8ecdc331-d565-4fcc-9f1f-a83e6e6bab9d" style="background-color: #C7B8CE;">gaan</span>
                                           <span>
                                               <hottext identifier="I4142aba3-f108-41dc-b537-ab44fd8cb5ae"/>
                                           </span>
                                           <span id="SI4142aba3-f108-41dc-b537-ab44fd8cb5ae" style="background-color: #C7B8CE;">we</span>
                                           <span>
                                               <hottext identifier="If9dc68d4-33ec-4176-adb2-eda10373f1a0"/>
                                           </span>
                                           <span id="SIf9dc68d4-33ec-4176-adb2-eda10373f1a0" style="background-color: #C7B8CE;">daarom</span>
                                           <span>
                                               <hottext identifier="Ice8f6ba6-0b24-4234-8e1c-ea2d6caf18f6"/>
                                           </span>
                                           <span id="SIce8f6ba6-0b24-4234-8e1c-ea2d6caf18f6" style="background-color: #C7B8CE;">in</span>
                                           <span>
                                               <hottext identifier="Ia64d8e75-aa61-481b-bd40-df7270053448"/>
                                           </span>
                                           <span id="SIa64d8e75-aa61-481b-bd40-df7270053448" style="background-color: #C7B8CE;">de</span>
                                           <span>
                                               <hottext identifier="I2a787ac6-adce-419f-b230-293302ab6e2b"/>
                                           </span>
                                           <span id="SI2a787ac6-adce-419f-b230-293302ab6e2b" style="background-color: #C7B8CE;">vijf</span>
                                           <span>
                                               <hottext identifier="I562dc41e-1fcd-45b2-ad63-e346a9b12ed3"/>
                                           </span>
                                           <span id="SI562dc41e-1fcd-45b2-ad63-e346a9b12ed3" style="background-color: #C7B8CE;">een</span>
                                           <span>
                                               <hottext identifier="I2ff56de9-53fd-4be6-adc3-b9d48cbac0bd"/>
                                           </span>
                                           <span id="SI2ff56de9-53fd-4be6-adc3-b9d48cbac0bd" style="background-color: #C7B8CE;">weekje</span>
                                           <span>
                                               <hottext identifier="I8c49dcaf-eea6-48ad-bcf0-5c00ccde0415"/>
                                           </span>
                                           <span id="SI8c49dcaf-eea6-48ad-bcf0-5c00ccde0415" style="background-color: #C7B8CE;">naar</span>
                                           <span>
                                               <hottext identifier="I7b431d95-a30e-403f-9b35-7575b30d31a3"/>
                                           </span>
                                           <span id="SI7b431d95-a30e-403f-9b35-7575b30d31a3" style="background-color: #C7B8CE;">het</span>
                                           <span>
                                               <hottext identifier="I46f0651d-a9f2-4b96-8172-8104b0938213"/>
                                           </span>
                                           <span id="SI46f0651d-a9f2-4b96-8172-8104b0938213" style="background-color: #C7B8CE;">zes</span>. <span>
                                               <hottext identifier="I1e40e819-c8a2-42dd-89de-dd29e070b99b"/>
                                           </span>
                                           <span id="SI1e40e819-c8a2-42dd-89de-dd29e070b99b" style="background-color: #C7B8CE;">Waarschijnlijk</span>
                                           <span>
                                               <hottext identifier="I67b6042e-7444-46be-b169-53ec421f9eda"/>
                                           </span>
                                           <span id="SI67b6042e-7444-46be-b169-53ec421f9eda" style="background-color: #C7B8CE;">gaan</span>
                                           <span>
                                               <hottext identifier="I18248834-8320-4342-967c-2007195ba14a"/>
                                           </span>
                                           <span id="SI18248834-8320-4342-967c-2007195ba14a" style="background-color: #C7B8CE;">de</span>
                                           <span>
                                               <hottext identifier="I0bf4350c-9288-4cec-9e37-1b0978b057ba"/>
                                           </span>
                                           <span id="SI0bf4350c-9288-4cec-9e37-1b0978b057ba" style="background-color: #C7B8CE;">Heer</span>
                                           <span>
                                               <hottext identifier="Ifb1bfeea-20a0-4d29-bb87-d502ff355522"/>
                                           </span>
                                           <span id="SIfb1bfeea-20a0-4d29-bb87-d502ff355522" style="background-color: #C7B8CE;">acht</span>
                                           <span>
                                               <hottext identifier="Ie1f531cd-79c9-4b87-a90c-76766792b183"/>
                                           </span>
                                           <span id="SIe1f531cd-79c9-4b87-a90c-76766792b183" style="background-color: #C7B8CE;">en</span>
                                           <span>
                                               <hottext identifier="I66faad9a-d004-4cdc-a275-6f9ff736bb14"/>
                                           </span>
                                           <span id="SI66faad9a-d004-4cdc-a275-6f9ff736bb14" style="background-color: #C7B8CE;">negen</span>
                                           <span>
                                               <hottext identifier="I665f2702-e926-4266-bc3c-3f04ea9a6c62"/>
                                           </span>
                                           <span id="SI665f2702-e926-4266-bc3c-3f04ea9a6c62" style="background-color: #C7B8CE;">tien</span> - <span>
                                               <hottext identifier="I7e8b004a-0223-432a-bb9a-536918190017"/>
                                           </span>
                                           <span id="SI7e8b004a-0223-432a-bb9a-536918190017" style="background-color: #C7B8CE;">van</span>
                                           <span>
                                               <hottext identifier="I217873b0-4970-405f-a80b-b2a9f42bbd85"/>
                                           </span>
                                           <span id="SI217873b0-4970-405f-a80b-b2a9f42bbd85" style="background-color: #C7B8CE;">der</span>
                                           <span>
                                               <hottext identifier="I46521597-f1f4-432c-96c2-8e3defb05950"/>
                                           </span>
                                           <span id="SI46521597-f1f4-432c-96c2-8e3defb05950" style="background-color: #C7B8CE;">wielen</span>
                                           <span>
                                               <hottext identifier="Ifbea86b5-20bc-4dde-a4e4-4a14153c8ead"/>
                                           </span>
                                           <span id="SIfbea86b5-20bc-4dde-a4e4-4a14153c8ead" style="background-color: #C7B8CE;">mee</span>
                                           <span>
                                               <hottext identifier="I279d3c47-95a8-42d1-9440-26fadf909090"/>
                                           </span>
                                           <span id="SI279d3c47-95a8-42d1-9440-26fadf909090" style="background-color: #C7B8CE;">als</span>
                                           <span>
                                               <hottext identifier="I6d80a45f-5042-4f42-99fe-151229268a43"/>
                                           </span>
                                           <span id="SI6d80a45f-5042-4f42-99fe-151229268a43" style="background-color: #C7B8CE;">begeleider</span>.</p>
                                   </hottextInteraction>
                               </div>
                           </div>
                       </div>
                       <div class="div_right">
                           <!-- div_right_inner is needed to create the 50px margin between the left and right columns in dual column mode -->
                           <div class="div_right_inner">
                               <div id="body">
                                   <p id="c1-id-11">Welkom.</p>
                               </div>
                               <div id="question">
                                   <p id="c1-id-11" class="UserSRVet">Klik ergens.</p>
                               </div>
                               <div id="itemgeneral">
                                   <br/>
                                   <p id="c1-id-11"> </p>
                               </div>
                           </div>
                       </div>
                   </div>
               </div>
           </itemBody>
       </wrapper>

    Private _finding1 As XElement =
        <keyFinding id="hottextController" scoringMethod="Polytomous">
            <keyFact id="Iba0772fe-878f-457c-b652-a0e922626082-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="Iba0772fe-878f-457c-b652-a0e922626082-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I41e6c74d-787a-4620-889f-fae84be71cdb-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I41e6c74d-787a-4620-889f-fae84be71cdb-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I0dd2d1e9-84b7-4b3f-a144-a2ec2c13061b-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I0dd2d1e9-84b7-4b3f-a144-a2ec2c13061b-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="Ifdbe8e9c-0eeb-4019-b8a7-5599265834fb-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="Ifdbe8e9c-0eeb-4019-b8a7-5599265834fb-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I01f17f48-6da9-44a0-bb7f-82e325f2f233-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I01f17f48-6da9-44a0-bb7f-82e325f2f233-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I3a1bf3a7-f9b4-472d-ac19-c6d27f671543-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I3a1bf3a7-f9b4-472d-ac19-c6d27f671543-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I0778d3a8-11c3-41f5-aeb7-4efd94338296-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I0778d3a8-11c3-41f5-aeb7-4efd94338296-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I2b4ed71d-8984-4ffd-a95a-126efbb12e48-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I2b4ed71d-8984-4ffd-a95a-126efbb12e48-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="Ie6f1fe1f-fe23-4f29-85ec-27d483138cfa-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="Ie6f1fe1f-fe23-4f29-85ec-27d483138cfa-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="Ibf683ce7-ac85-4610-b057-dd0578f3b71f-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="Ibf683ce7-ac85-4610-b057-dd0578f3b71f-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I7ea195a7-b0ec-465e-ada3-31c9e940f0dc-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I7ea195a7-b0ec-465e-ada3-31c9e940f0dc-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I19b64b54-f7f4-4284-af37-37999bd24935-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I19b64b54-f7f4-4284-af37-37999bd24935-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I498a96cd-114d-4299-b27c-639e41559e29-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I498a96cd-114d-4299-b27c-639e41559e29-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I41d5e633-d892-46fe-8a0c-ceba54c167f3-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I41d5e633-d892-46fe-8a0c-ceba54c167f3-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I3cb9edb6-551e-474a-9122-338b8c6a396d-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I3cb9edb6-551e-474a-9122-338b8c6a396d-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>true</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I7134c778-4641-40b5-8eae-1a80013cf777-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I7134c778-4641-40b5-8eae-1a80013cf777-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I5493e55f-fae9-499f-bd3e-16df52c028e1-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I5493e55f-fae9-499f-bd3e-16df52c028e1-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I279d3c47-95a8-42d1-9440-26fadf909090-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I279d3c47-95a8-42d1-9440-26fadf909090-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="Ifbea86b5-20bc-4dde-a4e4-4a14153c8ead-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="Ifbea86b5-20bc-4dde-a4e4-4a14153c8ead-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I46521597-f1f4-432c-96c2-8e3defb05950-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I46521597-f1f4-432c-96c2-8e3defb05950-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>true</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I217873b0-4970-405f-a80b-b2a9f42bbd85-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I217873b0-4970-405f-a80b-b2a9f42bbd85-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I7e8b004a-0223-432a-bb9a-536918190017-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I7e8b004a-0223-432a-bb9a-536918190017-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I665f2702-e926-4266-bc3c-3f04ea9a6c62-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I665f2702-e926-4266-bc3c-3f04ea9a6c62-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>true</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I66faad9a-d004-4cdc-a275-6f9ff736bb14-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I66faad9a-d004-4cdc-a275-6f9ff736bb14-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="Ie1f531cd-79c9-4b87-a90c-76766792b183-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="Ie1f531cd-79c9-4b87-a90c-76766792b183-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="Ifb1bfeea-20a0-4d29-bb87-d502ff355522-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="Ifb1bfeea-20a0-4d29-bb87-d502ff355522-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I0bf4350c-9288-4cec-9e37-1b0978b057ba-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I0bf4350c-9288-4cec-9e37-1b0978b057ba-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>true</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I18248834-8320-4342-967c-2007195ba14a-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I18248834-8320-4342-967c-2007195ba14a-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I67b6042e-7444-46be-b169-53ec421f9eda-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I67b6042e-7444-46be-b169-53ec421f9eda-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I1e40e819-c8a2-42dd-89de-dd29e070b99b-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I1e40e819-c8a2-42dd-89de-dd29e070b99b-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I1f639c54-57ee-428e-93ed-77d786d5a2d1-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I1f639c54-57ee-428e-93ed-77d786d5a2d1-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>true</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I6d80a45f-5042-4f42-99fe-151229268a43-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I6d80a45f-5042-4f42-99fe-151229268a43-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFactSet>
                <keyFact id="Ib580dda7-0717-4cfc-b70e-34059b4c78f1-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ib580dda7-0717-4cfc-b70e-34059b4c78f1-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I834d38a6-dac5-4235-9cf4-d323dd89f1d2-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I834d38a6-dac5-4235-9cf4-d323dd89f1d2-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ic858ebcf-6f25-46ee-be0b-afbee1f41971-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ic858ebcf-6f25-46ee-be0b-afbee1f41971-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="If6a1b756-b293-4baf-a3c0-3332221e3ea8-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="If6a1b756-b293-4baf-a3c0-3332221e3ea8-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I80cb35ef-89d7-4ada-aef4-eda2b963a606-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I80cb35ef-89d7-4ada-aef4-eda2b963a606-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ia2c49323-f160-4dfa-8f79-5abea876efa6-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ia2c49323-f160-4dfa-8f79-5abea876efa6-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I8ecdc331-d565-4fcc-9f1f-a83e6e6bab9d-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I8ecdc331-d565-4fcc-9f1f-a83e6e6bab9d-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I4142aba3-f108-41dc-b537-ab44fd8cb5ae-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I4142aba3-f108-41dc-b537-ab44fd8cb5ae-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="If9dc68d4-33ec-4176-adb2-eda10373f1a0-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="If9dc68d4-33ec-4176-adb2-eda10373f1a0-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ice8f6ba6-0b24-4234-8e1c-ea2d6caf18f6-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ice8f6ba6-0b24-4234-8e1c-ea2d6caf18f6-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ia64d8e75-aa61-481b-bd40-df7270053448-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ia64d8e75-aa61-481b-bd40-df7270053448-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I2a787ac6-adce-419f-b230-293302ab6e2b-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I2a787ac6-adce-419f-b230-293302ab6e2b-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I562dc41e-1fcd-45b2-ad63-e346a9b12ed3-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I562dc41e-1fcd-45b2-ad63-e346a9b12ed3-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I2ff56de9-53fd-4be6-adc3-b9d48cbac0bd-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I2ff56de9-53fd-4be6-adc3-b9d48cbac0bd-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I8c49dcaf-eea6-48ad-bcf0-5c00ccde0415-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I8c49dcaf-eea6-48ad-bcf0-5c00ccde0415-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I7b431d95-a30e-403f-9b35-7575b30d31a3-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I7b431d95-a30e-403f-9b35-7575b30d31a3-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I46f0651d-a9f2-4b96-8172-8104b0938213-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I46f0651d-a9f2-4b96-8172-8104b0938213-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
            <keyFactSet>
                <keyFact id="Ib580dda7-0717-4cfc-b70e-34059b4c78f1-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ib580dda7-0717-4cfc-b70e-34059b4c78f1-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I834d38a6-dac5-4235-9cf4-d323dd89f1d2-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I834d38a6-dac5-4235-9cf4-d323dd89f1d2-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ic858ebcf-6f25-46ee-be0b-afbee1f41971-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ic858ebcf-6f25-46ee-be0b-afbee1f41971-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="If6a1b756-b293-4baf-a3c0-3332221e3ea8-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="If6a1b756-b293-4baf-a3c0-3332221e3ea8-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I80cb35ef-89d7-4ada-aef4-eda2b963a606-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I80cb35ef-89d7-4ada-aef4-eda2b963a606-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ia2c49323-f160-4dfa-8f79-5abea876efa6-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ia2c49323-f160-4dfa-8f79-5abea876efa6-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I8ecdc331-d565-4fcc-9f1f-a83e6e6bab9d-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I8ecdc331-d565-4fcc-9f1f-a83e6e6bab9d-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I4142aba3-f108-41dc-b537-ab44fd8cb5ae-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I4142aba3-f108-41dc-b537-ab44fd8cb5ae-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="If9dc68d4-33ec-4176-adb2-eda10373f1a0-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="If9dc68d4-33ec-4176-adb2-eda10373f1a0-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ice8f6ba6-0b24-4234-8e1c-ea2d6caf18f6-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ice8f6ba6-0b24-4234-8e1c-ea2d6caf18f6-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ia64d8e75-aa61-481b-bd40-df7270053448-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ia64d8e75-aa61-481b-bd40-df7270053448-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I2a787ac6-adce-419f-b230-293302ab6e2b-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I2a787ac6-adce-419f-b230-293302ab6e2b-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I562dc41e-1fcd-45b2-ad63-e346a9b12ed3-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I562dc41e-1fcd-45b2-ad63-e346a9b12ed3-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I2ff56de9-53fd-4be6-adc3-b9d48cbac0bd-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I2ff56de9-53fd-4be6-adc3-b9d48cbac0bd-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I8c49dcaf-eea6-48ad-bcf0-5c00ccde0415-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I8c49dcaf-eea6-48ad-bcf0-5c00ccde0415-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I7b431d95-a30e-403f-9b35-7575b30d31a3-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I7b431d95-a30e-403f-9b35-7575b30d31a3-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I46f0651d-a9f2-4b96-8172-8104b0938213-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I46f0651d-a9f2-4b96-8172-8104b0938213-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
            <keyFactSet>
                <keyFact id="Ib580dda7-0717-4cfc-b70e-34059b4c78f1-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ib580dda7-0717-4cfc-b70e-34059b4c78f1-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I834d38a6-dac5-4235-9cf4-d323dd89f1d2-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I834d38a6-dac5-4235-9cf4-d323dd89f1d2-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ic858ebcf-6f25-46ee-be0b-afbee1f41971-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ic858ebcf-6f25-46ee-be0b-afbee1f41971-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="If6a1b756-b293-4baf-a3c0-3332221e3ea8-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="If6a1b756-b293-4baf-a3c0-3332221e3ea8-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I80cb35ef-89d7-4ada-aef4-eda2b963a606-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I80cb35ef-89d7-4ada-aef4-eda2b963a606-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ia2c49323-f160-4dfa-8f79-5abea876efa6-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ia2c49323-f160-4dfa-8f79-5abea876efa6-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I8ecdc331-d565-4fcc-9f1f-a83e6e6bab9d-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I8ecdc331-d565-4fcc-9f1f-a83e6e6bab9d-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I4142aba3-f108-41dc-b537-ab44fd8cb5ae-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I4142aba3-f108-41dc-b537-ab44fd8cb5ae-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="If9dc68d4-33ec-4176-adb2-eda10373f1a0-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="If9dc68d4-33ec-4176-adb2-eda10373f1a0-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ice8f6ba6-0b24-4234-8e1c-ea2d6caf18f6-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ice8f6ba6-0b24-4234-8e1c-ea2d6caf18f6-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ia64d8e75-aa61-481b-bd40-df7270053448-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ia64d8e75-aa61-481b-bd40-df7270053448-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I2a787ac6-adce-419f-b230-293302ab6e2b-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I2a787ac6-adce-419f-b230-293302ab6e2b-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I562dc41e-1fcd-45b2-ad63-e346a9b12ed3-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I562dc41e-1fcd-45b2-ad63-e346a9b12ed3-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I2ff56de9-53fd-4be6-adc3-b9d48cbac0bd-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I2ff56de9-53fd-4be6-adc3-b9d48cbac0bd-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I8c49dcaf-eea6-48ad-bcf0-5c00ccde0415-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I8c49dcaf-eea6-48ad-bcf0-5c00ccde0415-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I7b431d95-a30e-403f-9b35-7575b30d31a3-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I7b431d95-a30e-403f-9b35-7575b30d31a3-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I46f0651d-a9f2-4b96-8172-8104b0938213-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I46f0651d-a9f2-4b96-8172-8104b0938213-hottextController" occur="1">
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
                <keyFact id="Ib580dda7-0717-4cfc-b70e-34059b4c78f1-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ib580dda7-0717-4cfc-b70e-34059b4c78f1-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I834d38a6-dac5-4235-9cf4-d323dd89f1d2-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I834d38a6-dac5-4235-9cf4-d323dd89f1d2-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ic858ebcf-6f25-46ee-be0b-afbee1f41971-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ic858ebcf-6f25-46ee-be0b-afbee1f41971-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="If6a1b756-b293-4baf-a3c0-3332221e3ea8-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="If6a1b756-b293-4baf-a3c0-3332221e3ea8-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I80cb35ef-89d7-4ada-aef4-eda2b963a606-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I80cb35ef-89d7-4ada-aef4-eda2b963a606-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ia2c49323-f160-4dfa-8f79-5abea876efa6-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ia2c49323-f160-4dfa-8f79-5abea876efa6-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I8ecdc331-d565-4fcc-9f1f-a83e6e6bab9d-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I8ecdc331-d565-4fcc-9f1f-a83e6e6bab9d-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I4142aba3-f108-41dc-b537-ab44fd8cb5ae-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I4142aba3-f108-41dc-b537-ab44fd8cb5ae-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="If9dc68d4-33ec-4176-adb2-eda10373f1a0-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="If9dc68d4-33ec-4176-adb2-eda10373f1a0-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ice8f6ba6-0b24-4234-8e1c-ea2d6caf18f6-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ice8f6ba6-0b24-4234-8e1c-ea2d6caf18f6-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ia64d8e75-aa61-481b-bd40-df7270053448-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ia64d8e75-aa61-481b-bd40-df7270053448-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I2a787ac6-adce-419f-b230-293302ab6e2b-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I2a787ac6-adce-419f-b230-293302ab6e2b-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I562dc41e-1fcd-45b2-ad63-e346a9b12ed3-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I562dc41e-1fcd-45b2-ad63-e346a9b12ed3-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I2ff56de9-53fd-4be6-adc3-b9d48cbac0bd-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I2ff56de9-53fd-4be6-adc3-b9d48cbac0bd-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I8c49dcaf-eea6-48ad-bcf0-5c00ccde0415-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I8c49dcaf-eea6-48ad-bcf0-5c00ccde0415-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I7b431d95-a30e-403f-9b35-7575b30d31a3-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I7b431d95-a30e-403f-9b35-7575b30d31a3-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I46f0651d-a9f2-4b96-8172-8104b0938213-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I46f0651d-a9f2-4b96-8172-8104b0938213-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
            <keyFactSet>
                <keyFact id="Ib580dda7-0717-4cfc-b70e-34059b4c78f1-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ib580dda7-0717-4cfc-b70e-34059b4c78f1-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I834d38a6-dac5-4235-9cf4-d323dd89f1d2-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I834d38a6-dac5-4235-9cf4-d323dd89f1d2-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ic858ebcf-6f25-46ee-be0b-afbee1f41971-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ic858ebcf-6f25-46ee-be0b-afbee1f41971-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="If6a1b756-b293-4baf-a3c0-3332221e3ea8-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="If6a1b756-b293-4baf-a3c0-3332221e3ea8-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I80cb35ef-89d7-4ada-aef4-eda2b963a606-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I80cb35ef-89d7-4ada-aef4-eda2b963a606-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ia2c49323-f160-4dfa-8f79-5abea876efa6-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ia2c49323-f160-4dfa-8f79-5abea876efa6-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I8ecdc331-d565-4fcc-9f1f-a83e6e6bab9d-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I8ecdc331-d565-4fcc-9f1f-a83e6e6bab9d-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I4142aba3-f108-41dc-b537-ab44fd8cb5ae-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I4142aba3-f108-41dc-b537-ab44fd8cb5ae-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="If9dc68d4-33ec-4176-adb2-eda10373f1a0-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="If9dc68d4-33ec-4176-adb2-eda10373f1a0-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ice8f6ba6-0b24-4234-8e1c-ea2d6caf18f6-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ice8f6ba6-0b24-4234-8e1c-ea2d6caf18f6-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ia64d8e75-aa61-481b-bd40-df7270053448-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ia64d8e75-aa61-481b-bd40-df7270053448-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I2a787ac6-adce-419f-b230-293302ab6e2b-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I2a787ac6-adce-419f-b230-293302ab6e2b-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I562dc41e-1fcd-45b2-ad63-e346a9b12ed3-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I562dc41e-1fcd-45b2-ad63-e346a9b12ed3-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I2ff56de9-53fd-4be6-adc3-b9d48cbac0bd-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I2ff56de9-53fd-4be6-adc3-b9d48cbac0bd-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I8c49dcaf-eea6-48ad-bcf0-5c00ccde0415-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I8c49dcaf-eea6-48ad-bcf0-5c00ccde0415-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I7b431d95-a30e-403f-9b35-7575b30d31a3-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I7b431d95-a30e-403f-9b35-7575b30d31a3-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I46f0651d-a9f2-4b96-8172-8104b0938213-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I46f0651d-a9f2-4b96-8172-8104b0938213-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
            <keyFactSet>
                <keyFact id="Ib580dda7-0717-4cfc-b70e-34059b4c78f1-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ib580dda7-0717-4cfc-b70e-34059b4c78f1-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I834d38a6-dac5-4235-9cf4-d323dd89f1d2-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I834d38a6-dac5-4235-9cf4-d323dd89f1d2-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ic858ebcf-6f25-46ee-be0b-afbee1f41971-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ic858ebcf-6f25-46ee-be0b-afbee1f41971-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="If6a1b756-b293-4baf-a3c0-3332221e3ea8-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="If6a1b756-b293-4baf-a3c0-3332221e3ea8-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I80cb35ef-89d7-4ada-aef4-eda2b963a606-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I80cb35ef-89d7-4ada-aef4-eda2b963a606-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ia2c49323-f160-4dfa-8f79-5abea876efa6-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ia2c49323-f160-4dfa-8f79-5abea876efa6-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I8ecdc331-d565-4fcc-9f1f-a83e6e6bab9d-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I8ecdc331-d565-4fcc-9f1f-a83e6e6bab9d-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I4142aba3-f108-41dc-b537-ab44fd8cb5ae-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I4142aba3-f108-41dc-b537-ab44fd8cb5ae-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="If9dc68d4-33ec-4176-adb2-eda10373f1a0-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="If9dc68d4-33ec-4176-adb2-eda10373f1a0-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ice8f6ba6-0b24-4234-8e1c-ea2d6caf18f6-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ice8f6ba6-0b24-4234-8e1c-ea2d6caf18f6-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ia64d8e75-aa61-481b-bd40-df7270053448-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ia64d8e75-aa61-481b-bd40-df7270053448-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I2a787ac6-adce-419f-b230-293302ab6e2b-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I2a787ac6-adce-419f-b230-293302ab6e2b-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I562dc41e-1fcd-45b2-ad63-e346a9b12ed3-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I562dc41e-1fcd-45b2-ad63-e346a9b12ed3-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I2ff56de9-53fd-4be6-adc3-b9d48cbac0bd-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I2ff56de9-53fd-4be6-adc3-b9d48cbac0bd-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I8c49dcaf-eea6-48ad-bcf0-5c00ccde0415-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I8c49dcaf-eea6-48ad-bcf0-5c00ccde0415-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I7b431d95-a30e-403f-9b35-7575b30d31a3-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I7b431d95-a30e-403f-9b35-7575b30d31a3-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I46f0651d-a9f2-4b96-8172-8104b0938213-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I46f0651d-a9f2-4b96-8172-8104b0938213-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
            <keyFactSet>
                <keyFact id="I1e40e819-c8a2-42dd-89de-dd29e070b99b-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I1e40e819-c8a2-42dd-89de-dd29e070b99b-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I67b6042e-7444-46be-b169-53ec421f9eda-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I67b6042e-7444-46be-b169-53ec421f9eda-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I18248834-8320-4342-967c-2007195ba14a-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I18248834-8320-4342-967c-2007195ba14a-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I0bf4350c-9288-4cec-9e37-1b0978b057ba-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I0bf4350c-9288-4cec-9e37-1b0978b057ba-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ifb1bfeea-20a0-4d29-bb87-d502ff355522-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ifb1bfeea-20a0-4d29-bb87-d502ff355522-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ie1f531cd-79c9-4b87-a90c-76766792b183-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ie1f531cd-79c9-4b87-a90c-76766792b183-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I66faad9a-d004-4cdc-a275-6f9ff736bb14-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I66faad9a-d004-4cdc-a275-6f9ff736bb14-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I665f2702-e926-4266-bc3c-3f04ea9a6c62-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I665f2702-e926-4266-bc3c-3f04ea9a6c62-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I7e8b004a-0223-432a-bb9a-536918190017-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I7e8b004a-0223-432a-bb9a-536918190017-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I217873b0-4970-405f-a80b-b2a9f42bbd85-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I217873b0-4970-405f-a80b-b2a9f42bbd85-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I46521597-f1f4-432c-96c2-8e3defb05950-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I46521597-f1f4-432c-96c2-8e3defb05950-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ifbea86b5-20bc-4dde-a4e4-4a14153c8ead-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ifbea86b5-20bc-4dde-a4e4-4a14153c8ead-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I279d3c47-95a8-42d1-9440-26fadf909090-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I279d3c47-95a8-42d1-9440-26fadf909090-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I6d80a45f-5042-4f42-99fe-151229268a43-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I6d80a45f-5042-4f42-99fe-151229268a43-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
            <keyFactSet>
                <keyFact id="I1e40e819-c8a2-42dd-89de-dd29e070b99b-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I1e40e819-c8a2-42dd-89de-dd29e070b99b-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I67b6042e-7444-46be-b169-53ec421f9eda-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I67b6042e-7444-46be-b169-53ec421f9eda-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I18248834-8320-4342-967c-2007195ba14a-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I18248834-8320-4342-967c-2007195ba14a-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I0bf4350c-9288-4cec-9e37-1b0978b057ba-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I0bf4350c-9288-4cec-9e37-1b0978b057ba-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ifb1bfeea-20a0-4d29-bb87-d502ff355522-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ifb1bfeea-20a0-4d29-bb87-d502ff355522-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ie1f531cd-79c9-4b87-a90c-76766792b183-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ie1f531cd-79c9-4b87-a90c-76766792b183-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I66faad9a-d004-4cdc-a275-6f9ff736bb14-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I66faad9a-d004-4cdc-a275-6f9ff736bb14-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I665f2702-e926-4266-bc3c-3f04ea9a6c62-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I665f2702-e926-4266-bc3c-3f04ea9a6c62-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I7e8b004a-0223-432a-bb9a-536918190017-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I7e8b004a-0223-432a-bb9a-536918190017-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I217873b0-4970-405f-a80b-b2a9f42bbd85-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I217873b0-4970-405f-a80b-b2a9f42bbd85-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I46521597-f1f4-432c-96c2-8e3defb05950-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I46521597-f1f4-432c-96c2-8e3defb05950-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ifbea86b5-20bc-4dde-a4e4-4a14153c8ead-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ifbea86b5-20bc-4dde-a4e4-4a14153c8ead-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I279d3c47-95a8-42d1-9440-26fadf909090-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I279d3c47-95a8-42d1-9440-26fadf909090-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I6d80a45f-5042-4f42-99fe-151229268a43-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I6d80a45f-5042-4f42-99fe-151229268a43-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
            <keyFactSet>
                <keyFact id="Iba0772fe-878f-457c-b652-a0e922626082-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Iba0772fe-878f-457c-b652-a0e922626082-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I5493e55f-fae9-499f-bd3e-16df52c028e1-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I5493e55f-fae9-499f-bd3e-16df52c028e1-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I7134c778-4641-40b5-8eae-1a80013cf777-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I7134c778-4641-40b5-8eae-1a80013cf777-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I3cb9edb6-551e-474a-9122-338b8c6a396d-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I3cb9edb6-551e-474a-9122-338b8c6a396d-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I41d5e633-d892-46fe-8a0c-ceba54c167f3-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I41d5e633-d892-46fe-8a0c-ceba54c167f3-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I498a96cd-114d-4299-b27c-639e41559e29-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I498a96cd-114d-4299-b27c-639e41559e29-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I19b64b54-f7f4-4284-af37-37999bd24935-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I19b64b54-f7f4-4284-af37-37999bd24935-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I7ea195a7-b0ec-465e-ada3-31c9e940f0dc-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I7ea195a7-b0ec-465e-ada3-31c9e940f0dc-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ibf683ce7-ac85-4610-b057-dd0578f3b71f-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ibf683ce7-ac85-4610-b057-dd0578f3b71f-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ie6f1fe1f-fe23-4f29-85ec-27d483138cfa-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ie6f1fe1f-fe23-4f29-85ec-27d483138cfa-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I2b4ed71d-8984-4ffd-a95a-126efbb12e48-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I2b4ed71d-8984-4ffd-a95a-126efbb12e48-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I0778d3a8-11c3-41f5-aeb7-4efd94338296-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I0778d3a8-11c3-41f5-aeb7-4efd94338296-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I3a1bf3a7-f9b4-472d-ac19-c6d27f671543-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I3a1bf3a7-f9b4-472d-ac19-c6d27f671543-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I01f17f48-6da9-44a0-bb7f-82e325f2f233-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I01f17f48-6da9-44a0-bb7f-82e325f2f233-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ifdbe8e9c-0eeb-4019-b8a7-5599265834fb-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ifdbe8e9c-0eeb-4019-b8a7-5599265834fb-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I0dd2d1e9-84b7-4b3f-a144-a2ec2c13061b-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I0dd2d1e9-84b7-4b3f-a144-a2ec2c13061b-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I41e6c74d-787a-4620-889f-fae84be71cdb-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I41e6c74d-787a-4620-889f-fae84be71cdb-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I1f639c54-57ee-428e-93ed-77d786d5a2d1-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I1f639c54-57ee-428e-93ed-77d786d5a2d1-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
        </keyFinding>

    Private _finding3 As XElement =
        <keyFinding id="hottextController" scoringMethod="Polytomous">
            <keyFactSet>
                <keyFact id="Iba0772fe-878f-457c-b652-a0e922626082-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Iba0772fe-878f-457c-b652-a0e922626082-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I5493e55f-fae9-499f-bd3e-16df52c028e1-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I5493e55f-fae9-499f-bd3e-16df52c028e1-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I7134c778-4641-40b5-8eae-1a80013cf777-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I7134c778-4641-40b5-8eae-1a80013cf777-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I3cb9edb6-551e-474a-9122-338b8c6a396d-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I3cb9edb6-551e-474a-9122-338b8c6a396d-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I41d5e633-d892-46fe-8a0c-ceba54c167f3-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I41d5e633-d892-46fe-8a0c-ceba54c167f3-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I498a96cd-114d-4299-b27c-639e41559e29-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I498a96cd-114d-4299-b27c-639e41559e29-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I19b64b54-f7f4-4284-af37-37999bd24935-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I19b64b54-f7f4-4284-af37-37999bd24935-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I7ea195a7-b0ec-465e-ada3-31c9e940f0dc-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I7ea195a7-b0ec-465e-ada3-31c9e940f0dc-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ibf683ce7-ac85-4610-b057-dd0578f3b71f-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ibf683ce7-ac85-4610-b057-dd0578f3b71f-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ie6f1fe1f-fe23-4f29-85ec-27d483138cfa-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ie6f1fe1f-fe23-4f29-85ec-27d483138cfa-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I2b4ed71d-8984-4ffd-a95a-126efbb12e48-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I2b4ed71d-8984-4ffd-a95a-126efbb12e48-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I0778d3a8-11c3-41f5-aeb7-4efd94338296-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I0778d3a8-11c3-41f5-aeb7-4efd94338296-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I3a1bf3a7-f9b4-472d-ac19-c6d27f671543-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I3a1bf3a7-f9b4-472d-ac19-c6d27f671543-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I01f17f48-6da9-44a0-bb7f-82e325f2f233-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I01f17f48-6da9-44a0-bb7f-82e325f2f233-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ifdbe8e9c-0eeb-4019-b8a7-5599265834fb-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ifdbe8e9c-0eeb-4019-b8a7-5599265834fb-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I0dd2d1e9-84b7-4b3f-a144-a2ec2c13061b-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I0dd2d1e9-84b7-4b3f-a144-a2ec2c13061b-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I41e6c74d-787a-4620-889f-fae84be71cdb-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I41e6c74d-787a-4620-889f-fae84be71cdb-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I1f639c54-57ee-428e-93ed-77d786d5a2d1-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I1f639c54-57ee-428e-93ed-77d786d5a2d1-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ib580dda7-0717-4cfc-b70e-34059b4c78f1-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ib580dda7-0717-4cfc-b70e-34059b4c78f1-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I834d38a6-dac5-4235-9cf4-d323dd89f1d2-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I834d38a6-dac5-4235-9cf4-d323dd89f1d2-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ic858ebcf-6f25-46ee-be0b-afbee1f41971-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ic858ebcf-6f25-46ee-be0b-afbee1f41971-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="If6a1b756-b293-4baf-a3c0-3332221e3ea8-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="If6a1b756-b293-4baf-a3c0-3332221e3ea8-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I80cb35ef-89d7-4ada-aef4-eda2b963a606-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I80cb35ef-89d7-4ada-aef4-eda2b963a606-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ia2c49323-f160-4dfa-8f79-5abea876efa6-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ia2c49323-f160-4dfa-8f79-5abea876efa6-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I8ecdc331-d565-4fcc-9f1f-a83e6e6bab9d-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I8ecdc331-d565-4fcc-9f1f-a83e6e6bab9d-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I4142aba3-f108-41dc-b537-ab44fd8cb5ae-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I4142aba3-f108-41dc-b537-ab44fd8cb5ae-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="If9dc68d4-33ec-4176-adb2-eda10373f1a0-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="If9dc68d4-33ec-4176-adb2-eda10373f1a0-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ice8f6ba6-0b24-4234-8e1c-ea2d6caf18f6-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ice8f6ba6-0b24-4234-8e1c-ea2d6caf18f6-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ia64d8e75-aa61-481b-bd40-df7270053448-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ia64d8e75-aa61-481b-bd40-df7270053448-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I2a787ac6-adce-419f-b230-293302ab6e2b-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I2a787ac6-adce-419f-b230-293302ab6e2b-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I562dc41e-1fcd-45b2-ad63-e346a9b12ed3-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I562dc41e-1fcd-45b2-ad63-e346a9b12ed3-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I2ff56de9-53fd-4be6-adc3-b9d48cbac0bd-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I2ff56de9-53fd-4be6-adc3-b9d48cbac0bd-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I8c49dcaf-eea6-48ad-bcf0-5c00ccde0415-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I8c49dcaf-eea6-48ad-bcf0-5c00ccde0415-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I7b431d95-a30e-403f-9b35-7575b30d31a3-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I7b431d95-a30e-403f-9b35-7575b30d31a3-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I46f0651d-a9f2-4b96-8172-8104b0938213-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I46f0651d-a9f2-4b96-8172-8104b0938213-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I1e40e819-c8a2-42dd-89de-dd29e070b99b-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I1e40e819-c8a2-42dd-89de-dd29e070b99b-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I67b6042e-7444-46be-b169-53ec421f9eda-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I67b6042e-7444-46be-b169-53ec421f9eda-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I18248834-8320-4342-967c-2007195ba14a-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I18248834-8320-4342-967c-2007195ba14a-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I0bf4350c-9288-4cec-9e37-1b0978b057ba-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I0bf4350c-9288-4cec-9e37-1b0978b057ba-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ifb1bfeea-20a0-4d29-bb87-d502ff355522-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ifb1bfeea-20a0-4d29-bb87-d502ff355522-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ie1f531cd-79c9-4b87-a90c-76766792b183-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ie1f531cd-79c9-4b87-a90c-76766792b183-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I66faad9a-d004-4cdc-a275-6f9ff736bb14-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I66faad9a-d004-4cdc-a275-6f9ff736bb14-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I665f2702-e926-4266-bc3c-3f04ea9a6c62-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I665f2702-e926-4266-bc3c-3f04ea9a6c62-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I7e8b004a-0223-432a-bb9a-536918190017-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I7e8b004a-0223-432a-bb9a-536918190017-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I217873b0-4970-405f-a80b-b2a9f42bbd85-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I217873b0-4970-405f-a80b-b2a9f42bbd85-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I46521597-f1f4-432c-96c2-8e3defb05950-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I46521597-f1f4-432c-96c2-8e3defb05950-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ifbea86b5-20bc-4dde-a4e4-4a14153c8ead-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ifbea86b5-20bc-4dde-a4e4-4a14153c8ead-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I279d3c47-95a8-42d1-9440-26fadf909090-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I279d3c47-95a8-42d1-9440-26fadf909090-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I6d80a45f-5042-4f42-99fe-151229268a43-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I6d80a45f-5042-4f42-99fe-151229268a43-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
            <keyFactSet>
                <keyFact id="Iba0772fe-878f-457c-b652-a0e922626082-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Iba0772fe-878f-457c-b652-a0e922626082-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I5493e55f-fae9-499f-bd3e-16df52c028e1-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I5493e55f-fae9-499f-bd3e-16df52c028e1-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I7134c778-4641-40b5-8eae-1a80013cf777-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I7134c778-4641-40b5-8eae-1a80013cf777-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I3cb9edb6-551e-474a-9122-338b8c6a396d-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I3cb9edb6-551e-474a-9122-338b8c6a396d-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I41d5e633-d892-46fe-8a0c-ceba54c167f3-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I41d5e633-d892-46fe-8a0c-ceba54c167f3-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I498a96cd-114d-4299-b27c-639e41559e29-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I498a96cd-114d-4299-b27c-639e41559e29-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I19b64b54-f7f4-4284-af37-37999bd24935-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I19b64b54-f7f4-4284-af37-37999bd24935-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I7ea195a7-b0ec-465e-ada3-31c9e940f0dc-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I7ea195a7-b0ec-465e-ada3-31c9e940f0dc-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ibf683ce7-ac85-4610-b057-dd0578f3b71f-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ibf683ce7-ac85-4610-b057-dd0578f3b71f-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ie6f1fe1f-fe23-4f29-85ec-27d483138cfa-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ie6f1fe1f-fe23-4f29-85ec-27d483138cfa-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I2b4ed71d-8984-4ffd-a95a-126efbb12e48-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I2b4ed71d-8984-4ffd-a95a-126efbb12e48-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I0778d3a8-11c3-41f5-aeb7-4efd94338296-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I0778d3a8-11c3-41f5-aeb7-4efd94338296-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I3a1bf3a7-f9b4-472d-ac19-c6d27f671543-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I3a1bf3a7-f9b4-472d-ac19-c6d27f671543-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I01f17f48-6da9-44a0-bb7f-82e325f2f233-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I01f17f48-6da9-44a0-bb7f-82e325f2f233-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ifdbe8e9c-0eeb-4019-b8a7-5599265834fb-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ifdbe8e9c-0eeb-4019-b8a7-5599265834fb-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I0dd2d1e9-84b7-4b3f-a144-a2ec2c13061b-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I0dd2d1e9-84b7-4b3f-a144-a2ec2c13061b-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I41e6c74d-787a-4620-889f-fae84be71cdb-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I41e6c74d-787a-4620-889f-fae84be71cdb-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I1f639c54-57ee-428e-93ed-77d786d5a2d1-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I1f639c54-57ee-428e-93ed-77d786d5a2d1-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ib580dda7-0717-4cfc-b70e-34059b4c78f1-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ib580dda7-0717-4cfc-b70e-34059b4c78f1-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I834d38a6-dac5-4235-9cf4-d323dd89f1d2-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I834d38a6-dac5-4235-9cf4-d323dd89f1d2-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ic858ebcf-6f25-46ee-be0b-afbee1f41971-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ic858ebcf-6f25-46ee-be0b-afbee1f41971-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="If6a1b756-b293-4baf-a3c0-3332221e3ea8-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="If6a1b756-b293-4baf-a3c0-3332221e3ea8-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I80cb35ef-89d7-4ada-aef4-eda2b963a606-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I80cb35ef-89d7-4ada-aef4-eda2b963a606-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ia2c49323-f160-4dfa-8f79-5abea876efa6-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ia2c49323-f160-4dfa-8f79-5abea876efa6-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I8ecdc331-d565-4fcc-9f1f-a83e6e6bab9d-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I8ecdc331-d565-4fcc-9f1f-a83e6e6bab9d-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I4142aba3-f108-41dc-b537-ab44fd8cb5ae-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I4142aba3-f108-41dc-b537-ab44fd8cb5ae-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="If9dc68d4-33ec-4176-adb2-eda10373f1a0-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="If9dc68d4-33ec-4176-adb2-eda10373f1a0-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ice8f6ba6-0b24-4234-8e1c-ea2d6caf18f6-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ice8f6ba6-0b24-4234-8e1c-ea2d6caf18f6-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ia64d8e75-aa61-481b-bd40-df7270053448-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ia64d8e75-aa61-481b-bd40-df7270053448-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I2a787ac6-adce-419f-b230-293302ab6e2b-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I2a787ac6-adce-419f-b230-293302ab6e2b-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I562dc41e-1fcd-45b2-ad63-e346a9b12ed3-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I562dc41e-1fcd-45b2-ad63-e346a9b12ed3-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I2ff56de9-53fd-4be6-adc3-b9d48cbac0bd-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I2ff56de9-53fd-4be6-adc3-b9d48cbac0bd-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I8c49dcaf-eea6-48ad-bcf0-5c00ccde0415-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I8c49dcaf-eea6-48ad-bcf0-5c00ccde0415-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I7b431d95-a30e-403f-9b35-7575b30d31a3-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I7b431d95-a30e-403f-9b35-7575b30d31a3-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I46f0651d-a9f2-4b96-8172-8104b0938213-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I46f0651d-a9f2-4b96-8172-8104b0938213-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I1e40e819-c8a2-42dd-89de-dd29e070b99b-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I1e40e819-c8a2-42dd-89de-dd29e070b99b-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I67b6042e-7444-46be-b169-53ec421f9eda-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I67b6042e-7444-46be-b169-53ec421f9eda-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I18248834-8320-4342-967c-2007195ba14a-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I18248834-8320-4342-967c-2007195ba14a-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I0bf4350c-9288-4cec-9e37-1b0978b057ba-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I0bf4350c-9288-4cec-9e37-1b0978b057ba-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ifb1bfeea-20a0-4d29-bb87-d502ff355522-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ifb1bfeea-20a0-4d29-bb87-d502ff355522-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ie1f531cd-79c9-4b87-a90c-76766792b183-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ie1f531cd-79c9-4b87-a90c-76766792b183-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I66faad9a-d004-4cdc-a275-6f9ff736bb14-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I66faad9a-d004-4cdc-a275-6f9ff736bb14-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I665f2702-e926-4266-bc3c-3f04ea9a6c62-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I665f2702-e926-4266-bc3c-3f04ea9a6c62-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I7e8b004a-0223-432a-bb9a-536918190017-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I7e8b004a-0223-432a-bb9a-536918190017-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I217873b0-4970-405f-a80b-b2a9f42bbd85-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I217873b0-4970-405f-a80b-b2a9f42bbd85-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I46521597-f1f4-432c-96c2-8e3defb05950-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I46521597-f1f4-432c-96c2-8e3defb05950-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ifbea86b5-20bc-4dde-a4e4-4a14153c8ead-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ifbea86b5-20bc-4dde-a4e4-4a14153c8ead-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I279d3c47-95a8-42d1-9440-26fadf909090-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I279d3c47-95a8-42d1-9440-26fadf909090-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I6d80a45f-5042-4f42-99fe-151229268a43-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I6d80a45f-5042-4f42-99fe-151229268a43-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
        </keyFinding>

    Private _finding4 As XElement =
        <keyFinding id="hottextController" scoringMethod="Polytomous">
            <keyFact id="Iba0772fe-878f-457c-b652-a0e922626082-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="Iba0772fe-878f-457c-b652-a0e922626082-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="If9dc68d4-33ec-4176-adb2-eda10373f1a0-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="If9dc68d4-33ec-4176-adb2-eda10373f1a0-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="Ice8f6ba6-0b24-4234-8e1c-ea2d6caf18f6-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="Ice8f6ba6-0b24-4234-8e1c-ea2d6caf18f6-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="Ia64d8e75-aa61-481b-bd40-df7270053448-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="Ia64d8e75-aa61-481b-bd40-df7270053448-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I2a787ac6-adce-419f-b230-293302ab6e2b-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I2a787ac6-adce-419f-b230-293302ab6e2b-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>true</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I562dc41e-1fcd-45b2-ad63-e346a9b12ed3-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I562dc41e-1fcd-45b2-ad63-e346a9b12ed3-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I2ff56de9-53fd-4be6-adc3-b9d48cbac0bd-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I2ff56de9-53fd-4be6-adc3-b9d48cbac0bd-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I8c49dcaf-eea6-48ad-bcf0-5c00ccde0415-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I8c49dcaf-eea6-48ad-bcf0-5c00ccde0415-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I7b431d95-a30e-403f-9b35-7575b30d31a3-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I7b431d95-a30e-403f-9b35-7575b30d31a3-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I46f0651d-a9f2-4b96-8172-8104b0938213-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I46f0651d-a9f2-4b96-8172-8104b0938213-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>true</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I1e40e819-c8a2-42dd-89de-dd29e070b99b-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I1e40e819-c8a2-42dd-89de-dd29e070b99b-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I67b6042e-7444-46be-b169-53ec421f9eda-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I67b6042e-7444-46be-b169-53ec421f9eda-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I18248834-8320-4342-967c-2007195ba14a-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I18248834-8320-4342-967c-2007195ba14a-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I0bf4350c-9288-4cec-9e37-1b0978b057ba-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I0bf4350c-9288-4cec-9e37-1b0978b057ba-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>true</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="Ifb1bfeea-20a0-4d29-bb87-d502ff355522-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="Ifb1bfeea-20a0-4d29-bb87-d502ff355522-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="Ie1f531cd-79c9-4b87-a90c-76766792b183-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="Ie1f531cd-79c9-4b87-a90c-76766792b183-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I66faad9a-d004-4cdc-a275-6f9ff736bb14-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I66faad9a-d004-4cdc-a275-6f9ff736bb14-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I665f2702-e926-4266-bc3c-3f04ea9a6c62-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I665f2702-e926-4266-bc3c-3f04ea9a6c62-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>true</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I7e8b004a-0223-432a-bb9a-536918190017-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I7e8b004a-0223-432a-bb9a-536918190017-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I217873b0-4970-405f-a80b-b2a9f42bbd85-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I217873b0-4970-405f-a80b-b2a9f42bbd85-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I46521597-f1f4-432c-96c2-8e3defb05950-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I46521597-f1f4-432c-96c2-8e3defb05950-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>true</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="Ifbea86b5-20bc-4dde-a4e4-4a14153c8ead-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="Ifbea86b5-20bc-4dde-a4e4-4a14153c8ead-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I4142aba3-f108-41dc-b537-ab44fd8cb5ae-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I4142aba3-f108-41dc-b537-ab44fd8cb5ae-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I279d3c47-95a8-42d1-9440-26fadf909090-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I279d3c47-95a8-42d1-9440-26fadf909090-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I8ecdc331-d565-4fcc-9f1f-a83e6e6bab9d-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I8ecdc331-d565-4fcc-9f1f-a83e6e6bab9d-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I80cb35ef-89d7-4ada-aef4-eda2b963a606-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I80cb35ef-89d7-4ada-aef4-eda2b963a606-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I5493e55f-fae9-499f-bd3e-16df52c028e1-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I5493e55f-fae9-499f-bd3e-16df52c028e1-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I7134c778-4641-40b5-8eae-1a80013cf777-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I7134c778-4641-40b5-8eae-1a80013cf777-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I3cb9edb6-551e-474a-9122-338b8c6a396d-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I3cb9edb6-551e-474a-9122-338b8c6a396d-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>true</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I41d5e633-d892-46fe-8a0c-ceba54c167f3-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I41d5e633-d892-46fe-8a0c-ceba54c167f3-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I498a96cd-114d-4299-b27c-639e41559e29-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I498a96cd-114d-4299-b27c-639e41559e29-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I19b64b54-f7f4-4284-af37-37999bd24935-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I19b64b54-f7f4-4284-af37-37999bd24935-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I7ea195a7-b0ec-465e-ada3-31c9e940f0dc-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I7ea195a7-b0ec-465e-ada3-31c9e940f0dc-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="Ibf683ce7-ac85-4610-b057-dd0578f3b71f-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="Ibf683ce7-ac85-4610-b057-dd0578f3b71f-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="Ie6f1fe1f-fe23-4f29-85ec-27d483138cfa-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="Ie6f1fe1f-fe23-4f29-85ec-27d483138cfa-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I2b4ed71d-8984-4ffd-a95a-126efbb12e48-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I2b4ed71d-8984-4ffd-a95a-126efbb12e48-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I0778d3a8-11c3-41f5-aeb7-4efd94338296-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I0778d3a8-11c3-41f5-aeb7-4efd94338296-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I3a1bf3a7-f9b4-472d-ac19-c6d27f671543-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I3a1bf3a7-f9b4-472d-ac19-c6d27f671543-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I01f17f48-6da9-44a0-bb7f-82e325f2f233-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I01f17f48-6da9-44a0-bb7f-82e325f2f233-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="Ifdbe8e9c-0eeb-4019-b8a7-5599265834fb-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="Ifdbe8e9c-0eeb-4019-b8a7-5599265834fb-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I0dd2d1e9-84b7-4b3f-a144-a2ec2c13061b-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I0dd2d1e9-84b7-4b3f-a144-a2ec2c13061b-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I41e6c74d-787a-4620-889f-fae84be71cdb-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I41e6c74d-787a-4620-889f-fae84be71cdb-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I1f639c54-57ee-428e-93ed-77d786d5a2d1-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I1f639c54-57ee-428e-93ed-77d786d5a2d1-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>true</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="Ib580dda7-0717-4cfc-b70e-34059b4c78f1-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="Ib580dda7-0717-4cfc-b70e-34059b4c78f1-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I834d38a6-dac5-4235-9cf4-d323dd89f1d2-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I834d38a6-dac5-4235-9cf4-d323dd89f1d2-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="Ic858ebcf-6f25-46ee-be0b-afbee1f41971-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="Ic858ebcf-6f25-46ee-be0b-afbee1f41971-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="If6a1b756-b293-4baf-a3c0-3332221e3ea8-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="If6a1b756-b293-4baf-a3c0-3332221e3ea8-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="Ia2c49323-f160-4dfa-8f79-5abea876efa6-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="Ia2c49323-f160-4dfa-8f79-5abea876efa6-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I6d80a45f-5042-4f42-99fe-151229268a43-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I6d80a45f-5042-4f42-99fe-151229268a43-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
        </keyFinding>

    Private _finding5 As XElement =
        <keyFinding id="hottextController" scoringMethod="Dichotomous">
            <keyFact id="Iba0772fe-878f-457c-b652-a0e922626082-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="Iba0772fe-878f-457c-b652-a0e922626082-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I41e6c74d-787a-4620-889f-fae84be71cdb-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I41e6c74d-787a-4620-889f-fae84be71cdb-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I0dd2d1e9-84b7-4b3f-a144-a2ec2c13061b-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I0dd2d1e9-84b7-4b3f-a144-a2ec2c13061b-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="Ifdbe8e9c-0eeb-4019-b8a7-5599265834fb-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="Ifdbe8e9c-0eeb-4019-b8a7-5599265834fb-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I01f17f48-6da9-44a0-bb7f-82e325f2f233-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I01f17f48-6da9-44a0-bb7f-82e325f2f233-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I3a1bf3a7-f9b4-472d-ac19-c6d27f671543-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I3a1bf3a7-f9b4-472d-ac19-c6d27f671543-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I0778d3a8-11c3-41f5-aeb7-4efd94338296-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I0778d3a8-11c3-41f5-aeb7-4efd94338296-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I2b4ed71d-8984-4ffd-a95a-126efbb12e48-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I2b4ed71d-8984-4ffd-a95a-126efbb12e48-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="Ie6f1fe1f-fe23-4f29-85ec-27d483138cfa-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="Ie6f1fe1f-fe23-4f29-85ec-27d483138cfa-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="Ibf683ce7-ac85-4610-b057-dd0578f3b71f-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="Ibf683ce7-ac85-4610-b057-dd0578f3b71f-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I7ea195a7-b0ec-465e-ada3-31c9e940f0dc-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I7ea195a7-b0ec-465e-ada3-31c9e940f0dc-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I19b64b54-f7f4-4284-af37-37999bd24935-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I19b64b54-f7f4-4284-af37-37999bd24935-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I498a96cd-114d-4299-b27c-639e41559e29-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I498a96cd-114d-4299-b27c-639e41559e29-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I41d5e633-d892-46fe-8a0c-ceba54c167f3-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I41d5e633-d892-46fe-8a0c-ceba54c167f3-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I3cb9edb6-551e-474a-9122-338b8c6a396d-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I3cb9edb6-551e-474a-9122-338b8c6a396d-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>true</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I7134c778-4641-40b5-8eae-1a80013cf777-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I7134c778-4641-40b5-8eae-1a80013cf777-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I5493e55f-fae9-499f-bd3e-16df52c028e1-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I5493e55f-fae9-499f-bd3e-16df52c028e1-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I279d3c47-95a8-42d1-9440-26fadf909090-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I279d3c47-95a8-42d1-9440-26fadf909090-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="Ifbea86b5-20bc-4dde-a4e4-4a14153c8ead-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="Ifbea86b5-20bc-4dde-a4e4-4a14153c8ead-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I46521597-f1f4-432c-96c2-8e3defb05950-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I46521597-f1f4-432c-96c2-8e3defb05950-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>true</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I217873b0-4970-405f-a80b-b2a9f42bbd85-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I217873b0-4970-405f-a80b-b2a9f42bbd85-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I7e8b004a-0223-432a-bb9a-536918190017-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I7e8b004a-0223-432a-bb9a-536918190017-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I665f2702-e926-4266-bc3c-3f04ea9a6c62-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I665f2702-e926-4266-bc3c-3f04ea9a6c62-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>true</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I66faad9a-d004-4cdc-a275-6f9ff736bb14-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I66faad9a-d004-4cdc-a275-6f9ff736bb14-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="Ie1f531cd-79c9-4b87-a90c-76766792b183-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="Ie1f531cd-79c9-4b87-a90c-76766792b183-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="Ifb1bfeea-20a0-4d29-bb87-d502ff355522-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="Ifb1bfeea-20a0-4d29-bb87-d502ff355522-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I0bf4350c-9288-4cec-9e37-1b0978b057ba-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I0bf4350c-9288-4cec-9e37-1b0978b057ba-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>true</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I18248834-8320-4342-967c-2007195ba14a-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I18248834-8320-4342-967c-2007195ba14a-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I67b6042e-7444-46be-b169-53ec421f9eda-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I67b6042e-7444-46be-b169-53ec421f9eda-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I1e40e819-c8a2-42dd-89de-dd29e070b99b-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I1e40e819-c8a2-42dd-89de-dd29e070b99b-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I1f639c54-57ee-428e-93ed-77d786d5a2d1-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I1f639c54-57ee-428e-93ed-77d786d5a2d1-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>true</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I6d80a45f-5042-4f42-99fe-151229268a43-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I6d80a45f-5042-4f42-99fe-151229268a43-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFactSet>
                <keyFact id="Ib580dda7-0717-4cfc-b70e-34059b4c78f1-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ib580dda7-0717-4cfc-b70e-34059b4c78f1-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I834d38a6-dac5-4235-9cf4-d323dd89f1d2-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I834d38a6-dac5-4235-9cf4-d323dd89f1d2-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ic858ebcf-6f25-46ee-be0b-afbee1f41971-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ic858ebcf-6f25-46ee-be0b-afbee1f41971-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="If6a1b756-b293-4baf-a3c0-3332221e3ea8-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="If6a1b756-b293-4baf-a3c0-3332221e3ea8-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I80cb35ef-89d7-4ada-aef4-eda2b963a606-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I80cb35ef-89d7-4ada-aef4-eda2b963a606-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ia2c49323-f160-4dfa-8f79-5abea876efa6-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ia2c49323-f160-4dfa-8f79-5abea876efa6-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I8ecdc331-d565-4fcc-9f1f-a83e6e6bab9d-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I8ecdc331-d565-4fcc-9f1f-a83e6e6bab9d-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I4142aba3-f108-41dc-b537-ab44fd8cb5ae-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I4142aba3-f108-41dc-b537-ab44fd8cb5ae-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="If9dc68d4-33ec-4176-adb2-eda10373f1a0-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="If9dc68d4-33ec-4176-adb2-eda10373f1a0-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ice8f6ba6-0b24-4234-8e1c-ea2d6caf18f6-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ice8f6ba6-0b24-4234-8e1c-ea2d6caf18f6-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ia64d8e75-aa61-481b-bd40-df7270053448-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ia64d8e75-aa61-481b-bd40-df7270053448-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I2a787ac6-adce-419f-b230-293302ab6e2b-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I2a787ac6-adce-419f-b230-293302ab6e2b-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I562dc41e-1fcd-45b2-ad63-e346a9b12ed3-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I562dc41e-1fcd-45b2-ad63-e346a9b12ed3-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I2ff56de9-53fd-4be6-adc3-b9d48cbac0bd-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I2ff56de9-53fd-4be6-adc3-b9d48cbac0bd-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I8c49dcaf-eea6-48ad-bcf0-5c00ccde0415-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I8c49dcaf-eea6-48ad-bcf0-5c00ccde0415-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I7b431d95-a30e-403f-9b35-7575b30d31a3-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I7b431d95-a30e-403f-9b35-7575b30d31a3-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I46f0651d-a9f2-4b96-8172-8104b0938213-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I46f0651d-a9f2-4b96-8172-8104b0938213-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
            <keyFactSet>
                <keyFact id="Ib580dda7-0717-4cfc-b70e-34059b4c78f1-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ib580dda7-0717-4cfc-b70e-34059b4c78f1-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I834d38a6-dac5-4235-9cf4-d323dd89f1d2-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I834d38a6-dac5-4235-9cf4-d323dd89f1d2-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ic858ebcf-6f25-46ee-be0b-afbee1f41971-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ic858ebcf-6f25-46ee-be0b-afbee1f41971-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="If6a1b756-b293-4baf-a3c0-3332221e3ea8-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="If6a1b756-b293-4baf-a3c0-3332221e3ea8-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I80cb35ef-89d7-4ada-aef4-eda2b963a606-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I80cb35ef-89d7-4ada-aef4-eda2b963a606-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ia2c49323-f160-4dfa-8f79-5abea876efa6-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ia2c49323-f160-4dfa-8f79-5abea876efa6-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I8ecdc331-d565-4fcc-9f1f-a83e6e6bab9d-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I8ecdc331-d565-4fcc-9f1f-a83e6e6bab9d-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I4142aba3-f108-41dc-b537-ab44fd8cb5ae-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I4142aba3-f108-41dc-b537-ab44fd8cb5ae-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="If9dc68d4-33ec-4176-adb2-eda10373f1a0-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="If9dc68d4-33ec-4176-adb2-eda10373f1a0-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ice8f6ba6-0b24-4234-8e1c-ea2d6caf18f6-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ice8f6ba6-0b24-4234-8e1c-ea2d6caf18f6-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ia64d8e75-aa61-481b-bd40-df7270053448-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ia64d8e75-aa61-481b-bd40-df7270053448-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I2a787ac6-adce-419f-b230-293302ab6e2b-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I2a787ac6-adce-419f-b230-293302ab6e2b-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I562dc41e-1fcd-45b2-ad63-e346a9b12ed3-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I562dc41e-1fcd-45b2-ad63-e346a9b12ed3-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I2ff56de9-53fd-4be6-adc3-b9d48cbac0bd-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I2ff56de9-53fd-4be6-adc3-b9d48cbac0bd-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I8c49dcaf-eea6-48ad-bcf0-5c00ccde0415-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I8c49dcaf-eea6-48ad-bcf0-5c00ccde0415-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I7b431d95-a30e-403f-9b35-7575b30d31a3-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I7b431d95-a30e-403f-9b35-7575b30d31a3-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I46f0651d-a9f2-4b96-8172-8104b0938213-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I46f0651d-a9f2-4b96-8172-8104b0938213-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
            <keyFactSet>
                <keyFact id="Ib580dda7-0717-4cfc-b70e-34059b4c78f1-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ib580dda7-0717-4cfc-b70e-34059b4c78f1-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I834d38a6-dac5-4235-9cf4-d323dd89f1d2-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I834d38a6-dac5-4235-9cf4-d323dd89f1d2-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ic858ebcf-6f25-46ee-be0b-afbee1f41971-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ic858ebcf-6f25-46ee-be0b-afbee1f41971-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="If6a1b756-b293-4baf-a3c0-3332221e3ea8-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="If6a1b756-b293-4baf-a3c0-3332221e3ea8-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I80cb35ef-89d7-4ada-aef4-eda2b963a606-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I80cb35ef-89d7-4ada-aef4-eda2b963a606-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ia2c49323-f160-4dfa-8f79-5abea876efa6-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ia2c49323-f160-4dfa-8f79-5abea876efa6-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I8ecdc331-d565-4fcc-9f1f-a83e6e6bab9d-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I8ecdc331-d565-4fcc-9f1f-a83e6e6bab9d-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I4142aba3-f108-41dc-b537-ab44fd8cb5ae-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I4142aba3-f108-41dc-b537-ab44fd8cb5ae-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="If9dc68d4-33ec-4176-adb2-eda10373f1a0-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="If9dc68d4-33ec-4176-adb2-eda10373f1a0-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ice8f6ba6-0b24-4234-8e1c-ea2d6caf18f6-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ice8f6ba6-0b24-4234-8e1c-ea2d6caf18f6-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ia64d8e75-aa61-481b-bd40-df7270053448-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ia64d8e75-aa61-481b-bd40-df7270053448-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I2a787ac6-adce-419f-b230-293302ab6e2b-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I2a787ac6-adce-419f-b230-293302ab6e2b-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I562dc41e-1fcd-45b2-ad63-e346a9b12ed3-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I562dc41e-1fcd-45b2-ad63-e346a9b12ed3-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I2ff56de9-53fd-4be6-adc3-b9d48cbac0bd-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I2ff56de9-53fd-4be6-adc3-b9d48cbac0bd-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I8c49dcaf-eea6-48ad-bcf0-5c00ccde0415-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I8c49dcaf-eea6-48ad-bcf0-5c00ccde0415-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I7b431d95-a30e-403f-9b35-7575b30d31a3-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I7b431d95-a30e-403f-9b35-7575b30d31a3-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I46f0651d-a9f2-4b96-8172-8104b0938213-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I46f0651d-a9f2-4b96-8172-8104b0938213-hottextController" occur="1">
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
                <keyFact id="Ib580dda7-0717-4cfc-b70e-34059b4c78f1-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ib580dda7-0717-4cfc-b70e-34059b4c78f1-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I834d38a6-dac5-4235-9cf4-d323dd89f1d2-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I834d38a6-dac5-4235-9cf4-d323dd89f1d2-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ic858ebcf-6f25-46ee-be0b-afbee1f41971-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ic858ebcf-6f25-46ee-be0b-afbee1f41971-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="If6a1b756-b293-4baf-a3c0-3332221e3ea8-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="If6a1b756-b293-4baf-a3c0-3332221e3ea8-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I80cb35ef-89d7-4ada-aef4-eda2b963a606-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I80cb35ef-89d7-4ada-aef4-eda2b963a606-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ia2c49323-f160-4dfa-8f79-5abea876efa6-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ia2c49323-f160-4dfa-8f79-5abea876efa6-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I8ecdc331-d565-4fcc-9f1f-a83e6e6bab9d-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I8ecdc331-d565-4fcc-9f1f-a83e6e6bab9d-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I4142aba3-f108-41dc-b537-ab44fd8cb5ae-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I4142aba3-f108-41dc-b537-ab44fd8cb5ae-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="If9dc68d4-33ec-4176-adb2-eda10373f1a0-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="If9dc68d4-33ec-4176-adb2-eda10373f1a0-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ice8f6ba6-0b24-4234-8e1c-ea2d6caf18f6-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ice8f6ba6-0b24-4234-8e1c-ea2d6caf18f6-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ia64d8e75-aa61-481b-bd40-df7270053448-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ia64d8e75-aa61-481b-bd40-df7270053448-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I2a787ac6-adce-419f-b230-293302ab6e2b-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I2a787ac6-adce-419f-b230-293302ab6e2b-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I562dc41e-1fcd-45b2-ad63-e346a9b12ed3-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I562dc41e-1fcd-45b2-ad63-e346a9b12ed3-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I2ff56de9-53fd-4be6-adc3-b9d48cbac0bd-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I2ff56de9-53fd-4be6-adc3-b9d48cbac0bd-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I8c49dcaf-eea6-48ad-bcf0-5c00ccde0415-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I8c49dcaf-eea6-48ad-bcf0-5c00ccde0415-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I7b431d95-a30e-403f-9b35-7575b30d31a3-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I7b431d95-a30e-403f-9b35-7575b30d31a3-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I46f0651d-a9f2-4b96-8172-8104b0938213-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I46f0651d-a9f2-4b96-8172-8104b0938213-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
            <keyFactSet>
                <keyFact id="Ib580dda7-0717-4cfc-b70e-34059b4c78f1-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ib580dda7-0717-4cfc-b70e-34059b4c78f1-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I834d38a6-dac5-4235-9cf4-d323dd89f1d2-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I834d38a6-dac5-4235-9cf4-d323dd89f1d2-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ic858ebcf-6f25-46ee-be0b-afbee1f41971-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ic858ebcf-6f25-46ee-be0b-afbee1f41971-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="If6a1b756-b293-4baf-a3c0-3332221e3ea8-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="If6a1b756-b293-4baf-a3c0-3332221e3ea8-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I80cb35ef-89d7-4ada-aef4-eda2b963a606-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I80cb35ef-89d7-4ada-aef4-eda2b963a606-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ia2c49323-f160-4dfa-8f79-5abea876efa6-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ia2c49323-f160-4dfa-8f79-5abea876efa6-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I8ecdc331-d565-4fcc-9f1f-a83e6e6bab9d-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I8ecdc331-d565-4fcc-9f1f-a83e6e6bab9d-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I4142aba3-f108-41dc-b537-ab44fd8cb5ae-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I4142aba3-f108-41dc-b537-ab44fd8cb5ae-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="If9dc68d4-33ec-4176-adb2-eda10373f1a0-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="If9dc68d4-33ec-4176-adb2-eda10373f1a0-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ice8f6ba6-0b24-4234-8e1c-ea2d6caf18f6-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ice8f6ba6-0b24-4234-8e1c-ea2d6caf18f6-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ia64d8e75-aa61-481b-bd40-df7270053448-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ia64d8e75-aa61-481b-bd40-df7270053448-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I2a787ac6-adce-419f-b230-293302ab6e2b-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I2a787ac6-adce-419f-b230-293302ab6e2b-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I562dc41e-1fcd-45b2-ad63-e346a9b12ed3-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I562dc41e-1fcd-45b2-ad63-e346a9b12ed3-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I2ff56de9-53fd-4be6-adc3-b9d48cbac0bd-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I2ff56de9-53fd-4be6-adc3-b9d48cbac0bd-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I8c49dcaf-eea6-48ad-bcf0-5c00ccde0415-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I8c49dcaf-eea6-48ad-bcf0-5c00ccde0415-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I7b431d95-a30e-403f-9b35-7575b30d31a3-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I7b431d95-a30e-403f-9b35-7575b30d31a3-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I46f0651d-a9f2-4b96-8172-8104b0938213-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I46f0651d-a9f2-4b96-8172-8104b0938213-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
            <keyFactSet>
                <keyFact id="Ib580dda7-0717-4cfc-b70e-34059b4c78f1-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ib580dda7-0717-4cfc-b70e-34059b4c78f1-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I834d38a6-dac5-4235-9cf4-d323dd89f1d2-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I834d38a6-dac5-4235-9cf4-d323dd89f1d2-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ic858ebcf-6f25-46ee-be0b-afbee1f41971-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ic858ebcf-6f25-46ee-be0b-afbee1f41971-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="If6a1b756-b293-4baf-a3c0-3332221e3ea8-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="If6a1b756-b293-4baf-a3c0-3332221e3ea8-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I80cb35ef-89d7-4ada-aef4-eda2b963a606-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I80cb35ef-89d7-4ada-aef4-eda2b963a606-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ia2c49323-f160-4dfa-8f79-5abea876efa6-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ia2c49323-f160-4dfa-8f79-5abea876efa6-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I8ecdc331-d565-4fcc-9f1f-a83e6e6bab9d-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I8ecdc331-d565-4fcc-9f1f-a83e6e6bab9d-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I4142aba3-f108-41dc-b537-ab44fd8cb5ae-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I4142aba3-f108-41dc-b537-ab44fd8cb5ae-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="If9dc68d4-33ec-4176-adb2-eda10373f1a0-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="If9dc68d4-33ec-4176-adb2-eda10373f1a0-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ice8f6ba6-0b24-4234-8e1c-ea2d6caf18f6-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ice8f6ba6-0b24-4234-8e1c-ea2d6caf18f6-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ia64d8e75-aa61-481b-bd40-df7270053448-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ia64d8e75-aa61-481b-bd40-df7270053448-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I2a787ac6-adce-419f-b230-293302ab6e2b-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I2a787ac6-adce-419f-b230-293302ab6e2b-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I562dc41e-1fcd-45b2-ad63-e346a9b12ed3-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I562dc41e-1fcd-45b2-ad63-e346a9b12ed3-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I2ff56de9-53fd-4be6-adc3-b9d48cbac0bd-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I2ff56de9-53fd-4be6-adc3-b9d48cbac0bd-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I8c49dcaf-eea6-48ad-bcf0-5c00ccde0415-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I8c49dcaf-eea6-48ad-bcf0-5c00ccde0415-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I7b431d95-a30e-403f-9b35-7575b30d31a3-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I7b431d95-a30e-403f-9b35-7575b30d31a3-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I46f0651d-a9f2-4b96-8172-8104b0938213-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I46f0651d-a9f2-4b96-8172-8104b0938213-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
            <keyFactSet>
                <keyFact id="I1e40e819-c8a2-42dd-89de-dd29e070b99b-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I1e40e819-c8a2-42dd-89de-dd29e070b99b-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I67b6042e-7444-46be-b169-53ec421f9eda-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I67b6042e-7444-46be-b169-53ec421f9eda-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I18248834-8320-4342-967c-2007195ba14a-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I18248834-8320-4342-967c-2007195ba14a-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I0bf4350c-9288-4cec-9e37-1b0978b057ba-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I0bf4350c-9288-4cec-9e37-1b0978b057ba-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ifb1bfeea-20a0-4d29-bb87-d502ff355522-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ifb1bfeea-20a0-4d29-bb87-d502ff355522-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ie1f531cd-79c9-4b87-a90c-76766792b183-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ie1f531cd-79c9-4b87-a90c-76766792b183-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I66faad9a-d004-4cdc-a275-6f9ff736bb14-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I66faad9a-d004-4cdc-a275-6f9ff736bb14-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I665f2702-e926-4266-bc3c-3f04ea9a6c62-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I665f2702-e926-4266-bc3c-3f04ea9a6c62-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I7e8b004a-0223-432a-bb9a-536918190017-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I7e8b004a-0223-432a-bb9a-536918190017-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I217873b0-4970-405f-a80b-b2a9f42bbd85-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I217873b0-4970-405f-a80b-b2a9f42bbd85-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I46521597-f1f4-432c-96c2-8e3defb05950-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I46521597-f1f4-432c-96c2-8e3defb05950-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ifbea86b5-20bc-4dde-a4e4-4a14153c8ead-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ifbea86b5-20bc-4dde-a4e4-4a14153c8ead-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I279d3c47-95a8-42d1-9440-26fadf909090-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I279d3c47-95a8-42d1-9440-26fadf909090-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I6d80a45f-5042-4f42-99fe-151229268a43-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I6d80a45f-5042-4f42-99fe-151229268a43-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
            <keyFactSet>
                <keyFact id="I1e40e819-c8a2-42dd-89de-dd29e070b99b-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I1e40e819-c8a2-42dd-89de-dd29e070b99b-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I67b6042e-7444-46be-b169-53ec421f9eda-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I67b6042e-7444-46be-b169-53ec421f9eda-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I18248834-8320-4342-967c-2007195ba14a-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I18248834-8320-4342-967c-2007195ba14a-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I0bf4350c-9288-4cec-9e37-1b0978b057ba-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I0bf4350c-9288-4cec-9e37-1b0978b057ba-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ifb1bfeea-20a0-4d29-bb87-d502ff355522-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ifb1bfeea-20a0-4d29-bb87-d502ff355522-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ie1f531cd-79c9-4b87-a90c-76766792b183-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ie1f531cd-79c9-4b87-a90c-76766792b183-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I66faad9a-d004-4cdc-a275-6f9ff736bb14-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I66faad9a-d004-4cdc-a275-6f9ff736bb14-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I665f2702-e926-4266-bc3c-3f04ea9a6c62-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I665f2702-e926-4266-bc3c-3f04ea9a6c62-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I7e8b004a-0223-432a-bb9a-536918190017-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I7e8b004a-0223-432a-bb9a-536918190017-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I217873b0-4970-405f-a80b-b2a9f42bbd85-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I217873b0-4970-405f-a80b-b2a9f42bbd85-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I46521597-f1f4-432c-96c2-8e3defb05950-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I46521597-f1f4-432c-96c2-8e3defb05950-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ifbea86b5-20bc-4dde-a4e4-4a14153c8ead-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ifbea86b5-20bc-4dde-a4e4-4a14153c8ead-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I279d3c47-95a8-42d1-9440-26fadf909090-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I279d3c47-95a8-42d1-9440-26fadf909090-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I6d80a45f-5042-4f42-99fe-151229268a43-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I6d80a45f-5042-4f42-99fe-151229268a43-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
            <keyFactSet>
                <keyFact id="Iba0772fe-878f-457c-b652-a0e922626082-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Iba0772fe-878f-457c-b652-a0e922626082-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I5493e55f-fae9-499f-bd3e-16df52c028e1-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I5493e55f-fae9-499f-bd3e-16df52c028e1-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I7134c778-4641-40b5-8eae-1a80013cf777-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I7134c778-4641-40b5-8eae-1a80013cf777-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I3cb9edb6-551e-474a-9122-338b8c6a396d-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I3cb9edb6-551e-474a-9122-338b8c6a396d-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I41d5e633-d892-46fe-8a0c-ceba54c167f3-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I41d5e633-d892-46fe-8a0c-ceba54c167f3-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I498a96cd-114d-4299-b27c-639e41559e29-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I498a96cd-114d-4299-b27c-639e41559e29-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I19b64b54-f7f4-4284-af37-37999bd24935-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I19b64b54-f7f4-4284-af37-37999bd24935-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I7ea195a7-b0ec-465e-ada3-31c9e940f0dc-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I7ea195a7-b0ec-465e-ada3-31c9e940f0dc-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ibf683ce7-ac85-4610-b057-dd0578f3b71f-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ibf683ce7-ac85-4610-b057-dd0578f3b71f-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ie6f1fe1f-fe23-4f29-85ec-27d483138cfa-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ie6f1fe1f-fe23-4f29-85ec-27d483138cfa-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I2b4ed71d-8984-4ffd-a95a-126efbb12e48-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I2b4ed71d-8984-4ffd-a95a-126efbb12e48-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I0778d3a8-11c3-41f5-aeb7-4efd94338296-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I0778d3a8-11c3-41f5-aeb7-4efd94338296-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I3a1bf3a7-f9b4-472d-ac19-c6d27f671543-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I3a1bf3a7-f9b4-472d-ac19-c6d27f671543-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I01f17f48-6da9-44a0-bb7f-82e325f2f233-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I01f17f48-6da9-44a0-bb7f-82e325f2f233-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="Ifdbe8e9c-0eeb-4019-b8a7-5599265834fb-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="Ifdbe8e9c-0eeb-4019-b8a7-5599265834fb-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I0dd2d1e9-84b7-4b3f-a144-a2ec2c13061b-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I0dd2d1e9-84b7-4b3f-a144-a2ec2c13061b-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I41e6c74d-787a-4620-889f-fae84be71cdb-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I41e6c74d-787a-4620-889f-fae84be71cdb-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
                <keyFact id="I1f639c54-57ee-428e-93ed-77d786d5a2d1-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="I1f639c54-57ee-428e-93ed-77d786d5a2d1-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </keyValue>
                </keyFact>
            </keyFactSet>
        </keyFinding>

    Private _finding7 As XElement =
        <keyFinding id="hottextController" scoringMethod="Dichotomous">
            <keyFact id="Iba0772fe-878f-457c-b652-a0e922626082-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="Iba0772fe-878f-457c-b652-a0e922626082-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="If9dc68d4-33ec-4176-adb2-eda10373f1a0-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="If9dc68d4-33ec-4176-adb2-eda10373f1a0-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="Ice8f6ba6-0b24-4234-8e1c-ea2d6caf18f6-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="Ice8f6ba6-0b24-4234-8e1c-ea2d6caf18f6-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="Ia64d8e75-aa61-481b-bd40-df7270053448-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="Ia64d8e75-aa61-481b-bd40-df7270053448-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I2a787ac6-adce-419f-b230-293302ab6e2b-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I2a787ac6-adce-419f-b230-293302ab6e2b-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>true</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I562dc41e-1fcd-45b2-ad63-e346a9b12ed3-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I562dc41e-1fcd-45b2-ad63-e346a9b12ed3-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I2ff56de9-53fd-4be6-adc3-b9d48cbac0bd-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I2ff56de9-53fd-4be6-adc3-b9d48cbac0bd-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I8c49dcaf-eea6-48ad-bcf0-5c00ccde0415-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I8c49dcaf-eea6-48ad-bcf0-5c00ccde0415-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I7b431d95-a30e-403f-9b35-7575b30d31a3-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I7b431d95-a30e-403f-9b35-7575b30d31a3-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I46f0651d-a9f2-4b96-8172-8104b0938213-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I46f0651d-a9f2-4b96-8172-8104b0938213-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>true</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I1e40e819-c8a2-42dd-89de-dd29e070b99b-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I1e40e819-c8a2-42dd-89de-dd29e070b99b-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I67b6042e-7444-46be-b169-53ec421f9eda-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I67b6042e-7444-46be-b169-53ec421f9eda-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I18248834-8320-4342-967c-2007195ba14a-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I18248834-8320-4342-967c-2007195ba14a-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I0bf4350c-9288-4cec-9e37-1b0978b057ba-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I0bf4350c-9288-4cec-9e37-1b0978b057ba-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>true</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="Ifb1bfeea-20a0-4d29-bb87-d502ff355522-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="Ifb1bfeea-20a0-4d29-bb87-d502ff355522-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="Ie1f531cd-79c9-4b87-a90c-76766792b183-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="Ie1f531cd-79c9-4b87-a90c-76766792b183-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I66faad9a-d004-4cdc-a275-6f9ff736bb14-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I66faad9a-d004-4cdc-a275-6f9ff736bb14-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I665f2702-e926-4266-bc3c-3f04ea9a6c62-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I665f2702-e926-4266-bc3c-3f04ea9a6c62-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>true</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I7e8b004a-0223-432a-bb9a-536918190017-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I7e8b004a-0223-432a-bb9a-536918190017-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I217873b0-4970-405f-a80b-b2a9f42bbd85-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I217873b0-4970-405f-a80b-b2a9f42bbd85-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I46521597-f1f4-432c-96c2-8e3defb05950-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I46521597-f1f4-432c-96c2-8e3defb05950-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>true</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="Ifbea86b5-20bc-4dde-a4e4-4a14153c8ead-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="Ifbea86b5-20bc-4dde-a4e4-4a14153c8ead-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I4142aba3-f108-41dc-b537-ab44fd8cb5ae-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I4142aba3-f108-41dc-b537-ab44fd8cb5ae-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I279d3c47-95a8-42d1-9440-26fadf909090-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I279d3c47-95a8-42d1-9440-26fadf909090-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I8ecdc331-d565-4fcc-9f1f-a83e6e6bab9d-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I8ecdc331-d565-4fcc-9f1f-a83e6e6bab9d-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I80cb35ef-89d7-4ada-aef4-eda2b963a606-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I80cb35ef-89d7-4ada-aef4-eda2b963a606-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I5493e55f-fae9-499f-bd3e-16df52c028e1-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I5493e55f-fae9-499f-bd3e-16df52c028e1-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I7134c778-4641-40b5-8eae-1a80013cf777-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I7134c778-4641-40b5-8eae-1a80013cf777-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I3cb9edb6-551e-474a-9122-338b8c6a396d-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I3cb9edb6-551e-474a-9122-338b8c6a396d-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>true</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I41d5e633-d892-46fe-8a0c-ceba54c167f3-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I41d5e633-d892-46fe-8a0c-ceba54c167f3-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I498a96cd-114d-4299-b27c-639e41559e29-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I498a96cd-114d-4299-b27c-639e41559e29-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I19b64b54-f7f4-4284-af37-37999bd24935-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I19b64b54-f7f4-4284-af37-37999bd24935-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I7ea195a7-b0ec-465e-ada3-31c9e940f0dc-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I7ea195a7-b0ec-465e-ada3-31c9e940f0dc-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="Ibf683ce7-ac85-4610-b057-dd0578f3b71f-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="Ibf683ce7-ac85-4610-b057-dd0578f3b71f-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="Ie6f1fe1f-fe23-4f29-85ec-27d483138cfa-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="Ie6f1fe1f-fe23-4f29-85ec-27d483138cfa-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I2b4ed71d-8984-4ffd-a95a-126efbb12e48-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I2b4ed71d-8984-4ffd-a95a-126efbb12e48-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I0778d3a8-11c3-41f5-aeb7-4efd94338296-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I0778d3a8-11c3-41f5-aeb7-4efd94338296-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I3a1bf3a7-f9b4-472d-ac19-c6d27f671543-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I3a1bf3a7-f9b4-472d-ac19-c6d27f671543-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I01f17f48-6da9-44a0-bb7f-82e325f2f233-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I01f17f48-6da9-44a0-bb7f-82e325f2f233-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="Ifdbe8e9c-0eeb-4019-b8a7-5599265834fb-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="Ifdbe8e9c-0eeb-4019-b8a7-5599265834fb-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I0dd2d1e9-84b7-4b3f-a144-a2ec2c13061b-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I0dd2d1e9-84b7-4b3f-a144-a2ec2c13061b-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I41e6c74d-787a-4620-889f-fae84be71cdb-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I41e6c74d-787a-4620-889f-fae84be71cdb-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I1f639c54-57ee-428e-93ed-77d786d5a2d1-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I1f639c54-57ee-428e-93ed-77d786d5a2d1-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>true</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="Ib580dda7-0717-4cfc-b70e-34059b4c78f1-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="Ib580dda7-0717-4cfc-b70e-34059b4c78f1-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I834d38a6-dac5-4235-9cf4-d323dd89f1d2-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I834d38a6-dac5-4235-9cf4-d323dd89f1d2-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="Ic858ebcf-6f25-46ee-be0b-afbee1f41971-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="Ic858ebcf-6f25-46ee-be0b-afbee1f41971-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="If6a1b756-b293-4baf-a3c0-3332221e3ea8-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="If6a1b756-b293-4baf-a3c0-3332221e3ea8-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="Ia2c49323-f160-4dfa-8f79-5abea876efa6-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="Ia2c49323-f160-4dfa-8f79-5abea876efa6-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
            <keyFact id="I6d80a45f-5042-4f42-99fe-151229268a43-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <keyValue domain="I6d80a45f-5042-4f42-99fe-151229268a43-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </keyValue>
            </keyFact>
        </keyFinding>

    Private _responseProcessing1 As XElement =
        <responseProcessing>
            <responseCondition>
                <responseIf>
                    <or>
                        <and>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">Ib580dda7-0717-4cfc-b70e-34059b4c78f1</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I834d38a6-dac5-4235-9cf4-d323dd89f1d2</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">Ic858ebcf-6f25-46ee-be0b-afbee1f41971</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">If6a1b756-b293-4baf-a3c0-3332221e3ea8</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I80cb35ef-89d7-4ada-aef4-eda2b963a606</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">Ia2c49323-f160-4dfa-8f79-5abea876efa6</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I8ecdc331-d565-4fcc-9f1f-a83e6e6bab9d</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I4142aba3-f108-41dc-b537-ab44fd8cb5ae</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">If9dc68d4-33ec-4176-adb2-eda10373f1a0</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">Ice8f6ba6-0b24-4234-8e1c-ea2d6caf18f6</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">Ia64d8e75-aa61-481b-bd40-df7270053448</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <member>
                                <baseValue baseType="identifier">I2a787ac6-adce-419f-b230-293302ab6e2b</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I562dc41e-1fcd-45b2-ad63-e346a9b12ed3</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I2ff56de9-53fd-4be6-adc3-b9d48cbac0bd</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I8c49dcaf-eea6-48ad-bcf0-5c00ccde0415</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I7b431d95-a30e-403f-9b35-7575b30d31a3</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <member>
                                <baseValue baseType="identifier">I46f0651d-a9f2-4b96-8172-8104b0938213</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </and>
                        <and>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">Ib580dda7-0717-4cfc-b70e-34059b4c78f1</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I834d38a6-dac5-4235-9cf4-d323dd89f1d2</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">Ic858ebcf-6f25-46ee-be0b-afbee1f41971</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">If6a1b756-b293-4baf-a3c0-3332221e3ea8</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I80cb35ef-89d7-4ada-aef4-eda2b963a606</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">Ia2c49323-f160-4dfa-8f79-5abea876efa6</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I8ecdc331-d565-4fcc-9f1f-a83e6e6bab9d</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I4142aba3-f108-41dc-b537-ab44fd8cb5ae</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">If9dc68d4-33ec-4176-adb2-eda10373f1a0</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">Ice8f6ba6-0b24-4234-8e1c-ea2d6caf18f6</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">Ia64d8e75-aa61-481b-bd40-df7270053448</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I2a787ac6-adce-419f-b230-293302ab6e2b</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I562dc41e-1fcd-45b2-ad63-e346a9b12ed3</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I2ff56de9-53fd-4be6-adc3-b9d48cbac0bd</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I8c49dcaf-eea6-48ad-bcf0-5c00ccde0415</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I7b431d95-a30e-403f-9b35-7575b30d31a3</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <member>
                                <baseValue baseType="identifier">I46f0651d-a9f2-4b96-8172-8104b0938213</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </and>
                        <and>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">Ib580dda7-0717-4cfc-b70e-34059b4c78f1</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I834d38a6-dac5-4235-9cf4-d323dd89f1d2</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">Ic858ebcf-6f25-46ee-be0b-afbee1f41971</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">If6a1b756-b293-4baf-a3c0-3332221e3ea8</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I80cb35ef-89d7-4ada-aef4-eda2b963a606</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">Ia2c49323-f160-4dfa-8f79-5abea876efa6</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I8ecdc331-d565-4fcc-9f1f-a83e6e6bab9d</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I4142aba3-f108-41dc-b537-ab44fd8cb5ae</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">If9dc68d4-33ec-4176-adb2-eda10373f1a0</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">Ice8f6ba6-0b24-4234-8e1c-ea2d6caf18f6</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">Ia64d8e75-aa61-481b-bd40-df7270053448</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not
                            ><member>
                                <baseValue baseType="identifier">I2a787ac6-adce-419f-b230-293302ab6e2b</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I562dc41e-1fcd-45b2-ad63-e346a9b12ed3</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I2ff56de9-53fd-4be6-adc3-b9d48cbac0bd</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I8c49dcaf-eea6-48ad-bcf0-5c00ccde0415</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I7b431d95-a30e-403f-9b35-7575b30d31a3</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I46f0651d-a9f2-4b96-8172-8104b0938213</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                        </and>
                    </or>
                    <setOutcomeValue identifier="SCORE">
                        <sum>
                            <baseValue baseType="integer">1</baseValue>
                            <variable identifier="SCORE"/>
                        </sum>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>
            <responseCondition>
                <responseIf>
                    <member>
                        <baseValue baseType="identifier">I3cb9edb6-551e-474a-9122-338b8c6a396d</baseValue>
                        <variable identifier="RESPONSE"/>
                    </member>
                    <setOutcomeValue identifier="SCORE">
                        <sum>
                            <baseValue baseType="integer">1</baseValue>
                            <variable identifier="SCORE"/>
                        </sum>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>
            <responseCondition>
                <responseIf>
                    <member>
                        <baseValue baseType="identifier">I1f639c54-57ee-428e-93ed-77d786d5a2d1</baseValue>
                        <variable identifier="RESPONSE"/>
                    </member>
                    <setOutcomeValue identifier="SCORE">
                        <sum>
                            <baseValue baseType="integer">1</baseValue>
                            <variable identifier="SCORE"/>
                        </sum>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>
            <responseCondition>
                <responseIf>
                    <member>
                        <baseValue baseType="identifier">I0bf4350c-9288-4cec-9e37-1b0978b057ba</baseValue>
                        <variable identifier="RESPONSE"/>
                    </member>
                    <setOutcomeValue identifier="SCORE">
                        <sum>
                            <baseValue baseType="integer">1</baseValue>
                            <variable identifier="SCORE"/>
                        </sum>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>
            <responseCondition>
                <responseIf>
                    <member>
                        <baseValue baseType="identifier">I665f2702-e926-4266-bc3c-3f04ea9a6c62</baseValue>
                        <variable identifier="RESPONSE"/>
                    </member>
                    <setOutcomeValue identifier="SCORE">
                        <sum>
                            <baseValue baseType="integer">1</baseValue>
                            <variable identifier="SCORE"/>
                        </sum>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>
            <responseCondition>
                <responseIf>
                    <member>
                        <baseValue baseType="identifier">I46521597-f1f4-432c-96c2-8e3defb05950</baseValue>
                        <variable identifier="RESPONSE"/>
                    </member>
                    <setOutcomeValue identifier="SCORE">
                        <sum>
                            <baseValue baseType="integer">1</baseValue>
                            <variable identifier="SCORE"/>
                        </sum>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>
        </responseProcessing>

    Private _responseProcessing2 As XElement =
        <responseProcessing>
            <responseCondition>
                <responseIf>
                    <and>
                        <not>
                            <member>
                                <baseValue baseType="identifier">Iba0772fe-878f-457c-b652-a0e922626082</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <not>
                            <member>
                                <baseValue baseType="identifier">I5493e55f-fae9-499f-bd3e-16df52c028e1</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <not>
                            <member>
                                <baseValue baseType="identifier">I7134c778-4641-40b5-8eae-1a80013cf777</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <member>
                            <baseValue baseType="identifier">I3cb9edb6-551e-474a-9122-338b8c6a396d</baseValue>
                            <variable identifier="RESPONSE"/>
                        </member>
                        <not>
                            <member>
                                <baseValue baseType="identifier">I41d5e633-d892-46fe-8a0c-ceba54c167f3</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <not>
                            <member>
                                <baseValue baseType="identifier">I498a96cd-114d-4299-b27c-639e41559e29</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <not>
                            <member>
                                <baseValue baseType="identifier">I19b64b54-f7f4-4284-af37-37999bd24935</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <not>
                            <member>
                                <baseValue baseType="identifier">I7ea195a7-b0ec-465e-ada3-31c9e940f0dc</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <not>
                            <member>
                                <baseValue baseType="identifier">Ibf683ce7-ac85-4610-b057-dd0578f3b71f</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <not>
                            <member>
                                <baseValue baseType="identifier">Ie6f1fe1f-fe23-4f29-85ec-27d483138cfa</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <not>
                            <member>
                                <baseValue baseType="identifier">I2b4ed71d-8984-4ffd-a95a-126efbb12e48</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <not>
                            <member>
                                <baseValue baseType="identifier">I0778d3a8-11c3-41f5-aeb7-4efd94338296</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <not>
                            <member>
                                <baseValue baseType="identifier">I3a1bf3a7-f9b4-472d-ac19-c6d27f671543</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <not>
                            <member>
                                <baseValue baseType="identifier">I01f17f48-6da9-44a0-bb7f-82e325f2f233</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <not>
                            <member>
                                <baseValue baseType="identifier">Ifdbe8e9c-0eeb-4019-b8a7-5599265834fb</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <not>
                            <member>
                                <baseValue baseType="identifier">I0dd2d1e9-84b7-4b3f-a144-a2ec2c13061b</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <not>
                            <member>
                                <baseValue baseType="identifier">I41e6c74d-787a-4620-889f-fae84be71cdb</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <member>
                            <baseValue baseType="identifier">I1f639c54-57ee-428e-93ed-77d786d5a2d1</baseValue>
                            <variable identifier="RESPONSE"/>
                        </member>
                    </and>
                    <setOutcomeValue identifier="SCORE">
                        <sum>
                            <baseValue baseType="integer">1</baseValue>
                            <variable identifier="SCORE"/>
                        </sum>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>
            <responseCondition>
                <responseIf>
                    <or>
                        <and>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">Ib580dda7-0717-4cfc-b70e-34059b4c78f1</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I834d38a6-dac5-4235-9cf4-d323dd89f1d2</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">Ic858ebcf-6f25-46ee-be0b-afbee1f41971</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">If6a1b756-b293-4baf-a3c0-3332221e3ea8</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I80cb35ef-89d7-4ada-aef4-eda2b963a606</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">Ia2c49323-f160-4dfa-8f79-5abea876efa6</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I8ecdc331-d565-4fcc-9f1f-a83e6e6bab9d</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I4142aba3-f108-41dc-b537-ab44fd8cb5ae</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">If9dc68d4-33ec-4176-adb2-eda10373f1a0</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">Ice8f6ba6-0b24-4234-8e1c-ea2d6caf18f6</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">Ia64d8e75-aa61-481b-bd40-df7270053448</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <member>
                                <baseValue baseType="identifier">I2a787ac6-adce-419f-b230-293302ab6e2b</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I562dc41e-1fcd-45b2-ad63-e346a9b12ed3</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I2ff56de9-53fd-4be6-adc3-b9d48cbac0bd</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I8c49dcaf-eea6-48ad-bcf0-5c00ccde0415</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I7b431d95-a30e-403f-9b35-7575b30d31a3</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <member>
                                <baseValue baseType="identifier">I46f0651d-a9f2-4b96-8172-8104b0938213</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </and>
                        <and>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">Ib580dda7-0717-4cfc-b70e-34059b4c78f1</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I834d38a6-dac5-4235-9cf4-d323dd89f1d2</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">Ic858ebcf-6f25-46ee-be0b-afbee1f41971</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">If6a1b756-b293-4baf-a3c0-3332221e3ea8</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I80cb35ef-89d7-4ada-aef4-eda2b963a606</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">Ia2c49323-f160-4dfa-8f79-5abea876efa6</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I8ecdc331-d565-4fcc-9f1f-a83e6e6bab9d</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I4142aba3-f108-41dc-b537-ab44fd8cb5ae</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">If9dc68d4-33ec-4176-adb2-eda10373f1a0</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">Ice8f6ba6-0b24-4234-8e1c-ea2d6caf18f6</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">Ia64d8e75-aa61-481b-bd40-df7270053448</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I2a787ac6-adce-419f-b230-293302ab6e2b</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I562dc41e-1fcd-45b2-ad63-e346a9b12ed3</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I2ff56de9-53fd-4be6-adc3-b9d48cbac0bd</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I8c49dcaf-eea6-48ad-bcf0-5c00ccde0415</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I7b431d95-a30e-403f-9b35-7575b30d31a3</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <member>
                                <baseValue baseType="identifier">I46f0651d-a9f2-4b96-8172-8104b0938213</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </and>
                        <and>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">Ib580dda7-0717-4cfc-b70e-34059b4c78f1</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I834d38a6-dac5-4235-9cf4-d323dd89f1d2</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">Ic858ebcf-6f25-46ee-be0b-afbee1f41971</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">If6a1b756-b293-4baf-a3c0-3332221e3ea8</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I80cb35ef-89d7-4ada-aef4-eda2b963a606</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">Ia2c49323-f160-4dfa-8f79-5abea876efa6</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I8ecdc331-d565-4fcc-9f1f-a83e6e6bab9d</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I4142aba3-f108-41dc-b537-ab44fd8cb5ae</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">If9dc68d4-33ec-4176-adb2-eda10373f1a0</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">Ice8f6ba6-0b24-4234-8e1c-ea2d6caf18f6</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">Ia64d8e75-aa61-481b-bd40-df7270053448</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not
                            ><member>
                                <baseValue baseType="identifier">I2a787ac6-adce-419f-b230-293302ab6e2b</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I562dc41e-1fcd-45b2-ad63-e346a9b12ed3</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I2ff56de9-53fd-4be6-adc3-b9d48cbac0bd</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I8c49dcaf-eea6-48ad-bcf0-5c00ccde0415</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I7b431d95-a30e-403f-9b35-7575b30d31a3</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I46f0651d-a9f2-4b96-8172-8104b0938213</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                        </and>
                    </or>
                    <setOutcomeValue identifier="SCORE">
                        <sum>
                            <baseValue baseType="integer">1</baseValue>
                            <variable identifier="SCORE"/>
                        </sum>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>
            <responseCondition>
                <responseIf>
                    <or>
                        <and>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I1e40e819-c8a2-42dd-89de-dd29e070b99b</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I67b6042e-7444-46be-b169-53ec421f9eda</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I18248834-8320-4342-967c-2007195ba14a</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <member>
                                <baseValue baseType="identifier">I0bf4350c-9288-4cec-9e37-1b0978b057ba</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">Ifb1bfeea-20a0-4d29-bb87-d502ff355522</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">Ie1f531cd-79c9-4b87-a90c-76766792b183</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I66faad9a-d004-4cdc-a275-6f9ff736bb14</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <member>
                                <baseValue baseType="identifier">I665f2702-e926-4266-bc3c-3f04ea9a6c62</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I7e8b004a-0223-432a-bb9a-536918190017</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I217873b0-4970-405f-a80b-b2a9f42bbd85</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <member>
                                <baseValue baseType="identifier">I46521597-f1f4-432c-96c2-8e3defb05950</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">Ifbea86b5-20bc-4dde-a4e4-4a14153c8ead</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I279d3c47-95a8-42d1-9440-26fadf909090</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I6d80a45f-5042-4f42-99fe-151229268a43</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                        </and>
                        <and>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I1e40e819-c8a2-42dd-89de-dd29e070b99b</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I67b6042e-7444-46be-b169-53ec421f9eda</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I18248834-8320-4342-967c-2007195ba14a</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <member>
                                <baseValue baseType="identifier">I0bf4350c-9288-4cec-9e37-1b0978b057ba</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">Ifb1bfeea-20a0-4d29-bb87-d502ff355522</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">Ie1f531cd-79c9-4b87-a90c-76766792b183</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I66faad9a-d004-4cdc-a275-6f9ff736bb14</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <member>
                                <baseValue baseType="identifier">I665f2702-e926-4266-bc3c-3f04ea9a6c62</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <member>
                                <baseValue baseType="identifier">I7e8b004a-0223-432a-bb9a-536918190017</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I217873b0-4970-405f-a80b-b2a9f42bbd85</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <member>
                                <baseValue baseType="identifier">I46521597-f1f4-432c-96c2-8e3defb05950</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">Ifbea86b5-20bc-4dde-a4e4-4a14153c8ead</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I279d3c47-95a8-42d1-9440-26fadf909090</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I6d80a45f-5042-4f42-99fe-151229268a43</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                        </and>
                    </or>
                    <setOutcomeValue identifier="SCORE">
                        <sum>
                            <baseValue baseType="integer">1</baseValue>
                            <variable identifier="SCORE"/>
                        </sum>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>
        </responseProcessing>

    Private _responseProcessing3 As XElement =
        <responseProcessing>
            <responseCondition>
                <responseIf>
                    <or>
                        <and>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">Iba0772fe-878f-457c-b652-a0e922626082</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I5493e55f-fae9-499f-bd3e-16df52c028e1</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I7134c778-4641-40b5-8eae-1a80013cf777</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <member>
                                <baseValue baseType="identifier">I3cb9edb6-551e-474a-9122-338b8c6a396d</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I41d5e633-d892-46fe-8a0c-ceba54c167f3</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I498a96cd-114d-4299-b27c-639e41559e29</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I19b64b54-f7f4-4284-af37-37999bd24935</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I7ea195a7-b0ec-465e-ada3-31c9e940f0dc</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">Ibf683ce7-ac85-4610-b057-dd0578f3b71f</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">Ie6f1fe1f-fe23-4f29-85ec-27d483138cfa</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I2b4ed71d-8984-4ffd-a95a-126efbb12e48</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I0778d3a8-11c3-41f5-aeb7-4efd94338296</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I3a1bf3a7-f9b4-472d-ac19-c6d27f671543</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I01f17f48-6da9-44a0-bb7f-82e325f2f233</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">Ifdbe8e9c-0eeb-4019-b8a7-5599265834fb</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I0dd2d1e9-84b7-4b3f-a144-a2ec2c13061b</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I41e6c74d-787a-4620-889f-fae84be71cdb</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <member>
                                <baseValue baseType="identifier">I1f639c54-57ee-428e-93ed-77d786d5a2d1</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">Ib580dda7-0717-4cfc-b70e-34059b4c78f1</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I834d38a6-dac5-4235-9cf4-d323dd89f1d2</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">Ic858ebcf-6f25-46ee-be0b-afbee1f41971</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">If6a1b756-b293-4baf-a3c0-3332221e3ea8</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I80cb35ef-89d7-4ada-aef4-eda2b963a606</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">Ia2c49323-f160-4dfa-8f79-5abea876efa6</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I8ecdc331-d565-4fcc-9f1f-a83e6e6bab9d</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I4142aba3-f108-41dc-b537-ab44fd8cb5ae</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">If9dc68d4-33ec-4176-adb2-eda10373f1a0</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">Ice8f6ba6-0b24-4234-8e1c-ea2d6caf18f6</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">Ia64d8e75-aa61-481b-bd40-df7270053448</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <member>
                                <baseValue baseType="identifier">I2a787ac6-adce-419f-b230-293302ab6e2b</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I562dc41e-1fcd-45b2-ad63-e346a9b12ed3</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I2ff56de9-53fd-4be6-adc3-b9d48cbac0bd</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I8c49dcaf-eea6-48ad-bcf0-5c00ccde0415</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I7b431d95-a30e-403f-9b35-7575b30d31a3</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <member>
                                <baseValue baseType="identifier">I46f0651d-a9f2-4b96-8172-8104b0938213</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I1e40e819-c8a2-42dd-89de-dd29e070b99b</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I67b6042e-7444-46be-b169-53ec421f9eda</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I18248834-8320-4342-967c-2007195ba14a</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <member>
                                <baseValue baseType="identifier">I0bf4350c-9288-4cec-9e37-1b0978b057ba</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">Ifb1bfeea-20a0-4d29-bb87-d502ff355522</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">Ie1f531cd-79c9-4b87-a90c-76766792b183</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I66faad9a-d004-4cdc-a275-6f9ff736bb14</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <member>
                                <baseValue baseType="identifier">I665f2702-e926-4266-bc3c-3f04ea9a6c62</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I7e8b004a-0223-432a-bb9a-536918190017</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I217873b0-4970-405f-a80b-b2a9f42bbd85</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <member>
                                <baseValue baseType="identifier">I46521597-f1f4-432c-96c2-8e3defb05950</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">Ifbea86b5-20bc-4dde-a4e4-4a14153c8ead</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I279d3c47-95a8-42d1-9440-26fadf909090</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I6d80a45f-5042-4f42-99fe-151229268a43</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                        </and>
                        <and>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">Iba0772fe-878f-457c-b652-a0e922626082</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I5493e55f-fae9-499f-bd3e-16df52c028e1</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I7134c778-4641-40b5-8eae-1a80013cf777</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <member>
                                <baseValue baseType="identifier">I3cb9edb6-551e-474a-9122-338b8c6a396d</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I41d5e633-d892-46fe-8a0c-ceba54c167f3</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I498a96cd-114d-4299-b27c-639e41559e29</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I19b64b54-f7f4-4284-af37-37999bd24935</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I7ea195a7-b0ec-465e-ada3-31c9e940f0dc</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">Ibf683ce7-ac85-4610-b057-dd0578f3b71f</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">Ie6f1fe1f-fe23-4f29-85ec-27d483138cfa</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I2b4ed71d-8984-4ffd-a95a-126efbb12e48</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I0778d3a8-11c3-41f5-aeb7-4efd94338296</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I3a1bf3a7-f9b4-472d-ac19-c6d27f671543</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I01f17f48-6da9-44a0-bb7f-82e325f2f233</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">Ifdbe8e9c-0eeb-4019-b8a7-5599265834fb</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I0dd2d1e9-84b7-4b3f-a144-a2ec2c13061b</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I41e6c74d-787a-4620-889f-fae84be71cdb</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <member>
                                <baseValue baseType="identifier">I1f639c54-57ee-428e-93ed-77d786d5a2d1</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">Ib580dda7-0717-4cfc-b70e-34059b4c78f1</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I834d38a6-dac5-4235-9cf4-d323dd89f1d2</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">Ic858ebcf-6f25-46ee-be0b-afbee1f41971</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">If6a1b756-b293-4baf-a3c0-3332221e3ea8</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I80cb35ef-89d7-4ada-aef4-eda2b963a606</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <member>
                                <baseValue baseType="identifier">Ia2c49323-f160-4dfa-8f79-5abea876efa6</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I8ecdc331-d565-4fcc-9f1f-a83e6e6bab9d</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I4142aba3-f108-41dc-b537-ab44fd8cb5ae</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">If9dc68d4-33ec-4176-adb2-eda10373f1a0</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">Ice8f6ba6-0b24-4234-8e1c-ea2d6caf18f6</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">Ia64d8e75-aa61-481b-bd40-df7270053448</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I2a787ac6-adce-419f-b230-293302ab6e2b</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I562dc41e-1fcd-45b2-ad63-e346a9b12ed3</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I2ff56de9-53fd-4be6-adc3-b9d48cbac0bd</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I8c49dcaf-eea6-48ad-bcf0-5c00ccde0415</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I7b431d95-a30e-403f-9b35-7575b30d31a3</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <member>
                                <baseValue baseType="identifier">I46f0651d-a9f2-4b96-8172-8104b0938213</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I1e40e819-c8a2-42dd-89de-dd29e070b99b</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I67b6042e-7444-46be-b169-53ec421f9eda</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I18248834-8320-4342-967c-2007195ba14a</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I0bf4350c-9288-4cec-9e37-1b0978b057ba</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">Ifb1bfeea-20a0-4d29-bb87-d502ff355522</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">Ie1f531cd-79c9-4b87-a90c-76766792b183</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I66faad9a-d004-4cdc-a275-6f9ff736bb14</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <member>
                                <baseValue baseType="identifier">I665f2702-e926-4266-bc3c-3f04ea9a6c62</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I7e8b004a-0223-432a-bb9a-536918190017</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I217873b0-4970-405f-a80b-b2a9f42bbd85</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <member>
                                <baseValue baseType="identifier">I46521597-f1f4-432c-96c2-8e3defb05950</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">Ifbea86b5-20bc-4dde-a4e4-4a14153c8ead</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I279d3c47-95a8-42d1-9440-26fadf909090</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I6d80a45f-5042-4f42-99fe-151229268a43</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                        </and>
                    </or>
                    <setOutcomeValue identifier="SCORE">
                        <sum>
                            <baseValue baseType="integer">1</baseValue>
                            <variable identifier="SCORE"/>
                        </sum>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>
        </responseProcessing>

    Private _responseProcessing4 As XElement =
        <responseProcessing>
            <responseCondition>
                <responseIf>
                    <member>
                        <baseValue baseType="identifier">I3cb9edb6-551e-474a-9122-338b8c6a396d</baseValue>
                        <variable identifier="RESPONSE"/>
                    </member>
                    <setOutcomeValue identifier="SCORE">
                        <sum>
                            <baseValue baseType="integer">1</baseValue>
                            <variable identifier="SCORE"/>
                        </sum>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>
            <responseCondition>
                <responseIf>
                    <member>
                        <baseValue baseType="identifier">I1f639c54-57ee-428e-93ed-77d786d5a2d1</baseValue>
                        <variable identifier="RESPONSE"/>
                    </member>
                    <setOutcomeValue identifier="SCORE">
                        <sum>
                            <baseValue baseType="integer">1</baseValue>
                            <variable identifier="SCORE"/>
                        </sum>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>
            <responseCondition>
                <responseIf>
                    <member>
                        <baseValue baseType="identifier">I2a787ac6-adce-419f-b230-293302ab6e2b</baseValue>
                        <variable identifier="RESPONSE"/>
                    </member>
                    <setOutcomeValue identifier="SCORE">
                        <sum>
                            <baseValue baseType="integer">1</baseValue>
                            <variable identifier="SCORE"/>
                        </sum>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>
            <responseCondition>
                <responseIf>
                    <member>
                        <baseValue baseType="identifier">I46f0651d-a9f2-4b96-8172-8104b0938213</baseValue>
                        <variable identifier="RESPONSE"/>
                    </member>
                    <setOutcomeValue identifier="SCORE">
                        <sum>
                            <baseValue baseType="integer">1</baseValue>
                            <variable identifier="SCORE"/>
                        </sum>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>
            <responseCondition>
                <responseIf>
                    <member>
                        <baseValue baseType="identifier">I0bf4350c-9288-4cec-9e37-1b0978b057ba</baseValue>
                        <variable identifier="RESPONSE"/>
                    </member>
                    <setOutcomeValue identifier="SCORE">
                        <sum>
                            <baseValue baseType="integer">1</baseValue>
                            <variable identifier="SCORE"/>
                        </sum>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>
            <responseCondition>
                <responseIf>
                    <member>
                        <baseValue baseType="identifier">I665f2702-e926-4266-bc3c-3f04ea9a6c62</baseValue>
                        <variable identifier="RESPONSE"/>
                    </member>
                    <setOutcomeValue identifier="SCORE">
                        <sum>
                            <baseValue baseType="integer">1</baseValue>
                            <variable identifier="SCORE"/>
                        </sum>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>
            <responseCondition>
                <responseIf>
                    <member>
                        <baseValue baseType="identifier">I46521597-f1f4-432c-96c2-8e3defb05950</baseValue>
                        <variable identifier="RESPONSE"/>
                    </member>
                    <setOutcomeValue identifier="SCORE">
                        <sum>
                            <baseValue baseType="integer">1</baseValue>
                            <variable identifier="SCORE"/>
                        </sum>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>
        </responseProcessing>

    Private _responseProcessing5 As XElement =
        <responseProcessing>
            <responseCondition>
                <responseIf>
                    <and>
                        <or>
                            <and>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">Ib580dda7-0717-4cfc-b70e-34059b4c78f1</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">I834d38a6-dac5-4235-9cf4-d323dd89f1d2</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">Ic858ebcf-6f25-46ee-be0b-afbee1f41971</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">If6a1b756-b293-4baf-a3c0-3332221e3ea8</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">I80cb35ef-89d7-4ada-aef4-eda2b963a606</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">Ia2c49323-f160-4dfa-8f79-5abea876efa6</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">I8ecdc331-d565-4fcc-9f1f-a83e6e6bab9d</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">I4142aba3-f108-41dc-b537-ab44fd8cb5ae</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">If9dc68d4-33ec-4176-adb2-eda10373f1a0</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">Ice8f6ba6-0b24-4234-8e1c-ea2d6caf18f6</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">Ia64d8e75-aa61-481b-bd40-df7270053448</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <member>
                                    <baseValue baseType="identifier">I2a787ac6-adce-419f-b230-293302ab6e2b</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">I562dc41e-1fcd-45b2-ad63-e346a9b12ed3</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">I2ff56de9-53fd-4be6-adc3-b9d48cbac0bd</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">I8c49dcaf-eea6-48ad-bcf0-5c00ccde0415</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">I7b431d95-a30e-403f-9b35-7575b30d31a3</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <member>
                                    <baseValue baseType="identifier">I46f0651d-a9f2-4b96-8172-8104b0938213</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </and>
                            <and>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">Ib580dda7-0717-4cfc-b70e-34059b4c78f1</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">I834d38a6-dac5-4235-9cf4-d323dd89f1d2</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">Ic858ebcf-6f25-46ee-be0b-afbee1f41971</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">If6a1b756-b293-4baf-a3c0-3332221e3ea8</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">I80cb35ef-89d7-4ada-aef4-eda2b963a606</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">Ia2c49323-f160-4dfa-8f79-5abea876efa6</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">I8ecdc331-d565-4fcc-9f1f-a83e6e6bab9d</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">I4142aba3-f108-41dc-b537-ab44fd8cb5ae</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">If9dc68d4-33ec-4176-adb2-eda10373f1a0</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">Ice8f6ba6-0b24-4234-8e1c-ea2d6caf18f6</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">Ia64d8e75-aa61-481b-bd40-df7270053448</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">I2a787ac6-adce-419f-b230-293302ab6e2b</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">I562dc41e-1fcd-45b2-ad63-e346a9b12ed3</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">I2ff56de9-53fd-4be6-adc3-b9d48cbac0bd</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">I8c49dcaf-eea6-48ad-bcf0-5c00ccde0415</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">I7b431d95-a30e-403f-9b35-7575b30d31a3</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <member>
                                    <baseValue baseType="identifier">I46f0651d-a9f2-4b96-8172-8104b0938213</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </and>
                            <and>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">Ib580dda7-0717-4cfc-b70e-34059b4c78f1</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">I834d38a6-dac5-4235-9cf4-d323dd89f1d2</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">Ic858ebcf-6f25-46ee-be0b-afbee1f41971</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">If6a1b756-b293-4baf-a3c0-3332221e3ea8</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">I80cb35ef-89d7-4ada-aef4-eda2b963a606</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">Ia2c49323-f160-4dfa-8f79-5abea876efa6</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">I8ecdc331-d565-4fcc-9f1f-a83e6e6bab9d</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">I4142aba3-f108-41dc-b537-ab44fd8cb5ae</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">If9dc68d4-33ec-4176-adb2-eda10373f1a0</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">Ice8f6ba6-0b24-4234-8e1c-ea2d6caf18f6</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">Ia64d8e75-aa61-481b-bd40-df7270053448</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not
                                ><member>
                                    <baseValue baseType="identifier">I2a787ac6-adce-419f-b230-293302ab6e2b</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">I562dc41e-1fcd-45b2-ad63-e346a9b12ed3</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">I2ff56de9-53fd-4be6-adc3-b9d48cbac0bd</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">I8c49dcaf-eea6-48ad-bcf0-5c00ccde0415</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">I7b431d95-a30e-403f-9b35-7575b30d31a3</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">I46f0651d-a9f2-4b96-8172-8104b0938213</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                            </and>
                        </or>
                        <not>
                            <member>
                                <baseValue baseType="identifier">Iba0772fe-878f-457c-b652-a0e922626082</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <not>
                            <member>
                                <baseValue baseType="identifier">I5493e55f-fae9-499f-bd3e-16df52c028e1</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <not>
                            <member>
                                <baseValue baseType="identifier">I7134c778-4641-40b5-8eae-1a80013cf777</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <member>
                            <baseValue baseType="identifier">I3cb9edb6-551e-474a-9122-338b8c6a396d</baseValue>
                            <variable identifier="RESPONSE"/>
                        </member>
                        <not>
                            <member>
                                <baseValue baseType="identifier">I41d5e633-d892-46fe-8a0c-ceba54c167f3</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <not>
                            <member>
                                <baseValue baseType="identifier">I498a96cd-114d-4299-b27c-639e41559e29</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <not>
                            <member>
                                <baseValue baseType="identifier">I19b64b54-f7f4-4284-af37-37999bd24935</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <not>
                            <member>
                                <baseValue baseType="identifier">I7ea195a7-b0ec-465e-ada3-31c9e940f0dc</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <not>
                            <member>
                                <baseValue baseType="identifier">Ibf683ce7-ac85-4610-b057-dd0578f3b71f</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <not>
                            <member>
                                <baseValue baseType="identifier">Ie6f1fe1f-fe23-4f29-85ec-27d483138cfa</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <not>
                            <member>
                                <baseValue baseType="identifier">I2b4ed71d-8984-4ffd-a95a-126efbb12e48</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <not>
                            <member>
                                <baseValue baseType="identifier">I0778d3a8-11c3-41f5-aeb7-4efd94338296</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <not>
                            <member>
                                <baseValue baseType="identifier">I3a1bf3a7-f9b4-472d-ac19-c6d27f671543</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <not>
                            <member>
                                <baseValue baseType="identifier">I01f17f48-6da9-44a0-bb7f-82e325f2f233</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <not>
                            <member>
                                <baseValue baseType="identifier">Ifdbe8e9c-0eeb-4019-b8a7-5599265834fb</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <not>
                            <member>
                                <baseValue baseType="identifier">I0dd2d1e9-84b7-4b3f-a144-a2ec2c13061b</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <not>
                            <member>
                                <baseValue baseType="identifier">I41e6c74d-787a-4620-889f-fae84be71cdb</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <member>
                            <baseValue baseType="identifier">I1f639c54-57ee-428e-93ed-77d786d5a2d1</baseValue>
                            <variable identifier="RESPONSE"/>
                        </member>
                        <not>
                            <member>
                                <baseValue baseType="identifier">I1e40e819-c8a2-42dd-89de-dd29e070b99b</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <not>
                            <member>
                                <baseValue baseType="identifier">I67b6042e-7444-46be-b169-53ec421f9eda</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <not>
                            <member>
                                <baseValue baseType="identifier">I18248834-8320-4342-967c-2007195ba14a</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <member>
                            <baseValue baseType="identifier">I0bf4350c-9288-4cec-9e37-1b0978b057ba</baseValue>
                            <variable identifier="RESPONSE"/>
                        </member>
                        <not>
                            <member>
                                <baseValue baseType="identifier">Ifb1bfeea-20a0-4d29-bb87-d502ff355522</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <not>
                            <member>
                                <baseValue baseType="identifier">Ie1f531cd-79c9-4b87-a90c-76766792b183</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <not>
                            <member>
                                <baseValue baseType="identifier">I66faad9a-d004-4cdc-a275-6f9ff736bb14</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <member>
                            <baseValue baseType="identifier">I665f2702-e926-4266-bc3c-3f04ea9a6c62</baseValue>
                            <variable identifier="RESPONSE"/>
                        </member>
                        <not>
                            <member>
                                <baseValue baseType="identifier">I7e8b004a-0223-432a-bb9a-536918190017</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <not>
                            <member>
                                <baseValue baseType="identifier">I217873b0-4970-405f-a80b-b2a9f42bbd85</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <member>
                            <baseValue baseType="identifier">I46521597-f1f4-432c-96c2-8e3defb05950</baseValue>
                            <variable identifier="RESPONSE"/>
                        </member>
                        <not>
                            <member>
                                <baseValue baseType="identifier">Ifbea86b5-20bc-4dde-a4e4-4a14153c8ead</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <not>
                            <member>
                                <baseValue baseType="identifier">I279d3c47-95a8-42d1-9440-26fadf909090</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <not>
                            <member>
                                <baseValue baseType="identifier">I6d80a45f-5042-4f42-99fe-151229268a43</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                    </and>
                    <setOutcomeValue identifier="SCORE">
                        <sum>
                            <baseValue baseType="integer">1</baseValue>
                            <variable identifier="SCORE"/>
                        </sum>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>
            <responseCondition>
                <responseIf>
                    <gte>
                        <variable identifier="SCORE"/>
                        <baseValue baseType="integer">1</baseValue>
                    </gte>
                    <setOutcomeValue identifier="SCORE">
                        <baseValue baseType="integer">1</baseValue>
                    </setOutcomeValue>
                </responseIf>
                <responseElse>
                    <setOutcomeValue identifier="SCORE">
                        <baseValue baseType="integer">0</baseValue>
                    </setOutcomeValue>
                </responseElse>
            </responseCondition>
        </responseProcessing>

    Private _responseProcessing6 As XElement =
        <responseProcessing>
            <responseCondition>
                <responseIf>
                    <and>
                        <and>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">Iba0772fe-878f-457c-b652-a0e922626082</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I5493e55f-fae9-499f-bd3e-16df52c028e1</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I7134c778-4641-40b5-8eae-1a80013cf777</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <member>
                                <baseValue baseType="identifier">I3cb9edb6-551e-474a-9122-338b8c6a396d</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I41d5e633-d892-46fe-8a0c-ceba54c167f3</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I498a96cd-114d-4299-b27c-639e41559e29</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I19b64b54-f7f4-4284-af37-37999bd24935</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I7ea195a7-b0ec-465e-ada3-31c9e940f0dc</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">Ibf683ce7-ac85-4610-b057-dd0578f3b71f</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">Ie6f1fe1f-fe23-4f29-85ec-27d483138cfa</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I2b4ed71d-8984-4ffd-a95a-126efbb12e48</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I0778d3a8-11c3-41f5-aeb7-4efd94338296</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I3a1bf3a7-f9b4-472d-ac19-c6d27f671543</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I01f17f48-6da9-44a0-bb7f-82e325f2f233</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">Ifdbe8e9c-0eeb-4019-b8a7-5599265834fb</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I0dd2d1e9-84b7-4b3f-a144-a2ec2c13061b</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <not>
                                <member>
                                    <baseValue baseType="identifier">I41e6c74d-787a-4620-889f-fae84be71cdb</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </not>
                            <member>
                                <baseValue baseType="identifier">I1f639c54-57ee-428e-93ed-77d786d5a2d1</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </and>
                        <or>
                            <and>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">Ib580dda7-0717-4cfc-b70e-34059b4c78f1</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">I834d38a6-dac5-4235-9cf4-d323dd89f1d2</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">Ic858ebcf-6f25-46ee-be0b-afbee1f41971</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">If6a1b756-b293-4baf-a3c0-3332221e3ea8</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">I80cb35ef-89d7-4ada-aef4-eda2b963a606</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">Ia2c49323-f160-4dfa-8f79-5abea876efa6</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">I8ecdc331-d565-4fcc-9f1f-a83e6e6bab9d</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">I4142aba3-f108-41dc-b537-ab44fd8cb5ae</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">If9dc68d4-33ec-4176-adb2-eda10373f1a0</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">Ice8f6ba6-0b24-4234-8e1c-ea2d6caf18f6</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">Ia64d8e75-aa61-481b-bd40-df7270053448</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <member>
                                    <baseValue baseType="identifier">I2a787ac6-adce-419f-b230-293302ab6e2b</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">I562dc41e-1fcd-45b2-ad63-e346a9b12ed3</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">I2ff56de9-53fd-4be6-adc3-b9d48cbac0bd</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">I8c49dcaf-eea6-48ad-bcf0-5c00ccde0415</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">I7b431d95-a30e-403f-9b35-7575b30d31a3</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <member>
                                    <baseValue baseType="identifier">I46f0651d-a9f2-4b96-8172-8104b0938213</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </and>
                            <and>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">Ib580dda7-0717-4cfc-b70e-34059b4c78f1</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">I834d38a6-dac5-4235-9cf4-d323dd89f1d2</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">Ic858ebcf-6f25-46ee-be0b-afbee1f41971</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">If6a1b756-b293-4baf-a3c0-3332221e3ea8</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">I80cb35ef-89d7-4ada-aef4-eda2b963a606</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">Ia2c49323-f160-4dfa-8f79-5abea876efa6</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">I8ecdc331-d565-4fcc-9f1f-a83e6e6bab9d</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">I4142aba3-f108-41dc-b537-ab44fd8cb5ae</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">If9dc68d4-33ec-4176-adb2-eda10373f1a0</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">Ice8f6ba6-0b24-4234-8e1c-ea2d6caf18f6</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">Ia64d8e75-aa61-481b-bd40-df7270053448</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">I2a787ac6-adce-419f-b230-293302ab6e2b</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">I562dc41e-1fcd-45b2-ad63-e346a9b12ed3</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">I2ff56de9-53fd-4be6-adc3-b9d48cbac0bd</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">I8c49dcaf-eea6-48ad-bcf0-5c00ccde0415</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">I7b431d95-a30e-403f-9b35-7575b30d31a3</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <member>
                                    <baseValue baseType="identifier">I46f0651d-a9f2-4b96-8172-8104b0938213</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                            </and>
                            <and>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">Ib580dda7-0717-4cfc-b70e-34059b4c78f1</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">I834d38a6-dac5-4235-9cf4-d323dd89f1d2</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">Ic858ebcf-6f25-46ee-be0b-afbee1f41971</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">If6a1b756-b293-4baf-a3c0-3332221e3ea8</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">I80cb35ef-89d7-4ada-aef4-eda2b963a606</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">Ia2c49323-f160-4dfa-8f79-5abea876efa6</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">I8ecdc331-d565-4fcc-9f1f-a83e6e6bab9d</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">I4142aba3-f108-41dc-b537-ab44fd8cb5ae</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">If9dc68d4-33ec-4176-adb2-eda10373f1a0</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">Ice8f6ba6-0b24-4234-8e1c-ea2d6caf18f6</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">Ia64d8e75-aa61-481b-bd40-df7270053448</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not
                                ><member>
                                    <baseValue baseType="identifier">I2a787ac6-adce-419f-b230-293302ab6e2b</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">I562dc41e-1fcd-45b2-ad63-e346a9b12ed3</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">I2ff56de9-53fd-4be6-adc3-b9d48cbac0bd</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">I8c49dcaf-eea6-48ad-bcf0-5c00ccde0415</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">I7b431d95-a30e-403f-9b35-7575b30d31a3</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">I46f0651d-a9f2-4b96-8172-8104b0938213</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                            </and>
                        </or>
                        <or>
                            <and>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">I1e40e819-c8a2-42dd-89de-dd29e070b99b</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">I67b6042e-7444-46be-b169-53ec421f9eda</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">I18248834-8320-4342-967c-2007195ba14a</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <member>
                                    <baseValue baseType="identifier">I0bf4350c-9288-4cec-9e37-1b0978b057ba</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">Ifb1bfeea-20a0-4d29-bb87-d502ff355522</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">Ie1f531cd-79c9-4b87-a90c-76766792b183</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">I66faad9a-d004-4cdc-a275-6f9ff736bb14</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <member>
                                    <baseValue baseType="identifier">I665f2702-e926-4266-bc3c-3f04ea9a6c62</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">I7e8b004a-0223-432a-bb9a-536918190017</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">I217873b0-4970-405f-a80b-b2a9f42bbd85</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <member>
                                    <baseValue baseType="identifier">I46521597-f1f4-432c-96c2-8e3defb05950</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">Ifbea86b5-20bc-4dde-a4e4-4a14153c8ead</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">I279d3c47-95a8-42d1-9440-26fadf909090</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">I6d80a45f-5042-4f42-99fe-151229268a43</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                            </and>
                            <and>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">I1e40e819-c8a2-42dd-89de-dd29e070b99b</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">I67b6042e-7444-46be-b169-53ec421f9eda</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">I18248834-8320-4342-967c-2007195ba14a</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <member>
                                    <baseValue baseType="identifier">I0bf4350c-9288-4cec-9e37-1b0978b057ba</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">Ifb1bfeea-20a0-4d29-bb87-d502ff355522</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">Ie1f531cd-79c9-4b87-a90c-76766792b183</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">I66faad9a-d004-4cdc-a275-6f9ff736bb14</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <member>
                                    <baseValue baseType="identifier">I665f2702-e926-4266-bc3c-3f04ea9a6c62</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                                <member>
                                    <baseValue baseType="identifier">I7e8b004a-0223-432a-bb9a-536918190017</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">I217873b0-4970-405f-a80b-b2a9f42bbd85</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <member>
                                    <baseValue baseType="identifier">I46521597-f1f4-432c-96c2-8e3defb05950</baseValue>
                                    <variable identifier="RESPONSE"/>
                                </member>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">Ifbea86b5-20bc-4dde-a4e4-4a14153c8ead</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">I279d3c47-95a8-42d1-9440-26fadf909090</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                                <not>
                                    <member>
                                        <baseValue baseType="identifier">I6d80a45f-5042-4f42-99fe-151229268a43</baseValue>
                                        <variable identifier="RESPONSE"/>
                                    </member>
                                </not>
                            </and>
                        </or>
                    </and>
                    <setOutcomeValue identifier="SCORE">
                        <sum>
                            <baseValue baseType="integer">1</baseValue>
                            <variable identifier="SCORE"/>
                        </sum>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>
            <responseCondition>
                <responseIf>
                    <gte>
                        <variable identifier="SCORE"/>
                        <baseValue baseType="integer">1</baseValue>
                    </gte>
                    <setOutcomeValue identifier="SCORE">
                        <baseValue baseType="integer">1</baseValue>
                    </setOutcomeValue>
                </responseIf>
                <responseElse>
                    <setOutcomeValue identifier="SCORE">
                        <baseValue baseType="integer">0</baseValue>
                    </setOutcomeValue>
                </responseElse>
            </responseCondition>
        </responseProcessing>

    Private _responseProcessing7 As XElement =
        <responseProcessing>
            <responseCondition>
                <responseIf>
                    <and>
                        <not>
                            <member>
                                <baseValue baseType="identifier">Iba0772fe-878f-457c-b652-a0e922626082</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <not>
                            <member>
                                <baseValue baseType="identifier">I5493e55f-fae9-499f-bd3e-16df52c028e1</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <not>
                            <member>
                                <baseValue baseType="identifier">I7134c778-4641-40b5-8eae-1a80013cf777</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <member>
                            <baseValue baseType="identifier">I3cb9edb6-551e-474a-9122-338b8c6a396d</baseValue>
                            <variable identifier="RESPONSE"/>
                        </member>
                        <not>
                            <member>
                                <baseValue baseType="identifier">I41d5e633-d892-46fe-8a0c-ceba54c167f3</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <not>
                            <member>
                                <baseValue baseType="identifier">I498a96cd-114d-4299-b27c-639e41559e29</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <not>
                            <member>
                                <baseValue baseType="identifier">I19b64b54-f7f4-4284-af37-37999bd24935</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <not>
                            <member>
                                <baseValue baseType="identifier">I7ea195a7-b0ec-465e-ada3-31c9e940f0dc</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <not>
                            <member>
                                <baseValue baseType="identifier">Ibf683ce7-ac85-4610-b057-dd0578f3b71f</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <not>
                            <member>
                                <baseValue baseType="identifier">Ie6f1fe1f-fe23-4f29-85ec-27d483138cfa</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <not>
                            <member>
                                <baseValue baseType="identifier">I2b4ed71d-8984-4ffd-a95a-126efbb12e48</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <not>
                            <member>
                                <baseValue baseType="identifier">I0778d3a8-11c3-41f5-aeb7-4efd94338296</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <not>
                            <member>
                                <baseValue baseType="identifier">I3a1bf3a7-f9b4-472d-ac19-c6d27f671543</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <not>
                            <member>
                                <baseValue baseType="identifier">I01f17f48-6da9-44a0-bb7f-82e325f2f233</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <not>
                            <member>
                                <baseValue baseType="identifier">Ifdbe8e9c-0eeb-4019-b8a7-5599265834fb</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <not>
                            <member>
                                <baseValue baseType="identifier">I0dd2d1e9-84b7-4b3f-a144-a2ec2c13061b</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <not>
                            <member>
                                <baseValue baseType="identifier">I41e6c74d-787a-4620-889f-fae84be71cdb</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <member>
                            <baseValue baseType="identifier">I1f639c54-57ee-428e-93ed-77d786d5a2d1</baseValue>
                            <variable identifier="RESPONSE"/>
                        </member>
                        <not>
                            <member>
                                <baseValue baseType="identifier">Ib580dda7-0717-4cfc-b70e-34059b4c78f1</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <not>
                            <member>
                                <baseValue baseType="identifier">I834d38a6-dac5-4235-9cf4-d323dd89f1d2</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <not>
                            <member>
                                <baseValue baseType="identifier">Ic858ebcf-6f25-46ee-be0b-afbee1f41971</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <not>
                            <member>
                                <baseValue baseType="identifier">If6a1b756-b293-4baf-a3c0-3332221e3ea8</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <not>
                            <member>
                                <baseValue baseType="identifier">I80cb35ef-89d7-4ada-aef4-eda2b963a606</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <not>
                            <member>
                                <baseValue baseType="identifier">Ia2c49323-f160-4dfa-8f79-5abea876efa6</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <not>
                            <member>
                                <baseValue baseType="identifier">I8ecdc331-d565-4fcc-9f1f-a83e6e6bab9d</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <not>
                            <member>
                                <baseValue baseType="identifier">I4142aba3-f108-41dc-b537-ab44fd8cb5ae</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <not>
                            <member>
                                <baseValue baseType="identifier">If9dc68d4-33ec-4176-adb2-eda10373f1a0</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <not>
                            <member>
                                <baseValue baseType="identifier">Ice8f6ba6-0b24-4234-8e1c-ea2d6caf18f6</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <not>
                            <member>
                                <baseValue baseType="identifier">Ia64d8e75-aa61-481b-bd40-df7270053448</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <member>
                            <baseValue baseType="identifier">I2a787ac6-adce-419f-b230-293302ab6e2b</baseValue>
                            <variable identifier="RESPONSE"/>
                        </member>
                        <not>
                            <member>
                                <baseValue baseType="identifier">I562dc41e-1fcd-45b2-ad63-e346a9b12ed3</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <not>
                            <member>
                                <baseValue baseType="identifier">I2ff56de9-53fd-4be6-adc3-b9d48cbac0bd</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <not>
                            <member>
                                <baseValue baseType="identifier">I8c49dcaf-eea6-48ad-bcf0-5c00ccde0415</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <not>
                            <member>
                                <baseValue baseType="identifier">I7b431d95-a30e-403f-9b35-7575b30d31a3</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <member>
                            <baseValue baseType="identifier">I46f0651d-a9f2-4b96-8172-8104b0938213</baseValue>
                            <variable identifier="RESPONSE"/>
                        </member>
                        <not>
                            <member>
                                <baseValue baseType="identifier">I1e40e819-c8a2-42dd-89de-dd29e070b99b</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <not>
                            <member>
                                <baseValue baseType="identifier">I67b6042e-7444-46be-b169-53ec421f9eda</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <not>
                            <member>
                                <baseValue baseType="identifier">I18248834-8320-4342-967c-2007195ba14a</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <member>
                            <baseValue baseType="identifier">I0bf4350c-9288-4cec-9e37-1b0978b057ba</baseValue>
                            <variable identifier="RESPONSE"/>
                        </member>
                        <not>
                            <member>
                                <baseValue baseType="identifier">Ifb1bfeea-20a0-4d29-bb87-d502ff355522</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <not>
                            <member>
                                <baseValue baseType="identifier">Ie1f531cd-79c9-4b87-a90c-76766792b183</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <not>
                            <member>
                                <baseValue baseType="identifier">I66faad9a-d004-4cdc-a275-6f9ff736bb14</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <member>
                            <baseValue baseType="identifier">I665f2702-e926-4266-bc3c-3f04ea9a6c62</baseValue>
                            <variable identifier="RESPONSE"/>
                        </member>
                        <not>
                            <member>
                                <baseValue baseType="identifier">I7e8b004a-0223-432a-bb9a-536918190017</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <not>
                            <member>
                                <baseValue baseType="identifier">I217873b0-4970-405f-a80b-b2a9f42bbd85</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <member>
                            <baseValue baseType="identifier">I46521597-f1f4-432c-96c2-8e3defb05950</baseValue>
                            <variable identifier="RESPONSE"/>
                        </member>
                        <not>
                            <member>
                                <baseValue baseType="identifier">Ifbea86b5-20bc-4dde-a4e4-4a14153c8ead</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <not>
                            <member>
                                <baseValue baseType="identifier">I279d3c47-95a8-42d1-9440-26fadf909090</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                        <not>
                            <member>
                                <baseValue baseType="identifier">I6d80a45f-5042-4f42-99fe-151229268a43</baseValue>
                                <variable identifier="RESPONSE"/>
                            </member>
                        </not>
                    </and>
                    <setOutcomeValue identifier="SCORE">
                        <sum>
                            <baseValue baseType="integer">1</baseValue>
                            <variable identifier="SCORE"/>
                        </sum>
                    </setOutcomeValue>
                </responseIf>
            </responseCondition>
            <responseCondition>
                <responseIf>
                    <gte>
                        <variable identifier="SCORE"/>
                        <baseValue baseType="integer">1</baseValue>
                    </gte>
                    <setOutcomeValue identifier="SCORE">
                        <baseValue baseType="integer">1</baseValue>
                    </setOutcomeValue>
                </responseIf>
                <responseElse>
                    <setOutcomeValue identifier="SCORE">
                        <baseValue baseType="integer">0</baseValue>
                    </setOutcomeValue>
                </responseElse>
            </responseCondition>
        </responseProcessing>

End Class
