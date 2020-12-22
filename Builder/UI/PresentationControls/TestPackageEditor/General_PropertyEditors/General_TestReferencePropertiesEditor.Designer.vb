<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class General_TestReferencePropertiesEditor
    Inherits TestPackageEditorControlBase

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
        Dim TitleLabel As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(General_TestReferencePropertiesEditor))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.TitleTextBox = New System.Windows.Forms.TextBox()
        TitleLabel = New System.Windows.Forms.Label()
        CType(Me.ControlBindingSource, System.ComponentModel.ISupportInitialize).BeginInit
        Me.TableLayoutPanel1.SuspendLayout
        Me.SuspendLayout
        Me.ControlBindingSource.DataSource = GetType(Cito.Tester.ContentModel.GeneralTestReference)
        resources.ApplyResources(TitleLabel, "TitleLabel")
        TitleLabel.Name = "TitleLabel"
        resources.ApplyResources(Me.TableLayoutPanel1, "TableLayoutPanel1")
        Me.TableLayoutPanel1.Controls.Add(TitleLabel, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.TitleTextBox, 1, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        resources.ApplyResources(Me.TitleTextBox, "TitleTextBox")
        Me.TitleTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.ControlBindingSource, "Title", true))
        Me.TitleTextBox.Name = "TitleTextBox"
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "General_TestReferencePropertiesEditor"
        CType(Me.ControlBindingSource, System.ComponentModel.ISupportInitialize).EndInit
        Me.TableLayoutPanel1.ResumeLayout(false)
        Me.TableLayoutPanel1.PerformLayout
        Me.ResumeLayout(false)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TitleTextBox As System.Windows.Forms.TextBox

End Class
