<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class XhtmlGenericResourceViewer
    Inherits Questify.Builder.UI.GenericResourceViewerBase

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(XhtmlGenericResourceViewer))
        Me.GenericResourceEntityBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ReparentHtmlEditor1 = New Questify.Builder.UI.ReparentHtmlEditor()
        CType(Me.GenericResourceEntityBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        Me.GenericResourceEntityBindingSource.DataSource = GetType(Questify.Builder.Model.ContentModel.EntityClasses.GenericResourceEntity)
        Me.ReparentHtmlEditor1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        resources.ApplyResources(Me.ReparentHtmlEditor1, "ReparentHtmlEditor1")
        Me.ReparentHtmlEditor1.Name = "ReparentHtmlEditor1"
        resources.ApplyResources(Me, "$this")
        Me.Controls.Add(Me.ReparentHtmlEditor1)
        Me.Name = "XhtmlGenericResourceViewer"
        CType(Me.GenericResourceEntityBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GenericResourceEntityBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ReparentHtmlEditor1 As Questify.Builder.UI.ReparentHtmlEditor

End Class
