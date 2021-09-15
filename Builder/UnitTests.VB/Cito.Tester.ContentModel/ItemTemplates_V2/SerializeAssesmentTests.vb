
Imports Cito.Tester.ContentModel
Imports Cito.Tester.Common

<TestClass()>
Public Class SerializeAssesmentTests

    <TestMethod()> <TestCategory("ContentModel")> <WorkItem(9658)> <Description("Designersettings in serialized assessmeItem")>
    Public Sub SerializedAssesmentShouldNotContainDesignerSettings()
        'Arrange
        Dim r As New ResourceParameter() : r.DesignerSettings.Add(New DesignerSetting() With {.Key = "a"})

        Dim a As New AssessmentItem
        a.Parameters.Add(New ParameterCollection())
        a.Parameters(0).InnerParameters.Add(r)

        'Act
        Dim res As String = SerializeHelper.XmlSerializeToString(a)
       
        'Assert
        Assert.IsFalse(res.Contains("<designersetting"), "Should not contain DesignerSettings!!")
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub SerializedAssesmentShouldNotContainAttributeReferences()
        'Arrange
        Dim r As New ResourceParameter() : r.AttributeReferences.Add(New AttributeReference() With {.Name = "a", .Value = "b"})

        Dim a As New AssessmentItem
        a.Parameters.Add(New ParameterCollection())
        a.Parameters(0).InnerParameters.Add(r)

        'Act
        Dim res As String = SerializeHelper.XmlSerializeToString(a)
        
        'Assert
        Assert.IsFalse(res.Contains("<attributereference"), "Should not contain DesignerSettings!!")
    End Sub

End Class
