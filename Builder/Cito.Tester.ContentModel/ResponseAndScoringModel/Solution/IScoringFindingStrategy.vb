Public Interface IScoringFindingStrategy

    ReadOnly Property Information As String

    ReadOnly Property Finding As KeyFinding

    Function GetMaxScoreForFinding() As Integer

    Function ScoreFinding(responseFinding As ResponseFinding) As Integer

End Interface
