
Imports Questify.Builder.Logic

Namespace Dialogs.BusinessLogic
    Friend Class GridTableStyleStrategy
        Inherits baseTableStyleStrategy


        Sub New(presenter As Dialogs.BusinessLogic.BorderAndShadingPresenter)
            MyBase.New(presenter)
        End Sub

        Public Overrides Sub CalculateNewStyle()
            Presenter.Style.Box(Presenter.CurrentLineStyle, Presenter.CurrentLineWidth)
            Presenter.Style.Inner(LineStyle.Solid, 1)
        End Sub

        Public Overrides Function CanHandleState() As Boolean
            Return MyBase.GetInnerStatus(True) AndAlso GetBoxStatus(True)
        End Function

        Friend Overrides ReadOnly Property StrategyName As String
            Get
                Return "grid"
            End Get
        End Property

        Public Overrides Sub InitiateState()
            SetToggleForBox(True)
            SetToggleForInner(True)
        End Sub
    End Class
End Namespace
