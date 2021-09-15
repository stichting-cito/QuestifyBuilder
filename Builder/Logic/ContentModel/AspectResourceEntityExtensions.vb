Imports System.IO
Imports System.Linq
Imports System.Runtime.CompilerServices
Imports Cito.Tester.Common

Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.Service.Factories

Namespace ContentModel
    Public Module AspectResourceEntityExtensions

        <Extension>
        Public Function GetResources(resource As AspectResourceEntity) As HashSet(Of String)
            Dim xHtml As New XHtmlParameter()
            Dim result As New HashSet(Of String)
            If (resource.ResourceData IsNot Nothing AndAlso resource.ResourceData.BinData IsNot Nothing) Then
                Dim aspect As Aspect = CType(Cito.Tester.Common.SerializeHelper.XmlDeserializeFromByteArray(resource.ResourceData.BinData, GetType(Aspect)), Aspect)
                xHtml.Value = aspect.Description

                Dim xHtmlResourceExtractor As New XHtmlResourceExtractor(xHtml)
                xHtmlResourceExtractor.ExtractResources().All(Function(s) result.Add(s))
            End If

            If resource.DependentResourceCollection IsNot Nothing Then
                For Each depResource As DependentResourceEntity In resource.DependentResourceCollection
                    If TypeOf (depResource.DependentResource) Is GenericResourceEntity AndAlso DirectCast(depResource.DependentResource, GenericResourceEntity).MediaType = "text/css" Then
                        result.Add(depResource.DependentResource.Name)
                    End If
                Next
            End If

            Return result
        End Function

        <Extension>
        Public Function GetAspect(resource As AspectResourceEntity) As Aspect
            Dim ret As Aspect = Nothing
            resource.EnsureResourceData
            If resource.ResourceData IsNot Nothing AndAlso resource.ResourceData.BinData.Length > 0 Then
                ret = DirectCast(Cito.Tester.Common.SerializeHelper.XmlDeserializeFromByteArray(resource.ResourceData.BinData, GetType(Aspect), True), Aspect)
            End If
            Return ret
        End Function

        <Extension>
        Public Sub SetAspect(resource As AspectResourceEntity, aspect As Aspect)
            If (Not aspect.Identifier.Equals(resource.Name)) Then aspect.Identifier = resource.Name
            If (Not aspect.Title.Equals(resource.Title)) Then aspect.Title = resource.Title
            If resource.ResourceData Is Nothing Then resource.ResourceData = New ResourceDataEntity
            Using stream = New MemoryStream()
                Cito.Tester.Common.SerializeHelper.XmlSerializeToStream(stream, aspect)
                resource.ResourceData.BinData = stream.ToArray()
                resource.ResourceData.FileExtension = ".xml"
            End Using
        End Sub

        <Extension>
        Public Sub UpdateDepencencies(aspectResource As AspectResourceEntity)
            Dim resources = aspectResource.GetResources()

            If aspectResource.DependentResourceCollection IsNot Nothing Then
                aspectResource.DependentResourceCollection.Where(Function(dOrg) Not resources.Any(Function(dActual) dOrg.DependentResource Is Nothing OrElse dActual = dOrg.DependentResource.Name)).All(Function(d)
                                                                                                                                                                                                             aspectResource.DependentResourceCollection.Remove(d)
                                                                                                                                                                                                         End Function)
                aspectResource.AddDependencies(resources.Where(Function(dActual) Not aspectResource.DependentResourceCollection.Any(Function(dOrg) dActual = dOrg.DependentResource.Name)))
            End If
        End Sub

        <Extension>
        Private Sub AddDependency(aspectResource As AspectResourceEntity, resourceName As String)
            AddDependencies(aspectResource, New List(Of String) From {resourceName})
        End Sub

        <Extension>
        Private Sub AddDependencies(aspectResource As AspectResourceEntity, resources As IEnumerable(Of String))
            For Each res As ResourceEntity In ResourceFactory.Instance.GetResourcesByNamesWithOption(aspectResource.BankId, resources.Where(Function(r) Not String.IsNullOrEmpty(r) AndAlso aspectResource.GetDependentResourceByName(r) Is Nothing).ToList(), New ResourceRequestDTO())
                Dim depResource As New DependentResourceEntity()
                depResource.Resource = aspectResource
                depResource.DependentResource = res
                aspectResource.DependentResourceCollection.Add(depResource)
            Next
        End Sub
    End Module

End Namespace
