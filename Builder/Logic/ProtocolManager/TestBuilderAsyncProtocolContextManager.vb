
Imports System.Linq
Imports Cito.ItemViewer.AsyncPluggableProtocol
Imports Cito.Tester.Common


Public Class TestBuilderAsyncProtocolContextManager
    Private Shared _placeholderspacer As Byte()
    Public Const Internalhost As String = "internalhost"
    Public Const Placeholderspacer As String = "placeholderspacer"

    Private Shared _manager As ASyncResourceProtocolManager
    Private Shared _asyncPluggableProtocolExceptions As List(Of String)
    Private Shared _resourceManagers As New List(Of ResourceManagerBase)
    Private Shared ReadOnly _singleResourceFetchLock As New Object


    Public Shared Sub Initialize()
        _manager = New ASyncResourceProtocolManager("resource")
        AddHandler _manager.ResourceNeeded, AddressOf AsyncPluggableProtocol_ResourceNeeded
        _manager.Register()

        _asyncPluggableProtocolExceptions = New List(Of String)
        _resourceManagers = New List(Of ResourceManagerBase)
    End Sub

    Public Shared Sub Destroy()
        If _manager IsNot Nothing Then
            RemoveHandler _manager.ResourceNeeded, AddressOf AsyncPluggableProtocol_ResourceNeeded
            _manager.Unregister()
            _manager.Dispose()
        End If
        _asyncPluggableProtocolExceptions = Nothing
        _resourceManagers = Nothing
    End Sub

    Public Shared Function RegisterNewResourceManager(ByVal resourceManager As ResourceManagerBase) As Integer
        If Not _resourceManagers.Contains(resourceManager) Then
            _resourceManagers.Add(resourceManager)
        End If

        Return _resourceManagers.IndexOf(resourceManager) + 1
    End Function

    Public Shared Sub UnRegisterResourceManager(ByVal resourceManager As ResourceManagerBase)
        If _resourceManagers IsNot Nothing AndAlso _resourceManagers.Any() Then
            _resourceManagers.Remove(resourceManager)
        End If

        If _manager IsNot Nothing Then
            _manager.CleanUp()

            GC.Collect()
            GC.Collect()
        End If
    End Sub



    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")>
    Private Shared Sub AsyncPluggableProtocol_ResourceNeeded(ByVal sender As Object, ByVal e As Cito.ItemViewer.AsyncPluggableProtocol.ResourceNeededEventArgs)
        Try
            SyncLock _singleResourceFetchLock
                Dim resourceUri As New Uri(e.Url)
                If resourceUri.Host = "package" Then

                    Dim resourceName As String = System.Web.HttpUtility.UrlDecode(resourceUri.Segments(1))
                    Dim resource As BinaryResource
                    Dim contextIdentifier As Integer = resourceUri.Port - 1

                    If contextIdentifier < 0 Then
                        Throw New Exception($"Invalid context identifier value of {resourceUri.Port} set on resource '{e.Url}'")
                    ElseIf contextIdentifier >= _resourceManagers.Count Then
                        Throw New Exception($"Invalid context identifier set on resource '{e.Url}'. Value: {resourceUri.Port}. Registered resource managers: {_resourceManagers.Count}")
                    End If

                    Dim contextResourceManager As ResourceManagerBase = _resourceManagers(contextIdentifier)
                    Dim request = New ResourceRequestDTO With {.WithDependencies = True, .WithCustomProperties = True}
                    resource = contextResourceManager.GetResource(resourceName, AddressOf StreamConverters.ConvertStreamToByteArray, request)

                    If (resource Is Nothing) Then
                        Throw New Exception($"Resource {resourceName} was not loaded")
                    End If

                    Dim buffer = DirectCast(resource.ResourceObject, Byte())
                    If buffer.Length > 0 Then
                        e.ResourceStream.Write(buffer, 0, buffer.Length)
                    End If
                    Array.Resize(buffer, 0)
                ElseIf resourceUri.Host = Internalhost Then
                    If resourceUri.Segments(1) = Placeholderspacer Then
                        Dim spacer As Byte() = GetPlaceholderspacer()
                        e.ResourceStream.Write(spacer, 0, spacer.Length)
                    Else
                        Throw New ResourceException("Unknown host name")
                    End If
                Else
                    Throw New ResourceException("Unknown host name")
                End If
            End SyncLock
        Catch ex As Exception
            _asyncPluggableProtocolExceptions.Add(ex.Message)
        End Try
    End Sub

    Private Shared Function GetPlaceholderspacer() As Byte()
        If _placeholderspacer Is Nothing Then
            _placeholderspacer = My.Resources.PlaceHolderSpacer
        End If
        Return _placeholderspacer
    End Function


End Class
