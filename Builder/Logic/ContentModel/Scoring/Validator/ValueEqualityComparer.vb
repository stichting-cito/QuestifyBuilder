Public Class ValueEqualityComparer
    Inherits EqualityComparer(Of Value)

    Public Overloads Overrides Function Equals(x As Value, y As Value) As Boolean
        Return x.Domain = y.Domain
    End Function

    Public Overloads Overrides Function GetHashCode(obj As Value) As Integer
        Return obj.Domain.GetHashCode()
    End Function
End Class