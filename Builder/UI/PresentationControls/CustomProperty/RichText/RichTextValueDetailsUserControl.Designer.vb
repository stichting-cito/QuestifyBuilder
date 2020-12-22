<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RichTextValueDetailsUserControl
    Inherits CustomPropertyUserControlBase

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(RichTextValueDetailsUserControl))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.DescriptionEditor = New Questify.Builder.UI.ReparentHtmlEditor()
        Me.CustomPropertyRichTextBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.CustomPropertyRichTextBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        resources.ApplyResources(Me.TableLayoutPanel1, "TableLayoutPanel1")
        Me.TableLayoutPanel1.Controls.Add(Me.DescriptionEditor, 0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        resources.ApplyResources(Me.DescriptionEditor, "DescriptionEditor")
        Me.DescriptionEditor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.DescriptionEditor.DoHeightUpdate = False
        Me.DescriptionEditor.FormClosing = False
        Me.DescriptionEditor.Name = "DescriptionEditor"
        Me.DescriptionEditor.Tag = "1"
        Me.CustomPropertyRichTextBindingSource.DataSource = GetType(Questify.Builder.Model.ContentModel.EntityClasses.ConceptStructureCustomBankPropertyEntity)
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "RichTextValueDetailsUserControl"
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.CustomPropertyRichTextBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents CustomPropertyRichTextBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DescriptionEditor As Questify.Builder.UI.ReparentHtmlEditor

End Class
