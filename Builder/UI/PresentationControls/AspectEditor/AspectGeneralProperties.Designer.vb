<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AspectGeneralProperties
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AspectGeneralProperties))
        Me.TableLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelCode = New System.Windows.Forms.Label()
        Me.LabelTitle = New System.Windows.Forms.Label()
        Me.TextBoxCode = New System.Windows.Forms.TextBox()
        Me.AspectBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.TextBoxTitle = New System.Windows.Forms.TextBox()
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.TableLayoutPanel.SuspendLayout()
        CType(Me.AspectBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        resources.ApplyResources(Me.TableLayoutPanel, "TableLayoutPanel")
        Me.TableLayoutPanel.Controls.Add(Me.LabelCode, 0, 0)
        Me.TableLayoutPanel.Controls.Add(Me.LabelTitle, 0, 1)
        Me.TableLayoutPanel.Controls.Add(Me.TextBoxCode, 1, 0)
        Me.TableLayoutPanel.Controls.Add(Me.TextBoxTitle, 1, 1)
        Me.TableLayoutPanel.Name = "TableLayoutPanel"
        resources.ApplyResources(Me.LabelCode, "LabelCode")
        Me.LabelCode.Name = "LabelCode"
        resources.ApplyResources(Me.LabelTitle, "LabelTitle")
        Me.LabelTitle.Name = "LabelTitle"
        resources.ApplyResources(Me.TextBoxCode, "TextBoxCode")
        Me.TextBoxCode.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.AspectBindingSource, "Identifier", True))
        Me.TextBoxCode.Name = "TextBoxCode"
        Me.AspectBindingSource.DataSource = GetType(Cito.Tester.ContentModel.Aspect)
        resources.ApplyResources(Me.TextBoxTitle, "TextBoxTitle")
        Me.TextBoxTitle.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.AspectBindingSource, "Title", True))
        Me.TextBoxTitle.Name = "TextBoxTitle"
        Me.ErrorProvider1.ContainerControl = Me
        Me.ErrorProvider1.DataSource = Me.AspectBindingSource
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TableLayoutPanel)
        Me.Name = "AspectGeneralProperties"
        Me.TableLayoutPanel.ResumeLayout(False)
        Me.TableLayoutPanel.PerformLayout()
        CType(Me.AspectBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents LabelCode As System.Windows.Forms.Label
    Friend WithEvents LabelTitle As System.Windows.Forms.Label
    Friend WithEvents TextBoxCode As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxTitle As System.Windows.Forms.TextBox
    Friend WithEvents AspectBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider

End Class
