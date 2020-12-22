Public NotInheritable Class ReportSettings

    Public Shared Property ExcelReport As String
        Get
            Return My.Settings.Reports_ExcelReport
        End Get
        Set
            My.Settings.Reports_ExcelReport = value
        End Set
    End Property

    Public Shared Property MediaReferencesReport As String
        Get
            Return My.Settings.Reports_MediaReferencesReport
        End Get
        Set
            My.Settings.Reports_MediaReferencesReport = value
        End Set
    End Property

    Public Shared Property MediaReferencedByEntities As String
        Get
            Return My.Settings.Reports_MediaReferencedByEntities
        End Get
        Set
            My.Settings.Reports_MediaReferencedByEntities = value
        End Set
    End Property

    Public Shared Property WordReport As String
        Get
            Return My.Settings.Reports_WordReport
        End Get
        Set
            My.Settings.Reports_WordReport = value
        End Set
    End Property
End Class
