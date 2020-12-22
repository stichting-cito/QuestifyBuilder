
Imports Questify.Builder.Security
Imports Questify.Builder.Model.ContentModel.EntityClasses


Public Interface ITestEditorPropertyEditor

    ReadOnly Property ErrorMessage() As String

    ReadOnly Property ContainsValidationError() As Boolean

    ReadOnly Property FrameTitle() As String

    ReadOnly Property HasFieldsToFillByUser() As Boolean

    Property TestEntity() As AssessmentTestResourceEntity

    Sub ResetDatasource()

    Sub HandleTestDesignPermissionChange(ByVal permission As TestDesignPermission)

    Event DataChanged As EventHandler(Of EventArgs)

    Event CommandExecuteRequest As EventHandler(Of CommandExecuteRequestEventArgs)
End Interface
