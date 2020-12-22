Imports System.ComponentModel
Imports System.Xml.Serialization
Imports Cito.Tester.Common


<Serializable, XmlRoot("CustomSettingsCollection")> _
Public Class SettingsCollection2
    Inherits BindingList(Of Settings2)




    Public Overloads Sub Add(item As Settings2)
        If item Is Nothing Then
            Throw New ArgumentNullException("item", "item is null.")
        End If
        If Me.ContainsSettings(item.Name) Then
            Throw New DuplicateNameException($"Settings with '{item.Name}' already exists in collection.")
        End If
        MyBase.Add(item)
    End Sub


    Public Overloads Sub Remove(item As Settings2)
        If item Is Nothing Then
            Throw New ArgumentNullException("item", "item is null.")
        End If
        If Not Me.ContainsSettings(item.Name) Then
            Throw New ContentModelException($"Settings with '{item.Name}' does not exist in this collection.")
        End If
        MyBase.Remove(item)
    End Sub

    Public Function ContainsSettings(name As String) As Boolean
        For Each settings As Settings2 In Me
            If settings.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase) Then
                Return True
            End If
        Next
        Return False
    End Function


    Public Function GetSettingsByName(name As String) As Settings2
        For Each settings As Settings2 In Me
            If settings.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase) Then
                Return settings
            End If
        Next
        Return Nothing
    End Function


End Class

