<Serializable> _
Public Class ValidationValue
    Private _fieldName As String
    Private _message As String
    Private _friendlyEntityName As String
    Private _validatingEntity As ValidatingEntityBase

    Public ReadOnly Property FieldName As String
        Get
            Return _fieldName
        End Get
    End Property

    Public ReadOnly Property Message As String
        Get
            Return _message
        End Get
    End Property

    Public ReadOnly Property FriendlyEntityName As String
        Get
            Return _friendlyEntityName
        End Get
    End Property

    Public ReadOnly Property ValidatingEntity As ValidatingEntityBase
        Get
            Return _validatingEntity
        End Get
    End Property

    Public Sub New(field As String, message As String, friendlyEntityName As String, validatingEntity As ValidatingEntityBase)
        _fieldName = field
        _message = message
        _friendlyEntityName = friendlyEntityName
        _validatingEntity = validatingEntity
    End Sub

End Class