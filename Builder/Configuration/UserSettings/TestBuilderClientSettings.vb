Imports System.Drawing

Public NotInheritable Class TestBuilderClientSettings

    Public Shared Property SelectedBankId As Integer
        Get
            Return My.Settings.TestBuilderClient_SelectedBankId
        End Get
        Set
            My.Settings.TestBuilderClient_SelectedBankId = value
        End Set
    End Property

    Public Shared Property ExportLocation As String
        Get
            Return My.Settings.TestBuilderClient_ExportLocation
        End Get
        Set
            My.Settings.TestBuilderClient_ExportLocation = value
        End Set
    End Property

    Public Shared Property ImportLocation As String
        Get
            Return My.Settings.TestBuilderClient_ImportLocation
        End Get
        Set
            My.Settings.TestBuilderClient_ImportLocation = value
        End Set
    End Property

    Public Shared Property ItemEditorWindowState As Integer
        Get
            Return My.Settings.TestBuilderClient_ItemEditorWindowState
        End Get
        Set
            My.Settings.TestBuilderClient_ItemEditorWindowState = value
        End Set
    End Property

    Public Shared Property TestEditorWindowState As Integer
        Get
            Return My.Settings.TestBuilderClient_ItemEditorWindowState
        End Get
        Set
            My.Settings.TestBuilderClient_ItemEditorWindowState = value
        End Set
    End Property

    Public Shared Property ItemEditorBounds As Rectangle
        Get
            Return My.Settings.TestBuilderClient_ItemEditorBounds
        End Get
        Set
            My.Settings.TestBuilderClient_ItemEditorBounds = value
        End Set
    End Property

    Public Shared Property TestEditorBounds As Rectangle
        Get
            Return My.Settings.TestBuilderClient_TestEditorBounds
        End Get
        Set
            My.Settings.TestBuilderClient_TestEditorBounds = value
        End Set
    End Property

    Public Shared Property PublishLocation As String
        Get
            Return My.Settings.TestBuilderClient_PublishLocation
        End Get
        Set
            My.Settings.TestBuilderClient_PublishLocation = value
        End Set
    End Property

    Public Shared Property TestPackageEditorWindowState As Integer
        Get
            Return My.Settings.TestBuilderClient_TestPackageEditorWindowState
        End Get
        Set
            My.Settings.TestBuilderClient_TestPackageEditorWindowState = value
        End Set
    End Property

    Public Shared Property SelectedTabKey As String
        Get
            Return My.Settings.TestBuilderClient_SelectedTabKey
        End Get
        Set
            My.Settings.TestBuilderClient_SelectedTabKey = value
        End Set
    End Property


    Public Shared Property SelectedServer As String
        Get
            Return My.Settings.TestBuilderClient_SelectedServer
        End Get
        Set
            My.Settings.TestBuilderClient_SelectedServer = value
        End Set
    End Property

    Public Shared Property TestPackageEditorBounds As Rectangle
        Get
            Return My.Settings.TestBuilderClient_TestPackageEditorBounds
        End Get
        Set
            My.Settings.TestBuilderClient_TestPackageEditorBounds = value
        End Set
    End Property
End Class
