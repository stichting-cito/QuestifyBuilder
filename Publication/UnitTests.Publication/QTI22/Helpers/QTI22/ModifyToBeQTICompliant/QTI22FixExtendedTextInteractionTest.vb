
Imports System.Xml
Imports Questify.Builder.Logic.Publication
Imports Questify.Builder.UnitTests.Framework

<TestClass()> Public Class QTI22FixExtendedTextInteractionTest

    <TestMethod, TestCategory("FACET Document Tweaks")>
    Public Sub MultipleExtendedText()
        Dim input As XElement = <root xmlns="http://www.imgsglobal.org/xsd/imsqti_v2p1"><p id="c1-id-11-2" style="margin-bottom: 0px;">tekst
    <extendedTextInteraction responseIdentifier="EI_9156a867-4d61-4095-81e2-2e730dc2a16f" id="IEI_9156a867-4d61-4095-81e2-2e730dc2a16f" expectedLines="3" expectedLength="200"/>
                                    <extendedTextInteraction responseIdentifier="EI_3579df55-10df-40fe-8e1d-35e9ff268892" id="IEI_3579df55-10df-40fe-8e1d-35e9ff268892" expectedLines="3" expectedLength="200"/>tekst2</p>
                                </root>

        Dim inputDocument As XmlDocument = input.ToXmlDocument()
        Dim expectedOutput = New XDocument(<?xml version="1.0" encoding="utf-8"?>
                                           <root xmlns="http://www.imgsglobal.org/xsd/imsqti_v2p1">
                                               <p id="c1-id-11-2" style="margin-bottom: 0px;margin-bottom: 0px;">tekst
    </p>
                                               <extendedTextInteraction responseIdentifier="EI_9156a867-4d61-4095-81e2-2e730dc2a16f" id="IEI_9156a867-4d61-4095-81e2-2e730dc2a16f" expectedLines="3" expectedLength="200"/>
                                               <p id="c1-id-11-2_copied_3dbdc5c2-5bee-4d12-b29d-89669512b400_0" style="margin-bottom: 0px;"/>
                                               <extendedTextInteraction responseIdentifier="EI_3579df55-10df-40fe-8e1d-35e9ff268892" id="IEI_3579df55-10df-40fe-8e1d-35e9ff268892" expectedLines="3" expectedLength="200"/>
                                               <p id="c1-id-11-2_copied_3dbdc5c2-5bee-4d12-b29d-89669512b400_1" style="margin-bottom: 0px;">tekst2</p>
                                           </root>)

        inputDocument.BringElementOutSide("extendedTextInteraction", "p", False, "3dbdc5c2-5bee-4d12-b29d-89669512b400")
        Dim output = XDocument.Parse(inputDocument.OuterXml)
        Assert.IsTrue(UnitTestHelper.AreSame(expectedOutput, output))
    End Sub

End Class