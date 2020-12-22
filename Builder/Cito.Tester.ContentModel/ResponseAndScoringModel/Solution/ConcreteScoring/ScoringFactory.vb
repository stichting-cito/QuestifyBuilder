Namespace ResponseAndScoringModel.Solution.ConcreteScoring

    Public Class ScoringFactory

        Private Shared The_DEFAULT As String = Methods.ScoringFactSetsAndFacts

        Private Shared scoreMethod As String = The_DEFAULT

        Public Shared Function ConstructDefaultScoringMethod(keyFinding As KeyFinding) As IScoringFindingStrategy

            Dim method = scoreMethod
            Dim ret As IScoringFindingStrategy = Nothing

            While (ret Is Nothing)

                Select Case scoreMethod
                    Case Methods.ScoringFactSetsAndFacts
                        ret = New ScoringFactSetsAndFacts(keyFinding)
                    Case Methods.V23_Scoring
                        ret = New ScoringV23(keyFinding)
                    Case Else
                        Debug.Assert(False, "Not handled!")
                        method = The_DEFAULT
                End Select

            End While

            Debug.Assert(ret IsNot Nothing, "Should not occur!")
            Return ret
        End Function




        Public Shared Sub OverrideScoreMethod(method As String)
            scoreMethod = method
        End Sub

        Public Shared Sub ResetScoreMethodToDefault()
            scoreMethod = The_DEFAULT
        End Sub



        Public Class Methods

            Public Const ScoringFactSetsAndFacts As String = "ScoringFactSetsAndFacts"

            Public Const V23_Scoring As String = "ScoringV23"
        End Class


    End Class
End Namespace