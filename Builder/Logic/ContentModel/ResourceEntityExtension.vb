
Imports System.Runtime.CompilerServices
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports System.Linq
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Model.ContentModel.HelperClasses

Namespace ContentModel

    Public Module ResourceEntityExtension

        <Extension>
        Public Function CopyToNew(Of T As ResourceEntity)(orgResource As T, newName As String) As T
            Dim worker As New ResourceEntityCopier()
            Return worker.GetCopy(Of T)(orgResource, newName, orgResource.Name)
        End Function

        <Extension>
        Public Sub SetIdentifierToSharedModel(Of T As ResourceEntity)(orgResource As T)
            If TypeOf orgResource Is ItemResourceEntity Then
                Dim itemResource = DirectCast(CObj(orgResource), ItemResourceEntity)
                Dim item = itemResource.GetAssessmentItem
                item.Identifier = itemResource.Name
                itemResource.SetAssessmentItem(item)
            ElseIf TypeOf orgResource Is AssessmentTestResourceEntity Then
                Dim testResource = DirectCast(CObj(orgResource), AssessmentTestResourceEntity)
                Dim test = testResource.GetAssessmentTest
                test.Identifier = testResource.Name
                testResource.SetAssessmentTest(test)
            ElseIf TypeOf orgResource Is TestPackageResourceEntity Then
                Dim testPackageResource = DirectCast(CObj(orgResource), TestPackageResourceEntity)
                Dim testPackage = testPackageResource.GetTestPackage
                testPackage.Identifier = testPackageResource.Name
                testPackageResource.SetTestPackage(testPackage)
            ElseIf TypeOf orgResource Is DataSourceResourceEntity Then
                Dim datasourceResource = DirectCast(CObj(orgResource), DataSourceResourceEntity)
                Dim datasource = datasourceResource.GetDatasource()
                datasource.Identifier = datasourceResource.Name
                datasourceResource.SetDataSource(datasource)
            ElseIf TypeOf orgResource Is AspectResourceEntity Then
                Dim aspectResource = DirectCast(CObj(orgResource), AspectResourceEntity)
                Dim aspect = aspectResource.GetAspect
                aspect.Identifier = aspectResource.Name
                aspectResource.SetAspect(aspect)
            End If
        End Sub

        <Extension>
        Public Function ResourceData(resource As ResourceEntity) As ResourceDataEntity
            If resource IsNot Nothing Then
                If resource.ResourceData IsNot Nothing AndAlso resource.ResourceData.BinData IsNot Nothing AndAlso Not resource.ResourceData.BinData.Count = 0 Then
                    Return resource.ResourceData
                Else
                    Return ResourceFactory.Instance.GetResourceData(resource)
                End If
            End If
            Return Nothing
        End Function

        <Extension>
        Public Sub EnsureResourceData(resource As ResourceEntity)
            resource.EnsureResourceData(False)
        End Sub

        <Extension>
        Public Sub EnsureResourceData(resource As ResourceEntity, force As Boolean)
            If resource IsNot Nothing Then
                If Not resource.IsNew AndAlso (force OrElse resource.ResourceData Is Nothing) Then
                    Dim data = ResourceFactory.Instance.GetResourceData(resource)
                    If data IsNot Nothing Then
                        resource.ResourceData = data
                    End If
                End If
            End If
        End Sub

        <Extension>
        Public Function StateHasChanged(entity As ResourceEntity) As Boolean
            Dim stateIdField = entity.Fields("StateId")
            If stateIdField Is Nothing Then Return False

            Dim dbValue = CType(stateIdField.DbValue, Integer)
            Dim currValue = CType(stateIdField.CurrentValue, Integer)

            Return dbValue <> currValue
        End Function

        <Extension>
        Public Function RequiresMajorVersionIncrement(entity As ResourceEntity) As Boolean
            If Not entity.StateHasChanged Then Return False

            If EntityAlreadyHasMajorVersion(entity) Then Return False

            If entity.Fields("StateId") Is Nothing Then Return False

            Dim action = ResourceFactory.Instance.GetStateAction(CType(entity.Fields("StateId").CurrentValue, Integer), "resourcesaving")
            If action Is Nothing Then Return False

            Select Case action.Name.ToLower
                Case "incrementversion"
                    Return True
            End Select

            Return False
        End Function

        Private Function EntityAlreadyHasMajorVersion(entity As ResourceEntity) As Boolean
            If entity.Fields("Version") IsNot Nothing Then
                Dim majorVersion As Integer
                Return Integer.TryParse(CType(entity.Fields("Version").CurrentValue, String), majorVersion)
            End If
            Return False
        End Function


        <Extension>
        Public Function References(entity As ResourceEntity) As EntityCollection
            Return ResourceFactory.Instance.GetReferencesForResource(entity)
        End Function

        <Extension>
        Public Function References(Of T)(entity As ResourceEntity) As IEnumerable(Of T)
            Return ResourceFactory.Instance.GetReferencesForResource(entity).OfType(Of T)()
        End Function

        <Extension>
        Public Function GetFullBankPath(entity As ResourceEntity) As String
            Return BankFactory.Instance.GetBankPath(entity.BankId)
        End Function
    End Module

End Namespace