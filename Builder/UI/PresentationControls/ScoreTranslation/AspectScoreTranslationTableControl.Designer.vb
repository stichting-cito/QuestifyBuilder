<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AspectScoreTranslationTableControl
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
        Dim ScoreTranslationTableGridControl_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AspectScoreTranslationTableControl))
        Me.ScoreTranslationTableGridControl = New Janus.Windows.GridEX.GridEX()
        Me.ScoreTranslationTableBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        CType(Me.ScoreTranslationTableGridControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ScoreTranslationTableBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        Me.ScoreTranslationTableGridControl.AllowColumnDrag = False
        Me.ScoreTranslationTableGridControl.AlternatingColors = True
        Me.ScoreTranslationTableGridControl.ColumnAutoResize = True
        Me.ScoreTranslationTableGridControl.DataSource = Me.ScoreTranslationTableBindingSource
        ScoreTranslationTableGridControl_DesignTimeLayout.LayoutString = resources.GetString("ScoreTranslationTableGridControl_DesignTimeLayout.LayoutString")
        Me.ScoreTranslationTableGridControl.DesignTimeLayout = ScoreTranslationTableGridControl_DesignTimeLayout
        Me.ScoreTranslationTableGridControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ScoreTranslationTableGridControl.FocusCellFormatStyle.BackColor = System.Drawing.SystemColors.Highlight
        Me.ScoreTranslationTableGridControl.FocusCellFormatStyle.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.ScoreTranslationTableGridControl.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.ScoreTranslationTableGridControl.GroupByBoxVisible = False
        Me.ScoreTranslationTableGridControl.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight
        Me.ScoreTranslationTableGridControl.Location = New System.Drawing.Point(3, 3)
        Me.ScoreTranslationTableGridControl.Name = "ScoreTranslationTableGridControl"
        Me.ScoreTranslationTableGridControl.Size = New System.Drawing.Size(287, 35)
        Me.ScoreTranslationTableGridControl.TabIndex = 1
        Me.ScoreTranslationTableBindingSource.DataSource = GetType(Cito.Tester.ContentModel.AspectScoreTranslationTable)
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.Controls.Add(Me.ScoreTranslationTableGridControl)
        Me.Name = "AspectScoreTranslationTableControl"
        Me.Padding = New System.Windows.Forms.Padding(3)
        Me.Size = New System.Drawing.Size(293, 41)
        CType(Me.ScoreTranslationTableGridControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ScoreTranslationTableBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ScoreTranslationTableGridControl As Janus.Windows.GridEX.GridEX
    Friend WithEvents ScoreTranslationTableBindingSource As BindingSource
End Class
