
Option Strict On
Option Explicit On


Namespace My

    <Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute(), _
     Global.System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.7.0.0"), _
     Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)> _
    Partial Friend NotInheritable Class MySettings
        Inherits Global.System.Configuration.ApplicationSettingsBase

        Private Shared defaultInstance As MySettings = CType(Global.System.Configuration.ApplicationSettingsBase.Synchronized(New MySettings()), MySettings)

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

        <Global.System.Configuration.UserScopedSettingAttribute(), _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), _
         Global.System.Configuration.DefaultSettingValueAttribute("")> _
        Public Property Language() As String
            Get
                Return CType(Me("Language"), String)
            End Get
            Set
                Me("Language") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(), _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), _
         Global.System.Configuration.DefaultSettingValueAttribute("")> _
        Public Property ImportLastFilename() As String
            Get
                Return CType(Me("ImportLastFilename"), String)
            End Get
            Set
                Me("ImportLastFilename") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(), _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), _
         Global.System.Configuration.DefaultSettingValueAttribute("")> _
        Public Property CesTestPreviewerLocation() As String
            Get
                Return CType(Me("CesTestPreviewerLocation"), String)
            End Get
            Set
                Me("CesTestPreviewerLocation") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(), _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), _
         Global.System.Configuration.DefaultSettingValueAttribute("False")> _
        Public Property ItemEditorFullScreen() As Boolean
            Get
                Return CType(Me("ItemEditorFullScreen"), Boolean)
            End Get
            Set
                Me("ItemEditorFullScreen") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(), _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), _
         Global.System.Configuration.DefaultSettingValueAttribute("1000, 710")> _
        Public Property ItemEditorSize() As Global.System.Drawing.Size
            Get
                Return CType(Me("ItemEditorSize"), Global.System.Drawing.Size)
            End Get
            Set
                Me("ItemEditorSize") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(), _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), _
         Global.System.Configuration.DefaultSettingValueAttribute("0, 0")> _
        Public Property ItemEditorPosition() As Global.System.Drawing.Point
            Get
                Return CType(Me("ItemEditorPosition"), Global.System.Drawing.Point)
            End Get
            Set
                Me("ItemEditorPosition") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(), _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), _
         Global.System.Configuration.DefaultSettingValueAttribute("1000")> _
        Public Property ItemEditorWidth() As String
            Get
                Return CType(Me("ItemEditorWidth"), String)
            End Get
            Set
                Me("ItemEditorWidth") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(), _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), _
         Global.System.Configuration.DefaultSettingValueAttribute("710")> _
        Public Property ItemEditorHeight() As String
            Get
                Return CType(Me("ItemEditorHeight"), String)
            End Get
            Set
                Me("ItemEditorHeight") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(), _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), _
         Global.System.Configuration.DefaultSettingValueAttribute("")> _
        Public Property Base() As String
            Get
                Return CType(Me("Base"), String)
            End Get
            Set
                Me("Base") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(), _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), _
         Global.System.Configuration.DefaultSettingValueAttribute("")> _
        Public Property UserBankSettings() As String
            Get
                Return CType(Me("UserBankSettings"), String)
            End Get
            Set
                Me("UserBankSettings") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(), _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), _
         Global.System.Configuration.DefaultSettingValueAttribute("")> _
        Public Property GridColumnOrderSettings() As String
            Get
                Return CType(Me("GridColumnOrderSettings"), String)
            End Get
            Set
                Me("GridColumnOrderSettings") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(), _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), _
         Global.System.Configuration.DefaultSettingValueAttribute("")> _
        Public Property UserWizardSettings() As String
            Get
                Return CType(Me("UserWizardSettings"), String)
            End Get
            Set
                Me("UserWizardSettings") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(), _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), _
         Global.System.Configuration.DefaultSettingValueAttribute("")> _
        Public Property ItemEditorGroups() As String
            Get
                Return CType(Me("ItemEditorGroups"), String)
            End Get
            Set
                Me("ItemEditorGroups") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(), _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), _
         Global.System.Configuration.DefaultSettingValueAttribute("")> _
        Public Property Reports_WordReport() As String
            Get
                Return CType(Me("Reports_WordReport"), String)
            End Get
            Set
                Me("Reports_WordReport") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(), _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), _
         Global.System.Configuration.DefaultSettingValueAttribute("")> _
        Public Property Reports_ExcelReport() As String
            Get
                Return CType(Me("Reports_ExcelReport"), String)
            End Get
            Set
                Me("Reports_ExcelReport") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(), _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), _
         Global.System.Configuration.DefaultSettingValueAttribute("")> _
        Public Property Publication_PackageExportLocation() As String
            Get
                Return CType(Me("Publication_PackageExportLocation"), String)
            End Get
            Set
                Me("Publication_PackageExportLocation") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(), _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), _
         Global.System.Configuration.DefaultSettingValueAttribute("")> _
        Public Property Publication_TestServerTimeToLive() As String
            Get
                Return CType(Me("Publication_TestServerTimeToLive"), String)
            End Get
            Set
                Me("Publication_TestServerTimeToLive") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(), _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), _
         Global.System.Configuration.DefaultSettingValueAttribute("")> _
        Public Property Publication_TestServerPackageName() As String
            Get
                Return CType(Me("Publication_TestServerPackageName"), String)
            End Get
            Set
                Me("Publication_TestServerPackageName") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(), _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), _
         Global.System.Configuration.DefaultSettingValueAttribute("")> _
        Public Property Publication_WordExportLocation() As String
            Get
                Return CType(Me("Publication_WordExportLocation"), String)
            End Get
            Set
                Me("Publication_WordExportLocation") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(), _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), _
         Global.System.Configuration.DefaultSettingValueAttribute("")> _
        Public Property Publication_CesExportLocation() As String
            Get
                Return CType(Me("Publication_CesExportLocation"), String)
            End Get
            Set
                Me("Publication_CesExportLocation") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(), _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), _
         Global.System.Configuration.DefaultSettingValueAttribute("0")> _
        Public Property Publication_CesitemPreviewResolution() As Integer
            Get
                Return CType(Me("Publication_CesitemPreviewResolution"), Integer)
            End Get
            Set
                Me("Publication_CesitemPreviewResolution") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(), _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), _
         Global.System.Configuration.DefaultSettingValueAttribute("0")> _
        Public Property UI_itemPreviewResolution() As Integer
            Get
                Return CType(Me("UI_itemPreviewResolution"), Integer)
            End Get
            Set
                Me("UI_itemPreviewResolution") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(), _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public Property UI_formulaEditorFont() As Global.System.Drawing.Font
            Get
                Return CType(Me("UI_formulaEditorFont"), Global.System.Drawing.Font)
            End Get
            Set
                Me("UI_formulaEditorFont") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(), _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), _
         Global.System.Configuration.DefaultSettingValueAttribute("")> _
        Public Property UI_itemPreviewTarget() As String
            Get
                Return CType(Me("UI_itemPreviewTarget"), String)
            End Get
            Set
                Me("UI_itemPreviewTarget") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(), _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), _
         Global.System.Configuration.DefaultSettingValueAttribute("")> _
        Public Property UI_itemPreviewTheme() As String
            Get
                Return CType(Me("UI_itemPreviewTheme"), String)
            End Get
            Set
                Me("UI_itemPreviewTheme") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(), _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), _
         Global.System.Configuration.DefaultSettingValueAttribute("")> _
        Public Property UI_itemPreviewThemeConfig() As String
            Get
                Return CType(Me("UI_itemPreviewThemeConfig"), String)
            End Get
            Set
                Me("UI_itemPreviewThemeConfig") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(), _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), _
         Global.System.Configuration.DefaultSettingValueAttribute("0")> _
        Public Property TestBuilderClient_SelectedBankId() As Integer
            Get
                Return CType(Me("TestBuilderClient_SelectedBankId"), Integer)
            End Get
            Set
                Me("TestBuilderClient_SelectedBankId") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(), _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), _
         Global.System.Configuration.DefaultSettingValueAttribute("")> _
        Public Property TestBuilderClient_ExportLocation() As String
            Get
                Return CType(Me("TestBuilderClient_ExportLocation"), String)
            End Get
            Set
                Me("TestBuilderClient_ExportLocation") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(), _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), _
         Global.System.Configuration.DefaultSettingValueAttribute("")> _
        Public Property TestBuilderClient_ImportLocation() As String
            Get
                Return CType(Me("TestBuilderClient_ImportLocation"), String)
            End Get
            Set
                Me("TestBuilderClient_ImportLocation") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(), _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), _
         Global.System.Configuration.DefaultSettingValueAttribute("0")> _
        Public Property TestBuilderClient_ItemEditorWindowState() As Integer
            Get
                Return CType(Me("TestBuilderClient_ItemEditorWindowState"), Integer)
            End Get
            Set
                Me("TestBuilderClient_ItemEditorWindowState") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(), _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), _
         Global.System.Configuration.DefaultSettingValueAttribute("10, 10, 800, 600")> _
        Public Property TestBuilderClient_ItemEditorBounds() As Global.system.drawing.rectangle
            Get
                Return CType(Me("TestBuilderClient_ItemEditorBounds"), Global.system.drawing.rectangle)
            End Get
            Set
                Me("TestBuilderClient_ItemEditorBounds") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(), _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), _
         Global.System.Configuration.DefaultSettingValueAttribute("10, 10, 800, 600")> _
        Public Property TestBuilderClient_TestEditorBounds() As Global.system.drawing.rectangle
            Get
                Return CType(Me("TestBuilderClient_TestEditorBounds"), Global.system.drawing.rectangle)
            End Get
            Set
                Me("TestBuilderClient_TestEditorBounds") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(), _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), _
         Global.System.Configuration.DefaultSettingValueAttribute("")> _
        Public Property TestBuilderClient_PublishLocation() As String
            Get
                Return CType(Me("TestBuilderClient_PublishLocation"), String)
            End Get
            Set
                Me("TestBuilderClient_PublishLocation") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(), _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), _
         Global.System.Configuration.DefaultSettingValueAttribute("0")> _
        Public Property TestBuilderClient_TestPackageEditorWindowState() As Integer
            Get
                Return CType(Me("TestBuilderClient_TestPackageEditorWindowState"), Integer)
            End Get
            Set
                Me("TestBuilderClient_TestPackageEditorWindowState") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(), _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), _
         Global.System.Configuration.DefaultSettingValueAttribute("10, 10, 800, 600")> _
        Public Property TestBuilderClient_TestPackageEditorBounds() As Global.system.drawing.rectangle
            Get
                Return CType(Me("TestBuilderClient_TestPackageEditorBounds"), Global.system.drawing.rectangle)
            End Get
            Set
                Me("TestBuilderClient_TestPackageEditorBounds") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(), _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), _
         Global.System.Configuration.DefaultSettingValueAttribute("")> _
        Public Property TestBuilderClient_SelectedServer() As String
            Get
                Return CType(Me("TestBuilderClient_SelectedServer"), String)
            End Get
            Set
                Me("TestBuilderClient_SelectedServer") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(), _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), _
         Global.System.Configuration.DefaultSettingValueAttribute("")> _
        Public Property TestBuilderClient_SelectedTabKey() As String
            Get
                Return CType(Me("TestBuilderClient_SelectedTabKey"), String)
            End Get
            Set
                Me("TestBuilderClient_SelectedTabKey") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(), _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), _
         Global.System.Configuration.DefaultSettingValueAttribute("0")> _
        Public Property TestBuilderClient_TestEditorWindowState() As Integer
            Get
                Return CType(Me("TestBuilderClient_TestEditorWindowState"), Integer)
            End Get
            Set
                Me("TestBuilderClient_TestEditorWindowState") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(), _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public Property ItemEditorLeftColumnWidth() As Integer
            Get
                Return CType(Me("ItemEditorLeftColumnWidth"), Integer)
            End Get
            Set
                Me("ItemEditorLeftColumnWidth") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(), _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute()> _
        Public Property ItemEditorRightColumnWidth() As Integer
            Get
                Return CType(Me("ItemEditorRightColumnWidth"), Integer)
            End Get
            Set
                Me("ItemEditorRightColumnWidth") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(), _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), _
         Global.System.Configuration.DefaultSettingValueAttribute("False")> _
        Public Property IsQATSaveAsVisible() As Boolean
            Get
                Return CType(Me("IsQATSaveAsVisible"), Boolean)
            End Get
            Set
                Me("IsQATSaveAsVisible") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(), _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), _
         Global.System.Configuration.DefaultSettingValueAttribute("False")> _
        Public Property IsQATSaveAndCloseVisible() As Boolean
            Get
                Return CType(Me("IsQATSaveAndCloseVisible"), Boolean)
            End Get
            Set
                Me("IsQATSaveAndCloseVisible") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(), _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), _
         Global.System.Configuration.DefaultSettingValueAttribute("")> _
        Public Property Reports_MediaReferencesReport() As String
            Get
                Return CType(Me("Reports_MediaReferencesReport"), String)
            End Get
            Set
                Me("Reports_MediaReferencesReport") = value
            End Set
        End Property

        <Global.System.Configuration.UserScopedSettingAttribute(), _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), _
         Global.System.Configuration.DefaultSettingValueAttribute("")> _
        Public Property Reports_MediaReferencedByEntities() As String
            Get
                Return CType(Me("Reports_MediaReferencedByEntities"), String)
            End Get
            Set
                Me("Reports_MediaReferencedByEntities") = value
            End Set
        End Property
    End Class
End Namespace

Namespace My

    <Global.Microsoft.VisualBasic.HideModuleNameAttribute(), _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), _
     Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute()> _
    Friend Module MySettingsProperty

        <Global.System.ComponentModel.Design.HelpKeywordAttribute("My.Settings")> _
        Friend ReadOnly Property Settings() As Global.Questify.Builder.Configuration.My.MySettings
            Get
                Return Global.Questify.Builder.Configuration.My.MySettings.Default
            End Get
        End Property
    End Module
End Namespace
