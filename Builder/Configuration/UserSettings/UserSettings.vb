Imports System.Configuration
Imports System.Drawing
Imports System.IO
Imports System.Linq
Imports Newtonsoft.Json

Public NotInheritable Class UserSettings

    Private Shared _bankSettings As New List(Of BankSettings)
    Private Shared _wizardSettings As New List(Of WizardSettings)
    Private Shared _isInitialised As Boolean = False
    Private Shared ReadOnly _serializer As New JsonSerializer()

    Shared Sub New()

    End Sub

    Private Shared Sub Initialize()
        If Not String.IsNullOrEmpty(My.Settings.UserBankSettings) Then
            Try
                Using sr As JsonTextReader = New JsonTextReader(New StringReader(My.Settings.UserBankSettings))
                    _bankSettings = _serializer.Deserialize(Of List(Of BankSettings))(sr)
                End Using
            Catch ex As Exception
                _bankSettings = New List(Of BankSettings)
            End Try
        End If

        If Not String.IsNullOrEmpty(My.Settings.UserWizardSettings) Then
            Try
                Using sr As JsonTextReader = New JsonTextReader(New StringReader(My.Settings.UserWizardSettings))
                    _wizardSettings = _serializer.Deserialize(Of List(Of WizardSettings))(sr)
                End Using
            Catch ex As Exception
                _wizardSettings = New List(Of WizardSettings)
            End Try
        End If

        _isInitialised = True
    End Sub

    Public Shared Property ImportLastFilename As String
        Get
            Return My.Settings.ImportLastFilename
        End Get
        Set
            My.Settings.ImportLastFilename = Value
        End Set
    End Property

    Public Shared Property ItemEditorFullScreen As Boolean
        Get
            Return My.Settings.ItemEditorFullScreen
        End Get
        Set
            My.Settings.ItemEditorFullScreen = Value
        End Set
    End Property

    Public Shared Property ItemEditorSize As Size
        Get
            Return My.Settings.ItemEditorSize
        End Get
        Set
            My.Settings.ItemEditorSize = Value
        End Set
    End Property

    Public Shared Property ItemEditorPosition As Point
        Get
            Return My.Settings.ItemEditorPosition
        End Get
        Set
            My.Settings.ItemEditorPosition = Value
        End Set
    End Property

    Public Shared Property ItemEditorLeftColumnWidth As Integer
        Get
            Return My.Settings.ItemEditorLeftColumnWidth
        End Get
        Set
            My.Settings.ItemEditorLeftColumnWidth = Value
        End Set
    End Property

    Public Shared Property ItemEditorRightColumnWidth As Integer
        Get
            Return My.Settings.ItemEditorRightColumnWidth
        End Get
        Set
            My.Settings.ItemEditorRightColumnWidth = Value
        End Set
    End Property

    Public Shared Property IsQATSaveAsVisible As Boolean
        Get
            Return My.Settings.IsQATSaveAsVisible
        End Get
        Set
            My.Settings.IsQATSaveAsVisible = Value
        End Set
    End Property

    Public Shared Property IsQATSaveAndCloseVisible As Boolean
        Get
            Return My.Settings.IsQATSaveAndCloseVisible
        End Get
        Set
            My.Settings.IsQATSaveAndCloseVisible = Value
        End Set
    End Property

    Public Shared ReadOnly Property BankUserSettings As List(Of BankSettings)
        Get
            If Not _isInitialised Then
                Initialize()
            End If

            Return _bankSettings
        End Get
    End Property

    Public Shared Sub StoreUserBankSettings(listOfBanks As List(Of Integer))
        My.Settings.UserBankSettings = JsonConvert.SerializeObject(_bankSettings.Where(Function(bs) listOfBanks.Contains(bs.BankId)))
    End Sub

    Public Shared Function GetUserBankSettingsForGrid(bankId As Integer, gridId As String) As GridSettings
        Dim bankSetting = BankUserSettings.FirstOrDefault(Function(b) b.BankId = bankId)
        If bankSetting Is Nothing Then
            bankSetting = New BankSettings(bankId)
            BankUserSettings.Add(bankSetting)
        End If
        Dim columnSettings = bankSetting.GridSettings.FirstOrDefault(Function(c) c.GridIdentifier = gridId)
        If columnSettings Is Nothing Then
            columnSettings = New GridSettings(gridId)
            bankSetting.GridSettings.Add(columnSettings)
        End If
        Return columnSettings
    End Function

    Public Shared ReadOnly Property UserWizardSettings As List(Of WizardSettings)
        Get
            If Not _isInitialised Then
                Initialize()
            End If

            Return _wizardSettings
        End Get
    End Property

    Public Shared Sub StoreUserWizardSettings()
        My.Settings.UserWizardSettings = JsonConvert.SerializeObject(_wizardSettings)
    End Sub

    Public Shared Function GetUserWizardSettingsForWizard(wizardName As String) As WizardSettings
        Dim wizardSetting = UserWizardSettings.FirstOrDefault(Function(w) w.WizardName = wizardName)
        If wizardSetting Is Nothing Then
            wizardSetting = New WizardSettings(wizardName)
            UserWizardSettings.Add(wizardSetting)
        End If

        Return wizardSetting
    End Function

End Class
