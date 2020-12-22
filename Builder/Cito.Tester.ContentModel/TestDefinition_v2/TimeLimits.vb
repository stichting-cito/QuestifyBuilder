Imports System.ComponentModel
Imports System.Xml.Serialization

<Serializable> _
<XmlRoot(ElementName:="timeLimits")> _
Public Class TimeLimits
    Implements INotifyPropertyChanged

    Private _minTime As Integer
    Private _maxTime As Integer


    <XmlAttribute("minTime")> _
    Public Property MinTime As Integer
        Get
            Return _minTime
        End Get
        Set
            _minTime = value
            OnPropertyChanged(New PropertyChangedEventArgs("MinTime"))
        End Set
    End Property


    <XmlAttribute("maxTime")> _
    Public Property MaxTime As Integer
        Get
            Return _maxTime
        End Get
        Set
            _maxTime = value
            OnPropertyChanged(New PropertyChangedEventArgs("MaxTime"))
        End Set
    End Property


    Protected Overridable Sub OnPropertyChanged(e As PropertyChangedEventArgs)
        RaiseEvent PropertyChanged(Me, e)
    End Sub



    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged



    Public Sub New(minTime As Integer, maxTime As Integer)
        _minTime = minTime
        _maxTime = maxTime
    End Sub

    Public Sub New()
    End Sub

End Class
