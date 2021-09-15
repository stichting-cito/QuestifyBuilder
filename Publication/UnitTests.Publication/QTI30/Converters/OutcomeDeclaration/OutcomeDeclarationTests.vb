
Imports System.Drawing
Imports System.Xml
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Configuration
Imports Questify.Builder.Logic.HelperClasses
Imports Questify.Builder.Logic.QTI.Converters.ScoringConverter.QTI30
Imports Questify.Builder.Logic.QTI.PackageCreators.QTI30
Imports Questify.Builder.Logic.QTI.Xsd.QTI30

Namespace QTI30

    <TestClass()>
    Public Class OutcomeDeclarationTests

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring")>
        Public Sub OneGroupOneFactOnFinding_FourOutcomeDeclarations()

            'Arrange
            Dim responseIdentifierAttributes As XmlNodeList = PublicationTestHelper.GetResponseIdentifiers(_itemBody1)
            Dim solution As Solution = _solution1.Deserialize(Of Solution)()
            Dim converter = New CombinedScoringConverter()

            'Act
            Dim result = GetOutcomeDeclarations(converter, solution, responseIdentifierAttributes)

            'Assert
            Assert.AreEqual(2, result.Count)
            Assert.AreEqual("SCORE", result(0).identifier)
            Assert.AreEqual("MAXSCORE", result(1).identifier)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring")>
        Public Sub OneGroupOneFactOnFindingOneConcept_SixOutcomeDeclarations()
            'Arrange
            Dim responseIdentifierAttributes As XmlNodeList = PublicationTestHelper.GetResponseIdentifiers(_itemBody2)
            Dim solution As Solution = _solution2.Deserialize(Of Solution)()
            Dim converter = New CombinedScoringConverter(GetGapScoringParams())

            'Act
            Dim result = GetOutcomeDeclarations(converter, solution, responseIdentifierAttributes)

            'Assert
            Assert.AreEqual(4, result.Count)
            Assert.AreEqual("SCORE", result(0).identifier)
            Assert.AreEqual("MAXSCORE", result(1).identifier)
            Assert.AreEqual("CONCEPTRESPONSE_Concept2-1", result(2).identifier)
            Assert.AreEqual("CONCEPTRESPONSE2_Concept2-1", result(3).identifier)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring")>
        Public Sub GM_CombinationOfFactSetsAndFactsOnFinding_OutcomeDeclarationsShouldBeInRightOrder()
            'The outcomedeclarations should be created in the same order as the order in which the concept finding are processed in the response processing
            'Because else it could occur that f.i. CONCEPTRESPONSE2_Bla is created as an outcomedeclaration, but CONCEPTRESPONSE3_Bla is used in the response processing

            'Arrange
            Dim responseIdentifierAttributes As XmlNodeList = PublicationTestHelper.GetResponseIdentifiers(_itemBody3)
            Dim solution As Solution = _solution3.Deserialize(Of Solution)()
            Dim converter = New CombinedScoringConverter(GetGapMatchScoringParams())

            'Act
            Dim result = GetOutcomeDeclarations(converter, solution, responseIdentifierAttributes)

            'Assert
            Assert.AreEqual(12, result.Count)
            Assert.AreEqual("SCORE", result(0).identifier)
            Assert.AreEqual("MAXSCORE", result(1).identifier)
            Assert.AreEqual("CONCEPTRESPONSE_EN-SC-4", result(2).identifier)
            Assert.AreEqual("CONCEPTRESPONSE_EN-SC-4-1", result(3).identifier)
            Assert.AreEqual("CONCEPTRESPONSE2_EN-SC-1", result(4).identifier)
            Assert.AreEqual("CONCEPTRESPONSE2_EN-SC-4", result(5).identifier)
            Assert.AreEqual("CONCEPTRESPONSE2_EN-SC-4-1", result(6).identifier)
            Assert.AreEqual("CONCEPTRESPONSE3_EN-SC-2", result(7).identifier)
            Assert.AreEqual("CONCEPTRESPONSE3_EN-SC-3", result(8).identifier)
            Assert.AreEqual("CONCEPTRESPONSE3_EN-SC-3-1", result(9).identifier)
            Assert.AreEqual("CONCEPTRESPONSE3_EN-SC-4", result(10).identifier)
            Assert.AreEqual("CONCEPTRESPONSE3_EN-SC-4-1", result(11).identifier)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring")>
        Public Sub GGM_CombinationOfFactSetsAndFactsOnFinding_OutcomeDeclarationsShouldBeInRightOrder()
            'The outcomedeclarations should be created in the same order as the order in which the concept finding are processed in the response processing
            'Because else it could occur that f.i. CONCEPTRESPONSE2_Bla is created as an outcomedeclaration, but CONCEPTRESPONSE3_Bla is used in the response processing

            'Arrange
            Dim responseIdentifierAttributes As XmlNodeList = PublicationTestHelper.GetResponseIdentifiers(_itemBody4)
            Dim solution As Solution = _solution4.Deserialize(Of Solution)()
            Dim converter = New CombinedScoringConverter(GetGraphicGapMatchScoringParams())

            'Act
            Dim result = GetOutcomeDeclarations(converter, solution, responseIdentifierAttributes)

            'Assert
            Assert.AreEqual(12, result.Count)
            Assert.AreEqual("SCORE", result(0).identifier)
            Assert.AreEqual("MAXSCORE", result(1).identifier)
            Assert.AreEqual("CONCEPTRESPONSE_1-Doel-en-Publiek", result(2).identifier)
            Assert.AreEqual("CONCEPTRESPONSE_1-2-Rekening-houden-met-verhouding-tot-lezer", result(3).identifier)
            Assert.AreEqual("CONCEPTRESPONSE2_1-Doel-en-Publiek", result(4).identifier)
            Assert.AreEqual("CONCEPTRESPONSE2_1-1-Rekening-houden-met-voorkennis-lezer", result(5).identifier)
            Assert.AreEqual("CONCEPTRESPONSE3_2-Tekstopbouw", result(6).identifier)
            Assert.AreEqual("CONCEPTRESPONSE3_2-2-Passende-argumentstructuur-gebruiken-h-v-", result(7).identifier)
            Assert.AreEqual("CONCEPTRESPONSE4_3-Tekststructuur", result(8).identifier)
            Assert.AreEqual("CONCEPTRESPONSE4_3-1-Passende-tekstindeling-en-opmaak-gebruiken", result(9).identifier)
            Assert.AreEqual("CONCEPTRESPONSE5_2-Tekstopbouw", result(10).identifier)
            Assert.AreEqual("CONCEPTRESPONSE5_2-1-Hoofd--en-bijzaken-onderscheiden", result(11).identifier)

        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring")>
        Public Sub Gaps_CombinationOfMultipleFactSetsAndFactsOnFinding()
            'Arrange
            Dim responseIdentifierAttributes As XmlNodeList = PublicationTestHelper.GetResponseIdentifiers(_itemBody5)
            Dim solution As Solution = _solution5.Deserialize(Of Solution)()
            Dim converter = New CombinedScoringConverter(GetFiveGapScoringParams())

            'Act
            Dim result = GetOutcomeDeclarations(converter, solution, responseIdentifierAttributes)

            'Assert
            Assert.AreEqual(6, result.Count)
            Assert.AreEqual("SCORE", result(0).identifier)
            Assert.AreEqual("MAXSCORE", result(1).identifier)
            Assert.AreEqual("CONCEPTRESPONSE_economie-2", result(2).identifier)
            Assert.AreEqual("CONCEPTRESPONSE2_economie-2", result(3).identifier)
            Assert.AreEqual("CONCEPTRESPONSE2_economie-2-2", result(4).identifier)
            Assert.AreEqual("CONCEPTRESPONSE3_economie-1", result(5).identifier)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring")>
        Public Sub HottextCorrection_FactsOnFinding_OutcomeDeclarationsShouldBeInRightOrder()
            'Arrange
            Dim responseIdentifierAttributes As XmlNodeList = PublicationTestHelper.GetResponseIdentifiers(_itemBody6)
            Dim solution As Solution = _solution6.Deserialize(Of Solution)()
            Dim converter = New CombinedScoringConverter(GetHottextCorrectionScoringParams())

            'Act
            Dim result = GetOutcomeDeclarations(converter, solution, responseIdentifierAttributes)

            'Assert
            Assert.AreEqual(22, result.Count)
            Assert.AreEqual("SCORE", result(0).identifier)
            Assert.AreEqual("MAXSCORE", result(1).identifier)
            Assert.AreEqual("CONCEPTRESPONSE_Concept1", result(2).identifier)
            Assert.AreEqual("CONCEPTRESPONSE_Concept1-1", result(3).identifier)
            Assert.AreEqual("CONCEPTRESPONSE3_Concept1", result(4).identifier)
            Assert.AreEqual("CONCEPTRESPONSE3_Concept1-2", result(5).identifier)
            Assert.AreEqual("CONCEPTRESPONSE5_Concept1", result(6).identifier)
            Assert.AreEqual("CONCEPTRESPONSE5_Concept1-2", result(7).identifier)
            Assert.AreEqual("CONCEPTRESPONSE6_Concept2", result(8).identifier)
            Assert.AreEqual("CONCEPTRESPONSE6_Concept2-1", result(9).identifier)
            Assert.AreEqual("CONCEPTRESPONSE10_Concept2", result(10).identifier)
            Assert.AreEqual("CONCEPTRESPONSE11_Concept2", result(11).identifier)
            Assert.AreEqual("CONCEPTRESPONSE14_Concept1", result(12).identifier)
            Assert.AreEqual("CONCEPTRESPONSE14_Concept1-1", result(13).identifier)
            Assert.AreEqual("CONCEPTRESPONSE14_Concept1-2", result(14).identifier)
            Assert.AreEqual("CONCEPTRESPONSE17_Concept2", result(15).identifier)
            Assert.AreEqual("CONCEPTRESPONSE17_Concept2-1", result(16).identifier)
            Assert.AreEqual("CONCEPTRESPONSE21_Concept1", result(17).identifier)
            Assert.AreEqual("CONCEPTRESPONSE21_Concept1-1", result(18).identifier)
            Assert.AreEqual("CONCEPTRESPONSE21_Concept1-2", result(19).identifier)
            Assert.AreEqual("CONCEPTRESPONSE22_Concept1", result(20).identifier)
            Assert.AreEqual("CONCEPTRESPONSE22_Concept1-1", result(21).identifier)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring")>
        Public Sub HottextCorrection_FactSets_OutcomeDeclarationsShouldBeInRightOrder()
            'Arrange
            Dim responseIdentifierAttributes As XmlNodeList = PublicationTestHelper.GetResponseIdentifiers(_itemBody6)
            Dim solution As Solution = _solution7.Deserialize(Of Solution)()
            Dim converter = New CombinedScoringConverter(GetHottextCorrectionScoringParams())

            'Act
            Dim result = GetOutcomeDeclarations(converter, solution, responseIdentifierAttributes)

            'Assert
            Assert.AreEqual(7, result.Count)
            Assert.AreEqual("SCORE", result(0).identifier)
            Assert.AreEqual("MAXSCORE", result(1).identifier)
            Assert.AreEqual("CONCEPTRESPONSE_Concept1", result(2).identifier)
            Assert.AreEqual("CONCEPTRESPONSE_Concept1-1", result(3).identifier)
            Assert.AreEqual("CONCEPTRESPONSE_Concept1-2", result(4).identifier)
            Assert.AreEqual("CONCEPTRESPONSE_Concept2", result(5).identifier)
            Assert.AreEqual("CONCEPTRESPONSE_Concept2-1", result(6).identifier)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring")>
        Public Sub HottextCorrection_MultipleFactSets_OutcomeDeclarationsShouldBeInRightOrder()
            'Arrange
            Dim responseIdentifierAttributes As XmlNodeList = PublicationTestHelper.GetResponseIdentifiers(_itemBody6)
            Dim solution As Solution = _solution8.Deserialize(Of Solution)()
            Dim converter = New CombinedScoringConverter(GetHottextCorrectionScoringParams())

            'Act
            Dim result = GetOutcomeDeclarations(converter, solution, responseIdentifierAttributes)

            'Assert
            Assert.AreEqual(11, result.Count)
            Assert.AreEqual("SCORE", result(0).identifier)
            Assert.AreEqual("MAXSCORE", result(1).identifier)
            Assert.AreEqual("CONCEPTRESPONSE_Concept1", result(2).identifier)
            Assert.AreEqual("CONCEPTRESPONSE_Concept1-1", result(3).identifier)
            Assert.AreEqual("CONCEPTRESPONSE_Concept1-2", result(4).identifier)
            Assert.AreEqual("CONCEPTRESPONSE_Concept2", result(5).identifier)
            Assert.AreEqual("CONCEPTRESPONSE_Concept2-1", result(6).identifier)
            Assert.AreEqual("CONCEPTRESPONSE2_Concept1", result(7).identifier)
            Assert.AreEqual("CONCEPTRESPONSE2_Concept1-1", result(8).identifier)
            Assert.AreEqual("CONCEPTRESPONSE2_Concept2", result(9).identifier)
            Assert.AreEqual("CONCEPTRESPONSE2_Concept2-1", result(10).identifier)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring")>
        Public Sub HottextCorrection_CombinationOfFactSetsAndFactsOnFinding_OutcomeDeclarationsShouldBeInRightOrder()
            'Arrange
            Dim responseIdentifierAttributes As XmlNodeList = PublicationTestHelper.GetResponseIdentifiers(_itemBody6)
            Dim solution As Solution = _solution9.Deserialize(Of Solution)()
            Dim converter = New CombinedScoringConverter(GetHottextCorrectionScoringParams())

            'Act
            Dim result = GetOutcomeDeclarations(converter, solution, responseIdentifierAttributes)

            'Assert
            Assert.AreEqual(11, result.Count)
            Assert.AreEqual("SCORE", result(0).identifier)
            Assert.AreEqual("MAXSCORE", result(1).identifier)
            Assert.AreEqual("CONCEPTRESPONSE_Concept1", result(2).identifier)
            Assert.AreEqual("CONCEPTRESPONSE_Concept1-1", result(3).identifier)
            Assert.AreEqual("CONCEPTRESPONSE_Concept2", result(4).identifier)
            Assert.AreEqual("CONCEPTRESPONSE_Concept2-1", result(5).identifier)
            Assert.AreEqual("CONCEPTRESPONSE4_Concept1", result(6).identifier)
            Assert.AreEqual("CONCEPTRESPONSE4_Concept1-1", result(7).identifier)
            Assert.AreEqual("CONCEPTRESPONSE7_Concept2", result(8).identifier)
            Assert.AreEqual("CONCEPTRESPONSE7_Concept2-1", result(9).identifier)
            Assert.AreEqual("CONCEPTRESPONSE12_Concept1", result(10).identifier)
        End Sub

        ''' <remarks>Replace 'I6cc75685-147c-4a91-a899-791aa0201e6f' with 'inlineMC' in the whole file and this test Succeeds.</remarks>
        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring")>
        Public Sub MultiResponse_CorrectNumberOfOutcomeDeclarations()
            'Arrange
            Dim responseIdentifierAttributes As XmlNodeList = PublicationTestHelper.GetResponseIdentifiers(_itemBody10)
            Dim solution As Solution = _solution13.Deserialize(Of Solution)()
            Dim converter As CombinedScoringConverter = New CombinedScoringConverter(GetMultipleResponseScoringsParams())

            'Act
            Dim result = GetOutcomeDeclarations(converter, solution, responseIdentifierAttributes)

            'Assert
            Assert.AreEqual(20, result.Count)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring")>
        Public Sub Formula_CasEqual_FactsOnFinding()
            'Arrange
            Dim responseIdentifierAttributes As XmlNodeList = PublicationTestHelper.GetResponseIdentifiers(_itemBody7)
            Dim solution As Solution = _solution10.Deserialize(Of Solution)()
            Dim converter = New CombinedScoringConverter(GetFormulaCasEqualScoringParams())

            'Act
            Dim result = GetOutcomeDeclarations(converter, solution, responseIdentifierAttributes)

            'Assert
            Assert.AreEqual(6, result.Count)
            Assert.AreEqual("SCORE", result(0).identifier)
            Assert.AreEqual("MAXSCORE", result(1).identifier)
            Assert.AreEqual("CONCEPTRESPONSE_Concept1", result(2).identifier)
            Assert.AreEqual("CONCEPTRESPONSE_Concept1-1", result(3).identifier)
            Assert.AreEqual("CONCEPTRESPONSE3_Concept2", result(4).identifier)
            Assert.AreEqual("CONCEPTRESPONSE3_Concept2-1", result(5).identifier)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring")>
        Public Sub Formula_CasEqual_Multiple_FactsOnFinding()
            'Arrange
            Dim responseIdentifierAttributes As XmlNodeList = PublicationTestHelper.GetResponseIdentifiers(_itemBody8)
            Dim solution As Solution = _solution11.Deserialize(Of Solution)()
            Dim converter = New CombinedScoringConverter(GetFormulaCasEqualMultipleScoringParams())

            'Act
            Dim result = GetOutcomeDeclarations(converter, solution, responseIdentifierAttributes)

            'Assert
            Assert.AreEqual(10, result.Count)
            Assert.AreEqual("SCORE", result(0).identifier)
            Assert.AreEqual("MAXSCORE", result(1).identifier)
            Assert.AreEqual("CONCEPTRESPONSE_Concept1", result(2).identifier)
            Assert.AreEqual("CONCEPTRESPONSE_Concept1-1", result(3).identifier)
            Assert.AreEqual("CONCEPTRESPONSE3_Concept2", result(4).identifier)
            Assert.AreEqual("CONCEPTRESPONSE3_Concept2-1", result(5).identifier)
            Assert.AreEqual("CONCEPTRESPONSE4_Concept1", result(6).identifier)
            Assert.AreEqual("CONCEPTRESPONSE4_Concept1-1", result(7).identifier)
            Assert.AreEqual("CONCEPTRESPONSE4_Concept1-2", result(8).identifier)
            Assert.AreEqual("CONCEPTRESPONSE5_Concept1", result(9).identifier)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring")>
        Public Sub Formula_CasEqual_Mixed_FactsOnFinding()
            'Arrange
            Dim responseIdentifierAttributes As XmlNodeList = PublicationTestHelper.GetResponseIdentifiers(_itemBody9)
            Dim solution As Solution = _solution12.Deserialize(Of Solution)()
            Dim converter = New CombinedScoringConverter(GetFormulaCasEqualMixedScoringParams())

            'Act
            Dim result = GetOutcomeDeclarations(converter, solution, responseIdentifierAttributes)

            'Assert
            Assert.AreEqual(10, result.Count)
            Assert.AreEqual("SCORE", result(0).identifier)
            Assert.AreEqual("MAXSCORE", result(1).identifier)
            Assert.AreEqual("CONCEPTRESPONSE2_Concept1", result(2).identifier)
            Assert.AreEqual("CONCEPTRESPONSE2_Concept1-1", result(3).identifier)
            Assert.AreEqual("CONCEPTRESPONSE4_Concept2", result(4).identifier)
            Assert.AreEqual("CONCEPTRESPONSE4_Concept2-1", result(5).identifier)
            Assert.AreEqual("CONCEPTRESPONSE7_Concept1", result(6).identifier)
            Assert.AreEqual("CONCEPTRESPONSE7_Concept1-1", result(7).identifier)
            Assert.AreEqual("CONCEPTRESPONSE7_Concept1-2", result(8).identifier)
            Assert.AreEqual("CONCEPTRESPONSE8_Concept1", result(9).identifier)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring")>
        Public Sub McWithInlineCI_CombinationOfFactSetsAndFactsOnFinding()
            'Arrange
            Dim responseIdentifierAttributes As XmlNodeList = PublicationTestHelper.GetResponseIdentifiers(_itemBody11)
            Dim solution As Solution = _solution14.Deserialize(Of Solution)()
            Dim converter = New CombinedScoringConverter(GetScoringParams_MC_And_InlineCI())

            'Act
            Dim result = GetOutcomeDeclarations(converter, solution, responseIdentifierAttributes)

            'Assert
            Assert.AreEqual(10, result.Count)
            Assert.AreEqual("SCORE", result(0).identifier)
            Assert.AreEqual("MAXSCORE", result(1).identifier)
            Assert.AreEqual("CONCEPTRESPONSE_B", result(2).identifier)
            Assert.AreEqual("CONCEPTRESPONSE_B1", result(3).identifier)
            Assert.AreEqual("CONCEPTRESPONSE3_A", result(4).identifier)
            Assert.AreEqual("CONCEPTRESPONSE3_A1", result(5).identifier)
            Assert.AreEqual("CONCEPTRESPONSE3_A11", result(6).identifier)
            Assert.AreEqual("CONCEPTRESPONSE4_A", result(7).identifier)
            Assert.AreEqual("CONCEPTRESPONSE4_A1", result(8).identifier)
            Assert.AreEqual("CONCEPTRESPONSE4_A11", result(9).identifier)
        End Sub

        Private Function GetOutcomeDeclarations(converter As CombinedScoringConverter,
                                                solution As Solution,
                                                responseIdentifierAttributes As XmlNodeList) As List(Of OutcomeDeclarationType)
            Dim packageCreator = GetPackageCreator()
            Return converter.GetOutcomeDeclarations(solution, responseIdentifierAttributes, solution.ItemScoreTranslationTable, packageCreator)
        End Function


        Private Function GetPackageCreator() As PackageCreator
            Dim config As PluginHandlerConfigCollection = Nothing
            Return New PackageCreator(config)
        End Function

#Region "Scoring parameters"

        Private Function GetGapScoringParams() As HashSet(Of ScoringParameter)
            Dim scoringParameters As New HashSet(Of ScoringParameter)()
            scoringParameters.Add(New TimeScoringParameter() With {.ControllerId = "gapController", .FindingOverride = "gapController", .InlineId = "I0de6ade6-f82f-4111-b045-a3493f2d1ba6"}.AddSubParameters("1"))
            scoringParameters.Add(New IntegerScoringParameter() With {.ControllerId = "gapController", .FindingOverride = "gapController", .InlineId = "I52e3c4ab-3e4c-4b90-8dc7-90d05d4a9d22"}.AddSubParameters("1"))
            scoringParameters.Add(New TimeScoringParameter() With {.ControllerId = "gapController", .FindingOverride = "gapController", .InlineId = "I899a54e6-ecf1-4875-83f9-8be5c4503614"}.AddSubParameters("1"))
            Return scoringParameters
        End Function

        Private Function GetFiveGapScoringParams() As HashSet(Of ScoringParameter)
            Dim scoringParameters As New HashSet(Of ScoringParameter)()
            scoringParameters.Add(New StringScoringParameter() With {.ControllerId = "gapController", .FindingOverride = "gapController", .InlineId = "Ifaf38ff9-00f5-448c-b86b-252db6377abf"}.AddSubParameters("1"))
            scoringParameters.Add(New IntegerScoringParameter() With {.ControllerId = "gapController", .FindingOverride = "gapController", .InlineId = "Iebfe9a06-1bd8-4430-a1b8-412e268266f7"}.AddSubParameters("1"))
            scoringParameters.Add(New DecimalScoringParameter() With {.ControllerId = "gapController", .FindingOverride = "gapController", .InlineId = "I65bd20fb-80f1-4e34-95f8-b70ccf11cb9f"}.AddSubParameters("1"))
            scoringParameters.Add(New CurrencyScoringParameter() With {.ControllerId = "gapController", .FindingOverride = "gapController", .InlineId = "Ia7b2ea59-2a1d-43d1-a09f-fafff847191b"}.AddSubParameters("1"))
            scoringParameters.Add(New TimeScoringParameter() With {.ControllerId = "gapController", .FindingOverride = "gapController", .InlineId = "I59205989-12f5-4dbf-a5d0-e83e47d6c87f"}.AddSubParameters("1"))
            Return scoringParameters
        End Function

        Private Function GetFormulaCasEqualScoringParams() As HashSet(Of ScoringParameter)
            Dim scoreParams As New HashSet(Of ScoringParameter)
            Dim scoreParam1 As ScoringParameter = New CasEqualStepsScoringParameter() With {.ControllerId = "casEqualStepsScore", .FindingOverride = "gapController", .InlineId = "I5a35f7d3-acbc-4c02-8653-77e8bd20788c"}.AddSubParameters("Second")
            scoreParams.Add(scoreParam1)
            Dim scoreParam2 As ScoringParameter = New MathCasEqualScoringParameter() With {.ControllerId = "lastMathScore", .FindingOverride = "gapController", .InlineId = "I5a35f7d3-acbc-4c02-8653-77e8bd20788c"}.AddSubParameters("Last")
            scoreParams.Add(scoreParam2)
            Dim scoreParam3 As ScoringParameter = New MathCasEqualScoringParameter() With {.ControllerId = "firstMathScore", .FindingOverride = "gapController", .InlineId = "I5a35f7d3-acbc-4c02-8653-77e8bd20788c"}.AddSubParameters("First")
            scoreParams.Add(scoreParam3)
            Return scoreParams
        End Function

        Private Function GetFormulaCasEqualMultipleScoringParams() As HashSet(Of ScoringParameter)
            Dim scoreParams As HashSet(Of ScoringParameter) = GetFormulaCasEqualScoringParams()
            Dim scoreParam1 As ScoringParameter = New CasEqualStepsScoringParameter() With {.ControllerId = "casEqualStepsScore", .FindingOverride = "gapController", .InlineId = "I7b9b4ad0-6eb5-4928-b09d-16264ff8f120"}.AddSubParameters("Second")
            scoreParams.Add(scoreParam1)
            Dim scoreParam2 As ScoringParameter = New MathCasEqualScoringParameter() With {.ControllerId = "lastMathScore", .FindingOverride = "gapController", .InlineId = "I7b9b4ad0-6eb5-4928-b09d-16264ff8f120"}.AddSubParameters("Last")
            scoreParams.Add(scoreParam2)
            Dim scoreParam3 As ScoringParameter = New MathCasEqualScoringParameter() With {.ControllerId = "firstMathScore", .FindingOverride = "gapController", .InlineId = "I7b9b4ad0-6eb5-4928-b09d-16264ff8f120"}.AddSubParameters("First")
            scoreParams.Add(scoreParam3)
            Return scoreParams
        End Function

        Private Function GetFormulaCasEqualMixedScoringParams() As HashSet(Of ScoringParameter)
            Dim scoreParams As HashSet(Of ScoringParameter) = GetFormulaCasEqualMultipleScoringParams()
            Dim scoreParam1 As ScoringParameter = New IntegerScoringParameter() With {.ControllerId = "gapController", .FindingOverride = "gapController", .InlineId = "If5d399bf-f82e-4faf-bebd-c3bfa23fbfb1"}.AddSubParameters("1")
            scoreParams.Add(scoreParam1)
            Dim scoreParam2 As ScoringParameter = New StringScoringParameter() With {.ControllerId = "gapController", .FindingOverride = "gapController", .InlineId = "Ie7f3c962-96d9-417f-a793-0c506c67ec1d"}.AddSubParameters("1")
            scoreParams.Add(scoreParam2)
            Dim scoreParam3 As ScoringParameter = New TimeScoringParameter() With {.ControllerId = "gapController", .FindingOverride = "gapController", .InlineId = "If7c2200d-0063-4a23-a58c-2a38deea8529"}.AddSubParameters("1")
            scoreParams.Add(scoreParam3)
            Return scoreParams
        End Function

        Private Function GetGapMatchScoringParams() As HashSet(Of ScoringParameter)
            Dim scoreParams As New HashSet(Of ScoringParameter)
            Dim scoreParam As ScoringParameter = New GapMatchScoringParameter() With {.ControllerId = "gapMatchController", .FindingOverride = "gapMatchController"}.AddSubParameters("A", "B", "C", "D")

            Dim xhtmlValue As XElement = <xhtmlParameter name="itemInlineInput">
                                             <p id="c1-id-11" xmlns="http://www.w3.org/1999/xhtml">I <cito:InlineElement id="Ied60a714-2bc7-4c12-badb-8afc5f9d30f7" layoutTemplateSourceName="InlineGapMatchLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                     <cito:parameters>
                                                         <cito:parameterSet id="entireItem">
                                                             <cito:plaintextparameter name="inlineGapMatchId">Ied60a714-2bc7-4c12-badb-8afc5f9d30f7</cito:plaintextparameter>
                                                             <cito:plaintextparameter name="inlineGapMatchLabel">1</cito:plaintextparameter>
                                                             <cito:booleanparameter name="editSize">True</cito:booleanparameter>
                                                             <cito:integerparameter name="width"/>
                                                             <cito:integerparameter name="height"/>
                                                         </cito:parameterSet>
                                                     </cito:parameters>
                                                 </cito:InlineElement> <cito:InlineElement id="Ia48dc13e-6ba6-4334-944f-04aa7b55ccd0" layoutTemplateSourceName="InlineGapMatchLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                     <cito:parameters>
                                                         <cito:parameterSet id="entireItem">
                                                             <cito:plaintextparameter name="inlineGapMatchId">Ia48dc13e-6ba6-4334-944f-04aa7b55ccd0</cito:plaintextparameter>
                                                             <cito:plaintextparameter name="inlineGapMatchLabel">2</cito:plaintextparameter>
                                                             <cito:booleanparameter name="editSize">True</cito:booleanparameter>
                                                             <cito:integerparameter name="width"/>
                                                             <cito:integerparameter name="height"/>
                                                         </cito:parameterSet>
                                                     </cito:parameters>
                                                 </cito:InlineElement> <cito:InlineElement id="If8d49acf-9059-4c11-9761-14cc7e799c10" layoutTemplateSourceName="InlineGapMatchLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                     <cito:parameters>
                                                         <cito:parameterSet id="entireItem">
                                                             <cito:plaintextparameter name="inlineGapMatchId">If8d49acf-9059-4c11-9761-14cc7e799c10</cito:plaintextparameter>
                                                             <cito:plaintextparameter name="inlineGapMatchLabel">3</cito:plaintextparameter>
                                                             <cito:booleanparameter name="editSize">True</cito:booleanparameter>
                                                             <cito:integerparameter name="width"/>
                                                             <cito:integerparameter name="height"/>
                                                         </cito:parameterSet>
                                                     </cito:parameters>
                                                 </cito:InlineElement> <cito:InlineElement id="Idd3fcc2f-f9b9-4ffe-a169-17973ef1d4c9" layoutTemplateSourceName="InlineGapMatchLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                     <cito:parameters>
                                                         <cito:parameterSet id="entireItem">
                                                             <cito:plaintextparameter name="inlineGapMatchId">Idd3fcc2f-f9b9-4ffe-a169-17973ef1d4c9</cito:plaintextparameter>
                                                             <cito:plaintextparameter name="inlineGapMatchLabel">4</cito:plaintextparameter>
                                                             <cito:booleanparameter name="editSize">True</cito:booleanparameter>
                                                             <cito:integerparameter name="width"/>
                                                             <cito:integerparameter name="height"/>
                                                         </cito:parameterSet>
                                                     </cito:parameters>
                                                 </cito:InlineElement>. </p>
                                         </xhtmlParameter>

            Dim xhtmlPrm As New XHtmlParameter() With {.Name = "gapMatchInlineInput", .Value = xhtmlValue.ToString}

            DirectCast(scoreParam, GapMatchScoringParameter).GapXhtmlParameter = xhtmlPrm
            scoreParam = DirectCast(scoreParam, GapMatchScoringParameter).Transform()
            scoreParams.Add(scoreParam)

            Return scoreParams
        End Function

        Private Function GetGraphicGapMatchScoringParams() As HashSet(Of ScoringParameter)
            Dim scoreParams As New HashSet(Of ScoringParameter)
            Dim scoreParam As GraphGapMatchScoringParameter = New GraphGapMatchScoringParameter() With {.ControllerId = "gapMatchController", .FindingOverride = "gapMatchController", .IsCategorizationVariant = False}.AddSubParameters("A", "B")
            scoreParam.Value(0).InnerParameters.Add(New GapImageParameter() With {.MatchMax = -1, .Id = "A", .Name = GapMatchScoringParameter.GapControlName})
            scoreParam.Value(1).InnerParameters.Add(New GapImageParameter() With {.MatchMax = -1, .Id = "B", .Name = GapMatchScoringParameter.GapControlName})

            Dim area As New AreaParameter With {.Name = "itemQuestionArea"}
            scoreParam.Area = area
            scoreParam.Area.ShapeList.Add(New RectangleShape() With {.Identifier = "H", .BottomRight = New Point(126, 100), .TopLeft = New Point(101, 75)})
            scoreParam.Area.ShapeList.Add(New RectangleShape() With {.Identifier = "I", .BottomRight = New Point(126, 125), .TopLeft = New Point(101, 100)})
            scoreParam.Area.ShapeList.Add(New RectangleShape() With {.Identifier = "G", .BottomRight = New Point(126, 75), .TopLeft = New Point(101, 50)})
            scoreParam.Area.ShapeList.Add(New RectangleShape() With {.Identifier = "F", .BottomRight = New Point(126, 50), .TopLeft = New Point(101, 25)})
            scoreParam.Area.ShapeList.Add(New RectangleShape() With {.Identifier = "A", .BottomRight = New Point(25, 25), .TopLeft = New Point(0, 0)})
            scoreParam.Area.ShapeList.Add(New RectangleShape() With {.Identifier = "B", .BottomRight = New Point(51, 25), .TopLeft = New Point(25, 0)})
            scoreParam.Area.ShapeList.Add(New RectangleShape() With {.Identifier = "C", .BottomRight = New Point(76, 25), .TopLeft = New Point(51, 0)})
            scoreParam.Area.ShapeList.Add(New RectangleShape() With {.Identifier = "D", .BottomRight = New Point(101, 25), .TopLeft = New Point(76, 0)})
            scoreParam.Area.ShapeList.Add(New RectangleShape() With {.Identifier = "E", .BottomRight = New Point(126, 25), .TopLeft = New Point(101, 0)})

            Dim ggmScoringPrm As ScoringParameter = scoreParam.Transform
            scoreParams.Add(ggmScoringPrm)
            Return scoreParams
        End Function

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
                                                             <cito:hotTextCorrectionScoringParameter name="hotTextCorrectionScoring" ControllerId="hottextCorrectionController" findingOverride="hottextController" expectedLength="0" correctionIsApplicable="true">
                                                                 <cito:subparameterset id="1"/>
                                                                 <cito:definition id=""/>
                                                                 <cito:relatedControlLabel name="controlLabel">een</cito:relatedControlLabel>
                                                             </cito:hotTextCorrectionScoringParameter>
                                                         </cito:parameterSet>
                                                     </cito:parameters>
                                                 </cito:InlineElement><span id="SIce5853d6-5a4b-4fc6-9298-7bc4047d0e26" style="background-color: #C7B8CE;">een</span> twee <cito:InlineElement id="I1752ec64-4652-4723-b3b0-0404b15a0e6d" layoutTemplateSourceName="InlineHottextCorrectionLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                     <cito:parameters>
                                                         <cito:parameterSet id="entireItem">
                                                             <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                             <cito:plaintextparameter name="controlId">I1752ec64-4652-4723-b3b0-0404b15a0e6d</cito:plaintextparameter>
                                                             <cito:plaintextparameter name="controlLabel">twee</cito:plaintextparameter>
                                                             <cito:booleanparameter name="addHottextCorrection">True</cito:booleanparameter>
                                                             <cito:plaintextparameter name="hottextValue"/>
                                                             <cito:hotTextCorrectionScoringParameter name="hotTextCorrectionScoring" ControllerId="hottextCorrectionController" findingOverride="hottextController" expectedLength="0" correctionIsApplicable="true">
                                                                 <cito:subparameterset id="1"/>
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
                                                             <cito:hotTextCorrectionScoringParameter name="hotTextCorrectionScoring" ControllerId="hottextCorrectionController" findingOverride="hottextController" expectedLength="0" correctionIsApplicable="true">
                                                                 <cito:subparameterset id="1"/>
                                                                 <cito:definition id=""/>
                                                                 <cito:relatedControlLabel name="controlLabel">drie</cito:relatedControlLabel>
                                                             </cito:hotTextCorrectionScoringParameter>
                                                         </cito:parameterSet>
                                                     </cito:parameters>
                                                 </cito:InlineElement><span id="SIff799b19-6c0e-406a-ad0d-63f470eac66f" style="background-color: #C7B8CE;">drie</span>. Met .. <cito:InlineElement id="I88bb2ba0-543d-44e5-977a-1754f8a1d505" layoutTemplateSourceName="InlineHottextCorrectionLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                     <cito:parameters>
                                                         <cito:parameterSet id="entireItem">
                                                             <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                             <cito:plaintextparameter name="controlId">I88bb2ba0-543d-44e5-977a-1754f8a1d505</cito:plaintextparameter>
                                                             <cito:plaintextparameter name="controlLabel">vier</cito:plaintextparameter>
                                                             <cito:booleanparameter name="addHottextCorrection">True</cito:booleanparameter>
                                                             <cito:plaintextparameter name="hottextValue"/>
                                                             <cito:hotTextCorrectionScoringParameter name="hotTextCorrectionScoring" ControllerId="hottextCorrectionController" findingOverride="hottextController" expectedLength="0" correctionIsApplicable="true">
                                                                 <cito:subparameterset id="1"/>
                                                                 <cito:definition id=""/>
                                                                 <cito:relatedControlLabel name="controlLabel">vier</cito:relatedControlLabel>
                                                             </cito:hotTextCorrectionScoringParameter>
                                                         </cito:parameterSet>
                                                     </cito:parameters>
                                                 </cito:InlineElement><span id="SI88bb2ba0-543d-44e5-977a-1754f8a1d505" style="background-color: #C7B8CE;">vier</span> daarom <cito:InlineElement id="I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a" layoutTemplateSourceName="InlineHottextCorrectionLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                     <cito:parameters>
                                                         <cito:parameterSet id="entireItem">
                                                             <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                             <cito:plaintextparameter name="controlId">I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a</cito:plaintextparameter>
                                                             <cito:plaintextparameter name="controlLabel">vijf</cito:plaintextparameter>
                                                             <cito:booleanparameter name="addHottextCorrection">True</cito:booleanparameter>
                                                             <cito:plaintextparameter name="hottextValue"/>
                                                             <cito:hotTextCorrectionScoringParameter name="hotTextCorrectionScoring" ControllerId="hottextCorrectionController" findingOverride="hottextController" expectedLength="0" correctionIsApplicable="true">
                                                                 <cito:subparameterset id="1"/>
                                                                 <cito:definition id=""/>
                                                                 <cito:relatedControlLabel name="controlLabel">vijf</cito:relatedControlLabel>
                                                             </cito:hotTextCorrectionScoringParameter>
                                                         </cito:parameterSet>
                                                     </cito:parameters>
                                                 </cito:InlineElement><span id="SI9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a" style="background-color: #C7B8CE;">vijf</span> een <cito:InlineElement id="I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b" layoutTemplateSourceName="InlineHottextCorrectionLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                     <cito:parameters>
                                                         <cito:parameterSet id="entireItem">
                                                             <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                             <cito:plaintextparameter name="controlId">I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b</cito:plaintextparameter>
                                                             <cito:plaintextparameter name="controlLabel">zes</cito:plaintextparameter>
                                                             <cito:booleanparameter name="addHottextCorrection">True</cito:booleanparameter>
                                                             <cito:plaintextparameter name="hottextValue"/>
                                                             <cito:hotTextCorrectionScoringParameter name="hotTextCorrectionScoring" ControllerId="hottextCorrectionController" findingOverride="hottextController" expectedLength="0" correctionIsApplicable="true">
                                                                 <cito:subparameterset id="1"/>
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
                                                             <cito:hotTextCorrectionScoringParameter name="hotTextCorrectionScoring" ControllerId="hottextCorrectionController" findingOverride="hottextController" expectedLength="0" correctionIsApplicable="true">
                                                                 <cito:subparameterset id="1"/>
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
                                                                 <cito:subparameterset id="1"/>
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
                                                                 <cito:subparameterset id="1"/>
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
                                                                 <cito:subparameterset id="1"/>
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
                                                                 <cito:subparameterset id="1"/>
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

        Private Function GetMultipleResponseScoringsParams() As HashSet(Of ScoringParameter)
            Dim scoringParams As New HashSet(Of ScoringParameter)
            Dim innerparams As XElement = <ScoringParameter xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:type="MultiChoiceScoringParameter" name="inlineMCScoring" label="a" ControllerId="inlineMC" findingOverride="IEF" minChoices="0" maxChoices="0" multiChoice="Radio">
                                              <subparameterset id="A">
                                                  <xhtmlparameter name="mcOption">
                                                      <p id="c1-id-11" xmlns="http://www.w3.org/1999/xhtml">a</p>
                                                  </xhtmlparameter>
                                              </subparameterset>
                                              <subparameterset id="B">
                                                  <xhtmlparameter name="mcOption">
                                                      <p id="c1-id-11" xmlns="http://www.w3.org/1999/xhtml">b</p>
                                                  </xhtmlparameter>
                                              </subparameterset>
                                              <subparameterset id="C">
                                                  <xhtmlparameter name="mcOption">
                                                      <p id="c1-id-11" xmlns="http://www.w3.org/1999/xhtml">c</p>
                                                  </xhtmlparameter>
                                              </subparameterset>
                                              <subparameterset id="D">
                                                  <xhtmlparameter name="mcOption">
                                                      <p id="c1-id-11" xmlns="http://www.w3.org/1999/xhtml">d</p>
                                                  </xhtmlparameter>
                                              </subparameterset>
                                              <subparameterset id="E">
                                                  <xhtmlparameter name="mcOption">
                                                      <p id="c1-id-11" xmlns="http://www.w3.org/1999/xhtml">e</p>
                                                  </xhtmlparameter>
                                              </subparameterset>
                                              <subparameterset id="F">
                                                  <xhtmlparameter name="mcOption">
                                                      <p id="c1-id-11" xmlns="http://www.w3.org/1999/xhtml">f</p>
                                                  </xhtmlparameter>
                                              </subparameterset>
                                              <definition id="">
                                                  <xhtmlparameter name="mcOption"/>
                                              </definition>
                                          </ScoringParameter>
            Dim serialized = SerializeHelper.XmlDeserializeFromString(innerparams.ToString(), GetType(ScoringParameter))
            Dim param As ScoringParameter = CType(serialized, ScoringParameter)
            param.InlineId = "I6cc75685-147c-4a91-a899-791aa0201e6f"
            scoringParams.Add(param)
            Return scoringParams

        End Function

        Private Function GetScoringParams_MC_And_InlineCI() As HashSet(Of ScoringParameter)
            Dim scoreParams As New HashSet(Of ScoringParameter)
            Dim scoreParam1 As ScoringParameter = New DecimalScoringParameter() With {.Label = "CI-A - coord 1 - x (A)", .Name = "CI_SP_I59fb10a4-be7d-4c15-b655-ccd57bfacfd6_0", .ControllerId = "CI_SP_I59fb10a4-be7d-4c15-b655-ccd57bfacfd6_0", .InlineId = "CI_SP_I59fb10a4-be7d-4c15-b655-ccd57bfacfd6_0", .FindingOverride = "mc", .IntegerPartMaxLength = 10, .FractionPartMaxLength = 2}.AddSubParameters("A")
            scoreParams.Add(scoreParam1)
            Dim scoreParam2 As ScoringParameter = New DecimalScoringParameter() With {.Label = "CI-A - coord 1 - y (B)", .Name = "CI_SP_I59fb10a4-be7d-4c15-b655-ccd57bfacfd6_1", .ControllerId = "CI_SP_I59fb10a4-be7d-4c15-b655-ccd57bfacfd6_1", .InlineId = "CI_SP_I59fb10a4-be7d-4c15-b655-ccd57bfacfd6_1", .FindingOverride = "mc", .IntegerPartMaxLength = 10, .FractionPartMaxLength = 2}.AddSubParameters("A")
            scoreParams.Add(scoreParam2)
            Dim scoreParam3 As ScoringParameter = New DecimalScoringParameter() With {.Label = "CI-A - coord 2 - x (C)", .Name = "CI_SP_I59fb10a4-be7d-4c15-b655-ccd57bfacfd6_2", .ControllerId = "CI_SP_I59fb10a4-be7d-4c15-b655-ccd57bfacfd6_2", .InlineId = "CI_SP_I59fb10a4-be7d-4c15-b655-ccd57bfacfd6_2", .FindingOverride = "mc", .IntegerPartMaxLength = 10, .FractionPartMaxLength = 2}.AddSubParameters("A")
            scoreParams.Add(scoreParam3)
            Dim scoreParam4 As ScoringParameter = New DecimalScoringParameter() With {.Label = "CI-A - coord 2 - y (D)", .Name = "CI_SP_I59fb10a4-be7d-4c15-b655-ccd57bfacfd6_3", .ControllerId = "CI_SP_I59fb10a4-be7d-4c15-b655-ccd57bfacfd6_3", .InlineId = "CI_SP_I59fb10a4-be7d-4c15-b655-ccd57bfacfd6_3", .FindingOverride = "mc", .IntegerPartMaxLength = 10, .FractionPartMaxLength = 2}.AddSubParameters("A")
            scoreParams.Add(scoreParam4)
            Dim scoreParam5 As ScoringParameter = New MathScoringParameter() With {.Label = "CI-B (A)", .Name = "CI_SP_I880ab1e3-b1ea-4e1f-a769-cf763f00969c_0", .ControllerId = "CI_SP_I880ab1e3-b1ea-4e1f-a769-cf763f00969c_0", .InlineId = "CI_SP_I880ab1e3-b1ea-4e1f-a769-cf763f00969c_0", .FindingOverride = "mc"}.AddSubParameters("A")
            scoreParams.Add(scoreParam5)
            Dim scoreParam6 As ScoringParameter = New GeogebraScoringParameter() With {.Label = "CI-C - geo1 (A)", .Name = "CI_SP_Ie7ea5301-245a-44c6-a961-a4864bd35c64_0", .ControllerId = "CI_SP_Ie7ea5301-245a-44c6-a961-a4864bd35c64_0", .InlineId = "CI_SP_Ie7ea5301-245a-44c6-a961-a4864bd35c64_0", .FindingOverride = "mc"}.AddSubParameters("A")
            scoreParams.Add(scoreParam6)
            Dim scoreParam7 As ScoringParameter = New MultiChoiceScoringParameter() With {.ControllerId = "mc", .FindingOverride = "mc", .MinChoices = 1, .MaxChoices = 1, .MultiChoice = MultiChoiceType.Radio}.AddSubParameters("A", "B", "C", "D")
            scoreParams.Add(scoreParam7)
            Return scoreParams
        End Function

#End Region

#Region "Itembody"

        ReadOnly _itemBody1 As XElement =
            <wrapper>
                <qti-stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
                <qti-stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
                <qti-item-body class="defaultBody">
                    <div class="content">
                        <div>
                            <div id="itemquestion">
                                <p id="c1-id-11">Welke twee getallen zijn opgeteld 3?</p>
                            </div>
                            <div id="questionwithinlinecontrol">
                                <p id="c1-id-11">
                                    <qti-text-entry-interaction pattern-mask="^-?([0-9]{1,5})?$" response-identifier="I0dec2572-468c-49d9-bdff-c2482ec461c1" expected-length="6"/> <qti-text-entry-interaction pattern-mask="^-?([0-9]{1,5})?$" response-identifier="Ia896c73f-84fa-4ead-b3fc-210882efb8b9" expected-length="6"/> <qti-text-entry-interaction pattern-mask="^-?([0-9]{1,5})?$" response-identifier="I499a63a9-fdf8-43eb-bdce-e1ecabfa2bed" expected-length="6"/>
                                </p>
                            </div>
                            <div id="answer">

                            </div>
                        </div>
                    </div>
                </qti-item-body>
            </wrapper>

        ReadOnly _itemBody2 As XElement =
            <wrapper>
                <qti-stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
                <qti-stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
                <qti-item-body class="defaultBody">
                    <div class="content">
                        <div>
                            <div id="questionwithinlinecontrol">
                                <p id="c1-id-11">tijd1: <span>
                                        <qti-text-entry-interaction pattern-mask="^(([0-1]?[0-9])|([2][0-3]))$" response-identifier="I0de6ade6-f82f-4111-b045-a3493f2d1ba6" expected-length="2" timeSubType="hhmm"/>
							  
								    :
								    
								    <qti-text-entry-interaction pattern-mask="^([0-5]?[0-9])$" response-identifier="I0de6ade6-f82f-4111-b045-a3493f2d1ba6" expected-length="2" timeSubType="hhmm"/>
                                    </span> </p>
                                <p id="c1-id-12">getal1: <qti-text-entry-interaction pattern-mask="^-?([0-9]{1,5})?$" response-identifier="I52e3c4ab-3e4c-4b90-8dc7-90d05d4a9d22" expected-length="6"/>
                                </p>
                                <p id="c1-id-13">tijd2: <span>
                                        <qti-text-entry-interaction pattern-mask="^(([0-1]?[0-9])|([2][0-3]))$" response-identifier="I899a54e6-ecf1-4875-83f9-8be5c4503614" expected-length="2" timeSubType="hhmm"/>
							  
								    :
								    
								    <qti-text-entry-interaction pattern-mask="^([0-5]?[0-9])$" response-identifier="I899a54e6-ecf1-4875-83f9-8be5c4503614" expected-length="2" timeSubType="hhmm"/>
                                    </span>
                                </p>
                            </div>
                            <div id="answer">

                            </div>
                        </div>
                    </div>
                </qti-item-body>
            </wrapper>

        ReadOnly _itemBody3 As XElement =
            <wrapper>
                <qti-stylesheet href="resource://package/itemstyle.css" type="text/css"/>
                <qti-stylesheet href="resource://package/userstyle.css" type="text/css"/>
                <qti-item-body class="defaultBody">
                    <div class="content">
                        <styles xmlns="http://www.w3.org/1999/xhtml">

                        </styles>
                        <div class="div_left" xmlns="http://www.w3.org/1999/xhtml">
                            <!-- div_left_inner is needed to create the 50px margin between the left and right columns in dual column mode -->
                            <div class="div_left_inner">

                            </div>
                        </div>
                        <div class="div_right" xmlns="http://www.w3.org/1999/xhtml">
                            <!-- div_right_inner is needed to create the 50px margin between the left and right columns in dual column mode -->
                            <div class="div_right_inner">
                                <div id="intro">
                                    <p id="c1-id-11">Drag and drop</p>
                                </div>
                                <div id="question">
                                    <p id="c1-id-11">
                                        <strong id="c1-id-12">Zet de zinsdelen in de juiste volgorde. Het eerste woord staat er al.</strong>
                                    </p>
                                </div>
                                <div>
                                    <qti-gap-match-interaction response-identifier="gapMatchController" shuffle="false">
                                        <qti-gap-text identifier="A" matchMax="1">am going</qti-gap-text>
                                        <qti-gap-text identifier="B" matchMax="1">in July</qti-gap-text>
                                        <qti-gap-text identifier="C" matchMax="1">on holiday</qti-gap-text>
                                        <qti-gap-text identifier="D" matchMax="1">to France</qti-gap-text>
                                        <p id="c1-id-11">I <span>
                                                <gap identifier="Ied60a714-2bc7-4c12-badb-8afc5f9d30f7" required="true"/>
                                            </span> <span>
                                                <gap identifier="Ia48dc13e-6ba6-4334-944f-04aa7b55ccd0" required="true"/>
                                            </span> <span>
                                                <gap identifier="If8d49acf-9059-4c11-9761-14cc7e799c10" required="true"/>
                                            </span> <span>
                                                <gap identifier="Idd3fcc2f-f9b9-4ffe-a169-17973ef1d4c9" required="true"/>
                                            </span>. </p>
                                    </qti-gap-match-interaction>
                                </div>
                            </div>
                        </div>
                    </div>
                </qti-item-body>
            </wrapper>

        ReadOnly _itemBody4 As XElement =
            <wrapper>
                <qti-stylesheet href="resource://package/itemstyle.css" type="text/css"/>
                <qti-stylesheet href="resource://package/userstyle.css" type="text/css"/>
                <qti-item-body class="defaultBody">
                    <div class="content">
                        <styles xmlns="http://www.w3.org/1999/xhtml">

                        </styles>
                        <div class="div_left" xmlns="http://www.w3.org/1999/xhtml">
                            <!-- div_left_inner is needed to create the 50px margin between the left and right columns in dual column mode -->
                            <div class="div_left_inner">

                            </div>
                        </div>
                        <div class="div_right" xmlns="http://www.w3.org/1999/xhtml">
                            <!-- div_right_inner is needed to create the 50px margin between the left and right columns in dual column mode -->
                            <div class="div_right_inner">
                                <div id="intro">
                                    <p id="c1-id-11">Drag and drop</p>
                                </div>
                                <div id="question">
                                    <p id="c1-id-11">
                                        <strong id="c1-id-12">Zet de zinsdelen in de juiste volgorde. Het eerste woord staat er al.</strong>
                                    </p>
                                </div>
                                <div id="body">
                                    <br/>
                                    <p id="c1-id-11">Maak onderstaand blokkenpatroon verder af.</p>
                                </div>
                                <div id="hotSpotAnswer_Vertical">
                                    <qti-graphic-gap-match-interaction response-identifier="gapMatchController">
                                        <object type="image/jpeg" data="resource://package/blokkenpatroon.jpg" width="127" height="126"/>
                                        <qti-gap-img identifier="A" matchMax="9" class="">
                                            <object type="image/jpeg" data="resource://package/afbeelding_blokje-wit.jpg" class="hotspot_opacity" width="27" height="26"/>
                                        </qti-gap-img>
                                        <qti-gap-img identifier="B" matchMax="9" class="">
                                            <object type="image/jpeg" data="resource://package/afbeelding_blokje-grijs.jpg" class="hotspot_opacity" width="26" height="26"/>
                                        </qti-gap-img>
                                        <qti-associable-hotspot identifier="HSH" matchMax="1" coords="101,75,126,100" shape="rect"/>
                                        <qti-associable-hotspot identifier="HSI" matchMax="1" coords="101,100,126,125" shape="rect"/>
                                        <qti-associable-hotspot identifier="HSG" matchMax="1" coords="101,50,126,75" shape="rect"/>
                                        <qti-associable-hotspot identifier="HSF" matchMax="1" coords="101,25,126,50" shape="rect"/>
                                        <qti-associable-hotspot identifier="HSA" matchMax="1" coords="0,0,25,25" shape="rect"/>
                                        <qti-associable-hotspot identifier="HSB" matchMax="1" coords="25,0,51,25" shape="rect"/>
                                        <qti-associable-hotspot identifier="HSC" matchMax="1" coords="51,0,76,25" shape="rect"/>
                                        <qti-associable-hotspot identifier="HSD" matchMax="1" coords="76,0,101,25" shape="rect"/>
                                        <qti-associable-hotspot identifier="HSE" matchMax="1" coords="101,0,126,25" shape="rect"/>
                                    </qti-graphic-gap-match-interaction>
                                </div>
                            </div>
                        </div>
                    </div>
                </qti-item-body>
            </wrapper>

        ReadOnly _itemBody5 As XElement =
           <wrapper>
               <qti-stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
               <qti-stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
               <qti-item-body class="defaultBody">
                   <div class="content">
                       <div>
                           <p id="c1-id-11">Vul in, tekst:  nee</p>
                           <p id="c1-id-12">
                               <qti-text-entry-interaction pattern-mask="^[^A-Z]{0,5}$" response-identifier="Ifaf38ff9-00f5-448c-b86b-252db6377abf" expected-length="5"/> </p>
                           <p id="c1-id-13">Vul in, geheel getal:  10</p>
                           <p id="c1-id-14">
                               <qti-text-entry-interaction pattern-mask="^-?([0-9]{1,5})?$" response-identifier="Iebfe9a06-1bd8-4430-a1b8-412e268266f7" expected-length="6"/>
                           </p>
                           <p id="c1-id-15">Vul in, decimaal getal: 15,50</p>
                           <p id="c1-id-16">
                               <qti-text-entry-interaction pattern-mask="^-?([0-9]{1,15})?(([\,])([0-9]{0,2}))?$" response-identifier="I65bd20fb-80f1-4e34-95f8-b70ccf11cb9f" expected-length="18"/> </p>
                           <p id="c1-id-17">Vul in, valuta:  10,50</p>
                           <p id="c1-id-18">
                               <qti-text-entry-interaction pattern-mask="^-?([0-9]{1,5})?(([\,])([0-9]{0,2}))?$" response-identifier="Ia7b2ea59-2a1d-43d1-a09f-fafff847191b" expected-length="8"/> </p>
                           <p id="c1-id-19">Vul in, tijd:  12:15</p>
                           <p id="c1-id-20">
                               <span>
                                   <qti-text-entry-interaction pattern-mask="^(([0-1]?[0-9])|([2][0-3]))$" response-identifier="I59205989-12f5-4dbf-a5d0-e83e47d6c87f" expected-length="2" timeSubType="hhmm"/>
							  
								    :
								    
								    <qti-text-entry-interaction pattern-mask="^([0-5]?[0-9])$" response-identifier="I59205989-12f5-4dbf-a5d0-e83e47d6c87f" expected-length="2" timeSubType="hhmm"/>
                               </span>
                           </p>
                       </div>
                   </div>
               </qti-item-body>
           </wrapper>

        Private _itemBody6 As XElement =
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
                            <div class="div_right" xmlns="http://www.w3.org/1999/xhtml">
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
                                        <qti-extended-text-interaction id="HT_A-ti" response-identifier="Input_Ice5853d6-5a4b-4fc6-9298-7bc4047d0e26" expected-length="140" expected-lines="2" hottextId="Ice5853d6-5a4b-4fc6-9298-7bc4047d0e26"/>
                                        <qti-extended-text-interaction id="HT_B-ti" response-identifier="Input_I1752ec64-4652-4723-b3b0-0404b15a0e6d" expected-length="140" expected-lines="2" hottextId="I1752ec64-4652-4723-b3b0-0404b15a0e6d"/>
                                        <qti-extended-text-interaction id="HT_C-ti" response-identifier="Input_Iff799b19-6c0e-406a-ad0d-63f470eac66f" expected-length="140" expected-lines="2" hottextId="Iff799b19-6c0e-406a-ad0d-63f470eac66f"/>
                                        <qti-extended-text-interaction id="HT_D-ti" response-identifier="Input_I88bb2ba0-543d-44e5-977a-1754f8a1d505" expected-length="140" expected-lines="2" hottextId="I88bb2ba0-543d-44e5-977a-1754f8a1d505"/>
                                        <qti-extended-text-interaction id="HT_E-ti" response-identifier="Input_I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a" expected-length="140" expected-lines="2" hottextId="I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a"/>
                                        <qti-extended-text-interaction id="HT_F-ti" response-identifier="Input_I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b" expected-length="140" expected-lines="2" hottextId="I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b"/>
                                        <qti-extended-text-interaction id="HT_G-ti" response-identifier="Input_I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea" expected-length="140" expected-lines="2" hottextId="I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea"/>
                                        <qti-extended-text-interaction id="HT_H-ti" response-identifier="Input_I941e9a9a-fd22-43a4-acf2-aa862f943cfc" expected-length="140" expected-lines="2" hottextId="I941e9a9a-fd22-43a4-acf2-aa862f943cfc"/>
                                        <qti-extended-text-interaction id="HT_I-ti" response-identifier="Input_I6eb7f951-31d1-46e2-89fb-c7eca3849f9d" expected-length="140" expected-lines="2" hottextId="I6eb7f951-31d1-46e2-89fb-c7eca3849f9d"/>
                                        <qti-extended-text-interaction id="HT_J-ti" response-identifier="Input_I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4" expected-length="140" expected-lines="2" hottextId="I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4"/>
                                        <qti-extended-text-interaction id="HT_K-ti" response-identifier="Input_Iad84d52a-4b1e-45b3-8067-96811e46ff7e" expected-length="140" expected-lines="2" hottextId="Iad84d52a-4b1e-45b3-8067-96811e46ff7e"/>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </qti-item-body>
            </wrapper>

        Private _itemBody7 As XElement =
            <wrapper>
                <qti-item-body class="defaultBody">
                    <div class="content">
                        <div xmlns="http://www.w3.org/1999/xhtml">
                            <div id="body">
                                <p id="c1-id-11">Body</p>
                            </div>
                            <div id="itemquestion">
                                <p id="c1-id-11">Vraag</p>
                            </div>
                            <div id="questionwithinlinecontrol">
                                <p id="c1-id-11">
                                    <qti-extended-text-interaction id="I5a35f7d3-acbc-4c02-8653-77e8bd20788c" response-identifier="I5a35f7d3-acbc-4c02-8653-77e8bd20788c"/> </p>
                            </div>
                            <div id="answer">

                            </div>
                            <div id="itemgeneral">
                                <br/>
                                <p id="c1-id-11">En een stukje algemene tekst</p>
                            </div>
                        </div>
                    </div>
                </qti-item-body>
            </wrapper>

        Private _itemBody8 As XElement =
            <wrapper>
                <qti-item-body class="defaultBody">
                    <div class="content">
                        <div xmlns="http://www.w3.org/1999/xhtml">
                            <div id="body">
                                <p id="c1-id-11">Body</p>
                            </div>
                            <div id="itemquestion">
                                <p id="c1-id-11">Vraag</p>
                            </div>
                            <div id="questionwithinlinecontrol">
                                <p id="c1-id-11">
                                    <qti-extended-text-interaction id="I5a35f7d3-acbc-4c02-8653-77e8bd20788c" response-identifier="I5a35f7d3-acbc-4c02-8653-77e8bd20788c"/> </p>
                                <p id="c1-id-12">
                                    <qti-extended-text-interaction id="I7b9b4ad0-6eb5-4928-b09d-16264ff8f120" response-identifier="I7b9b4ad0-6eb5-4928-b09d-16264ff8f120"/> </p>
                            </div>
                            <div id="answer">

                            </div>
                            <div id="itemgeneral">
                                <br/>
                                <p id="c1-id-11">En een stukje algemene tekst</p>
                            </div>
                        </div>
                    </div>
                </qti-item-body>
            </wrapper>

        Private _itemBody9 As XElement =
            <wrapper>
                <qti-item-body class="defaultBody">
                    <div class="content">
                        <div xmlns="http://www.w3.org/1999/xhtml">
                            <div id="body">
                                <p id="c1-id-11">Body</p>
                            </div>
                            <div id="itemquestion">
                                <p id="c1-id-11">Vraag</p>
                            </div>
                            <div id="questionwithinlinecontrol">
                                <p id="c1-id-11">
                                    <qti-text-entry-interaction pattern-mask="^-?([0-9]{1,5})?$" response-identifier="If5d399bf-f82e-4faf-bebd-c3bfa23fbfb1" expected-length="6"/> </p>
                                <p id="c1-id-12">
                                    <qti-extended-text-interaction id="I5a35f7d3-acbc-4c02-8653-77e8bd20788c" response-identifier="I5a35f7d3-acbc-4c02-8653-77e8bd20788c"/> </p>
                                <p id="c1-id-13">
                                    <span>
                                        <qti-text-entry-interaction pattern-mask="^(([0-1]?[0-9])|([2][0-3]))$" response-identifier="If7c2200d-0063-4a23-a58c-2a38deea8529" expected-length="2" timeSubType="hhmm"/>



                                        <qti-text-entry-interaction pattern-mask="^([0-5]?[0-9])$" response-identifier="If7c2200d-0063-4a23-a58c-2a38deea8529" expected-length="2" timeSubType="hhmm"/>
                                    </span> </p>
                                <p id="c1-id-14">
                                    <qti-text-entry-interaction pattern-mask="^.{0,5}$" response-identifier="Ie7f3c962-96d9-417f-a793-0c506c67ec1d" expected-length="5"/> </p>
                                <p id="c1-id-15">
                                    <qti-extended-text-interaction id="I7b9b4ad0-6eb5-4928-b09d-16264ff8f120" response-identifier="I7b9b4ad0-6eb5-4928-b09d-16264ff8f120"/> </p>
                            </div>
                            <div id="answer">

                            </div>
                            <div id="itemgeneral">
                                <br/>
                                <p id="c1-id-11">En een stukje algemene tekst</p>
                            </div>
                        </div>
                    </div>
                </qti-item-body>
            </wrapper>

        Private _itemBody10 As XElement =
            <wrapper>
                <qti-item-body class="defaultBody">
                    <div class="content">
                        <div class="div_left" xmlns="http://www.w3.org/1999/xhtml">
                            <!-- div_left_inner is needed to create the 50px margin between the left and right columns in dual column mode -->
                            <div class="div_left_inner">
                                <div id="leftbody">
                                    <p id="c1-id-11">In de schets is punt E het snijpunt van AB en CD.<br id="c1-id-12"/>In deze figuur geldt het volgende:</p>
                                    <p id="c1-id-13">
                                        <img style="VERTICAL-ALIGN: 0px" id="b09be1a2-d59c-4c96-acc1-67cf49221ae0" alt="" src="resource://package/MFI_2015826_10_12_12_390.png" ismathmlimage="true"/>
                                    </p>
                                    <p id="c1-id-14">
                                        <img style="VERTICAL-ALIGN: 0px" id="3a7f2267-7a17-44eb-9936-96bf4cbba1d0" alt="" src="resource://package/MFI_2015826_10_12_12_534.png" ismathmlimage="true"/>
                                    </p>
                                    <p id="c1-id-15">
                                        <img style="VERTICAL-ALIGN: 0px" id="b36befbe-6336-4101-a50f-f3980a7e3fd0" alt="" src="resource://package/MFI_2015826_10_12_12_571.png" ismathmlimage="true"/>
                                    </p>
                                    <p id="c1-id-16">
                                        <br id="c1-id-17"/>
                                        <img id="IMG_a5201df6-0ecb-4256-a215-8782f8fd6561" src="resource://package/itembank-dtt-hv-025.gif" width="430" height="222" alt="" isinlineelement="true"/> </p>
                                </div>
                            </div>
                        </div>
                        <div class="div_right" xmlns="http://www.w3.org/1999/xhtml">
                            <!-- div_right_inner is needed to create the 50px margin between the left and right columns in dual column mode -->
                            <div class="div_right_inner">
                                <div id="itemquestion">
                                    <p id="c1-id-11">
                                        <strong id="c1-id-12">Om <img style="VERTICAL-ALIGN: 0px" id="b4a6cc4b-32c2-4956-a6ad-95d45bdfb9a7" alt="" src="resource://package/MFI_2015826_10_12_58_140.png" ismathmlimage="true"/> te berekenen, maak ik gebruik van de volgende eigenschap(pen): </strong>
                                    </p>
                                    <p id="c1-id-13">Er kunnen meerdere antwoorden goed zijn.</p>
                                    <p id="c1-id-14">
                                        <qti-choice-interaction id="I6cc75685-147c-4a91-a899-791aa0201e6f" class="" shuffle="false" response-identifier="I6cc75685-147c-4a91-a899-791aa0201e6f" max-choices="0">
                                            <qti-simple-choice identifier="I6cc75685-147c-4a91-a899-791aa0201e6fA">
                                                <p id="c1-id-11">a</p>
                                            </qti-simple-choice>
                                            <qti-simple-choice identifier="I6cc75685-147c-4a91-a899-791aa0201e6fB">
                                                <p id="c1-id-11">b</p>
                                            </qti-simple-choice>
                                            <qti-simple-choice identifier="I6cc75685-147c-4a91-a899-791aa0201e6fC">
                                                <p id="c1-id-11">c</p>
                                            </qti-simple-choice>
                                            <qti-simple-choice identifier="I6cc75685-147c-4a91-a899-791aa0201e6fD">
                                                <p id="c1-id-11">d</p>
                                            </qti-simple-choice>
                                            <qti-simple-choice identifier="I6cc75685-147c-4a91-a899-791aa0201e6fE">
                                                <p id="c1-id-11">e</p>
                                            </qti-simple-choice>
                                            <qti-simple-choice identifier="I6cc75685-147c-4a91-a899-791aa0201e6fF">
                                                <p id="c1-id-11">f</p>
                                            </qti-simple-choice>
                                        </qti-choice-interaction> </p>
                                    <p id="c1-id-15">
                                        <strong id="c1-id-16">Hoe groot is hoek D?</strong>
                                    </p>
                                    <p id="c1-id-17">
                                        <img style="VERTICAL-ALIGN: 0px" id="1e8fbdde-0311-428b-8d96-b388f3991e6b" alt="" src="resource://package/MFI_2015826_10_18_42_229.png" ismathmlimage="true"/> °</p>
                                </div>
                                <div id="answer">
                                </div>
                            </div>
                        </div>
                    </div>
                </qti-item-body>
            </wrapper>

        Private _itemBody11 As XElement =
           <wrapper>
               <qti-stylesheet href="resource://package/itemstyle.css" type="text/css"/>
               <qti-stylesheet href="resource://package/userstyle.css" type="text/css"/>
               <qti-item-body class="defaultBody">
                   <div class="content">
                       <div class="div_left">
                           <!-- div_left_inner is needed to create the 50px margin between the left and right columns in dual column mode -->
                           <div class="div_left_inner">
                               <p id="c1-id-11">new body</p>
                           </div>
                       </div>
                       <div class="div_right">
                           <!-- div_right_inner is needed to create the 50px margin between the left and right columns in dual column mode -->
                           <div class="div_right_inner">
                               <div id="body">
                                   <p id="c1-id-11">
                                       <span class="inlinecontrol">
                                           <qti-custom-interaction response-identifier="I59fb10a4-be7d-4c15-b655-ccd57bfacfd6">
                                               <object xmlns:html="http://www.w3.org/1999/xhtml" type="application/javascript" height="400" width="400" data="resource://package/dtt-wi-DWOtest1CI.ci">
                                                   <!-- + 1 because first entry is reserved to save state -->
                                                   <param name="responseLength" value="1" valuetype="DATA"/>
                                               </object>
                                           </qti-custom-interaction>
                                       </span> </p>
                                   <p id="c1-id-12">
                                       <span class="inlinecontrol">
                                           <qti-custom-interaction response-identifier="I880ab1e3-b1ea-4e1f-a769-cf763f00969c">
                                               <object xmlns:html="http://www.w3.org/1999/xhtml" type="application/javascript" height="400" width="400" data="resource://package/test1.ci">
                                                   <!-- + 1 because first entry is reserved to save state -->
                                                   <param name="responseLength" value="1" valuetype="DATA"/>
                                               </object>
                                           </qti-custom-interaction>
                                       </span> </p>
                               </div>
                               <div id="question">
                                   <p id="c1-id-11">Que ?</p>
                                   <p id="c1-id-12">
                                       <span class="inlinecontrol">
                                           <qti-custom-interaction response-identifier="Ie7ea5301-245a-44c6-a961-a4864bd35c64">
                                               <object xmlns:html="http://www.w3.org/1999/xhtml" type="application/vnd.GeoGebra.file" height="400" width="400" data="resource://package/dtt-wi-GGBtest.ggb">
                                                   <!-- extra parameters to show/hide toolbars, etc - presets have to be determinded first -->
                                                   <param name="enableRightClick" value="false" valuetype="DATA"/>
                                                   <param name="showToolbar" value="true" valuetype="DATA"/>
                                                   <param name="showMenuBar" value="false" valuetype="DATA"/>
                                                   <param name="showAlgebraInput" value="false" valuetype="DATA"/>
                                                   <param name="showResetIcon" value="false" valuetype="DATA"/>
                                                   <param name="customToolbar" value="0 | 1 7 46 | 2 18 15 | 10 34 20 21 | 16 51 | 4 3 8 9 | 29 30 32 31 | 17 | 40 41 42 27 28 35 6" valuetype="DATA"/>
                                               </object>
                                           </qti-custom-interaction>
                                       </span> </p>
                               </div>
                               <div id="mc">
                                   <qti-choice-interaction id="choiceInteraction1" class="" max-choices="1" shuffle="false" response-identifier="mc">
                                       <qti-simple-choice identifier="A">
                                           <p id="c1-id-11">new a</p>
                                       </qti-simple-choice>
                                       <qti-simple-choice identifier="B">
                                           <p id="c1-id-11">new b</p>
                                       </qti-simple-choice>
                                       <qti-simple-choice identifier="C">
                                           <p id="c1-id-11">new c</p>
                                       </qti-simple-choice>
                                       <qti-simple-choice identifier="D">
                                           <p id="c1-id-11">new d</p>
                                       </qti-simple-choice>
                                   </qti-choice-interaction>
                               </div>
                           </div>
                       </div>
                   </div>
               </qti-item-body>
           </wrapper>

#End Region

#Region "Solution"

        ReadOnly _solution1 As XElement =
            <solution>
                <keyFindings>
                    <keyFinding id="gapController" scoringMethod="Dichotomous">
                        <keyFact id="1-I499a63a9-fdf8-43eb-bdce-e1ecabfa2bed" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I499a63a9-fdf8-43eb-bdce-e1ecabfa2bed" occur="1">
                                <integerRangeValue rangeEnd="5" rangeStart="4"/>
                            </keyValue>
                        </keyFact>
                        <keyFactSet>
                            <keyFact id="1-Ia896c73f-84fa-4ead-b3fc-210882efb8b9" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="Ia896c73f-84fa-4ead-b3fc-210882efb8b9" occur="1">
                                    <integerValue>
                                        <typedValue>3</typedValue>
                                    </integerValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="1-I0dec2572-468c-49d9-bdff-c2482ec461c1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I0dec2572-468c-49d9-bdff-c2482ec461c1" occur="1">
                                    <integerValue>
                                        <typedValue>0</typedValue>
                                    </integerValue>
                                    <integerValue>
                                        <typedValue>2</typedValue>
                                    </integerValue>
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

        ReadOnly _solution2 As XElement =
            <solution>
                <keyFindings>
                    <keyFinding id="gapController" scoringMethod="Dichotomous">
                        <keyFact id="1-I899a54e6-ecf1-4875-83f9-8be5c4503614" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I899a54e6-ecf1-4875-83f9-8be5c4503614" occur="1">
                                <stringValue>
                                    <typedValue>12:00</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFactSet>
                            <keyFact id="1-I0de6ade6-f82f-4111-b045-a3493f2d1ba6" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I0de6ade6-f82f-4111-b045-a3493f2d1ba6" occur="1">
                                    <stringValue>
                                        <typedValue>10:00</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="1-I52e3c4ab-3e4c-4b90-8dc7-90d05d4a9d22" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I52e3c4ab-3e4c-4b90-8dc7-90d05d4a9d22" occur="1">
                                    <integerValue>
                                        <typedValue>5</typedValue>
                                    </integerValue>
                                </keyValue>
                            </keyFact>
                        </keyFactSet>
                        <keyFactSet>
                            <keyFact id="1-I0de6ade6-f82f-4111-b045-a3493f2d1ba6" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I0de6ade6-f82f-4111-b045-a3493f2d1ba6" occur="1">
                                    <stringValue>
                                        <typedValue>11:00</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="1-I52e3c4ab-3e4c-4b90-8dc7-90d05d4a9d22" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I52e3c4ab-3e4c-4b90-8dc7-90d05d4a9d22" occur="1">
                                    <integerValue>
                                        <typedValue>4</typedValue>
                                    </integerValue>
                                </keyValue>
                            </keyFact>
                        </keyFactSet>
                    </keyFinding>
                </keyFindings>
                <conceptFindings>
                    <conceptFinding id="gapController" scoringMethod="None">
                        <conceptFact id="1[*]-I899a54e6-ecf1-4875-83f9-8be5c4503614" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="I899a54e6-ecf1-4875-83f9-8be5c4503614" occur="1">
                                <catchAllValue/>
                            </conceptValue>
                            <concepts>
                                <concept value="1" code="Concept2.1"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="1-I899a54e6-ecf1-4875-83f9-8be5c4503614" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="I899a54e6-ecf1-4875-83f9-8be5c4503614" occur="1">
                                <stringValue>
                                    <typedValue>12:00</typedValue>
                                </stringValue>
                            </conceptValue>
                            <concepts>
                                <concept value="3" code="Concept2.1"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="1[1]-I899a54e6-ecf1-4875-83f9-8be5c4503614" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="I899a54e6-ecf1-4875-83f9-8be5c4503614" occur="1">
                                <stringValue>
                                    <typedValue>00:00</typedValue>
                                </stringValue>
                            </conceptValue>
                            <concepts>
                                <concept value="2" code="Concept2.1"/>
                            </concepts>
                        </conceptFact>
                        <conceptFactSet>
                            <conceptFact id="1-I0de6ade6-f82f-4111-b045-a3493f2d1ba6" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I0de6ade6-f82f-4111-b045-a3493f2d1ba6" occur="1">
                                    <stringValue>
                                        <typedValue>10:00</typedValue>
                                    </stringValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="1-I52e3c4ab-3e4c-4b90-8dc7-90d05d4a9d22" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I52e3c4ab-3e4c-4b90-8dc7-90d05d4a9d22" occur="1">
                                    <integerValue>
                                        <typedValue>5</typedValue>
                                    </integerValue>
                                </conceptValue>
                            </conceptFact>
                            <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                                <concept value="2" code="Concept2.1"/>
                            </concepts>
                        </conceptFactSet>
                        <conceptFactSet>
                            <conceptFact id="1-I0de6ade6-f82f-4111-b045-a3493f2d1ba6" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I0de6ade6-f82f-4111-b045-a3493f2d1ba6" occur="1">
                                    <stringValue>
                                        <typedValue>11:00</typedValue>
                                    </stringValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="1-I52e3c4ab-3e4c-4b90-8dc7-90d05d4a9d22" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I52e3c4ab-3e4c-4b90-8dc7-90d05d4a9d22" occur="1">
                                    <integerValue>
                                        <typedValue>4</typedValue>
                                    </integerValue>
                                </conceptValue>
                            </conceptFact>
                            <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                                <concept value="3" code="Concept2.1"/>
                            </concepts>
                        </conceptFactSet>
                        <conceptFactSet>
                            <conceptFact id="1[*]-I0de6ade6-f82f-4111-b045-a3493f2d1ba6" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I0de6ade6-f82f-4111-b045-a3493f2d1ba6" occur="1">
                                    <catchAllValue/>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="1[*]-I52e3c4ab-3e4c-4b90-8dc7-90d05d4a9d22" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I52e3c4ab-3e4c-4b90-8dc7-90d05d4a9d22" occur="1">
                                    <catchAllValue/>
                                </conceptValue>
                            </conceptFact>
                            <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                                <concept value="1" code="Concept2.1"/>
                            </concepts>
                        </conceptFactSet>
                    </conceptFinding>
                </conceptFindings>
                <aspectReferences/>
                <ItemScoreTranslationTable>
                    <ItemScoreTranslationTableEntry rawScore="0" translatedScore="0"/>
                    <ItemScoreTranslationTableEntry rawScore="1" translatedScore="1"/>
                </ItemScoreTranslationTable>
            </solution>

        ReadOnly _solution3 As XElement =
            <solution>
                <keyFindings>
                    <keyFinding id="gapMatchController" scoringMethod="Dichotomous">
                        <keyFact id="Ia48dc13e-6ba6-4334-944f-04aa7b55ccd0-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="Ia48dc13e-6ba6-4334-944f-04aa7b55ccd0" occur="1">
                                <stringValue>
                                    <typedValue>C</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="Ied60a714-2bc7-4c12-badb-8afc5f9d30f7-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="Ied60a714-2bc7-4c12-badb-8afc5f9d30f7" occur="1">
                                <stringValue>
                                    <typedValue>A</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFactSet>
                            <keyFact id="If8d49acf-9059-4c11-9761-14cc7e799c10-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="If8d49acf-9059-4c11-9761-14cc7e799c10" occur="1">
                                    <stringValue>
                                        <typedValue>D</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="Idd3fcc2f-f9b9-4ffe-a169-17973ef1d4c9-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="Idd3fcc2f-f9b9-4ffe-a169-17973ef1d4c9" occur="1">
                                    <stringValue>
                                        <typedValue>B</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                        </keyFactSet>
                        <keyFactSet>
                            <keyFact id="If8d49acf-9059-4c11-9761-14cc7e799c10-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="If8d49acf-9059-4c11-9761-14cc7e799c10" occur="1">
                                    <stringValue>
                                        <typedValue>B</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="Idd3fcc2f-f9b9-4ffe-a169-17973ef1d4c9-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="Idd3fcc2f-f9b9-4ffe-a169-17973ef1d4c9" occur="1">
                                    <stringValue>
                                        <typedValue>D</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                        </keyFactSet>
                    </keyFinding>
                </keyFindings>
                <conceptFindings>
                    <conceptFinding id="gapMatchController" scoringMethod="None">
                        <conceptFact id="Ia48dc13e-6ba6-4334-944f-04aa7b55ccd0-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="Ia48dc13e-6ba6-4334-944f-04aa7b55ccd0" occur="1">
                                <stringValue>
                                    <typedValue>C</typedValue>
                                </stringValue>
                            </conceptValue>
                            <concepts>
                                <concept value="2" code="EN-SC-4"/>
                                <concept value="2" code="EN-SC-4.1"/>
                                <concept value="0" code="EN-SC-2"/>
                                <concept value="1" code="EN-SC-3"/>
                                <concept value="1" code="EN-SC-3.1"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="Ied60a714-2bc7-4c12-badb-8afc5f9d30f7-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="Ied60a714-2bc7-4c12-badb-8afc5f9d30f7" occur="1">
                                <stringValue>
                                    <typedValue>A</typedValue>
                                </stringValue>
                            </conceptValue>
                            <concepts>
                                <concept value="0" code="EN-SC-1"/>
                                <concept value="1" code="EN-SC-4"/>
                                <concept value="1" code="EN-SC-4.1"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="Ied60a714-2bc7-4c12-badb-8afc5f9d30f7[*]-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="Ied60a714-2bc7-4c12-badb-8afc5f9d30f7" occur="1">
                                <catchAllValue/>
                            </conceptValue>
                            <concepts>
                                <concept value="0" code="EN-SC-1"/>
                                <concept value="0" code="EN-SC-4"/>
                                <concept value="0" code="EN-SC-4.1"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="Ia48dc13e-6ba6-4334-944f-04aa7b55ccd0[*]-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="Ia48dc13e-6ba6-4334-944f-04aa7b55ccd0" occur="1">
                                <catchAllValue/>
                            </conceptValue>
                            <concepts>
                                <concept value="0" code="EN-SC-4"/>
                                <concept value="0" code="EN-SC-4.1"/>
                                <concept value="0" code="EN-SC-2"/>
                                <concept value="0" code="EN-SC-3"/>
                                <concept value="0" code="EN-SC-3.1"/>
                            </concepts>
                        </conceptFact>
                        <conceptFactSet>
                            <conceptFact id="If8d49acf-9059-4c11-9761-14cc7e799c10-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="If8d49acf-9059-4c11-9761-14cc7e799c10" occur="1">
                                    <stringValue>
                                        <typedValue>D</typedValue>
                                    </stringValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="Idd3fcc2f-f9b9-4ffe-a169-17973ef1d4c9-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Idd3fcc2f-f9b9-4ffe-a169-17973ef1d4c9" occur="1">
                                    <stringValue>
                                        <typedValue>B</typedValue>
                                    </stringValue>
                                </conceptValue>
                            </conceptFact>
                            <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                                <concept value="1" code="EN-SC-4"/>
                                <concept value="1" code="EN-SC-4.1"/>
                            </concepts>
                        </conceptFactSet>
                        <conceptFactSet>
                            <conceptFact id="If8d49acf-9059-4c11-9761-14cc7e799c10-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="If8d49acf-9059-4c11-9761-14cc7e799c10" occur="1">
                                    <stringValue>
                                        <typedValue>B</typedValue>
                                    </stringValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="Idd3fcc2f-f9b9-4ffe-a169-17973ef1d4c9-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Idd3fcc2f-f9b9-4ffe-a169-17973ef1d4c9" occur="1">
                                    <stringValue>
                                        <typedValue>D</typedValue>
                                    </stringValue>
                                </conceptValue>
                            </conceptFact>
                            <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                                <concept value="1" code="EN-SC-4"/>
                                <concept value="1" code="EN-SC-4.1"/>
                            </concepts>
                        </conceptFactSet>
                        <conceptFactSet>
                            <conceptFact id="If8d49acf-9059-4c11-9761-14cc7e799c10[*]-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="If8d49acf-9059-4c11-9761-14cc7e799c10" occur="1">
                                    <catchAllValue/>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="Idd3fcc2f-f9b9-4ffe-a169-17973ef1d4c9[*]-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Idd3fcc2f-f9b9-4ffe-a169-17973ef1d4c9" occur="1">
                                    <catchAllValue/>
                                </conceptValue>
                            </conceptFact>
                            <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                                <concept value="0" code="EN-SC-4"/>
                                <concept value="0" code="EN-SC-4.1"/>
                            </concepts>
                        </conceptFactSet>
                    </conceptFinding>
                </conceptFindings>
                <aspectReferences/>
                <ItemScoreTranslationTable>
                    <ItemScoreTranslationTableEntry rawScore="0" translatedScore="0"/>
                    <ItemScoreTranslationTableEntry rawScore="1" translatedScore="1"/>
                </ItemScoreTranslationTable>
            </solution>

        ReadOnly _solution4 As XElement =
            <solution>
                <keyFindings>
                    <keyFinding id="gapMatchController" scoringMethod="Polytomous">
                        <keyFact id="H-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="H-gapMatchController" occur="1">
                                <stringValue>
                                    <typedValue>A</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="E-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="E-gapMatchController" occur="1">
                                <stringValue>
                                    <typedValue>B</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="A-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="A-gapMatchController" occur="1">
                                <stringValue>
                                    <typedValue>A</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFactSet>
                            <keyFact id="F-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="F-gapMatchController" occur="1">
                                    <stringValue>
                                        <typedValue>B</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="I-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I-gapMatchController" occur="1">
                                    <stringValue>
                                        <typedValue>A</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="G-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="G-gapMatchController" occur="1">
                                    <stringValue>
                                        <typedValue>A</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="D-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="D-gapMatchController" occur="1">
                                    <stringValue>
                                        <typedValue>B</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                        </keyFactSet>
                        <keyFactSet>
                            <keyFact id="B-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="B-gapMatchController" occur="1">
                                    <stringValue>
                                        <typedValue>A</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="C-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="C-gapMatchController" occur="1">
                                    <stringValue>
                                        <typedValue>A</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                        </keyFactSet>
                    </keyFinding>
                </keyFindings>
                <conceptFindings>
                    <conceptFinding id="gapMatchController" scoringMethod="None">
                        <conceptFact id="H-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="H-gapMatchController" occur="1">
                                <stringValue>
                                    <typedValue>A</typedValue>
                                </stringValue>
                            </conceptValue>
                            <concepts>
                                <concept value="1" code="2 Tekstopbouw"/>
                                <concept value="1" code="2.1 Hoofd- en bijzaken onderscheiden"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="E-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="E-gapMatchController" occur="1">
                                <stringValue>
                                    <typedValue>B</typedValue>
                                </stringValue>
                            </conceptValue>
                            <concepts>
                                <concept value="1" code="3 Tekststructuur"/>
                                <concept value="1" code="3.1 Passende tekstindeling en opmaak gebruiken"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="A-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="A-gapMatchController" occur="1">
                                <stringValue>
                                    <typedValue>A</typedValue>
                                </stringValue>
                            </conceptValue>
                            <concepts>
                                <concept value="1" code="2 Tekstopbouw"/>
                                <concept value="1" code="2.2 Passende argumentstructuur gebruiken (h/v)"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="H[*]-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="H[*]-gapMatchController" occur="1">
                                <catchAllValue/>
                            </conceptValue>
                            <concepts>
                                <concept value="0" code="2 Tekstopbouw"/>
                                <concept value="0" code="2.1 Hoofd- en bijzaken onderscheiden"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="A[*]-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="A[*]-gapMatchController" occur="1">
                                <catchAllValue/>
                            </conceptValue>
                            <concepts>
                                <concept value="0" code="2 Tekstopbouw"/>
                                <concept value="0" code="2.2 Passende argumentstructuur gebruiken (h/v)"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="E[*]-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="E[*]-gapMatchController" occur="1">
                                <catchAllValue/>
                            </conceptValue>
                            <concepts>
                                <concept value="0" code="3 Tekststructuur"/>
                                <concept value="0" code="3.1 Passende tekstindeling en opmaak gebruiken"/>
                            </concepts>
                        </conceptFact>
                        <conceptFactSet>
                            <conceptFact id="F-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="F-gapMatchController" occur="1">
                                    <stringValue>
                                        <typedValue>B</typedValue>
                                    </stringValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I-gapMatchController" occur="1">
                                    <stringValue>
                                        <typedValue>A</typedValue>
                                    </stringValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="G-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="G-gapMatchController" occur="1">
                                    <stringValue>
                                        <typedValue>A</typedValue>
                                    </stringValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="D-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="D-gapMatchController" occur="1">
                                    <stringValue>
                                        <typedValue>B</typedValue>
                                    </stringValue>
                                </conceptValue>
                            </conceptFact>
                            <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                                <concept value="1" code="1 Doel en Publiek"/>
                                <concept value="1" code="1.1 Rekening houden met voorkennis lezer"/>
                            </concepts>
                        </conceptFactSet>
                        <conceptFactSet>
                            <conceptFact id="B-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="B-gapMatchController" occur="1">
                                    <stringValue>
                                        <typedValue>A</typedValue>
                                    </stringValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="C-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="C-gapMatchController" occur="1">
                                    <stringValue>
                                        <typedValue>A</typedValue>
                                    </stringValue>
                                </conceptValue>
                            </conceptFact>
                            <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                                <concept value="1" code="1 Doel en Publiek"/>
                                <concept value="1" code="1.2 Rekening houden met verhouding tot lezer"/>
                            </concepts>
                        </conceptFactSet>
                        <conceptFactSet>
                            <conceptFact id="I[*]-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I[*]-gapMatchController" occur="1">
                                    <catchAllValue/>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="G[*]-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="G[*]-gapMatchController" occur="1">
                                    <catchAllValue/>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="F[*]-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="F[*]-gapMatchController" occur="1">
                                    <catchAllValue/>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="D[*]-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="D[*]-gapMatchController" occur="1">
                                    <catchAllValue/>
                                </conceptValue>
                            </conceptFact>
                            <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                                <concept value="0" code="1 Doel en Publiek"/>
                                <concept value="0" code="1.1 Rekening houden met voorkennis lezer"/>
                            </concepts>
                        </conceptFactSet>
                        <conceptFactSet>
                            <conceptFact id="B[*]-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="B[*]-gapMatchController" occur="1">
                                    <catchAllValue/>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="C[*]-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="C[*]-gapMatchController" occur="1">
                                    <catchAllValue/>
                                </conceptValue>
                            </conceptFact>
                            <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                                <concept value="0" code="1 Doel en Publiek"/>
                                <concept value="0" code="1.2 Rekening houden met verhouding tot lezer"/>
                            </concepts>
                        </conceptFactSet>
                    </conceptFinding>
                </conceptFindings>
                <aspectReferences/>
                <ItemScoreTranslationTable>
                    <ItemScoreTranslationTableEntry rawScore="0" translatedScore="0"/>
                    <ItemScoreTranslationTableEntry rawScore="1" translatedScore="1"/>
                    <ItemScoreTranslationTableEntry rawScore="2" translatedScore="2"/>
                    <ItemScoreTranslationTableEntry rawScore="3" translatedScore="3"/>
                    <ItemScoreTranslationTableEntry rawScore="4" translatedScore="4"/>
                    <ItemScoreTranslationTableEntry rawScore="5" translatedScore="5"/>
                </ItemScoreTranslationTable>
            </solution>

        ReadOnly _solution5 As XElement =
            <solution>
                <keyFindings>
                    <keyFinding id="gapController" scoringMethod="Dichotomous">
                        <keyFact id="1-Ifaf38ff9-00f5-448c-b86b-252db6377abf" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="Ifaf38ff9-00f5-448c-b86b-252db6377abf" occur="1">
                                <stringValue>
                                    <typedValue>nee</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFactSet>
                            <keyFact id="1-Ia7b2ea59-2a1d-43d1-a09f-fafff847191b" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="Ia7b2ea59-2a1d-43d1-a09f-fafff847191b" occur="1">
                                    <decimalValue>
                                        <typedValue>10.50</typedValue>
                                    </decimalValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="1-I59205989-12f5-4dbf-a5d0-e83e47d6c87f" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I59205989-12f5-4dbf-a5d0-e83e47d6c87f" occur="1">
                                    <stringValue>
                                        <typedValue>12:15</typedValue>
                                    </stringValue>
                                </keyValue>
                            </keyFact>
                        </keyFactSet>
                        <keyFactSet>
                            <keyFact id="1-Iebfe9a06-1bd8-4430-a1b8-412e268266f7" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="Iebfe9a06-1bd8-4430-a1b8-412e268266f7" occur="1">
                                    <integerValue>
                                        <typedValue>10</typedValue>
                                    </integerValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="1-I65bd20fb-80f1-4e34-95f8-b70ccf11cb9f" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="I65bd20fb-80f1-4e34-95f8-b70ccf11cb9f" occur="1">
                                    <decimalValue>
                                        <typedValue>15.50</typedValue>
                                    </decimalValue>
                                </keyValue>
                            </keyFact>
                        </keyFactSet>
                    </keyFinding>
                </keyFindings>
                <conceptFindings>
                    <conceptFinding id="gapController" scoringMethod="None">
                        <conceptFact id="1-Ifaf38ff9-00f5-448c-b86b-252db6377abf" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="Ifaf38ff9-00f5-448c-b86b-252db6377abf" occur="1">
                                <stringValue>
                                    <typedValue>nee</typedValue>
                                </stringValue>
                            </conceptValue>
                            <concepts>
                                <concept value="1" code="economie 1"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="1[*]-Ifaf38ff9-00f5-448c-b86b-252db6377abf" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="Ifaf38ff9-00f5-448c-b86b-252db6377abf" occur="1">
                                <catchAllValue/>
                            </conceptValue>
                            <concepts>
                                <concept value="0" code="economie 1"/>
                            </concepts>
                        </conceptFact>
                        <conceptFactSet>
                            <conceptFact id="1-Ia7b2ea59-2a1d-43d1-a09f-fafff847191b" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Ia7b2ea59-2a1d-43d1-a09f-fafff847191b" occur="1">
                                    <decimalValue>
                                        <typedValue>10.50</typedValue>
                                    </decimalValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="1-I59205989-12f5-4dbf-a5d0-e83e47d6c87f" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I59205989-12f5-4dbf-a5d0-e83e47d6c87f" occur="1">
                                    <stringValue>
                                        <typedValue>12:15</typedValue>
                                    </stringValue>
                                </conceptValue>
                            </conceptFact>
                            <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                                <concept value="0" code="economie 2"/>
                                <concept value="1" code="economie 2.2"/>
                            </concepts>
                        </conceptFactSet>
                        <conceptFactSet>
                            <conceptFact id="1-Iebfe9a06-1bd8-4430-a1b8-412e268266f7" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Iebfe9a06-1bd8-4430-a1b8-412e268266f7" occur="1">
                                    <integerValue>
                                        <typedValue>10</typedValue>
                                    </integerValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="1-I65bd20fb-80f1-4e34-95f8-b70ccf11cb9f" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I65bd20fb-80f1-4e34-95f8-b70ccf11cb9f" occur="1">
                                    <decimalValue>
                                        <typedValue>15.50</typedValue>
                                    </decimalValue>
                                </conceptValue>
                            </conceptFact>
                            <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                                <concept value="1" code="economie 2"/>
                            </concepts>
                        </conceptFactSet>
                        <conceptFactSet>
                            <conceptFact id="1[*]-Ia7b2ea59-2a1d-43d1-a09f-fafff847191b" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Ia7b2ea59-2a1d-43d1-a09f-fafff847191b" occur="1">
                                    <catchAllValue/>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="1[*]-I59205989-12f5-4dbf-a5d0-e83e47d6c87f" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I59205989-12f5-4dbf-a5d0-e83e47d6c87f" occur="1">
                                    <catchAllValue/>
                                </conceptValue>
                            </conceptFact>
                            <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                                <concept value="0" code="economie 2"/>
                                <concept value="0" code="economie 2.2"/>
                            </concepts>
                        </conceptFactSet>
                        <conceptFactSet>
                            <conceptFact id="1[*]-Iebfe9a06-1bd8-4430-a1b8-412e268266f7" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Iebfe9a06-1bd8-4430-a1b8-412e268266f7" occur="1">
                                    <catchAllValue/>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="1[*]-I65bd20fb-80f1-4e34-95f8-b70ccf11cb9f" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I65bd20fb-80f1-4e34-95f8-b70ccf11cb9f" occur="1">
                                    <catchAllValue/>
                                </conceptValue>
                            </conceptFact>
                            <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                                <concept value="0" code="economie 2"/>
                            </concepts>
                        </conceptFactSet>
                    </conceptFinding>
                </conceptFindings>
                <aspectReferences/>
                <ItemScoreTranslationTable>
                    <ItemScoreTranslationTableEntry rawScore="0" translatedScore="0"/>
                    <ItemScoreTranslationTableEntry rawScore="1" translatedScore="1"/>
                </ItemScoreTranslationTable>
            </solution>

        ReadOnly _solution6 As XElement =
            <solution>
                <keyFindings>
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
                </keyFindings>
                <conceptFindings>
                    <conceptFinding id="hottextController" scoringMethod="None">
                        <conceptFact id="Ice5853d6-5a4b-4fc6-9298-7bc4047d0e26-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="Ice5853d6-5a4b-4fc6-9298-7bc4047d0e26-hottextController" occur="1">
                                <booleanValue>
                                    <typedValue>false</typedValue>
                                </booleanValue>
                            </conceptValue>
                            <concepts>
                                <concept value="0" code="Concept1"/>
                                <concept value="0" code="Concept1.1"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="I1752ec64-4652-4723-b3b0-0404b15a0e6d-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="I1752ec64-4652-4723-b3b0-0404b15a0e6d-hottextController" occur="1">
                                <booleanValue>
                                    <typedValue>false</typedValue>
                                </booleanValue>
                            </conceptValue>
                        </conceptFact>
                        <conceptFact id="Iff799b19-6c0e-406a-ad0d-63f470eac66f-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="Iff799b19-6c0e-406a-ad0d-63f470eac66f-hottextController" occur="1">
                                <booleanValue>
                                    <typedValue>true</typedValue>
                                </booleanValue>
                            </conceptValue>
                            <concepts>
                                <concept value="0" code="Concept1"/>
                                <concept value="1" code="Concept1.2"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="I88bb2ba0-543d-44e5-977a-1754f8a1d505-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="I88bb2ba0-543d-44e5-977a-1754f8a1d505-hottextController" occur="1">
                                <booleanValue>
                                    <typedValue>false</typedValue>
                                </booleanValue>
                            </conceptValue>
                        </conceptFact>
                        <conceptFact id="I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a-hottextController" occur="1">
                                <booleanValue>
                                    <typedValue>false</typedValue>
                                </booleanValue>
                            </conceptValue>
                            <concepts>
                                <concept value="0" code="Concept1"/>
                                <concept value="0" code="Concept1.2"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b-hottextController" occur="1">
                                <booleanValue>
                                    <typedValue>true</typedValue>
                                </booleanValue>
                            </conceptValue>
                            <concepts>
                                <concept value="1" code="Concept2"/>
                                <concept value="1" code="Concept2.1"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea-hottextController" occur="1">
                                <booleanValue>
                                    <typedValue>true</typedValue>
                                </booleanValue>
                            </conceptValue>
                        </conceptFact>
                        <conceptFact id="I941e9a9a-fd22-43a4-acf2-aa862f943cfc-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="I941e9a9a-fd22-43a4-acf2-aa862f943cfc-hottextController" occur="1">
                                <booleanValue>
                                    <typedValue>false</typedValue>
                                </booleanValue>
                            </conceptValue>
                        </conceptFact>
                        <conceptFact id="I6eb7f951-31d1-46e2-89fb-c7eca3849f9d-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="I6eb7f951-31d1-46e2-89fb-c7eca3849f9d-hottextController" occur="1">
                                <booleanValue>
                                    <typedValue>false</typedValue>
                                </booleanValue>
                            </conceptValue>
                        </conceptFact>
                        <conceptFact id="I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4-hottextController" occur="1">
                                <booleanValue>
                                    <typedValue>true</typedValue>
                                </booleanValue>
                            </conceptValue>
                            <concepts>
                                <concept value="1" code="Concept2"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="Iad84d52a-4b1e-45b3-8067-96811e46ff7e-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="Iad84d52a-4b1e-45b3-8067-96811e46ff7e-hottextController" occur="1">
                                <booleanValue>
                                    <typedValue>true</typedValue>
                                </booleanValue>
                            </conceptValue>
                            <concepts>
                                <concept value="1" code="Concept2"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="Ice5853d6-5a4b-4fc6-9298-7bc4047d0e26[1]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="Ice5853d6-5a4b-4fc6-9298-7bc4047d0e26[1]-hottextController" occur="1">
                                <booleanValue>
                                    <typedValue>true</typedValue>
                                </booleanValue>
                            </conceptValue>
                            <concepts>
                                <concept value="0" code="Concept1"/>
                                <concept value="1" code="Concept1.1"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="Iff799b19-6c0e-406a-ad0d-63f470eac66f[1]-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="Iff799b19-6c0e-406a-ad0d-63f470eac66f[1]-hottextController" occur="1">
                                <booleanValue>
                                    <typedValue>false</typedValue>
                                </booleanValue>
                            </conceptValue>
                            <concepts>
                                <concept value="0" code="Concept1"/>
                                <concept value="0" code="Concept1.2"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="I1752ec64-4652-4723-b3b0-0404b15a0e6d[1]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="I1752ec64-4652-4723-b3b0-0404b15a0e6d[1]-hottextController" occur="1">
                                <booleanValue>
                                    <typedValue>true</typedValue>
                                </booleanValue>
                            </conceptValue>
                        </conceptFact>
                        <conceptFact id="I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b[1]-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b[1]-hottextController" occur="1">
                                <booleanValue>
                                    <typedValue>false</typedValue>
                                </booleanValue>
                            </conceptValue>
                            <concepts>
                                <concept value="0" code="Concept2"/>
                                <concept value="0" code="Concept2.1"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="1[*]-Input_I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="Input_I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b" occur="1">
                                <catchAllValue/>
                            </conceptValue>
                            <concepts>
                                <concept value="0" code="Concept2"/>
                                <concept value="0" code="Concept2.1"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4[1]-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4[1]-hottextController" occur="1">
                                <booleanValue>
                                    <typedValue>false</typedValue>
                                </booleanValue>
                            </conceptValue>
                            <concepts>
                                <concept value="0" code="Concept2"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="Iad84d52a-4b1e-45b3-8067-96811e46ff7e[1]-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="Iad84d52a-4b1e-45b3-8067-96811e46ff7e[1]-hottextController" occur="1">
                                <booleanValue>
                                    <typedValue>false</typedValue>
                                </booleanValue>
                            </conceptValue>
                            <concepts>
                                <concept value="0" code="Concept2"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a[1]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a[1]-hottextController" occur="1">
                                <booleanValue>
                                    <typedValue>true</typedValue>
                                </booleanValue>
                            </conceptValue>
                            <concepts>
                                <concept value="0" code="Concept1"/>
                                <concept value="1" code="Concept1.2"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="1-Input_Iff799b19-6c0e-406a-ad0d-63f470eac66f" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="Input_Iff799b19-6c0e-406a-ad0d-63f470eac66f" occur="1">
                                <stringValue>
                                    <typedValue>drie</typedValue>
                                </stringValue>
                            </conceptValue>
                            <concepts>
                                <concept value="0" code="Concept1"/>
                                <concept value="2" code="Concept1.1"/>
                                <concept value="2" code="Concept1.2"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="1-Input_I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="Input_I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b" occur="1">
                                <stringValue>
                                    <typedValue>zes</typedValue>
                                </stringValue>
                            </conceptValue>
                            <concepts>
                                <concept value="1" code="Concept2"/>
                                <concept value="1" code="Concept2.1"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="1-Input_I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="Input_I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea" occur="1">
                                <stringValue>
                                    <typedValue>heer</typedValue>
                                </stringValue>
                            </conceptValue>
                        </conceptFact>
                        <conceptFact id="1-Input_I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="Input_I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4" occur="1">
                                <stringValue>
                                    <typedValue>tien</typedValue>
                                </stringValue>
                            </conceptValue>
                            <concepts>
                                <concept value="0" code="Concept1"/>
                                <concept value="0" code="Concept1.1"/>
                                <concept value="1" code="Concept1.2"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="1-Input_Iad84d52a-4b1e-45b3-8067-96811e46ff7e" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="Input_Iad84d52a-4b1e-45b3-8067-96811e46ff7e" occur="1">
                                <stringValue>
                                    <typedValue>elf</typedValue>
                                </stringValue>
                            </conceptValue>
                            <concepts>
                                <concept value="0" code="Concept1"/>
                                <concept value="2" code="Concept1.1"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="1[*]-Input_Iff799b19-6c0e-406a-ad0d-63f470eac66f" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="Input_Iff799b19-6c0e-406a-ad0d-63f470eac66f" occur="1">
                                <catchAllValue/>
                            </conceptValue>
                            <concepts>
                                <concept value="0" code="Concept1"/>
                                <concept value="0" code="Concept1.1"/>
                                <concept value="0" code="Concept1.2"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="1[1]-Input_Iff799b19-6c0e-406a-ad0d-63f470eac66f" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="Input_Iff799b19-6c0e-406a-ad0d-63f470eac66f" occur="1">
                                <stringValue>
                                    <typedValue>water sport</typedValue>
                                </stringValue>
                            </conceptValue>
                            <concepts>
                                <concept value="0" code="Concept1"/>
                                <concept value="1" code="Concept1.2"/>
                                <concept value="0" code="Concept1.1"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="1[*]-Input_Iad84d52a-4b1e-45b3-8067-96811e46ff7e" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="Input_Iad84d52a-4b1e-45b3-8067-96811e46ff7e" occur="1">
                                <catchAllValue/>
                            </conceptValue>
                            <concepts>
                                <concept value="0" code="Concept1"/>
                                <concept value="0" code="Concept1.1"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="1[1]-Input_Iad84d52a-4b1e-45b3-8067-96811e46ff7e" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="Input_Iad84d52a-4b1e-45b3-8067-96811e46ff7e" occur="1">
                                <stringValue>
                                    <typedValue>elf</typedValue>
                                </stringValue>
                            </conceptValue>
                            <concepts>
                                <concept value="0" code="Concept1"/>
                                <concept value="1" code="Concept1.1"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="1[*]-Input_I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="Input_I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4" occur="1">
                                <catchAllValue/>
                            </conceptValue>
                            <concepts>
                                <concept value="0" code="Concept1"/>
                                <concept value="1" code="Concept1.1"/>
                                <concept value="0" code="Concept1.2"/>
                            </concepts>
                        </conceptFact>
                    </conceptFinding>
                </conceptFindings>
                <aspectReferences/>
                <ItemScoreTranslationTable>
                    <ItemScoreTranslationTableEntry rawScore="0" translatedScore="0"/>
                    <ItemScoreTranslationTableEntry rawScore="1" translatedScore="1"/>
                </ItemScoreTranslationTable>
            </solution>

        ReadOnly _solution7 As XElement =
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
                </keyFindings>
                <conceptFindings>
                    <conceptFinding id="hottextController" scoringMethod="None">
                        <conceptFactSet>
                            <conceptFact id="Ice5853d6-5a4b-4fc6-9298-7bc4047d0e26-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Ice5853d6-5a4b-4fc6-9298-7bc4047d0e26-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I1752ec64-4652-4723-b3b0-0404b15a0e6d-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I1752ec64-4652-4723-b3b0-0404b15a0e6d-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="Iff799b19-6c0e-406a-ad0d-63f470eac66f-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Iff799b19-6c0e-406a-ad0d-63f470eac66f-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>true</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I88bb2ba0-543d-44e5-977a-1754f8a1d505-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I88bb2ba0-543d-44e5-977a-1754f8a1d505-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>true</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>true</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I941e9a9a-fd22-43a4-acf2-aa862f943cfc-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I941e9a9a-fd22-43a4-acf2-aa862f943cfc-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I6eb7f951-31d1-46e2-89fb-c7eca3849f9d-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I6eb7f951-31d1-46e2-89fb-c7eca3849f9d-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>true</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="Iad84d52a-4b1e-45b3-8067-96811e46ff7e-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Iad84d52a-4b1e-45b3-8067-96811e46ff7e-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>true</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="1-Input_Iff799b19-6c0e-406a-ad0d-63f470eac66f" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Input_Iff799b19-6c0e-406a-ad0d-63f470eac66f" occur="1">
                                    <stringValue>
                                        <typedValue>drie</typedValue>
                                    </stringValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="1-Input_I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Input_I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b" occur="1">
                                    <stringValue>
                                        <typedValue>zes</typedValue>
                                    </stringValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="1-Input_I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Input_I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea" occur="1">
                                    <stringValue>
                                        <typedValue>heer</typedValue>
                                    </stringValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="1-Input_I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Input_I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4" occur="1">
                                    <stringValue>
                                        <typedValue>tien</typedValue>
                                    </stringValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="1-Input_Iad84d52a-4b1e-45b3-8067-96811e46ff7e" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Input_Iad84d52a-4b1e-45b3-8067-96811e46ff7e" occur="1">
                                    <stringValue>
                                        <typedValue>elf</typedValue>
                                    </stringValue>
                                </conceptValue>
                            </conceptFact>
                            <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                                <concept value="10" code="Concept1"/>
                                <concept value="12" code="Concept1.2"/>
                                <concept value="11" code="Concept1.1"/>
                                <concept value="20" code="Concept2"/>
                                <concept value="21" code="Concept2.1"/>
                            </concepts>
                        </conceptFactSet>
                        <conceptFactSet>
                            <conceptFact id="Ice5853d6-5a4b-4fc6-9298-7bc4047d0e26-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Ice5853d6-5a4b-4fc6-9298-7bc4047d0e26-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I1752ec64-4652-4723-b3b0-0404b15a0e6d-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I1752ec64-4652-4723-b3b0-0404b15a0e6d-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="Iff799b19-6c0e-406a-ad0d-63f470eac66f-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Iff799b19-6c0e-406a-ad0d-63f470eac66f-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I88bb2ba0-543d-44e5-977a-1754f8a1d505-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I88bb2ba0-543d-44e5-977a-1754f8a1d505-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>true</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I941e9a9a-fd22-43a4-acf2-aa862f943cfc-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I941e9a9a-fd22-43a4-acf2-aa862f943cfc-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I6eb7f951-31d1-46e2-89fb-c7eca3849f9d-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I6eb7f951-31d1-46e2-89fb-c7eca3849f9d-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>true</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="Iad84d52a-4b1e-45b3-8067-96811e46ff7e-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Iad84d52a-4b1e-45b3-8067-96811e46ff7e-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>true</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="1-Input_I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Input_I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b" occur="1">
                                    <stringValue>
                                        <typedValue>zes</typedValue>
                                    </stringValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="1-Input_I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Input_I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4" occur="1">
                                    <stringValue>
                                        <typedValue>tien</typedValue>
                                    </stringValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="1-Input_Iad84d52a-4b1e-45b3-8067-96811e46ff7e" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Input_Iad84d52a-4b1e-45b3-8067-96811e46ff7e" occur="1">
                                    <stringValue>
                                        <typedValue>elf</typedValue>
                                    </stringValue>
                                </conceptValue>
                            </conceptFact>
                            <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                                <concept value="2" code="Concept1"/>
                                <concept value="2" code="Concept1.2"/>
                                <concept value="2" code="Concept1.1"/>
                                <concept value="2" code="Concept2"/>
                                <concept value="2" code="Concept2.1"/>
                            </concepts>
                        </conceptFactSet>
                        <conceptFactSet>
                            <conceptFact id="Ice5853d6-5a4b-4fc6-9298-7bc4047d0e26-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Ice5853d6-5a4b-4fc6-9298-7bc4047d0e26-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I1752ec64-4652-4723-b3b0-0404b15a0e6d-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I1752ec64-4652-4723-b3b0-0404b15a0e6d-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="Iff799b19-6c0e-406a-ad0d-63f470eac66f-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Iff799b19-6c0e-406a-ad0d-63f470eac66f-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>true</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I88bb2ba0-543d-44e5-977a-1754f8a1d505-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I88bb2ba0-543d-44e5-977a-1754f8a1d505-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>true</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I941e9a9a-fd22-43a4-acf2-aa862f943cfc-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I941e9a9a-fd22-43a4-acf2-aa862f943cfc-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I6eb7f951-31d1-46e2-89fb-c7eca3849f9d-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I6eb7f951-31d1-46e2-89fb-c7eca3849f9d-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="Iad84d52a-4b1e-45b3-8067-96811e46ff7e-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Iad84d52a-4b1e-45b3-8067-96811e46ff7e-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="1-Input_Iff799b19-6c0e-406a-ad0d-63f470eac66f" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Input_Iff799b19-6c0e-406a-ad0d-63f470eac66f" occur="1">
                                    <stringValue>
                                        <typedValue>drie</typedValue>
                                    </stringValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="1-Input_I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Input_I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea" occur="1">
                                    <stringValue>
                                        <typedValue>heer</typedValue>
                                    </stringValue>
                                </conceptValue>
                            </conceptFact>
                            <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                                <concept value="1" code="Concept1"/>
                                <concept value="1" code="Concept1.2"/>
                                <concept value="1" code="Concept1.1"/>
                                <concept value="1" code="Concept2"/>
                                <concept value="1" code="Concept2.1"/>
                            </concepts>
                        </conceptFactSet>
                        <conceptFactSet>
                            <conceptFact id="Ice5853d6-5a4b-4fc6-9298-7bc4047d0e26[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Ice5853d6-5a4b-4fc6-9298-7bc4047d0e26-hottextController" occur="1">
                                    <catchAllValue/>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I1752ec64-4652-4723-b3b0-0404b15a0e6d[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I1752ec64-4652-4723-b3b0-0404b15a0e6d-hottextController" occur="1">
                                    <catchAllValue/>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="Iff799b19-6c0e-406a-ad0d-63f470eac66f[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Iff799b19-6c0e-406a-ad0d-63f470eac66f-hottextController" occur="1">
                                    <catchAllValue/>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I88bb2ba0-543d-44e5-977a-1754f8a1d505[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I88bb2ba0-543d-44e5-977a-1754f8a1d505-hottextController" occur="1">
                                    <catchAllValue/>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a-hottextController" occur="1">
                                    <catchAllValue/>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b-hottextController" occur="1">
                                    <catchAllValue/>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea-hottextController" occur="1">
                                    <catchAllValue/>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I941e9a9a-fd22-43a4-acf2-aa862f943cfc[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I941e9a9a-fd22-43a4-acf2-aa862f943cfc-hottextController" occur="1">
                                    <catchAllValue/>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I6eb7f951-31d1-46e2-89fb-c7eca3849f9d[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I6eb7f951-31d1-46e2-89fb-c7eca3849f9d-hottextController" occur="1">
                                    <catchAllValue/>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4-hottextController" occur="1">
                                    <catchAllValue/>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="Iad84d52a-4b1e-45b3-8067-96811e46ff7e[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Iad84d52a-4b1e-45b3-8067-96811e46ff7e-hottextController" occur="1">
                                    <catchAllValue/>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="1[*]-Input_Iff799b19-6c0e-406a-ad0d-63f470eac66f" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Input_Iff799b19-6c0e-406a-ad0d-63f470eac66f" occur="1">
                                    <catchAllValue/>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="1[*]-Input_I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Input_I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b" occur="1">
                                    <catchAllValue/>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="1[*]-Input_I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Input_I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea" occur="1">
                                    <catchAllValue/>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="1[*]-Input_I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Input_I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4" occur="1">
                                    <catchAllValue/>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="1[*]-Input_Iad84d52a-4b1e-45b3-8067-96811e46ff7e" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Input_Iad84d52a-4b1e-45b3-8067-96811e46ff7e" occur="1">
                                    <catchAllValue/>
                                </conceptValue>
                            </conceptFact>
                            <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                                <concept value="0" code="Concept1"/>
                                <concept value="0" code="Concept1.2"/>
                                <concept value="1" code="Concept1.1"/>
                                <concept value="0" code="Concept2"/>
                                <concept value="1" code="Concept2.1"/>
                            </concepts>
                        </conceptFactSet>
                    </conceptFinding>
                </conceptFindings>
                <aspectReferences/>
                <ItemScoreTranslationTable>
                    <ItemScoreTranslationTableEntry rawScore="0" translatedScore="0"/>
                    <ItemScoreTranslationTableEntry rawScore="1" translatedScore="1"/>
                </ItemScoreTranslationTable>
            </solution>

        ReadOnly _solution8 As XElement =
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
                <conceptFindings>
                    <conceptFinding id="hottextController" scoringMethod="None">
                        <conceptFactSet>
                            <conceptFact id="Ice5853d6-5a4b-4fc6-9298-7bc4047d0e26-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Ice5853d6-5a4b-4fc6-9298-7bc4047d0e26-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I1752ec64-4652-4723-b3b0-0404b15a0e6d-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I1752ec64-4652-4723-b3b0-0404b15a0e6d-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="Iff799b19-6c0e-406a-ad0d-63f470eac66f-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Iff799b19-6c0e-406a-ad0d-63f470eac66f-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>true</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I88bb2ba0-543d-44e5-977a-1754f8a1d505-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I88bb2ba0-543d-44e5-977a-1754f8a1d505-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>true</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>true</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I941e9a9a-fd22-43a4-acf2-aa862f943cfc-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I941e9a9a-fd22-43a4-acf2-aa862f943cfc-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I6eb7f951-31d1-46e2-89fb-c7eca3849f9d-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I6eb7f951-31d1-46e2-89fb-c7eca3849f9d-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>true</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="Iad84d52a-4b1e-45b3-8067-96811e46ff7e-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Iad84d52a-4b1e-45b3-8067-96811e46ff7e-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>true</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                                <concept value="10" code="Concept1"/>
                                <concept value="12" code="Concept1.2"/>
                                <concept value="11" code="Concept1.1"/>
                                <concept value="20" code="Concept2"/>
                                <concept value="21" code="Concept2.1"/>
                            </concepts>
                        </conceptFactSet>
                        <conceptFactSet>
                            <conceptFact id="Ice5853d6-5a4b-4fc6-9298-7bc4047d0e26-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Ice5853d6-5a4b-4fc6-9298-7bc4047d0e26-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I1752ec64-4652-4723-b3b0-0404b15a0e6d-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I1752ec64-4652-4723-b3b0-0404b15a0e6d-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="Iff799b19-6c0e-406a-ad0d-63f470eac66f-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Iff799b19-6c0e-406a-ad0d-63f470eac66f-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I88bb2ba0-543d-44e5-977a-1754f8a1d505-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I88bb2ba0-543d-44e5-977a-1754f8a1d505-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>true</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I941e9a9a-fd22-43a4-acf2-aa862f943cfc-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I941e9a9a-fd22-43a4-acf2-aa862f943cfc-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I6eb7f951-31d1-46e2-89fb-c7eca3849f9d-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I6eb7f951-31d1-46e2-89fb-c7eca3849f9d-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>true</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="Iad84d52a-4b1e-45b3-8067-96811e46ff7e-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Iad84d52a-4b1e-45b3-8067-96811e46ff7e-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>true</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                                <concept value="3" code="Concept1"/>
                                <concept value="3" code="Concept1.2"/>
                                <concept value="3" code="Concept1.1"/>
                                <concept value="3" code="Concept2"/>
                                <concept value="3" code="Concept2.1"/>
                            </concepts>
                        </conceptFactSet>
                        <conceptFactSet>
                            <conceptFact id="Ice5853d6-5a4b-4fc6-9298-7bc4047d0e26-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Ice5853d6-5a4b-4fc6-9298-7bc4047d0e26-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I1752ec64-4652-4723-b3b0-0404b15a0e6d-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I1752ec64-4652-4723-b3b0-0404b15a0e6d-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="Iff799b19-6c0e-406a-ad0d-63f470eac66f-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Iff799b19-6c0e-406a-ad0d-63f470eac66f-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>true</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I88bb2ba0-543d-44e5-977a-1754f8a1d505-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I88bb2ba0-543d-44e5-977a-1754f8a1d505-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>true</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I941e9a9a-fd22-43a4-acf2-aa862f943cfc-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I941e9a9a-fd22-43a4-acf2-aa862f943cfc-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I6eb7f951-31d1-46e2-89fb-c7eca3849f9d-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I6eb7f951-31d1-46e2-89fb-c7eca3849f9d-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="Iad84d52a-4b1e-45b3-8067-96811e46ff7e-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Iad84d52a-4b1e-45b3-8067-96811e46ff7e-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                                <concept value="2" code="Concept1"/>
                                <concept value="2" code="Concept1.2"/>
                                <concept value="2" code="Concept1.1"/>
                                <concept value="2" code="Concept2"/>
                                <concept value="2" code="Concept2.1"/>
                            </concepts>
                        </conceptFactSet>
                        <conceptFactSet>
                            <conceptFact id="1-Input_Iff799b19-6c0e-406a-ad0d-63f470eac66f" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Input_Iff799b19-6c0e-406a-ad0d-63f470eac66f" occur="1">
                                    <stringValue>
                                        <typedValue>drie</typedValue>
                                    </stringValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="1-Input_I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Input_I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b" occur="1">
                                    <stringValue>
                                        <typedValue>zes</typedValue>
                                    </stringValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="1-Input_I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Input_I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea" occur="1">
                                    <stringValue>
                                        <typedValue>heer</typedValue>
                                    </stringValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="1-Input_I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Input_I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4" occur="1">
                                    <stringValue>
                                        <typedValue>tien</typedValue>
                                    </stringValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="1-Input_Iad84d52a-4b1e-45b3-8067-96811e46ff7e" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Input_Iad84d52a-4b1e-45b3-8067-96811e46ff7e" occur="1">
                                    <stringValue>
                                        <typedValue>elf</typedValue>
                                    </stringValue>
                                </conceptValue>
                            </conceptFact>
                            <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                                <concept value="2" code="Concept1"/>
                                <concept value="2" code="Concept1.1"/>
                                <concept value="2" code="Concept2"/>
                                <concept value="2" code="Concept2.1"/>
                            </concepts>
                        </conceptFactSet>
                        <conceptFactSet>
                            <conceptFact id="1-Input_I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Input_I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4" occur="1">
                                    <stringValue>
                                        <typedValue>Ver kaak</typedValue>
                                    </stringValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="1-Input_Iad84d52a-4b1e-45b3-8067-96811e46ff7e" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Input_Iad84d52a-4b1e-45b3-8067-96811e46ff7e" occur="1">
                                    <stringValue>
                                        <typedValue>elf</typedValue>
                                    </stringValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="1-Input_I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Input_I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b" occur="1">
                                    <stringValue>
                                        <typedValue>Bzes</typedValue>
                                    </stringValue>
                                </conceptValue>
                            </conceptFact>
                        </conceptFactSet>
                        <conceptFactSet>
                            <conceptFact id="Ice5853d6-5a4b-4fc6-9298-7bc4047d0e26[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Ice5853d6-5a4b-4fc6-9298-7bc4047d0e26-hottextController" occur="1">
                                    <catchAllValue/>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I1752ec64-4652-4723-b3b0-0404b15a0e6d[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I1752ec64-4652-4723-b3b0-0404b15a0e6d-hottextController" occur="1">
                                    <catchAllValue/>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="Iff799b19-6c0e-406a-ad0d-63f470eac66f[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Iff799b19-6c0e-406a-ad0d-63f470eac66f-hottextController" occur="1">
                                    <catchAllValue/>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I88bb2ba0-543d-44e5-977a-1754f8a1d505[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I88bb2ba0-543d-44e5-977a-1754f8a1d505-hottextController" occur="1">
                                    <catchAllValue/>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a-hottextController" occur="1">
                                    <catchAllValue/>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b-hottextController" occur="1">
                                    <catchAllValue/>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea-hottextController" occur="1">
                                    <catchAllValue/>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I941e9a9a-fd22-43a4-acf2-aa862f943cfc[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I941e9a9a-fd22-43a4-acf2-aa862f943cfc-hottextController" occur="1">
                                    <catchAllValue/>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I6eb7f951-31d1-46e2-89fb-c7eca3849f9d[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I6eb7f951-31d1-46e2-89fb-c7eca3849f9d-hottextController" occur="1">
                                    <catchAllValue/>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4-hottextController" occur="1">
                                    <catchAllValue/>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="Iad84d52a-4b1e-45b3-8067-96811e46ff7e[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Iad84d52a-4b1e-45b3-8067-96811e46ff7e-hottextController" occur="1">
                                    <catchAllValue/>
                                </conceptValue>
                            </conceptFact>
                            <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                                <concept value="0" code="Concept1"/>
                                <concept value="0" code="Concept1.2"/>
                                <concept value="0" code="Concept1.1"/>
                                <concept value="0" code="Concept2"/>
                                <concept value="0" code="Concept2.1"/>
                            </concepts>
                        </conceptFactSet>
                        <conceptFactSet>
                            <conceptFact id="1[*]-Input_Iff799b19-6c0e-406a-ad0d-63f470eac66f" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Input_Iff799b19-6c0e-406a-ad0d-63f470eac66f" occur="1">
                                    <catchAllValue/>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="1[*]-Input_I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Input_I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b" occur="1">
                                    <catchAllValue/>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="1[*]-Input_I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Input_I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea" occur="1">
                                    <catchAllValue/>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="1[*]-Input_I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Input_I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4" occur="1">
                                    <catchAllValue/>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="1[*]-Input_Iad84d52a-4b1e-45b3-8067-96811e46ff7e" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Input_Iad84d52a-4b1e-45b3-8067-96811e46ff7e" occur="1">
                                    <catchAllValue/>
                                </conceptValue>
                            </conceptFact>
                            <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                                <concept value="0" code="Concept1"/>
                                <concept value="1" code="Concept1.1"/>
                                <concept value="0" code="Concept2"/>
                                <concept value="1" code="Concept2.1"/>
                            </concepts>
                        </conceptFactSet>
                        <conceptFactSet>
                            <conceptFact id="1[2]-Input_I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Input_I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b" occur="1">
                                    <stringValue>
                                        <typedValue>Bzes</typedValue>
                                    </stringValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="1[2]-Input_Iff799b19-6c0e-406a-ad0d-63f470eac66f" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Input_Iff799b19-6c0e-406a-ad0d-63f470eac66f" occur="1">
                                    <stringValue>
                                        <typedValue>drie</typedValue>
                                    </stringValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="1[2]-Input_I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Input_I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea" occur="1">
                                    <stringValue>
                                        <typedValue>heer</typedValue>
                                    </stringValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="1[2]-Input_I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Input_I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4" occur="1">
                                    <stringValue>
                                        <typedValue>tien</typedValue>
                                    </stringValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="1[2]-Input_Iad84d52a-4b1e-45b3-8067-96811e46ff7e" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Input_Iad84d52a-4b1e-45b3-8067-96811e46ff7e" occur="1">
                                    <stringValue>
                                        <typedValue>elf</typedValue>
                                    </stringValue>
                                </conceptValue>
                            </conceptFact>
                            <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                                <concept value="1" code="Concept1"/>
                                <concept value="1" code="Concept1.1"/>
                                <concept value="1" code="Concept2"/>
                                <concept value="1" code="Concept2.1"/>
                            </concepts>
                        </conceptFactSet>
                        <conceptFactSet>
                            <conceptFact id="1[3]-Input_Iff799b19-6c0e-406a-ad0d-63f470eac66f" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Input_Iff799b19-6c0e-406a-ad0d-63f470eac66f" occur="1">
                                    <stringValue>
                                        <typedValue>drie</typedValue>
                                    </stringValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="1[3]-Input_I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Input_I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b" occur="1">
                                    <stringValue>
                                        <typedValue>zes</typedValue>
                                    </stringValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="1[3]-Input_I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Input_I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea" occur="1">
                                    <stringValue>
                                        <typedValue>heer</typedValue>
                                    </stringValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="1[3]-Input_I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Input_I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4" occur="1">
                                    <stringValue>
                                        <typedValue>tien</typedValue>
                                    </stringValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="1[3]-Input_Iad84d52a-4b1e-45b3-8067-96811e46ff7e" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Input_Iad84d52a-4b1e-45b3-8067-96811e46ff7e" occur="1">
                                    <stringValue>
                                        <typedValue>elf</typedValue>
                                    </stringValue>
                                </conceptValue>
                            </conceptFact>
                            <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                                <concept value="1" code="Concept1"/>
                                <concept value="2" code="Concept1.1"/>
                                <concept value="1" code="Concept2"/>
                                <concept value="1" code="Concept2.1"/>
                            </concepts>
                        </conceptFactSet>
                    </conceptFinding>
                </conceptFindings>
                <aspectReferences/>
                <ItemScoreTranslationTable>
                    <ItemScoreTranslationTableEntry rawScore="0" translatedScore="0"/>
                    <ItemScoreTranslationTableEntry rawScore="1" translatedScore="1"/>
                </ItemScoreTranslationTable>
            </solution>

        ReadOnly _solution9 As XElement =
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
                <conceptFindings>
                    <conceptFinding id="hottextController" scoringMethod="None">
                        <conceptFact id="1-Input_Iff799b19-6c0e-406a-ad0d-63f470eac66f" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="Input_Iff799b19-6c0e-406a-ad0d-63f470eac66f" occur="1">
                                <stringValue>
                                    <typedValue>drie</typedValue>
                                </stringValue>
                            </conceptValue>
                            <concepts>
                                <concept value="2" code="Concept1"/>
                                <concept value="2" code="Concept1.1"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="1-Input_I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="Input_I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b" occur="1">
                                <stringValue>
                                    <typedValue>zes</typedValue>
                                </stringValue>
                            </conceptValue>
                            <concepts>
                                <concept value="2" code="Concept2"/>
                                <concept value="2" code="Concept2.1"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="1-Input_I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="Input_I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea" occur="1">
                                <stringValue>
                                    <typedValue>heer</typedValue>
                                </stringValue>
                            </conceptValue>
                        </conceptFact>
                        <conceptFact id="1-Input_I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="Input_I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4" occur="1">
                                <stringValue>
                                    <typedValue>tien</typedValue>
                                </stringValue>
                            </conceptValue>
                        </conceptFact>
                        <conceptFact id="1-Input_Iad84d52a-4b1e-45b3-8067-96811e46ff7e" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="Input_Iad84d52a-4b1e-45b3-8067-96811e46ff7e" occur="1">
                                <stringValue>
                                    <typedValue>elf</typedValue>
                                </stringValue>
                            </conceptValue>
                            <concepts>
                                <concept value="2" code="Concept1"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="1[*]-Input_Iff799b19-6c0e-406a-ad0d-63f470eac66f" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="Input_Iff799b19-6c0e-406a-ad0d-63f470eac66f" occur="1">
                                <catchAllValue/>
                            </conceptValue>
                            <concepts>
                                <concept value="0" code="Concept1"/>
                                <concept value="0" code="Concept1.1"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="1[1]-Input_Iff799b19-6c0e-406a-ad0d-63f470eac66f" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="Input_Iff799b19-6c0e-406a-ad0d-63f470eac66f" occur="1">
                                <stringValue>
                                    <typedValue>water sport</typedValue>
                                </stringValue>
                            </conceptValue>
                            <concepts>
                                <concept value="0" code="Concept1"/>
                                <concept value="1" code="Concept1.1"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="1[*]-Input_I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="Input_I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b" occur="1">
                                <catchAllValue/>
                            </conceptValue>
                            <concepts>
                                <concept value="0" code="Concept2"/>
                                <concept value="0" code="Concept2.1"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="1[*]-Input_Iad84d52a-4b1e-45b3-8067-96811e46ff7e" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="Input_Iad84d52a-4b1e-45b3-8067-96811e46ff7e" occur="1">
                                <catchAllValue/>
                            </conceptValue>
                            <concepts>
                                <concept value="0" code="Concept1"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="1[1]-Input_Iad84d52a-4b1e-45b3-8067-96811e46ff7e" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="Input_Iad84d52a-4b1e-45b3-8067-96811e46ff7e" occur="1">
                                <stringValue>
                                    <typedValue>elf</typedValue>
                                </stringValue>
                            </conceptValue>
                            <concepts>
                                <concept value="1" code="Concept1"/>
                            </concepts>
                        </conceptFact>
                        <conceptFactSet>
                            <conceptFact id="Ice5853d6-5a4b-4fc6-9298-7bc4047d0e26-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Ice5853d6-5a4b-4fc6-9298-7bc4047d0e26-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I1752ec64-4652-4723-b3b0-0404b15a0e6d-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I1752ec64-4652-4723-b3b0-0404b15a0e6d-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="Iff799b19-6c0e-406a-ad0d-63f470eac66f-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Iff799b19-6c0e-406a-ad0d-63f470eac66f-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>true</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I88bb2ba0-543d-44e5-977a-1754f8a1d505-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I88bb2ba0-543d-44e5-977a-1754f8a1d505-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>true</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>true</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I941e9a9a-fd22-43a4-acf2-aa862f943cfc-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I941e9a9a-fd22-43a4-acf2-aa862f943cfc-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I6eb7f951-31d1-46e2-89fb-c7eca3849f9d-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I6eb7f951-31d1-46e2-89fb-c7eca3849f9d-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>true</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="Iad84d52a-4b1e-45b3-8067-96811e46ff7e-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Iad84d52a-4b1e-45b3-8067-96811e46ff7e-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>true</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                                <concept value="3" code="Concept1"/>
                                <concept value="3" code="Concept1.1"/>
                                <concept value="3" code="Concept2"/>
                                <concept value="3" code="Concept2.1"/>
                            </concepts>
                        </conceptFactSet>
                        <conceptFactSet>
                            <conceptFact id="Ice5853d6-5a4b-4fc6-9298-7bc4047d0e26-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Ice5853d6-5a4b-4fc6-9298-7bc4047d0e26-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I1752ec64-4652-4723-b3b0-0404b15a0e6d-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I1752ec64-4652-4723-b3b0-0404b15a0e6d-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="Iff799b19-6c0e-406a-ad0d-63f470eac66f-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Iff799b19-6c0e-406a-ad0d-63f470eac66f-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I88bb2ba0-543d-44e5-977a-1754f8a1d505-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I88bb2ba0-543d-44e5-977a-1754f8a1d505-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>true</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I941e9a9a-fd22-43a4-acf2-aa862f943cfc-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I941e9a9a-fd22-43a4-acf2-aa862f943cfc-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I6eb7f951-31d1-46e2-89fb-c7eca3849f9d-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I6eb7f951-31d1-46e2-89fb-c7eca3849f9d-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>true</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="Iad84d52a-4b1e-45b3-8067-96811e46ff7e-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Iad84d52a-4b1e-45b3-8067-96811e46ff7e-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>true</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                                <concept value="2" code="Concept1"/>
                                <concept value="2" code="Concept1.1"/>
                                <concept value="2" code="Concept2"/>
                                <concept value="2" code="Concept2.1"/>
                            </concepts>
                        </conceptFactSet>
                        <conceptFactSet>
                            <conceptFact id="Ice5853d6-5a4b-4fc6-9298-7bc4047d0e26-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Ice5853d6-5a4b-4fc6-9298-7bc4047d0e26-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I1752ec64-4652-4723-b3b0-0404b15a0e6d-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I1752ec64-4652-4723-b3b0-0404b15a0e6d-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="Iff799b19-6c0e-406a-ad0d-63f470eac66f-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Iff799b19-6c0e-406a-ad0d-63f470eac66f-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>true</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I88bb2ba0-543d-44e5-977a-1754f8a1d505-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I88bb2ba0-543d-44e5-977a-1754f8a1d505-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>true</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I941e9a9a-fd22-43a4-acf2-aa862f943cfc-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I941e9a9a-fd22-43a4-acf2-aa862f943cfc-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I6eb7f951-31d1-46e2-89fb-c7eca3849f9d-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I6eb7f951-31d1-46e2-89fb-c7eca3849f9d-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="Iad84d52a-4b1e-45b3-8067-96811e46ff7e-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Iad84d52a-4b1e-45b3-8067-96811e46ff7e-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                                <concept value="1" code="Concept1"/>
                                <concept value="1" code="Concept1.1"/>
                                <concept value="1" code="Concept2"/>
                                <concept value="1" code="Concept2.1"/>
                            </concepts>
                        </conceptFactSet>
                        <conceptFactSet>
                            <conceptFact id="Ice5853d6-5a4b-4fc6-9298-7bc4047d0e26[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Ice5853d6-5a4b-4fc6-9298-7bc4047d0e26-hottextController" occur="1">
                                    <catchAllValue/>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I1752ec64-4652-4723-b3b0-0404b15a0e6d[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I1752ec64-4652-4723-b3b0-0404b15a0e6d-hottextController" occur="1">
                                    <catchAllValue/>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="Iff799b19-6c0e-406a-ad0d-63f470eac66f[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Iff799b19-6c0e-406a-ad0d-63f470eac66f-hottextController" occur="1">
                                    <catchAllValue/>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I88bb2ba0-543d-44e5-977a-1754f8a1d505[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I88bb2ba0-543d-44e5-977a-1754f8a1d505-hottextController" occur="1">
                                    <catchAllValue/>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a-hottextController" occur="1">
                                    <catchAllValue/>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b-hottextController" occur="1">
                                    <catchAllValue/>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea-hottextController" occur="1">
                                    <catchAllValue/>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I941e9a9a-fd22-43a4-acf2-aa862f943cfc[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I941e9a9a-fd22-43a4-acf2-aa862f943cfc-hottextController" occur="1">
                                    <catchAllValue/>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I6eb7f951-31d1-46e2-89fb-c7eca3849f9d[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I6eb7f951-31d1-46e2-89fb-c7eca3849f9d-hottextController" occur="1">
                                    <catchAllValue/>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4-hottextController" occur="1">
                                    <catchAllValue/>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="Iad84d52a-4b1e-45b3-8067-96811e46ff7e[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Iad84d52a-4b1e-45b3-8067-96811e46ff7e-hottextController" occur="1">
                                    <catchAllValue/>
                                </conceptValue>
                            </conceptFact>
                            <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                                <concept value="0" code="Concept1"/>
                                <concept value="0" code="Concept1.1"/>
                                <concept value="0" code="Concept2"/>
                                <concept value="0" code="Concept2.1"/>
                            </concepts>
                        </conceptFactSet>
                        <conceptFactSet>
                            <conceptFact id="Ice5853d6-5a4b-4fc6-9298-7bc4047d0e26[3]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Ice5853d6-5a4b-4fc6-9298-7bc4047d0e26-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I1752ec64-4652-4723-b3b0-0404b15a0e6d[3]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I1752ec64-4652-4723-b3b0-0404b15a0e6d-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="Iff799b19-6c0e-406a-ad0d-63f470eac66f[3]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Iff799b19-6c0e-406a-ad0d-63f470eac66f-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I88bb2ba0-543d-44e5-977a-1754f8a1d505[3]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I88bb2ba0-543d-44e5-977a-1754f8a1d505-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a[3]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b[3]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea[3]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I941e9a9a-fd22-43a4-acf2-aa862f943cfc[3]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I941e9a9a-fd22-43a4-acf2-aa862f943cfc-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I6eb7f951-31d1-46e2-89fb-c7eca3849f9d[3]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I6eb7f951-31d1-46e2-89fb-c7eca3849f9d-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4[3]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>true</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="Iad84d52a-4b1e-45b3-8067-96811e46ff7e[3]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="Iad84d52a-4b1e-45b3-8067-96811e46ff7e-hottextController" occur="1">
                                    <booleanValue>
                                        <typedValue>true</typedValue>
                                    </booleanValue>
                                </conceptValue>
                            </conceptFact>
                            <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                                <concept value="0" code="Concept1"/>
                                <concept value="1" code="Concept1.1"/>
                                <concept value="0" code="Concept2"/>
                                <concept value="0" code="Concept2.1"/>
                            </concepts>
                        </conceptFactSet>
                    </conceptFinding>
                </conceptFindings>
                <aspectReferences/>
                <ItemScoreTranslationTable>
                    <ItemScoreTranslationTableEntry rawScore="0" translatedScore="0"/>
                    <ItemScoreTranslationTableEntry rawScore="1" translatedScore="1"/>
                </ItemScoreTranslationTable>
            </solution>

        ReadOnly _solution10 As XElement =
            <solution>
                <keyFindings>
                    <keyFinding id="gapController" scoringMethod="Dichotomous">
                        <keyFact id="Last-I5a35f7d3-acbc-4c02-8653-77e8bd20788c" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I5a35f7d3-acbc-4c02-8653-77e8bd20788c" occur="1">
                                <stringValue>
                                    <typedValue>&lt;math xmlns="http://www.w3.org/1998/Math/MathML"&gt;&lt;apply&gt;&lt;root&gt;&lt;/root&gt;&lt;cn&gt;144&lt;/cn&gt;&lt;/apply&gt;&lt;/math&gt;</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="Second-I5a35f7d3-acbc-4c02-8653-77e8bd20788c" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I5a35f7d3-acbc-4c02-8653-77e8bd20788c" occur="1">
                                <booleanValue>
                                    <typedValue>true</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="First-I5a35f7d3-acbc-4c02-8653-77e8bd20788c" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I5a35f7d3-acbc-4c02-8653-77e8bd20788c" occur="1">
                                <stringValue>
                                    <typedValue>&lt;math xmlns="http://www.w3.org/1998/Math/MathML"&gt;&lt;apply&gt;&lt;root&gt;&lt;/root&gt;&lt;degree&gt;&lt;cn&gt;2&lt;/cn&gt;&lt;/degree&gt;&lt;cn&gt;144&lt;/cn&gt;&lt;/apply&gt;&lt;/math&gt;</typedValue>
                                </stringValue>
                                <stringValue>
                                    <typedValue>&lt;math xmlns="http://www.w3.org/1998/Math/MathML"&gt;&lt;apply&gt;&lt;divide&gt;&lt;/divide&gt;&lt;cn&gt;2&lt;/cn&gt;&lt;cn&gt;4&lt;/cn&gt;&lt;/apply&gt;&lt;/math&gt;</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                    </keyFinding>
                </keyFindings>
                <conceptFindings>
                    <conceptFinding id="gapController" scoringMethod="None">
                        <conceptFact id="Last-I5a35f7d3-acbc-4c02-8653-77e8bd20788c" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="I5a35f7d3-acbc-4c02-8653-77e8bd20788c" occur="1">
                                <stringValue>
                                    <typedValue>&lt;math xmlns="http://www.w3.org/1998/Math/MathML"&gt;&lt;apply&gt;&lt;root&gt;&lt;/root&gt;&lt;cn&gt;144&lt;/cn&gt;&lt;/apply&gt;&lt;/math&gt;</typedValue>
                                </stringValue>
                            </conceptValue>
                            <concepts>
                                <concept value="0" code="Concept2"/>
                                <concept value="0" code="Concept2.1"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="Second-I5a35f7d3-acbc-4c02-8653-77e8bd20788c" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="I5a35f7d3-acbc-4c02-8653-77e8bd20788c" occur="1">
                                <booleanValue>
                                    <typedValue>true</typedValue>
                                </booleanValue>
                            </conceptValue>
                        </conceptFact>
                        <conceptFact id="First-I5a35f7d3-acbc-4c02-8653-77e8bd20788c" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="I5a35f7d3-acbc-4c02-8653-77e8bd20788c" occur="1">
                                <stringValue>
                                    <typedValue>&lt;math xmlns="http://www.w3.org/1998/Math/MathML"&gt;&lt;apply&gt;&lt;root&gt;&lt;/root&gt;&lt;degree&gt;&lt;cn&gt;2&lt;/cn&gt;&lt;/degree&gt;&lt;cn&gt;144&lt;/cn&gt;&lt;/apply&gt;&lt;/math&gt;</typedValue>
                                </stringValue>
                                <stringValue>
                                    <typedValue>&lt;math xmlns="http://www.w3.org/1998/Math/MathML"&gt;&lt;apply&gt;&lt;divide&gt;&lt;/divide&gt;&lt;cn&gt;2&lt;/cn&gt;&lt;cn&gt;4&lt;/cn&gt;&lt;/apply&gt;&lt;/math&gt;</typedValue>
                                </stringValue>
                            </conceptValue>
                            <concepts>
                                <concept value="2" code="Concept1"/>
                                <concept value="3" code="Concept1.1"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="First[*]-I5a35f7d3-acbc-4c02-8653-77e8bd20788c" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="I5a35f7d3-acbc-4c02-8653-77e8bd20788c" occur="1">
                                <catchAllValue/>
                            </conceptValue>
                            <concepts>
                                <concept value="0" code="Concept1"/>
                                <concept value="0" code="Concept1.1"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="First[1]-I5a35f7d3-acbc-4c02-8653-77e8bd20788c" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="I5a35f7d3-acbc-4c02-8653-77e8bd20788c" occur="1">
                                <stringValue>
                                    <typedValue>&lt;math xmlns="http://www.w3.org/1998/Math/MathML"&gt;&lt;apply&gt;&lt;selector&gt;&lt;/selector&gt;&lt;cn&gt;4&lt;/cn&gt;&lt;cn&gt;3&lt;/cn&gt;&lt;/apply&gt;&lt;/math&gt;</typedValue>
                                </stringValue>
                            </conceptValue>
                            <concepts>
                                <concept value="1" code="Concept1"/>
                                <concept value="0" code="Concept1.1"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="Last[*]-I5a35f7d3-acbc-4c02-8653-77e8bd20788c" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="I5a35f7d3-acbc-4c02-8653-77e8bd20788c" occur="1">
                                <catchAllValue/>
                            </conceptValue>
                            <concepts>
                                <concept value="0" code="Concept2"/>
                                <concept value="1" code="Concept2.1"/>
                            </concepts>
                        </conceptFact>
                    </conceptFinding>
                </conceptFindings>
                <aspectReferences/>
                <ItemScoreTranslationTable>
                    <ItemScoreTranslationTableEntry rawScore="0" translatedScore="0"/>
                    <ItemScoreTranslationTableEntry rawScore="1" translatedScore="1"/>
                </ItemScoreTranslationTable>
            </solution>

        ReadOnly _solution11 As XElement =
            <solution>
                <keyFindings>
                    <keyFinding id="gapController" scoringMethod="Dichotomous">
                        <keyFact id="Last-I5a35f7d3-acbc-4c02-8653-77e8bd20788c" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I5a35f7d3-acbc-4c02-8653-77e8bd20788c" occur="1">
                                <stringValue>
                                    <typedValue>&lt;math xmlns="http://www.w3.org/1998/Math/MathML"&gt;&lt;apply&gt;&lt;root&gt;&lt;/root&gt;&lt;cn&gt;144&lt;/cn&gt;&lt;/apply&gt;&lt;/math&gt;</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="Second-I5a35f7d3-acbc-4c02-8653-77e8bd20788c" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I5a35f7d3-acbc-4c02-8653-77e8bd20788c" occur="1">
                                <booleanValue>
                                    <typedValue>true</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="First-I5a35f7d3-acbc-4c02-8653-77e8bd20788c" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I5a35f7d3-acbc-4c02-8653-77e8bd20788c" occur="1">
                                <stringValue>
                                    <typedValue>&lt;math xmlns="http://www.w3.org/1998/Math/MathML"&gt;&lt;apply&gt;&lt;root&gt;&lt;/root&gt;&lt;degree&gt;&lt;cn&gt;2&lt;/cn&gt;&lt;/degree&gt;&lt;cn&gt;144&lt;/cn&gt;&lt;/apply&gt;&lt;/math&gt;</typedValue>
                                </stringValue>
                                <stringValue>
                                    <typedValue>&lt;math xmlns="http://www.w3.org/1998/Math/MathML"&gt;&lt;apply&gt;&lt;divide&gt;&lt;/divide&gt;&lt;cn&gt;2&lt;/cn&gt;&lt;cn&gt;4&lt;/cn&gt;&lt;/apply&gt;&lt;/math&gt;</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="First-I7b9b4ad0-6eb5-4928-b09d-16264ff8f120" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I7b9b4ad0-6eb5-4928-b09d-16264ff8f120" occur="1">
                                <stringValue>
                                    <typedValue>&lt;math xmlns="http://www.w3.org/1998/Math/MathML"&gt;&lt;apply&gt;&lt;times&gt;&lt;/times&gt;&lt;ci&gt;a&lt;/ci&gt;&lt;ci&gt;b&lt;/ci&gt;&lt;/apply&gt;&lt;/math&gt;</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="Last-I7b9b4ad0-6eb5-4928-b09d-16264ff8f120" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I7b9b4ad0-6eb5-4928-b09d-16264ff8f120" occur="1">
                                <stringValue>
                                    <typedValue>&lt;math xmlns="http://www.w3.org/1998/Math/MathML"&gt;&lt;apply&gt;&lt;plus&gt;&lt;/plus&gt;&lt;ci&gt;a&lt;/ci&gt;&lt;ci&gt;a&lt;/ci&gt;&lt;ci&gt;b&lt;/ci&gt;&lt;ci&gt;b&lt;/ci&gt;&lt;/apply&gt;&lt;/math&gt;</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                    </keyFinding>
                </keyFindings>
                <conceptFindings>
                    <conceptFinding id="gapController" scoringMethod="None">
                        <conceptFact id="Last-I5a35f7d3-acbc-4c02-8653-77e8bd20788c" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="I5a35f7d3-acbc-4c02-8653-77e8bd20788c" occur="1">
                                <stringValue>
                                    <typedValue>&lt;math xmlns="http://www.w3.org/1998/Math/MathML"&gt;&lt;apply&gt;&lt;root&gt;&lt;/root&gt;&lt;cn&gt;144&lt;/cn&gt;&lt;/apply&gt;&lt;/math&gt;</typedValue>
                                </stringValue>
                            </conceptValue>
                            <concepts>
                                <concept value="0" code="Concept2"/>
                                <concept value="0" code="Concept2.1"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="Second-I5a35f7d3-acbc-4c02-8653-77e8bd20788c" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="I5a35f7d3-acbc-4c02-8653-77e8bd20788c" occur="1">
                                <booleanValue>
                                    <typedValue>true</typedValue>
                                </booleanValue>
                            </conceptValue>
                        </conceptFact>
                        <conceptFact id="First-I5a35f7d3-acbc-4c02-8653-77e8bd20788c" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="I5a35f7d3-acbc-4c02-8653-77e8bd20788c" occur="1">
                                <stringValue>
                                    <typedValue>&lt;math xmlns="http://www.w3.org/1998/Math/MathML"&gt;&lt;apply&gt;&lt;root&gt;&lt;/root&gt;&lt;degree&gt;&lt;cn&gt;2&lt;/cn&gt;&lt;/degree&gt;&lt;cn&gt;144&lt;/cn&gt;&lt;/apply&gt;&lt;/math&gt;</typedValue>
                                </stringValue>
                                <stringValue>
                                    <typedValue>&lt;math xmlns="http://www.w3.org/1998/Math/MathML"&gt;&lt;apply&gt;&lt;divide&gt;&lt;/divide&gt;&lt;cn&gt;2&lt;/cn&gt;&lt;cn&gt;4&lt;/cn&gt;&lt;/apply&gt;&lt;/math&gt;</typedValue>
                                </stringValue>
                            </conceptValue>
                            <concepts>
                                <concept value="2" code="Concept1"/>
                                <concept value="3" code="Concept1.1"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="First[*]-I5a35f7d3-acbc-4c02-8653-77e8bd20788c" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="I5a35f7d3-acbc-4c02-8653-77e8bd20788c" occur="1">
                                <catchAllValue/>
                            </conceptValue>
                            <concepts>
                                <concept value="0" code="Concept1"/>
                                <concept value="0" code="Concept1.1"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="First[1]-I5a35f7d3-acbc-4c02-8653-77e8bd20788c" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="I5a35f7d3-acbc-4c02-8653-77e8bd20788c" occur="1">
                                <stringValue>
                                    <typedValue>&lt;math xmlns="http://www.w3.org/1998/Math/MathML"&gt;&lt;apply&gt;&lt;selector&gt;&lt;/selector&gt;&lt;cn&gt;4&lt;/cn&gt;&lt;cn&gt;3&lt;/cn&gt;&lt;/apply&gt;&lt;/math&gt;</typedValue>
                                </stringValue>
                            </conceptValue>
                            <concepts>
                                <concept value="1" code="Concept1"/>
                                <concept value="0" code="Concept1.1"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="Last[*]-I5a35f7d3-acbc-4c02-8653-77e8bd20788c" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="I5a35f7d3-acbc-4c02-8653-77e8bd20788c" occur="1">
                                <catchAllValue/>
                            </conceptValue>
                            <concepts>
                                <concept value="0" code="Concept2"/>
                                <concept value="1" code="Concept2.1"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="First-I7b9b4ad0-6eb5-4928-b09d-16264ff8f120" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="I7b9b4ad0-6eb5-4928-b09d-16264ff8f120" occur="1">
                                <stringValue>
                                    <typedValue>&lt;math xmlns="http://www.w3.org/1998/Math/MathML"&gt;&lt;apply&gt;&lt;times&gt;&lt;/times&gt;&lt;ci&gt;a&lt;/ci&gt;&lt;ci&gt;b&lt;/ci&gt;&lt;/apply&gt;&lt;/math&gt;</typedValue>
                                </stringValue>
                            </conceptValue>
                            <concepts>
                                <concept value="0" code="Concept1"/>
                                <concept value="2" code="Concept1.1"/>
                                <concept value="2" code="Concept1.2"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="Last-I7b9b4ad0-6eb5-4928-b09d-16264ff8f120" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="I7b9b4ad0-6eb5-4928-b09d-16264ff8f120" occur="1">
                                <stringValue>
                                    <typedValue>&lt;math xmlns="http://www.w3.org/1998/Math/MathML"&gt;&lt;apply&gt;&lt;plus&gt;&lt;/plus&gt;&lt;ci&gt;a&lt;/ci&gt;&lt;ci&gt;a&lt;/ci&gt;&lt;ci&gt;b&lt;/ci&gt;&lt;ci&gt;b&lt;/ci&gt;&lt;/apply&gt;&lt;/math&gt;</typedValue>
                                </stringValue>
                            </conceptValue>
                        </conceptFact>
                        <conceptFact id="First[*]-I7b9b4ad0-6eb5-4928-b09d-16264ff8f120" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="I7b9b4ad0-6eb5-4928-b09d-16264ff8f120" occur="1">
                                <catchAllValue/>
                            </conceptValue>
                            <concepts>
                                <concept value="0" code="Concept1"/>
                                <concept value="0" code="Concept1.1"/>
                                <concept value="0" code="Concept1.2"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="Second[*]-I7b9b4ad0-6eb5-4928-b09d-16264ff8f120" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="I7b9b4ad0-6eb5-4928-b09d-16264ff8f120" occur="1">
                                <catchAllValue/>
                            </conceptValue>
                            <concepts>
                                <concept value="0" code="Concept1"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="Second[0]-I7b9b4ad0-6eb5-4928-b09d-16264ff8f120" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="I7b9b4ad0-6eb5-4928-b09d-16264ff8f120" occur="1">
                                <booleanValue>
                                    <typedValue>true</typedValue>
                                </booleanValue>
                            </conceptValue>
                            <concepts>
                                <concept value="1" code="Concept1"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="Last[*]-I7b9b4ad0-6eb5-4928-b09d-16264ff8f120" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="I7b9b4ad0-6eb5-4928-b09d-16264ff8f120" occur="1">
                                <catchAllValue/>
                            </conceptValue>
                        </conceptFact>
                    </conceptFinding>
                </conceptFindings>
                <aspectReferences/>
                <ItemScoreTranslationTable>
                    <ItemScoreTranslationTableEntry rawScore="0" translatedScore="0"/>
                    <ItemScoreTranslationTableEntry rawScore="1" translatedScore="1"/>
                </ItemScoreTranslationTable>
            </solution>

        ReadOnly _solution12 As XElement =
            <solution>
                <keyFindings>
                    <keyFinding id="gapController" scoringMethod="Dichotomous">
                        <keyFact id="1-If5d399bf-f82e-4faf-bebd-c3bfa23fbfb1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="If5d399bf-f82e-4faf-bebd-c3bfa23fbfb1" occur="1">
                                <integerValue>
                                    <typedValue>12345</typedValue>
                                </integerValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="Last-I5a35f7d3-acbc-4c02-8653-77e8bd20788c" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I5a35f7d3-acbc-4c02-8653-77e8bd20788c" occur="1">
                                <stringValue>
                                    <typedValue>&lt;math xmlns="http://www.w3.org/1998/Math/MathML"&gt;&lt;apply&gt;&lt;root&gt;&lt;/root&gt;&lt;cn&gt;144&lt;/cn&gt;&lt;/apply&gt;&lt;/math&gt;</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="Second-I5a35f7d3-acbc-4c02-8653-77e8bd20788c" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I5a35f7d3-acbc-4c02-8653-77e8bd20788c" occur="1">
                                <booleanValue>
                                    <typedValue>true</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="First-I5a35f7d3-acbc-4c02-8653-77e8bd20788c" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I5a35f7d3-acbc-4c02-8653-77e8bd20788c" occur="1">
                                <stringValue>
                                    <typedValue>&lt;math xmlns="http://www.w3.org/1998/Math/MathML"&gt;&lt;apply&gt;&lt;root&gt;&lt;/root&gt;&lt;degree&gt;&lt;cn&gt;2&lt;/cn&gt;&lt;/degree&gt;&lt;cn&gt;144&lt;/cn&gt;&lt;/apply&gt;&lt;/math&gt;</typedValue>
                                </stringValue>
                                <stringValue>
                                    <typedValue>&lt;math xmlns="http://www.w3.org/1998/Math/MathML"&gt;&lt;apply&gt;&lt;divide&gt;&lt;/divide&gt;&lt;cn&gt;2&lt;/cn&gt;&lt;cn&gt;4&lt;/cn&gt;&lt;/apply&gt;&lt;/math&gt;</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="1-If7c2200d-0063-4a23-a58c-2a38deea8529" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="If7c2200d-0063-4a23-a58c-2a38deea8529" occur="1">
                                <stringValue>
                                    <typedValue>12:34</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="1-Ie7f3c962-96d9-417f-a793-0c506c67ec1d" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="Ie7f3c962-96d9-417f-a793-0c506c67ec1d" occur="1">
                                <stringValue>
                                    <typedValue>tekst</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="First-I7b9b4ad0-6eb5-4928-b09d-16264ff8f120" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I7b9b4ad0-6eb5-4928-b09d-16264ff8f120" occur="1">
                                <stringValue>
                                    <typedValue>&lt;math xmlns="http://www.w3.org/1998/Math/MathML"&gt;&lt;apply&gt;&lt;times&gt;&lt;/times&gt;&lt;ci&gt;a&lt;/ci&gt;&lt;ci&gt;b&lt;/ci&gt;&lt;/apply&gt;&lt;/math&gt;</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="Last-I7b9b4ad0-6eb5-4928-b09d-16264ff8f120" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I7b9b4ad0-6eb5-4928-b09d-16264ff8f120" occur="1">
                                <stringValue>
                                    <typedValue>&lt;math xmlns="http://www.w3.org/1998/Math/MathML"&gt;&lt;apply&gt;&lt;plus&gt;&lt;/plus&gt;&lt;ci&gt;a&lt;/ci&gt;&lt;ci&gt;a&lt;/ci&gt;&lt;ci&gt;b&lt;/ci&gt;&lt;ci&gt;b&lt;/ci&gt;&lt;/apply&gt;&lt;/math&gt;</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                    </keyFinding>
                </keyFindings>
                <conceptFindings>
                    <conceptFinding id="gapController" scoringMethod="None">
                        <conceptFact id="Last-I5a35f7d3-acbc-4c02-8653-77e8bd20788c" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="I5a35f7d3-acbc-4c02-8653-77e8bd20788c" occur="1">
                                <stringValue>
                                    <typedValue>&lt;math xmlns="http://www.w3.org/1998/Math/MathML"&gt;&lt;apply&gt;&lt;root&gt;&lt;/root&gt;&lt;cn&gt;144&lt;/cn&gt;&lt;/apply&gt;&lt;/math&gt;</typedValue>
                                </stringValue>
                            </conceptValue>
                            <concepts>
                                <concept value="0" code="Concept2"/>
                                <concept value="0" code="Concept2.1"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="Second-I5a35f7d3-acbc-4c02-8653-77e8bd20788c" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="I5a35f7d3-acbc-4c02-8653-77e8bd20788c" occur="1">
                                <booleanValue>
                                    <typedValue>true</typedValue>
                                </booleanValue>
                            </conceptValue>
                        </conceptFact>
                        <conceptFact id="First-I5a35f7d3-acbc-4c02-8653-77e8bd20788c" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="I5a35f7d3-acbc-4c02-8653-77e8bd20788c" occur="1">
                                <stringValue>
                                    <typedValue>&lt;math xmlns="http://www.w3.org/1998/Math/MathML"&gt;&lt;apply&gt;&lt;root&gt;&lt;/root&gt;&lt;degree&gt;&lt;cn&gt;2&lt;/cn&gt;&lt;/degree&gt;&lt;cn&gt;144&lt;/cn&gt;&lt;/apply&gt;&lt;/math&gt;</typedValue>
                                </stringValue>
                                <stringValue>
                                    <typedValue>&lt;math xmlns="http://www.w3.org/1998/Math/MathML"&gt;&lt;apply&gt;&lt;divide&gt;&lt;/divide&gt;&lt;cn&gt;2&lt;/cn&gt;&lt;cn&gt;4&lt;/cn&gt;&lt;/apply&gt;&lt;/math&gt;</typedValue>
                                </stringValue>
                            </conceptValue>
                            <concepts>
                                <concept value="2" code="Concept1"/>
                                <concept value="3" code="Concept1.1"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="First[*]-I5a35f7d3-acbc-4c02-8653-77e8bd20788c" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="I5a35f7d3-acbc-4c02-8653-77e8bd20788c" occur="1">
                                <catchAllValue/>
                            </conceptValue>
                            <concepts>
                                <concept value="0" code="Concept1"/>
                                <concept value="0" code="Concept1.1"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="First[1]-I5a35f7d3-acbc-4c02-8653-77e8bd20788c" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="I5a35f7d3-acbc-4c02-8653-77e8bd20788c" occur="1">
                                <stringValue>
                                    <typedValue>&lt;math xmlns="http://www.w3.org/1998/Math/MathML"&gt;&lt;apply&gt;&lt;selector&gt;&lt;/selector&gt;&lt;cn&gt;4&lt;/cn&gt;&lt;cn&gt;3&lt;/cn&gt;&lt;/apply&gt;&lt;/math&gt;</typedValue>
                                </stringValue>
                            </conceptValue>
                            <concepts>
                                <concept value="1" code="Concept1"/>
                                <concept value="0" code="Concept1.1"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="Last[*]-I5a35f7d3-acbc-4c02-8653-77e8bd20788c" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="I5a35f7d3-acbc-4c02-8653-77e8bd20788c" occur="1">
                                <catchAllValue/>
                            </conceptValue>
                            <concepts>
                                <concept value="0" code="Concept2"/>
                                <concept value="1" code="Concept2.1"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="First-I7b9b4ad0-6eb5-4928-b09d-16264ff8f120" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="I7b9b4ad0-6eb5-4928-b09d-16264ff8f120" occur="1">
                                <stringValue>
                                    <typedValue>&lt;math xmlns="http://www.w3.org/1998/Math/MathML"&gt;&lt;apply&gt;&lt;times&gt;&lt;/times&gt;&lt;ci&gt;a&lt;/ci&gt;&lt;ci&gt;b&lt;/ci&gt;&lt;/apply&gt;&lt;/math&gt;</typedValue>
                                </stringValue>
                            </conceptValue>
                            <concepts>
                                <concept value="0" code="Concept1"/>
                                <concept value="2" code="Concept1.1"/>
                                <concept value="2" code="Concept1.2"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="Last-I7b9b4ad0-6eb5-4928-b09d-16264ff8f120" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="I7b9b4ad0-6eb5-4928-b09d-16264ff8f120" occur="1">
                                <stringValue>
                                    <typedValue>&lt;math xmlns="http://www.w3.org/1998/Math/MathML"&gt;&lt;apply&gt;&lt;plus&gt;&lt;/plus&gt;&lt;ci&gt;a&lt;/ci&gt;&lt;ci&gt;a&lt;/ci&gt;&lt;ci&gt;b&lt;/ci&gt;&lt;ci&gt;b&lt;/ci&gt;&lt;/apply&gt;&lt;/math&gt;</typedValue>
                                </stringValue>
                            </conceptValue>
                        </conceptFact>
                        <conceptFact id="First[*]-I7b9b4ad0-6eb5-4928-b09d-16264ff8f120" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="I7b9b4ad0-6eb5-4928-b09d-16264ff8f120" occur="1">
                                <catchAllValue/>
                            </conceptValue>
                            <concepts>
                                <concept value="0" code="Concept1"/>
                                <concept value="0" code="Concept1.1"/>
                                <concept value="0" code="Concept1.2"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="Second[*]-I7b9b4ad0-6eb5-4928-b09d-16264ff8f120" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="I7b9b4ad0-6eb5-4928-b09d-16264ff8f120" occur="1">
                                <catchAllValue/>
                            </conceptValue>
                            <concepts>
                                <concept value="0" code="Concept1"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="Second[0]-I7b9b4ad0-6eb5-4928-b09d-16264ff8f120" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="I7b9b4ad0-6eb5-4928-b09d-16264ff8f120" occur="1">
                                <booleanValue>
                                    <typedValue>true</typedValue>
                                </booleanValue>
                            </conceptValue>
                            <concepts>
                                <concept value="1" code="Concept1"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="Last[*]-I7b9b4ad0-6eb5-4928-b09d-16264ff8f120" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="I7b9b4ad0-6eb5-4928-b09d-16264ff8f120" occur="1">
                                <catchAllValue/>
                            </conceptValue>
                        </conceptFact>
                        <conceptFact id="1[*]-If5d399bf-f82e-4faf-bebd-c3bfa23fbfb1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="If5d399bf-f82e-4faf-bebd-c3bfa23fbfb1" occur="1">
                                <catchAllValue/>
                            </conceptValue>
                        </conceptFact>
                        <conceptFact id="1-If5d399bf-f82e-4faf-bebd-c3bfa23fbfb1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="If5d399bf-f82e-4faf-bebd-c3bfa23fbfb1" occur="1">
                                <integerValue>
                                    <typedValue>12345</typedValue>
                                </integerValue>
                            </conceptValue>
                        </conceptFact>
                        <conceptFact id="1-If7c2200d-0063-4a23-a58c-2a38deea8529" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="If7c2200d-0063-4a23-a58c-2a38deea8529" occur="1">
                                <stringValue>
                                    <typedValue>12:34</typedValue>
                                </stringValue>
                            </conceptValue>
                        </conceptFact>
                        <conceptFact id="1-Ie7f3c962-96d9-417f-a793-0c506c67ec1d" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="Ie7f3c962-96d9-417f-a793-0c506c67ec1d" occur="1">
                                <stringValue>
                                    <typedValue>tekst</typedValue>
                                </stringValue>
                            </conceptValue>
                        </conceptFact>
                    </conceptFinding>
                </conceptFindings>
                <aspectReferences/>
                <ItemScoreTranslationTable>
                    <ItemScoreTranslationTableEntry rawScore="0" translatedScore="0"/>
                    <ItemScoreTranslationTableEntry rawScore="1" translatedScore="1"/>
                </ItemScoreTranslationTable>
            </solution>

        ReadOnly _solution13 As XElement =
            <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                <keyFindings>
                    <keyFinding id="IEF" scoringMethod="Dichotomous">
                        <keyFact id="A-I6cc75685-147c-4a91-a899-791aa0201e6f" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="A-I6cc75685-147c-4a91-a899-791aa0201e6f" occur="1">
                                <booleanValue>
                                    <typedValue>true</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="E-I6cc75685-147c-4a91-a899-791aa0201e6f" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="E-I6cc75685-147c-4a91-a899-791aa0201e6f" occur="1">
                                <booleanValue>
                                    <typedValue>false</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="F-I6cc75685-147c-4a91-a899-791aa0201e6f" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="F-I6cc75685-147c-4a91-a899-791aa0201e6f" occur="1">
                                <booleanValue>
                                    <typedValue>false</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="D-I6cc75685-147c-4a91-a899-791aa0201e6f" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="D-I6cc75685-147c-4a91-a899-791aa0201e6f" occur="1">
                                <booleanValue>
                                    <typedValue>false</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="C-I6cc75685-147c-4a91-a899-791aa0201e6f" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="C-I6cc75685-147c-4a91-a899-791aa0201e6f" occur="1">
                                <booleanValue>
                                    <typedValue>false</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="B-I6cc75685-147c-4a91-a899-791aa0201e6f" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="B-I6cc75685-147c-4a91-a899-791aa0201e6f" occur="1">
                                <booleanValue>
                                    <typedValue>true</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                    </keyFinding>
                </keyFindings>
                <conceptFindings>
                    <conceptFinding id="IEF" scoringMethod="None">
                        <conceptFact id="A-I6cc75685-147c-4a91-a899-791aa0201e6f" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="A-I6cc75685-147c-4a91-a899-791aa0201e6f" occur="1">
                                <booleanValue>
                                    <typedValue>true</typedValue>
                                </booleanValue>
                            </conceptValue>
                            <concepts>
                                <concept value="1" code="B. Getallen"/>
                                <concept value="1" code="B1"/>
                                <concept value="1" code="1. wiskundige structuur"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="E-I6cc75685-147c-4a91-a899-791aa0201e6f" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="E-I6cc75685-147c-4a91-a899-791aa0201e6f" occur="1">
                                <booleanValue>
                                    <typedValue>false</typedValue>
                                </booleanValue>
                            </conceptValue>
                            <concepts>
                                <concept value="1" code="B. Getallen"/>
                                <concept value="1" code="B1"/>
                                <concept value="1" code="1. wiskundige structuur"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="F-I6cc75685-147c-4a91-a899-791aa0201e6f" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="F-I6cc75685-147c-4a91-a899-791aa0201e6f" occur="1">
                                <booleanValue>
                                    <typedValue>false</typedValue>
                                </booleanValue>
                            </conceptValue>
                            <concepts>
                                <concept value="1" code="B. Getallen"/>
                                <concept value="1" code="B1"/>
                                <concept value="1" code="1. wiskundige structuur"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="D-I6cc75685-147c-4a91-a899-791aa0201e6f" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="D-I6cc75685-147c-4a91-a899-791aa0201e6f" occur="1">
                                <booleanValue>
                                    <typedValue>false</typedValue>
                                </booleanValue>
                            </conceptValue>
                            <concepts>
                                <concept value="1" code="B. Getallen"/>
                                <concept value="1" code="B1"/>
                                <concept value="1" code="1. wiskundige structuur"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="C-I6cc75685-147c-4a91-a899-791aa0201e6f" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="C-I6cc75685-147c-4a91-a899-791aa0201e6f" occur="1">
                                <booleanValue>
                                    <typedValue>false</typedValue>
                                </booleanValue>
                            </conceptValue>
                            <concepts>
                                <concept value="1" code="B. Getallen"/>
                                <concept value="1" code="B1"/>
                                <concept value="1" code="1. wiskundige structuur"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="B-I6cc75685-147c-4a91-a899-791aa0201e6f" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="B-I6cc75685-147c-4a91-a899-791aa0201e6f" occur="1">
                                <booleanValue>
                                    <typedValue>true</typedValue>
                                </booleanValue>
                            </conceptValue>
                            <concepts>
                                <concept value="1" code="B. Getallen"/>
                                <concept value="1" code="B1"/>
                                <concept value="1" code="1. wiskundige structuur"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="A[1]-I6cc75685-147c-4a91-a899-791aa0201e6f" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="A[1]-I6cc75685-147c-4a91-a899-791aa0201e6f" occur="1">
                                <booleanValue>
                                    <typedValue>false</typedValue>
                                </booleanValue>
                            </conceptValue>
                            <concepts>
                                <concept value="0" code="B. Getallen"/>
                                <concept value="0" code="B1"/>
                                <concept value="0" code="1. wiskundige structuur"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="B[1]-I6cc75685-147c-4a91-a899-791aa0201e6f" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="B[1]-I6cc75685-147c-4a91-a899-791aa0201e6f" occur="1">
                                <booleanValue>
                                    <typedValue>false</typedValue>
                                </booleanValue>
                            </conceptValue>
                            <concepts>
                                <concept value="0" code="B. Getallen"/>
                                <concept value="0" code="B1"/>
                                <concept value="0" code="1. wiskundige structuur"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="C[1]-I6cc75685-147c-4a91-a899-791aa0201e6f" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="C[1]-I6cc75685-147c-4a91-a899-791aa0201e6f" occur="1">
                                <booleanValue>
                                    <typedValue>true</typedValue>
                                </booleanValue>
                            </conceptValue>
                            <concepts>
                                <concept value="0" code="B. Getallen"/>
                                <concept value="0" code="B1"/>
                                <concept value="0" code="1. wiskundige structuur"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="D[1]-I6cc75685-147c-4a91-a899-791aa0201e6f" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="D[1]-I6cc75685-147c-4a91-a899-791aa0201e6f" occur="1">
                                <booleanValue>
                                    <typedValue>true</typedValue>
                                </booleanValue>
                            </conceptValue>
                            <concepts>
                                <concept value="0" code="B. Getallen"/>
                                <concept value="0" code="B1"/>
                                <concept value="0" code="1. wiskundige structuur"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="E[1]-I6cc75685-147c-4a91-a899-791aa0201e6f" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="E[1]-I6cc75685-147c-4a91-a899-791aa0201e6f" occur="1">
                                <booleanValue>
                                    <typedValue>true</typedValue>
                                </booleanValue>
                            </conceptValue>
                            <concepts>
                                <concept value="0" code="B. Getallen"/>
                                <concept value="0" code="B1"/>
                                <concept value="0" code="1. wiskundige structuur"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="F[1]-I6cc75685-147c-4a91-a899-791aa0201e6f" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="F[1]-I6cc75685-147c-4a91-a899-791aa0201e6f" occur="1">
                                <booleanValue>
                                    <typedValue>true</typedValue>
                                </booleanValue>
                            </conceptValue>
                            <concepts>
                                <concept value="0" code="B. Getallen"/>
                                <concept value="0" code="B1"/>
                                <concept value="0" code="1. wiskundige structuur"/>
                            </concepts>
                        </conceptFact>
                    </conceptFinding>
                </conceptFindings>
                <aspectReferences/>
                <ItemScoreTranslationTable>
                    <ItemScoreTranslationTableEntry rawScore="0" translatedScore="0"/>
                    <ItemScoreTranslationTableEntry rawScore="1" translatedScore="1"/>
                </ItemScoreTranslationTable>
            </solution>

        Private _solution14 As XElement =
            <solution>
                <keyFindings>
                    <keyFinding id="mc" scoringMethod="Dichotomous">
                        <keyFact id="B-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="mc" occur="1">
                                <stringValue>
                                    <typedValue>B</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="A-CI_SP_I880ab1e3-b1ea-4e1f-a769-cf763f00969c_0" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="CI_SP_I880ab1e3-b1ea-4e1f-a769-cf763f00969c_0" occur="1">
                                <stringComparisonValue>
                                    <typedComparisonValue>&lt;math  xmlns="http://www.w3.org/1998/Math/MathML"&gt;&lt;apply&gt;&lt;root&gt;&lt;/root&gt;&lt;cn&gt;121&lt;/cn&gt;&lt;/apply&gt;&lt;/math&gt;</typedComparisonValue>
                                    <comparisonType>EqualsStrict</comparisonType>
                                </stringComparisonValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="A-CI_SP_Ie7ea5301-245a-44c6-a961-a4864bd35c64_0" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="CI_SP_Ie7ea5301-245a-44c6-a961-a4864bd35c64_0" occur="1">
                                <stringValue>
                                    <typedValue>Distance[A, B] ≟ Distance[B, C]</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFactSet>
                            <keyFact id="A-CI_SP_I59fb10a4-be7d-4c15-b655-ccd57bfacfd6_0" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="CI_SP_I59fb10a4-be7d-4c15-b655-ccd57bfacfd6_0" occur="1">
                                    <decimalValue>
                                        <typedValue>1</typedValue>
                                    </decimalValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="A-CI_SP_I59fb10a4-be7d-4c15-b655-ccd57bfacfd6_1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="CI_SP_I59fb10a4-be7d-4c15-b655-ccd57bfacfd6_1" occur="1">
                                    <decimalValue>
                                        <typedValue>2</typedValue>
                                    </decimalValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="A-CI_SP_I59fb10a4-be7d-4c15-b655-ccd57bfacfd6_2" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="CI_SP_I59fb10a4-be7d-4c15-b655-ccd57bfacfd6_2" occur="1">
                                    <decimalValue>
                                        <typedValue>4</typedValue>
                                    </decimalValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="A-CI_SP_I59fb10a4-be7d-4c15-b655-ccd57bfacfd6_3" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="CI_SP_I59fb10a4-be7d-4c15-b655-ccd57bfacfd6_3" occur="1">
                                    <decimalValue>
                                        <typedValue>8</typedValue>
                                    </decimalValue>
                                </keyValue>
                            </keyFact>
                        </keyFactSet>
                        <keyFactSet>
                            <keyFact id="A-CI_SP_I59fb10a4-be7d-4c15-b655-ccd57bfacfd6_0" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="CI_SP_I59fb10a4-be7d-4c15-b655-ccd57bfacfd6_0" occur="1">
                                    <decimalValue>
                                        <typedValue>2</typedValue>
                                    </decimalValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="A-CI_SP_I59fb10a4-be7d-4c15-b655-ccd57bfacfd6_1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="CI_SP_I59fb10a4-be7d-4c15-b655-ccd57bfacfd6_1" occur="1">
                                    <decimalValue>
                                        <typedValue>4</typedValue>
                                    </decimalValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="A-CI_SP_I59fb10a4-be7d-4c15-b655-ccd57bfacfd6_2" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="CI_SP_I59fb10a4-be7d-4c15-b655-ccd57bfacfd6_2" occur="1">
                                    <decimalValue>
                                        <typedValue>8</typedValue>
                                    </decimalValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="A-CI_SP_I59fb10a4-be7d-4c15-b655-ccd57bfacfd6_3" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="CI_SP_I59fb10a4-be7d-4c15-b655-ccd57bfacfd6_3" occur="1">
                                    <decimalValue>
                                        <typedValue>16</typedValue>
                                    </decimalValue>
                                </keyValue>
                            </keyFact>
                        </keyFactSet>
                    </keyFinding>
                </keyFindings>
                <conceptFindings>
                    <conceptFinding id="mc" scoringMethod="None">
                        <conceptFact id="B-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="mc" occur="1">
                                <stringValue>
                                    <typedValue>B</typedValue>
                                </stringValue>
                            </conceptValue>
                            <concepts>
                                <concept value="2" code="A11"/>
                                <concept value="2" code="A1"/>
                                <concept value="2" code="A"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="A-CI_SP_I880ab1e3-b1ea-4e1f-a769-cf763f00969c_0" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="CI_SP_I880ab1e3-b1ea-4e1f-a769-cf763f00969c_0" occur="1">
                                <stringComparisonValue>
                                    <typedComparisonValue>&lt;math  xmlns="http://www.w3.org/1998/Math/MathML"&gt;&lt;apply&gt;&lt;root&gt;&lt;/root&gt;&lt;cn&gt;121&lt;/cn&gt;&lt;/apply&gt;&lt;/math&gt;</typedComparisonValue>
                                    <comparisonType>EqualsStrict</comparisonType>
                                </stringComparisonValue>
                            </conceptValue>
                        </conceptFact>
                        <conceptFact id="A-CI_SP_Ie7ea5301-245a-44c6-a961-a4864bd35c64_0" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="CI_SP_Ie7ea5301-245a-44c6-a961-a4864bd35c64_0" occur="1">
                                <stringValue>
                                    <typedValue>Distance[A, B] ≟ Distance[B, C]</typedValue>
                                </stringValue>
                            </conceptValue>
                            <concepts>
                                <concept value="3" code="A11"/>
                                <concept value="3" code="A1"/>
                                <concept value="3" code="A"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="A[1]-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="mc" occur="1">
                                <stringValue>
                                    <typedValue>A</typedValue>
                                </stringValue>
                            </conceptValue>
                            <concepts>
                                <concept value="1" code="A11"/>
                                <concept value="1" code="A1"/>
                                <concept value="1" code="A"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="C[1]-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="mc" occur="1">
                                <stringValue>
                                    <typedValue>C</typedValue>
                                </stringValue>
                            </conceptValue>
                            <concepts>
                                <concept value="0" code="A11"/>
                                <concept value="0" code="A1"/>
                                <concept value="0" code="A"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="D[1]-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="mc" occur="1">
                                <stringValue>
                                    <typedValue>D</typedValue>
                                </stringValue>
                            </conceptValue>
                            <concepts>
                                <concept value="0" code="A11"/>
                                <concept value="0" code="A1"/>
                                <concept value="0" code="A"/>
                            </concepts>
                        </conceptFact>
                        <conceptFact id="A[*]-CI_SP_Ie7ea5301-245a-44c6-a961-a4864bd35c64_0" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <conceptValue domain="CI_SP_Ie7ea5301-245a-44c6-a961-a4864bd35c64_0" occur="1">
                                <catchAllValue/>
                            </conceptValue>
                            <concepts>
                                <concept value="0" code="A11"/>
                                <concept value="0" code="A1"/>
                                <concept value="1" code="A"/>
                            </concepts>
                        </conceptFact>
                        <conceptFactSet>
                            <conceptFact id="A-CI_SP_I59fb10a4-be7d-4c15-b655-ccd57bfacfd6_0" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="CI_SP_I59fb10a4-be7d-4c15-b655-ccd57bfacfd6_0" occur="1">
                                    <decimalValue>
                                        <typedValue>1</typedValue>
                                    </decimalValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="A-CI_SP_I59fb10a4-be7d-4c15-b655-ccd57bfacfd6_1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="CI_SP_I59fb10a4-be7d-4c15-b655-ccd57bfacfd6_1" occur="1">
                                    <decimalValue>
                                        <typedValue>2</typedValue>
                                    </decimalValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="A-CI_SP_I59fb10a4-be7d-4c15-b655-ccd57bfacfd6_2" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="CI_SP_I59fb10a4-be7d-4c15-b655-ccd57bfacfd6_2" occur="1">
                                    <decimalValue>
                                        <typedValue>4</typedValue>
                                    </decimalValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="A-CI_SP_I59fb10a4-be7d-4c15-b655-ccd57bfacfd6_3" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="CI_SP_I59fb10a4-be7d-4c15-b655-ccd57bfacfd6_3" occur="1">
                                    <decimalValue>
                                        <typedValue>8</typedValue>
                                    </decimalValue>
                                </conceptValue>
                            </conceptFact>
                            <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                                <concept value="2" code="B1"/>
                                <concept value="2" code="B"/>
                            </concepts>
                        </conceptFactSet>
                        <conceptFactSet>
                            <conceptFact id="A-CI_SP_I59fb10a4-be7d-4c15-b655-ccd57bfacfd6_0" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="CI_SP_I59fb10a4-be7d-4c15-b655-ccd57bfacfd6_0" occur="1">
                                    <decimalValue>
                                        <typedValue>2</typedValue>
                                    </decimalValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="A-CI_SP_I59fb10a4-be7d-4c15-b655-ccd57bfacfd6_1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="CI_SP_I59fb10a4-be7d-4c15-b655-ccd57bfacfd6_1" occur="1">
                                    <decimalValue>
                                        <typedValue>4</typedValue>
                                    </decimalValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="A-CI_SP_I59fb10a4-be7d-4c15-b655-ccd57bfacfd6_2" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="CI_SP_I59fb10a4-be7d-4c15-b655-ccd57bfacfd6_2" occur="1">
                                    <decimalValue>
                                        <typedValue>8</typedValue>
                                    </decimalValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="A-CI_SP_I59fb10a4-be7d-4c15-b655-ccd57bfacfd6_3" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="CI_SP_I59fb10a4-be7d-4c15-b655-ccd57bfacfd6_3" occur="1">
                                    <decimalValue>
                                        <typedValue>16</typedValue>
                                    </decimalValue>
                                </conceptValue>
                            </conceptFact>
                            <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                                <concept value="2" code="B1"/>
                                <concept value="2" code="B"/>
                            </concepts>
                        </conceptFactSet>
                        <conceptFactSet>
                            <conceptFact id="A[*]-CI_SP_I59fb10a4-be7d-4c15-b655-ccd57bfacfd6_0" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="CI_SP_I59fb10a4-be7d-4c15-b655-ccd57bfacfd6_0" occur="1">
                                    <catchAllValue/>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="A[*]-CI_SP_I59fb10a4-be7d-4c15-b655-ccd57bfacfd6_1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="CI_SP_I59fb10a4-be7d-4c15-b655-ccd57bfacfd6_1" occur="1">
                                    <catchAllValue/>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="A[*]-CI_SP_I59fb10a4-be7d-4c15-b655-ccd57bfacfd6_2" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="CI_SP_I59fb10a4-be7d-4c15-b655-ccd57bfacfd6_2" occur="1">
                                    <catchAllValue/>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="A[*]-CI_SP_I59fb10a4-be7d-4c15-b655-ccd57bfacfd6_3" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="CI_SP_I59fb10a4-be7d-4c15-b655-ccd57bfacfd6_3" occur="1">
                                    <catchAllValue/>
                                </conceptValue>
                            </conceptFact>
                            <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                                <concept value="1" code="B1"/>
                                <concept value="1" code="B"/>
                            </concepts>
                        </conceptFactSet>
                    </conceptFinding>
                </conceptFindings>
                <aspectReferences/>
                <ItemScoreTranslationTable>
                    <ItemScoreTranslationTableEntry rawScore="0" translatedScore="0"/>
                    <ItemScoreTranslationTableEntry rawScore="1" translatedScore="1"/>
                </ItemScoreTranslationTable>
            </solution>

#End Region

    End Class

End Namespace