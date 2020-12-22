Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ItemProcessing
Imports Cito.Tester.Common
Imports System.Text
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Logic.Service.Factories

Public Class AssessmentItemHelper
    Inherits ParameterSetCollectionHelper
    Private ReadOnly _itemResource As ItemResourceEntity


    Public Sub New(resourceManager As ResourceManagerBase, itemLayoutTemplateName As String, itemResource As ItemResourceEntity, cachingStrategy As IITemSetupCacheHelper)
        MyBase.New(resourceManager, itemLayoutTemplateName)
        MyBase.CachingStrategy = cachingStrategy
        GetExtractedParameters()
        _itemResource = itemResource
    End Sub


    Public Sub ReFillParameterSet(assessmentItem As AssessmentItem)
        assessmentItem.Parameters.Clear()
        Dim extractedParameterSets As ParameterSetCollection = _extractedParameters
        assessmentItem.Parameters.AddRange(extractedParameterSets)
    End Sub

    Public Function MergeParameters(assessmentItem As AssessmentItem, ByRef warnErr As WarningsAndErrors) As Boolean
        Dim returnValue As Boolean = True
        Try
            Dim wrn1 As WarningsAndErrors = ParameterHandler.Merge(_extractedParameters, assessmentItem.Parameters)
            warnErr.Merge(wrn1)
        Catch scriptException As ControlTemplateScriptException
            warnErr.ErrorList.Add(String.Format("{0}{1}{1}{2}", scriptException.Message, vbNewLine, GetFormattedExceptionDetails(scriptException)))
            returnValue = False
        Catch ex As Exception
            warnErr.ErrorList.Add(String.Format("Error while compiling parameter sets: {0}{0}{1}{2}", vbNewLine, GetFormattedExceptionDetails(ex), ex.Message))
            returnValue = False
        End Try
        Return returnValue
    End Function

    Public Function GetExistingAssessmentItem() As AssessmentItem
        Try
            If _itemResource.ResourceData Is Nothing Then
                Dim itemResourceData As ResourceDataEntity = ResourceFactory.Instance.GetResourceData(_itemResource)
                _itemResource.ResourceData = itemResourceData
            End If
            Dim assessmentItem As Cito.Tester.ContentModel.AssessmentItem
            assessmentItem = DirectCast(SerializeHelper.XmlDeserializeFromByteArray(_itemResource.ResourceData.BinData, GetType(Cito.Tester.ContentModel.AssessmentItem), True), Cito.Tester.ContentModel.AssessmentItem)
            If String.IsNullOrEmpty(assessmentItem.ItemId) Then
                assessmentItem.ItemId = _itemResource.ItemId
            End If
            Return assessmentItem
        Catch ex As SD.LLBLGen.Pro.ORMSupportClasses.ORMEntityOutOfSyncException
            Debug.Assert(False, ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function CreateNewAssessmentItem(itemResource As ItemResourceEntity, itemLayoutTemplate As ItemLayoutTemplateResourceEntity, ByRef warnErr As WarningsAndErrors) As AssessmentItem
        Dim assessmentItem As AssessmentItem
        assessmentItem = CreateNewEntity(itemResource, itemLayoutTemplate)
        Try
            Dim extractedParameterSets As ParameterSetCollection = _extractedParameters
            assessmentItem.Parameters.AddRange(extractedParameterSets)
        Catch ex As Exception
            warnErr.ErrorList.Add($"Error while compiling parameter sets: {ex.Message}")
        End Try
        Return assessmentItem
    End Function



    Private Function CreateNewEntity(
        itemResource As ItemResourceEntity,
        itemLayoutTemplate As ItemLayoutTemplateResourceEntity) As AssessmentItem

        Dim item As New AssessmentItem
        With item
            .Title = String.Empty
            .Identifier = String.Empty
            .LayoutTemplateSourceName = itemLayoutTemplate.Name
            .itemId = String.Empty
        End With

        Dim templateDependency As DependentResourceEntity = itemResource.DependentResourceCollection.AddNew()
        With templateDependency
            .Resource = itemResource
            .DependentResource = itemLayoutTemplate
        End With

        Return item
    End Function

    Private Function GetFormattedExceptionDetails(ByVal exception As Exception) As String
        Dim builder As New StringBuilder()

        If exception.InnerException IsNot Nothing Then
            builder.Append(exception.InnerException.Message)
            builder.Append(vbNewLine)
            builder.Append(GetFormattedExceptionDetails(exception.InnerException))
        End If

        Return builder.ToString()
    End Function


End Class
