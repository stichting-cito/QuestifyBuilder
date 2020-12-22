
Imports System.Xml.Linq
Imports Cito.Tester.ContentModel
Imports Cito.Tester.ContentModel.ResponseAndScoringModel.Solution.ConcreteScoring

<TestClass()>
Public Class String_Scoring
    Inherits ScoringTestBase

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub MaxScore()
        Dim solution = toSolution(string_Example)

        Dim result = solution.MaxSolutionRawScore

        Assert.AreEqual(1, result)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub GiveCorrectAnswer_SkipResponseFacts()
        Dim solution = toSolution(string_Example)
        Dim valuesToCreate As New List(Of Tuple(Of String, String)) From {New Tuple(Of String, String)("C", "A"), New Tuple(Of String, String)("A", "B"), New Tuple(Of String, String)("B", "C")}
        Dim r = GetResponse(valuesToCreate, "shapeInteractionController")

        Write("Response", "Arrange", r)

        Dim result = solution.ScoreSolution(r)

        Assert.AreEqual(1, result)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub GiveCorrectAnswer_UseResponseFactFromUnitTestThatErrors()
        Dim solution = toSolution(string_Example)
        Dim response = toResponse(failing_unitTestResponse)
        Write("Response", "Arrange", response)

        Dim result = solution.ScoreSolution(response)

        Assert.AreEqual(1, result)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub FourGaps_ThreeKeyFacts_CorrectResponses_ShouldScore1()

        Dim solution = toSolution(string_Example)
        Dim r As New Response()
        Dim rF As New ResponseFinding(id:="shapeInteractionController")
        Dim respFact As New ResponseFact() With {.Id = "A"}
        respFact.Values.Add(createResponseValue(value:="C", domain:="A"))
        rF.Facts.Add(respFact)
        respFact = New ResponseFact() With {.Id = "B"}
        respFact.Values.Add(createResponseValue(value:="A", domain:="B"))
        rF.Facts.Add(respFact)
        respFact = New ResponseFact() With {.Id = "C"}
        respFact.Values.Add(createResponseValue(value:="B", domain:="C"))
        rF.Facts.Add(respFact)
        respFact = New ResponseFact() With {.Id = "D"}
        respFact.Values.Add(createResponseValue(value:="D", domain:="D"))
        rF.Facts.Add(respFact) : r.Findings.Add(rF)

        Write("Response", "Arrange", r)

        Dim result = solution.ScoreSolution(r)

        If ScoringMethod IsNot Nothing AndAlso ScoringMethod.Equals(ScoringFactory.Methods.V23_Scoring) Then
            Assert.AreEqual(0, result)
        Else
            Assert.AreEqual(1, result)
        End If
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub FourGaps_ThreeKeyFacts_IncorrectResponses_ShouldScore0()

        Dim solution = toSolution(string_Example)
        Dim r As New Response()
        Dim rF As New ResponseFinding(id:="shapeInteractionController")
        Dim respFact As New ResponseFact() With {.Id = "A"}
        respFact.Values.Add(createResponseValue(value:="C", domain:="A"))
        rF.Facts.Add(respFact)
        respFact = New ResponseFact() With {.Id = "B"}
        respFact.Values.Add(createResponseValue(value:="A", domain:="B"))
        rF.Facts.Add(respFact)
        respFact = New ResponseFact() With {.Id = "C"}
        respFact.Values.Add(createResponseValue(value:="C", domain:="C"))
        rF.Facts.Add(respFact)
        respFact = New ResponseFact() With {.Id = "D"}
        respFact.Values.Add(createResponseValue(value:="D", domain:="D"))
        rF.Facts.Add(respFact) : r.Findings.Add(rF)

        Write("Response", "Arrange", r)

        Dim result = solution.ScoreSolution(r)

        Assert.AreEqual(0, result)
    End Sub

    Private string_Example As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                             <keyFindings>
                                                 <keyFinding id="shapeInteractionController" scoringMethod="Dichotomous">
                                                     <keyFact id="A" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                         <keyValue domain="A" occur="1">
                                                             <stringValue>
                                                                 <typedValue>C</typedValue>
                                                             </stringValue>
                                                         </keyValue>
                                                     </keyFact>
                                                     <keyFact id="B" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                         <keyValue domain="B" occur="1">
                                                             <stringValue>
                                                                 <typedValue>A</typedValue>
                                                             </stringValue>
                                                         </keyValue>
                                                     </keyFact>
                                                     <keyFact id="C" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                         <keyValue domain="C" occur="1">
                                                             <stringValue>
                                                                 <typedValue>B</typedValue>
                                                             </stringValue>
                                                         </keyValue>
                                                     </keyFact>
                                                 </keyFinding>
                                             </keyFindings>
                                             <aspectReferences/>
                                         </solution>

    Private failing_unitTestResponse As XElement = <Response xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" active="false" responseNr="-9223372036854775808" translatedScore="0" rawScore="0" navigatedToIndex="-2147483648">
                                                       <ResponseProperties xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                                       <responseFindings xmlns="http://Cito.Tester.Server/xml/serialization">
                                                           <responseFinding id="shapeInteractionController">
                                                               <responseFact id="A">
                                                                   <responseValue domain="A" occur="1">
                                                                       <stringValue>
                                                                           <typedValue>C</typedValue>
                                                                       </stringValue>
                                                                   </responseValue>
                                                               </responseFact>
                                                               <responseFact id="B">
                                                                   <responseValue domain="B" occur="1">
                                                                       <stringValue>
                                                                           <typedValue>A</typedValue>
                                                                       </stringValue>
                                                                   </responseValue>
                                                               </responseFact>
                                                               <responseFact id="C">
                                                                   <responseValue domain="C" occur="1">
                                                                       <stringValue>
                                                                           <typedValue>B</typedValue>
                                                                       </stringValue>
                                                                   </responseValue>
                                                               </responseFact>
                                                           </responseFinding>
                                                       </responseFindings>
                                                       <ItemIndexInTest xmlns="http://Cito.Tester.Server/xml/serialization">0</ItemIndexInTest>
                                                   </Response>

End Class
