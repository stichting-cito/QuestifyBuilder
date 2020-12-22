<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class AreaParameterEditorControl
    Inherits CollectionEditorControlBase


    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If (disposing) Then
                If (Not AreaEditor1.IsDisposed) Then
                    If _freeFormResourceParameterEditorControl IsNot Nothing Then
                        RemoveHandler _freeFormResourceParameterEditorControl.AddingResource, AddressOf ResourceChanged
                        RemoveHandler _freeFormResourceParameterEditorControl.RemovingResource, AddressOf OnSvgRemoved
                    Else
                        RemoveHandler _resourceParameterEditorControl.AddingResource, AddressOf ResourceChanged
                        RemoveHandler _resourceParameterEditorControl.ImageSizeChanged, AddressOf ImageSizeChanged
                    End If
                    RemoveHandler Me.AreaEditor1.ShapeRemoved, AddressOf OnShapeRemoved
                    RemoveHandler Me.AreaEditor1.ShapeAdded, AddressOf OnShapeAdded
                    AreaEditor1.Dispose()
                End If
            End If
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AreaParameterEditorControl))
        Me.parameterLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.AreaEditor1 = New AreaEditor()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        CType(Me.ParameterBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        Me.parameterLayoutPanel.AutoSize = True
        Me.parameterLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.parameterLayoutPanel.BackColor = System.Drawing.SystemColors.Control
        Me.parameterLayoutPanel.ColumnCount = 2
        Me.parameterLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.parameterLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.parameterLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.parameterLayoutPanel.Location = New System.Drawing.Point(3, 142)
        Me.parameterLayoutPanel.Name = "parameterLayoutPanel"
        Me.parameterLayoutPanel.RowCount = 1
        Me.parameterLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.parameterLayoutPanel.Size = New System.Drawing.Size(294, 55)
        Me.parameterLayoutPanel.TabIndex = 1002
        Me.parameterLayoutPanel.TabStop = True
        Me.AreaEditor1.AutoSize = True
        Me.AreaEditor1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.AreaEditor1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.AreaEditor1.DefaultShapeSize = New System.Drawing.Size(50, 50)
        Me.AreaEditor1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.AreaEditor1.Location = New System.Drawing.Point(3, 3)
        Me.AreaEditor1.MaxNrOfShapesToCreate = 150
        Me.AreaEditor1.MinimumSize = New System.Drawing.Size(50, 120)
        Me.AreaEditor1.Name = "AreaEditor1"
        Me.AreaEditor1.NewCircleButtonVisible = True
        Me.AreaEditor1.NewRectangleButtonVisible = True
        Me.AreaEditor1.Size = New System.Drawing.Size(294, 133)
        Me.AreaEditor1.TabIndex = 1003
        Me.TableLayoutPanel1.AutoSize = True
        Me.TableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.AreaEditor1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.parameterLayoutPanel, 0, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(300, 200)
        Me.TableLayoutPanel1.TabIndex = 1004
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.MinimumSize = New System.Drawing.Size(300, 200)
        Me.Name = "AreaParameterEditorControl"
        Me.Size = New System.Drawing.Size(300, 200)
        CType(Me.ParameterBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents parameterLayoutPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents AreaEditor1 As AreaEditor
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel

End Class
