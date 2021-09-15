
Imports Questify.Builder.Logic.ItemProcessing
Imports Cito.Tester.ContentModel
Imports System.Xml.Linq
Imports Questify.Builder.UnitTests.Framework

<TestClass()>
Public Class DefaultParameterMergerTest
    Inherits ItemProcessingBase

    <TestMethod(), TestCategory("ItemProcessing"), Ignore>
    <DeploymentItem("Cito.TestBuilder.Logic\ItemProcessing\DefaultMergerData.xlsx")>
    <DataSource("System.Data.OleDb", "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=DefaultMergerData.xlsx;Extended Properties=Excel 12.0;Persist Security Info=False", "DataTests$", DataAccessMethod.Sequential)>
    Public Sub DataDrivenMergetest_for_DefaultParametermerger()
        'Arrange

        '[DataDriven Test]
        'Available Params:
        '- Type
        '- NewParamSet
        '- CurrParamSet
        '- Warnings
        '- Errors
        '- Description

        Dim ser As System.Xml.Serialization.XmlSerializer
        Dim handler As New DefaultParameterHandler
        Dim newBooleanParam As New BooleanParameter()
        Dim currentBooleanParam As New BooleanParameter()
        Dim war As New WarningsAndErrors
        Dim desc As String = Data("Description")
        Dim paramType As Type = GetType(ParameterBase).Assembly.GetType(Data("Type"))

        If paramType Is Nothing Then Assert.Fail() 'Fail when unknown type.

        ser = GetSerializer(paramType)
        newBooleanParam = ser.Deserialize(Of BooleanParameter)(Data("NewParamSet"))
        currentBooleanParam = ser.Deserialize(Of BooleanParameter)(Data("CurrParamSet"))

        'Act
        handler.Merge(newBooleanParam, currentBooleanParam, war)

        'Assert
        'Expected NR# Errors?
        Assert.AreEqual(GetInt("Errors"), war.ErrorList.Count, desc)
        'Expected NR# Warnings?
        Assert.AreEqual(GetInt("Warnings"), war.WarningList.Count, desc)
    End Sub

    <TestMethod(), TestCategory("ItemProcessing")>
    Public Sub MergeDefaultParameter_ValueIsCopied()
        'Arrange
        Dim handler As New DefaultParameterHandler
        Dim newParam = Deserialize(Of PlainTextParameter)(_plainTextParameterToMergeTo)
        Dim currentParm = Deserialize(Of PlainTextParameter)(_plaingTextParameterToMergeFrom)
        Dim warningsAndErrors As New WarningsAndErrors

        'Act
        handler.Merge(newParam, currentParm, warningsAndErrors)

        'Assert
        Assert.AreEqual(0, warningsAndErrors.WarningList.Count)
        Assert.AreEqual(0, warningsAndErrors.ErrorList.Count)
    End Sub

#Region " Data "

    ReadOnly _plainTextParameterToMergeTo As XElement =
        <PlainTextParameter xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="gapId"/>

    ReadOnly _plaingTextParameterToMergeFrom As XElement =
        <PlainTextParameter xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="gapId">Ib262bdcb-2e1b-4c74-a675-1d3a8bc0e293</PlainTextParameter>

#End Region

End Class
