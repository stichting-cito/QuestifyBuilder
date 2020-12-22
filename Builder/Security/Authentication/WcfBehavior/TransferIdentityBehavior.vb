Imports System.ServiceModel.Configuration
Imports System.ServiceModel.Dispatcher
Imports System.ServiceModel.Description
Imports System.ServiceModel.Channels

Namespace Authentication.Wcfbehavior


    Public Class TransferIdentityBehavior
        Inherits BehaviorExtensionElement
        Implements IClientMessageInspector, IEndpointBehavior


        <DebuggerStepThrough()>
        Public Sub AfterReceiveReply(ByRef reply As System.ServiceModel.Channels.Message, correlationState As Object) Implements System.ServiceModel.Dispatcher.IClientMessageInspector.AfterReceiveReply

        End Sub

        <DebuggerStepThrough()>
        Public Function BeforeSendRequest(ByRef request As System.ServiceModel.Channels.Message, channel As System.ServiceModel.IClientChannel) As Object Implements System.ServiceModel.Dispatcher.IClientMessageInspector.BeforeSendRequest
            Dim qbPrincipal As TestBuilderPrincipal = TryCast(System.Threading.Thread.CurrentPrincipal, TestBuilderPrincipal)
            If (qbPrincipal IsNot Nothing) Then
                Dim mh = MessageHeader.CreateHeader("QbCredentials", "http://www.Questify.eu", qbPrincipal.Identity)
                request.Headers.Add(mh)
            Else
                Dim s As String = Environment.TickCount.ToString()
            End If
            Return Nothing
        End Function

        Public Sub AddBindingParameters(endpoint As System.ServiceModel.Description.ServiceEndpoint, bindingParameters As System.ServiceModel.Channels.BindingParameterCollection) Implements System.ServiceModel.Description.IEndpointBehavior.AddBindingParameters

        End Sub

        Public Sub ApplyClientBehavior(endpoint As System.ServiceModel.Description.ServiceEndpoint, clientRuntime As System.ServiceModel.Dispatcher.ClientRuntime) Implements System.ServiceModel.Description.IEndpointBehavior.ApplyClientBehavior
            clientRuntime.MessageInspectors.Add(Me)
        End Sub

        Public Sub ApplyDispatchBehavior(endpoint As System.ServiceModel.Description.ServiceEndpoint, endpointDispatcher As System.ServiceModel.Dispatcher.EndpointDispatcher) Implements System.ServiceModel.Description.IEndpointBehavior.ApplyDispatchBehavior

        End Sub

        Public Sub Validate(endpoint As System.ServiceModel.Description.ServiceEndpoint) Implements System.ServiceModel.Description.IEndpointBehavior.Validate

        End Sub

        Public Overrides ReadOnly Property BehaviorType As System.Type
            Get
                Return Me.GetType()
            End Get
        End Property

        Protected Overrides Function CreateBehavior() As Object
            Return Me
        End Function
    End Class

End Namespace

