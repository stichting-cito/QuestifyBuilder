
Imports Cito.Tester.ContentModel
Imports System.Globalization
Imports System.Threading

<TestClass()> Public Class DesignerSettingCollectionTest
    Inherits SerializationTestBase

    <TestMethod()> <TestCategory("ContentModel")> 
    Public Sub GetSettingValueByKeyShouldAlwaysReturnDefaultValueWhenNoGlobalizedValuesArePresent()
        'Arrange
        Dim key As String = "label"
        Dim value As String = "Engels"
        Dim desingerSetting As New DesignerSetting With {.Key = key, .Value = value}
        Dim designerSettingCollection As New DesignerSettingCollection()
        designerSettingCollection.Add(desingerSetting)

        Dim settingValueNL As String = String.Empty
        Dim settingValueEN As String = String.Empty

        'Act
        Try
            'Set the culture to NL
            SetLanguage("NL")
            'Get the NL value.
            settingValueNL = designerSettingCollection.GetSettingValueByKey(key)
            'Set the culture to EN
            SetLanguage("EN")
            'Get the EN value.
            settingValueEN = designerSettingCollection.GetSettingValueByKey(key)
        Catch ex As Exception
            Assert.Inconclusive("An error occured while trying to set the language.")
        End Try

        'Assert
        'Check the NL value.
        Assert.AreEqual(value, settingValueNL)
        'Check the EN value.
        Assert.AreEqual(value, settingValueEN)
    End Sub

    <TestMethod()> <TestCategory("ContentModel")> 
    Public Sub GetSettingValueByKeyShouldReturnDutchValueWhenUILanguageIsSetToDutch()
        'Arrange
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

        'Act
        Try
            'Set the culture to NL
            SetLanguage("NL")
            'Get the NL value.
            settingValueNL = designerSettingCollection.GetSettingValueByKey(keyDefault)
        Catch ex As Exception
            Assert.Inconclusive("An error occured while trying to set the language.")
        End Try

        'Assert
        'Check the NL value.
        Assert.AreEqual(valueNL, settingValueNL)
    End Sub

    <TestMethod()> <TestCategory("ContentModel")> 
    Public Sub GetSettingValueByKeyShouldReturnEnglishValueWhenUILanguageIsSetToEnglish()
        'Arrange
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

        'Act
        Try
            'Set the culture to EN
            SetLanguage("EN")
            'Get the EN value.
            settingValueEN = designerSettingCollection.GetSettingValueByKey(keyDefault)
        Catch ex As Exception
            Assert.Inconclusive("An error occured while trying to set the language.")
        End Try

        'Assert
        'Check the EN value.
        Assert.AreEqual(valueDefault, settingValueEN)
    End Sub

    <TestMethod()> <TestCategory("ContentModel")> 
    Public Sub GetSettingValueByKeyShouldReturnDefaultValueWhenUILanguageIsSetToANotSupportedLanguage()
        'Arrange
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

        'Act
        Try
            'Set the culture to DE
            SetLanguage("DE")
            'Get the DE value.
            settingValueDE = designerSettingCollection.GetSettingValueByKey(keyDefault)
        Catch ex As Exception
            Assert.Inconclusive("An error occured while trying to set the language.")
        End Try

        'Assert
        'Check the EN value.
        Assert.AreEqual(valueDefault, settingValueDE)
    End Sub

    <TestMethod()> <TestCategory("ContentModel")> 
    Public Sub GetSettingListValueByKeyShouldAlwaysReturnDefaultListValueWhenNoGlobalizedListValuesArePresent()
        'Arrange
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

        'Act
        Try
            'Set the culture to NL
            SetLanguage("NL")
            'Get the NL list.
            settingListNL = designerSettingCollection.GetListValuesByKey(key)
            'Get the NL value.
            settingValueNL = settingListNL(0).DisplayValue
            'Set the culture to EN
            SetLanguage("EN")
            'Get the EN list.
            settingListEN = designerSettingCollection.GetListValuesByKey(key)
            'Get the EN value.
            settingValueEN = settingListEN(0).DisplayValue
        Catch ex As Exception
            Assert.Inconclusive("An error occured while trying to set the language.")
        End Try

        'Assert
        'Check the NL value.
        Assert.AreEqual(value, settingValueNL)
        'Check the EN value.
        Assert.AreEqual(value, settingValueEN)
    End Sub

    <TestMethod()> <TestCategory("ContentModel")> 
    Public Sub GetSettingListValueByKeyShouldReturnDutchListValueWhenUILanguageIsSetToDutch()
        'Arrange
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

        'Act
        Try
            'Set the culture to NL
            SetLanguage("NL")
            'Get the NL list.
            settingListNL = designerSettingCollection.GetListValuesByKey(keyDefault)
            'Get the NL value.
            settingValueNL = settingListNL(0).DisplayValue
        Catch ex As Exception
            Assert.Inconclusive("An error occured while trying to set the language.")
        End Try

        'Assert
        'Check the NL value.
        Assert.AreEqual(valueNL, settingValueNL)
    End Sub

    <TestMethod()> <TestCategory("ContentModel")> 
    Public Sub GetSettingListValueByKeyShouldReturnEnglishListValueWhenUILanguageIsSetToEnglish()
        'Arrange
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

        'Act
        Try
            'Set the culture to EN
            SetLanguage("EN")
            'Get the EN list.
            settingListEN = designerSettingCollection.GetListValuesByKey(keyDefault)
            'Get the EN value.
            settingValueEN = settingListEN(0).DisplayValue
        Catch ex As Exception
            Assert.Inconclusive("An error occured while trying to set the language.")
        End Try

        'Assert
        'Check the EN value.
        Assert.AreEqual(valueDefault, settingValueEN)
    End Sub

    <TestMethod()> <TestCategory("ContentModel")> 
    Public Sub GetSettingListValueByKeyShouldReturnDefaultValueWhenUILanguageIsSetToANotSupportedLanguage()
        'Arrange
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

        'Act
        Try
            'Set the culture to DE
            SetLanguage("DE")
            'Get the DE list.
            settingListDE = designerSettingCollection.GetListValuesByKey(keyDefault)
            'Get the DE value.
            settingValueDE = settingListDE(0).DisplayValue
        Catch ex As Exception
            Assert.Inconclusive("An error occured while trying to set the language.")
        End Try

        'Assert
        'Check the EN value.
        Assert.AreEqual(valueDefault, settingValueDE)
    End Sub

    '''<summary>
    '''Sets the language for the current thread.
    '''</summary>
    '''<param name="languageSetting">The language to set.</param>
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