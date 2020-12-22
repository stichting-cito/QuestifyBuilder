
Imports Questify.Builder.Logic

Namespace Dialogs.BusinessLogic
    Friend Class BoxTableStyleStrategy
        Inherits baseTableStyleStrategy



        Sub New(presenter As Dialogs.BusinessLogic.BorderAndShadingPresenter)
            MyBase.New(presenter)
        End Sub

        Public Overrides Sub CalculateNewStyle()
            Presenter.Style.Inner(LineStyle.Hidden, 0)
            Presenter.Style.Box(Presenter.CurrentLineStyle, Presenter.CurrentLineWidth)
        End Sub

        Public Overrides Function CanHandleState() As Boolean
            Return MyBase.GetInnerStatus(False) AndAlso GetBoxStatus(True)
        End Function

        Friend Overrides ReadOnly Property StrategyName As String
            Get
                Return "box"
            End Get
        End Property

        Public Overrides Sub InitiateState()
            SetToggleForBox(True)
            SetToggleForInner(False)
        End Sub
    End Class
End Namespace
