<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class AuthorizationManagementDialog
    Inherits Questify.Builder.UI.PropertyDialogBase

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AuthorizationManagementDialog))
        Me.RolesTabPageOud = New System.Windows.Forms.TabPage()
        Me.RoleGridEX = New Janus.Windows.GridEX.GridEX()
        Me.RoleEntityBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.UsersTabPage = New System.Windows.Forms.TabPage()
        Me.TableLayoutUserTab = New System.Windows.Forms.TableLayoutPanel()
        Me.UserSelectionGrid = New Questify.Builder.UI.UserGrid()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.TransferCreatorModifierButton = New System.Windows.Forms.Button()
        Me.DeleteUserButton = New System.Windows.Forms.Button()
        Me.EditUserButton = New System.Windows.Forms.Button()
        Me.AddUserButton = New System.Windows.Forms.Button()
        Me.ImportButton = New System.Windows.Forms.Button()
        Me.AuthorizationTabControl = New System.Windows.Forms.TabControl()
        Me.ContentPanel.SuspendLayout()
        Me.RolesTabPageOud.SuspendLayout()
        CType(Me.RoleGridEX, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RoleEntityBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UsersTabPage.SuspendLayout()
        Me.TableLayoutUserTab.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.AuthorizationTabControl.SuspendLayout()
        Me.SuspendLayout()
        Me.ContentPanel.Controls.Add(Me.AuthorizationTabControl)
        resources.ApplyResources(Me.ContentPanel, "ContentPanel")
        Me.RolesTabPageOud.Controls.Add(Me.RoleGridEX)
        resources.ApplyResources(Me.RolesTabPageOud, "RolesTabPageOud")
        Me.RolesTabPageOud.Name = "RolesTabPageOud"
        Me.RolesTabPageOud.UseVisualStyleBackColor = True
        Me.RoleGridEX.AllowColumnDrag = False
        Me.RoleGridEX.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.RoleGridEX.AlternatingColors = True
        resources.ApplyResources(Me.RoleGridEX, "RoleGridEX")
        Me.RoleGridEX.CausesValidation = False
        Me.RoleGridEX.DataSource = Me.RoleEntityBindingSource
        Me.RoleGridEX.GroupByBoxVisible = False
        Me.RoleGridEX.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight
        Me.RoleGridEX.Hierarchical = True
        Me.RoleGridEX.Name = "RoleGridEX"
        Me.RoleGridEX.RepeatHeaders = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.RoleEntityBindingSource.DataSource = GetType(Questify.Builder.Model.ContentModel.EntityClasses.RoleEntity)
        Me.UsersTabPage.Controls.Add(Me.TableLayoutUserTab)
        resources.ApplyResources(Me.UsersTabPage, "UsersTabPage")
        Me.UsersTabPage.Name = "UsersTabPage"
        Me.UsersTabPage.UseVisualStyleBackColor = True
        resources.ApplyResources(Me.TableLayoutUserTab, "TableLayoutUserTab")
        Me.TableLayoutUserTab.Controls.Add(Me.UserSelectionGrid, 0, 0)
        Me.TableLayoutUserTab.Controls.Add(Me.TableLayoutPanel1, 0, 1)
        Me.TableLayoutUserTab.Name = "TableLayoutUserTab"
        Me.UserSelectionGrid.DataSource = Nothing
        resources.ApplyResources(Me.UserSelectionGrid, "UserSelectionGrid")
        Me.UserSelectionGrid.Name = "UserSelectionGrid"
        Me.UserSelectionGrid.SelectedUser = Nothing
        resources.ApplyResources(Me.TableLayoutPanel1, "TableLayoutPanel1")
        Me.TableLayoutPanel1.Controls.Add(Me.TransferCreatorModifierButton, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.DeleteUserButton, 4, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.EditUserButton, 3, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.AddUserButton, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.ImportButton, 1, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        resources.ApplyResources(Me.TransferCreatorModifierButton, "TransferCreatorModifierButton")
        Me.TransferCreatorModifierButton.Name = "TransferCreatorModifierButton"
        Me.TransferCreatorModifierButton.UseVisualStyleBackColor = True
        resources.ApplyResources(Me.DeleteUserButton, "DeleteUserButton")
        Me.DeleteUserButton.Name = "DeleteUserButton"
        Me.DeleteUserButton.UseVisualStyleBackColor = True
        resources.ApplyResources(Me.EditUserButton, "EditUserButton")
        Me.EditUserButton.Name = "EditUserButton"
        Me.EditUserButton.UseVisualStyleBackColor = True
        resources.ApplyResources(Me.AddUserButton, "AddUserButton")
        Me.AddUserButton.Name = "AddUserButton"
        Me.AddUserButton.UseVisualStyleBackColor = True
        resources.ApplyResources(Me.ImportButton, "ImportButton")
        Me.ImportButton.Name = "ImportButton"
        Me.ImportButton.UseVisualStyleBackColor = True
        Me.AuthorizationTabControl.Controls.Add(Me.UsersTabPage)
        Me.AuthorizationTabControl.Controls.Add(Me.RolesTabPageOud)
        resources.ApplyResources(Me.AuthorizationTabControl, "AuthorizationTabControl")
        Me.AuthorizationTabControl.Name = "AuthorizationTabControl"
        Me.AuthorizationTabControl.SelectedIndex = 0
        Me.ApplyEnabled = True
        resources.ApplyResources(Me, "$this")
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable
        Me.MaximizeBox = True
        Me.Name = "AuthorizationManagementDialog"
        Me.ContentPanel.ResumeLayout(False)
        Me.RolesTabPageOud.ResumeLayout(False)
        CType(Me.RoleGridEX, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RoleEntityBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UsersTabPage.ResumeLayout(False)
        Me.TableLayoutUserTab.ResumeLayout(False)
        Me.TableLayoutUserTab.PerformLayout()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.AuthorizationTabControl.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RolesTabPageOud As System.Windows.Forms.TabPage
    Friend WithEvents UsersTabPage As System.Windows.Forms.TabPage
    Friend WithEvents UserSelectionGrid As Questify.Builder.UI.UserGrid
    Friend WithEvents AuthorizationTabControl As System.Windows.Forms.TabControl
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents EditUserButton As System.Windows.Forms.Button
    Friend WithEvents AddUserButton As System.Windows.Forms.Button
    Friend WithEvents RoleGridEX As Janus.Windows.GridEX.GridEX
    Friend WithEvents RoleEntityBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ImportButton As System.Windows.Forms.Button
    Friend WithEvents DeleteUserButton As System.Windows.Forms.Button
    Friend WithEvents TableLayoutUserTab As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TransferCreatorModifierButton As System.Windows.Forms.Button
End Class
