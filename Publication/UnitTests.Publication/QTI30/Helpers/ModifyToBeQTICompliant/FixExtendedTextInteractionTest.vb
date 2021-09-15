
Imports System.Xml
Imports Questify.Builder.Logic.Publication
Imports Questify.Builder.UnitTests.Framework

Namespace QTI30.Helpers

    <TestClass()>
    Public Class FixExtendedTextInteractionTest

        <TestMethod, TestCategory("FACET Document Tweaks")>
        Public Sub MultipleExtendedText()
            'Arrange
            Dim input As XElement = <root xmlns="http://www.imgsglobal.org/xsd/imsqti_v2p1"><p id="c1-id-11-2" style="margin-bottom: 0px;">tekst
    <qti-extended-text-interaction response-identifier="EI_9156a867-4d61-4095-81e2-2e730dc2a16f" id="IEI_9156a867-4d61-4095-81e2-2e730dc2a16f" expected-lines="3" expected-length="200"/>
                                        <qti-extended-text-interaction response-identifier="EI_3579df55-10df-40fe-8e1d-35e9ff268892" id="IEI_3579df55-10df-40fe-8e1d-35e9ff268892" expected-lines="3" expected-length="200"/>tekst2</p>
                                    </root>

            Dim inputDocument As XmlDocument = input.ToXmlDocument()
            Dim expectedOutput = New XDocument(<?xml version="1.0" encoding="utf-8"?>
                                               <root xmlns="http://www.imgsglobal.org/xsd/imsqti_v2p1">
                                                   <p id="c1-id-11-2" style="margin-bottom: 0px;margin-bottom: 0px;">tekst
    </p>
                                                   <qti-extended-text-interaction response-identifier="EI_9156a867-4d61-4095-81e2-2e730dc2a16f" id="IEI_9156a867-4d61-4095-81e2-2e730dc2a16f" expected-lines="3" expected-length="200"/>
                                                   <p id="c1-id-11-2_copied_3dbdc5c2-5bee-4d12-b29d-89669512b400_0" style="margin-bottom: 0px;"/>
                                                   <qti-extended-text-interaction response-identifier="EI_3579df55-10df-40fe-8e1d-35e9ff268892" id="IEI_3579df55-10df-40fe-8e1d-35e9ff268892" expected-lines="3" expected-length="200"/>
                                                   <p id="c1-id-11-2_copied_3dbdc5c2-5bee-4d12-b29d-89669512b400_1" style="margin-bottom: 0px;">tekst2</p>
                                               </root>)

            ' Act
            inputDocument.BringElementOutSide("qti-extended-text-interaction", "p", False, "3dbdc5c2-5bee-4d12-b29d-89669512b400")
            ' Assert
            Dim output = XDocument.Parse(inputDocument.OuterXml)
            Assert.IsTrue(UnitTestHelper.AreSame(expectedOutput, output))
        End Sub

    End Class

End Namespace
