Imports System.Reflection
Imports Cito.Tester.Common

Namespace ResponseAndScoringModel.Solution.ConcreteScoring

    Friend MustInherit Class baseScoring : Implements IScoringFindingStrategy

        Private ReadOnly _finding As KeyFinding

        Public Sub New(finding As KeyFinding)
            _finding = finding
        End Sub

        Public MustOverride ReadOnly Property Information As String Implements IScoringFindingStrategy.Information

        Public ReadOnly Property Finding As KeyFinding Implements IScoringFindingStrategy.Finding
            Get
                Return _finding
            End Get
        End Property

        Public MustOverride Function GetMaxScoreForFinding() As Integer Implements IScoringFindingStrategy.GetMaxScoreForFinding

        Public MustOverride Function ScoreFinding(responseFinding As ResponseFinding) As Integer Implements IScoringFindingStrategy.ScoreFinding

        Protected Function GetPreProcessedValue(tempValue As BaseValue, selectedPreprocessorCollection As SelectedPreprocessorCollection) As BaseValue
            Debug.Assert(selectedPreprocessorCollection IsNot Nothing)
            Dim temporaryValue = tempvalue

            For Each preProcessor As SelectedPreprocessor In selectedPreprocessorCollection
                Dim thePreProcessingRule As IResponseKeyValuePreprocessor = ResponseKeyValuePreProcessorFactory.Create(preProcessor.Rule)
                temporaryValue = thePreProcessingRule.PreprocessValue(temporaryValue)
            Next

            Return temporaryValue
        End Function

        Protected Function GetMaxFindingScore() As Integer
            If Finding.Facts.Count > 0 OrElse Finding.KeyFactsets.Count > 0 Then
                Select Case Finding.Method
                    Case EnumScoringMethod.Dichotomous
                        Return 1

                    Case EnumScoringMethod.Polytomous
                        Dim cumulativeScore As Integer = GetCumulativeScore()
                        Return cumulativeScore
                    Case Else
                        Return 0
                End Select
            Else
                Return 0
            End If
        End Function

        protected MustOverride Function GetCumulativeScore() As Integer

    End Class

End Namespace