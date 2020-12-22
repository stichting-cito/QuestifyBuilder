
Option Strict On
Option Explicit On

Imports System

Namespace My.Resources

    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0"), _
Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), _
Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute(), _
Global.Microsoft.VisualBasic.HideModuleNameAttribute()> _
    Friend Module Resources

        Private resourceMan As Global.System.Resources.ResourceManager

        Private resourceCulture As Global.System.Globalization.CultureInfo

        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)> _
        Friend ReadOnly Property ResourceManager() As Global.System.Resources.ResourceManager
            Get
                If Object.ReferenceEquals(resourceMan, Nothing) Then
                    Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("Questify.Builder.Packaging.Resources", GetType(Resources).Assembly)
                    resourceMan = temp
                End If
                Return resourceMan
            End Get
        End Property

        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)> _
        Friend Property Culture() As Global.System.Globalization.CultureInfo
            Get
                Return resourceCulture
            End Get
            Set
                resourceCulture = value
            End Set
        End Property

        Friend ReadOnly Property Error_CitoTesterServerPackageManager_DownloadLengthInvalid() As String
            Get
                Return ResourceManager.GetString("Error_CitoTesterServerPackageManager_DownloadLengthInvalid", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_CitoTesterServerPackageManager_DownloadNotSame() As String
            Get
                Return ResourceManager.GetString("Error_CitoTesterServerPackageManager_DownloadNotSame", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_CitoTesterServerPackageManager_DownloadPackage() As String
            Get
                Return ResourceManager.GetString("Error_CitoTesterServerPackageManager_DownloadPackage", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_Manifest() As String
            Get
                Return ResourceManager.GetString("Error_Manifest", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_ManifestResourceHandler_ClearCache_CacheDirectoryError() As String
            Get
                Return ResourceManager.GetString("Error_ManifestResourceHandler_ClearCache_CacheDirectoryError", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_ManifestResourceManager_CannotGetResource() As String
            Get
                Return ResourceManager.GetString("Error_ManifestResourceManager_CannotGetResource", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_ManifestResourceManager_CopyMediaToCache_ErrorWhileWritingToCache() As String
            Get
                Return ResourceManager.GetString("Error_ManifestResourceManager_CopyMediaToCache_ErrorWhileWritingToCache", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_ManifestResourceManager_EmptyStream() As String
            Get
                Return ResourceManager.GetString("Error_ManifestResourceManager_EmptyStream", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_PackageManager_GetStream_UriNotRelative() As String
            Get
                Return ResourceManager.GetString("Error_PackageManager_GetStream_UriNotRelative", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_PackageManager_GetStreamRelativeUri_UriNotSet() As String
            Get
                Return ResourceManager.GetString("Error_PackageManager_GetStreamRelativeUri_UriNotSet", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_ResourceManifest_Load_StreamNotSet() As String
            Get
                Return ResourceManager.GetString("Error_ResourceManifest_Load_StreamNotSet", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_UriNotSet() As String
            Get
                Return ResourceManager.GetString("Error_UriNotSet", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_WhileImportingResource() As String
            Get
                Return ResourceManager.GetString("Error_WhileImportingResource", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_WhilePublishingResource() As String
            Get
                Return ResourceManager.GetString("Error_WhilePublishingResource", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ExportManager_UpdateManifest() As String
            Get
                Return ResourceManager.GetString("ExportManager_UpdateManifest", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property FileProtocolHandler_PutStream_FailedToWrite() As String
            Get
                Return ResourceManager.GetString("FileProtocolHandler_PutStream_FailedToWrite", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property GettingResource0() As String
            Get
                Return ResourceManager.GetString("GettingResource0", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ProcessingResource() As String
            Get
                Return ResourceManager.GetString("ProcessingResource", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ProcessingResource0() As String
            Get
                Return ResourceManager.GetString("ProcessingResource0", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ResourceManifest_ResourceContainerFileDoesNotExist() As String
            Get
                Return ResourceManager.GetString("ResourceManifest_ResourceContainerFileDoesNotExist", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ResourceManifest_ResourceContainerSchemeIsNotDefined() As String
            Get
                Return ResourceManager.GetString("ResourceManifest_ResourceContainerSchemeIsNotDefined", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ResourceManifest_ResourceFileNotFound() As String
            Get
                Return ResourceManager.GetString("ResourceManifest_ResourceFileNotFound", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Trace_ManifestResourceManager_ClearCache_CacheCleared() As String
            Get
                Return ResourceManager.GetString("Trace_ManifestResourceManager_ClearCache_CacheCleared", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Trace_ManifestResourceManager_ClearCache_CachedirNotFoundOrNoEntriesInCache() As String
            Get
                Return ResourceManager.GetString("Trace_ManifestResourceManager_ClearCache_CachedirNotFoundOrNoEntriesInCache", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Trace_ManifestResourceManager_CopyMediaToCache_AddedToCache() As String
            Get
                Return ResourceManager.GetString("Trace_ManifestResourceManager_CopyMediaToCache_AddedToCache", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Trace_ManifestResourceManager_CopyMediaToCache_CopyResources() As String
            Get
                Return ResourceManager.GetString("Trace_ManifestResourceManager_CopyMediaToCache_CopyResources", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Trace_ManifestResourceManager_GetFromCache_GetResourceStream() As String
            Get
                Return ResourceManager.GetString("Trace_ManifestResourceManager_GetFromCache_GetResourceStream", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Trace_ManifestResourceManager_GetFromCache_ProcessingStream() As String
            Get
                Return ResourceManager.GetString("Trace_ManifestResourceManager_GetFromCache_ProcessingStream", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Trace_ManifestResourceManager_GetResource_FoundInCache() As String
            Get
                Return ResourceManager.GetString("Trace_ManifestResourceManager_GetResource_FoundInCache", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Trace_ManifestResourceManager_GetResource_GetResource() As String
            Get
                Return ResourceManager.GetString("Trace_ManifestResourceManager_GetResource_GetResource", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Trace_ManifestResourceManager_GetResource_NotFoundInCache() As String
            Get
                Return ResourceManager.GetString("Trace_ManifestResourceManager_GetResource_NotFoundInCache", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Trace_ManifestResourceManager_GetResourceAndProcessStream_GetResourceStream() As String
            Get
                Return ResourceManager.GetString("Trace_ManifestResourceManager_GetResourceAndProcessStream_GetResourceStream", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Trace_ManifestResourceManager_GetResourceAndProcessStream_ProcessingStream() As String
            Get
                Return ResourceManager.GetString("Trace_ManifestResourceManager_GetResourceAndProcessStream_ProcessingStream", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Trace_ManifestResourceManager_GetTypedResource_GetResource() As String
            Get
                Return ResourceManager.GetString("Trace_ManifestResourceManager_GetTypedResource_GetResource", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property WaitingForPackageDownload() As String
            Get
                Return ResourceManager.GetString("WaitingForPackageDownload", resourceCulture)
            End Get
        End Property
    End Module
End Namespace
