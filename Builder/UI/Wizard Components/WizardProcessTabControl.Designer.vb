<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class WizardProcessTabControl
    Inherits WizardTabContentControl

    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WizardProcessTabControl))
        Me.ProcessProgressOverallBar = New System.Windows.Forms.ProgressBar()
        Me.ProcessStepInfoLabelDetail = New System.Windows.Forms.Label()
        Me.ProcessProgressDetailBar = New System.Windows.Forms.ProgressBar()
        Me.ProcessStepInfoLabelOverall = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        resources.ApplyResources(Me.ProcessProgressOverallBar, "ProcessProgressOverallBar")
        Me.ProcessProgressOverallBar.Name = "ProcessProgressOverallBar"
        resources.ApplyResources(Me.ProcessStepInfoLabelDetail, "ProcessStepInfoLabelDetail")
        Me.ProcessStepInfoLabelDetail.Name = "ProcessStepInfoLabelDetail"
        resources.ApplyResources(Me.ProcessProgressDetailBar, "ProcessProgressDetailBar")
        Me.ProcessProgressDetailBar.Name = "ProcessProgressDetailBar"
        resources.ApplyResources(Me.ProcessStepInfoLabelOverall, "ProcessStepInfoLabelOverall")
        Me.ProcessStepInfoLabelOverall.Name = "ProcessStepInfoLabelOverall"
        resources.ApplyResources(Me, "$this")
        Me.Controls.Add(Me.ProcessStepInfoLabelOverall)
        Me.Controls.Add(Me.ProcessProgressDetailBar)
        Me.Controls.Add(Me.ProcessStepInfoLabelDetail)
        Me.Controls.Add(Me.ProcessProgressOverallBar)
        Me.Name = "WizardProcessTabControl"
        Me.Controls.SetChildIndex(Me.ProcessProgressOverallBar, 0)
        Me.Controls.SetChildIndex(Me.ProcessStepInfoLabelDetail, 0)
        Me.Controls.SetChildIndex(Me.ProcessProgressDetailBar, 0)
        Me.Controls.SetChildIndex(Me.ProcessStepInfoLabelOverall, 0)
        Me.ResumeLayout(False)

    End Sub
    Protected WithEvents ProcessProgressOverallBar As System.Windows.Forms.ProgressBar
    Protected WithEvents ProcessStepInfoLabelDetail As System.Windows.Forms.Label
    Protected WithEvents ProcessProgressDetailBar As System.Windows.Forms.ProgressBar
    Friend WithEvents ProcessStepInfoLabelOverall As System.Windows.Forms.Label

End Class
