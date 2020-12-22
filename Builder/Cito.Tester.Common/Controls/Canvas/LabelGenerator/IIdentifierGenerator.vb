Namespace Controls.Canvas.LabelGenerator
    Public Interface IIdentifierGenerator(Of T)

        Function GetNewIdentifier() As T
        Function GetLastGeneratedIdentifier() As T
        Sub Reset()
    End Interface
End Namespace