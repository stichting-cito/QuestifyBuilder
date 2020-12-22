Imports Versioning

Imports System.Linq

Public Class ConceptStructuresComparer
    Inherits MetaDataComparerBase(Of IEnumerable(Of ConceptStructureMetaData))

    Public Sub New()
        MyBase.New()
    End Sub


    Public Overrides Function Compare(t1 As IEnumerable(Of ConceptStructureMetaData), t2 As IEnumerable(Of ConceptStructureMetaData)) As IEnumerable(Of MetaDataCompareResult)
        _results.AddRange(CompareVersions(t1, t2, CompareType.CompareOldToNew))
        _results.AddRange(CompareVersions(t2, t1, CompareType.CompareNewToOld))
        Return _results
    End Function


    Private Function CompareVersions(t1 As IEnumerable(Of ConceptStructureMetaData), t2 As IEnumerable(Of ConceptStructureMetaData), ByVal compareType As CompareType) As IEnumerable(Of MetaDataCompareResult)
        Dim result As New List(Of MetaDataCompareResult)

        For Each csmd1 As ConceptStructureMetaData In t1
            If Not _results.Any(Function(r) r.PropertyName = csmd1.Name AndAlso r.Category = csmd1.Category) Then
                Dim code1 = csmd1(0)
                Dim name1 = csmd1(code1)

                Dim csmd2 As ConceptStructureMetaData = t2.FirstOrDefault(Function(i) i.Id = csmd1.Id)
                If (csmd2 Is Nothing) Then
                    If compareType = CompareType.CompareNewToOld Then
                        result.Add(New MetaDataCompareResult(csmd1.Name, Nothing, String.Empty, name1, csmd1.Category, Nothing))
                    ElseIf compareType = CompareType.CompareOldToNew
                        result.Add(New MetaDataCompareResult(csmd1.Name, Nothing, name1, String.Empty, csmd1.Category, Nothing))
                    End If
                Else
                    Dim code2 = csmd2(0)
                    Dim name2 = csmd2(code2)

                    If Not String.Equals(name1, name2, StringComparison.CurrentCultureIgnoreCase) Then
                        If compareType = CompareType.CompareNewToOld Then
                            result.Add(New MetaDataCompareResult(csmd1.Name, Nothing, name1, name2, csmd1.Category, Nothing))
                        Else
                            result.Add(New MetaDataCompareResult(csmd1.Name, Nothing, name2, name1, csmd1.Category, Nothing))
                        End If
                    End If
                End If
            End If
        Next

        Return result
    End Function
End Class
