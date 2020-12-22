Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ItemSelector
Imports Questify.Builder.Logic.ItemSelector.Interfaces
Imports Questify.Builder.Logic.QTI.ItemSelector

Namespace QTI

    Public Class ItemSelectorManager
        Inherits ItemSelectorManagerBase

        Public Sub New(ByVal test As AssessmentTestViewBase)
            MyBase.New(test)
            If _customItemSelector Is Nothing Then
                _customItemSelector = New QTIItemSelector
            End If
        End Sub


        Private _customItemSelector As IItemSelectorV2
        Private _isLastItemInTest As Boolean
        Private _isLastItemInTestPart As Boolean



        Public ReadOnly Property IsLastItemInTest() As Boolean
            Get
                Return _isLastItemInTest
            End Get
        End Property




        Private Function DetermineLastItemInTest(ByVal itemRef As ItemReferenceViewBase) As Boolean
            Dim returnValue As Boolean = True

            For Each t As TestPartViewBase In Me.Test.TestParts
                If t.IsPickable() Then
                    returnValue = False
                    Exit For
                End If
            Next

            Return returnValue
        End Function



        Public Overloads Function PickNewItem(ByVal index As Integer) As ItemReferenceViewBase
            Dim newItem As ItemReferenceViewBase = DirectCast(Me.PickNewItem(Nothing, index, New TransactionData, _customItemSelector), ItemReferenceViewBase)
            Me.CurrentTestPart = GetTestPartOf(newItem)

            _isLastItemInTestPart = newItem.LastItemInTestPart
            _isLastItemInTest = DetermineLastItemInTest(newItem)

            Return newItem
        End Function


    End Class
End Namespace