<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ExcelImportOptionsControl
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
        Me.components = New System.ComponentModel.Container
        Dim PackageUrlLabel As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ExcelImportOptionsControl))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.BrowseButton = New System.Windows.Forms.Button
        Me.ExcelUrlTextBox = New System.Windows.Forms.TextBox
        Me.ExcelImportOptionsDataEntityBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ExcelOpenFileDialog = New System.Windows.Forms.OpenFileDialog
        Me.ExcelImportOptionsDataEntityErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        PackageUrlLabel = New System.Windows.Forms.Label
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.ExcelImportOptionsDataEntityBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ExcelImportOptionsDataEntityErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        resources.ApplyResources(PackageUrlLabel, "PackageUrlLabel")
        PackageUrlLabel.Name = "PackageUrlLabel"
        resources.ApplyResources(Me.TableLayoutPanel1, "TableLayoutPanel1")
        Me.TableLayoutPanel1.Controls.Add(Me.BrowseButton, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.ExcelUrlTextBox, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(PackageUrlLabel, 0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        resources.ApplyResources(Me.BrowseButton, "BrowseButton")
        Me.BrowseButton.Name = "BrowseButton"
        Me.BrowseButton.UseVisualStyleBackColor = True
        resources.ApplyResources(Me.ExcelUrlTextBox, "ExcelUrlTextBox")
        Me.ExcelUrlTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.ExcelImportOptionsDataEntityBindingSource, "ExcelUrl", True))
        Me.ExcelUrlTextBox.Name = "ExcelUrlTextBox"
        Me.ExcelImportOptionsDataEntityBindingSource.DataSource = GetType(Questify.Builder.Client.ExcelImportOptionsDataEntity)
        Me.ExcelOpenFileDialog.DefaultExt = "*.xlsx;*.*"
        Me.ExcelOpenFileDialog.DereferenceLinks = False
        resources.ApplyResources(Me.ExcelOpenFileDialog, "ExcelOpenFileDialog")
        Me.ExcelImportOptionsDataEntityErrorProvider.ContainerControl = Me
        Me.ExcelImportOptionsDataEntityErrorProvider.DataSource = Me.ExcelImportOptionsDataEntityBindingSource
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "ExcelImportOptionsControl"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        CType(Me.ExcelImportOptionsDataEntityBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ExcelImportOptionsDataEntityErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents BrowseButton As System.Windows.Forms.Button
    Friend WithEvents ExcelUrlTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ExcelImportOptionsDataEntityBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ExcelOpenFileDialog As System.Windows.Forms.OpenFileDialog
    Friend WithEvents ExcelImportOptionsDataEntityErrorProvider As System.Windows.Forms.ErrorProvider

End Class
