
Imports System.Linq
Imports System.Xml.Linq
Imports Cito.Tester.ContentModel
Imports XmlTools = Questify.Builder.UnitTests.Framework.Util.XmlTools

<TestClass>
Public Class SolutionPreprocessingTests : Inherits SerializationTestBase

    <TestMethod()> <TestCategory("ContentModel"), TestCategory("Solution")>
    Public Sub KeyPreprocessingRuleTest()
        Dim preProcRef = New SelectedPreprocessor() With {.Rule = "t"}
        Dim integerValue = New IntegerValue(42)
        Dim v = New KeyValue("someDomain", 1, integerValue) : v.PreProcessingRules.Add(preProcRef)
        Dim f = New KeyFact("keyId") : f.Values.Add(v)
        Dim finding = New KeyFinding("Finding") : finding.Facts.Add(f)
        Dim solution = New Solution() : solution.Findings.Add(finding)

        Dim result = DoSerialize(solution)

        Assert.IsTrue(XmlTools.DeepEqualsWithNormalization(New XDocument(solution_keyPreprocessingRule), New XDocument(result), Nothing))
    End Sub

    <TestMethod()> <TestCategory("ContentModel"), TestCategory("Solution")>
    Public Sub ConteptPreprocessingRuleTest()
        Dim preProcRef = New SelectedPreprocessor() With {.Rule = "t"}
        Dim integerValue = New IntegerValue(42)
        Dim v = New ConceptValue("someDomain", 1, integerValue) : v.PreProcessingRules.Add(preProcRef)
        Dim f = New ConceptFact("keyId") : f.Values.Add(v)
        Dim finding = New ConceptFinding("Finding") : finding.Facts.Add(f)
        Dim solution = New Solution() : solution.ConceptFindings.Add(finding)

        Dim result = DoSerialize(solution)

        Assert.IsTrue(XmlTools.DeepEqualsWithNormalization(New XDocument(solution_conceptProcessingRule), New XDocument(result), Nothing))
    End Sub

    <TestMethod()> <TestCategory("ContentModel"), TestCategory("Solution")>
    Public Sub DeserializeSolutionWithConceptPreprocessingRule_Hasit()
        Dim solution = Deserialize(Of Solution)(solution_conceptProcessingRule)

        Dim tmp = solution.ConceptFindings.First().Facts.First().Values.First()
        Dim conceptFact = DirectCast(tmp, ConceptValue)

        Assert.IsTrue(conceptFact.PreProcessingRules.Count > 0)
    End Sub

    ReadOnly solution_keyPreprocessingRule As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                                             <keyFindings>
                                                                 <keyFinding id="Finding" scoringMethod="None">
                                                                     <keyFact id="keyId" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                         <keyValue domain="someDomain" occur="1">
                                                                             <integerValue>
                                                                                 <typedValue>42</typedValue>
                                                                             </integerValue>
                                                                             <preprocessingRule>
                                                                                 <ruleName>t</ruleName>
                                                                             </preprocessingRule>
                                                                         </keyValue>
                                                                     </keyFact>
                                                                 </keyFinding>
                                                             </keyFindings>
                                                             <aspectReferences/>
                                                         </solution>

    ReadOnly solution_conceptProcessingRule As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                                              <keyFindings/>
                                                              <conceptFindings>
                                                                  <conceptFinding id="Finding" scoringMethod="None">
                                                                      <conceptFact id="keyId" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                          <conceptValue domain="someDomain" occur="1">
                                                                              <integerValue>
                                                                                  <typedValue>42</typedValue>
                                                                              </integerValue>
                                                                              <preprocessingRule>
                                                                                  <ruleName>t</ruleName>
                                                                              </preprocessingRule>
                                                                          </conceptValue>
                                                                      </conceptFact>
                                                                  </conceptFinding>
                                                              </conceptFindings>
                                                              <aspectReferences/>
                                                          </solution>

End Class
