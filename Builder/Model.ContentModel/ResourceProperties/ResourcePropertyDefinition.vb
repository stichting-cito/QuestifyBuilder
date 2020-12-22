Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports System.Collections.Generic
Imports Enums

Namespace Questify.Builder.Model.ContentModel.ResourceProperties


    Public NotInheritable Class ResourcePropertyDefinition


        Public Delegate Function RetrieveValuesFromResourceFunction(ByVal resourceEntity As ResourceEntity, ByVal propertyDefinition As ResourcePropertyDefinition) As IList(Of Object)



        Public Enum PropertyTypeEnum
            [Static] = 0
            Dynamic = 1
        End Enum

        Public Enum PropertyValueTypeEnum
            FreeText = 0
            SingleListValue = 1
            MultiListValue = 2
        End Enum

        Public Enum ListTypeEnum
            NoList = 0
            List = 1
            Tree = 2
            Concept = 3
        End Enum


        Private _key As Guid
        Private _name As String
        Private _title As String
        Private _publishable As Boolean
        Private _scorable As Boolean
        Private _applicableToMask As ResourceTypeEnum
        Private _returnType As Type
        Private _propertyType As PropertyTypeEnum
        Private _propertyValueType As PropertyValueTypeEnum
        Private _listType As ListTypeEnum
        Private _listValues As IList(Of ResourcePropertyListValueDefinition)
        Private _valueRetrieveFromResourceFunction As RetrieveValuesFromResourceFunction
        Private _relatedFilterEntity As String
        Private _relatedFilterField As String
        Private _filterOnListNameColumn As Boolean



        Public Sub New()
        End Sub

        Public Sub New(ByVal key As Guid, ByVal name As String, ByVal title As String, ByVal publishable As Boolean, ByVal scorable As Boolean, ByVal applicableToMask As ResourceTypeEnum, ByVal propertyType As PropertyTypeEnum, ByVal returnType As Type,
                       ByVal propertyValueType As PropertyValueTypeEnum, ByVal listType As ListTypeEnum, ByVal listValues As IList(Of ResourcePropertyListValueDefinition), ByVal ValueRetrieveFromResourceFunctionDelegate As RetrieveValuesFromResourceFunction,
                       Optional filterOnListNameColumn As Boolean = False)
            _key = key
            _name = name
            _title = title
            _publishable = publishable
            _scorable = scorable
            _applicableToMask = applicableToMask
            _propertyType = propertyType
            _returnType = returnType
            _propertyValueType = propertyValueType
            _listType = listType
            _listValues = listValues
            _valueRetrieveFromResourceFunction = ValueRetrieveFromResourceFunctionDelegate
            _filterOnListNameColumn = filterOnListNameColumn
            Code = key
        End Sub



        Public ReadOnly Property FilterOnListNameColumn() As Boolean
            Get
                Return _filterOnListNameColumn
            End Get
        End Property

        Public ReadOnly Property Key() As Guid
            Get
                Return _key
            End Get
        End Property

        Public ReadOnly Property Name() As String
            Get
                Return _name
            End Get
        End Property

        Public Property Title() As String
            Get
                Return _title
            End Get
            Set(ByVal value As String)
                _title = value
            End Set
        End Property

        Public Property PropertyType() As PropertyTypeEnum
            Get
                Return _propertyType
            End Get
            Set(ByVal value As PropertyTypeEnum)
                _propertyType = value
            End Set
        End Property

        Public Property PropertyListValue() As PropertyValueTypeEnum
            Get
                Return _propertyValueType
            End Get
            Set(ByVal value As PropertyValueTypeEnum)
                _propertyValueType = value
            End Set
        End Property

        Public Property ApplicableToMask() As ResourceTypeEnum
            Get
                Return _applicableToMask
            End Get
            Set(ByVal value As ResourceTypeEnum)
                _applicableToMask = value
            End Set
        End Property

        Public ReadOnly Property ReturnType() As Type
            Get
                Return _returnType
            End Get
        End Property

        Public ReadOnly Property ListType() As ListTypeEnum
            Get
                Return _listType
            End Get
        End Property

        Public ReadOnly Property ListValues() As IList(Of ResourcePropertyListValueDefinition)
            Get
                Return _listValues
            End Get
        End Property


        Public Property Code As Guid



        Public Function RetrievePropertyValuesFromResource(ByVal resourceEntity As ResourceEntity) As IList(Of Object)
            Return _valueRetrieveFromResourceFunction.Invoke(resourceEntity, Me)
        End Function


    End Class
End Namespace