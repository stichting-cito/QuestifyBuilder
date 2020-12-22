Imports Questify.Builder.UI

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Word_AssessmentTestPropertiesEditor
    Inherits TestEditorControlBase

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Word_AssessmentTestPropertiesEditor))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.PrintFormEditor1 = New Questify.Builder.Plugins.PaperBased.PrintFormEditor()
        CType(Me.ControlBindingSource, System.ComponentModel.ISupportInitialize).BeginInit
        Me.TableLayoutPanel1.SuspendLayout
        Me.SuspendLayout
        Me.ControlBindingSource.DataSource = GetType(Questify.Builder.Plugins.PaperBased.WordAssessmentTest)
        resources.ApplyResources(Me.TableLayoutPanel1, "TableLayoutPanel1")
        Me.TableLayoutPanel1.Controls.Add(Me.PrintFormEditor1, 0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        resources.ApplyResources(Me.PrintFormEditor1, "PrintFormEditor1")
        Me.PrintFormEditor1.DataBindings.Add(New System.Windows.Forms.Binding("DataSource", Me.ControlBindingSource, "PrintForm", true))
        Me.PrintFormEditor1.DataSource = Nothing
        Me.PrintFormEditor1.Name = "PrintFormEditor1"
        Me.PrintFormEditor1.TestEntity = Nothing
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "Word_AssessmentTestPropertiesEditor"
        CType(Me.ControlBindingSource, System.ComponentModel.ISupportInitialize).EndInit
        Me.TableLayoutPanel1.ResumeLayout(false)
        Me.TableLayoutPanel1.PerformLayout
        Me.ResumeLayout(false)
        Me.PerformLayout

    End Sub
    Friend WithEvents PrintFormEditor1 As PrintFormEditor
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel

End Class
