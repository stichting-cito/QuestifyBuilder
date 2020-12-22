<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ItemSessionControlEditor
    Inherits System.Windows.Forms.UserControl

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
        Me.components = New System.ComponentModel.Container()
        Dim ShowSolutionLabel As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ItemSessionControlEditor))
        Dim AllowCommentLabel As System.Windows.Forms.Label
        Dim AllowSkippingLabel As System.Windows.Forms.Label
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.AllowSkippingCheckBox = New System.Windows.Forms.CheckBox()
        Me.ItemSessionControlBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.AllowReviewCheckBox = New System.Windows.Forms.CheckBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.MaxAttemptsNumericUpDown = New System.Windows.Forms.NumericUpDown()
        Me.ShowFeedBackCheckBox = New System.Windows.Forms.CheckBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ShowSolutionCheckBox = New System.Windows.Forms.CheckBox()
        Me.AllowCommentCheckBox = New System.Windows.Forms.CheckBox()
        ShowSolutionLabel = New System.Windows.Forms.Label()
        AllowCommentLabel = New System.Windows.Forms.Label()
        AllowSkippingLabel = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.ItemSessionControlBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MaxAttemptsNumericUpDown, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        resources.ApplyResources(ShowSolutionLabel, "ShowSolutionLabel")
        ShowSolutionLabel.Name = "ShowSolutionLabel"
        resources.ApplyResources(AllowCommentLabel, "AllowCommentLabel")
        AllowCommentLabel.Name = "AllowCommentLabel"
        resources.ApplyResources(AllowSkippingLabel, "AllowSkippingLabel")
        AllowSkippingLabel.Name = "AllowSkippingLabel"
        resources.ApplyResources(Me.TableLayoutPanel1, "TableLayoutPanel1")
        Me.TableLayoutPanel1.Controls.Add(AllowSkippingLabel, 0, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.AllowSkippingCheckBox, 1, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.AllowReviewCheckBox, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Label2, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Label1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.MaxAttemptsNumericUpDown, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.ShowFeedBackCheckBox, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Label3, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(ShowSolutionLabel, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.ShowSolutionCheckBox, 1, 3)
        Me.TableLayoutPanel1.Controls.Add(AllowCommentLabel, 0, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.AllowCommentCheckBox, 1, 4)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.AllowSkippingCheckBox.DataBindings.Add(New System.Windows.Forms.Binding("CheckState", Me.ItemSessionControlBindingSource, "AllowSkipping", True))
        resources.ApplyResources(Me.AllowSkippingCheckBox, "AllowSkippingCheckBox")
        Me.AllowSkippingCheckBox.Name = "AllowSkippingCheckBox"
        Me.AllowSkippingCheckBox.UseVisualStyleBackColor = True
        Me.ItemSessionControlBindingSource.DataSource = GetType(Cito.Tester.ContentModel.ItemSessionControl)
        resources.ApplyResources(Me.AllowReviewCheckBox, "AllowReviewCheckBox")
        Me.AllowReviewCheckBox.DataBindings.Add(New System.Windows.Forms.Binding("CheckState", Me.ItemSessionControlBindingSource, "AllowReview", True))
        Me.AllowReviewCheckBox.Name = "AllowReviewCheckBox"
        Me.AllowReviewCheckBox.UseVisualStyleBackColor = True
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.Name = "Label2"
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        Me.MaxAttemptsNumericUpDown.DataBindings.Add(New System.Windows.Forms.Binding("Value", Me.ItemSessionControlBindingSource, "MaxAttempts", True))
        resources.ApplyResources(Me.MaxAttemptsNumericUpDown, "MaxAttemptsNumericUpDown")
        Me.MaxAttemptsNumericUpDown.Name = "MaxAttemptsNumericUpDown"
        resources.ApplyResources(Me.ShowFeedBackCheckBox, "ShowFeedBackCheckBox")
        Me.ShowFeedBackCheckBox.DataBindings.Add(New System.Windows.Forms.Binding("CheckState", Me.ItemSessionControlBindingSource, "ShowFeedback", True))
        Me.ShowFeedBackCheckBox.Name = "ShowFeedBackCheckBox"
        Me.ShowFeedBackCheckBox.UseVisualStyleBackColor = True
        resources.ApplyResources(Me.Label3, "Label3")
        Me.Label3.Name = "Label3"
        Me.ShowSolutionCheckBox.DataBindings.Add(New System.Windows.Forms.Binding("CheckState", Me.ItemSessionControlBindingSource, "ShowSolution", True))
        resources.ApplyResources(Me.ShowSolutionCheckBox, "ShowSolutionCheckBox")
        Me.ShowSolutionCheckBox.Name = "ShowSolutionCheckBox"
        Me.ShowSolutionCheckBox.UseVisualStyleBackColor = True
        Me.AllowCommentCheckBox.DataBindings.Add(New System.Windows.Forms.Binding("CheckState", Me.ItemSessionControlBindingSource, "AllowComment", True))
        resources.ApplyResources(Me.AllowCommentCheckBox, "AllowCommentCheckBox")
        Me.AllowCommentCheckBox.Name = "AllowCommentCheckBox"
        Me.AllowCommentCheckBox.UseVisualStyleBackColor = True
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "ItemSessionControlEditor"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        CType(Me.ItemSessionControlBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MaxAttemptsNumericUpDown, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ItemSessionControlBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents MaxAttemptsNumericUpDown As System.Windows.Forms.NumericUpDown
    Friend WithEvents ShowFeedBackCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents AllowReviewCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents AllowSkippingCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents ShowSolutionCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents AllowCommentCheckBox As System.Windows.Forms.CheckBox

End Class
