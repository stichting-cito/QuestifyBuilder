<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ItemEditorControlBase
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ItemEditorControlBase))
        Me.AssessmentItemBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.AssessementItemErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        CType(Me.AssessmentItemBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AssessementItemErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        Me.AssessmentItemBindingSource.DataSource = GetType(Cito.Tester.ContentModel.AssessmentItem)
        Me.AssessementItemErrorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink
        Me.AssessementItemErrorProvider.ContainerControl = Me
        Me.AssessementItemErrorProvider.DataSource = Me.AssessmentItemBindingSource
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Name = "ItemEditorControlBase"
        CType(Me.AssessmentItemBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AssessementItemErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents AssessementItemErrorProvider As System.Windows.Forms.ErrorProvider

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")> _
    Public WithEvents AssessmentItemBindingSource As System.Windows.Forms.BindingSource

End Class
