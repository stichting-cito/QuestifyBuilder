Namespace Versioning
    Public Class MetaDataCompareResult

        Private ReadOnly _id As Guid
        Private ReadOnly _localizedPropertyName As String
        Private ReadOnly _propertyName As String
        Private ReadOnly _oldValue As String
        Private ReadOnly _newValue As String
        Private ReadOnly _category As String
        Private ReadOnly _version As String

        Public Sub New(ByVal propertyName As String, ByVal localizedPropertyName As String, ByVal oldValue As String, ByVal newValue As String, ByVal category As String, ByVal version As String)
            _id = Guid.NewGuid()
            _propertyName = propertyName
            _localizedPropertyName = localizedPropertyName
            _oldValue = oldValue
            _newValue = newValue
            _category = category
            _version = version
        End Sub

        Public ReadOnly Property Id As Guid
            Get
                Return _id
            End Get
        End Property

        Public ReadOnly Property LocalizedPropertyName As String
            Get
                If String.IsNullOrEmpty(_localizedPropertyName) Then
                    Return PropertyName
                Else
                    If String.IsNullOrEmpty(_version) Then
                        Return _localizedPropertyName
                    Else
                        Return $"{_localizedPropertyName} ({_version})"
                    End If
                End If
            End Get
        End Property

        Public ReadOnly Property PropertyName As String
            Get
                If String.IsNullOrEmpty(_version) Then
                    Return _propertyName
                Else
                    Return $"{_propertyName} ({_version})"
                End If
            End Get
        End Property

        Public ReadOnly Property OldValue As String
            Get
                Return _oldValue
            End Get
        End Property

        Public ReadOnly Property NewValue As String
            Get
                Return _newValue
            End Get
        End Property

        Public ReadOnly Property Category As String
            Get
                Return _category
            End Get
        End Property

    End Class
End Namespace