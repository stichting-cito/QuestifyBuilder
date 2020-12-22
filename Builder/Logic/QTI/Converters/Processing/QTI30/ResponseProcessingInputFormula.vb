Imports System.Globalization
Imports System.Linq
Imports System.Text.RegularExpressions
Imports System.Xml
Imports System.Xml.Linq
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.QTI.Helpers.QTI30
Imports Questify.Builder.Logic.QTI.Helpers.QTI_Base
Imports Questify.Builder.Logic.QTI.Interfaces

Namespace QTI.Converters.Processing.QTI30

    Public Class ResponseProcessingInputFormula
        Inherits ResponseProcessingPerTypeBase

        Protected ReadOnly FormulaItemType As QTI30CombinedScoringHelper.FormulaItemType
        Protected ReadOnly Owner As IResponseProcessingCustomOperators
        Protected ReadOnly MultiLineFormulaGap As Boolean = False
        Protected ReadOnly DependentVariables As List(Of Tuple(Of String, Integer, Boolean))

        Public Sub New(responseIndex As Integer, formulaItemType As QTI30CombinedScoringHelper.FormulaItemType, owner As IResponseProcessingCustomOperators, multiLineFormulaGap As Boolean, Optional responseSubIndex As Integer = 0)
            MyBase.New(responseIndex, responseSubIndex)
            Me.FormulaItemType = formulaItemType
            Me.Owner = owner
            Me.MultiLineFormulaGap = multiLineFormulaGap
        End Sub

        Public Sub New(responseIndex As Integer, formulaItemType As QTI30CombinedScoringHelper.FormulaItemType, owner As IResponseProcessingCustomOperators, dependentVariables As List(Of Tuple(Of String, Integer, Boolean)), Optional responseSubIndex As Integer = 0)
            MyBase.New(responseIndex, responseSubIndex)
            Me.FormulaItemType = formulaItemType
            Me.Owner = owner
            Me.DependentVariables = dependentVariables
        End Sub

        Protected Overrides Function GetProcessingForFact(fact As KeyFact, Optional addNotMemberOfElement As Boolean = False, Optional outcomeIdentifier As String = "") As XElement
            Dim keyValue As KeyValue = DirectCast(fact.Values.First(), KeyValue)
            Dim processing As XElement = Nothing

            If keyValue.Values.Count = 1 Then
                processing = ProcessAnd(fact, keyValue)
            ElseIf keyValue.Values.Count > 1 Then
                processing = ProcessOr(fact, keyValue)
            End If

            Return processing
        End Function



        Protected Function ProcessAnd(fact As KeyFact, keyValue As KeyValue) As XElement
            Dim processing As XElement

            Dim value = keyValue.Values.First()
            If IsEvaluate(value) Then
                Dim endm = ExtendedNamedDecimalMap.FromString(CType(value, StringComparisonValue).Value)
                Dim solutions = endm.NamedDecimalMap.GetSolutions().Length
                processing = <qti-and></qti-and>

                For i = 0 To solutions - 1
                    processing.Add(GetProcessingForValue(value, True, ResponseIndex, fact, i))
                Next
            Else
                processing = GetProcessingForValue(value, False, ResponseIndex, fact)
            End If
            Return processing
        End Function

        Protected Overridable Function ProcessOr(fact As KeyFact, keyValue As KeyValue) As XElement
            Dim processing As XElement

            processing = <qti-or></qti-or>
            For Each value In keyValue.Values
                If IsEvaluate(value) Then
                    ProcessEval(fact, processing, value)
                Else
                    Dim subProcessing = GetProcessingForValue(value, False, ResponseIndex, fact)
                    If subProcessing IsNot Nothing Then
                        processing.Add(subProcessing)
                    Else
                        Dim responseProcessing = New ResponseProcessingInput(ResponseIndex, QTI30CombinedScoringHelper.EnumGapType.Unknown, Owner, ResponseSubIndex)
                        subProcessing = responseProcessing.GetProcessingForKeyFactValue(keyValue, value)
                        processing.Add(subProcessing)
                    End If
                End If
            Next
            Return processing
        End Function

        Protected Sub ProcessEval(fact As KeyFact, processing As XElement, val As BaseValue)
            Dim subProcessing = <qti-and></qti-and>
            Dim endm = ExtendedNamedDecimalMap.FromString(CType(val, StringComparisonValue).Value)
            For i = 0 To endm.NamedDecimalMap.GetSolutions.Length - 1
                subProcessing.Add(GetProcessingForValue(val, True, ResponseIndex, fact, i))
            Next
            processing.Add(subProcessing)
        End Sub

        Protected Function IsEvaluate(value As BaseValue) As Boolean
            Return TypeOf value Is StringComparisonValue AndAlso
                CType(value, StringComparisonValue).GetComparisonType(CType(value, StringComparisonValue).TypeOfComparison) = TypedComparisonValue(Of String).ComparisonType.Evaluate
        End Function

        Protected Sub ConvertMathMlDecimalSeparators(ByRef mathMlValue As String)
            For Each m As Match In Regex.Matches(mathMlValue, "(?<waarde>\d{1,9},\d{1,9})")
                Dim newValue As Decimal = Decimal.Parse(m.Groups("waarde").Value, Globalization.NumberStyles.Any, CultureInfo.GetCultureInfo("nl-NL"))
                mathMlValue = mathMlValue.Replace(m.Groups("waarde").Value, newValue.ToString(CultureInfo.InvariantCulture))
            Next
        End Sub

        Protected Function GetProcessingForValue(value As BaseValue, isEvaluate As Boolean, responseIndex As Integer, fact As KeyFact, Optional factSubIndex As Integer = 0) As XElement
            Dim processing As XElement = Nothing

            Dim convertMathMlDecimalSeparator As Boolean = QTIScoringHelper.BaseValueContainsMathML(value)

            If TypeOf value Is StringValue OrElse isEvaluate Then
                Dim correctResponse As String = If(TypeOf value Is StringValue, CType(value, StringValue).Value, CType(value, StringComparisonValue).Value)
                If convertMathMlDecimalSeparator Then ConvertMathMlDecimalSeparators(correctResponse)
                If FormulaItemType = QTI30CombinedScoringHelper.FormulaItemType.EvaluateDependency Then
                    correctResponse = AddVariableEvaluationToMathMl(correctResponse, responseIndex, TypedComparisonValue(Of String).ComparisonType.None)
                    processing = Owner.GetMathMLCustomOperator_EvaluateDependency(responseIndex, correctResponse, DependentVariables, ResponseSubIndex)
                ElseIf FormulaItemType = QTI30CombinedScoringHelper.FormulaItemType.EvaluateSteps AndAlso fact IsNot Nothing Then
                    Dim sequence As String = String.Empty
                    If (fact.Id.ToLower().StartsWith("first")) Then
                        sequence = "first"
                    ElseIf (fact.Id.ToLower().StartsWith("last")) Then
                        sequence = "last"
                    End If
                    processing = Owner.GetMathMLCustomOperator_Equals(responseIndex, correctResponse, sequence, ResponseSubIndex)
                ElseIf FormulaItemType = QTI30CombinedScoringHelper.FormulaItemType.EvaluateSubstitute OrElse isEvaluate Then
                    Dim variableName As String = String.Empty
                    Dim variableValue As String = String.Empty
                    Dim expectedResult As String = String.Empty
                    Dim degree As String = String.Empty

                    If isEvaluate Then
                        Dim endm = ExtendedNamedDecimalMap.FromString(correctResponse)
                        correctResponse = endm.NamedDecimalMap.GetSolutions(factSubIndex)
                        If endm.Extensions.ContainsKey(CasEvaluateHelper.DEGREE_KEY) Then
                            degree = endm.Extensions(CasEvaluateHelper.DEGREE_KEY)
                        End If
                    End If

                    If (ResponseProcessingInputFormulaHelper.InputIsValid(correctResponse)) Then
                        Dim content As String = correctResponse.Substring(1, correctResponse.Length - 2)
                        Dim parts() As String = content.Split(New Char() {";"c})
                        variableName = ResponseProcessingInputFormulaHelper.GetVariableName(parts(0))
                        variableValue = ResponseProcessingInputFormulaHelper.GetVariableValue(parts(0))
                        expectedResult = ResponseProcessingInputFormulaHelper.GetExpectedResult(parts(1))
                    End If
                    processing = Owner.GetMathMLCustomOperator_Evaluate(responseIndex, variableName, variableValue, expectedResult, ResponseSubIndex, degree)
                ElseIf MultiLineFormulaGap Then
                    processing = Owner.GetMathMLCustomOperator_Equals(responseIndex, correctResponse, "last", ResponseSubIndex)
                Else
                    processing = Owner.GetMathMLCustomOperator_Equals(responseIndex, correctResponse, ResponseSubIndex)
                End If
            ElseIf TypeOf value Is StringComparisonValue Then
                Dim correctResponse = CType(value, StringComparisonValue).Value
                If convertMathMlDecimalSeparator Then ConvertMathMlDecimalSeparators(correctResponse)
                Dim comparisonValue As StringComparisonValue = DirectCast(value, StringComparisonValue)
                If comparisonValue.GetComparisonType(comparisonValue.TypeOfComparison) = TypedComparisonValue(Of String).ComparisonType.Equivalent Then
                    If MultiLineFormulaGap Then
                        processing = Owner.GetMathMLCustomOperator_Equivalent(responseIndex, correctResponse, "last", ResponseSubIndex)
                    Else
                        processing = Owner.GetMathMLCustomOperator_Equivalent(responseIndex, correctResponse, ResponseSubIndex)
                    End If
                ElseIf comparisonValue.GetComparisonType(comparisonValue.TypeOfComparison) = TypedComparisonValue(Of String).ComparisonType.NotEquals Then
                    correctResponse = AddVariableEvaluationToMathMl(correctResponse, responseIndex, TypedComparisonValue(Of String).ComparisonType.NotEquals)
                    processing = Owner.GetMathMLCustomOperator_EvaluateDependency(responseIndex, correctResponse, DependentVariables, ResponseSubIndex)
                ElseIf comparisonValue.GetComparisonType(comparisonValue.TypeOfComparison) = TypedComparisonValue(Of String).ComparisonType.Dependency Then
                    Dim dependentVariablesForValue = ResponseProcessingInputFormulaHelper.FindDependentVariablesForValue(correctResponse, DependentVariables)
                    processing = Owner.GetMathMLCustomOperator_EvaluateDependency(responseIndex, correctResponse, dependentVariablesForValue, ResponseSubIndex, (ResponseSubIndex > 0))
                ElseIf comparisonValue.GetComparisonType(comparisonValue.TypeOfComparison) = TypedComparisonValue(Of String).ComparisonType.EqualsStrict Then
                    If MultiLineFormulaGap Then
                        processing = Owner.GetMathMLCustomOperator_Equals(responseIndex, correctResponse, "last", ResponseSubIndex)
                    Else
                        processing = Owner.GetMathMLCustomOperator_Equals(responseIndex, correctResponse, ResponseSubIndex)
                    End If
                ElseIf comparisonValue.GetComparisonType(comparisonValue.TypeOfComparison) = TypedComparisonValue(Of String).ComparisonType.EqualsSoft Then
                    If MultiLineFormulaGap Then
                        processing = Owner.GetMathMLCustomOperator_EqualsSoft(responseIndex, correctResponse, "last", ResponseSubIndex)
                    Else
                        processing = Owner.GetMathMLCustomOperator_EqualsSoft(responseIndex, correctResponse, ResponseSubIndex)
                    End If

                    processing.Add(<qti-base-value base-type="boolean">true</qti-base-value>)
                ElseIf comparisonValue.GetComparisonType(comparisonValue.TypeOfComparison) = TypedComparisonValue(Of String).ComparisonType.EqualEquation Then
                    processing = Owner.GetMathMLCustomOperator_EqualEquation(responseIndex, correctResponse, ResponseSubIndex)
                End If
            End If

            Return processing
        End Function

        Private Function AddVariableEvaluationToMathMl(inputMathML As String, responseIndex As Integer, typeOfComparison As TypedComparisonValue(Of String).ComparisonType) As String
            Dim result As String = inputMathML
            If Not String.IsNullOrEmpty(inputMathML) Then
                Dim mathMlDoc As New XmlDocument()
                Dim namespaceMan1 As New XmlNamespaceManager(mathMlDoc.NameTable)
                namespaceMan1.AddNamespace("m", "http://www.w3.org/1998/Math/MathML")
                mathMlDoc.LoadXml(inputMathML)
                If mathMlDoc.SelectNodes(ResponseProcessingInputFormulaHelper.GetVariableEvaluationMathML(typeOfComparison), namespaceMan1).Count > 0 AndAlso mathMlDoc.SelectNodes(ResponseProcessingInputFormulaHelper.GetVariableEvaluationMathML(), namespaceMan1).Count > 0 AndAlso mathMlDoc.SelectSingleNode(ResponseProcessingInputFormulaHelper.GetVariableEvaluationMathML(), namespaceMan1).InnerText.Equals(GetVariableByResponseIndex(responseIndex)) Then
                    Return result
                Else
                    Dim newMthMlDoc As New XmlDocument
                    Dim namespaceMan2 As New XmlNamespaceManager(mathMlDoc.NameTable)
                    namespaceMan2.AddNamespace("m", "http://www.w3.org/1998/Math/MathML")
                    newMthMlDoc.LoadXml(
                        $"<math xmlns=""http://www.w3.org/1998/Math/MathML""><apply>{String.Format($"<{ResponseProcessingInputFormulaHelper.GetVariableEvaluationTagName(typeOfComparison)}/>",
                                          ResponseProcessingInputFormulaHelper.GetVariableEvaluationTagName(typeOfComparison))}<ci>{GetVariableByResponseIndex(responseIndex)}</ci></apply></math>")
                    For Each node As XmlNode In mathMlDoc.DocumentElement.ChildNodes
                        Dim importedNode As XmlNode = newMthMlDoc.ImportNode(node, True)
                        newMthMlDoc.SelectSingleNode("/m:math/m:apply", namespaceMan2).AppendChild(importedNode)
                    Next
                    Return newMthMlDoc.OuterXml
                End If
            End If
            Return result
        End Function

        Private Function GetVariableByResponseIndex(responseIndex As Integer) As String
            Debug.Assert(DependentVariables.Any(Function(kvp) kvp.Item2 = responseIndex) = True,
                         $"GetVariableByResponseIndex: could not find key in dictionary with the requested value '{responseIndex}'")
            Return DependentVariables.Where(Function(kvp) kvp.Item2 = responseIndex).First.Item1
        End Function

    End Class

End Namespace