Imports System.Xml.Linq
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.QTI.Helpers.QTI_Base
Imports Questify.Builder.Logic.QTI.Interfaces

Namespace QTI.Converters.Processing.QTI30

    Public Class ResponseProcessingCustomOperators
        Implements IResponseProcessingCustomOperators

        Public Overridable Function GetPreProcessingCustomOperator(selectedPreprocessors As SelectedPreprocessorCollection, responseIndex As Integer, baseValue As String, Optional responseSubIndex As Integer = 0) As String Implements IResponseProcessingCustomOperators.GetPreProcessingCustomOperator
            Dim processing As XElement = <qti-string-match case-sensitive="true"></qti-string-match>
            processing.Add(GetProcessingForVariable(responseIndex, responseSubIndex))

            Dim correctValue As XElement = <qti-base-value base-type="string"><%= baseValue %></qti-base-value>
            processing.Add(correctValue)

            Return processing.ToString()
        End Function

        Public Function GetPreProcessingCustomOperator(selectedPreprocessors As SelectedPreprocessorCollection, responseIndex As Integer, baseValue As String, prefix As String, Optional responseSubIndex As Integer = 0) As String
            Dim result As String = String.Empty
            Dim subResult As String

            Dim toLower As Boolean = False
            Dim resultMatch As XElement = <qti-match>
											{0}
											{1}
									  </qti-match>
            Dim resultStringMatch As XElement = <qti-string-match case-sensitive="false">
												{0}
												{1}
											</qti-string-match>
            Dim customOperator As XElement = <qti-custom-operator definition="{0}">
								{1}
							</qti-custom-operator>
            Dim correctVariable As XElement = <qti-correct identifier="{0}"/>
            Dim baseValueVariable As XElement = <qti-base-value base-type="string">{0}</qti-base-value>

            For Each selectedPreprocessor As SelectedPreprocessor In selectedPreprocessors
                Dim ruleId As PreProcessingRuleId
                If [Enum].TryParse(selectedPreprocessor.Rule, ruleId) Then
                    If ruleId = PreProcessingRuleId.HLKL Then
                        toLower = True
                    ElseIf ruleId = PreProcessingRuleId.VSBE Then
                        If Not String.IsNullOrEmpty(result) Then
                            result = String.Format(customOperator.ToString, $"{prefix}:Trim", result)
                        Else
                            result = String.Format(customOperator.ToString, $"{prefix}:Trim", GetProcessingForVariable(responseIndex, responseSubIndex).ToString())
                        End If
                    ElseIf ruleId = PreProcessingRuleId.VDT Then
                        If Not String.IsNullOrEmpty(result) Then
                            result = String.Format(customOperator.ToString, $"{prefix}:ToAscii", result)
                        Else
                            result = String.Format(customOperator.ToString, $"{prefix}:ToAscii", GetProcessingForVariable(responseIndex, responseSubIndex).ToString())
                        End If
                    ElseIf ruleId = PreProcessingRuleId.VAS Then
                        Throw New ArgumentException("Preprocessing rule 'remove all spaces' is not supported.")
                    End If
                End If
            Next

            If Not String.IsNullOrEmpty(baseValue) Then
                subResult = String.Format(baseValueVariable.ToString, baseValue)
            Else
                subResult = String.Format(correctVariable.ToString, QTIScoringHelper.GetResponseId(responseIndex))
            End If

            If toLower Then
                If String.IsNullOrEmpty(result) Then
                    result = String.Format(resultStringMatch.ToString, GetProcessingForVariable(responseIndex, responseSubIndex).ToString(), subResult)
                Else
                    result = String.Format(resultStringMatch.ToString, result, subResult)
                End If
            Else
                If String.IsNullOrEmpty(result) Then
                    result = String.Format(resultMatch.ToString, GetProcessingForVariable(responseIndex, responseSubIndex).ToString(), subResult)
                Else
                    result = String.Format(resultMatch.ToString, result, subResult)
                End If
            End If

            Return result
        End Function

        Public Overridable Function GetDecimalCustomOperator(responseIndex As Integer, Optional responseSubIndex As Integer = 0) As XElement Implements IResponseProcessingCustomOperators.GetDecimalCustomOperator
            Return GetProcessingForVariable(responseIndex, responseSubIndex)
        End Function

        Public Overridable Function GetMathMLCustomOperator_Equals(responseIndex As Integer, correctResponse As String, Optional responseSubIndex As Integer = 0) As XElement Implements IResponseProcessingCustomOperators.GetMathMLCustomOperator_Equals
            Return <qti-variable identifier="{0}"/>
        End Function

        Public Overridable Function GetMathMLCustomOperator_Equals(responseIndex As Integer, correctResponse As String, sequence As String, Optional responseSubIndex As Integer = 0) As XElement Implements IResponseProcessingCustomOperators.GetMathMLCustomOperator_Equals
            Return GetMathMLCustomOperator_Equals(responseIndex, correctResponse, responseSubIndex)
        End Function

        Public Overridable Function GetMathMLCustomOperator_EqualsSoft(responseIndex As Integer, correctResponse As String, Optional responseSubIndex As Integer = 0) As XElement Implements IResponseProcessingCustomOperators.GetMathMLCustomOperator_EqualsSoft
            Return GetMathMLCustomOperator_Equals(responseIndex, correctResponse, responseSubIndex)
        End Function

        Public Overridable Function GetMathMLCustomOperator_EqualsSoft(responseIndex As Integer, correctResponse As String, sequence As String, Optional responseSubIndex As Integer = 0) As XElement Implements IResponseProcessingCustomOperators.GetMathMLCustomOperator_EqualsSoft
            Return GetMathMLCustomOperator_Equals(responseIndex, correctResponse, responseSubIndex)
        End Function

        Public Overridable Function GetMathMLCustomOperator_Evaluate(ByVal responseIndex As Integer, ByVal variableName As String, ByVal variableValue As String, ByVal expectedResult As String, Optional responseSubIndex As Integer = 0, Optional degree As String = "") As XElement Implements IResponseProcessingCustomOperators.GetMathMLCustomOperator_Evaluate
            Return <qti-variable identifier="{0}"/>
        End Function

        Public Overridable Function GetMathMLCustomOperator_Equivalent(responseIndex As Integer, correctResponse As String, Optional responseSubIndex As Integer = 0) As XElement Implements IResponseProcessingCustomOperators.GetMathMLCustomOperator_Equivalent
            Return <qti-variable identifier="{0}"/>
        End Function

        Public Overridable Function GetMathMLCustomOperator_Equivalent(responseIndex As Integer, correctResponse As String, sequence As String, Optional responseSubIndex As Integer = 0) As XElement Implements IResponseProcessingCustomOperators.GetMathMLCustomOperator_Equivalent
            Return GetMathMLCustomOperator_Equivalent(responseIndex, correctResponse, responseSubIndex)
        End Function

        Public Overridable Function GetMathMLCustomOperator_EvaluateDependency(responseIndex As Integer, correctResponse As String, variables As List(Of Tuple(Of String, Integer, Boolean)), Optional responseSubIndex As Integer = 0, Optional addNullCheck As Boolean = False) As XElement Implements IResponseProcessingCustomOperators.GetMathMLCustomOperator_EvaluateDependency
            Return <qti-variable identifier="{0}"/>
        End Function

        Public Overridable Function GetMathMLCustomOperator_EqualSteps(ByVal responseIndex As Integer, Optional responseSubIndex As Integer = 0) As XElement Implements IResponseProcessingCustomOperators.GetMathMLCustomOperator_EqualSteps
            Return <qti-variable identifier="{0}"/>
        End Function

        Public Overridable Function GetProcessingForVariable(responseIndex As Integer, Optional responseSubIndex As Integer = 0) As XElement Implements IResponseProcessingCustomOperators.GetProcessingForVariable
            Return GetProcessingForSpecifiedVariable(QTIScoringHelper.GetResponseId(responseIndex), responseSubIndex)
        End Function

        Public Overridable Function GetProcessingForSpecifiedVariable(variableIdentifier As String, Optional responseSubIndex As Integer = 0) As XElement Implements IResponseProcessingCustomOperators.GetProcessingForSpecifiedVariable
            Dim stringVariable = <qti-variable identifier=<%= variableIdentifier %>/>
            Dim subIndex = <qti-index n="{0}">{1}</qti-index>
            If responseSubIndex > 0 Then
                Return XElement.Parse(String.Format(subIndex.ToString, responseSubIndex, stringVariable))
            Else
                Return stringVariable
            End If
        End Function

        Public Overridable Function GetGeogebraCustomOperator(responseIndex As Integer, correctResponse As String, Optional responseSubIndex As Integer = 0) As XElement Implements IResponseProcessingCustomOperators.GetGeogebraCustomOperator
            Return <qti-variable identifier="{0}"/>
        End Function

        Public Overridable Function GetMathMLCustomOperator_EqualEquation(responseIndex As Integer, correctResponse As String, Optional responseSubIndex As Integer = 0) As XElement Implements IResponseProcessingCustomOperators.GetMathMLCustomOperator_EqualEquation
            Return <qti-variable identifier="{0}"/>
        End Function

    End Class
End Namespace