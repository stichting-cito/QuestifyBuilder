<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HandleImportConflictDialog
    Inherits Questify.Builder.Client.DialogBase

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(HandleImportConflictDialog))
        Me.BankEntityCollection = New Questify.Builder.Model.ContentModel.HelperClasses.EntityCollection()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.ResourceNameLabel = New System.Windows.Forms.Label()
        Me.ReplaceResourceQuestionLabel = New System.Windows.Forms.Label()
        Me.FillPanel.SuspendLayout
        Me.TableLayoutPanel1.SuspendLayout
        Me.SuspendLayout
        resources.ApplyResources(Me.FillPanel, "FillPanel")
        Me.FillPanel.Controls.Add(Me.TableLayoutPanel1)
        resources.ApplyResources(Me.DialogOkButton, "DialogOkButton")
        resources.ApplyResources(Me.DialogCancelButton, "DialogCancelButton")
        Me.BankEntityCollection.ActiveContext = Nothing
        Me.BankEntityCollection.AllowEdit = true
        Me.BankEntityCollection.AllowNew = true
        Me.BankEntityCollection.AllowRemove = true
        Me.BankEntityCollection.Capacity = 256
        Me.BankEntityCollection.ConcurrencyPredicateFactoryToUse = Nothing
        Me.BankEntityCollection.DoNotPerformAddIfPresent = false
        Me.BankEntityCollection.EntityFactoryToUse = CType(resources.GetObject("BankEntityCollection.EntityFactoryToUse"), SD.LLBLGen.Pro.ORMSupportClasses.IEntityFactory2)
        Me.BankEntityCollection.IsReadOnly = false
        Me.BankEntityCollection.RemovedEntitiesTracker = Nothing
        resources.ApplyResources(Me.TableLayoutPanel1, "TableLayoutPanel1")
        Me.TableLayoutPanel1.Controls.Add(Me.ResourceNameLabel, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.ReplaceResourceQuestionLabel, 0, 1)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        resources.ApplyResources(Me.ResourceNameLabel, "ResourceNameLabel")
        Me.ResourceNameLabel.CausesValidation = false
        Me.ResourceNameLabel.Name = "ResourceNameLabel"
        resources.ApplyResources(Me.ReplaceResourceQuestionLabel, "ReplaceResourceQuestionLabel")
        Me.ReplaceResourceQuestionLabel.Name = "ReplaceResourceQuestionLabel"
        resources.ApplyResources(Me, "$this")
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "HandleImportConflictDialog"
        Me.FillPanel.ResumeLayout(false)
        Me.FillPanel.PerformLayout
        Me.TableLayoutPanel1.ResumeLayout(false)
        Me.TableLayoutPanel1.PerformLayout
        Me.ResumeLayout(false)
        Me.PerformLayout

    End Sub
    Friend WithEvents BankEntityCollection As Questify.Builder.Model.ContentModel.HelperClasses.EntityCollection
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents ResourceNameLabel As Label
    Friend WithEvents ReplaceResourceQuestionLabel As Label
End Class
