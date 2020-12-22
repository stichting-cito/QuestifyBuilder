Imports System.Globalization
Imports System.Reflection

Public Interface IAttribute(Of T)
    ReadOnly Property Value() As T
End Interface

Public NotInheritable Class CasScoringOperatorAttribute
    Inherits Attribute
    Implements IAttribute(Of Boolean)

    Public Sub New(value As Boolean)
        Me.Value = value
    End Sub

    Public ReadOnly Property Value As Boolean Implements IAttribute(Of Boolean).Value

End Class

Public Class EnumAttributeHelper
    Public Shared Function GetAttributeValue(Of T, R)([enum] As IConvertible) As R
        Dim attributeValue As R = Nothing

        If [enum] IsNot Nothing Then
            Dim fi As FieldInfo = [enum].[GetType]().GetField([enum].ToString(CultureInfo.InvariantCulture))
            If fi IsNot Nothing Then
                Dim attributes As T() = TryCast(fi.GetCustomAttributes(GetType(T), False), T())

                If attributes IsNot Nothing AndAlso attributes.Length > 0 Then
                    Dim attribute As IAttribute(Of R) = TryCast(attributes(0), IAttribute(Of R))

                    If attribute IsNot Nothing Then
                        attributeValue = attribute.Value
                    End If
                End If
            End If
        End If

        Return attributeValue
    End Function
End Class
