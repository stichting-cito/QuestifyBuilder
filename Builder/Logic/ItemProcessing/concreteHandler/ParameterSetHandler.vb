Imports System.Linq
Imports Questify.Builder.Logic.ContentModel
Imports Cito.Tester.ContentModel
Imports Versioning

Namespace ItemProcessing

    Friend Class ParameterSetHandler

        Public Sub Merge(ByVal newParameters As ParameterSetCollection, ByVal currentParameters As ParameterSetCollection, ByVal warnErr As WarningsAndErrors)

            For Each collection As ParameterCollection In currentParameters

                Dim prmCollMerger As New ParameterCollectionHandler()
                Dim mirrorCollection As ParameterCollection = DirectCast(newParameters.GetParamCollectionByControlId(collection.Id), ParameterCollection)

                If collectionWasFound(mirrorCollection) Then

                    prmCollMerger.Merge(mirrorCollection, collection, warnErr)

                ElseIf IsDynamicCollection(collection) Then

                    Dim clonedCollection = collection.DeepCloneWithDesignerSettingsAndAttributeReferences()
                    newParameters.Add(clonedCollection)

                Else
                    warnErr.WarningList.Add(String.Format(My.Resources.ItemProcessing.ValidateParametersCollectionWillBeDeleted, collection.Id))
                End If

            Next
        End Sub

        Private Function IsDynamicCollection(parameterCollection As ParameterCollection) As Boolean
            Return parameterCollection.IsDynamicCollection
        End Function

        Private Function collectionWasFound(parameterCollection As ParameterCollection) As Boolean
            Return parameterCollection IsNot Nothing
        End Function

        Public Sub Compare(ByVal newParameters As ParameterSetCollection, ByVal currentParameters As ParameterSetCollection, ByVal metaDataCompareResults As List(Of MetaDataCompareResult))
            For Each currentSet As ParameterCollection In currentParameters
                Dim prmCollMerger As New ParameterCollectionHandler()
                Dim newCollection As ParameterCollection = DirectCast(newParameters.GetParamCollectionByControlId(currentSet.Id), ParameterCollection)

                If newCollection IsNot Nothing Then
                    metaDataCompareResults.AddRange(prmCollMerger.Compare(newCollection, currentSet))
                End If
            Next

            For Each newSet As ParameterCollection In newParameters.Where(Function(newPrmSet) Not currentParameters.Any(Function(currentPrmSet) currentPrmSet.Id = newPrmSet.Id))
                Dim prmCollMerger As New ParameterCollectionHandler()
                metaDataCompareResults.AddRange(prmCollMerger.Compare(newSet, New ParameterCollection() With {.Id = newSet.Id}))
            Next
        End Sub

    End Class
End Namespace

