
Imports Questify.Builder.Logic.Service.Interfaces

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ItemPreviewContainer
    Inherits System.Windows.Forms.UserControl


    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try

            If disposing Then

                If (ItempreviewCombobox IsNot Nothing) Then
                    RemoveHandler ItempreviewCombobox.SelectionChangeCommitted, AddressOf ItempreviewCombobox_SelectionChangeCommitted

                    For Each item As IItemPreviewHandler In ItempreviewCombobox.Items
                        item.CleanUp()
                    Next
                    ItempreviewCombobox = Nothing
                End If

                If (_previewerChromium IsNot Nothing) Then
                    RemoveHandler _previewerChromium.ItemValidatingRequired, AddressOf ItemValidatingProxy
                    _previewerChromium.Dispose()
                    _previewerChromium = Nothing
                End If

                If (_previewerWord IsNot Nothing) Then
                    RemoveHandler _previewerWord.ItemValidatingRequired, AddressOf ItemValidatingProxy
                    _previewerWord.Dispose()
                    _previewerWord = Nothing
                End If

                If (_assessmentItem IsNot Nothing) Then
                    _assessmentItem = Nothing
                End If

                If (_resourceManager IsNot Nothing) Then
                    _resourceManager = Nothing
                End If

                If _allAvailableItemPreviewHandlers IsNot Nothing Then
                    _allAvailableItemPreviewHandlers.Clear()
                    _allAvailableItemPreviewHandlers = Nothing
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ItemPreviewContainer))
        Me.ValidationFailedLabel = New System.Windows.Forms.Label()
        Me.ItempreviewCombobox = New System.Windows.Forms.ComboBox()
        Me.PreviewerTableLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.RefreshButton = New System.Windows.Forms.Button()
        Me.PreviewerTableLayoutPanel.SuspendLayout
        Me.SuspendLayout
        Me.ValidationFailedLabel.BackColor = System.Drawing.SystemColors.Window
        Me.PreviewerTableLayoutPanel.SetColumnSpan(Me.ValidationFailedLabel, 2)
        resources.ApplyResources(Me.ValidationFailedLabel, "ValidationFailedLabel")
        Me.ValidationFailedLabel.Name = "ValidationFailedLabel"
        resources.ApplyResources(Me.ItempreviewCombobox, "ItempreviewCombobox")
        Me.ItempreviewCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ItempreviewCombobox.FormattingEnabled = true
        Me.ItempreviewCombobox.Name = "ItempreviewCombobox"
        resources.ApplyResources(Me.PreviewerTableLayoutPanel, "PreviewerTableLayoutPanel")
        Me.PreviewerTableLayoutPanel.Controls.Add(Me.ItempreviewCombobox, 0, 0)
        Me.PreviewerTableLayoutPanel.Controls.Add(Me.ValidationFailedLabel, 0, 1)
        Me.PreviewerTableLayoutPanel.Controls.Add(Me.RefreshButton, 1, 0)
        Me.PreviewerTableLayoutPanel.Name = "PreviewerTableLayoutPanel"
        Me.RefreshButton.Image = Global.Questify.Builder.UI.My.Resources.Resources.reload_16x16
        resources.ApplyResources(Me.RefreshButton, "RefreshButton")
        Me.RefreshButton.Name = "RefreshButton"
        Me.RefreshButton.UseVisualStyleBackColor = true
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.PreviewerTableLayoutPanel)
        Me.Name = "ItemPreviewContainer"
        Me.PreviewerTableLayoutPanel.ResumeLayout(false)
        Me.ResumeLayout(false)

    End Sub
    Friend WithEvents ValidationFailedLabel As System.Windows.Forms.Label
    Friend WithEvents ItempreviewCombobox As System.Windows.Forms.ComboBox
    Friend WithEvents PreviewerTableLayoutPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents RefreshButton As System.Windows.Forms.Button

End Class
