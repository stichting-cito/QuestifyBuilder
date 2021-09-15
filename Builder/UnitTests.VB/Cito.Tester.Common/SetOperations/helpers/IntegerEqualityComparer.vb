
''' <summary>
''' Simple Implementation of IEqualityComparer
''' </summary>
Public Class IntegerEqualityComparer
    Implements IEqualityComparer(Of Integer)

    Public Function Equals1(x As Integer, y As Integer) As Boolean Implements IEqualityComparer(Of Integer).Equals
        Return x.Equals(y)
    End Function

    Public Function GetHashCode1(obj As Integer) As Integer Implements IEqualityComparer(Of Integer).GetHashCode
        Return obj.GetHashCode
    End Function

End Class
