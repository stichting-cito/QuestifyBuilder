
Imports Cito.Tester.ContentModel

<TestClass()>
Public Class MatrixScoringParameterSerialisationTest
    Inherits SerializationTestBase

    <TestMethod()> <TestCategory("ContentModel"), TestCategory("ScoringParameter")>
    Public Sub CompareWithPreviouslyKnowState()
        'Arrange
        Dim matrixPrm = New MatrixScoringParameter() With {.MatrixColumnsDefinition = New MultiChoiceScoringParameter() With {.MaxChoices = 1, .MinChoices = 1, .MultiChoice = MultiChoiceType.Check}, .ControllerId = "mtx_1"}
   
        'Act
        Dim result = DoSerialize(Of MatrixScoringParameter)(matrixPrm)
    
        'Assert
        'Compare with previously known result 
        Assert.AreEqual(<MatrixScoringParameter
                            xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
                            xmlns:xsd="http://www.w3.org/2001/XMLSchema"
                            ControllerId="mtx_1">
                            <matrixcolumnsdefinition minChoices="1" maxChoices="1" multiChoice="Check"/>
                        </MatrixScoringParameter>.ToString(), result.ToString())
    End Sub

    <TestMethod()> <TestCategory("ContentModel"), TestCategory("ScoringParameter")>
    Public Sub Deserialize_Test()
        'Arrange
        Dim xmlData = <MatrixScoringParameter
                          xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
                          xmlns:xsd="http://www.w3.org/2001/XMLSchema"
                          ControllerId="mtx_1">
                          <subparameterset id="A">
                              <xhtmlparameter name="statement"><p>De stelling</p></xhtmlparameter>
                          </subparameterset>
                          <matrixcolumnsdefinition minChoices="1" maxChoices="1" multiChoice="Radio">
                              <subparameterset id="1">
                                  <xhtmlparameter name="domainLabel">Kolom 1</xhtmlparameter>
                                  <integerParameter name="columnWidth">123</integerParameter>
                              </subparameterset>
                          </matrixcolumnsdefinition>
                          <linelabelcolumnwidth name="columnwidth">300</linelabelcolumnwidth>
                      </MatrixScoringParameter>
       
        'Act
        Dim result = Deserialize(Of MatrixScoringParameter)(xmlData)
      
        'Assert
        Assert.AreEqual("mtx_1", result.ControllerId)
        Assert.AreEqual(1, result.MatrixColumnsDefinition.MinChoices)
        Assert.AreEqual(1, result.MatrixColumnsDefinition.MaxChoices)
        Assert.AreEqual(MultiChoiceType.Radio, result.MatrixColumnsDefinition.MultiChoice)
        Assert.AreEqual(1, result.MatrixColumnsDefinition.Value.Count)
    End Sub


    <TestMethod()> <TestCategory("ContentModel"), TestCategory("ScoringParameter")>
    Public Sub SerializeMultiChoice_inAssessmentItem_CompareWithPreviouslyKnownResult_Test()
        'Arrange
        Dim a = New AssessmentItem With
                {.Title = "someTitle", .Identifier = "someIdentifier", .LayoutTemplateSourceName = "someIlt"}
        Dim p = a.Parameters.AddNew()
        p.Id = "id_1"
        p.InnerParameters.Add(New MatrixScoringParameter() With {.MatrixColumnsDefinition = New MultiChoiceScoringParameter() With {.MaxChoices = 1, .MinChoices = 1, .MultiChoice = MultiChoiceType.Check}, .ControllerId = "mtx_1"})
       
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
                                    <matrixScoringParameter ControllerId="mtx_1">
                                        <matrixcolumnsdefinition minChoices="1" maxChoices="1" multiChoice="Check"/>
                                    </matrixScoringParameter>
                                </parameterSet>
                            </parameters>
                        </assessmentItem>.ToString(), result.ToString())
    End Sub

    <TestMethod()> <TestCategory("ContentModel"), TestCategory("ScoringParameter")>
    Public Sub Deserialize_InAssessmentItem_Test()
        'Arrange
        Dim xmlData = <assessmentItem xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" identifier="someIdentifier" title="someTitle" layoutTemplateSrc="someIlt">
                          <solution>
                              <keyFindings/>
                              <aspectReferences/>
                          </solution>
                          <parameters>
                              <parameterSet id="id_1">
                                  <matrixScoringParameter ControllerId="mtx_1">
                                      <matrixcolumnsdefinition minChoices="1" maxChoices="1" multiChoice="Check"/>
                                  </matrixScoringParameter>
                              </parameterSet>
                          </parameters>
                      </assessmentItem>
        
        'Act
        Dim result = Deserialize(Of AssessmentItem)(xmlData)
        
        'Assert
        Assert.IsInstanceOfType(result.Parameters(0).InnerParameters(0), GetType(MatrixScoringParameter))
    End Sub

End Class
