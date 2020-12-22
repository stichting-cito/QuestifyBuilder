<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class BankMetaData
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(BankMetaData))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.NameLabel = New System.Windows.Forms.Label()
        Me.CreatedByLabel = New System.Windows.Forms.Label()
        Me.CreatedByLabel1 = New System.Windows.Forms.Label()
        Me.BankMetaDataBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.CreationDateLabel = New System.Windows.Forms.Label()
        Me.CreationDateLabel1 = New System.Windows.Forms.Label()
        Me.NameTextBox = New System.Windows.Forms.TextBox()
        Me.ModifiedByLabel = New System.Windows.Forms.Label()
        Me.ModifiedByLabel1 = New System.Windows.Forms.Label()
        Me.ModifiedDateLabel = New System.Windows.Forms.Label()
        Me.ModifiedDateLabel1 = New System.Windows.Forms.Label()
        Me.BankErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.TableLayoutPanel1.SuspendLayout
        CType(Me.BankMetaDataBindingSource, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.BankErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        resources.ApplyResources(Me.TableLayoutPanel1, "TableLayoutPanel1")
        Me.TableLayoutPanel1.Controls.Add(Me.NameLabel, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.CreatedByLabel, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.CreatedByLabel1, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.CreationDateLabel, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.CreationDateLabel1, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.NameTextBox, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.ModifiedByLabel, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.ModifiedByLabel1, 1, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.ModifiedDateLabel, 0, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.ModifiedDateLabel1, 1, 4)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        resources.ApplyResources(Me.NameLabel, "NameLabel")
        Me.NameLabel.Name = "NameLabel"
        resources.ApplyResources(Me.CreatedByLabel, "CreatedByLabel")
        Me.CreatedByLabel.Name = "CreatedByLabel"
        resources.ApplyResources(Me.CreatedByLabel1, "CreatedByLabel1")
        Me.CreatedByLabel1.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.BankMetaDataBindingSource, "CreatedByFullName", true))
        Me.CreatedByLabel1.Name = "CreatedByLabel1"
        Me.BankMetaDataBindingSource.DataSource = GetType(Questify.Builder.Model.ContentModel.EntityClasses.BankEntity)
        resources.ApplyResources(Me.CreationDateLabel, "CreationDateLabel")
        Me.CreationDateLabel.Name = "CreationDateLabel"
        resources.ApplyResources(Me.CreationDateLabel1, "CreationDateLabel1")
        Me.CreationDateLabel1.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.BankMetaDataBindingSource, "CreationDate", true))
        Me.CreationDateLabel1.Name = "CreationDateLabel1"
        resources.ApplyResources(Me.NameTextBox, "NameTextBox")
        Me.NameTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.BankMetaDataBindingSource, "Name", true))
        Me.NameTextBox.Name = "NameTextBox"
        resources.ApplyResources(Me.ModifiedByLabel, "ModifiedByLabel")
        Me.ModifiedByLabel.Name = "ModifiedByLabel"
        resources.ApplyResources(Me.ModifiedByLabel1, "ModifiedByLabel1")
        Me.ModifiedByLabel1.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.BankMetaDataBindingSource, "ModifiedByFullName", true))
        Me.ModifiedByLabel1.Name = "ModifiedByLabel1"
        resources.ApplyResources(Me.ModifiedDateLabel, "ModifiedDateLabel")
        Me.ModifiedDateLabel.Name = "ModifiedDateLabel"
        resources.ApplyResources(Me.ModifiedDateLabel1, "ModifiedDateLabel1")
        Me.ModifiedDateLabel1.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.BankMetaDataBindingSource, "ModifiedDate", true))
        Me.ModifiedDateLabel1.Name = "ModifiedDateLabel1"
        Me.BankErrorProvider.ContainerControl = Me
        Me.BankErrorProvider.DataSource = Me.BankMetaDataBindingSource
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "BankMetaData"
        Me.TableLayoutPanel1.ResumeLayout(false)
        Me.TableLayoutPanel1.PerformLayout
        CType(Me.BankMetaDataBindingSource, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.BankErrorProvider, System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents NameLabel As System.Windows.Forms.Label
    Friend WithEvents CreatedByLabel As System.Windows.Forms.Label
    Friend WithEvents CreatedByLabel1 As System.Windows.Forms.Label
    Friend WithEvents CreationDateLabel As System.Windows.Forms.Label
    Friend WithEvents CreationDateLabel1 As System.Windows.Forms.Label
    Friend WithEvents NameTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ModifiedByLabel As System.Windows.Forms.Label
    Friend WithEvents ModifiedByLabel1 As System.Windows.Forms.Label
    Friend WithEvents ModifiedDateLabel As System.Windows.Forms.Label
    Friend WithEvents ModifiedDateLabel1 As System.Windows.Forms.Label
    Friend WithEvents BankMetaDataBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents BankErrorProvider As System.Windows.Forms.ErrorProvider

End Class
