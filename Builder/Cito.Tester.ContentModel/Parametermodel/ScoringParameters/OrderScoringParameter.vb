Public Class OrderScoringParameter : Inherits ChoiceScoringParameter

    Public Overrides ReadOnly Property GroupInitially As Boolean
        Get
            Return True
        End Get
    End Property

    Public Overrides ReadOnly Property MustRemainGrouped As Boolean
        Get
            Return True
        End Get
    End Property
End Class
