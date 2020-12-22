
Imports System.IO
Imports System.Xml.Linq
Imports Cito.Tester.Common
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.UnitTests.Framework.FakeAppTemplate

Partial Public Class ItemTestData
    Public Shared Sub AddInlineItemTemplatesAndControlTemplates()
        With FakeDal.Add
            .ItemTemplate("choice.inline.dc", Sub(i) SetResourceIdAndXmlAsBinData(i, ilt_choice_inline_dc)).
                DependsOn.ControlTemplate("Cito.Generic.Interaction.Base.Inline", Sub(c) SetResourceIdAndXmlAsBinData(c, ct_base_inline))

            .ItemTemplate("InlineChoiceLayoutTemplate", Sub(ilt) SetResourceIdAndXmlAsBinData(ilt, ilt_Generic_InlineChoiceLayoutTemplate)).
                DependsOn.ControlTemplate("InlineChoice", Sub(c) SetResourceIdAndXmlAsBinData(c, ct_InlineChoice))

            .ItemTemplate("Cito.Generic.Gaps.Inline.DC", Sub(ilt) SetResourceIdAndXmlAsBinData(ilt, ilt_Gaps_Inline_DC)).
                DependsOn.ControlTemplate("Cito.Generic.Interaction.Base.Inline")

            .ItemTemplate("InlineGapStringLayoutTemplate", Sub(ilt) SetResourceIdAndXmlAsBinData(ilt, ilt_InlineGapStringLayoutTemplate)).
                DependsOn.ControlTemplate("InlineGap.String", Sub(ct) SetResourceIdAndXmlAsBinData(ct, ct_GenericInlineGapString))

            .ItemTemplate("InlineGapIntegerLayoutTemplate", Sub(ilt) SetResourceIdAndXmlAsBinData(ilt, ilt_InlineGapIntegerLayoutTemplate)).
                DependsOn.ControlTemplate("InlineGap.Integer", Sub(ct) SetResourceIdAndXmlAsBinData(ct, ct_GenericInlineGapInteger))

            .ItemTemplate("InlineGapDecimalLayoutTemplate", Sub(ilt) SetResourceIdAndXmlAsBinData(ilt, ilt_InlineGapDecimalLayoutTemplate)).
                DependsOn.ControlTemplate("InlineGap.Decimal", Sub(ct) SetResourceIdAndXmlAsBinData(ct, ct_GenericInlineGapDecimal))

            .ItemTemplate("InlineVideoLayoutTemplate", Sub(ilt) SetResourceIdAndXmlAsBinData(ilt, InlineVideo))

            .GenericResource("GenericVideoResource", Nothing)
        End With
    End Sub

    Public Shared Sub SetResourceIdAndXmlAsBinData(ByVal resource As ResourceEntity, ByVal data As XElement)
        resource.ResourceId = Guid.NewGuid()

        If (resource.ResourceData Is Nothing) Then resource.ResourceData = New ResourceDataEntity

        Using stream = New MemoryStream()
            SerializeHelper.XmlSerializeToStream(stream, data)
            resource.ResourceData.BinData = stream.ToArray()
        End Using
    End Sub


    Friend Shared ReadOnly item_choice_inline_dc As XElement = <assessmentItem xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" identifier="itm_inlinechoice" itemid="testId" title="code" layoutTemplateSrc="choice.inline.dc">
                                                                   <solution>
                                                                       <keyFindings>
                                                                           <keyFinding id="inlineChoideController" scoringMethod="Dichotomous">
                                                                               <keyFact id="A-I00000000-0000-0000-0000-000000000000" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                   <keyValue domain="I00000000-0000-0000-0000-000000000000" occur="1">
                                                                                       <stringValue>
                                                                                           <typedValue>A</typedValue>
                                                                                       </stringValue>
                                                                                   </keyValue>
                                                                               </keyFact>
                                                                               <keyFact id="B-I00000000-0000-0000-0000-000000000001" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                   <keyValue domain="I00000000-0000-0000-0000-000000000001" occur="1">
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
                                                                           <listedparameter name="inlineType">choice</listedparameter>
                                                                           <plaintextparameter name="inlineClass"/>
                                                                           <integerparameter name="maxChoices">0</integerparameter>
                                                                           <xhtmlparameter name="leftBody">
                                                                               <p id="c1-id-11" xmlns="http://www.w3.org/1999/xhtml"><strong id="c1-id-12">Patterns</strong></p>
                                                                               <p id="c1-id-13" xmlns="http://www.w3.org/1999/xhtml">Beantwoord de onderstaande vragen</p>
                                                                           </xhtmlparameter>
                                                                           <xhtmlparameter name="itemBody"/>
                                                                           <xhtmlparameter name="itemQuestion">
                                                                               <p id="c1-id-11" xmlns="http://www.w3.org/1999/xhtml">Hieronder staan een aantal vragen. Beantwoord ze.
                                                                                 <cito:InlineElement id="958327f1-6b1a-4669-b852-c4064b871c62" layoutTemplateSourceName="InlineVideoLayoutTemplate" xmlns:cito="http://www.cito.nl/citotester">
                                                                                       <cito:parameters>
                                                                                           <cito:parameterSet id="entireItem">
                                                                                               <cito:plaintextparameter name="controlId">VID_4964eaa1-b17f-4c00-aaaf-42d76eb6b42c</cito:plaintextparameter>
                                                                                               <cito:resourceparameter name="sourceWebm">GenericVideoResource</cito:resourceparameter>
                                                                                               <cito:resourceparameter name="sourceMp4"/>
                                                                                               <cito:resourceparameter name="sourceMpg"/>
                                                                                               <cito:booleanparameter name="autoStart">False</cito:booleanparameter>
                                                                                               <cito:integerparameter name="width">384</cito:integerparameter>
                                                                                               <cito:integerparameter name="height">288</cito:integerparameter>
                                                                                               <cito:booleanparameter name="showToolbar">True</cito:booleanparameter>
                                                                                               <cito:booleanparameter name="showPlayButton">True</cito:booleanparameter>
                                                                                               <cito:booleanparameter name="showPauseButton">False</cito:booleanparameter>
                                                                                               <cito:booleanparameter name="showStopButton">False</cito:booleanparameter>
                                                                                               <cito:booleanparameter name="showTimeSlider">False</cito:booleanparameter>
                                                                                               <cito:integerparameter name="maxPlay">0</cito:integerparameter>
                                                                                               <cito:booleanparameter name="showElapsedTime">False</cito:booleanparameter>
                                                                                               <cito:booleanparameter name="showTotalTime">False</cito:booleanparameter>
                                                                                               <cito:booleanparameter name="showFastforwardButton">False</cito:booleanparameter>
                                                                                               <cito:booleanparameter name="showRewindButton">False</cito:booleanparameter>
                                                                                               <cito:plaintextparameter name="mediaPlayerDescription"/>
                                                                                           </cito:parameterSet>
                                                                                       </cito:parameters>
                                                                                   </cito:InlineElement>
                                                                               </p>
                                                                           </xhtmlparameter>
                                                                           <xhtmlparameter name="itemInlineInput">
                                                                               <p id="c1-id-11" xmlns="http://www.w3.org/1999/xhtml">Vraag 1:</p>
                                                                               <p id="c1-id-12" xmlns="http://www.w3.org/1999/xhtml">
                                                                                   <cito:InlineElement id="I00000000-0000-0000-0000-000000000000" layoutTemplateSourceName="InlineChoiceLayoutTemplate" inlineFO="" xmlns:cito="http://www.cito.nl/citotester">
                                                                                       <cito:parameters>
                                                                                           <cito:parameterSet id="entireItem">
                                                                                               <cito:plaintextparameter name="inlineChoiceId">I00000000-0000-0000-0000-000000000000</cito:plaintextparameter>
                                                                                               <cito:plaintextparameter name="inlineChoiceLabel">Vraag1</cito:plaintextparameter>
                                                                                               <cito:inlineChoiceScoringParameter name="inlineChoiceScoring" label="Vraag1" ControllerId="inlineChoiceController" findingOverride="inlineChoiceController" minChoices="0" maxChoices="1">
                                                                                                   <cito:subparameterset id="A">
                                                                                                       <cito:plaintextparameter name="icOption">A</cito:plaintextparameter>
                                                                                                   </cito:subparameterset>
                                                                                                   <cito:subparameterset id="B">
                                                                                                       <cito:plaintextparameter name="icOption">B</cito:plaintextparameter>
                                                                                                   </cito:subparameterset>
                                                                                                   <cito:subparameterset id="C">
                                                                                                       <cito:plaintextparameter name="icOption">C</cito:plaintextparameter>
                                                                                                   </cito:subparameterset>
                                                                                                   <cito:subparameterset id="D">
                                                                                                       <cito:plaintextparameter name="icOption">D</cito:plaintextparameter>
                                                                                                   </cito:subparameterset>
                                                                                                   <cito:definition id="">
                                                                                                       <cito:plaintextparameter name="icOption"/>
                                                                                                   </cito:definition>
                                                                                               </cito:inlineChoiceScoringParameter>
                                                                                           </cito:parameterSet>
                                                                                       </cito:parameters>
                                                                                   </cito:InlineElement>
                                                                               </p>
                                                                               <p id="c1-id-13" xmlns="http://www.w3.org/1999/xhtml">Vraag 2</p>
                                                                               <p id="c1-id-14" xmlns="http://www.w3.org/1999/xhtml">
                                                                                   <cito:InlineElement id="I00000000-0000-0000-0000-000000000001" layoutTemplateSourceName="InlineChoiceLayoutTemplate" inlineFO="" xmlns:cito="http://www.cito.nl/citotester">
                                                                                       <cito:parameters>
                                                                                           <cito:parameterSet id="entireItem">
                                                                                               <cito:plaintextparameter name="inlineChoiceId">I00000000-0000-0000-0000-000000000001</cito:plaintextparameter>
                                                                                               <cito:plaintextparameter name="inlineChoiceLabel">Vraag2</cito:plaintextparameter>
                                                                                               <cito:inlineChoiceScoringParameter name="inlineChoiceScoring" label="Vraag2" ControllerId="inlineChoiceController" findingOverride="inlineChoiceController" minChoices="0" maxChoices="1">
                                                                                                   <cito:subparameterset id="A">
                                                                                                       <cito:plaintextparameter name="icOption">A</cito:plaintextparameter>
                                                                                                   </cito:subparameterset>
                                                                                                   <cito:subparameterset id="B">
                                                                                                       <cito:plaintextparameter name="icOption">B</cito:plaintextparameter>
                                                                                                   </cito:subparameterset>
                                                                                                   <cito:subparameterset id="C">
                                                                                                       <cito:plaintextparameter name="icOption">C</cito:plaintextparameter>
                                                                                                   </cito:subparameterset>
                                                                                                   <cito:subparameterset id="D">
                                                                                                       <cito:plaintextparameter name="icOption">D</cito:plaintextparameter>
                                                                                                   </cito:subparameterset>
                                                                                                   <cito:definition id="">
                                                                                                       <cito:plaintextparameter name="icOption"/>
                                                                                                   </cito:definition>
                                                                                               </cito:inlineChoiceScoringParameter>
                                                                                           </cito:parameterSet>
                                                                                       </cito:parameters>
                                                                                   </cito:InlineElement>
                                                                               </p>
                                                                           </xhtmlparameter>
                                                                           <xhtmlparameter name="itemGeneral"/>
                                                                       </parameterSet>
                                                                   </parameters>
                                                               </assessmentItem>

    Friend Shared ReadOnly ilt_choice_inline_dc As XElement = <Template xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" definitionVersion="1">
                                                                  <Description></Description>
                                                                  <Settings>
                                                                      <DesignerSetting key="sort">True</DesignerSetting>
                                                                      <DesignerSetting key="inlineAudioTemplate">InlineAudioLayoutTemplate</DesignerSetting>
                                                                      <DesignerSetting key="inlineImageTemplate">InlineImageLayoutTemplate</DesignerSetting>
                                                                      <DesignerSetting key="inlineVideoTemplate">InlineVideoLayoutTemplate</DesignerSetting>
                                                                  </Settings>
                                                                  <Targets>
                                                                      <Target xsi:type="ItemLayoutTemplateTarget" enabled="true" name="ces">
                                                                          <Description></Description>
                                                                          <Template>
                                                                              <![CDATA[
                                                                          <html>
                                                                              <head>
                                                                              </head>
                                                                              <body>
                                                                                  <cito:control xmlns:cito="http://www.cito.nl/citotester" id="entireItem" type="Cito.Generic.Interaction.Base.Inline">
                                                                                      <parameter name="dualColumnLayout">
                                                                                          <designersetting key="defaultvalue">True</designersetting>
                                                                                      </parameter>
                                                                                      <parameter name="leftBody">
                                                                                          <designersetting key="visible">True</designersetting>
                                                                                      </parameter>
                                                                                      <parameter name="inlineType">
                                                                                          <designersetting key="defaultvalue">choice</designersetting>
                                                                                      </parameter>
                                                                                      <parameter name="itemInlineInput">
                                                                                          <designersetting key="inlinetemplate">InlineChoiceLayoutTemplate</designersetting>
                                                                                      </parameter>
                                                                                  </cito:control>
                                                                              </body>
                                                                          </html>]]>
                                                                          </Template>
                                                                      </Target>
                                                                  </Targets>
                                                              </Template>

    Friend Shared ReadOnly ilt_Generic_InlineChoiceLayoutTemplate As XElement = <Template xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" definitionVersion="1">
                                                                                    <Description></Description>
                                                                                    <Targets>
                                                                                        <Target xsi:type="ItemLayoutTemplateTarget" enabled="true" name="ces">
                                                                                            <Description/>
                                                                                            <Template>
                                                                                                <html xmlns:cito="http://www.cito.nl/citotester">
                                                                                                    <cito:control id="entireItem" type="InlineChoice">
                                                                                                    </cito:control>
                                                                                                </html>
                                                                                            </Template>
                                                                                        </Target>
                                                                                    </Targets>
                                                                                </Template>

    Friend Shared ReadOnly ct_base_inline As XElement = <Template xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" definitionVersion="1">
                                                            <Description></Description>
                                                            <Settings>
                                                                <DesignerSetting key="sort">True</DesignerSetting>
                                                            </Settings>
                                                            <Targets>
                                                                <Target xsi:type="ControlTemplateTarget" enabled="true" name="ces">
                                                                    <Description>Questify Player</Description>
                                                                    <Template>
                                                                        <![CDATA[
]]>
                                                                    </Template>
                                                                </Target>
                                                            </Targets>
                                                            <SharedFunctions></SharedFunctions>
                                                            <SharedParameterSet id="parameters">
                                                                <booleanparameter name="dualColumnLayout">
                                                                    <designersetting key="visible">false</designersetting>
                                                                    <designersetting key="defaultvalue">false</designersetting>
                                                                </booleanparameter>
                                                                <listedparameter name="inlineType">
                                                                    <designersetting key="label">Type inline control</designersetting>
                                                                    <designersetting key="required">true</designersetting>
                                                                    <designersetting key="visible">false</designersetting>
                                                                    <designersetting key="defaultvalue">input</designersetting>
                                                                    <designersetting key="list">
                                                                        <listvalues>
                                                                            <listvalue key="input">Input</listvalue>
                                                                            <listvalue key="choice">Choice</listvalue>
                                                                            <listvalue key="combined">Combinatie</listvalue>
                                                                            <listvalue key="hottext">Hottext</listvalue>
                                                                            <listvalue key="hottextcorrection">Selecteren en verbeteren</listvalue>
                                                                        </listvalues>
                                                                    </designersetting>
                                                                </listedparameter>
                                                                <plaintextparameter name="inlineClass">
                                                                    <designersetting key="defaultvalue"/>
                                                                    <designersetting key="visible">false</designersetting>
                                                                </plaintextparameter>
                                                                <integerparameter name="maxChoices">
                                                                    <designersetting key="label">Max aantal selecties</designersetting>
                                                                    <designersetting key="group">3</designersetting>
                                                                    <designersetting key="required">true</designersetting>
                                                                    <designersetting key="visible">false</designersetting>
                                                                    <designersetting key="defaultvalue">0</designersetting>
                                                                </integerparameter>
                                                                <xhtmlparameter name="leftBody">
                                                                    <designersetting key="label">Body links</designersetting>
                                                                    <designersetting key="required">false</designersetting>
                                                                    <designersetting key="visible">false</designersetting>
                                                                    <designersetting key="group">3</designersetting>
                                                                </xhtmlparameter>
                                                                <xhtmlparameter name="itemBody">
                                                                    <designersetting key="label">Body</designersetting>
                                                                    <designersetting key="required">false</designersetting>
                                                                    <designersetting key="visible">false</designersetting>
                                                                    <designersetting key="group">4</designersetting>
                                                                </xhtmlparameter>
                                                                <xhtmlparameter name="itemQuestion">
                                                                    <designersetting key="label">Vraag</designersetting>
                                                                    <designersetting key="required">false</designersetting>
                                                                    <designersetting key="visible">true</designersetting>
                                                                    <designersetting key="inlinetemplate"></designersetting>
                                                                    <designersetting key="inlinetemplates"></designersetting>
                                                                    <designersetting key="group">4</designersetting>
                                                                </xhtmlparameter>
                                                                <xhtmlparameter name="itemInlineInput">
                                                                    <designersetting key="label">Invoervak</designersetting>
                                                                    <designersetting key="required">True</designersetting>
                                                                    <designersetting key="visible">True</designersetting>
                                                                    <designersetting key="inlinetemplate"/>
                                                                    <designersetting key="inlinetemplates"/>
                                                                    <designersetting key="group">4</designersetting>
                                                                </xhtmlparameter>
                                                                <xhtmlparameter name="itemGeneral">
                                                                    <designersetting key="label">Algemeen tekstveld</designersetting>
                                                                    <designersetting key="group">6</designersetting>
                                                                    <designersetting key="required">false</designersetting>
                                                                    <designersetting key="visible">false</designersetting>
                                                                </xhtmlparameter>
                                                            </SharedParameterSet>
                                                        </Template>

    Friend Shared ReadOnly ct_InlineChoice As XElement = <Template xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" definitionVersion="2">
                                                             <Description/>
                                                             <Targets>
                                                                 <Target xsi:type="ControlTemplateTarget" enabled="true" name="ces">
                                                                     <Description/>
                                                                     <Template><![CDATA[
                                                                          <%
                                                                          Dim controlId As String = GetInlineChoiceId(parameters)
                                                                          controlId = RemoveIllegalCharacters(controlId)
                                                                     ]]>
                                                                     </Template>
                                                                 </Target>
                                                             </Targets>
                                                             <SharedFunctions><![CDATA[
                                                                 Private Shared Function GetInlineChoiceId(Byval parameters As Cito.Tester.ContentModel.ParameterCollection) As String	
				                                                    If string.IsNullOrEmpty(parameters.GetParameterByName("inlineChoiceId").Value) then
					                                                    parameters.GetParameterByName("inlineChoiceId").Value = Guid.NewGuid().ToString()
				                                                    end if
				                                                    Return parameters.GetParameterByName("inlineChoiceId").Value
			                                                    End function
]]>
                                                             </SharedFunctions>
                                                             <SharedParameterSet>
                                                                 <plaintextparameter name="inlineChoiceid">
                                                                     <designersetting key="label"/>
                                                                     <designersetting key="visible">False</designersetting>
                                                                 </plaintextparameter>
                                                                 <plaintextparameter name="inlineChoiceLabel">
                                                                     <designersetting key="label">Label</designersetting>
                                                                     <designersetting key="visible">True</designersetting>
                                                                     <designersetting key="required">True</designersetting>
                                                                     <designersetting key="group">1</designersetting>
                                                                 </plaintextparameter>
                                                                 <inlineChoiceScoringparameter ControllerId="inlineChoiceController" name="inlineChoiceScoring" maxChoices="1" findingOverride="inlineChoiceController">
                                                                     <attributereference name="label">inlineChoiceLabel</attributereference>
                                                                     <designersetting key="label"/>
                                                                     <designersetting key="itemcountlabel">Aantal alternatieven</designersetting>
                                                                     <designersetting key="visible">true</designersetting>
                                                                     <designersetting key="minimumLength">2</designersetting>
                                                                     <designersetting key="maximumLength">12</designersetting>
                                                                     <designersetting key="subsetidentifiers">Alphabetic</designersetting>
                                                                     <designersetting key="group">2</designersetting>
                                                                     <definition>
                                                                         <plaintextparameter name="icOption">
                                                                             <designersetting key="label">Keuze</designersetting>
                                                                             <designersetting key="required">True</designersetting>
                                                                         </plaintextparameter>
                                                                     </definition>
                                                                 </inlineChoiceScoringparameter>
                                                             </SharedParameterSet>
                                                         </Template>

    Friend Shared ReadOnly ilt_Gaps_Inline_DC As XElement = <Template xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" definitionVersion="1">
                                                                <Description>Gap item (inline) met twee kolommen</Description>
                                                                <Settings>
                                                                    <DesignerSetting key="sort">True</DesignerSetting>
                                                                    <DesignerSetting key="inlineAudioTemplate">InlineAudioLayoutTemplate</DesignerSetting>
                                                                    <DesignerSetting key="inlineImageTemplate">InlineImageLayoutTemplate</DesignerSetting>
                                                                    <DesignerSetting key="inlineVideoTemplate">InlineVideoLayoutTemplate</DesignerSetting>
                                                                </Settings>
                                                                <Targets>
                                                                    <Target xsi:type="ItemLayoutTemplateTarget" enabled="true" name="ces">
                                                                        <Description>TestPlayer 1.x / 2.x</Description>
                                                                        <Template>
                                                                            <![CDATA[
				<html>
					<head>
						<link rel="Stylesheet" href="resource://package/Generic.css" type="text/css" /> 
						<link rel="Stylesheet" href="resource://package/userstyle.css" type="text/css" /> 
					</head>
					<body>
						<cito:control xmlns:cito="http://www.cito.nl/citotester" id="entireItem" type="Cito.Generic.Interaction.Base.Inline">
							<parameter name="dualColumnLayout">
								<designersetting key="defaultvalue">True</designersetting>
							</parameter>
							<parameter name="leftBody">
								<designersetting key="visible">True</designersetting>
							</parameter>
							<parameter name="inlineType">
								<designersetting key="defaultvalue">input</designersetting>
							</parameter>
							<parameter name="itemInlineInput">
								<designersetting key="inlinetemplate">InlineGapStringLayoutTemplate</designersetting>
								<designersetting key="inlinetemplates">
									(template=InlineGapStringLayoutTemplate;text=Tekst)
									(template=InlineGapIntegerLayoutTemplate;text=Geheel getal)
									(template=InlineGapDecimalLayoutTemplate;text=Decimaal)
									(template=InlineGapCurrencyLayoutTemplate;text=Valuta)
									(template=InlineGapDateLayoutTemplate;text=Datum)
									(template=InlineGapTimeLayoutTemplate;text=Tijd)
									(template=InlineGapFormulaLayoutTemplate;text=Formule)
									(template=InlineGapFormulaCasEqualLayoutTemplate;text=Cas Equal Formule)
								</designersetting>
							</parameter>
						</cito:control>
					</body>
				</html>
				]]>
                                                                        </Template>
                                                                    </Target>
                                                                </Targets>
                                                            </Template>

    Friend Shared ReadOnly ilt_InlineGapStringLayoutTemplate As XElement = <Template xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" definitionVersion="1">
                                                                               <Description/>
                                                                               <Targets>
                                                                                   <Target xsi:type="ItemLayoutTemplateTarget" enabled="true" name="ces">
                                                                                       <Description/>
                                                                                       <Template>
                                                                                           <html xmlns:cito="http://www.cito.nl/citotester">
                                                                                               <cito:control id="entireItem" type="InlineGap.String">
                                                                                                   <parameter name="gapType">
                                                                                                       <designersetting key="defaultvalue">String</designersetting>
                                                                                                       <designersetting key="visible">False</designersetting>
                                                                                                   </parameter>
                                                                                                   <parameter name="hasStringScoring">
                                                                                                       <designersetting key="defaultvalue">True</designersetting>
                                                                                                   </parameter>
                                                                                               </cito:control>
                                                                                           </html>
                                                                                       </Template>
                                                                                   </Target>
                                                                               </Targets>
                                                                           </Template>

    Friend Shared ReadOnly ilt_InlineGapIntegerLayoutTemplate As XElement = <Template xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" definitionVersion="1">
                                                                                <Description>Dit is een test omschrijving voor ItemLayoutTemplate</Description>
                                                                                <Targets>
                                                                                    <Target xsi:type="ItemLayoutTemplateTarget" enabled="true" name="ces">
                                                                                        <Description>TestPlayer 1.x / 2.x</Description>
                                                                                        <Template><![CDATA[
				<html xmlns:cito="http://www.cito.nl/citotester">
                <cito:control id="entireItem" type="InlineGap.Integer">
                    <parameter name="gapType">
                        <designersetting key="defaultvalue">Integer</designersetting>
                        <designersetting key="visible">False</designersetting>
                    </parameter>
                    <parameter name="hasIntegerScoring">
                        <designersetting key="defaultvalue">True</designersetting>
                    </parameter>
                </cito:control>
				</html>
			]]></Template>
                                                                                    </Target>
                                                                                </Targets>
                                                                            </Template>

    Friend Shared ReadOnly ilt_InlineGapDecimalLayoutTemplate As XElement = <Template xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" definitionVersion="1">
                                                                                <Description>Dit is een test omschrijving voor ItemLayoutTemplate</Description>
                                                                                <Targets>
                                                                                    <Target xsi:type="ItemLayoutTemplateTarget" enabled="true" name="ces">
                                                                                        <Description>TestPlayer 1.x / 2.x</Description>
                                                                                        <Template><![CDATA[
				<html xmlns:cito="http://www.cito.nl/citotester">
                <cito:control id="entireItem" type="InlineGap.Decimal">
                    <parameter name="gapType">
                        <designersetting key="defaultvalue">Decimal</designersetting>
                        <designersetting key="visible">False</designersetting>
                    </parameter>
                    <parameter name="hasDecimalScoring">
                        <designersetting key="defaultvalue">True</designersetting>
                    </parameter>
                </cito:control>
				</html>
			]]></Template>
                                                                                    </Target>
                                                                                </Targets>
                                                                            </Template>

    Friend Shared ReadOnly ct_GenericInlineGapString As XElement = <Template xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" definitionVersion="1">
                                                                       <Description/>
                                                                       <Targets>
                                                                           <Target xsi:type="ControlTemplateTarget" enabled="true" name="ces">
                                                                               <Description/>
                                                                               <Template><![CDATA[
                                                                                    ]]>
                                                                               </Template>
                                                                           </Target>
                                                                       </Targets>
                                                                       <SharedFunctions><![CDATA[
                                                                            Private Shared _responseIdentifier as string = "gapController"

                                                                            Private Shared Function GetGapId(Byval parameters As Cito.Tester.ContentModel.ParameterCollection) As String	
				                                                                If string.IsNullOrEmpty(parameters.GetParameterByName("gapId").Value) then
					                                                                parameters.GetParameterByName("gapId").Value = Guid.NewGuid().ToString()
				                                                                End If
				                                                                Return parameters.GetParameterByName("gapId").Value
			                                                                End Function
                                                                       ]]></SharedFunctions>
                                                                       <SharedParameterSet>
                                                                           <plaintextparameter name="gapId">
                                                                               <designersetting key="label">label</designersetting>
                                                                               <designersetting key="visible">False</designersetting>
                                                                           </plaintextparameter>
                                                                           <plaintextparameter name="controlType">
                                                                               <designersetting key="visible">False</designersetting>
                                                                               <designersetting key="defaultvalue">input</designersetting>
                                                                           </plaintextparameter>
                                                                           <plaintextparameter name="gapLabel">
                                                                               <designersetting key="label">Label</designersetting>
                                                                               <designersetting key="description">Label waaraan het invoervak (in de score editor) kan worden herkend</designersetting>
                                                                               <designersetting key="visible">True</designersetting>
                                                                               <designersetting key="required">True</designersetting>
                                                                               <designersetting key="group">Algemeen</designersetting>
                                                                           </plaintextparameter>
                                                                           <listedparameter name="gapType">
                                                                               <designersetting key="label">Type invoer</designersetting>
                                                                               <designersetting key="group">1</designersetting>
                                                                               <designersetting key="required">True</designersetting>
                                                                               <designersetting key="visible">False</designersetting>
                                                                               <designersetting key="defaultvalue">String</designersetting>
                                                                               <designersetting key="list">
                                                                                   <listvalues>
                                                                                       <listvalue key="Integer">Integer</listvalue>
                                                                                       <listvalue key="Decimal">Decimaal</listvalue>
                                                                                       <listvalue key="String">Tekst</listvalue>
                                                                                   </listvalues>
                                                                               </designersetting>
                                                                           </listedparameter>
                                                                           <integerparameter name="nrOfCharacters">
                                                                               <designersetting key="label">Aantal karakters</designersetting>
                                                                               <designersetting key="description"></designersetting>
                                                                               <designersetting key="defaultvalue">5</designersetting>
                                                                               <designersetting key="visible">true</designersetting>
                                                                               <designersetting key="group">Tekst</designersetting>
                                                                               <designersetting key="rangeFrom">1</designersetting>
                                                                               <designersetting key="rangeTo">255</designersetting>
                                                                           </integerparameter>
                                                                           <booleanparameter name="hasStringScoring">
                                                                               <designersetting key="label"></designersetting>
                                                                               <designersetting key="defaultvalue">False</designersetting>
                                                                               <designersetting key="visible">False</designersetting>
                                                                           </booleanparameter>
                                                                           <stringScoringParameter ControllerId="gapController" name="stringScoring" findingOverride="gapController">
                                                                               <attributereference name="label">gapLabel</attributereference>
                                                                               <attributereference name="expectedLength">nrOfCharacters</attributereference>
                                                                               <designersetting key="PreprocessRules">(ConvertToLower,ConvertYToIJ,RemoveAllSpaces,RemoveApostrophs,RemoveDiacritics,RemoveHyphens)</designersetting>
                                                                               <designersetting key="visible">true</designersetting>
                                                                               <designersetting key="minimumLength">1</designersetting>
                                                                               <designersetting key="maximumLength">1</designersetting>
                                                                               <definition>
                                                                                   <booleanparameter name="fictiveString">
                                                                                       <designersetting key="label"></designersetting>
                                                                                       <designersetting key="description"></designersetting>
                                                                                       <designersetting key="defaultvalue">true</designersetting>
                                                                                   </booleanparameter>
                                                                               </definition>
                                                                           </stringScoringParameter>
                                                                       </SharedParameterSet>
                                                                   </Template>

    Friend Shared ReadOnly ct_GenericInlineGapInteger As XElement = <Template xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" definitionVersion="1">
                                                                        <Description></Description>
                                                                        <Targets>
                                                                            <Target xsi:type="ControlTemplateTarget" enabled="true" name="ces">
                                                                                <Description></Description>
                                                                                <Template>
                                                                                    <![CDATA[
					  <%
				    Dim gapType As String = parameters.GetParameterByName("gapType").Value
					  'gapType (and any other listedParameter) may be empty due to a bug in the timing of collectionParameter value 
					  'setting while loading an existing item.
					  If String.IsNullOrEmpty(gapType) Then
						    Microsoft.VisualBasic.MsgBox("Empty gaptype !", Microsoft.VisualBasic.MsgBoxStyle.OkOnly)
					  Else
						    Dim controlId as String = GetGapId(parameters)
						    Dim controlLabel As String = parameters.GetParameterByName("gapLabel").Value
						    Dim unsignedParameter As Parameter(Of Boolean) = parameters.GetParameterByName("isUnsignedEntry", False)
						    Dim unsigned
						    If unsignedParameter IsNot Nothing Then
							    unsigned = unsignedParameter.Value
						    End If
						    Dim showEditMask As Boolean = parameters.GetParameterByName("showEditMask").Value
						    Dim showInvalidCharacterAudio As Boolean = parameters.GetParameterByName("showInvalidCharacterAudio").Value
						    Dim showInvalidCharacterMessage As Boolean = parameters.GetParameterByName("showInvalidCharacterMessage").Value
						    Dim invalidCharacterMessage As String = parameters.GetParameterByName("invalidCharacterMessage").Value
						    Dim gapMaskCharacter As String = parameters.GetParameterByName("gapMaskCharacterQP").Value
						    Dim nrOfDigits as Integer = parameters.GetParameterByName("nrOfDigits").Value
                Dim nrOfDecimals as Integer = 0
                If nrOfDigits > 9 Then
			              gapType = "Decimal"
			              nrOfDecimals = 0
		            End If
                Dim TotalNrOfCharacters As Integer = 1 + nrOfDigits
						    %>
						    <span class='inlinecontrol'>
						    </span>
				    <%End If%>
				]]>
                                                                                </Template>
                                                                            </Target>
                                                                        </Targets>

                                                                        <SharedFunctions>
                                                                            <![CDATA[
			  'Create a parameter for the interactionGap so we are sure
		 	  'that the fact identifier is equal to the 'interaction control'
		 	  'of the choiceInteraction
			  Private Shared _responseIdentifier as string = "gapController"

			  Private Shared Function GetGapId(Byval parameters As Cito.Tester.ContentModel.ParameterCollection) As String	
				    If string.IsNullOrEmpty(parameters.GetParameterByName("gapId").Value) then
					      parameters.GetParameterByName("gapId").Value = Guid.NewGuid().ToString()
				    End If
				    Return parameters.GetParameterByName("gapId").Value
			  End function

			  Private Shared Function GetCesGapPattern(Byval parameters As Cito.Tester.ContentModel.ParameterCollection, ByVal parameterName As String) As String	
				    Dim returnValue as String
		    	  Dim numberOfDigits as Integer = parameters.GetParameterByName("nrOfDigits").Value
				    Dim unsignedParameter As Parameter(Of Boolean) = parameters.GetParameterByName("isUnsignedEntry", False)
				    Dim isUnsignedEntry As Boolean
				    If unsignedParameter IsNot Nothing Then
					      isUnsignedEntry = unsignedParameter.Value
				    End If
						returnValue = Cito.Tester.Common.CesRegExHelper.CreateIntegerRegEx(numberOfDigits, isUnsignedEntry)
		    	  Return returnValue
			  End Function

			  Private Shared Function GetNrOfCharactersForTextBasedGaps(Byval parameters As Cito.Tester.ContentModel.ParameterCollection) As Integer
				    Return parameters.GetParameterByName("nrOfDigits").Value
			  End Function

			  Private Shared Function GetGapString(ByVal numberOfDigits As Integer, ByVal numberOfDecimals As Integer, ByVal maskCharacter As String, ByVal timeSubType As String, ByVal dateSubType As String) As String
						Return New String(maskCharacter, numberOfDigits)
			  End Function
		]]>
                                                                        </SharedFunctions>

                                                                        <SharedParameterSet>
                                                                            <plaintextparameter name="gapId">
                                                                                <designersetting key="label">label</designersetting>
                                                                                <designersetting key="visible">False</designersetting>
                                                                            </plaintextparameter>
                                                                            <plaintextparameter name="gapLabel">
                                                                                <designersetting key="label">Label</designersetting>
                                                                                <designersetting key="description">Label waaraan het invoervak (in de score editor) kan worden herkend</designersetting>
                                                                                <designersetting key="visible">True</designersetting>
                                                                                <designersetting key="required">True</designersetting>
                                                                                <designersetting key="group">Algemeen</designersetting>
                                                                            </plaintextparameter>
                                                                            <plaintextparameter name="controlType">
                                                                                <designersetting key="visible">False</designersetting>
                                                                                <designersetting key="defaultvalue">input</designersetting>
                                                                            </plaintextparameter>
                                                                            <listedparameter name="gapType">
                                                                                <designersetting key="label">Type invoer</designersetting>
                                                                                <designersetting key="description">Integer, Decimaal, Valuta, Tijd, Tekst, Datum, Formule (alleen Facet)</designersetting>
                                                                                <designersetting key="group">Algemeen</designersetting>
                                                                                <designersetting key="required">true</designersetting>
                                                                                <designersetting key="visible">false</designersetting>
                                                                                <designersetting key="defaultvalue">Integer</designersetting>
                                                                                <designersetting key="list">
                                                                                    <listvalues>
                                                                                        <listvalue key="Integer">Integer</listvalue>
                                                                                        <listvalue key="Decimal">Decimaal</listvalue>
                                                                                        <listvalue key="String">Tekst</listvalue>
                                                                                    </listvalues>
                                                                                </designersetting>
                                                                            </listedparameter>
                                                                            <booleanparameter name="isUnsignedEntry">
                                                                                <designersetting key="label">Alleen positieve invoer</designersetting>
                                                                                <designersetting key="description">Geef aan of alleen een positief getal als antwoord ingevoerd dient te kunnen worden.</designersetting>
                                                                                <designersetting key="group">Numeriek</designersetting>
                                                                                <designersetting key="required">false</designersetting>
                                                                                <designersetting key="visible">true</designersetting>
                                                                                <designersetting key="defaultvalue">False</designersetting>
                                                                            </booleanparameter>
                                                                            <listedparameter name="nrOfDigits">
                                                                                <designersetting key="label">Aantal cijfers</designersetting>
                                                                                <designersetting key="description"></designersetting>
                                                                                <designersetting key="group">Numeriek</designersetting>
                                                                                <designersetting key="defaultvalue">5</designersetting>
                                                                                <designersetting key="visible">true</designersetting>
                                                                                <designersetting key="list">
                                                                                    <listvalues>
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
                                                                                        <listvalue key="13">13</listvalue>
                                                                                        <listvalue key="14">14</listvalue>
                                                                                        <listvalue key="15">15</listvalue>
                                                                                    </listvalues>
                                                                                </designersetting>
                                                                            </listedparameter>
                                                                            <booleanparameter name="hasIntegerScoring">
                                                                                <designersetting key="label"></designersetting>
                                                                                <designersetting key="description">Indicatie of de integer-scoringparameter wel/niet getoond moet worden.</designersetting>
                                                                                <designersetting key="defaultvalue">false</designersetting>
                                                                                <designersetting key="visible">false</designersetting>
                                                                            </booleanparameter>
                                                                            <integerScoringParameter ControllerId="gapController" name="integerScoring" findingOverride="gapController">
                                                                                <attributereference name="label">gapLabel</attributereference>
                                                                                <attributereference name="maxLength">nrOfDigits</attributereference>
                                                                                <designersetting key="visible">true</designersetting>
                                                                                <designersetting key="minimumLength">1</designersetting>
                                                                                <designersetting key="maximumLength">1</designersetting>
                                                                                <definition>
                                                                                    <booleanparameter name="fictiveInteger">
                                                                                        <designersetting key="label"></designersetting>
                                                                                        <designersetting key="description"></designersetting>
                                                                                        <designersetting key="defaultvalue">true</designersetting>
                                                                                    </booleanparameter>
                                                                                </definition>
                                                                            </integerScoringParameter>
                                                                        </SharedParameterSet>
                                                                    </Template>

    Friend Shared ReadOnly ct_GenericInlineGapDecimal As XElement = <Template xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" definitionVersion="1">
                                                                        <Description></Description>
                                                                        <Targets>
                                                                            <Target xsi:type="ControlTemplateTarget" enabled="true" name="ces">
                                                                                <Description></Description>
                                                                                <Template>
                                                                                    <![CDATA[
					  <%
					  Dim gapType As String = parameters.GetParameterByName("gapType").Value
					  'gapType (and any other listedParameter) may be empty due to a bug in the timing of collectionParameter value 
					  'setting while loading an existing item.
					  If String.IsNullOrEmpty(gapType) Then
						    Microsoft.VisualBasic.MsgBox("Empty gaptype !", Microsoft.VisualBasic.MsgBoxStyle.OkOnly)
					  Else
						    Dim controlId as String = GetGapId(parameters)
						    Dim controlLabel As String = parameters.GetParameterByName("gapLabel").Value
						    Dim unsignedParameter As Parameter(Of Boolean) = parameters.GetParameterByName("isUnsignedEntry", False)
						    Dim unsigned
						    If unsignedParameter IsNot Nothing Then
							      unsigned = unsignedParameter.Value
						    End If
						    Dim showEditMask As Boolean = parameters.GetParameterByName("showEditMask").Value
						    Dim showInvalidCharacterAudio As Boolean = parameters.GetParameterByName("showInvalidCharacterAudio").Value
						    Dim showInvalidCharacterMessage As Boolean = parameters.GetParameterByName("showInvalidCharacterMessage").Value
						    Dim invalidCharacterMessage As String = parameters.GetParameterByName("invalidCharacterMessage").Value
						    Dim gapMaskCharacter As String = parameters.GetParameterByName("gapMaskCharacterQP").Value
						    Dim nrOfDigits as Integer = parameters.GetParameterByName("nrOfDigits").Value
						    Dim nrOfDecimals as Integer = 0
						    If Not String.IsNullOrEmpty(parameters.GetParameterByName("nrOfDecimals").Value) Then
							      nrOfDecimals = Convert.ToInt32(parameters.GetParameterByName("nrOfDecimals").Value)
						    End If 
						    Dim TotalNrOfCharacters As Integer = 1 + nrOfDigits + nrOfDecimals
						    %>
						    <span class='inlinecontrol'>
						    </span>
					  <%End If%>
				]]>
                                                                                </Template>
                                                                            </Target>
                                                                        </Targets>

                                                                        <SharedFunctions>
                                                                            <![CDATA[
			  'Create a parameter for the interactionGap so we are sure
		 	  'that the fact identifier is equal to the 'interaction control'
		 	  'of the choiceInteraction
			  Private Shared _responseIdentifier as string = "gapController"

			  Private Shared Function GetGapId(Byval parameters As Cito.Tester.ContentModel.ParameterCollection) As String	
				    If string.IsNullOrEmpty(parameters.GetParameterByName("gapId").Value) then
					      parameters.GetParameterByName("gapId").Value = Guid.NewGuid().ToString()
				    End If
				    Return parameters.GetParameterByName("gapId").Value
			  End function

			  Private Shared Function GetCesGapPattern(Byval parameters As Cito.Tester.ContentModel.ParameterCollection, ByVal parameterName As String) As String	
				    Dim returnValue as String
		    	  Dim numberOfDigits as Integer = parameters.GetParameterByName("nrOfDigits").Value
				    Dim numberOfDecimals as Integer = 0
				    If Not String.IsNullOrEmpty(parameters.GetParameterByName("nrOfDecimals").Value) Then
					      numberOfDecimals = Convert.ToInt32(parameters.GetParameterByName("nrOfDecimals").Value)
				    End If 
				    Dim unsignedParameter As Parameter(Of Boolean) = parameters.GetParameterByName("isUnsignedEntry", False)
				    Dim isUnsignedEntry As Boolean
				    If unsignedParameter IsNot Nothing Then
					      isUnsignedEntry = unsignedParameter.Value
				    End If
            returnValue = Cito.Tester.Common.CesRegExHelper.CreateDecimalRegEx(numberOfDigits, numberOfDecimals, isUnsignedEntry)
		    	  Return returnValue
			  End Function

			  Private Shared Function GetNrOfCharactersForTextBasedGaps(Byval parameters As Cito.Tester.ContentModel.ParameterCollection) As Integer
				    Return parameters.GetParameterByName("nrOfDigits").Value
			  End Function

			  Private Shared Function GetGapString(ByVal numberOfDigits As Integer, ByVal numberOfDecimals As Integer, ByVal maskCharacter As String, ByVal timeSubType As String, ByVal dateSubType As String) As String
				    Dim gapString As String
            Dim i As Integer
					  Dim tempString As String = New String(maskCharacter, numberOfDigits)
					  gapString += tempString
					  If numberOfDecimals > 0 Then
						    gapString += ","
						    For i = 1 To numberOfDecimals
							      gapString += maskCharacter
						    Next i
					  End If
				    Return gapString
			  End Function
		]]>
                                                                        </SharedFunctions>

                                                                        <SharedParameterSet>
                                                                            <plaintextparameter name="gapId">
                                                                                <designersetting key="label">label</designersetting>
                                                                                <designersetting key="visible">False</designersetting>
                                                                            </plaintextparameter>
                                                                            <plaintextparameter name="gapLabel">
                                                                                <designersetting key="label">Label</designersetting>
                                                                                <designersetting key="description">Label waaraan het invoervak (in de score editor) kan worden herkend</designersetting>
                                                                                <designersetting key="visible">True</designersetting>
                                                                                <designersetting key="required">True</designersetting>
                                                                                <designersetting key="group">Algemeen</designersetting>
                                                                            </plaintextparameter>
                                                                            <plaintextparameter name="controlType">
                                                                                <designersetting key="visible">False</designersetting>
                                                                                <designersetting key="defaultvalue">input</designersetting>
                                                                            </plaintextparameter>
                                                                            <listedparameter name="gapType">
                                                                                <designersetting key="label">Type invoer</designersetting>
                                                                                <designersetting key="description">Integer, Decimaal, Valuta, Tijd, Tekst, Datum, Formule (alleen Facet)</designersetting>
                                                                                <designersetting key="group">Algemeen</designersetting>
                                                                                <designersetting key="required">true</designersetting>
                                                                                <designersetting key="visible">false</designersetting>
                                                                                <designersetting key="defaultvalue">Decimal</designersetting>
                                                                                <designersetting key="list">
                                                                                    <listvalues>
                                                                                        <listvalue key="Integer">Integer</listvalue>
                                                                                        <listvalue key="Decimal">Decimaal</listvalue>
                                                                                        <listvalue key="String">Tekst</listvalue>
                                                                                    </listvalues>
                                                                                </designersetting>
                                                                            </listedparameter>
                                                                            <booleanparameter name="isUnsignedEntry">
                                                                                <designersetting key="label">Alleen positieve invoer</designersetting>
                                                                                <designersetting key="description">Geef aan of alleen een positief getal als antwoord ingevoerd dient te kunnen worden.</designersetting>
                                                                                <designersetting key="group">Numeriek</designersetting>
                                                                                <designersetting key="required">false</designersetting>
                                                                                <designersetting key="visible">true</designersetting>
                                                                                <designersetting key="defaultvalue">False</designersetting>
                                                                            </booleanparameter>
                                                                            <listedparameter name="nrOfDigits">
                                                                                <designersetting key="label">Aantal cijfers</designersetting>
                                                                                <designersetting key="description"></designersetting>
                                                                                <designersetting key="group">Numeriek</designersetting>
                                                                                <designersetting key="defaultvalue">5</designersetting>
                                                                                <designersetting key="visible">true</designersetting>
                                                                                <designersetting key="list">
                                                                                    <listvalues>
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
                                                                                        <listvalue key="13">13</listvalue>
                                                                                        <listvalue key="14">14</listvalue>
                                                                                        <listvalue key="15">15</listvalue>
                                                                                    </listvalues>
                                                                                </designersetting>
                                                                            </listedparameter>
                                                                            <listedparameter name="nrOfDecimals">
                                                                                <designersetting key="label">Aantal decimalen</designersetting>
                                                                                <designersetting key="description"></designersetting>
                                                                                <designersetting key="group">Numeriek</designersetting>
                                                                                <designersetting key="defaultvalue">0</designersetting>
                                                                                <designersetting key="visible">true</designersetting>
                                                                                <designersetting key="list">
                                                                                    <listvalues>
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
                                                                                    </listvalues>
                                                                                </designersetting>
                                                                            </listedparameter>
                                                                            <booleanparameter name="hasDecimalScoring">
                                                                                <designersetting key="label"></designersetting>
                                                                                <designersetting key="description">Indicatie of de decimal-scoringparameter wel/niet getoond moet worden.</designersetting>
                                                                                <designersetting key="defaultvalue">false</designersetting>
                                                                                <designersetting key="visible">false</designersetting>
                                                                            </booleanparameter>
                                                                            <decimalScoringParameter ControllerId="gapController" name="decimalScoring" findingOverride="gapController">
                                                                                <attributereference name="label">gapLabel</attributereference>
                                                                                <attributereference name="integerPartMaxLength">nrOfDigits</attributereference>
                                                                                <attributereference name="fractionPartMaxLength">nrOfDecimals</attributereference>
                                                                                <designersetting key="visible">true</designersetting>
                                                                                <designersetting key="minimumLength">1</designersetting>
                                                                                <designersetting key="maximumLength">1</designersetting>
                                                                                <definition>
                                                                                    <booleanparameter name="fictiveDecimal">
                                                                                        <designersetting key="label"></designersetting>
                                                                                        <designersetting key="description"></designersetting>
                                                                                        <designersetting key="defaultvalue">true</designersetting>
                                                                                    </booleanparameter>
                                                                                </definition>
                                                                            </decimalScoringParameter>
                                                                        </SharedParameterSet>
                                                                    </Template>


    Friend Shared ReadOnly InlineVideo As XElement = <Template xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" definitionVersion="1">
                                                         <Description>Dit is een test omschrijving voor ItemLayoutTemplate</Description>
                                                         <Targets>
                                                             <Target xsi:type="ItemLayoutTemplateTarget" enabled="true" name="word">
                                                                 <Description>MS Word</Description>
                                                                 <Template><![CDATA[
			    <html xmlns:cito="http://www.cito.nl/citotester">
			        <cito:control id="entireItem" type="Questify.InlineVideo" />
		        </html>]]>
                                                                 </Template>
                                                             </Target>
                                                         </Targets>
                                                     </Template>

End Class
