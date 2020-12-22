Imports System.Collections.ObjectModel
Imports System.Text
Imports Cito.Tester.Common

Public Class KeyFindingCollectionBase(Of T As KeyFinding)
    Inherits Collection(Of T)


    Private _keys As Dictionary(Of String, T)



    Public Overloads Property Item(id As String) As T
        Get
            Return _keys(id)
        End Get
        Set
            Me.Remove(value)
            Me.Add(value)
        End Set
    End Property



    Public Overloads Sub Add(finding As T)
        If Not Contains(finding.Id) Then
            MyBase.Add(finding)
            _keys.Add(finding.Id, finding)
        End If
    End Sub

    Public Overloads Function Contains(id As String) As Boolean
        Return _keys.ContainsKey(id)
    End Function

    Public Function FindById(id As String) As T
        If (id Is Nothing) Then Return Nothing
        Dim result As T = Nothing
        _keys.TryGetValue(id, result)
        Return result
    End Function

    Public Overloads Sub Clear()
        MyBase.Clear()
        _keys.Clear()
    End Sub

    Protected Overrides Sub RemoveItem(index As Integer)
        _keys.Remove(Items(index).Id)
        MyBase.RemoveItem(index)
    End Sub


    Public Sub Sort(comparison As Comparison(Of T))
        DirectCast(Items, List(Of T)).Sort(comparison)
    End Sub

    Public Overloads Sub Remove(id As String)
        Dim finding As T
        finding = _keys(id)

        If finding IsNot Nothing Then
            _keys.Remove(finding.Id)
            MyBase.Remove(finding)
        End If
    End Sub


    Public Overrides Function ToString() As String

        Dim returnValueStringBuilder As New StringBuilder
        For Each keyFinding As T In Me
            If returnValueStringBuilder.Length > 0 Then returnValueStringBuilder.Append("|")
            returnValueStringBuilder.Append(keyFinding.ToString)
        Next
        Return returnValueStringBuilder.ToString

    End Function

    Public Function ValidateSolution(interactionInfo As InteractionControllerInfoCollection) As Boolean
        Dim controllerIds As New List(Of String)
        Dim keyIds As New List(Of String)

        For Each e As InteractionControllerInfo In interactionInfo
            controllerIds.Add(e.Id)
        Next

        For Each e As T In Me
            If e.Method <> EnumScoringMethod.None OrElse controllerIds.Contains(e.Id) Then
                keyIds.Add(e.Id)
            End If
        Next

        Dim lst As IList(Of String) = SetOperations.Difference(keyIds, controllerIds, StringComparer.InvariantCultureIgnoreCase)

        Return lst.Count = 0

    End Function



    Public Sub New()
        MyBase.New()
        _keys = New Dictionary(Of String, T)
    End Sub


End Class
