
Imports System.Xml
Imports Questify.Builder.Logic.QTI.Helpers.QTI_Base.QTICompliantHelper
Imports Questify.Builder.Logic.QTI.Helpers.QTI30

Namespace QTI30.Helpers
    <TestClass()> Public Class QTI22FixAspectTests

        <TestMethod(), TestCategory("FACET Document Tweaks")>
        Public Sub FixAspectTextTagHierarchyNotAllowed()
            'arrange
            Dim itemDoc As New XmlDocument
            itemDoc.LoadXml(GetExampleItemXmlWithOneUnderline.ToString())

            'act
            Dim aspectFixer As New FixAspects
            aspectFixer.Modify(itemDoc, New QTI30DocumentHelper())

            'assert
            Dim expectedFragment As String = "<strong>Dit een een probleem voor QTI22 waarbij <xsi:span style=""TEXT-DECORATION: underline"">underline</xsi:span> is genest direct onder een strong tag</strong>"
            Assert.IsTrue(itemDoc.OuterXml.Contains(expectedFragment))
        End Sub

        <TestMethod(), TestCategory("FACET Document Tweaks")>
        Public Sub FixAspectTextTagHierarchyNotAllowedWithTwoUnderlineElements()
            'arrange
            Dim itemDoc As New XmlDocument
            itemDoc.LoadXml(GetExampleItemXmlWithTwoUnderlines.ToString())

            'act
            Dim aspectFixer As New FixAspects
            aspectFixer.Modify(itemDoc, New QTI30DocumentHelper())

            'assert
            Dim expectedFragment As String = "<strong>Dit een een probleem voor QTI22 waarbij <xsi:span style=""TEXT-DECORATION: underline"">underline</xsi:span> is genest direct onder een <xsi:span style=""TEXT-DECORATION: underline"">strong</xsi:span> tag</strong>"
            Assert.IsTrue(itemDoc.OuterXml.Contains(expectedFragment))
        End Sub


        <TestMethod(), TestCategory("FACET Document Tweaks")>
        Public Sub FixAspectTest()
            'arrange
            Dim item As XDocument = <?xml version="1.0" encoding="utf-8"?>
                                    <qti-assessment-item xmlns="http://www.imsglobal.org/xsd/imsqti_v2p1" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:schemaLocation="http://www.imsglobal.org/xsd/imsqti_v2p1 ../controlxsds/depv1p0_qtiitemv2p1_v1p0.xsd" identifier="ITM-TI_Essay" title="TI_Essay" adaptive="false" timeDependent="false">
                                        <qti-response-declaration identifier="VIDEORESPONSE" cardinality="single" base-type="integer"/>
                                        <qti-response-declaration identifier="RESPONSE2" cardinality="single" base-type="string"/>
                                        <qti-response-declaration identifier="RESPONSE" base-type="string" cardinality="single"/>
                                        <qti-response-declaration identifier="RESPONSE3" base-type="string" cardinality="single"/>
                                        <qti-outcome-declaration identifier="SCORE" cardinality="single" base-type="float">
                                            <qti-default-value>
                                                <qti-value>0</qti-value>
                                            </qti-default-value>
                                        </qti-outcome-declaration>
                                        <qti-outcome-declaration identifier="qtiAspectInhoudOutcomeDeclaration" cardinality="single" base-type="integer" view="scorer" normal-maximum="2" normal-minimum="0"/>
                                        <stylesheet href="css/cito_itemstyle.css" type="text/css"/>
                                        <stylesheet href="css/cito_userstyle.css" type="text/css"/>
                                        <stylesheet href="css/cito_generated.css" type="text/css"/>
                                        <qti-item-body class="defaultBody">
                                            <div class="content">
                                                <div>
                                                    <div id="question">
                                                        <p id="c1-id-12">De vraagstelling</p>
                                                    </div>
                                                    <div class="cito_genclass_TI_Essay_1">
                                                        <div>
                                                            <p id="c1-id-12">tekst</p>
                                                        </div>
                                                        <div>
                                                            <qti-extended-text-interaction response-identifier="RESPONSE2" expected-lines="5" expected-length="0"/>
                                                        </div>
                                                    </div>
                                                    <div class="cito_genclass_TI_Essay_2">
                                                        <div>
                                                            <qti-extended-text-interaction response-identifier="RESPONSE3" expected-lines="5" expected-length="0"/>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <qti-rubric-block id="qtiScoringRubricBlock" view="scorer">
                                                <p>Algemene instructie</p>
                                            </qti-rubric-block>
                                            <qti-rubric-block id="qtiAspectInhoudRubricBlock" view="scorer">
                                                <p>dit is een test van een aspect</p>
                                                <div id="VID_a1a3d9e8-3e55-4acf-b636-e72ff570d5f7">
                                                    <qti-media-interaction response-identifier="VIDEORESPONSE" autostart="true" maxPlays="2147483647" min-plays="0" loop="false" id="qtiVideo">
                                                        <object type="video/webm" data="video/TestVideoCES.webm" height="240" width="320"/>
                                                    </qti-media-interaction>
                                                </div>
                                                <p/> <p/> <p/>
                                                <div id="FLSH_ab10f3c8-ae8f-48a6-9466-85a49c258def">
                                                    <qti-custom-interaction response-identifier="RESPONSE">
                                                        <object type="application/x-shockwave-flash" data="flash/CalculatorBasic.swf" height="240" width="320"/>
                                                    </qti-custom-interaction>
                                                </div>
                                            </qti-rubric-block>
                                        </qti-item-body>
                                    </qti-assessment-item>
            Dim itemDoc As New XmlDocument
            itemDoc.LoadXml(item.ToString)
            Dim aspectFixer As New FixAspects

            'act
            aspectFixer.Modify(itemDoc, New QTI30DocumentHelper())
            'assert
            Assert.IsTrue(itemDoc.OuterXml.Contains("qti-custom-interaction") AndAlso itemDoc.OuterXml.Contains("qti-media-interaction"))
        End Sub


        <TestMethod(), TestCategory("FACET Document Tweaks")>
        Public Sub DoMessUpItem()
            Dim item As XDocument = <?xml version="1.0" encoding="utf-8"?>
                                    <qti-assessment-item xmlns="http://www.imsglobal.org/xsd/imsqti_v2p1" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:schemaLocation="http://www.imsglobal.org/xsd/imsqti_v2p1 ../controlxsds/depv1p0_qtiitemv2p1_v1p0.xsd" identifier="ITM-TI_Essay" title="TI_Essay" adaptive="false" timeDependent="false">
                                        <qti-response-declaration identifier="VIDEORESPONSE" cardinality="single" base-type="integer"/>
                                        <qti-response-declaration identifier="RESPONSE2" cardinality="single" base-type="string"/>
                                        <qti-response-declaration identifier="RESPONSE" base-type="string" cardinality="single"/>
                                        <qti-response-declaration identifier="RESPONSE3" base-type="string" cardinality="single"/>
                                        <qti-outcome-declaration identifier="SCORE" cardinality="single" base-type="float">
                                            <qti-default-value>
                                                <qti-value>0</qti-value>
                                            </qti-default-value>
                                        </qti-outcome-declaration>
                                        <qti-outcome-declaration identifier="qtiAspectInhoudOutcomeDeclaration" cardinality="single" base-type="integer" view="scorer" normal-maximum="2" normal-minimum="0"/>
                                        <stylesheet href="css/cito_itemstyle.css" type="text/css"/>
                                        <stylesheet href="css/cito_userstyle.css" type="text/css"/>
                                        <stylesheet href="css/cito_generated.css" type="text/css"/>
                                        <qti-item-body class="defaultBody">
                                            <div class="content">
                                                <div id="VID_a1a3d9e8-3e55-4acf-b636-e72ff570d5f7">
                                                    <qti-media-interaction response-identifier="VIDEORESPONSE" autostart="true" maxPlays="2147483647" min-plays="0" loop="false" id="qtiVideo">
                                                        <object type="video/webm" data="video/TestVideoCES.webm" height="240" width="320"/>
                                                    </qti-media-interaction>
                                                </div>
                                                <p/> <p/> <p/>
                                                <div id="FLSH_ab10f3c8-ae8f-48a6-9466-85a49c258def">
                                                    <qti-custom-interaction response-identifier="RESPONSE">
                                                        <object type="application/x-shockwave-flash" data="flash/CalculatorBasic.swf" height="240" width="320"/>
                                                    </qti-custom-interaction>
                                                </div>
                                                <div>
                                                    <div id="question">
                                                        <p id="c1-id-12">De vraagstelling</p>
                                                    </div>
                                                    <div class="cito_genclass_TI_Essay_1">
                                                        <div>
                                                            <p id="c1-id-12">tekst</p>
                                                        </div>
                                                        <div>
                                                            <qti-extended-text-interaction response-identifier="RESPONSE2" expected-lines="5" expected-length="0"/>
                                                        </div>
                                                    </div>
                                                    <div class="cito_genclass_TI_Essay_2">
                                                        <div>
                                                            <qti-extended-text-interaction response-identifier="RESPONSE3" expected-lines="5" expected-length="0"/>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <qti-rubric-block id="qtiScoringRubricBlock" view="scorer">
                                                <p>Algemene instructie</p>
                                            </qti-rubric-block>
                                            <qti-rubric-block id="qtiAspectInhoudRubricBlock" view="scorer">
                                                <p>dit is een test van een aspect</p>
                                            </qti-rubric-block>
                                        </qti-item-body>
                                    </qti-assessment-item>
            Dim itemDoc As New XmlDocument
            itemDoc.LoadXml(item.ToString)
            Dim aspectFixer As New FixAspects

            'act
            aspectFixer.Modify(itemDoc, New QTI30DocumentHelper())
            'assert
            Assert.IsTrue(itemDoc.OuterXml.Contains("qti-custom-interaction") AndAlso itemDoc.OuterXml.Contains("qti-custom-interaction"))
        End Sub

        Private Function GetExampleItemXmlWithOneUnderline() As XDocument
            Dim result As XDocument = <?xml version="1.0" encoding="utf-8"?>
                                      <qti-assessment-item xmlns="http://www.imsglobal.org/xsd/imsqti_v2p1" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:schemaLocation="http://www.imsglobal.org/xsd/imsqti_v2p1 ../controlxsds/depv1p0_qtiitemv2p1_v1p0.xsd" identifier="ITM-TI_Essay" title="TI_Essay" adaptive="false" timeDependent="false">
                                          <qti-response-declaration identifier="VIDEORESPONSE" cardinality="single" base-type="integer"/>
                                          <qti-response-declaration identifier="RESPONSE2" cardinality="single" base-type="string"/>
                                          <qti-response-declaration identifier="RESPONSE" base-type="string" cardinality="single"/>
                                          <qti-response-declaration identifier="RESPONSE3" base-type="string" cardinality="single"/>
                                          <qti-outcome-declaration identifier="SCORE" cardinality="single" base-type="float">
                                              <qti-default-value>
                                                  <qti-value>0</qti-value>
                                              </qti-default-value>
                                          </qti-outcome-declaration>
                                          <qti-outcome-declaration identifier="qtiAspectInhoudOutcomeDeclaration" cardinality="single" base-type="integer" view="scorer" normal-maximum="2" normal-minimum="0"/>
                                          <qti-item-body class="defaultBody">
                                              <div class="content">
                                                  <div>
                                                      <div id="question">
                                                          <p id="c1-id-12">De vraagstelling</p>
                                                      </div>
                                                      <div class="cito_genclass_TI_Essay_1">
                                                          <div>
                                                              <p id="c1-id-12">tekst</p>
                                                          </div>
                                                          <div>
                                                              <qti-extended-text-interaction response-identifier="RESPONSE2" expected-lines="5" expected-length="0"/>
                                                          </div>
                                                      </div>
                                                      <div class="cito_genclass_TI_Essay_2">
                                                          <div>
                                                              <qti-extended-text-interaction response-identifier="RESPONSE3" expected-lines="5" expected-length="0"/>
                                                          </div>
                                                      </div>
                                                  </div>
                                              </div>
                                              <qti-rubric-block id="qtiScoringRubricBlock" view="scorer">
                                                  <p>Algemene instructie</p>
                                              </qti-rubric-block>
                                              <qti-rubric-block id="qtiAspectInhoudRubricBlock" view="scorer">
                                                  <p>dit is een test van een aspect</p>
                                                  <strong>Dit een een probleem voor QTI22 waarbij <u>underline</u> is genest direct onder een strong tag</strong>
                                                  <p/> <p/> <p/>
                                              </qti-rubric-block>
                                          </qti-item-body>
                                      </qti-assessment-item>
            Return result
        End Function

        Private Function GetExampleItemXmlWithTwoUnderlines() As XDocument
            Dim result As XDocument = <?xml version="1.0" encoding="utf-8"?>
                                      <qti-assessment-item xmlns="http://www.imsglobal.org/xsd/imsqti_v2p1" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:schemaLocation="http://www.imsglobal.org/xsd/imsqti_v2p1 ../controlxsds/depv1p0_qtiitemv2p1_v1p0.xsd" identifier="ITM-TI_Essay" title="TI_Essay" adaptive="false" timeDependent="false">
                                          <qti-response-declaration identifier="VIDEORESPONSE" cardinality="single" base-type="integer"/>
                                          <qti-response-declaration identifier="RESPONSE2" cardinality="single" base-type="string"/>
                                          <qti-response-declaration identifier="RESPONSE" base-type="string" cardinality="single"/>
                                          <qti-response-declaration identifier="RESPONSE3" base-type="string" cardinality="single"/>
                                          <qti-outcome-declaration identifier="SCORE" cardinality="single" base-type="float">
                                              <qti-default-value>
                                                  <qti-value>0</qti-value>
                                              </qti-default-value>
                                          </qti-outcome-declaration>
                                          <qti-outcome-declaration identifier="qtiAspectInhoudOutcomeDeclaration" cardinality="single" base-type="integer" view="scorer" normal-maximum="2" normal-minimum="0"/>
                                          <qti-item-body class="defaultBody">
                                              <div class="content">
                                                  <div>
                                                      <div id="question">
                                                          <p id="c1-id-12">De vraagstelling</p>
                                                      </div>
                                                      <div class="cito_genclass_TI_Essay_1">
                                                          <div>
                                                              <p id="c1-id-12">tekst</p>
                                                          </div>
                                                          <div>
                                                              <qti-extended-text-interaction response-identifier="RESPONSE2" expected-lines="5" expected-length="0"/>
                                                          </div>
                                                      </div>
                                                      <div class="cito_genclass_TI_Essay_2">
                                                          <div>
                                                              <qti-extended-text-interaction response-identifier="RESPONSE3" expected-lines="5" expected-length="0"/>
                                                          </div>
                                                      </div>
                                                  </div>
                                              </div>
                                              <qti-rubric-block id="qtiScoringRubricBlock" view="scorer">
                                                  <p>Algemene instructie</p>
                                              </qti-rubric-block>
                                              <qti-rubric-block id="qtiAspectInhoudRubricBlock" view="scorer">
                                                  <p>dit is een test van een aspect</p>
                                                  <strong>Dit een een probleem voor QTI22 waarbij <u>underline</u> is genest direct onder een <u>strong</u> tag</strong>
                                                  <p/> <p/> <p/>
                                              </qti-rubric-block>
                                          </qti-item-body>
                                      </qti-assessment-item>
            Return result
        End Function

    End Class
End Namespace