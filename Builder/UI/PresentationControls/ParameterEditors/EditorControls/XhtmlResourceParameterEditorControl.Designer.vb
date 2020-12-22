<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class XhtmlResourceParameterEditorControl
    Inherits Questify.Builder.UI.ResourceParameterEditorControl

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(XhtmlResourceParameterEditorControl))
        Me.XhtmlReferenceBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.ReferenceComboBox = New System.Windows.Forms.ComboBox()
        Me.ReferenceLabel = New System.Windows.Forms.Label()
        CType(Me.ParameterBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XhtmlReferenceBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.SuspendLayout()
        Me.XhtmlReferenceBindingSource.DataSource = GetType(Cito.Tester.Common.XhtmlReference)
        Me.TableLayoutPanel2.BackColor = System.Drawing.SystemColors.Control
        resources.ApplyResources(Me.TableLayoutPanel2, "TableLayoutPanel2")
        Me.TableLayoutPanel2.Controls.Add(Me.ReferenceComboBox, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.ReferenceLabel, 0, 0)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        resources.ApplyResources(Me.ReferenceComboBox, "ReferenceComboBox")
        Me.ReferenceComboBox.DataSource = Me.XhtmlReferenceBindingSource
        Me.ReferenceComboBox.DisplayMember = "Description"
        Me.ReferenceComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ReferenceComboBox.FormattingEnabled = True
        Me.ReferenceComboBox.Name = "ReferenceComboBox"
        Me.ReferenceComboBox.ValueMember = "ID"
        resources.ApplyResources(Me.ReferenceLabel, "ReferenceLabel")
        Me.ReferenceLabel.Name = "ReferenceLabel"
        resources.ApplyResources(Me, "$this")
        Me.Controls.Add(Me.TableLayoutPanel2)
        Me.Name = "XhtmlResourceParameterEditorControl"
        Me.Controls.SetChildIndex(Me.TableLayoutPanel2, 0)
        CType(Me.ParameterBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XhtmlReferenceBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents XhtmlReferenceBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ReferenceComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents ReferenceLabel As System.Windows.Forms.Label

End Class
