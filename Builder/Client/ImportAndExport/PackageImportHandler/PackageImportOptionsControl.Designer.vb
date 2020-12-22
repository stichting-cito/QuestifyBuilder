<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PackageImportOptionsControl
    Inherits ImportOptionControlBase

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
        Dim PackageUrlLabel As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PackageImportOptionsControl))
        Me.PackageImportOptionsDataEntityBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.PackageImportOptionsDataEntityErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.PackageUrlTextBox = New System.Windows.Forms.TextBox()
        Me.ImportToRootCheckBox = New System.Windows.Forms.CheckBox()
        Me.BrowseButton = New System.Windows.Forms.Button()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.PackageOpenFileDialog = New System.Windows.Forms.OpenFileDialog()
        PackageUrlLabel = New System.Windows.Forms.Label()
        CType(Me.PackageImportOptionsDataEntityBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PackageImportOptionsDataEntityErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        resources.ApplyResources(PackageUrlLabel, "PackageUrlLabel")
        PackageUrlLabel.Name = "PackageUrlLabel"
        Me.PackageImportOptionsDataEntityBindingSource.DataSource = GetType(Questify.Builder.Client.PackageImportOptionsDataEntity)
        Me.PackageImportOptionsDataEntityErrorProvider.ContainerControl = Me
        Me.PackageImportOptionsDataEntityErrorProvider.DataSource = Me.PackageImportOptionsDataEntityBindingSource
        resources.ApplyResources(Me.PackageUrlTextBox, "PackageUrlTextBox")
        Me.PackageUrlTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.PackageImportOptionsDataEntityBindingSource, "PackageUrl", True))
        Me.PackageImportOptionsDataEntityErrorProvider.SetIconPadding(Me.PackageUrlTextBox, CType(resources.GetObject("PackageUrlTextBox.IconPadding"), Integer))
        Me.PackageUrlTextBox.Name = "PackageUrlTextBox"
        resources.ApplyResources(Me.ImportToRootCheckBox, "ImportToRootCheckBox")
        Me.ImportToRootCheckBox.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.PackageImportOptionsDataEntityBindingSource, "ImportToRoot", True))
        Me.ImportToRootCheckBox.Name = "ImportToRootCheckBox"
        Me.ImportToRootCheckBox.UseVisualStyleBackColor = True
        resources.ApplyResources(Me.BrowseButton, "BrowseButton")
        Me.BrowseButton.Name = "BrowseButton"
        Me.BrowseButton.UseVisualStyleBackColor = True
        resources.ApplyResources(Me.TableLayoutPanel1, "TableLayoutPanel1")
        Me.TableLayoutPanel1.Controls.Add(Me.BrowseButton, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.PackageUrlTextBox, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(PackageUrlLabel, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.ImportToRootCheckBox, 1, 1)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.PackageOpenFileDialog.DefaultExt = "*.export;*.exportset;*.*"
        resources.ApplyResources(Me.PackageOpenFileDialog, "PackageOpenFileDialog")
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "PackageImportOptionsControl"
        CType(Me.PackageImportOptionsDataEntityBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PackageImportOptionsDataEntityErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PackageImportOptionsDataEntityBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents PackageImportOptionsDataEntityErrorProvider As System.Windows.Forms.ErrorProvider
    Friend WithEvents PackageOpenFileDialog As System.Windows.Forms.OpenFileDialog
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents BrowseButton As System.Windows.Forms.Button
    Friend WithEvents PackageUrlTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ImportToRootCheckBox As System.Windows.Forms.CheckBox

End Class
