Imports System.Xml
Imports Questify.Builder.Logic.QTI.Helpers.QTI_Base
Imports Questify.Builder.UnitTests.Framework

Namespace QTI_Base

    <TestClass()>
    Public Class TextToSpeechHelperTests

        Private Const SSMLNAMESPACE As String = "http://www.w3.org/2010/10/synthesis"

        <TestMethod, TestCategory("Publication"), TestCategory("Helpers")>
        Public Sub TestTTSMuteContainsStrongTag()
            Dim input = <root><span class="TTSMute">Consectetuer incididunt <strong>nostrum mus dicta</strong> tristique? Mollit magnis vehicula ab.</span></root>
            Dim expected = New XDocument(<root><ssml:prosody xmlns:ssml="http://www.w3.org/2010/10/synthesis" volume="silent"><ssml:s xmlns:ssml="http://www.w3.org/2010/10/synthesis">Consectetuer incididunt <strong>nostrum mus dicta</strong> tristique? Mollit magnis vehicula ab.</ssml:s></ssml:prosody></root>)
            Dim xmlDoc = New XmlDocument()
            xmlDoc.LoadXml(input.ToString())

            TextToSpeechHelper.ConvertToSsml(xmlDoc, SSMLNAMESPACE)
            Dim result = XDocument.Parse(xmlDoc.OuterXml)

            Assert.IsTrue(UnitTestHelper.AreSame(expected, result))
        End Sub

        <TestMethod, TestCategory("Publication"), TestCategory("Helpers")>
        Public Sub TestTTsMute()
            Dim input = <p id="c1-a1-11">Consectetuer incididunt <span class="TTSMute">nostrum</span> mus dicta tristique? Mollit magnis vehicula ab.</p>
            Dim expected = New XDocument(<p id="c1-a1-11">Consectetuer incididunt <ssml:prosody volume="silent" xmlns:ssml="http://www.w3.org/2010/10/synthesis">nostrum</ssml:prosody> mus dicta tristique? Mollit magnis vehicula ab.</p>)
            TTSBaseTest(input, expected)
        End Sub

        <TestMethod, TestCategory("Publication"), TestCategory("Helpers")>
        Public Sub TestTTSAlternative()
            Dim input = <p id="c1-a1-11">Consectetuer incididunt <span class="TTSAlternative"><span class="TTSAlias">nostroem</span>nostrum</span> mus dicta tristique? Mollit magnis vehicula ab.</p>
            Dim expected = New XDocument(<p id="c1-a1-11">Consectetuer incididunt <ssml:sub alias="nostroem" xmlns:ssml="http://www.w3.org/2010/10/synthesis">nostrum</ssml:sub> mus dicta tristique? Mollit magnis vehicula ab.</p>)
            TTSBaseTest(input, expected)
        End Sub

        <TestMethod, TestCategory("Publication"), TestCategory("Helpers")>
        Public Sub TestTTSAlternativeWithHtmlContent()
            Dim input = <p id="c1-a1-11">Donec nostra, <span class="TTSAlternative"><span class="TTSAlias">Porro porta</span>pede <strong>itaque</strong></span> tenetur ligula dolore nec per a.</p>
            Dim expected = New XDocument(<p id="c1-a1-11">Donec nostra, <ssml:sub alias="Porro porta" xmlns:ssml="http://www.w3.org/2010/10/synthesis">pede <strong>itaque</strong></ssml:sub> tenetur ligula dolore nec per a.</p>)
            TTSBaseTest(input, expected)
        End Sub

        <TestMethod, TestCategory("Publication"), TestCategory("Helpers")>
        Public Sub TestTTSPause()
            Dim input = <p id="c1-a1-11">Consectetuer incididunt <span class="TTSPause PauseDuration_100"/>nostrum mus dicta tristique? Mollit magnis vehicula ab.</p>
            Dim expected = New XDocument(<p id="c1-a1-11">Consectetuer incididunt <ssml:break time="100ms" xmlns:ssml="http://www.w3.org/2010/10/synthesis"/>nostrum mus dicta tristique? Mollit magnis vehicula ab.</p>)
            TTSBaseTest(input, expected)
        End Sub

        <TestMethod, TestCategory("Publication"), TestCategory("Helpers")>
        Public Sub TestTTSPauseZeroNoChange()
            Dim input = <p id="c1-a1-11">Consectetuer incididunt <span class="TTSPause PauseDuration_0"/>nostrum mus dicta tristique? Mollit magnis vehicula ab.</p>
            Dim expected = New XDocument(<p id="c1-a1-11">Consectetuer incididunt <ssml:break time="0ms" xmlns:ssml="http://www.w3.org/2010/10/synthesis"/>nostrum mus dicta tristique? Mollit magnis vehicula ab.</p>)
            TTSBaseTest(input, expected)
        End Sub

        <TestMethod, TestCategory("Publication"), TestCategory("Helpers")>
        Public Sub TestTTSPauseText()
            Dim input = <p id="c1-a1-11">Consectetuer incididunt <span class="TTSPause PauseDuration_HONDERD"/>nostrum mus dicta tristique? Mollit magnis vehicula ab.</p>
            TTSBaseTest(input, New XDocument(input))
        End Sub

        <TestMethod, TestCategory("Publication"), TestCategory("Helpers")>
        Public Sub TestTTSPauseNoValue()
            Dim input = <p id="c1-a1-11">Consectetuer incididunt <span class="TTSPause PauseDuration_"/>nostrum mus dicta tristique? Mollit magnis vehicula ab.</p>
            TTSBaseTest(input, New XDocument(input))
        End Sub

        Private Sub TTSBaseTest(input As XElement, expected As XDocument)
            Dim xmlDoc = New XmlDocument()
            xmlDoc.LoadXml(input.ToString())

            TextToSpeechHelper.ConvertToSsml(xmlDoc, SSMLNAMESPACE)
            Dim result = XDocument.Parse(xmlDoc.OuterXml)

            Assert.IsTrue(UnitTestHelper.AreSame(expected, result))
        End Sub

        <TestMethod, TestCategory("Publication"), TestCategory("Helpers")>
        Public Sub TestTTSMuteContainsEmTag()
            Dim input = <xhtmlparameter name="itemQuestion">
                            <p id="c1-id-12" xmlns="http://www.w3.org/1999/xhtml">Dit is <span class="TTSMute" id="c1-id-13">de <em id="c1-id-14">vraag</em> met een stukje</span> tekst.</p>
                        </xhtmlparameter>
            Dim expected = New XDocument(<xhtmlparameter name="itemQuestion">
                                             <p xmlns="http://www.w3.org/1999/xhtml" id="c1-id-12">Dit is <ssml:prosody volume="silent" xmlns:ssml="http://www.w3.org/2010/10/synthesis"><ssml:s xmlns:ssml="http://www.w3.org/2010/10/synthesis">de <em id="c1-id-14" xmlns="http://www.w3.org/1999/xhtml">vraag</em> met een stukje</ssml:s></ssml:prosody> tekst.</p>
                                         </xhtmlparameter>)
            Dim xmlDoc = New XmlDocument()
            xmlDoc.LoadXml(input.ToString())

            TextToSpeechHelper.ConvertToSsml(xmlDoc, SSMLNAMESPACE)
            Dim result = XDocument.Parse(xmlDoc.OuterXml)

            Assert.IsTrue(UnitTestHelper.AreSame(expected, result))
        End Sub

        <TestMethod, TestCategory("Publication"), TestCategory("Helpers")>
        Public Sub TestTTSMuteContainsSpanWithTextDecorationUnderline()
            Dim input = <xhtmlparameter name="itemQuestion">
                            <p id="c1-id-12" xmlns="http://www.w3.org/1999/xhtml">Dit is <span class="TTSMute" id="c1-id-13">de vraag met een <span style="text-decoration:underline" id="c1-id-15">stukje</span></span> tekst.</p>
                        </xhtmlparameter>
            Dim expected = New XDocument(<xhtmlparameter name="itemQuestion">
                                             <p xmlns="http://www.w3.org/1999/xhtml" id="c1-id-12">Dit is <ssml:prosody volume="silent" xmlns:ssml="http://www.w3.org/2010/10/synthesis"><ssml:s xmlns:ssml="http://www.w3.org/2010/10/synthesis">de vraag met een <span style="text-decoration:underline" id="c1-id-15" xmlns="http://www.w3.org/1999/xhtml">stukje</span></ssml:s></ssml:prosody> tekst.</p>
                                         </xhtmlparameter>)
            Dim xmlDoc = New XmlDocument()
            xmlDoc.LoadXml(input.ToString())

            TextToSpeechHelper.ConvertToSsml(xmlDoc, SSMLNAMESPACE)
            Dim result = XDocument.Parse(xmlDoc.OuterXml)

            Assert.IsTrue(UnitTestHelper.AreSame(expected, result))
        End Sub

        <TestMethod, TestCategory("Publication"), TestCategory("Helpers")>
        Public Sub TestTTSMuteContainsSupTag()
            Dim input = <xhtmlparameter name="itemQuestion">
                            <p id="c1-id-12" xmlns="http://www.w3.org/1999/xhtml"><span class="TTSMute" id="c1-id-13">Dit is de vraag met een stukje <sup id="c1-id-17">tekst</sup> met opmaak dat niet verklankt</span> moet worden.</p>
                        </xhtmlparameter>
            Dim expected = New XDocument(<xhtmlparameter name="itemQuestion">
                                             <p xmlns="http://www.w3.org/1999/xhtml" id="c1-id-12"><ssml:prosody volume="silent" xmlns:ssml="http://www.w3.org/2010/10/synthesis"><ssml:s xmlns:ssml="http://www.w3.org/2010/10/synthesis">Dit is de vraag met een stukje <sup id="c1-id-17" xmlns="http://www.w3.org/1999/xhtml">tekst</sup> met opmaak dat niet verklankt</ssml:s></ssml:prosody> moet worden.</p>
                                         </xhtmlparameter>)
            Dim xmlDoc = New XmlDocument()
            xmlDoc.LoadXml(input.ToString())

            TextToSpeechHelper.ConvertToSsml(xmlDoc, SSMLNAMESPACE)
            Dim result = XDocument.Parse(xmlDoc.OuterXml)

            Assert.IsTrue(UnitTestHelper.AreSame(expected, result))
        End Sub

        <TestMethod, TestCategory("Publication"), TestCategory("Helpers")>
        Public Sub TestTTSMuteContainsSubTag()
            Dim input = <xhtmlparameter name="itemQuestion">
                            <p id="c1-id-12" xmlns="http://www.w3.org/1999/xhtml"><span class="TTSMute" id="c1-id-13">Dit is de vraag met een stukje tekst met <sub id="c1-id-18">opmaak</sub> dat niet verklankt</span> moet worden.</p>
                        </xhtmlparameter>
            Dim expected = New XDocument(<xhtmlparameter name="itemQuestion">
                                             <p xmlns="http://www.w3.org/1999/xhtml" id="c1-id-12"><ssml:prosody volume="silent" xmlns:ssml="http://www.w3.org/2010/10/synthesis"><ssml:s xmlns:ssml="http://www.w3.org/2010/10/synthesis">Dit is de vraag met een stukje tekst met <sub id="c1-id-18" xmlns="http://www.w3.org/1999/xhtml">opmaak</sub> dat niet verklankt</ssml:s></ssml:prosody> moet worden.</p>
                                         </xhtmlparameter>)
            Dim xmlDoc = New XmlDocument()
            xmlDoc.LoadXml(input.ToString())

            TextToSpeechHelper.ConvertToSsml(xmlDoc, SSMLNAMESPACE)
            Dim result = XDocument.Parse(xmlDoc.OuterXml)

            Assert.IsTrue(UnitTestHelper.AreSame(expected, result))
        End Sub

        <TestMethod, TestCategory("Publication"), TestCategory("Helpers")>
        Public Sub TestTTSMuteContainsSpanWithTextDecorationLineThrough()
            Dim input = <xhtmlparameter name="itemQuestion">
                            <p id="c1-id-12" xmlns="http://www.w3.org/1999/xhtml"><span class="TTSMute" id="c1-id-13">Dit is de vraag met een stukje tekst met opmaak dat <span style="text-decoration:line-through" id="c1-id-19">niet</span> verklankt</span> moet worden.</p>
                        </xhtmlparameter>
            Dim expected = New XDocument(<xhtmlparameter name="itemQuestion">
                                             <p xmlns="http://www.w3.org/1999/xhtml" id="c1-id-12"><ssml:prosody volume="silent" xmlns:ssml="http://www.w3.org/2010/10/synthesis"><ssml:s xmlns:ssml="http://www.w3.org/2010/10/synthesis">Dit is de vraag met een stukje tekst met opmaak dat <span id="c1-id-19" style="text-decoration:line-through">niet</span> verklankt</ssml:s></ssml:prosody> moet worden.</p>
                                         </xhtmlparameter>)
            Dim xmlDoc = New XmlDocument()
            xmlDoc.LoadXml(input.ToString())

            TextToSpeechHelper.ConvertToSsml(xmlDoc, SSMLNAMESPACE)
            Dim result = XDocument.Parse(xmlDoc.OuterXml)

            Assert.IsTrue(UnitTestHelper.AreSame(expected, result))
        End Sub

        <TestMethod, TestCategory("Publication"), TestCategory("Helpers")>
        Public Sub TestTTSMuteContainsNestedLayoutTags()
            Dim input = <xhtmlparameter name="itemQuestion">
                            <p id="c1-id-12" xmlns="http://www.w3.org/1999/xhtml"><span class="TTSMute" id="c1-id-13">Dit <strong id="c1-id-14">is <em id="c1-id-15"><sup id="c1-id-16">de</sup><span style="text-decoration:underline" id="c1-id-17">vraag</span></em></strong><span style="text-decoration:underline;" id="c1-id-18"><em id="c1-id-19"><sub id="c1-id-20">met</sub></em></span><em id="c1-id-21"><span style="text-decoration:underline" id="c1-id-22"></span></em><span style="text-decoration:underline;" id="c1-id-23"><span style="text-decoration:underline line-through" id="c1-id-24">een</span></span> stukje tekst</span> met opmaak dat niet verklankt moet worden.</p>
                        </xhtmlparameter>
            Dim expected = New XDocument(<xhtmlparameter name="itemQuestion">
                                             <p xmlns="http://www.w3.org/1999/xhtml" id="c1-id-12"><ssml:prosody volume="silent" xmlns:ssml="http://www.w3.org/2010/10/synthesis"><ssml:s xmlns:ssml="http://www.w3.org/2010/10/synthesis">Dit <strong id="c1-id-14" xmlns="http://www.w3.org/1999/xhtml">is <em id="c1-id-15"><sup id="c1-id-16">de</sup><span style="text-decoration:underline" id="c1-id-17">vraag</span></em></strong><span style="text-decoration:underline;" id="c1-id-18" xmlns="http://www.w3.org/1999/xhtml"><em id="c1-id-19"><sub id="c1-id-20">met</sub></em></span><em id="c1-id-21" xmlns="http://www.w3.org/1999/xhtml"><span style="text-decoration:underline" id="c1-id-22"></span></em><span style="text-decoration:underline;" id="c1-id-23" xmlns="http://www.w3.org/1999/xhtml"><span style="text-decoration:underline line-through" id="c1-id-24">een</span></span> stukje tekst</ssml:s></ssml:prosody> met opmaak dat niet verklankt moet worden.</p>
                                         </xhtmlparameter>)
            Dim xmlDoc = New XmlDocument()
            xmlDoc.LoadXml(input.ToString())

            TextToSpeechHelper.ConvertToSsml(xmlDoc, SSMLNAMESPACE)
            Dim result = XDocument.Parse(xmlDoc.OuterXml)

            Assert.IsTrue(UnitTestHelper.AreSame(expected, result))
        End Sub

        <TestMethod, TestCategory("Publication"), TestCategory("Helpers")>
        Public Sub TestTTSMuteContainsLists()
            Dim input = <xhtmlparameter name="itemQuestion">
                            <p id="c1-id-12" xmlns="http://www.w3.org/1999/xhtml">Dit is de vraag met lijstjes die niet verklankt moeten worden.</p>
                            <ol type="1" id="c1-id-13" xmlns="http://www.w3.org/1999/xhtml">
                                <li id="c1-id-14"><span class="TTSMute" id="c1-id-15">Een</span></li>
                                <li id="c1-id-16"><span class="TTSMute" id="c1-id-17">Twee</span></li>
                                <li id="c1-id-18"><span class="TTSMute" id="c1-id-19">Drie</span></li>
                                <li id="c1-id-20"><span class="TTSMute" id="c1-id-21">Vier</span></li>
                            </ol>
                            <p id="c1-id-22" class="LangTTSEngels" xmlns="http://www.w3.org/1999/xhtml"><span class="TTSMute" id="c1-id-23">Part two</span></p>
                            <ul id="c1-id-24" xmlns="http://www.w3.org/1999/xhtml">
                                <li id="c1-id-25"><span class="TTSMute" id="c1-id-26">Hoedje van</span></li>
                                <li id="c1-id-27"><span class="TTSMute" id="c1-id-28">Hoedje van</span></li>
                            </ul>
                            <p id="c1-id-29" class="LangTTSEngels" xmlns="http://www.w3.org/1999/xhtml"><span class="TTSMute" id="c1-id-30">Part three</span></p>
                            <ol type="I" id="c1-id-31" xmlns="http://www.w3.org/1999/xhtml">
                                <li id="c1-id-32"><span class="TTSMute" id="c1-id-33">Een</span></li>
                                <li id="c1-id-34"><span class="TTSMute" id="c1-id-35">Twee</span></li>
                                <li id="c1-id-36"><span class="TTSMute" id="c1-id-37">Drie</span></li>
                                <li id="c1-id-38"><span class="TTSMute" id="c1-id-39">Vier</span></li>
                            </ol>
                            <p id="c1-id-40" xmlns="http://www.w3.org/1999/xhtml"><span class="TTSMute" id="c1-id-41">Hoedje van papier</span></p>
                        </xhtmlparameter>
            Dim expected = New XDocument(<xhtmlparameter name="itemQuestion">
                                             <p xmlns="http://www.w3.org/1999/xhtml" id="c1-id-12">Dit is de vraag met lijstjes die niet verklankt moeten worden.</p>
                                             <ol xmlns="http://www.w3.org/1999/xhtml" type="1" id="c1-id-13">
                                                 <li id="c1-id-14"><ssml:prosody volume="silent" xmlns:ssml="http://www.w3.org/2010/10/synthesis">Een</ssml:prosody></li>
                                                 <li id="c1-id-16"><ssml:prosody volume="silent" xmlns:ssml="http://www.w3.org/2010/10/synthesis">Twee</ssml:prosody></li>
                                                 <li id="c1-id-18"><ssml:prosody volume="silent" xmlns:ssml="http://www.w3.org/2010/10/synthesis">Drie</ssml:prosody></li>
                                                 <li id="c1-id-20"><ssml:prosody volume="silent" xmlns:ssml="http://www.w3.org/2010/10/synthesis">Vier</ssml:prosody></li>
                                             </ol>
                                             <p xmlns="http://www.w3.org/1999/xhtml" id="c1-id-22" class="LangTTSEngels"><ssml:prosody volume="silent" xmlns:ssml="http://www.w3.org/2010/10/synthesis">Part two</ssml:prosody></p>
                                             <ul xmlns="http://www.w3.org/1999/xhtml" id="c1-id-24">
                                                 <li id="c1-id-25"><ssml:prosody volume="silent" xmlns:ssml="http://www.w3.org/2010/10/synthesis">Hoedje van</ssml:prosody></li>
                                                 <li id="c1-id-27"><ssml:prosody volume="silent" xmlns:ssml="http://www.w3.org/2010/10/synthesis">Hoedje van</ssml:prosody></li>
                                             </ul>
                                             <p xmlns="http://www.w3.org/1999/xhtml" id="c1-id-29" class="LangTTSEngels"><ssml:prosody volume="silent" xmlns:ssml="http://www.w3.org/2010/10/synthesis">Part three</ssml:prosody></p>
                                             <ol xmlns="http://www.w3.org/1999/xhtml" type="I" id="c1-id-31">
                                                 <li id="c1-id-32"><ssml:prosody volume="silent" xmlns:ssml="http://www.w3.org/2010/10/synthesis">Een</ssml:prosody></li>
                                                 <li id="c1-id-34"><ssml:prosody volume="silent" xmlns:ssml="http://www.w3.org/2010/10/synthesis">Twee</ssml:prosody></li>
                                                 <li id="c1-id-36"><ssml:prosody volume="silent" xmlns:ssml="http://www.w3.org/2010/10/synthesis">Drie</ssml:prosody></li>
                                                 <li id="c1-id-38"><ssml:prosody volume="silent" xmlns:ssml="http://www.w3.org/2010/10/synthesis">Vier</ssml:prosody></li>
                                             </ol>
                                             <p xmlns="http://www.w3.org/1999/xhtml" id="c1-id-40"><ssml:prosody volume="silent" xmlns:ssml="http://www.w3.org/2010/10/synthesis">Hoedje van papier</ssml:prosody></p>
                                         </xhtmlparameter>)
            Dim xmlDoc = New XmlDocument()
            xmlDoc.LoadXml(input.ToString())

            TextToSpeechHelper.ConvertToSsml(xmlDoc, SSMLNAMESPACE)
            Dim result = XDocument.Parse(xmlDoc.OuterXml)

            Assert.IsTrue(UnitTestHelper.AreSame(expected, result))
        End Sub

        <TestMethod, TestCategory("Publication"), TestCategory("Helpers")>
        Public Sub TestTTSMuteEntireItem()
            Dim input = _verklankingsItemInput
            Dim expected = New XDocument(_verklangingsItemExpectedResult)
            Dim xmlDoc = New XmlDocument()
            xmlDoc.LoadXml(input.ToString())

            TextToSpeechHelper.ConvertToSsml(xmlDoc, SSMLNAMESPACE)
            Dim result = XDocument.Parse(xmlDoc.OuterXml)

            Assert.IsTrue(UnitTestHelper.AreSame(expected, result))
        End Sub


        ReadOnly _verklankingsItemInput As XElement =
        <assessmentItem xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" identifier="TestVerklanking" itemid="322e7h" title="TestVerklanking" layoutTemplateSrc="Cito.Generic.MC.SC">
            <solution>
                <keyFindings>
                    <keyFinding id="mc" scoringMethod="Dichotomous">
                        <keyFact id="A-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="mc" occur="1">
                                <stringValue>
                                    <typedValue>A</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                    </keyFinding>
                </keyFindings>
                <aspectReferences/>
                <ItemScoreTranslationTable>
                    <ItemScoreTranslationTableEntry rawScore="0" translatedScore="0"/>
                    <ItemScoreTranslationTableEntry rawScore="1" translatedScore="1"/>
                </ItemScoreTranslationTable>
            </solution>
            <parameters>
                <parameterSet id="entireItem">
                    <booleanparameter name="isScoredItem">True</booleanparameter>
                    <booleanparameter name="dualColumnLayout">False</booleanparameter>
                    <booleanparameter name="showCalculatorButton"/>
                    <booleanparameter name="displayVerklankingOnTheRight">True</booleanparameter>
                    <texttospeechparameter name="verklankingLinks"/>
                    <texttospeechparameter name="verklankingRechts"/>
                    <xhtmlparameter name="leftBody"/>
                    <xhtmlresourceparameter name="leftSource"/>
                    <resourceparameter name="wordSourceText"/>
                    <integerparameter name="sourceHeight">200</integerparameter>
                    <integerparameter name="sourcePositionTop">0</integerparameter>
                    <xhtmlparameter name="itemBody">
                        <p id="c1-id-12" xmlns="http://www.w3.org/1999/xhtml">
                            <span class="TTSMute" id="c1-id-13">Deze <em id="c1-id-14"><strong id="c1-id-15">body</strong><span style="text-decoration:underline" id="c1-id-16">bevat</span></em><span style="text-decoration:underline" id="c1-id-17"> tekst</span><sup id="c1-id-18">met</sup><sub id="c1-id-19">opmaak</sub><span style="text-decoration:line-through" id="c1-id-20">die</span> niet verklankt hoeft te worden.</span>
                        </p>
                    </xhtmlparameter>
                    <xhtmlparameter name="itemQuestion">
                        <p id="c1-id-12" xmlns="http://www.w3.org/1999/xhtml">
                            <span class="TTSMute" id="c1-id-13">Deze vraag hoeft niet verklankt te worden maar bevat wel lijstjes en inspringing.</span>
                        </p>
                        <p id="c1-id-14" xmlns="http://www.w3.org/1999/xhtml">
                            <span class="TTSMute" id="c1-id-15"> </span>
                        </p>
                        <ol type="1" id="c1-id-16" xmlns="http://www.w3.org/1999/xhtml">
                            <li id="c1-id-17">
                                <span class="TTSMute" id="c1-id-18">1</span>
                            </li>
                            <li id="c1-id-19">
                                <span class="TTSMute" id="c1-id-20">2</span>
                            </li>
                            <li id="c1-id-21">
                                <span class="TTSMute" id="c1-id-22">3</span>
                            </li>
                            <li id="c1-id-23">
                                <span class="TTSMute" id="c1-id-24">4</span>
                            </li>
                        </ol>
                        <blockquote id="c1-id-25" xmlns="http://www.w3.org/1999/xhtml">
                            <p id="c1-id-26">
                                <span class="TTSMute" id="c1-id-27">Spring in</span>
                            </p>
                        </blockquote>
                        <p id="c1-id-28" xmlns="http://www.w3.org/1999/xhtml">
                            <span class="TTSMute" id="c1-id-29">Spring uit</span>
                        </p>
                        <p id="c1-id-30" style="TEXT-ALIGN: center" xmlns="http://www.w3.org/1999/xhtml">
                            <span class="TTSMute" id="c1-id-31">Gecentreerd</span>
                        </p>
                        <p id="c1-id-32" style="TEXT-ALIGN: right" xmlns="http://www.w3.org/1999/xhtml">
                            <span class="TTSMute" id="c1-id-33">Rechts</span>
                        </p>
                        <p id="c1-id-34" style="TEXT-ALIGN: left" xmlns="http://www.w3.org/1999/xhtml">
                            <span class="TTSMute" id="c1-id-35">En dan weer terug naar links</span>
                        </p>
                        <p id="c1-id-36" style="TEXT-ALIGN: left" xmlns="http://www.w3.org/1999/xhtml">
                            <span class="TTSMute" id="c1-id-37">En tot slot wat bullets:</span>
                        </p>
                        <ul id="c1-id-38" xmlns="http://www.w3.org/1999/xhtml">
                            <li id="c1-id-39">
                                <p id="c1-id-40" style="TEXT-ALIGN: left">
                                    <span class="TTSMute" id="c1-id-41">bul</span>
                                </p>
                            </li>
                            <li id="c1-id-42">
                                <p id="c1-id-43" style="TEXT-ALIGN: left">
                                    <span class="TTSMute" id="c1-id-44">let</span>
                                </p>
                            </li>
                        </ul>
                    </xhtmlparameter>
                    <listedparameter name="expectedAnswers">1</listedparameter>
                    <integerparameter name="multiChoiceType">1</integerparameter>
                    <integerparameter name="fixedHeightAlternatives">0</integerparameter>
                    <listedparameter name="nrAlternativesPerLine">1</listedparameter>
                    <booleanparameter name="horizontallyCenteredAlternatives">False</booleanparameter>
                    <booleanparameter name="hideRadiobuttons">False</booleanparameter>
                    <xhtmlparameter name="kolomtekst"/>
                    <xhtmlparameter name="kolomtekst2"/>
                    <collectionparameter name="multiChoice">
                        <definition id="">
                            <xhtmlparameter name="choice"/>
                            <xhtmlparameter name="choice2"/>
                        </definition>
                    </collectionparameter>
                    <multichoicescoringparameter name="multiChoiceScoring" ControllerId="mc" findingOverride="mc" minChoices="1" maxChoices="1" multiChoice="Radio">
                        <subparameterset id="A">
                            <xhtmlparameter name="mcOption">
                                <p id="c1-id-12" xmlns="http://www.w3.org/1999/xhtml">Oke...</p>
                            </xhtmlparameter>
                            <xhtmlparameter name="mcOption2"/>
                        </subparameterset>
                        <subparameterset id="B">
                            <xhtmlparameter name="mcOption">
                                <p id="c1-id-12" xmlns="http://www.w3.org/1999/xhtml">Echt waar joh?</p>
                            </xhtmlparameter>
                            <xhtmlparameter name="mcOption2"/>
                        </subparameterset>
                        <definition id="">
                            <xhtmlparameter name="mcOption"/>
                            <xhtmlparameter name="mcOption2"/>
                        </definition>
                    </multichoicescoringparameter>
                    <xhtmlparameter name="itemGeneral"/>
                    <booleanparameter name="boldedMcLettersForWord">True</booleanparameter>
                </parameterSet>
                <parameterSet id="kenmerken">
                    <booleanparameter name="showCalculatorButton">False</booleanparameter>
                    <integerparameter name="hightOfScrollText"/>
                    <integerparameter name="fixedWidthMatrixColumn">100</integerparameter>
                    <booleanparameter name="showChoicesBottomLayout">False</booleanparameter>
                    <integerparameter name="fixedHeightAlternatives">0</integerparameter>
                    <plaintextparameter name="calculatorDescription"/>
                    <listedparameter name="calculatorMode">basic</listedparameter>
                    <booleanparameter name="showReset">False</booleanparameter>
                    <booleanparameter name="showNotepad">False</booleanparameter>
                    <plaintextparameter name="notepadDescription"/>
                    <booleanparameter name="showSymbolPicker">False</booleanparameter>
                    <plaintextparameter name="symbolPickerDescription"/>
                    <plaintextparameter name="symbols"/>
                    <booleanparameter name="showRuler">False</booleanparameter>
                    <plaintextparameter name="rulerDescription"/>
                    <plaintextparameter name="rulerStartValue"/>
                    <plaintextparameter name="rulerEndValue"/>
                    <plaintextparameter name="rulerStepValue"/>
                    <integerparameter name="rulerStart"/>
                    <integerparameter name="rulerEnd"/>
                    <integerparameter name="rulerStep"/>
                    <integerparameter name="rulerStepSize"/>
                    <listedparameter name="rulerLengthUnit">centimeter</listedparameter>
                    <booleanparameter name="showProtractor">False</booleanparameter>
                    <plaintextparameter name="protractorPickerDescription"/>
                    <booleanparameter name="protractorEnableRotation">True</booleanparameter>
                    <booleanparameter name="showTriangle">False</booleanparameter>
                    <plaintextparameter name="trianglePickerDescription"/>
                    <integerparameter name="triangleMinDegrees"/>
                    <integerparameter name="triangleMaxDegrees"/>
                    <booleanparameter name="showSpellCheck">False</booleanparameter>
                    <listedparameter name="spellCheckCulture">nl-NL</listedparameter>
                    <booleanparameter name="showFormulaList">False</booleanparameter>
                    <plaintextparameter name="formulaListDescription"/>
                    <listedparameter name="formulaType">default</listedparameter>
                    <booleanparameter name="showTextMarker">False</booleanparameter>
                    <plaintextparameter name="textMarkerDescription"/>
                    <listedparameter name="itemLanguage">nl-NL</listedparameter>
                    <listedparameter name="foreignLanguage">0</listedparameter>
                    <listedparameter name="specialItemLayout">0</listedparameter>
                    <integerparameter name="fixedWidthAlternatives">100</integerparameter>
                    <listedparameter name="inputFilter"/>
                </parameterSet>
            </parameters>
        </assessmentItem>



        ReadOnly _verklangingsItemExpectedResult As XElement =
        <assessmentItem xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" identifier="TestVerklanking" itemid="322e7h" title="TestVerklanking" layoutTemplateSrc="Cito.Generic.MC.SC">
            <solution>
                <keyFindings>
                    <keyFinding id="mc" scoringMethod="Dichotomous">
                        <keyFact xmlns="http://Cito.Tester.Server/xml/serialization" id="A-mc" score="1">
                            <keyValue domain="mc" occur="1">
                                <stringValue>
                                    <typedValue>A</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                    </keyFinding>
                </keyFindings>
                <aspectReferences/>
                <ItemScoreTranslationTable>
                    <ItemScoreTranslationTableEntry rawScore="0" translatedScore="0"/>
                    <ItemScoreTranslationTableEntry rawScore="1" translatedScore="1"/>
                </ItemScoreTranslationTable>
            </solution>
            <parameters>
                <parameterSet id="entireItem">
                    <booleanparameter name="isScoredItem">True</booleanparameter>
                    <booleanparameter name="dualColumnLayout">False</booleanparameter>
                    <booleanparameter name="showCalculatorButton"/>
                    <booleanparameter name="displayVerklankingOnTheRight">True</booleanparameter>
                    <texttospeechparameter name="verklankingLinks"/>
                    <texttospeechparameter name="verklankingRechts"/>
                    <xhtmlparameter name="leftBody"/>
                    <xhtmlresourceparameter name="leftSource"/>
                    <resourceparameter name="wordSourceText"/>
                    <integerparameter name="sourceHeight">200</integerparameter>
                    <integerparameter name="sourcePositionTop">0</integerparameter>
                    <xhtmlparameter name="itemBody">
                        <p xmlns="http://www.w3.org/1999/xhtml" id="c1-id-12">
                            <ssml:prosody volume="silent" xmlns:ssml="http://www.w3.org/2010/10/synthesis">
                                <ssml:s xmlns:ssml="http://www.w3.org/2010/10/synthesis">Deze <em id="c1-id-14" xmlns="http://www.w3.org/1999/xhtml">
                                        <strong id="c1-id-15">body</strong>
                                        <span style="text-decoration:underline" id="c1-id-16">bevat</span>
                                    </em>
                                    <span style="text-decoration:underline" id="c1-id-17" xmlns="http://www.w3.org/1999/xhtml"> tekst</span>
                                    <sup id="c1-id-18" xmlns="http://www.w3.org/1999/xhtml">met</sup>
                                    <sub id="c1-id-19" xmlns="http://www.w3.org/1999/xhtml">opmaak</sub>
                                    <span style="text-decoration:line-through" id="c1-id-20" xmlns="http://www.w3.org/1999/xhtml">die</span> niet verklankt hoeft te worden.</ssml:s>
                            </ssml:prosody>
                        </p>
                    </xhtmlparameter>
                    <xhtmlparameter name="itemQuestion">
                        <p xmlns="http://www.w3.org/1999/xhtml" id="c1-id-12">
                            <ssml:prosody volume="silent" xmlns:ssml="http://www.w3.org/2010/10/synthesis">Deze vraag hoeft niet verklankt te worden maar bevat wel lijstjes en inspringing.</ssml:prosody>
                        </p>
                        <p xmlns="http://www.w3.org/1999/xhtml" id="c1-id-14">
                            <ssml:prosody volume="silent" xmlns:ssml="http://www.w3.org/2010/10/synthesis"> </ssml:prosody>
                        </p>
                        <ol xmlns="http://www.w3.org/1999/xhtml" type="1" id="c1-id-16">
                            <li id="c1-id-17">
                                <ssml:prosody volume="silent" xmlns:ssml="http://www.w3.org/2010/10/synthesis">1</ssml:prosody>
                            </li>
                            <li id="c1-id-19">
                                <ssml:prosody volume="silent" xmlns:ssml="http://www.w3.org/2010/10/synthesis">2</ssml:prosody>
                            </li>
                            <li id="c1-id-21">
                                <ssml:prosody volume="silent" xmlns:ssml="http://www.w3.org/2010/10/synthesis">3</ssml:prosody>
                            </li>
                            <li id="c1-id-23">
                                <ssml:prosody volume="silent" xmlns:ssml="http://www.w3.org/2010/10/synthesis">4</ssml:prosody>
                            </li>
                        </ol>
                        <blockquote xmlns="http://www.w3.org/1999/xhtml" id="c1-id-25">
                            <p id="c1-id-26">
                                <ssml:prosody volume="silent" xmlns:ssml="http://www.w3.org/2010/10/synthesis">Spring in</ssml:prosody>
                            </p>
                        </blockquote>
                        <p xmlns="http://www.w3.org/1999/xhtml" id="c1-id-28">
                            <ssml:prosody volume="silent" xmlns:ssml="http://www.w3.org/2010/10/synthesis">Spring uit</ssml:prosody>
                        </p>
                        <p xmlns="http://www.w3.org/1999/xhtml" id="c1-id-30" style="TEXT-ALIGN: center">
                            <ssml:prosody volume="silent" xmlns:ssml="http://www.w3.org/2010/10/synthesis">Gecentreerd</ssml:prosody>
                        </p>
                        <p xmlns="http://www.w3.org/1999/xhtml" id="c1-id-32" style="TEXT-ALIGN: right">
                            <ssml:prosody volume="silent" xmlns:ssml="http://www.w3.org/2010/10/synthesis">Rechts</ssml:prosody>
                        </p>
                        <p xmlns="http://www.w3.org/1999/xhtml" id="c1-id-34" style="TEXT-ALIGN: left">
                            <ssml:prosody volume="silent" xmlns:ssml="http://www.w3.org/2010/10/synthesis">En dan weer terug naar links</ssml:prosody>
                        </p>
                        <p xmlns="http://www.w3.org/1999/xhtml" id="c1-id-36" style="TEXT-ALIGN: left">
                            <ssml:prosody volume="silent" xmlns:ssml="http://www.w3.org/2010/10/synthesis">En tot slot wat bullets:</ssml:prosody>
                        </p>
                        <ul xmlns="http://www.w3.org/1999/xhtml" id="c1-id-38">
                            <li id="c1-id-39">
                                <p id="c1-id-40" style="TEXT-ALIGN: left">
                                    <ssml:prosody volume="silent" xmlns:ssml="http://www.w3.org/2010/10/synthesis">bul</ssml:prosody>
                                </p>
                            </li>
                            <li id="c1-id-42">
                                <p id="c1-id-43" style="TEXT-ALIGN: left">
                                    <ssml:prosody volume="silent" xmlns:ssml="http://www.w3.org/2010/10/synthesis">let</ssml:prosody>
                                </p>
                            </li>
                        </ul>
                    </xhtmlparameter>
                    <listedparameter name="expectedAnswers">1</listedparameter>
                    <integerparameter name="multiChoiceType">1</integerparameter>
                    <integerparameter name="fixedHeightAlternatives">0</integerparameter>
                    <listedparameter name="nrAlternativesPerLine">1</listedparameter>
                    <booleanparameter name="horizontallyCenteredAlternatives">False</booleanparameter>
                    <booleanparameter name="hideRadiobuttons">False</booleanparameter>
                    <xhtmlparameter name="kolomtekst"/>
                    <xhtmlparameter name="kolomtekst2"/>
                    <collectionparameter name="multiChoice">
                        <definition id="">
                            <xhtmlparameter name="choice"/>
                            <xhtmlparameter name="choice2"/>
                        </definition>
                    </collectionparameter>
                    <multichoicescoringparameter name="multiChoiceScoring" ControllerId="mc" findingOverride="mc" minChoices="1" maxChoices="1" multiChoice="Radio">
                        <subparameterset id="A">
                            <xhtmlparameter name="mcOption">
                                <p xmlns="http://www.w3.org/1999/xhtml" id="c1-id-12">Oke...</p>
                            </xhtmlparameter>
                            <xhtmlparameter name="mcOption2"/>
                        </subparameterset>
                        <subparameterset id="B">
                            <xhtmlparameter name="mcOption">
                                <p xmlns="http://www.w3.org/1999/xhtml" id="c1-id-12">Echt waar joh?</p>
                            </xhtmlparameter>
                            <xhtmlparameter name="mcOption2"/>
                        </subparameterset>
                        <definition id="">
                            <xhtmlparameter name="mcOption"/>
                            <xhtmlparameter name="mcOption2"/>
                        </definition>
                    </multichoicescoringparameter>
                    <xhtmlparameter name="itemGeneral"/>
                    <booleanparameter name="boldedMcLettersForWord">True</booleanparameter>
                </parameterSet>
                <parameterSet id="kenmerken">
                    <booleanparameter name="showCalculatorButton">False</booleanparameter>
                    <integerparameter name="hightOfScrollText"/>
                    <integerparameter name="fixedWidthMatrixColumn">100</integerparameter>
                    <booleanparameter name="showChoicesBottomLayout">False</booleanparameter>
                    <integerparameter name="fixedHeightAlternatives">0</integerparameter>
                    <plaintextparameter name="calculatorDescription"/>
                    <listedparameter name="calculatorMode">basic</listedparameter>
                    <booleanparameter name="showReset">False</booleanparameter>
                    <booleanparameter name="showNotepad">False</booleanparameter>
                    <plaintextparameter name="notepadDescription"/>
                    <booleanparameter name="showSymbolPicker">False</booleanparameter>
                    <plaintextparameter name="symbolPickerDescription"/>
                    <plaintextparameter name="symbols"/>
                    <booleanparameter name="showRuler">False</booleanparameter>
                    <plaintextparameter name="rulerDescription"/>
                    <plaintextparameter name="rulerStartValue"/>
                    <plaintextparameter name="rulerEndValue"/>
                    <plaintextparameter name="rulerStepValue"/>
                    <integerparameter name="rulerStart"/>
                    <integerparameter name="rulerEnd"/>
                    <integerparameter name="rulerStep"/>
                    <integerparameter name="rulerStepSize"/>
                    <listedparameter name="rulerLengthUnit">centimeter</listedparameter>
                    <booleanparameter name="showProtractor">False</booleanparameter>
                    <plaintextparameter name="protractorPickerDescription"/>
                    <booleanparameter name="protractorEnableRotation">True</booleanparameter>
                    <booleanparameter name="showTriangle">False</booleanparameter>
                    <plaintextparameter name="trianglePickerDescription"/>
                    <integerparameter name="triangleMinDegrees"/>
                    <integerparameter name="triangleMaxDegrees"/>
                    <booleanparameter name="showSpellCheck">False</booleanparameter>
                    <listedparameter name="spellCheckCulture">nl-NL</listedparameter>
                    <booleanparameter name="showFormulaList">False</booleanparameter>
                    <plaintextparameter name="formulaListDescription"/>
                    <listedparameter name="formulaType">default</listedparameter>
                    <booleanparameter name="showTextMarker">False</booleanparameter>
                    <plaintextparameter name="textMarkerDescription"/>
                    <listedparameter name="itemLanguage">nl-NL</listedparameter>
                    <listedparameter name="foreignLanguage">0</listedparameter>
                    <listedparameter name="specialItemLayout">0</listedparameter>
                    <integerparameter name="fixedWidthAlternatives">100</integerparameter>
                    <listedparameter name="inputFilter"/>
                </parameterSet>
            </parameters>
        </assessmentItem>


    End Class

End Namespace