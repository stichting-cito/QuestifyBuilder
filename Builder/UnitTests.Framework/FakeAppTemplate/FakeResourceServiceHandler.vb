Imports Cito.Tester.Common
Imports FakeItEasy
Imports FakeItEasy.Core
Imports HelperClasses
Imports Questify.Builder.Logic.Service.Interfaces
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports Questify.Builder.UnitTests.Framework.My.Resources

Namespace FakeAppTemplate

    Public Class FakeResourceServiceHandler

        Private _fakeResourceService As IResourceService
        Private _collResources As EntityCollection

        Public Sub New(fakeResourceService As IResourceService)
            _collResources = New EntityCollection
            _fakeResourceService = fakeResourceService
        End Sub


        Public Sub InitDefault()
            A.CallTo(Function() _fakeResourceService.GetAspectsForBank(A(Of Integer).Ignored)).ReturnsLazily(Function(a) GetAspectForBank(a))

            A.CallTo(Function() _fakeResourceService.GetResourceByNameWithOption(A(Of Integer).Ignored,
                                                                     A(Of String).Ignored,
                                                                     A(Of ResourceRequestDTO).Ignored)).ReturnsLazily(Function(a) GetResourceByName(a))

            A.CallTo(Function() _fakeResourceService.GetResourcesByNamesWithOption(A(Of Integer).Ignored,
                                                                                   A(Of List(Of String)).Ignored,
                                                                                   A(Of ResourceRequestDTO).Ignored)).ReturnsLazily(Function(a) GetResourcesByNames(a))

            A.CallTo(Function() _fakeResourceService.GetGenericResourceForBank(A(Of Integer).Ignored,
                                                                   A(Of String).Ignored,
                                                                   A(Of String).Ignored,
                                                                   A(Of Boolean).Ignored)).ReturnsLazily(Function(a) GetGenericResourceForBank(a))

            A.CallTo(Function() _fakeResourceService.GetItemLayoutTemplate(A(Of ItemLayoutTemplateResourceEntity).Ignored)
                     ).ReturnsLazily(Function(a) ItemLayoutTemplateResourceEntity(a))

            A.CallTo(Function() _fakeResourceService.GetResourceData(A(Of ResourceEntity).Ignored)
                     ).ReturnsLazily(Function(a) GetResourceData(a))

            A.CallTo(Function() _fakeResourceService.GetResourceDataByResourceId(A(Of Guid).Ignored)
                     ).ReturnsLazily(Function(a) GetResourceDataById(a))

            A.CallTo(Function() _fakeResourceService.GetItemsForBank(A(Of Integer).Ignored)
                     ).ReturnsLazily(Function(a) GetItemsForBank(a))

            A.CallTo(Function() _fakeResourceService.GetItemsForBankWithFullCustomProperties(A(Of Integer).Ignored)
                     ).ReturnsLazily(Function(a) GetItemsForBank(a))

            A.CallTo(Function() _fakeResourceService.GetItemLayoutTemplatesForBank(A(Of Integer).Ignored)
                     ).ReturnsLazily(Function(a) GetItemLayoutTemplatesForBank(a))

            A.CallTo(Function() _fakeResourceService.GetAspectsForBank(A(Of Integer).Ignored)
                     ).ReturnsLazily(Function(a) GetAspectsForBank(a))

            A.CallTo(Function() _fakeResourceService.GetControlTemplatesForBank(A(Of Integer).Ignored)
                     ).ReturnsLazily(Function(a) GetControlTemplatesForBank(a))

            A.CallTo(Function() _fakeResourceService.GetResourcesForBank(A(Of Integer).Ignored)
                     ).ReturnsLazily(Function(a) GetResourcesForBank(Of ResourceEntity)(a))

            A.CallTo(Function() _fakeResourceService.GetAssessmentTestsForBank(A(Of Integer).Ignored)
                     ).ReturnsLazily(Function(a) GetResourceXForBank(Of AssessmentTestResourceEntity)(a))

            A.CallTo(Function() _fakeResourceService.GetItemsByCodes(A(Of List(Of String)).Ignored, A(Of Integer).Ignored, A(Of ItemResourceRequestDTO).Ignored)).ReturnsLazily(Function(a) GetItemsByCodes(a))

            A.CallTo(Function() _fakeResourceService.GetResourceByIdWithOption(A(Of Guid).Ignored, A(Of ResourceRequestDTO).Ignored)).ReturnsLazily(Function(args) GetResourceById(args))

            A.CallTo(Function() _fakeResourceService.GetResourcesByIdsWithOption(A(Of List(Of Guid)).Ignored, A(Of ResourceRequestDTO).Ignored)).ReturnsLazily(Function(args) GetResourcesByIds(args))

            A.CallTo(Function() _fakeResourceService.GetResourceByNameWithOption(A(Of Integer).Ignored, A(Of String).Ignored, A(Of ResourceRequestDTO).Ignored)).ReturnsLazily(Function(args) GetResourceByName(args))

            A.CallTo(Function() _fakeResourceService.GetResourcesByNamesWithOption(A(Of Integer).Ignored, A(Of List(Of String)).Ignored, A(Of ResourceRequestDTO).Ignored)).ReturnsLazily(Function(args) GetResourcesByNames(args))

            A.CallTo(Function() _fakeResourceService.GetControlTemplate(A(Of ControlTemplateResourceEntity).Ignored)).ReturnsLazily(Function(args) GetControlTemplate(args))

            A.CallTo(Function() _fakeResourceService.GetDependenciesForResource(A(Of ResourceEntity).Ignored)).ReturnsLazily(Function(args) GetDependenciesForResource(args))

            A.CallTo(Function() _fakeResourceService.GetItemLayoutTemplatesFromListOfResourceIds(A(Of IEnumerable(Of Guid)).Ignored, A(of Boolean).Ignored)).ReturnsLazily(Function(args) GetItemLayoutTemplatesFromListOfResourceIds(args))
        End Sub

        Public Sub EnableSaveResources()
            A.CallTo(Function() _fakeResourceService.UpdateGenericResource(A(Of GenericResourceEntity).Ignored)
).ReturnsLazily(Function(a) UpdateGenericResourceForBank(a))

            A.CallTo(Function() _fakeResourceService.UpdateItemResource(A(Of ItemResourceEntity).Ignored)
         ).ReturnsLazily(Function(a) UpdateItemResource(a))

            A.CallTo(Function() _fakeResourceService.UpdateItemLayoutTemplateResource(A(Of ItemLayoutTemplateResourceEntity).Ignored)
                     ).ReturnsLazily(Function(a) UpdateItemLayoutTemplateResource(a))

            A.CallTo(Function() _fakeResourceService.UpdateAspectResource(A(Of AspectResourceEntity).Ignored)
                     ).ReturnsLazily(Function(a) UpdateAspectResource(a))

            A.CallTo(Function() _fakeResourceService.UpdateControlTemplateResource(A(Of ControlTemplateResourceEntity).Ignored)
                     ).ReturnsLazily(Function(a) UpdateControlTemplateResource(a))

            A.CallTo(Function() _fakeResourceService.UpdateAssessmentTestResource(A(Of AssessmentTestResourceEntity).Ignored)
                     ).ReturnsLazily(Function(a) UpdateResource(Of AssessmentTestResourceEntity)(a))

            A.CallTo(Function() _fakeResourceService.UpdateAssessmentTestResource(A(Of AssessmentTestResourceEntity).Ignored, A(Of Boolean).Ignored, A(Of Boolean).Ignored)
                     ).ReturnsLazily(Function(a) UpdateResource(Of AssessmentTestResourceEntity)(a))

        End Sub



        Public ReadOnly Property ResourceCollection As EntityCollection
            Get
                Return _collResources
            End Get
        End Property



        Private Function GetResourceByName(a As Core.IFakeObjectCall) As ResourceEntity
            Dim resName As String = a.Arguments.Get(Of String)(1)
            For Each e As ResourceEntity In _collResources
                If (e.Name = resName) Then
                    Return e
                End If
            Next
            Return Nothing
        End Function

        Private Function GetResourcesByNames(a As Core.IFakeObjectCall) As EntityCollection
            Dim returnValue As New EntityCollection()
            Dim names As List(Of String) = a.GetArgument(Of List(Of String))(1)
            Dim list = _collResources.OfType(Of ResourceEntity).Where(Function(c) names.Contains(c.Name)).ToList()
            returnValue.AddRange(list)
            Return returnValue
        End Function

        Private Function GetResourcesByIds(a As Core.IFakeObjectCall) As EntityCollection
            Dim returnValue As New EntityCollection()
            Dim ids As List(Of Guid) = a.GetArgument(Of List(Of Guid))(0)
            Dim list = _collResources.OfType(Of ResourceEntity).Where(Function(c) ids.Contains(c.ResourceId)).ToList()
            returnValue.AddRange(list)
            Return returnValue
        End Function


        Private Function GetResourcesForBank(Of T As ResourceEntity)(a As Core.IFakeObjectCall) As EntityCollection
            Dim ret As New EntityCollection()
            For Each o In (From e In _collResources
                           Where TypeOf e Is ResourceEntity Select e)
                ret.Add(o)
            Next
            Return ret
        End Function

        Private Function GetAspectForBank(a As Core.IFakeObjectCall) As EntityCollection
            Dim ret As New EntityCollection()
            For Each o In (From e In _collResources
                           Where TypeOf e Is AspectResourceEntity Select e)
                ret.Add(o)
            Next
            Return ret
        End Function

        Private Function GetControlTemplate(a As Core.IFakeObjectCall) As ControlTemplateResourceEntity

            Dim ret As New ControlTemplateResourceEntity()
            Dim arg As ControlTemplateResourceEntity = a.GetArgument(Of ControlTemplateResourceEntity)(0)
            For Each e As ControlTemplateResourceEntity In _collResources.OfType(Of ControlTemplateResourceEntity)()
                If e.ResourceId = arg.ResourceId Then
                    Return e
                End If
            Next

            Return ret
        End Function

        Private Function GetGenericResourceForBank(a As Core.IFakeObjectCall) As EntityCollection
            Dim filter = a.Arguments.Get(Of String)(1)
            Dim ret As New EntityCollection()
            For Each o In (From e In _collResources
                           Where TypeOf e Is GenericResourceEntity AndAlso
                                 (DirectCast(e, GenericResourceEntity).MediaType = filter) Select e)
                ret.Add(o)
            Next
            Return ret
        End Function

        Private Function ItemLayoutTemplateResourceEntity(a As Core.IFakeObjectCall) As ItemLayoutTemplateResourceEntity
            Dim ret = a.GetArgument(Of ItemLayoutTemplateResourceEntity)(0)
            Return ret
        End Function

        Private Function GetResourceData(a As Core.IFakeObjectCall) As ResourceDataEntity
            Dim ret As New ResourceDataEntity
            Dim arg As ResourceEntity = a.GetArgument(Of ResourceEntity)(0)

            Select Case arg.Name

                Case "DefaultStylesheet.css"
                    ret.BinData = New System.Text.UTF8Encoding().GetBytes(FakeStaticResources.DefaultStyleSheet)
                Case Else

                    For Each e As GenericResourceEntity In _collResources.Where(Function(r) TypeOf r Is GenericResourceEntity)
                        If e.Name = arg.Name Then
                            Return e.ResourceData
                        End If
                    Next

                    For Each e As AssessmentTestResourceEntity In _collResources.Where(Function(r) TypeOf r Is AssessmentTestResourceEntity)
                        If e.Name = arg.Name Then
                            Return e.ResourceData
                        End If
                    Next

                    If (arg.ResourceData IsNot Nothing) Then
                        Return arg.ResourceData
                    End If

                    Debug.Assert(False, $"GetResourceData for '{arg.Name}' not implemented/fakes.")
            End Select

            Return ret

        End Function


        Private Function GetResourceDataById(a As Core.IFakeObjectCall) As ResourceDataEntity
            Dim id As Guid = a.GetArgument(Of Guid)(0)

            Dim resource = _collResources.ToList.OfType(Of ResourceEntity).FirstOrDefault(Function(r) r.ResourceId = id)
            If (resource IsNot Nothing) Then Return resource.ResourceData

            Return Nothing

        End Function

        Private Function GetItemsForBank(a As Core.IFakeObjectCall) As EntityCollection
            Dim ret As New EntityCollection
            ret.AddRange(From e In _collResources Where TypeOf e Is ItemResourceEntity Select e)
            Return ret
        End Function

        Private Function GetItemsByCodes(a As Core.IFakeObjectCall) As ItemResourceEntityCollection
            Dim codes As List(Of String) = a.GetArgument(Of List(Of String))(0)
            Dim ret As New ItemResourceEntityCollection
            For Each e In _collResources.OfType(Of ItemResourceEntity)
                If (codes.Contains(e.Name)) Then
                    If ret.Any(Function(i) i.Name = e.Name) Then
                        ret.Remove(ret.FirstOrDefault(Function(i) i.Name = e.Name))
                    End If
                    ret.Add(DirectCast(e, ItemResourceEntity))
                End If
            Next
            Return ret
        End Function
        Private Function GetItemLayoutTemplatesForBank(a As Core.IFakeObjectCall) As EntityCollection
            Dim ret As New EntityCollection
            ret.AddRange(From e In _collResources Where TypeOf e Is ItemLayoutTemplateResourceEntity Select e)
            Return ret
        End Function

        Private Function GetAspectsForBank(a As Core.IFakeObjectCall) As EntityCollection
            Dim ret As New EntityCollection
            ret.AddRange(From e In _collResources Where TypeOf e Is AspectResourceEntity Select e)
            Return ret
        End Function

        Private Function GetControlTemplatesForBank(a As Core.IFakeObjectCall) As EntityCollection
            Dim ret As New EntityCollection
            ret.AddRange(From e In _collResources Where TypeOf e Is ControlTemplateResourceEntity Select e)
            Return ret
        End Function

        Private Function GetResourceXForBank(Of T As ResourceEntity)(a As Core.IFakeObjectCall) As EntityCollection
            Dim ret As New EntityCollection
            ret.AddRange(From e In _collResources Where TypeOf e Is T Select e)
            Return ret
        End Function

        Private Function GetResourceById(args As Core.IFakeObjectCall) As ResourceEntity
            Dim idToFind As Guid = args.Arguments.Get(Of Guid)(0)
            For Each e As ResourceEntity In _collResources
                If (e.ResourceId = idToFind) Then
                    Return e
                End If
            Next
            Return Nothing
        End Function

        Private Function GetDependenciesForResource(ByVal args As IFakeObjectCall) As EntityCollection
            Dim resource As ResourceEntity = args.Arguments.Get(Of ResourceEntity)(0)

            Dim resources As New EntityCollection()
            For Each e As DependentResourceEntity In resource.DependentResourceCollection
                If e.DependentResource IsNot Nothing Then
                    resources.Add(e.DependentResource)
                End If
            Next

            Return resources
        End Function

        Private Function GetItemLayoutTemplatesFromListOfResourceIds(a As Core.IFakeObjectCall) As EntityCollection
            Dim returnValue As New EntityCollection()
            Dim ids As IEnumerable(Of Guid) = a.GetArgument(Of IEnumerable(Of Guid))(0)
            Dim list = _collResources.OfType(Of ResourceEntity).Where(Function(c) ids.Contains(c.ResourceId)).ToList()
            returnValue.AddRange(list)
            Return returnValue
        End Function




        Private Function UpdateGenericResourceForBank(a As Core.IFakeObjectCall) As String
            Dim entry = a.Arguments.Get(Of GenericResourceEntity)(0)
            _collResources.Add(entry)
            Return String.Empty
        End Function

        Private Function UpdateItemResource(a As Core.IFakeObjectCall) As String
            Dim entry = a.Arguments.Get(Of ItemResourceEntity)(0)
            Dim entryToRemove = _collResources.OfType(Of ItemResourceEntity).FirstOrDefault(Function(i) i.ResourceId = entry.ResourceId)
            If entryToRemove IsNot Nothing Then _collResources.Remove(entryToRemove)
            _collResources.Add(entry)
            Return String.Empty
        End Function

        Private Function UpdateItemLayoutTemplateResource(a As Core.IFakeObjectCall) As String
            Dim entry = a.Arguments.Get(Of ItemLayoutTemplateResourceEntity)(0)
            _collResources.Add(entry)
            Return String.Empty
        End Function

        Private Function UpdateAspectResource(a As Core.IFakeObjectCall) As String
            Dim entry = a.Arguments.Get(Of AspectResourceEntity)(0)
            _collResources.Add(entry)
            Return String.Empty
        End Function

        Private Function UpdateControlTemplateResource(a As Core.IFakeObjectCall) As String
            Dim entry = a.Arguments.Get(Of ControlTemplateResourceEntity)(0)
            _collResources.Add(entry)
            Return String.Empty
        End Function

        Private Function UpdateResource(Of T As ResourceEntity)(a As Core.IFakeObjectCall) As String
            Dim entry = a.Arguments.Get(Of T)(0)
            _collResources.Add(entry)
            Return String.Empty
        End Function


    End Class
End NameSpace