
Imports System.Xml.Linq
Imports Cito.Tester.ContentModel
Imports XmlTools = Questify.Builder.UnitTests.Framework.Util.XmlTools

<TestClass>
Public Class ConceptFactsSetTests : Inherits SerializationTestBase

    <TestMethod()> <TestCategory("ContentModel"), TestCategory("Solution")>
    Public Sub SerializeTestAndCompare()
        Dim solution = New Solution
        Dim conceptFinding = New ConceptFinding With {.Id = "Id needs to be set"}
        Dim conceptset = New ConceptFactsSet
        Dim conceptFact = New ConceptFact
        Dim conceptValue = New ConceptValue

        conceptValue.Values.Add(New CatchAllValue())
        conceptFact.Values.Add(conceptValue)
        conceptset.Facts.Add(conceptFact)
        conceptFinding.KeyFactsets.Add(conceptset)
        solution.Findings.Add(conceptFinding)

        Dim result = DoSerialize(Of Solution)(solution)

        Assert.IsTrue(XmlTools.DeepEqualsWithNormalization(New XDocument(_recorded), new XDocument(result), Nothing))
    End Sub

    <TestMethod()> <TestCategory("ContentModel"), TestCategory("Solution")>
    Public Sub SerializeTestAndCompareWithConceptValue()
        Dim solution = New Solution
        Dim conceptFinding = New ConceptFinding With {.Id = "Id needs to be set"}
        Dim conceptset = New ConceptFactsSet
        Dim conceptFact = New ConceptFact
        Dim conceptValue = New ConceptValue

        conceptValue.Values.Add(New CatchAllValue())
        conceptFact.Values.Add(conceptValue)
        conceptset.Facts.Add(conceptFact)
        conceptFinding.KeyFactsets.Add(conceptset)
        solution.Findings.Add(conceptFinding)

        conceptset.Concepts.Add("someCode", 1)
        conceptset.Concepts.Add("someOtherCode", 42)

        Dim result = DoSerialize(Of Solution)(solution)

        Assert.IsTrue(XmlTools.DeepEqualsWithNormalization(New XDocument(_recorded2), new XDocument(result), Nothing))
    End Sub


    ReadOnly _recorded As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                         <keyFindings>
                                             <keyFinding xsi:type="ConceptFinding" id="Id needs to be set" scoringMethod="None">
                                                 <conceptFactSet>
                                                     <conceptFact score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                         <conceptValue occur="0">
                                                             <catchAllValue/>
                                                         </conceptValue>
                                                     </conceptFact>
                                                 </conceptFactSet>
                                             </keyFinding>
                                         </keyFindings>
                                         <aspectReferences/>
                                     </solution>

    ReadOnly _recorded2 As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                          <keyFindings>
                                              <keyFinding xsi:type="ConceptFinding" id="Id needs to be set" scoringMethod="None">
                                                  <conceptFactSet>
                                                      <conceptFact score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                          <conceptValue occur="0">
                                                              <catchAllValue/>
                                                          </conceptValue>
                                                      </conceptFact>
                                                      <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                                                          <concept value="1" code="someCode"/>
                                                          <concept value="42" code="someOtherCode"/>
                                                      </concepts>
                                                  </conceptFactSet>
                                              </keyFinding>
                                          </keyFindings>
                                          <aspectReferences/>
                                      </solution>

End Class
