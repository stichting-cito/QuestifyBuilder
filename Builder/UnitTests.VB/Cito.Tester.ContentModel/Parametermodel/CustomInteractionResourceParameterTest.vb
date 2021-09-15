
Imports System.Xml.Linq
Imports Cito.Tester.ContentModel

<TestClass()>
Public Class CustomInteractionResourceParameterTest
    Inherits SerializationTestBase

    <TestMethod()> <TestCategory("ContentModel")> 
    Public Sub TestSerializedResult()
        'Arrange
        Dim p As New CustomInteractionResourceParameter() With {.Name = "ci1", .Value = "someCi.ci", .Height = 20, .Scalable = True}
        Dim result As XElement = Nothing
       
        'Act
        result = DoSerialize(p)
       
        'Assert

        '[Expected Result:]
        '<CustomInteractionResourceParameter 
        'xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" 
        'xmlns:xsd="http://www.w3.org/2001/XMLSchema" 
        'name="ci1" height="20" scalable="true" 
        'interactioncount="0">someCi.ci
        '</CustomInteractionResourceParameter>

        Assert.IsNotNull(result)
        Assert.AreEqual("ci1", result.Attribute("name").Value)
        Assert.AreEqual("someCi.ci", result.Value)
        Assert.AreEqual("20", result.Attribute("height").Value)
        Assert.IsNull(result.Attribute("width"))
        Assert.IsTrue(Boolean.Parse(result.Attribute("scalable").Value))
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub SeralisationInParameterCollectionTest()
        'Arrange
        Dim params As New ParameterCollection
        Dim ciResourceParam As New CustomInteractionResourceParameter() With {.Name = "ci1", .Value = "someCi.ci", .Height = 20, .Width = 42, .Scalable = False, .Scorable = False, .CommunicationType = CustomInteractionResourceParameter.CustomInteractionCommunicationType.State}

        params.InnerParameters.Add(ciResourceParam)
        
        'Act
        Dim result = DoSerialize(params)
       
        'Assert
        Assert.AreEqual(recorded.ToString(), result.ToString())
    End Sub


    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub SeralisationInParameterCollectionTest_OmmitWidthAndHeight_ShouldNotBeSerialized()
        'Arrange
        Dim params As New ParameterCollection
        Dim ciResourceParam As New CustomInteractionResourceParameter() With {.Name = "ci1", .Value = "someCi.ci", .Scalable = True, .Scorable = True, .CommunicationType = CustomInteractionResourceParameter.CustomInteractionCommunicationType.Answer, .InteractionCount = 8}

        params.InnerParameters.Add(ciResourceParam)
      
        'Act
        Dim result = DoSerialize(params)
       
        'Assert
        Assert.AreEqual(recorded2.ToString(), result.ToString())
    End Sub

    ReadOnly recorded As XElement = <parameterSet xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" id="">
                                        <custominteractionresourceparameter name="ci1" height="20" width="42" scalable="false" interactioncount="0" inlineUsage="false" communicationType="State" scorable="false">someCi.ci</custominteractionresourceparameter>
                                    </parameterSet>

    ReadOnly recorded2 As XElement = <parameterSet xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" id="">
                                         <custominteractionresourceparameter name="ci1" scalable="true" interactioncount="8" inlineUsage="false" communicationType="Answer" scorable="true">someCi.ci</custominteractionresourceparameter>
                                     </parameterSet>

End Class
