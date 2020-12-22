Imports Cito.Tester.ContentModel
Imports Versioning

Public Class TestPackageComparer
    Inherits MetaDataComparerBase(Of TestPackage)

    Public Sub New()
        MyBase.New()
    End Sub


    Public Overrides Function Compare(t1 As TestPackage, t2 As TestPackage) As IEnumerable(Of MetaDataCompareResult)

        Return _results
    End Function
End Class
