
Imports Cito.Tester.ContentModel
Imports Questify.Builder.UnitTests.Framework.Faketory

<TestClass()>
Public Class ItemReferenceSerialisationTests

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub BinarySerializeTest()
        'Arrange
        Dim toSerialize As ItemReference2
        Dim assessment As AssessmentTest2
        'Very basic Assessment 
        assessment = FakeFactory.AssesmentTest.MakeAssessment(Function(s) s.MakeSection("s1",
                                                                                       Function(si) si.MakeItem("1001", Sub(i) toSerialize = i)
                                                                                           ))

        Dim formatter As New Runtime.Serialization.Formatters.Binary.BinaryFormatter
        Dim stream As New IO.MemoryStream
        'Act
        formatter.Serialize(stream, toSerialize)
        
        'Assert
        'Should not throw exception.
    End Sub

End Class
