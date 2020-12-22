


Imports System.Runtime.InteropServices

<ComImport()> _
<InterfaceType(ComInterfaceType.InterfaceIsIUnknown)> _
<Guid("8895b1c6-b41f-4c1c-a562-0d564250836f")> _
Interface IPreviewHandler


    Sub SetWindow(ByVal hwnd As IntPtr, ByRef rect As RECT)

    Sub SetRect(ByRef rect As RECT)

    Sub DoPreview()

    Sub Unload()

    Sub SetFocus()

    Sub QueryFocus(ByRef phwnd As IntPtr)

    <PreserveSig()> _
    Function TranslateAccelerator(ByRef pmsg As Integer) As UInteger


End Interface