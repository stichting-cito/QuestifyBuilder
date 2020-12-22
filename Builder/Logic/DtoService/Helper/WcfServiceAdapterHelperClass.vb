Imports System.ServiceModel

Public Class WcfServiceAdapterHelperClass


    Public Shared Function IsClientDisposed(client As Object) As Boolean
        If client IsNot Nothing Then
            Dim clientChannel = TryCast(client, IClientChannel)
            If clientChannel IsNot Nothing AndAlso (clientChannel.State = CommunicationState.Closing OrElse clientChannel.State = CommunicationState.Closed) Then
                Return True
            Else
                Return False
            End If
        End If
        Return True
    End Function
End Class
