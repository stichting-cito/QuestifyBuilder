

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ConceptScoringOptions
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ConceptScoringOptions))
        Me.ErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.OptionsValidatorBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.checkboxIncludeConceptsWithoutScore = New System.Windows.Forms.CheckBox()
        Me.SaveFileDialog = New System.Windows.Forms.SaveFileDialog()
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OptionsValidatorBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        Me.ErrorProvider.ContainerControl = Me
        Me.ErrorProvider.DataSource = Me.OptionsValidatorBindingSource
        Me.OptionsValidatorBindingSource.DataSource = GetType(Questify.Builder.UI.SelectColumnsOptionsValidator)
        resources.ApplyResources(Me.TableLayoutPanel1, "TableLayoutPanel1")
        Me.TableLayoutPanel1.Controls.Add(Me.checkboxIncludeConceptsWithoutScore, 0, 2)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        resources.ApplyResources(Me.checkboxIncludeConceptsWithoutScore, "checkboxIncludeConceptsWithoutScore")
        Me.checkboxIncludeConceptsWithoutScore.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.OptionsValidatorBindingSource, "IncludeConceptsWithoutScore", True))
        Me.checkboxIncludeConceptsWithoutScore.Name = "checkboxIncludeConceptsWithoutScore"
        Me.checkboxIncludeConceptsWithoutScore.UseVisualStyleBackColor = True
        resources.ApplyResources(Me.SaveFileDialog, "SaveFileDialog")
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "ConceptScoringOptions"
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OptionsValidatorBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ErrorProvider As System.Windows.Forms.ErrorProvider
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents SaveFileDialog As System.Windows.Forms.SaveFileDialog
    Friend WithEvents OptionsValidatorBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents checkboxIncludeConceptsWithoutScore As System.Windows.Forms.CheckBox

End Class
