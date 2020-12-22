Public Class DtoServiceFactory

    Private Shared _instance As IDtoServiceFactory

    Public Shared Property Instance As IDtoServiceFactory
        Get
            If (_instance Is Nothing) Then EnsureDefaultService()
            Debug.Assert(_instance IsNot Nothing)
            Return _instance
        End Get
        Set(value As IDtoServiceFactory)
            If (_instance Is Nothing) Then
                _instance = value
            Else
                Throw New InvalidOperationException("Can not set this instance when already instantiated. Please call DestroyInstance")
            End If
        End Set
    End Property

    Public Shared Sub Destroy()
        _instance = Nothing
    End Sub

    Private Shared Sub EnsureDefaultService()
        Instance = New WcfDtoServiceFactory()
    End Sub
End Class
