Imports System.Xml.Serialization

<XmlRoot("ResourcePropertyFilter")> _
Public Class ResourcePropertyFilterPredicate
    Inherits ItemFilterPredicate


    Private _op As FilterOperatorEnum
    Private _propertyKey As Guid = Guid.Empty
    Private _propertyName As String = String.Empty
    Private _selectedListValueKey As Guid = Guid.Empty
    Private _selectedListValueName As String = String.Empty
    Private _value As Object = Nothing
    Private _displayValue As String = String.Empty



    Sub New()
    End Sub



    Public Enum FilterOperatorEnum
        EQUAL
        NOT_EQUAL
        GREATER_THAN
        GREATER_THAN_OR_EQUAL
        LESS_THAN
        LESS_THAN_OR_EQUAL
    End Enum



    Public Overrides ReadOnly Property Name() As String
        Get
            Return My.Resources.ResourcePropertyFilterPredicateName
        End Get
    End Property

    Public Overrides ReadOnly Property NameLocalized() As String
        Get
            Return My.Resources.ResourcePropertyFilterPredicateNameLocalized
        End Get
    End Property

    <XmlAttribute("operator")> _
    Public Property Op() As FilterOperatorEnum
        Get
            Return _op
        End Get
        Set(ByVal value As FilterOperatorEnum)
            _op = value
        End Set
    End Property

    <XmlAttribute("propertykey")> _
    Public Property PropertyKey() As Guid
        Get
            Return _propertyKey
        End Get
        Set(ByVal value As Guid)
            _propertyKey = value
        End Set
    End Property

    <XmlAttribute("propertyName")> _
    Public Property PropertyName() As String
        Get
            Return _propertyName
        End Get
        Set(ByVal value As String)
            _propertyName = value
        End Set
    End Property

    <XmlAttribute("selectedListValueKey")> _
    Public Property SelectedListValueKey() As Guid
        Get
            Return _selectedListValueKey
        End Get
        Set(ByVal value As Guid)
            _selectedListValueKey = value
        End Set
    End Property

    <XmlAttribute("SelectedListValueName")> _
    Public Property SelectedListValueName() As String
        Get
            Return _selectedListValueName
        End Get
        Set(ByVal value As String)
            _selectedListValueName = value
        End Set
    End Property

    <XmlElement("DisplayValue")> _
    Public Property DisplayValue() As String
        Get
            If Not String.IsNullOrEmpty(_displayValue) Then
                Return _displayValue
            Else
                If _value Is Nothing Then
                    Return String.Empty
                End If
                Return _value.ToString()
            End If
        End Get
        Set(value As String)
            _displayValue = value
        End Set
    End Property

    <XmlElement("Value")> _
    Public Property Value() As Object
        Get
            Return _value
        End Get
        Set(ByVal value As Object)
            _value = value
        End Set
    End Property



    Public Overrides Function ToString() As String
        Return String.Format("{0}: [{1}] {2} ""{3}""", Me.NameLocalized, Me.PropertyName, Me.Op, Me.DisplayValue)
    End Function



End Class