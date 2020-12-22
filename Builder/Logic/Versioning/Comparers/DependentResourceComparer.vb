Imports System.Linq
Imports Versioning

Public Class DependentResourceComparer
    Inherits MetaDataComparerBase(Of IEnumerable(Of DependentResourceMetaData))

    Public Sub New()

    End Sub


    Public Overrides Function Compare(t1 As IEnumerable(Of DependentResourceMetaData), t2 As IEnumerable(Of DependentResourceMetaData)) As IEnumerable(Of MetaDataCompareResult)
        _results.AddRange(CompareVersions(t1, t2, CompareType.CompareOldToNew))
        _results.AddRange(CompareVersions(t2, t1, CompareType.CompareNewToOld))

        Return _results
    End Function


    Private Function CompareVersions(t1 As IEnumerable(Of DependentResourceMetaData), t2 As IEnumerable(Of DependentResourceMetaData), ByVal compareType As CompareType) As IEnumerable(Of MetaDataCompareResult)
        Dim result As New List(Of MetaDataCompareResult)()

        For Each dependentResourceMetaData1 As DependentResourceMetaData In t1
            Dim dependentResourceMetaData2 As DependentResourceMetaData = t2.FirstOrDefault(Function(i) i.Id = dependentResourceMetaData1.Id)

            If dependentResourceMetaData2 Is Nothing Then
                If _results.FirstOrDefault(Function(i) i.PropertyName = dependentResourceMetaData1.Name AndAlso i.Category = dependentResourceMetaData1.Category) Is Nothing Then
                    If compareType = compareType.CompareOldToNew Then
                        result.Add(New MetaDataCompareResult(dependentResourceMetaData1.Name, Nothing, dependentResourceMetaData1.Name, String.Empty, dependentResourceMetaData1.Category, dependentResourceMetaData1.Version))
                    Else
                        result.Add(New MetaDataCompareResult(dependentResourceMetaData1.Name, Nothing, String.Empty, dependentResourceMetaData1.Name, dependentResourceMetaData1.Category, dependentResourceMetaData1.Version))
                    End If
                End If
            Else
                If dependentResourceMetaData1.Name <> dependentResourceMetaData2.Name Then
                    If compareType = compareType.CompareOldToNew Then
                        If _results.FirstOrDefault(Function(i) i.PropertyName = dependentResourceMetaData2.Name AndAlso i.Category = dependentResourceMetaData2.Category) Is Nothing Then
                            result.Add(New MetaDataCompareResult(dependentResourceMetaData2.Name, Nothing, dependentResourceMetaData1.Name, dependentResourceMetaData2.Name, dependentResourceMetaData1.Category, dependentResourceMetaData1.Version))
                        End If
                    Else
                        If _results.FirstOrDefault(Function(i) i.PropertyName = dependentResourceMetaData1.Name AndAlso i.Category = dependentResourceMetaData1.Category) Is Nothing Then
                            result.Add(New MetaDataCompareResult(dependentResourceMetaData1.Name, Nothing, dependentResourceMetaData2.Name, dependentResourceMetaData1.Name, dependentResourceMetaData2.Category, dependentResourceMetaData2.Version))
                        End If
                    End If
                End If
            End If
        Next

        Return result
    End Function

End Class
