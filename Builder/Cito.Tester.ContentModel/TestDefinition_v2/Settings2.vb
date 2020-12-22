Imports System.ComponentModel
Imports System.Xml
Imports System.Xml.Serialization

Imports Cito.Tester.Common


<Serializable> _
<XmlType("CustomSettingSet")> _
Public Class Settings2
    Implements INotifyPropertyChanged


    Private _name As String

    Private _settings As XmlNode



    Public Sub New()
    End Sub

    Public Sub New(name As String, settings As XmlNode)
        Me.Name = name
        Me.Settings = settings
    End Sub


    Public Sub New(name As String, settings As Object)
        Me.Name = name

        SetTypedSettingSet(settings)
    End Sub




    <XmlAttribute("Name")> _
    Public Property Name As String
        Get
            Return _name
        End Get
        Set
            If value <> Me.Name Then
                _name = value
                Me.NotifyPropertyChanged("Name")
            End If
        End Set
    End Property


    <XmlAnyElement> _
    Public Property Settings As XmlNode
        Get
            Return _settings
        End Get
        Set
            _settings = value
            Me.NotifyPropertyChanged("Settings")
        End Set
    End Property



    Private Sub NotifyPropertyChanged(info As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(info))
    End Sub





    Public Function GetTypedSettingSet(Of T)() As T
        Dim returnValue As T = Nothing

        If Me.Settings IsNot Nothing Then
            returnValue = DirectCast(SerializeHelper.XmlDeserializeFromString(Me.Settings.OuterXml, GetType(T)), T)
        End If

        Return returnValue
    End Function


    Public Sub SetTypedSettingSet(settings As Object)
        Dim settingSerializeString As String = SerializeHelper.XmlSerializeToString(settings)
        Dim settingXmlDocument As New XmlDocument
        settingXmlDocument.LoadXml(settingSerializeString)

        Me.Settings = settingXmlDocument.DocumentElement
    End Sub



    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged


End Class