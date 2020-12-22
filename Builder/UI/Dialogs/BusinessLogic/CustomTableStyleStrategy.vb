Imports Questify.Builder.Logic
Imports Questify.Builder.UI.Controls

Namespace Dialogs.BusinessLogic

    Friend Class CustomTableStyleStrategy
        Inherits baseTableStyleStrategy


        Public Sub New(presenter As BorderAndShadingPresenter)
            MyBase.New(presenter)
        End Sub

        Public Overrides Sub CalculateNewStyle()
            With Presenter.Style
                SetStyleIfNoneAndChecked(Presenter.LeftChecked, .LeftVertical, .LeftVerticalWidth)
                SetStyleIfNoneAndChecked(Presenter.TopChecked, .TopHorizontal, .TopHorizontalWidth)
                SetStyleIfNoneAndChecked(Presenter.RightChecked, .RightVertical, .RightVerticalWidth)
                SetStyleIfNoneAndChecked(Presenter.BottomChecked, .BottomHorizontal, .BottomHorizontalWidth)

                If .HasMidHorizontal Then
                    SetStyleIfNoneAndChecked(Presenter.MidHorizontalChecked, .MidHorizontal, .MidHorizontalWidth)
                End If

                If .HasMidVertical Then
                    SetStyleIfNoneAndChecked(Presenter.MidVerticalChecked, .MidVertical, .MidVerticalWidth)
                End If
            End With
        End Sub

        Public Overrides Function CanHandleState() As Boolean
            Return True
        End Function

        Friend Overrides ReadOnly Property StrategyName As String
            Get
                Return "custom"
            End Get
        End Property

        Private Sub SetStyleIfNoneAndChecked(checked As Boolean, ByRef refLineStyle As LineStyle?, ByRef refLineWidth As Integer)
            If (checked AndAlso (refLineStyle = LineStyle.None OrElse refLineStyle = LineStyle.Hidden)) Then
                refLineStyle = Presenter.CurrentLineStyle
                refLineWidth = Presenter.CurrentLineWidth
            ElseIf Not checked Then
                refLineStyle = LineStyle.Hidden
                refLineWidth = 0
            End If
        End Sub

        Public Overrides Sub InitiateState()
            Dim s = Presenter.Style
            Presenter.LeftChecked = TrueOrFalse(s.LeftVertical)
            Presenter.TopChecked = TrueOrFalse(s.TopHorizontal)
            Presenter.RightChecked = TrueOrFalse(s.RightVertical)
            Presenter.BottomChecked = TrueOrFalse(s.BottomHorizontal)

            Presenter.MidHorizontalChecked = s.HasMidHorizontal AndAlso TrueOrFalse(s.MidHorizontal)
            Presenter.MidVerticalChecked = s.HasMidVertical AndAlso TrueOrFalse(s.MidVertical)
        End Sub

        Private Function TrueOrFalse(s As LineStyle?) As Boolean
            If s.HasValue Then
                Return (s.Value <> LineStyle.None AndAlso s.Value <> LineStyle.Hidden)
            End If
            Return True
        End Function

    End Class

End Namespace

