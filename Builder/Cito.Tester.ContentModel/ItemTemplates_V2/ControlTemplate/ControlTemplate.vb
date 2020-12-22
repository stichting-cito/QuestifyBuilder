Imports System.Xml.Serialization

<Serializable> _
<XmlRoot("Template")> _
Public Class ControlTemplate
    Inherits TemplateBase(Of ControlTemplateTarget)


    Private _sharedFunctions As CDATA = New CDATA("")
    Private _sharedParameterSet As New ParameterCollection



    Public Sub New()
    End Sub



    <XmlElement("SharedFunctions", GetType(CDATA))> _
    Public Property SharedFunctions As CDATA
        Get
            Return _sharedFunctions
        End Get
        Set
            SetValueWithChangeNotification("SharedFunctions", _sharedFunctions, value)
        End Set
    End Property

    <XmlElement("SharedParameterSet")> _
    Public Property SharedParameterSet As ParameterCollection
        Get
            Return Me._sharedParameterSet
        End Get
        Set
            Me._sharedParameterSet = value
        End Set
    End Property


End Class