
Imports Cito.Tester.Common
Imports Questify.Builder.Packaging

Public Interface IImportHandler

	ReadOnly Property UserFriendlyName() As String

	''' <summary>
	''' Gets the import package.
	''' </summary>
	ReadOnly Property ImportFileIsPackage() As Boolean

	
	''' <summary>
	''' Gets the supported resource types.
	''' </summary>
	ReadOnly Property SupportedResourceTypes() As String

	''' <summary>
	''' Gets the progress message.
	''' </summary>
	''' <value>The progress message.</value>
	ReadOnly Property ProgressMessage() As String

	''' <summary>
	''' Gets the get options user control.
	''' </summary>
	''' <value>The get options user control.</value>
	ReadOnly Property GetOptionsUserControl() As ImportOptionControlBase

	''' <summary>
	''' Imports the specified source resource manager.
	''' </summary>
	''' <param name="sourceResourceManager">The source resource manager.</param>
	Function Import(ByVal sourceResourceManager As ResourceManagerBase, ByVal bankId As Integer) As Boolean

	''' <summary>
	''' Imports the specified package set.
	''' </summary>
	''' <param name="packageSet">The package set.</param>
	''' <param name="parentBankId">The parent bank.</param>
	Function Import(ByVal packageSet As PackageSet, ByVal parentBankId As Integer?) As Boolean

	''' <summary>
	''' Imports the specified package set.
	''' </summary>
	Function Import(ByVal  parentBankId As Integer?) As Boolean

    ''' <summary>
    ''' Occurs when [import handler handle conflict].
    ''' </summary>
    Event ImportHandlerHandleConflict As EventHandler(Of ImportHandlerHandleConflictEventArgs)

	''' <summary>
	''' Occurs when [import handler handle conflict].
	''' </summary>
	Event ImportHandlerHandleError As EventHandler(Of ImportExportHandlerHandleErrorEventArgs)

    ''' <summary>
    ''' Occurs when [import handler removed custom bank properties].
    ''' </summary>
    Event ImportHandlerCustomBankPropertiesRemoved As EventHandler(Of ImportCustomBankPropertiesRemovedArgs)

    ''' <summary>
    ''' Occurs when [import handler handle warning].
    ''' </summary>
    Event ImportHandlerHandleWarning As EventHandler(Of ImportExportHandlerHandleWarningEventArgs)

    ''' <summary>
    ''' Occurs when [progress].
    ''' </summary>
    Event Progress(ByVal sender As Object, ByVal e As ProgressEventArgs)

    ''' <summary>
    ''' Occurs when [start progress].
    ''' </summary>
    Event StartProgress(ByVal sender As Object, ByVal e As StartEventArgs)

End Interface


Public Class ImportHandlerHandleConflictEventArgs
    Inherits EventArgs

#Region " Fields "

    Private ReadOnly _bankName As String = String.Empty
    Private ReadOnly _resourceName As String = String.Empty
    Private ReadOnly _bankIdExistingResource As Integer
    Private ReadOnly _bankContextId As Integer = -1
    Private _cancel As Boolean = True

#End Region

#Region " Properties "

    ''' <summary>
    ''' Gets the name of the bank.
    ''' </summary>
    ''' <value>
    ''' The name of the bank.
    ''' </value>
    Public ReadOnly Property BankName() As String
        Get
            Return _bankName
        End Get
    End Property


    ''' <summary>
    ''' Gets the name of the resource.
    ''' </summary>
    ''' <value>The name of the resource.</value>
    Public ReadOnly Property ResourceName() As String
        Get
            Return _resourceName
        End Get
    End Property

    ''' <summary>
    ''' Gets the bank id existing resource.
    ''' </summary>
    ''' <value>The bank id existing resource.</value>
   Public ReadOnly Property BankIdExistingResource() As Integer
        Get
            Return _bankIdExistingResource
        End Get
    End Property

    ''' <summary>
    ''' Gets the bank context id.
    ''' </summary>
    ''' <value>The bank context id.</value>
    Public ReadOnly Property BankContextId() As Integer
        Get
            Return _bankContextId
        End Get
    End Property

    ''' <summary>
    ''' Gets or sets a value indicating whether this <see cref="ImportHandlerHandleConflictEventArgs" /> is canceled.
    ''' </summary>
    ''' <value><c>true</c> if cancel; otherwise, <c>false</c>.</value>
    Public Property Cancel() As Boolean
        Get
            Return _cancel
        End Get
        Set(ByVal value As Boolean)
            _cancel = value
        End Set
    End Property

#End Region

#Region " Constructors "

    ''' <summary>
    ''' Initializes a new instance of the <see cref="ImportHandlerHandleConflictEventArgs" /> class.
    ''' </summary>
    ''' <param name="resourceName">Name of the resource.</param>
    Public Sub New(ByVal resourceName As String, ByVal bankIdExistingResource As Integer, ByVal bankContextId As Integer)
        Me.New(resourceName, bankIdExistingResource, bankContextId, String.Empty)
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="ImportHandlerHandleConflictEventArgs" /> class.
    ''' </summary>
    ''' <param name="resourceName">Name of the resource.</param>
    ''' <param name="bankIdExistingResource">The bank id existing resource.</param>
    ''' <param name="bankContextId">The bank context id.</param>
    ''' <param name="bankName">Name of the bank.</param>
    Public Sub New(ByVal resourceName As String, ByVal bankIdExistingResource As Integer, ByVal bankContextId As Integer, bankName As String)
        _bankName = bankName
        _resourceName = resourceName
        _bankIdExistingResource = bankIdExistingResource
        _bankContextId = bankContextId
    End Sub

#End Region

End Class

Public Class ImportExportHandlerHandleErrorEventArgs
    Inherits EventArgs
#Region " Fields "
    Private _errorMessage As String = String.Empty

#End Region
#Region " Constructors "
    ''' <summary>
    ''' Initializes a new instance of the <see cref="ImportExportHandlerHandleErrorEventArgs" /> class.
    ''' </summary>
    ''' <param name="errorMessage">The error message.</param>
    Public Sub New(ByVal errorMessage As String)
        _errorMessage = errorMessage
    End Sub
#End Region


#Region " Properties "
    ''' <summary>
    ''' Gets or sets a value indicating whether this <see cref="ImportHandlerHandleConflictEventArgs" /> is canceled.
    ''' </summary>
    ''' <value><c>true</c> if cancel; otherwise, <c>false</c>.</value>
    Public Property ErrorMessage() As String
        Get
            Return _errorMessage
        End Get
        Set(ByVal value As String)
            _errorMessage = value
        End Set
    End Property
#End Region
End Class

Public Class ImportExportHandlerHandleWarningEventArgs
    Inherits EventArgs

    Public Message As String = String.Empty

    Public Sub New(ByVal message As String)
        Me.Message = message
    End Sub
End Class
