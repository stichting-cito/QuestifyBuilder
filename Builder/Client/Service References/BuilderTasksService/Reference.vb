
Option Strict On
Option Explicit On


Namespace BuilderTasksService

    <System.Diagnostics.DebuggerStepThroughAttribute(), _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0"), _
     System.Runtime.Serialization.DataContractAttribute(Name:="BuilderTaskSessionTicket", [Namespace]:="http://schemas.datacontract.org/2004/07/Questify.Builder.Services.TasksService.Ta" & _
        "skClasses"), _
     System.SerializableAttribute()> _
    Partial Public Class BuilderTaskSessionTicket
        Inherits Object
        Implements System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged

        <System.NonSerializedAttribute()> _
        Private extensionDataField As System.Runtime.Serialization.ExtensionDataObject

        <System.Runtime.Serialization.OptionalFieldAttribute()> _
        Private IdField As System.Guid

        <Global.System.ComponentModel.BrowsableAttribute(false)> _
        Public Property ExtensionData() As System.Runtime.Serialization.ExtensionDataObject Implements System.Runtime.Serialization.IExtensibleDataObject.ExtensionData
            Get
                Return Me.extensionDataField
            End Get
            Set
                Me.extensionDataField = value
            End Set
        End Property

        <System.Runtime.Serialization.DataMemberAttribute()> _
        Public Property Id() As System.Guid
            Get
                Return Me.IdField
            End Get
            Set
                If (Me.IdField.Equals(value) <> true) Then
                    Me.IdField = value
                    Me.RaisePropertyChanged("Id")
                End If
            End Set
        End Property

        Public Event PropertyChanged As System.ComponentModel.PropertyChangedEventHandler Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged

        Protected Sub RaisePropertyChanged(ByVal propertyName As String)
            Dim propertyChanged As System.ComponentModel.PropertyChangedEventHandler = Me.PropertyChangedEvent
            If (Not (propertyChanged) Is Nothing) Then
                propertyChanged(Me, New System.ComponentModel.PropertyChangedEventArgs(propertyName))
            End If
        End Sub
    End Class

    <System.Diagnostics.DebuggerStepThroughAttribute(), _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0"), _
     System.Runtime.Serialization.DataContractAttribute(Name:="BuilderTaskProgress", [Namespace]:="http://schemas.datacontract.org/2004/07/Questify.Builder.Services.TasksService.Ta" & _
        "skClasses"), _
     System.SerializableAttribute()> _
    Partial Public Class BuilderTaskProgress
        Inherits Object
        Implements System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged

        <System.NonSerializedAttribute()> _
        Private extensionDataField As System.Runtime.Serialization.ExtensionDataObject

        <System.Runtime.Serialization.OptionalFieldAttribute()> _
        Private ProgressItemsField As System.Collections.Generic.List(Of BuilderTasksService.BuilderTaskProgressItem)

        <System.Runtime.Serialization.OptionalFieldAttribute()> _
        Private StateField As BuilderTasksService.BuilderTaskProgress.ExecutionState

        <Global.System.ComponentModel.BrowsableAttribute(false)> _
        Public Property ExtensionData() As System.Runtime.Serialization.ExtensionDataObject Implements System.Runtime.Serialization.IExtensibleDataObject.ExtensionData
            Get
                Return Me.extensionDataField
            End Get
            Set
                Me.extensionDataField = value
            End Set
        End Property

        <System.Runtime.Serialization.DataMemberAttribute()> _
        Public Property ProgressItems() As System.Collections.Generic.List(Of BuilderTasksService.BuilderTaskProgressItem)
            Get
                Return Me.ProgressItemsField
            End Get
            Set
                If (Object.ReferenceEquals(Me.ProgressItemsField, value) <> true) Then
                    Me.ProgressItemsField = value
                    Me.RaisePropertyChanged("ProgressItems")
                End If
            End Set
        End Property

        <System.Runtime.Serialization.DataMemberAttribute()> _
        Public Property State() As BuilderTasksService.BuilderTaskProgress.ExecutionState
            Get
                Return Me.StateField
            End Get
            Set
                If (Me.StateField.Equals(value) <> true) Then
                    Me.StateField = value
                    Me.RaisePropertyChanged("State")
                End If
            End Set
        End Property

        Public Event PropertyChanged As System.ComponentModel.PropertyChangedEventHandler Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged

        Protected Sub RaisePropertyChanged(ByVal propertyName As String)
            Dim propertyChanged As System.ComponentModel.PropertyChangedEventHandler = Me.PropertyChangedEvent
            If (Not (propertyChanged) Is Nothing) Then
                propertyChanged(Me, New System.ComponentModel.PropertyChangedEventArgs(propertyName))
            End If
        End Sub

        <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0"), _
         System.Runtime.Serialization.DataContractAttribute(Name:="BuilderTaskProgress.ExecutionState", [Namespace]:="http://schemas.datacontract.org/2004/07/Questify.Builder.Services.TasksService.Ta" & _
            "skClasses")> _
        Public Enum ExecutionState As Integer

            <System.Runtime.Serialization.EnumMemberAttribute()> _
            Finished = 0

            <System.Runtime.Serialization.EnumMemberAttribute()> _
            Running = 1

            <System.Runtime.Serialization.EnumMemberAttribute()> _
            Cancelled = 2
        End Enum
    End Class

    <System.Diagnostics.DebuggerStepThroughAttribute(), _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0"), _
     System.Runtime.Serialization.DataContractAttribute(Name:="BuilderTaskProgressItem", [Namespace]:="http://schemas.datacontract.org/2004/07/Questify.Builder.Services.TasksService.Ta" & _
        "skClasses"), _
     System.SerializableAttribute()> _
    Partial Public Class BuilderTaskProgressItem
        Inherits Object
        Implements System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged

        <System.NonSerializedAttribute()> _
        Private extensionDataField As System.Runtime.Serialization.ExtensionDataObject

        <System.Runtime.Serialization.OptionalFieldAttribute()> _
        Private ProcessedCountField As Integer

        <System.Runtime.Serialization.OptionalFieldAttribute()> _
        Private ProgressItemCodeField As String

        <System.Runtime.Serialization.OptionalFieldAttribute()> _
        Private ProgressItemLabelField As String

        <System.Runtime.Serialization.OptionalFieldAttribute()> _
        Private TotalCountField As Integer

        <Global.System.ComponentModel.BrowsableAttribute(false)> _
        Public Property ExtensionData() As System.Runtime.Serialization.ExtensionDataObject Implements System.Runtime.Serialization.IExtensibleDataObject.ExtensionData
            Get
                Return Me.extensionDataField
            End Get
            Set
                Me.extensionDataField = value
            End Set
        End Property

        <System.Runtime.Serialization.DataMemberAttribute()> _
        Public Property ProcessedCount() As Integer
            Get
                Return Me.ProcessedCountField
            End Get
            Set
                If (Me.ProcessedCountField.Equals(value) <> true) Then
                    Me.ProcessedCountField = value
                    Me.RaisePropertyChanged("ProcessedCount")
                End If
            End Set
        End Property

        <System.Runtime.Serialization.DataMemberAttribute()> _
        Public Property ProgressItemCode() As String
            Get
                Return Me.ProgressItemCodeField
            End Get
            Set
                If (Object.ReferenceEquals(Me.ProgressItemCodeField, value) <> true) Then
                    Me.ProgressItemCodeField = value
                    Me.RaisePropertyChanged("ProgressItemCode")
                End If
            End Set
        End Property

        <System.Runtime.Serialization.DataMemberAttribute()> _
        Public Property ProgressItemLabel() As String
            Get
                Return Me.ProgressItemLabelField
            End Get
            Set
                If (Object.ReferenceEquals(Me.ProgressItemLabelField, value) <> true) Then
                    Me.ProgressItemLabelField = value
                    Me.RaisePropertyChanged("ProgressItemLabel")
                End If
            End Set
        End Property

        <System.Runtime.Serialization.DataMemberAttribute()> _
        Public Property TotalCount() As Integer
            Get
                Return Me.TotalCountField
            End Get
            Set
                If (Me.TotalCountField.Equals(value) <> true) Then
                    Me.TotalCountField = value
                    Me.RaisePropertyChanged("TotalCount")
                End If
            End Set
        End Property

        Public Event PropertyChanged As System.ComponentModel.PropertyChangedEventHandler Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged

        Protected Sub RaisePropertyChanged(ByVal propertyName As String)
            Dim propertyChanged As System.ComponentModel.PropertyChangedEventHandler = Me.PropertyChangedEvent
            If (Not (propertyChanged) Is Nothing) Then
                propertyChanged(Me, New System.ComponentModel.PropertyChangedEventArgs(propertyName))
            End If
        End Sub
    End Class

    <System.Diagnostics.DebuggerStepThroughAttribute(), _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0"), _
     System.Runtime.Serialization.DataContractAttribute(Name:="BuilderTaskResult", [Namespace]:="http://schemas.datacontract.org/2004/07/Questify.Builder.Services.TasksService.Ta" & _
        "skClasses"), _
     System.SerializableAttribute()> _
    Partial Public Class BuilderTaskResult
        Inherits Object
        Implements System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged

        <System.NonSerializedAttribute()> _
        Private extensionDataField As System.Runtime.Serialization.ExtensionDataObject

        <System.Runtime.Serialization.OptionalFieldAttribute()> _
        Private ErrorsField As System.Collections.Generic.List(Of String)

        <System.Runtime.Serialization.OptionalFieldAttribute()> _
        Private ExceptionsField As System.Collections.Generic.List(Of String)

        <System.Runtime.Serialization.OptionalFieldAttribute()> _
        Private InfoField As System.Collections.Generic.List(Of String)

        <System.Runtime.Serialization.OptionalFieldAttribute()> _
        Private TaskTerminationField As BuilderTasksService.BuilderTaskResult.TaskTerminationState

        <System.Runtime.Serialization.OptionalFieldAttribute()> _
        Private WarningsField As System.Collections.Generic.List(Of String)

        <Global.System.ComponentModel.BrowsableAttribute(false)> _
        Public Property ExtensionData() As System.Runtime.Serialization.ExtensionDataObject Implements System.Runtime.Serialization.IExtensibleDataObject.ExtensionData
            Get
                Return Me.extensionDataField
            End Get
            Set
                Me.extensionDataField = value
            End Set
        End Property

        <System.Runtime.Serialization.DataMemberAttribute()> _
        Public Property Errors() As System.Collections.Generic.List(Of String)
            Get
                Return Me.ErrorsField
            End Get
            Set
                If (Object.ReferenceEquals(Me.ErrorsField, value) <> true) Then
                    Me.ErrorsField = value
                    Me.RaisePropertyChanged("Errors")
                End If
            End Set
        End Property

        <System.Runtime.Serialization.DataMemberAttribute()> _
        Public Property Exceptions() As System.Collections.Generic.List(Of String)
            Get
                Return Me.ExceptionsField
            End Get
            Set
                If (Object.ReferenceEquals(Me.ExceptionsField, value) <> true) Then
                    Me.ExceptionsField = value
                    Me.RaisePropertyChanged("Exceptions")
                End If
            End Set
        End Property

        <System.Runtime.Serialization.DataMemberAttribute()> _
        Public Property Info() As System.Collections.Generic.List(Of String)
            Get
                Return Me.InfoField
            End Get
            Set
                If (Object.ReferenceEquals(Me.InfoField, value) <> true) Then
                    Me.InfoField = value
                    Me.RaisePropertyChanged("Info")
                End If
            End Set
        End Property

        <System.Runtime.Serialization.DataMemberAttribute()> _
        Public Property TaskTermination() As BuilderTasksService.BuilderTaskResult.TaskTerminationState
            Get
                Return Me.TaskTerminationField
            End Get
            Set
                If (Me.TaskTerminationField.Equals(value) <> true) Then
                    Me.TaskTerminationField = value
                    Me.RaisePropertyChanged("TaskTermination")
                End If
            End Set
        End Property

        <System.Runtime.Serialization.DataMemberAttribute()> _
        Public Property Warnings() As System.Collections.Generic.List(Of String)
            Get
                Return Me.WarningsField
            End Get
            Set
                If (Object.ReferenceEquals(Me.WarningsField, value) <> true) Then
                    Me.WarningsField = value
                    Me.RaisePropertyChanged("Warnings")
                End If
            End Set
        End Property

        Public Event PropertyChanged As System.ComponentModel.PropertyChangedEventHandler Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged

        Protected Sub RaisePropertyChanged(ByVal propertyName As String)
            Dim propertyChanged As System.ComponentModel.PropertyChangedEventHandler = Me.PropertyChangedEvent
            If (Not (propertyChanged) Is Nothing) Then
                propertyChanged(Me, New System.ComponentModel.PropertyChangedEventArgs(propertyName))
            End If
        End Sub

        <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0"), _
         System.Runtime.Serialization.DataContractAttribute(Name:="BuilderTaskResult.TaskTerminationState", [Namespace]:="http://schemas.datacontract.org/2004/07/Questify.Builder.Services.TasksService.Ta" & _
            "skClasses")> _
        Public Enum TaskTerminationState As Integer

            <System.Runtime.Serialization.EnumMemberAttribute()> _
            Completed = 0

            <System.Runtime.Serialization.EnumMemberAttribute()> _
            Cancelled = 1

            <System.Runtime.Serialization.EnumMemberAttribute()> _
            Halted = 2
        End Enum
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"), _
     System.ServiceModel.ServiceContractAttribute(ConfigurationName:="BuilderTasksService.IQuestifyBuilderTasksService")> _
    Public Interface IQuestifyBuilderTasksService

        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/IQuestifyBuilderTasksService/PollProgress", ReplyAction:="http://tempuri.org/IQuestifyBuilderTasksService/PollProgressResponse")> _
        Function PollProgress(ByVal builderTaskSessionTicket As BuilderTasksService.BuilderTaskSessionTicket) As BuilderTasksService.BuilderTaskProgress

        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/IQuestifyBuilderTasksService/RequestCancellation", ReplyAction:="http://tempuri.org/IQuestifyBuilderTasksService/RequestCancellationResponse")> _
        Sub RequestCancellation(ByVal builderTaskSessionTicket As BuilderTasksService.BuilderTaskSessionTicket)

        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/IQuestifyBuilderTasksService/GetLogFileStream", ReplyAction:="http://tempuri.org/IQuestifyBuilderTasksService/GetLogFileStreamResponse")> _
        Function GetLogFileStream(ByVal builderTaskSessionTicket As BuilderTasksService.BuilderTaskSessionTicket) As System.IO.Stream

        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/IQuestifyBuilderTasksService/GetTaskResult", ReplyAction:="http://tempuri.org/IQuestifyBuilderTasksService/GetTaskResultResponse")> _
        Function GetTaskResult(ByVal builderTaskSessionTicket As BuilderTasksService.BuilderTaskSessionTicket) As BuilderTasksService.BuilderTaskResult

        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/IQuestifyBuilderTasksService/Cleanup", ReplyAction:="http://tempuri.org/IQuestifyBuilderTasksService/CleanupResponse")> _
        Sub Cleanup(ByVal builderTaskSessionTicket As BuilderTasksService.BuilderTaskSessionTicket)

        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/IQuestifyBuilderTasksService/HarmonizeWithItemLayoutTemplates", ReplyAction:="http://tempuri.org/IQuestifyBuilderTasksService/HarmonizeWithItemLayoutTemplatesR" & _
            "esponse")> _
        Function HarmonizeWithItemLayoutTemplates(ByVal templateGuids As System.Collections.Generic.List(Of System.Guid), ByVal logTheActions As Boolean) As BuilderTasksService.BuilderTaskSessionTicket

        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/IQuestifyBuilderTasksService/HarmonizeItems", ReplyAction:="http://tempuri.org/IQuestifyBuilderTasksService/HarmonizeItemsResponse")> _
        Function HarmonizeItems(ByVal itemResourceIds As System.Collections.Generic.List(Of System.Guid), ByVal logTheActions As Boolean) As BuilderTasksService.BuilderTaskSessionTicket

        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/IQuestifyBuilderTasksService/HarmonizeAfterImport", ReplyAction:="http://tempuri.org/IQuestifyBuilderTasksService/HarmonizeAfterImportResponse")> _
        Function HarmonizeAfterImport(ByVal bankId As Integer, ByVal templateGuids As System.Collections.Generic.List(Of System.Guid), ByVal itemCodes As System.Collections.Generic.List(Of String)) As BuilderTasksService.BuilderTaskSessionTicket
    End Interface

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")> _
    Public Interface IQuestifyBuilderTasksServiceChannel
        Inherits BuilderTasksService.IQuestifyBuilderTasksService, System.ServiceModel.IClientChannel
    End Interface

    <System.Diagnostics.DebuggerStepThroughAttribute(), _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")> _
    Partial Public Class QuestifyBuilderTasksServiceClient
        Inherits System.ServiceModel.ClientBase(Of BuilderTasksService.IQuestifyBuilderTasksService)
        Implements BuilderTasksService.IQuestifyBuilderTasksService

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

        Public Function PollProgress(ByVal builderTaskSessionTicket As BuilderTasksService.BuilderTaskSessionTicket) As BuilderTasksService.BuilderTaskProgress Implements BuilderTasksService.IQuestifyBuilderTasksService.PollProgress
            Return MyBase.Channel.PollProgress(builderTaskSessionTicket)
        End Function

        Public Sub RequestCancellation(ByVal builderTaskSessionTicket As BuilderTasksService.BuilderTaskSessionTicket) Implements BuilderTasksService.IQuestifyBuilderTasksService.RequestCancellation
            MyBase.Channel.RequestCancellation(builderTaskSessionTicket)
        End Sub

        Public Function GetLogFileStream(ByVal builderTaskSessionTicket As BuilderTasksService.BuilderTaskSessionTicket) As System.IO.Stream Implements BuilderTasksService.IQuestifyBuilderTasksService.GetLogFileStream
            Return MyBase.Channel.GetLogFileStream(builderTaskSessionTicket)
        End Function

        Public Function GetTaskResult(ByVal builderTaskSessionTicket As BuilderTasksService.BuilderTaskSessionTicket) As BuilderTasksService.BuilderTaskResult Implements BuilderTasksService.IQuestifyBuilderTasksService.GetTaskResult
            Return MyBase.Channel.GetTaskResult(builderTaskSessionTicket)
        End Function

        Public Sub Cleanup(ByVal builderTaskSessionTicket As BuilderTasksService.BuilderTaskSessionTicket) Implements BuilderTasksService.IQuestifyBuilderTasksService.Cleanup
            MyBase.Channel.Cleanup(builderTaskSessionTicket)
        End Sub

        Public Function HarmonizeWithItemLayoutTemplates(ByVal templateGuids As System.Collections.Generic.List(Of System.Guid), ByVal logTheActions As Boolean) As BuilderTasksService.BuilderTaskSessionTicket Implements BuilderTasksService.IQuestifyBuilderTasksService.HarmonizeWithItemLayoutTemplates
            Return MyBase.Channel.HarmonizeWithItemLayoutTemplates(templateGuids, logTheActions)
        End Function

        Public Function HarmonizeItems(ByVal itemResourceIds As System.Collections.Generic.List(Of System.Guid), ByVal logTheActions As Boolean) As BuilderTasksService.BuilderTaskSessionTicket Implements BuilderTasksService.IQuestifyBuilderTasksService.HarmonizeItems
            Return MyBase.Channel.HarmonizeItems(itemResourceIds, logTheActions)
        End Function

        Public Function HarmonizeAfterImport(ByVal bankId As Integer, ByVal templateGuids As System.Collections.Generic.List(Of System.Guid), ByVal itemCodes As System.Collections.Generic.List(Of String)) As BuilderTasksService.BuilderTaskSessionTicket Implements BuilderTasksService.IQuestifyBuilderTasksService.HarmonizeAfterImport
            Return MyBase.Channel.HarmonizeAfterImport(bankId, templateGuids, itemCodes)
        End Function
    End Class
End Namespace
