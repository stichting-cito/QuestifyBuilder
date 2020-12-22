Imports System.ComponentModel
Imports Questify.Builder.Model.ContentModel.FactoryClasses
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports Microsoft.VisualBasic.CompilerServices

<DesignerGenerated()> _
Partial Class AddBank
    Inherits DialogBase

    <DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    Private components As IContainer

    <DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New Container()
        Dim resources As ComponentResourceManager = New ComponentResourceManager(GetType(AddBank))
        Me.NameLabel = New Label()
        Me.NameTextBox = New TextBox()
        Me.BankEntityCollection = New EntityCollection()
        Me.ErrorProvider1 = New ErrorProvider(Me.components)
        Me.FillPanel.SuspendLayout()
        CType(Me.ErrorProvider1, ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        Me.FillPanel.Controls.Add(Me.NameTextBox)
        Me.FillPanel.Controls.Add(Me.NameLabel)
        resources.ApplyResources(Me.FillPanel, "FillPanel")
        resources.ApplyResources(Me.DialogOkButton, "DialogOkButton")
        resources.ApplyResources(Me.DialogCancelButton, "DialogCancelButton")
        resources.ApplyResources(Me.NameLabel, "NameLabel")
        Me.NameLabel.Name = "NameLabel"
        resources.ApplyResources(Me.NameTextBox, "NameTextBox")
        Me.NameTextBox.Name = "NameTextBox"
        Me.BankEntityCollection.ActiveContext = Nothing
        Me.BankEntityCollection.AllowEdit = True
        Me.BankEntityCollection.AllowNew = True
        Me.BankEntityCollection.AllowRemove = True
        Me.BankEntityCollection.Capacity = 256
        Me.BankEntityCollection.ConcurrencyPredicateFactoryToUse = Nothing
        Me.BankEntityCollection.DoNotPerformAddIfPresent = False
        Me.BankEntityCollection.EntityFactoryToUse = New BankEntityFactory()
        Me.BankEntityCollection.IsReadOnly = False
        Me.BankEntityCollection.RemovedEntitiesTracker = Nothing
        Me.ErrorProvider1.ContainerControl = Me
        resources.ApplyResources(Me, "$this")
        Me.AutoValidate = AutoValidate.EnableAllowFocusChange
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "AddBank"
        Me.FillPanel.ResumeLayout(False)
        Me.FillPanel.PerformLayout()
        CType(Me.ErrorProvider1, ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BankEntityCollection As EntityCollection
    Friend WithEvents NameLabel As Label
    Friend WithEvents NameTextBox As TextBox
    Friend WithEvents ErrorProvider1 As ErrorProvider

End Class
