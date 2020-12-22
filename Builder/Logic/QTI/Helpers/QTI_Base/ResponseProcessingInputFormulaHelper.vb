Imports System.Linq
Imports System.Text.RegularExpressions
Imports System.Xml
Imports Cito.Tester.ContentModel

Namespace QTI.Helpers.QTI_Base

    Public Class ResponseProcessingInputFormulaHelper

        Public Shared Function GetVariableEvaluationMathML(typeOfComparison As TypedComparisonValue(Of String).ComparisonType) As String
            Return $"/m:math/m:apply/m:{GetVariableEvaluationTagName(typeOfComparison)}"
        End Function

        Public Shared Function GetVariableEvaluationMathML() As String
            Return "/m:math/m:apply/m:ci"
        End Function

        Public Shared Function GetVariableEvaluationTagName(typeOfComparison As TypedComparisonValue(Of String).ComparisonType) As String
            If typeOfComparison = TypedComparisonValue(Of String).ComparisonType.None Then Return "eq"
            If typeOfComparison = TypedComparisonValue(Of String).ComparisonType.NotEquals Then Return "neq"
            Return String.Empty
        End Function

        Public Shared Function GetVariableName(variablePart As String) As String
            Dim keyValue() As String = variablePart.Split(New Char() {":"c})
            Dim variableName As String = keyValue(0)
            Return variableName
        End Function

        Public Shared Function GetVariableValue(variablePart As String) As String
            Dim keyValue() As String = variablePart.Split(New Char() {":"c})
            Dim variableValue As String = keyValue(1)
            Return variableValue
        End Function

        Public Shared Function GetExpectedResult(resultPart As String) As String
            Dim keyValue() As String = resultPart.Split(New Char() {":"c})
            Dim expectedResult As String = keyValue(1)
            Return expectedResult
        End Function

        Public Shared Function InputIsValid(input As String) As Boolean
            Const pattern As String = "^[{]{1}[a-z]{1}[:]{1}[-]?[\d]+[;]{1}[a-z]{1}[:]{1}[-]?[\d]+[}]{1}$"
            Return Regex.IsMatch(input, pattern)
        End Function

        Public Shared Function FindDependentVariablesForValue(inputMathML As String, knownDependentVariables As List(Of Tuple(Of String, Integer, Boolean))) As List(Of Tuple(Of String, Integer, Boolean))
            Dim result As New List(Of Tuple(Of String, Integer, Boolean))
            If Not String.IsNullOrEmpty(inputMathML) Then
                Dim mathMlDoc As New XmlDocument()
                Dim namespaceMan1 As New XmlNamespaceManager(mathMlDoc.NameTable)
                namespaceMan1.AddNamespace("m", "http://www.w3.org/1998/Math/MathML")
                mathMlDoc.LoadXml(inputMathML)
                For Each node As XmlNode In mathMlDoc.SelectNodes("//m:ci", namespaceMan1)
                    Dim variableName = node.InnerText
                    If variableName.Length = 1 AndAlso knownDependentVariables.Any(Function(dv) dv.Item1 = variableName) Then
                        Dim kvp = knownDependentVariables.First(Function(dv) dv.Item1 = variableName)

                        If Not result.Any(Function(r) r.Item1 = kvp.Item1) Then result.Add(New Tuple(Of String, Integer, Boolean)(kvp.Item1, kvp.Item2, kvp.Item3))
                    Else
                        Dim regexCoord As String = $"coord [0-9] - [xy] \([{variableName.ToUpper}]\)"
                        If knownDependentVariables.Any(Function(dv) Regex.IsMatch(dv.Item1, regexCoord)) Then
                            Dim kvp = knownDependentVariables.First(Function(dv) Regex.IsMatch(dv.Item1, regexCoord))
                            If Not result.Any(Function(r) r.Item1 = variableName) Then result.Add(New Tuple(Of String, Integer, Boolean)(variableName, kvp.Item2, kvp.Item3))
                        End If
                    End If
                Next
            End If

            Return result
        End Function

        Public Shared Function GetResponseSubIndexForCustomInteractionCasDependencyVariables(variableId As String) As Integer
            If String.IsNullOrEmpty(variableId) Then Return 0
            If Not variableId.Length = 1 Then Return 0
            If IsNumeric(variableId) Then Return 0
            Return (Asc(variableId.ToUpper) - 64) + 1
        End Function

    End Class
End NameSpace