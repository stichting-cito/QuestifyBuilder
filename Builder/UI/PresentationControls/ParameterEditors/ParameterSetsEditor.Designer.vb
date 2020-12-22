<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ParameterSetsEditor
    Inherits System.Windows.Forms.UserControl

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing Then
                ParameterErrorProvider.Dispose()

                If ResourceManager IsNot Nothing Then
                    If TypeOf ResourceManager Is IDisposable Then
                        DirectCast(ResourceManager, IDisposable).Dispose()
                    End If
                    ResourceManager = Nothing
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

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ParameterSetsEditor))
        Me.ParameterErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        CType(Me.ParameterErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        Me.ParameterErrorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink
        Me.ParameterErrorProvider.ContainerControl = Me
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.MinimumSize = New System.Drawing.Size(100, 100)
        Me.Name = "ParameterSetsEditor"
        CType(Me.ParameterErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ParameterErrorProvider As System.Windows.Forms.ErrorProvider

End Class
