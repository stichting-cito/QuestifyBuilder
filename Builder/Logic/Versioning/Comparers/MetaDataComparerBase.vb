Imports Versioning

Public MustInherit Class MetaDataComparerBase(Of T)

    Protected _results As New List(Of MetaDataCompareResult)()
    Protected Enum CompareType As Integer
        CompareOldToNew = 0
        CompareNewToOld = 1
    End Enum


    Public Sub New()

    End Sub


    Public MustOverride Function Compare(ByVal t1 As T, t2 As T) As IEnumerable(Of MetaDataCompareResult)

End Class
