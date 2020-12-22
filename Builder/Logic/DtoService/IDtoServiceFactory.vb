Public Interface IDtoServiceFactory

    Function CreateService(Of T)() As T

End Interface
