
Imports System.Xml.Serialization
Imports Cito.Tester.Common


<Serializable> _
Public MustInherit Class TestNodeBase


    Private _identifier As String
    Private _properties As DynamicPropertyCollection
    Private _state As ComponentState
    Private _title As String



    Public Sub New()
        MyBase.New()
        Me._properties = New DynamicPropertyCollection

        Me.State = ComponentState.Pickable
        Me.Identifier = String.Empty
        Me.Title = String.Empty
    End Sub



    <XmlAttribute("Identifier")> _
    Public Property Identifier As String
        Get
            Return Me._identifier
        End Get
        Set
            _identifier = value
        End Set
    End Property

    <XmlArray("DynamicProperties"), _
  XmlArrayItem(GetType(DynamicProperty), ElementName:="Property")> _
    Public ReadOnly Property Properties As DynamicPropertyCollection
        Get
            Return _properties
        End Get
    End Property

    <XmlIgnore> _
    Public Property State As ComponentState
        Get
            Return _state
        End Get
        Set
            _state = value
        End Set
    End Property

    <XmlAttribute("Title")> _
    Public Property Title As String
        Get
            Return _title
        End Get
        Set
            _title = value
        End Set
    End Property



    Public Sub AddDynamicProperty(name As String, propertyType As Type, mappedToViewType As String)
        AddDynamicProperty(name, propertyType, mappedToViewType, Nothing)
    End Sub

    Public Sub AddDynamicProperty(name As String, propertyType As Type, mappedToViewType As String, defaultValue As Object)
        If Not Me.Properties.ContainsProperty(name) Then
            Me.Properties.Add(New DynamicProperty(name, propertyType.FullName, mappedToViewType))

            If defaultValue IsNot Nothing Then
                Me.Properties(name).SetValue(defaultValue)
            End If
        Else
            Throw New ContentModelException($"Cannot add dynamic property: property with name '{name}' already exists!")
        End If
    End Sub

    Public Sub AddDynamicPropertyIfNotExists(name As String, propertyType As Type, mappedToViewType As String, defaultValue As Object)
        If Not Me.ContainsDynamicProperty(name) Then
            AddDynamicProperty(name, propertyType, mappedToViewType, defaultValue)
        Else
            Dim prop As DynamicProperty = Me.Properties(name)
            If mappedToViewType = Nothing Then
                prop.MappedToViews.Clear()
            Else

                If Not prop.MappedToViews.Contains(mappedToViewType) Then
                    prop.MappedToViews.Add(mappedToViewType)
                End If
            End If
        End If
    End Sub

    Public Function ContainsDynamicProperty(name As String) As Boolean
        Return Me.Properties.ContainsProperty(name)
    End Function


    Public Function GetPropertyValue(Of T)(name As String) As T
        Dim prop As DynamicProperty = Me.Properties(name)
        Dim returnValue As T

        If prop IsNot Nothing Then
            returnValue = prop.GetValue(Of T)()
        Else
            Throw New ContentModelException(
                $"Cannot get dynamic property value: property with name '{name}' doesn't exist!")
        End If

        Return returnValue
    End Function

    Public Sub RemoveDynamicProperty(name As String)
        If Me.Properties.ContainsProperty(name) Then
            Dim prop As DynamicProperty = Me.Properties(name)
            Me.Properties.Remove(prop)
        Else
            Throw New ContentModelException(
                $"Cannot remove dynamic property: property with name '{name}' doesn't exist!")
        End If
    End Sub

    Public Sub SetPropertyValue(name As String, value As Object)
        Dim prop As DynamicProperty = Me.Properties(name)

        If prop IsNot Nothing Then
            prop.SetValue(value)
        Else
            Throw New ContentModelException(
                $"Cannot set dynamic property value: property with name '{name}' doesn't exist!")
        End If
    End Sub



    Public Function GetMD5Hash() As Byte()
        Return SerializeHelper.GetMD5Hash(Me)
    End Function


End Class