<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AspectEditor
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AspectEditor))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBoxScore = New System.Windows.Forms.TextBox()
        Me.AspectBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.stylesheetTextBox = New System.Windows.Forms.TextBox()
        Me.selectStylesheetButton = New System.Windows.Forms.Button()
        Me.DescriptionEditor = New Questify.Builder.UI.FocusedReparentHtmlEditor()
        Me.AspectErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.AspectBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AspectErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        resources.ApplyResources(Me.TableLayoutPanel1, "TableLayoutPanel1")
        Me.TableLayoutPanel1.Controls.Add(Me.Label1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.TextBoxScore, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label2, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Label3, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.stylesheetTextBox, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.selectStylesheetButton, 2, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.DescriptionEditor, 0, 3)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        resources.ApplyResources(Me.TextBoxScore, "TextBoxScore")
        Me.TextBoxScore.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.AspectBindingSource, "MaxScore", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.TextBoxScore.Name = "TextBoxScore"
        Me.AspectBindingSource.DataSource = GetType(Cito.Tester.ContentModel.Aspect)
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.Name = "Label2"
        resources.ApplyResources(Me.Label3, "Label3")
        Me.Label3.Name = "Label3"
        Me.stylesheetTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.AspectBindingSource, "Stylesheet", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        resources.ApplyResources(Me.stylesheetTextBox, "stylesheetTextBox")
        Me.stylesheetTextBox.Name = "stylesheetTextBox"
        Me.stylesheetTextBox.ReadOnly = True
        resources.ApplyResources(Me.selectStylesheetButton, "selectStylesheetButton")
        Me.selectStylesheetButton.Name = "selectStylesheetButton"
        Me.selectStylesheetButton.UseVisualStyleBackColor = True
        resources.ApplyResources(Me.DescriptionEditor, "DescriptionEditor")
        Me.TableLayoutPanel1.SetColumnSpan(Me.DescriptionEditor, 3)
        Me.DescriptionEditor.Name = "DescriptionEditor"
        Me.DescriptionEditor.Tag = "1"
        Me.AspectErrorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink
        Me.AspectErrorProvider.ContainerControl = Me
        Me.AspectErrorProvider.DataSource = Me.AspectBindingSource
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "AspectEditor"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        CType(Me.AspectBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AspectErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextBoxScore As System.Windows.Forms.TextBox
    Friend WithEvents AspectBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents AspectErrorProvider As System.Windows.Forms.ErrorProvider
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents stylesheetTextBox As System.Windows.Forms.TextBox
    Friend WithEvents selectStylesheetButton As System.Windows.Forms.Button
    Friend WithEvents DescriptionEditor As FocusedReparentHtmlEditor
End Class
