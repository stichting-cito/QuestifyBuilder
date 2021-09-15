﻿
Imports Cito.Tester.ContentModel
Imports System.Linq

<TestClass()>
Public Class ExtendedNamedDecimalMapTests

#Region "Prepare values"
    Private Const _extendedDecimalMapString As String = "{a:4,b:3,c:8}{a:9,b:5,c:1}{a:2,b:7,c:6}[deg:2][other:none]"
    Private Const _incorrectOrderedFormatString = "[deg:2][other:none]{a:4,b:3,c:8}{a:9,b:5,c:1}{a:2,b:7,c:6}"
    Private Const _decimalMapPart As String = "{a:4,b:3,c:8}{a:9,b:5,c:1}{a:2,b:7,c:6}"

    Private Function GetExpectedExtendedDecimalMap() As ExtendedNamedDecimalMap
        Dim expected As New ExtendedNamedDecimalMap()
        expected.Extensions.Add("deg", "2")
        expected.Extensions.Add("other", "none")
        expected.NamedDecimalMap = NamedDecimalMap.FromString(_decimalMapPart)
        Return expected
    End Function
#End Region

    <TestMethod()> <TestCategory("ContentModel")>
    <ExpectedException(GetType(ArgumentException))>
    Public Sub ThrowOnIncorrectOrderedString()
        ExtendedNamedDecimalMap.FromString(_incorrectOrderedFormatString)
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub GetExtendedNamedDecimalFromNamedDecimalMapString()
        'Arrange
        Dim expected = NamedDecimalMap.FromString(_decimalMapPart).ToString
        
        'Act
        Dim result = ExtendedNamedDecimalMap.FromString(_decimalMapPart).NamedDecimalMap.ToString
        
        'Assert
        Assert.AreEqual(expected, result)
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub GetDecimalMapFromString()
        'Arrange
        Dim expected = GetExpectedExtendedDecimalMap.NamedDecimalMap.ToString
        
        'Act
        Dim result = ExtendedNamedDecimalMap.FromString(_extendedDecimalMapString).NamedDecimalMap.ToString
        
        'Assert
        Assert.AreEqual(expected, result)
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub GetExtensionsFromString()
        'Arrange
        Dim expected = GetExpectedExtendedDecimalMap.Extensions
        
        'Act
        Dim result = ExtendedNamedDecimalMap.FromString(_extendedDecimalMapString).Extensions
       
        'Assert
        Assert.IsTrue(expected.Count = result.Count AndAlso Not expected.Except(result).Any())
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub GetStringFromExtendedDecimalMap()
        'Arrange
        Dim expected As String = _extendedDecimalMapString
       
        'Act
        Dim result = GetExpectedExtendedDecimalMap().ToString()
       
        'Assert
        Assert.AreEqual(expected, result)
    End Sub

End Class
