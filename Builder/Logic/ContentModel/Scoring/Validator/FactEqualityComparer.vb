Public Class FactEqualityComparer
    Inherits EqualityComparer(Of Fact)

    Public Overloads Overrides Function Equals(x As Fact, y As Fact) As Boolean
        Return x.Values.SetEquals(y.Values)
    End Function

    Public Overloads Overrides Function GetHashCode(obj As fact) As Integer
        Dim hashcode As Integer = 0
        For Each value As Value In obj.Values
            hashcode = hashcode Xor value.Domain.GetHashCode()
        Next
        Return hashcode
    End Function
End Class