<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PackageExportOptionsControl
    Inherits ExportOptionControlBase

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
        Dim PackageUrlLabel As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PackageExportOptionsControl))
        Me.PackageExportOptionsDataEntityBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.PackageUrlTextBox = New System.Windows.Forms.TextBox()
        Me.PackageExportOptionsDataEntityErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.BrowseButton = New System.Windows.Forms.Button()
        Me.ExportSubBanksCheckBox = New System.Windows.Forms.CheckBox()
        PackageUrlLabel = New System.Windows.Forms.Label()
        CType(Me.PackageExportOptionsDataEntityBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PackageExportOptionsDataEntityErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        resources.ApplyResources(PackageUrlLabel, "PackageUrlLabel")
        PackageUrlLabel.Name = "PackageUrlLabel"
        Me.PackageExportOptionsDataEntityBindingSource.DataSource = GetType(Questify.Builder.Client.PackageExportOptionsDataEntity)
        resources.ApplyResources(Me.PackageUrlTextBox, "PackageUrlTextBox")
        Me.PackageUrlTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.PackageExportOptionsDataEntityBindingSource, "PackageUrl", True))
        Me.PackageUrlTextBox.Name = "PackageUrlTextBox"
        Me.PackageExportOptionsDataEntityErrorProvider.ContainerControl = Me
        Me.PackageExportOptionsDataEntityErrorProvider.DataSource = Me.PackageExportOptionsDataEntityBindingSource
        resources.ApplyResources(Me.TableLayoutPanel1, "TableLayoutPanel1")
        Me.TableLayoutPanel1.Controls.Add(Me.BrowseButton, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.PackageUrlTextBox, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(PackageUrlLabel, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.ExportSubBanksCheckBox, 1, 1)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        resources.ApplyResources(Me.BrowseButton, "BrowseButton")
        Me.BrowseButton.Name = "BrowseButton"
        Me.BrowseButton.UseVisualStyleBackColor = True
        resources.ApplyResources(Me.ExportSubBanksCheckBox, "ExportSubBanksCheckBox")
        Me.ExportSubBanksCheckBox.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.PackageExportOptionsDataEntityBindingSource, "ExportSubBanks", True))
        Me.ExportSubBanksCheckBox.Name = "ExportSubBanksCheckBox"
        Me.ExportSubBanksCheckBox.UseVisualStyleBackColor = True
        resources.ApplyResources(Me, "$this")
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "PackageExportOptionsControl"
        CType(Me.PackageExportOptionsDataEntityBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PackageExportOptionsDataEntityErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PackageExportOptionsDataEntityBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents PackageUrlTextBox As System.Windows.Forms.TextBox
    Friend WithEvents PackageExportOptionsDataEntityErrorProvider As System.Windows.Forms.ErrorProvider
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents BrowseButton As System.Windows.Forms.Button
    Friend WithEvents ExportSubBanksCheckBox As System.Windows.Forms.CheckBox

End Class
