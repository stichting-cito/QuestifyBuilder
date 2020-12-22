Imports System.Globalization
Imports System.Threading

Public Class DesignerSettingCollection
    Inherits List(Of DesignerSetting)

    Public Function GetSettingValueByKey(key As String) As String
        Dim value As String = String.Empty
        Dim setting As DesignerSetting = Nothing
        Dim cultureInfo As CultureInfo = Thread.CurrentThread.CurrentUICulture

        If (cultureInfo IsNot Nothing) Then
            setting = GetDesignerSettingByKey($"{key}-{cultureInfo.Name}")
        End If

        If (setting Is Nothing) Then
            setting = GetDesignerSettingByKey(key)
        End If

        If setting IsNot Nothing Then
            value = setting.Value
        End If

        Return value
    End Function

    Public Function GetDesignerSettingByKey(key As String) As DesignerSetting
        For Each setting As DesignerSetting In Me
            If setting.Key.Equals(key, StringComparison.CurrentCultureIgnoreCase) Then
                Return setting
            End If
        Next

        Return Nothing
    End Function

    Public Function GetListValuesByKey(key As String) As List(Of ListValue)
        Dim value As List(Of ListValue) = Nothing
        Dim setting As DesignerSetting = Nothing
        Dim cultureInfo As CultureInfo = Thread.CurrentThread.CurrentUICulture

        If (cultureInfo IsNot Nothing) Then setting = GetDesignerSettingByKey($"{key}-{cultureInfo.Name}")
        If (setting Is Nothing) Then setting = GetDesignerSettingByKey(key)

        If setting IsNot Nothing Then
            value = setting.ListValue
        End If

        Return value
    End Function

End Class

