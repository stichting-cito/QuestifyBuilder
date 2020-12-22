Imports System.Diagnostics.CodeAnalysis
Imports Cito.Tester.Common



Public Class ResourceNeededEventArgs
    Inherits EventArgs


    Private _resourceType As ResourceType
    Private _resourceName As String
    Private _binaryResource As BinaryResource
    Private _streamProcessingDelegate As ResourceProcessingFunction
    Private _typedResourceType As Type
    Private _command As ResourceNeededCommand
    Private _metadata As New MetaDataCollection



    Public ReadOnly Property ResourceType As ResourceType
        Get
            Return _resourceType
        End Get
    End Property


    Public ReadOnly Property ResourceName As String
        Get
            Return _resourceName
        End Get
    End Property

    Public Property BinaryResource As BinaryResource
        Get
            Return _binaryResource
        End Get
        Set
            _binaryResource = value
        End Set
    End Property

    Public Property TypedResourceType As Type
        Get
            Return _typedResourceType
        End Get
        Set
            _typedResourceType = value
        End Set
    End Property

    Public ReadOnly Property StreamProcessingDelegate As ResourceProcessingFunction
        Get
            Return _streamProcessingDelegate
        End Get
    End Property

    Public ReadOnly Property Command As ResourceNeededCommand
        Get
            Return _command
        End Get
    End Property

    Public ReadOnly Property Metadata As MetaDataCollection
        Get
            Return _metadata
        End Get
    End Property



    <SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")> _
    Public Function GetResource(Of TResource)() As TResource
        ReflectionHelper.CheckExpectedType(Me.BinaryResource.ResourceObject, GetType(TResource), GetType(TResource).FullName)

        Return DirectCast(Me.BinaryResource.ResourceObject, TResource)
    End Function



    Public Sub New(name As String, typedResourceType As Type)
        _resourceName = name
        _typedResourceType = typedResourceType
        _command = ResourceNeededCommand.Resource
    End Sub

    Public Sub New(name As String, typedResourceType As Type, command As ResourceNeededCommand)
        Me.New(name, typedResourceType)
        _command = command
    End Sub

    Public Sub New(name As String, streamProcessingDelegate As ResourceProcessingFunction)
        _resourceName = name
        _streamProcessingDelegate = streamProcessingDelegate
        _command = ResourceNeededCommand.Resource
    End Sub




End Class


Public Enum ResourceType
    InteractionControlAssembly
    ItemMediaResource
    ItemLayoutTemplate
    ControlTemplate
    AssessmentItem
    AssessmentTest
End Enum

Public Enum ResourceNeededCommand
    Resource = 1
    MetaData = 2
End Enum



