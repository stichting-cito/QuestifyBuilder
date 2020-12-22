Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports System.Linq

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ResourceCustomProperties
    Inherits System.Windows.Forms.UserControl

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing Then
                If _customProperties IsNot Nothing Then
                    CustomPropertiesTableLayoutPanel.SuspendLayout()

                    For Each customProperty As CustomBankPropertyEntity In _customProperties
                        Dim cont As Boolean = False

                        If CustomPropertyFilter IsNot Nothing Then
                            If customProperty.Name = CustomPropertyFilter.Name Then
                                cont = True
                            End If
                        Else
                            cont = True
                        End If

                        If cont Then
                            Select Case customProperty.GetType.ToString
                                Case GetType(FreeValueCustomBankPropertyEntity).ToString
                                Case GetType(ListCustomBankPropertyEntity).ToString
                                    For Each cmd As Button In CustomPropertiesTableLayoutPanel.Controls.OfType(Of Button).Where(Function(tb) tb.Tag IsNot Nothing AndAlso tb.Tag Is customProperty)
                                        RemoveHandler cmd.Click, AddressOf EditListValueButton_Click
                                        cmd.Dispose()
                                    Next
                                Case GetType(TreeStructureCustomBankPropertyEntity).ToString
                                    For Each cmd As Button In CustomPropertiesTableLayoutPanel.Controls.OfType(Of Button).Where(Function(tb) tb.Tag IsNot Nothing AndAlso tb.Tag Is customProperty)
                                        RemoveHandler cmd.Click, AddressOf EditTreeStructureButton_Click
                                        cmd.Dispose()
                                    Next
                                Case GetType(RichTextValueCustomBankPropertyEntity).ToString
                                    For Each cmd As Button In CustomPropertiesTableLayoutPanel.Controls.OfType(Of Button).Where(Function(tb) tb.Tag IsNot Nothing AndAlso tb.Tag Is customProperty)
                                        RemoveHandler cmd.Click, AddressOf EditRichTextValueButton_Click
                                        cmd.Dispose()
                                    Next
                            End Select
                        End If
                    Next
                End If

                If components IsNot Nothing Then
                    components.Dispose()
                End If


                _entity = Nothing
                If _removedEntities IsNot Nothing Then
                    _removedEntities.Dispose()
                    _removedEntities = Nothing
                End If
                _customPropertyValuesBeforeEditing = Nothing
                If _customProperties IsNot Nothing Then
                    _customProperties.Dispose()
                    _customProperties = Nothing
                End If
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ResourceCustomProperties))
        Me.CustomPropertiesTableLayoutPanel = New System.Windows.Forms.TableLayoutPanel
        Me.CustomBankPropertiesBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        CType(Me.CustomBankPropertiesBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        resources.ApplyResources(Me.CustomPropertiesTableLayoutPanel, "CustomPropertiesTableLayoutPanel")
        Me.CustomPropertiesTableLayoutPanel.Name = "CustomPropertiesTableLayoutPanel"
        Me.CustomBankPropertiesBindingSource.DataSource = GetType(Questify.Builder.Model.ContentModel.EntityClasses.CustomBankPropertyEntity)
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.CustomPropertiesTableLayoutPanel)
        Me.Name = "ResourceCustomProperties"
        CType(Me.CustomBankPropertiesBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents CustomBankPropertiesBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents CustomPropertiesTableLayoutPanel As System.Windows.Forms.TableLayoutPanel

End Class
