Imports System.Xml.Serialization

<Serializable>
Public MustInherit Class ScoreTranslationTable(Of T As ScoreTranslationTableEntry)
    Inherits List(Of T)

    Private _keys As New Hashtable

    <XmlIgnore>
    Public ReadOnly Property MaxTranslatedScore As Double
        Get
            Dim max As Double = 0
            For Each entry As T In Me
                If entry.TranslatedScore > max Then
                    max = entry.TranslatedScore
                End If
            Next

            Return max
        End Get
    End Property

    Public Overloads Sub Add(entry As T)
        If entry Is Nothing Then
            Throw New ArgumentNullException("entry")
        End If

        Me._keys.Add(entry.RawScore, entry)
        MyBase.Add(entry)
    End Sub

    Public Overloads Sub Remove(entry As T)
        If entry Is Nothing Then
            Throw New ArgumentNullException("entry")
        End If

        Me._keys.Remove(entry.RawScore)
        MyBase.Remove(entry)
    End Sub

    Public Function TranslateScore(score As Integer) As Double
        Dim entry As T

        If _keys.Count = 0 Then
            Return score
        Else
            entry = Me.Item(score)

            If entry IsNot Nothing Then
                Return entry.TranslatedScore
            Else
                Throw New ArgumentOutOfRangeException($"Translation table is incomplete. (RawScore : {score})")
            End If
        End If

    End Function

End Class
