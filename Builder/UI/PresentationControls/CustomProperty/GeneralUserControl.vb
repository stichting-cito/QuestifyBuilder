Imports Enums
Imports Questify.Builder.Model.ContentModel.EntityClasses

Public Class GeneralUserControl

    Const APPLICABLE_LABEL_HEIGHT As Integer = 25

    Public Overrides Sub Initialize(ByVal customBankPropertyEntity As CustomBankPropertyEntity, ByVal initAsReadOnly As Boolean)
        MyBase.Initialize(customBankPropertyEntity, initAsReadOnly)
        BindingSourceCustomProperty.DataSource = CustomBankProperty
        CheckBoxScorable.Enabled = CheckBoxPublishable.Checked

        If TypeOf CustomBankProperty Is ListCustomBankPropertyEntity OrElse TypeOf CustomBankProperty Is FreeValueCustomBankPropertyEntity OrElse TypeOf CustomBankProperty Is TreeStructureCustomBankPropertyEntity Then
            ApplicableToMaskControl.Labels.Add("Items", ResourceTypeEnum.ItemResource, True)
            ApplicableToMaskControl.Labels.Add(My.Resources.Tests, ResourceTypeEnum.AssessmentTestResource, True)
            ApplicableToMaskControl.Labels.Add("Media", ResourceTypeEnum.GenericResource, True)
            ApplicableToMaskControl.Height = (APPLICABLE_LABEL_HEIGHT * 3)

            If customBankPropertyEntity.IsNew Then
                CustomBankProperty.ApplicableToMask = CType(ResourceTypeEnum.ItemResource, Integer)
            End If

        ElseIf TypeOf CustomBankProperty Is RichTextValueCustomBankPropertyEntity Then
            ApplicableToMaskControl.Labels.Add("Items", ResourceTypeEnum.ItemResource, True)

            If customBankPropertyEntity.IsNew Then
                CustomBankProperty.ApplicableToMask = CType(ResourceTypeEnum.ItemResource, Integer)
            End If

        ElseIf TypeOf customBankPropertyEntity Is ConceptStructureCustomBankPropertyEntity Then
            TableLayoutPanel.Controls.Remove(LabelApplicable)
            TableLayoutPanel.Controls.Remove(ApplicableToMaskControl)

            TableLayoutPanel.Controls.Remove(LabelPublishable)
            TableLayoutPanel.Controls.Remove(CheckBoxPublishable)
            TableLayoutPanel.Controls.Remove(LabelScorable)
            TableLayoutPanel.Controls.Remove(CheckBoxScorable)
        End If
    End Sub

    Private Sub CheckBoxPublishable_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxPublishable.CheckedChanged
        CheckBoxScorable.Enabled = CheckBoxPublishable.Checked
    End Sub
End Class
