Imports System.Runtime.InteropServices
Imports System.Runtime.InteropServices.ComTypes

<ComImport(), _
InterfaceType(ComInterfaceType.InterfaceIsIUnknown), _
Guid("b824b49d-22ac-4161-ac8a-9916e8fa3f7f")> _
Interface IInitializeWithStream


    Sub Initialize(ByVal pstream As IStream, ByVal grfMode As UInteger)


End Interface