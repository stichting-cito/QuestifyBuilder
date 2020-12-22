
Imports System.Runtime.CompilerServices

Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.Service.Factories

Namespace ContentModel

    Public Module ItemLayoutTemplateResourceExtension

        <Extension>
        Public Function GetItemLayoutTemplate(templateResource As ItemLayoutTemplateResourceEntity) As ItemLayoutTemplate
            Dim ret As ItemLayoutTemplate = Nothing

            If (templateResource.ResourceData Is Nothing) Then
                templateResource.ResourceData = ResourceFactory.Instance.GetResourceData(templateResource)
            End If

            If templateResource.ResourceData IsNot Nothing AndAlso templateResource.ResourceData.BinData.Length > 0 Then
                ret = DirectCast(Cito.Tester.Common.SerializeHelper.XmlDeserializeFromByteArray(templateResource.ResourceData.BinData, GetType(ItemLayoutTemplate), True), ItemLayoutTemplate)
            End If

            Return ret
        End Function

    End Module

End Namespace
