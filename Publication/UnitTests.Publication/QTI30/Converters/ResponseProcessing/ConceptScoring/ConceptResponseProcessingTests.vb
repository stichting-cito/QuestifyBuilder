
Imports System.Drawing
Imports System.Xml
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.HelperClasses
Imports Questify.Builder.UnitTests.Framework
Imports Questify.Builder.Logic.QTI.Converters.Processing.QTI30
Imports Questify.Builder.Logic.QTI.Converters.ScoringConverter.QTI30
Imports Questify.Builder.UnitTests.Publication.QTI30

Namespace QTI30

    <TestClass()>
    Public Class ConceptResponseProcessingTests

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessingConcept")>
        Public Sub ConceptResponseProcessing_TimeGaps_CombinationOfFactSetsAndFactsOnFinding_Test()
            RunConceptResponseProcessingTest(_itemBody1, _finding1, _responseProcessing1, GetInputScoringParams())
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessingConcept")>
        Public Sub ConceptResponseProcessing_GGM_FactSets_Test()
            RunConceptResponseProcessingTest(_itemBody2, _finding2, _responseProcessing2, GetGGMScoringParams())
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessingConcept")>
        Public Sub ConceptResponseProcessing_GGM_FactsOnFinding_Test()
            RunConceptResponseProcessingTest(_itemBody2, _finding7, _responseProcessing7, GetGGMScoringParams())
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessingConcept")>
        Public Sub ConceptResponseProcessing_GGM_CombinationOfFactSetsAndFactsOnFinding_Test()
            RunConceptResponseProcessingTest(_itemBody2, _finding8, _responseProcessing8, GetGGMScoringParams())
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessingConcept")>
        Public Sub ConceptResponseProcessing_MC_FactsOnFinding_Test()
            RunConceptResponseProcessingTest(_itemBody3, _finding3, _responseProcessing3, GetMcScoringParams())
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessingConcept")>
        Public Sub ConceptResponseProcessing_MR_FactsOnFinding_Test()
            RunConceptResponseProcessingTest(_itemBody4, _finding4, _responseProcessing4, GetMrScoringParams())
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessingConcept")>
        Public Sub ConceptResponseProcessing_MR_CombinationOfFactSetsAndFactsOnFinding_Test()
            RunConceptResponseProcessingTest(_itemBody4, _finding5, _responseProcessing5, GetMrScoringParams())
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessingConcept")>
        Public Sub ConceptResponseProcessing_MR_FactSets_Test()
            RunConceptResponseProcessingTest(_itemBody4, _finding6, _responseProcessing6, GetMrScoringParams())
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessingConcept")>
        Public Sub ConceptResponseProcessing_Matrix_FactsOnFinding_Test()
            RunConceptResponseProcessingTest(_itemBody5, _finding9, _responseProcessing9, GetMatrixScoringParams())
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessingConcept")>
        Public Sub ConceptResponseProcessing_Matrix_FactSets_Test()
            RunConceptResponseProcessingTest(_itemBody5, _finding10, _responseProcessing10, GetMatrixScoringParams())
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessingConcept")>
        Public Sub ConceptResponseProcessing_Matrix_CombinationOfFactSetsAndFactsOnFinding_Test()
            RunConceptResponseProcessingTest(_itemBody5, _finding11, _responseProcessing11, GetMatrixScoringParams())
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessingConcept")>
        Public Sub ConceptResponseProcessing_InlineGaps_String()
            Dim scoreParams As New HashSet(Of ScoringParameter)
            scoreParams.Add(New StringScoringParameter() With {.Label = "1", .ControllerId = "gapController", .FindingOverride = "gapController", .InlineId = "I9fbcb0a6-f900-4d33-95e7-3fa309b27134", .ExpectedLength = 15}.AddSubParameters("1"))
            scoreParams.Add(New StringScoringParameter() With {.Label = "2", .ControllerId = "gapController", .FindingOverride = "gapController", .InlineId = "Ibb2885b7-4af2-4cfe-857e-393f43bbecca", .ExpectedLength = 15}.AddSubParameters("1"))
            scoreParams.Add(New StringScoringParameter() With {.Label = "3", .ControllerId = "gapController", .FindingOverride = "gapController", .InlineId = "I7ee48b89-318e-4cdf-b3bf-277b745a6749", .ExpectedLength = 15}.AddSubParameters("1"))
            scoreParams.Add(New StringScoringParameter() With {.Label = "4", .ControllerId = "gapController", .FindingOverride = "gapController", .InlineId = "I5fde8174-6a14-4d92-b099-1559a6945c06", .ExpectedLength = 15}.AddSubParameters("1"))

            RunConceptResponseProcessingTest(_itemBody6, _finding12, _responseProcessing12, scoreParams)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessingConcept")>
        Public Sub ConceptResponseProcessing_InlineChoice_FactsOnFinding_Test()
            RunConceptResponseProcessingTest(_itemBody7, _finding13, _responseProcessing13, GetInlineChoiceScoringParams())
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessingConcept")>
        Public Sub ConceptResponseProcessing_InlineChoice_FactSets_Test()
            RunConceptResponseProcessingTest(_itemBody7, _finding14, _responseProcessing14, GetInlineChoiceScoringParams())
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessingConcept")>
        Public Sub ConceptResponseProcessing_InlineChoice_CombinationOfFactSetsAndFactsOnFinding_Test()
            RunConceptResponseProcessingTest(_itemBody7, _finding15, _responseProcessing15, GetInlineChoiceScoringParams())
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessingConcept")>
        Public Sub ConceptResponseProcessing_GapMatch_FactsOnFinding_Test()
            RunConceptResponseProcessingTest(_itemBody8, _finding16, _responseProcessing16, GetGapMatchScoringParams())
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessingConcept")>
        Public Sub ConceptResponseProcessing_GapMatch_FactSets_Test()
            RunConceptResponseProcessingTest(_itemBody8, _finding17, _responseProcessing17, GetGapMatchScoringParams())
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessingConcept")>
        Public Sub ConceptResponseProcessing_GapMatch_CombinationOfFactSetsAndFactsOnFinding_Test()
            RunConceptResponseProcessingTest(_itemBody8, _finding18, _responseProcessing18, GetGapMatchScoringParams())
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessingConcept")>
        Public Sub ConceptResponseProcessing_Order()
            Dim scoreParams As New HashSet(Of ScoringParameter)
            scoreParams.Add(New OrderScoringParameter() With {.ControllerId = "orderController", .FindingOverride = "orderController"}.AddSubParameters("A", "B", "C"))

            RunConceptResponseProcessingTest(_itemBody9, _finding19, _responseProcessing19, scoreParams)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessingConcept")>
        Public Sub ConceptResponseProcessing_Order_TwoFactSets()
            Dim scoreParams As New HashSet(Of ScoringParameter)
            scoreParams.Add(New OrderScoringParameter() With {.ControllerId = "orderController", .FindingOverride = "orderController"}.AddSubParameters("A", "B", "C"))

            RunConceptResponseProcessingTest(_itemBody9, _finding20, _responseProcessing20, scoreParams)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessingConcept")>
        Public Sub ConceptResponseProcessing_ConceptCodeWithSpecialCharactersShouldCreateRightResponseProcessing()
            Dim scoreParams As New HashSet(Of ScoringParameter)
            scoreParams.Add(New OrderScoringParameter() With {.ControllerId = "orderController", .FindingOverride = "orderController"}.AddSubParameters("A", "B", "C"))

            RunConceptResponseProcessingTest(_itemBody9, _finding21, _responseProcessing21, scoreParams)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessingConcept")>
        Public Sub ConceptResponseProcessing_GapMatch_FactSetsAndFactsOnFinding_Test()
            RunConceptResponseProcessingTest(_itemBody10, _finding22, _responseProcessing22, GetGapMatchScoringParameters())
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessingConcept")>
        Public Sub ConceptResponseProcessing_Hotspot_FactsOnFinding_Test()
            RunConceptResponseProcessingTest(_itemBody11, _finding23, _responseProcessing23, GetHotspotScoringParams())
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessingConcept")>
        Public Sub ConceptResponseProcessing_Hotspot_FactSets_Test()
            RunConceptResponseProcessingTest(_itemBody11, _finding24, _responseProcessing24, GetHotspotScoringParams())
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessingConcept")>
        Public Sub ConceptResponseProcessing_Hotspot_CombinationOfFactSetsAndFactsOnFinding_Test()
            RunConceptResponseProcessingTest(_itemBody11, _finding25, _responseProcessing25, GetHotspotScoringParams())
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessingConcept")>
        Public Sub ConceptResponseProcessing_Hotspot_ConceptsWithValuesOfZeroShouldNotBeInResponseProcessing_Test()
            RunConceptResponseProcessingTest(_itemBody11, _finding26, _responseProcessing26, GetHotspotScoringParams())
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessingConcept")>
        Public Sub ConceptResponseProcessing_Hottext_FactsOnFinding_Test()
            RunConceptResponseProcessingTest(_itemBody12, _finding30, _responseProcessing28, GetHottextScoringParams())
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessingConcept")>
        Public Sub ConceptResponseProcessing_Hottext_FactSets_Test()
            RunConceptResponseProcessingTest(_itemBody12, _finding31, _responseProcessing29, GetHottextScoringParams())
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessingConcept")>
        Public Sub ConceptResponseProcessing_Hottext_CombinationOfFactSetsAndFactsOnFinding_Test()
            RunConceptResponseProcessingTest(_itemBody12, _finding32, _responseProcessing30, GetHottextScoringParams())
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessingConcept")>
        Public Sub ConceptResponseProcessing_FactsOnFinding_NoConceptValues_Test()
            'Situation that could occur when a user first enters conceptvalues, but later decides to remove the checks from all concepts
            RunConceptResponseProcessingTest(_itemBody8, _finding27, _responseProcessing27, GetGapMatchScoringParams())
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessingConcept")>
        Public Sub ConceptResponseProcessing_FactSets_NoConceptValues_Test()
            'Situation that could occur when a user first enters conceptvalues, but later decides to remove the checks from all concepts
            RunConceptResponseProcessingTest(_itemBody8, _finding28, _responseProcessing27, GetGapMatchScoringParams())
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessingConcept")>
        Public Sub ConceptResponseProcessing_CombinationOfFactSetsAndFactsOnFinding_NoConceptValues_Test()
            'Situation that could occur when a user first enters conceptvalues, but later decides to remove the checks from all concepts
            RunConceptResponseProcessingTest(_itemBody8, _finding29, _responseProcessing27, GetGapMatchScoringParams())
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessingConcept")>
        Public Sub ConceptResponseProcessing_GGM_GapWithoutKey_ButWithConceptValue_Test()
            RunConceptResponseProcessingTest(_itemBody13, _finding34, _responseProcessing32, GetExtendedGGMScoringParams())
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessingConcept")>
        Public Sub ConceptResponseProcessing_Matrix_MultipleFactSets_FactsetsShouldBeProcessedInRightOrder_Test()
            RunConceptResponseProcessingTest(_itemBody5, _finding33, _responseProcessing31, GetMatrixScoringParams())
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessingConcept")>
        Public Sub ConceptResponseProcessing_HottextCorrection_FactsOnFinding_Test()
            RunConceptResponseProcessingTest(_itemBody14, _finding35, _responseProcessing33, GetHottextCorrectionScoringParams())
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessingConcept")>
        Public Sub ConceptResponseProcessing_HottextCorrection_FactSets_Test()
            RunConceptResponseProcessingTest(_itemBody14, _finding36, _responseProcessing34, GetHottextCorrectionScoringParams())
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessingConcept")>
        Public Sub ConceptResponseProcessing_HottextCorrection_CombinationOfFactSetsAndFactsOnFinding_Test()
            RunConceptResponseProcessingTest(_itemBody14, _finding37, _responseProcessing35, GetHottextCorrectionScoringParams())
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessingConcept")>
        Public Sub ConceptResponseProcessing_HottextCorrection_MultipleFactSets_Test()
            RunConceptResponseProcessingTest(_itemBody14, _finding38, _responseProcessing36, GetHottextCorrectionScoringParams())
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessingConcept")>
        Public Sub ConceptResponseProcessing_HottextCorrection_FactsOnFinding_ZeroValueScenario_I_Test()
            'Scenario where only the catchall fact has conceptvalues. 
            'In that case the if-statement for the alternatives should be added (with zero values)
            RunConceptResponseProcessingTest(_itemBody14, _finding39, _responseProcessing37, GetHottextCorrectionScoringParams())
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessingConcept")>
        Public Sub ConceptResponseProcessing_HottextCorrection_FactsOnFinding_ZeroValueScenario_II_Test()
            'Scenario where the catchall fact has conceptvalues, the first alternative has zero values, and the second alternative has conceptvalues. 
            'In that case the if-statement for the the alternatives should be added, even for the first alternative (with zero values)
            RunConceptResponseProcessingTest(_itemBody14, _finding40, _responseProcessing38, GetHottextCorrectionScoringParams())
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessingConcept")>
        Public Sub ConceptResponseProcessing_HottextCorrection_FactSets_ZeroValueScenario_I_Test()
            'Scenario where only the catchall fact has conceptvalues. 
            'In that case the if-statement for the alternatives should be added (with zero values)
            RunConceptResponseProcessingTest(_itemBody14, _finding41, _responseProcessing39, GetHottextCorrectionScoringParams())
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessingConcept")>
        Public Sub ConceptResponseProcessing_HottextCorrection_FactSets_ZeroValueScenario_II_Test()
            'Scenario where the catchall fact has conceptvalues, the first alternative has zero values, and the second alternative has conceptvalues. 
            'In that case the if-statement for the the alternatives should be added, even for the first alternative (with zero values)
            RunConceptResponseProcessingTest(_itemBody14, _finding42, _responseProcessing40, GetHottextCorrectionScoringParams())
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessingConcept")>
        Public Sub ConceptResponseProcessing_HottextCorrection_FactSets_ZeroValueScenario_III_Test()
            'Scenario where the catchall fact has no conceptvalues, the first alternative has zero values, and the second alternative has conceptvalues. 
            'In that case the if-statement for the the second alternative should be added, the other ones (first and catchall) should be skipped
            RunConceptResponseProcessingTest(_itemBody14, _finding43, _responseProcessing41, GetHottextCorrectionScoringParams())
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessingConcept")>
        Public Sub ConceptResponseProcessing_CustomInteractions_FactsOnFinding_Test()
            RunConceptResponseProcessingTest(_itemBody15, _finding44, _responseProcessing42, GetCustomInteractionScoringParams())
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessingConcept")>
        Public Sub ConceptResponseProcessing_CustomInteractions_FactSets_Test()
            RunConceptResponseProcessingTest(_itemBody15, _finding45, _responseProcessing43, GetCustomInteractionScoringParams())
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseProcessingConcept")>
        Public Sub ConceptResponseProcessing_CustomInteractions_CombinationOfFactSetsAndFactsOnFinding_Test()
            RunConceptResponseProcessingTest(_itemBody15, _finding46, _responseProcessing44, GetCustomInteractionScoringParams())
        End Sub

        Private Sub RunConceptResponseProcessingTest(itemBodyElement As XElement, findingElement As XElement, responseProcessingElement As XElement, scoreParams As HashSet(Of ScoringParameter), Optional scoringHelper As CombinedScoringConverter = Nothing)
            'Arrange
            Dim responseIdentifierAttributeList As XmlNodeList = PublicationTestHelper.GetResponseIdentifiers(itemBodyElement)
            Dim finding As ConceptFinding = findingElement.Deserialize(Of ConceptFinding)()
            Dim processor = New ConceptResponseProcessing(responseIdentifierAttributeList, finding, scoreParams, If(scoringHelper IsNot Nothing, scoringHelper, New CombinedScoringConverter))

            'Act
            Dim result = processor.GetProcessing(False).ToXmlDocument()

            'Assert
            Assert.IsTrue(UnitTestHelper.AreSame(responseProcessingElement, result))
        End Sub

#Region "Get scoring parameter"

        Private Function GetMrScoringParams() As HashSet(Of ScoringParameter)
            Dim scoreParams As New HashSet(Of ScoringParameter)
            Dim scoreParam As ScoringParameter = New MultiChoiceScoringParameter() With {.ControllerId = "mc", .FindingOverride = "mc", .MinChoices = 2, .MaxChoices = 2, .MultiChoice = MultiChoiceType.Check}.AddSubParameters("A", "B", "C", "D", "E")
            scoreParams.Add(scoreParam)
            Return scoreParams
        End Function

        Private Function GetMcScoringParams() As HashSet(Of ScoringParameter)
            Dim scoreParams As New HashSet(Of ScoringParameter)
            Dim scoreParam As ScoringParameter = New MultiChoiceScoringParameter() With {.ControllerId = "mc", .FindingOverride = "mc", .MinChoices = 1, .MaxChoices = 1, .MultiChoice = MultiChoiceType.Radio}.AddSubParameters("A", "B", "C", "D", "E")
            scoreParams.Add(scoreParam)
            Return scoreParams
        End Function

        Private Function GetGGMScoringParams() As HashSet(Of ScoringParameter)
            Dim scoreParams As New HashSet(Of ScoringParameter)
            Dim scoreParam As GraphGapMatchScoringParameter = New GraphGapMatchScoringParameter() With {.ControllerId = "gapMatchController", .FindingOverride = "gapMatchController", .IsCategorizationVariant = False}.AddSubParameters("A", "B", "C")
            scoreParam.Value(0).InnerParameters.Add(New GapImageParameter() With {.MatchMax = -1, .Id = "A", .Name = GapMatchScoringParameter.GapControlName})
            scoreParam.Value(1).InnerParameters.Add(New GapImageParameter() With {.MatchMax = -1, .Id = "B", .Name = GapMatchScoringParameter.GapControlName})
            scoreParam.Value(2).InnerParameters.Add(New GapImageParameter() With {.MatchMax = -1, .Id = "C", .Name = GapMatchScoringParameter.GapControlName})

            Dim area As New AreaParameter With {.Name = "itemQuestionArea"}
            scoreParam.Area = area
            scoreParam.Area.ShapeList.Add(New RectangleShape() With {.Identifier = "C", .BottomRight = New Point(694, 285), .TopLeft = New Point(474, 35)})
            scoreParam.Area.ShapeList.Add(New RectangleShape() With {.Identifier = "A", .BottomRight = New Point(191, 284), .TopLeft = New Point(0, 100)})
            scoreParam.Area.ShapeList.Add(New RectangleShape() With {.Identifier = "B", .BottomRight = New Point(472, 285), .TopLeft = New Point(192, 35)})

            Dim ggmScoringPrm As ScoringParameter = scoreParam.Transform
            scoreParams.Add(ggmScoringPrm)
            Return scoreParams
        End Function

        Private Function GetExtendedGGMScoringParams() As HashSet(Of ScoringParameter)
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

        Private Function GetInputScoringParams() As HashSet(Of ScoringParameter)
            Dim scoreParams As New HashSet(Of ScoringParameter)
            Dim scoreParam1 As ScoringParameter = New TimeScoringParameter() With {.Label = "tijd1", .ControllerId = "gapController", .FindingOverride = "gapController", .InlineId = "I0de6ade6-f82f-4111-b045-a3493f2d1ba6", .TimeFormat = "hh:mm"}.AddSubParameters("1")
            scoreParams.Add(scoreParam1)
            Dim scoreParam2 As ScoringParameter = New TimeScoringParameter() With {.Label = "tijd2", .ControllerId = "gapController", .FindingOverride = "gapController", .InlineId = "I899a54e6-ecf1-4875-83f9-8be5c4503614", .TimeFormat = "hh:mm"}.AddSubParameters("1")
            scoreParams.Add(scoreParam2)
            Dim scoreParam3 As ScoringParameter = New IntegerScoringParameter() With {.Label = "getal1", .ControllerId = "gapController", .FindingOverride = "gapController", .InlineId = "I52e3c4ab-3e4c-4b90-8dc7-90d05d4a9d22", .MaxLength = 5}.AddSubParameters("1")
            scoreParams.Add(scoreParam3)
            Return scoreParams
        End Function

        Private Function GetMatrixScoringParams() As HashSet(Of ScoringParameter)
            Dim scoreParams As New HashSet(Of ScoringParameter)
            Dim matrixColumnsDefinition As MultiChoiceScoringParameter = New MultiChoiceScoringParameter() With {.MinChoices = 1, .MaxChoices = 1, .MultiChoice = MultiChoiceType.Check}.AddSubParameters("A", "B")
            Dim scoreParam As ScoringParameter = New MatrixScoringParameter() With {.ControllerId = "matrix", .FindingOverride = "matrix"}.AddSubParameters("1", "2", "3", "4")
            DirectCast(scoreParam, MatrixScoringParameter).MatrixColumnsDefinition = matrixColumnsDefinition
            scoreParams.Add(scoreParam)
            Return scoreParams
        End Function

        Private Function GetInlineChoiceScoringParams() As HashSet(Of ScoringParameter)
            Dim scoreParams As New HashSet(Of ScoringParameter)
            Dim scoreParam1 As ScoringParameter = New InlineChoiceScoringParameter() With {.Label = "choice1", .ControllerId = "inlineChoiceController", .FindingOverride = "inlineChoiceController", .InlineId = "I13abebba-ebb7-4ed8-81d0-46e9e1b60a53", .MinChoices = 0, .MaxChoices = 1}.AddSubParameters("A", "B", "C", "D")
            scoreParams.Add(scoreParam1)
            Dim scoreParam2 As ScoringParameter = New InlineChoiceScoringParameter() With {.Label = "choice2", .ControllerId = "inlineChoiceController", .FindingOverride = "inlineChoiceController", .InlineId = "Ie830b508-32a0-42f4-920f-0e06f0f19313", .MinChoices = 0, .MaxChoices = 1}.AddSubParameters("A", "B", "C", "D", "E")
            scoreParams.Add(scoreParam2)
            Dim scoreParam3 As ScoringParameter = New InlineChoiceScoringParameter() With {.Label = "choice3", .ControllerId = "inlineChoiceController", .FindingOverride = "inlineChoiceController", .InlineId = "I22f81e91-aaa4-4528-999e-61b31b8123ec", .MinChoices = 0, .MaxChoices = 1}.AddSubParameters("A", "B")
            scoreParams.Add(scoreParam3)
            Return scoreParams
        End Function

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
                                                             <cito:plaintextparameter name="controlLabel">zeven</cito:plaintextparameter>
                                                             <cito:booleanparameter name="addHottextCorrection">False</cito:booleanparameter>
                                                             <cito:plaintextparameter name="hottextValue"/>
                                                         </cito:parameterSet>
                                                     </cito:parameters>
                                                 </cito:InlineElement><span id="SI0bf4350c-9288-4cec-9e37-1b0978b057ba" style="background-color: #C7B8CE;">zeven</span><cito:InlineElement id="Ifb1bfeea-20a0-4d29-bb87-d502ff355522" layoutTemplateSourceName="InlineHottextLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
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
            correctionScoreParamSubSet.Add("I43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea", "zeven")
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
                                                 </cito:InlineElement><span id="SIce5853d6-5a4b-4fc6-9298-7bc4047d0e26" style="background-color: #C7B8CE;">een</span> meren ..<cito:InlineElement id="I1752ec64-4652-4723-b3b0-0404b15a0e6d" layoutTemplateSourceName="InlineHottextCorrectionLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
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
                                                             <cito:plaintextparameter name="controlLabel">zeven</cito:plaintextparameter>
                                                             <cito:booleanparameter name="addHottextCorrection">True</cito:booleanparameter>
                                                             <cito:plaintextparameter name="hottextValue"/>
                                                             <cito:hotTextCorrectionScoringParameter name="hotTextCorrectionScoring" ControllerId="hottextCorrectionController" findingOverride="hottextController" expected-length="0" correctionIsApplicable="true">
                                                                 <cito:subparameterset id="Input"/>
                                                                 <cito:definition id=""/>
                                                                 <cito:relatedControlLabel name="controlLabel">zeven</cito:relatedControlLabel>
                                                             </cito:hotTextCorrectionScoringParameter>
                                                         </cito:parameterSet>
                                                     </cito:parameters>
                                                 </cito:InlineElement><span id="SI43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea" style="background-color: #C7B8CE;">zeven</span><cito:InlineElement id="I941e9a9a-fd22-43a4-acf2-aa862f943cfc" layoutTemplateSourceName="InlineHottextCorrectionLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
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

        Private Function GetGapMatchScoringParams() As HashSet(Of ScoringParameter)
            Dim scoreParams As New HashSet(Of ScoringParameter)
            Dim gapMatchScoringParameter = New GapMatchScoringParameter() With {.ControllerId = "gapMatchController", .FindingOverride = "gapMatchController"}.AddSubParameters("A", "B", "C", "D", "E", "F")

            Dim xhtmlValue As XElement = <xhtmlParameter name="gapMatchInlineInput">
                                             <p id="c1-id-11" xmlns="http://www.w3.org/1999/xhtml">Tekst met <cito:InlineElement id="I51faf178-ca03-41eb-8276-385ef2a185b3" layoutTemplateSourceName="InlineGapMatchLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                     <cito:parameters>
                                                         <cito:parameterSet id="entireItem">
                                                             <cito:plaintextparameter name="inlineGapMatchId">I51faf178-ca03-41eb-8276-385ef2a185b3</cito:plaintextparameter>
                                                             <cito:plaintextparameter name="inlineGapMatchLabel">gap 1</cito:plaintextparameter>
                                                             <cito:booleanparameter name="editSize">True</cito:booleanparameter>
                                                             <cito:integerparameter name="width"/>
                                                             <cito:integerparameter name="height"/>
                                                         </cito:parameterSet>
                                                     </cito:parameters>
                                                 </cito:InlineElement> een aantal <cito:InlineElement id="I989f6f3c-9d38-492f-80fe-4b71cfee574f" layoutTemplateSourceName="InlineGapMatchLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                     <cito:parameters>
                                                         <cito:parameterSet id="entireItem">
                                                             <cito:plaintextparameter name="inlineGapMatchId">I989f6f3c-9d38-492f-80fe-4b71cfee574f</cito:plaintextparameter>
                                                             <cito:plaintextparameter name="inlineGapMatchLabel">gap 2</cito:plaintextparameter>
                                                             <cito:booleanparameter name="editSize">True</cito:booleanparameter>
                                                             <cito:integerparameter name="width"/>
                                                             <cito:integerparameter name="height"/>
                                                         </cito:parameterSet>
                                                     </cito:parameters>
                                                 </cito:InlineElement> gaten waarin teksten <cito:InlineElement id="Ie1f57945-a74c-4f6c-948b-5133fb9778e8" layoutTemplateSourceName="InlineGapMatchLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                     <cito:parameters>
                                                         <cito:parameterSet id="entireItem">
                                                             <cito:plaintextparameter name="inlineGapMatchId">Ie1f57945-a74c-4f6c-948b-5133fb9778e8</cito:plaintextparameter>
                                                             <cito:plaintextparameter name="inlineGapMatchLabel">gap 3</cito:plaintextparameter>
                                                             <cito:booleanparameter name="editSize">True</cito:booleanparameter>
                                                             <cito:integerparameter name="width"/>
                                                             <cito:integerparameter name="height"/>
                                                         </cito:parameterSet>
                                                     </cito:parameters>
                                                 </cito:InlineElement> kunnen worden gesleept <cito:InlineElement id="I723529e7-8893-455e-b785-595592528040" layoutTemplateSourceName="InlineGapMatchLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                     <cito:parameters>
                                                         <cito:parameterSet id="entireItem">
                                                             <cito:plaintextparameter name="inlineGapMatchId">I723529e7-8893-455e-b785-595592528040</cito:plaintextparameter>
                                                             <cito:plaintextparameter name="inlineGapMatchLabel">gap 4</cito:plaintextparameter>
                                                             <cito:booleanparameter name="editSize">True</cito:booleanparameter>
                                                             <cito:integerparameter name="width"/>
                                                             <cito:integerparameter name="height"/>
                                                         </cito:parameterSet>
                                                     </cito:parameters>
                                                 </cito:InlineElement> door de kandidaat.</p>
                                         </xhtmlParameter>

            Dim xhtmlPrm As New XHtmlParameter() With {.Name = "gapMatchInlineInput", .Value = xhtmlValue.ToString}

            gapMatchScoringParameter.GapXhtmlParameter = xhtmlPrm
            Dim scoreParam As ScoringParameter = gapMatchScoringParameter.Transform()
            scoreParams.Add(scoreParam)

            Return scoreParams
        End Function

        Private Function GetGapMatchScoringParameters() As HashSet(Of ScoringParameter)
            Dim scoreParams As New HashSet(Of ScoringParameter)
            Dim gapMatchScoringParameter = New GapMatchScoringParameter() With {.ControllerId = "gapMatchController", .FindingOverride = "gapMatchController"}.AddSubParameters("A", "B", "C", "D")

            Dim xhtmlValue As XElement = <xhtmlParameter name="gapMatchInlineInput">
                                             <p id="c1-id-11" xmlns="http://www.w3.org/1999/xhtml">De <cito:InlineElement id="I97f0b37a-df8c-4f3d-868f-2b2d6490998d" layoutTemplateSourceName="InlineGapMatchLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                     <cito:parameters>
                                                         <cito:parameterSet id="entireItem">
                                                             <cito:plaintextparameter name="inlineGapMatchId">I97f0b37a-df8c-4f3d-868f-2b2d6490998d</cito:plaintextparameter>
                                                             <cito:plaintextparameter name="inlineGapMatchLabel">gat1</cito:plaintextparameter>
                                                             <cito:booleanparameter name="editSize">True</cito:booleanparameter>
                                                             <cito:integerparameter name="width"/>
                                                             <cito:integerparameter name="height"/>
                                                         </cito:parameterSet>
                                                     </cito:parameters>
                                                 </cito:InlineElement> valt niet ver van de <cito:InlineElement id="I62fae0a2-b76f-412f-9afd-5b75b79d9a79" layoutTemplateSourceName="InlineGapMatchLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                     <cito:parameters>
                                                         <cito:parameterSet id="entireItem">
                                                             <cito:plaintextparameter name="inlineGapMatchId">I62fae0a2-b76f-412f-9afd-5b75b79d9a79</cito:plaintextparameter>
                                                             <cito:plaintextparameter name="inlineGapMatchLabel">gat2</cito:plaintextparameter>
                                                             <cito:booleanparameter name="editSize">True</cito:booleanparameter>
                                                             <cito:integerparameter name="width"/>
                                                             <cito:integerparameter name="height"/>
                                                         </cito:parameterSet>
                                                     </cito:parameters>
                                                 </cito:InlineElement>.</p>
                                         </xhtmlParameter>

            Dim xhtmlPrm As New XHtmlParameter() With {.Name = "gapMatchInlineInput", .Value = xhtmlValue.ToString}

            gapMatchScoringParameter.GapXhtmlParameter = xhtmlPrm
            Dim scoreParam As ScoringParameter = gapMatchScoringParameter.Transform()
            scoreParams.Add(scoreParam)

            Return scoreParams
        End Function

#End Region

