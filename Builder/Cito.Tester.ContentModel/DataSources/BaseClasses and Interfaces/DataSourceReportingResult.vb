Namespace Datasources

    Public MustInherit Class DataSourceReportingResult

        Private _name As String

        Public ReadOnly Property Name As String
            Get
                Return _name
            End Get
        End Property

        Public Sub New(name As String)
            _name = name
        End Sub

    End Class

End Namespace
