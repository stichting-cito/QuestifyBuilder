Imports Questify.Builder.Logic
Imports Questify.Builder.UI.Dialogs.BusinessLogic
Imports Questify.Builder.UI.Controls

Namespace Dialogs
    Public Class BordersAndShading
        Implements IBordersAndShadingView


        Private _presenter As BorderAndShadingPresenter
        Private ReadOnly _toolTip As New ToolTip

        Property Presenter As BorderAndShadingPresenter
            Get
                Return _presenter
            End Get
            Set(value As BorderAndShadingPresenter)
                _presenter = value
                If (_presenter IsNot Nothing) Then UpdateView()
            End Set
        End Property

        Public Sub New()
            InitializeComponent()

            ComboBoxLineWeight.SelectedIndex = 0
            ListBoxLineStyle.SelectedIndex = 0
        End Sub


        Private Sub Setting_None(sender As System.Object, e As EventArgs) Handles tbpNone.Click
            If Presenter Is Nothing Then Return
            Presenter.SetBorderStrategy("none")
        End Sub

        Private Sub Setting_Box(sender As System.Object, e As EventArgs) Handles tbpBox.Click
            If Presenter Is Nothing Then Return
            Presenter.SetBorderStrategy("box")
        End Sub

        Private Sub Setting_All(sender As System.Object, e As EventArgs) Handles tbpAll.Click
            If Presenter Is Nothing Then Return
            Presenter.SetBorderStrategy("all")
        End Sub

        Private Sub Setting_Grid(sender As System.Object, e As EventArgs) Handles tbpGrid.Click
            If Presenter Is Nothing Then Return
            Presenter.SetBorderStrategy("grid")
        End Sub

        Private Sub Setting_Custom(sender As System.Object, e As EventArgs) Handles tbpCustom.Click
            If Presenter Is Nothing Then Return
            Presenter.SetBorderStrategy("custom")
        End Sub

        Private Sub CheckTopHorChanged(sender As System.Object, e As EventArgs) Handles ToggleTopHorizontal.CheckedChanged
            If Presenter Is Nothing Then Return
            Presenter.TopChecked = ToggleTopHorizontal.Checked
        End Sub

        Private Sub CheckMidHorChanged(sender As System.Object, e As EventArgs) Handles ToggleMidHorizontal.CheckedChanged
            If Presenter Is Nothing Then Return
            Presenter.MidHorizontalChecked = ToggleMidHorizontal.Checked
        End Sub

        Private Sub CheckBottomHorChanged(sender As System.Object, e As EventArgs) Handles ToggleBottomHorizontal.CheckedChanged
            If Presenter Is Nothing Then Return
            Presenter.BottomChecked = ToggleBottomHorizontal.Checked
        End Sub

        Private Sub CheckLeftVertChanged(sender As System.Object, e As EventArgs) Handles ToggleLeftVertical.CheckedChanged
            If Presenter Is Nothing Then Return
            Presenter.LeftChecked = ToggleLeftVertical.Checked
        End Sub

        Private Sub CheckMidVertChanged(sender As System.Object, e As EventArgs) Handles ToggleMidVertical.CheckedChanged
            If Presenter Is Nothing Then Return
            Presenter.MidVerticalChecked = ToggleMidVertical.Checked
        End Sub

        Private Sub CheckRightVertChanged(sender As System.Object, e As EventArgs) Handles ToggleRightVertical.CheckedChanged
            If Presenter Is Nothing Then Return
            Presenter.RightChecked = ToggleRightVertical.Checked
        End Sub

        Private Sub ChangeLineWidth(sender As System.Object, e As EventArgs) Handles ComboBoxLineWeight.SelectionChangeCommitted
            If Presenter Is Nothing Then Return
            Dim str As String = ComboBoxLineWeight.SelectedItem.ToString()
            str = str.Replace("px", "")
            Presenter.CurrentLineWidth = Integer.Parse(str)
        End Sub

        Private Sub ChangeLineStyle(sender As System.Object, e As EventArgs) Handles ListBoxLineStyle.SelectedIndexChanged
            If Presenter Is Nothing Then Return
            Presenter.CurrentLineStyle = DirectCast([Enum].Parse(GetType(LineStyle), ListBoxLineStyle.SelectedItem.ToString(), True), LineStyle)
        End Sub

        Private Sub ColorPicked(sender As System.Object, e As EventArgs) Handles ColorButton1.SelectedColorChanged
            Presenter.SelectedBackgroundColor = ColorButton1.SelectedColor
        End Sub

        Private Sub Clear_Color(sender As System.Object, e As EventArgs) Handles ClearColor.Click
            Presenter.SelectedBackgroundColor = Nothing
        End Sub


        Private Sub LineColorPicked(sender As System.Object, e As EventArgs) Handles LineColorButton.SelectedColorChanged
            Presenter.SelectedLineColor = LineColorButton.SelectedColor
        End Sub

        Private Sub tbpGrid_MouseHover(sender As Object, e As EventArgs) Handles tbpGrid.MouseHover
            If Presenter Is Nothing Then Return
            _toolTip.Show(tbpGrid.AccessibleDescription, tbpGrid, 2000)
        End Sub



        Private Sub DrawLineStyle(sender As System.Object, e As System.Windows.Forms.DrawItemEventArgs) Handles ListBoxLineStyle.DrawItem
            If (e.State And DrawItemState.Selected) = DrawItemState.Selected Then
                e = New DrawItemEventArgs(e.Graphics, e.Font, e.Bounds, e.Index, e.State Xor DrawItemState.Selected, e.ForeColor, Color.LightBlue)
            End If

            e.DrawBackground()

            Using p As New Pen(Color.Black, 6)
                Dim y As Integer = 9

                Select Case e.Index
                    Case 0
                    Case 1
                        p.DashStyle = Drawing2D.DashStyle.Dot
                    Case 2
                        p.DashStyle = Drawing2D.DashStyle.Dash
                    Case 3
                        p.Width = 1
                        e.Graphics.DrawLine(p, e.Bounds.X + 10, e.Bounds.Y + y, e.Bounds.Right - 10, e.Bounds.Y + y)

                        y += 4
                        p.Width = 4
                End Select

                e.Graphics.DrawLine(p, e.Bounds.X + 10, e.Bounds.Y + y, e.Bounds.Right - 10, e.Bounds.Y + y)
            End Using

            e.DrawFocusRectangle()
        End Sub

        Private Sub UpdateView()
            UpdateChkBoxs()
            ToggleMidHorizontal.Visible = Presenter.Style.HasMidHorizontal
            ToggleMidVertical.Visible = Presenter.Style.HasMidVertical
            TableCellPreview1.SetDto(Presenter.Style)
            TableCellPreview2.SetDto(Presenter.Style)
            If Presenter.Style.LineColor.HasValue Then LineColorButton.SelectedColor = Presenter.Style.LineColor.Value
            If Presenter.Style.BackColor.HasValue Then ColorButton1.SelectedColor = Presenter.Style.BackColor.Value
            InitializeExamples()
            SetStrategyDisplay(Presenter.CurrentTableStyleStrategy)
        End Sub

        Sub InitializeExamples()
            Dim base = Presenter.Style.Clone()

            Dim none = base.Clone() : none.Inner(LineStyle.Hidden, 0) : none.Box(LineStyle.None, 0)
            Dim box = base.Clone() : box.Inner(LineStyle.Hidden, 0) : box.Box(LineStyle.Solid, 1)
            Dim all = base.Clone() : all.Inner(LineStyle.Solid, 1) : all.Box(LineStyle.Solid, 1)
            Dim grid = base.Clone() : grid.Inner(LineStyle.Solid, 1) : grid.Box(LineStyle.Solid, 2)
            Dim custom = base.Clone() : custom.Inner(LineStyle.Solid, 1) : custom.Box(LineStyle.Solid, 1)
            custom.LeftVertical = LineStyle.Dashed
            custom.LeftVerticalWidth = 4
            custom.BottomHorizontal = LineStyle.Dotted
            custom.RightVertical = Nothing

            tbpNone.SetDto(none)
            tbpBox.SetDto(box)
            tbpAll.SetDto(all)
            tbpGrid.SetDto(grid)
            tbpCustom.SetDto(custom)
        End Sub

        Private Sub UpdateChkBoxs()
            ToggleTopHorizontal.Checked = Presenter.TopChecked
            ToggleMidHorizontal.Checked = Presenter.MidHorizontalChecked
            ToggleBottomHorizontal.Checked = Presenter.BottomChecked
            ToggleLeftVertical.Checked = Presenter.LeftChecked
            ToggleMidVertical.Checked = Presenter.MidVerticalChecked
            ToggleRightVertical.Checked = Presenter.RightChecked
        End Sub


        Public Sub InvalidateExample() Implements BusinessLogic.IBordersAndShadingView.InvalidateExample
            TableCellPreview1.SetDto(Presenter.Style)
            TableCellPreview2.SetDto(Presenter.Style)
            InitializeExamples()
            UpdateChkBoxs()
        End Sub

        Public Sub SetStrategyDisplay(name As String) Implements BusinessLogic.IBordersAndShadingView.SetStrategyDisplay
            tbpNone.IsSelected = ("none" = name OrElse "hidden" = name)
            tbpBox.IsSelected = "box" = name
            tbpAll.IsSelected = "all" = name
            tbpGrid.IsSelected = "grid" = name
            tbpCustom.IsSelected = "custom" = name
        End Sub

    End Class
End Namespace
