
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.QTI.Helpers.QTI_Base

Namespace QTI_Base

    <TestClass()>
    Public Class KeyValueContainsDateTimeValuesTest

#Region "TimeValueTests"
        <TestMethod()> <TestCategory("KeyValueContainsDateTimeValues")> <Description("Keyvalue contains timevalues")>
        Public Sub KeyValueContainsTimeValues()
            'Scoring for time-gaps is saved as string
            Dim checkValue As BaseValue = New StringValue("12:34:56")
            Dim checkValue2 As BaseValue = New StringValue("test stringwaarde")
            Dim keyVal As KeyValue = New KeyValue("testDomain", 1)
            keyVal.Values.Add(checkValue)
            keyVal.Values.Add(checkValue2)
            Assert.IsTrue(GapTimeScoringHelper.ValueContainsTimeValues(keyVal.Values))
        End Sub

        <TestMethod()> <TestCategory("KeyValueContainsDateTimeValues")> <Description("Keyvalue contains no timevalues")>
        Public Sub KeyValueContainsNoTimeValues()
            'Scoring for time-gaps is saved as string
            Dim checkValue As BaseValue = New StringValue("het is nu 16:55 uur")
            Dim checkValue2 As BaseValue = New StringValue("test stringwaarde")
            Dim keyVal As KeyValue = New KeyValue("testDomain", 1)
            keyVal.Values.Add(checkValue)
            keyVal.Values.Add(checkValue2)
            Assert.IsFalse(GapTimeScoringHelper.ValueContainsTimeValues(keyVal.Values))
        End Sub

        <TestMethod()> <TestCategory("KeyValueContainsDateTimeValues")> <Description("Basevalue contains wrong time-format")>
        Public Sub BaseValueContainsWrongTimeFormat()
            'Scoring for time-gaps is saved as string
            Dim checkValue As BaseValue = New StringValue("12:34:56:78")
            Assert.IsFalse(GapTimeScoringHelper.BaseValueIsTimeValue(checkValue))
        End Sub

        <TestMethod()> <TestCategory("KeyValueContainsDateTimeValues")> <Description("Basevalue contains invalid timevalues - hour out of range")>
        Public Sub BaseValueContainsInvalidTimeValuesWrongHour()
            'Scoring for time-gaps is saved as string
            Dim checkValue As BaseValue = New StringValue("24:12:34")
            Assert.IsFalse(GapTimeScoringHelper.BaseValueIsTimeValue(checkValue))
        End Sub

        <TestMethod()> <TestCategory("KeyValueContainsDateTimeValues")> <Description("Basevalue contains invalid timevalues - seconds out of range")>
        Public Sub BaseValueContainsInvalidTimeValuesWrongSeconds()
            'Scoring for time-gaps is saved as string
            Dim checkValue As BaseValue = New StringValue("12:34:60")
            Assert.IsFalse(GapTimeScoringHelper.BaseValueIsTimeValue(checkValue))
        End Sub

        <TestMethod()> <TestCategory("KeyValueContainsDateTimeValues")> <Description("Basevalue contains invalid timevalues - negative numbers")>
        Public Sub BaseValueContainsInvalidTimeValuesNegative()
            'Scoring for time-gaps is saved as string
            Dim checkValue As BaseValue = New StringValue("12:-4:56")
            Assert.IsFalse(GapTimeScoringHelper.BaseValueIsTimeValue(checkValue))
        End Sub
#End Region

#Region "DateValueTests"
        <TestMethod()> <TestCategory("KeyValueContainsDateTimeValues")> <Description("Keyvalue contains datevalues")>
        Public Sub KeyValueContainsDateValues()
            'Scoring for date-gaps is saved as string (mm/dd/yyyy)
            Dim checkValue As BaseValue = New StringValue("12/13/2001")
            Dim checkValue2 As BaseValue = New StringValue("test stringwaarde")
            Dim keyVal As KeyValue = New KeyValue("testDomain", 1)
            keyVal.Values.Add(checkValue)
            keyVal.Values.Add(checkValue2)
            Assert.IsTrue(GapDateScoringHelper.ValueContainsDateValues(keyVal.Values))
        End Sub

        <TestMethod()> <TestCategory("KeyValueContainsDateTimeValues")> <Description("Keyvalue contains no datevalues")>
        Public Sub KeyValueContainsNoDateValues()
            'Scoring for date-gaps is saved as string (mm/dd/yyyy)
            Dim checkValue As BaseValue = New StringValue("het is vandaag 07/04/2013")
            Dim checkValue2 As BaseValue = New StringValue("test stringwaarde")
            Dim keyVal As KeyValue = New KeyValue("testDomain", 1)
            keyVal.Values.Add(checkValue)
            keyVal.Values.Add(checkValue2)
            Assert.IsFalse(GapDateScoringHelper.ValueContainsDateValues(keyVal.Values))
        End Sub

        <TestMethod()> <TestCategory("KeyValueContainsDateTimeValues")> <Description("Basevalue contains wrong date-format")>
        Public Sub BaseValueContainsWrongDateFormat()
            'Scoring for date-gaps is saved as string (mm/dd/yyyy)
            Dim checkValue As BaseValue = New StringValue("07/04/20/13")
            Assert.IsFalse(GapDateScoringHelper.BaseValueIsDateValue(checkValue))
        End Sub

        <TestMethod()> <TestCategory("KeyValueContainsDateTimeValues")> <Description("Basevalue contains invalid datevalues - day out of range")>
        Public Sub BaseValueContainsInvalidDateValuesWrongDay()
            'Scoring for date-gaps is saved as string (mm/dd/yyyy)
            Dim checkValue As BaseValue = New StringValue("07/32/2013")
            Assert.IsFalse(GapDateScoringHelper.BaseValueIsDateValue(checkValue))
        End Sub

        <TestMethod()> <TestCategory("KeyValueContainsDateTimeValues")> <Description("Basevalue contains invalid datevalues - month out of range")>
        Public Sub BaseValueContainsInvalidDateValuesWrongMonth()
            'Scoring for date-gaps is saved as string (mm/dd/yyyy)
            Dim checkValue As BaseValue = New StringValue("13/04/2013")
            Assert.IsFalse(GapDateScoringHelper.BaseValueIsDateValue(checkValue))
        End Sub

        <TestMethod()> <TestCategory("KeyValueContainsDateTimeValues")> <Description("Basevalue contains invalid datevalues - negative numbers")>
        Public Sub BaseValueContainsInvalidDateValuesNegative()
            'Scoring for date-gaps is saved as string (mm/dd/yyyy)
            Dim checkValue As BaseValue = New StringValue("07/-4/2013")
            Assert.IsFalse(GapDateScoringHelper.BaseValueIsDateValue(checkValue))
        End Sub
#End Region

    End Class

End Namespace
