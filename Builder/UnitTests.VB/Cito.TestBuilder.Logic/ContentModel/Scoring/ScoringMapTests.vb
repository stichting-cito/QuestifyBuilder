
Imports System.Xml.Linq
Imports System.Linq
Imports System.Xml.Serialization
Imports System.IO
Imports Cito.Tester.ContentModel
Imports System.Diagnostics
Imports Questify.Builder.Logic.ContentModel.Scoring
Imports Questify.Builder.UnitTests.Framework

<TestClass>
Public Class ScoringMapTests

    <TestMethod(), TestCategory("Logic"), TestCategory("ScoringAdv"), TestCategory("Scoring")>
    Public Sub GetScoreMap_NoFactSet()
        'Arrange
        Dim solution = Deserialize(Of Solution)(NoFactSets)
        Dim integerParam = New IntegerScoringParameter() With {.ControllerId = "Controller"}.AddSubParameters("A")
        Dim map = New ScoringMap(New ScoringParameter() {integerParam}, solution)
        
        'Act  
        Dim lst As IEnumerable(Of IEnumerable(Of ScoringMapKey)) = map.GetMap()
        
        'Assert
        Assert.AreEqual(1, lst.Count(), "Expect single entry")
        Assert.AreEqual(1, lst.First().Count(), "entry should contain single scoringMapKey")
    End Sub


    <TestMethod(), TestCategory("Logic"), TestCategory("ScoringAdv"), TestCategory("Scoring")>
    Public Sub GetScoreMap_NoFactSet2Integers()
        'Arrange
        Dim solution = Deserialize(Of Solution)(NoFactSets2Integers)
        Dim integerParam1 = New IntegerScoringParameter() With {.ControllerId = "Controller1"}.AddSubParameters("A")
        Dim integerParam2 = New IntegerScoringParameter() With {.ControllerId = "Controller2"}.AddSubParameters("A")
        Dim map = New ScoringMap(New ScoringParameter() {integerParam1, integerParam2}, solution)
        
        'Act  
        Dim lst = map.GetMap().ToList()
        
        'Assert
        '+--------+  
        '| I1=111 |  
        '+--------+  
        '+--------+  
        '| I2=222 |  
        '+--------+  
        Assert.AreEqual(2, lst.Count, "Expect Two entries, on finding is NOT grouped")
        Assert.AreEqual(1, lst(0).Count, "entry should contain single scoringMapKey")
        Assert.AreEqual(1, lst(1).Count, "entry should contain single scoringMapKey")
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("ScoringAdv"), TestCategory("Scoring")>
    Public Sub GetScoreMap_2FactSetGroups()
        'Arrange
        Dim solution = Deserialize(Of Solution)(solutionWith2FactSetGroupsAnd1SpInFinding)
        Dim integerParam = New IntegerScoringParameter() With {.ControllerId = "integerScore", .FindingOverride = "sharedIntegerFinding"}.AddSubParameters("1", "2", "3", "4", "5")
        Dim map = New ScoringMap(New ScoringParameter() {integerParam}, solution)
        
        'Act  
        Dim lst = map.GetMap().ToList()
        
        'Assert
        '+------+   +------+
        '| 1= 6 |   | 1=14 |
        '|  = 7 |   |      |
        '|  = 8 |   |      |            <=First entry. Should be integer key 1&2
        '| 2=14 |   | 2= 6 |
        '+------+   +------+
        '+------+   +------+
        '| 3= 3 |   | 4= 7 |            <=Second entry. Should be integer key 3&4
        '| 4= 7 |   | 4= 3 |
        '+------+   +------+
        '+------+           
        '| 5= 1 |           <=Last entry. Should be integer key 5
        '|  = 2 |           
        '+------+         
        Assert.AreEqual(3, lst.Count, "Expect three entries")

        Assert.AreEqual(2, lst(0).Count())
        Assert.AreEqual("1", lst(0).ToList()(0).ScoreKey)
        Assert.AreEqual("2", lst(0).ToList()(1).ScoreKey)

        Assert.AreEqual(2, lst(1).Count())
        Assert.AreEqual("3", lst(1).ToList()(0).ScoreKey)
        Assert.AreEqual("4", lst(1).ToList()(1).ScoreKey)

        Assert.AreEqual(1, lst(2).Count())
        Assert.AreEqual("5", lst(2).ToList()(0).ScoreKey)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("ScoringAdv"), TestCategory("Scoring")>
    Public Sub GetScoreMap_2FactSetGroups_IsGroup_Test()
        'Arrange
        Dim solution = Deserialize(Of Solution)(solutionWith2FactSetGroupsAnd1SpInFinding)
        Dim integerParam = New IntegerScoringParameter() With {.ControllerId = "integerScore", .FindingOverride = "sharedIntegerFinding"}.AddSubParameters("1", "2", "3", "4", "5")
        Dim map = New ScoringMap(New ScoringParameter() {integerParam}, solution)
        
        'Act  
        Dim lst = map.GetMap().ToList()
        
        'Assert
        '+------+   +------+
        '| 1= 6 |   | 1=14 |
        '|  = 7 |   |      |
        '|  = 8 |   |      |            <=First entry. Should be integer key 1&2
        '| 2=14 |   | 2= 6 |
        '+------+   +------+
        '+------+   +------+
        '| 3= 3 |   | 4= 7 |            <=Second entry. Should be integer key 3&4
        '| 4= 7 |   | 4= 3 |
        '+------+   +------+
        '+------+           
        '| 5= 1 |          <=Last entry. Should be integer key 5
        '|  = 2 |           
        '+------+         
        Assert.IsTrue(lst(0).IsGroup)
        Assert.IsTrue(lst(1).IsGroup)
        Assert.IsFalse(lst(2).IsGroup)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("ScoringAdv"), TestCategory("Scoring")>
    Public Sub GetScoreMap_2FactSetGroups_SetNumbers_Test()
        'Arrange
        Dim solution = Deserialize(Of Solution)(solutionWith2FactSetGroupsAnd1SpInFinding)
        Dim integerParam = New IntegerScoringParameter() With {.ControllerId = "integerScore", .FindingOverride = "sharedIntegerFinding"}.AddSubParameters("1", "2", "3", "4", "5")
        Dim map = New ScoringMap(New ScoringParameter() {integerParam}, solution)
        
        'Act  
        Dim lst = map.GetMap().ToList()
        
        'Assert
        '+------+   +------+
        '| 1= 6 |   | 1=14 |
        '|  = 7 |   |      |
        '|  = 8 |   |      |            <=First entry. Should be integer key 1&2
        '| 2=14 |   | 2= 6 |
        '+------+   +------+
        '+------+   +------+
        '| 3= 3 |   | 4= 7 |            <=Second entry. Should be integer key 3&4
        '| 4= 7 |   | 4= 3 |
        '+------+   +------+
        '+------+           
        '| 5= 1 |          <=Last entry. Should be integer key 5
        '|  = 2 |           
        '+------+         
        Assert.AreEqual(2, lst(0).SetNumbers.Count())
        Assert.AreEqual(2, lst(1).SetNumbers.Count())
        Assert.AreEqual(0, lst(2).SetNumbers.Count())
    End Sub


    <TestMethod(), TestCategory("Logic"), TestCategory("ScoringAdv"), TestCategory("Scoring")>
    Public Sub GetScoreMap_2FactSetGroups_WithMC()
        'Arrange
        Dim solution = Deserialize(Of Solution)(solutionWithIntAndMCInFactSets)
        Dim integerParam = New IntegerScoringParameter() With {.ControllerId = "integerScore", .FindingOverride = "DefaultFinding"}.AddSubParameters("1")
        Dim mcParam = New MultiChoiceScoringParameter() With {.ControllerId = "McScore", .FindingOverride = "DefaultFinding"}.AddSubParameters("A", "B")
        Dim map = New ScoringMap(New ScoringParameter() {integerParam, mcParam}, solution)
        
        'Act  
        Dim lst = map.GetMap().ToList()

        'Assert
        '+----++----+
        '|A =2||A =1|
        '|MC=A||MC=B|
        '+----++----+
        Assert.AreEqual(1, lst.Count, "Expect ONE entries")

        Assert.AreEqual(3, lst(0).Count())
        Assert.AreEqual("1", lst(0).ToList()(0).ScoreKey)
        Assert.AreEqual("A", lst(0).ToList()(1).ScoreKey)
        Assert.AreEqual("B", lst(0).ToList()(2).ScoreKey)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("ScoringAdv"), TestCategory("Scoring")>
    Public Sub GetScoreMap_2FactSetGroups_WithMC_MCB_NotSet()
        'Arrange
        Dim solution = Deserialize(Of Solution)(solutionWithIntAndMCInFactSets_MCB_NotSet) 'There is no value for B in solution!
        Dim integerParam = New IntegerScoringParameter() With {.ControllerId = "integerScore", .FindingOverride = "DefaultFinding"}.AddSubParameters("1")
        Dim mcParam = New MultiChoiceScoringParameter() With {.ControllerId = "McScore", .FindingOverride = "DefaultFinding", .MinChoices = 1, .MaxChoices = 1}.AddSubParameters("A", "B")
        Dim map = New ScoringMap(New ScoringParameter() {integerParam, mcParam}, solution)
        
        'Act  
        Dim lst = map.GetMap().ToList()

        'Assert
        '+----++----+
        '|A =2||A =1|
        '|MC=A||    |
        '+----++----+
        
        'Result should be the same as: GetScoreMap_2FactSetGroups_WithMC
        Assert.AreEqual(1, lst.Count, "Expect ONE entries")

        Assert.AreEqual(3, lst(0).Count())
        Assert.AreEqual("1", lst(0).ToList()(0).ScoreKey)
        Assert.AreEqual("A", lst(0).ToList()(1).ScoreKey)
        Assert.AreEqual("B", lst(0).ToList()(2).ScoreKey)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("ScoringAdv"), TestCategory("Scoring")>
    Public Sub GetScoreMap_2FactSetGroups_WithMC_MCA_NotSet()
        'Arrange
        Dim solution = Deserialize(Of Solution)(solutionWithIntAndMCInFactSets_MCA_NotSet) 'There is no value for B in solution!
        Dim integerParam = New IntegerScoringParameter() With {.ControllerId = "integerScore", .FindingOverride = "DefaultFinding"}.AddSubParameters("1")
        Dim mcParam = New MultiChoiceScoringParameter() With {.ControllerId = "McScore", .FindingOverride = "DefaultFinding", .MinChoices = 1, .MaxChoices = 1}.AddSubParameters("A", "B")
        Dim map = New ScoringMap(New ScoringParameter() {integerParam, mcParam}, solution)
        
        'Act  
        Dim lst = map.GetMap().ToList()

        'Assert
        '+----++----+
        '|A =2||A =1|
        '|MC=A||    |
        '+----++----+
        
        'Result should be the same as: GetScoreMap_2FactSetGroups_WithMC
        Assert.AreEqual(1, lst.Count, "Expect ONE entries")

        Assert.AreEqual(3, lst(0).Count())
        Assert.AreEqual("1", lst(0).ToList()(0).ScoreKey)
        Assert.AreEqual("A", lst(0).ToList()(1).ScoreKey)
        Assert.AreEqual("B", lst(0).ToList()(2).ScoreKey)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("ScoringAdv"), TestCategory("Scoring")>
    Public Sub GetScoreMap_MC_NoSolution_Expects_Grouped()
        'Arrange
        Dim solution = New Solution()
        Dim mcParam = New MultiChoiceScoringParameter() With {.ControllerId = "McScore", .FindingOverride = "DefaultFinding", .MaxChoices = 1}.AddSubParameters("A", "B", "C", "D")
        Dim map = New ScoringMap(New ScoringParameter() {mcParam}, solution)
        
        'Act  
        Dim lst = map.GetMap().ToList()

        'Assert
        Assert.AreEqual(1, lst.Count, "Expect ONE entries")

        Assert.AreEqual(4, lst(0).Count())
        Assert.AreEqual("A", lst(0).ToList()(0).ScoreKey)
        Assert.AreEqual("B", lst(0).ToList()(1).ScoreKey)
        Assert.AreEqual("C", lst(0).ToList()(2).ScoreKey)
        Assert.AreEqual("D", lst(0).ToList()(3).ScoreKey)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("ScoringAdv"), TestCategory("Scoring")>
    Public Sub GetScoreMap_Integer_NoSolution_Expects_Seperated()
        'Arrange
        Dim solution = New Solution()
        Dim intParam = New IntegerScoringParameter() With {.ControllerId = "McScore", .FindingOverride = "DefaultFinding"}.AddSubParameters("A", "B", "C", "D")
        Dim map = New ScoringMap(New ScoringParameter() {intParam}, solution)
        
        'Act  
        Dim lst = map.GetMap().ToList()

        'Assert
        Assert.AreEqual(4, lst.Count, "Expect 4 entries")

        Assert.AreEqual("A", lst(0).ToList()(0).ScoreKey)
        Assert.AreEqual("B", lst(1).ToList()(0).ScoreKey)
        Assert.AreEqual("C", lst(2).ToList()(0).ScoreKey)
        Assert.AreEqual("D", lst(3).ToList()(0).ScoreKey)

        Assert.IsTrue(lst.All(Function(x) x.Count() = 1))
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("ScoringAdv"), TestCategory("Scoring")>
    Public Sub GetScoreMap_3IntegerParams_NoSolution_Expects_Seperated()
        'Arrange
        Dim solution = New Solution()

        Dim intParam1 = New IntegerScoringParameter() With {.ControllerId = "McScore1", .FindingOverride = "DefaultFinding"}.AddSubParameters("A")
        Dim intParam2 = New IntegerScoringParameter() With {.ControllerId = "McScore2", .FindingOverride = "DefaultFinding"}.AddSubParameters("A")
        Dim intParam3 = New IntegerScoringParameter() With {.ControllerId = "McScore3", .FindingOverride = "DefaultFinding"}.AddSubParameters("A")

        Dim map = New ScoringMap(New ScoringParameter() {intParam1, intParam2, intParam3}, solution)
        
        'Act  
        Dim lst = map.GetMap().ToList()

        'Assert
        Assert.AreEqual(3, lst.Count, "Expect 3 entries")

        Assert.AreEqual("A", lst(0).ToList()(0).ScoreKey)
        Assert.AreEqual("A", lst(1).ToList()(0).ScoreKey)
        Assert.AreEqual("A", lst(2).ToList()(0).ScoreKey)

        Assert.IsTrue(lst.All(Function(x) x.Count() = 1))
    End Sub


    <TestMethod(), TestCategory("Logic"), TestCategory("ScoringAdv"), TestCategory("Scoring")>
    Public Sub GetScoreMap_3MC_Solution_Expects_Grouped()
        'Arrange
        Dim solution = Deserialize(Of Solution)(solutionWith3MC)
        Dim mc1 = New MultiChoiceScoringParameter() With {.ControllerId = "mc_1", .FindingOverride = "Opgave", .MaxChoices = 1}.AddSubParameters("A", "B", "C", "D")
        Dim mc2 = New MultiChoiceScoringParameter() With {.ControllerId = "mc_2", .FindingOverride = "Opgave", .MaxChoices = 1}.AddSubParameters("A", "B", "C", "D")
        Dim mc3 = New MultiChoiceScoringParameter() With {.ControllerId = "mc_3", .FindingOverride = "Opgave", .MaxChoices = 1}.AddSubParameters("A", "B", "C", "D")

        Dim map = New ScoringMap(New ScoringParameter() {mc1, mc2, mc3}, solution)
        
        'Act  
        Dim lst = map.GetMap().ToList()

        'Assert
        Assert.AreEqual(3, lst.Count, "Expect 3 entries")

        Assert.IsTrue(lst.All(Function(x) x.Count() = 4))
    End Sub



    <TestMethod(), TestCategory("Logic"), TestCategory("ScoringAdv"), TestCategory("Scoring")>
    Public Sub GetScoreMap_2factSetsIntAndMC_Expects_Grouped()
        'Arrange
        Dim solution = Deserialize(Of Solution)(solutionWithIntAndMC)
        Dim mc1 = New MultiChoiceScoringParameter() With {.ControllerId = "McScore", .FindingOverride = "DefaultFinding", .MinChoices = 1, .MaxChoices = 1}.AddSubParameters("A", "B", "C", "D")
        Dim int1 = New IntegerScoringParameter() With {.ControllerId = "integerScore", .FindingOverride = "DefaultFinding"}.AddSubParameters("1")

        Dim map = New ScoringMap(New ScoringParameter() {mc1, int1}, solution)
        
        'Act  
        Dim lst = map.GetMap().ToList()

        'Assert
        Assert.AreEqual(1, lst.Count, "Expect 1 entries")

        Assert.IsTrue(lst.All(Function(x) x.Count() = 5))
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("ScoringAdv"), TestCategory("Scoring")>
    Public Sub GetScoreMap_2factSetsIntAndMC_Expects_IsGrouped_BothTrue()
        'Arrange
        Dim solution = Deserialize(Of Solution)(solutionWithIntAndMC)
        Dim mc1 = New MultiChoiceScoringParameter() With {.ControllerId = "McScore", .FindingOverride = "DefaultFinding", .MinChoices = 1, .MaxChoices = 1}.AddSubParameters("A", "B", "C", "D")
        Dim int1 = New IntegerScoringParameter() With {.ControllerId = "integerScore", .FindingOverride = "DefaultFinding"}.AddSubParameters("1")

        Dim map = New ScoringMap(New ScoringParameter() {mc1, int1}, solution)
        
        'Act  
        Dim lst = map.GetMap().ToList()

        'Assert
        Assert.IsTrue(lst.All(Function(x) x.IsGroup))
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("ScoringAdv"), TestCategory("Scoring")>
    Public Sub GetScoreMap_2factSetsIntAndMC_Expects_2Sets()
        'Arrange
        Dim solution = Deserialize(Of Solution)(solutionWithIntAndMC)
        Dim mc1 = New MultiChoiceScoringParameter() With {.Name = "McScore", .ControllerId = "McScore", .FindingOverride = "DefaultFinding"}.AddSubParameters("A", "B", "C", "D")
        Dim int1 = New IntegerScoringParameter() With {.Name = "int", .ControllerId = "integerScore", .FindingOverride = "DefaultFinding"}.AddSubParameters("1")

        Dim map = New ScoringMap(New ScoringParameter() {mc1, int1}, solution)
        
        'Act  
        Dim lst = map.GetMap().ToList()

        'Assert
        Assert.AreEqual(2, lst(0).SetNumbers.Count())
    End Sub


    <TestMethod(), TestCategory("Logic"), TestCategory("ScoringAdv"), TestCategory("Scoring")>
    Public Sub GetScoreMap_MR_Solution_Expects_RowPerMCItem()
        'Arrange
        Dim solution = Deserialize(Of Solution)(solutionWith3MC)
        Dim mc1 = New MultiChoiceScoringParameter() With {.ControllerId = "mc_1", .FindingOverride = "Opgave", .MaxChoices = 4}.AddSubParameters("A", "B", "C", "D")

        Dim map = New ScoringMap(New ScoringParameter() {mc1}, solution)
        
        'Act  
        Dim lst As IEnumerable(Of CombinedScoringMapKey) = map.GetMap().ToList()

        'Assert
        Assert.AreEqual(4, lst.Count, "Expect 4 entries")
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("ScoringAdv"), TestCategory("Scoring")>
    Public Sub GetScoreMap_MR_Solution_With_FactSets_Expects_RowPerMCItem()
        'Arrange
        Dim solution = Deserialize(Of Solution)(solutionMR2FS)
        Dim mc1 = New MultiChoiceScoringParameter() With {.ControllerId = "mc_1", .FindingOverride = "Opgave", .MaxChoices = 4}.AddSubParameters("A", "B", "C", "D")

        Dim map = New ScoringMap(New ScoringParameter() {mc1}, solution)
       
        'Act  
        Dim lst As IEnumerable(Of CombinedScoringMapKey) = map.GetMap().ToList()

        'Assert
        Assert.AreEqual(1, lst.Count, "Expect 1 row")
        Assert.IsTrue(lst(0).IsGroup)
    End Sub


    <TestMethod(), TestCategory("Logic"), TestCategory("ScoringAdv"), TestCategory("Scoring")>
    Public Sub GetMapFor_solutionWith1FactSetWith2integerPrm_Expects2CombinedScoringMapKeys()
        'Arrange
        Dim solution = Deserialize(Of Solution)(solutionWith1FactSetWith2integerPrm)
        Dim intPrm = New IntegerScoringParameter() With {.ControllerId = "integerScore", .FindingOverride = "sharedIntegerFinding", .Name = "Int"}.AddSubParameters("1", "2", "3") 'Notice that the 3th is not mentioned in the solution.

        Dim map = New ScoringMap(New ScoringParameter() {intPrm}, solution)
        
        'Act  
        Dim lst As IEnumerable(Of CombinedScoringMapKey) = map.GetMap().ToList()

        'Assert
        Assert.AreEqual(2, lst.Count, "Expect 2 entries")
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("ScoringAdv"), TestCategory("Scoring")>
    Public Sub GetMapFor_solutionWith1FactSetWith2integerPrm_ExpectsCombinedScoringMap_ForInt3_InNoFactSet()
        'Arrange
        Dim solution = Deserialize(Of Solution)(solutionWith1FactSetWith2integerPrm)
        Dim intPrm = New IntegerScoringParameter() With {.ControllerId = "integerScore", .FindingOverride = "sharedIntegerFinding", .Name = "Int"}.AddSubParameters("1", "2", "3") 'Notice that the 3rd is not mentioned in the solution.

        Dim map = New ScoringMap(New ScoringParameter() {intPrm}, solution)
        
        'Act  
        Dim last As CombinedScoringMapKey = map.GetMap().ToList().Last()

        'Assert
        Assert.AreEqual("Int.3", last.Name)
        Assert.AreEqual(0, last.SetNumbers.Count())
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("ScoringAdv"), TestCategory("Scoring")>
    Public Sub GetMapFor_solutionWith1FactSetWith2MatrixPrm_ExpectsCombinedScoringMap_ForMatrix3_InNoFactSet()
        'Arrange
        Dim solution = Deserialize(Of Solution)(_3MatrixInteractions_2Grouped)
        Dim matrixPrm = New MatrixScoringParameter() With {.ControllerId = "matrix", .FindingOverride = "matrix", .Name = "matrixScoring"}.AddSubParameters("1", "2", "3")

        Dim map = New ScoringMap(New ScoringParameter() {matrixPrm}, solution)

        'Act  
        Dim last As CombinedScoringMapKey = map.GetMap().ToList().Last()

        'Assert
        Assert.AreEqual("matrixScoring.3", last.Name)
        Assert.AreEqual(0, last.SetNumbers.Count())
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("ScoringAdv"), TestCategory("Scoring")>
    Public Sub GetScoringMapOnEmptySolutionForOrderdParameter()
        'Arrange
        Dim solution = New Solution()
        Dim orderScoringParam = New OrderScoringParameter() With {.ControllerId = "xyz"}.AddSubParameters("A", "B", "C")
        
        'Act
        Dim scoringMap = New ScoringMap(New ScoringParameter() {orderScoringParam}, solution)
        Dim mapList = scoringMap.GetMap()
        Dim firstCombinedScoringMapKey = mapList.First()
        
        'Assert
        Assert.AreEqual(1, mapList.Count())
        Assert.AreEqual(True, firstCombinedScoringMapKey.IsGroup)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("ScoringAdv"), TestCategory("Scoring")>
    Public Sub GetConceptScoringMapOnEmptySolutionForOrderdParameter()
        'Arrange
        Dim solution = New Solution()
        Dim orderScoringParam = New OrderScoringParameter() With {.ControllerId = "xyz"}.AddSubParameters("A", "B", "C")
        
        'Act
        Dim scoringMap = New ConceptScoringMap(New ScoringParameter() {orderScoringParam}, solution)
        Dim mapList = scoringMap.GetMap()
        Dim firstCombinedScoringMapKey As CombinedScoringMapKey = mapList.First()
        
        'Assert
        Assert.AreEqual(1, mapList.Count())
        Assert.AreEqual(True, firstCombinedScoringMapKey.IsGroup)
        Assert.AreEqual(0, firstCombinedScoringMapKey.SetNumbers.Count())
    End Sub

