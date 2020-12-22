Imports Versioning

Public Class SourceComparer
    Inherits MetaDataComparerBase(Of String)

    Public Sub New()
        MyBase.New()
    End Sub


    Public Overrides Function Compare(t1 As String, t2 As String) As IEnumerable(Of MetaDataCompareResult)

        Return _results
    End Function
End Class
