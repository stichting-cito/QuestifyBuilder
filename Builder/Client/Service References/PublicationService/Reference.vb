
Option Strict On
Option Explicit On

Imports System
Imports System.Runtime.Serialization

Namespace PublicationService

    <System.Diagnostics.DebuggerStepThroughAttribute(), _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0"), _
     System.Runtime.Serialization.DataContractAttribute(Name:="PublicationHandlerIdentifier", [Namespace]:="http://schemas.datacontract.org/2004/07/Questify.Builder.Services.PublicationServ" & _
        "ice.Publication"), _
     System.SerializableAttribute()> _
    Partial Public Class PublicationHandlerIdentifier
        Inherits Object
        Implements System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged

        <System.NonSerializedAttribute()> _
        Private extensionDataField As System.Runtime.Serialization.ExtensionDataObject

        <System.Runtime.Serialization.OptionalFieldAttribute()> _
        Private FileExtensionField As String

        <System.Runtime.Serialization.OptionalFieldAttribute()> _
        Private QualifiesForCurrentSelectionField As Boolean

        <System.Runtime.Serialization.OptionalFieldAttribute()> _
        Private ReasonForNotQualifyingForCurrentSelectionField As String

        <System.Runtime.Serialization.OptionalFieldAttribute()> _
        Private TypeField As String

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
        Public Property FileExtension() As String
            Get
                Return Me.FileExtensionField
            End Get
            Set
                If (Object.ReferenceEquals(Me.FileExtensionField, value) <> true) Then
                    Me.FileExtensionField = value
                    Me.RaisePropertyChanged("FileExtension")
                End If
            End Set
        End Property

        <System.Runtime.Serialization.DataMemberAttribute()> _
        Public Property QualifiesForCurrentSelection() As Boolean
            Get
                Return Me.QualifiesForCurrentSelectionField
            End Get
            Set
                If (Me.QualifiesForCurrentSelectionField.Equals(value) <> true) Then
                    Me.QualifiesForCurrentSelectionField = value
                    Me.RaisePropertyChanged("QualifiesForCurrentSelection")
                End If
            End Set
        End Property

        <System.Runtime.Serialization.DataMemberAttribute()> _
        Public Property ReasonForNotQualifyingForCurrentSelection() As String
            Get
                Return Me.ReasonForNotQualifyingForCurrentSelectionField
            End Get
            Set
                If (Object.ReferenceEquals(Me.ReasonForNotQualifyingForCurrentSelectionField, value) <> true) Then
                    Me.ReasonForNotQualifyingForCurrentSelectionField = value
                    Me.RaisePropertyChanged("ReasonForNotQualifyingForCurrentSelection")
                End If
            End Set
        End Property

        <System.Runtime.Serialization.DataMemberAttribute()> _
        Public Property Type() As String
            Get
                Return Me.TypeField
            End Get
            Set
                If (Object.ReferenceEquals(Me.TypeField, value) <> true) Then
                    Me.TypeField = value
                    Me.RaisePropertyChanged("Type")
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
     System.Runtime.Serialization.DataContractAttribute(Name:="TestPreviewHandlerIdentifier", [Namespace]:="http://schemas.datacontract.org/2004/07/Questify.Builder.Services.PublicationServ" & _
        "ice.Publication"), _
     System.SerializableAttribute()> _
    Partial Public Class TestPreviewHandlerIdentifier
        Inherits Object
        Implements System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged

        <System.NonSerializedAttribute()> _
        Private extensionDataField As System.Runtime.Serialization.ExtensionDataObject

        <System.Runtime.Serialization.OptionalFieldAttribute()> _
        Private DefaultClientField As String

        <System.Runtime.Serialization.OptionalFieldAttribute()> _
        Private FileExtensionField As String

        <System.Runtime.Serialization.OptionalFieldAttribute()> _
        Private IsClickOnceField As Boolean

        <System.Runtime.Serialization.OptionalFieldAttribute()> _
        Private PublicationHandlerTypeField As String

        <System.Runtime.Serialization.OptionalFieldAttribute()> _
        Private UrlField As String

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
        Public Property DefaultClient() As String
            Get
                Return Me.DefaultClientField
            End Get
            Set
                If (Object.ReferenceEquals(Me.DefaultClientField, value) <> true) Then
                    Me.DefaultClientField = value
                    Me.RaisePropertyChanged("DefaultClient")
                End If
            End Set
        End Property

        <System.Runtime.Serialization.DataMemberAttribute()> _
        Public Property FileExtension() As String
            Get
                Return Me.FileExtensionField
            End Get
            Set
                If (Object.ReferenceEquals(Me.FileExtensionField, value) <> true) Then
                    Me.FileExtensionField = value
                    Me.RaisePropertyChanged("FileExtension")
                End If
            End Set
        End Property

        <System.Runtime.Serialization.DataMemberAttribute()> _
        Public Property IsClickOnce() As Boolean
            Get
                Return Me.IsClickOnceField
            End Get
            Set
                If (Me.IsClickOnceField.Equals(value) <> true) Then
                    Me.IsClickOnceField = value
                    Me.RaisePropertyChanged("IsClickOnce")
                End If
            End Set
        End Property

        <System.Runtime.Serialization.DataMemberAttribute()> _
        Public Property PublicationHandlerType() As String
            Get
                Return Me.PublicationHandlerTypeField
            End Get
            Set
                If (Object.ReferenceEquals(Me.PublicationHandlerTypeField, value) <> true) Then
                    Me.PublicationHandlerTypeField = value
                    Me.RaisePropertyChanged("PublicationHandlerType")
                End If
            End Set
        End Property

        <System.Runtime.Serialization.DataMemberAttribute()> _
        Public Property Url() As String
            Get
                Return Me.UrlField
            End Get
            Set
                If (Object.ReferenceEquals(Me.UrlField, value) <> true) Then
                    Me.UrlField = value
                    Me.RaisePropertyChanged("Url")
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
     System.Runtime.Serialization.KnownTypeAttribute(GetType(PublicationService.PublicationTaskProgress))> _
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
     System.Runtime.Serialization.DataContractAttribute(Name:="PublicationTaskProgress", [Namespace]:="http://schemas.datacontract.org/2004/07/Questify.Builder.Services.PublicationServ" & _
        "ice.Publication"), _
     System.SerializableAttribute()> _
    Partial Public Class PublicationTaskProgress
        Inherits PublicationService.TaskProgress

        <System.Runtime.Serialization.OptionalFieldAttribute()> _
        Private PublicationLocationsField() As String

        <System.Runtime.Serialization.OptionalFieldAttribute()> _
        Private PublicationUrlsField() As String

        <System.Runtime.Serialization.OptionalFieldAttribute()> _
        Private ReturnedIdsField() As String

        <System.Runtime.Serialization.OptionalFieldAttribute()> _
        Private SucceededField As Boolean

        <System.Runtime.Serialization.DataMemberAttribute()> _
        Public Property PublicationLocations() As String()
            Get
                Return Me.PublicationLocationsField
            End Get
            Set
                If (Object.ReferenceEquals(Me.PublicationLocationsField, value) <> true) Then
                    Me.PublicationLocationsField = value
                    Me.RaisePropertyChanged("PublicationLocations")
                End If
            End Set
        End Property

        <System.Runtime.Serialization.DataMemberAttribute()> _
        Public Property PublicationUrls() As String()
            Get
                Return Me.PublicationUrlsField
            End Get
            Set
                If (Object.ReferenceEquals(Me.PublicationUrlsField, value) <> true) Then
                    Me.PublicationUrlsField = value
                    Me.RaisePropertyChanged("PublicationUrls")
                End If
            End Set
        End Property

        <System.Runtime.Serialization.DataMemberAttribute()> _
        Public Property ReturnedIds() As String()
            Get
                Return Me.ReturnedIdsField
            End Get
            Set
                If (Object.ReferenceEquals(Me.ReturnedIdsField, value) <> true) Then
                    Me.ReturnedIdsField = value
                    Me.RaisePropertyChanged("ReturnedIds")
                End If
            End Set
        End Property

        <System.Runtime.Serialization.DataMemberAttribute()> _
        Public Property Succeeded() As Boolean
            Get
                Return Me.SucceededField
            End Get
            Set
                If (Me.SucceededField.Equals(value) <> true) Then
                    Me.SucceededField = value
                    Me.RaisePropertyChanged("Succeeded")
                End If
            End Set
        End Property
    End Class

    <System.Diagnostics.DebuggerStepThroughAttribute(), _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0"), _
     System.Runtime.Serialization.DataContractAttribute(Name:="ConceptProcessingLabelEntry", [Namespace]:="http://schemas.datacontract.org/2004/07/Questify.Builder.Services.PublicationServ" & _
        "ice.Publication"), _
     System.SerializableAttribute()> _
    Partial Public Class ConceptProcessingLabelEntry
        Inherits Object
        Implements System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged

        <System.NonSerializedAttribute()> _
        Private extensionDataField As System.Runtime.Serialization.ExtensionDataObject

        <System.Runtime.Serialization.OptionalFieldAttribute()> _
        Private ConceptCodeField As String

        <System.Runtime.Serialization.OptionalFieldAttribute()> _
        Private ConceptResponseLabelField As String

        <System.Runtime.Serialization.OptionalFieldAttribute()> _
        Private FactIdFirstFactField As String

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
        Public Property ConceptCode() As String
            Get
                Return Me.ConceptCodeField
            End Get
            Set
                If (Object.ReferenceEquals(Me.ConceptCodeField, value) <> true) Then
                    Me.ConceptCodeField = value
                    Me.RaisePropertyChanged("ConceptCode")
                End If
            End Set
        End Property

        <System.Runtime.Serialization.DataMemberAttribute()> _
        Public Property ConceptResponseLabel() As String
            Get
                Return Me.ConceptResponseLabelField
            End Get
            Set
                If (Object.ReferenceEquals(Me.ConceptResponseLabelField, value) <> true) Then
                    Me.ConceptResponseLabelField = value
                    Me.RaisePropertyChanged("ConceptResponseLabel")
                End If
            End Set
        End Property

        <System.Runtime.Serialization.DataMemberAttribute()> _
        Public Property FactIdFirstFact() As String
            Get
                Return Me.FactIdFirstFactField
            End Get
            Set
                If (Object.ReferenceEquals(Me.FactIdFirstFactField, value) <> true) Then
                    Me.FactIdFirstFactField = value
                    Me.RaisePropertyChanged("FactIdFirstFact")
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
     System.ServiceModel.ServiceContractAttribute(ConfigurationName:="PublicationService.IPublicationService")> _
    Public Interface IPublicationService

        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/IPublicationService/GetAvailablePublicationHandlers", ReplyAction:="http://tempuri.org/IPublicationService/GetAvailablePublicationHandlersResponse")> _
        Function GetAvailablePublicationHandlers(ByVal bankId As Integer, ByVal testNames() As String, ByVal testPackageNames() As String) As PublicationService.PublicationHandlerIdentifier()

        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/IPublicationService/GetAvailablePublicationHandlers", ReplyAction:="http://tempuri.org/IPublicationService/GetAvailablePublicationHandlersResponse")> _
        Function GetAvailablePublicationHandlersAsync(ByVal bankId As Integer, ByVal testNames() As String, ByVal testPackageNames() As String) As System.Threading.Tasks.Task(Of PublicationService.PublicationHandlerIdentifier())

        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/IPublicationService/GetAllPublicationHandlers", ReplyAction:="http://tempuri.org/IPublicationService/GetAllPublicationHandlersResponse")> _
        Function GetAllPublicationHandlers(ByVal bankId As Integer, ByVal testNames() As String, ByVal testPackageNames() As String) As PublicationService.PublicationHandlerIdentifier()

        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/IPublicationService/GetAllPublicationHandlers", ReplyAction:="http://tempuri.org/IPublicationService/GetAllPublicationHandlersResponse")> _
        Function GetAllPublicationHandlersAsync(ByVal bankId As Integer, ByVal testNames() As String, ByVal testPackageNames() As String) As System.Threading.Tasks.Task(Of PublicationService.PublicationHandlerIdentifier())

        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/IPublicationService/GetAllTestPreviewHandlers", ReplyAction:="http://tempuri.org/IPublicationService/GetAllTestPreviewHandlersResponse")> _
        Function GetAllTestPreviewHandlers() As PublicationService.TestPreviewHandlerIdentifier()

        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/IPublicationService/GetAllTestPreviewHandlers", ReplyAction:="http://tempuri.org/IPublicationService/GetAllTestPreviewHandlersResponse")> _
        Function GetAllTestPreviewHandlersAsync() As System.Threading.Tasks.Task(Of PublicationService.TestPreviewHandlerIdentifier())

        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/IPublicationService/GetAvailableTestPreviewHandlers", ReplyAction:="http://tempuri.org/IPublicationService/GetAvailableTestPreviewHandlersResponse")> _
        Function GetAvailableTestPreviewHandlers(ByVal bankId As Integer, ByVal testNames() As String, ByVal testPackages() As String) As PublicationService.TestPreviewHandlerIdentifier()

        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/IPublicationService/GetAvailableTestPreviewHandlers", ReplyAction:="http://tempuri.org/IPublicationService/GetAvailableTestPreviewHandlersResponse")> _
        Function GetAvailableTestPreviewHandlersAsync(ByVal bankId As Integer, ByVal testNames() As String, ByVal testPackages() As String) As System.Threading.Tasks.Task(Of PublicationService.TestPreviewHandlerIdentifier())

        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/IPublicationService/GetConfigurationOptions", ReplyAction:="http://tempuri.org/IPublicationService/GetConfigurationOptionsResponse")> _
        Function GetConfigurationOptions(ByVal publicationHandlerType As String, ByVal bankId As Integer, ByVal testNames() As String, ByVal testPackageNames() As String) As System.Collections.Generic.Dictionary(Of String, String)

        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/IPublicationService/GetConfigurationOptions", ReplyAction:="http://tempuri.org/IPublicationService/GetConfigurationOptionsResponse")> _
        Function GetConfigurationOptionsAsync(ByVal publicationHandlerType As String, ByVal bankId As Integer, ByVal testNames() As String, ByVal testPackageNames() As String) As System.Threading.Tasks.Task(Of System.Collections.Generic.Dictionary(Of String, String))

        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/IPublicationService/Publicize", ReplyAction:="http://tempuri.org/IPublicationService/PublicizeResponse")> _
        Function Publicize(ByVal publicationHandlerType As String, ByVal configurationOptions As System.Collections.Generic.Dictionary(Of String, String), ByVal bankId As Integer, ByVal testNames() As String, ByVal testPackageNames() As String, ByVal isForPreview As Boolean, ByVal customName As String) As String

        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/IPublicationService/Publicize", ReplyAction:="http://tempuri.org/IPublicationService/PublicizeResponse")> _
        Function PublicizeAsync(ByVal publicationHandlerType As String, ByVal configurationOptions As System.Collections.Generic.Dictionary(Of String, String), ByVal bankId As Integer, ByVal testNames() As String, ByVal testPackageNames() As String, ByVal isForPreview As Boolean, ByVal customName As String) As System.Threading.Tasks.Task(Of String)

        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/IPublicationService/GetProgress", ReplyAction:="http://tempuri.org/IPublicationService/GetProgressResponse")> _
        Function GetProgress(ByVal taskId As String) As PublicationService.PublicationTaskProgress

        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/IPublicationService/GetProgress", ReplyAction:="http://tempuri.org/IPublicationService/GetProgressResponse")> _
        Function GetProgressAsync(ByVal taskId As String) As System.Threading.Tasks.Task(Of PublicationService.PublicationTaskProgress)

        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/IPublicationService/FinishPublication", ReplyAction:="http://tempuri.org/IPublicationService/FinishPublicationResponse")> _
        Sub FinishPublication(ByVal taskId As String)

        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/IPublicationService/FinishPublication", ReplyAction:="http://tempuri.org/IPublicationService/FinishPublicationResponse")> _
        Function FinishPublicationAsync(ByVal taskId As String) As System.Threading.Tasks.Task

        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/IPublicationService/GetItemOutput", ReplyAction:="http://tempuri.org/IPublicationService/GetItemOutputResponse")> _
        Function GetItemOutput(ByVal publicationHandlerType As String, ByVal bankId As Integer, ByVal itemCode As String) As String

        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/IPublicationService/GetItemOutput", ReplyAction:="http://tempuri.org/IPublicationService/GetItemOutputResponse")> _
        Function GetItemOutputAsync(ByVal publicationHandlerType As String, ByVal bankId As Integer, ByVal itemCode As String) As System.Threading.Tasks.Task(Of String)

        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/IPublicationService/GetConceptRelatedResponseProcessingForRepo" & _
            "rtingPurposes", ReplyAction:="http://tempuri.org/IPublicationService/GetConceptRelatedResponseProcessingForRepo" & _
            "rtingPurposesResponse")> _
        Function GetConceptRelatedResponseProcessingForReportingPurposes(ByVal publicationHandlerType As String, ByVal itemResourceId As System.Guid) As PublicationService.ConceptProcessingLabelEntry()

        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/IPublicationService/GetConceptRelatedResponseProcessingForRepo" & _
            "rtingPurposes", ReplyAction:="http://tempuri.org/IPublicationService/GetConceptRelatedResponseProcessingForRepo" & _
            "rtingPurposesResponse")> _
        Function GetConceptRelatedResponseProcessingForReportingPurposesAsync(ByVal publicationHandlerType As String, ByVal itemResourceId As System.Guid) As System.Threading.Tasks.Task(Of PublicationService.ConceptProcessingLabelEntry())
    End Interface

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")> _
    Public Interface IPublicationServiceChannel
        Inherits PublicationService.IPublicationService, System.ServiceModel.IClientChannel
    End Interface

    <System.Diagnostics.DebuggerStepThroughAttribute(), _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")> _
    Partial Public Class PublicationServiceClient
        Inherits System.ServiceModel.ClientBase(Of PublicationService.IPublicationService)
        Implements PublicationService.IPublicationService

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

        Public Function GetAvailablePublicationHandlers(ByVal bankId As Integer, ByVal testNames() As String, ByVal testPackageNames() As String) As PublicationService.PublicationHandlerIdentifier() Implements PublicationService.IPublicationService.GetAvailablePublicationHandlers
            Return MyBase.Channel.GetAvailablePublicationHandlers(bankId, testNames, testPackageNames)
        End Function

        Public Function GetAvailablePublicationHandlersAsync(ByVal bankId As Integer, ByVal testNames() As String, ByVal testPackageNames() As String) As System.Threading.Tasks.Task(Of PublicationService.PublicationHandlerIdentifier()) Implements PublicationService.IPublicationService.GetAvailablePublicationHandlersAsync
            Return MyBase.Channel.GetAvailablePublicationHandlersAsync(bankId, testNames, testPackageNames)
        End Function

        Public Function GetAllPublicationHandlers(ByVal bankId As Integer, ByVal testNames() As String, ByVal testPackageNames() As String) As PublicationService.PublicationHandlerIdentifier() Implements PublicationService.IPublicationService.GetAllPublicationHandlers
            Return MyBase.Channel.GetAllPublicationHandlers(bankId, testNames, testPackageNames)
        End Function

        Public Function GetAllPublicationHandlersAsync(ByVal bankId As Integer, ByVal testNames() As String, ByVal testPackageNames() As String) As System.Threading.Tasks.Task(Of PublicationService.PublicationHandlerIdentifier()) Implements PublicationService.IPublicationService.GetAllPublicationHandlersAsync
            Return MyBase.Channel.GetAllPublicationHandlersAsync(bankId, testNames, testPackageNames)
        End Function

        Public Function GetAllTestPreviewHandlers() As PublicationService.TestPreviewHandlerIdentifier() Implements PublicationService.IPublicationService.GetAllTestPreviewHandlers
            Return MyBase.Channel.GetAllTestPreviewHandlers
        End Function

        Public Function GetAllTestPreviewHandlersAsync() As System.Threading.Tasks.Task(Of PublicationService.TestPreviewHandlerIdentifier()) Implements PublicationService.IPublicationService.GetAllTestPreviewHandlersAsync
            Return MyBase.Channel.GetAllTestPreviewHandlersAsync
        End Function

        Public Function GetAvailableTestPreviewHandlers(ByVal bankId As Integer, ByVal testNames() As String, ByVal testPackages() As String) As PublicationService.TestPreviewHandlerIdentifier() Implements PublicationService.IPublicationService.GetAvailableTestPreviewHandlers
            Return MyBase.Channel.GetAvailableTestPreviewHandlers(bankId, testNames, testPackages)
        End Function

        Public Function GetAvailableTestPreviewHandlersAsync(ByVal bankId As Integer, ByVal testNames() As String, ByVal testPackages() As String) As System.Threading.Tasks.Task(Of PublicationService.TestPreviewHandlerIdentifier()) Implements PublicationService.IPublicationService.GetAvailableTestPreviewHandlersAsync
            Return MyBase.Channel.GetAvailableTestPreviewHandlersAsync(bankId, testNames, testPackages)
        End Function

        Public Function GetConfigurationOptions(ByVal publicationHandlerType As String, ByVal bankId As Integer, ByVal testNames() As String, ByVal testPackageNames() As String) As System.Collections.Generic.Dictionary(Of String, String) Implements PublicationService.IPublicationService.GetConfigurationOptions
            Return MyBase.Channel.GetConfigurationOptions(publicationHandlerType, bankId, testNames, testPackageNames)
        End Function

        Public Function GetConfigurationOptionsAsync(ByVal publicationHandlerType As String, ByVal bankId As Integer, ByVal testNames() As String, ByVal testPackageNames() As String) As System.Threading.Tasks.Task(Of System.Collections.Generic.Dictionary(Of String, String)) Implements PublicationService.IPublicationService.GetConfigurationOptionsAsync
            Return MyBase.Channel.GetConfigurationOptionsAsync(publicationHandlerType, bankId, testNames, testPackageNames)
        End Function

        Public Function Publicize(ByVal publicationHandlerType As String, ByVal configurationOptions As System.Collections.Generic.Dictionary(Of String, String), ByVal bankId As Integer, ByVal testNames() As String, ByVal testPackageNames() As String, ByVal isForPreview As Boolean, ByVal customName As String) As String Implements PublicationService.IPublicationService.Publicize
            Return MyBase.Channel.Publicize(publicationHandlerType, configurationOptions, bankId, testNames, testPackageNames, isForPreview, customName)
        End Function

        Public Function PublicizeAsync(ByVal publicationHandlerType As String, ByVal configurationOptions As System.Collections.Generic.Dictionary(Of String, String), ByVal bankId As Integer, ByVal testNames() As String, ByVal testPackageNames() As String, ByVal isForPreview As Boolean, ByVal customName As String) As System.Threading.Tasks.Task(Of String) Implements PublicationService.IPublicationService.PublicizeAsync
            Return MyBase.Channel.PublicizeAsync(publicationHandlerType, configurationOptions, bankId, testNames, testPackageNames, isForPreview, customName)
        End Function

        Public Function GetProgress(ByVal taskId As String) As PublicationService.PublicationTaskProgress Implements PublicationService.IPublicationService.GetProgress
            Return MyBase.Channel.GetProgress(taskId)
        End Function

        Public Function GetProgressAsync(ByVal taskId As String) As System.Threading.Tasks.Task(Of PublicationService.PublicationTaskProgress) Implements PublicationService.IPublicationService.GetProgressAsync
            Return MyBase.Channel.GetProgressAsync(taskId)
        End Function

        Public Sub FinishPublication(ByVal taskId As String) Implements PublicationService.IPublicationService.FinishPublication
            MyBase.Channel.FinishPublication(taskId)
        End Sub

        Public Function FinishPublicationAsync(ByVal taskId As String) As System.Threading.Tasks.Task Implements PublicationService.IPublicationService.FinishPublicationAsync
            Return MyBase.Channel.FinishPublicationAsync(taskId)
        End Function

        Public Function GetItemOutput(ByVal publicationHandlerType As String, ByVal bankId As Integer, ByVal itemCode As String) As String Implements PublicationService.IPublicationService.GetItemOutput
            Return MyBase.Channel.GetItemOutput(publicationHandlerType, bankId, itemCode)
        End Function

        Public Function GetItemOutputAsync(ByVal publicationHandlerType As String, ByVal bankId As Integer, ByVal itemCode As String) As System.Threading.Tasks.Task(Of String) Implements PublicationService.IPublicationService.GetItemOutputAsync
            Return MyBase.Channel.GetItemOutputAsync(publicationHandlerType, bankId, itemCode)
        End Function

        Public Function GetConceptRelatedResponseProcessingForReportingPurposes(ByVal publicationHandlerType As String, ByVal itemResourceId As System.Guid) As PublicationService.ConceptProcessingLabelEntry() Implements PublicationService.IPublicationService.GetConceptRelatedResponseProcessingForReportingPurposes
            Return MyBase.Channel.GetConceptRelatedResponseProcessingForReportingPurposes(publicationHandlerType, itemResourceId)
        End Function

        Public Function GetConceptRelatedResponseProcessingForReportingPurposesAsync(ByVal publicationHandlerType As String, ByVal itemResourceId As System.Guid) As System.Threading.Tasks.Task(Of PublicationService.ConceptProcessingLabelEntry()) Implements PublicationService.IPublicationService.GetConceptRelatedResponseProcessingForReportingPurposesAsync
            Return MyBase.Channel.GetConceptRelatedResponseProcessingForReportingPurposesAsync(publicationHandlerType, itemResourceId)
        End Function
    End Class
End Namespace
