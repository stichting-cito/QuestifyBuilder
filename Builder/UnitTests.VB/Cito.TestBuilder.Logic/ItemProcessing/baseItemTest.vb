
Imports System.Linq
Imports System.Xml.Linq
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Logic.ContentModel
Imports Questify.Builder.UnitTests.Framework
Imports Questify.Builder.UnitTests.Framework.FakeAppTemplate

Public Class baseItemTest

    Protected Function CreateItem(name As String, assessment As XElement, ByVal iltName As String) As ItemResourceEntity
        'Get ItemLayout
        Dim col = FakeDal.FakeServices.FakeResourceService.GetItemLayoutTemplatesForBank(Nothing)
        Dim ilt = DirectCast(col.Where(Function(i) DirectCast(i, ItemLayoutTemplateResourceEntity).Name = iltName).FirstOrDefault(), ItemLayoutTemplateResourceEntity)
        If (ilt Is Nothing) Then Assert.Fail(String.Format("ItemLayoutTemplateResourceEntity [{0}] not found", iltName))
        'Construct new Item
        Dim newitem As new ItemResourceEntity
        newitem.ResourceId = Guid.NewGuid 
        newitem.SetXmlAsBinData(assessment)
        dim assessmentItem = newitem.GetAssessmentItem
        newitem.Name =assessmentItem.Identifier 
        newitem.Title  =assessmentItem.Title 
        FakeDal.Add.Item(name, Sub(i)
                                    i = newitem
                               End Sub)

        'Hookup ItemLayout (ilt) Template to Item (itm)
        newitem.DependentResourceCollection.Add(New DependentResourceEntity() With {.DependentResourceId = ilt.ResourceId, .DependentResource = ilt})

        'return
        Return newitem
    End Function

End Class
