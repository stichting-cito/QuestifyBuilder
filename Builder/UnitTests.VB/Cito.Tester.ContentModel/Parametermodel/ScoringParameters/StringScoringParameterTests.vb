
Imports Cito.Tester.ContentModel

<TestClass()>
Public Class StringScoringParameterTests
    Inherits SerializationTestBase

    <TestMethod()> <TestCategory("ContentModel"), TestCategory("ScoringParameter")>
    Public Sub CompareWithPreviouslyKnowState()
        'Arrange
        Dim scorePrm = New StringScoringParameter() With {.PatternMask = "\be(\w*)s\b", .ExpectedLength = 3, .ControllerId = "StrPrm"}
        
        'Act
        Dim result = DoSerialize(Of StringScoringParameter)(scorePrm)
      
        'Assert
        'Compare with previously known result 
        Assert.AreEqual(<StringScoringParameter
                            xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
                            xmlns:xsd="http://www.w3.org/2001/XMLSchema"
                            ControllerId="StrPrm"
                            expectedLength="3"
                            patternMask="\be(\w*)s\b"/>.ToString(), result.ToString())
    End Sub

    <TestMethod()> <TestCategory("ContentModel"), TestCategory("ScoringParameter")>
    Public Sub PreprocessingRuleShouldNotBeSerialized()
        'Arrange
        Dim scorePrm = New StringScoringParameter() With {.PatternMask = "\be(\w*)s\b", .ExpectedLength = 3, .ControllerId = "StrPrm", .PreprocessRules = "some string that will not be serialized"}
     
        'Act
        Dim result = DoSerialize(Of StringScoringParameter)(scorePrm)
     
        'Assert
        'Compare with previously known result 
        Assert.AreEqual(<StringScoringParameter
                            xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
                            xmlns:xsd="http://www.w3.org/2001/XMLSchema"
                            ControllerId="StrPrm"
                            expectedLength="3"
                            patternMask="\be(\w*)s\b"/>.ToString(), result.ToString())
    End Sub


    <TestMethod()> <TestCategory("ContentModel"), TestCategory("ScoringParameter")>
    Public Sub SerializeStringScore_inAssessmentItem_CompareWithPreviouslyKnownResult_Test()
        'Arrange
        Dim a = New AssessmentItem With
                {.Title = "someTitle", .Identifier = "someIdentifier", .LayoutTemplateSourceName = "someIlt"}
        Dim p = a.Parameters.AddNew()
        p.Id = "id_1"
        p.InnerParameters.Add(New StringScoringParameter() With {.PatternMask = "\be(\w*)s\b", .ExpectedLength = 3, .ControllerId = "StrPrm"})
       
        'Act
        Dim result = DoSerialize(Of AssessmentItem)(a)
   
        'Assert
        Assert.AreEqual(<assessmentItem xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" identifier="someIdentifier" title="someTitle" layoutTemplateSrc="someIlt">
                            <solution>
                                <keyFindings/>
                                <aspectReferences/>
                            </solution>
                            <parameters>
                                <parameterSet id="id_1">
                                    <stringScoringParameter ControllerId="StrPrm" expectedLength="3" patternMask="\be(\w*)s\b"/>
                                </parameterSet>
                            </parameters>
                        </assessmentItem>.ToString(), result.ToString())
    End Sub

End Class
