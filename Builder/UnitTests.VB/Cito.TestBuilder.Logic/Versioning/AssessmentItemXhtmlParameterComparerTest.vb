
Imports Questify.Builder.Model.ContentModel.Interfaces
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic
Imports System.Xml
Imports Questify.Builder.Logic.Service.Direct
Imports Versioning

<TestClass()>
Public Class AssessmentItemXhtmlParameterComparerTest
    Inherits ComparerTestBase

    Private _iltName1 As String = "ilt.compare.xhtml1"
    
    <TestMethod(), Description("Compare two AssessmentItem objects of type xhtml parameter. The number of parameters are the same and also their values."), TestCategory("Logic")>
    Public Sub TestTwoAssessmentItemsWithIdenticaXhtmlParametersAndSameValues()
        Dim assessmentItem1 As AssessmentItem = Helper.CreateAssessmentItem("Title of AssessmentItem 1", _iltName1)
        Dim assessmentItem2 As AssessmentItem = Helper.CreateAssessmentItem("Title of AssessmentItem 2", _iltName1)
        Dim paramCollection1 As ParameterCollection = assessmentItem1.Parameters.AddNew()
        Dim paramCollection2 As ParameterCollection = assessmentItem2.Parameters.AddNew()
        Dim xhtmlParam1 As New XHtmlParameter()
        Dim xhtmlParam2 As New XHtmlParameter()
        xhtmlParam1.Name = "xhtmlParameter1"    'Same parameter names!
        xhtmlParam2.Name = "xhtmlParameter1"    'Same parameter names!
        xhtmlParam1.Value = CreateXhtmlDoc("attributeValue") 'Same values!
        xhtmlParam2.Value = CreateXhtmlDoc("attributeValue") 'Same values!

        paramCollection1.InnerParameters.Add(xhtmlParam1)
        paramCollection2.InnerParameters.Add(xhtmlParam2)
        paramCollection1.Id = "invoer"
        paramCollection2.Id = "invoer"

        'Arrange
        Dim versionableEntity1 As IVersionable = Helper.CreateResourceEntity(New ItemResourceEntity(), assessmentItem1)
        Dim versionableEntity2 As IVersionable = Helper.CreateResourceEntity(New ItemResourceEntity(), assessmentItem2)

        'Act
        Dim resourceHistoryEntity1 As ResourceHistoryEntity = ResourceHistoryCreator.CreateResourceHistoryEntity(versionableEntity1, "user")
        Dim resourceHistoryEntity2 As ResourceHistoryEntity = ResourceHistoryCreator.CreateResourceHistoryEntity(versionableEntity2, "user")
        Dim metaDataCompareResults As New List(Of MetaDataCompareResult)
        metaDataCompareResults.AddRange(ResourceHistoryComparer.CompareResourceHistoryEntities(resourceHistoryEntity1, resourceHistoryEntity2, GetType(ItemResourceEntity), _resourceManager))

        'Assert
        Assert.IsNotNull(resourceHistoryEntity1.MetaData)
        Assert.IsNotNull(resourceHistoryEntity2.MetaData)
        Assert.IsNotNull(resourceHistoryEntity1.BinData)
        Assert.IsNotNull(resourceHistoryEntity2.BinData)
        Assert.IsNotNull(metaDataCompareResults)
        Assert.IsTrue(metaDataCompareResults.Count = 0)
    End Sub

    <TestMethod(), Description("Compare two AssessmentItem objects of type xhtml parameter. The number of parameters are the same but their values differ."), TestCategory("Logic")>
    Public Sub TestTwoAssessmentItemsWithIdenticaXhtmlParametersAndDifferentValues()
        Dim assessmentItem1 As AssessmentItem = Helper.CreateAssessmentItem("Title of AssessmentItem 1", _iltName1)
        Dim assessmentItem2 As AssessmentItem = Helper.CreateAssessmentItem("Title of AssessmentItem 2", _iltName1)
        Dim paramCollection1 As ParameterCollection = assessmentItem1.Parameters.AddNew()
        Dim paramCollection2 As ParameterCollection = assessmentItem2.Parameters.AddNew()
        Dim xhtmlParam1 As New XHtmlParameter()
        Dim xhtmlParam2 As New XHtmlParameter()
        xhtmlParam1.Name = "xhtmlParameter1"    'Same parameter names!
        xhtmlParam2.Name = "xhtmlParameter1"    'Same parameter names!
        xhtmlParam1.Value = CreateXhtmlDoc("attributeValue1")   'Different values!
        xhtmlParam2.Value = CreateXhtmlDoc("attributeValue2")   'Different values!

        paramCollection1.InnerParameters.Add(xhtmlParam1)
        paramCollection2.InnerParameters.Add(xhtmlParam2)
        paramCollection1.Id = "invoer"
        paramCollection2.Id = "invoer"

        'Arrange
        Dim versionableEntity1 As IVersionable = Helper.CreateResourceEntity(New ItemResourceEntity(), assessmentItem1)
        Dim versionableEntity2 As IVersionable = Helper.CreateResourceEntity(New ItemResourceEntity(), assessmentItem2)

        'Act
        Dim resourceHistoryEntity1 As ResourceHistoryEntity = ResourceHistoryCreator.CreateResourceHistoryEntity(versionableEntity1, "user")
        Dim resourceHistoryEntity2 As ResourceHistoryEntity = ResourceHistoryCreator.CreateResourceHistoryEntity(versionableEntity2, "user")
        Dim metaDataCompareResults As New List(Of MetaDataCompareResult)
        metaDataCompareResults.AddRange(ResourceHistoryComparer.CompareResourceHistoryEntities(resourceHistoryEntity1, resourceHistoryEntity2, GetType(ItemResourceEntity), _resourceManager))
        Dim docOldValue As New XmlDocument
        Dim docNewValue As New XmlDocument
        docOldValue.LoadXml(metaDataCompareResults(0).OldValue)
        docNewValue.LoadXml(metaDataCompareResults(0).NewValue)

        'Assert
        Assert.IsNotNull(resourceHistoryEntity1.MetaData)
        Assert.IsNotNull(resourceHistoryEntity2.MetaData)
        Assert.IsNotNull(resourceHistoryEntity1.BinData)
        Assert.IsNotNull(resourceHistoryEntity2.BinData)
        Assert.IsNotNull(metaDataCompareResults)
        Assert.IsTrue(metaDataCompareResults.Count = 1)
        Assert.IsTrue(metaDataCompareResults(0).PropertyName = xhtmlParam1.Name)
        Assert.IsTrue(docOldValue.OuterXml = xhtmlParam1.Value)
        Assert.IsTrue(docNewValue.OuterXml = xhtmlParam2.Value)
        Assert.IsTrue(metaDataCompareResults(0).LocalizedPropertyName <> metaDataCompareResults(0).PropertyName)
    End Sub

    Private Function CreateXhtmlDoc(attributeValue As String) As String
        Dim doc As New XmlDocument()
        Dim root As XmlElement = doc.CreateElement("root")
        Dim child1 As XmlElement = doc.CreateElement("child1")
        Dim attribute As XmlAttribute = doc.CreateAttribute("attribute1")
        attribute.Value = attributeValue
        child1.Attributes.Append(attribute)
        root.AppendChild(child1)
        doc.AppendChild(root)

        Return doc.OuterXml
    End Function

End Class
