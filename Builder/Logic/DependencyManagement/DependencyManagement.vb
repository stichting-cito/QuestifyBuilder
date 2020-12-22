Imports Cito.Tester.ContentModel
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Logic.Service.Factories
Imports System.Linq
Imports Cito.Tester.Common

Public NotInheritable Class DependencyManagement

    Public Shared Function IsResourceUsedInOtherPartOfTheTest(ByVal assessmentTest As AssessmentTest2, ByVal resourceName As String) As Boolean
        Dim returnValue As Boolean = False

        If Not String.IsNullOrEmpty(resourceName) Then
            Dim views As List(Of String) = AssessmentTestv2Factory.GetIncludedViews(assessmentTest)

            For Each includedView As String In views
                Dim theView As AssessmentTestViewBase = AssessmentTestv2Factory.CreateView(assessmentTest, includedView)

                Dim dependentResources As ResourceEntryCollection = theView.GetAllResourcesForTest()
                If dependentResources.ContainsResource(resourceName) Then
                    returnValue = True
                End If
            Next
        End If

        Return returnValue
    End Function

    Public Shared Sub UpdateSectionResourceReferenceBookkeeping(ByVal assessmentTest As AssessmentTest2, ByVal testEntity As AssessmentTestResourceEntity, ByVal possibleResourceReferencesToAdd As List(Of String), ByVal possibleResourceReferencesToBeRemoved As List(Of String))
        For Each resourceEntity As ResourceEntity In ResourceFactory.Instance.GetResourcesByNamesWithOption(testEntity.BankId, possibleResourceReferencesToAdd.Where(Function(r) Not IsResourceUsedInOtherPartOfTheTest(assessmentTest, r)).ToList(), New ResourceRequestDTO())
            If resourceEntity IsNot Nothing Then
                AddDependentResourceToResource(testEntity, resourceEntity)
            End If
        Next
        For Each resourceName As String In possibleResourceReferencesToBeRemoved
            If Not IsResourceUsedInOtherPartOfTheTest(assessmentTest, resourceName) Then
                RemoveDependentResourceFromResource(testEntity, resourceName)
            End If
        Next
    End Sub

    Public Shared Sub RemoveDependentResourceFromResource(ByVal entity As ResourceEntity, ByVal resourceName As String)
        Dim missingDependentResources = ResourceFactory.Instance.GetResourcesByIdsWithOption(entity.DependentResourceCollection.Where(Function(r) r.DependentResource Is Nothing).Select(Function(r) r.DependentResourceId).ToList(), New ResourceRequestDTO())
        For Each depResource As DependentResourceEntity In entity.DependentResourceCollection
            Dim resource = depResource.DependentResource
            If resource Is Nothing Then resource = DirectCast(missingDependentResources.Items.Cast(Of ResourceEntity).FirstOrDefault(Function(i) i.ResourceId = depResource.DependentResourceId), PackageResourceEntity)
            If resource.Name.Equals(resourceName) Then
                entity.DependentResourceCollection.Remove(depResource)
                Exit For
            End If
        Next
    End Sub

    Public Shared Function AddDependentResourceToResource(ByVal parentEntity As ResourceEntity, ByVal resourceToAddEntity As ResourceEntity) As Boolean
        If parentEntity Is Nothing Then Throw New ArgumentException("Parameter parentEntity cannot be Nothing!")
        If resourceToAddEntity Is Nothing Then Throw New ArgumentException("Parameter resourceToAddEntity cannot be Nothing!")

        If Not parentEntity.ContainsDependentResource(resourceToAddEntity) Then
            Dim depResource As New DependentResourceEntity()

            depResource.DependentResource = resourceToAddEntity
            parentEntity.DependentResourceCollection.Add(depResource)

            Return True
        End If

        Return False
    End Function

    Public Shared Function AddDependentResourceToResource(ByVal parentEntity As ResourceEntity, ByVal resourceToAddEntityId As Guid) As Boolean
        Dim resourceEntity As ResourceEntity = ResourceFactory.Instance.GetResourceByIdWithOption(resourceToAddEntityId, New ResourceRequestDTO())

        If resourceEntity IsNot Nothing Then
            Return AddDependentResourceToResource(parentEntity, resourceEntity)
        End If

        Return False
    End Function

End Class
