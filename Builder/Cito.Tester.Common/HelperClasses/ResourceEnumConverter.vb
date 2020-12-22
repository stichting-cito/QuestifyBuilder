
Imports System
Imports System.Collections.Generic
Imports System.Globalization
Imports System.ComponentModel

Public Class ResourceEnumConverter
    Inherits EnumConverter
    Private Class LookupTable
        Inherits Dictionary(Of String, Object)
    End Class
    Private _lookupTables As New Dictionary(Of CultureInfo, LookupTable)()
    Private _resourceManager As Resources.ResourceManager
    Private _isFlagEnum As Boolean = False
    Private _flagValues As Array


    Private Function GetLookupTable(culture As CultureInfo) As LookupTable
        Dim result As LookupTable = Nothing
        If culture Is Nothing Then
            culture = CultureInfo.CurrentCulture
        End If

        If Not _lookupTables.TryGetValue(culture, result) Then
            result = New LookupTable()
            For Each value As Object In GetStandardValues()
                Dim text As String = GetValueText(culture, value)
                If text IsNot Nothing Then
                    result.Add(text, value)
                End If
            Next
            _lookupTables.Add(culture, result)
        End If
        Return result
    End Function

    Private Function GetValueText(culture As CultureInfo, value As Object) As String
        Dim result As String
        Dim type As Type = value.[GetType]()
        Dim resourceName As String = $"{type.Name}_{value.ToString()}"
        result = _resourceManager.GetString(resourceName, culture)
        If result Is Nothing Then
#If DEBUG Then
            MsgBox($"Failed to get resource : {resourceName}")
#End If
            result = value.ToString()
        End If
        Return result
    End Function


    Private Function IsSingleBitValue(value As ULong) As Boolean
        Select Case value
            Case 0
                Return False
            Case 1
                Return True
        End Select
        Return ((value And (value - 1UL)) = 0)
    End Function

    Private Function GetFlagValueText(culture As CultureInfo, value As Object) As String
        If [Enum].IsDefined(value.[GetType](), value) Then
            Return GetValueText(culture, value)
        End If

        Dim lValue As ULong = Convert.ToUInt32(value)
        Dim result As String = Nothing
        For Each flagValue As Object In _flagValues
            Dim lFlagValue As ULong = Convert.ToUInt32(flagValue)
            If IsSingleBitValue(lFlagValue) Then
                If (lFlagValue And lValue) = lFlagValue Then
                    Dim valueText As String = GetValueText(culture, flagValue)
                    If result Is Nothing Then
                        result = valueText
                    Else
                        result = $"{result}, {valueText}"
                    End If
                End If
            End If
        Next
        Return result
    End Function

    Private Function GetValue(culture As CultureInfo, text As String) As Object
        Dim lookupTable As LookupTable = GetLookupTable(culture)
        Dim result As Object = Nothing
        lookupTable.TryGetValue(text, result)
        Return result
    End Function

    Private Function GetFlagValue(culture As CultureInfo, text As String) As Object
        Dim lookupTable As LookupTable = GetLookupTable(culture)
        Dim textValues As String() = text.Split(","c)
        Dim result As ULong = 0
        For Each textValue As String In textValues
            Dim value As Object = Nothing
            Dim trimmedTextValue As String = textValue.Trim()
            If Not lookupTable.TryGetValue(trimmedTextValue, value) Then
                Return Nothing
            End If
            result = result Or Convert.ToUInt32(value)
        Next
        Return [Enum].ToObject(EnumType, result)
    End Function

    Public Sub New(type As Type, resourceManager As Resources.ResourceManager)
        MyBase.New(type)
        _resourceManager = resourceManager
        Dim flagAttributes As Object() = type.GetCustomAttributes(GetType(FlagsAttribute), True)
        _isFlagEnum = flagAttributes.Length > 0
        If _isFlagEnum Then
            _flagValues = [Enum].GetValues(type)
        End If
    End Sub


    Public Overloads Overrides Function ConvertFrom(context As ITypeDescriptorContext, culture As CultureInfo, value As Object) As Object
        culture = CultureInfo.CurrentUICulture
        If TypeOf value Is String Then
            Dim result As Object
            If _isFlagEnum Then
                result = GetFlagValue(culture, DirectCast(value, String))
            Else
                result = GetValue(culture, DirectCast(value, String))
            End If

            If result Is Nothing Then
                result = MyBase.ConvertFrom(context, culture, value)
            End If
            Return result
        Else
            Return MyBase.ConvertFrom(context, culture, value)
        End If
    End Function


    Public Overloads Overrides Function ConvertTo(context As ITypeDescriptorContext, culture As CultureInfo, value As Object, destinationType As Type) As Object
        If value IsNot Nothing AndAlso destinationType Is GetType(String) Then
            Dim result As Object

            If _isFlagEnum Then
                result = GetFlagValueText(CultureInfo.CurrentUICulture, value)
            Else
                result = GetValueText(CultureInfo.CurrentUICulture, value)
            End If

            Return result
        Else
            Return MyBase.ConvertTo(context, CultureInfo.CurrentUICulture, value, destinationType)
        End If
    End Function

    Public Overloads Shared Function ConvertToString(value As [Enum]) As String
        Dim converter As TypeConverter = TypeDescriptor.GetConverter(value.[GetType]())

        Return converter.ConvertToString(Nothing, CultureInfo.CurrentUICulture, value)
    End Function


    Public Overloads Shared Function ConvertToString(value As [Enum], culture As CultureInfo) As String
        Dim converter As TypeConverter = TypeDescriptor.GetConverter(value.[GetType]())

        Return converter.ConvertToString(Nothing, culture, value)
    End Function

    Public Shared Function GetValues(enumType As Type, culture As CultureInfo) As List(Of KeyValuePair(Of [Enum], String))
        Dim result As New List(Of KeyValuePair(Of [Enum], String))()
        Dim converter As TypeConverter = TypeDescriptor.GetConverter(enumType)
        For Each value As [Enum] In [Enum].GetValues(enumType)
            Dim pair As New KeyValuePair(Of [Enum], String)(value, converter.ConvertToString(Nothing, culture, value))
            result.Add(pair)
        Next
        Return result
    End Function

    Public Shared Function GetValues(enumType As Type) As List(Of KeyValuePair(Of [Enum], String))
        Return GetValues(enumType, CultureInfo.CurrentUICulture)
    End Function
End Class
