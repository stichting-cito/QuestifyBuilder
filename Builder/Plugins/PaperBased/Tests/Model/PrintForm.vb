Imports System.ComponentModel
Imports System.Xml.Serialization
Imports Enums

Public Class PrintForm
    Implements INotifyPropertyChanged

    Private _type As PrintFormType
    Private _id As Guid
    Private _typeLabel As String
    Private _resourceName As String


    <XmlAttribute("type")>
    Public Property Type() As PrintFormType
        Get
            Return _type
        End Get
        Set(value As PrintFormType)
            _type = value
            OnPropertyChanged(New PropertyChangedEventArgs("Type"))
        End Set
    End Property

    <XmlAttribute("id")>
    Public Property Id() As Guid
        Get
            If _id = Guid.Empty Then
                _id = Guid.NewGuid
            End If
            Return _id
        End Get
        Set(value As Guid)
            _id = value
        End Set
    End Property

    <XmlAttribute("typelabel")>
    Public Property TypeLabel() As String
        Get
            If _type <> PrintFormType.UserDefinedBooklet AndAlso String.IsNullOrEmpty(_typeLabel) Then
                _typeLabel = LocalizedEnumConverter.ConvertToString(_type)
            End If
            Return _typeLabel
        End Get
        Set(value As String)
            _typeLabel = value
            OnPropertyChanged(New PropertyChangedEventArgs("TypeLabel"))
        End Set
    End Property

    <XmlAttribute("resourcename")>
    Public Property ResourceName() As String
        Get
            Return _resourceName
        End Get
        Set(value As String)
            _resourceName = value
            OnPropertyChanged(New PropertyChangedEventArgs("ResourceName"))
        End Set
    End Property



    Protected Overridable Sub OnPropertyChanged(e As PropertyChangedEventArgs)
        RaiseEvent PropertyChanged(Me, e)
    End Sub


    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged
    Public Sub New()

    End Sub
    Public Sub New(resourceName As String, type As PrintFormType)
        Me.ResourceName = resourceName
        Me.Type = type
    End Sub


End Class