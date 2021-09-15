
Imports Cito.Tester.ContentModel
Imports System.Xml.Linq
Imports Questify.Builder.Logic.ContentModel.Scoring
Imports Questify.Builder.UnitTests.Framework

<TestClass>
Public Class StaticExample_FindingToFindingManipulatorTests

    <TestMethod(), TestCategory("Logic"), TestCategory("ScoringAdv"), TestCategory("Scoring")>
    Public Sub SyncPreprocessingRule()
        'Arrange
        Dim soulution = solutionWithPreprocessingRule.To(Of Solution)()
        Dim conceptFinding = New ConceptFinding("mirror")
        soulution.ConceptFindings.Add(conceptFinding)
        Dim finding2Finding = New FindingToFindingManipulator(New KeyManipulator(soulution.GetFindingOrMakeIt("ctrl")), New ConceptManipulator(conceptFinding))
        
        'Act
        finding2Finding.Execute()
        
        'Assert            
        Assert.IsTrue(UnitTestHelper.AreSame(compare_solutionWithPreprocessingRule.ToString(), soulution.DoSerialize().ToString()))
    End Sub


    ReadOnly solutionWithPreprocessingRule As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                                             <keyFindings>
                                                                 <keyFinding id="ctrl" scoringMethod="None">
                                                                     <keyFact id="A-ctrl" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                         <keyValue domain="ctrl" occur="1">
                                                                             <stringValue>
                                                                                 <typedValue>SomeValue</typedValue>
                                                                             </stringValue>
                                                                             <preprocessingRule>
                                                                                 <ruleName>Full.Qualifying.Namespace.Class</ruleName>
                                                                             </preprocessingRule>
                                                                         </keyValue>
                                                                     </keyFact>
                                                                 </keyFinding>
                                                             </keyFindings>
                                                             <aspectReferences/>
                                                         </solution>

    ReadOnly compare_solutionWithPreprocessingRule As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                                                     <keyFindings>
                                                                         <keyFinding id="ctrl" scoringMethod="None">
                                                                             <keyFact id="A-ctrl" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                 <keyValue domain="ctrl" occur="1">
                                                                                     <stringValue>
                                                                                         <typedValue>SomeValue</typedValue>
                                                                                     </stringValue>
                                                                                     <preprocessingRule>
                                                                                         <ruleName>Full.Qualifying.Namespace.Class</ruleName>
                                                                                     </preprocessingRule>
                                                                                 </keyValue>
                                                                             </keyFact>
                                                                         </keyFinding>
                                                                     </keyFindings>
                                                                     <conceptFindings>
                                                                         <conceptFinding id="mirror" scoringMethod="None">
                                                                             <conceptFact id="A-ctrl" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                 <conceptValue domain="ctrl" occur="1">
                                                                                     <stringValue>
                                                                                         <typedValue>SomeValue</typedValue>
                                                                                     </stringValue>
                                                                                     <preprocessingRule>
                                                                                         <ruleName>Full.Qualifying.Namespace.Class</ruleName>
                                                                                     </preprocessingRule>
                                                                                 </conceptValue>
                                                                             </conceptFact>
                                                                         </conceptFinding>
                                                                     </conceptFindings>
                                                                     <aspectReferences/>
                                                                 </solution>

End Class
