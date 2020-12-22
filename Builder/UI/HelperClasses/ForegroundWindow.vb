Imports Cito.Tester.Common

Public Class ForegroundWindow
    Implements IWin32Window

    Private Shared _window As New ForegroundWindow()

    Public Shared ReadOnly Property Instance() As IWin32Window
        Get
            Return _window
        End Get
    End Property

    Public ReadOnly Property Handle() As System.IntPtr Implements System.Windows.Forms.IWin32Window.Handle
        Get
            Return WindowsNativeMethods.GetForegroundWindow()
        End Get
    End Property
End Class
