
Imports System.Xml.Serialization

<Serializable> _
<XmlRoot("valueCollection")> _
Public Class ValueCollection
    Inherits List(Of BaseFactValue)


    Public Function GetValueByDomain(domain As String) As BaseFactValue()
        Return GetValueByDomain(domain, True)
    End Function

    Public Function GetValueByDomain(domain As String, updateOccur As Boolean) As BaseFactValue()
        Dim resultArray As New List(Of BaseFactValue)

        For Each factValue As BaseFactValue In Me
            If factValue.Domain.Equals(domain) AndAlso factValue.Occur > 0 Then
                If updateOccur Then factValue.Occur -= 1
                resultArray.Add(factValue)
                Trace.Write(factValue.Domain)
            End If
        Next
        Return resultArray.ToArray
    End Function


End Class
