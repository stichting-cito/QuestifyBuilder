Imports Questify.Builder.Logic

Namespace Dialogs.BusinessLogic
    Friend Class NoneTableStyleStrategy
        Inherits baseTableStyleStrategy



        Public Sub New(presenter As BorderAndShadingPresenter)
            MyBase.New(presenter)
        End Sub

        Public Overrides Sub CalculateNewStyle()
            Dim newStyle = Presenter.Style

            newStyle.Box(LineStyle.Hidden, 0)
            newStyle.Inner(LineStyle.Hidden, 0)

            Presenter.Style = newStyle
        End Sub

        Friend Overrides ReadOnly Property StrategyName As String
            Get
                Return "none"
            End Get
        End Property

        Public Overrides Function CanHandleState() As Boolean
            Return GetBoxStatus(False) AndAlso GetInnerStatus(False)
        End Function

        Public Overrides Sub InitiateState()
            SetToggleForBox(False)
            SetToggleForInner(False)
        End Sub
    End Class
End Namespace