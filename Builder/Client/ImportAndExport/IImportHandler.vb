
Imports Cito.Tester.Common
Imports Questify.Builder.Packaging

Public Interface IImportHandler

    ReadOnly Property UserFriendlyName() As String

    ReadOnly Property ImportFileIsPackage() As Boolean


    ReadOnly Property SupportedResourceTypes() As String

    ReadOnly Property ProgressMessage() As String

    ReadOnly Property GetOptionsUserControl() As ImportOptionControlBase

    Function Import(ByVal sourceResourceManager As ResourceManagerBase, ByVal bankId As Integer) As Boolean

    Function Import(ByVal packageSet As PackageSet, ByVal parentBankId As Integer?) As Boolean

    Function Import(ByVal parentBankId As Integer?) As Boolean

    Event ImportHandlerHandleConflict As EventHandler(Of ImportHandlerHandleConflictEventArgs)

    Event ImportHandlerHandleError As EventHandler(Of ImportExportHandlerHandleErrorEventArgs)

    Event ImportHandlerCustomBankPropertiesRemoved As EventHandler(Of ImportCustomBankPropertiesRemovedArgs)

    Event ImportHandlerHandleWarning As EventHandler(Of ImportExportHandlerHandleWarningEventArgs)

    Event Progress(ByVal sender As Object, ByVal e As ProgressEventArgs)

    Event StartProgress(ByVal sender As Object, ByVal e As StartEventArgs)

End Interface


Public Class ImportHandlerHandleConflictEventArgs
    Inherits EventArgs


    Private ReadOnly _bankName As String = String.Empty
    Private ReadOnly _resourceName As String = String.Empty
    Private ReadOnly _bankIdExistingResource As Integer
    Private ReadOnly _bankContextId As Integer = -1
    Private _cancel As Boolean = True



    Public ReadOnly Property BankName() As String
        Get
            Return _bankName
        End Get
    End Property


    Public ReadOnly Property ResourceName() As String
        Get
            Return _resourceName
        End Get
    End Property

    Public ReadOnly Property BankIdExistingResource() As Integer
        Get
            Return _bankIdExistingResource
        End Get
    End Property

    Public ReadOnly Property BankContextId() As Integer
        Get
            Return _bankContextId
        End Get
    End Property

    Public Property Cancel() As Boolean
        Get
            Return _cancel
        End Get
        Set(ByVal value As Boolean)
            _cancel = value
        End Set
    End Property



    Public Sub New(ByVal resourceName As String, ByVal bankIdExistingResource As Integer, ByVal bankContextId As Integer)
        Me.New(resourceName, bankIdExistingResource, bankContextId, String.Empty)
    End Sub

    Public Sub New(ByVal resourceName As String, ByVal bankIdExistingResource As Integer, ByVal bankContextId As Integer, bankName As String)
        _bankName = bankName
        _resourceName = resourceName
        _bankIdExistingResource = bankIdExistingResource
        _bankContextId = bankContextId
    End Sub


End Class

Public Class ImportExportHandlerHandleErrorEventArgs
    Inherits EventArgs
    Private _errorMessage As String = String.Empty

    Public Sub New(ByVal errorMessage As String)
        _errorMessage = errorMessage
    End Sub


    Public Property ErrorMessage() As String
        Get
            Return _errorMessage
        End Get
        Set(ByVal value As String)
            _errorMessage = value
        End Set
    End Property
End Class

Public Class ImportExportHandlerHandleWarningEventArgs
    Inherits EventArgs

    Public Message As String = String.Empty

    Public Sub New(ByVal message As String)
        Me.Message = message
    End Sub
End Class
