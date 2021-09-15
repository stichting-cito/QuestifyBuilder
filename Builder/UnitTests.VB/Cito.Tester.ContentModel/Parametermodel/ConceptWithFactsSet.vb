
Imports System.Xml.Linq
Imports Cito.Tester.ContentModel
Imports XmlTools = Questify.Builder.UnitTests.Framework.Util.XmlTools

<TestClass>
Public Class ConceptWithFactsSet
    Inherits SerializationTestBase

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub ConceptWithSingleFactsetIsSerialized()
        'Arrange
        Dim sol = New Solution
        Dim conceptFinding = New ConceptFinding("ConceptId")
        Dim conceptFactSet = New ConceptFactsSet()

        Dim conceptFact1 = New ConceptFact("SomeFactId")
        conceptFact1.Values.Add(New ConceptValue("someDom", 1, New IntegerValue(42)))

        Dim conceptFact2 = New ConceptFact("SomeFactId2")
        conceptFact2.Values.Add(New ConceptValue("someDom", 1, New IntegerValue(65535)))

        conceptFactSet.Concepts = New ConceptCollection()
        conceptFactSet.Concepts.Add(New Concept("Some part", 1))
        conceptFactSet.Concepts.Add(New Concept("Some other part", 2))

        conceptFactSet.Facts.Add(conceptFact1)
        conceptFactSet.Facts.Add(conceptFact2)

        conceptFinding.KeyFactsets.Add(conceptFactSet)

        sol.ConceptFindings.Add(conceptFinding)

        'Act
        Dim result = MyBase.DoSerialize(Of Solution)(sol)
        
        'Assert
        Assert.IsTrue(XmlTools.DeepEqualsWithNormalization(New XDocument(recordedExample), New XDocument(result), Nothing))
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub ConceptWithTwoFactSetIsSerialized()
        'Arrange
        Dim sol = New Solution
        Dim conceptFinding = New ConceptFinding("ConceptId")

        'FactSet1
        Dim conceptFactSet1 = New ConceptFactsSet()

        Dim conceptFact1 = New ConceptFact("SomeFactId")
        conceptFact1.Values.Add(New ConceptValue("someDom", 1, New IntegerValue(42)))

        Dim conceptFact2 = New ConceptFact("SomeFactId2")
        conceptFact2.Values.Add(New ConceptValue("someDom", 1, New IntegerValue(65535)))

        conceptFactSet1.Concepts = New ConceptCollection()
        conceptFactSet1.Concepts.Add(New Concept("Some part", 1))
        conceptFactSet1.Concepts.Add(New Concept("Some other part", 2))

        conceptFactSet1.Facts.Add(conceptFact1)
        conceptFactSet1.Facts.Add(conceptFact2)
       
        'FactSet2
        Dim conceptFactSet2 = New ConceptFactsSet()

        conceptFact1 = New ConceptFact("SomeFactId")
        conceptFact2.Values.Add(New ConceptValue("someDom", 1, New IntegerValue(65535)))

        conceptFact2 = New ConceptFact("SomeFactId2")
        conceptFact2.Values.Add(New ConceptValue("someDom", 1, New IntegerValue(42)))

        conceptFactSet2.Concepts = New ConceptCollection()
        conceptFactSet2.Concepts.Add(New Concept("Some part", 1))
        conceptFactSet2.Concepts.Add(New Concept("Some other part", 2))

        conceptFactSet2.Facts.Add(conceptFact1)
        conceptFactSet2.Facts.Add(conceptFact2)

        'Adding
        conceptFinding.KeyFactsets.Add(conceptFactSet1)
        conceptFinding.KeyFactsets.Add(conceptFactSet2)

        sol.ConceptFindings.Add(conceptFinding)

        'Act
        Dim result = MyBase.DoSerialize(Of Solution)(sol)
        
        'Assert
        Assert.IsTrue(XmlTools.DeepEqualsWithNormalization(New XDocument(recordedExample2), New XDocument(result), Nothing))
    End Sub

    Private recordedExample As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                              <keyFindings/>
                                              <conceptFindings>
                                                  <conceptFinding id="ConceptId" scoringMethod="None">
                                                      <conceptFactSet>
                                                          <conceptFact id="SomeFactId" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                              <conceptValue domain="someDom" occur="1">
                                                                  <integerValue>
                                                                      <typedValue>42</typedValue>
                                                                  </integerValue>
                                                              </conceptValue>
                                                          </conceptFact>
                                                          <conceptFact id="SomeFactId2" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                              <conceptValue domain="someDom" occur="1">
                                                                  <integerValue>
                                                                      <typedValue>65535</typedValue>
                                                                  </integerValue>
                                                              </conceptValue>
                                                          </conceptFact>
                                                          <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                                                              <concept value="1" code="Some part"/>
                                                              <concept value="2" code="Some other part"/>
                                                          </concepts>
                                                      </conceptFactSet>
                                                  </conceptFinding>
                                              </conceptFindings>
                                              <aspectReferences/>
                                          </solution>

    Private recordedExample2 As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                               <keyFindings/>
                                               <conceptFindings>
                                                   <conceptFinding id="ConceptId" scoringMethod="None">
                                                       <conceptFactSet>
                                                           <conceptFact id="SomeFactId" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                               <conceptValue domain="someDom" occur="1">
                                                                   <integerValue>
                                                                       <typedValue>42</typedValue>
                                                                   </integerValue>
                                                               </conceptValue>
                                                           </conceptFact>
                                                           <conceptFact id="SomeFactId2" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                               <conceptValue domain="someDom" occur="1">
                                                                   <integerValue>
                                                                       <typedValue>65535</typedValue>
                                                                   </integerValue>
                                                               </conceptValue>
                                                               <conceptValue domain="someDom" occur="1">
                                                                   <integerValue>
                                                                       <typedValue>65535</typedValue>
                                                                   </integerValue>
                                                               </conceptValue>
                                                           </conceptFact>
                                                           <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                                                               <concept value="1" code="Some part"/>
                                                               <concept value="2" code="Some other part"/>
                                                           </concepts>
                                                       </conceptFactSet>
                                                       <conceptFactSet>
                                                           <conceptFact id="SomeFactId" score="1" xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                                           <conceptFact id="SomeFactId2" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                               <conceptValue domain="someDom" occur="1">
                                                                   <integerValue>
                                                                       <typedValue>42</typedValue>
                                                                   </integerValue>
                                                               </conceptValue>
                                                           </conceptFact>
                                                           <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                                                               <concept value="1" code="Some part"/>
                                                               <concept value="2" code="Some other part"/>
                                                           </concepts>
                                                       </conceptFactSet>
                                                   </conceptFinding>
                                               </conceptFindings>
                                               <aspectReferences/>
                                           </solution>

End Class
