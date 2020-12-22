<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ListedParameterEditorControl
    Inherits ParameterEditorControlBase

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ListedParameterEditorControl))
        Me.ParameterComboBox = New System.Windows.Forms.ComboBox()
        Me.ListValuesBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        CType(Me.ParameterBindingSource, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.ListValuesBindingSource, System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        Me.ParameterBindingSource.DataSource = GetType(Cito.Tester.ContentModel.ListedParameter)
        Me.ParameterComboBox.DataSource = Me.ListValuesBindingSource
        resources.ApplyResources(Me.ParameterComboBox, "ParameterComboBox")
        Me.ParameterComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ParameterComboBox.FormattingEnabled = true
        Me.ParameterComboBox.Name = "ParameterComboBox"
        Me.ListValuesBindingSource.DataSource = Me.ParameterBindingSource
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.ParameterComboBox)
        Me.Name = "ListedParameterEditorControl"
        CType(Me.ParameterBindingSource, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.ListValuesBindingSource, System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)

    End Sub
    Friend WithEvents ParameterComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents ListValuesBindingSource As System.Windows.Forms.BindingSource

End Class
