Imports System.Runtime.InteropServices

<ComImport(), _
InterfaceType(ComInterfaceType.InterfaceIsIUnknown), _
Guid("b7d14566-0509-4cce-a71f-0a554233bd9b")> _
Interface IInitializeWithFile


    Sub Initialize(<MarshalAs(UnmanagedType.LPWStr)> ByVal pszFilePath As String, ByVal grfMode As UInteger)


End Interface