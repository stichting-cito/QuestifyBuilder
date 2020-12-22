
Imports Cito.Tester.ContentModel
Imports System.Globalization
Imports System.Threading

<TestClass()> Public Class DesignerSettingCollectionTest
    Inherits SerializationTestBase

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub GetSettingValueByKeyShouldAlwaysReturnDefaultValueWhenNoGlobalizedValuesArePresent()
        Dim key As String = "label"
        Dim value As String = "Engels"
        Dim desingerSetting As New DesignerSetting With {.Key = key, .Value = value}
        Dim designerSettingCollection As New DesignerSettingCollection()
        designerSettingCollection.Add(desingerSetting)

        Dim settingValueNL As String = String.Empty
        Dim settingValueEN As String = String.Empty

        Try
            SetLanguage("NL")
            settingValueNL = designerSettingCollection.GetSettingValueByKey(key)
            SetLanguage("EN")
            settingValueEN = designerSettingCollection.GetSettingValueByKey(key)
        Catch ex As Exception
            Assert.Inconclusive("An error occured while trying to set the language.")
        End Try

        Assert.AreEqual(value, settingValueNL)
        Assert.AreEqual(value, settingValueEN)
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub GetSettingValueByKeyShouldReturnDutchValueWhenUILanguageIsSetToDutch()
        Dim keyDefault As String = "label"
        Dim valueDefault As String = "Engels"
        Dim keyNL As String = "label-NL"
        Dim valueNL As String = "Nederlands"

        Dim desingerSetting As New DesignerSetting
        Dim designerSettingCollection As New DesignerSettingCollection()
        desingerSetting = New DesignerSetting With {.Key = keyDefault, .Value = valueDefault}
        designerSettingCollection.Add(desingerSetting)
        desingerSetting = New DesignerSetting With {.Key = keyNL, .Value = valueNL}
        designerSettingCollection.Add(desingerSetting)

        Dim settingValueNL As String = String.Empty

        Try
            SetLanguage("NL")
            settingValueNL = designerSettingCollection.GetSettingValueByKey(keyDefault)
        Catch ex As Exception
            Assert.Inconclusive("An error occured while trying to set the language.")
        End Try

        Assert.AreEqual(valueNL, settingValueNL)
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub GetSettingValueByKeyShouldReturnEnglishValueWhenUILanguageIsSetToEnglish()
        Dim keyDefault As String = "label"
        Dim valueDefault As String = "Engels"
        Dim keyNL As String = "label-NL"
        Dim valueNL As String = "Nederlands"

        Dim desingerSetting As New DesignerSetting
        Dim designerSettingCollection As New DesignerSettingCollection()
        desingerSetting = New DesignerSetting With {.Key = keyDefault, .Value = valueDefault}
        designerSettingCollection.Add(desingerSetting)
        desingerSetting = New DesignerSetting With {.Key = keyNL, .Value = valueNL}
        designerSettingCollection.Add(desingerSetting)

        Dim settingValueEN As String = String.Empty

        Try
            SetLanguage("EN")
            settingValueEN = designerSettingCollection.GetSettingValueByKey(keyDefault)
        Catch ex As Exception
            Assert.Inconclusive("An error occured while trying to set the language.")
        End Try

        Assert.AreEqual(valueDefault, settingValueEN)
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub GetSettingValueByKeyShouldReturnDefaultValueWhenUILanguageIsSetToANotSupportedLanguage()
        Dim keyDefault As String = "label"
        Dim valueDefault As String = "Engels"
        Dim keyNL As String = "label-NL"
        Dim valueNL As String = "Nederlands"

        Dim desingerSetting As New DesignerSetting
        Dim designerSettingCollection As New DesignerSettingCollection()
        desingerSetting = New DesignerSetting With {.Key = keyDefault, .Value = valueDefault}
        designerSettingCollection.Add(desingerSetting)
        desingerSetting = New DesignerSetting With {.Key = keyNL, .Value = valueNL}
        designerSettingCollection.Add(desingerSetting)

        Dim settingValueDE As String = String.Empty

        Try
            SetLanguage("DE")
            settingValueDE = designerSettingCollection.GetSettingValueByKey(keyDefault)
        Catch ex As Exception
            Assert.Inconclusive("An error occured while trying to set the language.")
        End Try

        Assert.AreEqual(valueDefault, settingValueDE)
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub GetSettingListValueByKeyShouldAlwaysReturnDefaultListValueWhenNoGlobalizedListValuesArePresent()
        Dim key As String = "label"
        Dim value As String = "Engels"
        Dim listValue As New ListValue() With {.Key = "listValueKey", .DisplayValue = value}
        Dim list As New List(Of ListValue)()
        list.Add(listValue)

        Dim desingerSetting As New DesignerSetting With {.Key = key, .ListValue = list}
        Dim designerSettingCollection As New DesignerSettingCollection()
        designerSettingCollection.Add(desingerSetting)

        Dim settingListNL As List(Of ListValue) = Nothing
        Dim settingValueNL As String = String.Empty
        Dim settingListEN As List(Of ListValue) = Nothing
        Dim settingValueEN As String = String.Empty

        Try
            SetLanguage("NL")
            settingListNL = designerSettingCollection.GetListValuesByKey(key)
            settingValueNL = settingListNL(0).DisplayValue
            SetLanguage("EN")
            settingListEN = designerSettingCollection.GetListValuesByKey(key)
            settingValueEN = settingListEN(0).DisplayValue
        Catch ex As Exception
            Assert.Inconclusive("An error occured while trying to set the language.")
        End Try

        Assert.AreEqual(value, settingValueNL)
        Assert.AreEqual(value, settingValueEN)
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub GetSettingListValueByKeyShouldReturnDutchListValueWhenUILanguageIsSetToDutch()
        Dim keyDefault As String = "label"
        Dim valueDefault As String = "Engels"
        Dim listValueDefault As New ListValue() With {.Key = "listValueKey", .DisplayValue = valueDefault}
        Dim listDefault As New List(Of ListValue)()
        listDefault.Add(listValueDefault)

        Dim keyNL As String = "label-NL"
        Dim valueNL As String = "Nederlands"
        Dim listValueNL As New ListValue() With {.Key = "listValueKey", .DisplayValue = valueNL}
        Dim listNL As New List(Of ListValue)()
        listNL.Add(listValueNL)

        Dim desingerSetting As New DesignerSetting
        Dim designerSettingCollection As New DesignerSettingCollection()
        desingerSetting = New DesignerSetting With {.Key = keyDefault, .ListValue = listDefault}
        designerSettingCollection.Add(desingerSetting)
        desingerSetting = New DesignerSetting With {.Key = keyNL, .ListValue = listNL}
        designerSettingCollection.Add(desingerSetting)

        Dim settingListNL As List(Of ListValue) = Nothing
        Dim settingValueNL As String = String.Empty

        Try
            SetLanguage("NL")
            settingListNL = designerSettingCollection.GetListValuesByKey(keyDefault)
            settingValueNL = settingListNL(0).DisplayValue
        Catch ex As Exception
            Assert.Inconclusive("An error occured while trying to set the language.")
        End Try

        Assert.AreEqual(valueNL, settingValueNL)
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub GetSettingListValueByKeyShouldReturnEnglishListValueWhenUILanguageIsSetToEnglish()
        Dim keyDefault As String = "label"
        Dim valueDefault As String = "Engels"
        Dim listValueDefault As New ListValue() With {.Key = "listValueKey", .DisplayValue = valueDefault}
        Dim listDefault As New List(Of ListValue)()
        listDefault.Add(listValueDefault)

        Dim keyNL As String = "label-NL"
        Dim valueNL As String = "Nederlands"
        Dim listValueNL As New ListValue() With {.Key = "listValueKey", .DisplayValue = valueNL}
        Dim listNL As New List(Of ListValue)()
        listNL.Add(listValueNL)

        Dim desingerSetting As New DesignerSetting
        Dim designerSettingCollection As New DesignerSettingCollection()
        desingerSetting = New DesignerSetting With {.Key = keyDefault, .ListValue = listDefault}
        designerSettingCollection.Add(desingerSetting)
        desingerSetting = New DesignerSetting With {.Key = keyNL, .ListValue = listNL}
        designerSettingCollection.Add(desingerSetting)

        Dim settingListEN As List(Of ListValue) = Nothing
        Dim settingValueEN As String = String.Empty

        Try
            SetLanguage("EN")
            settingListEN = designerSettingCollection.GetListValuesByKey(keyDefault)
            settingValueEN = settingListEN(0).DisplayValue
        Catch ex As Exception
            Assert.Inconclusive("An error occured while trying to set the language.")
        End Try

        Assert.AreEqual(valueDefault, settingValueEN)
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub GetSettingListValueByKeyShouldReturnDefaultValueWhenUILanguageIsSetToANotSupportedLanguage()
        Dim keyDefault As String = "list"
        Dim valueDefault As String = "Engels"
        Dim listValueDefault As New ListValue() With {.Key = "listValueKey", .DisplayValue = valueDefault}
        Dim listDefault As New List(Of ListValue)()
        listDefault.Add(listValueDefault)

        Dim keyNL As String = "list-NL"
        Dim valueNL As String = "Nederlands"
        Dim listValueNL As New ListValue() With {.Key = "listValueKey", .DisplayValue = valueNL}
        Dim listNL As New List(Of ListValue)()
        listNL.Add(listValueNL)

        Dim desingerSetting As New DesignerSetting
        Dim designerSettingCollection As New DesignerSettingCollection()
        desingerSetting = New DesignerSetting With {.Key = keyDefault, .ListValue = listDefault}
        designerSettingCollection.Add(desingerSetting)
        desingerSetting = New DesignerSetting With {.Key = keyNL, .ListValue = listNL}
        designerSettingCollection.Add(desingerSetting)

        Dim settingListDE As List(Of ListValue) = Nothing
        Dim settingValueDE As String = String.Empty

        Try
            SetLanguage("DE")
            settingListDE = designerSettingCollection.GetListValuesByKey(keyDefault)
            settingValueDE = settingListDE(0).DisplayValue
        Catch ex As Exception
            Assert.Inconclusive("An error occured while trying to set the language.")
        End Try

        Assert.AreEqual(valueDefault, settingValueDE)
    End Sub

    Public Shared Sub SetLanguage(languageSetting As String)
        Dim cultureInfo As CultureInfo
        Try
            cultureInfo = New CultureInfo(languageSetting)
            Thread.CurrentThread.CurrentUICulture = cultureInfo
        Catch ex As Exception
            Throw New Exception("Multi Language Settings - Language Not Loaded")
        End Try
    End Sub

End Class