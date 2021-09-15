﻿
Imports Cito.Tester.ContentModel

<TestClass>
Public Class TimeScoringSerialisationParameterTest
    Inherits SerializationTestBase

    <TestMethod()> <TestCategory("ContentModel"), TestCategory("ScoringParameter")>
    Public Sub CompareWithPreviouslyKnowState()
        'Arrange
        Dim scorePrm = New TimeScoringParameter() With {.ControllerId = "TimePrm", .TimeFormat = "hh:mm"}
       
        'Act
        Dim result = DoSerialize(Of TimeScoringParameter)(scorePrm)
     
        'Assert
        'Compare with previously known result 
        Assert.AreEqual(<TimeScoringParameter
                            xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
                            xmlns:xsd="http://www.w3.org/2001/XMLSchema"
                            ControllerId="TimePrm"
                            timeFormat="hh:mm"
                        />.ToString(), result.ToString())
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
                                  <timeScoringParameter timeFormat="hh:mm"/>
                              </parameterSet>
                          </parameters>
                      </assessmentItem>
     
        'Act
        Dim result = Deserialize(Of AssessmentItem)(xmlData)
       
        'Assert
        Assert.IsInstanceOfType(result.Parameters(0).InnerParameters(0), GetType(TimeScoringParameter))
        Assert.AreEqual("hh:mm", DirectCast(result.Parameters(0).InnerParameters(0), TimeScoringParameter).TimeFormat)
    End Sub

End Class
