Imports Cito.Tester.ContentModel
Imports Cito.Tester.Common

Namespace CustomInteractions
    MustInherit Class BaseAdapter(Of TScoringTypeBase As ScoringTypeBase)
        Implements IScoreParameterAdapter
        Public Function Adapt(
                     parameterNr As Integer,
                     input As ScoringTypeBase,
                     findingOverride As String,
                     controllerId As String,
                     scoringLabel As String) As ScoringParameter Implements IScoreParameterAdapter.Adapt
            Debug.Assert(TypeOf input Is TScoringTypeBase)

            Dim param As ScoringParameter = MakeParameter(DirectCast(input, TScoringTypeBase))

            Debug.Assert(param.Value IsNot Nothing)
            Debug.Assert(param.Value.Count <> 0)

            SetDefaultProperties(param, parameterNr, input, findingOverride, controllerId, scoringLabel)

            Return param
        End Function

        Private Sub SetDefaultProperties(
                                        param As ScoringParameter,
                                        parameterNr As Integer,
                                        input As ScoringTypeBase,
                                        findingOverride As String,
                                        controllerIdParam As String,
                                        scoringLabel As String)
            Dim alphabeticIdentifier As String = If(scoringLabel IsNot Nothing AndAlso input.Label Is Nothing,
                                                    $"({ _
                                                       AlphabeticIdentifierHelper.GetAlphabeticIdentifier(
                                                           parameterNr + 1)})", AlphabeticIdentifierHelper.GetAlphabeticIdentifier(parameterNr + 1))
            Dim label As String = If(input.Label IsNot Nothing, $"{input.Label} ({alphabeticIdentifier})", alphabeticIdentifier)
            If scoringLabel IsNot Nothing AndAlso input.Label Is Nothing Then
                label = $"{scoringLabel} {label}"
            End If

            If scoringLabel IsNot Nothing AndAlso input.Label IsNot Nothing Then
                label = $"{scoringLabel} - {label}"
            End If

            param.BluePrint = New ParameterCollection()
            param.FindingOverride = If(String.IsNullOrEmpty(findingOverride), IdentifierHelper.CI_FindingName, findingOverride)
            param.ControllerId = ControllerId(parameterNr, controllerIdParam)
            param.Name = If(param.Name, ControllerId(parameterNr, controllerIdParam))
            param.Label = label
            If Not String.IsNullOrEmpty(controllerIdParam) Then
                param.InlineId = ControllerId(parameterNr, controllerIdParam)
            End If
        End Sub

        Private Function ControllerId(nr As Integer, controllerIdParam As String) As String
            If Not String.IsNullOrEmpty(controllerIdParam) Then
                Return $"CI_SP_{controllerIdParam}_{nr}"
            End If
            Return $"CI_SP{nr}"
        End Function

        Protected MustOverride Function MakeParameter(input As TScoringTypeBase) As ScoringParameter
    End Class
End Namespace
