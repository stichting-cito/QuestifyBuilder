Imports Cito.Tester.Common
Imports Questify.Builder.Logic.PluginExtensibility.Html.EditBehavior
Imports Questify.Builder.Model.ContentModel
Imports Questify.Builder.Model.ContentModel.EntityClasses

Public Class RichTextValueEditor


    Private _behaviour As CustomPropertyRichTextBehavior
    Private _richTextCustomBankProperty As EntityClasses.RichTextValueCustomBankPropertyEntity
    Private _richTextCustomBankPropertyValue As EntityClasses.RichTextValueCustomBankPropertyValueEntity



    Public Property RichTextCustomBankProperty() As EntityClasses.RichTextValueCustomBankPropertyEntity
        Get
            Return _richTextCustomBankProperty
        End Get
        Set(ByVal value As EntityClasses.RichTextValueCustomBankPropertyEntity)
            _richTextCustomBankProperty = value

            Me.Text = String.Format(My.Resources.RichTextValueEditorText, _richTextCustomBankProperty.Name)
        End Set
    End Property

    Public Property RichTextCustomBankPropertyValue() As EntityClasses.RichTextValueCustomBankPropertyValueEntity
        Get
            Return _richTextCustomBankPropertyValue
        End Get
        Set(ByVal value As EntityClasses.RichTextValueCustomBankPropertyValueEntity)
            _richTextCustomBankPropertyValue = value
        End Set
    End Property



    Public Sub InitEditor(resourceEntity As ResourceEntity, resourceManager As ResourceManagerBase, contextIdentifier As Integer?)
        _behaviour = New CustomPropertyRichTextBehavior(resourceEntity, resourceManager, contextIdentifier, RichTextCustomBankPropertyValue)
        DescriptionEditor.Initialize(_behaviour)
    End Sub

    Private Sub UpdateDescription()
        DescriptionEditor.StopEditor()
    End Sub

    Private Sub ButtonOK_Click(sender As Object, e As EventArgs) Handles ButtonOK.Click
        UpdateDescription()
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub


End Class