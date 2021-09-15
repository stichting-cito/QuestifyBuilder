
Imports System.Xml
Imports Questify.Builder.Logic.Publication
Imports Questify.Builder.Logic.QTI.Helpers.QTI22
Imports Questify.Builder.Logic.QTI.Helpers.QTI_Base.QTICompliantHelper
Imports Questify.Builder.UnitTests.Framework
Imports Questify.Builder.Logic.QTI.Helpers.QTI_Base.QTICompliantHelper.Interfaces
Imports Questify.Builder.Logic.QTI.Helpers.QTI22.QTICompliantHelper

Namespace QTI22.Helpers
    <TestClass()>
    Public Class QTI22DocumentHelperTests

        ''' <summary>
        ''' Elements with _copied suffix should be ignored. Those elements previously led to an infinite loop in FixNodes.
        ''' </summary>
        <TestMethod, TestCategory("FACET Document Tweaks")>
        Public Sub CopiedElementIsProcessed()
            'Arrange
            Dim input As XElement = <root xmlns="http://www.imgsglobal.org/xsd/imsqti_v2p1"><p id="c1-id-14_copied">
                                        <br id="c1-id-15"/>omdat <extendedTextInteraction responseIdentifier="Id24e3adf-46d5-4dc6-835a-00191c31e12e" id="IId24e3adf-46d5-4dc6-835a-00191c31e12e" expectedLines="4" expectedLength="240"/>
                                        </p>
                                    </root>

            Dim inputDocument As XmlDocument = input.ToXmlDocument()
            inputDocument.BringElementOutSide("extendedTextInteraction", "p", False, "e45cb5de-11ee-404a-9b31-490b82a17b2e")

            Dim expectedOutput = New XDocument(<root xmlns="http://www.imgsglobal.org/xsd/imsqti_v2p1">
                                                   <p id="c1-id-14_copied" style="margin-bottom: 0px;">
                                                       <br id="c1-id-15"/>omdat </p>
                                                   <extendedTextInteraction responseIdentifier="Id24e3adf-46d5-4dc6-835a-00191c31e12e" id="IId24e3adf-46d5-4dc6-835a-00191c31e12e" expectedLines="4" expectedLength="240"/>
                                                   <p id="c1-id-14_copied_copied_e45cb5de-11ee-404a-9b31-490b82a17b2e_0"/>
                                               </root>)

            ' Act
            Dim docHelper = New QTI22DocumentHelper()
            docHelper.ModifyXmlDocument(Of IModifyItemDocument)(inputDocument)
            Dim output = New XDocument(XElement.Parse(inputDocument.OuterXml))

            ' Assert
            Assert.IsTrue(UnitTestHelper.AreSame(expectedOutput, output))
        End Sub

        <TestMethod(), TestCategory("FACET Document Tweaks")>
        Public Sub PlaceMediaInterActionOutsideParagraphTest()
            'arrange
            Dim input As XElement = <root xmlns="http://www.imsglobal.org/xsd/imsqti_v2p1">
                                        <p>test<mediaInteraction responseIdentifier="AUDIORESPONSE1">audio01.mp3</mediaInteraction>test<mediaInteraction responseIdentifier="AUDIORESPONSE2">audio02.mp3</mediaInteraction>test</p>
                                    </root>
            Dim inputDocument = input.ToXmlDocument()
            Dim mediaFixer As New FixInteractions
            Dim expectedoutput = New XDocument(<root xmlns="http://www.imsglobal.org/xsd/imsqti_v2p1">
                                                   <br/><span xmlns="http://www.imsglobal.org/xsd/imsqti_v2p1">test</span><mediaInteraction responseIdentifier="AUDIORESPONSE1">audio01.mp3</mediaInteraction><span>test</span><mediaInteraction responseIdentifier="AUDIORESPONSE2">audio02.mp3</mediaInteraction><span>test</span>
                                                   <br/>
                                               </root>)
            'act
            mediaFixer.Modify(inputDocument, New QTI22DocumentHelper())
            'assert
            Dim output = New XDocument(XElement.Parse(inputDocument.OuterXml))
            Assert.IsTrue(UnitTestHelper.AreSame(expectedoutput, output))
        End Sub

        <TestMethod(), TestCategory("FACET Document Tweaks")>
        Public Sub PlaceDivOutsideParagraphAndCopyStyleTest()
            'arrange
            Dim input As XElement = <root xmlns="http://www.imsglobal.org/xsd/imsqti_v2p1">
                                        <p style="someStyle">
                                            <div>
                                                <mediaInteraction responseIdentifier="AUDIORESPONSE1">audio01.mp3</mediaInteraction>
                                            </div>
                                        </p>
                                    </root>
            Dim inputDocument = input.ToXmlDocument()
            Dim mediaFixer As New FixDiv
            Dim expectedoutput = New XDocument(<root xmlns="http://www.imsglobal.org/xsd/imsqti_v2p1">
                                                   <p style="someStylemargin-bottom: 0px;"/>
                                                   <div style="someStyle">
                                                       <mediaInteraction responseIdentifier="AUDIORESPONSE1">audio01.mp3</mediaInteraction>
                                                   </div>
                                                   <p style="someStyle"/>
                                               </root>)
            'act
            mediaFixer.Modify(inputDocument, New QTI22DocumentHelper())
            'assert
            Dim output = New XDocument(XElement.Parse(inputDocument.OuterXml))
            Assert.IsTrue(UnitTestHelper.AreSame(expectedoutput, output))
        End Sub

        <TestMethod(), TestCategory("FACET Document Tweaks")>
        Public Sub PlacePopupOutsideSpanTest()
            'arrange
            Dim input As XElement = <root xmlns="http://www.imsglobal.org/xsd/imsqti_v2p1">
                                        <span class="UserStyle">test<div id="qtiWindowContent"><img id="IMG_840bf09e-9559-40f6-a4b7-090642f9f26e" src="img/clock.jpg" width="804" height="800" alt=""/></div>test</span>
                                    </root>
            Dim inputDocument = input.ToXmlDocument()
            Dim expectedoutput = New XDocument(<root xmlns="http://www.imsglobal.org/xsd/imsqti_v2p1">
                                                   <span class="UserStyle" xmlns="http://www.imsglobal.org/xsd/imsqti_v2p1">test</span>
                                                   <div id="qtiWindowContent"><img id="IMG_840bf09e-9559-40f6-a4b7-090642f9f26e" src="img/clock.jpg" width="804" height="800" alt=""/></div><span class="UserStyle" xmlns="http://www.imsglobal.org/xsd/imsqti_v2p1">test</span>
                                               </root>)
            Dim popupFixer As New FixDiv
            'act
            popupFixer.Modify(inputDocument, New QTI22DocumentHelper())
            'assert
            Dim output = New XDocument(XElement.Parse(inputDocument.OuterXml))
            Assert.IsTrue(UnitTestHelper.AreSame(expectedoutput, output))
        End Sub

        <TestMethod(), TestCategory("FACET Document Tweaks")>
        Public Sub PlacePopupOutsideParagraphTest()
            'arrange
            Dim input As XElement = <root xmlns="http://www.imsglobal.org/xsd/imsqti_v2p1">
                                        <p id="c1-id-11">Klik op deze knop voor de situatiebeschrijving.<br id="c1-id-12"/><br id="c1-id-13"/>
                                            <div id="QTIWCT_f2427abf-3afb-4d7c-969f-7aba84aae98c">
                                                <img id="IMG_cfe779d4-aec9-4375-bc7c-c83b780f5189" src="resource://package:1/NEKB12_B1e_33_Apenland_casus-nieuw.gif" width="435" height="640" alt="" isinlineelement="true"/>
                                            </div>
                                        </p>
                                    </root>

            Dim inputDocument = input.ToXmlDocument()
            Dim expectedoutput = New XDocument(<root xmlns="http://www.imsglobal.org/xsd/imsqti_v2p1">
                                                   <p id="c1-id-11" style="margin-bottom: 0px;">Klik op deze knop voor de situatiebeschrijving.<br id="c1-id-12"/></p>
                                                   <div id="QTIWCT_f2427abf-3afb-4d7c-969f-7aba84aae98c">
                                                       <img id="IMG_cfe779d4-aec9-4375-bc7c-c83b780f5189" src="resource://package:1/NEKB12_B1e_33_Apenland_casus-nieuw.gif" width="435" height="640" alt="" isinlineelement="true"/>
                                                   </div>
                                                   <p id="c1-id-11_copied_cfe779d4-aec9-4375-bc7c-c83b780f5189_0"/>
                                               </root>)
            Dim popupFixer As New FixDiv
            'act
            popupFixer.Modify(inputDocument, "cfe779d4-aec9-4375-bc7c-c83b780f5189", New QTI22DocumentHelper())
            'assert
            Dim output = New XDocument(XElement.Parse(inputDocument.OuterXml))
            Assert.IsTrue(UnitTestHelper.AreSame(expectedoutput, output))
        End Sub

        <TestMethod(), TestCategory("FACET Document Tweaks")>
        Public Sub PlaceMediaInteractionOutsideSpanTest()
            'arrange
            Dim input As XElement = <root xmlns="http://www.imsglobal.org/xsd/imsqti_v2p1"><span class="UserStyle">test<mediaInteraction responseIdentifier="AUDIORESPONSE1">audio01.mp3</mediaInteraction>test<mediaInteraction responseIdentifier="AUDIORESPONSE2">audio02.mp3</mediaInteraction>test</span></root>
            Dim inputDocument = input.ToXmlDocument()
            Dim mediaFixer As New FixInteractions
            Dim expectedoutput = New XDocument(<root xmlns="http://www.imsglobal.org/xsd/imsqti_v2p1">
                                                   <span class="UserStyle" xmlns="http://www.imsglobal.org/xsd/imsqti_v2p1">test</span><mediaInteraction responseIdentifier="AUDIORESPONSE1">audio01.mp3</mediaInteraction><span class="UserStyle" xmlns="http://www.imsglobal.org/xsd/imsqti_v2p1">test</span><mediaInteraction responseIdentifier="AUDIORESPONSE2">audio02.mp3</mediaInteraction><span class="UserStyle" xmlns="http://www.imsglobal.org/xsd/imsqti_v2p1">test</span>
                                               </root>)
            'act
            mediaFixer.Modify(inputDocument, New QTI22DocumentHelper())

            'assert
            Dim output = New XDocument(XElement.Parse(inputDocument.OuterXml))
            Assert.IsTrue(UnitTestHelper.AreSame(expectedoutput, output))
        End Sub

        <TestMethod(), TestCategory("FACET Document Tweaks")>
        Public Sub PlaceMediaInteractionIntoDivAndCopyStyleTest()
            'arrange
            Dim input As XElement = <root xmlns="http://www.imsglobal.org/xsd/imsqti_v2p1">
                                        <p id="c1-id-10" style="text-align:center;">
                                            <mediaInteraction class="questify_videoWrapper autoNavigation hideToolbar pauseBefore_15 pauseAfter_7" id="I74354353-66f6-4fbe-8dcb-de23e4e8af87" responseIdentifier="VIDEORESPONSE" autostart="false" maxPlays="1">
                                                <object type="video/webm" data="resource://package:1/BKEMd10060.webm" height="270" width="480"/>
                                            </mediaInteraction>
                                        </p>
                                    </root>

            Dim inputDocument = input.ToXmlDocument()
            Dim mediaFixer As New FixInteractions
            Dim expectedoutput = New XDocument(<root xmlns="http://www.imsglobal.org/xsd/imsqti_v2p1">
                                                   <br/>
                                                   <span id="c1-id-10" style="text-align:center;"/>
                                                   <div style="text-align:center;">
                                                       <mediaInteraction class="questify_videoWrapper autoNavigation hideToolbar pauseBefore_15 pauseAfter_7" id="I74354353-66f6-4fbe-8dcb-de23e4e8af87" responseIdentifier="VIDEORESPONSE" autostart="false" maxPlays="1">
                                                           <object type="video/webm" data="resource://package:1/BKEMd10060.webm" height="270" width="480"/>
                                                       </mediaInteraction>
                                                   </div>
                                                   <span id="c1-id-10_copied_13d4fd61-b89d-4117-bbe4-a6fa7afe8c03_0" style="text-align:center;"/>
                                                   <br/>
                                               </root>)
            'act
            mediaFixer.Modify(inputDocument, "13d4fd61-b89d-4117-bbe4-a6fa7afe8c03", New QTI22DocumentHelper())

            'assert
            Dim output = New XDocument(XElement.Parse(inputDocument.OuterXml))
            Assert.IsTrue(UnitTestHelper.AreSame(expectedoutput, output))
        End Sub

        <TestMethod(), TestCategory("FACET Document Tweaks")>
        Public Sub TwoAdjacentCustomInteractionsMediaFixerTest()
            'arrange
            Dim inputDocument = _twoAdjacentCustomInteractionsInput.ToXmlDocument
            Dim expectedOutput = New XDocument(_twoAdjacentCustomInteractionsExpectedOutput)
            Dim mediaFixer As New FixInteractions

            'act
            mediaFixer.Modify(inputDocument, New QTI22DocumentHelper())

            'assert
            Dim output = New XDocument(XElement.Parse(inputDocument.OuterXml))
            Assert.IsTrue(UnitTestHelper.AreSame(expectedOutput, output))
        End Sub

        <TestMethod(), TestCategory("FACET Document Tweaks")>
        Public Sub PopUpDivFixerTest()
            'arrange
            Dim inputDocument = _popUpElement.ToXmlDocument
            Dim expectedOutput = New XDocument(_popupExpectedOutput)
            Dim divFixer As New FixDiv

            'act
            divFixer.Modify(inputDocument, "e45cb5de-11ee-404a-9b31-490b82a17b2e", New QTI22DocumentHelper())

            'assert
            Dim output = New XDocument(XElement.Parse(inputDocument.OuterXml))
            Assert.IsTrue(UnitTestHelper.AreSame(expectedOutput, output))
        End Sub

        Private ReadOnly _popUpElement As XElement = <wrapper>
                                                         <p id="c1-id-11">
                                                             <div id="POPCT_2f9bfee1-2979-4f58-bc15-6dc69c220a93">
                                                                 <p id="c1-id-11-2">videocontent
                                                                <div id="I1becf4b6-e6d5-4cf4-973d-d497d0c23a5c">
                                                                         <mediaInteraction responseIdentifier="VIDEORESPONSE" autostart="false" maxPlays="0" minPlays="0" loop="false" id="Ie5c3a6b6-2752-43c9-80a3-a741fa7e8ec9">
                                                                             <object type="video/webm" data="resource://package:1/013.webm" height="1080" width="1920"/>
                                                                         </mediaInteraction>
                                                                     </div>
                                                                 </p>
                                                             </div>
                                                         </p>
                                                     </wrapper>

        Private ReadOnly _popupExpectedOutput As XElement =
            <wrapper>
                <p id="c1-id-11" style="margin-bottom: 0px;"/>
                <div id="POPCT_2f9bfee1-2979-4f58-bc15-6dc69c220a93">
                    <p id="c1-id-11-2">videocontent
                                                                </p>
                    <div id="I1becf4b6-e6d5-4cf4-973d-d497d0c23a5c">
                        <mediaInteraction responseIdentifier="VIDEORESPONSE" autostart="false" maxPlays="0" minPlays="0" loop="false" id="Ie5c3a6b6-2752-43c9-80a3-a741fa7e8ec9">
                            <object type="video/webm" data="resource://package:1/013.webm" height="1080" width="1920"/>
                        </mediaInteraction>
                    </div>
                    <p id="c1-id-11_copied_e45cb5de-11ee-404a-9b31-490b82a17b2e_1"/>
                </div>
                <p id="c1-id-11_copied_e45cb5de-11ee-404a-9b31-490b82a17b2e_0"/>
            </wrapper>

        Private ReadOnly _twoAdjacentCustomInteractionsInput As XElement =
        <wrapper>
            <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
            <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
            <itemBody class="defaultBody">
                <div class="content">
                    <div class="div_left">
                        <!-- div_left_inner is needed to create the 50px margin between the left and right columns in dual column mode -->
                        <div class="div_left_inner"/>
                    </div>
                    <div class="div_right">
                        <!-- div_right_inner is needed to create the 50px margin between the left and right columns in dual column mode -->
                        <div class="div_right_inner">
                            <div id="body">
                                <p id="c1-id-11">
                                    <span class="inlinecontrol">
                                        <customInteraction responseIdentifier="I42eccf52-b0eb-4c99-940e-456808a8d9ea">
                                            <html:object xmlns:html="http://www.w3.org/1999/xhtml" type="application/javascript" height="150" width="150" data="resource://package:1/dtt-wi-DWOtest1.ci">
                                                <!-- + 1 because first entry is reserved to save state -->
                                                <param name="responseLength" value="1" valuetype="DATA"/>
                                            </html:object>
                                        </customInteraction>
                                    </span> <span class="inlinecontrol">
                                        <customInteraction responseIdentifier="I2a838e07-4026-49b6-9d32-1582bb468344">
                                            <html:object xmlns:html="http://www.w3.org/1999/xhtml" type="application/vnd.GeoGebra.file" height="150" width="150" data="resource://package:1/dtt-wi-GGBtest3-aaa.ggb">
                                                <!-- extra parameters to show/hide toolbars, etc - presets have to be determinded first -->
                                                <param name="enableRightClick" value="false" valuetype="DATA"/>
                                                <param name="showToolbar" value="true" valuetype="DATA"/>
                                                <param name="showMenuBar" value="false" valuetype="DATA"/>
                                                <param name="showAlgebraInput" value="false" valuetype="DATA"/>
                                                <param name="showResetIcon" value="false" valuetype="DATA"/>
                                                <param name="customToolbar" value="0 | 1 7 46 | 2 18 15 | 10 34 20 21 | 16 51 | 4 3 8 9 | 29 30 32 31 | 17 | 40 41 42 27 28 35 6" valuetype="DATA"/>
                                            </html:object>
                                        </customInteraction>
                                    </span>
                                </p>
                            </div>
                            <div id="question">
                                <p id="c1-id-11-2">Huh?</p>
                            </div>
                            <div id="mc">
                                <choiceInteraction id="choiceInteraction1" class="" maxChoices="1" shuffle="false" responseIdentifier="mc">
                                    <simpleChoice identifier="A">
                                        <p id="c1-id-11-3">A</p>
                                    </simpleChoice>
                                    <simpleChoice identifier="B">
                                        <p id="c1-id-11-4">B</p>
                                    </simpleChoice>
                                </choiceInteraction>
                            </div>
                        </div>
                    </div>
                </div>
            </itemBody>
        </wrapper>

        ReadOnly _twoAdjacentCustomInteractionsExpectedOutput As XElement =
            <wrapper>
                <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
                <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
                <itemBody class="defaultBody">
                    <div class="content">
                        <div class="div_left">
                            <!-- div_left_inner is needed to create the 50px margin between the left and right columns in dual column mode -->
                            <div class="div_left_inner"/>
                        </div>
                        <div class="div_right">
                            <!-- div_right_inner is needed to create the 50px margin between the left and right columns in dual column mode -->
                            <div class="div_right_inner">
                                <div id="body">
                                    <p id="c1-id-11">
                                        <span class="inlinecontrol">
                                            <customInteraction responseIdentifier="I42eccf52-b0eb-4c99-940e-456808a8d9ea">
                                                <html:object xmlns:html="http://www.w3.org/1999/xhtml" type="application/javascript" height="150" width="150" data="resource://package:1/dtt-wi-DWOtest1.ci">
                                                    <!-- + 1 because first entry is reserved to save state -->
                                                    <param name="responseLength" value="1" valuetype="DATA"/>
                                                </html:object>
                                            </customInteraction>
                                        </span> <span class="inlinecontrol"><customInteraction responseIdentifier="I2a838e07-4026-49b6-9d32-1582bb468344"><html:object xmlns:html="http://www.w3.org/1999/xhtml" type="application/vnd.GeoGebra.file" height="150" width="150" data="resource://package:1/dtt-wi-GGBtest3-aaa.ggb"><!-- extra parameters to show/hide toolbars, etc - presets have to be determinded first --><param name="enableRightClick" value="false" valuetype="DATA"/><param name="showToolbar" value="true" valuetype="DATA"/><param name="showMenuBar" value="false" valuetype="DATA"/><param name="showAlgebraInput" value="false" valuetype="DATA"/><param name="showResetIcon" value="false" valuetype="DATA"/><param name="customToolbar" value="0 | 1 7 46 | 2 18 15 | 10 34 20 21 | 16 51 | 4 3 8 9 | 29 30 32 31 | 17 | 40 41 42 27 28 35 6" valuetype="DATA"/></html:object></customInteraction></span></p>
                                </div>
                                <div id="question">
                                    <p id="c1-id-11-2">Huh?</p>
                                </div>
                                <div id="mc">
                                    <choiceInteraction id="choiceInteraction1" class="" maxChoices="1" shuffle="false" responseIdentifier="mc">
                                        <simpleChoice identifier="A">
                                            <p id="c1-id-11-3">A</p>
                                        </simpleChoice>
                                        <simpleChoice identifier="B">
                                            <p id="c1-id-11-4">B</p>
                                        </simpleChoice>
                                    </choiceInteraction>
                                </div>
                            </div>
                        </div>
                    </div>
                </itemBody>
            </wrapper>

    End Class
End Namespace
