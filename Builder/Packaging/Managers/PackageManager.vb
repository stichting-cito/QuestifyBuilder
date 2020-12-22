Imports System.Globalization
Imports System.IO
Imports System.IO.Compression
Imports System.Linq
Imports System.Security.Cryptography
Imports System.Text
Imports Cito.Tester.Common

Public Class PackageManager
    Implements IDisposable

    Private Shared _compressedFolderExtensionForPublication As String = ".package/"
    Private Shared _compressedFolderExtensionForExport As String = ".export/"
    Private Shared _compressedFolderContainerExtension As String = ".container/"
    Private Shared _packages As New Dictionary(Of String, ZipArchive)
    Private Shared _packageStreams As New Dictionary(Of String, Stream)
    Private _packageKey As String = ""

    Public Shared Sub RemovePackageFromCache(packageUri As Uri)
        Dim packageKey As String = GetPackageUri(packageUri).ToString

        If _packages IsNot Nothing Then
            If _packageStreams.ContainsKey(packageKey) Then
                _packageStreams.Item(packageKey).Dispose()
                _packageStreams.Remove(packageKey)
                _packages.Remove(packageKey)
            End If
        End If

    End Sub

    Public Shared Sub ReleaseSharedResources()
        If _packages IsNot Nothing Then _packages.Clear()

        If _packageStreams IsNot Nothing Then
            For Each _packageStream As Stream In _packageStreams.Values
                _packageStream.Dispose()
            Next
        End If
        _packageStreams.Clear()
    End Sub

    Public ReadOnly Property Packagekey() As String
        Get
            Return _packageKey
        End Get
    End Property

    Public Shared Function GetPackageUri(uri As Uri) As Uri
        If uri Is Nothing Then
            Throw New ResourceManagerException(My.Resources.Error_UriNotSet)
        End If

        Dim packageUriSB As New StringBuilder
        Dim isValidPackageUri As Boolean = False

        packageUriSB.Append(uri.Scheme)
        packageUriSB.Append(uri.SchemeDelimiter)
        packageUriSB.Append(uri.Host)

        For Each segment As String In uri.Segments
            If segment.EndsWith(_compressedFolderExtensionForExport, True, My.Application.Culture) OrElse segment.EndsWith(_compressedFolderExtensionForPublication, True, My.Application.Culture) OrElse segment.EndsWith(_compressedFolderContainerExtension, True, My.Application.Culture) Then
                packageUriSB.Append(segment.Substring(0, segment.Length - 1))
                isValidPackageUri = True
                Exit For
            Else
                packageUriSB.Append(segment)
            End If
        Next

        If isValidPackageUri Then
            Return New Uri(packageUriSB.ToString)
        Else
            Return Nothing
        End If
    End Function

    Public Shared Function GetStreamRelativeUri(uri As Uri) As Uri
        If uri Is Nothing Then
            Throw New ResourceManagerException(My.Resources.Error_PackageManager_GetStreamRelativeUri_UriNotSet)
        End If

        Dim packageUri As Uri = GetPackageUri(uri)

        If packageUri IsNot Nothing Then
            Return New Uri(uri.ToString.Substring(packageUri.ToString.Length), UriKind.Relative)
        Else
            Return Nothing
        End If
    End Function

    Public Function GetStream(relativeUri As Uri) As StreamResource
        If relativeUri Is Nothing Then
            Throw New ResourceManagerException(My.Resources.Error_UriNotSet)
        End If

        If relativeUri.IsAbsoluteUri Then
            Throw New ResourceManagerException(My.Resources.Error_PackageManager_GetStream_UriNotRelative)
        End If

        Dim package As ZipArchive = _packages(_packageKey)
        Dim file As ZipArchiveEntry

        Try
            Dim name As String = relativeUri.OriginalString.TrimStart(CChar("/"))
            file = package.Entries.First(Function(e) e.FullName.Equals(name, StringComparison.InvariantCultureIgnoreCase))
            Dim length = file.Length
            Return New StreamResource(file.Open(), length)
        Catch ex As Exception
            Throw New FileNotFoundException(ex.Message, ex)
        End Try

    End Function

    Public Sub PutStream(relativeUri As Uri, streamResource As StreamResource)
        If relativeUri Is Nothing Then
            Throw New ResourceManagerException(My.Resources.Error_UriNotSet)
        End If

        If relativeUri.IsAbsoluteUri Then
            Throw New ResourceManagerException(My.Resources.Error_PackageManager_GetStream_UriNotRelative)
        End If

        Dim file As ZipArchiveEntry
        Dim package As ZipArchive = _packages(_packageKey)
        file = package.CreateEntry(relativeUri.OriginalString.TrimStart(CChar("/")))

        Using destinationStream As Stream = file.Open()
            streamResource.ResourceObject.CopyTo(destinationStream)
        End Using
    End Sub

    Private Sub New()
    End Sub

    Public Sub New(packageUri As Uri, createNew As Boolean, Optional ByVal isReadOnly As Boolean = False)
        MyBase.New()

        If packageUri Is Nothing Then
            Throw New ResourceManagerException(My.Resources.Error_UriNotSet)
        End If

        _packageKey = packageUri.ToString

        If Not _packages.ContainsKey(_packageKey) Then
            Dim protocolHandler As IResourceReader = ProtocolHandlerFactory.GetReaderHandler(packageUri.Scheme)

            Dim package As ZipArchive
            Dim resource As StreamResource
            Dim packageStream As Stream

            If isReadOnly Then
                resource = protocolHandler.GetReadonlyStream(packageUri)
            Else
                resource = protocolHandler.GetStream(packageUri)
            End If

            If resource Is Nothing Then
                If createNew Then
                    If packageUri.Scheme.ToLower(CultureInfo.CurrentCulture) <> "file" Then
                        Throw New NotSupportedException("Creation of packages only supported on local filesystem")
                    End If

                    If (File.Exists(packageUri.LocalPath)) Then
                        File.Delete(packageUri.LocalPath)
                    End If
                    packageStream = New FileStream(packageUri.LocalPath, FileMode.CreateNew)
                Else
                    Throw New ResourceException("Package does not exist!")
                End If
            Else
                packageStream = resource.ResourceObject
            End If

            If packageStream.CanWrite Then
                package = New ZipArchive(packageStream, If(createNew, ZipArchiveMode.Create, ZipArchiveMode.Update), True)
            Else
                package = New ZipArchive(packageStream, ZipArchiveMode.Read, True)
            End If

            _packages.Add(_packageKey, package)
            _packageStreams.Add(_packageKey, packageStream)
        End If
    End Sub

    Public Sub ClosePackage()
        If _packages.ContainsKey(_packageKey) Then
            Dim package As ZipArchive = _packages(_packageKey)
            package.Dispose()
            _packages.Remove(_packageKey)
        End If

        If _packageStreams.ContainsKey(_packageKey) Then
            Dim packageStream As Stream = _packageStreams(_packageKey)
            packageStream.Dispose()
            _packageStreams.Remove(_packageKey)
        End If
    End Sub

    Public Shared Sub WriteResourceStreamToFile(stream As Stream, name As String)
        Dim file As FileStream = IO.File.Create("c:\" + name + ".txt")

        stream.CopyTo(file)
        file.Flush()
        file.Close()
    End Sub

    Private disposedValue As Boolean

    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                Me.ClosePackage()
                ReleaseSharedResources()
            End If
        End If
        Me.disposedValue = True
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
    End Sub

    Friend Shared Sub ValidateResource(baseUri As Uri, resourceUri As Uri)
        Dim absoluteResourceUri As Uri = New Uri(baseUri, resourceUri)

        If absoluteResourceUri.IsFile Then
            baseUri = GetPackageUri(absoluteResourceUri)

            If baseUri Is Nothing Then
                If Not File.Exists(Uri.UnescapeDataString(absoluteResourceUri.AbsolutePath)) Then
                    Throw New TesterException(String.Format(My.Resources.ResourceManifest_ResourceContainerFileDoesNotExist, baseUri.AbsolutePath))
                End If
            Else
                Dim packageArchive As ZipArchive

                If Not _packages.ContainsKey(baseUri.ToString) Then
                    Dim packageManager As PackageManager = New PackageManager(baseUri, False, True)
                End If

                packageArchive = _packages(baseUri.ToString)

                Dim path = Uri.UnescapeDataString(absoluteResourceUri.AbsolutePath.Replace(baseUri.AbsolutePath, "").TrimStart(CChar("/")))
                If Not packageArchive.Entries.Any(Function(e) e.FullName.Equals(path, StringComparison.InvariantCultureIgnoreCase)) Then
                    Throw New TesterException(String.Format(My.Resources.ResourceManifest_ResourceContainerFileDoesNotExist, absoluteResourceUri))
                End If
            End If
        End If
    End Sub

End Class
