
Option Infer On

Imports System.Xml
Imports Cito.Tester.ContentModel
Imports System.Drawing
Imports System.Xml.Linq
Imports Cito.Tester.Common

<TestClass()>
Public Class InlineElementImageConverterTests

    <TestMethod()> <TestCategory("InlineElementImageConverter")>
    Public Sub InlineElement_NoWhidthHeightParamsPresent_DoNotTakeOriginalImageDimensions()
        Dim inlineImgConverter As New InlineElement.InlineElementImageConverter(GetNamespaceManager(), "resource://", True)
        Dim inlineElmnt As New InlineElement With {.Identifier = "UnitTestID_123"}
        inlineElmnt.Parameters.Add(New ParameterCollection)
        inlineElmnt.Parameters(0).InnerParameters.Add(New ResourceParameter() With {.Value = "TestPICA.jpg", .Name = "source"})
        inlineElmnt.Parameters(0).InnerParameters.Add(New BooleanParameter() With {.Value = False, .Name = "editSize"})

        Dim _imgElement As XElement = <img src="http://www.eendomain.com/TestPICA.jpg" id="" alt=""/>
        Dim imgElement As XmlElement = _imgElement.ToXmlElement()

        inlineImgConverter.ConvertInlineElementLayoutToHtmlBasedOnOldItemLayout(imgElement, inlineElmnt)

        Assert.IsTrue(imgElement.OuterXml.Contains("resource://TestPICA.jpg"))
        Assert.IsTrue(imgElement.OuterXml.Contains("id=""UnitTestID_123"""))
    End Sub

    <TestMethod(), TestCategory("InlineElementImageConverter")>
    Public Sub InlineElement_WidthHeightParamsPresent_TakeOriginalImageDimensions()
        Dim inlineImgConverter As New InlineElement.InlineElementImageConverter(GetNamespaceManager(), "resource://", True)
        Dim inlineElmnt As New InlineElement With {.Identifier = "UnitTestID_123"}
        inlineElmnt.Parameters.Add(New ParameterCollection)
        inlineElmnt.Parameters(0).InnerParameters.Add(New ResourceParameter() With {.Value = "TestPICA.jpg", .Name = "source", .Width = 1000, .Height = 2000, .EditSize = False})

        Dim evnt = Sub(s As Object, e As ResourceNeededEventArgs)
                       If (e.ResourceName = "TestPICA.jpg") Then
                           Dim br As New BinaryResource(New ImageConverter().ConvertTo(New Bitmap(1000, 2000), GetType(Byte())))
                           e.BinaryResource = br
                       End If
                   End Sub
        AddHandler TestSessionContext.ResourceNeeded, evnt

        Dim _imgElement As XElement = <img src="http://www.eendomain.com/TestPICA.jpg" id="" alt=""/>
        Dim imgElement As XmlElement = _imgElement.ToXmlElement()

        inlineImgConverter.ConvertInlineElementLayoutToHtmlBasedOnOldItemLayout(imgElement, inlineElmnt)

        RemoveHandler TestSessionContext.ResourceNeeded, evnt
        Assert.IsTrue(imgElement.OuterXml.Contains("width=""1000"""))
        Assert.IsTrue(imgElement.OuterXml.Contains("height=""2000"""))
    End Sub

    <TestMethod(), TestCategory("InlineElementImageConverter")>
    Public Sub InlineElement_WidthHeightParamsPresent_EditSizeIsTrue_DoNotTakeOriginalImageDimensions()
        Dim inlineImgConverter As New InlineElement.InlineElementImageConverter(GetNamespaceManager(), "resource://", True)
        Dim inlineElmnt As New InlineElement With {.Identifier = "UnitTestID_123"}
        inlineElmnt.Parameters.Add(New ParameterCollection)
        inlineElmnt.Parameters(0).InnerParameters.Add(New ResourceParameter() With {.Value = "TestPICA.jpg", .Name = "source"})
        inlineElmnt.Parameters(0).InnerParameters.Add(New IntegerParameter() With {.Value = 1000, .Name = "width"})
        inlineElmnt.Parameters(0).InnerParameters.Add(New IntegerParameter() With {.Value = 2000, .Name = "height"})
        inlineElmnt.Parameters(0).InnerParameters.Add(New BooleanParameter() With {.Value = True, .Name = "editSize"})

        Dim evnt = Sub(s As Object, e As ResourceNeededEventArgs)
                       Assert.Fail()
                   End Sub
        AddHandler TestSessionContext.ResourceNeeded, evnt

        Dim _imgElement As XElement = <img src="http://www.eendomain.com/TestPICA.jpg" width="1000" height="2000" id="" alt=""/>
        Dim imgElement As XmlElement = _imgElement.ToXmlElement()

        inlineImgConverter.ConvertInlineElementLayoutToHtmlBasedOnOldItemLayout(imgElement, inlineElmnt)

        RemoveHandler TestSessionContext.ResourceNeeded, evnt
        Assert.IsTrue(imgElement.OuterXml.Contains("width=""1000"""))
        Assert.IsTrue(imgElement.OuterXml.Contains("height=""2000"""))
    End Sub

    <TestMethod(), TestCategory("InlineElementImageConverter")>
    Public Sub InlineElement_WidthHeightParamsPresent_EditSizeIsTrue_TakeUserSettings()
        Dim inlineImgConverter As New InlineElement.InlineElementImageConverter(GetNamespaceManager(), "resource://", True)
        Dim inlineElmnt As New InlineElement With {.Identifier = "UnitTestID_123"}
        inlineElmnt.Parameters.Add(New ParameterCollection)
        inlineElmnt.Parameters(0).InnerParameters.Add(New ResourceParameter() With {.Value = "TestPICA.jpg", .Name = "source", .EditSize = True, .Width = 100, .Height = 200})

        Dim _imgElement As XElement = <img src="http://www.eendomain.com/TestPICA.jpg" width="1000" height="2000" id="" alt=""/>
        Dim imgElement As XmlElement = _imgElement.ToXmlElement()

        inlineImgConverter.ConvertInlineElementLayoutToHtmlBasedOnOldItemLayout(imgElement, inlineElmnt)

        Assert.IsTrue(imgElement.OuterXml.Contains("resource://"))
        Assert.IsTrue(imgElement.OuterXml.Contains("width=""100"""))
        Assert.IsTrue(imgElement.OuterXml.Contains("height=""200"""))
    End Sub

    Private Shared Function GetNamespaceManager() As XmlNamespaceManager
        Dim nsMgr As XmlNamespaceManager = New XmlNamespaceManager(New NameTable)
        nsMgr.AddNamespace("xhtml", "http://www.w3.org/1999/xhtml")
        Return nsMgr
    End Function

End Class
