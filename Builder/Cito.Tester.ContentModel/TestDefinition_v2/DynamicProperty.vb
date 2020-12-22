Imports System.ComponentModel
Imports System.Xml
Imports System.Xml.Serialization

Imports Cito.Tester.Common


<Serializable>
<XmlRoot("Property")>
Public Class DynamicProperty
    Implements INotifyPropertyChanged


    Private _innerValue As Object

    Private _mappedToViews As List(Of String)

    Private _name As String

    Private _typeName As String

    Private _value As XmlNode



    Public Sub New()
        _mappedToViews = New List(Of String)
    End Sub


    Public Sub New(name As String, typeName As String, mappedToViewType As String)
        Me.New()
        Me.Name = name
        Me.TypeName = typeName
        Me.MappedToViews.Add(mappedToViewType)
    End Sub



    Private Property InnerValue As Object
        Get
            Return _innerValue
        End Get
        Set
            _innerValue = value

            If TypeOf _innerValue Is INotifyPropertyChanged Then
                AddHandler DirectCast(_innerValue, INotifyPropertyChanged).PropertyChanged, AddressOf InnerProperty_Changed
            End If

            If TypeOf _innerValue Is IBindingList Then
                AddHandler DirectCast(_innerValue, IBindingList).ListChanged, AddressOf InnerProperty_ListChanged
            End If
        End Set
    End Property



    <XmlIgnore>
    Public ReadOnly Property MappedToViews As List(Of String)
        Get
            Return _mappedToViews
        End Get
    End Property

    <XmlAttribute("MappedToViews")>
    Public Property MappedToViewsString As String
        Get
            Dim returnValue As String = String.Empty

            If _mappedToViews.Count > 0 Then
                Dim stringList As New List(Of String)()

                For Each viewType In _mappedToViews
                    stringList.Add(viewType)
                Next

                returnValue = String.Join(",", stringList.ToArray())
            End If

            Return returnValue
        End Get
        Set
            _mappedToViews = New List(Of String)()

            If Not String.IsNullOrEmpty(value) Then
                For Each viewTypeString As String In value.Split(","c)
                    _mappedToViews.Add(viewTypeString)
                Next
            End If
        End Set
    End Property


    <XmlAttribute("Name")>
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


    <XmlAttribute("TypeName")>
    Public Property TypeName As String
        Get
            Return _typeName
        End Get
        Set
            If value <> _typeName Then
                _typeName = value
                Me.NotifyPropertyChanged("TypeName")
            End If
        End Set
    End Property


    <XmlAnyElement>
    Public Property Value As XmlNode
        Get
            Return _value
        End Get
        Set
            _value = value
            Me.NotifyPropertyChanged("Value")
        End Set
    End Property



    Private Sub InnerProperty_Changed(sender As Object, e As PropertyChangedEventArgs)
        If Me.InnerValue IsNot Nothing Then
            Dim serializedPropertyString As String = SerializeHelper.XmlSerializeToString(Me.InnerValue, True)
            Dim propertyXmlDocument As New XmlDocument
            propertyXmlDocument.LoadXml(serializedPropertyString)
            Me.Value = propertyXmlDocument.DocumentElement
        Else
            Me.Value = Nothing
        End If
    End Sub


    Private Sub InnerProperty_ListChanged(sender As Object, e As ListChangedEventArgs)
        Dim serializedPropertyString As String = SerializeHelper.XmlSerializeToString(Me.InnerValue, True)
        Dim propertyXmlDocument As New XmlDocument
        propertyXmlDocument.LoadXml(serializedPropertyString)
        Me.Value = propertyXmlDocument.DocumentElement
    End Sub

    Private Sub NotifyPropertyChanged(info As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(info))
    End Sub


    Private Function CheckType(currentType As String, serialisedType As String) As Boolean
        Dim returnValue As Boolean = False
        Const VERSION As String = ", version="

        If currentType = serialisedType OrElse (currentType.ToLower.Contains(VERSION) AndAlso serialisedType.ToLower.Contains(VERSION) AndAlso
               currentType.ToLower.Substring(0, currentType.ToLower.IndexOf(VERSION, 0)) = serialisedType.ToLower.Substring(0, serialisedType.ToLower.IndexOf(VERSION))) Then
            returnValue = True
        End If

        Return returnValue
    End Function




    Public Function GetValue(Of T)() As T
        If Not CheckType(GetType(T).FullName, _typeName) Then
            Throw New ArgumentException(
                $"Invalid type requested from this property. Request type: {GetType(T).FullName}, Expected type: {_typeName}")
        End If

        If Me.InnerValue Is Nothing AndAlso Me.Value IsNot Nothing Then
            Dim propertyXMLString As String = Me.Value.OuterXml
            Me.InnerValue = SerializeHelper.XmlDeserializeFromString(propertyXMLString, GetType(T))
        End If

        Return DirectCast(Me.InnerValue, T)
    End Function


    Public Sub SetValue(value As Object)
        If value IsNot Nothing AndAlso Not CheckType(value.GetType().FullName, _typeName) Then
            Throw New ArgumentException(
                $"Trying to set object of invalid type in this property. Object type: {value.GetType().FullName}, Expected type: {_typeName}")
        End If

        If value IsNot Me.InnerValue Then
            Dim oldNotifyPropertyChangedValue As INotifyPropertyChanged = TryCast(Me.InnerValue, INotifyPropertyChanged)
            If oldNotifyPropertyChangedValue IsNot Nothing Then
                RemoveHandler oldNotifyPropertyChangedValue.PropertyChanged, AddressOf InnerProperty_Changed
            End If

            Me.InnerValue = value

            InnerProperty_Changed(Me, New PropertyChangedEventArgs("innerValue"))
        End If
    End Sub




    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged


End Class