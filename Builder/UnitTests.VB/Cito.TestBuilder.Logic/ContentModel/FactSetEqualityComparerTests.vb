
Imports System.Xml.Linq
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ContentModel.Scoring
Imports Questify.Builder.UnitTests.Framework

<TestClass>
Public Class FactSetEqualityComparerTests

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("ScoringAdv")>
    Public Sub CompareSetsThatShouldBeEqual()
        'Arrange
        Dim solution = _solution.To(Of Solution)()
        Dim keyFactset1 As KeyFactSet = solution.Findings(0).KeyFactsets(0)
        Dim conceptFactSet1 As KeyFactSet = solution.ConceptFindings(0).KeyFactsets(0)
        
        'Act
        Dim result = GetComparer().Equals(keyFactset1, conceptFactSet1)
        
        'Assert
        Assert.IsTrue(result)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("ScoringAdv")>
    Public Sub CompareSetsThatShouldNotBeEqual()
        'Arrange
        Dim solution = _solution.To(Of Solution)()
        Dim keyFactset1 As KeyFactSet = solution.Findings(0).KeyFactsets(0)
        Dim conceptFactSet1 As KeyFactSet = solution.ConceptFindings(0).KeyFactsets(1)
        
        'Act
        Dim result = GetComparer().Equals(keyFactset1, conceptFactSet1)
        
        'Assert
        Assert.IsFalse(result)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("ScoringAdv")>
    Public Sub CompareCatchAllSets_ShouldNotMatchAnyKeyFactSet()
        'Arrange
        Dim solution = _solution.To(Of Solution)()
        Dim keyFactset1 As KeyFactSet = solution.Findings(0).KeyFactsets(0)
        Dim keyFactset2 As KeyFactSet = solution.Findings(0).KeyFactsets(1)
        Dim catchAllFactSet As KeyFactSet = solution.ConceptFindings(0).KeyFactsets(2)
        
        'Act
        Dim result1 = GetComparer().Equals(keyFactset1, catchAllFactSet)
        Dim result2 = GetComparer().Equals(keyFactset2, catchAllFactSet)
        
        'Assert
        Assert.IsFalse(result1)
        Assert.IsFalse(result2)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("ScoringAdv")>
    Public Sub CompareSetsFactidReversedThatShouldBeEqual()
        'Arrange
        Dim solution = _solution.To(Of Solution)()
        Dim keyFactset1 As KeyFactSet = solution.Findings(0).KeyFactsets(0)
        Dim conceptFactSet1 As KeyFactSet = solution.ConceptFindings(0).KeyFactsets(3)
        
        'Act
        Dim result = GetComparer().Equals(keyFactset1, conceptFactSet1)
        
        'Assert
        Assert.IsTrue(result)
    End Sub

    Private Function GetComparer() As IEqualityComparer(Of KeyFactSet)
        Return New FactSetEqualityComparer()
    End Function

#Region "Data"

    Private _solution As XElement = <solution>
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
                                                   <conceptValue domain="integerScore" occur="1">
                                                       <integerValue>
                                                           <typedValue>6</typedValue>
                                                       </integerValue>
                                                   </conceptValue>
                                               </conceptFact>
                                               <conceptFact id="2-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                   <conceptValue domain="integerScore" occur="1">
                                                       <integerValue>
                                                           <typedValue>14</typedValue>
                                                       </integerValue>
                                                   </conceptValue>
                                               </conceptFact>
                                               <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                                                   <concept value="1" code="1"/>
                                                   <concept value="1" code="1.1"/>
                                               </concepts>
                                           </conceptFactSet>
                                           <conceptFactSet>
                                               <conceptFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                   <conceptValue domain="integerScore" occur="1">
                                                       <integerValue>
                                                           <typedValue>14</typedValue>
                                                       </integerValue>
                                                   </conceptValue>
                                               </conceptFact>
                                               <conceptFact id="2-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                   <conceptValue domain="integerScore" occur="1">
                                                       <integerValue>
                                                           <typedValue>6</typedValue>
                                                       </integerValue>
                                                   </conceptValue>
                                               </conceptFact>
                                               <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                                                   <concept value="2" code="1"/>
                                                   <concept value="2" code="1.1"/>
                                               </concepts>
                                           </conceptFactSet>
                                           <conceptFactSet>
                                               <conceptFact id="1[*]-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                   <conceptValue domain="integerScore" occur="1">
                                                       <catchAllValue/>
                                                   </conceptValue>
                                               </conceptFact>
                                               <conceptFact id="2[*]-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                   <conceptValue domain="integerScore" occur="1">
                                                       <catchAllValue/>
                                                   </conceptValue>
                                               </conceptFact>
                                               <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                                                   <concept value="3" code="1"/>
                                                   <concept value="3" code="1.1"/>
                                               </concepts>
                                           </conceptFactSet>
                                           <conceptFactSet>
                                               <conceptFact id="2-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                   <conceptValue domain="integerScore" occur="1">
                                                       <integerValue>
                                                           <typedValue>14</typedValue>
                                                       </integerValue>
                                                   </conceptValue>
                                               </conceptFact>
                                               <conceptFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                   <conceptValue domain="integerScore" occur="1">
                                                       <integerValue>
                                                           <typedValue>6</typedValue>
                                                       </integerValue>
                                                   </conceptValue>
                                               </conceptFact>
                                               <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                                                   <concept value="1" code="1"/>
                                                   <concept value="1" code="1.1"/>
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
