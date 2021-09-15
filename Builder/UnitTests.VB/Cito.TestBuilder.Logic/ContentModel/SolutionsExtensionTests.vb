﻿
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ContentModel

<TestClass()>
Public Class SolutionsExtensionTests

    <TestMethod(), TestCategory("Logic")>
    Public Sub BaseFactValueToControlId_ControlId()
        'Arrange
        Dim keyValue As New KeyValue("1[0]-I3a005164-9453-4e00-ad76-4f5eefd1624c", 1)

        'Act
        Dim result = keyValue.GetControlId("I3a005164-9453-4e00-ad76-4f5eefd1624c")

        'Assert
        Assert.AreEqual("I3a005164-9453-4e00-ad76-4f5eefd1624c", result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub BaseFactValueToControlId_NotAGuid_ControlId()
        'Arrange
        Dim keyValue As New KeyValue("1-integerScore", 1)

        'Act
        Dim result = keyValue.GetControlId("integerScore")

        'Assert
        Assert.AreEqual("integerScore", result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub BaseFactValueToControlId_GuidAndPostfix_ControlId()
        'Arrange
        Dim keyValue As New KeyValue("1[0]-I3a005164-9453-4e00-ad76-4f5eefd1624cSomeExtraText", 1)

        'Act
        Dim result = keyValue.GetControlId("I3a005164-9453-4e00-ad76-4f5eefd1624c")

        'Assert
        Assert.AreEqual("I3a005164-9453-4e00-ad76-4f5eefd1624c", result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub BaseFactValueToControlId_ControlIdNotInDomain_StringEmpty()
        'Arrange
        Dim keyValue As New KeyValue("1[0]-I3a005164-9453-4e00-ad76-4f5eefd1624cSomeExtraText", 1)

        'Act
        Dim result = keyValue.GetControlId("I00005164-9453-4e00-ad76-4f5eefd1624c")

        'Assert
        Assert.AreEqual(String.Empty, result)
    End Sub

End Class
