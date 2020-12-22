
Imports System.Globalization
Imports System.Linq
Imports System.Xml.Linq
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.QTI.Helpers.QTI_Base
Imports Questify.Builder.Logic.QTI.Helpers.QTI22
Imports Questify.Builder.Logic.QTI.Interfaces

Namespace QTI.Converters.Processing.QTI22

    Public Class ResponseProcessingInput
        Inherits ResponseProcessingPerTypeBase

        Private ReadOnly _gapType As CombinedScoringHelper.EnumGapType
        Private ReadOnly _decimalSeparator As CombinedScoringHelper.DecimalSeparator = CombinedScoringHelper.DecimalSeparator.None
        Protected Owner As IResponseProcessingCustomOperators

        Public Sub New(responseIndex As Integer, owner As IResponseProcessingCustomOperators, Optional responseSubIndex As Integer = 0)
            MyBase.New(responseIndex, responseSubIndex)
            Me.Owner = owner
            Me.ResponseIndex = responseIndex
            Me.ResponseSubIndex = responseSubIndex
        End Sub

        Public Sub New(responseIndex As Integer, owner As IResponseProcessingCustomOperators, decimalSeparator As CombinedScoringHelper.DecimalSeparator, Optional responseSubIndex As Integer = 0)
            MyBase.New(responseIndex, responseSubIndex)
            _decimalSeparator = decimalSeparator
            Me.Owner = owner
            Me.ResponseIndex = responseIndex
            Me.ResponseSubIndex = responseSubIndex
        End Sub

        Public Sub New(responseIndex As Integer, gapType As CombinedScoringHelper.EnumGapType, owner As IResponseProcessingCustomOperators, Optional responseSubIndex As Integer = 0)
            MyBase.New(responseIndex, responseSubIndex)
            _gapType = gapType
            Me.Owner = owner
            Me.ResponseSubIndex = responseSubIndex
        End Sub

        Public Sub New(responseIndex As Integer, gapType As CombinedScoringHelper.EnumGapType, decimalSeparator As CombinedScoringHelper.DecimalSeparator, owner As IResponseProcessingCustomOperators, Optional responseSubIndex As Integer = 0)
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
                processing = <or></or>
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

            Dim processing As XElement = <equal toleranceMode="exact"></equal>
            processing.Add(GetProcessingForVariable(respIndex))

            Dim baseType As String = GetBaseType(value)
            Dim correctResponse = CType(value, IntegerValue).Value.ToString(CultureInfo.InvariantCulture.NumberFormat)
            Dim correctValue As XElement = <baseValue baseType=<%= baseType %>><%= correctResponse %></baseValue>
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
                <and>
                    <gte>
                        <%= GetProcessingForVariable(respIndex) %>
                        <baseValue baseType=<%= baseType %>><%= valueStart %></baseValue>
                    </gte>
                    <lte>
                        <%= GetProcessingForVariable(respIndex) %>
                        <baseValue baseType=<%= baseType %>><%= valueEnd %></baseValue>
                    </lte>
                </and>

            Return processing
        End Function

        Protected Function GetProcessingForIntegerComparison(value As BaseValue, respIndex As Integer) As XElement

            Dim integerComparisonValue = DirectCast(value, IntegerComparisonValue)
            Dim processing As XElement = GetIntegerComparisonOperator(integerComparisonValue)
            processing.Add(GetProcessingForVariable(respIndex))

            Dim baseType As String = GetBaseType(value)
            Dim correctResponse = integerComparisonValue.Value.ToString(CultureInfo.InvariantCulture.NumberFormat)
            Dim correctValue As XElement = <baseValue baseType=<%= baseType %>><%= correctResponse %></baseValue>
            processing.Add(correctValue)

            Return processing
        End Function

        Private Function GetIntegerComparisonOperator(integerComparisonValue As IntegerComparisonValue) As XElement

            If integerComparisonValue.GetComparisonType(integerComparisonValue.TypeOfComparison) = TypedComparisonValue(Of Integer).ComparisonType.GreaterThan Then
                Return <gt></gt>
            ElseIf integerComparisonValue.GetComparisonType(integerComparisonValue.TypeOfComparison) = TypedComparisonValue(Of Integer).ComparisonType.GreaterThanEquals Then
                Return <gte></gte>
            ElseIf integerComparisonValue.GetComparisonType(integerComparisonValue.TypeOfComparison) = TypedComparisonValue(Of Integer).ComparisonType.SmallerThan Then
                Return <lt></lt>
            ElseIf integerComparisonValue.GetComparisonType(integerComparisonValue.TypeOfComparison) = TypedComparisonValue(Of Integer).ComparisonType.SmallerThanEquals Then
                Return <lte></lte>
            Else
                Return <equal toleranceMode="exact"></equal>
            End If

        End Function

        Protected Function GetProcessingForDecimalValue(value As BaseValue, respIndex As Integer) As XElement

            Dim processing As XElement = <equal toleranceMode="exact"></equal>
            Dim decimalVariable = GetDecimalVariableOperator(respIndex)
            processing.Add(decimalVariable)

            Dim baseType As String = GetBaseType(value)
            Dim correctResponse = CType(value, DecimalValue).Value.ToString(CultureInfo.InvariantCulture.NumberFormat)
            Dim correctValue As XElement = <baseValue baseType=<%= baseType %>><%= correctResponse %></baseValue>
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
                <and>
                    <gte>
                        <%= decimalVariable %>
                        <baseValue baseType=<%= baseType %>><%= valueStart %></baseValue>
                    </gte>
                    <lte>
                        <%= decimalVariable %>
                        <baseValue baseType=<%= baseType %>><%= valueEnd %></baseValue>
                    </lte>
                </and>

            Return processing
        End Function

        Private Function GetProcessingForDecimalComparison(value As BaseValue, respIndex As Integer) As XElement

            Dim decimalComparisonValue = DirectCast(value, DecimalComparisonValue)
            Dim processing As XElement = GetDecimalComparisonOperator(decimalComparisonValue)
            Dim decimalVariable = GetDecimalVariableOperator(respIndex)
            processing.Add(decimalVariable)

            Dim baseType As String = GetBaseType(value)
            Dim correctResponse = decimalComparisonValue.Value.ToString(CultureInfo.InvariantCulture.NumberFormat)
            Dim correctValue As XElement = <baseValue baseType=<%= baseType %>><%= correctResponse %></baseValue>
            processing.Add(correctValue)

            Return processing
        End Function

        Private Function GetDecimalVariableOperator(respIndex As Integer) As XElement
            Dim result As XElement = Owner.GetProcessingForVariable(respIndex, ResponseSubIndex)
            Select Case _decimalSeparator
                Case CombinedScoringHelper.DecimalSeparator.None, CombinedScoringHelper.DecimalSeparator.Comma
                    result = Owner.GetDecimalCustomOperator(respIndex, ResponseSubIndex)
                Case CombinedScoringHelper.DecimalSeparator.Dot
                    result = Owner.GetProcessingForVariable(respIndex, ResponseSubIndex)
                Case CombinedScoringHelper.DecimalSeparator.Both
                    result = Owner.GetProcessingForSpecifiedVariable(QTIScoringHelper.GetDecimalResponseId(respIndex), ResponseSubIndex)
            End Select
            Return result
        End Function

        Protected Function GetDecimalComparisonOperator(decimalComparisonValue As DecimalComparisonValue) As XElement

            If decimalComparisonValue.GetComparisonType(decimalComparisonValue.TypeOfComparison) = TypedComparisonValue(Of Decimal).ComparisonType.GreaterThan Then
                Return <gt></gt>
            ElseIf decimalComparisonValue.GetComparisonType(decimalComparisonValue.TypeOfComparison) = TypedComparisonValue(Of Decimal).ComparisonType.GreaterThanEquals Then
                Return <gte></gte>
            ElseIf decimalComparisonValue.GetComparisonType(decimalComparisonValue.TypeOfComparison) = TypedComparisonValue(Of Decimal).ComparisonType.SmallerThan Then
                Return <lt></lt>
            ElseIf decimalComparisonValue.GetComparisonType(decimalComparisonValue.TypeOfComparison) = TypedComparisonValue(Of Decimal).ComparisonType.SmallerThanEquals Then
                Return <lte></lte>
            Else
                Return <equal toleranceMode="exact"></equal>
            End If

        End Function

        Protected Overridable Function GetBaseType(baseValue As BaseValue) As String
            Return QTI22ScoringHelper.GetBaseType(baseValue).ToString()
        End Function

        Protected Overridable Function GetProcessingForStringValue(value As BaseValue, respIndex As Integer, keyValue As KeyValue) As XElement
            Dim processing As XElement

            If _gapType = CombinedScoringHelper.EnumGapType.TimeGap Then
                processing = GetProcessingForTimeValue(value, respIndex)
            ElseIf _gapType = CombinedScoringHelper.EnumGapType.DateGap Then
                processing = GetProcessingForDateValue(value, respIndex)
            ElseIf _gapType = CombinedScoringHelper.EnumGapType.FormulaGap Then
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
            Dim processing As XElement = <stringMatch caseSensitive="true"></stringMatch>
            processing.Add(GetProcessingForVariable(respIndex))

            Dim baseType As String = GetBaseType(value)
            Dim correctResponse = CType(value, StringComparisonValue).Value
            Dim correctValue As XElement = <baseValue baseType=<%= baseType %>><%= correctResponse %></baseValue>
            processing.Add(correctValue)

            Return processing
        End Function

        Protected Function GetProcessingForStringValueWithPreProcessingRules(value As BaseValue, respIndex As Integer, keyValue As KeyValue) As XElement
            Dim correctResponse = CType(value, StringValue).Value
            Dim processing = XElement.Parse(Owner.GetPreProcessingCustomOperator(keyValue.PreProcessingRules, respIndex, correctResponse, ResponseSubIndex))
            Return processing
        End Function

        Protected Function GetProcessingForStringValue(value As BaseValue, respIndex As Integer) As XElement

            Dim processing As XElement = <stringMatch caseSensitive="true"></stringMatch>
            processing.Add(GetProcessingForVariable(respIndex))

            Dim baseType As String = GetBaseType(value)
            Dim correctResponse = CType(value, StringValue).Value
            Dim correctValue As XElement = <baseValue baseType=<%= baseType %>><%= correctResponse %></baseValue>
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

            Dim processing As XElement = <and></and>
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

            Dim processing As XElement = <equal toleranceMode="exact"></equal>
            processing.Add(GetProcessingForVariable(respIndex))

            Dim correctValue As XElement = <baseValue baseType="integer"><%= correctResponse %></baseValue>
            processing.Add(correctValue)

            Return processing
        End Function

        Protected Function GetProcessingForDateValue(value As BaseValue, respIndex As Integer) As XElement

            Dim processing As XElement = <and></and>
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

            Dim processing As XElement = <equal toleranceMode="exact"></equal>
            processing.Add(GetProcessingForVariable(respIndex))

            Dim correctValue As XElement = <baseValue baseType="integer"><%= correctResponse %></baseValue>
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