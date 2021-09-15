
Imports System.Xml.Linq
Imports System.Linq
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ContentModel.Scoring
Imports Questify.Builder.UnitTests.Framework

<TestClass>
Public Class ScoringMapConceptTests

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("ScoringAdv")>
    Public Sub GetScoringMapForConcept()
        'Arrange
        Dim solution = IntegerParam_2GapsIn_2factSets.To(Of Solution)()
        Dim sp = New IntegerScoringParameter() With {.Name = "Answer", .ControllerId = "integerScore", .FindingOverride = "sharedIntegerFinding"}.AddSubParameters("1", "2")
        
        'Act    
        Dim resultKey = New ScoringMap(New ScoringParameter() {sp}, solution).GetMap().ToList()
        Dim resultConcept = New ConceptScoringMap(New ScoringParameter() {sp}, solution).GetMap().ToList()
        
        'Assert
        Assert.AreEqual(1, resultConcept.Count, "int.1 and int.2 are grouped,. thus expecting a single combinedScoringMapKey")
        Assert.AreEqual(4, resultConcept.First().SetNumbers.Count(), "Expected 4 ConceptSets found")

        Assert.AreEqual(1, resultKey.Count, "int.1 and int.2 are grouped,. thus expecting a single combinedScoringMapKey")
        Assert.AreEqual(2, resultKey.First().SetNumbers.Count(), "Expected 2 KeySets found")
    End Sub


    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("ScoringAdv")>
    Public Sub GetScoringMapForConcept2()
        'Arrange
        Dim solution = _2factsets1Fact_WithConceptFinding.To(Of Solution)()
        Dim sp = New IntegerScoringParameter() With {.Name = "Answer", .ControllerId = "integerScore", .FindingOverride = "sharedIntegerFinding"}.AddSubParameters("1", "2", "3", "4", "5")
        
        'Act    
        Dim resultKey = New ScoringMap(New ScoringParameter() {sp}, solution).GetMap().ToList()
        Dim resultConcept = New ConceptScoringMap(New ScoringParameter() {sp}, solution).GetMap().ToList()
        
        'Assert
        Assert.AreEqual(3, resultConcept.Count, "int.1 and int.2 are grouped, int.3&int.4 , and int 5 is alone thus 3")
        Assert.AreEqual(2, resultConcept(0).SetNumbers.Count(), "Expected 2 ConceptSets found")
        Assert.AreEqual(2, resultConcept(1).SetNumbers.Count(), "Expected 2 ConceptSets found")
        Assert.AreEqual(0, resultConcept(2).SetNumbers.Count(), "Expected no")

        Assert.AreEqual(3, resultKey.Count, "int.1 and int.2 are grouped,. thus expecting a single combinedScoringMapKey")
        Assert.AreEqual(2, resultKey(0).SetNumbers.Count(), "Expected 2 ConceptSets found")
        Assert.AreEqual(2, resultKey(1).SetNumbers.Count(), "Expected 2 ConceptSets found")
        Assert.AreEqual(0, resultKey(2).SetNumbers.Count(), "Expected no")
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("ScoringAdv")>
    Public Sub GetScoringMapForConcept3()
        'Arrange
        Dim solution = _2factsets1Fact_WithConceptFinding_AndAddedSet.To(Of Solution)()
        Dim sp = New IntegerScoringParameter() With {.Name = "Answer", .ControllerId = "integerScore", .FindingOverride = "sharedIntegerFinding"}.AddSubParameters("1", "2", "3", "4", "5")
        
        'Act    
        Dim resultKey = New ScoringMap(New ScoringParameter() {sp}, solution).GetMap().ToList()
        Dim resultConcept = New ConceptScoringMap(New ScoringParameter() {sp}, solution).GetMap().ToList()
        
        'Assert
        Assert.AreEqual(3, resultConcept.Count, "int.1 and int.2 are grouped, int.3&int.4 , and int 5 is alone thus 3")
        Assert.AreEqual(3, resultConcept(0).SetNumbers.Count(), "Expected 3 ConceptSets found, due to added sets")
        Assert.AreEqual(2, resultConcept(1).SetNumbers.Count(), "Expected 2 ConceptSets found")
        Assert.AreEqual(0, resultConcept(2).SetNumbers.Count(), "Expected no")

        Assert.AreEqual(3, resultKey.Count, "int.1 and int.2 are grouped,. thus expecting a single combinedScoringMapKey")
        Assert.AreEqual(2, resultKey(0).SetNumbers.Count(), "Expected 2 ConceptSets found")
        Assert.AreEqual(2, resultKey(1).SetNumbers.Count(), "Expected 2 ConceptSets found")
        Assert.AreEqual(0, resultKey(2).SetNumbers.Count(), "Expected no")
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("ScoringAdv")>
    Public Sub GetScoringMapForConcept4()
        'Arrange
        Dim solution = _factSetsWithCatchAll.To(Of Solution)()
        Dim sp = New IntegerScoringParameter() With {.Name = "Answer", .ControllerId = "integerScore", .FindingOverride = "sharedIntegerFinding"}.AddSubParameters("1", "2")
        
        'Act    
        Dim resultKey = New ScoringMap(New ScoringParameter() {sp}, solution).GetMap().ToList()
        Dim resultConcept = New ConceptScoringMap(New ScoringParameter() {sp}, solution).GetMap().ToList()
        
        'Assert
        Assert.AreEqual(1, resultConcept.Count)
        Assert.AreEqual(3, resultConcept(0).SetNumbers.Count())

        Assert.AreEqual(1, resultKey.Count)
        Assert.AreEqual(2, resultKey(0).SetNumbers.Count())
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("ScoringAdv")>
    Public Sub GetScoringMapForConcept_With2ChoicePrm_3FactsInConcept_Expects_SetNumbersCountIs1()
        'Arrange
        Dim solution = _solutionForInlineChoice_ConceptHas3Facts.To(Of Solution)()
        Dim spInlineChoice1 = New InlineChoiceScoringParameter() With {.Name = "Answer", .InlineId = "I16746288-1b56-4c53-880d-2d54d060fba8", .FindingOverride = "Opgave", .MaxChoices = 1}.AddSubParameters("A", "B", "C", "D")
        Dim spInlineChoice2 = New InlineChoiceScoringParameter() With {.Name = "Answer", .InlineId = "Ibb37a53e-43d7-49e6-ab4a-fb967ecba6cc", .FindingOverride = "Opgave", .MaxChoices = 1}.AddSubParameters("A", "B", "C", "D")
        
        'Act    
        Dim resultConcept = New ConceptScoringMap(New ScoringParameter() {spInlineChoice1, spInlineChoice2}, solution).GetMap().ToList()
        
        'Assert
        Assert.AreEqual(1, resultConcept.Count)
        Assert.AreEqual(1, resultConcept(0).SetNumbers.Count())
    End Sub

