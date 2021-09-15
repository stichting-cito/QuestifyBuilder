
Imports System.Xml.Linq
Imports System.Xml
Imports Questify.Builder.Logic.PluginExtensibility.Html.Converters

<TestClass()>
Public Class HtmlConverter_RepairElementReferenceTests

    Private _sampleHtml As XElement = <html xmlns="http://www.w3.org/1999/xhtml"><body><p><span><u>DONOTCHANGE</u></span></p></body></html>
    Private _sampleHtmlWithReference As XElement = <html xmlns="http://www.w3.org/1999/xhtml"><body><p><span id="refda8ccb1e-b659-41ce-99fe-f9290e85c7df" contenteditable="false" cito_type="reference" cito_reftype="Element" cito_description="Element 1" cito_value="1"><u>    1    </u></span></p></body></html>
    Private _namespaceManager As XmlNamespaceManager = CreateNamespaceManager()

    <TestMethod(), TestCategory("UILogic")>
    Public Sub Move_U_tag_OutsideReferenceSpanElement()
        'Arrange
        Dim converter As New HtmlConverter_RepairElementReference(_namespaceManager)
        
        'Act
        Dim result As String = converter.ConvertHtml(_sampleHtmlWithReference.ToString())
        
        'Assert
        Assert.IsTrue(result.Contains("</span></u>") AndAlso result.Contains("<u><span"))
    End Sub

    <TestMethod(), TestCategory("UILogic")>
    Public Sub DONOT_Move_U_tag_OutsideSpanElement()
        'Arrange
        Dim converter As New HtmlConverter_RepairElementReference(_namespaceManager)
        
        'Act
        Dim result As String = converter.ConvertHtml(_sampleHtml.ToString())
       
        'Assert
        Assert.IsFalse(result.Contains("</span></u>") AndAlso result.Contains("<u><span"))
    End Sub

    Private Function CreateNamespaceManager() As XmlNamespaceManager
        Dim nsm As New Xml.XmlNamespaceManager(New System.Xml.NameTable())
        nsm.AddNamespace("def", "http://www.w3.org/1999/xhtml")
        nsm.AddNamespace("cito", "http://www.cito.nl/citotester")
        Return nsm
    End Function

End Class
