Imports System.Xml.Serialization

Public Class CustomInteractionResourceParameter
    Inherits ResourceParameter

    Public Enum CustomInteractionCommunicationType
        Answer
        State
        None
    End Enum

    Private _height As Integer?
    Private _width As Integer?
    Private _scalable As Boolean
    Private _interactionCount As Integer
    Private _inlineUsage As Boolean
    Private _scorable As Boolean
    Private _communicationType As CustomInteractionCommunicationType


    <XmlAttribute("scalable")>
    Public Property Scalable As Boolean
        Get
            Return _scalable
        End Get
        Set(scaleValue As Boolean)
            If scaleValue <> _scalable Then
                _scalable = scaleValue
                NotifyPropertyChanged("Scalable")
            End If
        End Set
    End Property

    <XmlAttribute("interactioncount")>
    Public Property InteractionCount As Integer
        Get
            Return _interactionCount
        End Get
        Set(interactionCountValue As Integer)
            If interactionCountValue <> _interactionCount Then
                _interactionCount = interactionCountValue
                NotifyPropertyChanged("InteractionCount")
            End If
        End Set
    End Property

    <XmlAttribute("inlineUsage")>
    Public Property InlineUsage As Boolean
        Get
            Return _inlineUsage
        End Get
        Set
            _inlineUsage = value
        End Set
    End Property

    <XmlAttribute("communicationType")>
    Public Property CommunicationType As CustomInteractionCommunicationType
        Get
            Return _communicationType
        End Get
        Set(communicationTypeValue As CustomInteractionCommunicationType)
            If communicationTypeValue <> _communicationType Then
                _communicationType = communicationTypeValue
                NotifyPropertyChanged("CommunicationType")
            End If
        End Set
    End Property

    <XmlAttribute("scorable")>
    Public Property Scorable As Boolean
        Get
            Return _scorable
        End Get
        Set(scorableValue As Boolean)
            If scorableValue <> _scorable Then
                _scorable = scorableValue
                NotifyPropertyChanged("Scorable")
            End If
        End Set
    End Property

End Class