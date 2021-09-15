
Imports System.Xml.Linq
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel

<TestClass>
Public Class TemplateHelperTests

    <TestMethod()> <TestCategory("TemplateHelper")>
    Public Sub IsXHtmlParameterEmpty_StrongTagWithImage_IsNotEmpty()
        'Arrange
        Dim xhtml As XElement =
            <p id="c1-id-11" xmlns="http://www.w3.org/1999/xhtml">
                <strong id="c1-id-12">
                    <img src="resource://package/MFI_2015420_11_4__549.png" id="a089f7c2-9dbc-4b28-beb9-e5c381a32f9d" ismathmlimage="true" style="vertical-align:-8px;" alt=""/>
                </strong>
            </p>

        Dim prm As New XHtmlParameter()
        prm.Value = xhtml.ToString()

        'Act
        Dim result = TemplateHelper.IsXHtmlParameterEmpty(prm.Nodes)
        
        'Assert
        Assert.IsFalse(result)
    End Sub

    <TestMethod()> <TestCategory("TemplateHelper")>
    Public Sub IsXHtmlParameterEmpty_SpanTagWithImage_IsNotEmpty()
        'Arrange
        Dim xhtml As XElement =
            <p>
                <span class="UserSRCourier">
                    <img src="resource://package/MFI_2015420_11_4__549.png" id="a089f7c2-9dbc-4b28-beb9-e5c381a32f9d" ismathmlimage="true" style="vertical-align:-8px;" alt=""/>
                </span>
            </p>

        Dim prm As New XHtmlParameter()
        prm.Value = xhtml.ToString()

        'Act
        Dim result = TemplateHelper.IsXHtmlParameterEmpty(prm.Nodes)
       
        'Assert
        Assert.IsFalse(result)
    End Sub

    <TestMethod()> <TestCategory("TemplateHelper")>
    Public Sub IsXHtmlParameterEmpty_EmptyStrongTag_IsEmpty()
        'Arrange
        Dim xhtml As XElement =
            <p><strong></strong></p>

        Dim prm As New XHtmlParameter()
        prm.Value = xhtml.ToString()

        'Act
        Dim result = TemplateHelper.IsXHtmlParameterEmpty(prm.Nodes)
        
        'Assert
        Assert.IsTrue(result)
    End Sub

    <TestMethod()> <TestCategory("TemplateHelper")>
    Public Sub IsXHtmlParameterEmpty_SeveralEmptyTags_IsEmpty()
        'Arrange
        Dim xhtml As XElement =
            <p><strong><em><sup></sup></em></strong></p>

        Dim prm As New XHtmlParameter()
        prm.Value = xhtml.ToString()

        'Act
        Dim result = TemplateHelper.IsXHtmlParameterEmpty(prm.Nodes)
        
        'Assert
        Assert.IsTrue(result)
    End Sub

End Class
