Imports Enums

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CustomPropertyGrid
    Inherits GridInUseAsItemGridBase

    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim GridControl_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CustomPropertyGrid))
        CType(Me.GridControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        Me.GridControl.AllowColumnDrag = True
        Me.GridControl.AlternatingRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        resources.ApplyResources(GridControl_DesignTimeLayout, "GridControl_DesignTimeLayout")
        Me.GridControl.DesignTimeLayout = GridControl_DesignTimeLayout
        Me.GridControl.FocusCellFormatStyle.BackColor = System.Drawing.SystemColors.Highlight
        Me.GridControl.FocusCellFormatStyle.ForeColor = System.Drawing.Color.White
        Me.GridControl.GroupRowFormatStyle.FontBold = Janus.Windows.GridEX.TriState.[True]
        Me.GridControl.HideColumnsWhenGrouped = Janus.Windows.GridEX.InheritableBoolean.[True]
        resources.ApplyResources(Me.GridControl, "GridControl")
        resources.ApplyResources(Me, "$this")
        Me.CustomPropertyFilter = ResourceTypeEnum.None
        Me.DataSource = GetType(Questify.Builder.Model.ContentModel.EntityClasses.CustomBankPropertyEntity)
        Me.Name = "CustomPropertyGrid"
        CType(Me.GridControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

End Class
