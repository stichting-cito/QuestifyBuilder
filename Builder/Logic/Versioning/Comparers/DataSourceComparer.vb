Imports Cito.Tester.ContentModel.Datasources
Imports Versioning

Public Class DataSourceComparer
    Inherits MetaDataComparerBase(Of DataSourceSettings)

    Public Sub New()
        MyBase.New()
    End Sub


    Public Overrides Function Compare(t1 As DataSourceSettings, t2 As DataSourceSettings) As IEnumerable(Of MetaDataCompareResult)

        Return _results
    End Function
End Class
