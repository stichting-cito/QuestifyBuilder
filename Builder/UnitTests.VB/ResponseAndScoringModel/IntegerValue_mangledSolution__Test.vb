﻿
Imports System.Xml.Linq

<TestClass>
Public Class IntegerValue_mangledSolution__Test
    Inherits ScoringTestBase

    <TestMethod()> <TestCategory("ResponseAndScoringModel")> <Description("one key value has no value!!")>
    Public Sub GiveCorrectAnswer_experts_0()
        Dim solution = toSolution(INT_Dichotomous_42_AND_ERROR)
        Dim r = GetResponse(New List(Of Integer) From {42, 131})

        Write("Response", "Arrange", r)

        Dim result = solution.ScoreSolution(r)

        Assert.AreEqual(0, result)
    End Sub

    Private INT_Dichotomous_42_AND_ERROR As XElement = <solution>
                                                           <keyFindings>
                                                               <keyFinding id="mc" scoringMethod="Dichotomous">

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
