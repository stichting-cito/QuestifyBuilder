
Imports Questify.Builder.Logic.Chain
Imports Cito.Tester.ContentModel.Datasources
Imports Cito.Tester.ContentModel
Imports System.Linq
Imports Questify.Builder.Logic.ResourceManager
Imports Cito.Tester.Common
Imports Questify.Builder.Logic.TestConstruction.Requests

Namespace TestConstruction.ChainHandlers.Validating
    ''' <summary>
    '''     When adding the all the items at one of an inclusion datasource. Prefer setting a datasource on the section. If
    '''     current section contains unrelated item suggest new section.
    ''' </summary>
    ''' <remarks></remarks>
    Class PreferDatabindingValidation
        Inherits ChainHandlerBase(Of TestConstructionRequest)

        Private ReadOnly _comparer As IEqualityComparer(Of ResourceRef) = New ResourceRefIdentityEqualityComparer 
        Private ReadOnly _targetSection As TestSection2
        Private ReadOnly _resourceManager As DataBaseResourceManager
        Private _groups As New Dictionary(Of DataSourceSettings, IEnumerable(Of ResourceRef))

        Sub New(ByVal resourceManager As DataBaseResourceManager, ByVal targetSection As TestSection2)
            _targetSection = targetSection
            _resourceManager = resourceManager
        End Sub

        Public Overrides Function ProcessRequest(requestData As TestConstructionRequest) As ChainHandlerResult
            Dim dsAddedAsWhole As List(Of DataSourceSettings)
            Dim nrOfItemsInTargetSection As Integer = _targetSection.Components.Count 
            'Count all. It's best to add new section with binding when other sections are present.
            Dim datasourceId As String = _targetSection.ItemDataSource
            Dim toSuggestToBindToCurrent As Boolean
            'All inclusion groups
            _groups = ItemHelpers.GetItemsPerGroup(_resourceManager, New String() {"inclusion"})
            'possible conflicting datasources
            dsAddedAsWhole = FindADatasourcesContainedInRequest(requestData)

            'if the requested items are the same as the group being added,.. we can possibly set binding to current section.
            toSuggestToBindToCurrent = (dsAddedAsWhole.Count = 1) AndAlso SetOperations.ContainsAll(requestData.Items,
                                                                                                    ItemHelpers.
                                                                                                       GetItemsFromDataSource(
                                                                                                           _resourceManager,
                                                                                                           dsAddedAsWhole.First()),
                                                                                                    _comparer)

            'Verify target
            For Each ds As DataSourceSettings In dsAddedAsWhole
                If Not (ds.Identifier = datasourceId) Then
                    'Found whole dataset, now suggest new section of bind to current?
                    If (toSuggestToBindToCurrent) AndAlso (nrOfItemsInTargetSection = 0) Then 
                        'one dataset and no other items in section?
                        'Suggest to set binding to current section
                        Throw New SuggestDatasourceBindingException(ds.Identifier, _groups(ds), _targetSection)
                    Else
                        'Suggest to set binding to NEW Nested section
                        Throw _
                            New SuggestDatasourceBindingException(ds.Identifier, _groups(ds), _targetSection,
                                                                  $"{_targetSection.Title}.{ds.Identifier}")
                    End If
                Else
                    'All items are same as current section ds binding,. .do nothing.
                End If
            Next


            Return ChainHandlerResult.RequestHandled
        End Function


        Private Function FindADatasourcesContainedInRequest(requestData As TestConstructionRequest) As List(Of DataSourceSettings)
            Dim ret As New List(Of DataSourceSettings)

            Dim itmsToCheck As New List(Of ResourceRef) 'Do not check items that with overridden target.
            itmsToCheck.AddRange(From e In requestData.Items Where Not requestData.OverridenTarget.ContainsKey(e) Select e)

            For Each ds As DataSourceSettings In _groups.Keys
                If (SetOperations.ContainsAll(_groups(ds), itmsToCheck, _comparer)) Then
                    ret.Add(ds)
                End If
            Next

            Return ret
        End Function

    End Class
End Namespace