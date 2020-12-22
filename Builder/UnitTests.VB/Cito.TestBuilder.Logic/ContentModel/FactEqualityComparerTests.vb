
Imports System.Xml.Linq
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ContentModel.Scoring
Imports Questify.Builder.UnitTests.Framework

<TestClass>
Public Class FactEqualityComparerTests

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("ScoringAdv")>
    Public Sub SimpleFactsWithSameId_AreEqual()
        Dim solution = _simpleFact.To(Of Solution)()
        Dim comparer = GetComparer()
        Dim fact1 = DirectCast(solution.Findings(0).Facts(0), KeyFact)
        Dim fact2 = DirectCast(solution.Findings(0).Facts(1), KeyFact)

        Dim result = comparer.Equals(fact1, fact2)

        Assert.IsTrue(result)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("ScoringAdv")>
    Public Sub SimpleFactsWithSameId_HashcodeEquals()
        Dim solution = _simpleFact.To(Of Solution)()

        Dim fact1 = DirectCast(solution.Findings(0).Facts(0), KeyFact)
        Dim fact2 = DirectCast(solution.Findings(0).Facts(1), KeyFact)
        Dim areSame = GetComparer().Equals(fact1, fact2)

        Dim result1 = New FactEqualityComparer().CompareGetHashCode(fact1)
        Dim result2 = New FactEqualityComparer().CompareGetHashCode(fact2)

        Assert.AreEqual(result1, result2)
        Assert.IsTrue(areSame)
    End Sub
    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("ScoringAdv")>
    Public Sub SimpleFactsWithDifferentSameId_AreNotEqual()
        Dim solution = _simpleFactDifferentFactId.To(Of Solution)()
        Dim comparer As IEqualityComparer(Of KeyFact) = New FactEqualityComparer()
        Dim fact1 = DirectCast(solution.Findings(0).Facts(0), KeyFact)
        Dim fact2 = DirectCast(solution.Findings(0).Facts(1), KeyFact)

        Dim result = comparer.Equals(fact1, fact2)

        Assert.IsFalse(result)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("ScoringAdv")>
    Public Sub SimpleFactsWithDifferentSameId_HashcodeNotEquals()
        Dim solution = _simpleFactDifferentFactId.To(Of Solution)()

        Dim fact1 = DirectCast(solution.Findings(0).Facts(0), KeyFact)
        Dim fact2 = DirectCast(solution.Findings(0).Facts(1), KeyFact)
        Dim areSame = GetComparer().Equals(fact1, fact2)

        Dim result1 = GetComparer().GetHashCode(fact1)
        Dim result2 = GetComparer().GetHashCode(fact2)

        Assert.AreNotEqual(result1, result2)
        Assert.IsFalse(areSame)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("ScoringAdv")>
    Public Sub SimpleFactsWithDifferentDomainId_AreNotEqual()
        Dim solution = _simpleFactDifferentDomainId.To(Of Solution)()
        Dim comparer As IEqualityComparer(Of KeyFact) = New FactEqualityComparer()
        Dim fact1 = DirectCast(solution.Findings(0).Facts(0), KeyFact)
        Dim fact2 = DirectCast(solution.Findings(0).Facts(1), KeyFact)

        Dim result = comparer.Equals(fact1, fact2)

        Assert.IsFalse(result)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("ScoringAdv")>
    Public Sub SimpleFactsWithDifferentDomainId_HashcodeNotEquals()
        Dim solution = _simpleFactDifferentDomainId.To(Of Solution)()

        Dim fact1 = DirectCast(solution.Findings(0).Facts(0), KeyFact)
        Dim fact2 = DirectCast(solution.Findings(0).Facts(1), KeyFact)
        Dim areSame = GetComparer().Equals(fact1, fact2)

        Dim result1 = GetComparer().GetHashCode(fact1)
        Dim result2 = GetComparer().GetHashCode(fact2)

        Assert.AreNotEqual(result1, result2)
        Assert.IsFalse(areSame)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("ScoringAdv")>
    Public Sub SimpleFactsWithDifferentValues_AreNotEqual()
        Dim solution = _simpleFactValuesNotSame.To(Of Solution)()
        Dim comparer As IEqualityComparer(Of KeyFact) = New FactEqualityComparer()
        Dim fact1 = DirectCast(solution.Findings(0).Facts(0), KeyFact)
        Dim fact2 = DirectCast(solution.Findings(0).Facts(1), KeyFact)

        Dim result = comparer.Equals(fact1, fact2)

        Assert.IsFalse(result)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring"), TestCategory("ScoringAdv")>
    Public Sub SimpleFactsWithDifferentValues_HashcodeNotEquals()
        Dim solution = _simpleFactValuesNotSame.To(Of Solution)()

        Dim fact1 = DirectCast(solution.Findings(0).Facts(0), KeyFact)
        Dim fact2 = DirectCast(solution.Findings(0).Facts(1), KeyFact)
        Dim areSame = GetComparer().Equals(fact1, fact2)

        Dim result1 = GetComparer().GetHashCode(fact1)
        Dim result2 = GetComparer().GetHashCode(fact2)

        Assert.AreNotEqual(result1, result2)
        Assert.IsFalse(areSame)
    End Sub

    ReadOnly _simpleFact As XElement = <solution>
                                           <keyFindings>
                                               <keyFinding id="sharedIntegerFinding" scoringMethod="Dichotomous">

                                                   <keyFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                       <keyValue domain="integerScore" occur="1">
                                                           <integerValue>
                                                               <typedValue>6</typedValue>
                                                           </integerValue>
                                                       </keyValue>
                                                   </keyFact>
                                                   <keyFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                       <keyValue domain="integerScore" occur="1">
                                                           <integerValue>
                                                               <typedValue>6</typedValue>
                                                           </integerValue>
                                                       </keyValue>
                                                   </keyFact>

                                               </keyFinding>
                                           </keyFindings>
                                       </solution>

    ReadOnly _simpleFactDifferentFactId As XElement = <solution>
                                                          <keyFindings>
                                                              <keyFinding id="sharedIntegerFinding" scoringMethod="Dichotomous">

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
                                                                              <typedValue>6</typedValue>
                                                                          </integerValue>
                                                                      </keyValue>
                                                                  </keyFact>

                                                              </keyFinding>
                                                          </keyFindings>
                                                      </solution>

    ReadOnly _simpleFactDifferentDomainId As XElement = <solution>
                                                            <keyFindings>
                                                                <keyFinding id="sharedIntegerFinding" scoringMethod="Dichotomous">

                                                                    <keyFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                        <keyValue domain="1integerScore1" occur="1">
                                                                            <integerValue>
                                                                                <typedValue>6</typedValue>
                                                                            </integerValue>
                                                                        </keyValue>
                                                                    </keyFact>
                                                                    <keyFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                        <keyValue domain="2integerScore2" occur="1">
                                                                            <integerValue>
                                                                                <typedValue>6</typedValue>
                                                                            </integerValue>
                                                                        </keyValue>
                                                                    </keyFact>

                                                                </keyFinding>
                                                            </keyFindings>
                                                        </solution>

    ReadOnly _simpleFactValuesNotSame As XElement = <solution>
                                                        <keyFindings>
                                                            <keyFinding id="sharedIntegerFinding" scoringMethod="Dichotomous">

                                                                <keyFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                    <keyValue domain="integerScore" occur="1">
                                                                        <integerValue>
                                                                            <typedValue>1</typedValue>
                                                                        </integerValue>
                                                                    </keyValue>
                                                                </keyFact>
                                                                <keyFact id="1-integerScore" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                    <keyValue domain="integerScore" occur="1">
                                                                        <integerValue>
                                                                            <typedValue>8888</typedValue>
                                                                        </integerValue>
                                                                    </keyValue>
                                                                </keyFact>

                                                            </keyFinding>
                                                        </keyFindings>
                                                    </solution>

    Private Function GetComparer() As IEqualityComparer(Of KeyFact)
        Return New FactEqualityComparer()
    End Function

End Class
