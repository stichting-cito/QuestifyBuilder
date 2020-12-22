Imports System.Runtime.CompilerServices
Imports System.Linq

Public Module Extensions

    <Extension>
    Public Sub OverrideAttributeReferences(paramSetCollection As ParameterSetCollection)
        For Each paramCollection As ParameterCollection In paramSetCollection
            paramCollection.OverrideAttributeReferences()
        Next
    End Sub

    <Extension>
    Public Sub OverrideAttributeReferences(controlParameterSet As ParameterCollection)
        For Each param As ParameterBase In controlParameterSet.InnerParameters
            For Each attrRef In param.AttributeReferences
                Dim sourceParam = controlParameterSet.GetParameterByName(attrRef.Value)
                Debug.Assert(sourceParam IsNot Nothing, "Cannot find source parameter: " & attrRef.Value)
                Dim targetProp = param.GetType().GetProperties().FirstOrDefault(Function(prop) prop.Name.Equals(attrRef.Name, StringComparison.InvariantCultureIgnoreCase))
                Debug.Assert(targetProp IsNot Nothing, "Cannot find property to set: " & attrRef.Name)

                If attrRef.WhatToCopy = AttributeReference.WhatToCpy.Value Then
                    Dim sourceValue = sourceParam.GetType().GetProperty("Value")
                    Debug.Assert(sourceValue IsNot Nothing, "Cannot find 'Value' of source parameter: " & attrRef.Value)

                    If targetProp.PropertyType.IsEnum Then
                        Dim newConvertedValue = [Enum].Parse(targetProp.PropertyType, sourceValue.GetValue(sourceParam, Nothing).ToString)
                        targetProp.SetValue(param, newConvertedValue, Nothing)
                    Else
                        Dim newConvertedValue = Convert.ChangeType(sourceValue.GetValue(sourceParam, Nothing), targetProp.PropertyType)
                        targetProp.SetValue(param, newConvertedValue, Nothing)
                    End If
                ElseIf attrRef.WhatToCopy = AttributeReference.WhatToCpy.Parameter Then
                    targetProp.SetValue(param, sourceParam, Nothing)
                End If
            Next
            If TypeOf param Is CollectionParameter AndAlso DirectCast(param, CollectionParameter).BluePrint IsNot Nothing Then
                OverrideAttributeReferences(DirectCast(param, CollectionParameter).BluePrint)
            End If
        Next
    End Sub

    <Extension>
    public Function HasValue(baseValue As BaseValue) As Boolean
        If TypeOf baseValue Is IntegerRangeValue Then
            Return Not String.IsNullOrEmpty(DirectCast(baseValue, IntegerRangeValue).RangeStart.ToString()) AndAlso Not String.IsNullOrEmpty(DirectCast(baseValue, IntegerRangeValue).RangeEnd.ToString())
        ElseIf TypeOf baseValue Is IntegerComparisonValue Then
            Return Not String.IsNullOrEmpty(DirectCast(baseValue, IntegerComparisonValue).Value.ToString())
        ElseIf TypeOf baseValue Is IntegerValue Then
            Return Not String.IsNullOrEmpty(DirectCast(baseValue, IntegerValue).ToString())
        ElseIf TypeOf baseValue Is DecimalRangeValue Then
            Return Not String.IsNullOrEmpty(DirectCast(baseValue, DecimalRangeValue).RangeStart.ToString()) AndAlso Not String.IsNullOrEmpty(DirectCast(baseValue, DecimalRangeValue).RangeEnd.ToString())
        ElseIf TypeOf baseValue Is DecimalComparisonValue Then
            Return Not String.IsNullOrEmpty(DirectCast(baseValue, DecimalComparisonValue).Value.ToString())
        ElseIf TypeOf baseValue Is DecimalValue Then
            Return Not String.IsNullOrEmpty(DirectCast(baseValue, DecimalValue).ToString())
        ElseIf TypeOf baseValue Is StringValue Then
            Return Not String.IsNullOrEmpty(DirectCast(baseValue, StringValue).Value)
        ElseIf TypeOf baseValue Is StringComparisonValue Then
            Return Not String.IsNullOrEmpty(DirectCast(baseValue, StringComparisonValue).Value)
        Else
            Return True
        End If
    End Function

End Module
