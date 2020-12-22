Public Class CustomActionEventArgs
	Inherits EventArgs

#Region "Fields"

	Private _layoutTemplateName As String
	Private _inlineElementId As String

#End Region	'Fields

#Region "Constructors"

	''' <summary>
	''' Initializes a new instance of the <see cref="CustomActionEventArgs" /> class.
	''' </summary>
	''' <param name="layoutTemplateName">The name.</param>
	''' <history>
	'''    [remcor] 12-04-2012 Created
	''' </history>
	Public Sub New(ByVal layoutTemplateName As String, ByVal inlineElementId As String)
		Me._layoutTemplateName = layoutTemplateName
		Me._inlineElementId = inlineElementId
	End Sub

	''' <summary>
	''' Initializes a new instance of the <see cref="CustomActionEventArgs" /> class.
	''' </summary>
	''' <history>
	'''    [remcor] 12-04-2012 Created
	''' </history>
	Private Sub New()

	End Sub

#End Region	'Constructors

#Region "Public Properties"

	''' <summary>
	''' Gets the id of the inline element.
	''' </summary>
	''' <value>The id of the inline element.</value>
	''' <history>
	'''    [remcor] 13-04-2012 Created
	''' </history>
	Public ReadOnly Property InlineElementId As String
		Get
			Return _inlineElementId
		End Get
	End Property

	''' <summary>
	''' Gets the name of the layout template.
	''' </summary>
	''' <value>The name of the layout template.</value>
	''' <history>
	'''    [remcor] 12-04-2012 Created
	''' </history>
	Public ReadOnly Property LayoutTemplateName() As String
		Get
			Return _layoutTemplateName
		End Get
	End Property

#End Region	'Public Properties

End Class