#Region "Data"
    Private ReadOnly NoFactSets As XElement = <solution>
                                                  <keyFindings>
                                                      <keyFinding id="Controller" scoringMethod="None">
                                                          <keyFact id="A-Controller" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                              <keyValue domain="Controller" occur="1">
                                                                  <integerValue>
                                                                      <typedValue>1</typedValue>
                                                                  </integerValue>
                                                              </keyValue>
                                                          </keyFact>
                                                      </keyFinding>
                                                  </keyFindings>
                                                  <aspectReferences/>
                                              </solution>

    '+--------+ 
    '| I1=111 | 
    '+--------+ 
    '+--------+ 
    '| I2=222 | 
    '+--------+ 
    Private ReadOnly NoFactSets2Integers As XElement = <solution>
                                                           <keyFindings>
                                                               <keyFinding id="Controller" scoringMethod="None">

                                                                   <keyFact id="A-Controller1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                       <keyValue domain="Controller1" occur="1">
                                                                           <integerValue>
                                                                               <typedValue>111</typedValue>
                                                                           </integerValue>
                                                                       </keyValue>
                                                                   </keyFact>

                                                                   <keyFact id="A-Controller2" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                       <keyValue domain="Controller2" occur="1">
                                                                           <integerValue>
                                                                               <typedValue>222</typedValue>
                                                                           </integerValue>
                                                                       </keyValue>
                                                                   </keyFact>

                                                               </keyFinding>
                                                           </keyFindings>
                                                           <aspectReferences/>
                                                       </solution>

    '+------+   +------+
    '| 1= 6 |   | 1=14 |
    '|  = 7 |   |      |
    '|  = 8 |   |      |
    '| 2=14 |   | 2= 6 |
    '+------+   +------+
    '+------+   +------+
    '| 3= 3 |   | 4= 7 |
    '| 4= 7 |   | 4= 3 |
    '+------+   +------+
    '+------+           
    '| 5= 1 |           
    '|  = 2 |           
    '+------+           
    Private ReadOnly solutionWith2FactSetGroupsAnd1SpInFinding As XElement = <solution>
                                                                                 <keyFindings>
                                                                                     <keyFinding id="sharedIntegerFinding" scoringMethod="Dichotomous">
                                                                                         <keyFactSet>
                                                                                             <keyFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                                 <keyValue domain="integerScore" occur="1">
                                                                                                     <integerValue>
                                                                                                         <typedValue>6</typedValue>
                                                                                                     </integerValue>
                                                                                                     <integerValue>
                                                                                                         <typedValue>7</typedValue>
                                                                                                     </integerValue>
                                                                                                     <integerValue>
                                                                                                         <typedValue>8</typedValue>
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
                                                                                         <keyFact id="5-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                             <keyValue domain="integerScore" occur="1">
                                                                                                 <integerValue>
                                                                                                     <typedValue>1</typedValue>
                                                                                                 </integerValue>
                                                                                                 <integerValue>
                                                                                                     <typedValue>2</typedValue>
                                                                                                 </integerValue>
                                                                                             </keyValue>
                                                                                         </keyFact>
                                                                                     </keyFinding>
                                                                                 </keyFindings>
                                                                             </solution>

    '+----++----+         
    '|A =2||A =1|         
    '|MC=A||MC=B|         
    '+----++----+         
    Private ReadOnly solutionWithIntAndMCInFactSets As XElement = <solution>
                                                                      <keyFindings>
                                                                          <keyFinding id="DefaultFinding" scoringMethod="Dichotomous">

                                                                              <keyFactSet>
                                                                                  <keyFact id="A-McScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                      <keyValue domain="McScore" occur="1">
                                                                                          <stringValue>
                                                                                              <typedValue>A</typedValue>
                                                                                          </stringValue>
                                                                                      </keyValue>
                                                                                  </keyFact>
                                                                                  <keyFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                      <keyValue domain="integerScore" occur="1">
                                                                                          <integerValue>
                                                                                              <typedValue>2</typedValue>
                                                                                          </integerValue>
                                                                                      </keyValue>
                                                                                  </keyFact>
                                                                              </keyFactSet>

                                                                              <keyFactSet>
                                                                                  <keyFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                      <keyValue domain="integerScore" occur="1">
                                                                                          <integerValue>
                                                                                              <typedValue>1</typedValue>
                                                                                          </integerValue>
                                                                                      </keyValue>
                                                                                  </keyFact>
                                                                                  <keyFact id="B-McScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                      <keyValue domain="McScore" occur="1">
                                                                                          <stringValue>
                                                                                              <typedValue>B</typedValue>
                                                                                          </stringValue>
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

    '+----++----+         
    '|A =2||A =1|         
    '|MC=A||    |         
    '+----++----+         
    Private ReadOnly solutionWithIntAndMCInFactSets_MCB_NotSet As XElement = <solution>
                                                                                 <keyFindings>
                                                                                     <keyFinding id="DefaultFinding" scoringMethod="Dichotomous">

                                                                                         <keyFactSet>
                                                                                             <keyFact id="A-McScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                                 <keyValue domain="McScore" occur="1">
                                                                                                     <stringValue>
                                                                                                         <typedValue>A</typedValue>
                                                                                                     </stringValue>
                                                                                                 </keyValue>
                                                                                             </keyFact>
                                                                                             <keyFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                                 <keyValue domain="integerScore" occur="1">
                                                                                                     <integerValue>
                                                                                                         <typedValue>2</typedValue>
                                                                                                     </integerValue>
                                                                                                 </keyValue>
                                                                                             </keyFact>
                                                                                         </keyFactSet>

                                                                                         <keyFactSet>
                                                                                             <keyFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                                 <keyValue domain="integerScore" occur="1">
                                                                                                     <integerValue>
                                                                                                         <typedValue>1</typedValue>
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

    '+----++----+         
    '|A =2||A =1|         
    '|    ||MC=B|         
    '+----++----+         
    Private ReadOnly solutionWithIntAndMCInFactSets_MCA_NotSet As XElement = <solution>
                                                                                 <keyFindings>
                                                                                     <keyFinding id="DefaultFinding" scoringMethod="Dichotomous">

                                                                                         <keyFactSet>
                                                                                             <keyFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                                 <keyValue domain="integerScore" occur="1">
                                                                                                     <integerValue>
                                                                                                         <typedValue>2</typedValue>
                                                                                                     </integerValue>
                                                                                                 </keyValue>
                                                                                             </keyFact>
                                                                                         </keyFactSet>

                                                                                         <keyFactSet>
                                                                                             <keyFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                                 <keyValue domain="integerScore" occur="1">
                                                                                                     <integerValue>
                                                                                                         <typedValue>1</typedValue>
                                                                                                     </integerValue>
                                                                                                 </keyValue>
                                                                                             </keyFact>
                                                                                             <keyFact id="B-McScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                                 <keyValue domain="McScore" occur="1">
                                                                                                     <stringValue>
                                                                                                         <typedValue>B</typedValue>
                                                                                                     </stringValue>
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

    '+--------+ 
    '| MC1=A  | 
    '+--------+ 
    '+--------+ 
    '| MC2=B  | 
    '+--------+ 
    '+--------+ 
    '| MC3=DC | 
    '+--------+ 
    Private ReadOnly solutionWith3MC As XElement = <solution>
                                                       <keyFindings>
                                                           <keyFinding id="Opgave" scoringMethod="Dichotomous">
                                                               <keyFact id="A-mc_1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                   <keyValue domain="mc_1" occur="1">
                                                                       <stringValue>
                                                                           <typedValue>A</typedValue>
                                                                       </stringValue>
                                                                   </keyValue>
                                                               </keyFact>
                                                               <keyFact id="B-mc_2" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                   <keyValue domain="mc_2" occur="1">
                                                                       <stringValue>
                                                                           <typedValue>B</typedValue>
                                                                       </stringValue>
                                                                   </keyValue>
                                                               </keyFact>
                                                               <keyFact id="D-mc_3" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                   <keyValue domain="mc_3" occur="1">
                                                                       <stringValue>
                                                                           <typedValue>D</typedValue>
                                                                       </stringValue>
                                                                   </keyValue>
                                                               </keyFact>
                                                               <keyFact id="C-mc_3" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                   <keyValue domain="mc_3" occur="1">
                                                                       <stringValue>
                                                                           <typedValue>C</typedValue>
                                                                       </stringValue>
                                                                   </keyValue>
                                                               </keyFact>
                                                           </keyFinding>
                                                       </keyFindings>
                                                       <conceptFindings>
                                                           <conceptFinding id="Opgave" scoringMethod="None"/>
                                                       </conceptFindings>
                                                       <aspectReferences/>
                                                   </solution>

    ' +----++----+ 
    ' |A =2||A =1| 
    ' |MC=A||MC=B| 
    ' +----++----+    
    Private ReadOnly solutionWithIntAndMC As XElement = <solution>
                                                            <keyFindings>
                                                                <keyFinding id="DefaultFinding" scoringMethod="Dichotomous">
                                                                    <keyFactSet>
                                                                        <keyFact id="A-McScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                            <keyValue domain="McScore" occur="1">
                                                                                <stringValue>
                                                                                    <typedValue>A</typedValue>
                                                                                </stringValue>
                                                                            </keyValue>
                                                                        </keyFact>
                                                                        <keyFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                            <keyValue domain="integerScore" occur="1">
                                                                                <integerValue>
                                                                                    <typedValue>2</typedValue>
                                                                                </integerValue>
                                                                            </keyValue>
                                                                        </keyFact>
                                                                    </keyFactSet>
                                                                    <keyFactSet>
                                                                        <keyFact id="A-McScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                            <keyValue domain="McScore" occur="1">
                                                                                <stringValue>
                                                                                    <typedValue>B</typedValue>
                                                                                </stringValue>
                                                                            </keyValue>
                                                                        </keyFact>
                                                                        <keyFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                            <keyValue domain="integerScore" occur="1">
                                                                                <integerValue>
                                                                                    <typedValue>1</typedValue>
                                                                                </integerValue>
                                                                            </keyValue>
                                                                        </keyFact>
                                                                    </keyFactSet>
                                                                </keyFinding>
                                                            </keyFindings>
                                                            <aspectReferences/>
                                                        </solution>

    ' +----------++--------+ 
    ' |MC.A = X  ||MC.A =- | 
    ' |MC.B = -  ||MC.B =- | 
    ' |MC.C = -  ||MC.C =X | 
    ' |MC.D = -  ||MC.D =- | 
    ' +----------++--------+ 
    Private ReadOnly solutionMR2FS As XElement = <solution>
                                                     <keyFindings>
                                                         <keyFinding id="Opgave" scoringMethod="Dichotomous">
                                                             <keyFactSet>
                                                                 <keyFact id="A-mc_1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                     <keyValue domain="mc_1" occur="1">
                                                                         <booleanValue>
                                                                             <typedValue>true</typedValue>
                                                                         </booleanValue>
                                                                     </keyValue>
                                                                 </keyFact>
                                                                 <keyFact id="B-mc_1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                     <keyValue domain="mc_1" occur="1">
                                                                         <booleanValue>
                                                                             <typedValue>false</typedValue>
                                                                         </booleanValue>
                                                                     </keyValue>
                                                                 </keyFact>
                                                                 <keyFact id="C-mc_1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                     <keyValue domain="mc_1" occur="1">
                                                                         <booleanValue>
                                                                             <typedValue>false</typedValue>
                                                                         </booleanValue>
                                                                     </keyValue>
                                                                 </keyFact>
                                                                 <keyFact id="D-mc_1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                     <keyValue domain="mc_1" occur="1">
                                                                         <booleanValue>
                                                                             <typedValue>false</typedValue>
                                                                         </booleanValue>
                                                                     </keyValue>
                                                                 </keyFact>
                                                             </keyFactSet>
                                                             <keyFactSet>
                                                                 <keyFact id="A-mc_1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                     <keyValue domain="mc_1" occur="1">
                                                                         <booleanValue>
                                                                             <typedValue>false</typedValue>
                                                                         </booleanValue>
                                                                     </keyValue>
                                                                 </keyFact>
                                                                 <keyFact id="B-mc_1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                     <keyValue domain="mc_1" occur="1">
                                                                         <booleanValue>
                                                                             <typedValue>false</typedValue>
                                                                         </booleanValue>
                                                                     </keyValue>
                                                                 </keyFact>
                                                                 <keyFact id="C-mc_1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                     <keyValue domain="mc_1" occur="1">
                                                                         <booleanValue>
                                                                             <typedValue>true</typedValue>
                                                                         </booleanValue>
                                                                     </keyValue>
                                                                 </keyFact>
                                                                 <keyFact id="D-mc_1" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                     <keyValue domain="mc_1" occur="1">
                                                                         <booleanValue>
                                                                             <typedValue>true</typedValue>
                                                                         </booleanValue>
                                                                     </keyValue>
                                                                 </keyFact>
                                                             </keyFactSet>
                                                         </keyFinding>
                                                     </keyFindings>
                                                     <aspectReferences/>
                                                 </solution>

    Private ReadOnly solutionWith1FactSetWith2integerPrm As XElement = <solution>
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
                                                                               </keyFinding>
                                                                           </keyFindings>
                                                                       </solution>

    Private _3MatrixInteractions_2Grouped As XElement =
        <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
            <keyFindings>
                <keyFinding id="matrix" scoringMethod="Dichotomous">
                    <keyFact id="3-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="matrix" occur="1">
                            <stringValue>
                                <typedValue>B</typedValue>
                            </stringValue>
                        </keyValue>
                    </keyFact>
                    <keyFactSet>
                        <keyFact id="1-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="matrix" occur="1">
                                <stringValue>
                                    <typedValue>A</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="2-matrix" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="matrix" occur="1">
                                <stringValue>
                                    <typedValue>A</typedValue>
                                </stringValue>
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

#End Region

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