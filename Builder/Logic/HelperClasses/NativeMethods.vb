Imports System.Runtime.InteropServices
Namespace HelperClasses
    Public Class NativeMethods

        <DllImport("user32.dll", SetLastError:=True)> _
        Public Shared Function SetWindowPos(ByVal hWnd As IntPtr, ByVal hWndInsertAfter As IntPtr, ByVal X As Integer, ByVal Y As Integer, ByVal cx As Integer, ByVal cy As Integer, ByVal uFlags As SetWindowPosFlags) As Boolean
        End Function

        <Flags> _
        Public Enum SetWindowPosFlags As UInteger
            SynchronousWindowPosition = &H4000
            DeferErase = &H2000
            DrawFrame = &H20
            FrameChanged = &H20
            HideWindow = &H80
            DoNotActivate = &H10
            DoNotCopyBits = &H100
            IgnoreMove = &H2
            DoNotChangeOwnerZOrder = &H200
            DoNotRedraw = &H8
            DoNotReposition = &H200
            DoNotSendChangingEvent = &H400
            IgnoreResize = &H1
            IgnoreZOrder = &H4
            ShowWindow = &H40
        End Enum
    End Class
End Namespace