
Imports System.Xml
Imports Questify.Builder.Logic.QTI.Helpers.QTI_Base.QTICompliantHelper
Imports Questify.Builder.Logic.QTI.Helpers.QTI22

Namespace QTI22.Helpers
    <TestClass()> Public Class QTI22FixAspectTests

        <TestMethod(), TestCategory("FACET Document Tweaks")>
        Public Sub FixAspectTextTagHierarchyNotAllowed()
            'arrange
            Dim itemDoc As New XmlDocument
            itemDoc.LoadXml(GetExampleItemXmlWithOneUnderline.ToString())

            'act
            Dim aspectFixer As New FixAspects
            aspectFixer.Modify(itemDoc, New QTI22DocumentHelper())

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
            aspectFixer.Modify(itemDoc, New QTI22DocumentHelper())

            'assert
            Dim expectedFragment As String = "<strong>Dit een een probleem voor QTI22 waarbij <xsi:span style=""TEXT-DECORATION: underline"">underline</xsi:span> is genest direct onder een <xsi:span style=""TEXT-DECORATION: underline"">strong</xsi:span> tag</strong>"
            Assert.IsTrue(itemDoc.OuterXml.Contains(expectedFragment))
        End Sub


        <TestMethod(), TestCategory("FACET Document Tweaks")>
        Public Sub FixAspectTest()
            'arrange
            Dim item As XDocument = <?xml version="1.0" encoding="utf-8"?>
                                    <assessmentItem xmlns="http://www.imsglobal.org/xsd/imsqti_v2p1" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:schemaLocation="http://www.imsglobal.org/xsd/imsqti_v2p1 ../controlxsds/depv1p0_qtiitemv2p1_v1p0.xsd" identifier="ITM-TI_Essay" title="TI_Essay" adaptive="false" timeDependent="false">
                                        <responseDeclaration identifier="VIDEORESPONSE" cardinality="single" baseType="integer"/>
                                        <responseDeclaration identifier="RESPONSE2" cardinality="single" baseType="string"/>
                                        <responseDeclaration identifier="RESPONSE" baseType="string" cardinality="single"/>
                                        <responseDeclaration identifier="RESPONSE3" baseType="string" cardinality="single"/>
                                        <outcomeDeclaration identifier="SCORE" cardinality="single" baseType="float">
                                            <defaultValue>
                                                <value>0</value>
                                            </defaultValue>
                                        </outcomeDeclaration>
                                        <outcomeDeclaration identifier="qtiAspectInhoudOutcomeDeclaration" cardinality="single" baseType="integer" view="scorer" normalMaximum="2" normalMinimum="0"/>
                                        <stylesheet href="css/cito_itemstyle.css" type="text/css"/>
                                        <stylesheet href="css/cito_userstyle.css" type="text/css"/>
                                        <stylesheet href="css/cito_generated.css" type="text/css"/>
                                        <itemBody class="defaultBody">
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
                                                            <extendedTextInteraction responseIdentifier="RESPONSE2" expectedLines="5" expectedLength="0"/>
                                                        </div>
                                                    </div>
                                                    <div class="cito_genclass_TI_Essay_2">
                                                        <div>
                                                            <extendedTextInteraction responseIdentifier="RESPONSE3" expectedLines="5" expectedLength="0"/>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <rubricBlock id="qtiScoringRubricBlock" view="scorer">
                                                <p>Algemene instructie</p>
                                            </rubricBlock>
                                            <rubricBlock id="qtiAspectInhoudRubricBlock" view="scorer">
                                                <p>dit is een test van een aspect</p>
                                                <div id="VID_a1a3d9e8-3e55-4acf-b636-e72ff570d5f7">
                                                    <mediaInteraction responseIdentifier="VIDEORESPONSE" autostart="true" maxPlays="2147483647" minPlays="0" loop="false" id="qtiVideo">
                                                        <object type="video/webm" data="video/TestVideoCES.webm" height="240" width="320"/>
                                                    </mediaInteraction>
                                                </div>
                                                <p/> <p/> <p/>
                                                <div id="FLSH_ab10f3c8-ae8f-48a6-9466-85a49c258def">
                                                    <customInteraction responseIdentifier="RESPONSE">
                                                        <object type="application/x-shockwave-flash" data="flash/CalculatorBasic.swf" height="240" width="320"/>
                                                    </customInteraction>
                                                </div>
                                            </rubricBlock>
                                        </itemBody>
                                    </assessmentItem>
            Dim itemDoc As New XmlDocument
            itemDoc.LoadXml(item.ToString)
            Dim aspectFixer As New FixAspects

            'act
            aspectFixer.Modify(itemDoc, New QTI22DocumentHelper())
            'assert
            Assert.IsTrue(itemDoc.OuterXml.Contains("customInteraction") AndAlso itemDoc.OuterXml.Contains("mediaInteraction"))
        End Sub


        <TestMethod(), TestCategory("FACET Document Tweaks")>
        Public Sub DoMessUpItem()
            Dim item As XDocument = <?xml version="1.0" encoding="utf-8"?>
                                    <assessmentItem xmlns="http://www.imsglobal.org/xsd/imsqti_v2p1" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:schemaLocation="http://www.imsglobal.org/xsd/imsqti_v2p1 ../controlxsds/depv1p0_qtiitemv2p1_v1p0.xsd" identifier="ITM-TI_Essay" title="TI_Essay" adaptive="false" timeDependent="false">
                                        <responseDeclaration identifier="VIDEORESPONSE" cardinality="single" baseType="integer"/>
                                        <responseDeclaration identifier="RESPONSE2" cardinality="single" baseType="string"/>
                                        <responseDeclaration identifier="RESPONSE" baseType="string" cardinality="single"/>
                                        <responseDeclaration identifier="RESPONSE3" baseType="string" cardinality="single"/>
                                        <outcomeDeclaration identifier="SCORE" cardinality="single" baseType="float">
                                            <defaultValue>
                                                <value>0</value>
                                            </defaultValue>
                                        </outcomeDeclaration>
                                        <outcomeDeclaration identifier="qtiAspectInhoudOutcomeDeclaration" cardinality="single" baseType="integer" view="scorer" normalMaximum="2" normalMinimum="0"/>
                                        <stylesheet href="css/cito_itemstyle.css" type="text/css"/>
                                        <stylesheet href="css/cito_userstyle.css" type="text/css"/>
                                        <stylesheet href="css/cito_generated.css" type="text/css"/>
                                        <itemBody class="defaultBody">
                                            <div class="content">
                                                <div id="VID_a1a3d9e8-3e55-4acf-b636-e72ff570d5f7">
                                                    <mediaInteraction responseIdentifier="VIDEORESPONSE" autostart="true" maxPlays="2147483647" minPlays="0" loop="false" id="qtiVideo">
                                                        <object type="video/webm" data="video/TestVideoCES.webm" height="240" width="320"/>
                                                    </mediaInteraction>
                                                </div>
                                                <p/> <p/> <p/>
                                                <div id="FLSH_ab10f3c8-ae8f-48a6-9466-85a49c258def">
                                                    <customInteraction responseIdentifier="RESPONSE">
                                                        <object type="application/x-shockwave-flash" data="flash/CalculatorBasic.swf" height="240" width="320"/>
                                                    </customInteraction>
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
                                                            <extendedTextInteraction responseIdentifier="RESPONSE2" expectedLines="5" expectedLength="0"/>
                                                        </div>
                                                    </div>
                                                    <div class="cito_genclass_TI_Essay_2">
                                                        <div>
                                                            <extendedTextInteraction responseIdentifier="RESPONSE3" expectedLines="5" expectedLength="0"/>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <rubricBlock id="qtiScoringRubricBlock" view="scorer">
                                                <p>Algemene instructie</p>
                                            </rubricBlock>
                                            <rubricBlock id="qtiAspectInhoudRubricBlock" view="scorer">
                                                <p>dit is een test van een aspect</p>
                                            </rubricBlock>
                                        </itemBody>
                                    </assessmentItem>
            Dim itemDoc As New XmlDocument
            itemDoc.LoadXml(item.ToString)
            Dim aspectFixer As New FixAspects

            'act
            aspectFixer.Modify(itemDoc, New QTI22DocumentHelper())
            'assert
            Assert.IsTrue(itemDoc.OuterXml.Contains("customInteraction") AndAlso itemDoc.OuterXml.Contains("customInteraction"))
        End Sub

        Private Function GetExampleItemXmlWithOneUnderline() As XDocument
            Dim result As XDocument = <?xml version="1.0" encoding="utf-8"?>
                                      <assessmentItem xmlns="http://www.imsglobal.org/xsd/imsqti_v2p1" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:schemaLocation="http://www.imsglobal.org/xsd/imsqti_v2p1 ../controlxsds/depv1p0_qtiitemv2p1_v1p0.xsd" identifier="ITM-TI_Essay" title="TI_Essay" adaptive="false" timeDependent="false">
                                          <responseDeclaration identifier="VIDEORESPONSE" cardinality="single" baseType="integer"/>
                                          <responseDeclaration identifier="RESPONSE2" cardinality="single" baseType="string"/>
                                          <responseDeclaration identifier="RESPONSE" baseType="string" cardinality="single"/>
                                          <responseDeclaration identifier="RESPONSE3" baseType="string" cardinality="single"/>
                                          <outcomeDeclaration identifier="SCORE" cardinality="single" baseType="float">
                                              <defaultValue>
                                                  <value>0</value>
                                              </defaultValue>
                                          </outcomeDeclaration>
                                          <outcomeDeclaration identifier="qtiAspectInhoudOutcomeDeclaration" cardinality="single" baseType="integer" view="scorer" normalMaximum="2" normalMinimum="0"/>
                                          <itemBody class="defaultBody">
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
                                                              <extendedTextInteraction responseIdentifier="RESPONSE2" expectedLines="5" expectedLength="0"/>
                                                          </div>
                                                      </div>
                                                      <div class="cito_genclass_TI_Essay_2">
                                                          <div>
                                                              <extendedTextInteraction responseIdentifier="RESPONSE3" expectedLines="5" expectedLength="0"/>
                                                          </div>
                                                      </div>
                                                  </div>
                                              </div>
                                              <rubricBlock id="qtiScoringRubricBlock" view="scorer">
                                                  <p>Algemene instructie</p>
                                              </rubricBlock>
                                              <rubricBlock id="qtiAspectInhoudRubricBlock" view="scorer">
                                                  <p>dit is een test van een aspect</p>
                                                  <strong>Dit een een probleem voor QTI22 waarbij <u>underline</u> is genest direct onder een strong tag</strong>
                                                  <p/> <p/> <p/>
                                              </rubricBlock>
                                          </itemBody>
                                      </assessmentItem>
            Return result
        End Function

        Private Function GetExampleItemXmlWithTwoUnderlines() As XDocument
            Dim result As XDocument = <?xml version="1.0" encoding="utf-8"?>
                                      <assessmentItem xmlns="http://www.imsglobal.org/xsd/imsqti_v2p1" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:schemaLocation="http://www.imsglobal.org/xsd/imsqti_v2p1 ../controlxsds/depv1p0_qtiitemv2p1_v1p0.xsd" identifier="ITM-TI_Essay" title="TI_Essay" adaptive="false" timeDependent="false">
                                          <responseDeclaration identifier="VIDEORESPONSE" cardinality="single" baseType="integer"/>
                                          <responseDeclaration identifier="RESPONSE2" cardinality="single" baseType="string"/>
                                          <responseDeclaration identifier="RESPONSE" baseType="string" cardinality="single"/>
                                          <responseDeclaration identifier="RESPONSE3" baseType="string" cardinality="single"/>
                                          <outcomeDeclaration identifier="SCORE" cardinality="single" baseType="float">
                                              <defaultValue>
                                                  <value>0</value>
                                              </defaultValue>
                                          </outcomeDeclaration>
                                          <outcomeDeclaration identifier="qtiAspectInhoudOutcomeDeclaration" cardinality="single" baseType="integer" view="scorer" normalMaximum="2" normalMinimum="0"/>
                                          <itemBody class="defaultBody">
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
                                                              <extendedTextInteraction responseIdentifier="RESPONSE2" expectedLines="5" expectedLength="0"/>
                                                          </div>
                                                      </div>
                                                      <div class="cito_genclass_TI_Essay_2">
                                                          <div>
                                                              <extendedTextInteraction responseIdentifier="RESPONSE3" expectedLines="5" expectedLength="0"/>
                                                          </div>
                                                      </div>
                                                  </div>
                                              </div>
                                              <rubricBlock id="qtiScoringRubricBlock" view="scorer">
                                                  <p>Algemene instructie</p>
                                              </rubricBlock>
                                              <rubricBlock id="qtiAspectInhoudRubricBlock" view="scorer">
                                                  <p>dit is een test van een aspect</p>
                                                  <strong>Dit een een probleem voor QTI22 waarbij <u>underline</u> is genest direct onder een <u>strong</u> tag</strong>
                                                  <p/> <p/> <p/>
                                              </rubricBlock>
                                          </itemBody>
                                      </assessmentItem>
            Return result
        End Function

    End Class
End Namespace