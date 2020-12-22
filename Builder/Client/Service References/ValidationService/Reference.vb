
Option Strict On
Option Explicit On


Namespace ValidationService

    <System.Diagnostics.DebuggerStepThroughAttribute(), _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0"), _
     System.Runtime.Serialization.DataContractAttribute(Name:="ValidationHandlerIdentifier", [Namespace]:="http://schemas.datacontract.org/2004/07/Questify.Builder.Services.PublicationServ" & _
        "ice.Validation"), _
     System.SerializableAttribute()> _
    Partial Public Class ValidationHandlerIdentifier
        Inherits Object
        Implements System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged

        <System.NonSerializedAttribute()> _
        Private extensionDataField As System.Runtime.Serialization.ExtensionDataObject

        <System.Runtime.Serialization.OptionalFieldAttribute()> _
        Private TaskIdField As String

        <System.Runtime.Serialization.OptionalFieldAttribute()> _
        Private UserFriendlyNameField As String

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
        Public Property TaskId() As String
            Get
                Return Me.TaskIdField
            End Get
            Set
                If (Object.ReferenceEquals(Me.TaskIdField, value) <> true) Then
                    Me.TaskIdField = value
                    Me.RaisePropertyChanged("TaskId")
                End If
            End Set
        End Property

        <System.Runtime.Serialization.DataMemberAttribute()> _
        Public Property UserFriendlyName() As String
            Get
                Return Me.UserFriendlyNameField
            End Get
            Set
                If (Object.ReferenceEquals(Me.UserFriendlyNameField, value) <> true) Then
                    Me.UserFriendlyNameField = value
                    Me.RaisePropertyChanged("UserFriendlyName")
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
     System.Runtime.Serialization.DataContractAttribute(Name:="TaskProgress", [Namespace]:="http://schemas.datacontract.org/2004/07/Questify.Builder.Services.PublicationServ" & _
        "ice"), _
     System.SerializableAttribute(), _
     System.Runtime.Serialization.KnownTypeAttribute(GetType(ValidationService.ValidationTaskProgress))> _
    Partial Public Class TaskProgress
        Inherits Object
        Implements System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged

        <System.NonSerializedAttribute()> _
        Private extensionDataField As System.Runtime.Serialization.ExtensionDataObject

        <System.Runtime.Serialization.OptionalFieldAttribute()> _
        Private ErrorsField As String

        <System.Runtime.Serialization.OptionalFieldAttribute()> _
        Private FinishedField As Boolean

        <System.Runtime.Serialization.OptionalFieldAttribute()> _
        Private ProgressField As Integer

        <System.Runtime.Serialization.OptionalFieldAttribute()> _
        Private ProgressStringField As String

        <System.Runtime.Serialization.OptionalFieldAttribute()> _
        Private TaskIdField As String

        <System.Runtime.Serialization.OptionalFieldAttribute()> _
        Private TotalField As Integer

        <System.Runtime.Serialization.OptionalFieldAttribute()> _
        Private WarningsField As String

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
        Public Property Errors() As String
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
        Public Property Finished() As Boolean
            Get
                Return Me.FinishedField
            End Get
            Set
                If (Me.FinishedField.Equals(value) <> true) Then
                    Me.FinishedField = value
                    Me.RaisePropertyChanged("Finished")
                End If
            End Set
        End Property

        <System.Runtime.Serialization.DataMemberAttribute()> _
        Public Property Progress() As Integer
            Get
                Return Me.ProgressField
            End Get
            Set
                If (Me.ProgressField.Equals(value) <> true) Then
                    Me.ProgressField = value
                    Me.RaisePropertyChanged("Progress")
                End If
            End Set
        End Property

        <System.Runtime.Serialization.DataMemberAttribute()> _
        Public Property ProgressString() As String
            Get
                Return Me.ProgressStringField
            End Get
            Set
                If (Object.ReferenceEquals(Me.ProgressStringField, value) <> true) Then
                    Me.ProgressStringField = value
                    Me.RaisePropertyChanged("ProgressString")
                End If
            End Set
        End Property

        <System.Runtime.Serialization.DataMemberAttribute()> _
        Public Property TaskId() As String
            Get
                Return Me.TaskIdField
            End Get
            Set
                If (Object.ReferenceEquals(Me.TaskIdField, value) <> true) Then
                    Me.TaskIdField = value
                    Me.RaisePropertyChanged("TaskId")
                End If
            End Set
        End Property

        <System.Runtime.Serialization.DataMemberAttribute()> _
        Public Property Total() As Integer
            Get
                Return Me.TotalField
            End Get
            Set
                If (Me.TotalField.Equals(value) <> true) Then
                    Me.TotalField = value
                    Me.RaisePropertyChanged("Total")
                End If
            End Set
        End Property

        <System.Runtime.Serialization.DataMemberAttribute()> _
        Public Property Warnings() As String
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
    End Class

    <System.Diagnostics.DebuggerStepThroughAttribute(), _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0"), _
     System.Runtime.Serialization.DataContractAttribute(Name:="ValidationTaskProgress", [Namespace]:="http://schemas.datacontract.org/2004/07/Questify.Builder.Services.PublicationServ" & _
        "ice.Validation"), _
     System.SerializableAttribute()> _
    Partial Public Class ValidationTaskProgress
        Inherits ValidationService.TaskProgress

        <System.Runtime.Serialization.OptionalFieldAttribute()> _
        Private IsReportAvailableField As Boolean

        <System.Runtime.Serialization.OptionalFieldAttribute()> _
        Private ReportField As String

        <System.Runtime.Serialization.OptionalFieldAttribute()> _
        Private ResultTextField As String

        <System.Runtime.Serialization.OptionalFieldAttribute()> _
        Private ValidationResultField As Enums.ValidationResult

        <System.Runtime.Serialization.DataMemberAttribute()> _
        Public Property IsReportAvailable() As Boolean
            Get
                Return Me.IsReportAvailableField
            End Get
            Set
                If (Me.IsReportAvailableField.Equals(value) <> true) Then
                    Me.IsReportAvailableField = value
                    Me.RaisePropertyChanged("IsReportAvailable")
                End If
            End Set
        End Property

        <System.Runtime.Serialization.DataMemberAttribute()> _
        Public Property Report() As String
            Get
                Return Me.ReportField
            End Get
            Set
                If (Object.ReferenceEquals(Me.ReportField, value) <> true) Then
                    Me.ReportField = value
                    Me.RaisePropertyChanged("Report")
                End If
            End Set
        End Property

        <System.Runtime.Serialization.DataMemberAttribute()> _
        Public Property ResultText() As String
            Get
                Return Me.ResultTextField
            End Get
            Set
                If (Object.ReferenceEquals(Me.ResultTextField, value) <> true) Then
                    Me.ResultTextField = value
                    Me.RaisePropertyChanged("ResultText")
                End If
            End Set
        End Property

        <System.Runtime.Serialization.DataMemberAttribute()> _
        Public Property ValidationResult() As Enums.ValidationResult
            Get
                Return Me.ValidationResultField
            End Get
            Set
                If (Me.ValidationResultField.Equals(value) <> true) Then
                    Me.ValidationResultField = value
                    Me.RaisePropertyChanged("ValidationResult")
                End If
            End Set
        End Property
    End Class

    <System.Diagnostics.DebuggerStepThroughAttribute(), _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0"), _
     System.Runtime.Serialization.DataContractAttribute(Name:="Version", [Namespace]:="http://schemas.datacontract.org/2004/07/System"), _
     System.SerializableAttribute()> _
    Partial Public Class Version
        Inherits Object
        Implements System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged

        <System.NonSerializedAttribute()> _
        Private extensionDataField As System.Runtime.Serialization.ExtensionDataObject

        Private _BuildField As Integer

        Private _MajorField As Integer

        Private _MinorField As Integer

        Private _RevisionField As Integer

        <Global.System.ComponentModel.BrowsableAttribute(false)> _
        Public Property ExtensionData() As System.Runtime.Serialization.ExtensionDataObject Implements System.Runtime.Serialization.IExtensibleDataObject.ExtensionData
            Get
                Return Me.extensionDataField
            End Get
            Set
                Me.extensionDataField = value
            End Set
        End Property

        <System.Runtime.Serialization.DataMemberAttribute(IsRequired:=true)> _
        Public Property _Build() As Integer
            Get
                Return Me._BuildField
            End Get
            Set
                If (Me._BuildField.Equals(value) <> true) Then
                    Me._BuildField = value
                    Me.RaisePropertyChanged("_Build")
                End If
            End Set
        End Property

        <System.Runtime.Serialization.DataMemberAttribute(IsRequired:=true)> _
        Public Property _Major() As Integer
            Get
                Return Me._MajorField
            End Get
            Set
                If (Me._MajorField.Equals(value) <> true) Then
                    Me._MajorField = value
                    Me.RaisePropertyChanged("_Major")
                End If
            End Set
        End Property

        <System.Runtime.Serialization.DataMemberAttribute(IsRequired:=true)> _
        Public Property _Minor() As Integer
            Get
                Return Me._MinorField
            End Get
            Set
                If (Me._MinorField.Equals(value) <> true) Then
                    Me._MinorField = value
                    Me.RaisePropertyChanged("_Minor")
                End If
            End Set
        End Property

        <System.Runtime.Serialization.DataMemberAttribute(IsRequired:=true)> _
        Public Property _Revision() As Integer
            Get
                Return Me._RevisionField
            End Get
            Set
                If (Me._RevisionField.Equals(value) <> true) Then
                    Me._RevisionField = value
                    Me.RaisePropertyChanged("_Revision")
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

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"), _
     System.ServiceModel.ServiceContractAttribute(ConfigurationName:="ValidationService.IValidationService")> _
    Public Interface IValidationService

        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/IValidationService/AtLeastOneHandlerAvailable", ReplyAction:="http://tempuri.org/IValidationService/AtLeastOneHandlerAvailableResponse")> _
        Function AtLeastOneHandlerAvailable(ByVal bankId As Integer, ByVal testNames() As String) As Boolean

        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/IValidationService/AtLeastOneHandlerAvailable", ReplyAction:="http://tempuri.org/IValidationService/AtLeastOneHandlerAvailableResponse")> _
        Function AtLeastOneHandlerAvailableAsync(ByVal bankId As Integer, ByVal testNames() As String) As System.Threading.Tasks.Task(Of Boolean)

        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/IValidationService/Validate", ReplyAction:="http://tempuri.org/IValidationService/ValidateResponse")> _
        Function Validate(ByVal bankId As Integer, ByVal testNames() As String) As ValidationService.ValidationHandlerIdentifier()

        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/IValidationService/Validate", ReplyAction:="http://tempuri.org/IValidationService/ValidateResponse")> _
        Function ValidateAsync(ByVal bankId As Integer, ByVal testNames() As String) As System.Threading.Tasks.Task(Of ValidationService.ValidationHandlerIdentifier())

        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/IValidationService/GetProgress", ReplyAction:="http://tempuri.org/IValidationService/GetProgressResponse")> _
        Function GetProgress(ByVal taskId As String) As ValidationService.ValidationTaskProgress

        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/IValidationService/GetProgress", ReplyAction:="http://tempuri.org/IValidationService/GetProgressResponse")> _
        Function GetProgressAsync(ByVal taskId As String) As System.Threading.Tasks.Task(Of ValidationService.ValidationTaskProgress)

        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/IValidationService/FinishValidation", ReplyAction:="http://tempuri.org/IValidationService/FinishValidationResponse")> _
        Sub FinishValidation(ByVal taskId As String)

        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/IValidationService/FinishValidation", ReplyAction:="http://tempuri.org/IValidationService/FinishValidationResponse")> _
        Function FinishValidationAsync(ByVal taskId As String) As System.Threading.Tasks.Task

        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/IValidationService/GetCurrentVersion", ReplyAction:="http://tempuri.org/IValidationService/GetCurrentVersionResponse")> _
        Function GetCurrentVersion() As ValidationService.Version

        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/IValidationService/GetCurrentVersion", ReplyAction:="http://tempuri.org/IValidationService/GetCurrentVersionResponse")> _
        Function GetCurrentVersionAsync() As System.Threading.Tasks.Task(Of ValidationService.Version)
    End Interface

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")> _
    Public Interface IValidationServiceChannel
        Inherits ValidationService.IValidationService, System.ServiceModel.IClientChannel
    End Interface

    <System.Diagnostics.DebuggerStepThroughAttribute(), _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")> _
    Partial Public Class ValidationServiceClient
        Inherits System.ServiceModel.ClientBase(Of ValidationService.IValidationService)
        Implements ValidationService.IValidationService

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

        Public Function AtLeastOneHandlerAvailable(ByVal bankId As Integer, ByVal testNames() As String) As Boolean Implements ValidationService.IValidationService.AtLeastOneHandlerAvailable
            Return MyBase.Channel.AtLeastOneHandlerAvailable(bankId, testNames)
        End Function

        Public Function AtLeastOneHandlerAvailableAsync(ByVal bankId As Integer, ByVal testNames() As String) As System.Threading.Tasks.Task(Of Boolean) Implements ValidationService.IValidationService.AtLeastOneHandlerAvailableAsync
            Return MyBase.Channel.AtLeastOneHandlerAvailableAsync(bankId, testNames)
        End Function

        Public Function Validate(ByVal bankId As Integer, ByVal testNames() As String) As ValidationService.ValidationHandlerIdentifier() Implements ValidationService.IValidationService.Validate
            Return MyBase.Channel.Validate(bankId, testNames)
        End Function

        Public Function ValidateAsync(ByVal bankId As Integer, ByVal testNames() As String) As System.Threading.Tasks.Task(Of ValidationService.ValidationHandlerIdentifier()) Implements ValidationService.IValidationService.ValidateAsync
            Return MyBase.Channel.ValidateAsync(bankId, testNames)
        End Function

        Public Function GetProgress(ByVal taskId As String) As ValidationService.ValidationTaskProgress Implements ValidationService.IValidationService.GetProgress
            Return MyBase.Channel.GetProgress(taskId)
        End Function

        Public Function GetProgressAsync(ByVal taskId As String) As System.Threading.Tasks.Task(Of ValidationService.ValidationTaskProgress) Implements ValidationService.IValidationService.GetProgressAsync
            Return MyBase.Channel.GetProgressAsync(taskId)
        End Function

        Public Sub FinishValidation(ByVal taskId As String) Implements ValidationService.IValidationService.FinishValidation
            MyBase.Channel.FinishValidation(taskId)
        End Sub

        Public Function FinishValidationAsync(ByVal taskId As String) As System.Threading.Tasks.Task Implements ValidationService.IValidationService.FinishValidationAsync
            Return MyBase.Channel.FinishValidationAsync(taskId)
        End Function

        Public Function GetCurrentVersion() As ValidationService.Version Implements ValidationService.IValidationService.GetCurrentVersion
            Return MyBase.Channel.GetCurrentVersion
        End Function

        Public Function GetCurrentVersionAsync() As System.Threading.Tasks.Task(Of ValidationService.Version) Implements ValidationService.IValidationService.GetCurrentVersionAsync
            Return MyBase.Channel.GetCurrentVersionAsync
        End Function
    End Class
End Namespace
