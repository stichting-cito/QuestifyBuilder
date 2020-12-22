Public Class FocussedCheckBox
    Inherits CheckBox

    Protected Overrides Sub OnEnter(e As EventArgs)
        MyBase.OnEnter(e)
        MyBase.OnMouseEnter(e)
    End Sub

    Protected Overrides Sub OnLeave(e As EventArgs)
        MyBase.OnLeave(e)
        MyBase.OnMouseLeave(e)
    End Sub
End Class