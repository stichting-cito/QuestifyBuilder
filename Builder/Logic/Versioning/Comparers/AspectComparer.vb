Imports Cito.Tester.ContentModel
Imports Versioning

Public Class AspectComparer
    Inherits MetaDataComparerBase(Of Aspect)

    Public Sub New()
        MyBase.New()
    End Sub


    Public Overrides Function Compare(t1 As Aspect, t2 As Aspect) As IEnumerable(Of MetaDataCompareResult)

        Return _results
    End Function
End Class
