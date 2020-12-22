Imports System.ComponentModel
Imports System.Xml.Serialization

<Serializable> _
<XmlRoot("testSectionAdaptiveItemSelectorSettings")> _
Public Class TestSectionAdaptiveItemSelectorSettings
    Inherits TestSectionDefaultSettings
    Implements INotifyPropertyChanged

    Private _otdFile As String

    <XmlAttribute("otdFile")> _
    Public Property OtdFile As String
        Get
            Return _otdFile
        End Get
        Set
            _otdFile = value
            Me.NotifyPropertyChanged("OtdFile")
        End Set
    End Property

End Class