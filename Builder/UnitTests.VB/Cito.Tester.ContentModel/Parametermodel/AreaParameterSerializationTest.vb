
Imports Cito.Tester.ContentModel
Imports System.Linq
Imports System.Drawing
Imports System.Xml.Linq
Imports Cito.Tester.Common

<TestClass()>
Public Class AreaParameterSerializationTest
    Inherits SerializationTestBase

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub TestSerializedResultWithShapes()
        'Arrange
        Dim p As New AreaParameter() With {.Name = "Area51"}
        p.ShapeList = Test_ShapeList()
        Dim result As XElement = Nothing
      
        'Act
        result = DoSerialize(Of AreaParameter)(p)
        
        'Assert
        Assert.IsNotNull(result)
        Assert.AreEqual("AreaParameter", result.Name.LocalName)
        Assert.AreEqual("Area51", result.Attribute("name").Value)
        Assert.AreEqual(1, result.Nodes().Count) 'This is the Shape Node
        Assert.AreEqual(p.ShapeList.Count, result.Element("Shapes").Nodes().Count())
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub TestSerializedResultWithResourceParameter()
        'Arrange
        Dim p As New AreaParameter() With {.Name = "Area51"}
        p.ShapeList = Test_ShapeList()
        p.BluePrint = New ParameterCollection() With {.Id = "prm1"}
        p.BluePrint.InnerParameters.Add(New ResourceParameter())

        Dim result As XElement = Nothing
      
        'Act
        result = DoSerialize(Of AreaParameter)(p)
       
        'Assert
        Assert.IsNotNull(result)
        Assert.AreEqual("AreaParameter", result.Name.LocalName)
        Assert.AreEqual(2, result.Nodes().Count) 'This is the Shape Node + subparameterset
        Assert.IsNotNull(result.Element("definition"))
    End Sub


    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub TestSerializedControlTemplateWithAreaParameter()
        'Arrange
        Dim ct As New ControlTemplate()
        ct.SharedParameterSet.InnerParameters.Add(New AreaParameter)

        Dim result As XElement = Nothing
    
        'Act
        result = DoSerialize(Of ControlTemplate)(ct)
    
        'Assert
        Assert.IsNotNull(result)
        Assert.IsNotNull(result.Element("SharedParameterSet").Element("areaparameter")) 'Simple test that areaparameter is part of...
    End Sub


    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub TestSerializedControlTemplateWithAreaParameterAndResourceParam()
        'Arrange
        Dim ct As New ControlTemplate()
        Dim ap As New AreaParameter
        ap.BluePrint = New ParameterCollection() With {.Id = "set1"}
        Dim rp = New ResourceParameter() With {.Name = "Picture", .Value = "rembrant.gif"}


        rp.DesignerSettings.Add(New DesignerSetting() With {.Key = "xyz", .Value = "123"})
        ap.BluePrint.InnerParameters.Add(rp)
        ct.SharedParameterSet.InnerParameters.Add(ap)

        Dim result As XElement = Nothing
      
        'Act
        result = DoSerialize(Of ControlTemplate)(ct)
      
        'Assert
        Assert.IsNotNull(result)
        Assert.IsNotNull(result.Element("SharedParameterSet").Element("areaparameter")) 'Simple test that areaparameter is part of...
    End Sub

    Private ap1 as XElement = <AreaParameter name="itemQuestionArea">
                                  <subparameterset id="A">
                                      <resourceparameter name="clickableImage" height="74" width="100" editSize="true">L_Z3_FR_Les_A0148_00013_Bild11.png</resourceparameter>
                                  </subparameterset>
                                  <definition id="">
                                      <resourceparameter name="clickableImage"/>
                                  </definition>
                                  <Shapes>
                                      <Rectangle id="A">
                                          <TopLeft>
                                              <X>9</X>
                                              <Y>12</Y>
                                          </TopLeft>
                                          <BottomRight>
                                              <X>147</X>
                                              <Y>90</Y>
                                          </BottomRight>
                                      </Rectangle>
                                  </Shapes>
                              </AreaParameter>

    <TestMethod()> <TestCategory("ContentModel")> 
    Public Sub SizeIsSerialisedCorrect()
        'Arrange

        'Act
        Dim area As AreaParameter = DirectCast(SerializeHelper.XmlDeserializeFromString(ap1.ToString(), GetType(AreaParameter)), AreaParameter)

        dim res = directCast(area.Value(0).InnerParameters.FirstOrDefault, ResourceParameter)
        
        'Assert
        Assert.AreEqual(100, res.Width)
        Assert.AreEqual(74, res.Height)
        Assert.AreEqual(True, res.EditSize)
    End Sub

    Private Function Test_ShapeList() As ShapeList
        'Creates a test list
        Dim ret As New ShapeList
        ret.Add(New RectangleShape With {.TopLeft = New Point(5, 7), .BottomRight = New Point(154, 208), .Identifier = "a"})
        ret.Add(New CircleShape() With {.AnchorPoint = New Point(140, 75), .Radius = 3, .Identifier = "b"})
        ret.Add(New EllipseShape() With {.AnchorPoint = New Point(140, 75), .VRadius = 3, .HRadius = 2, .Identifier = "b"})
        ret.Add(new PointDownTriangleShape With {.BaseLeft = new Point(10, 10), .BaseRight = new Point(30, 10), .Top = new Point(20, 30), .Identifier = "c"})
        ret.Add(new PointUpTriangleShape With {.BaseLeft = new Point(10, 30), .BaseRight = new Point(30, 30), .Top = new Point(20, 10), .Identifier = "c"})
        Return ret
    End Function

End Class
