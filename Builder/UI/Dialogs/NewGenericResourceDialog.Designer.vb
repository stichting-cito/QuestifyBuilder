<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class NewGenericResourceDialog
    Inherits DialogBase

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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(NewGenericResourceDialog))
        Me.CodeTextBox = New System.Windows.Forms.TextBox()
        Me.GenericResourceBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.CodeLabel = New System.Windows.Forms.Label()
        Me.TitleLabel = New System.Windows.Forms.Label()
        Me.DescriptionLabel = New System.Windows.Forms.Label()
        Me.TitleTextBox = New System.Windows.Forms.TextBox()
        Me.DescriptionTextBox = New System.Windows.Forms.TextBox()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.BrowseLabel = New System.Windows.Forms.Label()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.BrowseButton = New System.Windows.Forms.Button()
        Me.BrowseTextBox = New System.Windows.Forms.TextBox()
        Me.ResourceErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.ResourceDataErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.ContentPanel.SuspendLayout
        CType(Me.GenericResourceBindingSource, System.ComponentModel.ISupportInitialize).BeginInit
        Me.TableLayoutPanel1.SuspendLayout
        Me.TableLayoutPanel2.SuspendLayout
        CType(Me.ResourceErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.ResourceDataErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        Me.ContentPanel.Controls.Add(Me.TableLayoutPanel1)
        resources.ApplyResources(Me.ContentPanel, "ContentPanel")
        resources.ApplyResources(Me.CodeTextBox, "CodeTextBox")
        Me.CodeTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.GenericResourceBindingSource, "Name", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.CodeTextBox.Name = "CodeTextBox"
        Me.GenericResourceBindingSource.DataSource = GetType(Questify.Builder.Model.ContentModel.EntityClasses.ResourceEntity)
        resources.ApplyResources(Me.CodeLabel, "CodeLabel")
        Me.CodeLabel.Name = "CodeLabel"
        resources.ApplyResources(Me.TitleLabel, "TitleLabel")
        Me.TitleLabel.Name = "TitleLabel"
        resources.ApplyResources(Me.DescriptionLabel, "DescriptionLabel")
        Me.DescriptionLabel.Name = "DescriptionLabel"
        resources.ApplyResources(Me.TitleTextBox, "TitleTextBox")
        Me.TitleTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.GenericResourceBindingSource, "Title", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.TitleTextBox.Name = "TitleTextBox"
        Me.DescriptionTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.GenericResourceBindingSource, "Description", true))
        resources.ApplyResources(Me.DescriptionTextBox, "DescriptionTextBox")
        Me.DescriptionTextBox.Name = "DescriptionTextBox"
        resources.ApplyResources(Me.TableLayoutPanel1, "TableLayoutPanel1")
        Me.TableLayoutPanel1.Controls.Add(Me.TitleLabel, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.DescriptionTextBox, 1, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.CodeTextBox, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.CodeLabel, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TitleTextBox, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.BrowseLabel, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.DescriptionLabel, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 1, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        resources.ApplyResources(Me.BrowseLabel, "BrowseLabel")
        Me.BrowseLabel.Name = "BrowseLabel"
        resources.ApplyResources(Me.TableLayoutPanel2, "TableLayoutPanel2")
        Me.TableLayoutPanel2.Controls.Add(Me.BrowseButton, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.BrowseTextBox, 0, 0)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        resources.ApplyResources(Me.BrowseButton, "BrowseButton")
        Me.BrowseButton.Name = "BrowseButton"
        Me.BrowseButton.UseVisualStyleBackColor = true
        resources.ApplyResources(Me.BrowseTextBox, "BrowseTextBox")
        Me.BrowseTextBox.Name = "BrowseTextBox"
        Me.BrowseTextBox.TabStop = false
        Me.ResourceErrorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink
        Me.ResourceErrorProvider.ContainerControl = Me
        Me.ResourceErrorProvider.DataSource = Me.GenericResourceBindingSource
        Me.ResourceDataErrorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink
        Me.ResourceDataErrorProvider.ContainerControl = Me
        resources.ApplyResources(Me, "$this")
        Me.Name = "NewGenericResourceDialog"
        Me.ContentPanel.ResumeLayout(false)
        CType(Me.GenericResourceBindingSource, System.ComponentModel.ISupportInitialize).EndInit
        Me.TableLayoutPanel1.ResumeLayout(false)
        Me.TableLayoutPanel1.PerformLayout
        Me.TableLayoutPanel2.ResumeLayout(false)
        Me.TableLayoutPanel2.PerformLayout
        CType(Me.ResourceErrorProvider, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.ResourceDataErrorProvider, System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)

    End Sub
    Friend WithEvents DescriptionTextBox As System.Windows.Forms.TextBox
    Friend WithEvents TitleTextBox As System.Windows.Forms.TextBox
    Friend WithEvents DescriptionLabel As System.Windows.Forms.Label
    Friend WithEvents TitleLabel As System.Windows.Forms.Label
    Friend WithEvents CodeLabel As System.Windows.Forms.Label
    Friend WithEvents CodeTextBox As System.Windows.Forms.TextBox
    Friend WithEvents GenericResourceBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents BrowseTextBox As System.Windows.Forms.TextBox
    Friend WithEvents BrowseLabel As System.Windows.Forms.Label
    Friend WithEvents BrowseButton As System.Windows.Forms.Button
    Friend WithEvents ResourceErrorProvider As System.Windows.Forms.ErrorProvider
    Friend WithEvents ResourceDataErrorProvider As System.Windows.Forms.ErrorProvider
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel

End Class
