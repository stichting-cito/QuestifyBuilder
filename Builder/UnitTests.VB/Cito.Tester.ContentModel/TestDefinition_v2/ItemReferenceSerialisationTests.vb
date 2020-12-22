
Imports Cito.Tester.ContentModel
Imports Questify.Builder.UnitTests.Framework.Faketory

<TestClass()>
Public Class ItemReferenceSerialisationTests

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub BinarySerializeTest()
        Dim toSerialize As ItemReference2
        Dim assessment As AssessmentTest2
        assessment = FakeFactory.AssesmentTest.MakeAssessment(Function(s) s.MakeSection("s1",
                                                                               Function(si) si.MakeItem("1001", Sub(i) toSerialize = i)
                                                                                   ))

        Dim formatter As New Runtime.Serialization.Formatters.Binary.BinaryFormatter
        Dim stream As New IO.MemoryStream
        formatter.Serialize(stream, toSerialize)

    End Sub

End Class
