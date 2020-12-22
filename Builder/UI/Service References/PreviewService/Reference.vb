
Option Strict On
Option Explicit On


Namespace PreviewService

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"), _
     System.ServiceModel.ServiceContractAttribute(ConfigurationName:="PreviewService.IItemPreviewService")> _
    Public Interface IItemPreviewService

        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/IItemPreviewService/PreviewItemByCode", ReplyAction:="http://tempuri.org/IItemPreviewService/PreviewItemByCodeResponse")> _
        Function PreviewItemByCode(ByVal itemHandlerType As String, ByVal target As String, ByVal bankId As Integer, ByVal itemCode As String, ByVal isDebug As Boolean, ByVal publicationProperties As System.Collections.Generic.List(Of Questify.Builder.Logic.Service.Classes.PublicationProperty)) As Questify.Builder.Logic.Service.Classes.PublicationResult

        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/IItemPreviewService/PreviewItemByCode", ReplyAction:="http://tempuri.org/IItemPreviewService/PreviewItemByCodeResponse")> _
        Function PreviewItemByCodeAsync(ByVal itemHandlerType As String, ByVal target As String, ByVal bankId As Integer, ByVal itemCode As String, ByVal isDebug As Boolean, ByVal publicationProperties As System.Collections.Generic.List(Of Questify.Builder.Logic.Service.Classes.PublicationProperty)) As System.Threading.Tasks.Task(Of Questify.Builder.Logic.Service.Classes.PublicationResult)

        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/IItemPreviewService/PreviewItemByAssessmentItem", ReplyAction:="http://tempuri.org/IItemPreviewService/PreviewItemByAssessmentItemResponse")> _
        Function PreviewItemByAssessmentItem(ByVal itemHandlerType As String, ByVal target As String, ByVal bankId As Integer, ByVal assessmentItem() As Byte, ByVal isDebug As Boolean, ByVal publicationProperties As System.Collections.Generic.List(Of Questify.Builder.Logic.Service.Classes.PublicationProperty)) As Questify.Builder.Logic.Service.Classes.PublicationResult

        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/IItemPreviewService/PreviewItemByAssessmentItem", ReplyAction:="http://tempuri.org/IItemPreviewService/PreviewItemByAssessmentItemResponse")> _
        Function PreviewItemByAssessmentItemAsync(ByVal itemHandlerType As String, ByVal target As String, ByVal bankId As Integer, ByVal assessmentItem() As Byte, ByVal isDebug As Boolean, ByVal publicationProperties As System.Collections.Generic.List(Of Questify.Builder.Logic.Service.Classes.PublicationProperty)) As System.Threading.Tasks.Task(Of Questify.Builder.Logic.Service.Classes.PublicationResult)
    End Interface

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")> _
    Public Interface IItemPreviewServiceChannel
        Inherits PreviewService.IItemPreviewService, System.ServiceModel.IClientChannel
    End Interface

    <System.Diagnostics.DebuggerStepThroughAttribute(), _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")> _
    Partial Public Class ItemPreviewServiceClient
        Inherits System.ServiceModel.ClientBase(Of PreviewService.IItemPreviewService)
        Implements PreviewService.IItemPreviewService

        Public Sub New()
            MyBase.New
        End Sub

        Public Sub New(ByVal endpointConfigurationName As String)
            MyBase.New(endpointConfigurationName)
        End Sub

        Public Sub New(ByVal endpointConfigurationName As String, ByVal remoteAddress As String)
            MyBase.New(endpointConfigurationName, remoteAddress)
        End Sub

        Public Sub New(ByVal endpointConfigurationName As String, ByVal remoteAddress As System.ServiceModel.EndpointAddress)
            MyBase.New(endpointConfigurationName, remoteAddress)
        End Sub

        Public Sub New(ByVal binding As System.ServiceModel.Channels.Binding, ByVal remoteAddress As System.ServiceModel.EndpointAddress)
            MyBase.New(binding, remoteAddress)
        End Sub

        Public Function PreviewItemByCode(ByVal itemHandlerType As String, ByVal target As String, ByVal bankId As Integer, ByVal itemCode As String, ByVal isDebug As Boolean, ByVal publicationProperties As System.Collections.Generic.List(Of Questify.Builder.Logic.Service.Classes.PublicationProperty)) As Questify.Builder.Logic.Service.Classes.PublicationResult Implements PreviewService.IItemPreviewService.PreviewItemByCode
            Return MyBase.Channel.PreviewItemByCode(itemHandlerType, target, bankId, itemCode, isDebug, publicationProperties)
        End Function

        Public Function PreviewItemByCodeAsync(ByVal itemHandlerType As String, ByVal target As String, ByVal bankId As Integer, ByVal itemCode As String, ByVal isDebug As Boolean, ByVal publicationProperties As System.Collections.Generic.List(Of Questify.Builder.Logic.Service.Classes.PublicationProperty)) As System.Threading.Tasks.Task(Of Questify.Builder.Logic.Service.Classes.PublicationResult) Implements PreviewService.IItemPreviewService.PreviewItemByCodeAsync
            Return MyBase.Channel.PreviewItemByCodeAsync(itemHandlerType, target, bankId, itemCode, isDebug, publicationProperties)
        End Function

        Public Function PreviewItemByAssessmentItem(ByVal itemHandlerType As String, ByVal target As String, ByVal bankId As Integer, ByVal assessmentItem() As Byte, ByVal isDebug As Boolean, ByVal publicationProperties As System.Collections.Generic.List(Of Questify.Builder.Logic.Service.Classes.PublicationProperty)) As Questify.Builder.Logic.Service.Classes.PublicationResult Implements PreviewService.IItemPreviewService.PreviewItemByAssessmentItem
            Return MyBase.Channel.PreviewItemByAssessmentItem(itemHandlerType, target, bankId, assessmentItem, isDebug, publicationProperties)
        End Function

        Public Function PreviewItemByAssessmentItemAsync(ByVal itemHandlerType As String, ByVal target As String, ByVal bankId As Integer, ByVal assessmentItem() As Byte, ByVal isDebug As Boolean, ByVal publicationProperties As System.Collections.Generic.List(Of Questify.Builder.Logic.Service.Classes.PublicationProperty)) As System.Threading.Tasks.Task(Of Questify.Builder.Logic.Service.Classes.PublicationResult) Implements PreviewService.IItemPreviewService.PreviewItemByAssessmentItemAsync
            Return MyBase.Channel.PreviewItemByAssessmentItemAsync(itemHandlerType, target, bankId, assessmentItem, isDebug, publicationProperties)
        End Function
    End Class
End Namespace
