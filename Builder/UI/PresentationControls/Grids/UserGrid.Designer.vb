<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UserGrid
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
        Dim UserGridControl_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(UserGrid))
        Me.UserGridControl = New Janus.Windows.GridEX.GridEX
        Me.UsersBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        CType(Me.UserGridControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UsersBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        Me.UserGridControl.AllowColumnDrag = False
        Me.UserGridControl.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.UserGridControl.AlternatingColors = True
        Me.UserGridControl.ColumnAutoResize = True
        Me.UserGridControl.DataSource = Me.UsersBindingSource
        resources.ApplyResources(UserGridControl_DesignTimeLayout, "UserGridControl_DesignTimeLayout")
        Me.UserGridControl.DesignTimeLayout = UserGridControl_DesignTimeLayout
        resources.ApplyResources(Me.UserGridControl, "UserGridControl")
        Me.UserGridControl.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.UserGridControl.FocusCellFormatStyle.BackColor = System.Drawing.SystemColors.Highlight
        Me.UserGridControl.FocusCellFormatStyle.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.UserGridControl.GroupByBoxVisible = False
        Me.UserGridControl.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight
        Me.UserGridControl.Name = "UserGridControl"
        Me.UsersBindingSource.DataSource = GetType(Questify.Builder.Model.ContentModel.EntityClasses.UserEntity)
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.UserGridControl)
        Me.Name = "UserGrid"
        CType(Me.UserGridControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UsersBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents UserGridControl As Janus.Windows.GridEX.GridEX
    Friend WithEvents UsersBindingSource As System.Windows.Forms.BindingSource

End Class
