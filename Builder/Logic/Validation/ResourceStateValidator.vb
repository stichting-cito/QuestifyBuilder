Imports Cito.Tester.Common
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports System.Text
Imports System.Linq
Imports Enums
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Logic.Service.Model.Entities

Public Class ResourceStateValidator
    Inherits BaseValidator

    Public Overrides ReadOnly Property Description() As String
        Get
            Return My.Resources.DescriptionResourceStateValidator
        End Get
    End Property

    Public Overrides Function IsDatasourceSupported() As Boolean
        Return True
    End Function

    Public Overrides Function GetValidationNameLocalized() As String
        Return My.Resources.NameResourceStateValidator
    End Function

    Public Overrides Function DoValidation() As ValidationResult
        If _entities.Count = 0 Then
            Return ValidateBank()
        Else
            Return ValidateAssessments()
        End If
    End Function

    Private Function ValidateAssessments() As ValidationResult
        Dim assesments = Collection.OfType(Of AssessmentTestResourceDto)

        Dim dependencyIds = assesments.SelectMany(Function(a) a.DependentResourceIds).ToList()
        Dim request = New ResourceRequestDTO With {.WithDependencies = True, .WithState = True}
        Dim dependencies = ResourceFactory.Instance.GetResourcesByIdsWithOption(dependencyIds, request)

        Return DoValidate(dependencies.OfType(Of ResourceEntity).ToList())
    End Function

    Private Function ValidateBank() As ValidationResult
        Dim testResources = ResourceFactory.Instance.GetAssessmentTestsForBank(_bankId).OfType(Of AssessmentTestResourceEntity)

        Dim items = ResourceFactory.Instance.GetItemsForBank(_bankId).OfType(Of ResourceEntity).Select(Function(i) i.ResourceId)
        Dim request = New ResourceRequestDTO With {.WithDependencies = True, .WithState = True}

        Dim dependencies = ResourceFactory.Instance.GetResourcesByIdsWithOption(items.ToList(), request)
        Return DoValidate(dependencies.OfType(Of ResourceEntity).ToList())
    End Function

    Public Event ValidationProgress(ByVal sender As Object, ByVal e As ProgressEventArgs)

    Private Function DoValidate(resourceEntities As List(Of ResourceEntity)) As ValidationResult
        Dim actionDictionary As New Dictionary(Of Integer, String)
        OnStartValidationProgress(New StartEventArgs(resourceEntities.Count))

        Dim states = resourceEntities.Where(Function(r) r.StateId.HasValue).Select(Function(r) r.StateId.Value).Distinct()
        For Each state In states
            Dim action = ResourceFactory.Instance.GetStateAction(state, "publication")
            If action IsNot Nothing Then
                actionDictionary.Add(state, action.Name)
            End If
        Next

        Dim resourcesWithWarnAction = resourceEntities.Where(Function(r) r.StateId.HasValue AndAlso actionDictionary.ContainsKey(r.StateId.Value) AndAlso actionDictionary(r.StateId.Value).ToLower = "warn").Distinct()
        Dim resourcesWithProhibitAction = resourceEntities.Where(Function(r) r.StateId.HasValue AndAlso actionDictionary.ContainsKey(r.StateId.Value) AndAlso actionDictionary(r.StateId.Value).ToLower = "prohibit").Distinct()

        Return CreateMessage(resourcesWithWarnAction, resourcesWithProhibitAction)
    End Function

    Private Function CreateMessage(resourcesWithWarnAction As IEnumerable(Of ResourceEntity), resourcesWithProhibitAction As IEnumerable(Of ResourceEntity)) As ValidationResult
        Dim summarytext As New StringBuilder
        summarytext.AppendLine()
        summarytext.AppendLine()

        If resourcesWithProhibitAction.Count > 0 Then
            summarytext.AppendLine(String.Format(My.Resources.TheFollowingResourcesPreventYouFromPublishingThisPackage, resourcesWithProhibitAction.Count))
            For Each resource In resourcesWithProhibitAction
                summarytext.AppendLine(String.Format("{0} -> {1}", resource.Name, resource.StateName))
            Next
            summarytext.AppendLine()
        End If

        If resourcesWithWarnAction.Count > 0 Then
            summarytext.AppendLine(String.Format(My.Resources.TheFollowingResourcesResultInAWarning, resourcesWithWarnAction.Count))
            For Each resource In resourcesWithWarnAction
                summarytext.AppendLine(String.Format("{0} -> {1}", resource.Name, resource.StateName))
            Next
        End If

        _resText = New StringBuilder()
        If Not resourcesWithProhibitAction.Count = 0 OrElse Not resourcesWithWarnAction.Count = 0 Then
            _resText.Append(String.Format(My.Resources.Warning, resourcesWithProhibitAction.Count.ToString, vbCrLf, resourcesWithWarnAction.Count.ToString))
            _isReportAvailable = True
            _exportString = summarytext.ToString
            If Not resourcesWithProhibitAction.Count = 0 Then
                Return ValidationResult.NotValid
            Else
                Return ValidationResult.Warning
            End If
        Else
            _resText.Append(My.Resources.ResourcesCanBePublished)
            Return ValidationResult.Valid
        End If
    End Function
End Class