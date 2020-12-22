Imports System.Drawing
Imports Newtonsoft.Json


Public Class QbSettingsParser

    Public Shared Function GetUserSettingLanguage() As String
        Return My.Settings.Language
    End Function

    Public Shared Sub SetSettings(json As String)

        Dim qbSettings = JsonConvert.DeserializeObject(Of QbSettings)(json, {New SizeJsonConverter, New PointJsonConverter, New RectangleJsonConverter})

        If qbSettings IsNot Nothing Then
            My.Settings.Language = If(qbSettings.Language, String.Empty)
            My.Settings.ImportLastFilename = If(qbSettings.ImportLastFilename, String.Empty)
            My.Settings.CesTestPreviewerLocation = If(qbSettings.CesTestPreviewerLocation, String.Empty)
            My.Settings.ItemEditorFullScreen = qbSettings.ItemEditorFullScreen
            My.Settings.ItemEditorSize = If(qbSettings.ItemEditorSize.IsEmpty, Size.Empty, qbSettings.ItemEditorSize)
            My.Settings.ItemEditorPosition = If(qbSettings.ItemEditorPosition.IsEmpty, Point.Empty, qbSettings.ItemEditorPosition)
            My.Settings.Base = If(qbSettings.Base, String.Empty)
            My.Settings.UserBankSettings = If(qbSettings.UserBankSettings, String.Empty)
            My.Settings.GridColumnOrderSettings = If(qbSettings.GridColumnOrderSettings, String.Empty)
            My.Settings.UserWizardSettings = If(qbSettings.UserWizardSettings, String.Empty)
            My.Settings.ItemEditorGroups = If(qbSettings.ItemEditorGroups, String.Empty)
            My.Settings.ItemEditorLeftColumnWidth = qbSettings.ItemEditorLeftColumnWidth
            My.Settings.ItemEditorRightColumnWidth = qbSettings.ItemEditorRightColumnWidth

            My.Settings.Reports_WordReport = If(qbSettings.Reports_WordReport, String.Empty)
            My.Settings.Reports_ExcelReport = If(qbSettings.Reports_ExcelReport, String.Empty)

            My.Settings.Publication_PackageExportLocation = If(qbSettings.Publication_PackageExportLocation, String.Empty)
            My.Settings.Publication_TestServerTimeToLive = If(qbSettings.Publication_TestServerTimeToLive, String.Empty)
            My.Settings.Publication_TestServerPackageName = If(qbSettings.Publication_TestServerPackageName, String.Empty)
            My.Settings.Publication_WordExportLocation = If(qbSettings.Publication_WordExportLocation, String.Empty)
            My.Settings.Publication_CesExportLocation = If(qbSettings.Publication_CesExportLocation, String.Empty)
            My.Settings.Publication_CesitemPreviewResolution = qbSettings.Publication_CesitemPreviewResolution

            My.Settings.UI_itemPreviewResolution = qbSettings.UI_itemPreviewResolution
            My.Settings.UI_formulaEditorFont = qbSettings.UI_formulaEditorFont
            My.Settings.UI_itemPreviewTarget = If(qbSettings.UI_itemPreviewTarget, String.Empty)
            My.Settings.UI_itemPreviewTheme = If(qbSettings.UI_itemPreviewTheme, String.Empty)
            My.Settings.UI_itemPreviewThemeConfig = If(qbSettings.UI_itemPreviewThemeConfig, String.Empty)

            My.Settings.TestBuilderClient_SelectedBankId = qbSettings.TestBuilderClient_SelectedBankId
            My.Settings.TestBuilderClient_ExportLocation = If(qbSettings.TestBuilderClient_ExportLocation, String.Empty)
            My.Settings.TestBuilderClient_ImportLocation = If(qbSettings.TestBuilderClient_ImportLocation, String.Empty)
            My.Settings.TestBuilderClient_ItemEditorWindowState = qbSettings.TestBuilderClient_ItemEditorWindowState
            My.Settings.TestBuilderClient_ItemEditorBounds = qbSettings.TestBuilderClient_ItemEditorBounds
            My.Settings.TestBuilderClient_TestEditorBounds = qbSettings.TestBuilderClient_TestEditorBounds
            My.Settings.TestBuilderClient_PublishLocation = qbSettings.TestBuilderClient_PublishLocation
            My.Settings.TestBuilderClient_TestPackageEditorWindowState = qbSettings.TestBuilderClient_TestPackageEditorWindowState
            My.Settings.TestBuilderClient_TestPackageEditorBounds = qbSettings.TestBuilderClient_TestPackageEditorBounds
            My.Settings.TestBuilderClient_SelectedServer = If(qbSettings.TestBuilderClient_SelectedServer, String.Empty)
            My.Settings.TestBuilderClient_SelectedTabKey = If(qbSettings.TestBuilderClient_SelectedTabKey, String.Empty)
            My.Settings.TestBuilderClient_TestEditorWindowState = qbSettings.TestBuilderClient_TestEditorWindowState

            My.Settings.IsQATSaveAsVisible = qbSettings.IsQATSaveAsVisible
            My.Settings.IsQATSaveAndCloseVisible = qbSettings.IsQATSaveAndCloseVisible

            My.Settings.Save()
        End If

    End Sub


    Public Shared Function GettingsSettingsString(listOfBanks As List(Of Integer)) As String
        Dim qbSettings As New QbSettings

        qbSettings.Language = My.Settings.Language
        qbSettings.ImportLastFilename = My.Settings.ImportLastFilename
        qbSettings.CesTestPreviewerLocation = My.Settings.CesTestPreviewerLocation
        qbSettings.ItemEditorFullScreen = My.Settings.ItemEditorFullScreen
        qbSettings.ItemEditorSize = My.Settings.ItemEditorSize
        qbSettings.ItemEditorPosition = My.Settings.ItemEditorPosition
        qbSettings.Base = My.Settings.Base

        UserSettings.StoreUserBankSettings(listOfBanks)
        qbSettings.UserBankSettings = My.Settings.UserBankSettings

        qbSettings.GridColumnOrderSettings = My.Settings.GridColumnOrderSettings
        qbSettings.UserWizardSettings = My.Settings.UserWizardSettings
        qbSettings.ItemEditorGroups = My.Settings.ItemEditorGroups
        qbSettings.Reports_WordReport = My.Settings.Reports_WordReport
        qbSettings.Reports_ExcelReport = My.Settings.Reports_ExcelReport
        qbSettings.ItemEditorLeftColumnWidth = My.Settings.ItemEditorLeftColumnWidth
        qbSettings.ItemEditorRightColumnWidth = My.Settings.ItemEditorRightColumnWidth

        qbSettings.Reports_WordReport = My.Settings.Reports_WordReport
        qbSettings.Reports_ExcelReport = My.Settings.Reports_ExcelReport

        qbSettings.Publication_PackageExportLocation = My.Settings.Publication_PackageExportLocation
        qbSettings.Publication_TestServerTimeToLive = My.Settings.Publication_TestServerTimeToLive
        qbSettings.Publication_TestServerPackageName = My.Settings.Publication_TestServerPackageName
        qbSettings.Publication_WordExportLocation = My.Settings.Publication_WordExportLocation
        qbSettings.Publication_CesExportLocation = My.Settings.Publication_CesExportLocation
        qbSettings.Publication_CesitemPreviewResolution = My.Settings.Publication_CesitemPreviewResolution

        qbSettings.UI_itemPreviewResolution = My.Settings.UI_itemPreviewResolution
        qbSettings.UI_formulaEditorFont = My.Settings.UI_formulaEditorFont
        qbSettings.UI_itemPreviewTarget = My.Settings.UI_itemPreviewTarget
        qbSettings.UI_itemPreviewTheme = My.Settings.UI_itemPreviewTheme
        qbSettings.UI_itemPreviewThemeConfig = My.Settings.UI_itemPreviewThemeConfig

        qbSettings.TestBuilderClient_SelectedBankId = My.Settings.TestBuilderClient_SelectedBankId
        qbSettings.TestBuilderClient_ExportLocation = My.Settings.TestBuilderClient_ExportLocation
        qbSettings.TestBuilderClient_ImportLocation = My.Settings.TestBuilderClient_ImportLocation
        qbSettings.TestBuilderClient_ItemEditorWindowState = My.Settings.TestBuilderClient_ItemEditorWindowState
        qbSettings.TestBuilderClient_ItemEditorBounds = My.Settings.TestBuilderClient_ItemEditorBounds
        qbSettings.TestBuilderClient_TestEditorBounds = My.Settings.TestBuilderClient_TestEditorBounds
        qbSettings.TestBuilderClient_PublishLocation = My.Settings.TestBuilderClient_PublishLocation
        qbSettings.TestBuilderClient_TestPackageEditorWindowState = My.Settings.TestBuilderClient_TestPackageEditorWindowState
        qbSettings.TestBuilderClient_TestPackageEditorBounds = My.Settings.TestBuilderClient_TestPackageEditorBounds
        qbSettings.TestBuilderClient_SelectedServer = My.Settings.TestBuilderClient_SelectedServer
        qbSettings.TestBuilderClient_SelectedTabKey = My.Settings.TestBuilderClient_SelectedTabKey
        qbSettings.TestBuilderClient_TestEditorWindowState = My.Settings.TestBuilderClient_TestEditorWindowState

        qbSettings.IsQATSaveAsVisible = My.Settings.IsQATSaveAsVisible
        qbSettings.IsQATSaveAndCloseVisible = My.Settings.IsQATSaveAndCloseVisible

        Return JsonConvert.SerializeObject(qbSettings, {New SizeJsonConverter, New PointJsonConverter, New RectangleJsonConverter})
    End Function

End Class

