
Imports System.Xml.Serialization
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ContentModel
Imports System.Linq
Imports System.Xml.Linq
Imports System.IO
Imports Questify.Builder.UnitTests.Framework

<TestClass()>
Public Class ScoreFixTests

    <TestMethod(), TestCategory("Scoring"), TestCategory("Scoring"), Description("Test whether a valid solution is modified. Expected: Not modified.")>
    Public Sub ValidSolution_NotModified()
        'Arrange
        Dim xmlSerializer As New XmlSerializer(GetType(AssessmentItem))
        Dim item = xmlSerializer.Deserialize(Of AssessmentItem)(My.Resources.InlineChoice_SingleAnswer)
        Dim solution = item.Solution
        Dim scoringParameters As HashSet(Of ScoringParameter) = item.Parameters.DeepFetchInlineScoringParameters()
        Dim factsBeforeRemoval = solution.Findings(0).Facts.Count

        'Act
        solution.FixRemovedScoringParameters(scoringParameters)

        'Assert
        Assert.AreEqual(2, solution.Findings(0).Facts.Count)
        Assert.AreEqual(factsBeforeRemoval, solution.Findings(0).Facts.Count)
    End Sub

    <TestMethod(), TestCategory("Scoring"), Description("Test whether a broken solution (with invalid facts) is fixed.")>
    Public Sub RemoveScoringParameter_SolutionIsFixed_SingleAnswer()
        'Arrange
        Dim xmlSerializer As New XmlSerializer(GetType(AssessmentItem))
        Dim item = xmlSerializer.Deserialize(Of AssessmentItem)(My.Resources.InlineChoice_SingleAnswer)
        Dim solution = item.Solution

        Dim scoringParameters As HashSet(Of ScoringParameter) = item.Parameters.DeepFetchInlineScoringParameters()
        scoringParameters.RemoveWhere(Function(p) p.InlineId = "Ibb37a53e-43d7-49e6-ab4a-fb967ecba6cc")

        Dim factsBeforeRemoval = solution.Findings(0).Facts.Count

        'Act
        solution.FixRemovedScoringParameters(scoringParameters)

        'Assert
        Assert.AreEqual(1, solution.Findings(0).Facts.Count)
        Assert.AreNotEqual(factsBeforeRemoval, solution.Findings(0).Facts.Count)
    End Sub

    <TestMethod(), TestCategory("Scoring"), Description("Test whether a broken solution (with invalid findings) is fixed.")>
    Public Sub FindingIdNotFound_SolutionIsFixed_FindingIsRemoved()
        'Arrange
        Dim xmlSerializer As New XmlSerializer(GetType(AssessmentItem))
        Dim item = xmlSerializer.Deserialize(Of AssessmentItem)(My.Resources.InlineChoice_SingleAnswer)
        Dim solution = item.Solution

        solution.Findings(0).Id = "Vraag"
        Dim scoringParameters As HashSet(Of ScoringParameter) = item.Parameters.DeepFetchInlineScoringParameters()

        Dim findingsBeforeRemoval = solution.Findings.Count

        'Act
        solution.FixRemovedScoringParameters(scoringParameters)

        'Assert
        Assert.AreEqual(0, solution.Findings.Count)
        Assert.AreNotEqual(findingsBeforeRemoval, solution.Findings.Count)
    End Sub

    <TestMethod(), TestCategory("Scoring"), Description("Test whether a broken solution (with multiple answers) is fixed.")>
    Public Sub RemoveScoringParameter_FixGroupedSolution_SolutionIsFixedAndUngrouped()
        'Arrange
        Dim xmlSerializer As New XmlSerializer(GetType(AssessmentItem))
        Dim item = xmlSerializer.Deserialize(Of AssessmentItem)(My.Resources.InlineChoice_MultipleAnswer)
        Dim solution = item.Solution

        Dim scoringParameters As HashSet(Of ScoringParameter) = item.Parameters.DeepFetchInlineScoringParameters()
        scoringParameters.RemoveWhere(Function(p) p.InlineId = "Ibb37a53e-43d7-49e6-ab4a-fb967ecba6cc")

        solution.WriteToDebug("Arrange")

        'Act
        solution.FixRemovedScoringParameters(scoringParameters)

        'Assert
        solution.WriteToDebug("Assert")
        Assert.AreEqual(0, solution.Findings(0).KeyFactsets.Count, "Expected no KeyFactSets, the facts should be moved to the finding.facts ")
        Assert.AreEqual(2, solution.Findings(0).Facts.Count)
    End Sub

    <TestMethod(), TestCategory("Scoring"), TestCategory("Scoring"), Description("Test whether a broken solution (with empty facts) is fixed.")>
    Public Sub InvalidSolutionWithEmptyFacts_SolutionIsFixed()
        'Arrange
        Dim xmlSerializer As New XmlSerializer(GetType(AssessmentItem))
        Dim item = xmlSerializer.Deserialize(Of AssessmentItem)(My.Resources.InlineChoice_EmptyFacts)
        Dim solution = item.Solution

        Dim scoringParameters As HashSet(Of ScoringParameter) = item.Parameters.DeepFetchInlineScoringParameters()
        Dim factsBeforeRemoval = solution.Findings(0).Facts.Count

        'Act
        solution.FixRemovedScoringParameters(scoringParameters)

        'Assert
        Assert.AreEqual(0, solution.Findings(0).Facts.Count)
        Assert.AreNotEqual(factsBeforeRemoval, solution.Findings(0).Facts.Count)
    End Sub

    <TestMethod(), TestCategory("Scoring"), Description("Correct fix of grouped scoring when removing alternatives from score parameter")>
    Public Sub SolutionWithRemovedIntegerAlternatives_FactsFor4and5_areRemoved_factsFor3AreMoved()
        'Arrange
        Dim xmlSerializer As New XmlSerializer(GetType(AssessmentItem))
        Dim item = xmlSerializer.Deserialize(Of AssessmentItem)(My.Resources.IntegerGrouping)
        Dim solution = item.Solution

        Dim scoringParameters As HashSet(Of ScoringParameter) = item.Parameters.DeepFetchInlineScoringParameters()
        solution.WriteToDebug("Arrange")

        'Act
        solution.FixRemovedScoringParameters(scoringParameters)

        'Assert
        solution.WriteToDebug("Assert")
        Assert.AreEqual(1, solution.Findings(0).Facts.Count, "Expecting a single fact to be found in the finding")
        Assert.AreEqual(2, solution.Findings(0).KeyFactsets.Count, "Expecting 2 fact sets")

        Dim keyFact As KeyFact = DirectCast(solution.Findings(0).Facts(0), KeyFact)
        Assert.AreEqual(1, keyFact.Values.Count, "Fact should have a single value collection,.. with 2 values")

        Dim keyValue As KeyValue = DirectCast(solution.Findings(0).Facts(0).Values(0), KeyValue)
        Assert.AreEqual(2, keyValue.Values.Count)
        Assert.IsTrue(keyValue.Values.Any(Function(v) v.IsMatch(New IntegerValue(3))))
        Assert.IsTrue(keyValue.Values.Any(Function(v) v.IsMatch(New IntegerValue(7))))
    End Sub

    <TestMethod(), TestCategory("Scoring"), Description("Correct fix of grouped scoring when removing alternatives (Multi response")>
    Public Sub SolutionWithRemovedMRAlternatives_SolutionIsFixed()
        'Arrange
        Dim xmlSerializer As New XmlSerializer(GetType(AssessmentItem))
        Dim item = xmlSerializer.Deserialize(Of AssessmentItem)(My.Resources.MRGroupedRemovedAlternative)
        Dim solution = item.Solution

        Dim scoringParameters As HashSet(Of ScoringParameter) = item.Parameters.DeepFetchInlineScoringParameters()
        solution.WriteToDebug("Arrange")

        'Act
        solution.FixRemovedScoringParameters(scoringParameters)

        'Assert
        solution.WriteToDebug("Assert")
        Assert.AreEqual(2, solution.Findings(0).Facts.Count)
        Assert.AreEqual(0, solution.Findings(0).KeyFactsets.Count)

        Dim keyFact As KeyFact = DirectCast(solution.Findings(0).Facts(0), KeyFact)
        Assert.AreEqual(1, keyFact.Values.Count)

        Dim keyValue As KeyValue = DirectCast(solution.Findings(0).Facts(0).Values(0), KeyValue)
        Assert.AreEqual(1, keyValue.Values.Count)
        keyValue = DirectCast(solution.Findings(0).Facts(1).Values(0), KeyValue)
        Assert.AreEqual(1, keyValue.Values.Count)
    End Sub

    <TestMethod(), TestCategory("Scoring")>
    Public Sub ConceptSolution_AnswerCategoriesFacts_ShouldNotBeRemoved()
        'Answer fact Ids look something like: 1[1]-Ice32c0ba-73db-456d-b3d3-c92265282cf7
        'Arrange
        Dim solution = _conceptSolution1.To(Of Solution)()

        Dim scoringParameters = New ScoringParameter() {
            New StringScoringParameter() With {.Name = "str1", .FindingOverride = "gapController", .InlineId = "Ice32c0ba-73db-456d-b3d3-c92265282cf7"}.AddSubParameters("1"),
            New StringScoringParameter() With {.Name = "str2", .FindingOverride = "gapController", .InlineId = "If62b4c70-e785-4b2d-892f-5ecaea3824a3"}.AddSubParameters("1")
            }
        solution.WriteToDebug("arrange")
        
        'Act
        solution.FixRemovedScoringParameters(scoringParameters)

        'Assert
        solution.WriteToDebug("Assert")
        Assert.AreEqual(0, solution.ConceptFindings(0).Facts.Count, "No facts on finding level expected")
        Assert.AreEqual(2, solution.ConceptFindings(0).KeyFactsets.Count, "2 factsets expected")
    End Sub

    Protected Function Deserialize(Of T)(input As XElement) As T
        Dim ret As T
        Dim s = New XmlSerializer(GetType(T))

        Using m As New StringReader(input.ToString())
            ret = DirectCast(s.Deserialize(m), T)
        End Using

        Return ret
    End Function

    Private ReadOnly _serializedGapMatchItem As XElement = <assessmentItem xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" identifier="2001" title="2001" layoutTemplateSrc="ilt.gapmatch">
                                                               <solution>
                                                                   <keyFindings>
                                                                       <keyFinding scoringMethod="Dichotomous" id="gapMatchController">
                                                                           <keyFact id="1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                               <keyValue domain="1" occur="1">
                                                                                   <stringValue>
                                                                                       <typedValue></typedValue>
                                                                                   </stringValue>
                                                                               </keyValue>
                                                                           </keyFact>
                                                                           <keyFact id="2" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                               <keyValue domain="2" occur="1">
                                                                                   <stringValue>
                                                                                       <typedValue>I57c72f50-cf3a-499f-9473-3bd398a13282</typedValue>
                                                                                   </stringValue>
                                                                               </keyValue>
                                                                           </keyFact>
                                                                           <keyFact id="3" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                               <keyValue domain="3" occur="1">
                                                                                   <stringValue>
                                                                                       <typedValue>I68a92575-2ae9-4cfc-8ce2-284844f72fa9</typedValue>
                                                                                   </stringValue>
                                                                               </keyValue>
                                                                           </keyFact>
                                                                           <keyFact id="4" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                               <keyValue domain="4" occur="1">
                                                                                   <stringValue>
                                                                                       <typedValue></typedValue>
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
                                                               <parameters>
                                                                   <parameterSet id="invoer">
                                                                       <gapMatchScoringParameter name="domainx" findingOverride="gapMatchController">
                                                                           <subparameterset id="1">
                                                                               <gapTextParameter name="gapText" matchMax="2">winter</gapTextParameter>
                                                                           </subparameterset>
                                                                           <subparameterset id="2">
                                                                               <gapTextParameter name="gapText" matchMax="2">spring</gapTextParameter>
                                                                           </subparameterset>
                                                                           <subparameterset id="3">
                                                                               <gapTextParameter name="gapText" matchMax="1">summer</gapTextParameter>
                                                                           </subparameterset>
                                                                           <subparameterset id="4">
                                                                               <gapTextParameter name="gapText" matchMax="1">autumn</gapTextParameter>
                                                                           </subparameterset>
                                                                           <definition id="">
                                                                               <gapTextParameter name="gapText" matchMax="1"/>
                                                                           </definition>
                                                                           <xhtmlParameter name="itemInlineInput">
                                                                               <p id="c1-id-9" xmlns="http://www.w3.org/1999/xhtml">
            Now is the <cito:InlineElement id="I68a92575-2ae9-4cfc-8ce2-284844f72fa9" layoutTemplateSourceName="InlineGapMatchLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                                                       <cito:parameters>
                                                                                           <cito:parameterSet id="entireItem">
                                                                                               <cito:plaintextparameter name="inlineGapMatchId">I68a92575-2ae9-4cfc-8ce2-284844f72fa9</cito:plaintextparameter>
                                                                                               <cito:plaintextparameter name="inlineGapMatchLabel">Gat 1</cito:plaintextparameter>
                                                                                           </cito:parameterSet>
                                                                                       </cito:parameters>
                                                                                   </cito:InlineElement><br id="c1-id-11"/>of our discontent<br id="c1-id-12"/>Made glorious <cito:InlineElement id="I57c72f50-cf3a-499f-9473-3bd398a13282" layoutTemplateSourceName="InlineGapMatchLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                                                       <cito:parameters>
                                                                                           <cito:parameterSet id="entireItem">
                                                                                               <cito:plaintextparameter name="inlineGapMatchId">I57c72f50-cf3a-499f-9473-3bd398a13282</cito:plaintextparameter>
                                                                                               <cito:plaintextparameter name="inlineGapMatchLabel">Gat 2</cito:plaintextparameter>
                                                                                           </cito:parameterSet>
                                                                                       </cito:parameters>
                                                                                   </cito:InlineElement><br id="c1-id-14"/>by this sun of York;<br id="c1-id-15"/>And all the clouds that lour'd upon our house<br id="c1-id-16"/>In the deep bosom of the ocean buried.
          </p>
                                                                           </xhtmlParameter>
                                                                       </gapMatchScoringParameter>
                                                                   </parameterSet>
                                                               </parameters>
                                                           </assessmentItem>

    ReadOnly _conceptSolution1 As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                                 <keyFindings>
                                                     <keyFinding id="gapController" scoringMethod="None">
                                                         <keyFactSet>
                                                             <keyFact id="1-Ice32c0ba-73db-456d-b3d3-c92265282cf7" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                 <keyValue domain="1-Ice32c0ba-73db-456d-b3d3-c92265282cf7" occur="1">
                                                                     <stringValue>
                                                                         <typedValue>1</typedValue>
                                                                     </stringValue>
                                                                 </keyValue>
                                                             </keyFact>
                                                             <keyFact id="1-If62b4c70-e785-4b2d-892f-5ecaea3824a3" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                 <keyValue domain="1-If62b4c70-e785-4b2d-892f-5ecaea3824a3" occur="1">
                                                                     <stringValue>
                                                                         <typedValue>2</typedValue>
                                                                     </stringValue>
                                                                 </keyValue>
                                                             </keyFact>
                                                         </keyFactSet>
                                                     </keyFinding>
                                                 </keyFindings>
                                                 <conceptFindings>
                                                     <conceptFinding id="gapController" scoringMethod="None">
                                                         <conceptFactSet>
                                                             <conceptFact id="1-Ice32c0ba-73db-456d-b3d3-c92265282cf7" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                 <conceptValue domain="1-Ice32c0ba-73db-456d-b3d3-c92265282cf7" occur="1">
                                                                     <stringValue>
                                                                         <typedValue>1</typedValue>
                                                                     </stringValue>
                                                                 </conceptValue>
                                                                 <concepts/>
                                                             </conceptFact>
                                                             <conceptFact id="1-If62b4c70-e785-4b2d-892f-5ecaea3824a3" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                 <conceptValue domain="1-If62b4c70-e785-4b2d-892f-5ecaea3824a3" occur="1">
                                                                     <stringValue>
                                                                         <typedValue>2</typedValue>
                                                                     </stringValue>
                                                                 </conceptValue>
                                                                 <concepts/>
                                                             </conceptFact>
                                                             <concepts xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                                         </conceptFactSet>
                                                         <conceptFactSet>
                                                             <conceptFact id="1[1]-Ice32c0ba-73db-456d-b3d3-c92265282cf7" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                 <conceptValue domain="1[1]-Ice32c0ba-73db-456d-b3d3-c92265282cf7" occur="1">
                                                                     <stringValue>
                                                                         <typedValue>TestValue1</typedValue>
                                                                     </stringValue>
                                                                 </conceptValue>
                                                             </conceptFact>
                                                             <conceptFact id="1[1]-If62b4c70-e785-4b2d-892f-5ecaea3824a3" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                 <conceptValue domain="1[1]-If62b4c70-e785-4b2d-892f-5ecaea3824a3" occur="1">
                                                                     <stringValue>
                                                                         <typedValue>TestValue2</typedValue>
                                                                     </stringValue>
                                                                 </conceptValue>
                                                             </conceptFact>
                                                         </conceptFactSet>
                                                     </conceptFinding>
                                                 </conceptFindings>
                                                 <aspectReferences/>
                                             </solution>

End Class
