Imports System.Runtime.InteropServices

Public Class MessageFilter
    Implements _IOleMessageFilter

    Public Shared Sub Register()

        Dim newFilter As _IOleMessageFilter = New MessageFilter
        Dim oldFilter As _IOleMessageFilter = Nothing
        CoRegisterMessageFilter(newFilter, oldFilter)

    End Sub

    Public Shared Sub Revoke()

        Dim oldFilter As _IOleMessageFilter = Nothing
        CoRegisterMessageFilter(Nothing, oldFilter)

    End Sub

    Public Function HandleInComingCall(ByVal dwCallType As Integer, ByVal hTaskCaller As System.IntPtr, ByVal dwTickCount As Integer, ByVal lpInterfaceInfo As System.IntPtr) As Integer Implements _IOleMessageFilter.HandleInComingCall
        Return 0
    End Function

    Public Function MessagePending(ByVal hTaskCallee As System.IntPtr, ByVal dwTickCount As Integer, ByVal dwPendingType As Integer) As Integer Implements _IOleMessageFilter.MessagePending
        Return 2
    End Function

    Public Function RetryRejectedCall(ByVal hTaskCallee As System.IntPtr, ByVal dwTickCount As Integer, ByVal dwRejectType As Integer) As Integer Implements _IOleMessageFilter.RetryRejectedCall
        If dwRejectType = 2 Then
            Return 99
        End If
        Return -1
    End Function

    <DllImport("ole32.dll")> _
    <PreserveSig()> _
    Private Shared Function CoRegisterMessageFilter(ByVal lpMessageFilter As _IOleMessageFilter, ByRef lplpMessageFilter As _IOleMessageFilter) As Integer
    End Function

End Class

