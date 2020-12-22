Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports System.Data
Imports System.Xml.Serialization
Imports System.Xml
Imports System.IO
Imports Cito.TestBuilder.Service.Direct
Imports CustomClasses

<TestClass()>
Public Class DataSetToBanklStatisticsTest

#Region "How to create Dataset"
    '[How I Create dataset]
    'changed method in "RetrievalProcedures.vb" to: 
    '
    'Public Shared Function GetBankStatistics(bankId As System.Int32, userName As System.String, adapter As DataAccessAdapter) As DataSet
    '    Dim parameters() As SqlParameter = New SqlParameter(2 - 1) {}
    '    parameters(0) = New SqlParameter("@bankId", SqlDbType.Int, 0, ParameterDirection.Input, True, 10, 0, "", DataRowVersion.Current, bankId)
    '    parameters(1) = New SqlParameter("@userName", SqlDbType.VarChar, 50, ParameterDirection.Input, True, 0, 0, "", DataRowVersion.Current, userName)

    '    Dim toReturn As DataSet = New DataSet("GetBankStatistics")
    '    Dim hasSucceeded As Boolean = adapter.CallRetrievalStoredProcedure("[TestBuilder].[dbo].[GetBankStatistics]", parameters, toReturn)

    '    Dim a As XmlSerializer = New XmlSerializer(GetType(DataSet)) 
    '    Dim xmlS As XmlWriter = XmlWriter.Create(String.Format("c:\tmp\dataset_[{1}]_{0}.txt", Environment.TickCount, bankId))
    '    a.Serialize(xmlS, toReturn)
    '    xmlS.Close()

    '    Return toReturn
    'End Function
#End Region

    <TestMethod(), Owner("unknown")>
    Public Sub ConvertSavedDataSet_VerifyWithExpectedResults()
        'Arrange
        Dim ds As DataSet = GetDataSetFrom(My.Resources.SerializedDataSets.SomeDataSet)
        Dim converter As New DataSetToBanklStatistics
        'Act
        Dim result As BankStatistics = converter.Convert(ds)
        'Assert
        '-Items
        Assert.AreEqual(19, result.TotalNumberOfItems)
        Assert.AreEqual(2, result.NumberOfUnusedItems)
        Assert.AreEqual(19, result.NumberOfItemsCreatedByMe)
        '-Toetsen
        Assert.AreEqual(8, result.TotalNumberOfTest)
        Assert.AreEqual(8, result.NumberOfTestCreatedByMe)
        '-Media
        Assert.AreEqual(15, result.TotalNumberOfMedia)
        Assert.AreEqual(0, result.NumberOfUnusedMedia)
        '-Toets Template
        Assert.AreEqual(2, result.TotalNumberOfTestTemplates)
        Assert.AreEqual(2, result.NumberOfTestTemplatesCreatedByMe)
        '-Item Template
        Assert.AreEqual(7, result.TotalNumberOfItemTemplates)
        Assert.AreEqual(0, result.NumberOfUnusedItemsTemplates)
        Assert.AreEqual(7, result.NumberOfItemTemplatesCreatedByMe)
        '-Control Templates
        Assert.AreEqual(8, result.TotalNumberOfControlTemplates)
        Assert.AreEqual(0, result.NumberOfUnusedControlTemplates)
        Assert.AreEqual(8, result.NumberOfControlTemplatesCreatedByMe)
        '-Plugin
        Assert.AreEqual(8, result.TotalNumberOfPlugins)
        Assert.AreEqual(0, result.NumberOfUnusedPlugins)
        Assert.AreEqual(8, result.NumberOfPluginsCreatedByMe)

        '--Collections
        Assert.AreEqual(19, result.LastModifiedItems.Count)

    End Sub

    Private Function GetDataSetFrom(serializedDataset As String) As DataSet
        Dim ser As New XmlSerializer(GetType(DataSet))
        Dim ret As DataSet = Nothing
        Using s = XmlReader.Create(New StringReader(serializedDataset))
            ret = DirectCast(ser.Deserialize(s), DataSet)
        End Using
        Return ret
    End Function

End Class
