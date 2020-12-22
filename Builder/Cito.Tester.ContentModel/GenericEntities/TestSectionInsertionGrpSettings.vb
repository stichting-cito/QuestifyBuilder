Imports System.ComponentModel
Imports System.Xml.Serialization

<Serializable> _
<XmlRoot("testSectionInsertionGrpSettings")> _
Public Class TestSectionInsertionGrpSettings
    Implements INotifyPropertyChanged


    Private _insertionMethod As enumInsertionMethod
    Private _insertionModulo As Integer




    <XmlAttribute("insertionMethod")> _
    Public Property InsertionMethod As enumInsertionMethod
        Get
            Return _insertionMethod
        End Get
        Set
            If value <> Me.InsertionMethod Then
                _insertionMethod = value
                Me.NotifyPropertyChanged("InsertionMethod")
            End If
        End Set
    End Property


    <XmlAttribute("insertionModulo")> _
    Public Property InsertionModulo As Integer
        Get
            Return _insertionModulo
        End Get
        Set
            If value <> Me.InsertionModulo Then
                _insertionModulo = value
                Me.NotifyPropertyChanged("InsertionModulo")
            End If
        End Set
    End Property




    Private Sub NotifyPropertyChanged(info As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(info))
    End Sub




    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged


End Class

Public Enum enumInsertionMethod
    linear = 0
    random = 1
End Enum