
Imports Questify.Builder.Logic.HtmlHelpers
Imports Questify.Builder.Logic.ContentModel
Imports Questify.Builder.Logic.ItemProcessing
Imports Cito.Tester.ContentModel
Imports System.Xml.Linq
Imports System.IO
Imports Questify.Builder.UnitTests.Framework

<TestClass()>
Public Class ParameterMergerTest
    Inherits ItemProcessingBase

    <TestMethod(), TestCategory("ItemProcessing"), Ignore>
    <DeploymentItem("Cito.TestBuilder.Logic\ItemProcessing\ParameterMergerData.xlsx")>
    <DataSource("System.Data.OleDb", "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=ParameterMergerData.xlsx;Extended Properties=Excel 12.0;Persist Security Info=False", "DataTests$", DataAccessMethod.Sequential)>
    Public Sub DataDrivenMergetest()
        'Available data: newParamSet	CurrParamSet	Warnings	Errors	ExpectedResult	Description
        'Arrange
        Dim t As Type = GetType(ParameterSetCollection)
        Dim newPSet As ParameterSetCollection = GetSerializer(t).Deserialize(Of ParameterSetCollection)(Data("NewParamSet"))
        Dim curPSet As ParameterSetCollection = GetSerializer(t).Deserialize(Of ParameterSetCollection)(Data("CurrParamSet"))
        Dim result As ParameterSetCollection = GetSerializer(t).Deserialize(Of ParameterSetCollection)(Data("ExpectedResult"))
        Dim desc As String = Data("Description")
        Dim war As WarningsAndErrors

        'Act
        war = ParameterHandler.Merge(newPSet, curPSet)

        'Assert
        'Expected NR# Errors?
        Assert.AreEqual(GetInt("Errors"), war.ErrorList.Count, desc)
        'Expected NR# Warnings?
        Assert.AreEqual(GetInt("Warnings"), war.WarningList.Count, desc)

        'result == expected?
        Dim XResult As XDocument = XDocument.Parse(GetSerializer(t).Serialize(newPSet))
        Dim XExpected As XDocument = XDocument.Parse(GetSerializer(t).Serialize(result))

        Dim same As Boolean = XDocument.DeepEquals(XExpected, XResult) 'Compare xml!!!

        If Not same Then
            PerforceP4Merge(newPSet, result)
        End If

        Assert.IsTrue(same) 'Is the xml the same?
    End Sub

    <TestMethod(), TestCategory("ItemProcessing"), Ignore>
    <DeploymentItem("Cito.TestBuilder.Logic\ItemProcessing\ParameterMergerData.xlsx")>
    <DataSource("System.Data.OleDb", "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=ParameterMergerData.xlsx;Extended Properties=Excel 12.0;Persist Security Info=False", "DataTests$", DataAccessMethod.Sequential)>
    Public Sub DataDrivenMultipleMergetest()
        'Available data: newParamSet	CurrParamSet	Warnings	Errors	ExpectedResult	Description
        'Arrange
        Dim t As Type = GetType(ParameterSetCollection)
        Dim newPSet As ParameterSetCollection = GetSerializer(t).Deserialize(Of ParameterSetCollection)(Data("NewParamSet"))
        Dim curPSet As ParameterSetCollection = GetSerializer(t).Deserialize(Of ParameterSetCollection)(Data("CurrParamSet"))
        Dim war As WarningsAndErrors
        Dim nrOfCollectionParametersCurPSet As Integer = DirectCast(curPSet.Item(0).InnerParameters(14), CollectionParameter).Value.Count

        'Act
        war = ParameterHandler.Merge(newPSet, curPSet)
        war = ParameterHandler.Merge(newPSet, newPSet)   'Merge again to check that the nr of CollectionParameters stay the same.

        Dim newNrOfCollectionParametersNewPSet As Integer = DirectCast(newPSet.Item(0).InnerParameters(14), CollectionParameter).Value.Count

        Assert.IsTrue(newNrOfCollectionParametersNewPSet = nrOfCollectionParametersCurPSet)
    End Sub


    <TestMethod(), TestCategory("ItemProcessing"), Ignore>
    <DeploymentItem("Cito.TestBuilder.Logic\ItemProcessing\TemplateParameterMergerData.xlsx")>
    <DataSource("System.Data.OleDb", "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=TemplateParameterMergerData.xlsx;Extended Properties=Excel 12.0;Persist Security Info=False", "DataTests$", DataAccessMethod.Sequential)>
    Public Sub DataDrivenMergeInlineTemplatetest()
        'Available data: newParamSet	CurrParamSet	Warnings	Errors	ExpectedResult	Description
        'Arrange

        Dim parameterSet As ParameterSetCollection = GetSerializer(GetType(ParameterSetCollection)).Deserialize(Of ParameterSetCollection)(Data("newParameter"))
        Dim currentItem As AssessmentItem = GetSerializer(GetType(AssessmentItem)).Deserialize(Of AssessmentItem)(Data("CurrItem"))
        Dim result As AssessmentItem = GetSerializer(GetType(AssessmentItem)).Deserialize(Of AssessmentItem)(Data("ExpectedResult"))
        Dim desc As String = Data("Description")
        Dim template As String = Data("TemplateName")
        Dim war As WarningsAndErrors = Nothing

        'Act
        Dim xHtmlParameterList As New List(Of XHtmlParameter)
        For Each parameterCollection As ParameterCollection In currentItem.Parameters
            GetXhtmlParameters(xHtmlParameterList, parameterCollection.InnerParameters)
        Next

        Dim htmlInlineConverter As New HtmlInlineConverter(Nothing, XHtmlParameterExtensions.GetNamespaceManager(), Nothing, Nothing)
        Dim listOfInlineElements As Dictionary(Of XHtmlParameter, IEnumerable(Of InlineElement)) = htmlInlineConverter.GetInlineElementsOfTemplateFromXhtmlParameters(template, xHtmlParameterList)
        For Each xHtmlParameter As XHtmlParameter In listOfInlineElements.Keys
            For Each inlineElement As InlineElement In listOfInlineElements(xHtmlParameter)
                war = ParameterHandler.Merge(parameterSet, inlineElement.Parameters)
                inlineElement.Parameters.Clear()
                inlineElement.Parameters.AddRange(parameterSet)
                htmlInlineConverter.UpdateInlineElements(inlineElement, xHtmlParameter)
            Next
        Next

        'Assert
        'Expected NR# Errors?
        Assert.AreEqual(GetInt("Errors"), war.ErrorList.Count, desc)
        'Expected NR# Warnings?
        Assert.AreEqual(GetInt("Warnings"), war.WarningList.Count, desc)

        'result == expected?
        Dim XResult As XDocument = XDocument.Parse(GetSerializer(GetType(AssessmentItem)).Serialize(currentItem))
        Dim XExpected As XDocument = XDocument.Parse(GetSerializer(GetType(AssessmentItem)).Serialize(result))

        Dim same As Boolean = XDocument.DeepEquals(XExpected, XResult) 'Compare xml!!!

        If Not same Then
            PerforceP4Merge(currentItem, result)
        End If

        Assert.IsTrue(same) 'Is the xml the same?
    End Sub


    Private Sub PerforceP4Merge(ByVal currentItem As Object, ByVal result As Object)
        Dim compare As String = "C:\Program Files (x86)\Perforce\p4merge.exe"

        If (File.Exists(compare)) Then
            Dim tmpFile1 As String = Path.GetTempFileName()
            Dim tmpFile2 As String = Path.GetTempFileName()
            Dim splt As Char() = Environment.NewLine.ToCharArray()
            File.WriteAllLines(tmpFile1, GetSerializer(GetType(AssessmentItem)).Serialize(currentItem).Split(splt, StringSplitOptions.RemoveEmptyEntries))
            File.WriteAllLines(tmpFile2, GetSerializer(GetType(AssessmentItem)).Serialize(result).Split(splt, StringSplitOptions.RemoveEmptyEntries))

            System.Diagnostics.Process.Start(compare, String.Format(" ""{0}"" ""{1}"" ", tmpFile1, tmpFile2))
        End If
    End Sub

    ''' <summary>
    ''' Gets the XHTML parameters.
    ''' </summary>
    ''' <param name="xHtmlParameterList">The x HTML parameter list.</param>
    ''' <param name="parameterList">The parameter list.</param>
    Private Sub GetXhtmlParameters(ByRef xHtmlParameterList As List(Of XHtmlParameter), parameterList As ParameterList)
        For Each parameter As ParameterBase In parameterList
            If (TypeOf parameter Is XHtmlParameter AndAlso Not String.IsNullOrEmpty(DirectCast(parameter, XHtmlParameter).Value.Trim)) Then
                Dim xHtmlParameter As XHtmlParameter = DirectCast(parameter, XHtmlParameter)
                xHtmlParameterList.Add(xHtmlParameter)
            ElseIf (TypeOf parameter Is XhtmlResourceParameter AndAlso Not String.IsNullOrEmpty(DirectCast(parameter, XhtmlResourceParameter).Value.Trim)) Then
                'Give a warning the file cannot be loaded
            ElseIf TypeOf parameter Is CollectionParameter Then
                Dim collectionParameter As CollectionParameter = DirectCast(parameter, CollectionParameter)
                For Each paramaterCol As ParameterCollection In collectionParameter.Value
                    GetXhtmlParameters(xHtmlParameterList, paramaterCol.InnerParameters)
                Next
            End If
        Next
    End Sub

End Class