#Region "Data"
    Private IntegerParam_2GapsIn_2factSets As XElement = <solution>

                                                             <keyFindings>
                                                                 <keyFinding id="sharedIntegerFinding" scoringMethod="Dichotomous">
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

                                                             <conceptFindings>
                                                                 <conceptFinding id="sharedIntegerFinding" scoringMethod="None">

                                                                     <conceptFactSet>
                                                                         <conceptFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                             <conceptValue domain="1-integerScore" occur="1">
                                                                                 <integerValue>
                                                                                     <typedValue>6</typedValue>
                                                                                 </integerValue>
                                                                             </conceptValue>
                                                                         </conceptFact>
                                                                         <conceptFact id="2-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                             <conceptValue domain="2-integerScore" occur="1">
                                                                                 <integerValue>
                                                                                     <typedValue>14</typedValue>
                                                                                 </integerValue>
                                                                             </conceptValue>
                                                                         </conceptFact>
                                                                         <concepts xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                                                     </conceptFactSet>

                                                                     <conceptFactSet>
                                                                         <conceptFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                             <conceptValue domain="1-integerScore" occur="1">
                                                                                 <integerValue>
                                                                                     <typedValue>14</typedValue>
                                                                                 </integerValue>
                                                                             </conceptValue>
                                                                         </conceptFact>
                                                                         <conceptFact id="2-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                             <conceptValue domain="2-integerScore" occur="1">
                                                                                 <integerValue>
                                                                                     <typedValue>6</typedValue>
                                                                                 </integerValue>
                                                                             </conceptValue>
                                                                         </conceptFact>
                                                                         <concepts xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                                                     </conceptFactSet>

                                                                     <conceptFactSet>
                                                                         <conceptFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                             <conceptValue domain="1-integerScore" occur="1">
                                                                                 <integerValue>
                                                                                     <typedValue>111</typedValue>
                                                                                 </integerValue>
                                                                             </conceptValue>
                                                                         </conceptFact>
                                                                         <conceptFact id="2-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                             <conceptValue domain="2-integerScore" occur="1">
                                                                                 <integerValue>
                                                                                     <typedValue>222</typedValue>
                                                                                 </integerValue>
                                                                             </conceptValue>
                                                                         </conceptFact>
                                                                     </conceptFactSet>

                                                                     <conceptFactSet>
                                                                         <conceptFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                             <conceptValue domain="1-integerScore" occur="1">
                                                                                 <integerValue>
                                                                                     <typedValue>333</typedValue>
                                                                                 </integerValue>
                                                                             </conceptValue>
                                                                         </conceptFact>
                                                                         <conceptFact id="2-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                             <conceptValue domain="2-integerScore" occur="1">
                                                                                 <integerValue>
                                                                                     <typedValue>444</typedValue>
                                                                                 </integerValue>
                                                                             </conceptValue>
                                                                         </conceptFact>
                                                                     </conceptFactSet>

                                                                 </conceptFinding>
                                                             </conceptFindings>
                                                             <aspectReferences/>
                                                         </solution>

    Private _2factsets1Fact_WithConceptFinding As XElement = <solution>

                                                                 <keyFindings>
                                                                     <keyFinding id="sharedIntegerFinding" scoringMethod="Dichotomous">
                                                                         <keyFact id="5-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                             <keyValue domain="integerScore" occur="1">
                                                                                 <integerValue>
                                                                                     <typedValue>42</typedValue>
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
                                                                         <keyFactSet>
                                                                             <keyFact id="3-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                 <keyValue domain="integerScore" occur="1">
                                                                                     <integerValue>
                                                                                         <typedValue>3</typedValue>
                                                                                     </integerValue>
                                                                                 </keyValue>
                                                                             </keyFact>
                                                                             <keyFact id="4-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                 <keyValue domain="integerScore" occur="1">
                                                                                     <integerValue>
                                                                                         <typedValue>7</typedValue>
                                                                                     </integerValue>
                                                                                 </keyValue>
                                                                             </keyFact>
                                                                         </keyFactSet>
                                                                         <keyFactSet>
                                                                             <keyFact id="3-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                 <keyValue domain="integerScore" occur="1">
                                                                                     <integerValue>
                                                                                         <typedValue>7</typedValue>
                                                                                     </integerValue>
                                                                                 </keyValue>
                                                                             </keyFact>
                                                                             <keyFact id="4-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                 <keyValue domain="integerScore" occur="1">
                                                                                     <integerValue>
                                                                                         <typedValue>3</typedValue>
                                                                                     </integerValue>
                                                                                 </keyValue>
                                                                             </keyFact>
                                                                         </keyFactSet>
                                                                     </keyFinding>
                                                                 </keyFindings>

                                                                 <conceptFindings>

                                                                     <conceptFinding id="sharedIntegerFinding" scoringMethod="None">
                                                                         <conceptFact id="5-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                             <conceptValue domain="5-integerScore" occur="1">
                                                                                 <integerValue>
                                                                                     <typedValue>42</typedValue>
                                                                                 </integerValue>
                                                                             </conceptValue>
                                                                         </conceptFact>

                                                                         <conceptFactSet>
                                                                             <conceptFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                 <conceptValue domain="1-integerScore" occur="1">
                                                                                     <integerValue>
                                                                                         <typedValue>6</typedValue>
                                                                                     </integerValue>
                                                                                 </conceptValue>
                                                                             </conceptFact>
                                                                             <conceptFact id="2-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                 <conceptValue domain="2-integerScore" occur="1">
                                                                                     <integerValue>
                                                                                         <typedValue>14</typedValue>
                                                                                     </integerValue>
                                                                                 </conceptValue>
                                                                             </conceptFact>
                                                                             <concepts xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                                                         </conceptFactSet>

                                                                         <conceptFactSet>
                                                                             <conceptFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                 <conceptValue domain="1-integerScore" occur="1">
                                                                                     <integerValue>
                                                                                         <typedValue>14</typedValue>
                                                                                     </integerValue>
                                                                                 </conceptValue>
                                                                             </conceptFact>
                                                                             <conceptFact id="2-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                 <conceptValue domain="2-integerScore" occur="1">
                                                                                     <integerValue>
                                                                                         <typedValue>6</typedValue>
                                                                                     </integerValue>
                                                                                 </conceptValue>
                                                                             </conceptFact>
                                                                             <concepts xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                                                         </conceptFactSet>

                                                                         <conceptFactSet>
                                                                             <conceptFact id="3-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                 <conceptValue domain="3-integerScore" occur="1">
                                                                                     <integerValue>
                                                                                         <typedValue>3</typedValue>
                                                                                     </integerValue>
                                                                                 </conceptValue>
                                                                             </conceptFact>
                                                                             <conceptFact id="4-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                 <conceptValue domain="4-integerScore" occur="1">
                                                                                     <integerValue>
                                                                                         <typedValue>7</typedValue>
                                                                                     </integerValue>
                                                                                 </conceptValue>
                                                                             </conceptFact>
                                                                         </conceptFactSet>

                                                                         <conceptFactSet>
                                                                             <conceptFact id="3-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                 <conceptValue domain="3-integerScore" occur="1">
                                                                                     <integerValue>
                                                                                         <typedValue>7</typedValue>
                                                                                     </integerValue>
                                                                                 </conceptValue>
                                                                             </conceptFact>
                                                                             <conceptFact id="4-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                 <conceptValue domain="4-integerScore" occur="1">
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

    Private _2factsets1Fact_WithConceptFinding_AndAddedSet As XElement = <solution>
                                                                             <keyFindings>
                                                                                 <keyFinding id="sharedIntegerFinding" scoringMethod="Dichotomous">
                                                                                     <keyFact id="5-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                         <keyValue domain="integerScore" occur="1">
                                                                                             <integerValue>
                                                                                                 <typedValue>42</typedValue>
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
                                                                                     <keyFactSet>
                                                                                         <keyFact id="3-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                             <keyValue domain="integerScore" occur="1">
                                                                                                 <integerValue>
                                                                                                     <typedValue>3</typedValue>
                                                                                                 </integerValue>
                                                                                             </keyValue>
                                                                                         </keyFact>
                                                                                         <keyFact id="4-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                             <keyValue domain="integerScore" occur="1">
                                                                                                 <integerValue>
                                                                                                     <typedValue>7</typedValue>
                                                                                                 </integerValue>
                                                                                             </keyValue>
                                                                                         </keyFact>
                                                                                     </keyFactSet>
                                                                                     <keyFactSet>
                                                                                         <keyFact id="3-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                             <keyValue domain="integerScore" occur="1">
                                                                                                 <integerValue>
                                                                                                     <typedValue>7</typedValue>
                                                                                                 </integerValue>
                                                                                             </keyValue>
                                                                                         </keyFact>
                                                                                         <keyFact id="4-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                             <keyValue domain="integerScore" occur="1">
                                                                                                 <integerValue>
                                                                                                     <typedValue>3</typedValue>
                                                                                                 </integerValue>
                                                                                             </keyValue>
                                                                                         </keyFact>
                                                                                     </keyFactSet>
                                                                                 </keyFinding>
                                                                             </keyFindings>
                                                                             <conceptFindings>
                                                                                 <conceptFinding id="sharedIntegerFinding" scoringMethod="None">
                                                                                     <conceptFact id="5-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                         <conceptValue domain="5-integerScore" occur="1">
                                                                                             <integerValue>
                                                                                                 <typedValue>42</typedValue>
                                                                                             </integerValue>
                                                                                         </conceptValue>
                                                                                     </conceptFact>
                                                                                     <conceptFactSet>
                                                                                         <conceptFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                             <conceptValue domain="1-integerScore" occur="1">
                                                                                                 <integerValue>
                                                                                                     <typedValue>6</typedValue>
                                                                                                 </integerValue>
                                                                                             </conceptValue>
                                                                                         </conceptFact>
                                                                                         <conceptFact id="2-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                             <conceptValue domain="2-integerScore" occur="1">
                                                                                                 <integerValue>
                                                                                                     <typedValue>14</typedValue>
                                                                                                 </integerValue>
                                                                                             </conceptValue>
                                                                                         </conceptFact>
                                                                                         <concepts xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                                                                     </conceptFactSet>
                                                                                     <conceptFactSet>
                                                                                         <conceptFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                             <conceptValue domain="1-integerScore" occur="1">
                                                                                                 <integerValue>
                                                                                                     <typedValue>14</typedValue>
                                                                                                 </integerValue>
                                                                                             </conceptValue>
                                                                                         </conceptFact>
                                                                                         <conceptFact id="2-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                             <conceptValue domain="2-integerScore" occur="1">
                                                                                                 <integerValue>
                                                                                                     <typedValue>6</typedValue>
                                                                                                 </integerValue>
                                                                                             </conceptValue>
                                                                                         </conceptFact>
                                                                                         <concepts xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                                                                     </conceptFactSet>
                                                                                     <conceptFactSet>
                                                                                         <conceptFact id="3-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                             <conceptValue domain="3-integerScore" occur="1">
                                                                                                 <integerValue>
                                                                                                     <typedValue>3</typedValue>
                                                                                                 </integerValue>
                                                                                             </conceptValue>
                                                                                         </conceptFact>
                                                                                         <conceptFact id="4-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                             <conceptValue domain="4-integerScore" occur="1">
                                                                                                 <integerValue>
                                                                                                     <typedValue>7</typedValue>
                                                                                                 </integerValue>
                                                                                             </conceptValue>
                                                                                         </conceptFact>
                                                                                     </conceptFactSet>
                                                                                     <conceptFactSet>
                                                                                         <conceptFact id="3-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                             <conceptValue domain="3-integerScore" occur="1">
                                                                                                 <integerValue>
                                                                                                     <typedValue>7</typedValue>
                                                                                                 </integerValue>
                                                                                             </conceptValue>
                                                                                         </conceptFact>
                                                                                         <conceptFact id="4-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                             <conceptValue domain="4-integerScore" occur="1">
                                                                                                 <integerValue>
                                                                                                     <typedValue>3</typedValue>
                                                                                                 </integerValue>
                                                                                             </conceptValue>
                                                                                         </conceptFact>
                                                                                     </conceptFactSet>
                                                                                     <conceptFactSet>
                                                                                         <conceptFact id="1[2]-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                             <conceptValue domain="1[2]-integerScore" occur="1">
                                                                                                 <integerValue>
                                                                                                     <typedValue>8888</typedValue>
                                                                                                 </integerValue>
                                                                                             </conceptValue>
                                                                                         </conceptFact>
                                                                                         <conceptFact id="2[2]-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                             <conceptValue domain="2[2]-integerScore" occur="1">
                                                                                                 <integerValue>
                                                                                                     <typedValue>9999</typedValue>
                                                                                                 </integerValue>
                                                                                             </conceptValue>
                                                                                         </conceptFact>
                                                                                     </conceptFactSet>
                                                                                 </conceptFinding>
                                                                             </conceptFindings>
                                                                             <aspectReferences/>
                                                                         </solution>

    ReadOnly _factSetsWithCatchAll As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                                     <keyFindings>
                                                         <keyFinding id="sharedIntegerFinding" scoringMethod="Dichotomous">
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
                                                     <conceptFindings>
                                                         <conceptFinding id="sharedIntegerFinding" scoringMethod="None">
                                                             <conceptFactSet>
                                                                 <conceptFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                     <conceptValue domain="1-integerScore" occur="1">
                                                                         <integerValue>
                                                                             <typedValue>6</typedValue>
                                                                         </integerValue>
                                                                     </conceptValue>
                                                                 </conceptFact>
                                                                 <conceptFact id="2-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                     <conceptValue domain="2-integerScore" occur="1">
                                                                         <integerValue>
                                                                             <typedValue>14</typedValue>
                                                                         </integerValue>
                                                                     </conceptValue>
                                                                 </conceptFact>
                                                             </conceptFactSet>
                                                             <conceptFactSet>
                                                                 <conceptFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                     <conceptValue domain="1-integerScore" occur="1">
                                                                         <integerValue>
                                                                             <typedValue>14</typedValue>
                                                                         </integerValue>
                                                                     </conceptValue>
                                                                 </conceptFact>
                                                                 <conceptFact id="2-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                     <conceptValue domain="2-integerScore" occur="1">
                                                                         <integerValue>
                                                                             <typedValue>6</typedValue>
                                                                         </integerValue>
                                                                     </conceptValue>
                                                                 </conceptFact>
                                                             </conceptFactSet>

                                                             <conceptFactSet>
                                                                 <conceptFact id="1[*]-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                     <conceptValue domain="*-integerScore" occur="1">
                                                                         <catchAllValue/>
                                                                     </conceptValue>
                                                                 </conceptFact>
                                                                 <conceptFact id="2[*]-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                     <conceptValue domain="*-integerScore" occur="1">
                                                                         <catchAllValue/>
                                                                     </conceptValue>
                                                                 </conceptFact>
                                                             </conceptFactSet>

                                                         </conceptFinding>
                                                     </conceptFindings>
                                                     <aspectReferences/>
                                                 </solution>


    'ConceptFacts has 3 facts for 2 basically mcs
    ReadOnly _solutionForInlineChoice_ConceptHas3Facts As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                                                         <keyFindings>
                                                                             <keyFinding id="Opgave" scoringMethod="Dichotomous">
                                                                                 <keyFactSet>
                                                                                     <keyFact id="D-Ibb37a53e-43d7-49e6-ab4a-fb967ecba6cc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                         <keyValue domain="Ibb37a53e-43d7-49e6-ab4a-fb967ecba6cc" occur="1">
                                                                                             <stringValue>
                                                                                                 <typedValue>D</typedValue>
                                                                                             </stringValue>
                                                                                         </keyValue>
                                                                                     </keyFact>
                                                                                     <keyFact id="B-I16746288-1b56-4c53-880d-2d54d060fba8" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                         <keyValue domain="I16746288-1b56-4c53-880d-2d54d060fba8" occur="1">
                                                                                             <stringValue>
                                                                                                 <typedValue>B</typedValue>
                                                                                             </stringValue>
                                                                                         </keyValue>
                                                                                     </keyFact>
                                                                                 </keyFactSet>
                                                                             </keyFinding>
                                                                         </keyFindings>
                                                                         <conceptFindings>
                                                                             <conceptFinding id="Opgave" scoringMethod="None">
                                                                                 <conceptFactSet>
                                                                                     <conceptFact id="D-Ibb37a53e-43d7-49e6-ab4a-fb967ecba6cc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                         <conceptValue domain="D-Ibb37a53e-43d7-49e6-ab4a-fb967ecba6cc" occur="1">
                                                                                             <stringValue>
                                                                                                 <typedValue>D</typedValue>
                                                                                             </stringValue>
                                                                                         </conceptValue>
                                                                                     </conceptFact>
                                                                                     <conceptFact id="A-I16746288-1b56-4c53-880d-2d54d060fba8" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                         <conceptValue domain="A-I16746288-1b56-4c53-880d-2d54d060fba8" occur="1">
                                                                                             <stringValue>
                                                                                                 <typedValue>A</typedValue>
                                                                                             </stringValue>
                                                                                         </conceptValue>
                                                                                     </conceptFact>
                                                                                     <conceptFact id="B-I16746288-1b56-4c53-880d-2d54d060fba8" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                         <conceptValue domain="B-I16746288-1b56-4c53-880d-2d54d060fba8" occur="1">
                                                                                             <stringValue>
                                                                                                 <typedValue>B</typedValue>
                                                                                             </stringValue>
                                                                                         </conceptValue>
                                                                                     </conceptFact>
                                                                                     <concepts xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                                                                 </conceptFactSet>
                                                                             </conceptFinding>
                                                                         </conceptFindings>
                                                                         <aspectReferences/>
                                                                     </solution>

#End Region

End Class
