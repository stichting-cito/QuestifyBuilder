<Serializable>
Public Class ValidationValueCollection
    Inherits List(Of ValidationValue)

    Public Function ContainsValidationValue(fieldName As String, validatingEntity As ValidatingEntityBase) As Boolean

        For Each validation As ValidationValue In Me
            If validation.FieldName = fieldName AndAlso validation.ValidatingEntity.ValidationEntityIdentifier = validatingEntity.ValidationEntityIdentifier Then
                Return True
            End If
        Next

        Return False
    End Function


    Public Function GetValidationValue(fieldName As String, validatingEntity As ValidatingEntityBase) As ValidationValue

        For Each validation As ValidationValue In Me
            If validation.FieldName = fieldName AndAlso validation.ValidatingEntity.Equals(validatingEntity) Then
                Return validation
            End If
        Next

        Return Nothing
    End Function

    Public Sub RemoveValidationValue(fieldName As String, validatingEntity As ValidatingEntityBase)
        Dim foundValidationValue As ValidationValue = Nothing

        For Each validation As ValidationValue In Me
            If validation.FieldName = fieldName AndAlso validation.ValidatingEntity.Equals(validatingEntity) Then
                foundValidationValue = validation
                If foundValidationValue IsNot Nothing Then
                    Me.Remove(foundValidationValue)
                End If
                Return
            End If
        Next

    End Sub

    Public Sub AddValidationValue(fieldName As String, friendlyEntityName As String, validatingEntity As ValidatingEntityBase, message As String)
        Me.Add(New ValidationValue(fieldName, message, friendlyEntityName, validatingEntity))
    End Sub
End Class
