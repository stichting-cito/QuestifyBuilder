Public Class WizardSettings
    Public Property WizardName As String
    Public Property TabSettings As Dictionary(Of String, List(Of String))
    Public Property ControlSettings As Dictionary(Of String, String)


    Public Sub New()
        WizardName = String.Empty
        TabSettings = New Dictionary(Of String, List(Of String))
        ControlSettings = New Dictionary(Of String, String)
    End Sub

    Public Sub New(wizardName As String)
        Me.WizardName = wizardName
        TabSettings = New Dictionary(Of String, List(Of String))
        ControlSettings = New Dictionary(Of String, String)
    End Sub

    Public Function GetTabSettingsForTab(tab As String) As List(Of String)
        Dim settings = New List(Of String)

        If (TabSettings.ContainsKey(tab)) Then
            settings = TabSettings(tab)
        End If

        Return settings
    End Function

    Public Sub SetTabSettingsForTab(tab As String, settings As IList(Of String))
        If (TabSettings.ContainsKey(tab)) Then
            TabSettings(tab) = CType(settings, List(Of String))
        Else
            TabSettings.Add(tab, CType(settings, List(Of String)))
        End If
    End Sub

    Public Function GetSettingsForControl(controlName As String) As String
        Dim setting As String = String.Empty
        If (ControlSettings.ContainsKey(controlName)) Then
            setting = ControlSettings(controlName)
        End If
        Return setting
    End Function


    Public Sub AddSettingsForControl(controlName As String, value As String)
        If (ControlSettings.ContainsKey(controlName)) Then
            If String.IsNullOrEmpty(value) Then
                ControlSettings.Remove(controlName)
            Else
                ControlSettings(controlName) = value
            End If
        Else
            ControlSettings.Add(controlName, value)
        End If
    End Sub

End Class
