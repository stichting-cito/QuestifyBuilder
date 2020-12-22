Imports System.ComponentModel
Imports System.Xml.Serialization

<Serializable, XmlRoot(ElementName:="testPartCalculatorMode")>
Public Class TestPartCalculatorMode
    Implements INotifyPropertyChanged

    Private _calculatorAllowed As Boolean
    Private _calculatorSwitchAllowed As Boolean

    <XmlElement("calculatorAllowed")>
    Public Property CalculatorAllowed As Boolean
        Get
            Return _calculatorAllowed
        End Get
        Set
            _calculatorAllowed = value
            OnPropertyChanged("calculatorAllowed")
        End Set
    End Property

    <XmlElement("calculatorSwitchAllowed")>
    Public Property CalculatorSwitchAllowed As Boolean
        Get
            Return _calculatorSwitchAllowed
        End Get
        Set
            _calculatorSwitchAllowed = value
            OnPropertyChanged("calculatorSwitchAllowed")
        End Set
    End Property

    Public Sub New()

    End Sub

    Public Sub New(calculatorAllowed As Boolean, calculatorSwitchAllowed As Boolean)
        _calculatorAllowed = calculatorAllowed
        _calculatorSwitchAllowed = calculatorSwitchAllowed
    End Sub

    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged

    Protected Sub OnPropertyChanged(propertyName As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
    End Sub

End Class
