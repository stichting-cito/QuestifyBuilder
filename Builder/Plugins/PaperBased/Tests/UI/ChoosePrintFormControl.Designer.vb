<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ChoosePrintFormControl
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ChoosePrintFormControl))
        Me.ResourceParameterTextBox = New System.Windows.Forms.TextBox()
        Me.PrintFormBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.SelectResourceButton = New System.Windows.Forms.Button()
        Me.DeleteResourceButton = New System.Windows.Forms.Button()
        Me.PrintFormLabelTextBox = New System.Windows.Forms.TextBox()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        CType(Me.PrintFormBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        resources.ApplyResources(Me.ResourceParameterTextBox, "ResourceParameterTextBox")
        Me.ResourceParameterTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.PrintFormBindingSource, "ResourceName", True))
        Me.ResourceParameterTextBox.Name = "ResourceParameterTextBox"
        Me.PrintFormBindingSource.DataSource = GetType(PrintForm)
        resources.ApplyResources(Me.SelectResourceButton, "SelectResourceButton")
        Me.SelectResourceButton.Name = "SelectResourceButton"
        Me.SelectResourceButton.UseVisualStyleBackColor = True
        resources.ApplyResources(Me.DeleteResourceButton, "DeleteResourceButton")
        Me.DeleteResourceButton.Name = "DeleteResourceButton"
        Me.DeleteResourceButton.UseVisualStyleBackColor = True
        Me.PrintFormLabelTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.PrintFormBindingSource, "TypeLabel", True))
        resources.ApplyResources(Me.PrintFormLabelTextBox, "PrintFormLabelTextBox")
        Me.PrintFormLabelTextBox.Name = "PrintFormLabelTextBox"
        resources.ApplyResources(Me.TableLayoutPanel1, "TableLayoutPanel1")
        Me.TableLayoutPanel1.Controls.Add(Me.PrintFormLabelTextBox, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.DeleteResourceButton, 3, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.SelectResourceButton, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.ResourceParameterTextBox, 1, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.ErrorProvider1.ContainerControl = Me
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "ChoosePrintFormControl"
        CType(Me.PrintFormBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ResourceParameterTextBox As System.Windows.Forms.TextBox
    Friend WithEvents SelectResourceButton As System.Windows.Forms.Button
    Friend WithEvents DeleteResourceButton As System.Windows.Forms.Button
    Friend WithEvents PrintFormLabelTextBox As System.Windows.Forms.TextBox
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents PrintFormBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider

End Class