#Region "Itembody"

        ReadOnly _itemBody1 As XElement =
        <wrapper>
            <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
            <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
            <qti-item-body class="defaultBody">
                <div class="content">
                    <div>
                        <div id="questionwithinlinecontrol">
                            <p id="c1-id-11">tijd1: <span>
                                    <text-entry-interaction pattern-mask="^(([0-1]?[0-9])|([2][0-3]))$" response-identifier="I0de6ade6-f82f-4111-b045-a3493f2d1ba6" expected-length="2" time-sub-type="hhmm"/>
							  
								    :
								    
								    <text-entry-interaction pattern-mask="^([0-5]?[0-9])$" response-identifier="I0de6ade6-f82f-4111-b045-a3493f2d1ba6" expected-length="2" time-sub-type="hhmm"/>
                                </span> </p>
                            <p id="c1-id-12">getal1: <text-entry-interaction pattern-mask="^-?([0-9]{1,5})?$" response-identifier="I52e3c4ab-3e4c-4b90-8dc7-90d05d4a9d22" expected-length="6"/>
                            </p>
                            <p id="c1-id-13">tijd2: <span>
                                    <text-entry-interaction pattern-mask="^(([0-1]?[0-9])|([2][0-3]))$" response-identifier="I899a54e6-ecf1-4875-83f9-8be5c4503614" expected-length="2" time-sub-type="hhmm"/>
							  
								    :
								    
								    <text-entry-interaction pattern-mask="^([0-5]?[0-9])$" response-identifier="I899a54e6-ecf1-4875-83f9-8be5c4503614" expected-length="2" time-sub-type="hhmm"/>
                                </span>
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
            <stylesheet href="resource://package/itemstyle.css" type="text/css"/>
            <stylesheet href="resource://package/userstyle.css" type="text/css"/>
            <qti-item-body class="defaultBody">
                <div class="content">
                    <styles>
                        <stylecollection>
                            <style classname=".hotspot_opacity img" attributename="opacity" value="1"/>
                            <style classname=".hotspot_opacity img" attributename="filter" value="alpha(opacity=100)"/>
                        </stylecollection>
                    </styles>
                    <div>
                        <div id="hotSpotAnswer_Vertical">
                            <qti-graphic-gap-match-interaction response-identifier="gapMatchController">
                                <object type="image/png" data="resource://package/bigsmall.png" width="468" height="328"/>
                                <qti-gap-img identifier="A" match-max="1" class="">
                                    <object type="image/jpeg" data="resource://package/elephant.jpg" class="hotspot_opacity" width="200" height="145"/>
                                </qti-gap-img>
                                <qti-gap-img identifier="B" match-max="1" class="">
                                    <object type="image/png" data="resource://package/mier.png" class="hotspot_opacity" width="200" height="160"/>
                                </qti-gap-img>
                                <qti-associable-hotspot identifier="HSA" match-max="1" coords="-9,100,191,284" shape="rect"/>
                                <qti-associable-hotspot identifier="HSB" match-max="1" coords="192,35,472,285" shape="rect"/>
                                <qti-associable-hotspot identifier="HSC" match-max="1" coords="474,35,694,285" shape="rect"/>
                            </qti-graphic-gap-match-interaction>
                        </div>
                    </div>
                </div>
            </qti-item-body>
        </wrapper>

        ReadOnly _itemBody3 As XElement =
        <wrapper>
            <stylesheet href="resource://package/itemstyle.css" type="text/css"/>
            <stylesheet href="resource://package/userstyle.css" type="text/css"/>
            <qti-item-body class="defaultBody">
                <div class="content">
                    <div>
                        <div id="question">
                            <p id="c1-id-11">Vraag</p>
                        </div>
                        <div id="mc">
                            <qti-choice-interaction id="choiceInteraction1" class="" max-choices="1" shuffle="false" response-identifier="mc">
                                <qti-simple-choice identifier="A">
                                    <p id="c1-id-11">opt.A</p>
                                </qti-simple-choice>
                                <qti-simple-choice identifier="B">
                                    <p id="c1-id-11">opt.B</p>
                                </qti-simple-choice>
                                <qti-simple-choice identifier="C">
                                    <p id="c1-id-11">opt.C</p>
                                </qti-simple-choice>
                                <qti-simple-choice identifier="D">
                                    <p id="c1-id-11">opt.D</p>
                                </qti-simple-choice>
                                <qti-simple-choice identifier="E">
                                    <p id="c1-id-11">opt.E</p>
                                </qti-simple-choice>
                            </qti-choice-interaction>
                        </div>
                    </div>
                </div>
            </qti-item-body>
        </wrapper>

        ReadOnly _itemBody4 As XElement =
        <wrapper>
            <stylesheet href="resource://package/itemstyle.css" type="text/css"/>
            <stylesheet href="resource://package/userstyle.css" type="text/css"/>
            <qti-item-body class="defaultBody">
                <div class="content">
                    <div>
                        <div id="question">
                            <p id="c1-id-11">Vraag</p>
                        </div>
                        <div id="mc">
                            <qti-choice-interaction id="choiceInteraction1" class="" max-choices="3" shuffle="false" response-identifier="mc">
                                <qti-simple-choice identifier="A">
                                    <p id="c1-id-11">opt.A</p>
                                </qti-simple-choice>
                                <qti-simple-choice identifier="B">
                                    <p id="c1-id-11">opt.B</p>
                                </qti-simple-choice>
                                <qti-simple-choice identifier="C">
                                    <p id="c1-id-11">opt.C</p>
                                </qti-simple-choice>
                                <qti-simple-choice identifier="D">
                                    <p id="c1-id-11">opt.D</p>
                                </qti-simple-choice>
                                <qti-simple-choice identifier="E">
                                    <p id="c1-id-11">opt.E</p>
                                </qti-simple-choice>
                            </qti-choice-interaction>
                        </div>
                        <div id="itemgeneral">
                            <br/>
                            <p id="c1-id-11"> </p>
                        </div>
                    </div>
                </div>
            </qti-item-body>
        </wrapper>

        ReadOnly _itemBody5 As XElement =
        <wrapper>
            <stylesheet href="resource://package/itemstyle.css" type="text/css"/>
            <stylesheet href="resource://package/userstyle.css" type="text/css"/>
            <qti-item-body class="defaultBody">
                <div class="content">
                    <styles xmlns="http://www.w3.org/1999/xhtml">

                    </styles>
                    <div xmlns="http://www.w3.org/1999/xhtml">
                        <div id="question">
                            <p id="c1-id-11">Kloppen deze beweringen ?</p>
                        </div>
                        <div>
                            <qti-match-interaction id="matchInteraction1" class="" max-associations="4" shuffle="false" response-identifier="matrix">
                                <qti-simple-match-set>
                                    <qti-simple-associable-choice identifier="y_A" match-max="1">
                                        <div>
                                            <p id="c1-id-11">One = Eén</p>
                                        </div>
                                    </qti-simple-associable-choice>
                                    <qti-simple-associable-choice identifier="y_B" match-max="1">
                                        <div>
                                            <p id="c1-id-11">Twenty = Twee</p>
                                        </div>
                                    </qti-simple-associable-choice>
                                    <qti-simple-associable-choice identifier="y_C" match-max="1">
                                        <div>
                                            <p id="c1-id-11">Three = Drie</p>
                                        </div>
                                    </qti-simple-associable-choice>
                                    <qti-simple-associable-choice identifier="y_D" match-max="1">
                                        <div>
                                            <p id="c1-id-11">Four = Veertig</p>
                                        </div>
                                    </qti-simple-associable-choice>
                                </qti-simple-match-set>
                                <qti-simple-match-set>
                                    <qti-simple-associable-choice identifier="x_1" match-max="4">
                                        <div style="width: 194px;">
                                            <p id="c1-id-11">Ja</p>
                                        </div>
                                    </qti-simple-associable-choice>
                                    <qti-simple-associable-choice identifier="x_2" match-max="4">
                                        <div style="width: 194px;">
                                            <p id="c1-id-11">Nee</p>
                                        </div>
                                    </qti-simple-associable-choice>
                                </qti-simple-match-set>
                            </qti-match-interaction>
                        </div>
                    </div>
                </div>
            </qti-item-body>
        </wrapper>

        ReadOnly _itemBody6 As XElement =
        <wrapper>
            <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
            <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
            <qti-item-body class="defaultBody">
                <div class="content">
                    <div class="div_left">
                        <!-- div_left_inner is needed to create the 50px margin between the left and right columns in dual column mode -->
                        <div class="div_left_inner">

                        </div>
                    </div>
                    <div class="div_right">
                        <!-- div_right_inner is needed to create the 50px margin between the left and right columns in dual column mode -->
                        <div class="div_right_inner">
                            <div id="itemquestion">
                                <p id="c1-id-11">
                                    <strong id="c1-id-12">Vul de juiste kleuren in.</strong>
                                </p>
                            </div>
                            <div id="questionwithinlinecontrol">
                                <p id="c1-id-11" style="text-align:left;">Look at my <text-entry-interaction pattern-mask="^.{0,15}$" response-identifier="I9fbcb0a6-f900-4d33-95e7-3fa309b27134" expected-length="15"/> sweater. <img id="IMG_49a84a59-8691-49a1-aeb3-5dc016697c67" src="resource://package:1/bal%20groen.png" width="29" height="29" alt=""/>
                                    <br id="c1-id-12"/>And my lovely <text-entry-interaction pattern-mask="^.{0,15}$" response-identifier="Ibb2885b7-4af2-4cfe-857e-393f43bbecca" expected-length="15"/> skirt. <img id="IMG_e58be776-4220-4500-85b2-49f1a2eca1d4" src="resource://package:1/bal%20rood.jpg" width="30" height="30" alt=""/>
                                    <br id="c1-id-13"/>My big <text-entry-interaction pattern-mask="^.{0,15}$" response-identifier="I7ee48b89-318e-4cdf-b3bf-277b745a6749" expected-length="15"/> eyes. <img id="IMG_19b94873-ba4e-4aa6-864a-3baf3a5eef4a" src="resource://package:1/bal%20blauw.png" width="30" height="30" alt=""/> <br id="c1-id-14"/>My jacket is <text-entry-interaction pattern-mask="^.{0,15}$" response-identifier="I5fde8174-6a14-4d92-b099-1559a6945c06" expected-length="15"/>. <img id="IMG_b5fde02f-9aa5-410c-8382-5315ebd64433" src="resource://package:1/bal%20geel.png" width="29" height="29" alt=""/>
                                </p>
                                <p id="c1-id-15">How colourful  am I?<br id="c1-id-16"/>
                                </p>
                            </div>
                            <div id="answer">

                            </div>
                        </div>
                    </div>
                </div>
            </qti-item-body>
        </wrapper>

        ReadOnly _itemBody7 As XElement =
        <wrapper>
            <stylesheet href="resource://package/itemstyle.css" type="text/css"/>
            <stylesheet href="resource://package/userstyle.css" type="text/css"/>
            <qti-item-body class="defaultBody">
                <div class="content">
                    <styles xmlns="http://www.w3.org/1999/xhtml">
                        <stylecollection>

                        </stylecollection>
                    </styles>
                    <div xmlns="http://www.w3.org/1999/xhtml">
                        <div id="questionwithinlinecontrol">
                            <p id="c1-id-11">Drop down 1 <qti-inline-choice-interaction response-identifier="I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" shuffle="false" required="true">
                                    <qti-inline-choice identifier="A">alt.A</qti-inline-choice>
                                    <qti-inline-choice identifier="B">alt.B</qti-inline-choice>
                                    <qti-inline-choice identifier="C">alt.C</qti-inline-choice>
                                    <qti-inline-choice identifier="D">alt.D</qti-inline-choice>
                                </qti-inline-choice-interaction>
                            </p>
                            <p id="c1-id-12">Drop down 2 <qti-inline-choice-interaction response-identifier="Ie830b508-32a0-42f4-920f-0e06f0f19313" shuffle="false" required="true">
                                    <qti-inline-choice identifier="A">opt.1</qti-inline-choice>
                                    <qti-inline-choice identifier="B">opt.2</qti-inline-choice>
                                    <qti-inline-choice identifier="C">opt.3</qti-inline-choice>
                                    <qti-inline-choice identifier="D">opt.4</qti-inline-choice>
                                    <qti-inline-choice identifier="E">opt.5</qti-inline-choice>
                                </qti-inline-choice-interaction>
                            </p>
                            <p id="c1-id-13">Drop down 3 <qti-inline-choice-interaction response-identifier="I22f81e91-aaa4-4528-999e-61b31b8123ec" shuffle="false" required="true">
                                    <qti-inline-choice identifier="A">keuze 1</qti-inline-choice>
                                    <qti-inline-choice identifier="B">keuze 2</qti-inline-choice>
                                </qti-inline-choice-interaction>
                            </p>
                        </div>
                        <div id="answer">

                        </div>
                        <div id="itemgeneral">
                            <br/>
                            <p id="c1-id-11"> </p>
                        </div>
                    </div>
                </div>
            </qti-item-body>
        </wrapper>

        ReadOnly _itemBody8 As XElement =
        <wrapper>
            <stylesheet href="resource://package/itemstyle.css" type="text/css"/>
            <stylesheet href="resource://package/userstyle.css" type="text/css"/>
            <qti-item-body class="defaultBody">
                <div class="content">
                    <styles xmlns="http://www.w3.org/1999/xhtml">

                    </styles>
                    <div xmlns="http://www.w3.org/1999/xhtml">
                        <div>
                            <qti-gap-match-interaction response-identifier="gapMatchController" shuffle="false">
                                <qti-gap-text identifier="A" match-max="1">A</qti-gap-text>
                                <qti-gap-text identifier="B" match-max="1">B</qti-gap-text>
                                <qti-gap-text identifier="C" match-max="1">C</qti-gap-text>
                                <qti-gap-text identifier="D" match-max="1">D</qti-gap-text>
                                <qti-gap-text identifier="E" match-max="1">E</qti-gap-text>
                                <qti-gap-text identifier="F" match-max="1">F</qti-gap-text>
                                <p id="c1-id-11">Tekst met <span>
                                        <qti-gap identifier="I51faf178-ca03-41eb-8276-385ef2a185b3" required="true"/>
                                    </span> een aantal <span>
                                        <qti-gap identifier="I989f6f3c-9d38-492f-80fe-4b71cfee574f" required="true"/>
                                    </span> gaten waarin teksten <span>
                                        <qti-gap identifier="Ie1f57945-a74c-4f6c-948b-5133fb9778e8" required="true"/>
                                    </span> kunnen worden gesleept <span>
                                        <qti-gap identifier="I723529e7-8893-455e-b785-595592528040" required="true"/>
                                    </span> door de kandidaat.</p>
                            </qti-gap-match-interaction>
                        </div>
                    </div>
                </div>
            </qti-item-body>
        </wrapper>

        ReadOnly _itemBody9 As XElement =
        <wrapper>
            <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
            <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
            <qti-item-body class="defaultBody">
                <div class="content">
                    <div class="div_left">
                        <!-- div_left_inner is needed to create the 50px margin between the left and right columns in dual column mode -->
                        <div class="div_left_inner">

                        </div>
                    </div>
                    <div class="div_right">
                        <!-- div_right_inner is needed to create the 50px margin between the left and right columns in dual column mode -->
                        <div class="div_right_inner">
                            <div id="question">
                                <p id="c1-id-11">Vraag</p>
                            </div>
                            <div id="answer">
                                <orderInteraction response-identifier="orderController" shuffle="false" orientation="vertical" minChoices="1">
                                    <qti-simple-choice identifier="A">
                                        <p id="c1-id-11">aaa</p>
                                    </qti-simple-choice>
                                    <qti-simple-choice identifier="B">
                                        <p id="c1-id-11">bbb</p>
                                    </qti-simple-choice>
                                    <qti-simple-choice identifier="C">
                                        <p id="c1-id-11">ccc</p>
                                    </qti-simple-choice>
                                </orderInteraction>
                            </div>
                        </div>
                    </div>
                </div>
            </qti-item-body>
        </wrapper>

        ReadOnly _itemBody10 As XElement =
       <wrapper>
           <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
           <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
           <qti-item-body class="defaultBody">
               <div class="content">
                   <div>
                       <div>
                           <qti-gap-match-interaction response-identifier="gapMatchController" shuffle="false">
                               <qti-gap-text identifier="A" match-max="1">peer</qti-gap-text>
                               <qti-gap-text identifier="B" match-max="1">appel</qti-gap-text>
                               <qti-gap-text identifier="C" match-max="1">sloot</qti-gap-text>
                               <qti-gap-text identifier="D" match-max="1">boom</qti-gap-text>
                               <p id="c1-id-11">De <span>
                                       <qti-gap identifier="I97f0b37a-df8c-4f3d-868f-2b2d6490998d" required="true"/>
                                   </span> valt niet ver van de <span>
                                       <qti-gap identifier="I62fae0a2-b76f-412f-9afd-5b75b79d9a79" required="true"/>
                                   </span>.</p>
                           </qti-gap-match-interaction>
                       </div>
                   </div>
               </div>
           </qti-item-body>
       </wrapper>

        ReadOnly _itemBody11 As XElement =
       <wrapper>
           <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
           <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
           <qti-item-body class="defaultBody">
               <div class="content">
                   <div>
                       <div>
                           <qti-hotspot-interaction response-identifier="areaInteractionController" min-choices="1" max-choices="5">
                               <object type="image/jpeg" data="resource://package/UK.jpg" width="197" height="256"/>
                               <qti-hotspot-choice identifier="A" coords="42,42,35" shape="circle"/>
                               <qti-hotspot-choice identifier="B" coords="153,41,35" shape="circle"/>
                               <qti-hotspot-choice identifier="C" coords="96,122,35" shape="circle"/>
                               <qti-hotspot-choice identifier="D" coords="46,204,35" shape="circle"/>
                               <qti-hotspot-choice identifier="E" coords="154,206,35" shape="circle"/>
                           </qti-hotspot-interaction>
                       </div>
                   </div>
               </div>
           </qti-item-body>
       </wrapper>

        ReadOnly _itemBody12 As XElement =
       <wrapper>
           <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
           <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
           <qti-item-body class="defaultBody">
               <div class="content">
                   <div>
                       <div>
                           <qti-hottext-interaction response-identifier="hottextController" max-choices="25">
                               <p id="c1-id-11">
                                   <span>
                                       <qti-hottext identifier="Iba0772fe-878f-457c-b652-a0e922626082"/>
                                   </span>
                                   <span id="SIba0772fe-878f-457c-b652-a0e922626082" style="background-color: #C7B8CE;">Op</span>
                                   <span>
                                       <qti-hottext identifier="I5493e55f-fae9-499f-bd3e-16df52c028e1"/>
                                   </span>
                                   <span id="SI5493e55f-fae9-499f-bd3e-16df52c028e1" style="background-color: #C7B8CE;">vrijwel</span>
                                   <span>
                                       <qti-hottext identifier="I7134c778-4641-40b5-8eae-1a80013cf777"/>
                                   </span>
                                   <span id="SI7134c778-4641-40b5-8eae-1a80013cf777" style="background-color: #C7B8CE;">alle</span>
                                   <span>
                                       <qti-hottext identifier="I3cb9edb6-551e-474a-9122-338b8c6a396d"/>
                                   </span>
                                   <span id="SI3cb9edb6-551e-474a-9122-338b8c6a396d" style="background-color: #C7B8CE;">een</span>
                                   <span>
                                       <qti-hottext identifier="I41d5e633-d892-46fe-8a0c-ceba54c167f3"/>
                                   </span>
                                   <span id="SI41d5e633-d892-46fe-8a0c-ceba54c167f3" style="background-color: #C7B8CE;">meren</span>
                                   <span>
                                       <qti-hottext identifier="I498a96cd-114d-4299-b27c-639e41559e29"/>
                                   </span>
                                   <span id="SI498a96cd-114d-4299-b27c-639e41559e29" style="background-color: #C7B8CE;">is</span>
                                   <span>
                                       <qti-hottext identifier="I19b64b54-f7f4-4284-af37-37999bd24935"/>
                                   </span>
                                   <span id="SI19b64b54-f7f4-4284-af37-37999bd24935" style="background-color: #C7B8CE;">in</span>
                                   <span>
                                       <qti-hottext identifier="I7ea195a7-b0ec-465e-ada3-31c9e940f0dc"/>
                                   </span>
                                   <span id="SI7ea195a7-b0ec-465e-ada3-31c9e940f0dc" style="background-color: #C7B8CE;">de</span>
                                   <span>
                                       <qti-hottext identifier="Ibf683ce7-ac85-4610-b057-dd0578f3b71f"/>
                                   </span>
                                   <span id="SIbf683ce7-ac85-4610-b057-dd0578f3b71f" style="background-color: #C7B8CE;">zomer</span>
                                   <span>
                                       <qti-hottext identifier="Ie6f1fe1f-fe23-4f29-85ec-27d483138cfa"/>
                                   </span>
                                   <span id="SIe6f1fe1f-fe23-4f29-85ec-27d483138cfa" style="background-color: #C7B8CE;">veel</span>
                                   <span>
                                       <qti-hottext identifier="I2b4ed71d-8984-4ffd-a95a-126efbb12e48"/>
                                   </span>
                                   <span id="SI2b4ed71d-8984-4ffd-a95a-126efbb12e48" style="background-color: #C7B8CE;">te</span>
                                   <span>
                                       <qti-hottext identifier="I0778d3a8-11c3-41f5-aeb7-4efd94338296"/>
                                   </span>
                                   <span id="SI0778d3a8-11c3-41f5-aeb7-4efd94338296" style="background-color: #C7B8CE;">doen</span>: <span>
                                       <qti-hottext identifier="I3a1bf3a7-f9b4-472d-ac19-c6d27f671543"/>
                                   </span>
                                   <span id="SI3a1bf3a7-f9b4-472d-ac19-c6d27f671543" style="background-color: #C7B8CE;">ze</span>
                                   <span>
                                       <qti-hottext identifier="I01f17f48-6da9-44a0-bb7f-82e325f2f233"/>
                                   </span>
                                   <span id="SI01f17f48-6da9-44a0-bb7f-82e325f2f233" style="background-color: #C7B8CE;">zijn</span>
                                   <span>
                                       <qti-hottext identifier="Ifdbe8e9c-0eeb-4019-b8a7-5599265834fb"/>
                                   </span>
                                   <span id="SIfdbe8e9c-0eeb-4019-b8a7-5599265834fb" style="background-color: #C7B8CE;">twee</span>
                                   <span>
                                       <qti-hottext identifier="I0dd2d1e9-84b7-4b3f-a144-a2ec2c13061b"/>
                                   </span>
                                   <span id="SI0dd2d1e9-84b7-4b3f-a144-a2ec2c13061b" style="background-color: #C7B8CE;">geschikt</span>
                                   <span>
                                       <qti-hottext identifier="I41e6c74d-787a-4620-889f-fae84be71cdb"/>
                                   </span>
                                   <span id="SI41e6c74d-787a-4620-889f-fae84be71cdb" style="background-color: #C7B8CE;">voor</span>
                                   <span>
                                       <qti-hottext identifier="I1f639c54-57ee-428e-93ed-77d786d5a2d1"/>
                                   </span>
                                   <span id="SI1f639c54-57ee-428e-93ed-77d786d5a2d1" style="background-color: #C7B8CE;">drie</span>. <span>
                                       <qti-hottext identifier="Ib580dda7-0717-4cfc-b70e-34059b4c78f1"/>
                                   </span>
                                   <span id="SIb580dda7-0717-4cfc-b70e-34059b4c78f1" style="background-color: #C7B8CE;">Met</span>
                                   <span>
                                       <qti-hottext identifier="I834d38a6-dac5-4235-9cf4-d323dd89f1d2"/>
                                   </span>
                                   <span id="SI834d38a6-dac5-4235-9cf4-d323dd89f1d2" style="background-color: #C7B8CE;">de</span>
                                   <span>
                                       <qti-hottext identifier="Ic858ebcf-6f25-46ee-be0b-afbee1f41971"/>
                                   </span>
                                   <span id="SIc858ebcf-6f25-46ee-be0b-afbee1f41971" style="background-color: #C7B8CE;">bovenbouw</span>
                                   <span>
                                       <qti-hottext identifier="If6a1b756-b293-4baf-a3c0-3332221e3ea8"/>
                                   </span>
                                   <span id="SIf6a1b756-b293-4baf-a3c0-3332221e3ea8" style="background-color: #C7B8CE;">van</span>
                                   <span>
                                       <qti-hottext identifier="I80cb35ef-89d7-4ada-aef4-eda2b963a606"/>
                                   </span>
                                   <span id="SI80cb35ef-89d7-4ada-aef4-eda2b963a606" style="background-color: #C7B8CE;">de</span>
                                   <span>
                                       <qti-hottext identifier="Ia2c49323-f160-4dfa-8f79-5abea876efa6"/>
                                   </span>
                                   <span id="SIa2c49323-f160-4dfa-8f79-5abea876efa6" style="background-color: #C7B8CE;">vier</span>
                                   <span>
                                       <qti-hottext identifier="I8ecdc331-d565-4fcc-9f1f-a83e6e6bab9d"/>
                                   </span>
                                   <span id="SI8ecdc331-d565-4fcc-9f1f-a83e6e6bab9d" style="background-color: #C7B8CE;">gaan</span>
                                   <span>
                                       <qti-hottext identifier="I4142aba3-f108-41dc-b537-ab44fd8cb5ae"/>
                                   </span>
                                   <span id="SI4142aba3-f108-41dc-b537-ab44fd8cb5ae" style="background-color: #C7B8CE;">we</span>
                                   <span>
                                       <qti-hottext identifier="If9dc68d4-33ec-4176-adb2-eda10373f1a0"/>
                                   </span>
                                   <span id="SIf9dc68d4-33ec-4176-adb2-eda10373f1a0" style="background-color: #C7B8CE;">daarom</span>
                                   <span>
                                       <qti-hottext identifier="Ice8f6ba6-0b24-4234-8e1c-ea2d6caf18f6"/>
                                   </span>
                                   <span id="SIce8f6ba6-0b24-4234-8e1c-ea2d6caf18f6" style="background-color: #C7B8CE;">in</span>
                                   <span>
                                       <qti-hottext identifier="Ia64d8e75-aa61-481b-bd40-df7270053448"/>
                                   </span>
                                   <span id="SIa64d8e75-aa61-481b-bd40-df7270053448" style="background-color: #C7B8CE;">de</span>
                                   <span>
                                       <qti-hottext identifier="I2a787ac6-adce-419f-b230-293302ab6e2b"/>
                                   </span>
                                   <span id="SI2a787ac6-adce-419f-b230-293302ab6e2b" style="background-color: #C7B8CE;">vijf</span>
                                   <span>
                                       <qti-hottext identifier="I562dc41e-1fcd-45b2-ad63-e346a9b12ed3"/>
                                   </span>
                                   <span id="SI562dc41e-1fcd-45b2-ad63-e346a9b12ed3" style="background-color: #C7B8CE;">een</span>
                                   <span>
                                       <qti-hottext identifier="I2ff56de9-53fd-4be6-adc3-b9d48cbac0bd"/>
                                   </span>
                                   <span id="SI2ff56de9-53fd-4be6-adc3-b9d48cbac0bd" style="background-color: #C7B8CE;">weekje</span>
                                   <span>
                                       <qti-hottext identifier="I8c49dcaf-eea6-48ad-bcf0-5c00ccde0415"/>
                                   </span>
                                   <span id="SI8c49dcaf-eea6-48ad-bcf0-5c00ccde0415" style="background-color: #C7B8CE;">naar</span>
                                   <span>
                                       <qti-hottext identifier="I7b431d95-a30e-403f-9b35-7575b30d31a3"/>
                                   </span>
                                   <span id="SI7b431d95-a30e-403f-9b35-7575b30d31a3" style="background-color: #C7B8CE;">het</span>
                                   <span>
                                       <qti-hottext identifier="I46f0651d-a9f2-4b96-8172-8104b0938213"/>
                                   </span>
                                   <span id="SI46f0651d-a9f2-4b96-8172-8104b0938213" style="background-color: #C7B8CE;">zes</span>. <span>
                                       <qti-hottext identifier="I1e40e819-c8a2-42dd-89de-dd29e070b99b"/>
                                   </span>
                                   <span id="SI1e40e819-c8a2-42dd-89de-dd29e070b99b" style="background-color: #C7B8CE;">Waarschijnlijk</span>
                                   <span>
                                       <qti-hottext identifier="I67b6042e-7444-46be-b169-53ec421f9eda"/>
                                   </span>
                                   <span id="SI67b6042e-7444-46be-b169-53ec421f9eda" style="background-color: #C7B8CE;">gaan</span>
                                   <span>
                                       <qti-hottext identifier="I18248834-8320-4342-967c-2007195ba14a"/>
                                   </span>
                                   <span id="SI18248834-8320-4342-967c-2007195ba14a" style="background-color: #C7B8CE;">de</span>
                                   <span>
                                       <qti-hottext identifier="I0bf4350c-9288-4cec-9e37-1b0978b057ba"/>
                                   </span>
                                   <span id="SI0bf4350c-9288-4cec-9e37-1b0978b057ba" style="background-color: #C7B8CE;">zeven</span>
                                   <span>
                                       <qti-hottext identifier="Ifb1bfeea-20a0-4d29-bb87-d502ff355522"/>
                                   </span>
                                   <span id="SIfb1bfeea-20a0-4d29-bb87-d502ff355522" style="background-color: #C7B8CE;">acht</span>
                                   <span>
                                       <qti-hottext identifier="Ie1f531cd-79c9-4b87-a90c-76766792b183"/>
                                   </span>
                                   <span id="SIe1f531cd-79c9-4b87-a90c-76766792b183" style="background-color: #C7B8CE;">en</span>
                                   <span>
                                       <qti-hottext identifier="I66faad9a-d004-4cdc-a275-6f9ff736bb14"/>
                                   </span>
                                   <span id="SI66faad9a-d004-4cdc-a275-6f9ff736bb14" style="background-color: #C7B8CE;">negen</span>
                                   <span>
                                       <qti-hottext identifier="I665f2702-e926-4266-bc3c-3f04ea9a6c62"/>
                                   </span>
                                   <span id="SI665f2702-e926-4266-bc3c-3f04ea9a6c62" style="background-color: #C7B8CE;">tien</span> - <span>
                                       <qti-hottext identifier="I7e8b004a-0223-432a-bb9a-536918190017"/>
                                   </span>
                                   <span id="SI7e8b004a-0223-432a-bb9a-536918190017" style="background-color: #C7B8CE;">van</span>
                                   <span>
                                       <qti-hottext identifier="I217873b0-4970-405f-a80b-b2a9f42bbd85"/>
                                   </span>
                                   <span id="SI217873b0-4970-405f-a80b-b2a9f42bbd85" style="background-color: #C7B8CE;">der</span>
                                   <span>
                                       <qti-hottext identifier="I46521597-f1f4-432c-96c2-8e3defb05950"/>
                                   </span>
                                   <span id="SI46521597-f1f4-432c-96c2-8e3defb05950" style="background-color: #C7B8CE;">wielen</span>
                                   <span>
                                       <qti-hottext identifier="Ifbea86b5-20bc-4dde-a4e4-4a14153c8ead"/>
                                   </span>
                                   <span id="SIfbea86b5-20bc-4dde-a4e4-4a14153c8ead" style="background-color: #C7B8CE;">mee</span>
                                   <span>
                                       <qti-hottext identifier="I279d3c47-95a8-42d1-9440-26fadf909090"/>
                                   </span>
                                   <span id="SI279d3c47-95a8-42d1-9440-26fadf909090" style="background-color: #C7B8CE;">als</span>
                                   <span>
                                       <qti-hottext identifier="I6d80a45f-5042-4f42-99fe-151229268a43"/>
                                   </span>
                                   <span id="SI6d80a45f-5042-4f42-99fe-151229268a43" style="background-color: #C7B8CE;">begeleider</span>.</p>
                           </qti-hottext-interaction>
                       </div>
                   </div>
               </div>
           </qti-item-body>
       </wrapper>

        ReadOnly _itemBody13 As XElement =
       <wrapper>
           <stylesheet href="resource://package/itemstyle.css" type="text/css"/>
           <stylesheet href="resource://package/userstyle.css" type="text/css"/>
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
                                   <strong id="c1-id-12">Zet ....</strong>
                               </p>
                           </div>
                           <div id="body">
                               <br/>
                               <p id="c1-id-11">Maak ...</p>
                           </div>
                           <div id="hotSpotAnswer_Vertical">
                               <qti-graphic-gap-match-interaction response-identifier="gapMatchController">
                                   <object type="image/jpeg" data="resource://package/blokkenpatroon.jpg" width="127" height="126"/>
                                   <qti-gap-img identifier="A" match-max="9" class="">
                                       <object type="image/jpeg" data="resource://package/afbeelding_blokje-wit.jpg" class="hotspot_opacity" width="27" height="26"/>
                                   </qti-gap-img>
                                   <qti-gap-img identifier="B" match-max="9" class="">
                                       <object type="image/jpeg" data="resource://package/afbeelding_blokje-grijs.jpg" class="hotspot_opacity" width="26" height="26"/>
                                   </qti-gap-img>
                                   <qti-associable-hotspot identifier="HSH" match-max="1" coords="101,75,126,100" shape="rect"/>
                                   <qti-associable-hotspot identifier="HSI" match-max="1" coords="101,100,126,125" shape="rect"/>
                                   <qti-associable-hotspot identifier="HSG" match-max="1" coords="101,50,126,75" shape="rect"/>
                                   <qti-associable-hotspot identifier="HSF" match-max="1" coords="101,25,126,50" shape="rect"/>
                                   <qti-associable-hotspot identifier="HSA" match-max="1" coords="0,0,25,25" shape="rect"/>
                                   <qti-associable-hotspot identifier="HSB" match-max="1" coords="25,0,51,25" shape="rect"/>
                                   <qti-associable-hotspot identifier="HSC" match-max="1" coords="51,0,76,25" shape="rect"/>
                                   <qti-associable-hotspot identifier="HSD" match-max="1" coords="76,0,101,25" shape="rect"/>
                                   <qti-associable-hotspot identifier="HSE" match-max="1" coords="101,0,126,25" shape="rect"/>
                               </qti-graphic-gap-match-interaction>
                           </div>
                       </div>
                   </div>
               </div>
           </qti-item-body>
       </wrapper>

        Private _itemBody14 As XElement =
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
                                            <span id="SI43f7bf2d-73d6-4bae-a5da-8e1a51cc97ea" style="background-color: #C7B8CE;">zeven</span>
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

        Private _itemBody15 As XElement =
        <wrapper>
            <stylesheet href="resource://package/itemstyle.css" type="text/css"/>
            <stylesheet href="resource://package/userstyle.css" type="text/css"/>
            <qti-item-body class="defaultBody">
                <div class="content">
                    <qti-custom-interaction response-identifier="CustomInteractions">
                        <qti-prompt>Hieronder zou de Custom Interaction moeten staan...</qti-prompt>
                        <object xmlns:html="http://www.w3.org/1999/xhtml" type="application/javascript" data="../ref/json/test.manifest.json" height="680" width="680">
                            <param name="responseLength" value="4" valuetype="DATA"/>
                        </object>
                    </qti-custom-interaction>
                </div>
            </qti-item-body>
        </wrapper>



#End Region

