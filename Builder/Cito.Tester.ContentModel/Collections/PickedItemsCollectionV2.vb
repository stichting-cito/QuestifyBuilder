Imports System.Collections.ObjectModel
Imports System.Diagnostics.CodeAnalysis


<SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix"),
    SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")>
Public Class PickedItemCollectionV2
    Inherits SortedDictionary(Of Integer, ItemReferenceViewBase)

    Public Shadows Sub Add(index As Integer, ref As ItemReferenceViewBase)
        If Me.ContainsKey(index) Then
            Me.Remove(index)
        End If
        MyBase.Add(index, ref)
    End Sub

    Public Function GetItemRefByIndex(index As Integer) As ItemReferenceViewBase
        If Me.ContainsKey(index) Then
            Return Me(index)
        Else
            Return Nothing
        End If
    End Function

    Public Function GetIndexByItemRef(itemRef As ItemReferenceViewBase) As Integer
        For Each entry As KeyValuePair(Of Integer, ItemReferenceViewBase) In Me
            If entry.Value.Equals(itemRef) Then
                Return entry.Key
            End If
        Next

        Return -1
    End Function


    Public Function GetIndexByItemTitle(title As String) As Integer
        For Each entry As KeyValuePair(Of Integer, ItemReferenceViewBase) In Me
            If entry.Value.Title = title Then
                Return entry.Key
            End If
        Next

        Return -1
    End Function

    Public Function GetIndexByItemIdentifier(id As String) As Integer
        For Each entry As KeyValuePair(Of Integer, ItemReferenceViewBase) In Me
            If entry.Value.Identifier = id Then
                Return entry.Key
            End If
        Next

        Return -1
    End Function

    Public Function GetItemsInTestComponent(comp As TestComponentBase) As ReadOnlyCollection(Of ItemReferenceViewBase)
        Dim foundItems As New List(Of ItemReferenceViewBase)

        For Each entry As ItemReferenceViewBase In Values
            If IsInComponent(entry, comp) Then
                foundItems.Add(entry)
            End If
        Next

        Return New ReadOnlyCollection(Of ItemReferenceViewBase)(foundItems)
    End Function

    Public Function GetStartOffsetOfTestPart(part As TestPartViewBase) As Integer
        Dim index As Integer = -1

        For Each entry As KeyValuePair(Of Integer, ItemReferenceViewBase) In Me
            If (index = -1 OrElse entry.Key < index) AndAlso IsInComponent(entry.Value, part) Then
                Return entry.Key
            End If
        Next

        Return index
    End Function

    Public Function GetStartOffsetOfSection(section As TestSectionViewBase) As Integer
        Dim index As Integer = -1

        For Each entry As KeyValuePair(Of Integer, ItemReferenceViewBase) In Me
            If (index = -1 OrElse entry.Key < index) AndAlso IsInComponent(entry.Value, section) Then
                Return entry.Key
            End If
        Next

        Return index
    End Function

    Public Function GetStartItemOfSection(section As TestSectionViewBase) As ItemReferenceViewBase
        For Each entry As KeyValuePair(Of Integer, ItemReferenceViewBase) In Me
            If entry.Value.FirstItemInSection AndAlso IsInComponent(entry.Value, section) Then
                Return entry.Value
            End If
        Next

        Return Nothing
    End Function

    Public Function GetLengthOfSection(section As TestSectionViewBase) As Integer
        Dim entryCount As Integer = 0

        For Each entry As ItemReferenceViewBase In Values
            If IsInComponent(entry, section) Then
                entryCount += 1
            End If
        Next

        Return entryCount
    End Function

    Public Function GetLengthOfTestPart(testPart As TestPartViewBase) As Integer
        Dim entryCount As Integer = 0

        For Each entry As ItemReferenceViewBase In Me.Values
            If IsInComponent(entry, testPart) Then
                entryCount += 1
            End If
        Next

        Return entryCount
    End Function
    Public Function GetNextSelectableItemIndexFrom(startIndex As Integer) As Integer
        For Each itemIndex As Integer In Me.Keys
            If itemIndex > startIndex AndAlso IsThisItemSelectable(Me(itemIndex)) Then
                Return itemIndex
            End If
        Next

        Return -1
    End Function

    Public Function GetNextSelectableItemIndexFrom(itemReference As ItemReferenceViewBase) As Integer
        Dim startIndex As Integer = GetIndexByItemRef(itemReference)

        If startIndex > -1 Then
            Return GetNextSelectableItemIndexFrom(startIndex)
        Else
            Return -1
        End If
    End Function

    Public Function GetLastSelectableItem() As ItemReferenceViewBase
        Dim lastSelectableIndex As Integer = -1

        For Each entry As KeyValuePair(Of Integer, ItemReferenceViewBase) In Me
            If entry.Key > lastSelectableIndex AndAlso IsThisItemSelectable(entry.Value) Then
                lastSelectableIndex = entry.Key
            End If
        Next

        If lastSelectableIndex > -1 Then
            Return Me(lastSelectableIndex)
        Else
            Return Nothing
        End If
    End Function

    Public Function GetLogicalItemSequenceNumber(itemReference As ItemReferenceViewBase, currentTestPart As TestPartViewBase) As Integer
        Dim startIndex As Integer = GetStartOffsetOfTestPart(currentTestPart)

        Dim sequenceNumberCount = 0
        For index As Integer = startIndex To Count - 1
            Dim itemRefViewBase As ItemReferenceViewBase = Me(index)

            If itemRefViewBase.ItemFunctionalType = ItemFunctionalType.Regular Then
                sequenceNumberCount += 1
            End If

            If itemRefViewBase.Equals(itemReference) Then
                Return sequenceNumberCount
            End If
        Next

        Return sequenceNumberCount
    End Function


    Private Shared Function IsInComponent(item As ItemReferenceViewBase, theComponent As TestComponentBase) As Boolean
        Dim component As TestComponentBase = item.Parent

        Do
            If component.Equals(theComponent) Then
                Return True
            End If

            If TypeOf component Is TestSectionViewBase Then
                component = DirectCast(component, TestSectionViewBase).Parent
            Else
                component = Nothing
            End If
        Loop Until component Is Nothing

        Return False
    End Function

    Private Shared Function IsThisItemSelectable(item As ItemReferenceViewBase) As Boolean
        If item.State = ComponentState.Picked OrElse item.State = ComponentState.Pickable Then
            Dim parent As TestComponentBase = item.Parent
            Do Until parent Is Nothing
                If parent.State <> ComponentState.Picked AndAlso parent.State <> ComponentState.Pickable Then
                    Return False
                End If
                If TypeOf parent Is TestSectionViewBase Then
                    parent = DirectCast(parent, TestSectionViewBase).Parent
                Else
                    parent = Nothing
                End If
            Loop

            Return True
        Else
            Return False
        End If
    End Function


End Class
