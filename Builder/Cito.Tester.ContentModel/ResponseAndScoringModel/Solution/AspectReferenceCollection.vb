Imports System.Diagnostics.CodeAnalysis
Imports System.Xml.Serialization

<Serializable> _
Public Class AspectReferenceCollection


    Private _id As String
    Private _items As New List(Of AspectReference)



    Public Sub New()
    End Sub

    Public Sub New(id As String)
        Me.New()
        Me.Id = id
    End Sub



    <XmlAttribute("id")> _
    Public Property Id As String
        Get
            Return _id
        End Get
        Set
            _id = value
        End Set
    End Property

    <SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")> _
    <XmlElement("aspectReference", GetType(AspectReference))> _
    Public ReadOnly Property Items As List(Of AspectReference)
        Get
            Return _items
        End Get
    End Property



    Public Overloads Function Find(aspectName As String) As AspectReference
        For Each reference As AspectReference In Me.Items
            If reference.SourceName.Equals(aspectName, StringComparison.InvariantCultureIgnoreCase) Then
                Return reference
            End If
        Next

        Return Nothing
    End Function

    Public Function GetMaxScore() As Integer
        Dim maxScore As Integer = 0
        For Each aspect As AspectReference In Me.Items
            maxScore += aspect.MaxScore
        Next
        Return maxScore
    End Function


End Class