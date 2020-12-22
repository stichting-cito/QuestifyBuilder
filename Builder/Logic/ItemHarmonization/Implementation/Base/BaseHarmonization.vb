Imports System.Collections.Concurrent
Imports Cito.Tester.Common
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Logic.ItemHarmonization.Interface
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.Service.Factories

Namespace ItemHarmonization.Implementation.Base
    Public MustInherit Class BaseHarmonization
        Implements IHarmonize
        Implements IDisposable

        Private ReadOnly _bankResourceManagerCache As New ConcurrentDictionary(Of Integer, ResourceManagerHolder)()
        Private ReadOnly _parameterSetCache As New ConcurrentDictionary(Of String, ParameterSetCollection)()

        Protected ParametersetCollections As New ConcurrentDictionary(Of String, ParameterSetCollection)(StringComparer.OrdinalIgnoreCase)


        Public Overridable Function Harmonize(templates As IEnumerable(Of String), item As ItemResourceEntity) As Boolean Implements IHarmonize.Harmonize
            Dim dictionary As ConcurrentDictionary(Of String, ParameterSetCollection) = New ConcurrentDictionary(Of String, ParameterSetCollection)()
            For Each template As String In templates
                Dim extractedParameterSets As ParameterSetCollection = GetParameterSetForTemplate(item.BankId, template)
                dictionary.TryAdd(template, extractedParameterSets)
            Next
            Return Harmonize(dictionary, item)
        End Function

        Public Function Harmonize(parametersetColl As ConcurrentDictionary(Of String, ParameterSetCollection), item As ItemResourceEntity) As Boolean Implements IHarmonize.Harmonize
            ParametersetCollections = parametersetColl
            Return Harmonize(item)
        End Function

        Public Function Harmonize(item As ItemResourceEntity) As Boolean Implements IHarmonize.Harmonize
            Return Harmonize(item.GetAssessmentItem(), item)
        End Function

        Public Overridable Function Harmonize(assementItem As AssessmentItem, item As ItemResourceEntity) As Boolean Implements IHarmonize.Harmonize

        End Function

        Public Overridable Function Harmonize(assementItem As AssessmentItem, item As ItemResourceEntity, template As String) As Boolean Implements IHarmonize.Harmonize

        End Function

        Protected Function GetParameterSetForTemplate(itemBankId As Integer, template As String) As ParameterSetCollection
            If Not _parameterSetCache.ContainsKey(template) Then
                Dim templateBankId As Integer = ResourceFactory.Instance.GetResourceByNameWithOption(itemBankId, template, new ResourceRequestDTO()).BankId
                If Not _bankResourceManagerCache.ContainsKey(templateBankId) Then
                    _bankResourceManagerCache.TryAdd(templateBankId, New ResourceManagerHolder(templateBankId))
                End If
                Dim adapter As ItemLayoutAdapter = New ItemLayoutAdapter(template, Nothing, _bankResourceManagerCache(templateBankId).ResourceNeeded)
                Dim newParameterSets As ParameterSetCollection = ParameterSetCollection.DeepClone(adapter.CreateParameterSetsFromItemTemplate())
                _parameterSetCache.TryAdd(template, newParameterSets)
            End If
            Return _parameterSetCache(template)
        End Function

        Private disposedValue As Boolean

        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not disposedValue Then
                If disposing Then
                    _bankResourceManagerCache.Clear()
                    _parameterSetCache.Clear()
                    ParametersetCollections.Clear()
                End If
            End If
            disposedValue = True
        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
        End Sub
    End Class
End Namespace
