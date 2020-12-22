
Imports System.Xml.Linq
Imports Cito.Tester.ContentModel
Imports XmlTools = Questify.Builder.UnitTests.Framework.Util.XmlTools

<TestClass>
Public Class ConceptAtConceptFinding
    Inherits SerializationTestBase

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub ConceptScoringIsSetToConceptFact()
        Dim cFinding = New ConceptFinding("SomeId")
        cFinding.Method = EnumScoringMethod.Polytomous
        cFinding.Facts.Add(New ConceptFact("SomeFactId"))
        Dim firstFact = DirectCast(cFinding.Facts(0), ConceptFact)

        firstFact.Concepts = New ConceptCollection()
        firstFact.Concepts.Add(New Concept("Concept x", 2))
        firstFact.Concepts.Add("Concept y", -5)

        Dim result = MyBase.DoSerialize(Of ConceptFinding)(cFinding)

        Assert.IsTrue(XmlTools.DeepEqualsWithNormalization(New XDocument(example1), new XDocument(result), Nothing))
    End Sub

    Private example1 As XElement = <conceptFinding xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" id="SomeId" scoringMethod="Polytomous">
                                       <conceptFact id="SomeFactId" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                           <concepts>
                                               <concept value="2" code="Concept x"/>
                                               <concept value="-5" code="Concept y"/>
                                           </concepts>
                                       </conceptFact>
                                   </conceptFinding>

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub ConceptIsSerialized()
        Dim sol = New Solution
        Dim conceptFinding = New ConceptFinding("ConceptId")
        Dim conceptFact = New ConceptFact("SomeFactId")

        conceptFact.Concepts = New ConceptCollection()
        conceptFact.Concepts.Add("code1", 42)
        conceptFact.Concepts.Add("code2", 123)

        conceptFinding.Facts.Add(conceptFact)
        sol.ConceptFindings.Add(conceptFinding)

        Dim result = MyBase.DoSerialize(Of Solution)(sol)

        Assert.IsTrue(XmlTools.DeepEqualsWithNormalization(New XDocument(recordedExample), New XDocument(result), Nothing))
    End Sub

    Private recordedExample As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                              <keyFindings/>
                                              <conceptFindings>
                                                  <conceptFinding id="ConceptId" scoringMethod="None">
                                                      <conceptFact id="SomeFactId" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                          <concepts>
                                                              <concept value="42" code="code1"/>
                                                              <concept value="123" code="code2"/>
                                                          </concepts>
                                                      </conceptFact>
                                                  </conceptFinding>
                                              </conceptFindings>
                                              <aspectReferences/>
                                          </solution>

End Class
