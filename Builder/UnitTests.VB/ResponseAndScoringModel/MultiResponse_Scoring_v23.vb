
Imports System.Xml.Linq
Imports Cito.Tester.ContentModel.ResponseAndScoringModel.Solution.ConcreteScoring
Imports FluentAssertions

<TestClass()>
<ScoringMethod(ScoringFactory.Methods.V23_Scoring)>
Public Class MultiResponse_Scoring_v23
    Inherits ScoringTestBase

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub MaxScore_MR_Poly()
        'Arrange
        Dim solution = toSolution(MultiResponse_Scoring.MrFindingPolytomous)
      
        'Act
        Dim result = solution.MaxSolutionRawScore
        
        'Assert
        Assert.AreEqual(2, result)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub MaxScore_MR_Dich()
        'Arrange
        Dim solution = toSolution(MultiResponse_Scoring.MrFindingAllCorrectDichotomous)
       
        'Act
        Dim result = solution.MaxSolutionRawScore
       
        'Assert
        Assert.AreEqual(1, result)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub GiveCorrectAnswer_Polytomous()
        GetScoreSolution(MultiResponse_Scoring.MrFindingPolytomous, _responseMr).Should().Be(2)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub GiveIncompleteAnswer_Polytomous()
        GetScoreSolution(MultiResponse_Scoring.MrFindingPolytomous, _responseMrIncorrect).Should().Be(1)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub GiveIncompleteAnswer_Dichotomous()
        GetScoreSolution(MultiResponse_Scoring.MrFindingAllCorrectDichotomous, _responseMrIncorrect).Should().Be(0)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub GiveCorrectAnswer_Dichotomous()
        GetScoreSolution(MultiResponse_Scoring.MrFindingDichotomous, _responseMr).Should().Be(1)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub CheckMoreOptionsThanCorrectAnswers_Dichotomous_ShouldBe0()
        GetScoreSolution(MultiResponse_Scoring.MrFindingDichotomous, MultiResponse_Scoring.ResponseMrAllOptionsChecked).Should().Be(0)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub CheckAllOptionsAllOptionsAreCorrect_Dichotomous_ShouldBe1()
        GetScoreSolution(MultiResponse_Scoring.MrFindingAllCorrectDichotomous, MultiResponse_Scoring.ResponseMrAllOptionsChecked).Should().Be(1)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub CheckMoreOptionsThanCorrectAnswers_Polytomous_ShouldBe2()
        GetScoreSolution(MultiResponse_Scoring.MrFindingPolytomous, MultiResponse_Scoring.ResponseMrAllOptionsChecked).Should().Be(2)
    End Sub

    <TestMethod()> <TestCategory("ResponseAndScoringModel")>
    Public Sub CheckAllOptionsAllOptionsAreCorrect_Polytomous_ShouldBe4()
        GetScoreSolution(MultiResponse_Scoring.MrFindingAllCorrectPolytomous, MultiResponse_Scoring.ResponseMrAllOptionsChecked).Should().Be(4)
    End Sub

    Private ReadOnly _responseMr As XElement = <Response xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" active="false" responseNr="-9223372036854775808" translatedScore="0" rawScore="0" navigatedToIndex="-2147483648">
                                         <ResponseProperties xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                         <responseFindings xmlns="http://Cito.Tester.Server/xml/serialization">
                                             <responseFinding id="mc">
                                                 <responseFact id="mc">
                                                     <responseValue domain="mc" occur="1">
                                                         <stringValue>
                                                             <typedValue>B</typedValue>
                                                         </stringValue>
                                                     </responseValue>
                                                 </responseFact>
                                                 <responseFact id="mc">
                                                     <responseValue domain="mc" occur="1">
                                                         <stringValue>
                                                             <typedValue>C</typedValue>
                                                         </stringValue>
                                                     </responseValue>
                                                 </responseFact>
                                             </responseFinding>
                                             <responseFinding id="audioController1"/>
                                         </responseFindings>
                                         <ItemIndexInTest xmlns="http://Cito.Tester.Server/xml/serialization">0</ItemIndexInTest>
                                     </Response>
    
    Private ReadOnly _responseMrIncorrect As XElement = <Response xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" active="false" responseNr="-9223372036854775808" translatedScore="0" rawScore="0" navigatedToIndex="-2147483648">
                                                   <ResponseProperties xmlns="http://Cito.Tester.Server/xml/serialization"/>
                                                   <responseFindings xmlns="http://Cito.Tester.Server/xml/serialization">
                                                       <responseFinding id="mc">
                                                           <responseFact id="mc">
                                                               <responseValue domain="mc" occur="1">
                                                                   <stringValue>
                                                                       <typedValue>B</typedValue>
                                                                   </stringValue>
                                                               </responseValue>
                                                           </responseFact>
                                                           <responseFact id="mc">
                                                               <responseValue domain="mc" occur="1">
                                                                   <stringValue>
                                                                       <typedValue>D</typedValue>
                                                                   </stringValue>
                                                               </responseValue>
                                                           </responseFact>
                                                       </responseFinding>
                                                       <responseFinding id="audioController1"/>
                                                   </responseFindings>
                                                   <ItemIndexInTest xmlns="http://Cito.Tester.Server/xml/serialization">0</ItemIndexInTest>
                                               </Response>

End Class
