Friend Class PreviewHandlerInstanceInfo


    Private _clsid As Guid
    Private _instance As Object
    Private _isInitialized As Boolean = False



    Public Sub New(ByVal clsid As Guid, ByVal instance As Object)
        _clsid = clsid
        _instance = instance
    End Sub



    Public ReadOnly Property Clsid() As Guid
        Get
            Return _clsid
        End Get
    End Property

    Public ReadOnly Property Instance() As Object
        Get
            Return _instance
        End Get
    End Property

    Public ReadOnly Property PreviewHandlerInstance() As IPreviewHandler
        Get
            If TypeOf Me.Instance Is IPreviewHandler Then
                Return DirectCast(Me.Instance, IPreviewHandler)
            Else
                Return Nothing
            End If
        End Get
    End Property

    Public ReadOnly Property IsInitialized() As Boolean
        Get
            Return _isInitialized
        End Get
    End Property

    Public ReadOnly Property SupportsFileInit() As Boolean
        Get
            Return (TypeOf _instance Is IInitializeWithFile)
        End Get
    End Property

    Public ReadOnly Property SupportsStreamInit() As Boolean
        Get
            Return (TypeOf _instance Is IInitializeWithStream)
        End Get
    End Property



    Public Function Initialize(ByVal stream As Runtime.InteropServices.ComTypes.IStream) As Boolean
        If Not Me.SupportsStreamInit Then
            Throw New InvalidOperationException("Preview Handler does not support initialization using streams.")
        End If
        If Me.IsInitialized Then
            Throw New InvalidOperationException("Already initialzed.")
        End If

        Dim streamInit As IInitializeWithStream = DirectCast(Me.Instance, IInitializeWithStream)
        streamInit.Initialize(stream, 0)

        Me._isInitialized = True

        Return Me.IsInitialized
    End Function

    Public Function Initialize(ByVal filename As String) As Boolean
        If Not Me.SupportsFileInit Then
            Throw New InvalidOperationException("Preview Handler does not support initialization using filepaths.")
        End If

        If Me.IsInitialized Then
            Throw New InvalidOperationException("Already initialzed.")
        End If

        Dim fileInit As IInitializeWithFile = DirectCast(Me.Instance, IInitializeWithFile)
        fileInit.Initialize(filename, 0)

        Me._isInitialized = True

        Return Me.IsInitialized
    End Function


End Class