Imports System.Linq
Imports Versioning

Public Class CustomPropertiesComparer
    Inherits MetaDataComparerBase(Of IEnumerable(Of CustomBankPropertyMetaData))

    Public Sub New()

    End Sub


    Public Overrides Function Compare(t1 As IEnumerable(Of CustomBankPropertyMetaData), t2 As IEnumerable(Of CustomBankPropertyMetaData)) As IEnumerable(Of MetaDataCompareResult)
        _results.AddRange(CompareVersions(t1, t2, CompareType.CompareOldToNew))
        _results.AddRange(CompareVersions(t2, t1, CompareType.CompareNewToOld))

        Return _results
    End Function


    Private Function CompareVersions(t1 As IEnumerable(Of CustomBankPropertyMetaData), t2 As IEnumerable(Of CustomBankPropertyMetaData), ByVal compareType As CompareType) As IEnumerable(Of MetaDataCompareResult)
        Dim result As New List(Of MetaDataCompareResult)()

        For Each customBankPropertyMetaData1 As CustomBankPropertyMetaData In t1
            Dim customBankPropertyMetaData2 As CustomBankPropertyMetaData = t2.FirstOrDefault(Function(i) i.Id = customBankPropertyMetaData1.Id)

            If customBankPropertyMetaData2 Is Nothing Then
                Dim concatenatedValues1 As String = String.Join(", ", customBankPropertyMetaData1.Values.ToArray())

                If _results.FirstOrDefault(Function(i) i.PropertyName = customBankPropertyMetaData1.Name AndAlso i.Category = customBankPropertyMetaData1.Category) Is Nothing Then
                    If compareType = compareType.CompareOldToNew Then
                        result.Add(New MetaDataCompareResult(customBankPropertyMetaData1.Name, Nothing, concatenatedValues1, String.Empty, customBankPropertyMetaData1.Category, Nothing))
                    Else
                        result.Add(New MetaDataCompareResult(customBankPropertyMetaData1.Name, Nothing, String.Empty, concatenatedValues1, customBankPropertyMetaData1.Category, Nothing))
                    End If
                End If
            Else
                customBankPropertyMetaData1.Values.Sort()
                customBankPropertyMetaData2.Values.Sort()

                If customBankPropertyMetaData1.Values IsNot Nothing AndAlso customBankPropertyMetaData1.Values.Count > 0 Then
                    Dim concatenatedValues1 As String = String.Join(", ", customBankPropertyMetaData1.Values.ToArray())
                    Dim concatenatedValues2 As String = String.Join(", ", customBankPropertyMetaData2.Values.ToArray())

                    If concatenatedValues1 <> concatenatedValues2 Then
                        If _results.FirstOrDefault(Function(i) i.PropertyName = customBankPropertyMetaData1.Name AndAlso i.Category = customBankPropertyMetaData1.Category) Is Nothing Then
                            result.Add(New MetaDataCompareResult(customBankPropertyMetaData1.Name, Nothing, concatenatedValues1, concatenatedValues2, customBankPropertyMetaData1.Category, Nothing))
                        End If
                    End If
                End If
            End If
        Next

        Return result

    End Function

End Class
