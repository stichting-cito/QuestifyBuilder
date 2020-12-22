Imports System.Xml.Serialization
Imports Cito.Tester.Common

Namespace Datasources

    <XmlRoot("ResourceRef")>
    Public Class ResourceRef


        Private _identifier As String
        Private _properties As New SerializableGenericDictionary(Of String, String)



        Public Sub New()
        End Sub

        Public Sub New(identifier As String)
            _identifier = identifier
        End Sub




        <XmlAttribute("identifier")>
        Public Property Identifier As String
            Get
                Return _identifier
            End Get
            Set
                _identifier = value
            End Set
        End Property

        Public Property Properties As SerializableGenericDictionary(Of String, String)
            Get
                Return _properties
            End Get
            Set
                _properties = value
            End Set
        End Property




        Public Shared Function FromItemReference(itemReference As ItemReference2) As ResourceRef
            Dim reference As New ResourceRef(itemReference.Title)

            reference.Properties.Add("IsAnchorItem", itemReference.IsAnchorItem.ToString())
            Return reference
        End Function


        Public Shared Function FromItemReferences(itemReferences As IEnumerable(Of ItemReference2)) As IList(Of ResourceRef)
            Dim returnValue As New List(Of ResourceRef)

            For Each ref As ItemReference2 In itemReferences
                returnValue.Add(FromItemReference(ref))
            Next

            Return returnValue
        End Function

        Public Shared Function GetByProperty(resourceRefs As IEnumerable(Of ResourceRef), propertyName As String, propertyValue As String) As IEnumerable(Of ResourceRef)
            Dim returnValue As New List(Of ResourceRef)

            For Each resourceRef As ResourceRef In resourceRefs
                If resourceRef.Properties.ContainsKey(propertyName) AndAlso resourceRef.Properties(propertyName) = propertyValue Then
                    returnValue.Add(resourceRef)
                End If
            Next

            Return returnValue
        End Function

        Public Shared Function ToIdentifierList(resourceRefs As IEnumerable(Of ResourceRef)) As IEnumerable(Of String)
            Dim identifierListToReturn As New List(Of String)

            If resourceRefs IsNot Nothing Then
                For Each ref As ResourceRef In resourceRefs
                    identifierListToReturn.Add(ref.Identifier)
                Next
            End If

            Return identifierListToReturn
        End Function

        <Obsolete>
        Public Shared Function ToFlatIdentifierList(resourceRefs As IEnumerable(Of ResourceRef)) As IEnumerable(Of String)
            Return ToIdentifierList(resourceRefs)
        End Function

        Public Shared Function GetByItemIdentifier(list As IEnumerable(Of ResourceRef), identifier As String) As ResourceRef

            For Each ref As ResourceRef In list
                If ref.Identifier = identifier Then
                    Return ref
                End If
            Next

            Return Nothing
        End Function


        Public Overrides Function ToString() As String
            Return Me.Identifier
        End Function

        Public Overrides Function Equals(obj As Object) As Boolean
            Dim y As ResourceRef = TryCast(obj, ResourceRef)
            If (y IsNot Nothing)
                Return Identifier = y.Identifier
            End If
            Return False
        End Function

        Public Overrides Function GetHashCode() As Integer
            Return Identifier.GetHashCode()
        End Function


    End Class

    Public Class ResourceRefIdentityEqualityComparer
        Implements IEqualityComparer(Of ResourceRef)



        Public Function Equals1(x As ResourceRef, y As ResourceRef) As Boolean Implements IEqualityComparer(Of ResourceRef).Equals
            Return x.Identifier = y.Identifier
        End Function

        Public Function GetHashCode1(obj As ResourceRef) As Integer Implements IEqualityComparer(Of ResourceRef).GetHashCode
            Return obj.Identifier.GetHashCode
        End Function


    End Class

End Namespace