#Region "Finding"

        ReadOnly _finding1 As XElement =
        <conceptFinding id="gapController" scoringMethod="None">
            <conceptFact id="1[*]-I899a54e6-ecf1-4875-83f9-8be5c4503614" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I899a54e6-ecf1-4875-83f9-8be5c4503614" occur="1">
                    <catchAllValue/>
                </conceptValue>
                <concepts>
                    <concept value="1" code="Concept2-1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="1-I899a54e6-ecf1-4875-83f9-8be5c4503614" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I899a54e6-ecf1-4875-83f9-8be5c4503614" occur="1">
                    <stringValue>
                        <typedValue>12:00</typedValue>
                    </stringValue>
                </conceptValue>
                <concepts>
                    <concept value="3" code="Concept2-1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="1[1]-I899a54e6-ecf1-4875-83f9-8be5c4503614" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I899a54e6-ecf1-4875-83f9-8be5c4503614" occur="1">
                    <stringValue>
                        <typedValue>00:00</typedValue>
                    </stringValue>
                </conceptValue>
                <concepts>
                    <concept value="2" code="Concept2-1"/>
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
                    <concept value="2" code="Concept2-1"/>
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
                    <concept value="3" code="Concept2-1"/>
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
                    <concept value="1" code="Concept2-1"/>
                </concepts>
            </conceptFactSet>
        </conceptFinding>

        ReadOnly _finding2 As XElement =
        <conceptFinding id="gapMatchController" scoringMethod="None">
            <conceptFactSet>
                <conceptFact id="A-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="A-gapMatchController" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="B-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="B-gapMatchController" occur="1">
                        <stringValue>
                            <typedValue>A</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="3" code="Concept2-1"/>
                </concepts>
            </conceptFactSet>
            <conceptFactSet>
                <conceptFact id="A[*]-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="A[*]-gapMatchController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="B[*]-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="B[*]-gapMatchController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="1" code="Concept2-1"/>
                </concepts>
            </conceptFactSet>
            <conceptFactSet>
                <conceptFact id="A[1]-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="A[1]-gapMatchController" occur="1">
                        <stringValue>
                            <typedValue>A</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="B[1]-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="B[1]-gapMatchController" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="2" code="Concept2-1"/>
                </concepts>
            </conceptFactSet>
        </conceptFinding>

        ReadOnly _finding3 As XElement =
        <conceptFinding id="mc" scoringMethod="None">
            <conceptFact id="A-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="mc" occur="1">
                    <stringValue>
                        <typedValue>A</typedValue>
                    </stringValue>
                </conceptValue>
                <concepts>
                    <concept value="0" code="Concept1-2"/>
                    <concept value="0" code="Concept1-1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="B[1]-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="mc" occur="1">
                    <stringValue>
                        <typedValue>B</typedValue>
                    </stringValue>
                </conceptValue>
                <concepts>
                    <concept value="0" code="Concept1-2"/>
                    <concept value="1" code="Concept1-1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="C[1]-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="mc" occur="1">
                    <stringValue>
                        <typedValue>C</typedValue>
                    </stringValue>
                </conceptValue>
                <concepts>
                    <concept value="0" code="Concept1-2"/>
                    <concept value="1" code="Concept1-1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="D[1]-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="mc" occur="1">
                    <stringValue>
                        <typedValue>D</typedValue>
                    </stringValue>
                </conceptValue>
                <concepts>
                    <concept value="1" code="Concept1-2"/>
                    <concept value="0" code="Concept1-1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="E[1]-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="mc" occur="1">
                    <stringValue>
                        <typedValue>E</typedValue>
                    </stringValue>
                </conceptValue>
                <concepts>
                    <concept value="0" code="Concept1-2"/>
                    <concept value="1" code="Concept1-1"/>
                </concepts>
            </conceptFact>
        </conceptFinding>

        ReadOnly _finding4 As XElement =
        <conceptFinding id="mc" scoringMethod="None">
            <conceptFact id="A-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="A-mc" occur="1">
                    <booleanValue>
                        <typedValue>true</typedValue>
                    </booleanValue>
                </conceptValue>
                <concepts>
                    <concept value="2" code="Concept1-2"/>
                    <concept value="2" code="Concept1-1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="B-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="B-mc" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
                <concepts>
                    <concept value="2" code="Concept1-2"/>
                    <concept value="2" code="Concept1-1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="C-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="C-mc" occur="1">
                    <booleanValue>
                        <typedValue>true</typedValue>
                    </booleanValue>
                </conceptValue>
                <concepts>
                    <concept value="2" code="Concept1-2"/>
                    <concept value="2" code="Concept1-1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="D-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="D-mc" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
                <concepts>
                    <concept value="2" code="Concept1-2"/>
                    <concept value="2" code="Concept1-1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="E-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="E-mc" occur="1">
                    <booleanValue>
                        <typedValue>true</typedValue>
                    </booleanValue>
                </conceptValue>
                <concepts>
                    <concept value="2" code="Concept1-2"/>
                    <concept value="2" code="Concept1-1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="A[1]-mc" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="A[1]-mc" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
                <concepts>
                    <concept value="1" code="Concept1-2"/>
                    <concept value="1" code="Concept1-1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="C[1]-mc" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="C[1]-mc" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
                <concepts>
                    <concept value="1" code="Concept1-2"/>
                    <concept value="1" code="Concept1-1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="B[1]-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="B[1]-mc" occur="1">
                    <booleanValue>
                        <typedValue>true</typedValue>
                    </booleanValue>
                </conceptValue>
                <concepts>
                    <concept value="1" code="Concept1-2"/>
                    <concept value="1" code="Concept1-1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="D[1]-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="D[1]-mc" occur="1">
                    <booleanValue>
                        <typedValue>true</typedValue>
                    </booleanValue>
                </conceptValue>
                <concepts>
                    <concept value="1" code="Concept1-2"/>
                    <concept value="1" code="Concept1-1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="E[1]-mc" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="E[1]-mc" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
                <concepts>
                    <concept value="1" code="Concept1-2"/>
                    <concept value="1" code="Concept1-1"/>
                </concepts>
            </conceptFact>
        </conceptFinding>

        ReadOnly _finding5 As XElement =
        <conceptFinding id="mc" scoringMethod="None">
            <conceptFact id="A-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="A-mc" occur="1">
                    <booleanValue>
                        <typedValue>true</typedValue>
                    </booleanValue>
                </conceptValue>
                <concepts>
                    <concept value="2" code="Concept1-2"/>
                    <concept value="2" code="Concept1-1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="B-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="B-mc" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
                <concepts>
                    <concept value="2" code="Concept1-2"/>
                    <concept value="2" code="Concept1-1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="A[1]-mc" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="A[1]-mc" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
                <concepts>
                    <concept value="1" code="Concept1-2"/>
                    <concept value="1" code="Concept1-1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="B[1]-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="B[1]-mc" occur="1">
                    <booleanValue>
                        <typedValue>true</typedValue>
                    </booleanValue>
                </conceptValue>
                <concepts>
                    <concept value="1" code="Concept1-2"/>
                    <concept value="1" code="Concept1-1"/>
                </concepts>
            </conceptFact>
            <conceptFactSet>
                <conceptFact id="C-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="C-mc" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="D-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="D-mc" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="E-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="E-mc" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="2" code="Concept1-2"/>
                    <concept value="2" code="Concept1-1"/>
                </concepts>
            </conceptFactSet>
            <conceptFactSet>
                <conceptFact id="C-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="C-mc" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="D-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="D-mc" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="E-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="E-mc" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="2" code="Concept1-2"/>
                    <concept value="2" code="Concept1-1"/>
                </concepts>
            </conceptFactSet>
            <conceptFactSet>
                <conceptFact id="C[*]-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="C[*]-mc" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="D[*]-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="D[*]-mc" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="E[*]-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="E[*]-mc" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="0" code="Concept1-2"/>
                    <concept value="0" code="Concept1-1"/>
                </concepts>
            </conceptFactSet>
            <conceptFactSet>
                <conceptFact id="C[2]-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="C[2]-mc" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="D[2]-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="D[2]-mc" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="E[2]-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="E[2]-mc" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="1" code="Concept1-2"/>
                    <concept value="1" code="Concept1-1"/>
                </concepts>
            </conceptFactSet>
        </conceptFinding>

        ReadOnly _finding6 As XElement =
        <conceptFinding id="mc" scoringMethod="None">
            <conceptFactSet>
                <conceptFact id="C-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="C-mc" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="D-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="D-mc" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="E-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="E-mc" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="B-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="B-mc" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="A-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="A-mc" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="2" code="Concept1-2"/>
                    <concept value="2" code="Concept1-1"/>
                </concepts>
            </conceptFactSet>
            <conceptFactSet>
                <conceptFact id="A-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="A-mc" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="B-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="B-mc" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="C-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="C-mc" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="D-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="D-mc" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="E-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="E-mc" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="2" code="Concept1-2"/>
                    <concept value="2" code="Concept1-1"/>
                </concepts>
            </conceptFactSet>
            <conceptFactSet>
                <conceptFact id="A[2]-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="A[2]-mc" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="B[2]-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="B[2]-mc" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="C[2]-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="C[2]-mc" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="D[2]-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="D[2]-mc" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="E[2]-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="E[2]-mc" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="1" code="Concept1-2"/>
                    <concept value="1" code="Concept1-1"/>
                </concepts>
            </conceptFactSet>
        </conceptFinding>

        ReadOnly _finding7 As XElement =
        <conceptFinding id="gapMatchController" scoringMethod="None">
            <conceptFact id="A-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="A-gapMatchController" occur="1">
                    <stringValue>
                        <typedValue>B</typedValue>
                    </stringValue>
                </conceptValue>
                <concepts>
                    <concept value="2" code="Concept1.2"/>
                    <concept value="2" code="Concept1.1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="B-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="B-gapMatchController" occur="1">
                    <stringValue>
                        <typedValue>A</typedValue>
                    </stringValue>
                </conceptValue>
                <concepts>
                    <concept value="2" code="Concept1.2"/>
                    <concept value="2" code="Concept1.1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="A[*]-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="A[*]-gapMatchController" occur="1">
                    <catchAllValue/>
                </conceptValue>
                <concepts>
                    <concept value="0" code="Concept1.2"/>
                    <concept value="0" code="Concept1.1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="A[1]-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="A[1]-gapMatchController" occur="1">
                    <stringValue>
                        <typedValue>A</typedValue>
                    </stringValue>
                </conceptValue>
                <concepts>
                    <concept value="1" code="Concept1.2"/>
                    <concept value="1" code="Concept1.1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="B[*]-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="B[*]-gapMatchController" occur="1">
                    <catchAllValue/>
                </conceptValue>
                <concepts>
                    <concept value="1" code="Concept1.2"/>
                    <concept value="1" code="Concept1.1"/>
                </concepts>
            </conceptFact>
        </conceptFinding>

        ReadOnly _finding8 As XElement =
        <conceptFinding id="gapMatchController" scoringMethod="None">
            <conceptFact id="A-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="A-gapMatchController" occur="1">
                    <stringValue>
                        <typedValue>B</typedValue>
                    </stringValue>
                </conceptValue>
                <concepts>
                    <concept value="2" code="Concept1.2"/>
                    <concept value="2" code="Concept1.1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="A[*]-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="A[*]-gapMatchController" occur="1">
                    <catchAllValue/>
                </conceptValue>
                <concepts>
                    <concept value="0" code="Concept1.2"/>
                    <concept value="0" code="Concept1.1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="A[1]-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="A[1]-gapMatchController" occur="1">
                    <stringValue>
                        <typedValue>A</typedValue>
                    </stringValue>
                </conceptValue>
                <concepts>
                    <concept value="1" code="Concept1.2"/>
                    <concept value="1" code="Concept1.1"/>
                </concepts>
            </conceptFact>
            <conceptFactSet>
                <conceptFact id="C-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="C-gapMatchController" occur="1">
                        <stringValue>
                            <typedValue>A</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="B-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="B-gapMatchController" occur="1">
                        <stringValue>
                            <typedValue>A</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="3" code="Concept1.2"/>
                    <concept value="3" code="Concept1.1"/>
                </concepts>
            </conceptFactSet>
            <conceptFactSet>
                <conceptFact id="B-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="B-gapMatchController" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="C-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="C-gapMatchController" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="2" code="Concept1.2"/>
                    <concept value="2" code="Concept1.1"/>
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
                    <concept value="1" code="Concept1.2"/>
                    <concept value="1" code="Concept1.1"/>
                </concepts>
            </conceptFactSet>
        </conceptFinding>

        ReadOnly _finding9 As XElement =
        <conceptFinding id="matrix" scoringMethod="None">
            <conceptFact id="1[*]-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="matrix1" occur="1">
                    <catchAllValue/>
                </conceptValue>
                <concepts>
                    <concept value="0" code="Concept1.2"/>
                    <concept value="0" code="Concept1.1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="1-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="matrix1" occur="1">
                    <stringValue>
                        <typedValue>A</typedValue>
                    </stringValue>
                </conceptValue>
                <concepts>
                    <concept value="1" code="Concept1.2"/>
                    <concept value="1" code="Concept1.1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="2-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="matrix2" occur="1">
                    <stringValue>
                        <typedValue>B</typedValue>
                    </stringValue>
                </conceptValue>
                <concepts>
                    <concept value="2" code="Concept1.2"/>
                    <concept value="3" code="Concept1.1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="3-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="matrix3" occur="1">
                    <stringValue>
                        <typedValue>A</typedValue>
                    </stringValue>
                </conceptValue>
                <concepts>
                    <concept value="3" code="Concept1.1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="4-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="matrix4" occur="1">
                    <stringValue>
                        <typedValue>B</typedValue>
                    </stringValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="2[*]-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="matrix2" occur="1">
                    <catchAllValue/>
                </conceptValue>
                <concepts>
                    <concept value="0" code="Concept1.2"/>
                    <concept value="1" code="Concept1.1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="2[1]-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="matrix2" occur="1">
                    <stringValue>
                        <typedValue>A</typedValue>
                    </stringValue>
                </conceptValue>
                <concepts>
                    <concept value="1" code="Concept1.2"/>
                    <concept value="2" code="Concept1.1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="3[*]-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="matrix3" occur="1">
                    <catchAllValue/>
                </conceptValue>
                <concepts>
                    <concept value="1" code="Concept1.1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="3[1]-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="matrix3" occur="1">
                    <stringValue>
                        <typedValue>B</typedValue>
                    </stringValue>
                </conceptValue>
                <concepts>
                    <concept value="2" code="Concept1.1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="4[*]-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="matrix4" occur="1">
                    <catchAllValue/>
                </conceptValue>
            </conceptFact>
        </conceptFinding>

        ReadOnly _finding10 As XElement =
        <conceptFinding id="matrix" scoringMethod="None">
            <conceptFactSet>
                <conceptFact id="1-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="matrix1" occur="1">
                        <stringValue>
                            <typedValue>A</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="2-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="matrix2" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="3-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="matrix3" occur="1">
                        <stringValue>
                            <typedValue>A</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="4-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="matrix4" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="3" code="Concept1.2"/>
                    <concept value="3" code="Concept1.1"/>
                </concepts>
            </conceptFactSet>
            <conceptFactSet>
                <conceptFact id="1[*]-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="matrix1" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="2[*]-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="matrix2" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="3[*]-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="matrix3" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="4[*]-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="matrix4" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="0" code="Concept1.2"/>
                    <concept value="0" code="Concept1.1"/>
                </concepts>
            </conceptFactSet>
            <conceptFactSet>
                <conceptFact id="1-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="matrix1" occur="1">
                        <stringValue>
                            <typedValue>A</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="2-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="matrix2" occur="1">
                        <stringValue>
                            <typedValue>A</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="3-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="matrix3" occur="1">
                        <stringValue>
                            <typedValue>A</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="4-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="matrix4" occur="1">
                        <stringValue>
                            <typedValue>A</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="2" code="Concept1.2"/>
                    <concept value="2" code="Concept1.1"/>
                </concepts>
            </conceptFactSet>
            <conceptFactSet>
                <conceptFact id="1[2]-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="matrix1" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="2[2]-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="matrix2" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="3[2]-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="matrix3" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="4[2]-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="matrix4" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="1" code="Concept1.2"/>
                    <concept value="1" code="Concept1.1"/>
                </concepts>
            </conceptFactSet>
        </conceptFinding>

        ReadOnly _finding11 As XElement =
        <conceptFinding id="matrix" scoringMethod="None">
            <conceptFact id="3-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="matrix3" occur="1">
                    <stringValue>
                        <typedValue>A</typedValue>
                    </stringValue>
                </conceptValue>
                <concepts>
                    <concept value="2" code="Concept1.1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="2-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="matrix2" occur="1">
                    <stringValue>
                        <typedValue>B</typedValue>
                    </stringValue>
                </conceptValue>
                <concepts>
                    <concept value="2" code="Concept1.2"/>
                </concepts>
            </conceptFact>
            <conceptFact id="2[*]-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="matrix2" occur="1">
                    <catchAllValue/>
                </conceptValue>
                <concepts>
                    <concept value="1" code="Concept1.2"/>
                </concepts>
            </conceptFact>
            <conceptFact id="3[*]-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="matrix3" occur="1">
                    <catchAllValue/>
                </conceptValue>
                <concepts>
                    <concept value="0" code="Concept1.1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="3[1]-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="matrix3" occur="1">
                    <stringValue>
                        <typedValue>B</typedValue>
                    </stringValue>
                </conceptValue>
                <concepts>
                    <concept value="1" code="Concept1.1"/>
                </concepts>
            </conceptFact>
            <conceptFactSet>
                <conceptFact id="1-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="matrix1" occur="1">
                        <stringValue>
                            <typedValue>A</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="4-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="matrix4" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="2" code="Concept1.1"/>
                    <concept value="3" code="Concept1.2"/>
                </concepts>
            </conceptFactSet>
            <conceptFactSet>
                <conceptFact id="1-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="matrix1" occur="1">
                        <stringValue>
                            <typedValue>A</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="4-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="matrix4" occur="1">
                        <stringValue>
                            <typedValue>A</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="1" code="Concept1.1"/>
                    <concept value="2" code="Concept1.2"/>
                </concepts>
            </conceptFactSet>
            <conceptFactSet>
                <conceptFact id="1[*]-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="matrix1" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="4[*]-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="matrix4" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="0" code="Concept1.1"/>
                    <concept value="1" code="Concept1.2"/>
                </concepts>
            </conceptFactSet>
        </conceptFinding>

        ReadOnly _finding12 As XElement =
        <conceptFinding id="gapController" scoringMethod="None">
            <conceptFact id="1-I9fbcb0a6-f900-4d33-95e7-3fa309b27134" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I9fbcb0a6-f900-4d33-95e7-3fa309b27134" occur="1">
                    <stringValue>
                        <typedValue>green</typedValue>
                    </stringValue>
                    <stringValue>
                        <typedValue>GREEN</typedValue>
                    </stringValue>
                </conceptValue>
                <concepts>
                    <concept value="0" code="EN-SC-1"/>
                    <concept value="2" code="EN-SC-3"/>
                    <concept value="2" code="EN-SC-3.1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="1-Ibb2885b7-4af2-4cfe-857e-393f43bbecca" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="Ibb2885b7-4af2-4cfe-857e-393f43bbecca" occur="1">
                    <stringValue>
                        <typedValue>red</typedValue>
                    </stringValue>
                    <stringValue>
                        <typedValue>RED</typedValue>
                    </stringValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="1-I7ee48b89-318e-4cdf-b3bf-277b745a6749" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I7ee48b89-318e-4cdf-b3bf-277b745a6749" occur="1">
                    <stringValue>
                        <typedValue>blue</typedValue>
                    </stringValue>
                    <stringValue>
                        <typedValue>BLUE</typedValue>
                    </stringValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="1-I5fde8174-6a14-4d92-b099-1559a6945c06" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I5fde8174-6a14-4d92-b099-1559a6945c06" occur="1">
                    <stringValue>
                        <typedValue>yellow</typedValue>
                    </stringValue>
                    <stringValue>
                        <typedValue>YELLOW</typedValue>
                    </stringValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="1[*]-I9fbcb0a6-f900-4d33-95e7-3fa309b27134" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I9fbcb0a6-f900-4d33-95e7-3fa309b27134" occur="1">
                    <catchAllValue/>
                </conceptValue>
                <concepts>
                    <concept value="0" code="EN-SC-1"/>
                    <concept value="0" code="EN-SC-3"/>
                    <concept value="0" code="EN-SC-3.1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="1[1]-I9fbcb0a6-f900-4d33-95e7-3fa309b27134" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I9fbcb0a6-f900-4d33-95e7-3fa309b27134" occur="1">
                    <stringValue>
                        <typedValue>grean</typedValue>
                    </stringValue>
                </conceptValue>
                <concepts>
                    <concept value="0" code="EN-SC-1"/>
                    <concept value="1" code="EN-SC-3"/>
                    <concept value="1" code="EN-SC-3.1"/>
                </concepts>
            </conceptFact>
        </conceptFinding>

        ReadOnly _finding13 As XElement =
        <conceptFinding id="inlineChoiceController" scoringMethod="None">
            <conceptFact id="C-I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" occur="1">
                    <stringValue>
                        <typedValue>C</typedValue>
                    </stringValue>
                </conceptValue>
                <concepts>
                    <concept value="3" code="Concept1"/>
                    <concept value="3" code="Concept1.2"/>
                    <concept value="3" code="Concept1.1"/>
                    <concept value="0" code="Concept2"/>
                    <concept value="2" code="Concept2.1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="E-Ie830b508-32a0-42f4-920f-0e06f0f19313" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="Ie830b508-32a0-42f4-920f-0e06f0f19313" occur="1">
                    <stringValue>
                        <typedValue>E</typedValue>
                    </stringValue>
                </conceptValue>
                <concepts>
                    <concept value="0" code="Concept1"/>
                    <concept value="3" code="Concept2"/>
                    <concept value="2" code="Concept1.1"/>
                    <concept value="3" code="Concept2.1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="A[1]-Ie830b508-32a0-42f4-920f-0e06f0f19313" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="Ie830b508-32a0-42f4-920f-0e06f0f19313" occur="1">
                    <stringValue>
                        <typedValue>A</typedValue>
                    </stringValue>
                </conceptValue>
                <concepts>
                    <concept value="0" code="Concept1"/>
                    <concept value="1" code="Concept2"/>
                    <concept value="0" code="Concept1.1"/>
                    <concept value="1" code="Concept2.1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="B[1]-Ie830b508-32a0-42f4-920f-0e06f0f19313" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="Ie830b508-32a0-42f4-920f-0e06f0f19313" occur="1">
                    <stringValue>
                        <typedValue>B</typedValue>
                    </stringValue>
                </conceptValue>
                <concepts>
                    <concept value="0" code="Concept1"/>
                    <concept value="0" code="Concept2"/>
                    <concept value="1" code="Concept1.1"/>
                    <concept value="0" code="Concept2.1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="C[1]-Ie830b508-32a0-42f4-920f-0e06f0f19313" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="Ie830b508-32a0-42f4-920f-0e06f0f19313" occur="1">
                    <stringValue>
                        <typedValue>C</typedValue>
                    </stringValue>
                </conceptValue>
                <concepts>
                    <concept value="0" code="Concept1"/>
                    <concept value="1" code="Concept2"/>
                    <concept value="0" code="Concept1.1"/>
                    <concept value="1" code="Concept2.1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="D[1]-Ie830b508-32a0-42f4-920f-0e06f0f19313" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="Ie830b508-32a0-42f4-920f-0e06f0f19313" occur="1">
                    <stringValue>
                        <typedValue>D</typedValue>
                    </stringValue>
                </conceptValue>
                <concepts>
                    <concept value="0" code="Concept1"/>
                    <concept value="2" code="Concept2"/>
                    <concept value="0" code="Concept1.1"/>
                    <concept value="2" code="Concept2.1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="A[1]-I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" occur="1">
                    <stringValue>
                        <typedValue>A</typedValue>
                    </stringValue>
                </conceptValue>
                <concepts>
                    <concept value="2" code="Concept1"/>
                    <concept value="2" code="Concept1.2"/>
                    <concept value="2" code="Concept1.1"/>
                    <concept value="0" code="Concept2"/>
                    <concept value="1" code="Concept2.1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="B[1]-I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" occur="1">
                    <stringValue>
                        <typedValue>B</typedValue>
                    </stringValue>
                </conceptValue>
                <concepts>
                    <concept value="1" code="Concept1"/>
                    <concept value="0" code="Concept1.2"/>
                    <concept value="1" code="Concept1.1"/>
                    <concept value="0" code="Concept2"/>
                    <concept value="1" code="Concept2.1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="D[1]-I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" occur="1">
                    <stringValue>
                        <typedValue>D</typedValue>
                    </stringValue>
                </conceptValue>
                <concepts>
                    <concept value="1" code="Concept1"/>
                    <concept value="1" code="Concept1.2"/>
                    <concept value="0" code="Concept1.1"/>
                    <concept value="0" code="Concept2"/>
                    <concept value="0" code="Concept2.1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="A-I22f81e91-aaa4-4528-999e-61b31b8123ec" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I22f81e91-aaa4-4528-999e-61b31b8123ec" occur="1">
                    <stringValue>
                        <typedValue>A</typedValue>
                    </stringValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="B[1]-I22f81e91-aaa4-4528-999e-61b31b8123ec" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I22f81e91-aaa4-4528-999e-61b31b8123ec" occur="1">
                    <stringValue>
                        <typedValue>B</typedValue>
                    </stringValue>
                </conceptValue>
            </conceptFact>
        </conceptFinding>

        ReadOnly _finding14 As XElement =
        <conceptFinding id="inlineChoiceController" scoringMethod="None">
            <conceptFactSet>
                <conceptFact id="A-I22f81e91-aaa4-4528-999e-61b31b8123ec" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I22f81e91-aaa4-4528-999e-61b31b8123ec" occur="1">
                        <stringValue>
                            <typedValue>A</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="A-I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" occur="1">
                        <stringValue>
                            <typedValue>A</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="A-Ie830b508-32a0-42f4-920f-0e06f0f19313" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ie830b508-32a0-42f4-920f-0e06f0f19313" occur="1">
                        <stringValue>
                            <typedValue>A</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="3" code="Concept1"/>
                    <concept value="3" code="Concept1.2"/>
                    <concept value="3" code="Concept1.1"/>
                    <concept value="2" code="Concept2"/>
                    <concept value="2" code="Concept2.1"/>
                </concepts>
            </conceptFactSet>
            <conceptFactSet>
                <conceptFact id="B-I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="B-Ie830b508-32a0-42f4-920f-0e06f0f19313" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ie830b508-32a0-42f4-920f-0e06f0f19313" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="B-I22f81e91-aaa4-4528-999e-61b31b8123ec" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I22f81e91-aaa4-4528-999e-61b31b8123ec" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="3" code="Concept1"/>
                    <concept value="3" code="Concept1.2"/>
                    <concept value="3" code="Concept1.1"/>
                    <concept value="2" code="Concept2"/>
                    <concept value="2" code="Concept2.1"/>
                </concepts>
            </conceptFactSet>
            <conceptFactSet>
                <conceptFact id="D-I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" occur="1">
                        <stringValue>
                            <typedValue>D</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="E-Ie830b508-32a0-42f4-920f-0e06f0f19313" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ie830b508-32a0-42f4-920f-0e06f0f19313" occur="1">
                        <stringValue>
                            <typedValue>E</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="B-I22f81e91-aaa4-4528-999e-61b31b8123ec" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I22f81e91-aaa4-4528-999e-61b31b8123ec" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="2" code="Concept1"/>
                    <concept value="2" code="Concept1.2"/>
                    <concept value="2" code="Concept1.1"/>
                    <concept value="3" code="Concept2"/>
                    <concept value="3" code="Concept2.1"/>
                </concepts>
            </conceptFactSet>
            <conceptFactSet>
                <conceptFact id="A[*]-I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="A[*]-I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="B[*]-I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="B[*]-I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="C[*]-I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="C[*]-I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="D[*]-I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="D[*]-I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="A[*]-Ie830b508-32a0-42f4-920f-0e06f0f19313" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="A[*]-Ie830b508-32a0-42f4-920f-0e06f0f19313" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="B[*]-Ie830b508-32a0-42f4-920f-0e06f0f19313" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="B[*]-Ie830b508-32a0-42f4-920f-0e06f0f19313" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="C[*]-Ie830b508-32a0-42f4-920f-0e06f0f19313" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="C[*]-Ie830b508-32a0-42f4-920f-0e06f0f19313" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="D[*]-Ie830b508-32a0-42f4-920f-0e06f0f19313" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="D[*]-Ie830b508-32a0-42f4-920f-0e06f0f19313" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="E[*]-Ie830b508-32a0-42f4-920f-0e06f0f19313" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="E[*]-Ie830b508-32a0-42f4-920f-0e06f0f19313" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="A[*]-I22f81e91-aaa4-4528-999e-61b31b8123ec" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="A[*]-I22f81e91-aaa4-4528-999e-61b31b8123ec" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="B[*]-I22f81e91-aaa4-4528-999e-61b31b8123ec" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="B[*]-I22f81e91-aaa4-4528-999e-61b31b8123ec" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="0" code="Concept1"/>
                    <concept value="0" code="Concept2"/>
                    <concept value="0" code="Concept1.1"/>
                    <concept value="1" code="Concept1.2"/>
                    <concept value="1" code="Concept2.1"/>
                </concepts>
            </conceptFactSet>
        </conceptFinding>

        ReadOnly _finding15 As XElement =
        <conceptFinding id="inlineChoiceController" scoringMethod="None">
            <conceptFact id="A-I22f81e91-aaa4-4528-999e-61b31b8123ec" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I22f81e91-aaa4-4528-999e-61b31b8123ec" occur="1">
                    <stringValue>
                        <typedValue>A</typedValue>
                    </stringValue>
                </conceptValue>
                <concepts>
                    <concept value="1" code="Concept1"/>
                    <concept value="1" code="Concept1.2"/>
                    <concept value="1" code="Concept1.1"/>
                    <concept value="1" code="Concept2"/>
                    <concept value="1" code="Concept2.1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="B[1]-I22f81e91-aaa4-4528-999e-61b31b8123ec" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I22f81e91-aaa4-4528-999e-61b31b8123ec" occur="1">
                    <stringValue>
                        <typedValue>B</typedValue>
                    </stringValue>
                </conceptValue>
                <concepts>
                    <concept value="0" code="Concept1"/>
                    <concept value="1" code="Concept1.2"/>
                    <concept value="0" code="Concept1.1"/>
                    <concept value="0" code="Concept2"/>
                    <concept value="1" code="Concept2.1"/>
                </concepts>
            </conceptFact>
            <conceptFactSet>
                <conceptFact id="A-I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" occur="1">
                        <stringValue>
                            <typedValue>A</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="A-Ie830b508-32a0-42f4-920f-0e06f0f19313" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ie830b508-32a0-42f4-920f-0e06f0f19313" occur="1">
                        <stringValue>
                            <typedValue>A</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="3" code="Concept1"/>
                    <concept value="3" code="Concept1.2"/>
                    <concept value="3" code="Concept1.1"/>
                    <concept value="2" code="Concept2"/>
                    <concept value="2" code="Concept2.1"/>
                </concepts>
            </conceptFactSet>
            <conceptFactSet>
                <conceptFact id="B-I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="B-Ie830b508-32a0-42f4-920f-0e06f0f19313" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ie830b508-32a0-42f4-920f-0e06f0f19313" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="3" code="Concept1"/>
                    <concept value="3" code="Concept1.2"/>
                    <concept value="3" code="Concept1.1"/>
                    <concept value="2" code="Concept2"/>
                    <concept value="2" code="Concept2.1"/>
                </concepts>
            </conceptFactSet>
            <conceptFactSet>
                <conceptFact id="D-I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" occur="1">
                        <stringValue>
                            <typedValue>D</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="E-Ie830b508-32a0-42f4-920f-0e06f0f19313" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ie830b508-32a0-42f4-920f-0e06f0f19313" occur="1">
                        <stringValue>
                            <typedValue>E</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="2" code="Concept1"/>
                    <concept value="2" code="Concept1.2"/>
                    <concept value="2" code="Concept1.1"/>
                    <concept value="3" code="Concept2"/>
                    <concept value="3" code="Concept2.1"/>
                </concepts>
            </conceptFactSet>
            <conceptFactSet>
                <conceptFact id="A[*]-I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="A[*]-I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="B[*]-I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="B[*]-I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="C[*]-I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="C[*]-I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="D[*]-I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="D[*]-I13abebba-ebb7-4ed8-81d0-46e9e1b60a53" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="A[*]-Ie830b508-32a0-42f4-920f-0e06f0f19313" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="A[*]-Ie830b508-32a0-42f4-920f-0e06f0f19313" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="B[*]-Ie830b508-32a0-42f4-920f-0e06f0f19313" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="B[*]-Ie830b508-32a0-42f4-920f-0e06f0f19313" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="C[*]-Ie830b508-32a0-42f4-920f-0e06f0f19313" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="C[*]-Ie830b508-32a0-42f4-920f-0e06f0f19313" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="D[*]-Ie830b508-32a0-42f4-920f-0e06f0f19313" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="D[*]-Ie830b508-32a0-42f4-920f-0e06f0f19313" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="E[*]-Ie830b508-32a0-42f4-920f-0e06f0f19313" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="E[*]-Ie830b508-32a0-42f4-920f-0e06f0f19313" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="0" code="Concept1"/>
                    <concept value="1" code="Concept1.2"/>
                    <concept value="0" code="Concept1.1"/>
                    <concept value="0" code="Concept2"/>
                    <concept value="1" code="Concept2.1"/>
                </concepts>
            </conceptFactSet>
        </conceptFinding>

        ReadOnly _finding16 As XElement =
        <conceptFinding id="gapMatchController" scoringMethod="None">
            <conceptFact id="I51faf178-ca03-41eb-8276-385ef2a185b3-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I51faf178-ca03-41eb-8276-385ef2a185b3" occur="1">
                    <stringValue>
                        <typedValue>A</typedValue>
                    </stringValue>
                </conceptValue>
                <concepts>
                    <concept value="2" code="Concept1"/>
                    <concept value="2" code="Concept1.2"/>
                    <concept value="2" code="Concept1.1"/>
                    <concept value="2" code="Concept2"/>
                    <concept value="2" code="Concept2.1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="I989f6f3c-9d38-492f-80fe-4b71cfee574f-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I989f6f3c-9d38-492f-80fe-4b71cfee574f" occur="1">
                    <stringValue>
                        <typedValue>B</typedValue>
                    </stringValue>
                </conceptValue>
                <concepts>
                    <concept value="0" code="Concept1"/>
                    <concept value="1" code="Concept1.2"/>
                </concepts>
            </conceptFact>
            <conceptFact id="Ie1f57945-a74c-4f6c-948b-5133fb9778e8-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="Ie1f57945-a74c-4f6c-948b-5133fb9778e8" occur="1">
                    <stringValue>
                        <typedValue>C</typedValue>
                    </stringValue>
                </conceptValue>
                <concepts>
                    <concept value="0" code="Concept1.1"/>
                    <concept value="0" code="Concept2"/>
                    <concept value="2" code="Concept2.1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="I723529e7-8893-455e-b785-595592528040-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I723529e7-8893-455e-b785-595592528040" occur="1">
                    <stringValue>
                        <typedValue>D</typedValue>
                    </stringValue>
                </conceptValue>
                <concepts>
                    <concept value="2" code="Concept1"/>
                    <concept value="2" code="Concept1.2"/>
                    <concept value="2" code="Concept1.1"/>
                    <concept value="2" code="Concept2"/>
                    <concept value="2" code="Concept2.1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="I51faf178-ca03-41eb-8276-385ef2a185b3[*]-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I51faf178-ca03-41eb-8276-385ef2a185b3" occur="1">
                    <catchAllValue/>
                </conceptValue>
                <concepts>
                    <concept value="0" code="Concept1"/>
                    <concept value="1" code="Concept1.2"/>
                    <concept value="0" code="Concept1.1"/>
                    <concept value="1" code="Concept2"/>
                    <concept value="0" code="Concept2.1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="I989f6f3c-9d38-492f-80fe-4b71cfee574f[*]-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I989f6f3c-9d38-492f-80fe-4b71cfee574f" occur="1">
                    <catchAllValue/>
                </conceptValue>
                <concepts>
                    <concept value="0" code="Concept1"/>
                    <concept value="0" code="Concept1.2"/>
                </concepts>
            </conceptFact>
            <conceptFact id="Ie1f57945-a74c-4f6c-948b-5133fb9778e8[*]-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="Ie1f57945-a74c-4f6c-948b-5133fb9778e8" occur="1">
                    <catchAllValue/>
                </conceptValue>
                <concepts>
                    <concept value="0" code="Concept1.1"/>
                    <concept value="0" code="Concept2"/>
                    <concept value="0" code="Concept2.1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="I723529e7-8893-455e-b785-595592528040[*]-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I723529e7-8893-455e-b785-595592528040" occur="1">
                    <catchAllValue/>
                </conceptValue>
                <concepts>
                    <concept value="0" code="Concept1"/>
                    <concept value="0" code="Concept1.2"/>
                    <concept value="0" code="Concept1.1"/>
                    <concept value="0" code="Concept2"/>
                    <concept value="0" code="Concept2.1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="I723529e7-8893-455e-b785-595592528040[1]-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I723529e7-8893-455e-b785-595592528040" occur="1">
                    <stringValue>
                        <typedValue>F</typedValue>
                    </stringValue>
                </conceptValue>
                <concepts>
                    <concept value="1" code="Concept1"/>
                    <concept value="1" code="Concept1.2"/>
                    <concept value="1" code="Concept1.1"/>
                    <concept value="1" code="Concept2"/>
                    <concept value="1" code="Concept2.1"/>
                </concepts>
            </conceptFact>
        </conceptFinding>

        ReadOnly _finding17 As XElement =
        <conceptFinding id="gapMatchController" scoringMethod="None">
            <conceptFactSet>
                <conceptFact id="I51faf178-ca03-41eb-8276-385ef2a185b3-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I51faf178-ca03-41eb-8276-385ef2a185b3" occur="1">
                        <stringValue>
                            <typedValue>A</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I989f6f3c-9d38-492f-80fe-4b71cfee574f-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I989f6f3c-9d38-492f-80fe-4b71cfee574f" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="Ie1f57945-a74c-4f6c-948b-5133fb9778e8-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ie1f57945-a74c-4f6c-948b-5133fb9778e8" occur="1">
                        <stringValue>
                            <typedValue>C</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I723529e7-8893-455e-b785-595592528040-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I723529e7-8893-455e-b785-595592528040" occur="1">
                        <stringValue>
                            <typedValue>D</typedValue>
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
                <conceptFact id="I51faf178-ca03-41eb-8276-385ef2a185b3[*]-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I51faf178-ca03-41eb-8276-385ef2a185b3" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I989f6f3c-9d38-492f-80fe-4b71cfee574f[*]-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I989f6f3c-9d38-492f-80fe-4b71cfee574f" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="Ie1f57945-a74c-4f6c-948b-5133fb9778e8[*]-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ie1f57945-a74c-4f6c-948b-5133fb9778e8" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I723529e7-8893-455e-b785-595592528040[*]-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I723529e7-8893-455e-b785-595592528040" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="0" code="Concept1"/>
                    <concept value="0" code="Concept2"/>
                    <concept value="1" code="Concept1.1"/>
                    <concept value="1" code="Concept2.1"/>
                    <concept value="1" code="Concept1.2"/>
                </concepts>
            </conceptFactSet>
            <conceptFactSet>
                <conceptFact id="I51faf178-ca03-41eb-8276-385ef2a185b3-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I51faf178-ca03-41eb-8276-385ef2a185b3" occur="1">
                        <stringValue>
                            <typedValue>D</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I989f6f3c-9d38-492f-80fe-4b71cfee574f-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I989f6f3c-9d38-492f-80fe-4b71cfee574f" occur="1">
                        <stringValue>
                            <typedValue>C</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="Ie1f57945-a74c-4f6c-948b-5133fb9778e8-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ie1f57945-a74c-4f6c-948b-5133fb9778e8" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I723529e7-8893-455e-b785-595592528040-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I723529e7-8893-455e-b785-595592528040" occur="1">
                        <stringValue>
                            <typedValue>A</typedValue>
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
                <conceptFact id="I51faf178-ca03-41eb-8276-385ef2a185b3-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I51faf178-ca03-41eb-8276-385ef2a185b3" occur="1">
                        <stringValue>
                            <typedValue>F</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I989f6f3c-9d38-492f-80fe-4b71cfee574f-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I989f6f3c-9d38-492f-80fe-4b71cfee574f" occur="1">
                        <stringValue>
                            <typedValue>E</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="Ie1f57945-a74c-4f6c-948b-5133fb9778e8-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ie1f57945-a74c-4f6c-948b-5133fb9778e8" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I723529e7-8893-455e-b785-595592528040-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I723529e7-8893-455e-b785-595592528040" occur="1">
                        <stringValue>
                            <typedValue>A</typedValue>
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
                <conceptFact id="I51faf178-ca03-41eb-8276-385ef2a185b3[3]-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I51faf178-ca03-41eb-8276-385ef2a185b3" occur="1">
                        <stringValue>
                            <typedValue>C</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I989f6f3c-9d38-492f-80fe-4b71cfee574f[3]-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I989f6f3c-9d38-492f-80fe-4b71cfee574f" occur="1">
                        <stringValue>
                            <typedValue>D</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="Ie1f57945-a74c-4f6c-948b-5133fb9778e8[3]-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ie1f57945-a74c-4f6c-948b-5133fb9778e8" occur="1">
                        <stringValue>
                            <typedValue>E</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I723529e7-8893-455e-b785-595592528040[3]-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I723529e7-8893-455e-b785-595592528040" occur="1">
                        <stringValue>
                            <typedValue>F</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="0" code="Concept1"/>
                    <concept value="0" code="Concept1.1"/>
                    <concept value="0" code="Concept2"/>
                    <concept value="1" code="Concept1.2"/>
                    <concept value="1" code="Concept2.1"/>
                </concepts>
            </conceptFactSet>
        </conceptFinding>

        ReadOnly _finding18 As XElement =
        <conceptFinding id="gapMatchController" scoringMethod="None">
            <conceptFact id="I723529e7-8893-455e-b785-595592528040-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I723529e7-8893-455e-b785-595592528040" occur="1">
                    <stringValue>
                        <typedValue>D</typedValue>
                    </stringValue>
                </conceptValue>
                <concepts>
                    <concept value="2" code="Concept1"/>
                    <concept value="2" code="Concept1.2"/>
                    <concept value="2" code="Concept1.1"/>
                    <concept value="2" code="Concept2"/>
                    <concept value="2" code="Concept2.1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="I723529e7-8893-455e-b785-595592528040[*]-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I723529e7-8893-455e-b785-595592528040" occur="1">
                    <catchAllValue/>
                </conceptValue>
                <concepts>
                    <concept value="0" code="Concept1"/>
                    <concept value="0" code="Concept1.2"/>
                    <concept value="0" code="Concept1.1"/>
                    <concept value="0" code="Concept2"/>
                    <concept value="0" code="Concept2.1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="I723529e7-8893-455e-b785-595592528040[1]-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I723529e7-8893-455e-b785-595592528040" occur="1">
                    <stringValue>
                        <typedValue>E</typedValue>
                    </stringValue>
                </conceptValue>
                <concepts>
                    <concept value="1" code="Concept1"/>
                    <concept value="1" code="Concept1.2"/>
                    <concept value="1" code="Concept1.1"/>
                    <concept value="1" code="Concept2"/>
                    <concept value="1" code="Concept2.1"/>
                </concepts>
            </conceptFact>
            <conceptFactSet>
                <conceptFact id="Ie1f57945-a74c-4f6c-948b-5133fb9778e8-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ie1f57945-a74c-4f6c-948b-5133fb9778e8" occur="1">
                        <stringValue>
                            <typedValue>C</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I989f6f3c-9d38-492f-80fe-4b71cfee574f-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I989f6f3c-9d38-492f-80fe-4b71cfee574f" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I51faf178-ca03-41eb-8276-385ef2a185b3-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I51faf178-ca03-41eb-8276-385ef2a185b3" occur="1">
                        <stringValue>
                            <typedValue>A</typedValue>
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
                <conceptFact id="I51faf178-ca03-41eb-8276-385ef2a185b3[*]-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I51faf178-ca03-41eb-8276-385ef2a185b3" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I989f6f3c-9d38-492f-80fe-4b71cfee574f[*]-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I989f6f3c-9d38-492f-80fe-4b71cfee574f" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="Ie1f57945-a74c-4f6c-948b-5133fb9778e8[*]-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ie1f57945-a74c-4f6c-948b-5133fb9778e8" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="0" code="Concept1"/>
                    <concept value="0" code="Concept1.1"/>
                    <concept value="0" code="Concept2"/>
                    <concept value="0" code="Concept2.1"/>
                    <concept value="1" code="Concept1.2"/>
                </concepts>
            </conceptFactSet>
            <conceptFactSet>
                <conceptFact id="I51faf178-ca03-41eb-8276-385ef2a185b3-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I51faf178-ca03-41eb-8276-385ef2a185b3" occur="1">
                        <stringValue>
                            <typedValue>C</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I989f6f3c-9d38-492f-80fe-4b71cfee574f-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I989f6f3c-9d38-492f-80fe-4b71cfee574f" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="Ie1f57945-a74c-4f6c-948b-5133fb9778e8-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ie1f57945-a74c-4f6c-948b-5133fb9778e8" occur="1">
                        <stringValue>
                            <typedValue>A</typedValue>
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
                <conceptFact id="I51faf178-ca03-41eb-8276-385ef2a185b3-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I51faf178-ca03-41eb-8276-385ef2a185b3" occur="1">
                        <stringValue>
                            <typedValue>F</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I989f6f3c-9d38-492f-80fe-4b71cfee574f-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I989f6f3c-9d38-492f-80fe-4b71cfee574f" occur="1">
                        <stringValue>
                            <typedValue>E</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="Ie1f57945-a74c-4f6c-948b-5133fb9778e8-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ie1f57945-a74c-4f6c-948b-5133fb9778e8" occur="1">
                        <stringValue>
                            <typedValue>D</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="0" code="Concept1"/>
                    <concept value="1" code="Concept1.2"/>
                    <concept value="1" code="Concept1.1"/>
                    <concept value="0" code="Concept2"/>
                    <concept value="1" code="Concept2.1"/>
                </concepts>
            </conceptFactSet>
            <conceptFactSet>
                <conceptFact id="I51faf178-ca03-41eb-8276-385ef2a185b3[3]-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I51faf178-ca03-41eb-8276-385ef2a185b3" occur="1">
                        <stringValue>
                            <typedValue>D</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I989f6f3c-9d38-492f-80fe-4b71cfee574f[3]-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I989f6f3c-9d38-492f-80fe-4b71cfee574f" occur="1">
                        <stringValue>
                            <typedValue>C</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="Ie1f57945-a74c-4f6c-948b-5133fb9778e8[3]-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ie1f57945-a74c-4f6c-948b-5133fb9778e8" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="0" code="Concept1"/>
                    <concept value="0" code="Concept1.1"/>
                    <concept value="0" code="Concept2"/>
                    <concept value="1" code="Concept1.2"/>
                    <concept value="1" code="Concept2.1"/>
                </concepts>
            </conceptFactSet>
        </conceptFinding>

        ReadOnly _finding19 As XElement =
        <conceptFinding id="orderController" scoringMethod="None">
            <conceptFactSet>
                <conceptFact id="A-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="orderController" occur="1">
                        <integerValue>
                            <typedValue>3</typedValue>
                        </integerValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="B-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="orderController" occur="1">
                        <integerValue>
                            <typedValue>1</typedValue>
                        </integerValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="C-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="orderController" occur="1">
                        <integerValue>
                            <typedValue>2</typedValue>
                        </integerValue>
                    </conceptValue>
                </conceptFact>
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="2" code="Concept2.1"/>
                </concepts>
            </conceptFactSet>
            <conceptFactSet>
                <conceptFact id="A[*]-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="orderController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="B[*]-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="orderController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="C[*]-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="orderController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="1" code="Concept2.1"/>
                </concepts>
            </conceptFactSet>
        </conceptFinding>

        ReadOnly _finding20 As XElement =
        <conceptFinding id="orderController" scoringMethod="None">
            <conceptFactSet>
                <conceptFact id="A-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="orderController" occur="1">
                        <integerValue>
                            <typedValue>3</typedValue>
                        </integerValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="B-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="orderController" occur="1">
                        <integerValue>
                            <typedValue>1</typedValue>
                        </integerValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="C-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="orderController" occur="1">
                        <integerValue>
                            <typedValue>2</typedValue>
                        </integerValue>
                    </conceptValue>
                </conceptFact>
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="2" code="Concept2.1"/>
                </concepts>
            </conceptFactSet>
            <conceptFactSet>
                <conceptFact id="A[*]-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="orderController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="B[*]-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="orderController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="C[*]-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="orderController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="1" code="Concept2.1"/>
                </concepts>
            </conceptFactSet>
            <conceptFactSet>
                <conceptFact id="A-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="orderController" occur="1">
                        <integerValue>
                            <typedValue>2</typedValue>
                        </integerValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="B-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="orderController" occur="1">
                        <integerValue>
                            <typedValue>3</typedValue>
                        </integerValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="C-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="orderController" occur="1">
                        <integerValue>
                            <typedValue>1</typedValue>
                        </integerValue>
                    </conceptValue>
                </conceptFact>
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="3" code="Concept2.1"/>
                </concepts>
            </conceptFactSet>
        </conceptFinding>

        ReadOnly _finding21 As XElement =
        <conceptFinding id="orderController" scoringMethod="None">
            <conceptFactSet>
                <conceptFact id="A-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="orderController" occur="1">
                        <integerValue>
                            <typedValue>3</typedValue>
                        </integerValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="B-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="orderController" occur="1">
                        <integerValue>
                            <typedValue>1</typedValue>
                        </integerValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="C-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="orderController" occur="1">
                        <integerValue>
                            <typedValue>2</typedValue>
                        </integerValue>
                    </conceptValue>
                </conceptFact>
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="2" code="1. wiskundige structuur"/>
                    <concept value="2" code="B1"/>
                    <concept value="2" code="B, Getallen"/>
                </concepts>
            </conceptFactSet>
        </conceptFinding>

        ReadOnly _finding22 As XElement =
        <conceptFinding id="gapMatchController" scoringMethod="None">
            <conceptFactSet>
                <conceptFact id="I97f0b37a-df8c-4f3d-868f-2b2d6490998d-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I97f0b37a-df8c-4f3d-868f-2b2d6490998d" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I62fae0a2-b76f-412f-9afd-5b75b79d9a79-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I62fae0a2-b76f-412f-9afd-5b75b79d9a79" occur="1">
                        <stringValue>
                            <typedValue>D</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="2" code="Concept2.1"/>
                </concepts>
            </conceptFactSet>
            <conceptFactSet>
                <conceptFact id="I97f0b37a-df8c-4f3d-868f-2b2d6490998d[*]-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I97f0b37a-df8c-4f3d-868f-2b2d6490998d" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I62fae0a2-b76f-412f-9afd-5b75b79d9a79[*]-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I62fae0a2-b76f-412f-9afd-5b75b79d9a79" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="1" code="Concept2.1"/>
                </concepts>
            </conceptFactSet>
        </conceptFinding>

        ReadOnly _finding23 As XElement =
        <conceptFinding id="areaInteractionController" scoringMethod="None">
            <conceptFact id="A-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="A-areaInteractionController" occur="1">
                    <booleanValue>
                        <typedValue>true</typedValue>
                    </booleanValue>
                </conceptValue>
                <concepts>
                    <concept value="2" code="Concept1"/>
                    <concept value="2" code="Concept1.2"/>
                    <concept value="2" code="Concept1.1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="B-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="B-areaInteractionController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="C-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="C-areaInteractionController" occur="1">
                    <booleanValue>
                        <typedValue>true</typedValue>
                    </booleanValue>
                </conceptValue>
                <concepts>
                    <concept value="2" code="Concept2"/>
                    <concept value="2" code="Concept2.1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="D-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="D-areaInteractionController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
                <concepts>
                    <concept value="0" code="Concept2"/>
                    <concept value="0" code="Concept2.1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="E-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="E-areaInteractionController" occur="1">
                    <booleanValue>
                        <typedValue>true</typedValue>
                    </booleanValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="A[1]-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="A[1]-areaInteractionController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
                <concepts>
                    <concept value="0" code="Concept1"/>
                    <concept value="0" code="Concept1.2"/>
                    <concept value="1" code="Concept1.1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="B[1]-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="B[1]-areaInteractionController" occur="1">
                    <booleanValue>
                        <typedValue>true</typedValue>
                    </booleanValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="C[1]-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="C[1]-areaInteractionController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
                <concepts>
                    <concept value="0" code="Concept2"/>
                    <concept value="1" code="Concept2.1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="E[1]-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="E[1]-areaInteractionController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="D[1]-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="D[1]-areaInteractionController" occur="1">
                    <booleanValue>
                        <typedValue>true</typedValue>
                    </booleanValue>
                </conceptValue>
                <concepts>
                    <concept value="1" code="Concept2"/>
                    <concept value="1" code="Concept2.1"/>
                </concepts>
            </conceptFact>
        </conceptFinding>

        ReadOnly _finding24 As XElement =
        <conceptFinding id="areaInteractionController" scoringMethod="None">
            <conceptFactSet>
                <conceptFact id="A-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="A-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="B-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="B-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="C-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="C-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="D-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="D-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="E-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="E-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="1" code="Concept1"/>
                    <concept value="1" code="Concept1.2"/>
                    <concept value="1" code="Concept2"/>
                    <concept value="1" code="Concept2.1"/>
                </concepts>
            </conceptFactSet>
            <conceptFactSet>
                <conceptFact id="A-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="A-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="B-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="B-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="C-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="C-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="D-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="D-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="E-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="E-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="2" code="Concept1"/>
                    <concept value="2" code="Concept1.2"/>
                    <concept value="2" code="Concept2"/>
                    <concept value="2" code="Concept2.1"/>
                </concepts>
            </conceptFactSet>
            <conceptFactSet>
                <conceptFact id="A-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="A-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="B-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="B-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="C-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="C-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="D-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="D-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="E-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="E-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="3" code="Concept1"/>
                    <concept value="3" code="Concept1.2"/>
                    <concept value="3" code="Concept2"/>
                    <concept value="3" code="Concept2.1"/>
                </concepts>
            </conceptFactSet>
            <conceptFactSet>
                <conceptFact id="A[*]-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="A-areaInteractionController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="B[*]-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="B-areaInteractionController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="C[*]-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="C-areaInteractionController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="D[*]-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="D-areaInteractionController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="E[*]-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="E-areaInteractionController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="0" code="Concept1.2"/>
                    <concept value="0" code="Concept2"/>
                    <concept value="0" code="Concept2.1"/>
                    <concept value="1" code="Concept1"/>
                </concepts>
            </conceptFactSet>
            <conceptFactSet>
                <conceptFact id="A[3]-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="A-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="B[3]-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="B-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="C[3]-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="C-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="D[3]-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="D-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="E[3]-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="E-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="0" code="Concept1"/>
                    <concept value="0" code="Concept2"/>
                    <concept value="1" code="Concept1.2"/>
                    <concept value="1" code="Concept2.1"/>
                </concepts>
            </conceptFactSet>
        </conceptFinding>

        ReadOnly _finding25 As XElement =
        <conceptFinding id="areaInteractionController" scoringMethod="None">
            <conceptFact id="D-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="D-areaInteractionController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
                <concepts>
                    <concept value="0" code="Concept1"/>
                    <concept value="2" code="Concept1.1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="E-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="E-areaInteractionController" occur="1">
                    <booleanValue>
                        <typedValue>true</typedValue>
                    </booleanValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="D[1]-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="D[1]-areaInteractionController" occur="1">
                    <booleanValue>
                        <typedValue>true</typedValue>
                    </booleanValue>
                </conceptValue>
                <concepts>
                    <concept value="0" code="Concept1"/>
                    <concept value="1" code="Concept1.1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="E[1]-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="E[1]-areaInteractionController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
            </conceptFact>
            <conceptFactSet>
                <conceptFact id="A-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="A-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="B-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="B-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="C-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="C-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="0" code="Concept1"/>
                    <concept value="1" code="Concept1.2"/>
                    <concept value="1" code="Concept1.1"/>
                    <concept value="0" code="Concept2"/>
                    <concept value="1" code="Concept2.1"/>
                </concepts>
            </conceptFactSet>
            <conceptFactSet>
                <conceptFact id="A-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="A-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="B-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="B-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="C-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="C-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="0" code="Concept1"/>
                    <concept value="2" code="Concept1.2"/>
                    <concept value="2" code="Concept1.1"/>
                    <concept value="0" code="Concept2"/>
                    <concept value="2" code="Concept2.1"/>
                </concepts>
            </conceptFactSet>
            <conceptFactSet>
                <conceptFact id="A-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="A-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="B-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="B-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="C-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="C-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="0" code="Concept1"/>
                    <concept value="3" code="Concept1.2"/>
                    <concept value="3" code="Concept1.1"/>
                    <concept value="0" code="Concept2"/>
                    <concept value="3" code="Concept2.1"/>
                </concepts>
            </conceptFactSet>
            <conceptFactSet>
                <conceptFact id="A[*]-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="A-areaInteractionController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="B[*]-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="B-areaInteractionController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="C[*]-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="C-areaInteractionController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="1" code="Concept1"/>
                    <concept value="0" code="Concept1.2"/>
                    <concept value="0" code="Concept1.1"/>
                    <concept value="1" code="Concept2"/>
                    <concept value="0" code="Concept2.1"/>
                </concepts>
            </conceptFactSet>
            <conceptFactSet>
                <conceptFact id="A[3]-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="A-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="B[3]-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="B-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="C[3]-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="C-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="0" code="Concept1"/>
                    <concept value="0" code="Concept1.2"/>
                    <concept value="0" code="Concept2"/>
                    <concept value="0" code="Concept2.1"/>
                    <concept value="1" code="Concept1.1"/>
                </concepts>
            </conceptFactSet>
        </conceptFinding>

        ReadOnly _finding26 As XElement =
        <conceptFinding id="areaInteractionController" scoringMethod="None">
            <conceptFact id="D-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="D-areaInteractionController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
                <concepts>
                    <concept value="0" code="Concept1"/>
                    <concept value="0" code="Concept1.1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="E-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="E-areaInteractionController" occur="1">
                    <booleanValue>
                        <typedValue>true</typedValue>
                    </booleanValue>
                </conceptValue>
                <concepts>
                    <concept value="0" code="Concept2"/>
                    <concept value="0" code="Concept2.1"/>
                    <concept value="0" code="Concept1"/>
                    <concept value="0" code="Concept1.2"/>
                    <concept value="0" code="Concept1.1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="D[1]-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="D[1]-areaInteractionController" occur="1">
                    <booleanValue>
                        <typedValue>true</typedValue>
                    </booleanValue>
                </conceptValue>
                <concepts>
                    <concept value="0" code="Concept1"/>
                    <concept value="1" code="Concept1.1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="E[1]-areaInteractionController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="E[1]-areaInteractionController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
                <concepts>
                    <concept value="0" code="Concept2"/>
                    <concept value="1" code="Concept2.1"/>
                    <concept value="0" code="Concept1"/>
                    <concept value="0" code="Concept1.2"/>
                    <concept value="1" code="Concept1.1"/>
                </concepts>
            </conceptFact>
            <conceptFactSet>
                <conceptFact id="A-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="A-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="B-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="B-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="C-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="C-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
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
                <conceptFact id="A-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="A-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="B-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="B-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="C-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="C-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
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
                <conceptFact id="A-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="A-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="B-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="B-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="C-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="C-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="0" code="Concept1"/>
                    <concept value="3" code="Concept1.2"/>
                    <concept value="3" code="Concept1.1"/>
                    <concept value="0" code="Concept2"/>
                    <concept value="3" code="Concept2.1"/>
                </concepts>
            </conceptFactSet>
            <conceptFactSet>
                <conceptFact id="A[*]-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="A-areaInteractionController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="B[*]-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="B-areaInteractionController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="C[*]-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="C-areaInteractionController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="1" code="Concept1"/>
                    <concept value="0" code="Concept1.2"/>
                    <concept value="0" code="Concept1.1"/>
                    <concept value="1" code="Concept2"/>
                    <concept value="0" code="Concept2.1"/>
                </concepts>
            </conceptFactSet>
            <conceptFactSet>
                <conceptFact id="A[3]-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="A-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="B[3]-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="B-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="C[3]-areaInteractionController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="C-areaInteractionController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="0" code="Concept1"/>
                    <concept value="0" code="Concept1.2"/>
                    <concept value="1" code="Concept1.1"/>
                    <concept value="0" code="Concept2"/>
                    <concept value="0" code="Concept2.1"/>
                </concepts>
            </conceptFactSet>
        </conceptFinding>

        ReadOnly _finding27 As XElement =
        <conceptFinding id="gapMatchController" scoringMethod="None">
            <conceptFact id="I51faf178-ca03-41eb-8276-385ef2a185b3-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I51faf178-ca03-41eb-8276-385ef2a185b3" occur="1">
                    <stringValue>
                        <typedValue>A</typedValue>
                    </stringValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="I989f6f3c-9d38-492f-80fe-4b71cfee574f-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I989f6f3c-9d38-492f-80fe-4b71cfee574f" occur="1">
                    <stringValue>
                        <typedValue>B</typedValue>
                    </stringValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="Ie1f57945-a74c-4f6c-948b-5133fb9778e8-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="Ie1f57945-a74c-4f6c-948b-5133fb9778e8" occur="1">
                    <stringValue>
                        <typedValue>C</typedValue>
                    </stringValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="I723529e7-8893-455e-b785-595592528040-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I723529e7-8893-455e-b785-595592528040" occur="1">
                    <stringValue>
                        <typedValue>D</typedValue>
                    </stringValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="I51faf178-ca03-41eb-8276-385ef2a185b3[*]-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I51faf178-ca03-41eb-8276-385ef2a185b3" occur="1">
                    <catchAllValue/>
                </conceptValue>
            </conceptFact>
            <conceptFact id="I989f6f3c-9d38-492f-80fe-4b71cfee574f[*]-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I989f6f3c-9d38-492f-80fe-4b71cfee574f" occur="1">
                    <catchAllValue/>
                </conceptValue>
            </conceptFact>
            <conceptFact id="Ie1f57945-a74c-4f6c-948b-5133fb9778e8[*]-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="Ie1f57945-a74c-4f6c-948b-5133fb9778e8" occur="1">
                    <catchAllValue/>
                </conceptValue>
            </conceptFact>
            <conceptFact id="I723529e7-8893-455e-b785-595592528040[*]-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I723529e7-8893-455e-b785-595592528040" occur="1">
                    <catchAllValue/>
                </conceptValue>
            </conceptFact>
            <conceptFact id="I723529e7-8893-455e-b785-595592528040[1]-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I723529e7-8893-455e-b785-595592528040" occur="1">
                    <stringValue>
                        <typedValue>F</typedValue>
                    </stringValue>
                </conceptValue>
            </conceptFact>
        </conceptFinding>

        ReadOnly _finding28 As XElement =
        <conceptFinding id="gapMatchController" scoringMethod="None">
            <conceptFactSet>
                <conceptFact id="I51faf178-ca03-41eb-8276-385ef2a185b3-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I51faf178-ca03-41eb-8276-385ef2a185b3" occur="1">
                        <stringValue>
                            <typedValue>A</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I989f6f3c-9d38-492f-80fe-4b71cfee574f-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I989f6f3c-9d38-492f-80fe-4b71cfee574f" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="Ie1f57945-a74c-4f6c-948b-5133fb9778e8-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ie1f57945-a74c-4f6c-948b-5133fb9778e8" occur="1">
                        <stringValue>
                            <typedValue>C</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I723529e7-8893-455e-b785-595592528040-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I723529e7-8893-455e-b785-595592528040" occur="1">
                        <stringValue>
                            <typedValue>D</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
            </conceptFactSet>
            <conceptFactSet>
                <conceptFact id="I51faf178-ca03-41eb-8276-385ef2a185b3[*]-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I51faf178-ca03-41eb-8276-385ef2a185b3" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I989f6f3c-9d38-492f-80fe-4b71cfee574f[*]-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I989f6f3c-9d38-492f-80fe-4b71cfee574f" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="Ie1f57945-a74c-4f6c-948b-5133fb9778e8[*]-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ie1f57945-a74c-4f6c-948b-5133fb9778e8" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I723529e7-8893-455e-b785-595592528040[*]-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I723529e7-8893-455e-b785-595592528040" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
            </conceptFactSet>
            <conceptFactSet>
                <conceptFact id="I51faf178-ca03-41eb-8276-385ef2a185b3-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I51faf178-ca03-41eb-8276-385ef2a185b3" occur="1">
                        <stringValue>
                            <typedValue>D</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I989f6f3c-9d38-492f-80fe-4b71cfee574f-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I989f6f3c-9d38-492f-80fe-4b71cfee574f" occur="1">
                        <stringValue>
                            <typedValue>C</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="Ie1f57945-a74c-4f6c-948b-5133fb9778e8-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ie1f57945-a74c-4f6c-948b-5133fb9778e8" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I723529e7-8893-455e-b785-595592528040-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I723529e7-8893-455e-b785-595592528040" occur="1">
                        <stringValue>
                            <typedValue>A</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
            </conceptFactSet>
            <conceptFactSet>
                <conceptFact id="I51faf178-ca03-41eb-8276-385ef2a185b3-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I51faf178-ca03-41eb-8276-385ef2a185b3" occur="1">
                        <stringValue>
                            <typedValue>F</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I989f6f3c-9d38-492f-80fe-4b71cfee574f-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I989f6f3c-9d38-492f-80fe-4b71cfee574f" occur="1">
                        <stringValue>
                            <typedValue>E</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="Ie1f57945-a74c-4f6c-948b-5133fb9778e8-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ie1f57945-a74c-4f6c-948b-5133fb9778e8" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I723529e7-8893-455e-b785-595592528040-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I723529e7-8893-455e-b785-595592528040" occur="1">
                        <stringValue>
                            <typedValue>A</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
            </conceptFactSet>
            <conceptFactSet>
                <conceptFact id="I51faf178-ca03-41eb-8276-385ef2a185b3[3]-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I51faf178-ca03-41eb-8276-385ef2a185b3" occur="1">
                        <stringValue>
                            <typedValue>C</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I989f6f3c-9d38-492f-80fe-4b71cfee574f[3]-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I989f6f3c-9d38-492f-80fe-4b71cfee574f" occur="1">
                        <stringValue>
                            <typedValue>D</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="Ie1f57945-a74c-4f6c-948b-5133fb9778e8[3]-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ie1f57945-a74c-4f6c-948b-5133fb9778e8" occur="1">
                        <stringValue>
                            <typedValue>E</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I723529e7-8893-455e-b785-595592528040[3]-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I723529e7-8893-455e-b785-595592528040" occur="1">
                        <stringValue>
                            <typedValue>F</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
            </conceptFactSet>
        </conceptFinding>

        ReadOnly _finding29 As XElement =
        <conceptFinding id="gapMatchController" scoringMethod="None">
            <conceptFact id="I723529e7-8893-455e-b785-595592528040-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I723529e7-8893-455e-b785-595592528040" occur="1">
                    <stringValue>
                        <typedValue>D</typedValue>
                    </stringValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="I723529e7-8893-455e-b785-595592528040[*]-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I723529e7-8893-455e-b785-595592528040" occur="1">
                    <catchAllValue/>
                </conceptValue>
            </conceptFact>
            <conceptFact id="I723529e7-8893-455e-b785-595592528040[1]-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I723529e7-8893-455e-b785-595592528040" occur="1">
                    <stringValue>
                        <typedValue>E</typedValue>
                    </stringValue>
                </conceptValue>
            </conceptFact>
            <conceptFactSet>
                <conceptFact id="Ie1f57945-a74c-4f6c-948b-5133fb9778e8-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ie1f57945-a74c-4f6c-948b-5133fb9778e8" occur="1">
                        <stringValue>
                            <typedValue>C</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I989f6f3c-9d38-492f-80fe-4b71cfee574f-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I989f6f3c-9d38-492f-80fe-4b71cfee574f" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I51faf178-ca03-41eb-8276-385ef2a185b3-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I51faf178-ca03-41eb-8276-385ef2a185b3" occur="1">
                        <stringValue>
                            <typedValue>A</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
            </conceptFactSet>
            <conceptFactSet>
                <conceptFact id="I51faf178-ca03-41eb-8276-385ef2a185b3[*]-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I51faf178-ca03-41eb-8276-385ef2a185b3" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I989f6f3c-9d38-492f-80fe-4b71cfee574f[*]-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I989f6f3c-9d38-492f-80fe-4b71cfee574f" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="Ie1f57945-a74c-4f6c-948b-5133fb9778e8[*]-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ie1f57945-a74c-4f6c-948b-5133fb9778e8" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
            </conceptFactSet>
            <conceptFactSet>
                <conceptFact id="I51faf178-ca03-41eb-8276-385ef2a185b3-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I51faf178-ca03-41eb-8276-385ef2a185b3" occur="1">
                        <stringValue>
                            <typedValue>C</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I989f6f3c-9d38-492f-80fe-4b71cfee574f-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I989f6f3c-9d38-492f-80fe-4b71cfee574f" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="Ie1f57945-a74c-4f6c-948b-5133fb9778e8-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ie1f57945-a74c-4f6c-948b-5133fb9778e8" occur="1">
                        <stringValue>
                            <typedValue>A</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
            </conceptFactSet>
            <conceptFactSet>
                <conceptFact id="I51faf178-ca03-41eb-8276-385ef2a185b3-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I51faf178-ca03-41eb-8276-385ef2a185b3" occur="1">
                        <stringValue>
                            <typedValue>F</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I989f6f3c-9d38-492f-80fe-4b71cfee574f-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I989f6f3c-9d38-492f-80fe-4b71cfee574f" occur="1">
                        <stringValue>
                            <typedValue>E</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="Ie1f57945-a74c-4f6c-948b-5133fb9778e8-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ie1f57945-a74c-4f6c-948b-5133fb9778e8" occur="1">
                        <stringValue>
                            <typedValue>D</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
            </conceptFactSet>
            <conceptFactSet>
                <conceptFact id="I51faf178-ca03-41eb-8276-385ef2a185b3[3]-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I51faf178-ca03-41eb-8276-385ef2a185b3" occur="1">
                        <stringValue>
                            <typedValue>D</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I989f6f3c-9d38-492f-80fe-4b71cfee574f[3]-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I989f6f3c-9d38-492f-80fe-4b71cfee574f" occur="1">
                        <stringValue>
                            <typedValue>C</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="Ie1f57945-a74c-4f6c-948b-5133fb9778e8[3]-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ie1f57945-a74c-4f6c-948b-5133fb9778e8" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
            </conceptFactSet>
        </conceptFinding>

        ReadOnly _finding30 As XElement =
        <conceptFinding id="hottextController" scoringMethod="None">
            <conceptFact id="Iba0772fe-878f-457c-b652-a0e922626082-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="Iba0772fe-878f-457c-b652-a0e922626082-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="If9dc68d4-33ec-4176-adb2-eda10373f1a0-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="If9dc68d4-33ec-4176-adb2-eda10373f1a0-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="Ice8f6ba6-0b24-4234-8e1c-ea2d6caf18f6-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="Ice8f6ba6-0b24-4234-8e1c-ea2d6caf18f6-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="Ia64d8e75-aa61-481b-bd40-df7270053448-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="Ia64d8e75-aa61-481b-bd40-df7270053448-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="I2a787ac6-adce-419f-b230-293302ab6e2b-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I2a787ac6-adce-419f-b230-293302ab6e2b-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>true</typedValue>
                    </booleanValue>
                </conceptValue>
                <concepts>
                    <concept value="1" code="Concept2"/>
                </concepts>
            </conceptFact>
            <conceptFact id="I562dc41e-1fcd-45b2-ad63-e346a9b12ed3-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I562dc41e-1fcd-45b2-ad63-e346a9b12ed3-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="I2ff56de9-53fd-4be6-adc3-b9d48cbac0bd-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I2ff56de9-53fd-4be6-adc3-b9d48cbac0bd-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="I8c49dcaf-eea6-48ad-bcf0-5c00ccde0415-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I8c49dcaf-eea6-48ad-bcf0-5c00ccde0415-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="I7b431d95-a30e-403f-9b35-7575b30d31a3-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I7b431d95-a30e-403f-9b35-7575b30d31a3-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="I46f0651d-a9f2-4b96-8172-8104b0938213-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I46f0651d-a9f2-4b96-8172-8104b0938213-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>true</typedValue>
                    </booleanValue>
                </conceptValue>
                <concepts>
                    <concept value="1" code="Concept2"/>
                    <concept value="1" code="Concept2.1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="I1e40e819-c8a2-42dd-89de-dd29e070b99b-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I1e40e819-c8a2-42dd-89de-dd29e070b99b-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="I67b6042e-7444-46be-b169-53ec421f9eda-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I67b6042e-7444-46be-b169-53ec421f9eda-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="I18248834-8320-4342-967c-2007195ba14a-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I18248834-8320-4342-967c-2007195ba14a-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="I0bf4350c-9288-4cec-9e37-1b0978b057ba-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I0bf4350c-9288-4cec-9e37-1b0978b057ba-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>true</typedValue>
                    </booleanValue>
                </conceptValue>
                <concepts>
                    <concept value="1" code="Concept1"/>
                    <concept value="1" code="Concept2"/>
                </concepts>
            </conceptFact>
            <conceptFact id="Ifb1bfeea-20a0-4d29-bb87-d502ff355522-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="Ifb1bfeea-20a0-4d29-bb87-d502ff355522-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="Ie1f531cd-79c9-4b87-a90c-76766792b183-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="Ie1f531cd-79c9-4b87-a90c-76766792b183-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="I66faad9a-d004-4cdc-a275-6f9ff736bb14-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I66faad9a-d004-4cdc-a275-6f9ff736bb14-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="I665f2702-e926-4266-bc3c-3f04ea9a6c62-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I665f2702-e926-4266-bc3c-3f04ea9a6c62-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>true</typedValue>
                    </booleanValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="I7e8b004a-0223-432a-bb9a-536918190017-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I7e8b004a-0223-432a-bb9a-536918190017-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
                <concepts>
                    <concept value="2" code="Concept1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="I217873b0-4970-405f-a80b-b2a9f42bbd85-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I217873b0-4970-405f-a80b-b2a9f42bbd85-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="I46521597-f1f4-432c-96c2-8e3defb05950-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I46521597-f1f4-432c-96c2-8e3defb05950-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>true</typedValue>
                    </booleanValue>
                </conceptValue>
                <concepts>
                    <concept value="2" code="Concept1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="Ifbea86b5-20bc-4dde-a4e4-4a14153c8ead-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="Ifbea86b5-20bc-4dde-a4e4-4a14153c8ead-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="I4142aba3-f108-41dc-b537-ab44fd8cb5ae-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I4142aba3-f108-41dc-b537-ab44fd8cb5ae-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="I279d3c47-95a8-42d1-9440-26fadf909090-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I279d3c47-95a8-42d1-9440-26fadf909090-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="I8ecdc331-d565-4fcc-9f1f-a83e6e6bab9d-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I8ecdc331-d565-4fcc-9f1f-a83e6e6bab9d-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="I80cb35ef-89d7-4ada-aef4-eda2b963a606-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I80cb35ef-89d7-4ada-aef4-eda2b963a606-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="I5493e55f-fae9-499f-bd3e-16df52c028e1-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I5493e55f-fae9-499f-bd3e-16df52c028e1-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="I7134c778-4641-40b5-8eae-1a80013cf777-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I7134c778-4641-40b5-8eae-1a80013cf777-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="I3cb9edb6-551e-474a-9122-338b8c6a396d-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I3cb9edb6-551e-474a-9122-338b8c6a396d-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>true</typedValue>
                    </booleanValue>
                </conceptValue>
                <concepts>
                    <concept value="0" code="Concept2"/>
                </concepts>
            </conceptFact>
            <conceptFact id="I41d5e633-d892-46fe-8a0c-ceba54c167f3-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I41d5e633-d892-46fe-8a0c-ceba54c167f3-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="I498a96cd-114d-4299-b27c-639e41559e29-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I498a96cd-114d-4299-b27c-639e41559e29-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="I19b64b54-f7f4-4284-af37-37999bd24935-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I19b64b54-f7f4-4284-af37-37999bd24935-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="I7ea195a7-b0ec-465e-ada3-31c9e940f0dc-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I7ea195a7-b0ec-465e-ada3-31c9e940f0dc-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="Ibf683ce7-ac85-4610-b057-dd0578f3b71f-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="Ibf683ce7-ac85-4610-b057-dd0578f3b71f-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="Ie6f1fe1f-fe23-4f29-85ec-27d483138cfa-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="Ie6f1fe1f-fe23-4f29-85ec-27d483138cfa-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="I2b4ed71d-8984-4ffd-a95a-126efbb12e48-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I2b4ed71d-8984-4ffd-a95a-126efbb12e48-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="I0778d3a8-11c3-41f5-aeb7-4efd94338296-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I0778d3a8-11c3-41f5-aeb7-4efd94338296-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="I3a1bf3a7-f9b4-472d-ac19-c6d27f671543-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I3a1bf3a7-f9b4-472d-ac19-c6d27f671543-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="I01f17f48-6da9-44a0-bb7f-82e325f2f233-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I01f17f48-6da9-44a0-bb7f-82e325f2f233-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="Ifdbe8e9c-0eeb-4019-b8a7-5599265834fb-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="Ifdbe8e9c-0eeb-4019-b8a7-5599265834fb-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="I0dd2d1e9-84b7-4b3f-a144-a2ec2c13061b-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I0dd2d1e9-84b7-4b3f-a144-a2ec2c13061b-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="I41e6c74d-787a-4620-889f-fae84be71cdb-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I41e6c74d-787a-4620-889f-fae84be71cdb-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="I1f639c54-57ee-428e-93ed-77d786d5a2d1-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I1f639c54-57ee-428e-93ed-77d786d5a2d1-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>true</typedValue>
                    </booleanValue>
                </conceptValue>
                <concepts>
                    <concept value="1" code="Concept2"/>
                </concepts>
            </conceptFact>
            <conceptFact id="Ib580dda7-0717-4cfc-b70e-34059b4c78f1-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="Ib580dda7-0717-4cfc-b70e-34059b4c78f1-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="I834d38a6-dac5-4235-9cf4-d323dd89f1d2-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I834d38a6-dac5-4235-9cf4-d323dd89f1d2-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="Ic858ebcf-6f25-46ee-be0b-afbee1f41971-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="Ic858ebcf-6f25-46ee-be0b-afbee1f41971-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="If6a1b756-b293-4baf-a3c0-3332221e3ea8-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="If6a1b756-b293-4baf-a3c0-3332221e3ea8-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="Ia2c49323-f160-4dfa-8f79-5abea876efa6-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="Ia2c49323-f160-4dfa-8f79-5abea876efa6-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="I6d80a45f-5042-4f42-99fe-151229268a43-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I6d80a45f-5042-4f42-99fe-151229268a43-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="Iba0772fe-878f-457c-b652-a0e922626082[1]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="Iba0772fe-878f-457c-b652-a0e922626082[1]-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>true</typedValue>
                    </booleanValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="I0bf4350c-9288-4cec-9e37-1b0978b057ba[1]-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I0bf4350c-9288-4cec-9e37-1b0978b057ba[1]-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
                <concepts>
                    <concept value="2" code="Concept1"/>
                    <concept value="2" code="Concept2"/>
                </concepts>
            </conceptFact>
            <conceptFact id="I7e8b004a-0223-432a-bb9a-536918190017[1]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I7e8b004a-0223-432a-bb9a-536918190017[1]-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>true</typedValue>
                    </booleanValue>
                </conceptValue>
                <concepts>
                    <concept value="1" code="Concept1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="I46521597-f1f4-432c-96c2-8e3defb05950[1]-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I46521597-f1f4-432c-96c2-8e3defb05950[1]-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
                <concepts>
                    <concept value="0" code="Concept1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="I3cb9edb6-551e-474a-9122-338b8c6a396d[1]-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I3cb9edb6-551e-474a-9122-338b8c6a396d[1]-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
                <concepts>
                    <concept value="0" code="Concept2"/>
                </concepts>
            </conceptFact>
            <conceptFact id="I1f639c54-57ee-428e-93ed-77d786d5a2d1[1]-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I1f639c54-57ee-428e-93ed-77d786d5a2d1[1]-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
                <concepts>
                    <concept value="0" code="Concept2"/>
                </concepts>
            </conceptFact>
            <conceptFact id="I2a787ac6-adce-419f-b230-293302ab6e2b[1]-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I2a787ac6-adce-419f-b230-293302ab6e2b[1]-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
                <concepts>
                    <concept value="0" code="Concept2"/>
                </concepts>
            </conceptFact>
            <conceptFact id="I46f0651d-a9f2-4b96-8172-8104b0938213[1]-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I46f0651d-a9f2-4b96-8172-8104b0938213[1]-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
                <concepts>
                    <concept value="0" code="Concept2"/>
                    <concept value="0" code="Concept2.1"/>
                </concepts>
            </conceptFact>
        </conceptFinding>

        ReadOnly _finding31 As XElement =
        <conceptFinding id="hottextController" scoringMethod="None">
            <conceptFactSet>
                <conceptFact id="Iba0772fe-878f-457c-b652-a0e922626082-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Iba0772fe-878f-457c-b652-a0e922626082-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I5493e55f-fae9-499f-bd3e-16df52c028e1-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I5493e55f-fae9-499f-bd3e-16df52c028e1-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I7134c778-4641-40b5-8eae-1a80013cf777-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I7134c778-4641-40b5-8eae-1a80013cf777-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I3cb9edb6-551e-474a-9122-338b8c6a396d-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I3cb9edb6-551e-474a-9122-338b8c6a396d-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I41d5e633-d892-46fe-8a0c-ceba54c167f3-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I41d5e633-d892-46fe-8a0c-ceba54c167f3-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I498a96cd-114d-4299-b27c-639e41559e29-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I498a96cd-114d-4299-b27c-639e41559e29-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I19b64b54-f7f4-4284-af37-37999bd24935-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I19b64b54-f7f4-4284-af37-37999bd24935-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I7ea195a7-b0ec-465e-ada3-31c9e940f0dc-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I7ea195a7-b0ec-465e-ada3-31c9e940f0dc-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="Ibf683ce7-ac85-4610-b057-dd0578f3b71f-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ibf683ce7-ac85-4610-b057-dd0578f3b71f-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="Ie6f1fe1f-fe23-4f29-85ec-27d483138cfa-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ie6f1fe1f-fe23-4f29-85ec-27d483138cfa-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I2b4ed71d-8984-4ffd-a95a-126efbb12e48-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I2b4ed71d-8984-4ffd-a95a-126efbb12e48-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I0778d3a8-11c3-41f5-aeb7-4efd94338296-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I0778d3a8-11c3-41f5-aeb7-4efd94338296-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I3a1bf3a7-f9b4-472d-ac19-c6d27f671543-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I3a1bf3a7-f9b4-472d-ac19-c6d27f671543-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I01f17f48-6da9-44a0-bb7f-82e325f2f233-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I01f17f48-6da9-44a0-bb7f-82e325f2f233-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="Ifdbe8e9c-0eeb-4019-b8a7-5599265834fb-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ifdbe8e9c-0eeb-4019-b8a7-5599265834fb-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I0dd2d1e9-84b7-4b3f-a144-a2ec2c13061b-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I0dd2d1e9-84b7-4b3f-a144-a2ec2c13061b-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I41e6c74d-787a-4620-889f-fae84be71cdb-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I41e6c74d-787a-4620-889f-fae84be71cdb-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I1f639c54-57ee-428e-93ed-77d786d5a2d1-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I1f639c54-57ee-428e-93ed-77d786d5a2d1-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="Ib580dda7-0717-4cfc-b70e-34059b4c78f1-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ib580dda7-0717-4cfc-b70e-34059b4c78f1-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I834d38a6-dac5-4235-9cf4-d323dd89f1d2-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I834d38a6-dac5-4235-9cf4-d323dd89f1d2-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="Ic858ebcf-6f25-46ee-be0b-afbee1f41971-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ic858ebcf-6f25-46ee-be0b-afbee1f41971-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="If6a1b756-b293-4baf-a3c0-3332221e3ea8-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="If6a1b756-b293-4baf-a3c0-3332221e3ea8-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I80cb35ef-89d7-4ada-aef4-eda2b963a606-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I80cb35ef-89d7-4ada-aef4-eda2b963a606-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="Ia2c49323-f160-4dfa-8f79-5abea876efa6-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ia2c49323-f160-4dfa-8f79-5abea876efa6-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I8ecdc331-d565-4fcc-9f1f-a83e6e6bab9d-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I8ecdc331-d565-4fcc-9f1f-a83e6e6bab9d-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I4142aba3-f108-41dc-b537-ab44fd8cb5ae-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I4142aba3-f108-41dc-b537-ab44fd8cb5ae-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="If9dc68d4-33ec-4176-adb2-eda10373f1a0-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="If9dc68d4-33ec-4176-adb2-eda10373f1a0-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="Ice8f6ba6-0b24-4234-8e1c-ea2d6caf18f6-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ice8f6ba6-0b24-4234-8e1c-ea2d6caf18f6-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="Ia64d8e75-aa61-481b-bd40-df7270053448-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ia64d8e75-aa61-481b-bd40-df7270053448-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I2a787ac6-adce-419f-b230-293302ab6e2b-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I2a787ac6-adce-419f-b230-293302ab6e2b-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I562dc41e-1fcd-45b2-ad63-e346a9b12ed3-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I562dc41e-1fcd-45b2-ad63-e346a9b12ed3-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I2ff56de9-53fd-4be6-adc3-b9d48cbac0bd-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I2ff56de9-53fd-4be6-adc3-b9d48cbac0bd-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I8c49dcaf-eea6-48ad-bcf0-5c00ccde0415-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I8c49dcaf-eea6-48ad-bcf0-5c00ccde0415-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I7b431d95-a30e-403f-9b35-7575b30d31a3-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I7b431d95-a30e-403f-9b35-7575b30d31a3-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I46f0651d-a9f2-4b96-8172-8104b0938213-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I46f0651d-a9f2-4b96-8172-8104b0938213-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I1e40e819-c8a2-42dd-89de-dd29e070b99b-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I1e40e819-c8a2-42dd-89de-dd29e070b99b-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I67b6042e-7444-46be-b169-53ec421f9eda-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I67b6042e-7444-46be-b169-53ec421f9eda-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I18248834-8320-4342-967c-2007195ba14a-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I18248834-8320-4342-967c-2007195ba14a-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I0bf4350c-9288-4cec-9e37-1b0978b057ba-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I0bf4350c-9288-4cec-9e37-1b0978b057ba-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="Ifb1bfeea-20a0-4d29-bb87-d502ff355522-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ifb1bfeea-20a0-4d29-bb87-d502ff355522-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="Ie1f531cd-79c9-4b87-a90c-76766792b183-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ie1f531cd-79c9-4b87-a90c-76766792b183-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I66faad9a-d004-4cdc-a275-6f9ff736bb14-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I66faad9a-d004-4cdc-a275-6f9ff736bb14-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I665f2702-e926-4266-bc3c-3f04ea9a6c62-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I665f2702-e926-4266-bc3c-3f04ea9a6c62-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I7e8b004a-0223-432a-bb9a-536918190017-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I7e8b004a-0223-432a-bb9a-536918190017-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I217873b0-4970-405f-a80b-b2a9f42bbd85-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I217873b0-4970-405f-a80b-b2a9f42bbd85-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I46521597-f1f4-432c-96c2-8e3defb05950-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I46521597-f1f4-432c-96c2-8e3defb05950-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="Ifbea86b5-20bc-4dde-a4e4-4a14153c8ead-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ifbea86b5-20bc-4dde-a4e4-4a14153c8ead-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I279d3c47-95a8-42d1-9440-26fadf909090-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I279d3c47-95a8-42d1-9440-26fadf909090-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I6d80a45f-5042-4f42-99fe-151229268a43-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I6d80a45f-5042-4f42-99fe-151229268a43-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
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
                <conceptFact id="Iba0772fe-878f-457c-b652-a0e922626082-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Iba0772fe-878f-457c-b652-a0e922626082-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I5493e55f-fae9-499f-bd3e-16df52c028e1-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I5493e55f-fae9-499f-bd3e-16df52c028e1-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I7134c778-4641-40b5-8eae-1a80013cf777-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I7134c778-4641-40b5-8eae-1a80013cf777-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I3cb9edb6-551e-474a-9122-338b8c6a396d-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I3cb9edb6-551e-474a-9122-338b8c6a396d-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I41d5e633-d892-46fe-8a0c-ceba54c167f3-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I41d5e633-d892-46fe-8a0c-ceba54c167f3-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I498a96cd-114d-4299-b27c-639e41559e29-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I498a96cd-114d-4299-b27c-639e41559e29-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I19b64b54-f7f4-4284-af37-37999bd24935-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I19b64b54-f7f4-4284-af37-37999bd24935-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I7ea195a7-b0ec-465e-ada3-31c9e940f0dc-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I7ea195a7-b0ec-465e-ada3-31c9e940f0dc-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="Ibf683ce7-ac85-4610-b057-dd0578f3b71f-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ibf683ce7-ac85-4610-b057-dd0578f3b71f-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="Ie6f1fe1f-fe23-4f29-85ec-27d483138cfa-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ie6f1fe1f-fe23-4f29-85ec-27d483138cfa-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I2b4ed71d-8984-4ffd-a95a-126efbb12e48-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I2b4ed71d-8984-4ffd-a95a-126efbb12e48-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I0778d3a8-11c3-41f5-aeb7-4efd94338296-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I0778d3a8-11c3-41f5-aeb7-4efd94338296-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I3a1bf3a7-f9b4-472d-ac19-c6d27f671543-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I3a1bf3a7-f9b4-472d-ac19-c6d27f671543-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I01f17f48-6da9-44a0-bb7f-82e325f2f233-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I01f17f48-6da9-44a0-bb7f-82e325f2f233-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="Ifdbe8e9c-0eeb-4019-b8a7-5599265834fb-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ifdbe8e9c-0eeb-4019-b8a7-5599265834fb-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I0dd2d1e9-84b7-4b3f-a144-a2ec2c13061b-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I0dd2d1e9-84b7-4b3f-a144-a2ec2c13061b-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I41e6c74d-787a-4620-889f-fae84be71cdb-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I41e6c74d-787a-4620-889f-fae84be71cdb-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I1f639c54-57ee-428e-93ed-77d786d5a2d1-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I1f639c54-57ee-428e-93ed-77d786d5a2d1-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="Ib580dda7-0717-4cfc-b70e-34059b4c78f1-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ib580dda7-0717-4cfc-b70e-34059b4c78f1-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I834d38a6-dac5-4235-9cf4-d323dd89f1d2-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I834d38a6-dac5-4235-9cf4-d323dd89f1d2-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="Ic858ebcf-6f25-46ee-be0b-afbee1f41971-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ic858ebcf-6f25-46ee-be0b-afbee1f41971-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="If6a1b756-b293-4baf-a3c0-3332221e3ea8-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="If6a1b756-b293-4baf-a3c0-3332221e3ea8-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I80cb35ef-89d7-4ada-aef4-eda2b963a606-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I80cb35ef-89d7-4ada-aef4-eda2b963a606-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="Ia2c49323-f160-4dfa-8f79-5abea876efa6-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ia2c49323-f160-4dfa-8f79-5abea876efa6-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I8ecdc331-d565-4fcc-9f1f-a83e6e6bab9d-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I8ecdc331-d565-4fcc-9f1f-a83e6e6bab9d-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I4142aba3-f108-41dc-b537-ab44fd8cb5ae-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I4142aba3-f108-41dc-b537-ab44fd8cb5ae-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="If9dc68d4-33ec-4176-adb2-eda10373f1a0-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="If9dc68d4-33ec-4176-adb2-eda10373f1a0-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="Ice8f6ba6-0b24-4234-8e1c-ea2d6caf18f6-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ice8f6ba6-0b24-4234-8e1c-ea2d6caf18f6-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="Ia64d8e75-aa61-481b-bd40-df7270053448-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ia64d8e75-aa61-481b-bd40-df7270053448-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I2a787ac6-adce-419f-b230-293302ab6e2b-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I2a787ac6-adce-419f-b230-293302ab6e2b-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I562dc41e-1fcd-45b2-ad63-e346a9b12ed3-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I562dc41e-1fcd-45b2-ad63-e346a9b12ed3-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I2ff56de9-53fd-4be6-adc3-b9d48cbac0bd-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I2ff56de9-53fd-4be6-adc3-b9d48cbac0bd-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I8c49dcaf-eea6-48ad-bcf0-5c00ccde0415-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I8c49dcaf-eea6-48ad-bcf0-5c00ccde0415-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I7b431d95-a30e-403f-9b35-7575b30d31a3-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I7b431d95-a30e-403f-9b35-7575b30d31a3-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I46f0651d-a9f2-4b96-8172-8104b0938213-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I46f0651d-a9f2-4b96-8172-8104b0938213-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I1e40e819-c8a2-42dd-89de-dd29e070b99b-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I1e40e819-c8a2-42dd-89de-dd29e070b99b-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I67b6042e-7444-46be-b169-53ec421f9eda-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I67b6042e-7444-46be-b169-53ec421f9eda-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I18248834-8320-4342-967c-2007195ba14a-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I18248834-8320-4342-967c-2007195ba14a-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I0bf4350c-9288-4cec-9e37-1b0978b057ba-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I0bf4350c-9288-4cec-9e37-1b0978b057ba-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="Ifb1bfeea-20a0-4d29-bb87-d502ff355522-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ifb1bfeea-20a0-4d29-bb87-d502ff355522-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="Ie1f531cd-79c9-4b87-a90c-76766792b183-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ie1f531cd-79c9-4b87-a90c-76766792b183-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I66faad9a-d004-4cdc-a275-6f9ff736bb14-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I66faad9a-d004-4cdc-a275-6f9ff736bb14-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I665f2702-e926-4266-bc3c-3f04ea9a6c62-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I665f2702-e926-4266-bc3c-3f04ea9a6c62-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I7e8b004a-0223-432a-bb9a-536918190017-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I7e8b004a-0223-432a-bb9a-536918190017-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I217873b0-4970-405f-a80b-b2a9f42bbd85-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I217873b0-4970-405f-a80b-b2a9f42bbd85-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I46521597-f1f4-432c-96c2-8e3defb05950-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I46521597-f1f4-432c-96c2-8e3defb05950-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="Ifbea86b5-20bc-4dde-a4e4-4a14153c8ead-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ifbea86b5-20bc-4dde-a4e4-4a14153c8ead-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I279d3c47-95a8-42d1-9440-26fadf909090-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I279d3c47-95a8-42d1-9440-26fadf909090-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I6d80a45f-5042-4f42-99fe-151229268a43-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I6d80a45f-5042-4f42-99fe-151229268a43-hottextController" occur="1">
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
                <conceptFact id="Iba0772fe-878f-457c-b652-a0e922626082[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Iba0772fe-878f-457c-b652-a0e922626082-hottextController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I5493e55f-fae9-499f-bd3e-16df52c028e1[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I5493e55f-fae9-499f-bd3e-16df52c028e1-hottextController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I7134c778-4641-40b5-8eae-1a80013cf777[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I7134c778-4641-40b5-8eae-1a80013cf777-hottextController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I3cb9edb6-551e-474a-9122-338b8c6a396d[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I3cb9edb6-551e-474a-9122-338b8c6a396d-hottextController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I41d5e633-d892-46fe-8a0c-ceba54c167f3[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I41d5e633-d892-46fe-8a0c-ceba54c167f3-hottextController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I498a96cd-114d-4299-b27c-639e41559e29[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I498a96cd-114d-4299-b27c-639e41559e29-hottextController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I19b64b54-f7f4-4284-af37-37999bd24935[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I19b64b54-f7f4-4284-af37-37999bd24935-hottextController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I7ea195a7-b0ec-465e-ada3-31c9e940f0dc[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I7ea195a7-b0ec-465e-ada3-31c9e940f0dc-hottextController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="Ibf683ce7-ac85-4610-b057-dd0578f3b71f[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ibf683ce7-ac85-4610-b057-dd0578f3b71f-hottextController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="Ie6f1fe1f-fe23-4f29-85ec-27d483138cfa[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ie6f1fe1f-fe23-4f29-85ec-27d483138cfa-hottextController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I2b4ed71d-8984-4ffd-a95a-126efbb12e48[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I2b4ed71d-8984-4ffd-a95a-126efbb12e48-hottextController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I0778d3a8-11c3-41f5-aeb7-4efd94338296[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I0778d3a8-11c3-41f5-aeb7-4efd94338296-hottextController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I3a1bf3a7-f9b4-472d-ac19-c6d27f671543[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I3a1bf3a7-f9b4-472d-ac19-c6d27f671543-hottextController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I01f17f48-6da9-44a0-bb7f-82e325f2f233[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I01f17f48-6da9-44a0-bb7f-82e325f2f233-hottextController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="Ifdbe8e9c-0eeb-4019-b8a7-5599265834fb[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ifdbe8e9c-0eeb-4019-b8a7-5599265834fb-hottextController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I0dd2d1e9-84b7-4b3f-a144-a2ec2c13061b[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I0dd2d1e9-84b7-4b3f-a144-a2ec2c13061b-hottextController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I41e6c74d-787a-4620-889f-fae84be71cdb[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I41e6c74d-787a-4620-889f-fae84be71cdb-hottextController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I1f639c54-57ee-428e-93ed-77d786d5a2d1[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I1f639c54-57ee-428e-93ed-77d786d5a2d1-hottextController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="Ib580dda7-0717-4cfc-b70e-34059b4c78f1[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ib580dda7-0717-4cfc-b70e-34059b4c78f1-hottextController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I834d38a6-dac5-4235-9cf4-d323dd89f1d2[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I834d38a6-dac5-4235-9cf4-d323dd89f1d2-hottextController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="Ic858ebcf-6f25-46ee-be0b-afbee1f41971[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ic858ebcf-6f25-46ee-be0b-afbee1f41971-hottextController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="If6a1b756-b293-4baf-a3c0-3332221e3ea8[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="If6a1b756-b293-4baf-a3c0-3332221e3ea8-hottextController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I80cb35ef-89d7-4ada-aef4-eda2b963a606[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I80cb35ef-89d7-4ada-aef4-eda2b963a606-hottextController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="Ia2c49323-f160-4dfa-8f79-5abea876efa6[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ia2c49323-f160-4dfa-8f79-5abea876efa6-hottextController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I8ecdc331-d565-4fcc-9f1f-a83e6e6bab9d[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I8ecdc331-d565-4fcc-9f1f-a83e6e6bab9d-hottextController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I4142aba3-f108-41dc-b537-ab44fd8cb5ae[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I4142aba3-f108-41dc-b537-ab44fd8cb5ae-hottextController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="If9dc68d4-33ec-4176-adb2-eda10373f1a0[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="If9dc68d4-33ec-4176-adb2-eda10373f1a0-hottextController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="Ice8f6ba6-0b24-4234-8e1c-ea2d6caf18f6[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ice8f6ba6-0b24-4234-8e1c-ea2d6caf18f6-hottextController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="Ia64d8e75-aa61-481b-bd40-df7270053448[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ia64d8e75-aa61-481b-bd40-df7270053448-hottextController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I2a787ac6-adce-419f-b230-293302ab6e2b[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I2a787ac6-adce-419f-b230-293302ab6e2b-hottextController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I562dc41e-1fcd-45b2-ad63-e346a9b12ed3[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I562dc41e-1fcd-45b2-ad63-e346a9b12ed3-hottextController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I2ff56de9-53fd-4be6-adc3-b9d48cbac0bd[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I2ff56de9-53fd-4be6-adc3-b9d48cbac0bd-hottextController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I8c49dcaf-eea6-48ad-bcf0-5c00ccde0415[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I8c49dcaf-eea6-48ad-bcf0-5c00ccde0415-hottextController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I7b431d95-a30e-403f-9b35-7575b30d31a3[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I7b431d95-a30e-403f-9b35-7575b30d31a3-hottextController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I46f0651d-a9f2-4b96-8172-8104b0938213[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I46f0651d-a9f2-4b96-8172-8104b0938213-hottextController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I1e40e819-c8a2-42dd-89de-dd29e070b99b[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I1e40e819-c8a2-42dd-89de-dd29e070b99b-hottextController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I67b6042e-7444-46be-b169-53ec421f9eda[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I67b6042e-7444-46be-b169-53ec421f9eda-hottextController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I18248834-8320-4342-967c-2007195ba14a[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I18248834-8320-4342-967c-2007195ba14a-hottextController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I0bf4350c-9288-4cec-9e37-1b0978b057ba[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I0bf4350c-9288-4cec-9e37-1b0978b057ba-hottextController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="Ifb1bfeea-20a0-4d29-bb87-d502ff355522[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ifb1bfeea-20a0-4d29-bb87-d502ff355522-hottextController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="Ie1f531cd-79c9-4b87-a90c-76766792b183[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ie1f531cd-79c9-4b87-a90c-76766792b183-hottextController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I66faad9a-d004-4cdc-a275-6f9ff736bb14[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I66faad9a-d004-4cdc-a275-6f9ff736bb14-hottextController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I665f2702-e926-4266-bc3c-3f04ea9a6c62[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I665f2702-e926-4266-bc3c-3f04ea9a6c62-hottextController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I7e8b004a-0223-432a-bb9a-536918190017[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I7e8b004a-0223-432a-bb9a-536918190017-hottextController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I217873b0-4970-405f-a80b-b2a9f42bbd85[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I217873b0-4970-405f-a80b-b2a9f42bbd85-hottextController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I46521597-f1f4-432c-96c2-8e3defb05950[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I46521597-f1f4-432c-96c2-8e3defb05950-hottextController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="Ifbea86b5-20bc-4dde-a4e4-4a14153c8ead[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ifbea86b5-20bc-4dde-a4e4-4a14153c8ead-hottextController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I279d3c47-95a8-42d1-9440-26fadf909090[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I279d3c47-95a8-42d1-9440-26fadf909090-hottextController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I6d80a45f-5042-4f42-99fe-151229268a43[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I6d80a45f-5042-4f42-99fe-151229268a43-hottextController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="1" code="Concept1"/>
                    <concept value="0" code="Concept1.2"/>
                    <concept value="0" code="Concept1.1"/>
                    <concept value="1" code="Concept2"/>
                    <concept value="0" code="Concept2.1"/>
                </concepts>
            </conceptFactSet>
            <conceptFactSet>
                <conceptFact id="Iba0772fe-878f-457c-b652-a0e922626082[2]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Iba0772fe-878f-457c-b652-a0e922626082-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I5493e55f-fae9-499f-bd3e-16df52c028e1[2]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I5493e55f-fae9-499f-bd3e-16df52c028e1-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I7134c778-4641-40b5-8eae-1a80013cf777[2]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I7134c778-4641-40b5-8eae-1a80013cf777-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I3cb9edb6-551e-474a-9122-338b8c6a396d[2]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I3cb9edb6-551e-474a-9122-338b8c6a396d-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I41d5e633-d892-46fe-8a0c-ceba54c167f3[2]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I41d5e633-d892-46fe-8a0c-ceba54c167f3-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I498a96cd-114d-4299-b27c-639e41559e29[2]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I498a96cd-114d-4299-b27c-639e41559e29-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I19b64b54-f7f4-4284-af37-37999bd24935[2]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I19b64b54-f7f4-4284-af37-37999bd24935-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I7ea195a7-b0ec-465e-ada3-31c9e940f0dc[2]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I7ea195a7-b0ec-465e-ada3-31c9e940f0dc-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="Ibf683ce7-ac85-4610-b057-dd0578f3b71f[2]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ibf683ce7-ac85-4610-b057-dd0578f3b71f-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="Ie6f1fe1f-fe23-4f29-85ec-27d483138cfa[2]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ie6f1fe1f-fe23-4f29-85ec-27d483138cfa-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I2b4ed71d-8984-4ffd-a95a-126efbb12e48[2]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I2b4ed71d-8984-4ffd-a95a-126efbb12e48-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I0778d3a8-11c3-41f5-aeb7-4efd94338296[2]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I0778d3a8-11c3-41f5-aeb7-4efd94338296-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I3a1bf3a7-f9b4-472d-ac19-c6d27f671543[2]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I3a1bf3a7-f9b4-472d-ac19-c6d27f671543-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I01f17f48-6da9-44a0-bb7f-82e325f2f233[2]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I01f17f48-6da9-44a0-bb7f-82e325f2f233-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="Ifdbe8e9c-0eeb-4019-b8a7-5599265834fb[2]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ifdbe8e9c-0eeb-4019-b8a7-5599265834fb-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I0dd2d1e9-84b7-4b3f-a144-a2ec2c13061b[2]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I0dd2d1e9-84b7-4b3f-a144-a2ec2c13061b-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I41e6c74d-787a-4620-889f-fae84be71cdb[2]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I41e6c74d-787a-4620-889f-fae84be71cdb-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I1f639c54-57ee-428e-93ed-77d786d5a2d1[2]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I1f639c54-57ee-428e-93ed-77d786d5a2d1-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="Ib580dda7-0717-4cfc-b70e-34059b4c78f1[2]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ib580dda7-0717-4cfc-b70e-34059b4c78f1-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I834d38a6-dac5-4235-9cf4-d323dd89f1d2[2]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I834d38a6-dac5-4235-9cf4-d323dd89f1d2-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="Ic858ebcf-6f25-46ee-be0b-afbee1f41971[2]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ic858ebcf-6f25-46ee-be0b-afbee1f41971-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="If6a1b756-b293-4baf-a3c0-3332221e3ea8[2]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="If6a1b756-b293-4baf-a3c0-3332221e3ea8-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I80cb35ef-89d7-4ada-aef4-eda2b963a606[2]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I80cb35ef-89d7-4ada-aef4-eda2b963a606-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="Ia2c49323-f160-4dfa-8f79-5abea876efa6[2]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ia2c49323-f160-4dfa-8f79-5abea876efa6-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I8ecdc331-d565-4fcc-9f1f-a83e6e6bab9d[2]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I8ecdc331-d565-4fcc-9f1f-a83e6e6bab9d-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I4142aba3-f108-41dc-b537-ab44fd8cb5ae[2]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I4142aba3-f108-41dc-b537-ab44fd8cb5ae-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="If9dc68d4-33ec-4176-adb2-eda10373f1a0[2]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="If9dc68d4-33ec-4176-adb2-eda10373f1a0-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="Ice8f6ba6-0b24-4234-8e1c-ea2d6caf18f6[2]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ice8f6ba6-0b24-4234-8e1c-ea2d6caf18f6-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="Ia64d8e75-aa61-481b-bd40-df7270053448[2]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ia64d8e75-aa61-481b-bd40-df7270053448-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I2a787ac6-adce-419f-b230-293302ab6e2b[2]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I2a787ac6-adce-419f-b230-293302ab6e2b-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I562dc41e-1fcd-45b2-ad63-e346a9b12ed3[2]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I562dc41e-1fcd-45b2-ad63-e346a9b12ed3-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I2ff56de9-53fd-4be6-adc3-b9d48cbac0bd[2]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I2ff56de9-53fd-4be6-adc3-b9d48cbac0bd-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I8c49dcaf-eea6-48ad-bcf0-5c00ccde0415[2]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I8c49dcaf-eea6-48ad-bcf0-5c00ccde0415-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I7b431d95-a30e-403f-9b35-7575b30d31a3[2]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I7b431d95-a30e-403f-9b35-7575b30d31a3-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I46f0651d-a9f2-4b96-8172-8104b0938213[2]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I46f0651d-a9f2-4b96-8172-8104b0938213-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I1e40e819-c8a2-42dd-89de-dd29e070b99b[2]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I1e40e819-c8a2-42dd-89de-dd29e070b99b-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I67b6042e-7444-46be-b169-53ec421f9eda[2]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I67b6042e-7444-46be-b169-53ec421f9eda-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I18248834-8320-4342-967c-2007195ba14a[2]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I18248834-8320-4342-967c-2007195ba14a-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I0bf4350c-9288-4cec-9e37-1b0978b057ba[2]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I0bf4350c-9288-4cec-9e37-1b0978b057ba-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="Ifb1bfeea-20a0-4d29-bb87-d502ff355522[2]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ifb1bfeea-20a0-4d29-bb87-d502ff355522-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="Ie1f531cd-79c9-4b87-a90c-76766792b183[2]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ie1f531cd-79c9-4b87-a90c-76766792b183-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I66faad9a-d004-4cdc-a275-6f9ff736bb14[2]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I66faad9a-d004-4cdc-a275-6f9ff736bb14-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I665f2702-e926-4266-bc3c-3f04ea9a6c62[2]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I665f2702-e926-4266-bc3c-3f04ea9a6c62-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I7e8b004a-0223-432a-bb9a-536918190017[2]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I7e8b004a-0223-432a-bb9a-536918190017-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I217873b0-4970-405f-a80b-b2a9f42bbd85[2]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I217873b0-4970-405f-a80b-b2a9f42bbd85-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I46521597-f1f4-432c-96c2-8e3defb05950[2]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I46521597-f1f4-432c-96c2-8e3defb05950-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="Ifbea86b5-20bc-4dde-a4e4-4a14153c8ead[2]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ifbea86b5-20bc-4dde-a4e4-4a14153c8ead-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I279d3c47-95a8-42d1-9440-26fadf909090[2]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I279d3c47-95a8-42d1-9440-26fadf909090-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I6d80a45f-5042-4f42-99fe-151229268a43[2]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I6d80a45f-5042-4f42-99fe-151229268a43-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="0" code="Concept1"/>
                    <concept value="0" code="Concept2"/>
                    <concept value="0" code="Concept2.1"/>
                    <concept value="1" code="Concept1.1"/>
                    <concept value="1" code="Concept1.2"/>
                </concepts>
            </conceptFactSet>
        </conceptFinding>

        ReadOnly _finding32 As XElement =
        <conceptFinding id="hottextController" scoringMethod="None">
            <conceptFact id="Iba0772fe-878f-457c-b652-a0e922626082-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="Iba0772fe-878f-457c-b652-a0e922626082-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="I41e6c74d-787a-4620-889f-fae84be71cdb-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I41e6c74d-787a-4620-889f-fae84be71cdb-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="I0dd2d1e9-84b7-4b3f-a144-a2ec2c13061b-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I0dd2d1e9-84b7-4b3f-a144-a2ec2c13061b-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="Ifdbe8e9c-0eeb-4019-b8a7-5599265834fb-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="Ifdbe8e9c-0eeb-4019-b8a7-5599265834fb-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="I01f17f48-6da9-44a0-bb7f-82e325f2f233-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I01f17f48-6da9-44a0-bb7f-82e325f2f233-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="I3a1bf3a7-f9b4-472d-ac19-c6d27f671543-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I3a1bf3a7-f9b4-472d-ac19-c6d27f671543-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="I0778d3a8-11c3-41f5-aeb7-4efd94338296-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I0778d3a8-11c3-41f5-aeb7-4efd94338296-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="I2b4ed71d-8984-4ffd-a95a-126efbb12e48-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I2b4ed71d-8984-4ffd-a95a-126efbb12e48-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="Ie6f1fe1f-fe23-4f29-85ec-27d483138cfa-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="Ie6f1fe1f-fe23-4f29-85ec-27d483138cfa-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="Ibf683ce7-ac85-4610-b057-dd0578f3b71f-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="Ibf683ce7-ac85-4610-b057-dd0578f3b71f-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="I7ea195a7-b0ec-465e-ada3-31c9e940f0dc-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I7ea195a7-b0ec-465e-ada3-31c9e940f0dc-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="I19b64b54-f7f4-4284-af37-37999bd24935-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I19b64b54-f7f4-4284-af37-37999bd24935-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="I498a96cd-114d-4299-b27c-639e41559e29-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I498a96cd-114d-4299-b27c-639e41559e29-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="I41d5e633-d892-46fe-8a0c-ceba54c167f3-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I41d5e633-d892-46fe-8a0c-ceba54c167f3-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="I3cb9edb6-551e-474a-9122-338b8c6a396d-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I3cb9edb6-551e-474a-9122-338b8c6a396d-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>true</typedValue>
                    </booleanValue>
                </conceptValue>
                <concepts>
                    <concept value="1" code="Concept1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="I7134c778-4641-40b5-8eae-1a80013cf777-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I7134c778-4641-40b5-8eae-1a80013cf777-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="I5493e55f-fae9-499f-bd3e-16df52c028e1-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I5493e55f-fae9-499f-bd3e-16df52c028e1-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="I279d3c47-95a8-42d1-9440-26fadf909090-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I279d3c47-95a8-42d1-9440-26fadf909090-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="Ifbea86b5-20bc-4dde-a4e4-4a14153c8ead-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="Ifbea86b5-20bc-4dde-a4e4-4a14153c8ead-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="I46521597-f1f4-432c-96c2-8e3defb05950-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I46521597-f1f4-432c-96c2-8e3defb05950-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>true</typedValue>
                    </booleanValue>
                </conceptValue>
                <concepts>
                    <concept value="1" code="Concept1"/>
                    <concept value="1" code="Concept1.2"/>
                    <concept value="1" code="Concept1.1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="I217873b0-4970-405f-a80b-b2a9f42bbd85-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I217873b0-4970-405f-a80b-b2a9f42bbd85-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="I7e8b004a-0223-432a-bb9a-536918190017-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I7e8b004a-0223-432a-bb9a-536918190017-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="I665f2702-e926-4266-bc3c-3f04ea9a6c62-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I665f2702-e926-4266-bc3c-3f04ea9a6c62-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>true</typedValue>
                    </booleanValue>
                </conceptValue>
                <concepts>
                    <concept value="1" code="Concept1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="I66faad9a-d004-4cdc-a275-6f9ff736bb14-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I66faad9a-d004-4cdc-a275-6f9ff736bb14-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="Ie1f531cd-79c9-4b87-a90c-76766792b183-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="Ie1f531cd-79c9-4b87-a90c-76766792b183-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="Ifb1bfeea-20a0-4d29-bb87-d502ff355522-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="Ifb1bfeea-20a0-4d29-bb87-d502ff355522-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="I0bf4350c-9288-4cec-9e37-1b0978b057ba-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I0bf4350c-9288-4cec-9e37-1b0978b057ba-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>true</typedValue>
                    </booleanValue>
                </conceptValue>
                <concepts>
                    <concept value="2" code="Concept2"/>
                </concepts>
            </conceptFact>
            <conceptFact id="I18248834-8320-4342-967c-2007195ba14a-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I18248834-8320-4342-967c-2007195ba14a-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="I67b6042e-7444-46be-b169-53ec421f9eda-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I67b6042e-7444-46be-b169-53ec421f9eda-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="I1e40e819-c8a2-42dd-89de-dd29e070b99b-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I1e40e819-c8a2-42dd-89de-dd29e070b99b-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="I1f639c54-57ee-428e-93ed-77d786d5a2d1-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I1f639c54-57ee-428e-93ed-77d786d5a2d1-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>true</typedValue>
                    </booleanValue>
                </conceptValue>
                <concepts>
                    <concept value="1" code="Concept1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="I6d80a45f-5042-4f42-99fe-151229268a43-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I6d80a45f-5042-4f42-99fe-151229268a43-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="Iba0772fe-878f-457c-b652-a0e922626082[1]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="Iba0772fe-878f-457c-b652-a0e922626082[1]-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>true</typedValue>
                    </booleanValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="I3cb9edb6-551e-474a-9122-338b8c6a396d[1]-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I3cb9edb6-551e-474a-9122-338b8c6a396d[1]-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
                <concepts>
                    <concept value="0" code="Concept1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="I1f639c54-57ee-428e-93ed-77d786d5a2d1[1]-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I1f639c54-57ee-428e-93ed-77d786d5a2d1[1]-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
                <concepts>
                    <concept value="0" code="Concept1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="I0bf4350c-9288-4cec-9e37-1b0978b057ba[1]-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I0bf4350c-9288-4cec-9e37-1b0978b057ba[1]-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
                <concepts>
                    <concept value="0" code="Concept2"/>
                </concepts>
            </conceptFact>
            <conceptFact id="I665f2702-e926-4266-bc3c-3f04ea9a6c62[1]-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I665f2702-e926-4266-bc3c-3f04ea9a6c62[1]-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
                <concepts>
                    <concept value="0" code="Concept1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="I46521597-f1f4-432c-96c2-8e3defb05950[1]-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="I46521597-f1f4-432c-96c2-8e3defb05950[1]-hottextController" occur="1">
                    <booleanValue>
                        <typedValue>false</typedValue>
                    </booleanValue>
                </conceptValue>
                <concepts>
                    <concept value="0" code="Concept1"/>
                    <concept value="0" code="Concept1.2"/>
                    <concept value="0" code="Concept1.1"/>
                </concepts>
            </conceptFact>
            <conceptFactSet>
                <conceptFact id="Ib580dda7-0717-4cfc-b70e-34059b4c78f1-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ib580dda7-0717-4cfc-b70e-34059b4c78f1-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I834d38a6-dac5-4235-9cf4-d323dd89f1d2-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I834d38a6-dac5-4235-9cf4-d323dd89f1d2-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="Ic858ebcf-6f25-46ee-be0b-afbee1f41971-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ic858ebcf-6f25-46ee-be0b-afbee1f41971-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="If6a1b756-b293-4baf-a3c0-3332221e3ea8-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="If6a1b756-b293-4baf-a3c0-3332221e3ea8-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I80cb35ef-89d7-4ada-aef4-eda2b963a606-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I80cb35ef-89d7-4ada-aef4-eda2b963a606-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="Ia2c49323-f160-4dfa-8f79-5abea876efa6-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ia2c49323-f160-4dfa-8f79-5abea876efa6-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I8ecdc331-d565-4fcc-9f1f-a83e6e6bab9d-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I8ecdc331-d565-4fcc-9f1f-a83e6e6bab9d-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I4142aba3-f108-41dc-b537-ab44fd8cb5ae-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I4142aba3-f108-41dc-b537-ab44fd8cb5ae-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="If9dc68d4-33ec-4176-adb2-eda10373f1a0-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="If9dc68d4-33ec-4176-adb2-eda10373f1a0-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="Ice8f6ba6-0b24-4234-8e1c-ea2d6caf18f6-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ice8f6ba6-0b24-4234-8e1c-ea2d6caf18f6-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="Ia64d8e75-aa61-481b-bd40-df7270053448-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ia64d8e75-aa61-481b-bd40-df7270053448-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I2a787ac6-adce-419f-b230-293302ab6e2b-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I2a787ac6-adce-419f-b230-293302ab6e2b-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I562dc41e-1fcd-45b2-ad63-e346a9b12ed3-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I562dc41e-1fcd-45b2-ad63-e346a9b12ed3-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I2ff56de9-53fd-4be6-adc3-b9d48cbac0bd-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I2ff56de9-53fd-4be6-adc3-b9d48cbac0bd-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I8c49dcaf-eea6-48ad-bcf0-5c00ccde0415-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I8c49dcaf-eea6-48ad-bcf0-5c00ccde0415-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I7b431d95-a30e-403f-9b35-7575b30d31a3-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I7b431d95-a30e-403f-9b35-7575b30d31a3-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I46f0651d-a9f2-4b96-8172-8104b0938213-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I46f0651d-a9f2-4b96-8172-8104b0938213-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="0" code="Concept1"/>
                    <concept value="3" code="Concept2"/>
                </concepts>
            </conceptFactSet>
            <conceptFactSet>
                <conceptFact id="Ib580dda7-0717-4cfc-b70e-34059b4c78f1-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ib580dda7-0717-4cfc-b70e-34059b4c78f1-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I834d38a6-dac5-4235-9cf4-d323dd89f1d2-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I834d38a6-dac5-4235-9cf4-d323dd89f1d2-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="Ic858ebcf-6f25-46ee-be0b-afbee1f41971-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ic858ebcf-6f25-46ee-be0b-afbee1f41971-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="If6a1b756-b293-4baf-a3c0-3332221e3ea8-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="If6a1b756-b293-4baf-a3c0-3332221e3ea8-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I80cb35ef-89d7-4ada-aef4-eda2b963a606-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I80cb35ef-89d7-4ada-aef4-eda2b963a606-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="Ia2c49323-f160-4dfa-8f79-5abea876efa6-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ia2c49323-f160-4dfa-8f79-5abea876efa6-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I8ecdc331-d565-4fcc-9f1f-a83e6e6bab9d-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I8ecdc331-d565-4fcc-9f1f-a83e6e6bab9d-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I4142aba3-f108-41dc-b537-ab44fd8cb5ae-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I4142aba3-f108-41dc-b537-ab44fd8cb5ae-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="If9dc68d4-33ec-4176-adb2-eda10373f1a0-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="If9dc68d4-33ec-4176-adb2-eda10373f1a0-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="Ice8f6ba6-0b24-4234-8e1c-ea2d6caf18f6-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ice8f6ba6-0b24-4234-8e1c-ea2d6caf18f6-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="Ia64d8e75-aa61-481b-bd40-df7270053448-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ia64d8e75-aa61-481b-bd40-df7270053448-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I2a787ac6-adce-419f-b230-293302ab6e2b-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I2a787ac6-adce-419f-b230-293302ab6e2b-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I562dc41e-1fcd-45b2-ad63-e346a9b12ed3-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I562dc41e-1fcd-45b2-ad63-e346a9b12ed3-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I2ff56de9-53fd-4be6-adc3-b9d48cbac0bd-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I2ff56de9-53fd-4be6-adc3-b9d48cbac0bd-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I8c49dcaf-eea6-48ad-bcf0-5c00ccde0415-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I8c49dcaf-eea6-48ad-bcf0-5c00ccde0415-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I7b431d95-a30e-403f-9b35-7575b30d31a3-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I7b431d95-a30e-403f-9b35-7575b30d31a3-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I46f0651d-a9f2-4b96-8172-8104b0938213-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I46f0651d-a9f2-4b96-8172-8104b0938213-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="0" code="Concept1"/>
                    <concept value="2" code="Concept2"/>
                </concepts>
            </conceptFactSet>
            <conceptFactSet>
                <conceptFact id="Ib580dda7-0717-4cfc-b70e-34059b4c78f1-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ib580dda7-0717-4cfc-b70e-34059b4c78f1-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I834d38a6-dac5-4235-9cf4-d323dd89f1d2-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I834d38a6-dac5-4235-9cf4-d323dd89f1d2-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="Ic858ebcf-6f25-46ee-be0b-afbee1f41971-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ic858ebcf-6f25-46ee-be0b-afbee1f41971-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="If6a1b756-b293-4baf-a3c0-3332221e3ea8-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="If6a1b756-b293-4baf-a3c0-3332221e3ea8-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I80cb35ef-89d7-4ada-aef4-eda2b963a606-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I80cb35ef-89d7-4ada-aef4-eda2b963a606-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="Ia2c49323-f160-4dfa-8f79-5abea876efa6-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ia2c49323-f160-4dfa-8f79-5abea876efa6-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I8ecdc331-d565-4fcc-9f1f-a83e6e6bab9d-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I8ecdc331-d565-4fcc-9f1f-a83e6e6bab9d-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I4142aba3-f108-41dc-b537-ab44fd8cb5ae-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I4142aba3-f108-41dc-b537-ab44fd8cb5ae-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="If9dc68d4-33ec-4176-adb2-eda10373f1a0-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="If9dc68d4-33ec-4176-adb2-eda10373f1a0-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="Ice8f6ba6-0b24-4234-8e1c-ea2d6caf18f6-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ice8f6ba6-0b24-4234-8e1c-ea2d6caf18f6-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="Ia64d8e75-aa61-481b-bd40-df7270053448-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ia64d8e75-aa61-481b-bd40-df7270053448-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I2a787ac6-adce-419f-b230-293302ab6e2b-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I2a787ac6-adce-419f-b230-293302ab6e2b-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>true</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I562dc41e-1fcd-45b2-ad63-e346a9b12ed3-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I562dc41e-1fcd-45b2-ad63-e346a9b12ed3-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I2ff56de9-53fd-4be6-adc3-b9d48cbac0bd-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I2ff56de9-53fd-4be6-adc3-b9d48cbac0bd-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I8c49dcaf-eea6-48ad-bcf0-5c00ccde0415-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I8c49dcaf-eea6-48ad-bcf0-5c00ccde0415-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I7b431d95-a30e-403f-9b35-7575b30d31a3-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I7b431d95-a30e-403f-9b35-7575b30d31a3-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I46f0651d-a9f2-4b96-8172-8104b0938213-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I46f0651d-a9f2-4b96-8172-8104b0938213-hottextController" occur="1">
                        <booleanValue>
                            <typedValue>false</typedValue>
                        </booleanValue>
                    </conceptValue>
                </conceptFact>
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="0" code="Concept1"/>
                    <concept value="1" code="Concept2"/>
                </concepts>
            </conceptFactSet>
            <conceptFactSet>
                <conceptFact id="Ib580dda7-0717-4cfc-b70e-34059b4c78f1[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ib580dda7-0717-4cfc-b70e-34059b4c78f1-hottextController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I834d38a6-dac5-4235-9cf4-d323dd89f1d2[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I834d38a6-dac5-4235-9cf4-d323dd89f1d2-hottextController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="Ic858ebcf-6f25-46ee-be0b-afbee1f41971[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ic858ebcf-6f25-46ee-be0b-afbee1f41971-hottextController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="If6a1b756-b293-4baf-a3c0-3332221e3ea8[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="If6a1b756-b293-4baf-a3c0-3332221e3ea8-hottextController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I80cb35ef-89d7-4ada-aef4-eda2b963a606[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I80cb35ef-89d7-4ada-aef4-eda2b963a606-hottextController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="Ia2c49323-f160-4dfa-8f79-5abea876efa6[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ia2c49323-f160-4dfa-8f79-5abea876efa6-hottextController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I8ecdc331-d565-4fcc-9f1f-a83e6e6bab9d[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I8ecdc331-d565-4fcc-9f1f-a83e6e6bab9d-hottextController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I4142aba3-f108-41dc-b537-ab44fd8cb5ae[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I4142aba3-f108-41dc-b537-ab44fd8cb5ae-hottextController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="If9dc68d4-33ec-4176-adb2-eda10373f1a0[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="If9dc68d4-33ec-4176-adb2-eda10373f1a0-hottextController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="Ice8f6ba6-0b24-4234-8e1c-ea2d6caf18f6[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ice8f6ba6-0b24-4234-8e1c-ea2d6caf18f6-hottextController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="Ia64d8e75-aa61-481b-bd40-df7270053448[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="Ia64d8e75-aa61-481b-bd40-df7270053448-hottextController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I2a787ac6-adce-419f-b230-293302ab6e2b[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I2a787ac6-adce-419f-b230-293302ab6e2b-hottextController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I562dc41e-1fcd-45b2-ad63-e346a9b12ed3[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I562dc41e-1fcd-45b2-ad63-e346a9b12ed3-hottextController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I2ff56de9-53fd-4be6-adc3-b9d48cbac0bd[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I2ff56de9-53fd-4be6-adc3-b9d48cbac0bd-hottextController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I8c49dcaf-eea6-48ad-bcf0-5c00ccde0415[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I8c49dcaf-eea6-48ad-bcf0-5c00ccde0415-hottextController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I7b431d95-a30e-403f-9b35-7575b30d31a3[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I7b431d95-a30e-403f-9b35-7575b30d31a3-hottextController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="I46f0651d-a9f2-4b96-8172-8104b0938213[*]-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="I46f0651d-a9f2-4b96-8172-8104b0938213-hottextController" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="0" code="Concept1"/>
                    <concept value="0" code="Concept2"/>
                </concepts>
            </conceptFactSet>
        </conceptFinding>

        ReadOnly _finding33 As XElement =
        <conceptFinding id="matrix" scoringMethod="None">
            <conceptFactSet>
                <conceptFact id="4-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="matrix4" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="3-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="matrix3" occur="1">
                        <stringValue>
                            <typedValue>A</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="2" code="Concept1.2"/>
                </concepts>
            </conceptFactSet>
            <conceptFactSet>
                <conceptFact id="3-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="matrix3" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="4-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="matrix4" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="1" code="Concept1.2"/>
                </concepts>
            </conceptFactSet>
            <conceptFactSet>
                <conceptFact id="1-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="matrix1" occur="1">
                        <stringValue>
                            <typedValue>A</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="2-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="matrix2" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="2" code="Concept1.1"/>
                </concepts>
            </conceptFactSet>
            <conceptFactSet>
                <conceptFact id="1-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="matrix1" occur="1">
                        <stringValue>
                            <typedValue>A</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="2-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="matrix2" occur="1">
                        <stringValue>
                            <typedValue>A</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="1" code="Concept1.1"/>
                </concepts>
            </conceptFactSet>
            <conceptFactSet>
                <conceptFact id="1[*]-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="matrix1" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="2[*]-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="matrix2" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="0" code="Concept1.1"/>
                </concepts>
            </conceptFactSet>
            <conceptFactSet>
                <conceptFact id="3[*]-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="matrix3" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="4[*]-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="matrix4" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="0" code="Concept1.2"/>
                </concepts>
            </conceptFactSet>
        </conceptFinding>

        ReadOnly _finding34 As XElement =
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
            <conceptFact id="H[*]-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="H[*]-gapMatchController" occur="1">
                    <catchAllValue/>
                </conceptValue>
                <concepts>
                    <concept value="0" code="2 Tekstopbouw"/>
                    <concept value="0" code="2.1 Hoofd- en bijzaken onderscheiden"/>
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
            <conceptFact id="A[*]-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="A[*]-gapMatchController" occur="1">
                    <catchAllValue/>
                </conceptValue>
                <concepts>
                    <concept value="1" code="3 Tekststructuur"/>
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

        ReadOnly _finding35 As XElement =
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
                        <typedValue>zeven</typedValue>
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

        ReadOnly _finding36 As XElement =
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
                            <typedValue>zeven</typedValue>
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
                            <typedValue>zeven</typedValue>
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

        ReadOnly _finding37 As XElement =
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
                        <typedValue>zeven</typedValue>
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

        ReadOnly _finding38 As XElement =
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
                            <typedValue>zeven</typedValue>
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
                            <typedValue>zeven</typedValue>
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
                            <typedValue>zeven</typedValue>
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

        ReadOnly _finding39 As XElement =
        <conceptFinding id="hottextController" scoringMethod="None">
            <conceptFact id="1-Input_Iff799b19-6c0e-406a-ad0d-63f470eac66f" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="Input_Iff799b19-6c0e-406a-ad0d-63f470eac66f" occur="1">
                    <stringValue>
                        <typedValue>drie</typedValue>
                    </stringValue>
                </conceptValue>
                <concepts>
                    <concept value="0" code="Concept1"/>
                    <concept value="0" code="Concept1.1"/>
                    <concept value="0" code="Concept1.2"/>
                </concepts>
            </conceptFact>
            <conceptFact id="1[*]-Input_Iff799b19-6c0e-406a-ad0d-63f470eac66f" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="Input_Iff799b19-6c0e-406a-ad0d-63f470eac66f" occur="1">
                    <catchAllValue/>
                </conceptValue>
                <concepts>
                    <concept value="0" code="Concept1"/>
                    <concept value="1" code="Concept1.1"/>
                    <concept value="0" code="Concept1.2"/>
                </concepts>
            </conceptFact>
        </conceptFinding>

        ReadOnly _finding40 As XElement =
        <conceptFinding id="hottextController" scoringMethod="None">
            <conceptFact id="1-Input_Iff799b19-6c0e-406a-ad0d-63f470eac66f" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="Input_Iff799b19-6c0e-406a-ad0d-63f470eac66f" occur="1">
                    <stringValue>
                        <typedValue>drie</typedValue>
                    </stringValue>
                </conceptValue>
                <concepts>
                    <concept value="0" code="Concept1"/>
                    <concept value="0" code="Concept1.1"/>
                    <concept value="0" code="Concept1.2"/>
                </concepts>
            </conceptFact>
            <conceptFact id="1[*]-Input_Iff799b19-6c0e-406a-ad0d-63f470eac66f" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="Input_Iff799b19-6c0e-406a-ad0d-63f470eac66f" occur="1">
                    <catchAllValue/>
                </conceptValue>
                <concepts>
                    <concept value="0" code="Concept1"/>
                    <concept value="1" code="Concept1.1"/>
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
                    <concept value="0" code="Concept1.2"/>
                    <concept value="1" code="Concept1.1"/>
                </concepts>
            </conceptFact>
        </conceptFinding>

        ReadOnly _finding41 As XElement =
        <conceptFinding id="hottextController" scoringMethod="None">
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
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="0" code="Concept1"/>
                    <concept value="0" code="Concept1.2"/>
                    <concept value="1" code="Concept1.1"/>
                    <concept value="0" code="Concept2"/>
                    <concept value="1" code="Concept2.1"/>
                </concepts>
            </conceptFactSet>
        </conceptFinding>

        ReadOnly _finding42 As XElement =
        <conceptFinding id="hottextController" scoringMethod="None">
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
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="0" code="Concept1"/>
                    <concept value="0" code="Concept1.2"/>
                    <concept value="0" code="Concept1.1"/>
                    <concept value="0" code="Concept2"/>
                    <concept value="0" code="Concept2.1"/>
                </concepts>
            </conceptFactSet>
            <conceptFactSet>
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
                    <concept value="0" code="Concept1"/>
                    <concept value="0" code="Concept1.2"/>
                    <concept value="0" code="Concept1.1"/>
                    <concept value="0" code="Concept2"/>
                    <concept value="1" code="Concept2.1"/>
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
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="0" code="Concept1"/>
                    <concept value="0" code="Concept1.2"/>
                    <concept value="1" code="Concept1.1"/>
                    <concept value="0" code="Concept2"/>
                    <concept value="1" code="Concept2.1"/>
                </concepts>
            </conceptFactSet>
        </conceptFinding>

        ReadOnly _finding43 As XElement =
        <conceptFinding id="hottextController" scoringMethod="None">
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
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="0" code="Concept1"/>
                    <concept value="0" code="Concept1.2"/>
                    <concept value="0" code="Concept1.1"/>
                    <concept value="0" code="Concept2"/>
                    <concept value="0" code="Concept2.1"/>
                </concepts>
            </conceptFactSet>
            <conceptFactSet>
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
                    <concept value="0" code="Concept1"/>
                    <concept value="0" code="Concept1.2"/>
                    <concept value="0" code="Concept1.1"/>
                    <concept value="0" code="Concept2"/>
                    <concept value="1" code="Concept2.1"/>
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
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="0" code="Concept1"/>
                    <concept value="0" code="Concept1.2"/>
                    <concept value="0" code="Concept1.1"/>
                    <concept value="0" code="Concept2"/>
                    <concept value="0" code="Concept2.1"/>
                </concepts>
            </conceptFactSet>
        </conceptFinding>

        ReadOnly _finding44 As XElement =
        <conceptFinding id="CustomInteractions" scoringMethod="None">
            <conceptFact id="1-CI_SP0" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="CI_SP0" occur="1">
                    <integerValue>
                        <typedValue>12345</typedValue>
                    </integerValue>
                </conceptValue>
                <concepts>
                    <concept value="0" code="Concept1"/>
                    <concept value="2" code="Concept1.2"/>
                    <concept value="2" code="Concept1.1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="D-CI_SP1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="CI_SP1" occur="1">
                    <stringValue>
                        <typedValue>D</typedValue>
                    </stringValue>
                </conceptValue>
                <concepts>
                    <concept value="0" code="Concept2"/>
                    <concept value="2" code="Concept2.1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="B-CI_SP2" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="CI_SP2" occur="1">
                    <stringValue>
                        <typedValue>B</typedValue>
                    </stringValue>
                </conceptValue>
                <concepts>
                    <concept value="0" code="Concept2"/>
                    <concept value="3" code="Concept2.1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="F-CI_SP3" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="CI_SP3" occur="1">
                    <stringValue>
                        <typedValue>F</typedValue>
                    </stringValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="1[*]-CI_SP0" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="CI_SP0" occur="1">
                    <catchAllValue/>
                </conceptValue>
                <concepts>
                    <concept value="0" code="Concept1"/>
                    <concept value="1" code="Concept1.2"/>
                    <concept value="0" code="Concept1.1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="A[1]-CI_SP1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="CI_SP1" occur="1">
                    <stringValue>
                        <typedValue>A</typedValue>
                    </stringValue>
                </conceptValue>
                <concepts>
                    <concept value="0" code="Concept2"/>
                    <concept value="0" code="Concept2.1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="B[1]-CI_SP1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="CI_SP1" occur="1">
                    <stringValue>
                        <typedValue>B</typedValue>
                    </stringValue>
                </conceptValue>
                <concepts>
                    <concept value="0" code="Concept2"/>
                    <concept value="1" code="Concept2.1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="C[1]-CI_SP1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="CI_SP1" occur="1">
                    <stringValue>
                        <typedValue>C</typedValue>
                    </stringValue>
                </conceptValue>
                <concepts>
                    <concept value="0" code="Concept2"/>
                    <concept value="0" code="Concept2.1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="E[1]-CI_SP1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="CI_SP1" occur="1">
                    <stringValue>
                        <typedValue>E</typedValue>
                    </stringValue>
                </conceptValue>
                <concepts>
                    <concept value="0" code="Concept2"/>
                    <concept value="1" code="Concept2.1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="A[1]-CI_SP2" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="CI_SP2" occur="1">
                    <stringValue>
                        <typedValue>A</typedValue>
                    </stringValue>
                </conceptValue>
                <concepts>
                    <concept value="0" code="Concept2"/>
                    <concept value="2" code="Concept2.1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="C[1]-CI_SP2" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="CI_SP2" occur="1">
                    <stringValue>
                        <typedValue>C</typedValue>
                    </stringValue>
                </conceptValue>
                <concepts>
                    <concept value="0" code="Concept2"/>
                    <concept value="1" code="Concept2.1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="1[1]-CI_SP0" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="CI_SP0" occur="1">
                    <integerValue>
                        <typedValue>54321</typedValue>
                    </integerValue>
                </conceptValue>
                <concepts>
                    <concept value="0" code="Concept1"/>
                    <concept value="1" code="Concept1.2"/>
                    <concept value="1" code="Concept1.1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="A[1]-CI_SP3" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="CI_SP3" occur="1">
                    <stringValue>
                        <typedValue>A</typedValue>
                    </stringValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="B[1]-CI_SP3" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="CI_SP3" occur="1">
                    <stringValue>
                        <typedValue>B</typedValue>
                    </stringValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="C[1]-CI_SP3" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="CI_SP3" occur="1">
                    <stringValue>
                        <typedValue>C</typedValue>
                    </stringValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="D[1]-CI_SP3" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="CI_SP3" occur="1">
                    <stringValue>
                        <typedValue>D</typedValue>
                    </stringValue>
                </conceptValue>
            </conceptFact>
            <conceptFact id="E[1]-CI_SP3" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="CI_SP3" occur="1">
                    <stringValue>
                        <typedValue>E</typedValue>
                    </stringValue>
                </conceptValue>
            </conceptFact>
        </conceptFinding>

        ReadOnly _finding45 As XElement =
        <conceptFinding id="CustomInteractions" scoringMethod="None">
            <conceptFactSet>
                <conceptFact id="1-CI_SP0" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="CI_SP0" occur="1">
                        <integerValue>
                            <typedValue>12345</typedValue>
                        </integerValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="D-CI_SP1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="CI_SP1" occur="1">
                        <stringValue>
                            <typedValue>D</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="F-CI_SP3" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="CI_SP3" occur="1">
                        <stringValue>
                            <typedValue>F</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="B-CI_SP2" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="CI_SP2" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="2" code="Concept2"/>
                    <concept value="2" code="Concept2.1"/>
                </concepts>
            </conceptFactSet>
            <conceptFactSet>
                <conceptFact id="1-CI_SP0" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="CI_SP0" occur="1">
                        <integerValue>
                            <typedValue>54321</typedValue>
                        </integerValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="D-CI_SP1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="CI_SP1" occur="1">
                        <stringValue>
                            <typedValue>D</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="B-CI_SP2" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="CI_SP2" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="E-CI_SP3" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="CI_SP3" occur="1">
                        <stringValue>
                            <typedValue>E</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="2" code="Concept2"/>
                    <concept value="2" code="Concept2.1"/>
                </concepts>
            </conceptFactSet>
            <conceptFactSet>
                <conceptFact id="1[*]-CI_SP0" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="CI_SP0" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="A[*]-CI_SP1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="CI_SP1" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="B[*]-CI_SP1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="CI_SP1" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="C[*]-CI_SP1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="CI_SP1" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="D[*]-CI_SP1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="CI_SP1" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="E[*]-CI_SP1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="CI_SP1" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="A[*]-CI_SP2" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="CI_SP2" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="B[*]-CI_SP2" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="CI_SP2" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="C[*]-CI_SP2" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="CI_SP2" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="A[*]-CI_SP3" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="CI_SP3" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="B[*]-CI_SP3" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="CI_SP3" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="C[*]-CI_SP3" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="CI_SP3" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="D[*]-CI_SP3" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="CI_SP3" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="E[*]-CI_SP3" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="CI_SP3" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="F[*]-CI_SP3" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="CI_SP3" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="0" code="Concept2"/>
                    <concept value="0" code="Concept2.1"/>
                </concepts>
            </conceptFactSet>
            <conceptFactSet>
                <conceptFact id="1[2]-CI_SP0" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="CI_SP0" occur="1">
                        <integerValue>
                            <typedValue>43210</typedValue>
                        </integerValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="D[2]-CI_SP1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="CI_SP1" occur="1">
                        <stringValue>
                            <typedValue>D</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="B[2]-CI_SP2" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="CI_SP2" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="E[2]-CI_SP3" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="CI_SP3" occur="1">
                        <stringValue>
                            <typedValue>E</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="1" code="Concept2"/>
                    <concept value="1" code="Concept2.1"/>
                </concepts>
            </conceptFactSet>
        </conceptFinding>

        ReadOnly _finding46 As XElement =
        <conceptFinding id="IEF" scoringMethod="None">
            <conceptFact id="D-CI_SP1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="CI_SP1" occur="1">
                    <stringValue>
                        <typedValue>D</typedValue>
                    </stringValue>
                </conceptValue>
                <concepts>
                    <concept value="2" code="Concept2"/>
                    <concept value="2" code="Concept2.1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="F-CI_SP3" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="CI_SP3" occur="1">
                    <stringValue>
                        <typedValue>F</typedValue>
                    </stringValue>
                </conceptValue>
                <concepts>
                    <concept value="1" code="Concept2"/>
                </concepts>
            </conceptFact>
            <conceptFact id="A[1]-CI_SP1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="CI_SP1" occur="1">
                    <stringValue>
                        <typedValue>A</typedValue>
                    </stringValue>
                </conceptValue>
                <concepts>
                    <concept value="0" code="Concept2"/>
                    <concept value="0" code="Concept2.1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="B[1]-CI_SP1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="CI_SP1" occur="1">
                    <stringValue>
                        <typedValue>B</typedValue>
                    </stringValue>
                </conceptValue>
                <concepts>
                    <concept value="1" code="Concept2"/>
                    <concept value="0" code="Concept2.1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="C[1]-CI_SP1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="CI_SP1" occur="1">
                    <stringValue>
                        <typedValue>C</typedValue>
                    </stringValue>
                </conceptValue>
                <concepts>
                    <concept value="0" code="Concept2"/>
                    <concept value="0" code="Concept2.1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="E[1]-CI_SP1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="CI_SP1" occur="1">
                    <stringValue>
                        <typedValue>E</typedValue>
                    </stringValue>
                </conceptValue>
                <concepts>
                    <concept value="0" code="Concept2"/>
                    <concept value="0" code="Concept2.1"/>
                </concepts>
            </conceptFact>
            <conceptFact id="A[1]-CI_SP3" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="CI_SP3" occur="1">
                    <stringValue>
                        <typedValue>A</typedValue>
                    </stringValue>
                </conceptValue>
                <concepts>
                    <concept value="0" code="Concept2"/>
                </concepts>
            </conceptFact>
            <conceptFact id="B[1]-CI_SP3" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="CI_SP3" occur="1">
                    <stringValue>
                        <typedValue>B</typedValue>
                    </stringValue>
                </conceptValue>
                <concepts>
                    <concept value="0" code="Concept2"/>
                </concepts>
            </conceptFact>
            <conceptFact id="C[1]-CI_SP3" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="CI_SP3" occur="1">
                    <stringValue>
                        <typedValue>C</typedValue>
                    </stringValue>
                </conceptValue>
                <concepts>
                    <concept value="0" code="Concept2"/>
                </concepts>
            </conceptFact>
            <conceptFact id="D[1]-CI_SP3" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="CI_SP3" occur="1">
                    <stringValue>
                        <typedValue>D</typedValue>
                    </stringValue>
                </conceptValue>
                <concepts>
                    <concept value="0" code="Concept2"/>
                </concepts>
            </conceptFact>
            <conceptFact id="E[1]-CI_SP3" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                <conceptValue domain="CI_SP3" occur="1">
                    <stringValue>
                        <typedValue>E</typedValue>
                    </stringValue>
                </conceptValue>
                <concepts>
                    <concept value="0" code="Concept2"/>
                </concepts>
            </conceptFact>
            <conceptFactSet>
                <conceptFact id="1-CI_SP0" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="CI_SP0" occur="1">
                        <integerValue>
                            <typedValue>12345</typedValue>
                        </integerValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="B-CI_SP2" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="CI_SP2" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="2" code="Concept1"/>
                    <concept value="2" code="Concept1.2"/>
                    <concept value="2" code="Concept1.1"/>
                    <concept value="0" code="Concept2"/>
                    <concept value="0" code="Concept2.1"/>
                </concepts>
            </conceptFactSet>
            <conceptFactSet>
                <conceptFact id="1[*]-CI_SP0" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="CI_SP0" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="A[*]-CI_SP2" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="CI_SP2" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="B[*]-CI_SP2" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="CI_SP2" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="C[*]-CI_SP2" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="CI_SP2" occur="1">
                        <catchAllValue/>
                    </conceptValue>
                </conceptFact>
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="0" code="Concept1"/>
                    <concept value="0" code="Concept1.2"/>
                    <concept value="0" code="Concept1.1"/>
                    <concept value="0" code="Concept2"/>
                    <concept value="1" code="Concept2.1"/>
                </concepts>
            </conceptFactSet>
            <conceptFactSet>
                <conceptFact id="1-CI_SP0" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="CI_SP0" occur="1">
                        <integerValue>
                            <typedValue>54321</typedValue>
                        </integerValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="B-CI_SP2" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="CI_SP2" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="2" code="Concept1"/>
                    <concept value="2" code="Concept1.2"/>
                    <concept value="2" code="Concept1.1"/>
                    <concept value="0" code="Concept2"/>
                    <concept value="0" code="Concept2.1"/>
                </concepts>
            </conceptFactSet>
            <conceptFactSet>
                <conceptFact id="1[2]-CI_SP0" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="CI_SP0" occur="1">
                        <integerValue>
                            <typedValue>43210</typedValue>
                        </integerValue>
                    </conceptValue>
                </conceptFact>
                <conceptFact id="B[2]-CI_SP2" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <conceptValue domain="CI_SP2" occur="1">
                        <stringValue>
                            <typedValue>B</typedValue>
                        </stringValue>
                    </conceptValue>
                </conceptFact>
                <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                    <concept value="1" code="Concept1"/>
                    <concept value="1" code="Concept1.2"/>
                    <concept value="1" code="Concept1.1"/>
                    <concept value="0" code="Concept2"/>
                    <concept value="0" code="Concept2.1"/>
                </concepts>
            </conceptFactSet>
        </conceptFinding>


#End Region

#Region "Response processing"

        ReadOnly _responseProcessing1 As XElement =
<qti-response-processing>
    <qti-response-condition>
        <qti-response-if>
            <qti-and>
                <qti-and>
                    <qti-equal toleranceMode="exact">
                        <qti-variable identifier="RESPONSE"/>
                        <qti-base-value base-type="integer">10</qti-base-value>
                    </qti-equal>
                    <qti-equal toleranceMode="exact">
                        <qti-variable identifier="RESPONSE2"/>
                        <qti-base-value base-type="integer">00</qti-base-value>
                    </qti-equal>
                </qti-and>
                <qti-equal tolerance-mode="exact">
                    <qti-variable identifier="RESPONSE3"/>
                    <qti-base-value base-type="integer">5</qti-base-value>
                </qti-equal>
            </qti-and>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
        <qti-response-else-if>
            <qti-and>
                <qti-and>
                    <qti-equal toleranceMode="exact">
                        <qti-variable identifier="RESPONSE"/>
                        <qti-base-value base-type="integer">11</qti-base-value>
                    </qti-equal>
                    <qti-equal toleranceMode="exact">
                        <qti-variable identifier="RESPONSE2"/>
                        <qti-base-value base-type="integer">00</qti-base-value>
                    </qti-equal>
                </qti-and>
                <qti-equal tolerance-mode="exact">
                    <qti-variable identifier="RESPONSE3"/>
                    <qti-base-value base-type="integer">4</qti-base-value>
                </qti-equal>
            </qti-and>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2-1">
                <qti-base-value base-type="float">3</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
        <qti-response-else>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else>
    </qti-response-condition>
    <qti-response-condition>
        <qti-response-if>
            <qti-and>
                <qti-equal toleranceMode="exact">
                    <qti-variable identifier="RESPONSE4"/>
                    <qti-base-value base-type="integer">12</qti-base-value>
                </qti-equal>
                <qti-equal toleranceMode="exact">
                    <qti-variable identifier="RESPONSE5"/>
                    <qti-base-value base-type="integer">00</qti-base-value>
                </qti-equal>
            </qti-and>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_Concept2-1">
                <qti-base-value base-type="float">3</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
        <qti-response-else-if>
            <qti-and>
                <qti-equal toleranceMode="exact">
                    <qti-variable identifier="RESPONSE4"/>
                    <qti-base-value base-type="integer">00</qti-base-value>
                </qti-equal>
                <qti-equal toleranceMode="exact">
                    <qti-variable identifier="RESPONSE5"/>
                    <qti-base-value base-type="integer">00</qti-base-value>
                </qti-equal>
            </qti-and>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_Concept2-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
        <qti-response-else>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_Concept2-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else>
    </qti-response-condition>
</qti-response-processing>

        ReadOnly _responseProcessing2 As XElement =
<qti-response-processing>
    <qti-response-condition>
        <qti-response-if>
            <qti-and>
                <qti-member>
                    <qti-base-value base-type="directedPair">B HSA</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-member>
                    <qti-base-value base-type="directedPair">A HSB</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
            </qti-and>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2-1">
                <qti-base-value base-type="float">3</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
        <qti-response-else-if>
            <qti-and>
                <qti-member>
                    <qti-base-value base-type="directedPair">A HSA</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-member>
                    <qti-base-value base-type="directedPair">B HSB</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
            </qti-and>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
        <qti-response-else>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else>
    </qti-response-condition>
</qti-response-processing>

        ReadOnly _responseProcessing3 As XElement =
<qti-response-processing>
    <qti-response-condition>
        <qti-response-if>
            <qti-member>
                <qti-base-value base-type="identifier">E</qti-base-value>
                <qti-variable identifier="RESPONSE"/>
            </qti-member>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
        <qti-response-else-if>
            <qti-member>
                <qti-base-value base-type="identifier">D</qti-base-value>
                <qti-variable identifier="RESPONSE"/>
            </qti-member>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-2">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
        <qti-response-else-if>
            <qti-member>
                <qti-base-value base-type="identifier">C</qti-base-value>
                <qti-variable identifier="RESPONSE"/>
            </qti-member>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
        <qti-response-else-if>
            <qti-member>
                <qti-base-value base-type="identifier">B</qti-base-value>
                <qti-variable identifier="RESPONSE"/>
            </qti-member>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
    </qti-response-condition>
</qti-response-processing>

        ReadOnly _responseProcessing4 As XElement =
<qti-response-processing>
    <qti-response-condition>
        <qti-response-if>
            <qti-member>
                <qti-base-value base-type="identifier">A</qti-base-value>
                <qti-variable identifier="RESPONSE"/>
            </qti-member>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-2">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
        <qti-response-else-if>
            <qti-not>
                <qti-member>
                    <qti-base-value base-type="identifier">A</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
            </qti-not>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-2">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
    </qti-response-condition>
    <qti-response-condition>
        <qti-response-if>
            <qti-not>
                <qti-member>
                    <qti-base-value base-type="identifier">B</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
            </qti-not>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_Concept1-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_Concept1-2">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
        <qti-response-else-if>
            <qti-member>
                <qti-base-value base-type="identifier">B</qti-base-value>
                <qti-variable identifier="RESPONSE"/>
            </qti-member>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_Concept1-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_Concept1-2">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
    </qti-response-condition>
    <qti-response-condition>
        <qti-response-if>
            <qti-member>
                <qti-base-value base-type="identifier">C</qti-base-value>
                <qti-variable identifier="RESPONSE"/>
            </qti-member>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE3_Concept1-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE3_Concept1-2">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
        <qti-response-else-if>
            <qti-not>
                <qti-member>
                    <qti-base-value base-type="identifier">C</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
            </qti-not>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE3_Concept1-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE3_Concept1-2">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
    </qti-response-condition>
    <qti-response-condition>
        <qti-response-if>
            <qti-not>
                <qti-member>
                    <qti-base-value base-type="identifier">D</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
            </qti-not>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE4_Concept1-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE4_Concept1-2">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
        <qti-response-else-if>
            <qti-member>
                <qti-base-value base-type="identifier">D</qti-base-value>
                <qti-variable identifier="RESPONSE"/>
            </qti-member>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE4_Concept1-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE4_Concept1-2">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
    </qti-response-condition>
    <qti-response-condition>
        <qti-response-if>
            <qti-member>
                <qti-base-value base-type="identifier">E</qti-base-value>
                <qti-variable identifier="RESPONSE"/>
            </qti-member>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE5_Concept1-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE5_Concept1-2">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
        <qti-response-else-if>
            <qti-not>
                <qti-member>
                    <qti-base-value base-type="identifier">E</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
            </qti-not>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE5_Concept1-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE5_Concept1-2">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
    </qti-response-condition>
</qti-response-processing>

        ReadOnly _responseProcessing5 As XElement =
<qti-response-processing>
    <qti-response-condition>
        <qti-response-if>
            <qti-and>
                <qti-member>
                    <qti-base-value base-type="identifier">C</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">D</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-member>
                    <qti-base-value base-type="identifier">E</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
            </qti-and>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-2">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
        <qti-response-else-if>
            <qti-and>
                <qti-member>
                    <qti-base-value base-type="identifier">C</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-member>
                    <qti-base-value base-type="identifier">D</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">E</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
            </qti-and>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-2">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
        <qti-response-else-if>
            <qti-and>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">C</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-member>
                    <qti-base-value base-type="identifier">D</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-member>
                    <qti-base-value base-type="identifier">E</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
            </qti-and>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-2">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
    </qti-response-condition>
    <qti-response-condition>
        <qti-response-if>
            <qti-member>
                <qti-base-value base-type="identifier">A</qti-base-value>
                <qti-variable identifier="RESPONSE"/>
            </qti-member>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_Concept1-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_Concept1-2">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
        <qti-response-else-if>
            <qti-not>
                <qti-member>
                    <qti-base-value base-type="identifier">A</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
            </qti-not>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_Concept1-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_Concept1-2">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
    </qti-response-condition>
    <qti-response-condition>
        <qti-response-if>
            <qti-not>
                <qti-member>
                    <qti-base-value base-type="identifier">B</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
            </qti-not>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE3_Concept1-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE3_Concept1-2">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
        <qti-response-else-if>
            <qti-member>
                <qti-base-value base-type="identifier">B</qti-base-value>
                <qti-variable identifier="RESPONSE"/>
            </qti-member>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE3_Concept1-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE3_Concept1-2">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
    </qti-response-condition>
</qti-response-processing>

        ReadOnly _responseProcessing6 As XElement =
<qti-response-processing>
    <qti-response-condition>
        <qti-response-if>
            <qti-and>
                <qti-member>
                    <qti-base-value base-type="identifier">A</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-member>
                    <qti-base-value base-type="identifier">B</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-member>
                    <qti-base-value base-type="identifier">C</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">D</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">E</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
            </qti-and>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-2">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
        <qti-response-else-if>
            <qti-and>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">A</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">B</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-member>
                    <qti-base-value base-type="identifier">C</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-member>
                    <qti-base-value base-type="identifier">D</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-member>
                    <qti-base-value base-type="identifier">E</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
            </qti-and>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-2">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
        <qti-response-else-if>
            <qti-and>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">A</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-member>
                    <qti-base-value base-type="identifier">B</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-member>
                    <qti-base-value base-type="identifier">C</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-member>
                    <qti-base-value base-type="identifier">D</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">E</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
            </qti-and>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-2">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
    </qti-response-condition>
</qti-response-processing>

        ReadOnly _responseProcessing7 As XElement =
<qti-response-processing>
    <qti-response-condition>
        <qti-response-if>
            <qti-member>
                <qti-base-value base-type="directedPair">B HSA</qti-base-value>
                <qti-variable identifier="RESPONSE"/>
            </qti-member>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-2">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
        <qti-response-else-if>
            <qti-member>
                <qti-base-value base-type="directedPair">A HSA</qti-base-value>
                <qti-variable identifier="RESPONSE"/>
            </qti-member>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-2">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
    </qti-response-condition>
    <qti-response-condition>
        <qti-response-if>
            <qti-member>
                <qti-base-value base-type="directedPair">A HSB</qti-base-value>
                <qti-variable identifier="RESPONSE"/>
            </qti-member>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_Concept1-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_Concept1-2">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
        <qti-response-else>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_Concept1-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_Concept1-2">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else>
    </qti-response-condition>
</qti-response-processing>

        ReadOnly _responseProcessing8 As XElement =
<qti-response-processing>
    <qti-response-condition>
        <qti-response-if>
            <qti-and>
                <qti-member>
                    <qti-base-value base-type="directedPair">A HSB</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-member>
                    <qti-base-value base-type="directedPair">A HSC</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
            </qti-and>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-1">
                <qti-base-value base-type="float">3</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-2">
                <qti-base-value base-type="float">3</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
        <qti-response-else-if>
            <qti-and>
                <qti-member>
                    <qti-base-value base-type="directedPair">B HSB</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-member>
                    <qti-base-value base-type="directedPair">B HSC</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
            </qti-and>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-2">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
        <qti-response-else>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-2">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else>
    </qti-response-condition>
    <qti-response-condition>
        <qti-response-if>
            <qti-member>
                <qti-base-value base-type="directedPair">B HSA</qti-base-value>
                <qti-variable identifier="RESPONSE"/>
            </qti-member>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_Concept1-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_Concept1-2">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
        <qti-response-else-if>
            <qti-member>
                <qti-base-value base-type="directedPair">A HSA</qti-base-value>
                <qti-variable identifier="RESPONSE"/>
            </qti-member>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_Concept1-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_Concept1-2">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
    </qti-response-condition>
</qti-response-processing>

        ReadOnly _responseProcessing9 As XElement =
<qti-response-processing>
    <qti-response-condition>
        <qti-response-if>
            <qti-member>
                <qti-base-value base-type="directedPair">y_A x_1</qti-base-value>
                <qti-variable identifier="RESPONSE"/>
            </qti-member>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-2">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
    </qti-response-condition>
    <qti-response-condition>
        <qti-response-if>
            <qti-member>
                <qti-base-value base-type="directedPair">y_B x_2</qti-base-value>
                <qti-variable identifier="RESPONSE"/>
            </qti-member>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_Concept1-1">
                <qti-base-value base-type="float">3</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_Concept1-2">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
        <qti-response-else-if>
            <qti-member>
                <qti-base-value base-type="directedPair">y_B x_1</qti-base-value>
                <qti-variable identifier="RESPONSE"/>
            </qti-member>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_Concept1-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_Concept1-2">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
        <qti-response-else>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_Concept1-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else>
    </qti-response-condition>
    <qti-response-condition>
        <qti-response-if>
            <qti-member>
                <qti-base-value base-type="directedPair">y_C x_1</qti-base-value>
                <qti-variable identifier="RESPONSE"/>
            </qti-member>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE3_Concept1-1">
                <qti-base-value base-type="float">3</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
        <qti-response-else-if>
            <qti-member>
                <qti-base-value base-type="directedPair">y_C x_2</qti-base-value>
                <qti-variable identifier="RESPONSE"/>
            </qti-member>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE3_Concept1-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
        <qti-response-else>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE3_Concept1-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else>
    </qti-response-condition>
</qti-response-processing>

        ReadOnly _responseProcessing10 As XElement =
<qti-response-processing>
    <qti-response-condition>
        <qti-response-if>
            <qti-and>
                <qti-member>
                    <qti-base-value base-type="directedPair">y_A x_1</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-member>
                    <qti-base-value base-type="directedPair">y_B x_2</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-member>
                    <qti-base-value base-type="directedPair">y_C x_1</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-member>
                    <qti-base-value base-type="directedPair">y_D x_2</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
            </qti-and>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-1">
                <qti-base-value base-type="float">3</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-2">
                <qti-base-value base-type="float">3</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
        <qti-response-else-if>
            <qti-and>
                <qti-member>
                    <qti-base-value base-type="directedPair">y_A x_1</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-member>
                    <qti-base-value base-type="directedPair">y_B x_1</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-member>
                    <qti-base-value base-type="directedPair">y_C x_1</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-member>
                    <qti-base-value base-type="directedPair">y_D x_1</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
            </qti-and>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-2">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
        <qti-response-else-if>
            <qti-and>
                <qti-member>
                    <qti-base-value base-type="directedPair">y_A x_2</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-member>
                    <qti-base-value base-type="directedPair">y_B x_2</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-member>
                    <qti-base-value base-type="directedPair">y_C x_2</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-member>
                    <qti-base-value base-type="directedPair">y_D x_2</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
            </qti-and>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-2">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
    </qti-response-condition>
</qti-response-processing>

        ReadOnly _responseProcessing11 As XElement =
<qti-response-processing>
    <qti-response-condition>
        <qti-response-if>
            <qti-and>
                <qti-member>
                    <qti-base-value base-type="directedPair">y_A x_1</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-member>
                    <qti-base-value base-type="directedPair">y_D x_2</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
            </qti-and>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-2">
                <qti-base-value base-type="float">3</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
        <qti-response-else-if>
            <qti-and>
                <qti-member>
                    <qti-base-value base-type="directedPair">y_A x_1</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-member>
                    <qti-base-value base-type="directedPair">y_D x_1</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
            </qti-and>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-2">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
        <qti-response-else>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-2">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else>
    </qti-response-condition>
    <qti-response-condition>
        <qti-response-if>
            <qti-member>
                <qti-base-value base-type="directedPair">y_B x_2</qti-base-value>
                <qti-variable identifier="RESPONSE"/>
            </qti-member>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_Concept1-2">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
        <qti-response-else>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_Concept1-2">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else>
    </qti-response-condition>
    <qti-response-condition>
        <qti-response-if>
            <qti-member>
                <qti-base-value base-type="directedPair">y_C x_1</qti-base-value>
                <qti-variable identifier="RESPONSE"/>
            </qti-member>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE3_Concept1-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
        <qti-response-else-if>
            <qti-member>
                <qti-base-value base-type="directedPair">y_C x_2</qti-base-value>
                <qti-variable identifier="RESPONSE"/>
            </qti-member>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE3_Concept1-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
    </qti-response-condition>
</qti-response-processing>

        ReadOnly _responseProcessing12 As XElement =
<qti-response-processing>
    <qti-response-condition>
        <qti-response-if>
            <qti-or>
                <qti-string-match case-sensitive="true">
                    <qti-variable identifier="RESPONSE"/>
                    <qti-base-value base-type="string">green</qti-base-value>
                </qti-string-match>
                <qti-string-match case-sensitive="true">
                    <qti-variable identifier="RESPONSE"/>
                    <qti-base-value base-type="string">GREEN</qti-base-value>
                </qti-string-match>
            </qti-or>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_EN-SC-3">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_EN-SC-3-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
        <qti-response-else-if>
            <qti-string-match case-sensitive="true">
                <qti-variable identifier="RESPONSE"/>
                <qti-base-value base-type="string">grean</qti-base-value>
            </qti-string-match>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_EN-SC-3">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_EN-SC-3-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
    </qti-response-condition>
</qti-response-processing>

        ReadOnly _responseProcessing13 As XElement =
<qti-response-processing>
    <qti-response-condition>
        <qti-response-if>
            <qti-match>
                <qti-base-value base-type="identifier">D</qti-base-value>
                <qti-variable identifier="RESPONSE"/>
            </qti-match>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-2">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
        <qti-response-else-if>
            <qti-match>
                <qti-base-value base-type="identifier">C</qti-base-value>
                <qti-variable identifier="RESPONSE"/>
            </qti-match>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1">
                <qti-base-value base-type="float">3</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-1">
                <qti-base-value base-type="float">3</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-2">
                <qti-base-value base-type="float">3</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
        <qti-response-else-if>
            <qti-match>
                <qti-base-value base-type="identifier">B</qti-base-value>
                <qti-variable identifier="RESPONSE"/>
            </qti-match>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
        <qti-response-else-if>
            <qti-match>
                <qti-base-value base-type="identifier">A</qti-base-value>
                <qti-variable identifier="RESPONSE"/>
            </qti-match>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-2">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
    </qti-response-condition>
    <qti-response-condition>
        <qti-response-if>
            <qti-match>
                <qti-base-value base-type="identifier">E</qti-base-value>
                <qti-variable identifier="RESPONSE2"/>
            </qti-match>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_Concept1-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_Concept2">
                <qti-base-value base-type="float">3</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_Concept2-1">
                <qti-base-value base-type="float">3</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
        <qti-response-else-if>
            <qti-match>
                <qti-base-value base-type="identifier">D</qti-base-value>
                <qti-variable identifier="RESPONSE2"/>
            </qti-match>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_Concept2">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_Concept2-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
        <qti-response-else-if>
            <qti-match>
                <qti-base-value base-type="identifier">C</qti-base-value>
                <qti-variable identifier="RESPONSE2"/>
            </qti-match>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_Concept2">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_Concept2-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
        <qti-response-else-if>
            <qti-match>
                <qti-base-value base-type="identifier">B</qti-base-value>
                <qti-variable identifier="RESPONSE2"/>
            </qti-match>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_Concept1-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
        <qti-response-else-if>
            <qti-match>
                <qti-base-value base-type="identifier">A</qti-base-value>
                <qti-variable identifier="RESPONSE2"/>
            </qti-match>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_Concept2">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_Concept2-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
    </qti-response-condition>
</qti-response-processing>

        ReadOnly _responseProcessing14 As XElement =
<qti-response-processing>
    <qti-response-condition>
        <qti-response-if>
            <qti-and>
                <qti-match>
                    <qti-base-value base-type="identifier">D</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-match>
                <qti-match>
                    <qti-base-value base-type="identifier">E</qti-base-value>
                    <qti-variable identifier="RESPONSE2"/>
                </qti-match>
                <qti-match>
                    <qti-base-value base-type="identifier">B</qti-base-value>
                    <qti-variable identifier="RESPONSE3"/>
                </qti-match>
            </qti-and>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-2">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2">
                <qti-base-value base-type="float">3</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2-1">
                <qti-base-value base-type="float">3</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
        <qti-response-else-if>
            <qti-and>
                <qti-match>
                    <qti-base-value base-type="identifier">B</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-match>
                <qti-match>
                    <qti-base-value base-type="identifier">B</qti-base-value>
                    <qti-variable identifier="RESPONSE2"/>
                </qti-match>
                <qti-match>
                    <qti-base-value base-type="identifier">B</qti-base-value>
                    <qti-variable identifier="RESPONSE3"/>
                </qti-match>
            </qti-and>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1">
                <qti-base-value base-type="float">3</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-1">
                <qti-base-value base-type="float">3</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-2">
                <qti-base-value base-type="float">3</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
        <qti-response-else-if>
            <qti-and>
                <qti-match>
                    <qti-base-value base-type="identifier">A</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-match>
                <qti-match>
                    <qti-base-value base-type="identifier">A</qti-base-value>
                    <qti-variable identifier="RESPONSE2"/>
                </qti-match>
                <qti-match>
                    <qti-base-value base-type="identifier">A</qti-base-value>
                    <qti-variable identifier="RESPONSE3"/>
                </qti-match>
            </qti-and>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1">
                <qti-base-value base-type="float">3</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-1">
                <qti-base-value base-type="float">3</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-2">
                <qti-base-value base-type="float">3</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
        <qti-response-else>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-2">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else>
    </qti-response-condition>
</qti-response-processing>

        ReadOnly _responseProcessing15 As XElement =
<qti-response-processing>
    <qti-response-condition>
        <qti-response-if>
            <qti-and>
                <qti-match>
                    <qti-base-value base-type="identifier">D</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-match>
                <qti-match>
                    <qti-base-value base-type="identifier">E</qti-base-value>
                    <qti-variable identifier="RESPONSE2"/>
                </qti-match>
            </qti-and>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-2">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2">
                <qti-base-value base-type="float">3</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2-1">
                <qti-base-value base-type="float">3</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
        <qti-response-else-if>
            <qti-and>
                <qti-match>
                    <qti-base-value base-type="identifier">B</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-match>
                <qti-match>
                    <qti-base-value base-type="identifier">B</qti-base-value>
                    <qti-variable identifier="RESPONSE2"/>
                </qti-match>
            </qti-and>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1">
                <qti-base-value base-type="float">3</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-1">
                <qti-base-value base-type="float">3</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-2">
                <qti-base-value base-type="float">3</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
        <qti-response-else-if>
            <qti-and>
                <qti-match>
                    <qti-base-value base-type="identifier">A</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-match>
                <qti-match>
                    <qti-base-value base-type="identifier">A</qti-base-value>
                    <qti-variable identifier="RESPONSE2"/>
                </qti-match>
            </qti-and>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1">
                <qti-base-value base-type="float">3</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-1">
                <qti-base-value base-type="float">3</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-2">
                <qti-base-value base-type="float">3</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
        <qti-response-else>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-2">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else>
    </qti-response-condition>
    <qti-response-condition>
        <qti-response-if>
            <qti-match>
                <qti-base-value base-type="identifier">B</qti-base-value>
                <qti-variable identifier="RESPONSE3"/>
            </qti-match>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_Concept1-2">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_Concept2-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
        <qti-response-else-if>
            <qti-match>
                <qti-base-value base-type="identifier">A</qti-base-value>
                <qti-variable identifier="RESPONSE3"/>
            </qti-match>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_Concept1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_Concept1-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_Concept1-2">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_Concept2">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_Concept2-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
    </qti-response-condition>
</qti-response-processing>

        ReadOnly _responseProcessing16 As XElement =
<qti-response-processing>
    <qti-response-condition>
        <qti-response-if>
            <qti-member>
                <qti-base-value base-type="directedPair">A I51faf178-ca03-41eb-8276-385ef2a185b3</qti-base-value>
                <qti-variable identifier="RESPONSE"/>
            </qti-member>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-2">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
        <qti-response-else>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-2">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else>
    </qti-response-condition>
    <qti-response-condition>
        <qti-response-if>
            <qti-member>
                <qti-base-value base-type="directedPair">B I989f6f3c-9d38-492f-80fe-4b71cfee574f</qti-base-value>
                <qti-variable identifier="RESPONSE"/>
            </qti-member>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_Concept1-2">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
    </qti-response-condition>
    <qti-response-condition>
        <qti-response-if>
            <qti-member>
                <qti-base-value base-type="directedPair">C Ie1f57945-a74c-4f6c-948b-5133fb9778e8</qti-base-value>
                <qti-variable identifier="RESPONSE"/>
            </qti-member>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE3_Concept2-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
    </qti-response-condition>
    <qti-response-condition>
        <qti-response-if>
            <qti-member>
                <qti-base-value base-type="directedPair">D I723529e7-8893-455e-b785-595592528040</qti-base-value>
                <qti-variable identifier="RESPONSE"/>
            </qti-member>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE4_Concept1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE4_Concept1-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE4_Concept1-2">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE4_Concept2">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE4_Concept2-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
        <qti-response-else-if>
            <qti-member>
                <qti-base-value base-type="directedPair">F I723529e7-8893-455e-b785-595592528040</qti-base-value>
                <qti-variable identifier="RESPONSE"/>
            </qti-member>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE4_Concept1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE4_Concept1-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE4_Concept1-2">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE4_Concept2">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE4_Concept2-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
    </qti-response-condition>
</qti-response-processing>

        ReadOnly _responseProcessing17 As XElement =
<qti-response-processing>
    <qti-response-condition>
        <qti-response-if>
            <qti-and>
                <qti-member>
                    <qti-base-value base-type="directedPair">A I51faf178-ca03-41eb-8276-385ef2a185b3</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-member>
                    <qti-base-value base-type="directedPair">B I989f6f3c-9d38-492f-80fe-4b71cfee574f</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-member>
                    <qti-base-value base-type="directedPair">C Ie1f57945-a74c-4f6c-948b-5133fb9778e8</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-member>
                    <qti-base-value base-type="directedPair">D I723529e7-8893-455e-b785-595592528040</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
            </qti-and>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-2">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
        <qti-response-else-if>
            <qti-and>
                <qti-member>
                    <qti-base-value base-type="directedPair">D I51faf178-ca03-41eb-8276-385ef2a185b3</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-member>
                    <qti-base-value base-type="directedPair">C I989f6f3c-9d38-492f-80fe-4b71cfee574f</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-member>
                    <qti-base-value base-type="directedPair">B Ie1f57945-a74c-4f6c-948b-5133fb9778e8</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-member>
                    <qti-base-value base-type="directedPair">A I723529e7-8893-455e-b785-595592528040</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
            </qti-and>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-2">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
        <qti-response-else-if>
            <qti-and>
                <qti-member>
                    <qti-base-value base-type="directedPair">F I51faf178-ca03-41eb-8276-385ef2a185b3</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-member>
                    <qti-base-value base-type="directedPair">E I989f6f3c-9d38-492f-80fe-4b71cfee574f</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-member>
                    <qti-base-value base-type="directedPair">B Ie1f57945-a74c-4f6c-948b-5133fb9778e8</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-member>
                    <qti-base-value base-type="directedPair">A I723529e7-8893-455e-b785-595592528040</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
            </qti-and>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-2">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
        <qti-response-else-if>
            <qti-and>
                <qti-member>
                    <qti-base-value base-type="directedPair">C I51faf178-ca03-41eb-8276-385ef2a185b3</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-member>
                    <qti-base-value base-type="directedPair">D I989f6f3c-9d38-492f-80fe-4b71cfee574f</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-member>
                    <qti-base-value base-type="directedPair">E Ie1f57945-a74c-4f6c-948b-5133fb9778e8</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-member>
                    <qti-base-value base-type="directedPair">F I723529e7-8893-455e-b785-595592528040</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
            </qti-and>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-2">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
        <qti-response-else>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-2">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else>
    </qti-response-condition>
</qti-response-processing>

        ReadOnly _responseProcessing18 As XElement =
<qti-response-processing>
    <qti-response-condition>
        <qti-response-if>
            <qti-and>
                <qti-member>
                    <qti-base-value base-type="directedPair">A I51faf178-ca03-41eb-8276-385ef2a185b3</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-member>
                    <qti-base-value base-type="directedPair">B I989f6f3c-9d38-492f-80fe-4b71cfee574f</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-member>
                    <qti-base-value base-type="directedPair">C Ie1f57945-a74c-4f6c-948b-5133fb9778e8</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
            </qti-and>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-2">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
        <qti-response-else-if>
            <qti-and>
                <qti-member>
                    <qti-base-value base-type="directedPair">C I51faf178-ca03-41eb-8276-385ef2a185b3</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-member>
                    <qti-base-value base-type="directedPair">B I989f6f3c-9d38-492f-80fe-4b71cfee574f</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-member>
                    <qti-base-value base-type="directedPair">A Ie1f57945-a74c-4f6c-948b-5133fb9778e8</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
            </qti-and>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-2">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
        <qti-response-else-if>
            <qti-and>
                <qti-member>
                    <qti-base-value base-type="directedPair">F I51faf178-ca03-41eb-8276-385ef2a185b3</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-member>
                    <qti-base-value base-type="directedPair">E I989f6f3c-9d38-492f-80fe-4b71cfee574f</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-member>
                    <qti-base-value base-type="directedPair">D Ie1f57945-a74c-4f6c-948b-5133fb9778e8</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
            </qti-and>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-2">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
        <qti-response-else-if>
            <qti-and>
                <qti-member>
                    <qti-base-value base-type="directedPair">D I51faf178-ca03-41eb-8276-385ef2a185b3</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-member>
                    <qti-base-value base-type="directedPair">C I989f6f3c-9d38-492f-80fe-4b71cfee574f</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-member>
                    <qti-base-value base-type="directedPair">B Ie1f57945-a74c-4f6c-948b-5133fb9778e8</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
            </qti-and>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-2">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
        <qti-response-else>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-2">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else>
    </qti-response-condition>
    <qti-response-condition>
        <qti-response-if>
            <qti-member>
                <qti-base-value base-type="directedPair">D I723529e7-8893-455e-b785-595592528040</qti-base-value>
                <qti-variable identifier="RESPONSE"/>
            </qti-member>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_Concept1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_Concept1-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_Concept1-2">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_Concept2">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_Concept2-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
        <qti-response-else-if>
            <qti-member>
                <qti-base-value base-type="directedPair">E I723529e7-8893-455e-b785-595592528040</qti-base-value>
                <qti-variable identifier="RESPONSE"/>
            </qti-member>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_Concept1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_Concept1-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_Concept1-2">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_Concept2">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_Concept2-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
    </qti-response-condition>
</qti-response-processing>

        ReadOnly _responseProcessing19 As XElement =
<qti-response-processing>
    <qti-response-condition>
        <qti-response-if>
            <qti-and>
                <qti-match>
                    <qti-index n="1">
                        <qti-variable identifier="RESPONSE"/>
                    </qti-index>
                    <qti-base-value base-type="identifier">C</qti-base-value>
                </qti-match>
                <qti-match>
                    <qti-index n="2">
                        <qti-variable identifier="RESPONSE"/>
                    </qti-index>
                    <qti-base-value base-type="identifier">A</qti-base-value>
                </qti-match>
                <qti-match>
                    <qti-index n="3">
                        <qti-variable identifier="RESPONSE"/>
                    </qti-index>
                    <qti-base-value base-type="identifier">B</qti-base-value>
                </qti-match>
            </qti-and>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
        <qti-response-else>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else>
    </qti-response-condition>
</qti-response-processing>

        ReadOnly _responseProcessing20 As XElement =
<qti-response-processing>
    <qti-response-condition>
        <qti-response-if>
            <qti-and>
                <qti-match>
                    <qti-index n="1">
                        <qti-variable identifier="RESPONSE"/>
                    </qti-index>
                    <qti-base-value base-type="identifier">C</qti-base-value>
                </qti-match>
                <qti-match>
                    <qti-index n="2">
                        <qti-variable identifier="RESPONSE"/>
                    </qti-index>
                    <qti-base-value base-type="identifier">A</qti-base-value>
                </qti-match>
                <qti-match>
                    <qti-index n="3">
                        <qti-variable identifier="RESPONSE"/>
                    </qti-index>
                    <qti-base-value base-type="identifier">B</qti-base-value>
                </qti-match>
            </qti-and>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
        <qti-response-else-if>
            <qti-and>
                <qti-match>
                    <qti-index n="1">
                        <qti-variable identifier="RESPONSE"/>
                    </qti-index>
                    <qti-base-value base-type="identifier">B</qti-base-value>
                </qti-match>
                <qti-match>
                    <qti-index n="2">
                        <qti-variable identifier="RESPONSE"/>
                    </qti-index>
                    <qti-base-value base-type="identifier">C</qti-base-value>
                </qti-match>
                <qti-match>
                    <qti-index n="3">
                        <qti-variable identifier="RESPONSE"/>
                    </qti-index>
                    <qti-base-value base-type="identifier">A</qti-base-value>
                </qti-match>
            </qti-and>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2-1">
                <qti-base-value base-type="float">3</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
        <qti-response-else>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else>
    </qti-response-condition>
</qti-response-processing>

        ReadOnly _responseProcessing21 As XElement =
<qti-response-processing>
    <qti-response-condition>
        <qti-response-if>
            <qti-and>
                <qti-match>
                    <qti-index n="1">
                        <qti-variable identifier="RESPONSE"/>
                    </qti-index>
                    <qti-base-value base-type="identifier">C</qti-base-value>
                </qti-match>
                <qti-match>
                    <qti-index n="2">
                        <qti-variable identifier="RESPONSE"/>
                    </qti-index>
                    <qti-base-value base-type="identifier">A</qti-base-value>
                </qti-match>
                <qti-match>
                    <qti-index n="3">
                        <qti-variable identifier="RESPONSE"/>
                    </qti-index>
                    <qti-base-value base-type="identifier">B</qti-base-value>
                </qti-match>
            </qti-and>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_1-wiskundige-structuur">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_B-Getallen">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_B1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
    </qti-response-condition>
</qti-response-processing>

        ReadOnly _responseProcessing22 As XElement =
<qti-response-processing>
    <qti-response-condition>
        <qti-response-if>
            <qti-and>
                <qti-member>
                    <qti-base-value base-type="directedPair">B I97f0b37a-df8c-4f3d-868f-2b2d6490998d</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-member>
                    <qti-base-value base-type="directedPair">D I62fae0a2-b76f-412f-9afd-5b75b79d9a79</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
            </qti-and>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
        <qti-response-else>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else>
    </qti-response-condition>
</qti-response-processing>

        ReadOnly _responseProcessing23 As XElement =
<qti-response-processing>
    <qti-response-condition>
        <qti-response-if>
            <qti-member>
                <qti-base-value base-type="identifier">A</qti-base-value>
                <qti-variable identifier="RESPONSE"/>
            </qti-member>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-2">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
        <qti-response-else-if>
            <qti-not>
                <qti-member>
                    <qti-base-value base-type="identifier">A</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
            </qti-not>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
    </qti-response-condition>
    <qti-response-condition>
        <qti-response-if>
            <qti-member>
                <qti-base-value base-type="identifier">C</qti-base-value>
                <qti-variable identifier="RESPONSE"/>
            </qti-member>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE3_Concept2">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE3_Concept2-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
        <qti-response-else-if>
            <qti-not>
                <qti-member>
                    <qti-base-value base-type="identifier">C</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
            </qti-not>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE3_Concept2-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
    </qti-response-condition>
    <qti-response-condition>
        <qti-response-if>
            <qti-member>
                <qti-base-value base-type="identifier">D</qti-base-value>
                <qti-variable identifier="RESPONSE"/>
            </qti-member>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE4_Concept2">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE4_Concept2-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
    </qti-response-condition>
</qti-response-processing>

        ReadOnly _responseProcessing24 As XElement =
<qti-response-processing>
    <qti-response-condition>
        <qti-response-if>
            <qti-and>
                <qti-member>
                    <qti-base-value base-type="identifier">A</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">B</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-member>
                    <qti-base-value base-type="identifier">C</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">D</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-member>
                    <qti-base-value base-type="identifier">E</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
            </qti-and>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-2">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
        <qti-response-else-if>
            <qti-and>
                <qti-member>
                    <qti-base-value base-type="identifier">A</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-member>
                    <qti-base-value base-type="identifier">B</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-member>
                    <qti-base-value base-type="identifier">C</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">D</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">E</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
            </qti-and>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-2">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
        <qti-response-else-if>
            <qti-and>
                <qti-member>
                    <qti-base-value base-type="identifier">A</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">B</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">C</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">D</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-member>
                    <qti-base-value base-type="identifier">E</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
            </qti-and>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1">
                <qti-base-value base-type="float">3</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-2">
                <qti-base-value base-type="float">3</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2">
                <qti-base-value base-type="float">3</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2-1">
                <qti-base-value base-type="float">3</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
        <qti-response-else-if>
            <qti-and>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">A</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-member>
                    <qti-base-value base-type="identifier">B</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">C</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-member>
                    <qti-base-value base-type="identifier">D</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">E</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
            </qti-and>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-2">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
        <qti-response-else>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else>
    </qti-response-condition>
</qti-response-processing>

        ReadOnly _responseProcessing25 As XElement =
<qti-response-processing>
    <qti-response-condition>
        <qti-response-if>
            <qti-and>
                <qti-member>
                    <qti-base-value base-type="identifier">A</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">B</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-member>
                    <qti-base-value base-type="identifier">C</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
            </qti-and>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-2">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
        <qti-response-else-if>
            <qti-and>
                <qti-member>
                    <qti-base-value base-type="identifier">A</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-member>
                    <qti-base-value base-type="identifier">B</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">C</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
            </qti-and>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-2">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
        <qti-response-else-if>
            <qti-and>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">A</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-member>
                    <qti-base-value base-type="identifier">B</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-member>
                    <qti-base-value base-type="identifier">C</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
            </qti-and>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-1">
                <qti-base-value base-type="float">3</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-2">
                <qti-base-value base-type="float">3</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2-1">
                <qti-base-value base-type="float">3</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
        <qti-response-else-if>
            <qti-and>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">A</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">B</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-member>
                    <qti-base-value base-type="identifier">C</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
            </qti-and>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
        <qti-response-else>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else>
    </qti-response-condition>
    <qti-response-condition>
        <qti-response-if>
            <qti-not>
                <qti-member>
                    <qti-base-value base-type="identifier">D</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
            </qti-not>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_Concept1-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
        <qti-response-else-if>
            <qti-member>
                <qti-base-value base-type="identifier">D</qti-base-value>
                <qti-variable identifier="RESPONSE"/>
            </qti-member>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_Concept1-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
    </qti-response-condition>
</qti-response-processing>

        ReadOnly _responseProcessing26 As XElement =
<qti-response-processing>
    <qti-response-condition>
        <qti-response-if>
            <qti-and>
                <qti-member>
                    <qti-base-value base-type="identifier">A</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">B</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-member>
                    <qti-base-value base-type="identifier">C</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
            </qti-and>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1">
                <qti-base-value base-type="float">0</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-1">
                <qti-base-value base-type="float">0</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-2">
                <qti-base-value base-type="float">0</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2">
                <qti-base-value base-type="float">0</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2-1">
                <qti-base-value base-type="float">0</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
        <qti-response-else-if>
            <qti-and>
                <qti-member>
                    <qti-base-value base-type="identifier">A</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-member>
                    <qti-base-value base-type="identifier">B</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">C</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
            </qti-and>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1">
                <qti-base-value base-type="float">0</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-1">
                <qti-base-value base-type="float">0</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-2">
                <qti-base-value base-type="float">0</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2">
                <qti-base-value base-type="float">0</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2-1">
                <qti-base-value base-type="float">0</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
        <qti-response-else-if>
            <qti-and>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">A</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-member>
                    <qti-base-value base-type="identifier">B</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-member>
                    <qti-base-value base-type="identifier">C</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
            </qti-and>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-1">
                <qti-base-value base-type="float">3</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-2">
                <qti-base-value base-type="float">3</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2-1">
                <qti-base-value base-type="float">3</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
        <qti-response-else-if>
            <qti-and>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">A</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">B</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-member>
                    <qti-base-value base-type="identifier">C</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
            </qti-and>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
        <qti-response-else>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else>
    </qti-response-condition>
    <qti-response-condition>
        <qti-response-if>
            <qti-member>
                <qti-base-value base-type="identifier">D</qti-base-value>
                <qti-variable identifier="RESPONSE"/>
            </qti-member>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_Concept1-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
    </qti-response-condition>
    <qti-response-condition>
        <qti-response-if>
            <qti-not>
                <qti-member>
                    <qti-base-value base-type="identifier">E</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
            </qti-not>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE3_Concept1-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE3_Concept2-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
    </qti-response-condition>
</qti-response-processing>

        ReadOnly _responseProcessing27 As XElement =
<qti-response-processing></qti-response-processing>

        ReadOnly _responseProcessing28 As XElement =
<qti-response-processing>
    <qti-response-condition>
        <qti-response-if>
            <qti-member>
                <qti-base-value base-type="identifier">I1f639c54-57ee-428e-93ed-77d786d5a2d1</qti-base-value>
                <qti-variable identifier="RESPONSE"/>
            </qti-member>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE18_Concept2">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
    </qti-response-condition>
    <qti-response-condition>
        <qti-response-if>
            <qti-member>
                <qti-base-value base-type="identifier">I2a787ac6-adce-419f-b230-293302ab6e2b</qti-base-value>
                <qti-variable identifier="RESPONSE"/>
            </qti-member>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE30_Concept2">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
    </qti-response-condition>
    <qti-response-condition>
        <qti-response-if>
            <qti-member>
                <qti-base-value base-type="identifier">I46f0651d-a9f2-4b96-8172-8104b0938213</qti-base-value>
                <qti-variable identifier="RESPONSE"/>
            </qti-member>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE35_Concept2">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE35_Concept2-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
    </qti-response-condition>
    <qti-response-condition>
        <qti-response-if>
            <qti-member>
                <qti-base-value base-type="identifier">I0bf4350c-9288-4cec-9e37-1b0978b057ba</qti-base-value>
                <qti-variable identifier="RESPONSE"/>
            </qti-member>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE39_Concept1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE39_Concept2">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
        <qti-response-else-if>
            <qti-not>
                <qti-member>
                    <qti-base-value base-type="identifier">I0bf4350c-9288-4cec-9e37-1b0978b057ba</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
            </qti-not>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE39_Concept1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE39_Concept2">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
    </qti-response-condition>
    <qti-response-condition>
        <qti-response-if>
            <qti-not>
                <qti-member>
                    <qti-base-value base-type="identifier">I7e8b004a-0223-432a-bb9a-536918190017</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
            </qti-not>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE44_Concept1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
        <qti-response-else-if>
            <qti-member>
                <qti-base-value base-type="identifier">I7e8b004a-0223-432a-bb9a-536918190017</qti-base-value>
                <qti-variable identifier="RESPONSE"/>
            </qti-member>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE44_Concept1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
    </qti-response-condition>
    <qti-response-condition>
        <qti-response-if>
            <qti-member>
                <qti-base-value base-type="identifier">I46521597-f1f4-432c-96c2-8e3defb05950</qti-base-value>
                <qti-variable identifier="RESPONSE"/>
            </qti-member>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE46_Concept1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
    </qti-response-condition>
</qti-response-processing>

        ReadOnly _responseProcessing29 As XElement =
<qti-response-processing>
    <qti-response-condition>
        <qti-response-if>
            <qti-and>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">Iba0772fe-878f-457c-b652-a0e922626082</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I5493e55f-fae9-499f-bd3e-16df52c028e1</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I7134c778-4641-40b5-8eae-1a80013cf777</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-member>
                    <qti-base-value base-type="identifier">I3cb9edb6-551e-474a-9122-338b8c6a396d</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I41d5e633-d892-46fe-8a0c-ceba54c167f3</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I498a96cd-114d-4299-b27c-639e41559e29</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I19b64b54-f7f4-4284-af37-37999bd24935</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I7ea195a7-b0ec-465e-ada3-31c9e940f0dc</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">Ibf683ce7-ac85-4610-b057-dd0578f3b71f</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">Ie6f1fe1f-fe23-4f29-85ec-27d483138cfa</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I2b4ed71d-8984-4ffd-a95a-126efbb12e48</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I0778d3a8-11c3-41f5-aeb7-4efd94338296</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I3a1bf3a7-f9b4-472d-ac19-c6d27f671543</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I01f17f48-6da9-44a0-bb7f-82e325f2f233</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">Ifdbe8e9c-0eeb-4019-b8a7-5599265834fb</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I0dd2d1e9-84b7-4b3f-a144-a2ec2c13061b</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I41e6c74d-787a-4620-889f-fae84be71cdb</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-member>
                    <qti-base-value base-type="identifier">I1f639c54-57ee-428e-93ed-77d786d5a2d1</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">Ib580dda7-0717-4cfc-b70e-34059b4c78f1</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I834d38a6-dac5-4235-9cf4-d323dd89f1d2</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">Ic858ebcf-6f25-46ee-be0b-afbee1f41971</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">If6a1b756-b293-4baf-a3c0-3332221e3ea8</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I80cb35ef-89d7-4ada-aef4-eda2b963a606</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">Ia2c49323-f160-4dfa-8f79-5abea876efa6</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I8ecdc331-d565-4fcc-9f1f-a83e6e6bab9d</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I4142aba3-f108-41dc-b537-ab44fd8cb5ae</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">If9dc68d4-33ec-4176-adb2-eda10373f1a0</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">Ice8f6ba6-0b24-4234-8e1c-ea2d6caf18f6</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">Ia64d8e75-aa61-481b-bd40-df7270053448</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-member>
                    <qti-base-value base-type="identifier">I2a787ac6-adce-419f-b230-293302ab6e2b</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I562dc41e-1fcd-45b2-ad63-e346a9b12ed3</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I2ff56de9-53fd-4be6-adc3-b9d48cbac0bd</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I8c49dcaf-eea6-48ad-bcf0-5c00ccde0415</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I7b431d95-a30e-403f-9b35-7575b30d31a3</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-member>
                    <qti-base-value base-type="identifier">I46f0651d-a9f2-4b96-8172-8104b0938213</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I1e40e819-c8a2-42dd-89de-dd29e070b99b</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I67b6042e-7444-46be-b169-53ec421f9eda</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I18248834-8320-4342-967c-2007195ba14a</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-member>
                    <qti-base-value base-type="identifier">I0bf4350c-9288-4cec-9e37-1b0978b057ba</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">Ifb1bfeea-20a0-4d29-bb87-d502ff355522</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">Ie1f531cd-79c9-4b87-a90c-76766792b183</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I66faad9a-d004-4cdc-a275-6f9ff736bb14</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-member>
                    <qti-base-value base-type="identifier">I665f2702-e926-4266-bc3c-3f04ea9a6c62</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I7e8b004a-0223-432a-bb9a-536918190017</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I217873b0-4970-405f-a80b-b2a9f42bbd85</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-member>
                    <qti-base-value base-type="identifier">I46521597-f1f4-432c-96c2-8e3defb05950</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">Ifbea86b5-20bc-4dde-a4e4-4a14153c8ead</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I279d3c47-95a8-42d1-9440-26fadf909090</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I6d80a45f-5042-4f42-99fe-151229268a43</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
            </qti-and>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1">
                <qti-base-value base-type="float">3</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-1">
                <qti-base-value base-type="float">3</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-2">
                <qti-base-value base-type="float">3</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2">
                <qti-base-value base-type="float">3</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2-1">
                <qti-base-value base-type="float">3</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
        <qti-response-else-if>
            <qti-and>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">Iba0772fe-878f-457c-b652-a0e922626082</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I5493e55f-fae9-499f-bd3e-16df52c028e1</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I7134c778-4641-40b5-8eae-1a80013cf777</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-member>
                    <qti-base-value base-type="identifier">I3cb9edb6-551e-474a-9122-338b8c6a396d</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I41d5e633-d892-46fe-8a0c-ceba54c167f3</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I498a96cd-114d-4299-b27c-639e41559e29</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I19b64b54-f7f4-4284-af37-37999bd24935</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I7ea195a7-b0ec-465e-ada3-31c9e940f0dc</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">Ibf683ce7-ac85-4610-b057-dd0578f3b71f</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">Ie6f1fe1f-fe23-4f29-85ec-27d483138cfa</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I2b4ed71d-8984-4ffd-a95a-126efbb12e48</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I0778d3a8-11c3-41f5-aeb7-4efd94338296</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I3a1bf3a7-f9b4-472d-ac19-c6d27f671543</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I01f17f48-6da9-44a0-bb7f-82e325f2f233</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">Ifdbe8e9c-0eeb-4019-b8a7-5599265834fb</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I0dd2d1e9-84b7-4b3f-a144-a2ec2c13061b</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I41e6c74d-787a-4620-889f-fae84be71cdb</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-member>
                    <qti-base-value base-type="identifier">I1f639c54-57ee-428e-93ed-77d786d5a2d1</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">Ib580dda7-0717-4cfc-b70e-34059b4c78f1</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I834d38a6-dac5-4235-9cf4-d323dd89f1d2</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">Ic858ebcf-6f25-46ee-be0b-afbee1f41971</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">If6a1b756-b293-4baf-a3c0-3332221e3ea8</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I80cb35ef-89d7-4ada-aef4-eda2b963a606</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-member>
                    <qti-base-value base-type="identifier">Ia2c49323-f160-4dfa-8f79-5abea876efa6</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I8ecdc331-d565-4fcc-9f1f-a83e6e6bab9d</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I4142aba3-f108-41dc-b537-ab44fd8cb5ae</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">If9dc68d4-33ec-4176-adb2-eda10373f1a0</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">Ice8f6ba6-0b24-4234-8e1c-ea2d6caf18f6</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">Ia64d8e75-aa61-481b-bd40-df7270053448</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I2a787ac6-adce-419f-b230-293302ab6e2b</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I562dc41e-1fcd-45b2-ad63-e346a9b12ed3</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I2ff56de9-53fd-4be6-adc3-b9d48cbac0bd</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I8c49dcaf-eea6-48ad-bcf0-5c00ccde0415</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I7b431d95-a30e-403f-9b35-7575b30d31a3</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-member>
                    <qti-base-value base-type="identifier">I46f0651d-a9f2-4b96-8172-8104b0938213</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I1e40e819-c8a2-42dd-89de-dd29e070b99b</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I67b6042e-7444-46be-b169-53ec421f9eda</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I18248834-8320-4342-967c-2007195ba14a</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I0bf4350c-9288-4cec-9e37-1b0978b057ba</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">Ifb1bfeea-20a0-4d29-bb87-d502ff355522</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">Ie1f531cd-79c9-4b87-a90c-76766792b183</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I66faad9a-d004-4cdc-a275-6f9ff736bb14</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-member>
                    <qti-base-value base-type="identifier">I665f2702-e926-4266-bc3c-3f04ea9a6c62</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I7e8b004a-0223-432a-bb9a-536918190017</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I217873b0-4970-405f-a80b-b2a9f42bbd85</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-member>
                    <qti-base-value base-type="identifier">I46521597-f1f4-432c-96c2-8e3defb05950</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">Ifbea86b5-20bc-4dde-a4e4-4a14153c8ead</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I279d3c47-95a8-42d1-9440-26fadf909090</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I6d80a45f-5042-4f42-99fe-151229268a43</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
            </qti-and>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-2">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
        <qti-response-else-if>
            <qti-and>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">Iba0772fe-878f-457c-b652-a0e922626082</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I5493e55f-fae9-499f-bd3e-16df52c028e1</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I7134c778-4641-40b5-8eae-1a80013cf777</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I3cb9edb6-551e-474a-9122-338b8c6a396d</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I41d5e633-d892-46fe-8a0c-ceba54c167f3</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I498a96cd-114d-4299-b27c-639e41559e29</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I19b64b54-f7f4-4284-af37-37999bd24935</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I7ea195a7-b0ec-465e-ada3-31c9e940f0dc</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">Ibf683ce7-ac85-4610-b057-dd0578f3b71f</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">Ie6f1fe1f-fe23-4f29-85ec-27d483138cfa</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I2b4ed71d-8984-4ffd-a95a-126efbb12e48</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I0778d3a8-11c3-41f5-aeb7-4efd94338296</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I3a1bf3a7-f9b4-472d-ac19-c6d27f671543</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I01f17f48-6da9-44a0-bb7f-82e325f2f233</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">Ifdbe8e9c-0eeb-4019-b8a7-5599265834fb</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I0dd2d1e9-84b7-4b3f-a144-a2ec2c13061b</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I41e6c74d-787a-4620-889f-fae84be71cdb</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-member>
                    <qti-base-value base-type="identifier">I1f639c54-57ee-428e-93ed-77d786d5a2d1</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">Ib580dda7-0717-4cfc-b70e-34059b4c78f1</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I834d38a6-dac5-4235-9cf4-d323dd89f1d2</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">Ic858ebcf-6f25-46ee-be0b-afbee1f41971</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">If6a1b756-b293-4baf-a3c0-3332221e3ea8</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I80cb35ef-89d7-4ada-aef4-eda2b963a606</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">Ia2c49323-f160-4dfa-8f79-5abea876efa6</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I8ecdc331-d565-4fcc-9f1f-a83e6e6bab9d</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I4142aba3-f108-41dc-b537-ab44fd8cb5ae</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">If9dc68d4-33ec-4176-adb2-eda10373f1a0</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">Ice8f6ba6-0b24-4234-8e1c-ea2d6caf18f6</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">Ia64d8e75-aa61-481b-bd40-df7270053448</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-member>
                    <qti-base-value base-type="identifier">I2a787ac6-adce-419f-b230-293302ab6e2b</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I562dc41e-1fcd-45b2-ad63-e346a9b12ed3</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I2ff56de9-53fd-4be6-adc3-b9d48cbac0bd</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I8c49dcaf-eea6-48ad-bcf0-5c00ccde0415</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I7b431d95-a30e-403f-9b35-7575b30d31a3</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I46f0651d-a9f2-4b96-8172-8104b0938213</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I1e40e819-c8a2-42dd-89de-dd29e070b99b</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I67b6042e-7444-46be-b169-53ec421f9eda</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I18248834-8320-4342-967c-2007195ba14a</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-member>
                    <qti-base-value base-type="identifier">I0bf4350c-9288-4cec-9e37-1b0978b057ba</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">Ifb1bfeea-20a0-4d29-bb87-d502ff355522</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">Ie1f531cd-79c9-4b87-a90c-76766792b183</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I66faad9a-d004-4cdc-a275-6f9ff736bb14</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I665f2702-e926-4266-bc3c-3f04ea9a6c62</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I7e8b004a-0223-432a-bb9a-536918190017</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I217873b0-4970-405f-a80b-b2a9f42bbd85</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I46521597-f1f4-432c-96c2-8e3defb05950</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">Ifbea86b5-20bc-4dde-a4e4-4a14153c8ead</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I279d3c47-95a8-42d1-9440-26fadf909090</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I6d80a45f-5042-4f42-99fe-151229268a43</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
            </qti-and>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-2">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
        <qti-response-else>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else>
    </qti-response-condition>
</qti-response-processing>

        ReadOnly _responseProcessing30 As XElement =
<qti-response-processing>
    <qti-response-condition>
        <qti-response-if>
            <qti-and>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">Ib580dda7-0717-4cfc-b70e-34059b4c78f1</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I834d38a6-dac5-4235-9cf4-d323dd89f1d2</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">Ic858ebcf-6f25-46ee-be0b-afbee1f41971</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">If6a1b756-b293-4baf-a3c0-3332221e3ea8</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I80cb35ef-89d7-4ada-aef4-eda2b963a606</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">Ia2c49323-f160-4dfa-8f79-5abea876efa6</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I8ecdc331-d565-4fcc-9f1f-a83e6e6bab9d</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I4142aba3-f108-41dc-b537-ab44fd8cb5ae</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">If9dc68d4-33ec-4176-adb2-eda10373f1a0</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">Ice8f6ba6-0b24-4234-8e1c-ea2d6caf18f6</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">Ia64d8e75-aa61-481b-bd40-df7270053448</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-member>
                    <qti-base-value base-type="identifier">I2a787ac6-adce-419f-b230-293302ab6e2b</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I562dc41e-1fcd-45b2-ad63-e346a9b12ed3</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I2ff56de9-53fd-4be6-adc3-b9d48cbac0bd</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I8c49dcaf-eea6-48ad-bcf0-5c00ccde0415</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I7b431d95-a30e-403f-9b35-7575b30d31a3</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-member>
                    <qti-base-value base-type="identifier">I46f0651d-a9f2-4b96-8172-8104b0938213</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
            </qti-and>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2">
                <qti-base-value base-type="float">3</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
        <qti-response-else-if>
            <qti-and>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">Ib580dda7-0717-4cfc-b70e-34059b4c78f1</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I834d38a6-dac5-4235-9cf4-d323dd89f1d2</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">Ic858ebcf-6f25-46ee-be0b-afbee1f41971</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">If6a1b756-b293-4baf-a3c0-3332221e3ea8</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I80cb35ef-89d7-4ada-aef4-eda2b963a606</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">Ia2c49323-f160-4dfa-8f79-5abea876efa6</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I8ecdc331-d565-4fcc-9f1f-a83e6e6bab9d</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I4142aba3-f108-41dc-b537-ab44fd8cb5ae</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">If9dc68d4-33ec-4176-adb2-eda10373f1a0</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">Ice8f6ba6-0b24-4234-8e1c-ea2d6caf18f6</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">Ia64d8e75-aa61-481b-bd40-df7270053448</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I2a787ac6-adce-419f-b230-293302ab6e2b</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I562dc41e-1fcd-45b2-ad63-e346a9b12ed3</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I2ff56de9-53fd-4be6-adc3-b9d48cbac0bd</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I8c49dcaf-eea6-48ad-bcf0-5c00ccde0415</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I7b431d95-a30e-403f-9b35-7575b30d31a3</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-member>
                    <qti-base-value base-type="identifier">I46f0651d-a9f2-4b96-8172-8104b0938213</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
            </qti-and>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
        <qti-response-else-if>
            <qti-and>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">Ib580dda7-0717-4cfc-b70e-34059b4c78f1</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I834d38a6-dac5-4235-9cf4-d323dd89f1d2</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">Ic858ebcf-6f25-46ee-be0b-afbee1f41971</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">If6a1b756-b293-4baf-a3c0-3332221e3ea8</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I80cb35ef-89d7-4ada-aef4-eda2b963a606</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">Ia2c49323-f160-4dfa-8f79-5abea876efa6</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I8ecdc331-d565-4fcc-9f1f-a83e6e6bab9d</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I4142aba3-f108-41dc-b537-ab44fd8cb5ae</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">If9dc68d4-33ec-4176-adb2-eda10373f1a0</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">Ice8f6ba6-0b24-4234-8e1c-ea2d6caf18f6</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">Ia64d8e75-aa61-481b-bd40-df7270053448</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-member>
                    <qti-base-value base-type="identifier">I2a787ac6-adce-419f-b230-293302ab6e2b</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I562dc41e-1fcd-45b2-ad63-e346a9b12ed3</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I2ff56de9-53fd-4be6-adc3-b9d48cbac0bd</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I8c49dcaf-eea6-48ad-bcf0-5c00ccde0415</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I7b431d95-a30e-403f-9b35-7575b30d31a3</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I46f0651d-a9f2-4b96-8172-8104b0938213</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
            </qti-and>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
    </qti-response-condition>
    <qti-response-condition>
        <qti-response-if>
            <qti-member>
                <qti-base-value base-type="identifier">I3cb9edb6-551e-474a-9122-338b8c6a396d</qti-base-value>
                <qti-variable identifier="RESPONSE"/>
            </qti-member>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE5_Concept1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
    </qti-response-condition>
    <qti-response-condition>
        <qti-response-if>
            <qti-member>
                <qti-base-value base-type="identifier">I1f639c54-57ee-428e-93ed-77d786d5a2d1</qti-base-value>
                <qti-variable identifier="RESPONSE"/>
            </qti-member>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE19_Concept1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
    </qti-response-condition>
    <qti-response-condition>
        <qti-response-if>
            <qti-member>
                <qti-base-value base-type="identifier">I0bf4350c-9288-4cec-9e37-1b0978b057ba</qti-base-value>
                <qti-variable identifier="RESPONSE"/>
            </qti-member>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE23_Concept2">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
    </qti-response-condition>
    <qti-response-condition>
        <qti-response-if>
            <qti-member>
                <qti-base-value base-type="identifier">I665f2702-e926-4266-bc3c-3f04ea9a6c62</qti-base-value>
                <qti-variable identifier="RESPONSE"/>
            </qti-member>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE27_Concept1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
    </qti-response-condition>
    <qti-response-condition>
        <qti-response-if>
            <qti-member>
                <qti-base-value base-type="identifier">I46521597-f1f4-432c-96c2-8e3defb05950</qti-base-value>
                <qti-variable identifier="RESPONSE"/>
            </qti-member>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE30_Concept1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE30_Concept1-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE30_Concept1-2">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
    </qti-response-condition>
</qti-response-processing>

        ReadOnly _responseProcessing31 As XElement =
<qti-response-processing>
    <qti-response-condition>
        <qti-response-if>
            <qti-and>
                <qti-member>
                    <qti-base-value base-type="directedPair">y_A x_1</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-member>
                    <qti-base-value base-type="directedPair">y_B x_2</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
            </qti-and>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
        <qti-response-else-if>
            <qti-and>
                <qti-member>
                    <qti-base-value base-type="directedPair">y_A x_1</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-member>
                    <qti-base-value base-type="directedPair">y_B x_1</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
            </qti-and>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
    </qti-response-condition>
    <qti-response-condition>
        <qti-response-if>
            <qti-and>
                <qti-member>
                    <qti-base-value base-type="directedPair">y_C x_1</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-member>
                    <qti-base-value base-type="directedPair">y_D x_2</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
            </qti-and>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_Concept1-2">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
        <qti-response-else-if>
            <qti-and>
                <qti-member>
                    <qti-base-value base-type="directedPair">y_C x_2</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-member>
                    <qti-base-value base-type="directedPair">y_D x_2</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
            </qti-and>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_Concept1-2">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
    </qti-response-condition>
</qti-response-processing>

        ReadOnly _responseProcessing32 As XElement =
<qti-response-processing>
    <qti-response-condition>
        <qti-response-if>
            <qti-and>
                <qti-member>
                    <qti-base-value base-type="directedPair">A HSB</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-member>
                    <qti-base-value base-type="directedPair">A HSC</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
            </qti-and>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_1-Doel-en-Publiek">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_1-2-Rekening-houden-met-verhouding-tot-lezer">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
    </qti-response-condition>
    <qti-response-condition>
        <qti-response-if>
            <qti-and>
                <qti-member>
                    <qti-base-value base-type="directedPair">B HSD</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-member>
                    <qti-base-value base-type="directedPair">B HSF</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-member>
                    <qti-base-value base-type="directedPair">A HSG</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
                <qti-member>
                    <qti-base-value base-type="directedPair">A HSI</qti-base-value>
                    <qti-variable identifier="RESPONSE"/>
                </qti-member>
            </qti-and>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_1-Doel-en-Publiek">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_1-1-Rekening-houden-met-voorkennis-lezer">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
    </qti-response-condition>
    <qti-set-outcome-value identifier="CONCEPTRESPONSE3_3-Tekststructuur">
        <qti-base-value base-type="float">1</qti-base-value>
    </qti-set-outcome-value>
    <qti-response-condition>
        <qti-response-if>
            <qti-member>
                <qti-base-value base-type="directedPair">B HSE</qti-base-value>
                <qti-variable identifier="RESPONSE"/>
            </qti-member>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE4_3-Tekststructuur">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE4_3-1-Passende-tekstindeling-en-opmaak-gebruiken">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
    </qti-response-condition>
    <qti-response-condition>
        <qti-response-if>
            <qti-member>
                <qti-base-value base-type="directedPair">A HSH</qti-base-value>
                <qti-variable identifier="RESPONSE"/>
            </qti-member>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE5_2-Tekstopbouw">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE5_2-1-Hoofd--en-bijzaken-onderscheiden">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
    </qti-response-condition>
</qti-response-processing>

        ReadOnly _responseProcessing33 As XElement =
<qti-response-processing>
    <qti-response-condition>
        <qti-response-if>
            <qti-member>
                <qti-base-value base-type="identifier">Ice5853d6-5a4b-4fc6-9298-7bc4047d0e26</qti-base-value>
                <qti-variable identifier="RESPONSE"/>
            </qti-member>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
    </qti-response-condition>
    <qti-response-condition>
        <qti-response-if>
            <qti-member>
                <qti-base-value base-type="identifier">Iff799b19-6c0e-406a-ad0d-63f470eac66f</qti-base-value>
                <qti-variable identifier="RESPONSE"/>
            </qti-member>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE3_Concept1-2">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
    </qti-response-condition>
    <qti-response-condition>
        <qti-response-if>
            <qti-member>
                <qti-base-value base-type="identifier">I9a0627b2-067c-4fde-a9a9-4aa5ecc56a4a</qti-base-value>
                <qti-variable identifier="RESPONSE"/>
            </qti-member>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE5_Concept1-2">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
    </qti-response-condition>
    <qti-response-condition>
        <qti-response-if>
            <qti-member>
                <qti-base-value base-type="identifier">I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b</qti-base-value>
                <qti-variable identifier="RESPONSE"/>
            </qti-member>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE6_Concept2">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE6_Concept2-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
    </qti-response-condition>
    <qti-response-condition>
        <qti-response-if>
            <qti-member>
                <qti-base-value base-type="identifier">I7ac4361f-5aae-4a8f-9850-a93bb81ad3a4</qti-base-value>
                <qti-variable identifier="RESPONSE"/>
            </qti-member>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE10_Concept2">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
    </qti-response-condition>
    <qti-response-condition>
        <qti-response-if>
            <qti-member>
                <qti-base-value base-type="identifier">Iad84d52a-4b1e-45b3-8067-96811e46ff7e</qti-base-value>
                <qti-variable identifier="RESPONSE"/>
            </qti-member>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE11_Concept2">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
    </qti-response-condition>
    <qti-response-condition>
        <qti-response-if>
            <qti-string-match case-sensitive="true">
                <qti-variable identifier="RESPONSE4"/>
                <qti-base-value base-type="string">drie</qti-base-value>
            </qti-string-match>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE14_Concept1-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE14_Concept1-2">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
        <qti-response-else-if>
            <qti-string-match case-sensitive="true">
                <qti-variable identifier="RESPONSE4"/>
                <qti-base-value base-type="string">water sport</qti-base-value>
            </qti-string-match>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE14_Concept1-2">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
    </qti-response-condition>
    <qti-response-condition>
        <qti-response-if>
            <qti-string-match case-sensitive="true">
                <qti-variable identifier="RESPONSE7"/>
                <qti-base-value base-type="string">zes</qti-base-value>
            </qti-string-match>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE17_Concept2">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE17_Concept2-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
    </qti-response-condition>
    <qti-response-condition>
        <qti-response-if>
            <qti-string-match case-sensitive="true">
                <qti-variable identifier="RESPONSE11"/>
                <qti-base-value base-type="string">tien</qti-base-value>
            </qti-string-match>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE21_Concept1-2">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
        <qti-response-else>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE21_Concept1-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else>
    </qti-response-condition>
    <qti-response-condition>
        <qti-response-if>
            <qti-string-match case-sensitive="true">
                <qti-variable identifier="RESPONSE12"/>
                <qti-base-value base-type="string">elf</qti-base-value>
            </qti-string-match>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE22_Concept1-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
        <qti-response-else-if>
            <qti-string-match case-sensitive="true">
                <qti-variable identifier="RESPONSE12"/>
                <qti-base-value base-type="string">elf</qti-base-value>
            </qti-string-match>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE22_Concept1-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
    </qti-response-condition>
</qti-response-processing>

        ReadOnly _responseProcessing34 As XElement =
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
                    <qti-base-value base-type="string">zeven</qti-base-value>
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
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1">
                <qti-base-value base-type="float">10</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-1">
                <qti-base-value base-type="float">11</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-2">
                <qti-base-value base-type="float">12</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2">
                <qti-base-value base-type="float">20</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2-1">
                <qti-base-value base-type="float">21</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
        <qti-response-else-if>
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
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-2">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
        <qti-response-else-if>
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
                    <qti-base-value base-type="string">zeven</qti-base-value>
                </qti-string-match>
            </qti-and>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-2">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
        <qti-response-else>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else>
    </qti-response-condition>
</qti-response-processing>

        ReadOnly _responseProcessing35 As XElement =
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
            </qti-and>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1">
                <qti-base-value base-type="float">3</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-1">
                <qti-base-value base-type="float">3</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2">
                <qti-base-value base-type="float">3</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2-1">
                <qti-base-value base-type="float">3</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
        <qti-response-else-if>
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
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
        <qti-response-else-if>
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
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
        <qti-response-else-if>
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
                <qti-not>
                    <qti-member>
                        <qti-base-value base-type="identifier">I0f93467a-f5fd-4b54-a0dc-5afd4bf5475b</qti-base-value>
                        <qti-variable identifier="RESPONSE"/>
                    </qti-member>
                </qti-not>
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
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
    </qti-response-condition>
    <qti-response-condition>
        <qti-response-if>
            <qti-string-match case-sensitive="true">
                <qti-variable identifier="RESPONSE4"/>
                <qti-base-value base-type="string">drie</qti-base-value>
            </qti-string-match>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE4_Concept1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE4_Concept1-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
        <qti-response-else-if>
            <qti-string-match case-sensitive="true">
                <qti-variable identifier="RESPONSE4"/>
                <qti-base-value base-type="string">water sport</qti-base-value>
            </qti-string-match>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE4_Concept1-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
    </qti-response-condition>
    <qti-response-condition>
        <qti-response-if>
            <qti-string-match case-sensitive="true">
                <qti-variable identifier="RESPONSE7"/>
                <qti-base-value base-type="string">zes</qti-base-value>
            </qti-string-match>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE7_Concept2">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE7_Concept2-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
    </qti-response-condition>
    <qti-response-condition>
        <qti-response-if>
            <qti-string-match case-sensitive="true">
                <qti-variable identifier="RESPONSE12"/>
                <qti-base-value base-type="string">elf</qti-base-value>
            </qti-string-match>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE12_Concept1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
        <qti-response-else-if>
            <qti-string-match case-sensitive="true">
                <qti-variable identifier="RESPONSE12"/>
                <qti-base-value base-type="string">elf</qti-base-value>
            </qti-string-match>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE12_Concept1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
    </qti-response-condition>
</qti-response-processing>

        ReadOnly _responseProcessing36 As XElement =
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
            </qti-and>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1">
                <qti-base-value base-type="float">10</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-1">
                <qti-base-value base-type="float">11</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-2">
                <qti-base-value base-type="float">12</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2">
                <qti-base-value base-type="float">20</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2-1">
                <qti-base-value base-type="float">21</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
        <qti-response-else-if>
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
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1">
                <qti-base-value base-type="float">3</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-1">
                <qti-base-value base-type="float">3</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-2">
                <qti-base-value base-type="float">3</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2">
                <qti-base-value base-type="float">3</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2-1">
                <qti-base-value base-type="float">3</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
        <qti-response-else-if>
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
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-2">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
    </qti-response-condition>
    <qti-response-condition>
        <qti-response-if>
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
                    <qti-base-value base-type="string">zeven</qti-base-value>
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
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_Concept1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_Concept1-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_Concept2">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_Concept2-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
        <qti-response-else-if>
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
                    <qti-base-value base-type="string">zeven</qti-base-value>
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
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_Concept1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_Concept1-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_Concept2">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_Concept2-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
        <qti-response-else-if>
            <qti-and>
                <qti-string-match case-sensitive="true">
                    <qti-variable identifier="RESPONSE4"/>
                    <qti-base-value base-type="string">drie</qti-base-value>
                </qti-string-match>
                <qti-string-match case-sensitive="true">
                    <qti-variable identifier="RESPONSE7"/>
                    <qti-base-value base-type="string">Bzes</qti-base-value>
                </qti-string-match>
                <qti-string-match case-sensitive="true">
                    <qti-variable identifier="RESPONSE8"/>
                    <qti-base-value base-type="string">zeven</qti-base-value>
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
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_Concept1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_Concept1-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_Concept2">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_Concept2-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
        <qti-response-else>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_Concept1-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_Concept2-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else>
    </qti-response-condition>
</qti-response-processing>

        ReadOnly _responseProcessing37 As XElement =
<qti-response-processing>
    <qti-response-condition>
        <qti-response-if>
            <qti-string-match case-sensitive="true">
                <qti-variable identifier="RESPONSE4"/>
                <qti-base-value base-type="string">drie</qti-base-value>
            </qti-string-match>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE14_Concept1">
                <qti-base-value base-type="float">0</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE14_Concept1-1">
                <qti-base-value base-type="float">0</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE14_Concept1-2">
                <qti-base-value base-type="float">0</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
        <qti-response-else>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE14_Concept1-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else>
    </qti-response-condition>
</qti-response-processing>

        ReadOnly _responseProcessing38 As XElement =
<qti-response-processing>
    <qti-response-condition>
        <qti-response-if>
            <qti-string-match case-sensitive="true">
                <qti-variable identifier="RESPONSE4"/>
                <qti-base-value base-type="string">drie</qti-base-value>
            </qti-string-match>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE14_Concept1">
                <qti-base-value base-type="float">0</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE14_Concept1-1">
                <qti-base-value base-type="float">0</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE14_Concept1-2">
                <qti-base-value base-type="float">0</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
        <qti-response-else-if>
            <qti-string-match case-sensitive="true">
                <qti-variable identifier="RESPONSE4"/>
                <qti-base-value base-type="string">water sport</qti-base-value>
            </qti-string-match>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE14_Concept1-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
        <qti-response-else>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE14_Concept1-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else>
    </qti-response-condition>
</qti-response-processing>

        ReadOnly _responseProcessing39 As XElement =
<qti-response-processing>
    <qti-response-condition>
        <qti-response-if>
            <qti-and>
                <qti-string-match case-sensitive="true">
                    <qti-variable identifier="RESPONSE4"/>
                    <qti-base-value base-type="string">drie</qti-base-value>
                </qti-string-match>
                <qti-string-match case-sensitive="true">
                    <qti-variable identifier="RESPONSE7"/>
                    <qti-base-value base-type="string">zes</qti-base-value>
                </qti-string-match>
            </qti-and>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1">
                <qti-base-value base-type="float">0</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-1">
                <qti-base-value base-type="float">0</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-2">
                <qti-base-value base-type="float">0</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2">
                <qti-base-value base-type="float">0</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2-1">
                <qti-base-value base-type="float">0</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
        <qti-response-else>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else>
    </qti-response-condition>
</qti-response-processing>

        ReadOnly _responseProcessing40 As XElement =
<qti-response-processing>
    <qti-response-condition>
        <qti-response-if>
            <qti-and>
                <qti-string-match case-sensitive="true">
                    <qti-variable identifier="RESPONSE4"/>
                    <qti-base-value base-type="string">drie</qti-base-value>
                </qti-string-match>
                <qti-string-match case-sensitive="true">
                    <qti-variable identifier="RESPONSE7"/>
                    <qti-base-value base-type="string">zes</qti-base-value>
                </qti-string-match>
            </qti-and>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1">
                <qti-base-value base-type="float">0</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-1">
                <qti-base-value base-type="float">0</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-2">
                <qti-base-value base-type="float">0</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2">
                <qti-base-value base-type="float">0</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2-1">
                <qti-base-value base-type="float">0</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
        <qti-response-else-if>
            <qti-and>
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
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
        <qti-response-else>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else>
    </qti-response-condition>
</qti-response-processing>

        ReadOnly _responseProcessing41 As XElement =
<qti-response-processing>
    <qti-response-condition>
        <qti-response-if>
            <qti-and>
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
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
    </qti-response-condition>
</qti-response-processing>

        ReadOnly _responseProcessing42 As XElement =
<qti-response-processing>
    <qti-response-condition>
        <qti-response-if>
            <qti-equal tolerance-mode="exact">
                <qti-index n="2">
                    <qti-variable identifier="RESPONSE"/>
                </qti-index>
                <qti-base-value base-type="integer">12345</qti-base-value>
            </qti-equal>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-2">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
        <qti-response-else-if>
            <qti-equal tolerance-mode="exact">
                <qti-index n="2">
                    <qti-variable identifier="RESPONSE"/>
                </qti-index>
                <qti-base-value base-type="integer">54321</qti-base-value>
            </qti-equal>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-2">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
        <qti-response-else>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-2">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else>
    </qti-response-condition>
    <qti-response-condition>
        <qti-response-if>
            <qti-member>
                <qti-base-value base-type="identifier">E</qti-base-value>
                <qti-index n="3">
                    <qti-variable identifier="RESPONSE"/>
                </qti-index>
            </qti-member>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_Concept2-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
        <qti-response-else-if>
            <qti-member>
                <qti-base-value base-type="identifier">D</qti-base-value>
                <qti-index n="3">
                    <qti-variable identifier="RESPONSE"/>
                </qti-index>
            </qti-member>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_Concept2-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
        <qti-response-else-if>
            <qti-member>
                <qti-base-value base-type="identifier">B</qti-base-value>
                <qti-index n="3">
                    <qti-variable identifier="RESPONSE"/>
                </qti-index>
            </qti-member>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_Concept2-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
    </qti-response-condition>
    <qti-response-condition>
        <qti-response-if>
            <qti-match>
                <qti-base-value base-type="identifier">C</qti-base-value>
                <qti-index n="4">
                    <qti-variable identifier="RESPONSE"/>
                </qti-index>
            </qti-match>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE3_Concept2-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
        <qti-response-else-if>
            <qti-match>
                <qti-base-value base-type="identifier">B</qti-base-value>
                <qti-index n="4">
                    <qti-variable identifier="RESPONSE"/>
                </qti-index>
            </qti-match>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE3_Concept2-1">
                <qti-base-value base-type="float">3</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
        <qti-response-else-if>
            <qti-match>
                <qti-base-value base-type="identifier">A</qti-base-value>
                <qti-index n="4">
                    <qti-variable identifier="RESPONSE"/>
                </qti-index>
            </qti-match>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE3_Concept2-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
    </qti-response-condition>
</qti-response-processing>

        ReadOnly _responseProcessing43 As XElement =
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
                    <qti-base-value base-type="identifier">B</qti-base-value>
                    <qti-index n="4">
                        <qti-variable identifier="RESPONSE"/>
                    </qti-index>
                </qti-match>
                <qti-match>
                    <qti-base-value base-type="identifier">F</qti-base-value>
                    <qti-index n="5">
                        <qti-variable identifier="RESPONSE"/>
                    </qti-index>
                </qti-match>
            </qti-and>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
        <qti-response-else-if>
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
                    <qti-base-value base-type="identifier">B</qti-base-value>
                    <qti-index n="4">
                        <qti-variable identifier="RESPONSE"/>
                    </qti-index>
                </qti-match>
                <qti-match>
                    <qti-base-value base-type="identifier">E</qti-base-value>
                    <qti-index n="5">
                        <qti-variable identifier="RESPONSE"/>
                    </qti-index>
                </qti-match>
            </qti-and>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
        <qti-response-else-if>
            <qti-and>
                <qti-equal tolerance-mode="exact">
                    <qti-index n="2">
                        <qti-variable identifier="RESPONSE"/>
                    </qti-index>
                    <qti-base-value base-type="integer">43210</qti-base-value>
                </qti-equal>
                <qti-member>
                    <qti-base-value base-type="identifier">D</qti-base-value>
                    <qti-index n="3">
                        <qti-variable identifier="RESPONSE"/>
                    </qti-index>
                </qti-member>
                <qti-match>
                    <qti-base-value base-type="identifier">B</qti-base-value>
                    <qti-index n="4">
                        <qti-variable identifier="RESPONSE"/>
                    </qti-index>
                </qti-match>
                <qti-match>
                    <qti-base-value base-type="identifier">E</qti-base-value>
                    <qti-index n="5">
                        <qti-variable identifier="RESPONSE"/>
                    </qti-index>
                </qti-match>
            </qti-and>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
    </qti-response-condition>
</qti-response-processing>

        ReadOnly _responseProcessing44 As XElement =
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
                <qti-match>
                    <qti-base-value base-type="identifier">B</qti-base-value>
                    <qti-index n="4">
                        <qti-variable identifier="RESPONSE"/>
                    </qti-index>
                </qti-match>
            </qti-and>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-2">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
        <qti-response-else-if>
            <qti-and>
                <qti-equal tolerance-mode="exact">
                    <qti-index n="2">
                        <qti-variable identifier="RESPONSE"/>
                    </qti-index>
                    <qti-base-value base-type="integer">54321</qti-base-value>
                </qti-equal>
                <qti-match>
                    <qti-base-value base-type="identifier">B</qti-base-value>
                    <qti-index n="4">
                        <qti-variable identifier="RESPONSE"/>
                    </qti-index>
                </qti-match>
            </qti-and>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-2">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
        <qti-response-else-if>
            <qti-and>
                <qti-equal tolerance-mode="exact">
                    <qti-index n="2">
                        <qti-variable identifier="RESPONSE"/>
                    </qti-index>
                    <qti-base-value base-type="integer">43210</qti-base-value>
                </qti-equal>
                <qti-match>
                    <qti-base-value base-type="identifier">B</qti-base-value>
                    <qti-index n="4">
                        <qti-variable identifier="RESPONSE"/>
                    </qti-index>
                </qti-match>
            </qti-and>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept1-2">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
        <qti-response-else>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE_Concept2-1">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else>
    </qti-response-condition>
    <qti-response-condition>
        <qti-response-if>
            <qti-member>
                <qti-base-value base-type="identifier">D</qti-base-value>
                <qti-index n="3">
                    <qti-variable identifier="RESPONSE"/>
                </qti-index>
            </qti-member>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_Concept2">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_Concept2-1">
                <qti-base-value base-type="float">2</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
        <qti-response-else-if>
            <qti-member>
                <qti-base-value base-type="identifier">B</qti-base-value>
                <qti-index n="3">
                    <qti-variable identifier="RESPONSE"/>
                </qti-index>
            </qti-member>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE2_Concept2">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-else-if>
    </qti-response-condition>
    <qti-response-condition>
        <qti-response-if>
            <qti-match>
                <qti-base-value base-type="identifier">F</qti-base-value>
                <qti-index n="5">
                    <qti-variable identifier="RESPONSE"/>
                </qti-index>
            </qti-match>
            <qti-set-outcome-value identifier="CONCEPTRESPONSE3_Concept2">
                <qti-base-value base-type="float">1</qti-base-value>
            </qti-set-outcome-value>
        </qti-response-if>
    </qti-response-condition>
</qti-response-processing>

#End Region

    End Class

End Namespace