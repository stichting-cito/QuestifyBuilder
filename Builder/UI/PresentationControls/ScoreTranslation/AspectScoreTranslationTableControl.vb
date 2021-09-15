Imports Cito.Tester.ContentModel

Public Class AspectScoreTranslationTableControl

    Public Property DataSource() As AspectScoreTranslationTable
        Get
            Dim data As AspectScoreTranslationTable = Nothing
            If ScoreTranslationTableBindingSource.DataSource IsNot Nothing AndAlso TypeOf ScoreTranslationTableBindingSource.DataSource Is AspectScoreTranslationTable Then
                data = DirectCast(ScoreTranslationTableBindingSource.DataSource, AspectScoreTranslationTable)
            End If
            Return data
        End Get
        Set(ByVal value As AspectScoreTranslationTable)
            ScoreTranslationTableBindingSource.DataSource = value
        End Set
    End Property

    Public Overrides Sub Refresh()
        ScoreTranslationTableGridControl.Refetch()
        MyBase.Refresh()
    End Sub

End Class
