
Option Strict On
Option Explicit On

Imports System.CodeDom.Compiler
Imports System.ComponentModel
Imports System.ComponentModel.Design
Imports System.Configuration
Imports System.Runtime.CompilerServices

Namespace My

    <CompilerGenerated(), _
     GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.7.0.0"), _
     EditorBrowsable(EditorBrowsableState.Advanced)> _
    Partial Friend NotInheritable Class MySettings
        Inherits ApplicationSettingsBase

        Private Shared defaultInstance As MySettings = CType(Synchronized(New MySettings()), MySettings)

#If _MyType = "WindowsForms" Then
    Private Shared addedHandler As Boolean

    Private Shared addedHandlerLockObject As New Object

    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)> _
    Private Shared Sub AutoSaveSettings(sender As Global.System.Object, e As Global.System.EventArgs)
        If My.Application.SaveMySettingsOnExit Then
            My.Settings.Save()
        End If
    End Sub
#End If

        Public Shared ReadOnly Property [Default]() As MySettings
            Get

#If _MyType = "WindowsForms" Then
               If Not addedHandler Then
                    SyncLock addedHandlerLockObject
                        If Not addedHandler Then
                            AddHandler My.Application.Shutdown, AddressOf AutoSaveSettings
                            addedHandler = True
                        End If
                    End SyncLock
                End If
#End If
                Return defaultInstance
            End Get
        End Property
    End Class
End Namespace

Namespace My

    <HideModuleName(), _
     DebuggerNonUserCode(), _
     CompilerGenerated()> _
    Friend Module MySettingsProperty

        <HelpKeyword("My.Settings")> _
        Friend ReadOnly Property Settings() As MySettings
            Get
                Return MySettings.Default
            End Get
        End Property
    End Module
End Namespace
