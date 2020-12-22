Imports Questify.Builder.UI

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Word_ItemReferencePropertiesEditor
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Word_ItemReferencePropertiesEditor))
        Me.ItemScoreTranslationTableBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        CType(Me.ControlBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ItemScoreTranslationTableBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        Me.ControlBindingSource.DataSource = GetType(WordItemReference)
        Me.ItemScoreTranslationTableBindingSource.DataSource = GetType(Cito.Tester.ContentModel.ItemScoreTranslationTable)
        resources.ApplyResources(Me.TableLayoutPanel1, "TableLayoutPanel1")
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "Word_ItemReferencePropertiesEditor"
        CType(Me.ControlBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ItemScoreTranslationTableBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ItemScoreTranslationTableBindingSource As System.Windows.Forms.BindingSource

End Class
