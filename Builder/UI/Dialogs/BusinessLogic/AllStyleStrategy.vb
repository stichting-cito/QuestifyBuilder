
Imports Questify.Builder.Logic

Namespace Dialogs.BusinessLogic
    Friend Class AllStyleStrategy
        Inherits baseTableStyleStrategy


        Sub New(presenter As Dialogs.BusinessLogic.BorderAndShadingPresenter)
            MyBase.New(presenter)
        End Sub

        Public Overrides Sub CalculateNewStyle()
            Presenter.Style.Box(Presenter.CurrentLineStyle, Presenter.CurrentLineWidth)
            Presenter.Style.Inner(Presenter.CurrentLineStyle, Presenter.CurrentLineWidth)
        End Sub

        Public Overrides Function CanHandleState() As Boolean
            Return GetBoxStatus(True) AndAlso GetInnerStatus(True)
        End Function

        Friend Overrides ReadOnly Property StrategyName As String
            Get
                Return "all"
            End Get
        End Property

        Public Overrides Sub InitiateState()
            SetToggleForBox(True)
            SetToggleForInner(True)
        End Sub

        Public Overrides Sub GetDefaultSettings(ByRef currentLineStyle As LineStyle, ByRef currentLineWidth As Integer)
            currentLineStyle = Presenter.Style.LeftVertical.Value
            currentLineWidth = Presenter.Style.LeftVerticalWidth
        End Sub
    End Class
End Namespace
