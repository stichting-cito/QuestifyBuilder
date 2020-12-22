Imports System.Activities
Imports System.IO
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Cito.Tester.Common
Imports Questify.Builder.Logic.Service.Factories

Namespace CodeActivities
    Public NotInheritable Class SaveResourceToFile
        Inherits CodeActivity(Of String)

        Public Property ResourceName As InArgument(Of String)

        Public Property BankIdentifier As InArgument(Of Integer)

        Protected Overrides Function Execute(context As CodeActivityContext) As String
            Dim name As String = context.GetValue(Of String)(ResourceName)
            Dim bankId As Integer = context.GetValue(Of Integer)(BankIdentifier)

            Dim fileName As String = TempStorageHelper.GetTempFilename()

            Dim resource As ResourceEntity = ResourceFactory.Instance.GetResourceByNameWithOption(bankId, name, new ResourceRequestDTO())

            Dim resourceData As ResourceDataEntity = If((resource.ResourceData Is Nothing), ResourceFactory.Instance.GetResourceData(resource), resource.ResourceData)

            Debug.Assert(resourceData IsNot Nothing)

            File.WriteAllBytes(fileName, resourceData.BinData)

            Return fileName
        End Function
    End Class
End Namespace
