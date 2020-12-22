<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ItemEditor
    Inherits ItemEditorControlBase

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
        Dim IdentifierLabel As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ItemEditor))
        Dim TitleLabel As System.Windows.Forms.Label
        Dim LayoutTemplateSourceNameLabel As System.Windows.Forms.Label
        Me.ItemIdLabel = New System.Windows.Forms.Label()
        Me.AssessmentItemTableLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.ItemIdTextBox = New System.Windows.Forms.TextBox()
        Me.SwitchItemTemplateButton = New System.Windows.Forms.Button()
        Me.TitleTextBox = New System.Windows.Forms.TextBox()
        Me.CodeTextBox = New System.Windows.Forms.TextBox()
        Me.LayoutTemplateSourceNameTextBox = New System.Windows.Forms.TextBox()
        Me.ChangeCodeButton = New System.Windows.Forms.Button()
        IdentifierLabel = New System.Windows.Forms.Label()
        TitleLabel = New System.Windows.Forms.Label()
        LayoutTemplateSourceNameLabel = New System.Windows.Forms.Label()
        CType(Me.AssessmentItemBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.AssessmentItemTableLayoutPanel.SuspendLayout()
        Me.SuspendLayout()
        resources.ApplyResources(IdentifierLabel, "IdentifierLabel")
        IdentifierLabel.Name = "IdentifierLabel"
        resources.ApplyResources(TitleLabel, "TitleLabel")
        TitleLabel.Name = "TitleLabel"
        resources.ApplyResources(LayoutTemplateSourceNameLabel, "LayoutTemplateSourceNameLabel")
        LayoutTemplateSourceNameLabel.Name = "LayoutTemplateSourceNameLabel"
        resources.ApplyResources(Me.ItemIdLabel, "ItemIdLabel")
        Me.ItemIdLabel.Name = "ItemIdLabel"
        resources.ApplyResources(Me.AssessmentItemTableLayoutPanel, "AssessmentItemTableLayoutPanel")
        Me.AssessmentItemTableLayoutPanel.Controls.Add(Me.ItemIdLabel, 0, 0)
        Me.AssessmentItemTableLayoutPanel.Controls.Add(Me.ItemIdTextBox, 1, 0)
        Me.AssessmentItemTableLayoutPanel.Controls.Add(Me.SwitchItemTemplateButton, 2, 3)
        Me.AssessmentItemTableLayoutPanel.Controls.Add(LayoutTemplateSourceNameLabel, 0, 3)
        Me.AssessmentItemTableLayoutPanel.Controls.Add(TitleLabel, 0, 2)
        Me.AssessmentItemTableLayoutPanel.Controls.Add(Me.TitleTextBox, 1, 2)
        Me.AssessmentItemTableLayoutPanel.Controls.Add(IdentifierLabel, 0, 1)
        Me.AssessmentItemTableLayoutPanel.Controls.Add(Me.CodeTextBox, 1, 1)
        Me.AssessmentItemTableLayoutPanel.Controls.Add(Me.LayoutTemplateSourceNameTextBox, 1, 3)
        Me.AssessmentItemTableLayoutPanel.Controls.Add(Me.ChangeCodeButton, 2, 1)
        Me.AssessmentItemTableLayoutPanel.Name = "AssessmentItemTableLayoutPanel"
        resources.ApplyResources(Me.ItemIdTextBox, "ItemIdTextBox")
        Me.ItemIdTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.AssessmentItemBindingSource, "ItemId", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.ItemIdTextBox.Name = "ItemIdTextBox"
        resources.ApplyResources(Me.SwitchItemTemplateButton, "SwitchItemTemplateButton")
        Me.SwitchItemTemplateButton.Name = "SwitchItemTemplateButton"
        Me.SwitchItemTemplateButton.UseVisualStyleBackColor = True
        resources.ApplyResources(Me.TitleTextBox, "TitleTextBox")
        Me.TitleTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.AssessmentItemBindingSource, "Title", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.TitleTextBox.Name = "TitleTextBox"
        resources.ApplyResources(Me.CodeTextBox, "CodeTextBox")
        Me.CodeTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.AssessmentItemBindingSource, "Identifier", True))
        Me.CodeTextBox.Name = "CodeTextBox"
        resources.ApplyResources(Me.LayoutTemplateSourceNameTextBox, "LayoutTemplateSourceNameTextBox")
        Me.LayoutTemplateSourceNameTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.AssessmentItemBindingSource, "LayoutTemplateSourceName", True))
        Me.LayoutTemplateSourceNameTextBox.Name = "LayoutTemplateSourceNameTextBox"
        resources.ApplyResources(Me.ChangeCodeButton, "ChangeCodeButton")
        Me.ChangeCodeButton.Name = "ChangeCodeButton"
        Me.ChangeCodeButton.UseVisualStyleBackColor = True
        resources.ApplyResources(Me, "$this")
        Me.Controls.Add(Me.AssessmentItemTableLayoutPanel)
        Me.Name = "ItemEditor"
        CType(Me.AssessmentItemBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.AssessmentItemTableLayoutPanel.ResumeLayout(False)
        Me.AssessmentItemTableLayoutPanel.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents AssessmentItemTableLayoutPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TitleTextBox As System.Windows.Forms.TextBox
    Friend WithEvents CodeTextBox As System.Windows.Forms.TextBox
    Friend WithEvents LayoutTemplateSourceNameTextBox As System.Windows.Forms.TextBox
    Friend WithEvents SwitchItemTemplateButton As System.Windows.Forms.Button
    Friend WithEvents ChangeCodeButton As System.Windows.Forms.Button
    Friend WithEvents ItemIdTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ItemIdLabel As System.Windows.Forms.Label

End Class
