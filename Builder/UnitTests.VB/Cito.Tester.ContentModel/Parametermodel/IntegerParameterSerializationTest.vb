
Imports System.Xml.Linq
Imports Cito.Tester.ContentModel

<TestClass()>
Public Class IntegerParameterSerializationTest
    Inherits SerializationTestBase

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub TestSerializedResult()
        'Arrange
        Dim p As New IntegerParameter() With {.Name = "int1", .Value = 3}
        Dim result As XElement = Nothing
       
        'Act
        result = DoSerialize(Of IntegerParameter)(p)
       
        'Assert

        '[Expected Result:]
        '<?xml version="1.0" encoding="utf-16"?>
        '<IntegerParameter xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="int1">3</IntegerParameter>

        Assert.IsNotNull(result)
        Assert.AreEqual("int1", result.Attribute("name").Value)
        Assert.AreEqual("3", result.Value)
    End Sub

End Class
