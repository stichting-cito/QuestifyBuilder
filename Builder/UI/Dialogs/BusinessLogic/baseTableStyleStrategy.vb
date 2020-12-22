Imports Questify.Builder.Logic

Namespace Dialogs.BusinessLogic
    Friend MustInherit Class baseTableStyleStrategy
        Private _presenter As BorderAndShadingPresenter

        Public Sub New(presenter As BorderAndShadingPresenter)
            _presenter = presenter
        End Sub

        Protected ReadOnly Property Presenter As BorderAndShadingPresenter
            Get
                Return _presenter
            End Get
        End Property

        Public MustOverride Sub InitiateState()

        Public MustOverride Function CanHandleState() As Boolean

        Public MustOverride Sub CalculateNewStyle()

        Friend MustOverride ReadOnly Property StrategyName As String

        Friend Sub SetToggleForBox(checked As Boolean)
            Presenter.LeftChecked = checked
            Presenter.TopChecked = checked
            Presenter.RightChecked = checked
            Presenter.BottomChecked = checked
        End Sub

        Friend Sub SetToggleForInner(checked As Boolean)
            Presenter.MidHorizontalChecked = checked
            Presenter.MidVerticalChecked = checked
        End Sub

        Friend Function GetBoxStatus(checked As Boolean) As Boolean
            Return (Presenter.LeftChecked = checked AndAlso
            Presenter.TopChecked = checked AndAlso
            Presenter.RightChecked = checked AndAlso
            Presenter.BottomChecked = checked)
        End Function

        Friend Function GetInnerStatus(checked As Boolean) As Boolean
            Return (Presenter.MidHorizontalChecked = checked AndAlso
            Presenter.MidVerticalChecked = checked)
        End Function

        Overridable Sub GetDefaultSettings(ByRef currentLineStyle As LineStyle, ByRef currentLineWidth As Integer)
        End Sub

    End Class
End Namespace