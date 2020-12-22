
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.QTI.Helpers.QTI_Base

<TestClass()>
Public Class QTI22KeyValueContainsDateTimeValuesTest

    <TestMethod()> <TestCategory("KeyValueContainsDateTimeValues")> <Description("Keyvalue contains timevalues")>
    Public Sub KeyValueContainsTimeValues()
        Dim checkValue As BaseValue = New StringValue("12:34:56")
        Dim checkValue2 As BaseValue = New StringValue("test stringwaarde")
        Dim keyVal As KeyValue = New KeyValue("testDomain", 1)
        keyVal.Values.Add(checkValue)
        keyVal.Values.Add(checkValue2)
        Assert.IsTrue(GapTimeScoringHelper.ValueContainsTimeValues(keyVal.Values))
    End Sub

    <TestMethod()> <TestCategory("KeyValueContainsDateTimeValues")> <Description("Keyvalue contains no timevalues")>
    Public Sub KeyValueContainsNoTimeValues()
        Dim checkValue As BaseValue = New StringValue("het is nu 16:55 uur")
        Dim checkValue2 As BaseValue = New StringValue("test stringwaarde")
        Dim keyVal As KeyValue = New KeyValue("testDomain", 1)
        keyVal.Values.Add(checkValue)
        keyVal.Values.Add(checkValue2)
        Assert.IsFalse(GapTimeScoringHelper.ValueContainsTimeValues(keyVal.Values))
    End Sub

    <TestMethod()> <TestCategory("KeyValueContainsDateTimeValues")> <Description("Basevalue contains wrong time-format")>
    Public Sub BaseValueContainsWrongTimeFormat()
        Dim checkValue As BaseValue = New StringValue("12:34:56:78")
        Assert.IsFalse(GapTimeScoringHelper.BaseValueIsTimeValue(checkValue))
    End Sub

    <TestMethod()> <TestCategory("KeyValueContainsDateTimeValues")> <Description("Basevalue contains invalid timevalues - hour out of range")>
    Public Sub BaseValueContainsInvalidTimeValuesWrongHour()
        Dim checkValue As BaseValue = New StringValue("24:12:34")
        Assert.IsFalse(GapTimeScoringHelper.BaseValueIsTimeValue(checkValue))
    End Sub

    <TestMethod()> <TestCategory("KeyValueContainsDateTimeValues")> <Description("Basevalue contains invalid timevalues - seconds out of range")>
    Public Sub BaseValueContainsInvalidTimeValuesWrongSeconds()
        Dim checkValue As BaseValue = New StringValue("12:34:60")
        Assert.IsFalse(GapTimeScoringHelper.BaseValueIsTimeValue(checkValue))
    End Sub

    <TestMethod()> <TestCategory("KeyValueContainsDateTimeValues")> <Description("Basevalue contains invalid timevalues - negative numbers")>
    Public Sub BaseValueContainsInvalidTimeValuesNegative()
        Dim checkValue As BaseValue = New StringValue("12:-4:56")
        Assert.IsFalse(GapTimeScoringHelper.BaseValueIsTimeValue(checkValue))
    End Sub

    <TestMethod()> <TestCategory("KeyValueContainsDateTimeValues")> <Description("Keyvalue contains datevalues")>
    Public Sub KeyValueContainsDateValues()
        Dim checkValue As BaseValue = New StringValue("12/13/2001")
        Dim checkValue2 As BaseValue = New StringValue("test stringwaarde")
        Dim keyVal As KeyValue = New KeyValue("testDomain", 1)
        keyVal.Values.Add(checkValue)
        keyVal.Values.Add(checkValue2)
        Assert.IsTrue(GapDateScoringHelper.ValueContainsDateValues(keyVal.Values))
    End Sub

    <TestMethod()> <TestCategory("KeyValueContainsDateTimeValues")> <Description("Keyvalue contains no datevalues")>
    Public Sub KeyValueContainsNoDateValues()
        Dim checkValue As BaseValue = New StringValue("het is vandaag 07/04/2013")
        Dim checkValue2 As BaseValue = New StringValue("test stringwaarde")
        Dim keyVal As KeyValue = New KeyValue("testDomain", 1)
        keyVal.Values.Add(checkValue)
        keyVal.Values.Add(checkValue2)
        Assert.IsFalse(GapDateScoringHelper.ValueContainsDateValues(keyVal.Values))
    End Sub

    <TestMethod()> <TestCategory("KeyValueContainsDateTimeValues")> <Description("Basevalue contains wrong date-format")>
    Public Sub BaseValueContainsWrongDateFormat()
        Dim checkValue As BaseValue = New StringValue("07/04/20/13")
        Assert.IsFalse(GapDateScoringHelper.BaseValueIsDateValue(checkValue))
    End Sub

    <TestMethod()> <TestCategory("KeyValueContainsDateTimeValues")> <Description("Basevalue contains invalid datevalues - day out of range")>
    Public Sub BaseValueContainsInvalidDateValuesWrongDay()
        Dim checkValue As BaseValue = New StringValue("07/32/2013")
        Assert.IsFalse(GapDateScoringHelper.BaseValueIsDateValue(checkValue))
    End Sub

    <TestMethod()> <TestCategory("KeyValueContainsDateTimeValues")> <Description("Basevalue contains invalid datevalues - month out of range")>
    Public Sub BaseValueContainsInvalidDateValuesWrongMonth()
        Dim checkValue As BaseValue = New StringValue("13/04/2013")
        Assert.IsFalse(GapDateScoringHelper.BaseValueIsDateValue(checkValue))
    End Sub

    <TestMethod()> <TestCategory("KeyValueContainsDateTimeValues")> <Description("Basevalue contains invalid datevalues - negative numbers")>
    Public Sub BaseValueContainsInvalidDateValuesNegative()
        Dim checkValue As BaseValue = New StringValue("07/-4/2013")
        Assert.IsFalse(GapDateScoringHelper.BaseValueIsDateValue(checkValue))
    End Sub

End Class
