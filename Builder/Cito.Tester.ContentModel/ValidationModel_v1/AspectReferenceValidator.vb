Public Class AspectReferenceValidator
    Inherits EntityValidationBase(Of AspectReference)



    Public Overrides ReadOnly Property FriendlyEntityName As String
        Get
            Return "Aspectreference properties"
        End Get
    End Property

    Protected Overloads Overrides Function ValidateEntityFieldValue(entity As AspectReference, fieldName As String, value As Object) As String
        Return String.Empty
    End Function
End Class
