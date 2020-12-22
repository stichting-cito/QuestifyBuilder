
Public Class ContentModelHelper


    Public Shared Function GetTestPartForItemRef(itemRef As ItemReferenceViewBase) As TestPartViewBase
        If Not (itemRef.Parent Is Nothing) Then
            Dim testContainer As TestComponentBase = itemRef.Parent

            If Not (testContainer Is Nothing) Then
                While Not (TypeOf testContainer Is TestPartViewBase)
                    testContainer = DirectCast(testContainer, TestSectionViewBase).Parent
                End While

                Return DirectCast(testContainer, TestPartViewBase)
            Else
                Return Nothing
            End If
        Else
            Return Nothing
        End If
    End Function

    Public Shared Function IsLastItemInTest(itemRef As ItemReferenceViewBase) As Boolean
        Return itemRef.LastItemInTestPart
    End Function


    Public Shared Function SectionContainsItems(section As TestSectionViewBase) As Boolean
        For Each component As TestComponentViewBase In section.Components
            If TypeOf component Is ItemReferenceViewBase Then
                Return True
            Else
                If SectionContainsItems(DirectCast(component, TestSectionViewBase)) Then
                    Return True
                End If
            End If
        Next

        Return False
    End Function

    Public Shared Function IsEmptyTestPart(part As TestPartViewBase) As Boolean
        For Each section As TestSectionViewBase In part.Sections
            If SectionContainsItems(section) Then
                Return False
            End If
        Next

        Return True
    End Function


    Public Shared Function GetLastTestPartOfTestContainingItems(test As AssessmentTestViewBase) As TestPartViewBase
        Dim testPartIndex As Integer = test.TestParts.Count - 1

        Do While IsEmptyTestPart(test.TestParts(testPartIndex)) AndAlso testPartIndex >= 0
            testPartIndex -= 1
        Loop

        If testPartIndex >= 0 Then
            Return test.TestParts(testPartIndex)
        Else
            Return Nothing
        End If
    End Function


    Public Shared Function GetPickedItemsForCurrentTestpart(sessionContextPickedItems As PickedItemCollectionV2) As PickedItemCollectionV2
        Dim pickedTestpartItems As New PickedItemCollectionV2
        Dim indexCounter As Integer = 0

        If Not (TestSessionContext.CurrentItem.ItemRefContextV2 Is Nothing) Then
            Dim currentTestpart As TestPartViewBase = GetTestPartForItemRef(TestSessionContext.CurrentItem.ItemRefContextV2)

            For Each pickedItem As KeyValuePair(Of Integer, ItemReferenceViewBase) In sessionContextPickedItems
                Dim itemref As ItemReferenceViewBase = DirectCast(pickedItem.Value, ItemReferenceViewBase)
                If Not (itemref Is Nothing) Then
                    Dim itemTestpart As TestPartViewBase = GetTestPartForItemRef(itemref)
                    If (Not (itemTestpart Is Nothing)) AndAlso (Not (currentTestpart Is Nothing)) Then
                        If itemTestpart.Identifier = currentTestpart.Identifier Then
                            pickedTestpartItems.Add(indexCounter, itemref)
                        End If
                        indexCounter += 1
                    End If
                End If
            Next
        End If

        Return pickedTestpartItems
    End Function

End Class
