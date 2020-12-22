Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Cito.Tester.ContentModel.Datasources
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports Questify.Builder.Logic.ResourceManager
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.Service.Factories

Namespace TestConstruction.ChainHandlers.Validating
    Friend Class ItemHelpers

        Public Const Inclusion As String = "inclusion"

        Public Const Exclusion As String = "exclusion"

        Public Shared Function GetItemsPerGroup(ByVal resourceManager As DataBaseResourceManager, ByVal dataSourceBehaviours() As String) As Dictionary(Of DataSourceSettings, IEnumerable(Of ResourceRef))
            If resourceManager Is Nothing Then
                Throw New ArgumentNullException("bankCresourceManagerontext")
            End If
            Dim bankId As Integer = resourceManager.BankId
            If dataSourceBehaviours.Length = 0 Then
                Throw New ArgumentException("dataSourceBehaviours")
            End If
            Dim relatedItems As New Dictionary(Of DataSourceSettings, IEnumerable(Of ResourceRef))
            Dim entityCollection As EntityCollection
            entityCollection = ResourceFactory.Instance.GetDataSourcesForBank(bankId, False, dataSourceBehaviours)
            For Each dsResourceEntity As DataSourceResourceEntity In entityCollection
                Dim settings As DataSourceSettings = Parsers.ParseItemDataSourceSettingsFromResourceEntity(dsResourceEntity)
                If settings IsNot Nothing Then
                    Dim items As IEnumerable(Of ResourceRef) = CType(settings.CreateGetDataSource(), ItemDataSource).Get(resourceManager)
                    relatedItems.Add(settings, items)

                End If
            Next
            Return relatedItems
        End Function

        Public Shared Function GetItemsFromDataSource(ByVal resourceManager As DataBaseResourceManager, ByVal datasourceSettings As DataSourceSettings) As IEnumerable(Of ResourceRef)
            Dim items As IEnumerable(Of ResourceRef) = CType(datasourceSettings.CreateGetDataSource(), ItemDataSource).Get(resourceManager)
            Return items
        End Function


        Public Shared Function GetAssessmentItem(ByVal entity As ResourceEntity) As Cito.Tester.ContentModel.AssessmentItem
            If entity Is Nothing Then
                Throw New ArgumentNullException("entity")
            End If

            Dim data As ResourceDataEntity = ResourceFactory.Instance.GetResourceData(entity)

            Dim item As Cito.Tester.ContentModel.AssessmentItem
            Dim resourceStream As IO.MemoryStream = New IO.MemoryStream(data.BinData)
            item = DirectCast(SerializeHelper.XmlDeserializeFromStream(resourceStream, GetType(Cito.Tester.ContentModel.AssessmentItem)), AssessmentItem)

            Return item
        End Function
    End Class

End Namespace