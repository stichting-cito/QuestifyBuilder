
Imports System.Xml.Linq
Imports Cito.Tester.ContentModel
Imports Cito.Tester.Common

<TestClass()>
Public Class ControlAdapterTest

    <TestMethod()> <TestCategory("ContentModel")> <ExpectedException(GetType(InteractionControlException))>
    Public Sub TryLoadWithoutResourceNeededEventSet()

        Dim n As XElement = <cito:control xmlns:cito="http://www.cito.nl/citotester" id="Test" type="Test.Control"/>

        Dim ct As ControlTemplate
        Dim adapter As New ControlAdapter(n.ToXmlElement(), Nothing)

        ct = adapter.GetControlAdaptertemplate()

    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub LoadSimpleControlTemplate()
        Dim control As XDocument = <?xml version="1.0" encoding="utf-8"?>
                                   <Template xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" definitionVersion="2">
                                       <Description></Description>
                                       <Targets>
                                           <Target xsi:type="ControlTemplateTarget" enabled="true" name="ces">
                                               <Template>
                                                   <![CDATA[	]]>
                                               </Template>
                                           </Target>
                                       </Targets>
                                       <SharedFunctions/>
                                       <SharedParameterSet>
                                           <booleanparameter name="bool1"/>
                                       </SharedParameterSet>
                                   </Template>

        Dim n As XElement = <cito:control xmlns:cito="http://www.cito.nl/citotester" id="Test" type="Test.Control"/>

        Dim ct As ControlTemplate
        Dim adapter As New ControlAdapter(n.ToXmlElement(), Nothing)

        Dim handler As EventHandler(Of ResourceNeededEventArgs) =
            Sub(o As Object, e As ResourceNeededEventArgs) e.BinaryResource = New BinaryResource(e.ResourceName, Nothing, control.ToString(), Nothing)
        AddHandler adapter.ResourceNeeded, handler

        ct = adapter.GetControlAdaptertemplate()

        Assert.AreEqual(1, ct.Targets.Count)
    End Sub


    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub LoadControlTemplateWithParametersAndDesignerParams()
        Dim control As XDocument = <?xml version="1.0" encoding="utf-8"?>
                                   <Template xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" definitionVersion="2">
                                       <Description></Description>
                                       <Targets>
                                           <Target xsi:type="ControlTemplateTarget" enabled="true" name="ces">
                                               <Template>
                                                   <![CDATA[	]]>
                                               </Template>
                                           </Target>
                                       </Targets>
                                       <SharedFunctions/>
                                       <SharedParameterSet>
                                           <booleanparameter name="bool1">
                                               <designersetting key="k1">v1</designersetting>
                                               <designersetting key="k1">v1</designersetting>
                                           </booleanparameter>
                                       </SharedParameterSet>
                                   </Template>

        Dim n As XElement = <cito:control xmlns:cito="http://www.cito.nl/citotester" id="Test" type="Test.Control"/>

        Dim ct As ControlTemplate
        Dim adapter As New ControlAdapter(n.ToXmlElement(), Nothing)

        Dim handler As EventHandler(Of ResourceNeededEventArgs) =
            Sub(o As Object, e As ResourceNeededEventArgs) e.BinaryResource = New BinaryResource(e.ResourceName, Nothing, control.ToString(), Nothing)
        AddHandler adapter.ResourceNeeded, handler

        ct = adapter.GetControlAdaptertemplate()

        Assert.AreEqual(1, ct.SharedParameterSet.InnerParameters.Count)
        Assert.AreEqual(0, ct.SharedParameterSet.InnerParameters(0).DesignerSettings.Count)
    End Sub



    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub LoadControlTemplateWithParametersAndAttributeReferences()
        Dim control As XDocument = <?xml version="1.0" encoding="utf-8"?>
                                   <Template xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" definitionVersion="2">
                                       <Description></Description>
                                       <Targets>
                                           <Target xsi:type="ControlTemplateTarget" enabled="true" name="ces">
                                               <Template>
                                                   <![CDATA[	]]>
                                               </Template>
                                           </Target>
                                       </Targets>
                                       <SharedFunctions/>
                                       <SharedParameterSet>
                                           <booleanparameter name="bool1">
                                               <attributereference name="n1">v1</attributereference>
                                               <attributereference name="n2">v2</attributereference>
                                           </booleanparameter>
                                       </SharedParameterSet>
                                   </Template>

        Dim n As XElement = <cito:control xmlns:cito="http://www.cito.nl/citotester" id="Test" type="Test.Control"/>

        Dim ct As ControlTemplate
        Dim adapter As New ControlAdapter(n.ToXmlElement(), Nothing)

        Dim handler As EventHandler(Of ResourceNeededEventArgs) =
            Sub(o As Object, e As ResourceNeededEventArgs) e.BinaryResource = New BinaryResource(e.ResourceName, Nothing, control.ToString(), Nothing)
        AddHandler adapter.ResourceNeeded, handler

        ct = adapter.GetControlAdaptertemplate()

        Assert.AreEqual(1, ct.SharedParameterSet.InnerParameters.Count)
        Assert.AreEqual(0, ct.SharedParameterSet.InnerParameters(0).AttributeReferences.Count)
    End Sub

End Class
