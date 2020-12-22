
Imports Cito.Tester.ContentModel
Imports Cito.Tester.Common
Imports System.Linq
Imports System.Xml.Linq

<TestClass()>
Public Class ResourceParameterTests

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub DeserializeResourceParameterShoudlNotContainDesignerSettings()

        Dim res As ResourceParameter = DirectCast(SerializeHelper.XmlDeserializeFromString(data.ToString(), GetType(ResourceParameter)), ResourceParameter)

        Assert.IsFalse(res.Value.Contains("<designersetting"))
    End Sub

    <TestMethod()> <TestCategory("ContentModel")> <WorkItem(9658)>
    Public Sub DeserializeResourceWithDesignerSettings_WillLooseThoseWhenValueIsSet()
        Dim res As ResourceParameter = DirectCast(SerializeHelper.XmlDeserializeFromString(data.ToString(), GetType(ResourceParameter)), ResourceParameter)

        res.Value = "aaa.png"

        Assert.IsFalse(res.Value.Contains("<designersetting"))
        Assert.IsFalse(res.Nodes.Any(Function(p) p.Name.Equals("designersetting", StringComparison.InvariantCultureIgnoreCase)))
    End Sub


    Private data As XElement = <ResourceParameter>
                                   <designersetting key="label">Algemeen tekstveld<listvalues/></designersetting>
                                   <designersetting key="group">1 Algemeen tekstveld<listvalues/></designersetting>
                                   InlineElement_20121018_7_57_21_843_0.gif</ResourceParameter>


    Private rp1 As XElement = <ResourceParameter name="clickableImage" height="75" width="100" editSize="true">L_Z3_FR_Les_A0148_00013_Bild11.png</ResourceParameter>

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub SizeIsSerialisedCorrect()

        Dim res As ResourceParameter = DirectCast(SerializeHelper.XmlDeserializeFromString(rp1.ToString(), GetType(ResourceParameter)), ResourceParameter)

        Assert.AreEqual(100, res.Width)
        Assert.AreEqual(75, res.Height)
        Assert.AreEqual(True, res.EditSize)
    End Sub

End Class
