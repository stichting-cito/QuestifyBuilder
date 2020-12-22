<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SplashScreen
    Inherits System.Windows.Forms.Form


    Protected Overrides Sub Dispose(ByVal disposing As Boolean)

        If disposing Then

            Dim bitmap = Me.BackgroundImage
            bitmap.Dispose()
            Me.BackgroundImage = Nothing

            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub
    Friend WithEvents Version As System.Windows.Forms.Label

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SplashScreen))
        Me.Version = New System.Windows.Forms.Label()
        Me.SuspendLayout
        resources.ApplyResources(Me.Version, "Version")
        Me.Version.BackColor = System.Drawing.Color.White
        Me.Version.ForeColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Version.Name = "Version"
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImage = Global.Questify.Builder.Client.My.Resources.Resources.Splashscreen_Questify_Builder
        Me.ControlBox = false
        Me.Controls.Add(Me.Version)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "SplashScreen"
        Me.ShowInTaskbar = false
        Me.ResumeLayout(false)

    End Sub

End Class
