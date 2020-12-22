Imports MakarovDev.ExpandCollapsePanel

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ParameterGroupBox
    Inherits ExpandCollapsePanel

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If (disposing) Then

                If (TableLayoutControl IsNot Nothing) Then
                    TableLayoutControl.Dispose()
                    TableLayoutControl = Nothing
                End If

                If _toolTips IsNot Nothing Then
                    _toolTips.ForEach(Sub(t) If t IsNot Nothing Then t.Dispose())
                End If
                _toolTips = Nothing

                RemoveHandler BeforeExpandCollapse, AddressOf ParameterGroupBox_BeforeExpandCollapse
                RemoveHandler ExpandCollapse, AddressOf ParameterGroupBox_ExpandCollapse

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ParameterGroupBox))
        Me.TableLayoutControl = New System.Windows.Forms.TableLayoutPanel()
        Me.SuspendLayout()
        resources.ApplyResources(Me.TableLayoutControl, "TableLayoutControl")
        Me.TableLayoutControl.Name = "TableLayoutControl"
        resources.ApplyResources(Me, "$this")
        Me.ButtonSize = MakarovDev.ExpandCollapsePanel.ExpandCollapseButton.ExpandButtonSize.Small
        Me.Controls.Add(Me.TableLayoutControl)
        Me.UseAnimation = False
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutControl As TableLayoutPanel

End Class
