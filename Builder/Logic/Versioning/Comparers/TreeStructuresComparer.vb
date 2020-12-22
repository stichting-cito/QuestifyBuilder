Imports Versioning

Imports System.Linq

Public Class TreeStructuresComparer
    Inherits MetaDataComparerBase(Of IEnumerable(Of TreeStructureMetaData))

    Public Sub New()
        MyBase.New()
    End Sub


    Public Overrides Function Compare(t1 As IEnumerable(Of TreeStructureMetaData), t2 As IEnumerable(Of TreeStructureMetaData)) As IEnumerable(Of MetaDataCompareResult)
        _results.AddRange(CompareVersions(t1, t2, CompareType.CompareOldToNew))
        _results.AddRange(CompareVersions(t2, t1, CompareType.CompareNewToOld))
        Return _results
    End Function

    Private Function CompareVersions(t1 As IEnumerable(Of TreeStructureMetaData), t2 As IEnumerable(Of TreeStructureMetaData), ByVal compareType As CompareType) As IEnumerable(Of MetaDataCompareResult)
        Dim result As New List(Of MetaDataCompareResult)

        For Each csmd1 As TreeStructureMetaData In t1
            If Not _results.Any(Function(r) r.PropertyName = csmd1.Name AndAlso r.Category = csmd1.Category) Then
                Dim concatenatedValues1 = csmd1.ConcatenatedValues

                Dim csmd2 As TreeStructureMetaData = t2.FirstOrDefault(Function(i) i.Id = csmd1.Id)
                If (csmd2 Is Nothing) Then
                    If compareType = CompareType.CompareNewToOld Then
                        result.Add(New MetaDataCompareResult(csmd1.Name, Nothing, String.Empty, concatenatedValues1, csmd1.Category, Nothing))
                    ElseIf compareType = CompareType.CompareOldToNew
                        result.Add(New MetaDataCompareResult(csmd1.Name, Nothing, concatenatedValues1, String.Empty, csmd1.Category, Nothing))
                    End If
                Else
                    Dim concatenatedValues2 = csmd2.ConcatenatedValues

                    If Not String.Equals(concatenatedValues1, concatenatedValues2, StringComparison.CurrentCultureIgnoreCase) Then
                        If compareType = CompareType.CompareNewToOld Then
                            result.Add(New MetaDataCompareResult(csmd1.Name, Nothing, concatenatedValues1, concatenatedValues2, csmd1.Category, Nothing))
                        Else
                            result.Add(New MetaDataCompareResult(csmd1.Name, Nothing, concatenatedValues2, concatenatedValues1, csmd1.Category, Nothing))
                        End If
                    End If
                End If
            End If
        Next

        Return result
    End Function
End Class
