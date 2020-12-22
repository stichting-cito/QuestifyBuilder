Namespace ContentModel

    Class FactIdEqualityComparer
        Implements IEqualityComparer(Of String)

        Function _Equals(x As String, y As String) As Boolean Implements IEqualityComparer(Of String).Equals
            Return DefaultStringOperations.FactIdEquals(x, y)
        End Function

        Function _GetHashCode(obj As String) As Integer Implements IEqualityComparer(Of String).GetHashCode
            Return StringRegexOp.GetCapturedString(obj, DefaultStringOperations.FactIdMatch).GetHashCode()
        End Function

    End Class
End Namespace