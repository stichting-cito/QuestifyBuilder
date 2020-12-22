
Imports System.Xml.Linq
Imports Cito.Tester.ContentModel

<TestClass()>
Public Class IntegerParameterSerializationTest
    Inherits SerializationTestBase

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub TestSerializedResult()
        Dim p As New IntegerParameter() With {.Name = "int1", .Value = 3}
        Dim result As XElement = Nothing

        result = DoSerialize(Of IntegerParameter)(p)



        Assert.IsNotNull(result)
        Assert.AreEqual("int1", result.Attribute("name").Value)
        Assert.AreEqual("3", result.Value)
    End Sub

End Class
