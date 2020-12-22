Imports System.Globalization
Imports System.Linq
Imports System.Xml
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.QTI.Helpers.QTI_Base
Imports Questify.Builder.Logic.QTI.Helpers.QTI22
Imports Questify.Builder.Logic.QTI.Xsd.QTI22_Final

Namespace QTI.Converters.Declaration.QTI22

    Public Class ResponseDeclarationInput
        Inherits ResponseDeclarationPerTypeBase

        Private ReadOnly _gapType As CombinedScoringHelper.EnumGapType


        Public Sub New(gapType As CombinedScoringHelper.EnumGapType)
            _gapType = gapType
        End Sub


        Public Overloads Function GetCorrectResponseValuesForFact(fact As KeyFact, Optional valuePartIndex As Integer = 0, Optional completeBaseValue As Boolean = False) As List(Of ValueType)
            If fact Is Nothing OrElse fact.Values Is Nothing Then Return Nothing
            Return GetCorrectResponses(fact, valuePartIndex, completeBaseValue)
        End Function

        Public Overrides Function GetSingleCorrectResponseValueForFact(fact As KeyFact, valuePartIndex As Integer) As ValueType
            If fact IsNot Nothing AndAlso fact.Values IsNot Nothing AndAlso DirectCast(fact.Values(0), KeyValue).Values IsNot Nothing Then
                Return GetCorrectResponse(DirectCast(fact.Values(0), KeyValue).Values(0), valuePartIndex)
            End If
            Return Nothing
        End Function

        Public Overrides Function GetResponseDefaultValue(fact As KeyFact, responseIdentifierAttribute As XmlNode) As ValueType
            If fact IsNot Nothing AndAlso fact.Values IsNot Nothing AndAlso DirectCast(fact.Values(0), KeyValue).Values IsNot Nothing Then
                Return GetDefaultValue(responseIdentifierAttribute)
            End If
            Return Nothing
        End Function

        Public Overrides Function GetInterpretationValueForFact(fact As KeyFact) As String
            Dim result As String = String.Empty
            If (IsEvaluateComparisonType(fact)) Then
                result = GetEvaluateInterpretationValue(fact)
            Else
                Dim values As List(Of ValueType) = GetCorrectResponseValuesForFact(fact, , True)
                If values Is Nothing Then Return result
                result = String.Join("#", ResponseDeclarationHelper.GetStringInterpretationOfValueTypes(values))
            End If
            Return result
        End Function

        Public Function IsEvaluateComparisonType(fact As KeyFact) As Boolean
            Return fact.Values.OfType(Of KeyValue)().All(Function(value As KeyValue) value.Values.All(Function(baseValue As BaseValue) IsEvaluateComparisonType(baseValue)))
        End Function

        Private Function IsEvaluateComparisonType(baseValue As BaseValue) As Boolean
            If TypeOf baseValue Is StringComparisonValue Then
                Dim typedValue = CType(baseValue, StringComparisonValue)
                Return typedValue.GetComparisonType(typedValue.TypeOfComparison) = TypedComparisonValue(Of String).ComparisonType.Evaluate
            End If

            Return False
        End Function

        Public Overrides Function GetCorrectResponses(fact As KeyFact) As List(Of ValueType)
            Return MyBase.GetCorrectResponses(fact)
        End Function

        Public Overloads Function GetCorrectResponses(fact As KeyFact, Optional valuePartIndex As Integer = 0, Optional completeBaseValue As Boolean = False) As List(Of ValueType)
            Dim correctResponses As New List(Of ValueType)
            For Each value As KeyValue In fact.Values
                For Each baseValue As BaseValue In value.Values
                    If IsEvaluateComparisonType(baseValue) Then
                        correctResponses.Add(New ValueType)
                    Else
                        Dim correctResponse As ValueType = GetCorrectResponse(baseValue, valuePartIndex, completeBaseValue)
                        If correctResponse IsNot Nothing Then correctResponses.Add(correctResponse)
                    End If
                Next
            Next
            Return correctResponses
        End Function

        Private Function GetEvaluateInterpretationValue(fact As KeyFact) As String
            Dim list As New List(Of String)

            For Each value As KeyValue In fact.Values
                For Each baseValue As BaseValue In value.Values
                    If Not IsEvaluateComparisonType(baseValue) Then Continue For
                    Dim typedValue = CType(baseValue, StringComparisonValue)

                    Dim endm = ExtendedNamedDecimalMap.FromString(typedValue.Value)
                    If endm.NamedDecimalMap.IsValid Then
                        Dim solutions = endm.NamedDecimalMap.GetSolutions()
                        Dim subResult As String = String.Join("&", solutions)
                        If endm.Extensions IsNot Nothing AndAlso endm.Extensions.ContainsKey(CasEvaluateHelper.DEGREE_KEY) Then
                            subResult = String.Concat(subResult, $"[{CasEvaluateHelper.DEGREE_KEY}:{endm.Extensions(CasEvaluateHelper.DEGREE_KEY)}]")
                        End If
                        list.Add(subResult)
                    End If
                Next
            Next

            Return String.Join("#", list)
        End Function


        Private Function GetCorrectResponse(baseValue As BaseValue, Optional valuePartIndex As Integer = 0, Optional completeBaseValue As Boolean = False) As ValueType
            Dim correctResponseValue As ValueType = Nothing
            Dim value As String = baseValue.ToString
            If TypeOf baseValue Is DecimalValue Then
                value = CType(baseValue, DecimalValue).Value.ToString(CultureInfo.InvariantCulture.NumberFormat)
            ElseIf TypeOf baseValue Is IntegerValue Then
                value = CType(baseValue, IntegerValue).Value.ToString(CultureInfo.InvariantCulture.NumberFormat)
            End If
            If Not completeBaseValue Then
                If _gapType = CombinedScoringHelper.EnumGapType.TimeGap Then
                    value = GapTimeScoringHelper.GetPartOfTimeValue(baseValue, valuePartIndex)
                ElseIf _gapType = CombinedScoringHelper.EnumGapType.DateGap Then
                    value = GapDateScoringHelper.GetPartOfDateValue(baseValue, valuePartIndex)
                ElseIf TypeOf baseValue Is DecimalRangeValue Then
                    value = CType(baseValue, DecimalRangeValue).RangeStart.ToString(CultureInfo.InvariantCulture.NumberFormat)
                ElseIf TypeOf baseValue Is IntegerRangeValue Then
                    value = CType(baseValue, IntegerRangeValue).RangeStart.ToString(CultureInfo.InvariantCulture.NumberFormat)
                ElseIf TypeOf baseValue Is DecimalComparisonValue Then
                    Dim typedValue As DecimalComparisonValue = CType(baseValue, DecimalComparisonValue)
                    If typedValue.GetComparisonType(typedValue.TypeOfComparison) = TypedComparisonValue(Of Integer).ComparisonType.SmallerThan Then
                        value = (typedValue.Value - 0.1).ToString(CultureInfo.InvariantCulture.NumberFormat)
                    ElseIf typedValue.GetComparisonType(typedValue.TypeOfComparison) = TypedComparisonValue(Of Integer).ComparisonType.GreaterThan Then
                        value = (typedValue.Value + 0.1).ToString(CultureInfo.InvariantCulture.NumberFormat)
                    End If
                ElseIf TypeOf baseValue Is IntegerComparisonValue Then
                    Dim typedValue As IntegerComparisonValue = CType(baseValue, IntegerComparisonValue)
                    If typedValue.GetComparisonType(typedValue.TypeOfComparison) = TypedComparisonValue(Of Integer).ComparisonType.SmallerThan Then
                        value = (typedValue.Value - 1).ToString(CultureInfo.InvariantCulture.NumberFormat)
                    ElseIf typedValue.GetComparisonType(typedValue.TypeOfComparison) = TypedComparisonValue(Of Integer).ComparisonType.GreaterThan Then
                        value = (typedValue.Value + 1).ToString(CultureInfo.InvariantCulture.NumberFormat)
                    End If
                ElseIf IsEvaluateComparisonType(baseValue) Then
                    Dim typedValue As StringComparisonValue = CType(baseValue, StringComparisonValue)
                    If typedValue.GetComparisonType(typedValue.TypeOfComparison) = TypedComparisonValue(Of String).ComparisonType.Evaluate Then
                        Dim endm = ExtendedNamedDecimalMap.FromString(typedValue.Value)
                        value = endm.NamedDecimalMap.GetSolutions()(valuePartIndex)
                    End If
                End If
            Else
                If TypeOf baseValue Is StringValue OrElse TypeOf baseValue Is StringComparisonValue Then
                    value = baseValue.ToString()
                End If
            End If

            If Not String.IsNullOrEmpty(value) Then correctResponseValue = New ValueType With {.Value = value}
            Return correctResponseValue
        End Function

        Private Function GetDefaultValue(responseIdentifierAttribute As XmlNode) As ValueType
            Dim defaultResponseValue As ValueType = Nothing
            Dim defaultValue = String.Empty
            If (Not DirectCast(responseIdentifierAttribute, XmlAttribute).OwnerElement.Attributes Is Nothing AndAlso Not DirectCast(responseIdentifierAttribute, XmlAttribute).OwnerElement.Attributes("hottextId") Is Nothing) Then
                defaultValue = HottextScoringHelper.GetHottextValueForRelatedInputField(DirectCast(responseIdentifierAttribute, XmlAttribute).OwnerDocument, DirectCast(responseIdentifierAttribute, XmlAttribute).OwnerElement.Attributes("hottextId").Value)
            End If
            If Not String.IsNullOrEmpty(defaultValue) Then defaultResponseValue = New ValueType With {.Value = defaultValue}
            Return defaultResponseValue
        End Function


    End Class

End Namespace