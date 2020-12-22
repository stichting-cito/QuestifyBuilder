<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ChoicesParameterEditorControl
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ChoicesParameterEditorControl))
        Dim AlternativesGrid_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout
        Me.ChoicesBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label1 = New System.Windows.Forms.Label
        Me.NrOfAlternativesComboBox = New System.Windows.Forms.ComboBox
        Me.AlternativesGrid = New Janus.Windows.GridEX.GridEX
        CType(Me.ParameterBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChoicesBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.AlternativesGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        Me.ParameterBindingSource.DataSource = GetType(Cito.Tester.ContentModel.ChoiceCollectionParameter)
        Me.ChoicesBindingSource.DataSource = Me.ParameterBindingSource
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.NrOfAlternativesComboBox)
        resources.ApplyResources(Me.Panel1, "Panel1")
        Me.Panel1.Name = "Panel1"
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        Me.NrOfAlternativesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.NrOfAlternativesComboBox.FormattingEnabled = True
        resources.ApplyResources(Me.NrOfAlternativesComboBox, "NrOfAlternativesComboBox")
        Me.NrOfAlternativesComboBox.Name = "NrOfAlternativesComboBox"
        Me.AlternativesGrid.AutomaticSort = False
        Me.AlternativesGrid.ColumnAutoResize = True
        Me.AlternativesGrid.DataSource = Me.ChoicesBindingSource
        resources.ApplyResources(AlternativesGrid_DesignTimeLayout, "AlternativesGrid_DesignTimeLayout")
        Me.AlternativesGrid.DesignTimeLayout = AlternativesGrid_DesignTimeLayout
        resources.ApplyResources(Me.AlternativesGrid, "AlternativesGrid")
        Me.AlternativesGrid.GroupByBoxVisible = False
        Me.AlternativesGrid.Name = "AlternativesGrid"
        Me.AlternativesGrid.NewRowPosition = Janus.Windows.GridEX.NewRowPosition.BottomRow
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.AlternativesGrid)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "ChoicesParameterEditorControl"
        CType(Me.ParameterBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChoicesBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.AlternativesGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents NrOfAlternativesComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents AlternativesGrid As Janus.Windows.GridEX.GridEX
    Friend WithEvents ChoicesBindingSource As System.Windows.Forms.BindingSource

End Class
