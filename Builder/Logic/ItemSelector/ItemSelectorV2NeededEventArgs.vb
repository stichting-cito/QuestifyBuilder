Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ItemSelector.Interfaces

Namespace ItemSelector

    Public Class ItemSelectorV2NeededEventArgs
        Inherits EventArgs


        Private _section As TestSectionViewBase
        Private _selector As IItemSelectorV2

        Public Sub New(section As TestSectionViewBase)
            _section = section
        End Sub

        Public ReadOnly Property Section() As TestSectionViewBase
            Get
                Return _section
            End Get
        End Property

        Public Property Selector() As IItemSelectorV2
            Get
                Return _selector
            End Get
            Set(value As IItemSelectorV2)
                _selector = value
            End Set
        End Property

    End Class

End Namespace