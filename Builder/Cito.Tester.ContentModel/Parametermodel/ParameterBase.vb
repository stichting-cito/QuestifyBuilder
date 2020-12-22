Imports System.Xml
Imports System.ComponentModel
Imports System.Xml.Serialization

<Serializable>
Public MustInherit Class ParameterBase
    Implements INotifyPropertyChanged

    Private _name As String

    <NonSerialized> Private _nodes() As XmlNode

    <NonSerialized> Private _designerSettings As DesignerSettingCollection

    <NonSerialized> Private _attributeReferences As AttributeReferenceCollection

    <XmlAttribute("name")>
    Public Property Name As String
        Get
            Return _name
        End Get
        Set
            If value <> Me.Name Then
                Me._name = value
                NotifyPropertyChanged("Name")
            End If
        End Set
    End Property

    <XmlIgnore>
    Public Property DesignerSettings As DesignerSettingCollection
        Get
            Return Me._designerSettings
        End Get
        Set
            Me._designerSettings = value
        End Set
    End Property

    <XmlIgnore>
    Public Property AttributeReferences As AttributeReferenceCollection
        Get
            Return Me._attributeReferences
        End Get
        Set
            Me._attributeReferences = value
        End Set
    End Property

    <XmlAnyElement, XmlText>
    Public Property Nodes As XmlNode()
        Get
            Return _nodes
        End Get
        Set
            Me._nodes = value
            NotifyPropertyChanged("Nodes")
        End Set
    End Property



    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged

    Protected Sub NotifyPropertyChanged(info As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(info))
    End Sub


    Public Sub New()
        _designerSettings = New DesignerSettingCollection()
        _attributeReferences = New AttributeReferenceCollection()
    End Sub

    Public MustOverride Function SetValue(value As String) As Boolean

    Public MustOverride Function EqualsByValue(param As ParameterBase) As Boolean

End Class
