Imports System.Xml.Serialization

Public Class CollectionParameter
    Inherits Parameter(Of ParameterSetCollection)
    Implements ICollectionParameter

    Private _blueprint As ParameterCollection
    Private WithEvents _parameterSetCollection As ParameterSetCollection

    <XmlElement("subparameterset", GetType(ParameterCollection))> _
    Public Overrides Property Value As ParameterSetCollection Implements ICollectionParameter.Collection
        Get
            Return _parameterSetCollection
        End Get
        Set
            If _parameterSetCollection IsNot Nothing Then
                Throw New ArgumentException(My.Resources.Error_ChoiceCollectionParameter_Value_SetValueNotAllowed)
            End If

            _parameterSetCollection = value
            AddHandler _parameterSetCollection.ListChanged, Sub() NotifyPropertyChanged("Value")
        End Set
    End Property

    <XmlElement("definition", GetType(ParameterCollection))> _
    Public Property BluePrint As ParameterCollection Implements ICollectionParameter.BluePrint
        Get
            Return _blueprint
        End Get
        Set
            If _blueprint IsNot Nothing Then
                Throw New ArgumentException(My.Resources.Error_ChoiceCollectionParameter_Value_SetValueNotAllowed)
            End If
            _blueprint = value
        End Set
    End Property

    Public Overrides Function SetValue(value As String) As Boolean
        Return True
    End Function

End Class