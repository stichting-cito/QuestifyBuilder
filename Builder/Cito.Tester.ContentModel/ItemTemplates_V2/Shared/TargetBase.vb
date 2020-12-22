Imports System.ComponentModel
Imports System.Xml.Serialization

<Serializable> _
<XmlInclude(GetType(ControlTemplateTarget))> _
<XmlInclude(GetType(ItemLayoutTemplateTarget))> _
<XmlRoot("Target")> _
Public MustInherit Class TargetBase
    Implements INotifyPropertyChanged


    Private _description As String
    Private _enabled As Boolean = True
    Private _name As String
    Private _template As CDATA




    Public Sub New(name As String)
        Me.Name = name
    End Sub


    Public Sub New(name As String, description As String)
        Me.New(name)
        Me.Description = description
    End Sub


    Public Sub New()
    End Sub



    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged




    <XmlElement("Description")> _
    Public Property Description As String
        Get
            Return _description
        End Get
        Set
            SetValueWithChangeNotification(Of String)("Description", _description, value)
        End Set
    End Property


    <XmlAttribute("enabled")> _
    Public Property Enabled As Boolean
        Get
            Return _enabled
        End Get
        Set
            SetValueWithChangeNotification(Of Boolean)("Enabled", _enabled, value)
        End Set
    End Property


    <XmlAttribute("name")> _
    Public Property Name As String
        Get
            Return _name
        End Get
        Set
            SetValueWithChangeNotification("Name", _name, value)
        End Set
    End Property


    <XmlElement("Template", GetType(CDATA))> _
    Public Property Template As CDATA
        Get
            Return _template
        End Get
        Set
            SetValueWithChangeNotification("Template", _template, value)
        End Set
    End Property



    Friend Sub SetValueWithChangeNotification(Of T)(propertyName As String, ByRef oldValue As T, newValue As T)
        If oldValue Is Nothing OrElse Not oldValue.Equals(newValue) Then
            oldValue = newValue
            NotifyPropertyChanged(propertyName)
        End If
    End Sub

    Private Sub NotifyPropertyChanged(info As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(info))
    End Sub


End Class