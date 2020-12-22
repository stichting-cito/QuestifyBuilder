

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SelectDates
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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.FromDateLabel = New System.Windows.Forms.Label()
        Me.ToDateLabel = New System.Windows.Forms.Label()
        Me.FromDatePicker = New System.Windows.Forms.DateTimePicker()
        Me.ToDatePicker = New System.Windows.Forms.DateTimePicker()
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.TableLayoutPanel1.SuspendLayout
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.15318!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 68.84682!))
        Me.TableLayoutPanel1.Controls.Add(Me.FromDateLabel, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.ToDateLabel, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.FromDatePicker, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.ToDatePicker, 1, 2)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 4
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(581, 264)
        Me.TableLayoutPanel1.TabIndex = 0
        Me.FromDateLabel.AutoSize = true
        Me.FromDateLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FromDateLabel.Location = New System.Drawing.Point(3, 50)
        Me.FromDateLabel.Name = "FromDateLabel"
        Me.FromDateLabel.Size = New System.Drawing.Size(174, 30)
        Me.FromDateLabel.TabIndex = 0
        Me.FromDateLabel.Text = "From"
        Me.FromDateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToDateLabel.AutoSize = true
        Me.ToDateLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ToDateLabel.Location = New System.Drawing.Point(3, 80)
        Me.ToDateLabel.Name = "ToDateLabel"
        Me.ToDateLabel.Size = New System.Drawing.Size(174, 30)
        Me.ToDateLabel.TabIndex = 1
        Me.ToDateLabel.Text = "To"
        Me.ToDateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.FromDatePicker.Location = New System.Drawing.Point(183, 53)
        Me.FromDatePicker.Name = "FromDatePicker"
        Me.FromDatePicker.Size = New System.Drawing.Size(200, 20)
        Me.FromDatePicker.TabIndex = 2
        Me.ToDatePicker.Location = New System.Drawing.Point(183, 83)
        Me.ToDatePicker.Name = "ToDatePicker"
        Me.ToDatePicker.Size = New System.Drawing.Size(200, 20)
        Me.ToDatePicker.TabIndex = 3
        Me.ErrorProvider1.ContainerControl = Me
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "SelectDates"
        Me.Size = New System.Drawing.Size(587, 270)
        Me.TableLayoutPanel1.ResumeLayout(false)
        Me.TableLayoutPanel1.PerformLayout
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)

    End Sub

    Friend WithEvents TableLayoutPanel1 As Windows.Forms.TableLayoutPanel
    Friend WithEvents FromDateLabel As Windows.Forms.Label
    Friend WithEvents ToDateLabel As Windows.Forms.Label
    Friend WithEvents FromDatePicker As Windows.Forms.DateTimePicker
    Friend WithEvents ToDatePicker As Windows.Forms.DateTimePicker
    Friend WithEvents ErrorProvider1 As Windows.Forms.ErrorProvider
End Class
