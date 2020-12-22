Imports System.Linq
Imports System.Xml.Serialization

<Serializable> _
<XmlRoot("concepts")> _
Public Class ConceptCollection
    Inherits List(Of Concept)


    Public Sub New()
    End Sub



    Public Overloads Property Item(code As String) As Concept
        Get
            Return GetConceptByCode(code)
        End Get
        Set
            Me.Remove(value)
            Me.Add(value)
        End Set
    End Property

    Public Overloads Sub Add(code As String, value As Integer)
        If Me.Item(code) Is Nothing Then
            Me.Add(New Concept(code, value))
        Else
            Throw New Exception("concept already in collection.")
        End If
    End Sub


    Public Overloads Function Remove(code As String) As Boolean
        Dim concept = GetConceptByCode(code)
        If concept IsNot Nothing Then
            Return Me.Remove(concept)
        Else
            Return False
        End If
    End Function


    Private Function GetConceptByCode(code As String) As Concept
        Dim concept As Concept = Nothing
        Dim clist = From c In Me
                    Where c.Code = code
        If clist IsNot Nothing AndAlso clist.Count = 1 Then
            concept = clist(0)
        End If
        Return concept
    End Function


End Class
