Imports System.ComponentModel
Imports System.Xml.Serialization

Public MustInherit Class TestSectionDefaultSettings
    Implements INotifyPropertyChanged

    Private _maxTime As Integer
    Private _minTime As Integer

    <XmlAttribute("maxTime")> _
    Public Property MaxTime As Integer
        Get
            Return _maxTime
        End Get
        Set
            _maxTime = value
            Me.NotifyPropertyChanged("MaxTime")
        End Set
    End Property

    <XmlAttribute("minTime")> _
    Public Property MinTime As Integer
        Get
            Return _minTime
        End Get
        Set
            _minTime = value
            Me.NotifyPropertyChanged("MinTime")
        End Set
    End Property
    Protected Sub NotifyPropertyChanged(info As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(info))
    End Sub

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
End Class
