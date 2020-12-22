Imports System.Linq
Imports Versioning

Public Class PropertyEntityComparer
    Inherits MetaDataComparerBase(Of IEnumerable(Of PropertyEntityMetaData))

    Public Sub New()
        MyBase.New()
    End Sub


    Public Overrides Function Compare(t1 As IEnumerable(Of PropertyEntityMetaData), t2 As IEnumerable(Of PropertyEntityMetaData)) As IEnumerable(Of MetaDataCompareResult)
        _results.AddRange(CompareVersions(t1, t2))
        _results.AddRange(CompareVersions(t2, t1))

        Return _results
    End Function

    Private Function CompareVersions(t1 As IEnumerable(Of PropertyEntityMetaData), t2 As IEnumerable(Of PropertyEntityMetaData)) As IEnumerable(Of MetaDataCompareResult)
        Dim result As New List(Of MetaDataCompareResult)()

        For Each propertyEntityMetaData1 As PropertyEntityMetaData In t1
            If t2.FirstOrDefault(Function(i) i.Name = propertyEntityMetaData1.Name) Is Nothing Then
                Dim localizedPropertyName As String = My.Resources.ResourceManager.GetString(
                    $"Property_{propertyEntityMetaData1.Name}")

                result.Add(New MetaDataCompareResult(propertyEntityMetaData1.Name, localizedPropertyName, propertyEntityMetaData1.Name, String.Empty, propertyEntityMetaData1.Category, Nothing))
            End If
        Next

        For Each propertyEntityMetaData1 As PropertyEntityMetaData In t1
            Dim propertyEntityMetaData2 As PropertyEntityMetaData = t2.FirstOrDefault(Function(i) i.Name = propertyEntityMetaData1.Name)

            If propertyEntityMetaData2 IsNot Nothing Then
                If Not propertyEntityMetaData1.Value.Equals(propertyEntityMetaData2.Value) Then
                    If _results.FirstOrDefault(Function(i) i.PropertyName = propertyEntityMetaData1.Name AndAlso i.Category = propertyEntityMetaData1.Category) Is Nothing Then
                        Dim localizedPropertyName As String = My.Resources.ResourceManager.GetString(
                            $"Property_{propertyEntityMetaData1.Name}")

                        result.Add(New MetaDataCompareResult(propertyEntityMetaData1.Name, localizedPropertyName, propertyEntityMetaData1.Value.ToString(), propertyEntityMetaData2.Value.ToString(), propertyEntityMetaData1.Category, Nothing))
                    End If
                End If
            End If
        Next

        Return result
    End Function

End Class
