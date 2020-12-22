<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ResourceIdentifierEditor
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
        Dim TitleLabel As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ResourceIdentifierEditor))
        Dim IdentifierLabel As System.Windows.Forms.Label
        Me.ResourceEntityBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.AssessmentItemTableLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.TitleTextBox = New System.Windows.Forms.TextBox()
        Me.CodeTextBox = New System.Windows.Forms.TextBox()
        Me.ChangeCodeButton = New System.Windows.Forms.Button()
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        TitleLabel = New System.Windows.Forms.Label()
        IdentifierLabel = New System.Windows.Forms.Label()
        CType(Me.ResourceEntityBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.AssessmentItemTableLayoutPanel.SuspendLayout()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        resources.ApplyResources(TitleLabel, "TitleLabel")
        TitleLabel.Name = "TitleLabel"
        resources.ApplyResources(IdentifierLabel, "IdentifierLabel")
        IdentifierLabel.Name = "IdentifierLabel"
        Me.ResourceEntityBindingSource.DataSource = GetType(Questify.Builder.Model.ContentModel.EntityClasses.ResourceEntity)
        resources.ApplyResources(Me.AssessmentItemTableLayoutPanel, "AssessmentItemTableLayoutPanel")
        Me.AssessmentItemTableLayoutPanel.Controls.Add(TitleLabel, 0, 1)
        Me.AssessmentItemTableLayoutPanel.Controls.Add(Me.TitleTextBox, 1, 1)
        Me.AssessmentItemTableLayoutPanel.Controls.Add(IdentifierLabel, 0, 0)
        Me.AssessmentItemTableLayoutPanel.Controls.Add(Me.CodeTextBox, 1, 0)
        Me.AssessmentItemTableLayoutPanel.Controls.Add(Me.ChangeCodeButton, 2, 0)
        Me.AssessmentItemTableLayoutPanel.Name = "AssessmentItemTableLayoutPanel"
        resources.ApplyResources(Me.TitleTextBox, "TitleTextBox")
        Me.TitleTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.ResourceEntityBindingSource, "Title", True))
        Me.TitleTextBox.Name = "TitleTextBox"
        resources.ApplyResources(Me.CodeTextBox, "CodeTextBox")
        Me.CodeTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.ResourceEntityBindingSource, "Name", True))
        Me.CodeTextBox.Name = "CodeTextBox"
        resources.ApplyResources(Me.ChangeCodeButton, "ChangeCodeButton")
        Me.ChangeCodeButton.Name = "ChangeCodeButton"
        Me.ChangeCodeButton.UseVisualStyleBackColor = True
        Me.ErrorProvider1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink
        Me.ErrorProvider1.ContainerControl = Me
        Me.ErrorProvider1.DataSource = Me.ResourceEntityBindingSource
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.AssessmentItemTableLayoutPanel)
        Me.Name = "ResourceIdentifierEditor"
        CType(Me.ResourceEntityBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.AssessmentItemTableLayoutPanel.ResumeLayout(False)
        Me.AssessmentItemTableLayoutPanel.PerformLayout()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ResourceEntityBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents AssessmentItemTableLayoutPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TitleTextBox As System.Windows.Forms.TextBox
    Friend WithEvents CodeTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ChangeCodeButton As System.Windows.Forms.Button
    Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider


End Class
