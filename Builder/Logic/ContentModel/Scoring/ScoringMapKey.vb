Imports Cito.Tester.ContentModel

Namespace ContentModel.Scoring

    <DebuggerDisplayAttribute("[{Name}] {TypeOfScoringParameter} {ScoreKey}")>
    Public Class ScoringMapKey

        Private ReadOnly _scoringParameter As ScoringParameter
        Private ReadOnly _name As String
        Private ReadOnly _shouldSuffix As Boolean

        Public Sub New(scoringParameter As ScoringParameter, scoreKey As String)
            _scoringParameter = scoringParameter
            Me.ScoreKey = scoreKey

            Dim label As String = scoringParameter.GetLabelFor(scoreKey)

            _name = If(String.IsNullOrEmpty(label), scoringParameter.Name, label)
            _name = If(_name, String.Empty)

            _shouldSuffix = _scoringParameter.ShouldSuffix
        End Sub

        Public ReadOnly Property ScoringParameter As ScoringParameter
            Get
                Return _scoringParameter
            End Get
        End Property

        Public ReadOnly Property ScoreKey As String

        Public ReadOnly Property Name As String
            Get
                If (_shouldSuffix) Then
                    Return $"{_name}{_name.Length:\.;.;#}{ScoreKey}"
                Else
                    Return _name
                End If
            End Get
        End Property

        Public ReadOnly Property ScoringParameterName As String
            Get
                Return _name
            End Get
        End Property

        Public ReadOnly Property TypeOfScoringParameter As String
            Get
                Return _scoringParameter.GetType().Name
            End Get
        End Property

        Public Overrides Function Equals(obj As Object) As Boolean

            Dim other = TryCast(obj, ScoringMapKey)
            If (other IsNot Nothing) Then
                Return Object.ReferenceEquals(ScoringParameter, other.ScoringParameter) AndAlso (ScoreKey = other.ScoreKey)
            Else
                Return MyBase.Equals(obj)
            End If

        End Function

        Public Overrides Function GetHashCode() As Integer
            Return ScoringParameter.GetHashCode() Xor ScoreKey.GetHashCode()
        End Function

    End Class

End Namespace
