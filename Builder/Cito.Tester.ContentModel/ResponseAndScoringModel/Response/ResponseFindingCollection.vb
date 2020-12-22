
Imports System.Xml.Serialization

<Serializable> _
<XmlRoot("responseFindingCollection")> _
Public Class ResponseFindingCollection
    Inherits List(Of ResponseFinding)


    Private _keys As Dictionary(Of String, ResponseFinding)



    Public Shadows Sub Add(finding As ResponseFinding)
        MyBase.Add(finding)
        _keys.Add(finding.Id, finding)
    End Sub

    Public Overloads Property Item(id As String) As ResponseFinding
        Get
            Return _keys(id)
        End Get
        Set
            Me.Remove(value)
            Me.Add(value)
        End Set
    End Property

    Public Overloads Function Contains(id As String) As Boolean
        Return _keys.ContainsKey(id)
    End Function


    Public Shadows Sub Remove(finding As ResponseFinding)
        _keys.Remove(finding.Id)
        MyBase.Remove(finding)
    End Sub

    Public Shadows Sub Remove(id As String)
        Dim finding As ResponseFinding
        finding = _keys(id)

        If finding IsNot Nothing Then
            _keys.Remove(finding.Id)
            MyBase.Remove(finding)
        End If
    End Sub



    Public Sub New()
        MyBase.New()
        _keys = New Dictionary(Of String, ResponseFinding)
    End Sub


End Class
