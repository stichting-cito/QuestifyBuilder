
Imports System.Xml.Linq
Imports Cito.Tester.ContentModel

<TestClass>
Public Class ParameterSetCollectionTest
    Inherits SerializationTestBase

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub SaveAndAssessmentItem_CompareToRecorded()
        Dim assessment As New AssessmentItem

        Dim result = DoSerialize(assessment)

        Assert.AreEqual(data1.ToString(), result.ToString())
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub AssessmentWith2ParameterCollection_CompareToRecorded()
        Dim assessment As New AssessmentItem
        assessment.Parameters.Add(New ParameterCollection())
        assessment.Parameters.Add(New ParameterCollection())

        Dim result = DoSerialize(assessment)

        Assert.AreEqual(data2.ToString(), result.ToString())
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub AssessmentWith2ParameterCollection1Dynamic_CompareToRecorded()
        Dim assessment As New AssessmentItem
        assessment.Parameters.Add(New ParameterCollection())
        assessment.Parameters.Add(New ParameterCollection() With {.IsDynamicCollection = True})

        Dim result = DoSerialize(assessment)

        Assert.AreEqual(data3.ToString(), result.ToString())
    End Sub


    ReadOnly data1 As XElement = <assessmentItem xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" identifier="" title="" layoutTemplateSrc="">
                                     <solution>
                                         <keyFindings/>
                                         <aspectReferences/>
                                     </solution>
                                     <parameters/>
                                 </assessmentItem>

    ReadOnly data2 As XElement = <assessmentItem xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" identifier="" title="" layoutTemplateSrc="">
                                     <solution>
                                         <keyFindings/>
                                         <aspectReferences/>
                                     </solution>
                                     <parameters>
                                         <parameterSet id=""/>
                                         <parameterSet id=""/>
                                     </parameters>
                                 </assessmentItem>

    ReadOnly data3 As XElement = <assessmentItem xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" identifier="" title="" layoutTemplateSrc="">
                                     <solution>
                                         <keyFindings/>
                                         <aspectReferences/>
                                     </solution>
                                     <parameters>
                                         <parameterSet id=""/>
                                         <parameterSet id="" isDynamicCollection="true"/>
                                     </parameters>
                                 </assessmentItem>


End Class