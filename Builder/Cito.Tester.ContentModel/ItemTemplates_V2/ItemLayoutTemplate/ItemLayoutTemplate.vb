Imports System.Xml.Serialization

<Serializable>
<XmlRoot("Template")>
Public Class ItemLayoutTemplate
    Inherits TemplateBase(Of ItemLayoutTemplateTarget)


    Private _designerSettings As DesignerSettingCollection
    Private _groupCollection As GroupCollection


    <XmlArray("Settings"),
XmlArrayItem("DesignerSetting", GetType(DesignerSetting))>
    Public Property DesignerSettings As DesignerSettingCollection
        Get
            Return _designerSettings
        End Get
        Set
            _designerSettings = Value
        End Set
    End Property


    <XmlArray("Groups"),
XmlArrayItem("Groups", GetType(Group))>
    Public Property Groups As GroupCollection
        Get
            Return _groupCollection
        End Get
        Set
            _groupCollection = Value
        End Set
    End Property



    Public Sub New()
        MyBase.New()
        _designerSettings = New DesignerSettingCollection
        _groupCollection = New GroupCollection
    End Sub
End Class