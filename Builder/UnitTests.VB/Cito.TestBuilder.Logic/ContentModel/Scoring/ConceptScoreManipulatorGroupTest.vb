
Imports System.Xml.Linq
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ContentModel.Scoring
Imports System.Linq
Imports Questify.Builder.Logic.ContentModel
Imports System.Xml.Serialization
Imports System.IO
Imports System.Diagnostics
Imports Questify.Builder.UnitTests.Framework

<TestClass>
Public Class ConceptScoreManipulatorGroupTest
    Const CPartName As String = "part Name"

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Concept")>
    Public Sub SetConceptScores()
        Dim solution = Deserialize(Of Solution)(_withFactSets_AIs3_BIs7_or_AIs7_BIs3) _
        Dim integerPrmA =
        New IntegerScoringParameter() _
        With {.FindingOverride = "finding", .ControllerId = "FieldA", .Name = "Field A"}.AddSubParameters("A")
        Dim integerPrmB =
                New IntegerScoringParameter() _
                With {.FindingOverride = "finding", .ControllerId = "FieldB", .Name = "Field B"}.AddSubParameters("A")

        Dim scoreMap = New ScoringMap(New ScoringParameter() {integerPrmA, integerPrmB}, solution).GetMap().First()
        Dim manipulator = scoreMap.GetConceptManipulator(solution)

        WriteSolution("BeforeAct", solution)
        manipulator.SetScore(CPartName, "0", 42)
        WriteSolution("AfterAct", solution)

        Assert.IsNotNull(solution.ConceptFindings, "Expected ConceptFindings to be initialized")
        Assert.AreEqual(1, solution.ConceptFindings.Count, "Expected precisly 1 conceptFinding")
        Assert.AreEqual(2 + 1, solution.ConceptFindings(0).KeyFactsets.Count,
                        "Just like the original we expecting 2 fact sets, but one catch all set.")

        Dim conceptFactSet = DirectCast(solution.ConceptFindings(0).KeyFactsets(0), ConceptFactsSet)
        Assert.IsNotNull(conceptFactSet.Concepts)

        Assert.AreEqual(1, conceptFactSet.Concepts.Count, "Expected 1 concept")
        Assert.AreEqual(42, conceptFactSet.Concepts(0).Value, "Expected 42 as score for concept")
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Concept")>
    Public Sub GetConceptIds()
        Dim solution = Deserialize(Of Solution)(_withFactSets_AIs3_BIs7_or_AIs7_BIs3_AndConceptScoringFirstSet) _
        Dim integerPrmA =
        New IntegerScoringParameter() _
        With {.FindingOverride = "finding", .ControllerId = "FieldA", .Name = "Field A"}.AddSubParameters("A")
        Dim integerPrmB =
                New IntegerScoringParameter() _
                With {.FindingOverride = "finding", .ControllerId = "FieldB", .Name = "Field B"}.AddSubParameters("A")

        Dim scoreMap = New ScoringMap(New ScoringParameter() {integerPrmA, integerPrmB}, solution).GetMap().First()
        Dim manipulator = scoreMap.GetConceptManipulator(solution)

        Dim result = manipulator.GetConceptIds().ToList()

        Assert.AreEqual(2 + 1, result.Count)
        Assert.AreEqual("0", result(0))
        Assert.AreEqual("1", result(1))
        Assert.AreEqual("2", result(2))
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Concept")>
    Public Sub GetDisplayValueForFact()
        Dim solution = Deserialize(Of Solution)(_withFactSets_AIs3_BIs7_or_AIs7_BIs3_AndConceptScoringFirstSet) _
        Dim integerPrmA =
        New IntegerScoringParameter() _
        With {.FindingOverride = "finding", .ControllerId = "FieldA", .Name = "Field A"}.AddSubParameters("A")
        Dim integerPrmB =
                New IntegerScoringParameter() _
                With {.FindingOverride = "finding", .ControllerId = "FieldB", .Name = "Field B"}.AddSubParameters("A")

        Dim scoreMap = New ScoringMap(New ScoringParameter() {integerPrmA, integerPrmB}, solution).GetMap().First()
        Dim manipulator = scoreMap.GetConceptManipulator(solution)

        Dim result = manipulator.GetDisplayValueForConceptId("1")

        Assert.AreEqual("7&3", result)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Concept")>
    Public Sub GetScoreForPart_Expects_42_null()
        Dim solution = Deserialize(Of Solution)(_withFactSets_AIs3_BIs7_or_AIs7_BIs3_AndConceptScoringFirstSet) _
        Dim integerPrmA =
        New IntegerScoringParameter() _
        With {.FindingOverride = "finding", .ControllerId = "FieldA", .Name = "Field A"}.AddSubParameters("A")
        Dim integerPrmB =
                New IntegerScoringParameter() _
                With {.FindingOverride = "finding", .ControllerId = "FieldB", .Name = "Field B"}.AddSubParameters("A")

        Dim scoreMap = New ScoringMap(New ScoringParameter() {integerPrmA, integerPrmB}, solution).GetMap().First()
        Dim manipulator = scoreMap.GetConceptManipulator(solution)

        Dim result = manipulator.GetScoreForPart(CPartName, New String() {"0", "1"})

        Assert.AreEqual(2, result.Count)
        Assert.AreEqual(42, result(0))
        Assert.IsNull(result(1))
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("Concept")>
    Public Sub GetConceptIdsFromNonSetInSetExample()
        Dim solution = Deserialize(Of Solution)(TwoFactSets_1FactOnFinding) _
        Dim integerPrmA =
        New IntegerScoringParameter() _
        With {.FindingOverride = "sharedIntegerFinding", .ControllerId = "integerScore", .Name = "integerScore"} _
        .AddSubParameters("1", "2", "3")


        Dim combinedScoringMapKey_NotIngroup =
                New ScoringMap(New ScoringParameter() {integerPrmA}, solution).GetMap().Where(
                    Function(scoreKey) Not scoreKey.IsGroup).FirstOrDefault()
        Dim manipulator = combinedScoringMapKey_NotIngroup.GetConceptManipulator(solution)

        Dim result = manipulator.GetConceptIds()

        Assert.AreEqual(1 + 1, result.Count)
        Assert.AreEqual("3", result(0))
        Assert.AreEqual("3[*]", result(1))
    End Sub



    Private _withFactSets_AIs3_BIs7_or_AIs7_BIs3 As XElement =
                <solution>
                    <keyFindings>
                        <keyFinding id="finding">

                            <keyFactSet>
                                <keyFact id="A-FieldA" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                    <keyValue domain="FieldA" occur="1">
                                        <integerValue>
                                            <typedValue>3</typedValue>
                                        </integerValue>
                                    </keyValue>
                                </keyFact>
                                <keyFact id="A-FieldB" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                    <keyValue domain="FieldB" occur="1">
                                        <integerValue>
                                            <typedValue>7</typedValue>
                                        </integerValue>
                                    </keyValue>
                                </keyFact>
                            </keyFactSet>

                            <keyFactSet>
                                <keyFact id="A-FieldA" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                    <keyValue domain="FieldA" occur="1">
                                        <integerValue>
                                            <typedValue>7</typedValue>
                                        </integerValue>
                                    </keyValue>
                                </keyFact>
                                <keyFact id="A-FieldB" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                    <keyValue domain="FieldB" occur="1">
                                        <integerValue>
                                            <typedValue>3</typedValue>
                                        </integerValue>
                                    </keyValue>
                                </keyFact>
                            </keyFactSet>

                        </keyFinding>
                    </keyFindings>
                </solution>

    Private _withFactSets_AIs3_BIs7_or_AIs7_BIs3_AndConceptScoringFirstSet As XElement =
            <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                <keyFindings>
                    <keyFinding id="finding" scoringMethod="None">
                        <keyFactSet>
                            <keyFact id="A-FieldA" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="FieldA" occur="1">
                                    <integerValue>
                                        <typedValue>3</typedValue>
                                    </integerValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="A-FieldB" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="FieldB" occur="1">
                                    <integerValue>
                                        <typedValue>7</typedValue>
                                    </integerValue>
                                </keyValue>
                            </keyFact>
                        </keyFactSet>
                        <keyFactSet>
                            <keyFact id="A-FieldA" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="FieldA" occur="1">
                                    <integerValue>
                                        <typedValue>7</typedValue>
                                    </integerValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="A-FieldB" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="FieldB" occur="1">
                                    <integerValue>
                                        <typedValue>3</typedValue>
                                    </integerValue>
                                </keyValue>
                            </keyFact>
                        </keyFactSet>
                    </keyFinding>
                </keyFindings>
                <conceptFindings>
                    <conceptFinding id="finding" scoringMethod="None">
                        <conceptFactSet>
                            <conceptFact id="A-FieldA" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="A-FieldA" occur="1">
                                    <integerValue>
                                        <typedValue>3</typedValue>
                                    </integerValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="A-FieldB" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="A-FieldB" occur="1">
                                    <integerValue>
                                        <typedValue>7</typedValue>
                                    </integerValue>
                                </conceptValue>
                            </conceptFact>
                            <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                                <concept value="42" code="part Name"/>
                            </concepts>
                        </conceptFactSet>
                        <conceptFactSet>
                            <conceptFact id="A-FieldA" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="A-FieldA" occur="1">
                                    <integerValue>
                                        <typedValue>7</typedValue>
                                    </integerValue>
                                </conceptValue>
                            </conceptFact>
                            <conceptFact id="A-FieldB" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <conceptValue domain="A-FieldB" occur="1">
                                    <integerValue>
                                        <typedValue>3</typedValue>
                                    </integerValue>
                                </conceptValue>
                            </conceptFact>
                        </conceptFactSet>
                    </conceptFinding>
                </conceptFindings>
                <aspectReferences/>
            </solution>

    Private TwoFactSets_1FactOnFinding As XElement = <solution>
                                                         <keyFindings>
                                                             <keyFinding id="sharedIntegerFinding" scoringMethod="Dichotomous">
                                                                 <keyFact id="3-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                     <keyValue domain="integerScore" occur="1">
                                                                         <integerValue>
                                                                             <typedValue>84</typedValue>
                                                                         </integerValue>
                                                                     </keyValue>
                                                                 </keyFact>
                                                                 <keyFactSet>
                                                                     <keyFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                         <keyValue domain="integerScore" occur="1">
                                                                             <integerValue>
                                                                                 <typedValue>6</typedValue>
                                                                             </integerValue>
                                                                         </keyValue>
                                                                     </keyFact>
                                                                     <keyFact id="2-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                         <keyValue domain="integerScore" occur="1">
                                                                             <integerValue>
                                                                                 <typedValue>14</typedValue>
                                                                             </integerValue>
                                                                         </keyValue>
                                                                     </keyFact>
                                                                 </keyFactSet>
                                                                 <keyFactSet>
                                                                     <keyFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                         <keyValue domain="integerScore" occur="1">
                                                                             <integerValue>
                                                                                 <typedValue>14</typedValue>
                                                                             </integerValue>
                                                                         </keyValue>
                                                                     </keyFact>
                                                                     <keyFact id="2-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                         <keyValue domain="integerScore" occur="1">
                                                                             <integerValue>
                                                                                 <typedValue>6</typedValue>
                                                                             </integerValue>
                                                                         </keyValue>
                                                                     </keyFact>
                                                                 </keyFactSet>
                                                             </keyFinding>
                                                         </keyFindings>
                                                         <aspectReferences/>
                                                     </solution>



    Function DoSerialize(Of T)(obj As T) As XElement
        Dim s = New XmlSerializer(GetType(T))
        Dim ret As XElement = Nothing
        Using m As New StringWriter()
            s.Serialize(m, obj)
            ret = XElement.Parse(m.ToString())
        End Using
        Return ret
    End Function

    Protected Function Deserialize(Of T)(input As XElement) As T
        Dim ret As T
        Dim s = New XmlSerializer(GetType(T))

        Using m As New StringReader(input.ToString())
            ret = DirectCast(s.Deserialize(m), T)
        End Using

        Return ret
    End Function

    Sub WriteSolution(stateName As String, s As Solution)
        Dim a As New XmlSerializer(GetType(Solution))
        Debug.WriteLine(String.Empty)
        Debug.WriteLine(String.Format("WriteSolution for State [{0}]", stateName))
        Using stream = New StringWriter()
            a.Serialize(stream, s)
            Debug.WriteLine(stream.ToString())
        End Using
    End Sub
End Class
