
Public Class CasEqualStepsScoringParameter
    Inherits BooleanScoringParameter

    Public Overrides ReadOnly Property AlternativesCanBeAdded As Boolean
        Get
            Return False
        End Get
    End Property

    Public Overrides ReadOnly Property Groupable As Boolean
        Get
            Return False
        End Get
    End Property

End Class
