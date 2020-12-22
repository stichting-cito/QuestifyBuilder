Imports System.Runtime.CompilerServices
Imports Cito.Tester.ContentModel.Datasources

Public Module ResourceRefExtensions
    <Extension>
    Public Function IsSeedingItem(resourceRef As ResourceRef) As Boolean
        Return resourceRef.Properties.ContainsKey("equallyDivided") AndAlso
                            Boolean.TryParse(resourceRef.Properties("equallyDivided"), True) AndAlso
                            Boolean.Parse(resourceRef.Properties("equallyDivided"))
    End Function
    <Extension>
    Public Function GetSeedingGroup(resourceRef As ResourceRef) As String
        Dim groupName = String.Empty
        If resourceRef.Properties.ContainsKey("seeding_group") Then
            groupName = resourceRef.Properties("seeding_group")
        End If
        Return groupName
    End Function
    <Extension>
    Public Function GetFirstItem(resourceRef As ResourceRef) As String
        Dim firstItem = String.Empty
        If resourceRef.Properties.ContainsKey("seeding_group_first_item") Then
            firstItem = resourceRef.Properties("seeding_group_first_item")
        End If
        Return firstItem
    End Function

End Module
