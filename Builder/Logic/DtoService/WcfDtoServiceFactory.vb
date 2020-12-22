Imports System.Reflection
Imports System.ServiceModel

Public NotInheritable Class WcfDtoServiceFactory : Implements IDtoServiceFactory

    Const serviceNamingPrefix As String = "BasicHttpBinding_"

    Public Function CreateService(Of T)() As T Implements IDtoServiceFactory.CreateService

        Dim serviceType = GetType(T)

#If DEBUG Then
        Debug.Assert(serviceType.IsInterface, "Type should be an interface")
        Debug.Assert(serviceType.GetCustomAttribute(GetType(ServiceContractAttribute)) IsNot Nothing, "This factory is meant for ServiceContract")
#End If

        Dim name = $"{serviceNamingPrefix}{serviceType.Name}"

        Dim factory As New ChannelFactory(Of T)(name)
        Return factory.CreateChannel()

    End Function


End Class
