Namespace CustomClasses
    <DebuggerDisplay("name:{Name} FullName:{fullName} Date:{ModifiedDate} id:{resourceId}")>
    Public Class ModifiedItems
        Public Property Name As String
        Public Property ModifiedDate As DateTime
        Public Property fullName As String
        Public Property resourceId As Guid
    End Class
End Namespace