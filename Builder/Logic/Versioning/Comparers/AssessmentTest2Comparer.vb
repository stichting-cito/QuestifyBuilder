Imports Cito.Tester.ContentModel
Imports Versioning

Public Class AssessmentTest2Comparer
    Inherits MetaDataComparerBase(Of AssessmentTest2)

    Public Sub New()
        MyBase.New()
    End Sub


    Public Overrides Function Compare(t1 As AssessmentTest2, t2 As AssessmentTest2) As IEnumerable(Of MetaDataCompareResult)

        Return _results
    End Function
End Class
