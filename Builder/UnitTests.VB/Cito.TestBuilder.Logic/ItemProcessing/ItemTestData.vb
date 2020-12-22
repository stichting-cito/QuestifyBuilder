
Imports System.Xml.Linq
Imports Questify.Builder.UnitTests.Framework
Imports Questify.Builder.UnitTests.Framework.FakeAppTemplate

Partial Public Class ItemTestData

    Public Shared Sub AddItemTemplatesAndControlTemplates()
        With FakeDal.Add
            .ItemTemplate("Cito.Generic.GraphicGapMatch.DC", Sub(ilt) SetResourceIdAndXmlAsBinData(ilt, ilt_ggm_dc)).
                DependsOn.ControlTemplate("Cito.Generic.Interaction.GraphicGapMatch", Sub(ct) SetResourceIdAndXmlAsBinData(ct, ct_ggm))

            .ItemTemplate("Cito.Generic.GraphicGapMatch.SC", Sub(ilt) SetResourceIdAndXmlAsBinData(ilt, ilt_ggm_sc)).
                DependsOn.ControlTemplate("Cito.Generic.Interaction.GraphicGapMatch")

            .ItemTemplate("Cito.Generic.Tabular.DC", Sub(ilt) SetResourceIdAndXmlAsBinData(ilt, ilt_tabular_dc)).
                DependsOn.ControlTemplate("Cito.Generic.Interaction.MC", Sub(ct) SetResourceIdAndXmlAsBinData(ct, ct_mc_for_tabular))

            .ItemTemplate("Cito.Generic.MC.SC", Sub(ilt) SetResourceIdAndXmlAsBinData(ilt, ilt_mc_sc_for_tabular)).
                DependsOn.ControlTemplate("Cito.Generic.Interaction.MC")

            .ItemTemplate("ilt.html", Sub(i)
                                          i.SetXmlAsBinData(ilt_html_ilt)
                                          i.ResourceId = Guid.NewGuid()
                                      End Sub).
                DependsOn.ControlTemplate("min.html", Sub(c)
                                                          c.SetXmlAsBinData(min_html_ctrl)
                                                          c.ResourceId = Guid.NewGuid()
                                                      End Sub)

            .ItemTemplate("InlineHottextCorrectionLayoutTemplate", Sub(ilt) SetResourceIdAndXmlAsBinData(ilt, ilt_inlinehottextcorrection)).
                DependsOn.ControlTemplate("InlineHottext", Sub(ct) SetResourceIdAndXmlAsBinData(ct, ct_inlinehottext))

            .ItemTemplate("InlineHottextLayoutTemplate", Sub(ilt) SetResourceIdAndXmlAsBinData(ilt, ilt_inlinehottext)).
                DependsOn.ControlTemplate("InlineHottext")

            .ItemTemplate("Cito.Generic.Hottext.Corrections.DC", Sub(ilt) SetResourceIdAndXmlAsBinData(ilt, ilt_hottext_corrections_dc)).
                DependsOn.ControlTemplate("Cito.Generic.Interaction.Hottext", Sub(ct) SetResourceIdAndXmlAsBinData(ct, ct_hottext))

            .ItemTemplate("Cito.Generic.Hottext.DC", Sub(ilt) SetResourceIdAndXmlAsBinData(ilt, ilt_hottext_dc)).
                DependsOn.ControlTemplate("Cito.Generic.Interaction.Hottext")

            .GenericResource("GenericImage", Nothing)


            .ItemTemplate("ilt.htmlErr", Sub(i)
                                             i.SetXmlAsBinData(ilt_htmlErr_ilt)
                                             i.ResourceId = Guid.NewGuid()
                                         End Sub).
               DependsOn.ControlTemplate("min.htmlErr", Sub(c)
                                                            c.SetXmlAsBinData(min_htmlErr_ctrl)
                                                            c.ResourceId = Guid.NewGuid()
                                                        End Sub)

            .ItemTemplate("ilt.integer", Sub(i)
                                             i.ResourceId = Guid.NewGuid()
                                             i.SetXmlAsBinData(ilt_integer_ilt)
                                         End Sub).
                DependsOn.ControlTemplate("min.integer", Sub(c)
                                                             c.ResourceId = Guid.NewGuid()
                                                             c.SetXmlAsBinData(min_integer_ctrl)
                                                         End Sub)

            .ItemTemplate("ilt.2xhtml", Sub(i)
                                            i.SetXmlAsBinData(ilt_2xhtml_ilt)
                                            i.ResourceId = Guid.NewGuid()
                                        End Sub).
                DependsOn.ControlTemplate("min.2xhtml", Sub(c)
                                                            c.SetXmlAsBinData(min_2xhtml_ctrl)
                                                            c.ResourceId = Guid.NewGuid()
                                                        End Sub)

            .ItemTemplate("ilt.compare.boolean1", Sub(i)
                                                      i.SetXmlAsBinData(ilt_compare_boolean1)
                                                      i.ResourceId = Guid.NewGuid()
                                                  End Sub).
                DependsOn.ControlTemplate("min.compare.boolean1", Sub(c)
                                                                      c.SetXmlAsBinData(min_compare_boolean_ctrl1)
                                                                      c.ResourceId = Guid.NewGuid()
                                                                  End Sub)

            .ItemTemplate("ilt.compare.boolean2", Sub(i)
                                                      i.SetXmlAsBinData(ilt_compare_boolean2)
                                                      i.ResourceId = Guid.NewGuid()
                                                  End Sub).
                DependsOn.ControlTemplate("min.compare.boolean2", Sub(c)
                                                                      c.ResourceId = Guid.NewGuid()
                                                                      c.SetXmlAsBinData(min_compare_boolean_ctrl2)
                                                                  End Sub)

            .ItemTemplate("ilt.compare.integer1", Sub(i)
                                                      i.ResourceId = Guid.NewGuid()
                                                      i.SetXmlAsBinData(ilt_compare_integer1)
                                                  End Sub).
                DependsOn.ControlTemplate("min.compare.integer1", Sub(c)
                                                                      c.ResourceId = Guid.NewGuid()
                                                                      c.SetXmlAsBinData(min_compare_integer_ctrl1)
                                                                  End Sub)

            .ItemTemplate("ilt.compare.integer2", Sub(i)
                                                      i.ResourceId = Guid.NewGuid()
                                                      i.SetXmlAsBinData(ilt_compare_integer2)
                                                  End Sub).
                DependsOn.ControlTemplate("min.compare.integer2", Sub(c)
                                                                      c.ResourceId = Guid.NewGuid()
                                                                      c.SetXmlAsBinData(min_compare_integer_ctrl2)
                                                                  End Sub)

            .ItemTemplate("ilt.compare.collection1", Sub(i)
                                                         i.ResourceId = Guid.NewGuid()
                                                         i.SetXmlAsBinData(ilt_compare_collection1)
                                                     End Sub).
                DependsOn.ControlTemplate("min.compare.collection1", Sub(c)
                                                                         c.ResourceId = Guid.NewGuid()
                                                                         c.SetXmlAsBinData(min_compare_collection_ctrl1)
                                                                     End Sub)

            .ItemTemplate("ilt.compare.collection2", Sub(i)
                                                         i.ResourceId = Guid.NewGuid()
                                                         i.SetXmlAsBinData(ilt_compare_collection2)
                                                     End Sub).
                DependsOn.ControlTemplate("min.compare.collection2", Sub(c)
                                                                         c.ResourceId = Guid.NewGuid()
                                                                         c.SetXmlAsBinData(min_compare_collection_ctrl2)
                                                                     End Sub)

            .ItemTemplate("ilt.compare.xhtml1", Sub(i)
                                                    i.ResourceId = Guid.NewGuid()
                                                    i.SetXmlAsBinData(ilt_compare_xhtml1)
                                                End Sub).
                DependsOn.ControlTemplate("min.compare.xhtml1", Sub(c)
                                                                    c.ResourceId = Guid.NewGuid()
                                                                    c.SetXmlAsBinData(min_compare_xhtml_ctrl1)
                                                                End Sub)

            .ItemTemplate("ilt.compare.xhtml2", Sub(i)
                                                    i.SetXmlAsBinData(ilt_compare_xhtml2)
                                                    i.ResourceId = Guid.NewGuid()
                                                End Sub).
            DependsOn.ControlTemplate("min.compare.xhtml2", Sub(c)
                                                                c.ResourceId = Guid.NewGuid()
                                                                c.SetXmlAsBinData(min_compare_xhtml_ctrl2)
                                                            End Sub)

            .ItemTemplate("ilt.compare.listed1", Sub(i)
                                                     i.SetXmlAsBinData(ilt_compare_listed1)
                                                     i.ResourceId = Guid.NewGuid()
                                                 End Sub).
                DependsOn.ControlTemplate("min.compare.listed1", Sub(c)
                                                                     c.SetXmlAsBinData(min_compare_listed_ctrl1)
                                                                     c.ResourceId = Guid.NewGuid()
                                                                 End Sub)

            .ItemTemplate("ilt.compare.listed2", Sub(i)
                                                     i.SetXmlAsBinData(ilt_compare_listed2)
                                                     i.ResourceId = Guid.NewGuid()
                                                 End Sub).
                DependsOn.ControlTemplate("min.compare.listed2", Sub(c)
                                                                     c.SetXmlAsBinData(min_compare_listed_ctrl2)
                                                                     c.ResourceId = Guid.NewGuid()
                                                                 End Sub)

            .ItemTemplate("ilt.mc.dc", Sub(i)
                                           i.SetXmlAsBinData(ilt_mc_dc)
                                           i.ResourceId = Guid.NewGuid()
                                       End Sub).
                DependsOn.ControlTemplate("ct.mc", Sub(c)
                                                       c.SetXmlAsBinData(ct_mc)
                                                       c.ResourceId = Guid.NewGuid()
                                                   End Sub)

            .ItemTemplate("ilt.mc.sc", Sub(i)
                                           i.SetXmlAsBinData(ilt_mc_sc)
                                           i.ResourceId = Guid.NewGuid()
                                       End Sub).
                DependsOn.ControlTemplate("ct.mc")

            .ItemTemplate("ilt.mr.dc", Sub(i)
                                           i.SetXmlAsBinData(ilt_mr_dc)
                                           i.ResourceId = Guid.NewGuid()
                                       End Sub).
                DependsOn.ControlTemplate("ct.mc")

            .ItemTemplate("ilt.essay.sc", Sub(i)
                                              i.SetXmlAsBinData(ilt_essay_sc)
                                              i.ResourceId = Guid.NewGuid()
                                          End Sub).
                DependsOn.ControlTemplate("ct.essay", Sub(c)
                                                          c.SetXmlAsBinData(ct_essay)
                                                          c.ResourceId = Guid.NewGuid()
                                                      End Sub)

            .ItemTemplate("ilt.essay.dc", Sub(i)
                                              i.SetXmlAsBinData(ilt_essay_dc)
                                              i.ResourceId = Guid.NewGuid()
                                          End Sub).
                DependsOn.ControlTemplate("ct.essay")

        End With

    End Sub

    Public Shared info_AsmntItem As XElement = <assessmentItem xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" identifier="code" itemid="testId" title="title" layoutTemplateSrc="ilt.html">
                                                   <solution>
                                                       <keyFindings/>
                                                       <aspectReferences/>
                                                   </solution>
                                                   <parameters>
                                                       <parameterSet id="invoer">
                                                           <xhtmlparameter name="xhtml">
                                                               <p id="c1-id-8" xmlns="http://www.w3.org/1999/xhtml">Dit is tekst.</p>
                                                           </xhtmlparameter>
                                                       </parameterSet>
                                                   </parameters>
                                               </assessmentItem>

    Public Shared ReadOnly mc_dc_item As XElement = <assessmentItem xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" identifier="code" itemid="testId" title="title" layoutTemplateSrc="ilt.mc.dc">
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
                                                                <booleanparameter name="dualColumnLayout">True</booleanparameter>
                                                                <integerparameter name="testInteger">15</integerparameter>
                                                                <xhtmlparameter name="leftBody">
                                                                    <p id="c1-id-10" xmlns="http://www.w3.org/1999/xhtml">Dit is de leftBody</p>
                                                                </xhtmlparameter>
                                                                <xhtmlparameter name="itemQuestion">
                                                                    <p id="c1-id-10" xmlns="http://www.w3.org/1999/xhtml">Dit is de question(zonder inline elementen nog)</p>
                                                                </xhtmlparameter>
                                                                <integerparameter name="multiChoiceType">1</integerparameter>
                                                                <listedparameter name="expectedAnswers">1</listedparameter>
                                                                <multichoicescoringparameter name="multiChoiceScoring" ControllerId="mc" findingOverride="mc" minChoices="1" maxChoices="1" multiChoice="Radio">
                                                                    <definition id="">
                                                                        <xhtmlparameter name="mcOption"/>
                                                                        <xhtmlparameter name="mcOption2"/>
                                                                    </definition>
                                                                    <subparameterset id="A">
                                                                        <xhtmlparameter name="mcOption">
                                                                            <p id="c1-id-10" xmlns="http://www.w3.org/1999/xhtml">Antwoord A</p>
                                                                        </xhtmlparameter>
                                                                        <xhtmlparameter name="mcOption2"/>
                                                                    </subparameterset>
                                                                    <subparameterset id="B">
                                                                        <xhtmlparameter name="mcOption">
                                                                            <p id="c1-id-10" xmlns="http://www.w3.org/1999/xhtml">Antwoord B</p>
                                                                        </xhtmlparameter>
                                                                        <xhtmlparameter name="mcOption2"/>
                                                                    </subparameterset>
                                                                    <subparameterset id="C">
                                                                        <xhtmlparameter name="mcOption">
                                                                            <p id="c1-id-10" xmlns="http://www.w3.org/1999/xhtml">Antwoord B</p>
                                                                        </xhtmlparameter>
                                                                        <xhtmlparameter name="mcOption2"/>
                                                                    </subparameterset>
                                                                </multichoicescoringparameter>
                                                            </parameterSet>
                                                        </parameters>
                                                    </assessmentItem>

    Public Shared ReadOnly itm_essay_sc As XElement = <assessmentItem xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" identifier="code" itemid="testId" title="title" layoutTemplateSrc="ilt.essay.sc">
                                                          <solution>
                                                              <keyFindings/>
                                                              <aspectReferences/>
                                                          </solution>
                                                          <parameters>
                                                              <parameterSet id="Answers">
                                                                  <booleanparameter name="dualColumnLayout">false</booleanparameter>
                                                                  <collectionparameter name="answerFields">
                                                                      <subparameterset id="1">
                                                                          <booleanparameter name="showAnswerIntro">True</booleanparameter>
                                                                          <xhtmlparameter name="answerIntro">
                                                                              <p id="id-1">Intro 1</p>
                                                                          </xhtmlparameter>
                                                                      </subparameterset>
                                                                      <subparameterset id="2">
                                                                          <booleanparameter name="showAnswerIntro">True</booleanparameter>
                                                                          <xhtmlparameter name="answerIntro">
                                                                              <p id="id-2">Intro 2</p>
                                                                          </xhtmlparameter>
                                                                      </subparameterset>
                                                                      <definition id="">
                                                                          <booleanparameter name="showAnswerIntro"/>
                                                                          <xhtmlparameter name="answerIntro"/>
                                                                      </definition>
                                                                  </collectionparameter>
                                                              </parameterSet>
                                                          </parameters>
                                                      </assessmentItem>


    Public Shared ReadOnly ilt_essay_sc As XElement = <Template xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" definitionVersion="1">
                                                          <Description></Description>
                                                          <Settings>
                                                              <DesignerSetting key="sort">False</DesignerSetting>
                                                          </Settings>
                                                          <Targets>
                                                              <Target xsi:type="ItemLayoutTemplateTarget" enabled="true" name="ces">
                                                                  <Description></Description>
                                                                  <Template>
                                                                      <![CDATA[
                                                                      <html>
                                                                          <head>
                                                                              <title></title>
                                                                          </head>
                                                                          <body>
                                                                              <cito:control xmlns:cito="http://www.cito.nl/citotester" id="Answers" type="ct.essay" />
                                                                          </body>
                                                                      </html>
]]>
                                                                  </Template>
                                                              </Target>
                                                          </Targets>
                                                      </Template>

    Public Shared ReadOnly ilt_essay_dc As XElement = <Template xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" definitionVersion="1">
                                                          <Description></Description>
                                                          <Settings>
                                                              <DesignerSetting key="sort">False</DesignerSetting>
                                                          </Settings>
                                                          <Targets>
                                                              <Target xsi:type="ItemLayoutTemplateTarget" enabled="true" name="ces">
                                                                  <Description></Description>
                                                                  <Template>
                                                                      <![CDATA[
                                                                      <html>
                                                                          <head>
                                                                              <title></title>
                                                                          </head>
                                                                          <body>
                                                                              <cito:control xmlns:cito="http://www.cito.nl/citotester" id="Answers" type="ct.essay">
                                                                                  <parameter name="dualColumnLayout">
                                                                                      <designersetting key="defaultvalue">true</designersetting>
                                                                                  </parameter>
                                                                                  <parameter name="answerFields">
                                                                                      <definition>
                                                                                          <parameter name="showAnswerIntro">
                                                                                              <designersetting key="visible">false</designersetting>
                                                                                              <designersetting key="defaultvalue">false</designersetting>
                                                                                          </parameter>
                                                                                          <parameter name="answerIntro">
                                                                                              <designersetting key="visible">false</designersetting>
                                                                                          </parameter>
                                                                                      </definition>
                                                                                  </parameter>
                                                                              </cito:control>
                                                                          </body>
                                                                      </html>
]]>
                                                                  </Template>
                                                              </Target>
                                                          </Targets>
                                                      </Template>

    Public Shared ReadOnly ct_essay As XElement = <Template xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" definitionVersion="1">
                                                      <Description></Description>
                                                      <Targets>
                                                          <Target xsi:type="ControlTemplateTarget" enabled="true" name="ces">
                                                              <Description></Description>
                                                              <Template>
                                                                  <![CDATA[
                                                                ]]>
                                                              </Template>
                                                          </Target>
                                                      </Targets>
                                                      <SharedFunctions>
                                                      </SharedFunctions>
                                                      <SharedParameterSet id="">
                                                          <booleanparameter name="dualColumnLayout">
                                                              <designersetting key="visible">false</designersetting>
                                                              <designersetting key="defaultvalue">false</designersetting>
                                                          </booleanparameter>
                                                          <collectionparameter name="answerFields">
                                                              <designersetting key="label"></designersetting>
                                                              <designersetting key="itemcountlabel">Aantal antwoordvelden</designersetting>
                                                              <designersetting key="minimumLength">0</designersetting>
                                                              <designersetting key="maximumLength">12</designersetting>
                                                              <designersetting key="subsetidentifiers">Numeric</designersetting>
                                                              <designersetting key="required">false</designersetting>
                                                              <definition>
                                                                  <booleanparameter name="showAnswerIntro">
                                                                      <designersetting key="visible">true</designersetting>
                                                                      <designersetting key="defaultvalue">true</designersetting>
                                                                  </booleanparameter>
                                                                  <xhtmlparameter name="answerIntro">
                                                                      <designersetting key="visible">true</designersetting>
                                                                      <designersetting key="label">Inleidende media</designersetting>
                                                                  </xhtmlparameter>
                                                              </definition>
                                                          </collectionparameter>
                                                      </SharedParameterSet>
                                                  </Template>



    Friend Shared ReadOnly itm_ggm_dc As XElement = <assessmentItem xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" identifier="TestItemIteration12_Original" itemid="testId" title="TestItemIteration12_Original" layoutTemplateSrc="Cito.Generic.GraphicGapMatch.DC">
                                                        <solution>
                                                            <keyFindings>
                                                                <keyFinding id="gapMatchController" scoringMethod="Dichotomous">
                                                                    <keyFact id="A-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                        <keyValue domain="A-gapMatchController" occur="1">
                                                                            <stringValue>
                                                                                <typedValue>A</typedValue>
                                                                            </stringValue>
                                                                        </keyValue>
                                                                    </keyFact>
                                                                    <keyFact id="B-gapMatchController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                        <keyValue domain="B-gapMatchController" occur="1">
                                                                            <stringValue>
                                                                                <typedValue>B</typedValue>
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
                                                                <booleanparameter name="dualColumnLayout">True</booleanparameter>
                                                                <booleanparameter name="isCategorizationItem">False</booleanparameter>
                                                                <graphGapMatchScoringParameter name="graphicGapMatchScoring" ControllerId="gapMatchController" findingOverride="gapMatchController" iscategorizationvariant="false">
                                                                    <subparameterset id="A">
                                                                        <gapImageParameter name="alternative" matchMax="1" width="0" height="0" contentType="Image" enteredText="">GenericImage</gapImageParameter>
                                                                    </subparameterset>
                                                                    <subparameterset id="B">
                                                                        <gapImageParameter name="alternative" matchMax="1" width="0" height="0" contentType="Text" enteredText="TEXTMATCH"/>
                                                                    </subparameterset>
                                                                    <subparameterset id="C">
                                                                        <gapImageParameter name="alternative" matchMax="1" width="0" height="0" contentType="FormulaImage" enteredText="">GenericImage</gapImageParameter>
                                                                    </subparameterset>
                                                                    <definition id="">
                                                                        <gapImageParameter name="alternative" matchMax="1" width="0" height="0" contentType="Image"/>
                                                                    </definition>
                                                                    <areaparameter name="itemQuestionArea">
                                                                        <subparameterset id="A">
                                                                            <resourceparameter name="clickableImage">GenericImage</resourceparameter>
                                                                        </subparameterset>
                                                                        <definition id="">
                                                                            <resourceparameter name="clickableImage"/>
                                                                        </definition>
                                                                        <Shapes>
                                                                            <Rectangle id="A" label="A">
                                                                                <TopLeft>
                                                                                    <X>1</X>
                                                                                    <Y>204</Y>
                                                                                </TopLeft>
                                                                                <BottomRight>
                                                                                    <X>85</X>
                                                                                    <Y>284</Y>
                                                                                </BottomRight>
                                                                            </Rectangle>
                                                                            <Rectangle id="B" label="B">
                                                                                <TopLeft>
                                                                                    <X>546</X>
                                                                                    <Y>201</Y>
                                                                                </TopLeft>
                                                                                <BottomRight>
                                                                                    <X>636</X>
                                                                                    <Y>277</Y>
                                                                                </BottomRight>
                                                                            </Rectangle>
                                                                        </Shapes>
                                                                    </areaparameter>
                                                                </graphGapMatchScoringParameter>
                                                            </parameterSet>
                                                        </parameters>
                                                    </assessmentItem>

    Friend Shared ReadOnly ilt_ggm_dc As XElement = <Template xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" definitionVersion="1">
                                                        <Description>Hotspot item met opmaak in twee kolommen</Description>
                                                        <Settings>
                                                            <DesignerSetting key="sort">True</DesignerSetting>
                                                        </Settings>
                                                        <Targets>
                                                            <Target xsi:type="ItemLayoutTemplateTarget" enabled="true" name="ces">
                                                                <Description>CES</Description>
                                                                <Template>
                                                                    <![CDATA[
				<root xmlns:cito="http://www.cito.nl/citotester">
					<itemBody class="defaultBody">
						<div class="content">
							<cito:control xmlns:cito="http://www.cito.nl/citotester" id="entireItem" type="Cito.Generic.Interaction.GraphicGapMatch">
								<parameter name="dualColumnLayout">
									<designersetting key="defaultvalue">True</designersetting>
								</parameter>
							</cito:control>
						</div>
					</itemBody>
				</root>
				]]>
                                                                </Template>
                                                            </Target>
                                                        </Targets>
                                                    </Template>

    Friend Shared ReadOnly ilt_ggm_sc As XElement = <Template xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" definitionVersion="1">
                                                        <Description>Hotspot item met opmaak in een kolom</Description>
                                                        <Settings>
                                                            <DesignerSetting key="sort">True</DesignerSetting>
                                                        </Settings>
                                                        <Targets>
                                                            <Target xsi:type="ItemLayoutTemplateTarget" enabled="true" name="ces">
                                                                <Description>CES</Description>
                                                                <Template><![CDATA[
				<root xmlns:cito="http://www.cito.nl/citotester">
					<itemBody class="defaultBody">
						<div class="content">
							<cito:control xmlns:cito="http://www.cito.nl/citotester" id="entireItem" type="Cito.Generic.Interaction.GraphicGapMatch">
							</cito:control>
						</div>
					</itemBody>
				</root>
				]]>
                                                                </Template>
                                                            </Target>
                                                        </Targets>
                                                    </Template>

    Friend Shared ReadOnly ct_ggm As XElement = <Template xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" definitionVersion="1">
                                                    <Description>Control template voor koppelitems</Description>
                                                    <Targets>
                                                        <Target xsi:type="ControlTemplateTarget" enabled="true" name="ces">
                                                            <Template>
                                                            </Template>
                                                        </Target>
                                                    </Targets>
                                                    <SharedFunctions>
                                                    </SharedFunctions>
                                                    <SharedParameterSet id="">
                                                        <booleanparameter name="dualColumnLayout">
                                                            <designersetting key="visible">False</designersetting>
                                                            <designersetting key="defaultvalue">False</designersetting>
                                                        </booleanparameter>
                                                        <booleanparameter name="isCategorizationItem">
                                                            <designersetting key="visible">false</designersetting>
                                                            <designersetting key="defaultvalue">false</designersetting>
                                                        </booleanparameter>
                                                        <graphGapMatchScoringParameter name="graphicGapMatchScoring" ControllerId="gapMatchController" findingOverride="gapMatchController">
                                                            <attributereference name="IsCategorizationVariant">isCategorizationItem</attributereference>
                                                            <designersetting key="label"></designersetting>
                                                            <designersetting key="itemcountlabel">Aantal plaatjes om te koppelen</designersetting>
                                                            <designersetting key="description"></designersetting>
                                                            <designersetting key="minimumLength">1</designersetting>
                                                            <designersetting key="maximumLength">100</designersetting>
                                                            <designersetting key="group">4 Stam</designersetting>
                                                            <designersetting key="sortkey">2</designersetting>
                                                            <designersetting key="required">true</designersetting>
                                                            <designersetting key="subsetidentifiers">Alphabetic</designersetting>
                                                            <designersetting key="linkedresourceparametername">clickableImage</designersetting>
                                                            <designersetting key="toolbarvisible">true</designersetting>
                                                            <designersetting key="maxnrofshapestocreate">100</designersetting>
                                                            <definition id="">
                                                                <gapImageParameter name="alternative">
                                                                    <designersetting key="filter">image/png|image/jpeg|image/gif|image/x-png|image/pjpeg</designersetting>
                                                                    <designersetting key="required">true</designersetting>
                                                                </gapImageParameter>
                                                            </definition>
                                                            <areaparameter name="itemQuestionArea">
                                                                <designersetting key="group">4 Stam</designersetting>
                                                                <designersetting key="sortkey">2</designersetting>
                                                                <designersetting key="required">true</designersetting>
                                                                <designersetting key="subsetidentifiers">Alphabetic</designersetting>
                                                                <designersetting key="linkedresourceparametername">clickableImage</designersetting>
                                                                <definition>
                                                                    <resourceparameter name="clickableImage">
                                                                        <designersetting key="label">Plaatje met klikbare gebieden</designersetting>
                                                                        <designersetting key="description">Selecteer de afbeelding</designersetting>
                                                                        <designersetting key="required">true</designersetting>
                                                                        <designersetting key="filter">image/png|image/jpeg|image/gif|image/x-png|image/pjpeg</designersetting>
                                                                        <designersetting key="group">4 Stam</designersetting>
                                                                        <designersetting key="editbuttonVisible">false</designersetting>
                                                                        <designersetting key="deletebuttonVisible">false</designersetting>
                                                                    </resourceparameter>
                                                                </definition>
                                                            </areaparameter>
                                                        </graphGapMatchScoringParameter>
                                                    </SharedParameterSet>
                                                </Template>


    Public Shared ReadOnly ilt_mc_dc As XElement = <Template xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" definitionVersion="1">
                                                       <Description></Description>
                                                       <Settings>
                                                           <DesignerSetting key="sort">True</DesignerSetting>
                                                       </Settings>
                                                       <Targets>
                                                           <Target xsi:type="ItemLayoutTemplateTarget" enabled="true" name="ces">
                                                               <Description></Description>
                                                               <Template>
                                                                   <![CDATA[
                                                                   <html>
                                                                       <head><title></title></head>
                                                                       <body>
                                                                           <cito:control xmlns:cito="http://www.cito.nl/citotester" id="entireItem" type="ct.mc">
                                                                               <parameter name="dualColumnLayout">
                                                                                   <designersetting key="defaultvalue">True</designersetting>
                                                                               </parameter>
                                                                               <parameter name="leftBody">
                                                                                   <designersetting key="visible">True</designersetting>
                                                                               </parameter>
                                                                               <parameter name="multiChoiceType">
                                                                                   <designersetting key="defaultvalue">1</designersetting>
                                                                               </parameter>
                                                                               <parameter name="expectedAnswers">
                                                                                    <designersetting key="visible">False</designersetting>
                                                                                </parameter>
                                                                           </cito:control>
                                                                       </body>
                                                                   </html>
                                                                  ]]>
                                                               </Template>
                                                           </Target>
                                                       </Targets>
                                                   </Template>

    Public Shared ReadOnly ilt_mc_sc As XElement = <Template xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" definitionVersion="1">
                                                       <Description></Description>
                                                       <Settings>
                                                           <DesignerSetting key="sort">True</DesignerSetting>
                                                       </Settings>
                                                       <Targets>
                                                           <Target xsi:type="ItemLayoutTemplateTarget" enabled="true" name="ces">
                                                               <Description></Description>
                                                               <Template>
                                                                   <![CDATA[
                                                                   <html>
                                                                       <head><title></title></head>
                                                                       <body>
                                                                           <cito:control xmlns:cito="http://www.cito.nl/citotester" id="entireItem" type="ct.mc">
                                                                               <parameter name="dualColumnLayout">
                                                                                   <designersetting key="defaultvalue">False</designersetting>
                                                                               </parameter>
                                                                               <parameter name="leftBody">
                                                                                   <designersetting key="visible">False</designersetting>
                                                                               </parameter>
                                                                               <parameter name="multiChoiceType">
                                                                                   <designersetting key="defaultvalue">1</designersetting>
                                                                               </parameter>
                                                                           </cito:control>
                                                                       </body>
                                                                   </html>
                                                                  ]]>
                                                               </Template>
                                                           </Target>
                                                       </Targets>
                                                   </Template>

    Public Shared ReadOnly ilt_mr_dc As XElement = <Template xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" definitionVersion="1">
                                                       <Description></Description>
                                                       <Settings>
                                                           <DesignerSetting key="sort">True</DesignerSetting>
                                                       </Settings>
                                                       <Targets>
                                                           <Target xsi:type="ItemLayoutTemplateTarget" enabled="true" name="ces">
                                                               <Description></Description>
                                                               <Template>
                                                                   <![CDATA[
                                                                   <html>
                                                                       <head><title></title></head>
                                                                       <body>
                                                                           <cito:control xmlns:cito="http://www.cito.nl/citotester" id="entireItem" type="ct.mc">
                                                                               <parameter name="dualColumnLayout">
                                                                                   <designersetting key="defaultvalue">True</designersetting>
                                                                               </parameter>
                                                                               <parameter name="leftBody">
                                                                                   <designersetting key="visible">True</designersetting>
                                                                               </parameter>
                                                                               <parameter name="multiChoiceType">
                                                                                   <designersetting key="defaultvalue">0</designersetting>
                                                                               </parameter>
                                                                               <parameter name="testInteger">
                                                                                  <designersetting key="visible">False</designersetting>
                                                                                  <designersetting key="defaultvalue">1</designersetting>
                                                                               </parameter>
                                                                                <parameter name="expectedAnswers">
                                                                                    <designersetting key="visible">True</designersetting>
                                                                                    <designersetting key="defaultvalue">2</designersetting>
                                                                                </parameter>
                                                                           </cito:control>
                                                                       </body>
                                                                   </html>
                                                                 ]]>
                                                               </Template>
                                                           </Target>
                                                       </Targets>
                                                   </Template>


    Friend Shared ReadOnly itm_tabular_dc As XElement = <assessmentItem xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" identifier="tabular_dc" itemid="testId" title="tabular_dc" layoutTemplateSrc="Cito.Generic.Tabular.DC">
                                                            <solution>
                                                                <keyFindings/>
                                                                <aspectReferences/>
                                                            </solution>
                                                            <parameters>
                                                                <parameterSet id="entireItem">
                                                                    <booleanparameter name="dualColumnLayout">True</booleanparameter>
                                                                    <listedparameter name="expectedAnswers">1</listedparameter>
                                                                    <integerparameter name="multiChoiceType">1</integerparameter>
                                                                    <xhtmlparameter name="kolomtekst">
                                                                        <p id="c1-id-11" xmlns="http://www.w3.org/1999/xhtml">A</p>
                                                                    </xhtmlparameter>
                                                                    <xhtmlparameter name="kolomtekst2">
                                                                        <p id="c1-id-11" xmlns="http://www.w3.org/1999/xhtml">B</p>
                                                                    </xhtmlparameter>
                                                                    <multichoicescoringparameter name="multiChoiceScoring" ControllerId="mc" findingOverride="mc" minChoices="1" maxChoices="1" multiChoice="Radio">
                                                                        <subparameterset id="A">
                                                                            <xhtmlparameter name="mcOption">
                                                                                <p id="c1-id-11" xmlns="http://www.w3.org/1999/xhtml">A1</p>
                                                                            </xhtmlparameter>
                                                                            <xhtmlparameter name="mcOption2">
                                                                                <p id="c1-id-11" xmlns="http://www.w3.org/1999/xhtml">A2</p>
                                                                            </xhtmlparameter>
                                                                        </subparameterset>
                                                                        <subparameterset id="B">
                                                                            <xhtmlparameter name="mcOption">
                                                                                <p id="c1-id-11" xmlns="http://www.w3.org/1999/xhtml">B1</p>
                                                                            </xhtmlparameter>
                                                                            <xhtmlparameter name="mcOption2">
                                                                                <p id="c1-id-11" xmlns="http://www.w3.org/1999/xhtml">B2</p>
                                                                            </xhtmlparameter>
                                                                        </subparameterset>
                                                                        <definition id="">
                                                                            <xhtmlparameter name="mcOption"/>
                                                                            <xhtmlparameter name="mcOption2"/>
                                                                        </definition>
                                                                    </multichoicescoringparameter>
                                                                </parameterSet>
                                                            </parameters>
                                                        </assessmentItem>

    Friend Shared ReadOnly ilt_tabular_dc As XElement = <Template xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" definitionVersion="1">
                                                            <Description/>
                                                            <Targets>
                                                                <Target xsi:type="ItemLayoutTemplateTarget" enabled="true" name="ces">
                                                                    <Description/>
                                                                    <Template>
                                                                        <![CDATA[
                                                                        <cito:control xmlns:cito="http://www.cito.nl/citotester" id="entireItem" type="Cito.Generic.Interaction.MC">
                                                                            <parameter name="dualColumnLayout">
                                                                                <designersetting key="defaultvalue">True</designersetting>
                                                                            </parameter>
                                                                            <parameter name="expectedAnswers">
                                                                                <designersetting key="visible">False</designersetting>
                                                                            </parameter>
                                                                            <parameter name="kolomtekst">
                                                                                <designersetting key="required">True</designersetting>
                                                                                <designersetting key="visible">True</designersetting>
                                                                            </parameter>
                                                                            <parameter name="kolomtekst2">
                                                                                <designersetting key="required">True</designersetting>
                                                                                <designersetting key="visible">True</designersetting>
                                                                            </parameter>
                                                                            <parameter name="multiChoiceScoring">
                                                                                <definition>
                                                                                    <parameter name="mcOption">
                                                                                        <designersetting key="label">Keuze kolom 1</designersetting>
                                                                                    </parameter>
                                                                                    <parameter name="mcOption2">
                                                                                        <designersetting key="required">True</designersetting>
                                                                                        <designersetting key="visible">True</designersetting>
                                                                                    </parameter>
                                                                                </definition>
                                                                            </parameter>
                                                                            <parameter name="multiChoiceType">
                                                                                <designersetting key="defaultvalue">1</designersetting>
                                                                            </parameter>
                                                                        </cito:control>
]]>
                                                                    </Template>
                                                                </Target>
                                                            </Targets>
                                                        </Template>

    Friend Shared ReadOnly ilt_mc_sc_for_tabular As XElement = <Template xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" definitionVersion="1">
                                                                   <Description/>
                                                                   <Targets>
                                                                       <Target xsi:type="ItemLayoutTemplateTarget" enabled="true" name="ces">
                                                                           <Description></Description>
                                                                           <Template>
                                                                               <![CDATA[
                                                                               <cito:control xmlns:cito="http://www.cito.nl/citotester" id="entireItem" type="Cito.Generic.Interaction.MC">
                                                                                   <parameter name="expectedAnswers">
                                                                                       <designersetting key="visible">False</designersetting>
                                                                                   </parameter>
                                                                                   <parameter name="multiChoiceType">
                                                                                       <designersetting key="defaultvalue">1</designersetting>
                                                                                   </parameter>
                                                                               </cito:control>
                                                                            ]]>
                                                                           </Template>
                                                                       </Target>
                                                                   </Targets>
                                                               </Template>

    Friend Shared ReadOnly ct_mc_for_tabular As XElement = <Template xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" definitionVersion="1">
                                                               <Description/>
                                                               <Targets>
                                                                   <Target xsi:type="ControlTemplateTarget" enabled="true" name="ces">
                                                                       <![CDATA[<html><head></head><body></body></html>]]>
                                                                   </Target>
                                                               </Targets>
                                                               <SharedFunctions/>
                                                               <SharedParameterSet id="">
                                                                   <booleanparameter name="dualColumnLayout">
                                                                       <designersetting key="visible">False</designersetting>
                                                                       <designersetting key="defaultvalue">False</designersetting>
                                                                   </booleanparameter>
                                                                   <listedparameter name="expectedAnswers">
                                                                       <designersetting key="label">Max. sleutels</designersetting>
                                                                       <designersetting key="description">Geef aan uit hoeveel sleutels het antwoord maximaal bestaat.</designersetting>
                                                                       <designersetting key="group">6 Alternatieven</designersetting>
                                                                       <designersetting key="visible">True</designersetting>
                                                                       <designersetting key="defaultvalue">1</designersetting>
                                                                       <designersetting key="sortkey">1</designersetting>
                                                                       <designersetting key="list">
                                                                           <listvalues>
                                                                               <listvalue key="0">Onbeperkt</listvalue>
                                                                               <listvalue key="1">1</listvalue>
                                                                               <listvalue key="2">2</listvalue>
                                                                               <listvalue key="3">3</listvalue>
                                                                               <listvalue key="4">4</listvalue>
                                                                               <listvalue key="5">5</listvalue>
                                                                               <listvalue key="6">6</listvalue>
                                                                               <listvalue key="7">7</listvalue>
                                                                               <listvalue key="8">8</listvalue>
                                                                               <listvalue key="9">9</listvalue>
                                                                               <listvalue key="10">10</listvalue>
                                                                               <listvalue key="11">11</listvalue>
                                                                               <listvalue key="12">12</listvalue>
                                                                           </listvalues>
                                                                       </designersetting>
                                                                   </listedparameter>
                                                                   <integerparameter name="multiChoiceType">
                                                                       <designersetting key="label">Type: mc/mr</designersetting>
                                                                       <designersetting key="description">0: check, 1: radio, 2: unknown</designersetting>
                                                                       <designersetting key="group">6 Alternatieven</designersetting>
                                                                       <designersetting key="visible">False</designersetting>
                                                                       <designersetting key="defaultvalue">0</designersetting>
                                                                   </integerparameter>
                                                                   <xhtmlparameter name="kolomtekst">
                                                                       <designersetting key="label">Kolomtekst kolom 1</designersetting>
                                                                       <designersetting key="description"></designersetting>
                                                                       <designersetting key="group">6 Alternatieven</designersetting>
                                                                       <designersetting key="required">False</designersetting>
                                                                       <designersetting key="visible">False</designersetting>
                                                                       <designersetting key="sortkey">2</designersetting>
                                                                   </xhtmlparameter>
                                                                   <xhtmlparameter name="kolomtekst2">
                                                                       <designersetting key="label">Kolomtekst kolom 2</designersetting>
                                                                       <designersetting key="description"></designersetting>
                                                                       <designersetting key="group">6 Alternatieven</designersetting>
                                                                       <designersetting key="required">False</designersetting>
                                                                       <designersetting key="visible">False</designersetting>
                                                                       <designersetting key="sortkey">3</designersetting>
                                                                   </xhtmlparameter>
                                                                   <multichoicescoringparameter ControllerId="mc" findingOverride="mc" name="multiChoiceScoring">
                                                                       <attributereference name="minChoices">expectedAnswers</attributereference>
                                                                       <attributereference name="maxChoices">expectedAnswers</attributereference>
                                                                       <attributereference name="multiChoice">multiChoiceType</attributereference>
                                                                       <designersetting key="label"></designersetting>
                                                                       <designersetting key="itemcountlabel">Aantal alternatieven</designersetting>
                                                                       <designersetting key="description"></designersetting>
                                                                       <designersetting key="visible">true</designersetting>
                                                                       <designersetting key="group">6 Alternatieven</designersetting>
                                                                       <designersetting key="sortkey">4</designersetting>
                                                                       <designersetting key="minimumLength">2</designersetting>
                                                                       <designersetting key="maximumLength">12</designersetting>
                                                                       <designersetting key="subsetidentifiers">Alphabetic</designersetting>
                                                                       <definition>
                                                                           <xhtmlparameter name="mcOption">
                                                                               <designersetting key="label">Keuze</designersetting>
                                                                               <designersetting key="description"></designersetting>
                                                                               <designersetting key="required">true</designersetting>
                                                                               <designersetting key="visible">true</designersetting>
                                                                           </xhtmlparameter>
                                                                           <xhtmlparameter name="mcOption2">
                                                                               <designersetting key="label">Keuze kolom 2</designersetting>
                                                                               <designersetting key="description"></designersetting>
                                                                               <designersetting key="required">False</designersetting>
                                                                               <designersetting key="visible">False</designersetting>
                                                                           </xhtmlparameter>
                                                                       </definition>
                                                                   </multichoicescoringparameter>
                                                               </SharedParameterSet>
                                                           </Template>

    Public Shared ReadOnly ct_mc As XElement = <Template xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" definitionVersion="1">
                                                   <Description></Description>
                                                   <Targets>
                                                       <Target xsi:type="ControlTemplateTarget" enabled="true" name="ces">
                                                           <Description></Description>
                                                           <Template>
                                                               <![CDATA[
                                                                <html>
                                                                    <body>
                                                                    </body>
                                                                </html>
                                                            ]]>
                                                           </Template>
                                                       </Target>
                                                   </Targets>
                                                   <SharedFunctions>
                                                   </SharedFunctions>
                                                   <SharedParameterSet id="">
                                                       <booleanparameter name="isScoredItem">
                                                           <designersetting key="visible">False</designersetting>
                                                           <designersetting key="defaultvalue">True</designersetting>
                                                       </booleanparameter>
                                                       <booleanparameter name="dualColumnLayout">
                                                           <designersetting key="visible">False</designersetting>
                                                           <designersetting key="defaultvalue">False</designersetting>
                                                       </booleanparameter>
                                                       <integerparameter name="testInteger">
                                                           <designersetting key="visible">True</designersetting>
                                                           <designersetting key="defaultvalue">10</designersetting>
                                                       </integerparameter>
                                                       <xhtmlparameter name="leftBody">
                                                           <designersetting key="label">Body links</designersetting>
                                                           <designersetting key="description">Geef hier de tekst en/of de afbeelding in zoals die in het linkerdeel weergegeven dient te worden.</designersetting>
                                                           <designersetting key="group">3 Linkerkolom</designersetting>
                                                           <designersetting key="required">False</designersetting>
                                                           <designersetting key="visible">False</designersetting>
                                                           <designersetting key="sortkey">1</designersetting>
                                                       </xhtmlparameter>
                                                       <xhtmlparameter name="itemQuestion">
                                                           <designersetting key="label">Vraag</designersetting>
                                                           <designersetting key="description">Some description</designersetting>
                                                           <designersetting key="visible">True</designersetting>
                                                           <designersetting key="required">True</designersetting>
                                                           <designersetting key="group">4 Stam</designersetting>
                                                           <designersetting key="sortkey">2</designersetting>
                                                       </xhtmlparameter>
                                                       <integerparameter name="multiChoiceType">
                                                           <designersetting key="label">Type: mc/mr</designersetting>
                                                           <designersetting key="description">0: check, 1: radio, 2: unknown</designersetting>
                                                           <designersetting key="group">6 Alternatieven</designersetting>
                                                           <designersetting key="visible">False</designersetting>
                                                           <designersetting key="defaultvalue">0</designersetting>
                                                       </integerparameter>
                                                       <listedparameter name="expectedAnswers">
                                                           <designersetting key="label">Max. sleutels</designersetting>
                                                           <designersetting key="description">Geef aan uit hoeveel sleutels het antwoord maximaal bestaat.</designersetting>
                                                           <designersetting key="group">6 Alternatieven</designersetting>
                                                           <designersetting key="visible">True</designersetting>
                                                           <designersetting key="defaultvalue">1</designersetting>
                                                           <designersetting key="sortkey">1</designersetting>
                                                           <designersetting key="list">
                                                               <listvalues>
                                                                   <listvalue key="0">Onbeperkt</listvalue>
                                                                   <listvalue key="1">1</listvalue>
                                                                   <listvalue key="2">2</listvalue>
                                                                   <listvalue key="3">3</listvalue>
                                                                   <listvalue key="4">4</listvalue>
                                                                   <listvalue key="5">5</listvalue>
                                                                   <listvalue key="6">6</listvalue>
                                                                   <listvalue key="7">7</listvalue>
                                                                   <listvalue key="8">8</listvalue>
                                                                   <listvalue key="9">9</listvalue>
                                                                   <listvalue key="10">10</listvalue>
                                                                   <listvalue key="11">11</listvalue>
                                                                   <listvalue key="12">12</listvalue>
                                                               </listvalues>
                                                           </designersetting>
                                                       </listedparameter>
                                                       <multichoicescoringparameter ControllerId="mc" findingOverride="mc" name="multiChoiceScoring">
                                                           <attributereference name="minChoices">expectedAnswers</attributereference>
                                                           <attributereference name="maxChoices">expectedAnswers</attributereference>
                                                           <attributereference name="multiChoice">multiChoiceType</attributereference>
                                                           <designersetting key="label"></designersetting>
                                                           <designersetting key="itemcountlabel">Aantal alternatieven</designersetting>
                                                           <designersetting key="description"></designersetting>
                                                           <designersetting key="visible">true</designersetting>
                                                           <designersetting key="group">6 Alternatieven</designersetting>
                                                           <designersetting key="sortkey">4</designersetting>
                                                           <designersetting key="minimumLength">2</designersetting>
                                                           <designersetting key="maximumLength">12</designersetting>
                                                           <designersetting key="subsetidentifiers">Alphabetic</designersetting>
                                                           <definition>
                                                               <xhtmlparameter name="mcOption">
                                                                   <designersetting key="label">Keuze</designersetting>
                                                                   <designersetting key="description"></designersetting>
                                                                   <designersetting key="required">true</designersetting>
                                                                   <designersetting key="visible">true</designersetting>
                                                               </xhtmlparameter>
                                                               <xhtmlparameter name="mcOption2">
                                                                   <designersetting key="label">Keuze kolom 2</designersetting>
                                                                   <designersetting key="description"></designersetting>
                                                                   <designersetting key="required">False</designersetting>
                                                                   <designersetting key="visible">False</designersetting>
                                                               </xhtmlparameter>
                                                           </definition>
                                                       </multichoicescoringparameter>
                                                   </SharedParameterSet>
                                               </Template>

    Friend Shared ReadOnly item_hottext_dc As XElement = <assessmentItem xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" identifier="HottextItem" itemid="testId" title="HottextItem" layoutTemplateSrc="Cito.Generic.Hottext.DC">
                                                             <solution>
                                                                 <keyFindings>
                                                                     <keyFinding id="hottextController" scoringMethod="Dichotomous">
                                                                         <keyFact id="I82efd231-365b-4444-ae27-c23d6d60cdac-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                             <keyValue domain="I82efd231-365b-4444-ae27-c23d6d60cdac-hottextController" occur="1">
                                                                                 <booleanValue>
                                                                                     <typedValue>true</typedValue>
                                                                                 </booleanValue>
                                                                             </keyValue>
                                                                         </keyFact>
                                                                         <keyFact id="Iaab4ae78-3d71-4d49-b3b6-ec87d036754e-hottextController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                             <keyValue domain="Iaab4ae78-3d71-4d49-b3b6-ec87d036754e-hottextController" occur="1">
                                                                                 <booleanValue>
                                                                                     <typedValue>true</typedValue>
                                                                                 </booleanValue>
                                                                             </keyValue>
                                                                         </keyFact>
                                                                         <keyFact id="Icf698f91-3286-427c-a485-59865c4e4cb3-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                             <keyValue domain="Icf698f91-3286-427c-a485-59865c4e4cb3-hottextController" occur="1">
                                                                                 <booleanValue>
                                                                                     <typedValue>false</typedValue>
                                                                                 </booleanValue>
                                                                             </keyValue>
                                                                         </keyFact>
                                                                         <keyFact id="Id47b0288-ed4d-4cf1-bf6b-d4eac873d136-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                             <keyValue domain="Id47b0288-ed4d-4cf1-bf6b-d4eac873d136-hottextController" occur="1">
                                                                                 <booleanValue>
                                                                                     <typedValue>false</typedValue>
                                                                                 </booleanValue>
                                                                             </keyValue>
                                                                         </keyFact>
                                                                         <keyFact id="I8c3a8e43-c691-4cb2-bd92-4cfb864968a6-hottextController" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                             <keyValue domain="I8c3a8e43-c691-4cb2-bd92-4cfb864968a6-hottextController" occur="1">
                                                                                 <booleanValue>
                                                                                     <typedValue>false</typedValue>
                                                                                 </booleanValue>
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
                                                                     <booleanparameter name="dualColumnLayout">True</booleanparameter>
                                                                     <booleanparameter name="isCorrectionVariant">False</booleanparameter>
                                                                     <xhtmlparameter name="hottextInput">
                                                                         <p id="c1-id-11" xmlns="http://www.w3.org/1999/xhtml">Dit is een <cito:InlineElement id="I82efd231-365b-4444-ae27-c23d6d60cdac" layoutTemplateSourceName="InlineHottextLayoutTemplate" inlineFO="" xmlns:cito="http://www.cito.nl/citotester">
                                                                                 <cito:parameters>
                                                                                     <cito:parameterSet id="entireItem">
                                                                                         <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                                                         <cito:plaintextparameter name="controlId">I82efd231-365b-4444-ae27-c23d6d60cdac</cito:plaintextparameter>
                                                                                         <cito:plaintextparameter name="controlLabel">hottext</cito:plaintextparameter>
                                                                                         <cito:booleanparameter name="addHottextCorrection">False</cito:booleanparameter>
                                                                                         <cito:plaintextparameter name="hottextValue"/>
                                                                                         <cito:hotTextCorrectionScoringParameter name="hotTextCorrectionScoring" ControllerId="hottextCorrectionController" findingOverride="hottextController" expectedLength="0" correctionIsApplicable="false">
                                                                                             <cito:definition id=""/>
                                                                                             <cito:relatedControlLabel name="controlLabel">hottext</cito:relatedControlLabel>
                                                                                         </cito:hotTextCorrectionScoringParameter>
                                                                                     </cito:parameterSet>
                                                                                 </cito:parameters>
                                                                             </cito:InlineElement><span id="SI82efd231-365b-4444-ae27-c23d6d60cdac" style="BACKGROUND-COLOR: #c7b8ce">hottext</span><cito:InlineElement id="Iaab4ae78-3d71-4d49-b3b6-ec87d036754e" layoutTemplateSourceName="InlineHottextLayoutTemplate" inlineFO="" xmlns:cito="http://www.cito.nl/citotester">
                                                                                 <cito:parameters>
                                                                                     <cito:parameterSet id="entireItem">
                                                                                         <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                                                         <cito:plaintextparameter name="controlId">Iaab4ae78-3d71-4d49-b3b6-ec87d036754e</cito:plaintextparameter>
                                                                                         <cito:plaintextparameter name="controlLabel">invoervak</cito:plaintextparameter>
                                                                                         <cito:booleanparameter name="addHottextCorrection">False</cito:booleanparameter>
                                                                                         <cito:plaintextparameter name="hottextValue"/>
                                                                                         <cito:hotTextCorrectionScoringParameter name="hotTextCorrectionScoring" ControllerId="hottextCorrectionController" findingOverride="hottextController" expectedLength="0" correctionIsApplicable="false">
                                                                                             <cito:definition id=""/>
                                                                                             <cito:relatedControlLabel name="controlLabel">invoervak</cito:relatedControlLabel>
                                                                                         </cito:hotTextCorrectionScoringParameter>
                                                                                     </cito:parameterSet>
                                                                                 </cito:parameters>
                                                                             </cito:InlineElement><span id="SIaab4ae78-3d71-4d49-b3b6-ec87d036754e" style="BACKGROUND-COLOR: #c7b8ce">invoervak</span></p>
                                                                         <p id="c1-id-12" xmlns="http://www.w3.org/1999/xhtml">
                                                                             <cito:InlineElement id="Icf698f91-3286-427c-a485-59865c4e4cb3" layoutTemplateSourceName="InlineHottextLayoutTemplate" inlineFO="" xmlns:cito="http://www.cito.nl/citotester">
                                                                                 <cito:parameters>
                                                                                     <cito:parameterSet id="entireItem">
                                                                                         <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                                                         <cito:plaintextparameter name="controlId">Icf698f91-3286-427c-a485-59865c4e4cb3</cito:plaintextparameter>
                                                                                         <cito:plaintextparameter name="controlLabel">Dit is een zin.</cito:plaintextparameter>
                                                                                         <cito:booleanparameter name="addHottextCorrection">False</cito:booleanparameter>
                                                                                         <cito:plaintextparameter name="hottextValue"/>
                                                                                         <cito:hotTextCorrectionScoringParameter name="hotTextCorrectionScoring" ControllerId="hottextCorrectionController" findingOverride="hottextController" expectedLength="0" correctionIsApplicable="false">
                                                                                             <cito:definition id=""/>
                                                                                             <cito:relatedControlLabel name="controlLabel">Dit is een zin.</cito:relatedControlLabel>
                                                                                         </cito:hotTextCorrectionScoringParameter>
                                                                                     </cito:parameterSet>
                                                                                 </cito:parameters>
                                                                             </cito:InlineElement>
                                                                             <span id="SIcf698f91-3286-427c-a485-59865c4e4cb3" style="background-color: #C7B8CE;">Dit is een zin.</span>
                                                                         </p>
                                                                         <p id="c1-id-13" xmlns="http://www.w3.org/1999/xhtml">
                                                                             <cito:InlineElement id="Id47b0288-ed4d-4cf1-bf6b-d4eac873d136" layoutTemplateSourceName="InlineHottextLayoutTemplate" inlineFO="" xmlns:cito="http://www.cito.nl/citotester">
                                                                                 <cito:parameters>
                                                                                     <cito:parameterSet id="entireItem">
                                                                                         <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                                                         <cito:plaintextparameter name="controlId">Id47b0288-ed4d-4cf1-bf6b-d4eac873d136</cito:plaintextparameter>
                                                                                         <cito:plaintextparameter name="controlLabel">Dit is een alinea.</cito:plaintextparameter>
                                                                                         <cito:booleanparameter name="addHottextCorrection">False</cito:booleanparameter>
                                                                                         <cito:plaintextparameter name="hottextValue"/>
                                                                                         <cito:hotTextCorrectionScoringParameter name="hotTextCorrectionScoring" ControllerId="hottextCorrectionController" findingOverride="hottextController" expectedLength="0" correctionIsApplicable="false">
                                                                                             <cito:definition id=""/>
                                                                                             <cito:relatedControlLabel name="controlLabel">Dit is een alinea.</cito:relatedControlLabel>
                                                                                         </cito:hotTextCorrectionScoringParameter>
                                                                                     </cito:parameterSet>
                                                                                 </cito:parameters>
                                                                             </cito:InlineElement>
                                                                             <span id="SId47b0288-ed4d-4cf1-bf6b-d4eac873d136" style="background-color: #C7B8CE;">Dit is een alinea.</span>
                                                                         </p>
                                                                         <p id="c1-id-14" xmlns="http://www.w3.org/1999/xhtml">
                                                                             <cito:InlineElement id="I8c3a8e43-c691-4cb2-bd92-4cfb864968a6" layoutTemplateSourceName="InlineHottextLayoutTemplate" inlineFO="" xmlns:cito="http://www.cito.nl/citotester">
                                                                                 <cito:parameters>
                                                                                     <cito:parameterSet id="entireItem">
                                                                                         <cito:listedparameter name="controlType">hottext</cito:listedparameter>
                                                                                         <cito:plaintextparameter name="controlId">I8c3a8e43-c691-4cb2-bd92-4cfb864968a6</cito:plaintextparameter>
                                                                                         <cito:plaintextparameter name="controlLabel">Dit is vrij.</cito:plaintextparameter>
                                                                                         <cito:booleanparameter name="addHottextCorrection">False</cito:booleanparameter>
                                                                                         <cito:plaintextparameter name="hottextValue"/>
                                                                                         <cito:hotTextCorrectionScoringParameter name="hotTextCorrectionScoring" ControllerId="hottextCorrectionController" findingOverride="hottextController" expectedLength="0" correctionIsApplicable="false">
                                                                                             <cito:definition id=""/>
                                                                                             <cito:relatedControlLabel name="controlLabel">Dit is vrij.</cito:relatedControlLabel>
                                                                                         </cito:hotTextCorrectionScoringParameter>
                                                                                     </cito:parameterSet>
                                                                                 </cito:parameters>
                                                                             </cito:InlineElement>
                                                                             <span id="SI8c3a8e43-c691-4cb2-bd92-4cfb864968a6" style="background-color: #C7B8CE;">Dit is vrij.</span>
                                                                         </p>
                                                                     </xhtmlparameter>
                                                                     <hotTextScoringParameter name="hotTextScoring" ControllerId="hottextController" findingOverride="hottextController" minChoices="1" maxChoices="0" multiChoice="Check" isCorrectionVariant="false">
                                                                         <subparameterset id="I82efd231-365b-4444-ae27-c23d6d60cdac">
                                                                             <plaintextparameter name="contentLabel">hottext</plaintextparameter>
                                                                         </subparameterset>
                                                                         <subparameterset id="Iaab4ae78-3d71-4d49-b3b6-ec87d036754e">
                                                                             <plaintextparameter name="contentLabel">invoervak</plaintextparameter>
                                                                         </subparameterset>
                                                                         <subparameterset id="Icf698f91-3286-427c-a485-59865c4e4cb3">
                                                                             <plaintextparameter name="contentLabel">Dit is een zin.</plaintextparameter>
                                                                         </subparameterset>
                                                                         <subparameterset id="Id47b0288-ed4d-4cf1-bf6b-d4eac873d136">
                                                                             <plaintextparameter name="contentLabel">Dit is een alinea.</plaintextparameter>
                                                                         </subparameterset>
                                                                         <subparameterset id="I8c3a8e43-c691-4cb2-bd92-4cfb864968a6">
                                                                             <plaintextparameter name="contentLabel">Dit is vrij.</plaintextparameter>
                                                                         </subparameterset>
                                                                         <definition id="">
                                                                             <plaintextparameter name="contentLabel"/>
                                                                         </definition>
                                                                     </hotTextScoringParameter>
                                                                     <integerparameter name="maxChoices">0</integerparameter>
                                                                 </parameterSet>
                                                             </parameters>
                                                         </assessmentItem>

    Friend Shared ReadOnly ilt_hottext_corrections_dc As XElement = <Template xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" definitionVersion="1">
                                                                        <Description/>
                                                                        <Settings/>
                                                                        <Targets>
                                                                            <Target xsi:type="ItemLayoutTemplateTarget" enabled="true" name="ces">
                                                                                <Description/>
                                                                                <Template>
                                                                                    <![CDATA[
                                                                                    <cito:control xmlns:cito="http://www.cito.nl/citotester" id="entireItem" type="Cito.Generic.Interaction.Hottext">
                                                                                        <parameter name="dualColumnLayout">
                                                                                            <designersetting key="defaultvalue">True</designersetting>
                                                                                        </parameter>
                                                                                        <parameter name="isCorrectionVariant">
                                                                                            <designersetting key="defaultvalue">True</designersetting>
                                                                                        </parameter>
                                                                                        <parameter name="hottextInput">
                                                                                            <designersetting key="inlinetemplate">InlineHottextCorrectionLayoutTemplate</designersetting>
                                                                                            <designersetting key="inlinetemplates">(template=InlineHottextCorrectionLayoutTemplate;icon=Icon;selection=required;text=Woord;divideStrategy=word)(template=InlineHottextCorrectionLayoutTemplate;icon=Icon;selection=required;text=Zin;divideStrategy=sentence)(template=InlineHottextCorrectionLayoutTemplate;icon=Icon;selection=required;text=Alinea;divideStrategy=paragraph)(template=InlineHottextCorrectionLayoutTemplate;icon=Icon;selection=required;text=Vrij;divideStrategy=free)</designersetting>
                                                                                        </parameter>
                                                                                    </cito:control>
                                                                                    ]]>
                                                                                </Template>
                                                                            </Target>
                                                                        </Targets>
                                                                    </Template>

    Friend Shared ReadOnly ilt_hottext_dc As XElement = <Template xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" definitionVersion="1">
                                                            <Description/>
                                                            <Settings/>
                                                            <Targets>
                                                                <Target xsi:type="ItemLayoutTemplateTarget" enabled="true" name="ces">
                                                                    <Template>
                                                                        <![CDATA[
                                                                        <cito:control xmlns:cito="http://www.cito.nl/citotester" id="entireItem" type="Cito.Generic.Interaction.Hottext">
                                                                            <parameter name="dualColumnLayout">
                                                                                <designersetting key="defaultvalue">True</designersetting>
                                                                            </parameter>
                                                                            <parameter name="hottextInput">
                                                                                <designersetting key="inlinetemplate">InlineHottextLayoutTemplate</designersetting>
                                                                                <designersetting key="inlinetemplates">(template=InlineHottextLayoutTemplate;icon=Icon;selection=required;text=Woord;divideStrategy=word)(template=InlineHottextLayoutTemplate;icon=Icon;selection=required;text=Zin;divideStrategy=sentence)(template=InlineHottextLayoutTemplate;icon=Icon;selection=required;text=Alinea;divideStrategy=paragraph)(template=InlineHottextLayoutTemplate;icon=Icon;selection=required;text=Vrij;divideStrategy=free)</designersetting>
                                                                            </parameter>
                                                                        </cito:control>
                                                                        ]]>
                                                                    </Template>
                                                                </Target>
                                                            </Targets>
                                                        </Template>

    Friend Shared ReadOnly ct_hottext As XElement = <Template xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" definitionVersion="1">
                                                        <Description/>
                                                        <Targets>
                                                            <Target xsi:type="ControlTemplateTarget" enabled="true" name="ces">
                                                                <![CDATA[
                                                                <html>
                                                                    <head>
                                                                    </head>
                                                                    <body>
                                                                        Hoi
                                                                    </body>
                                                                </html>
                                                                ]]>
                                                            </Target>
                                                        </Targets>
                                                        <SharedFunctions/>
                                                        <SharedParameterSet id="">
                                                            <booleanparameter name="dualColumnLayout">
                                                                <designersetting key="visible">False</designersetting>
                                                                <designersetting key="defaultvalue">False</designersetting>
                                                            </booleanparameter>
                                                            <booleanparameter name="isCorrectionVariant">
                                                                <designersetting key="visible">False</designersetting>
                                                                <designersetting key="defaultvalue">False</designersetting>
                                                            </booleanparameter>
                                                            <xhtmlparameter name="hottextInput">
                                                                <designersetting key="label">Invoervak</designersetting>
                                                                <designersetting key="description">Geef hier de tekst op, waarbinnen de kandidaat de selecties kan aanbrengen.</designersetting>
                                                                <designersetting key="visible">False</designersetting>
                                                                <designersetting key="required">False</designersetting>
                                                                <designersetting key="inlinetemplate"/>
                                                                <designersetting key="inlinetemplates"/>
                                                                <designersetting key="group">Linkerkolom</designersetting>
                                                            </xhtmlparameter>
                                                            <hotTextScoringParameter name="hotTextScoring" findingOverride="hottextController" ControllerId="hottextController" minChoices="1">
                                                                <attributereference name="maxChoices">maxChoices</attributereference>
                                                                <attributereference name="isCorrectionVariant">isCorrectionVariant</attributereference>
                                                                <attributereference name="HotTextText" whattocopy="Parameter">hottextInput</attributereference>
                                                                <designersetting key="label"/>
                                                                <designersetting key="description"/>
                                                                <designersetting key="group">Linkerkolom</designersetting>
                                                                <designersetting key="sortkey">2</designersetting>
                                                                <designersetting key="required">true</designersetting>
                                                                <definition id="">
                                                                    <plaintextparameter name="contentLabel"/>
                                                                </definition>
                                                            </hotTextScoringParameter>
                                                            <integerparameter name="maxChoices">
                                                                <designersetting key="label">Maximum aantal selecties.</designersetting>
                                                                <designersetting key="description">Maximum aantal hottext elementen dat de kandidaat kan selecteren.</designersetting>
                                                                <designersetting key="group">Linkerkolom</designersetting>
                                                                <designersetting key="required">True</designersetting>
                                                                <designersetting key="visible">True</designersetting>
                                                                <designersetting key="defaultvalue">0</designersetting>
                                                                <designersetting key="sortkey">3</designersetting>
                                                            </integerparameter>
                                                        </SharedParameterSet>
                                                    </Template>

    Friend Shared ReadOnly ilt_inlinehottext As XElement = <Template xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" definitionVersion="1">
                                                               <Description>Dit is een test omschrijving voor ItemLayoutTemplate</Description>
                                                               <Targets>
                                                                   <Target xsi:type="ItemLayoutTemplateTarget" enabled="true" name="ces">
                                                                       <Description>CES Player</Description>
                                                                       <Template><![CDATA[
				<html xmlns:cito="http://www.cito.nl/citotester">
					<cito:control id="entireItem" type="InlineHottext">
					</cito:control>
				</html>
			]]>
                                                                       </Template>
                                                                   </Target>
                                                               </Targets>
                                                           </Template>

    Friend Shared ReadOnly ilt_inlinehottextcorrection As XElement = <Template xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" definitionVersion="1">
                                                                         <Description>Dit is een test omschrijving voor ItemLayoutTemplate</Description>
                                                                         <Targets>
                                                                             <Target xsi:type="ItemLayoutTemplateTarget" enabled="true" name="ces">
                                                                                 <Description>CES Player</Description>
                                                                                 <Template><![CDATA[
				<html xmlns:cito="http://www.cito.nl/citotester">
					<cito:control id="entireItem" type="InlineHottext">
                        <parameter name="addHottextCorrection">
                            <designersetting key="defaultvalue">True</designersetting>
                        </parameter>
                        <parameter name="hotTextCorrectionScoring">
                            <designersetting key="PreprocessRules">(ConvertToLower,RemoveAllSpaces,RemoveDiacritics)</designersetting>
                        </parameter>
					</cito:control>
				</html>
			]]>
                                                                                 </Template>
                                                                             </Target>
                                                                         </Targets>
                                                                     </Template>

    Friend Shared ReadOnly ct_inlinehottext As XElement = <Template xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" definitionVersion="1">
                                                              <Description/>
                                                              <Targets>
                                                                  <Target xsi:type="ControlTemplateTarget" enabled="true" name="ces">
                                                                      <Description/>
                                                                      <Template>
                                                                          <![CDATA[]]>
                                                                      </Template>
                                                                  </Target>
                                                              </Targets>
                                                              <SharedParameterSet>
                                                                  <listedparameter name="controlType">
                                                                      <designersetting key="label">Type inline control</designersetting>
                                                                      <designersetting key="description">Welk type control moet er ingevoegd worden.</designersetting>
                                                                      <designersetting key="group">0 Type</designersetting>
                                                                      <designersetting key="required">True</designersetting>
                                                                      <designersetting key="visible">False</designersetting>
                                                                      <designersetting key="defaultvalue">hottext</designersetting>
                                                                      <designersetting key="list">
                                                                          <listvalues>
                                                                              <listvalue key="hottext">Hottext (Facet)</listvalue>
                                                                          </listvalues>
                                                                      </designersetting>
                                                                  </listedparameter>
                                                                  <plaintextparameter name="controlId">
                                                                      <designersetting key="label">label</designersetting>
                                                                      <designersetting key="visible">False</designersetting>
                                                                  </plaintextparameter>
                                                                  <plaintextparameter name="controlLabel">
                                                                      <designersetting key="label">Label</designersetting>
                                                                      <designersetting key="description">Label waaraan de control (in de score editor) kan worden herkend</designersetting>
                                                                      <designersetting key="visible">True</designersetting>
                                                                      <designersetting key="required">True</designersetting>
                                                                      <designersetting key="group">1 Algemeen</designersetting>
                                                                  </plaintextparameter>
                                                                  <booleanparameter name="addHottextCorrection">
                                                                      <designersetting key="visible">False</designersetting>
                                                                      <designersetting key="defaultvalue">False</designersetting>
                                                                  </booleanparameter>
                                                                  <plaintextparameter name="hottextValue">
                                                                      <designersetting key="conditionalEnabled">true</designersetting>
                                                                      <designersetting key="conditionalEnabledSwitchParameter">controlType</designersetting>
                                                                      <designersetting key="conditionalEnabledWhenValue">hottext</designersetting>
                                                                      <designersetting key="label">Selecteerbare tekst</designersetting>
                                                                      <designersetting key="description"></designersetting>
                                                                      <designersetting key="visible">False</designersetting>
                                                                      <designersetting key="required">False</designersetting>
                                                                      <designersetting key="group">2 Hottext</designersetting>
                                                                  </plaintextparameter>
                                                                  <hotTextCorrectionScoringParameter name="hotTextCorrectionScoring" findingOverride="hottextController" ControllerId="hottextCorrectionController">
                                                                      <attributereference name="CorrectionIsApplicable">addHottextCorrection</attributereference>
                                                                      <attributereference name="RelatedControlLabelParameter" whattocopy="Parameter">controlLabel</attributereference>
                                                                      <designersetting key="PreprocessRules"></designersetting>
                                                                      <definition id=""/>
                                                                  </hotTextCorrectionScoringParameter>
                                                              </SharedParameterSet>
                                                          </Template>

    Public Shared ilt_html_ilt As XElement = <Template xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" definitionVersion="1">
                                                 <Description>Test</Description>
                                                 <Settings>
                                                 </Settings>
                                                 <Targets>
                                                     <Target xsi:type="ItemLayoutTemplateTarget" enabled="true" name="word">
                                                         <Description>Alleen voor word 2010+</Description>
                                                         <Template>
                                                             <![CDATA[
				                            <html>
					                            <body>
						                            <cito:control xmlns:cito="http://www.cito.nl/citotester" id="invoer" type="min.html" />
					                            </body>
				                            </html>
			                            ]]>
                                                         </Template>
                                                     </Target>
                                                     <Target xsi:type="ItemLayoutTemplateTarget" enabled="true" name="ces">
                                                         <Description>CES</Description>
                                                         <Template>
                                                             <![CDATA[
				                            <html>
					                            <body>
						                            <cito:control xmlns:cito="http://www.cito.nl/citotester" id="invoer" type="min.html" />
					                            </body>
				                            </html>
			                            ]]>
                                                         </Template>
                                                     </Target>
                                                 </Targets>
                                             </Template>


    Public Shared min_html_ctrl As XElement = <Template xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" definitionVersion="1">
                                                  <Description></Description>
                                                  <Targets>
                                                      <Target xsi:type="ControlTemplateTarget" enabled="true" name="Word">
                                                          <Description>Word</Description>
                                                          <Template><![CDATA[<html><body></body></html>]]></Template>
                                                      </Target>
                                                      <Target xsi:type="ControlTemplateTarget" enabled="true" name="ces">
                                                          <Description>CES</Description>
                                                          <Template><![CDATA[<html><body><%#itemGeneral%></body></html>]]></Template>
                                                      </Target>
                                                  </Targets>
                                                  <SharedFunctions>
                                                  </SharedFunctions>
                                                  <SharedParameterSet id="parameters">
                                                      <xhtmlparameter name="xhtml">
                                                          <designersetting key="label">Algemeen tekstveld</designersetting>
                                                          <designersetting key="description">Geef hier tekst en/of afbeelding in zoals die in het rechter deel onder de antwoorden weergegeven dient te worden.</designersetting>
                                                          <designersetting key="group">1 Algemeen tekstveld</designersetting>
                                                          <designersetting key="required">True</designersetting>
                                                      </xhtmlparameter>
                                                  </SharedParameterSet>
                                              </Template>

    Public Shared ilt_htmlErr_ilt As XElement = <Template xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" definitionVersion="1">
                                                    <Description></Description>
                                                    <Settings>
                                                    </Settings>
                                                    <Targets>
                                                        <Target xsi:type="ItemLayoutTemplateTarget" enabled="true" name="ces">
                                                            <Description>TestPlayer 2.x</Description>
                                                            <Template>
                                                                <![CDATA[
				                            <html>
					                            <body>
						                            <cito:control xmlns:cito="http://www.cito.nl/citotester" id="invoer" type="min.htmlErr" />
					                            </body>
				                            </html>
			                            ]]>
                                                            </Template>
                                                        </Target>
                                                        <Target xsi:type="ItemLayoutTemplateTarget" enabled="true" name="word">
                                                            <Description>Alleen voor word 2010+</Description>
                                                            <Template><![CDATA[<html><body></body></html>]]></Template>
                                                        </Target>
                                                        <Target xsi:type="ItemLayoutTemplateTarget" enabled="true" name="ces">
                                                            <Description>CES</Description>
                                                            <Template><![CDATA[<html><body></body></html>]]></Template>
                                                        </Target>
                                                    </Targets>
                                                </Template>

    Public Shared min_htmlErr_ctrl As XElement = <Template xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" definitionVersion="1">
                                                     <Description></Description>
                                                     <Targets>
                                                         <Target xsi:type="ControlTemplateTarget" enabled="true" name="Word">
                                                             <Description>Word</Description>
                                                             <Template><![CDATA[<html><body></body></html>]]></Template>
                                                         </Target>
                                                         <Target xsi:type="ControlTemplateTarget" enabled="true" name="ces">
                                                             <Description>CES</Description>
                                                             <Template><![CDATA[<html><body></body></html>]]></Template>
                                                         </Target>
                                                     </Targets>
                                                     <SharedFunctions>
                                                     </SharedFunctions>
                                                     <SharedParameterSet id="parameters">
                                                         <integerparameter name="xhtml">
                                                             <designersetting key="label">Algemeen tekstveld</designersetting>
                                                             <designersetting key="required">True</designersetting>
                                                         </integerparameter>
                                                     </SharedParameterSet>
                                                 </Template>

    Public Shared ilt_integer_ilt As XElement = <Template xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" definitionVersion="1">
                                                    <Description></Description>
                                                    <Settings>
                                                    </Settings>
                                                    <Targets>
                                                        <Target xsi:type="ItemLayoutTemplateTarget" enabled="true" name="ces">
                                                            <Description>TestPlayer 2.x</Description>
                                                            <Template>
                                                                <![CDATA[
				                            <html>
					                            <body>
						                            <cito:control xmlns:cito="http://www.cito.nl/citotester" id="invoer" type="min.integer" />
					                            </body>
				                            </html>
			                            ]]>
                                                            </Template>
                                                        </Target>
                                                        <Target xsi:type="ItemLayoutTemplateTarget" enabled="true" name="word">
                                                            <Description>Alleen voor word 2010+</Description>
                                                            <Template><![CDATA[<html><body></body></html>]]></Template>
                                                        </Target>
                                                        <Target xsi:type="ItemLayoutTemplateTarget" enabled="true" name="ces">
                                                            <Description>CES</Description>
                                                            <Template><![CDATA[<html><body></body></html>]]></Template>
                                                        </Target>
                                                    </Targets>
                                                </Template>


    Public Shared min_integer_ctrl As XElement = <Template xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" definitionVersion="1">
                                                     <Description></Description>
                                                     <Targets>
                                                         <Target xsi:type="ControlTemplateTarget" enabled="true" name="Word">
                                                             <Description>Word</Description>
                                                             <Template><![CDATA[<html><body></body></html>]]></Template>
                                                         </Target>
                                                         <Target xsi:type="ControlTemplateTarget" enabled="true" name="ces">
                                                             <Description>CES</Description>
                                                             <Template><![CDATA[<html><body></body></html>]]></Template>
                                                         </Target>
                                                     </Targets>
                                                     <SharedFunctions>
                                                     </SharedFunctions>
                                                     <SharedParameterSet id="parameters">
                                                         <integer name="integer">
                                                             <designersetting key="label">Algemeen Nummer</designersetting>
                                                             <designersetting key="description">Geef hier tekst en/of afbeelding in zoals die in het rechter deel onder de antwoorden weergegeven dient te worden.</designersetting>
                                                             <designersetting key="group">1 Algemeen tekstveld</designersetting>
                                                             <designersetting key="required">True</designersetting>
                                                         </integer>
                                                     </SharedParameterSet>
                                                 </Template>

    Public Shared ilt_2xhtml_ilt As XElement = <Template xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" definitionVersion="1">
                                                   <Description></Description>
                                                   <Settings>
                                                   </Settings>
                                                   <Targets>
                                                       <Target xsi:type="ItemLayoutTemplateTarget" enabled="true" name="word">
                                                           <Description>Alleen voor word 2010+</Description>
                                                           <Template>
                                                               <![CDATA[
				                            <html>
					                            <body>
						                            <cito:control xmlns:cito="http://www.cito.nl/citotester" id="invoer" type="min.2xhtml" />
					                            </body>
				                            </html>
			                            ]]>
                                                           </Template>
                                                       </Target>
                                                       <Target xsi:type="ItemLayoutTemplateTarget" enabled="true" name="ces">
                                                           <Description>CES</Description>
                                                           <Template>
                                                               <![CDATA[
				                            <html>
					                            <body>
						                            <cito:control xmlns:cito="http://www.cito.nl/citotester" id="invoer" type="min.2xhtml" />
					                            </body>
				                            </html>
			                            ]]>
                                                           </Template>
                                                       </Target>
                                                   </Targets>
                                               </Template>


    Public Shared min_2xhtml_ctrl As XElement = <Template xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" definitionVersion="1">
                                                    <Description></Description>
                                                    <Targets>
                                                        <Target xsi:type="ControlTemplateTarget" enabled="true" name="Word">
                                                            <Description>Word</Description>
                                                            <Template><![CDATA[<html><body></body></html>]]></Template>
                                                        </Target>
                                                        <Target xsi:type="ControlTemplateTarget" enabled="true" name="ces">
                                                            <Description>CES</Description>
                                                            <Template><![CDATA[<html><body></body></html>]]></Template>
                                                        </Target>
                                                    </Targets>
                                                    <SharedFunctions>
                                                    </SharedFunctions>
                                                    <SharedParameterSet id="parameters">
                                                        <xhtmlparameter name="xhtml">
                                                            <designersetting key="label">Algemeen tekstveld</designersetting>
                                                            <designersetting key="description">Geef hier tekst en/of afbeelding in zoals die in het rechter deel onder de antwoorden weergegeven dient te worden.</designersetting>
                                                            <designersetting key="group">1 Algemeen tekstveld</designersetting>
                                                            <designersetting key="required">True</designersetting>
                                                        </xhtmlparameter>
                                                        <xhtmlparameter name="xhtml2">
                                                            <designersetting key="label">Algemeen tekstveld</designersetting>
                                                            <designersetting key="description">Geef hier tekst en/of afbeelding in zoals die in het rechter deel onder de antwoorden weergegeven dient te worden.</designersetting>
                                                            <designersetting key="group">1 Algemeen tekstveld</designersetting>
                                                            <designersetting key="required">True</designersetting>
                                                        </xhtmlparameter>
                                                    </SharedParameterSet>
                                                </Template>


    Public Shared ilt_compare_boolean1 As XElement = <Template xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" definitionVersion="1">
                                                         <Description></Description>
                                                         <Settings>
                                                         </Settings>
                                                         <Targets>
                                                             <Target xsi:type="ItemLayoutTemplateTarget" enabled="true" name="word">
                                                                 <Description>Alleen voor word 2010+</Description>
                                                                 <Template>
                                                                     <![CDATA[
				                            <html>
					                            <body>
						                            <cito:control xmlns:cito="http://www.cito.nl/citotester" id="invoer" type="min.compare.boolean1" />
					                            </body>
				                            </html>
			                            ]]>
                                                                 </Template>
                                                             </Target>
                                                             <Target xsi:type="ItemLayoutTemplateTarget" enabled="true" name="ces">
                                                                 <Description>CES</Description>
                                                                 <Template>
                                                                     <![CDATA[
				                            <html>
					                            <body>
						                            <cito:control xmlns:cito="http://www.cito.nl/citotester" id="invoer" type="min.compare.boolean1" />
					                            </body>
				                            </html>
			                            ]]>
                                                                 </Template>
                                                             </Target>
                                                         </Targets>
                                                     </Template>


    Public Shared min_compare_boolean_ctrl1 As XElement = <Template xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" definitionVersion="1">
                                                              <Description></Description>
                                                              <Targets>
                                                                  <Target xsi:type="ControlTemplateTarget" enabled="true" name="Word">
                                                                      <Description>Word</Description>
                                                                      <Template><![CDATA[<html><body></body></html>]]></Template>
                                                                  </Target>
                                                                  <Target xsi:type="ControlTemplateTarget" enabled="true" name="ces">
                                                                      <Description>CES</Description>
                                                                      <Template><![CDATA[<html><body><%#itemGeneral%></body></html>]]></Template>
                                                                  </Target>
                                                              </Targets>
                                                              <SharedFunctions>
                                                              </SharedFunctions>
                                                              <SharedParameterSet id="parameters">
                                                                  <booleanparameter name="parameter1">
                                                                      <designersetting key="label">Rekenmachine</designersetting>
                                                                      <designersetting key="description">Geef aan of tijdens het beantwoorden van het item de kandidaat gebruik mag maken van de ingebouwde calculator.</designersetting>
                                                                      <designersetting key="required">False</designersetting>
                                                                      <designersetting key="sortkey">1</designersetting>
                                                                      <designersetting key="group">1 Hulpmiddelen</designersetting>
                                                                      <designersetting key="visible">True</designersetting>
                                                                  </booleanparameter>
                                                              </SharedParameterSet>
                                                          </Template>

    Public Shared ilt_compare_boolean2 As XElement = <Template xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" definitionVersion="1">
                                                         <Description></Description>
                                                         <Settings>
                                                         </Settings>
                                                         <Targets>
                                                             <Target xsi:type="ItemLayoutTemplateTarget" enabled="true" name="ces">
                                                                 <Description>TestPlayer 2.x</Description>
                                                                 <Template>
                                                                     <![CDATA[
				                            <html>
					                            <body>
						                            <cito:control xmlns:cito="http://www.cito.nl/citotester" id="invoer" type="min.compare.boolean2" />
					                            </body>
				                            </html>
			                            ]]>
                                                                 </Template>
                                                             </Target>
                                                             <Target xsi:type="ItemLayoutTemplateTarget" enabled="true" name="word">
                                                                 <Description>Alleen voor word 2010+</Description>
                                                                 <Template><![CDATA[<html><body></body></html>]]></Template>
                                                             </Target>
                                                             <Target xsi:type="ItemLayoutTemplateTarget" enabled="true" name="ces">
                                                                 <Description>CES</Description>
                                                                 <Template><![CDATA[<html><body></body></html>]]></Template>
                                                             </Target>
                                                         </Targets>
                                                     </Template>


    Public Shared min_compare_boolean_ctrl2 As XElement = <Template xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" definitionVersion="1">
                                                              <Description></Description>
                                                              <Targets>
                                                                  <Target xsi:type="ControlTemplateTarget" enabled="true" name="Word">
                                                                      <Description>Word</Description>
                                                                      <Template><![CDATA[<html><body></body></html>]]></Template>
                                                                  </Target>
                                                                  <Target xsi:type="ControlTemplateTarget" enabled="true" name="ces">
                                                                      <Description>CES</Description>
                                                                      <Template><![CDATA[<html><body></body></html>]]></Template>
                                                                  </Target>
                                                              </Targets>
                                                              <SharedFunctions>
                                                              </SharedFunctions>
                                                              <SharedParameterSet id="parameters">
                                                                  <booleanparameter name="parameter2">
                                                                      <designersetting key="label">Rekenmachine</designersetting>
                                                                      <designersetting key="description">Geef aan of tijdens het beantwoorden van het item de kandidaat gebruik mag maken van de ingebouwde calculator.</designersetting>
                                                                      <designersetting key="required">False</designersetting>
                                                                      <designersetting key="sortkey">1</designersetting>
                                                                      <designersetting key="group">1 Hulpmiddelen</designersetting>
                                                                      <designersetting key="visible">True</designersetting>
                                                                  </booleanparameter>
                                                              </SharedParameterSet>
                                                          </Template>


    Public Shared ilt_compare_integer1 As XElement = <Template xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" definitionVersion="1">
                                                         <Description></Description>
                                                         <Settings>
                                                         </Settings>
                                                         <Targets>
                                                             <Target xsi:type="ItemLayoutTemplateTarget" enabled="true" name="word">
                                                                 <Description>Alleen voor word 2010+</Description>
                                                                 <Template><![CDATA[<html><body></body></html>]]></Template>
                                                             </Target>
                                                             <Target xsi:type="ItemLayoutTemplateTarget" enabled="true" name="ces">
                                                                 <Description>CES</Description>
                                                                 <Template>
                                                                     <![CDATA[
				                            <html>
					                            <body>
						                            <cito:control xmlns:cito="http://www.cito.nl/citotester" id="invoer" type="min.compare.integer1" />
					                            </body>
				                            </html>
			                            ]]>
                                                                 </Template>
                                                             </Target>
                                                         </Targets>
                                                     </Template>


    Public Shared min_compare_integer_ctrl1 As XElement = <Template xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" definitionVersion="1">
                                                              <Description></Description>
                                                              <Targets>
                                                                  <Target xsi:type="ControlTemplateTarget" enabled="true" name="Word">
                                                                      <Description>Word</Description>
                                                                      <Template><![CDATA[<html><body></body></html>]]></Template>
                                                                  </Target>
                                                                  <Target xsi:type="ControlTemplateTarget" enabled="true" name="ces">
                                                                      <Description>CES</Description>
                                                                      <Template><![CDATA[<html><body></body></html>]]></Template>
                                                                  </Target>
                                                              </Targets>
                                                              <SharedFunctions>
                                                              </SharedFunctions>
                                                              <SharedParameterSet id="parameters">
                                                                  <integerparameter name="parameter1">
                                                                      <designersetting key="label">Aantal</designersetting>
                                                                      <designersetting key="description">Een omschrijving.</designersetting>
                                                                      <designersetting key="required">False</designersetting>
                                                                      <designersetting key="sortkey">1</designersetting>
                                                                      <designersetting key="group">1 Hulpmiddelen</designersetting>
                                                                      <designersetting key="visible">True</designersetting>
                                                                  </integerparameter>
                                                              </SharedParameterSet>
                                                          </Template>

    Public Shared ilt_compare_integer2 As XElement = <Template xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" definitionVersion="1">
                                                         <Description></Description>
                                                         <Settings>
                                                         </Settings>
                                                         <Targets>
                                                             <Target xsi:type="ItemLayoutTemplateTarget" enabled="true" name="word">
                                                                 <Description>Alleen voor word 2010+</Description>
                                                                 <Template><![CDATA[<html><body></body></html>]]></Template>
                                                             </Target>
                                                             <Target xsi:type="ItemLayoutTemplateTarget" enabled="true" name="ces">
                                                                 <Description>CES</Description>
                                                                 <Template>
                                                                     <![CDATA[
				                            <html>
					                            <body>
						                            <cito:control xmlns:cito="http://www.cito.nl/citotester" id="invoer" type="min.compare.integer2" />
					                            </body>
				                            </html>
			                            ]]>
                                                                 </Template>
                                                             </Target>
                                                         </Targets>
                                                     </Template>

    Public Shared min_compare_integer_ctrl2 As XElement = <Template xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" definitionVersion="1">
                                                              <Description></Description>
                                                              <Targets>
                                                                  <Target xsi:type="ControlTemplateTarget" enabled="true" name="Word">
                                                                      <Description>Word</Description>
                                                                      <Template><![CDATA[<html><body></body></html>]]></Template>
                                                                  </Target>
                                                                  <Target xsi:type="ControlTemplateTarget" enabled="true" name="ces">
                                                                      <Description>CES</Description>
                                                                      <Template><![CDATA[<html><body><%#itemGeneral%></body></html>]]></Template>
                                                                  </Target>
                                                              </Targets>
                                                              <SharedFunctions>
                                                              </SharedFunctions>
                                                              <SharedParameterSet id="parameters">
                                                                  <integerparameter name="parameter2">
                                                                      <designersetting key="label">Aantal</designersetting>
                                                                      <designersetting key="description">Een omschrijving.</designersetting>
                                                                      <designersetting key="required">False</designersetting>
                                                                      <designersetting key="sortkey">1</designersetting>
                                                                      <designersetting key="group">1 Hulpmiddelen</designersetting>
                                                                      <designersetting key="visible">True</designersetting>
                                                                  </integerparameter>
                                                              </SharedParameterSet>
                                                          </Template>



    Public Shared ilt_compare_collection1 As XElement = <Template xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" definitionVersion="1">
                                                            <Description></Description>
                                                            <Settings>
                                                            </Settings>
                                                            <Targets>
                                                                <Target xsi:type="ItemLayoutTemplateTarget" enabled="true" name="word">
                                                                    <Description>Alleen voor word 2010+</Description>
                                                                    <Template><![CDATA[<html><body></body></html>]]></Template>
                                                                </Target>
                                                                <Target xsi:type="ItemLayoutTemplateTarget" enabled="true" name="ces">
                                                                    <Description>CES</Description>
                                                                    <Template>
                                                                        <![CDATA[
				                            <html>
					                            <body>
						                            <cito:control xmlns:cito="http://www.cito.nl/citotester" id="invoer" type="min.compare.collection1" />
					                            </body>
				                            </html>
			                            ]]>
                                                                    </Template>
                                                                </Target>
                                                            </Targets>
                                                        </Template>

    Public Shared min_compare_collection_ctrl1 As XElement = <Template xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" definitionVersion="1">
                                                                 <Description></Description>
                                                                 <Targets>
                                                                     <Target xsi:type="ControlTemplateTarget" enabled="true" name="Word">
                                                                         <Description>Word</Description>
                                                                         <Template><![CDATA[<html><body></body></html>]]></Template>
                                                                     </Target>
                                                                     <Target xsi:type="ControlTemplateTarget" enabled="true" name="ces">
                                                                         <Description>CES</Description>
                                                                         <Template><![CDATA[<html><body><%#itemGeneral%></body></html>]]></Template>
                                                                     </Target>
                                                                 </Targets>
                                                                 <SharedFunctions>
                                                                 </SharedFunctions>
                                                                 <SharedParameterSet id="parameters">
                                                                     <collectionparameter name="collectionParam1">
                                                                         <designersetting key="label"></designersetting>
                                                                         <designersetting key="itemcountlabel">Aantal audio bestanden</designersetting>
                                                                         <designersetting key="description"></designersetting>
                                                                         <designersetting key="minimumLength">0</designersetting>
                                                                         <designersetting key="maximumLength">1</designersetting>
                                                                         <designersetting key="defaultvalue">0</designersetting>
                                                                         <designersetting key="subsetidentifiers">Numeric</designersetting>
                                                                         <designersetting key="group">2 Verklanking</designersetting>
                                                                         <designersetting key="required">true</designersetting>
                                                                         <designersetting key="sortkey">2</designersetting>
                                                                         <definition>
                                                                             <integerparameter name="integerParameter1">
                                                                                 <designersetting key="label">Aantal1</designersetting>
                                                                                 <designersetting key="description">Een omschrijving.</designersetting>
                                                                                 <designersetting key="required">False</designersetting>
                                                                                 <designersetting key="sortkey">1</designersetting>
                                                                                 <designersetting key="group">1 Hulpmiddelen</designersetting>
                                                                                 <designersetting key="visible">True</designersetting>
                                                                             </integerparameter>
                                                                             <integerparameter name="integerParameter2">
                                                                                 <designersetting key="label">Aantal2</designersetting>
                                                                                 <designersetting key="description">Een omschrijving.</designersetting>
                                                                                 <designersetting key="required">False</designersetting>
                                                                                 <designersetting key="sortkey">1</designersetting>
                                                                                 <designersetting key="group">1 Hulpmiddelen</designersetting>
                                                                                 <designersetting key="visible">True</designersetting>
                                                                             </integerparameter>
                                                                         </definition>
                                                                     </collectionparameter>
                                                                 </SharedParameterSet>
                                                             </Template>

    Public Shared ilt_compare_collection2 As XElement = <Template xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" definitionVersion="1">
                                                            <Description></Description>
                                                            <Settings>
                                                            </Settings>
                                                            <Targets>
                                                                <Target xsi:type="ItemLayoutTemplateTarget" enabled="true" name="word">
                                                                    <Description>Alleen voor word 2010+</Description>
                                                                    <Template><![CDATA[<html><body></body></html>]]></Template>
                                                                </Target>
                                                                <Target xsi:type="ItemLayoutTemplateTarget" enabled="true" name="ces">
                                                                    <Description>CES</Description>
                                                                    <Template>
                                                                        <![CDATA[
				                            <html>
					                            <body>
						                            <cito:control xmlns:cito="http://www.cito.nl/citotester" id="invoer" type="min.compare.collection2" />
					                            </body>
				                            </html>
			                            ]]>
                                                                    </Template>
                                                                </Target>
                                                            </Targets>
                                                        </Template>

    Public Shared min_compare_collection_ctrl2 As XElement = <Template xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" definitionVersion="1">
                                                                 <Description></Description>
                                                                 <Targets>
                                                                     <Target xsi:type="ControlTemplateTarget" enabled="true" name="Word">
                                                                         <Description>Word</Description>
                                                                         <Template><![CDATA[<html><body></body></html>]]></Template>
                                                                     </Target>
                                                                     <Target xsi:type="ControlTemplateTarget" enabled="true" name="ces">
                                                                         <Description>CES</Description>
                                                                         <Template><![CDATA[<html><body><%#itemGeneral%></body></html>]]></Template>
                                                                     </Target>
                                                                 </Targets>
                                                                 <SharedFunctions>
                                                                 </SharedFunctions>
                                                                 <SharedParameterSet id="parameters">
                                                                     <collectionparameter name="collectionParam2">
                                                                         <designersetting key="label"></designersetting>
                                                                         <designersetting key="itemcountlabel">Aantal audio bestanden</designersetting>
                                                                         <designersetting key="description"></designersetting>
                                                                         <designersetting key="minimumLength">0</designersetting>
                                                                         <designersetting key="maximumLength">1</designersetting>
                                                                         <designersetting key="defaultvalue">0</designersetting>
                                                                         <designersetting key="subsetidentifiers">Numeric</designersetting>
                                                                         <designersetting key="group">2 Verklanking</designersetting>
                                                                         <designersetting key="required">true</designersetting>
                                                                         <designersetting key="sortkey">2</designersetting>
                                                                         <definition>
                                                                             <integerparameter name="integerParameter1">
                                                                                 <designersetting key="label">Aantal1</designersetting>
                                                                                 <designersetting key="description">Een omschrijving.</designersetting>
                                                                                 <designersetting key="required">False</designersetting>
                                                                                 <designersetting key="sortkey">1</designersetting>
                                                                                 <designersetting key="group">1 Hulpmiddelen</designersetting>
                                                                                 <designersetting key="visible">True</designersetting>
                                                                             </integerparameter>
                                                                             <integerparameter name="integerParameter2">
                                                                                 <designersetting key="label">Aantal2</designersetting>
                                                                                 <designersetting key="description">Een omschrijving.</designersetting>
                                                                                 <designersetting key="required">False</designersetting>
                                                                                 <designersetting key="sortkey">1</designersetting>
                                                                                 <designersetting key="group">1 Hulpmiddelen</designersetting>
                                                                                 <designersetting key="visible">True</designersetting>
                                                                             </integerparameter>
                                                                         </definition>
                                                                     </collectionparameter>
                                                                 </SharedParameterSet>
                                                             </Template>



    Public Shared ilt_compare_xhtml1 As XElement = <Template xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" definitionVersion="1">
                                                       <Description></Description>
                                                       <Settings>
                                                       </Settings>
                                                       <Targets>
                                                           <Target xsi:type="ItemLayoutTemplateTarget" enabled="true" name="word">
                                                               <Description>Alleen voor word 2010+</Description>
                                                               <Template><![CDATA[<html><body></body></html>]]></Template>
                                                           </Target>
                                                           <Target xsi:type="ItemLayoutTemplateTarget" enabled="true" name="ces">
                                                               <Description>CES</Description>
                                                               <Template>
                                                                   <![CDATA[
				                            <html>
					                            <body>
						                            <cito:control xmlns:cito="http://www.cito.nl/citotester" id="invoer" type="min.compare.xhtml1" />
					                            </body>
				                            </html>
			                            ]]>
                                                               </Template>
                                                           </Target>
                                                       </Targets>
                                                   </Template>


    Public Shared min_compare_xhtml_ctrl1 As XElement = <Template xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" definitionVersion="1">
                                                            <Description></Description>
                                                            <Targets>
                                                                <Target xsi:type="ControlTemplateTarget" enabled="true" name="Word">
                                                                    <Description>Word</Description>
                                                                    <Template><![CDATA[<html><body></body></html>]]></Template>
                                                                </Target>
                                                                <Target xsi:type="ControlTemplateTarget" enabled="true" name="ces">
                                                                    <Description>CES</Description>
                                                                    <Template><![CDATA[<html><body><%#itemGeneral%></body></html>]]></Template>
                                                                </Target>
                                                            </Targets>
                                                            <SharedFunctions>
                                                            </SharedFunctions>
                                                            <SharedParameterSet id="parameters">
                                                                <xhtmlparameter name="xhtmlParameter1">
                                                                    <designersetting key="label">Beschrijving</designersetting>
                                                                    <designersetting key="description">Een omschrijving.</designersetting>
                                                                    <designersetting key="required">False</designersetting>
                                                                    <designersetting key="sortkey">1</designersetting>
                                                                    <designersetting key="group">1 Hulpmiddelen</designersetting>
                                                                    <designersetting key="visible">True</designersetting>
                                                                </xhtmlparameter>
                                                            </SharedParameterSet>
                                                        </Template>

    Public Shared ilt_compare_xhtml2 As XElement = <Template xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" definitionVersion="1">
                                                       <Description></Description>
                                                       <Settings>
                                                       </Settings>
                                                       <Targets>
                                                           <Target xsi:type="ItemLayoutTemplateTarget" enabled="true" name="word">
                                                               <Description>Alleen voor word 2010+</Description>
                                                               <Template><![CDATA[<html><body></body></html>]]></Template>
                                                           </Target>
                                                           <Target xsi:type="ItemLayoutTemplateTarget" enabled="true" name="ces">
                                                               <Description>CES</Description>
                                                               <Template>
                                                                   <![CDATA[
				                            <html>
					                            <body>
						                            <cito:control xmlns:cito="http://www.cito.nl/citotester" id="invoer" type="min.compare.xhtml2" />
					                            </body>
				                            </html>
			                            ]]>
                                                               </Template>
                                                           </Target>
                                                       </Targets>
                                                   </Template>

    Public Shared min_compare_xhtml_ctrl2 As XElement = <Template xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" definitionVersion="1">
                                                            <Description></Description>
                                                            <Targets>
                                                                <Target xsi:type="ControlTemplateTarget" enabled="true" name="Word">
                                                                    <Description>Word</Description>
                                                                    <Template><![CDATA[<html><body></body></html>]]></Template>
                                                                </Target>
                                                                <Target xsi:type="ControlTemplateTarget" enabled="true" name="ces">
                                                                    <Description>CES</Description>
                                                                    <Template><![CDATA[<html><body><%#itemGeneral%></body></html>]]></Template>
                                                                </Target>
                                                            </Targets>
                                                            <SharedFunctions>
                                                            </SharedFunctions>
                                                            <SharedParameterSet id="parameters">
                                                                <xhtmlparameter name="xhtmlParameter2">
                                                                    <designersetting key="label">Beschrijving</designersetting>
                                                                    <designersetting key="description">Een omschrijving.</designersetting>
                                                                    <designersetting key="required">False</designersetting>
                                                                    <designersetting key="sortkey">1</designersetting>
                                                                    <designersetting key="group">1 Hulpmiddelen</designersetting>
                                                                    <designersetting key="visible">True</designersetting>
                                                                </xhtmlparameter>
                                                            </SharedParameterSet>
                                                        </Template>



    Public Shared ilt_compare_listed1 As XElement = <Template xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" definitionVersion="1">
                                                        <Description></Description>
                                                        <Settings>
                                                        </Settings>
                                                        <Targets>
                                                            <Target xsi:type="ItemLayoutTemplateTarget" enabled="true" name="word">
                                                                <Description>Alleen voor word 2010+</Description>
                                                                <Template><![CDATA[<html><body></body></html>]]></Template>
                                                            </Target>
                                                            <Target xsi:type="ItemLayoutTemplateTarget" enabled="true" name="ces">
                                                                <Description>CES</Description>
                                                                <Template>
                                                                    <![CDATA[
				                            <html>
					                            <body>
						                            <cito:control xmlns:cito="http://www.cito.nl/citotester" id="invoer" type="min.compare.listed1" />
					                            </body>
				                            </html>
			                            ]]>
                                                                </Template>
                                                            </Target>
                                                        </Targets>
                                                    </Template>


    Public Shared min_compare_listed_ctrl1 As XElement = <Template xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" definitionVersion="1">
                                                             <Description></Description>
                                                             <Targets>
                                                                 <Target xsi:type="ControlTemplateTarget" enabled="true" name="Word">
                                                                     <Description>Word</Description>
                                                                     <Template><![CDATA[<html><body></body></html>]]></Template>
                                                                 </Target>
                                                                 <Target xsi:type="ControlTemplateTarget" enabled="true" name="ces">
                                                                     <Description>CES</Description>
                                                                     <Template><![CDATA[<html><body><%#itemGeneral%></body></html>]]></Template>
                                                                 </Target>
                                                             </Targets>
                                                             <SharedFunctions>
                                                             </SharedFunctions>
                                                             <SharedParameterSet id="parameters">
                                                                 <listedparameter name="listedParameter1">
                                                                     <designersetting key="label">Max. sleutels</designersetting>
                                                                     <designersetting key="description">Geef aan uit hoeveel sleutels het antwoord maximaal bestaat.</designersetting>
                                                                     <designersetting key="group">6 Alternatieven</designersetting>
                                                                     <designersetting key="visible">True</designersetting>
                                                                     <designersetting key="defaultvalue">1</designersetting>
                                                                     <designersetting key="sortkey">1</designersetting>
                                                                     <designersetting key="list">
                                                                         <listvalues>
                                                                             <listvalue key="0">Onbeperkt</listvalue>
                                                                             <listvalue key="1">1</listvalue>
                                                                             <listvalue key="2">2</listvalue>
                                                                             <listvalue key="3">3</listvalue>
                                                                             <listvalue key="4">4</listvalue>
                                                                             <listvalue key="5">5</listvalue>
                                                                             <listvalue key="6">6</listvalue>
                                                                             <listvalue key="7">7</listvalue>
                                                                             <listvalue key="8">8</listvalue>
                                                                             <listvalue key="9">9</listvalue>
                                                                         </listvalues>
                                                                     </designersetting>
                                                                 </listedparameter>
                                                             </SharedParameterSet>
                                                         </Template>

    Public Shared ilt_compare_listed2 As XElement = <Template xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" definitionVersion="1">
                                                        <Description></Description>
                                                        <Settings>
                                                        </Settings>
                                                        <Targets>
                                                            <Target xsi:type="ItemLayoutTemplateTarget" enabled="true" name="word">
                                                                <Description>Alleen voor word 2010+</Description>
                                                                <Template><![CDATA[<html><body></body></html>]]></Template>
                                                            </Target>
                                                            <Target xsi:type="ItemLayoutTemplateTarget" enabled="true" name="ces">
                                                                <Description>CES</Description>
                                                                <Template>
                                                                    <![CDATA[
				                            <html>
					                            <body>
						                            <cito:control xmlns:cito="http://www.cito.nl/citotester" id="invoer" type="min.compare.listed2" />
					                            </body>
				                            </html>
			                            ]]>
                                                                </Template>
                                                            </Target>
                                                        </Targets>
                                                    </Template>

    Public Shared min_compare_listed_ctrl2 As XElement = <Template xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" definitionVersion="1">
                                                             <Description></Description>
                                                             <Targets>
                                                                 <Target xsi:type="ControlTemplateTarget" enabled="true" name="Word">
                                                                     <Description>Word</Description>
                                                                     <Template><![CDATA[<html><body></body></html>]]></Template>
                                                                 </Target>
                                                                 <Target xsi:type="ControlTemplateTarget" enabled="true" name="ces">
                                                                     <Description>CES</Description>
                                                                     <Template><![CDATA[<html><body><%#itemGeneral%></body></html>]]></Template>
                                                                 </Target>
                                                             </Targets>
                                                             <SharedFunctions>
                                                             </SharedFunctions>
                                                             <SharedParameterSet id="parameters">
                                                                 <listedparameter name="listedParameter2">
                                                                     <designersetting key="label">Max. sleutels</designersetting>
                                                                     <designersetting key="description">Geef aan uit hoeveel sleutels het antwoord maximaal bestaat.</designersetting>
                                                                     <designersetting key="group">6 Alternatieven</designersetting>
                                                                     <designersetting key="visible">True</designersetting>
                                                                     <designersetting key="defaultvalue">1</designersetting>
                                                                     <designersetting key="sortkey">1</designersetting>
                                                                     <designersetting key="list">
                                                                         <listvalues>
                                                                             <listvalue key="0">Onbeperkt</listvalue>
                                                                             <listvalue key="1">1</listvalue>
                                                                             <listvalue key="2">2</listvalue>
                                                                             <listvalue key="3">3</listvalue>
                                                                             <listvalue key="4">4</listvalue>
                                                                             <listvalue key="5">5</listvalue>
                                                                             <listvalue key="6">6</listvalue>
                                                                             <listvalue key="7">7</listvalue>
                                                                             <listvalue key="8">8</listvalue>
                                                                             <listvalue key="9">9</listvalue>
                                                                         </listvalues>
                                                                     </designersetting>
                                                                 </listedparameter>
                                                             </SharedParameterSet>
                                                         </Template>



End Class