Imports Questify.Builder.Logic.PluginExtensibility.Html.EditBehavior
Imports Questify.Builder.Model.ContentModel.EntityClasses

Public Class RichTextValueDetailsUserControl


    Private _behaviour As CustomPropertyRichTextBehavior
    Private _contentChanged As Boolean = False

    Public Sub New()
        InitializeComponent()

    End Sub

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If

            CustomPropertyRichTextBindingSource.DataSource = Nothing
            CustomPropertyRichTextBindingSource.Dispose()
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Public Overrides Sub Initialize(customBankProperty As CustomBankPropertyEntity, ByVal initAsReadOnly As Boolean)
        MyBase.Initialize(customBankProperty, initAsReadOnly)
        CustomPropertyRichTextBindingSource.DataSource = RichTextCustomBankPropertyEntity
        InitEditor()
    End Sub

    Private Sub InitEditor()
        Dim value As RichTextValueCustomBankPropertyValueEntity = Nothing
        If RichTextCustomBankPropertyEntity.RichTextValueCustomBankPropertyValueCollection IsNot Nothing AndAlso RichTextCustomBankPropertyEntity.RichTextValueCustomBankPropertyValueCollection.Count = 1 Then
            value = RichTextCustomBankPropertyEntity.RichTextValueCustomBankPropertyValueCollection(0)
        Else
            value = New RichTextValueCustomBankPropertyValueEntity(RichTextCustomBankPropertyEntity.CustomBankPropertyId, Guid.NewGuid())
            RichTextCustomBankPropertyEntity.RichTextValueCustomBankPropertyValueCollection.Add(value)
        End If
        _behaviour = New CustomPropertyRichTextBehavior(Nothing, Nothing, Nothing, value)
        DescriptionEditor.Initialize(_behaviour)
    End Sub

    Private ReadOnly Property RichTextCustomBankPropertyEntity As RichTextValueCustomBankPropertyEntity
        Get
            Return CType(CustomBankProperty, RichTextValueCustomBankPropertyEntity)
        End Get
    End Property

    Public Function IsDirty() As Boolean
        Return False
    End Function



End Class
