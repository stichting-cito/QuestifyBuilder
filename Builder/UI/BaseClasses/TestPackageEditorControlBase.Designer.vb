<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TestPackageEditorControlBase
    Inherits TestResourceEventBase

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TestPackageEditorControlBase))
        Me.ControlBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ControlErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        CType(Me.ControlBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ControlErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        Me.ControlErrorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink
        Me.ControlErrorProvider.ContainerControl = Me
        Me.ControlErrorProvider.DataSource = Me.ControlBindingSource
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange
        Me.Name = "TestEditorControlBase"
        CType(Me.ControlBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ControlErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ControlErrorProvider As System.Windows.Forms.ErrorProvider
    Protected Friend WithEvents ControlBindingSource As System.Windows.Forms.BindingSource

End Class
