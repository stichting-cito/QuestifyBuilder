Imports System.Runtime.CompilerServices
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Cito.Tester.ContentModel
Imports Cito.Tester.Common
Imports Questify.Builder.Logic.Service.Factories

Namespace ContentModel
    Public Module TestPackageExtensions

        <Extension>
        Public Function GetTestPackage(testPackageEntity As TestPackageResourceEntity) As TestPackage
            Dim testPackageDefinition As TestPackage = Nothing
            If testPackageEntity.IsNew Then
                Dim identifier As String = String.Empty
                Dim title As String = String.Empty

                If testPackageEntity.ResourceData.BinData.Length > 0 Then
                    testPackageDefinition = TestPackageFactory.ReturnTestPackageModelFromByteArray(testPackageEntity.ResourceData.BinData)
                    identifier = testPackageEntity.Name
                    title = testPackageDefinition.Title
                Else
                    testPackageDefinition = New TestPackage
                    title = testPackageEntity.Name
                    identifier = ""
                End If

                testPackageDefinition.Title = title
                testPackageDefinition.Identifier = identifier
            Else
                Dim data As ResourceDataEntity = ResourceFactory.Instance.GetResourceData(testPackageEntity)
                testPackageDefinition = TestPackageFactory.ReturnTestPackageModelFromByteArray(data.BinData)
                testPackageEntity.ResourceData = data
            End If

            If Not testPackageDefinition.IncludedViews.Contains(GenericTestModelPlugin.PLUGIN_NAME) Then
                testPackageDefinition.IncludedViews.Add(GenericTestModelPlugin.PLUGIN_NAME)
            End If

            Return testPackageDefinition
        End Function

        <Extension>
        Public Sub SetTestPackage(testPackageResource As TestPackageResourceEntity, testPackage As TestPackage)
            If (Not testPackage.Identifier.Equals(testPackageResource.Name)) Then testPackage.Identifier = testPackageResource.Name
            If (Not testPackage.Title.Equals(testPackageResource.Title)) Then testPackage.Title = testPackageResource.Title
            If testPackageResource.ResourceData Is Nothing Then
                testPackageResource.ResourceData = New ResourceDataEntity
            End If
            testPackageResource.ResourceData.BinData = SerializeHelper.XmlSerializeToByteArray(testPackage)
        End Sub

    End Module
End Namespace

