
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Logic.Service.Interfaces
Imports Questify.Builder.UI.Wpf.Service
Imports Questify.Builder.UI.Wpf.Service.Interfaces

Namespace Forms.Services

    Class ResourceEditorService
        Implements IResourceEditorService

        Private ReadOnly _windowFacade As IWindowFacade = New WindowFacade()

        Public Sub Edit(resourceId As Guid, mediaType As string) Implements IResourceEditorService.Edit
            Dim openSourceTextEditor As Boolean = (mediaType = "application/xhtml+xml" OrElse mediaType = "text/plain")

            If openSourceTextEditor Then
                _windowFacade.OpenSourceTextEditorDialogById(resourceId)
            Else
                Dim dlg As New GenericResourceEditor(resourceId)

                AddHandler dlg.MetaData.OpenResourcePropertyDialogButtonClicked, Sub()
                                                                                     _windowFacade.OpenResourcePropertyDialog(resourceId, GetType(GenericResourceEntity), 4)
                                                                                 End Sub
                dlg.ShowDialog()
            End If
        End Sub

    End Class

End Namespace