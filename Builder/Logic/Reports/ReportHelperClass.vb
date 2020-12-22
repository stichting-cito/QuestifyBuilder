Imports Cito.Tester.ContentModel
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Logic.HelperClasses
Imports System.Linq
Imports Cito.Tester.Common
Imports Questify.Builder.Logic.ResourceManager
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Logic.Service.Interfaces
Imports Questify.Builder.Logic.Service.Model.Entities

Public Class ReportHelperClass

    Private _resourceManager As ResourceManagerBase

    Public Sub New(bankId As Integer)
        _resourceManager = New DataBaseResourceManager(bankId, True)
    End Sub

    Public Shared Function GetTargetDictionaryByHandlers(handlers As List(Of IItemPreviewHandler)) As Dictionary(Of String, IItemPreviewHandler)
        Dim retValue As New Dictionary(Of String, IItemPreviewHandler)
        handlers.ForEach(Sub(h)
                             If Not retValue.ContainsKey(h.UserFriendlyName) Then
                                 retValue.Add(h.UserFriendlyName, h)
                             End If
                         End Sub)

        Return retValue
    End Function

    Public Function GetPreviewMethodsByItems(selectedEntities As IList(Of ResourceDto)) As List(Of IItemPreviewHandler)
        Dim targets = GetSupportedTypesFromItems(selectedEntities).Select(Function(s) s.ToLower())
        Dim previewers As List(Of IItemPreviewHandler) = GeneralHelper.CreateItemPreviewHandlers(Nothing)

        If previewers IsNot Nothing Then
            Return (From p In previewers Where targets.Contains(p.PreviewTarget.ToLower())).ToList
        End If

        Return Nothing
    End Function


    Public Function GetPreviewMethodsByTest(assessmentTest As AssessmentTest2) As List(Of IItemPreviewHandler)
        Dim previewers As List(Of IItemPreviewHandler) = GeneralHelper.CreateItemPreviewHandlers(Nothing)

        If previewers IsNot Nothing Then
            Return (From p In previewers Where assessmentTest.IncludedViews.Contains(p.PreviewTarget)).ToList
        End If

        Return Nothing
    End Function


    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")>
    Public Function GetFullTestEntity(ByVal entity As AssessmentTestResourceEntity) As AssessmentTestResourceEntity
        Return ResourceFactory.Instance.GetAssessmentTest(entity)
    End Function

    Public Function GetTestFromResource(ByVal testEntity As AssessmentTestResourceEntity) As AssessmentTest2
        testEntity.ResourceData = ResourceFactory.Instance.GetResourceData(testEntity)

        Return AssessmentTestv2Factory.ReturnAssessmentTestv2ModelFromByteArray(testEntity.ResourceData.BinData, True).AssessmentTestv2
    End Function

    Private Function GetSupportedTypesFromItems(selectedEntities As IList(Of ResourceDto)) As List(Of String)
        Dim supportedTargets As New List(Of String)
        Dim index As Integer = 0

        For Each item In selectedEntities.OfType(Of ItemResourceDto)
            Dim cachedSupportedViewsOfItemLayoutTemplates As New Dictionary(Of String, List(Of String))

            Dim templateTargets As New List(Of String)

            If Not cachedSupportedViewsOfItemLayoutTemplates.ContainsKey(item.ItemLayoutTemplateUsedName) Then
                Dim adapter As New ItemLayoutAdapter(item.ItemLayoutTemplateUsedName, Nothing, AddressOf SubControl_ResourceNeeded)

                templateTargets.AddRange(adapter.Template.GetEnabledTargetNames())
                cachedSupportedViewsOfItemLayoutTemplates.Add(item.ItemLayoutTemplateUsedName, templateTargets)
            Else
                templateTargets.AddRange(cachedSupportedViewsOfItemLayoutTemplates(item.ItemLayoutTemplateUsedName))
            End If

            If index = 0 Then
                supportedTargets.AddRange(templateTargets)
            Else
                Dim viewTypesToRemove As New List(Of String)

                For Each viewtype As String In supportedTargets
                    If Not templateTargets.Contains(viewtype) Then
                        viewTypesToRemove.Add(viewtype)
                    End If
                Next

                viewTypesToRemove.ForEach(Sub(rv)
                                              supportedTargets.Remove(rv)
                                          End Sub)
            End If

            index += 1
        Next

        Return supportedTargets
    End Function

    Private Sub SubControl_ResourceNeeded(ByVal sender As System.Object, ByVal e As Cito.Tester.ContentModel.ResourceNeededEventArgs)
        Dim resource As BinaryResource = Nothing
        Dim request = New ResourceRequestDTO()
        If e.TypedResourceType IsNot Nothing Then
            resource = _resourceManager.GetTypedResource(e.ResourceName, e.TypedResourceType, request)
        Else
            resource = _resourceManager.GetResource(e.ResourceName, e.StreamProcessingDelegate, request)
        End If

        e.BinaryResource = resource
    End Sub

End Class
