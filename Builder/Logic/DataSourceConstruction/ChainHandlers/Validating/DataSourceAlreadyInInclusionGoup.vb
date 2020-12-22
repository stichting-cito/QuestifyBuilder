Imports Cito.Tester.Common
Imports Questify.Builder.Logic.Chain
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Cito.Tester.ContentModel.Datasources
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports Questify.Builder.Logic.Service.Factories

Public Class DataSourceAlreadyInInclusionGoup
    Inherits ChainHandlerBase(Of DataSourceConstructionRequest)

    Private Const CInclusion As String = "inclusion"

    Private ReadOnly _relatedItems As New Dictionary(Of String, String)


    Public Sub New(ByVal resourceManager As ResourceManagerBase, ByVal bankId As Integer)
        _relatedItems = GetItemsInInclusionGroups(bankId, resourceManager)
    End Sub

    Public Overrides Function ProcessRequest(requestData As DataSourceConstructionRequest) As ChainHandlerResult
        Dim exception As DataSourceValidationException = Nothing

        For Each itm As ResourceRef In requestData.Items
            Dim key As String = itm.Identifier
            If _relatedItems.ContainsKey(key) Then

                If (exception Is Nothing) Then
                    exception = New DataSourceValidationException()
                End If
                exception.AddValidationError(itm.Identifier, _relatedItems(key))
            End If
        Next

        If (Not exception Is Nothing) Then
            Throw exception
        End If

        Return ChainHandlerResult.RequestHandled
    End Function


    Public Shared Function GetItemsInInclusionGroups(bankId As Integer, resourcemanager As ResourceManagerBase) As Dictionary(Of String, String)
        Dim relatedItems As New Dictionary(Of String, String)
        Dim resEntityCol As EntityCollection

        resEntityCol = ResourceFactory.Instance.GetDataSourcesForBank(bankId, False, CInclusion)

        For Each dsResourceEntity As DataSourceResourceEntity In resEntityCol
            Dim settings As DataSourceSettings = Parsers.ParseItemDataSourceSettingsFromResourceEntity(dsResourceEntity)

            If settings IsNot Nothing Then
                Dim dataSource = CType(settings.CreateGetDataSource(), ItemDataSource)
                If dataSource IsNot Nothing Then
                    For Each item As ResourceRef In dataSource.Get(resourcemanager)
                        If Not relatedItems.ContainsKey(item.Identifier) Then
                            relatedItems.Add(item.Identifier,
                                             $"{dsResourceEntity.Name} @ bank {dsResourceEntity.BankName}")
                        Else
                            Debug.Assert(False,
             $"Item Present in multiple inclusion groups! Item [ {dsResourceEntity.Name _
                } ] found in bank [ {dsResourceEntity.BankName _
                } ]  previously found in [ {relatedItems(item.Identifier)} ]")
                        End If
                    Next
                End If
            End If
        Next
        Return relatedItems
    End Function


End Class
