Imports Cito.Tester.ContentModel
Imports Enums
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports Questify.Builder.Model.ContentModel.ResourceProperties
Imports Questify.Builder.UnitTests.Framework.FakeAppTemplate
Imports Questify.Builder.UnitTests.Framework.Faketory.interface
Imports Questify.Builder.UnitTests.Framework.My.Resources

Namespace Faketory.impl

    Friend MustInherit Class AddBase
        Implements IAddResources
        Implements IAddRootObjects
        Implements IAddAll


        Protected _resource As ResourceEntity

        Public Sub New(resource As ResourceEntity)
            _resource = resource
        End Sub



        Public Function Aspect(name As String, postAction As System.Action(Of AspectResourceEntity)) As IActionsAfter Implements IAddRootObjects.Aspect
            Dim newItem As New Questify.Builder.Model.ContentModel.EntityClasses.AspectResourceEntity() With {.Name = name}
            postAction(newItem)
            HookupResource(newItem)
            FakeDal.FakeServices.FakeResourceService.UpdateAspectResource(newItem)
            Return New AddKeyword(New AspectResourceEntity)
        End Function

        Public Function Aspect(name As String) As IActionsAfter Implements IAddRootObjects.Aspect
            Return Aspect(name, Sub() nop())
        End Function

        Private Function Item(name As String, postAction As System.Action(Of ItemResourceEntity)) As IActionsAfter Implements IAddRootObjects.Item
            Dim newItem As New ItemResourceEntity() With {.Name = name}
            postAction(newItem)
            HookupResource(newItem)
            FakeDal.FakeServices.FakeResourceService.UpdateItemResource(newItem)
            Return New AddKeyword(newItem)
        End Function

        Private Function Item(name As String) As IActionsAfter Implements IAddRootObjects.Item
            Return Item(name, Sub() nop())
        End Function

        Public Function ItemTemplate(name As String, postAction As System.Action(Of ItemLayoutTemplateResourceEntity)) As IActionsAfter Implements IAddRootObjects.ItemTemplate
            Dim newItem As New ItemLayoutTemplateResourceEntity() With {.Title = name, .Name = name}
            postAction(newItem)
            HookupResource(newItem)
            FakeDal.FakeServices.FakeResourceService.UpdateItemLayoutTemplateResource(newItem)
            Return New AddKeyword(newItem)
        End Function

        Public Function ItemTemplate(name As String) As IActionsAfter Implements IAddRootObjects.ItemTemplate
            Return ItemTemplate(name, Sub() nop())
        End Function

        Public Function ControlTemplate(name As String, postAction As System.Action(Of ControlTemplateResourceEntity)) As IActionsAfter Implements IAddRootObjects.ControlTemplate
            Dim newItem As New ControlTemplateResourceEntity() With {.Title = name, .Name = name}
            postAction(newItem)
            HookupResource(newItem)
            FakeDal.FakeServices.FakeResourceService.UpdateControlTemplateResource(newItem)
            Return New AddKeyword(newItem)
        End Function

        Public Function ControlTemplate(name As String) As IActionsAfter Implements IAddRootObjects.ControlTemplate
            Return ControlTemplate(name, Sub() nop())
        End Function

        Public Function SourceText(name As String, postAction As System.Action(Of GenericResourceEntity)) As IActionsAfter Implements IAddRootObjects.SourceText
            Return DoAddTextLike(name, "sourceText", "application/xhtml+xml", postAction)
        End Function

        Public Function SourceText(name As String) As IActionsAfter Implements IAddRootObjects.SourceText
            Return DoAddTextLike(name, "sourceText", "application/xhtml+xml", Sub() nop())
        End Function

        Public Function AssessmentTest(name As String) As IActionsAfter Implements IAddRootObjects.AssessmentTest
            Return AssessmentTest(name, Sub() nop())
        End Function

        Public Function AssessmentTest(name As String, postAction As Action(Of AssessmentTestResourceEntity)) As IActionsAfter Implements IAddRootObjects.AssessmentTest
            Return AssessmentTest(Guid.NewGuid, name, postAction)
        End Function

        Public Function AssessmentTest(resourceId As Guid, name As String, postAction As Action(Of AssessmentTestResourceEntity)) As IActionsAfter
            Dim newTest As New AssessmentTestResourceEntity() With {.ResourceId = resourceId, .Title = name, .Name = name}
            postAction(newTest)
            HookupResource(newTest)
            FakeDal.FakeServices.FakeResourceService.UpdateAssessmentTestResource(newTest)
            Return New AddKeyword(newTest)
        End Function

        Public Function AssessmentTest(id As Guid, name As String) As IActionsAfter Implements IAddRootObjects.AssessmentTest
            Return AssessmentTest(id, name, Sub() nop())
        End Function

        Public Function AssessmentTest(id As Guid, name As String, someTest As AssessmentTest2) As IActionsAfter Implements IAddRootObjects.AssessmentTest
            Return AssessmentTest(id, name, Sub(entity)
                                                entity.ResourceData = New ResourceDataEntity()
                                                entity.ResourceData.BinData = Cito.Tester.Common.SerializeHelper.XmlSerializeToByteArray(someTest)
                                            End Sub)
        End Function

        Public Function AssessmentTest(name As String, someTest As AssessmentTest2) As IActionsAfter Implements IAddRootObjects.AssessmentTest

            Return AssessmentTest(name, Sub(entity)
                                            entity.ResourceData = New ResourceDataEntity()
                                            entity.ResourceData.BinData = Cito.Tester.Common.SerializeHelper.XmlSerializeToByteArray(someTest)
                                        End Sub)

        End Function

        Sub nop()
        End Sub


        Public Function GenericResource(name As String, data As Byte()) As IAddResourcesKeyword Implements IAddResources.GenericResource
            Dim id As Guid = FakeDal.NextId()
            Dim ret As New GenericResourceEntity(id)

            ret.Name = name
            ret.Description = name
            ret.ResourceData = New ResourceDataEntity(FakeDal.NextId()) With {
                .BinData = data}
            ret.Bank = New BankEntity()
            HookupResource(ret)
            FakeDal.FakeServices.FakeResourceService.UpdateGenericResource(ret)

            Return New AddKeyword(ret)
        End Function

        Public Function Image(name As String) As IAddResourcesKeyword Implements IAddResources.Image
            Dim id As Guid = FakeDal.NextId()
            Dim ret As New GenericResourceEntity(id)
            Dim img As System.Drawing.Image = FakeStaticResources.transparentPix
            ret.Name = name
            ret.Dimensions = $"{img.Width} x {img.Height}"
            ret.MediaType = "image"
            ret.Description = name
            ret.ResourceData = New ResourceDataEntity(FakeDal.NextId()) With {
                .BinData = DirectCast(New System.Drawing.ImageConverter().ConvertTo(img, GetType(Byte())), Byte())}
            ret.Bank = New BankEntity()
            HookupResource(ret)
            FakeDal.FakeServices.FakeResourceService.UpdateGenericResource(ret)
            Return New AddKeyword(ret)
        End Function

        Public Function Image(name As String, postAction As Action(Of GenericResourceEntity)) As IAddResourcesKeyword Implements IAddResources.Image
            Dim ret As New GenericResourceEntity() With {.Name = name}
            postAction(ret)
            HookupResource(ret)
            FakeDal.FakeServices.FakeResourceService.UpdateGenericResource(ret)
            Return New AddKeyword(ret)
        End Function

        Public Function StyleSheet(name As String, stylesheetData As String) As IAddResourcesKeyword Implements IAddResources.StyleSheet
            Return DoAddTextLike(name, stylesheetData, "text/css", Sub() nop())
        End Function

        Public Function StyleSheet(name As String, stylesheetData As String, postAction As Action(Of GenericResourceEntity)) As IAddResourcesKeyword Implements IAddResources.StyleSheet
            Return DoAddTextLike(name, stylesheetData, "text/css", Sub(genericResource) postAction(genericResource))
        End Function

        Public Function Text(name As String, HtmlTxt As System.Xml.Linq.XElement) As IAddResourcesKeyword Implements IAddResources.Text
            Return Text(name, HtmlTxt, Sub() nop())
        End Function

        Public Function Text(name As String, HtmlTxt As System.Xml.Linq.XElement, postaction As Action(Of GenericResourceEntity)) As IAddResourcesKeyword Implements IAddResources.Text
            Return DoAddTextLike(name, HtmlTxt.ToString(), "text/plain", postaction)
        End Function


        Public Function SingleListProperty(code As Guid, name As String, listitems As List(Of KeyValuePair(Of String, String))) As IAddAll Implements IAddCustomProperties.SingleListProperty
            Dim listCustomBankProperty As ListCustomBankPropertyEntity = New ListCustomBankPropertyEntity()
            listCustomBankProperty.Code = code
            listCustomBankProperty.Name = name
            listCustomBankProperty.Title = name
            listCustomBankProperty.MultipleSelect = False
            listCustomBankProperty.ApplicableToMask = ResourceTypeEnum.AllResources

            For Each listitem As KeyValuePair(Of String, String) In listitems

                Dim value = listCustomBankProperty.ListValueCustomBankPropertyCollection.AddNew()
                value.Code = Guid.NewGuid()
                value.Name = listitem.Key
                value.Title = listitem.Value
            Next
            FakeDal.FakeServices.FakeBankService.UpdateCustomProperty(listCustomBankProperty)
            Return Me
        End Function

        Public Function TreeProperty(code As Guid, name As String) As IAddTreePropertyKeyword Implements IAddCustomProperties.TreeProperty
            Return New AddTreePropertyKeyword(code, name)
        End Function


        Private Function CreateResourcePropertyListDefinitionCollection(ByVal listValueCustomBankPropertyEntities As EntityCollection(Of ListValueCustomBankPropertyEntity)) As IList(Of ResourcePropertyListValueDefinition)
            Dim result As IList(Of ResourcePropertyListValueDefinition) = New List(Of ResourcePropertyListValueDefinition)()

            Return result
        End Function

        Private Function DoAddTextLike(name As String, data As String, media As String, postaction As Action(Of GenericResourceEntity)) As IActionsAfter
            Dim id As Guid = FakeDal.NextId()
            Dim ret As New GenericResourceEntity(id)
            ret.Name = name
            ret.MediaType = media
            ret.Description = name
            ret.ResourceData = New ResourceDataEntity(FakeDal.NextId()) With {
                .BinData = New System.Text.UTF8Encoding().GetBytes(data)}
            postaction(ret)
            HookupResource(ret)
            FakeDal.FakeServices.FakeResourceService.UpdateGenericResource(ret)
            Return New AddKeyword(ret)
        End Function

        Protected MustOverride Sub HookupResource(ret As ResourceEntity)

    End Class
End NameSpace