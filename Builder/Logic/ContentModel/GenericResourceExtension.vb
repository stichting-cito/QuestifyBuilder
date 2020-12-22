Imports System.Linq
Imports System.Runtime.CompilerServices
Imports System.Text
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports System.Drawing
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Logic.Service.Model.Entities

Namespace ContentModel

    Public Module GenericResourceExtension


        <Extension>
        Public Function CopyToNew(originalGenericResource As GenericResourceEntity, newName As String) As GenericResourceEntity
            Dim worker As New ResourceEntityCopier()
            Return worker.GetCopy(Of GenericResourceEntity)(originalGenericResource, newName)
        End Function

        <Extension>
        Public Function GetResources(resource As GenericResourceEntity) As HashSet(Of String)
            Dim xHtml As New XHtmlParameter()
            Dim result As New HashSet(Of String)
            Select Case (resource.MediaType.ToLower)
                Case "text/plain", "application/xhtml+xml", "text/html", "text/css"
                    If (resource.ResourceData IsNot Nothing AndAlso resource.ResourceData.BinData IsNot Nothing) Then
                        xHtml.Value = Encoding.UTF8.GetString(resource.ResourceData.BinData())
                        Dim xHtmlResourceExtractor As New XHtmlResourceExtractor(xHtml)
                        xHtmlResourceExtractor.ExtractResources().All(Function(s) result.Add(s))
                    End If
            End Select

            If resource.DependentResourceCollection IsNot Nothing Then
                For Each depResource As DependentResourceEntity In resource.DependentResourceCollection
                    If TypeOf (depResource.DependentResource) Is GenericResourceEntity AndAlso DirectCast(depResource.DependentResource, GenericResourceEntity).MediaType.ToLower = "text/css" Then
                        result.Add(depResource.DependentResource.Name)
                    End If
                Next
            End If

            Return result
        End Function

        <Extension>
        Public Function Height(genericResource As GenericResourceEntity) As Integer?
            Dim returnValue As Integer? = Nothing
            Dim h As Integer = 0
            Dim dimensions = genericResource.Dimensions.Split("x"c)
            If dimensions.Count = 2 AndAlso Integer.TryParse(dimensions(1), h) Then returnValue = h
            Return returnValue
        End Function

        <Extension>
        Public Function Width(genericResource As GenericResourceEntity) As Integer?
            Dim returnValue As Integer? = Nothing
            Dim w As Integer = 0
            Dim dimensions = genericResource.Dimensions.Split("x"c)
            If dimensions.Count = 2 AndAlso Integer.TryParse(dimensions(0), w) Then returnValue = w
            Return returnValue
        End Function

        <Extension>
        Public Function Height(genericResource As GenericResourceDto) As Integer?
            Dim returnValue As Integer? = Nothing
            Dim h As Integer = 0
            Dim dimensions = genericResource.Dimensions.Split("x"c)
            If dimensions.Count = 2 AndAlso Integer.TryParse(dimensions(1), h) Then returnValue = h
            Return returnValue
        End Function

        <Extension>
        Public Function Width(genericResource As GenericResourceDto) As Integer?
            Dim returnValue As Integer? = Nothing
            Dim w As Integer = 0
            Dim dimensions = genericResource.Dimensions.Split("x"c)
            If dimensions.Count = 2 AndAlso Integer.TryParse(dimensions(0), w) Then returnValue = w
            Return returnValue
        End Function

        <Extension>
        Public Sub UpdateDependencies(genericResource As GenericResourceEntity)
            If genericResource.DependentResourceCollection IsNot Nothing Then

                For Each depRes As DependentResourceEntity In genericResource.DependentResourceCollection
                    If depRes.DependentResource Is Nothing Then
                        depRes.DependentResource = ResourceFactory.Instance.GetResourceByIdWithOption(depRes.DependentResourceId, new ResourceRequestDTO())
                    End If
                Next

                Dim resources = genericResource.GetResources()

                Dim depsToRemove = genericResource.DependentResourceCollection.Where(Function(dOrg) Not resources.Any(Function(dActual) dOrg.DependentResource Is Nothing OrElse dActual = dOrg.DependentResource.Name)).ToList()
                genericResource.DependentResourceCollection.RemoveRange(depsToRemove)

                genericResource.AddDependencies(resources)
            End If
        End Sub

        <Extension>
        Private Sub AddDependencies(genericResource As GenericResourceEntity, resources As IEnumerable(Of String))
            Dim resourcesToCheck = resources.Where(Function(r) Not String.IsNullOrEmpty(r) AndAlso genericResource.GetDependentResourceByName(r) Is Nothing).ToList()
            If resourcesToCheck IsNot Nothing AndAlso resourcesToCheck.Any() Then
                For Each res As ResourceEntity In ResourceFactory.Instance.GetResourcesByNamesWithOption(genericResource.BankId, resourcesToCheck, new ResourceRequestDTO())
                    If Not genericResource.DependentResourceCollection.Any(Function(dr) dr.DependentResourceId = res.ResourceId AndAlso dr.ResourceId = genericResource.ResourceId) Then
                        Dim depResource As DependentResourceEntity = genericResource.DependentResourceCollection.AddNew()
                        depResource.ResourceId = genericResource.ResourceId

                        depResource.DependentResourceId = res.ResourceId
                    End If
                Next
            End If
        End Sub

        <Extension>
        Public Sub SetResource(resource As GenericResourceEntity, byteArray As Byte(), mimeType As String)
            If resource.ResourceData Is Nothing Then resource.ResourceData = New ResourceDataEntity
            resource.ResourceData.BinData = byteArray
            resource.Size = Convert.ToInt32(byteArray.Length / 1024)
            If resource.Size = 0 Then
                Dim realSize As Double = resource.ResourceData.BinData.Length / 1024
                If realSize > 0 Then resource.Size = 1
            End If
            Dim helper As New MediaDimensionsHelper()
            Dim size As Size = helper.GetDimensions(mimeType, byteArray)
            If Not size.IsEmpty Then
                resource.Dimensions = $"{size.Width} x {size.Height}"
            End If
            resource.MediaType = mimeType
        End Sub

        <Extension>
        Public Sub SetResource(resource As GenericResourceEntity, fileName As String)
            If IO.File.Exists(fileName) Then
                Try
                    Dim byteArray = FileHelper.MakeByteArrayFromFile(fileName)
                    Dim mime = FileHelper.GetMimeFromFile(fileName)
                    resource.SetResource(byteArray, mime)
                Catch ex As Exception
                End Try
            End If
        End Sub

    End Module

End Namespace

