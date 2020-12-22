


Imports System.IO

Imports Microsoft.Win32

Public NotInheritable Class PreviewManager


    Friend Shared Function AttachPreviewHandler(ByVal hWnd As IntPtr, ByVal fileName As String, ByVal viewRect As System.Drawing.Rectangle) As PreviewHandlerInstanceInfo
        Debug.WriteLine("AttachPreviewHandler", "PreviewManager")
        Dim pHandler As PreviewHandlerInstanceInfo = CreatePreviewHandlerInstance(fileName)

        If pHandler IsNot Nothing Then
            MessageFilter.Register()

            Dim isInitialized As Boolean = pHandler.IsInitialized

            If pHandler.SupportsStreamInit Then

                Dim stream As New COMStream(File.Open(fileName, FileMode.Open))
                isInitialized = pHandler.Initialize(stream)

            ElseIf pHandler.SupportsFileInit Then

                isInitialized = pHandler.Initialize(fileName)
            End If

            If isInitialized Then
                Dim r As New RECT(viewRect)
                With pHandler.PreviewHandlerInstance
                    .SetWindow(hWnd, r)
                    .SetRect(r)
                    .DoPreview()
                End With
            End If

            MessageFilter.Revoke()
        End If

        Return pHandler
    End Function

    Friend Shared Sub DetachPreviewHandler(ByVal info As PreviewHandlerInstanceInfo)
        Debug.WriteLine("DetachPreviewHandler", "PreviewManager")
        ReleaseInstance(info)
    End Sub



    Friend Shared Sub InvalidateAttachedPreview(ByVal info As PreviewHandlerInstanceInfo, ByVal viewRect As Rectangle)
        Debug.WriteLine("InvalidateAttachedPreview", "PreviewManager")
        Try
            If info IsNot Nothing AndAlso info.IsInitialized Then
                Dim r As New RECT(viewRect)
                MessageFilter.Register()
                info.PreviewHandlerInstance.SetRect(r)
                MessageFilter.Revoke()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Shared Function CreateInstance(ByVal CLSID As Guid) As PreviewHandlerInstanceInfo
        Debug.WriteLine("CreateInstance START", "PreviewManager")

        Dim instanceInfoToReturn As PreviewHandlerInstanceInfo


        Dim typeToInstantiate As Type = Type.GetTypeFromCLSID(CLSID, True)
        Dim instance As Object

        MessageFilter.Register()
        instance = Activator.CreateInstance(typeToInstantiate)
        MessageFilter.Revoke()

        instanceInfoToReturn = New PreviewHandlerInstanceInfo(CLSID, instance)

        Debug.WriteLine(String.Format("CreateInstance END {0}", IIf(instance Is Nothing, "NOTHING", "NOTNOTJHING")), "PreviewManager")
        Return instanceInfoToReturn
    End Function

    Private Shared Function CreatePreviewHandlerInstance(ByVal fileToPreview As String) As PreviewHandlerInstanceInfo
        Debug.WriteLine("CreatePreviewHandlerInstance", "PreviewManager")

        If String.IsNullOrEmpty(fileToPreview) Then
            Throw New ArgumentException("fileToPreview is nothing or empty.", "fileToPreview")
        End If

        Dim instanceInfoToReturn As PreviewHandlerInstanceInfo = Nothing
        Dim previewHandlerRegSubkey As String = "{8895b1c6-b41f-4c1c-a562-0d564250836f}"
        Dim fileExtention As String = IO.Path.GetExtension(fileToPreview)

        Using previewHandlerSubKey As RegistryKey = Registry.ClassesRoot.OpenSubKey(String.Format("{0}\ShellEx\{1}", fileExtention, previewHandlerRegSubkey))

            If previewHandlerSubKey IsNot Nothing Then
                Dim previewHandlerCLSID As Guid = New Guid(previewHandlerSubKey.GetValue("").ToString())
                Dim instanceInfo As PreviewHandlerInstanceInfo = CreateInstance(previewHandlerCLSID)

                If TypeOf instanceInfo.Instance Is IPreviewHandler Then
                    instanceInfoToReturn = instanceInfo
                Else
                    ReleaseInstance(instanceInfo)
                End If
            End If
        End Using

        Return instanceInfoToReturn
    End Function

    Private Shared Sub ReleaseInstance(ByVal info As PreviewHandlerInstanceInfo)
        Debug.WriteLine("ReleaseInstance", "PreviewManager")

        If info IsNot Nothing Then
            ReleaseInstance(info.Instance)
        End If
    End Sub

    Private Shared Sub ReleaseInstance(ByVal instance As Object)
        Try
            Debug.WriteLine("ReleaseInstance", "PreviewManager")

            If instance IsNot Nothing AndAlso TypeOf instance Is IPreviewHandler Then
                MessageFilter.Register()
                DirectCast(instance, IPreviewHandler).Unload()
                MessageFilter.Register()
            End If

            System.Runtime.InteropServices.Marshal.ReleaseComObject(instance)
        Catch ex As Exception
        End Try
    End Sub


End Class