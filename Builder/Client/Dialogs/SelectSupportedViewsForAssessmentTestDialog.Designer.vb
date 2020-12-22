<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SelectSupportedViewsForAssessmentTestDialog
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SelectSupportedViewsForAssessmentTestDialog))
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.ViewTypesCheckedListBox = New System.Windows.Forms.CheckedListBox()
        Me.ContentLabel = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel1.SuspendLayout
        Me.SuspendLayout
        resources.ApplyResources(Me.OK_Button, "OK_Button")
        Me.OK_Button.Name = "OK_Button"
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        resources.ApplyResources(Me.Cancel_Button, "Cancel_Button")
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.ViewTypesCheckedListBox.CheckOnClick = true
        Me.ViewTypesCheckedListBox.FormattingEnabled = true
        resources.ApplyResources(Me.ViewTypesCheckedListBox, "ViewTypesCheckedListBox")
        Me.ViewTypesCheckedListBox.Name = "ViewTypesCheckedListBox"
        resources.ApplyResources(Me.ContentLabel, "ContentLabel")
        Me.ContentLabel.Name = "ContentLabel"
        Me.Panel1.Controls.Add(Me.OK_Button)
        Me.Panel1.Controls.Add(Me.Cancel_Button)
        resources.ApplyResources(Me.Panel1, "Panel1")
        Me.Panel1.Name = "Panel1"
        Me.AcceptButton = Me.OK_Button
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ControlBox = false
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.ContentLabel)
        Me.Controls.Add(Me.ViewTypesCheckedListBox)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "SelectSupportedViewsForAssessmentTestDialog"
        Me.ShowInTaskbar = false
        Me.Panel1.ResumeLayout(false)
        Me.ResumeLayout(false)

    End Sub
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents ViewTypesCheckedListBox As System.Windows.Forms.CheckedListBox
    Friend WithEvents ContentLabel As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
End Class
