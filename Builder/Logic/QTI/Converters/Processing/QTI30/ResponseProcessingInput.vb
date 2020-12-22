Imports System.Globalization
Imports System.Linq
Imports System.Xml.Linq
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.QTI.Helpers.QTI_Base
Imports Questify.Builder.Logic.QTI.Helpers.QTI30
Imports Questify.Builder.Logic.QTI.Interfaces

Namespace QTI.Converters.Processing.QTI30

    Public Class ResponseProcessingInput
        Inherits ResponseProcessingPerTypeBase

        Private ReadOnly _gapType As QTI30CombinedScoringHelper.EnumGapType
        Private ReadOnly _decimalSeparator As QTI30CombinedScoringHelper.DecimalSeparator = QTI30CombinedScoringHelper.DecimalSeparator.None
        Protected Owner As IResponseProcessingCustomOperators

        Public Sub New(responseIndex As Integer, owner As IResponseProcessingCustomOperators, Optional responseSubIndex As Integer = 0)
            MyBase.New(responseIndex, responseSubIndex)
            Me.Owner = owner
            Me.ResponseIndex = responseIndex
            Me.ResponseSubIndex = responseSubIndex
        End Sub

        Public Sub New(responseIndex As Integer, owner As IResponseProcessingCustomOperators, decimalSeparator As QTI30CombinedScoringHelper.DecimalSeparator, Optional responseSubIndex As Integer = 0)
            MyBase.New(responseIndex, responseSubIndex)
            _decimalSeparator = decimalSeparator
            Me.Owner = owner
            Me.ResponseIndex = responseIndex
            Me.ResponseSubIndex = responseSubIndex
        End Sub

        Public Sub New(responseIndex As Integer, gapType As QTI30CombinedScoringHelper.EnumGapType, owner As IResponseProcessingCustomOperators, Optional responseSubIndex As Integer = 0)
            MyBase.New(responseIndex, responseSubIndex)
            _gapType = gapType
            Me.Owner = owner
            Me.ResponseSubIndex = responseSubIndex
        End Sub

        Public Sub New(responseIndex As Integer, gapType As QTI30CombinedScoringHelper.EnumGapType, decimalSeparator As QTI30CombinedScoringHelper.DecimalSeparator, owner As IResponseProcessingCustomOperators, Optional responseSubIndex As Integer = 0)
            MyBase.New(responseIndex, responseSubIndex)
            _gapType = gapType
            _decimalSeparator = decimalSeparator
            Me.Owner = owner
            Me.ResponseSubIndex = responseSubIndex
        End Sub

        Protected Overrides Function GetProcessingForFact(fact As KeyFact, Optional addNotMemberOfElement As Boolean = False, Optional outcomeIdentifier As String = "") As XElement
            Dim keyValue As KeyValue = DirectCast(fact.Values.First(), KeyValue)
            Return GetProcessingForKeyValue(keyValue)
        End Function

        Protected Function GetProcessingForKeyValue(keyValue As KeyValue) As XElement
            Dim processing As XElement = Nothing

            If keyValue.Values.Count = 1 Then
                Dim value = keyValue.Values.First()
                processing = GetProcessingForValue(value, ResponseIndex, keyValue)
            ElseIf keyValue.Values.Count > 1 Then
                processing = <qti-or></qti-or>
                For Each value In keyValue.Values
                    processing.Add(GetProcessingForValue(value, ResponseIndex, keyValue))
                Next
            End If

            Return processing
        End Function

        Protected Friend Function GetProcessingForKeyFactValue(keyValue As KeyValue, value As BaseValue) As XElement
            Return GetProcessingForValue(value, ResponseIndex, keyValue)
        End Function

        Private Function GetProcessingForValue(value As BaseValue, respIndex As Integer, keyValue As KeyValue) As XElement
            Dim processing As XElement = Nothing

            If TypeOf value Is IntegerValue Then
                processing = GetProcessingForIntegerValue(value, respIndex)
            ElseIf TypeOf value Is IntegerRangeValue Then
                processing = GetProcessingForIntegerRange(value, respIndex)
            ElseIf TypeOf value Is IntegerComparisonValue Then
                processing = GetProcessingForIntegerComparison(value, respIndex)
            ElseIf TypeOf value Is DecimalValue Then
                processing = GetProcessingForDecimalValue(value, respIndex)
            ElseIf TypeOf value Is DecimalRangeValue Then
                processing = GetProcessingForDecimalRange(value, respIndex)
            ElseIf TypeOf value Is DecimalComparisonValue Then
                processing = GetProcessingForDecimalComparison(value, respIndex)
            ElseIf TypeOf value Is StringValue OrElse TypeOf value Is StringComparisonValue Then
                processing = GetProcessingForStringValue(value, respIndex, keyValue)
            ElseIf TypeOf value Is BooleanValue Then
                processing = GetProcessingForBooleanValue(value, respIndex)
            End If

            Return processing
        End Function

        Protected Function GetProcessingForIntegerValue(value As BaseValue, respIndex As Integer) As XElement

            Dim processing As XElement = <qti-equal tolerance-mode="exact"></qti-equal>
            processing.Add(GetProcessingForVariable(respIndex))

            Dim baseType As String = GetBaseType(value)
            Dim correctResponse = CType(value, IntegerValue).Value.ToString(CultureInfo.InvariantCulture.NumberFormat)
            Dim correctValue As XElement = <qti-base-value base-type=<%= baseType %>><%= correctResponse %></qti-base-value>
            processing.Add(correctValue)

            Return processing
        End Function

        Protected Function GetProcessingForIntegerRange(value As BaseValue, respIndex As Integer) As XElement

            Dim integerValue = DirectCast(value, TypedRangeValue(Of Integer))
            Dim valueStart = integerValue.RangeStart.ToString(CultureInfo.InvariantCulture.NumberFormat)
            Dim valueEnd = integerValue.RangeEnd.ToString(CultureInfo.InvariantCulture.NumberFormat)
            Dim baseType = GetBaseType(value)
            QTIScoringHelper.GetResponseId(respIndex)

            Dim processing As XElement =
                <qti-and>
                    <qti-gte>
                        <%= GetProcessingForVariable(respIndex) %>
                        <qti-base-value base-type=<%= baseType %>><%= valueStart %></qti-base-value>
                    </qti-gte>
                    <qti-lte>
                        <%= GetProcessingForVariable(respIndex) %>
                        <qti-base-value base-type=<%= baseType %>><%= valueEnd %></qti-base-value>
                    </qti-lte>
                </qti-and>

            Return processing
        End Function

        Protected Function GetProcessingForIntegerComparison(value As BaseValue, respIndex As Integer) As XElement

            Dim integerComparisonValue = DirectCast(value, IntegerComparisonValue)
            Dim processing As XElement = GetIntegerComparisonOperator(integerComparisonValue)
            processing.Add(GetProcessingForVariable(respIndex))

            Dim baseType As String = GetBaseType(value)
            Dim correctResponse = integerComparisonValue.Value.ToString(CultureInfo.InvariantCulture.NumberFormat)
            Dim correctValue As XElement = <qti-base-value base-type=<%= baseType %>><%= correctResponse %></qti-base-value>
            processing.Add(correctValue)

            Return processing
        End Function

        Private Function GetIntegerComparisonOperator(integerComparisonValue As IntegerComparisonValue) As XElement

            If integerComparisonValue.GetComparisonType(integerComparisonValue.TypeOfComparison) = TypedComparisonValue(Of Integer).ComparisonType.GreaterThan Then
                Return <qti-gt></qti-gt>
            ElseIf integerComparisonValue.GetComparisonType(integerComparisonValue.TypeOfComparison) = TypedComparisonValue(Of Integer).ComparisonType.GreaterThanEquals Then
                Return <qti-gte></qti-gte>
            ElseIf integerComparisonValue.GetComparisonType(integerComparisonValue.TypeOfComparison) = TypedComparisonValue(Of Integer).ComparisonType.SmallerThan Then
                Return <qti-lt></qti-lt>
            ElseIf integerComparisonValue.GetComparisonType(integerComparisonValue.TypeOfComparison) = TypedComparisonValue(Of Integer).ComparisonType.SmallerThanEquals Then
                Return <qti-lte></qti-lte>
            Else
                Return <qti-equal tolerance-mode="exact"></qti-equal>
            End If

        End Function

        Protected Function GetProcessingForDecimalValue(value As BaseValue, respIndex As Integer) As XElement

            Dim processing As XElement = <qti-equal tolerance-mode="exact"></qti-equal>
            Dim decimalVariable = GetDecimalVariableOperator(respIndex)
            processing.Add(decimalVariable)

            Dim baseType As String = GetBaseType(value)
            Dim correctResponse = CType(value, DecimalValue).Value.ToString(CultureInfo.InvariantCulture.NumberFormat)
            Dim correctValue As XElement = <qti-base-value base-type=<%= baseType %>><%= correctResponse %></qti-base-value>
            processing.Add(correctValue)

            Return processing
        End Function

        Protected Function GetProcessingForDecimalRange(value As BaseValue, respIndex As Integer) As XElement

            Dim decimalValue = DirectCast(value, TypedRangeValue(Of Decimal))
            Dim valueStart = decimalValue.RangeStart.ToString(CultureInfo.InvariantCulture.NumberFormat)
            Dim valueEnd = decimalValue.RangeEnd.ToString(CultureInfo.InvariantCulture.NumberFormat)
            Dim baseType = GetBaseType(value)
            Dim decimalVariable = GetDecimalVariableOperator(respIndex)

            Dim processing As XElement =
                <qti-and>
                    <qti-gte>
                        <%= decimalVariable %>
                        <qti-base-value base-type=<%= baseType %>><%= valueStart %></qti-base-value>
                    </qti-gte>
                    <qti-lte>
                        <%= decimalVariable %>
                        <qti-base-value base-type=<%= baseType %>><%= valueEnd %></qti-base-value>
                    </qti-lte>
                </qti-and>

            Return processing
        End Function

        Private Function GetProcessingForDecimalComparison(value As BaseValue, respIndex As Integer) As XElement

            Dim decimalComparisonValue = DirectCast(value, DecimalComparisonValue)
            Dim processing As XElement = GetDecimalComparisonOperator(decimalComparisonValue)
            Dim decimalVariable = GetDecimalVariableOperator(respIndex)
            processing.Add(decimalVariable)

            Dim baseType As String = GetBaseType(value)
            Dim correctResponse = decimalComparisonValue.Value.ToString(CultureInfo.InvariantCulture.NumberFormat)
            Dim correctValue As XElement = <qti-base-value base-type=<%= baseType %>><%= correctResponse %></qti-base-value>
            processing.Add(correctValue)

            Return processing
        End Function

        Private Function GetDecimalVariableOperator(respIndex As Integer) As XElement
            Dim result As XElement = Owner.GetProcessingForVariable(respIndex, ResponseSubIndex)
            Select Case _decimalSeparator
                Case QTI30CombinedScoringHelper.DecimalSeparator.None, QTI30CombinedScoringHelper.DecimalSeparator.Comma
                    result = Owner.GetDecimalCustomOperator(respIndex, ResponseSubIndex)
                Case QTI30CombinedScoringHelper.DecimalSeparator.Dot
                    result = Owner.GetProcessingForVariable(respIndex, ResponseSubIndex)
                Case QTI30CombinedScoringHelper.DecimalSeparator.Both
                    result = Owner.GetProcessingForSpecifiedVariable(QTIScoringHelper.GetDecimalResponseId(respIndex), ResponseSubIndex)
            End Select
            Return result
        End Function

        Protected Function GetDecimalComparisonOperator(decimalComparisonValue As DecimalComparisonValue) As XElement

            If decimalComparisonValue.GetComparisonType(decimalComparisonValue.TypeOfComparison) = TypedComparisonValue(Of Decimal).ComparisonType.GreaterThan Then
                Return <qti-gt></qti-gt>
            ElseIf decimalComparisonValue.GetComparisonType(decimalComparisonValue.TypeOfComparison) = TypedComparisonValue(Of Decimal).ComparisonType.GreaterThanEquals Then
                Return <qti-gte></qti-gte>
            ElseIf decimalComparisonValue.GetComparisonType(decimalComparisonValue.TypeOfComparison) = TypedComparisonValue(Of Decimal).ComparisonType.SmallerThan Then
                Return <qti-lt></qti-lt>
            ElseIf decimalComparisonValue.GetComparisonType(decimalComparisonValue.TypeOfComparison) = TypedComparisonValue(Of Decimal).ComparisonType.SmallerThanEquals Then
                Return <qti-lte></qti-lte>
            Else
                Return <qti-equal tolerance-mode="exact"></qti-equal>
            End If

        End Function

        Protected Overridable Function GetBaseType(baseValue As BaseValue) As String
            Return ScoringHelper.GetBaseType(baseValue).ToString()
        End Function

        Protected Overridable Function GetProcessingForStringValue(value As BaseValue, respIndex As Integer, keyValue As KeyValue) As XElement
            Dim processing As XElement

            If _gapType = QTI30CombinedScoringHelper.EnumGapType.TimeGap Then
                processing = GetProcessingForTimeValue(value, respIndex)
            ElseIf _gapType = QTI30CombinedScoringHelper.EnumGapType.DateGap Then
                processing = GetProcessingForDateValue(value, respIndex)
            ElseIf _gapType = QTI30CombinedScoringHelper.EnumGapType.FormulaGap Then
                processing = GetProcessingForFormulaValue(value, respIndex)
            ElseIf QTIScoringHelper.ValueContainsPreProcessingRules(keyValue) Then
                processing = GetProcessingForStringValueWithPreProcessingRules(value, respIndex, keyValue)
            ElseIf TypeOf value Is StringComparisonValue Then
                processing = GetProcessingForStringComparisonValue(value, respIndex)
            Else
                processing = GetProcessingForStringValue(value, respIndex)
            End If

            Return processing
        End Function

        Protected Function GetProcessingForStringComparisonValue(value As BaseValue, respIndex As Integer) As XElement
            Dim processing As XElement = <qti-string-match case-sensitive="true"></qti-string-match>
            processing.Add(GetProcessingForVariable(respIndex))

            Dim baseType As String = GetBaseType(value)
            Dim correctResponse = CType(value, StringComparisonValue).Value
            Dim correctValue As XElement = <qti-base-value base-type=<%= baseType %>><%= correctResponse %></qti-base-value>
            processing.Add(correctValue)

            Return processing
        End Function

        Protected Function GetProcessingForStringValueWithPreProcessingRules(value As BaseValue, respIndex As Integer, keyValue As KeyValue) As XElement
            Dim correctResponse = CType(value, StringValue).Value
            Dim processing = XElement.Parse(Owner.GetPreProcessingCustomOperator(keyValue.PreProcessingRules, respIndex, correctResponse, ResponseSubIndex))
            Return processing
        End Function

        Protected Function GetProcessingForStringValue(value As BaseValue, respIndex As Integer) As XElement

            Dim processing As XElement = <qti-string-match case-sensitive="true"></qti-string-match>
            processing.Add(GetProcessingForVariable(respIndex))

            Dim baseType As String = GetBaseType(value)
            Dim correctResponse = CType(value, StringValue).Value
            Dim correctValue As XElement = <qti-base-value base-type=<%= baseType %>><%= correctResponse %></qti-base-value>
            processing.Add(correctValue)

            Return processing
        End Function

        Private Function GetProcessingForFormulaValue(value As BaseValue, respIndex As Integer) As XElement

            Dim processing As XElement = Nothing
            Dim correctResponse As String = String.Empty

            If TypeOf value Is StringValue Then
                correctResponse = CType(value, StringValue).Value
            ElseIf TypeOf value Is StringComparisonValue Then
                correctResponse = CType(value, StringComparisonValue).Value
            End If
            processing = Owner.GetMathMLCustomOperator_Equals(respIndex, correctResponse, ResponseSubIndex)

            Return processing
        End Function

        Protected Function GetProcessingForTimeValue(value As BaseValue, respIndex As Integer) As XElement

            Dim processing As XElement = <qti-and></qti-and>
            Dim nrOfTimeParts = GapTimeScoringHelper.NrOfTimeParts(value)
            Dim index As Integer = respIndex
            For i As Integer = 1 To nrOfTimeParts
                Dim correctTimePart = GapTimeScoringHelper.GetPartOfTimeValue(value, i)
                Dim timePartProcessing = GetProcessingForTimePart(correctTimePart, index)
                processing.Add(timePartProcessing)
                index += 1
            Next

            Return processing
        End Function

        Protected Function GetProcessingForTimePart(correctResponse As String, respIndex As Integer) As XElement

            Dim processing As XElement = <qti-equal toleranceMode="exact"></qti-equal>
            processing.Add(GetProcessingForVariable(respIndex))

            Dim correctValue As XElement = <qti-base-value base-type="integer"><%= correctResponse %></qti-base-value>
            processing.Add(correctValue)

            Return processing
        End Function

        Protected Function GetProcessingForDateValue(value As BaseValue, respIndex As Integer) As XElement

            Dim processing As XElement = <qti-and></qti-and>
            Dim nrOfDateParts = GapDateScoringHelper.NrOfDateParts(value)
            Dim index As Integer = respIndex
            For i As Integer = 1 To nrOfDateParts
                Dim correctDatePart = GapDateScoringHelper.GetPartOfDateValue(value, i)
                Dim datePartProcessing = GetProcessingForDatePart(correctDatePart, index)
                processing.Add(datePartProcessing)
                index += 1
            Next

            Return processing
        End Function

        Protected Function GetProcessingForDatePart(correctResponse As String, respIndex As Integer) As XElement

            Dim processing As XElement = <qti-equal tolerance-mode="exact"></qti-equal>
            processing.Add(GetProcessingForVariable(respIndex))

            Dim correctValue As XElement = <qti-base-value base-type="integer"><%= correctResponse %></qti-base-value>
            processing.Add(correctValue)

            Return processing
        End Function

        Private Function GetProcessingForBooleanValue(value As BaseValue, respIndex As Integer) As XElement

            Dim processing As XElement = Nothing

            Dim equalStepsValue As BooleanValue = TryCast(value, BooleanValue)
            If (equalStepsValue IsNot Nothing) Then
                Dim equalSteps As Boolean = equalStepsValue.Value
                processing = GetProcessingForEqualStepsValue(equalSteps, respIndex)
            End If

            Return processing
        End Function

        Protected Overridable Function GetProcessingForEqualStepsValue(ByVal equalSteps As Boolean, ByVal respIndex As Integer) As XElement
            Dim processing As XElement = Nothing
            If (equalSteps) Then
                processing = Owner.GetMathMLCustomOperator_EqualSteps(respIndex, ResponseSubIndex)
            End If
            Return processing
        End Function

    End Class

End Namespace