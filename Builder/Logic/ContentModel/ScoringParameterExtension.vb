Imports System.Runtime.CompilerServices
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ContentModel.Scoring

Namespace ContentModel
    Public Module ScoringParameterExtension
        <Extension>
        Public Function GetScoreManipulator(choice As ChoiceScoringParameter, solution As Solution) _
            As IChoiceScoringManipulator
            Return ScoringParameterFactory.GetKeyScoreBaseManipulator(Of IChoiceScoringManipulator)(choice, solution)
        End Function

        <Extension>
        Public Function GetScoreManipulator(matrix As MatrixScoringParameter, solution As Solution) _
            As IChoiceArrayScoringManipulator
            Return ScoringParameterFactory.GetKeyScoreBaseManipulator(Of IChoiceArrayScoringManipulator)(matrix, solution)
        End Function

        <Extension>
        Public Function GetScoreManipulator(stringGap As StringScoringParameter, solution As Solution) _
            As IGapScoringManipulator(Of String)
            Return _
                ScoringParameterFactory.GetKeyScoreBaseManipulator(Of IGapScoringManipulator(Of String))(stringGap,
                                                                                                          solution)
        End Function

        <Extension>
        Public Function GetScoreManipulator(dateGap As DateScoringParameter, solution As Solution) _
            As IGapScoringManipulator(Of String)
            Return _
                ScoringParameterFactory.GetKeyScoreBaseManipulator(Of IGapScoringManipulator(Of String))(dateGap,
                                                                                                          solution)
        End Function

        <Extension>
        Public Function GetScoreManipulator(timeGap As TimeScoringParameter, solution As Solution) _
            As IGapScoringManipulator(Of String)
            Return _
                ScoringParameterFactory.GetKeyScoreBaseManipulator(Of IGapScoringManipulator(Of String))(timeGap,
                                                                                                          solution)
        End Function

        <Extension>
        Public Function GetScoreManipulator(mathGap As MathScoringParameter, solution As Solution) _
            As IGapScoringManipulator(Of String)
            Return _
                ScoringParameterFactory.GetKeyScoreBaseManipulator(Of IGapScoringManipulator(Of String))(mathGap,
                                                                                                          solution)
        End Function

        <Extension>
        Public Function GetScoreManipulator(integerGap As IntegerScoringParameter, solution As Solution) _
            As IGapScoringManipulator(Of MultiType)
            Return _
                ScoringParameterFactory.GetKeyScoreBaseManipulator(Of IGapScoringManipulator(Of MultiType))(integerGap,
                                                                                                            solution)
        End Function

        <Extension>
        Public Function GetScoreManipulator(decimalGap As DecimalScoringParameter, solution As Solution) _
            As IGapScoringManipulator(Of MultiType)
            Return _
                ScoringParameterFactory.GetKeyScoreBaseManipulator(Of IGapScoringManipulator(Of MultiType))(decimalGap,
                                                                                                            solution)
        End Function

        <Extension>
        Public Function GetScoreManipulator(currencyGap As CurrencyScoringParameter, solution As Solution) _
            As IGapScoringManipulator(Of Decimal?)
            Return _
                ScoringParameterFactory.GetKeyScoreBaseManipulator(Of IGapScoringManipulator(Of Decimal?))(currencyGap,
                                                                                                            solution)
        End Function

        <Extension>
        Public Function GetScoreManipulator(gapMatchScoringParam As GapMatchScoringParameter, solution As Solution) _
            As IValidatingChoiceArrayScoringManipulator(Of String)
            Return _
                ScoringParameterFactory.GetKeyScoreBaseManipulator(Of IValidatingChoiceArrayScoringManipulator(Of String))(gapMatchScoringParam,
                                                                                                    solution)
        End Function

        <Extension>
        Public Function GetScoreManipulator(graphGapMatchScoringParam As GraphGapMatchScoringParameter, solution As Solution) _
            As IValidatingChoiceArrayScoringManipulator(Of String)
            Return _
                ScoringParameterFactory.GetKeyScoreBaseManipulator(Of IValidatingChoiceArrayScoringManipulator(Of String))(graphGapMatchScoringParam,
                                                                                                    solution)
        End Function

        <Extension>
        Public Function GetScoreManipulator(orderScoringParam As OrderScoringParameter, solution As Solution) _
            As IOrderScoringManipulator
            Return _
                ScoringParameterFactory.GetKeyScoreBaseManipulator(Of IOrderScoringManipulator)(orderScoringParam,
                                                                                                    solution)
        End Function

        <Extension>
        Public Function GetScoreManipulator(stringGap As GeogebraScoringParameter, solution As Solution) _
            As IGapScoringManipulator(Of String)
            Return _
                ScoringParameterFactory.GetKeyScoreBaseManipulator(Of IGapScoringManipulator(Of String))(stringGap,
                                                                                                          solution)
        End Function

        <Extension>
        Public Function GetScoreManipulator(scoringParam As ScoringParameter, solution As Solution) As IScoreManipulator
            Return ScoringParameterFactory.GetKeyScoreBaseManipulator(scoringParam, solution)
        End Function

        <Extension>
        Public Function GetScoreManipulator(booleanGap As BooleanScoringParameter, solution As Solution) As IGapScoringManipulator(Of Boolean)
            Return ScoringParameterFactory.GetKeyScoreBaseManipulator(Of IGapScoringManipulator(Of Boolean))(booleanGap, solution)
        End Function

        <Extension>
        Public Function GetConceptScoreManipulator(scoringParam As ScoringParameter, solution As Solution) _
            As IScoreManipulator
            Return ScoringParameterFactory.GetConceptScoreManipulator(scoringParam, solution)
        End Function

        <Extension>
        Public Function GetConceptManipulator(combinedScoringMapKey As CombinedScoringMapKey, solution As Solution) _
            As IConceptScoreManipulator
            Return ScoringParameterFactory.GetConceptManipulator(combinedScoringMapKey, solution)
        End Function



        <Extension>
        Public Function IdentifierPostFix(scoringParam As ScoringParameter) As String
            Return ScoringParameterLogic.getPostfix(scoringParam)
        End Function
    End Module
End Namespace

