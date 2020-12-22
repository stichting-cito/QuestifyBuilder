
Imports System.IO
Imports Cito.Tester.Common
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports SD.LLBLGen.Pro.ORMSupportClasses

Namespace FakeAppTemplate

    Public Class ExportReadyResourceManager
        Inherits SimpleFakedResourceManager

        Sub New(resourceCollection As EntityCollection)
            MyBase.New(resourceCollection)
        End Sub

        Public Overrides Function GetResource(ByVal name As String) As StreamResource
            For Each resource As ResourceEntity In ResourceCollection
                If resource.Name = name Then

                    Dim stream As MemoryStream = Nothing
                    If (resource.ResourceData IsNot Nothing) Then
                        stream = New MemoryStream(resource.ResourceData.BinData)
                    Else
                        stream = New MemoryStream(New Byte() {1})
                    End If

                    Dim entityCore = CType(resource, IEntityCore)
                    Dim ret = New StreamResource(resource.Name, 1, entityCore.LLBLGenProEntityName, False, stream, ToCollection(resource.DependentResourceCollection()), 0)
                    stream = Nothing
                    resource.AddMetaDataTo(ret)
                    Return ret
                End If
            Next

            Debug.Assert(False)

            Return Nothing
        End Function

        Public Overrides Function GetResourceEntry(ByVal name As String) As ResourceEntry
            If name Is Nothing Then Throw New ArgumentNullException("name")

            Dim resource = GetResource(name)
            If (resource IsNot Nothing) Then
                Try
                    Return New ResourceEntry(name, resource.Version, GetResourceUri(name).ToString(), resource.Type, False, resource.DependentResources, resource.OriginalName, resource.OriginalVersion, resource.State)
                Finally
                    resource.Dispose()
                End Try
            End If

            Return Nothing
        End Function

        Private Function GetResourceUri(ByVal name As String) As Uri
            Return New Uri($"db://{name.Replace(" ", "").Replace(".....", "_").Replace("....", "_").Replace("...", "_").Replace("..", "_")}")
        End Function

        Private Function ToCollection(entityCollection As EntityCollection(Of DependentResourceEntity)) As IEnumerable(Of DependentResource)
            Return From e In entityCollection Select New DependentResource(e.DependentResource.Name)
        End Function

    End Class
End NameSpace