
Imports System.Web.Script.Serialization
Imports Questify.Builder.Configuration
Imports Questify.Builder.UI


<TestClass()>
Public Class UserSettingsTest
    <TestMethod()> <TestCategory("UserSettings")> 
    Public Sub UserBankSettingsAreSerializable()
        'Arrange
        Dim settings = UserSettings.GetUserBankSettingsForGrid(ActionCommand.Instance.CurrentBankId, "SomeGridKey")
        settings.ColumnSettings = New List(Of ColumnSettings)
        settings.ColumnSettings.Add(New ColumnSettings("column1"))
        settings.ColumnSettings.Add(New ColumnSettings("column2"))
        settings.ColumnSettings.Add(New ColumnSettings("column3"))
       
        'Act
        Dim userSettingsJsoString = New JavaScriptSerializer().Serialize(UserSettings.BankUserSettings)
        Dim settingsList = CType(New JavaScriptSerializer().Deserialize(userSettingsJsoString, GetType(List(Of Configuration.BankSettings))), List(Of Configuration.BankSettings))
        
        'Assert
        'Two possibilities: exception or IDataErrorInfo functionality.
        Assert.AreNotEqual(userSettingsJsoString, String.Empty)
        Assert.AreEqual(settingsList.Item(0).GridSettings(0).ColumnSettings.Count, 3)
    End Sub

    <TestMethod()> <TestCategory("UserSettings")> 
    Public Sub UserWizardSettingsAreSerializable()
        'Arrange
        Dim settings = UserSettings.GetUserWizardSettingsForWizard("SomeWizardName")
        settings.TabSettings.Add("SomeTab", New List(Of String) From {"Setting1", "Setting2"})

        'Act
        Dim userSettingsJsoString = New JavaScriptSerializer().Serialize(UserSettings.UserWizardSettings)
        Dim wizardSettingsList = CType(New JavaScriptSerializer().Deserialize(userSettingsJsoString, GetType(List(Of WizardSettings))), List(Of WizardSettings))

        'Assert
        Assert.AreNotEqual(userSettingsJsoString, String.Empty)
        Assert.AreEqual(wizardSettingsList.Count, 1)
        Assert.AreEqual(wizardSettingsList(0).TabSettings("SomeTab").Count, 2)
    End Sub
End Class