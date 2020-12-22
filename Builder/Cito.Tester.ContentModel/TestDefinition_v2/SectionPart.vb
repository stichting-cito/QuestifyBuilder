
Imports System.ComponentModel
Imports System.Xml.Serialization

<XmlRoot(ElementName:="sectionPart")> _
Public Class SectionPart
    Implements INotifyPropertyChanged

    Private _required As Boolean = False
    Private _fixed As Boolean = False
    Private _timeLimits As TimeLimits
    Private _itemSessionControl As ItemSessionControl



    <XmlAttribute("required")> _
    Public Property Required As Boolean
        Get
            Return _required
        End Get
        Set
            _required = value
            OnPropertyChanged(New PropertyChangedEventArgs("Required"))
        End Set
    End Property

    <XmlIgnore> _
    Public Property RequiredSpecified As Boolean

    <XmlAttribute("fixed")> _
    Public Property Fixed As Boolean
        Get
            Return _fixed
        End Get
        Set
            _fixed = value
            OnPropertyChanged(New PropertyChangedEventArgs("Fixed"))
        End Set
    End Property

    <XmlIgnore> _
    Public Property FixedSpecified As Boolean

    <XmlElement("timeLimits")> _
    Public Property TimeLimits As TimeLimits
        Get
            Return _timeLimits
        End Get
        Set
            _timeLimits = value
            OnPropertyChanged(New PropertyChangedEventArgs("TimeLimits"))
        End Set
    End Property

    <XmlElement("itemSessionControl")> _
    Public Property ItemSessionControl As ItemSessionControl
        Get
            Return _itemSessionControl
        End Get
        Set
            _itemSessionControl = value
            OnPropertyChanged(New PropertyChangedEventArgs("ItemSessionControl"))
        End Set
    End Property


    Protected Overridable Sub OnPropertyChanged(e As PropertyChangedEventArgs)
        RaiseEvent PropertyChanged(Me, e)
    End Sub



    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged




    Public Sub New(required As Boolean, fixed As Boolean, timeLimits As TimeLimits, itemSessionControl As ItemSessionControl)

        _required = required
        _fixed = fixed
        _timeLimits = timeLimits
        _itemSessionControl = itemSessionControl
    End Sub

    Public Sub New()
    End Sub

End Class
