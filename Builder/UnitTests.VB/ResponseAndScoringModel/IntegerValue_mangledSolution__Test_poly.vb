
Imports System.Xml.Linq

<TestClass>
Public Class IntegerValue_mangledSolution__Test_poly
    Inherits ScoringTestBase

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub GiveCorrectAnswer_experts_1()
        Dim solution = toSolution(INT_Dichotomous_42_AND_ERROR)
        Dim r = GetResponse(New List(Of Integer) From {42, 131})

        Write("Response", "Arrange", r)

        Dim result = solution.ScoreSolution(r)

        Assert.AreEqual(1, result)
    End Sub

    Private INT_Dichotomous_42_AND_ERROR As XElement = <solution>
                                                           <keyFindings>
                                                               <keyFinding id="mc" scoringMethod="Polytomous">

                                                                   <keyFact score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                       <keyValue domain="A" occur="1">
                                                                           <integerValue>
                                                                               <typedValue>42</typedValue>
                                                                           </integerValue>
                                                                       </keyValue>
                                                                   </keyFact>
                                                                   <keyFact score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                       <keyValue domain="B" occur="1">
                                                                       </keyValue>
                                                                   </keyFact>

                                                               </keyFinding>
                                                           </keyFindings>
                                                           <aspectReferences/>
                                                       </solution>

End Class
