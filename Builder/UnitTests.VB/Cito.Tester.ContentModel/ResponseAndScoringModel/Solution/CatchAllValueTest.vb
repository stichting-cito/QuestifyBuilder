
Imports System.Xml.Linq
Imports Cito.Tester.ContentModel
Imports XmlTools = Questify.Builder.UnitTests.Framework.Util.XmlTools

<TestClass>
Public Class CatchAllValueTest : Inherits SerializationTestBase

    <TestMethod()> <TestCategory("ContentModel"), TestCategory("Solution")>
    Public Sub SerializeTestAndCompare()
        Dim solution = New Solution
        Dim conceptFinding = New ConceptFinding With {.Id = "Id needs to be set"}
        Dim conceptFact = New ConceptFact
        Dim conceptValue = New ConceptValue

        conceptValue.Values.Add(New CatchAllValue())
        conceptFact.Values.Add(conceptValue)
        conceptFinding.Facts.Add(conceptFact)
        solution.Findings.Add(conceptFinding)

        Dim result = DoSerialize(Of Solution)(solution)

        Assert.IsTrue(XmlTools.DeepEqualsWithNormalization(New XDocument(_recorderd), New XDocument(result), Nothing))
    End Sub

    ReadOnly _recorderd As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                          <keyFindings>
                                              <keyFinding xsi:type="ConceptFinding" id="Id needs to be set" scoringMethod="None">
                                                  <conceptFact score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                      <conceptValue occur="0">
                                                          <catchAllValue/>
                                                      </conceptValue>
                                                  </conceptFact>
                                              </keyFinding>
                                          </keyFindings>
                                          <aspectReferences/>
                                      </solution>

End Class
