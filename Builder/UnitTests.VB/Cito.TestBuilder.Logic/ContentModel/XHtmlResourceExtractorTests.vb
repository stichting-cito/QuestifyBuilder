
Imports System.Xml.Linq
Imports Cito.Tester.ContentModel
Imports Cito.Tester.Common
Imports Questify.Builder.Logic.ContentModel

<TestClass()>
Public Class XHtmlResourceExtractorTests

    <TestMethod(), TestCategory("Logic")>
    Public Sub ExtractResourcesFromProblematicParam()
        'Arrange
        Dim param As XHtmlParameter = DirectCast(SerializeHelper.XmlDeserializeFromString(testCase2.ToString(), GetType(XHtmlParameter)), XHtmlParameter)
        Dim tst As New XHtmlResourceExtractor(param)
        
        'Act
        Dim result As HashSet(Of String) = tst.ExtractResources()
        
        'Assert
        Assert.AreEqual(3, result.Count)
        Assert.IsTrue(result.Contains("InlineImageLayoutTemplate"))
        Assert.IsTrue(result.Contains("cultivator3.jpg"))
        Assert.IsTrue(result.Contains("schoffel2.jpg"))
    End Sub

    Private testCase2 As XElement = <XHtmlParameter name="text">
                                        Op de afbeeldingen staan twee gereedschappen.
<br id="c1-id-13" xmlns="http://www.w3.org/1999/xhtml"/>
                                        <br id="c1-id-14" xmlns="http://www.w3.org/1999/xhtml"/>
                                        <cito:InlineElement id="8b27bee8-5f36-4d2e-8992-19799a7aefda" layoutTemplateSourceName="InlineImageLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                            <cito:parameters>
                                                <cito:parameterSet id="entireItem">
                                                    <cito:resourceparameter name="source">
                                                        <cito:designersetting key="deletebuttonVisible">false<cito:listvalues/></cito:designersetting>
                                                        cultivator3.jpg</cito:resourceparameter>
                                                    <cito:plaintextparameter name="popupDescription">
                                                        <cito:designersetting key="conditionalEnabled">true<cito:listvalues/></cito:designersetting>
                                                        <cito:designersetting key="conditionalEnabledSwitchParameter">showPopup<cito:listvalues/></cito:designersetting>
                                                        <cito:designersetting key="conditionalEnabledWhenValue">True<cito:listvalues/></cito:designersetting>
                                                        <cito:designersetting key="label">Titel popup venster<cito:listvalues/></cito:designersetting>
                                                        <cito:designersetting key="description">Titel voor het popup venster.<cito:listvalues/></cito:designersetting>
                                                        <cito:designersetting key="group">Grote afbeelding<cito:listvalues/></cito:designersetting>
                                                        <cito:designersetting key="required">false<cito:listvalues/></cito:designersetting>
                                                        <cito:designersetting key="sortkey">3<cito:listvalues/></cito:designersetting>
                                                    </cito:plaintextparameter>
                                                    <cito:resourceparameter name="source">schoffel2.jpg</cito:resourceparameter>
                                                </cito:parameterSet>
                                            </cito:parameters>
                                        </cito:InlineElement>
                                    </XHtmlParameter>

End Class
