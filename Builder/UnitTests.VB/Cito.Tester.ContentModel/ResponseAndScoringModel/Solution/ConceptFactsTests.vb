
Imports System.Xml.Linq
Imports Cito.Tester.ContentModel
Imports XmlTools = Questify.Builder.UnitTests.Framework.Util.XmlTools

<TestClass>
Public Class ConceptFactsTests : Inherits SerializationTestBase

    <TestMethod()> <TestCategory("ContentModel"), TestCategory("Solution")>
    <Description("Concepts should not serialize, since not used")>
    Public Sub SerializeTestAndCompare()
        'Arrange
        Dim solution = New Solution
        Dim conceptFinding = New ConceptFinding With {.Id = "Id needs to be set"}
        Dim conceptFact = New ConceptFact
        Dim conceptValue = New ConceptValue

        conceptValue.Values.Add(New CatchAllValue())
        conceptFact.Values.Add(conceptValue)
        conceptFinding.Facts.Add(conceptFact)

        solution.Findings.Add(conceptFinding)
      
        'Act
        Dim result = DoSerialize(Of Solution)(solution)
       
        'Assert
        Assert.IsTrue(XmlTools.DeepEqualsWithNormalization(New XDocument(_recorded), New XDocument(result), Nothing))
    End Sub

    <TestMethod()> <TestCategory("ContentModel"), TestCategory("Solution")>
    <Description("Concepts should serialize")>
    Public Sub SerializeTestAndCompareWithConceptValue()
        'Arrange
        Dim solution = New Solution
        Dim conceptFinding = New ConceptFinding With {.Id = "Id needs to be set"}

        Dim conceptFact = New ConceptFact
        Dim conceptValue = New ConceptValue

        conceptValue.Values.Add(New CatchAllValue())
        conceptFact.Values.Add(conceptValue)
        conceptFinding.Facts.Add(conceptFact)

        solution.Findings.Add(conceptFinding)

        conceptFact.Concepts.Add("someCode", 1)
        conceptFact.Concepts.Add("someOtherCode", 42)

        'Act
        Dim result = DoSerialize(Of Solution)(solution)
       
        'Assert
        Assert.IsTrue(XmlTools.DeepEqualsWithNormalization(New XDocument(_recorded2), New XDocument(result), Nothing))
    End Sub

    ReadOnly _recorded As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
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

    ReadOnly _recorded2 As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                          <keyFindings>
                                              <keyFinding xsi:type="ConceptFinding" id="Id needs to be set" scoringMethod="None">
                                                  <conceptFact score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                      <conceptValue occur="0">
                                                          <catchAllValue/>
                                                      </conceptValue>
                                                      <concepts>
                                                          <concept value="1" code="someCode"/>
                                                          <concept value="42" code="someOtherCode"/>
                                                      </concepts>
                                                  </conceptFact>
                                              </keyFinding>
                                          </keyFindings>
                                          <aspectReferences/>
                                      </solution>

End Class
