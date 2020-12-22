Imports Questify.Builder.Logic.PluginExtensibility.Html.EditBehavior

Public Class FocusedReparentHtmlEditor
    Private Sub FocusedReparentHtmlEditor_Enter(sender As Object, e As EventArgs) Handles MyBase.Enter
        HtmlEditor_ReceivedFocus(Me, True)
    End Sub

    Private Sub FocusedReparentHtmlEditor_Leave(sender As Object, e As EventArgs) Handles MyBase.Leave
        HtmlEditor_ReceivedFocus(Me, False)
    End Sub

    Private Sub HtmlEditor_ReceivedFocus(sender As Object, setFocus As Boolean)
        LayoutPanel.BackColor = If(setFocus, Color.FromArgb(0, 120, 215), Color.FromArgb(122, 122, 122))

        If setFocus Then
            editor.SetFocus()
        Else
            editor.SetMouseFocused(False)
        End If
    End Sub

    Public ReadOnly Property HtmlEditor As ReparentHtmlEditor
        Get
            Return editor
        End Get
    End Property

    Public Sub Initialize(behavior As BaseHtmlEditorBehavior)
        editor.Initialize(behavior)
        AddHandler editor.EditorReceivedFocus, AddressOf HtmlEditor_ReceivedFocus
    End Sub

    Private Sub DoDispose(disposing As Boolean)

        If disposing Then
            If editor IsNot Nothing Then
                RemoveHandler editor.EditorReceivedFocus, AddressOf HtmlEditor_ReceivedFocus

                editor.Dispose()
                editor = Nothing
            End If
        End If

    End Sub
End Class
