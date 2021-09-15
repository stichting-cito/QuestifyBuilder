
Imports Questify.Builder.Security
Imports Questify.Builder.Model.ContentModel.EntityClasses


''' <summary>
''' Interface for the TestEditorControlBase class, where it implements all public functions/events/properties of this class. This interface is inherited by all specific property editor 
''' interfaces and therefore a property editor doesn't have to be cast to both the property editor specific interface and the base class.
''' </summary>
Public Interface ITestEditorPropertyEditor

    ''' <summary>
    ''' Gets the error message.
    ''' </summary>
    ''' <value>The error message.</value>
    ReadOnly Property ErrorMessage() As String

    ''' <summary>
    ''' Gets a value indicating whether [contains validation error].
    ''' </summary>
    ''' <value>
    ''' <c>true</c> if [contains validation error]; otherwise, <c>false</c>.
    ''' </value>
    ReadOnly Property ContainsValidationError() As Boolean

    ''' <summary>
    ''' Gets the frame title.
    ''' </summary>
    ''' <value>The frame title.</value>
    ReadOnly Property FrameTitle() As String

    ''' <summary>
    ''' Gets a value indicating whether this editor has fields to fill by the user.
    ''' </summary>
    ''' <value>
    ''' <c>true</c> if this editor has fields to fill by user; otherwise, <c>false</c>.
    ''' </value>
    ReadOnly Property HasFieldsToFillByUser() As Boolean

    ''' <summary>
    ''' Gets or sets the test entity.
    ''' </summary>
    ''' <value>The test entity.</value>
    Property TestEntity() As AssessmentTestResourceEntity

    ''' <summary>
    ''' Resets the datasource of this property editor
    ''' </summary>
    Sub ResetDatasource()

    ''' <summary>
    ''' Handles the test design permission change.
    ''' </summary>
    ''' <param name="permission">The permission.</param>
    Sub HandleTestDesignPermissionChange(ByVal permission As TestDesignPermission)

    ''' <summary>
    ''' Occurs when data is changed in this property editor.
    ''' </summary>
    Event DataChanged As EventHandler(Of EventArgs)

    ''' <summary>
    ''' This event will be raised when an action is initiated from a property editor.
    ''' </summary>
    Event CommandExecuteRequest As EventHandler(Of CommandExecuteRequestEventArgs)
End Interface
