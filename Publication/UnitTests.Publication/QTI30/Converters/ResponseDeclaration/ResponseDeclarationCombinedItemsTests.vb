Imports Cito.Tester.ContentModel
Imports System.Xml.Serialization
Imports System.IO
Imports Questify.Builder.UnitTests.Publication.QTI30

Namespace QTI30

    <TestClass()>
    Public Class ResponseDeclarationCombinedItemsTests

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
        Public Sub Combined_NoFinding_Test()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody1, _solution1, GetScoringParams(), _result1, 18)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
        Public Sub TFS_26798_Test()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody2, _solution6, Nothing, _result6, 2)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
        Public Sub TFS_28312_GapDateTime_Test()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody3, _solution7, Nothing, _result7, 11)
        End Sub

        <TestMethod(), TestCategory("Publication"), TestCategory("QTIScoring"), TestCategory("CesResponseDeclaration")>
        Public Sub TFS_28312_GapMatch_Test()
            PublicationTestHelper.RunResponseDeclarationTest(_itemBody4, _solution8, Nothing, _result8, 1)
        End Sub


        Private Function GetScoringParams() As HashSet(Of ScoringParameter)
            Dim scoreParams As New HashSet(Of ScoringParameter)
            Dim assessmentItem = Deserialize(Of AssessmentItem)(_item1)
            scoreParams = assessmentItem.Parameters.DeepFetchScoringParameters()
            Return scoreParams
        End Function

        Private Function Deserialize(Of T)(input As XElement) As T
            Dim ret As T
            Dim s = New XmlSerializer(GetType(T))

            Using m As New StringReader(input.ToString())
                ret = DirectCast(s.Deserialize(m), T)
            End Using

            Return ret
        End Function


        Private _item1 As XElement =
            <assessmentItem xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" identifier="TI_Combined_NoFinding" title="TI_Combined_NoFinding" layoutTemplateSrc="Cito.CTE.Combined.Inline.SC">
                <solution>
                    <keyFindings>
                        <keyFinding id="IEF" scoringMethod="Dichotomous">
                            <keyFact id="A-Id8d3499a-99fa-446d-a588-6216b3ae6109" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="Id8d3499a-99fa-446d-a588-6216b3ae6109" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="B-Id8d3499a-99fa-446d-a588-6216b3ae6109" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="Id8d3499a-99fa-446d-a588-6216b3ae6109" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="C-Id8d3499a-99fa-446d-a588-6216b3ae6109" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="Id8d3499a-99fa-446d-a588-6216b3ae6109" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="D-Id8d3499a-99fa-446d-a588-6216b3ae6109" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="Id8d3499a-99fa-446d-a588-6216b3ae6109" occur="1">
                                    <booleanValue>
                                        <typedValue>false</typedValue>
                                    </booleanValue>
                                </keyValue>
                            </keyFact>
                            <keyFact id="E-Id8d3499a-99fa-446d-a588-6216b3ae6109" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                <keyValue domain="Id8d3499a-99fa-446d-a588-6216b3ae6109" occur="1">
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
                    <parameterSet id="kenmerken">
                        <booleanparameter name="showCalculatorButton">False</booleanparameter>
                        <integerparameter name="hightOfScrollText"/>
                        <integerparameter name="fixedWidthMatrixColumn">100</integerparameter>
                        <booleanparameter name="showChoicesBottomLayout"/>
                        <integerparameter name="fixedHeightAlternatives">0</integerparameter>
                        <booleanparameter name="showGroup">False</booleanparameter>
                        <plaintextparameter name="calculatorDescription"/>
                        <listedparameter name="calculatorMode">basic</listedparameter>
                        <booleanparameter name="showReset"/>
                        <booleanparameter name="showNotepad"/>
                        <plaintextparameter name="notepadDescription"/>
                        <booleanparameter name="showSymbolPicker"/>
                        <plaintextparameter name="symbolPickerDescription"/>
                        <plaintextparameter name="symbols"/>
                        <booleanparameter name="showRuler"/>
                        <plaintextparameter name="rulerDescription"/>
                        <plaintextparameter name="rulerStartValue"/>
                        <plaintextparameter name="rulerEndValue"/>
                        <plaintextparameter name="rulerStepValue"/>
                        <integerparameter name="rulerStart"/>
                        <integerparameter name="rulerEnd"/>
                        <integerparameter name="rulerStep"/>
                        <integerparameter name="rulerStepSize"/>
                        <listedparameter name="rulerLengthUnit">centimeter</listedparameter>
                        <booleanparameter name="showProtractor"/>
                        <plaintextparameter name="protractorPickerDescription"/>
                        <booleanparameter name="protractorEnableRotation"/>
                        <booleanparameter name="showTriangle"/>
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
                    </parameterSet>
                    <parameterSet id="entireItem">
                        <booleanparameter name="dualColumnLayout">False</booleanparameter>
                        <booleanparameter name="showCalculatorButton"/>
                        <xhtmlparameter name="leftBody"/>
                        <xhtmlresourceparameter name="leftSource"/>
                        <integerparameter name="sourceHeight">200</integerparameter>
                        <integerparameter name="sourcePositionTop">0</integerparameter>
                        <xhtmlparameter name="itemBody"/>
                        <xhtmlparameter name="itemQuestion">
                            <p id="c1-id-11" xmlns="http://www.w3.org/1999/xhtml">Tekst <cito:InlineElement id="I48c06d9f-edb6-40f3-a759-df23c4d95e2f" layoutTemplateSourceName="CTE.InlineGapStringLayoutTemplate" inlineFO="IEF" xmlns:cito="http://www.cito.nl/citotester">
                                    <cito:parameters>
                                        <cito:parameterSet id="entireItem">
                                            <cito:plaintextparameter name="gapId">I48c06d9f-edb6-40f3-a759-df23c4d95e2f</cito:plaintextparameter>
                                            <cito:plaintextparameter name="gapLabel">Tekst</cito:plaintextparameter>
                                            <cito:plaintextparameter name="controlType">input</cito:plaintextparameter>
                                            <cito:listedparameter name="gapType">String</cito:listedparameter>
                                            <cito:listedparameter name="autoInputProcessing">None</cito:listedparameter>
                                            <cito:integerparameter name="nrOfCharacters">10</cito:integerparameter>
                                            <cito:booleanparameter name="showEditMask">False</cito:booleanparameter>
                                            <cito:booleanparameter name="showInvalidCharacterAudio">False</cito:booleanparameter>
                                            <cito:booleanparameter name="showInvalidCharacterMessage">False</cito:booleanparameter>
                                            <cito:plaintextparameter name="invalidCharacterMessage">U kunt alleen de volgende tekens invoeren ...</cito:plaintextparameter>
                                            <cito:plaintextparameter name="gapMaskCharacterQP">_</cito:plaintextparameter>
                                            <cito:plaintextparameter name="gapMaskCharacter">_</cito:plaintextparameter>
                                            <cito:booleanparameter name="hasStringScoring">True</cito:booleanparameter>
                                            <cito:stringScoringParameter name="stringScoring" label="Tekst" ControllerId="gapController" findingOverride="IEF" expected-length="10">
                                                <cito:subparameterset id="1">
                                                    <cito:booleanparameter name="fictiveString">True</cito:booleanparameter>
                                                </cito:subparameterset>
                                                <cito:definition id="">
                                                    <cito:booleanparameter name="fictiveString"/>
                                                </cito:definition>
                                            </cito:stringScoringParameter>
                                        </cito:parameterSet>
                                    </cito:parameters>
                                </cito:InlineElement></p>
                            <p id="c1-id-12" xmlns="http://www.w3.org/1999/xhtml">Geheel getal <cito:InlineElement id="Ie4b22c84-5b60-4e02-a4bf-df6dd36cc504" layoutTemplateSourceName="CTE.InlineGapIntegerLayoutTemplate" inlineFO="IEF" xmlns:cito="http://www.cito.nl/citotester">
                                    <cito:parameters>
                                        <cito:parameterSet id="entireItem">
                                            <cito:plaintextparameter name="gapId">Ie4b22c84-5b60-4e02-a4bf-df6dd36cc504</cito:plaintextparameter>
                                            <cito:plaintextparameter name="gapLabel">Integer</cito:plaintextparameter>
                                            <cito:plaintextparameter name="controlType">input</cito:plaintextparameter>
                                            <cito:listedparameter name="gapType">Integer</cito:listedparameter>
                                            <cito:booleanparameter name="isUnsignedEntry">False</cito:booleanparameter>
                                            <cito:listedparameter name="nrOfDigits">5</cito:listedparameter>
                                            <cito:booleanparameter name="showEditMask">False</cito:booleanparameter>
                                            <cito:booleanparameter name="showInvalidCharacterAudio">False</cito:booleanparameter>
                                            <cito:booleanparameter name="showInvalidCharacterMessage">False</cito:booleanparameter>
                                            <cito:plaintextparameter name="invalidCharacterMessage">U kunt alleen de volgende tekens invoeren ...</cito:plaintextparameter>
                                            <cito:plaintextparameter name="gapMaskCharacterQP">_</cito:plaintextparameter>
                                            <cito:plaintextparameter name="gapMaskCharacter">_</cito:plaintextparameter>
                                            <cito:booleanparameter name="hasIntegerScoring">True</cito:booleanparameter>
                                            <cito:integerScoringParameter name="integerScoring" label="Integer" ControllerId="gapController" findingOverride="IEF" maxLength="5">
                                                <cito:subparameterset id="1">
                                                    <cito:booleanparameter name="fictiveInteger">True</cito:booleanparameter>
                                                </cito:subparameterset>
                                                <cito:definition id="">
                                                    <cito:booleanparameter name="fictiveInteger"/>
                                                </cito:definition>
                                            </cito:integerScoringParameter>
                                        </cito:parameterSet>
                                    </cito:parameters>
                                </cito:InlineElement></p>
                            <p id="c1-id-13" xmlns="http://www.w3.org/1999/xhtml">Decimaal <cito:InlineElement id="I7063ab42-3f7e-432f-a6bc-fa3f3867e617" layoutTemplateSourceName="CTE.InlineGapDecimalLayoutTemplate" inlineFO="IEF" xmlns:cito="http://www.cito.nl/citotester">
                                    <cito:parameters>
                                        <cito:parameterSet id="entireItem">
                                            <cito:plaintextparameter name="gapId">I7063ab42-3f7e-432f-a6bc-fa3f3867e617</cito:plaintextparameter>
                                            <cito:plaintextparameter name="gapLabel">Decimaal</cito:plaintextparameter>
                                            <cito:plaintextparameter name="controlType">input</cito:plaintextparameter>
                                            <cito:listedparameter name="gapType">Decimal</cito:listedparameter>
                                            <cito:booleanparameter name="isUnsignedEntry">False</cito:booleanparameter>
                                            <cito:listedparameter name="nrOfDigits">6</cito:listedparameter>
                                            <cito:listedparameter name="nrOfDecimals">2</cito:listedparameter>
                                            <cito:booleanparameter name="showEditMask">False</cito:booleanparameter>
                                            <cito:booleanparameter name="showInvalidCharacterAudio">False</cito:booleanparameter>
                                            <cito:booleanparameter name="showInvalidCharacterMessage">False</cito:booleanparameter>
                                            <cito:plaintextparameter name="invalidCharacterMessage">U kunt alleen de volgende tekens invoeren ...</cito:plaintextparameter>
                                            <cito:plaintextparameter name="gapMaskCharacterQP">_</cito:plaintextparameter>
                                            <cito:plaintextparameter name="gapMaskCharacter">_</cito:plaintextparameter>
                                            <cito:booleanparameter name="hasDecimalScoring">True</cito:booleanparameter>
                                            <cito:decimalScoringParameter name="decimalScoring" label="Decimaal" ControllerId="gapController" findingOverride="IEF" integerPartMaxLength="6" fractionPartMaxLength="2">
                                                <cito:subparameterset id="1">
                                                    <cito:booleanparameter name="fictiveDecimal">True</cito:booleanparameter>
                                                </cito:subparameterset>
                                                <cito:definition id="">
                                                    <cito:booleanparameter name="fictiveDecimal"/>
                                                </cito:definition>
                                            </cito:decimalScoringParameter>
                                        </cito:parameterSet>
                                    </cito:parameters>
                                </cito:InlineElement></p>
                            <p id="c1-id-14" xmlns="http://www.w3.org/1999/xhtml">Bedrag <cito:InlineElement id="I05fc1135-87b1-4070-9f1b-44396624a85c" layoutTemplateSourceName="CTE.InlineGapCurrencyLayoutTemplate" inlineFO="IEF" xmlns:cito="http://www.cito.nl/citotester">
                                    <cito:parameters>
                                        <cito:parameterSet id="entireItem">
                                            <cito:plaintextparameter name="gapId">I05fc1135-87b1-4070-9f1b-44396624a85c</cito:plaintextparameter>
                                            <cito:plaintextparameter name="gapLabel">Bedrag</cito:plaintextparameter>
                                            <cito:plaintextparameter name="controlType">input</cito:plaintextparameter>
                                            <cito:listedparameter name="gapType">Currency</cito:listedparameter>
                                            <cito:booleanparameter name="includeCurrencySymbolInMask">False</cito:booleanparameter>
                                            <cito:booleanparameter name="isUnsignedEntry">False</cito:booleanparameter>
                                            <cito:listedparameter name="nrOfDigits">5</cito:listedparameter>
                                            <cito:listedparameter name="nrOfDecimals">2</cito:listedparameter>
                                            <cito:booleanparameter name="showEditMask">False</cito:booleanparameter>
                                            <cito:booleanparameter name="showInvalidCharacterAudio">False</cito:booleanparameter>
                                            <cito:booleanparameter name="showInvalidCharacterMessage">False</cito:booleanparameter>
                                            <cito:plaintextparameter name="invalidCharacterMessage">U kunt alleen de volgende tekens invoeren ...</cito:plaintextparameter>
                                            <cito:plaintextparameter name="gapMaskCharacterQP">_</cito:plaintextparameter>
                                            <cito:plaintextparameter name="gapMaskCharacter">_</cito:plaintextparameter>
                                            <cito:booleanparameter name="hasCurrencyScoring">True</cito:booleanparameter>
                                            <cito:currencyScoringParameter name="currencyScoring" label="Bedrag" ControllerId="gapController" findingOverride="IEF" integerPartMaxLength="5" fractionPartMaxLength="2">
                                                <cito:subparameterset id="1">
                                                    <cito:booleanparameter name="fictiveCurrency">True</cito:booleanparameter>
                                                </cito:subparameterset>
                                                <cito:definition id="">
                                                    <cito:booleanparameter name="fictiveCurrency"/>
                                                </cito:definition>
                                            </cito:currencyScoringParameter>
                                        </cito:parameterSet>
                                    </cito:parameters>
                                </cito:InlineElement></p>
                            <p id="c1-id-15" xmlns="http://www.w3.org/1999/xhtml">Datum <cito:InlineElement id="Ib946bb76-51f2-4a58-b865-c5aef235a439" layoutTemplateSourceName="CTE.InlineGapDateLayoutTemplate" inlineFO="IEF" xmlns:cito="http://www.cito.nl/citotester">
                                    <cito:parameters>
                                        <cito:parameterSet id="entireItem">
                                            <cito:plaintextparameter name="gapId">Ib946bb76-51f2-4a58-b865-c5aef235a439</cito:plaintextparameter>
                                            <cito:plaintextparameter name="gapLabel">Datum</cito:plaintextparameter>
                                            <cito:plaintextparameter name="controlType">input</cito:plaintextparameter>
                                            <cito:listedparameter name="gapType">Date</cito:listedparameter>
                                            <cito:listedparameter name="date-sub-type">dd-MM-yyyy</cito:listedparameter>
                                            <cito:booleanparameter name="showEditMask">False</cito:booleanparameter>
                                            <cito:booleanparameter name="showInvalidCharacterAudio">False</cito:booleanparameter>
                                            <cito:booleanparameter name="showInvalidCharacterMessage">False</cito:booleanparameter>
                                            <cito:plaintextparameter name="invalidCharacterMessage">U kunt alleen de volgende tekens invoeren ...</cito:plaintextparameter>
                                            <cito:plaintextparameter name="gapMaskCharacterQP">_</cito:plaintextparameter>
                                            <cito:plaintextparameter name="gapMaskCharacter">_</cito:plaintextparameter>
                                            <cito:booleanparameter name="hasDateScoring">True</cito:booleanparameter>
                                            <cito:dateScoringParameter name="dateScoring" label="Datum" ControllerId="gapController" findingOverride="IEF" dateFormat="dd-MM-yyyy">
                                                <cito:subparameterset id="1">
                                                    <cito:booleanparameter name="fictiveDate">True</cito:booleanparameter>
                                                </cito:subparameterset>
                                                <cito:definition id="">
                                                    <cito:booleanparameter name="fictiveDate"/>
                                                </cito:definition>
                                            </cito:dateScoringParameter>
                                        </cito:parameterSet>
                                    </cito:parameters>
                                </cito:InlineElement></p>
                            <p id="c1-id-16" xmlns="http://www.w3.org/1999/xhtml">Tijd (hh:mm:ss) <cito:InlineElement id="Ie2c5a24d-8187-4ab3-bb89-cb31d1a250da" layoutTemplateSourceName="CTE.InlineGapTimeLayoutTemplate" inlineFO="IEF" xmlns:cito="http://www.cito.nl/citotester">
                                    <cito:parameters>
                                        <cito:parameterSet id="entireItem">
                                            <cito:plaintextparameter name="gapId">Ie2c5a24d-8187-4ab3-bb89-cb31d1a250da</cito:plaintextparameter>
                                            <cito:plaintextparameter name="gapLabel">Tijd</cito:plaintextparameter>
                                            <cito:plaintextparameter name="controlType">input</cito:plaintextparameter>
                                            <cito:listedparameter name="gapType">Time</cito:listedparameter>
                                            <cito:listedparameter name="time-sub-type">hh:mm:ss</cito:listedparameter>
                                            <cito:booleanparameter name="showEditMask">False</cito:booleanparameter>
                                            <cito:booleanparameter name="showInvalidCharacterAudio">False</cito:booleanparameter>
                                            <cito:booleanparameter name="showInvalidCharacterMessage">False</cito:booleanparameter>
                                            <cito:plaintextparameter name="invalidCharacterMessage">U kunt alleen de volgende tekens invoeren ...</cito:plaintextparameter>
                                            <cito:plaintextparameter name="gapMaskCharacterQP">_</cito:plaintextparameter>
                                            <cito:plaintextparameter name="gapMaskCharacter">_</cito:plaintextparameter>
                                            <cito:booleanparameter name="hasTimeScoring">True</cito:booleanparameter>
                                            <cito:timeScoringParameter name="timeScoring" label="Tijd" ControllerId="gapController" findingOverride="IEF" timeFormat="hh:mm:ss">
                                                <cito:subparameterset id="1">
                                                    <cito:booleanparameter name="fictiveTime">True</cito:booleanparameter>
                                                </cito:subparameterset>
                                                <cito:definition id="">
                                                    <cito:booleanparameter name="fictiveTime"/>
                                                </cito:definition>
                                            </cito:timeScoringParameter>
                                        </cito:parameterSet>
                                    </cito:parameters>
                                </cito:InlineElement></p>
                            <p id="c1-id-17" xmlns="http://www.w3.org/1999/xhtml">Formule <cito:InlineElement id="If8544370-1046-4080-9dda-3cf3077d9384" layoutTemplateSourceName="InlineGapFormulaLayoutTemplate" inlineFO="IEF" xmlns:cito="http://www.cito.nl/citotester">
                                    <cito:parameters>
                                        <cito:parameterSet id="entireItem">
                                            <cito:plaintextparameter name="gapId">If8544370-1046-4080-9dda-3cf3077d9384</cito:plaintextparameter>
                                            <cito:plaintextparameter name="gapLabel">Formule</cito:plaintextparameter>
                                            <cito:plaintextparameter name="controlType">input</cito:plaintextparameter>
                                            <cito:listedparameter name="gapType">Formula</cito:listedparameter>
                                            <cito:plaintextparameter name="gapMaskCharacter">_</cito:plaintextparameter>
                                            <cito:booleanparameter name="hasMathMLScoring">True</cito:booleanparameter>
                                            <cito:mathMLParameter name="initialMathML"/>
                                            <cito:booleanparameter name="multiLineMathML">False</cito:booleanparameter>
                                            <cito:booleanparameter name="cursorOnNewLine">False</cito:booleanparameter>
                                            <cito:listedparameter name="editorControlPreSet">all</cito:listedparameter>
                                            <cito:mathScoringParameter name="mathMLScoring" label="Formule" ControllerId="gapController" findingOverride="IEF">
                                                <cito:subparameterset id="1">
                                                    <cito:booleanparameter name="fictiveMathML">True</cito:booleanparameter>
                                                </cito:subparameterset>
                                                <cito:definition id="">
                                                    <cito:booleanparameter name="fictiveMathML"/>
                                                </cito:definition>
                                            </cito:mathScoringParameter>
                                        </cito:parameterSet>
                                    </cito:parameters>
                                </cito:InlineElement></p>
                            <p id="c1-id-18" xmlns="http://www.w3.org/1999/xhtml">Formule (meerregelig) <cito:InlineElement id="Ifd8a5530-6ab4-43f7-a25c-372d1dd4570f" layoutTemplateSourceName="InlineGapFormulaLayoutTemplate" inlineFO="IEF" xmlns:cito="http://www.cito.nl/citotester">
                                    <cito:parameters>
                                        <cito:parameterSet id="entireItem">
                                            <cito:plaintextparameter name="gapId">Ifd8a5530-6ab4-43f7-a25c-372d1dd4570f</cito:plaintextparameter>
                                            <cito:plaintextparameter name="gapLabel">Formule 2</cito:plaintextparameter>
                                            <cito:plaintextparameter name="controlType">input</cito:plaintextparameter>
                                            <cito:listedparameter name="gapType">Formula</cito:listedparameter>
                                            <cito:plaintextparameter name="gapMaskCharacter">_</cito:plaintextparameter>
                                            <cito:booleanparameter name="hasMathMLScoring">True</cito:booleanparameter>
                                            <cito:mathMLParameter name="initialMathML"/>
                                            <cito:booleanparameter name="multiLineMathML">True</cito:booleanparameter>
                                            <cito:booleanparameter name="cursorOnNewLine">True</cito:booleanparameter>
                                            <cito:listedparameter name="editorControlPreSet">all</cito:listedparameter>
                                            <cito:mathScoringParameter name="mathMLScoring" label="Formule 2" ControllerId="gapController" findingOverride="IEF">
                                                <cito:subparameterset id="1">
                                                    <cito:booleanparameter name="fictiveMathML">True</cito:booleanparameter>
                                                </cito:subparameterset>
                                                <cito:definition id="">
                                                    <cito:booleanparameter name="fictiveMathML"/>
                                                </cito:definition>
                                            </cito:mathScoringParameter>
                                        </cito:parameterSet>
                                    </cito:parameters>
                                </cito:InlineElement></p>
                            <p id="c1-id-20" xmlns="http://www.w3.org/1999/xhtml">Choice <cito:InlineElement id="Ibe763973-0fb7-42da-aadb-32950e644bfd" layoutTemplateSourceName="CTE.InlineChoiceLayoutTemplate" inlineFO="IEF" xmlns:cito="http://www.cito.nl/citotester">
                                    <cito:parameters>
                                        <cito:parameterSet id="entireItem">
                                            <cito:plaintextparameter name="inlineChoiceId">Ibe763973-0fb7-42da-aadb-32950e644bfd</cito:plaintextparameter>
                                            <cito:plaintextparameter name="inlineChoiceLabel">IC1</cito:plaintextparameter>
                                            <cito:inlineChoiceScoringparameter name="inlineChoiceScoring" label="IC1" ControllerId="inlineChoiceController" findingOverride="IEF" minChoices="0" max-choices="1">
                                                <cito:subparameterset id="A">
                                                    <cito:xhtmlparameter name="icOption">
                                                        <p id="c1-id-11" xmlns="http://www.w3.org/1999/xhtml">A</p>
                                                    </cito:xhtmlparameter>
                                                </cito:subparameterset>
                                                <cito:subparameterset id="B">
                                                    <cito:xhtmlparameter name="icOption">
                                                        <p id="c1-id-11" xmlns="http://www.w3.org/1999/xhtml">B</p>
                                                    </cito:xhtmlparameter>
                                                </cito:subparameterset>
                                                <cito:subparameterset id="C">
                                                    <cito:xhtmlparameter name="icOption">
                                                        <p id="c1-id-11" xmlns="http://www.w3.org/1999/xhtml">C</p>
                                                    </cito:xhtmlparameter>
                                                </cito:subparameterset>
                                                <cito:subparameterset id="D">
                                                    <cito:xhtmlparameter name="icOption">
                                                        <p id="c1-id-11" xmlns="http://www.w3.org/1999/xhtml">D</p>
                                                    </cito:xhtmlparameter>
                                                </cito:subparameterset>
                                                <cito:definition id="">
                                                    <cito:xhtmlparameter name="icOption"/>
                                                </cito:definition>
                                            </cito:inlineChoiceScoringparameter>
                                            <cito:booleanparameter name="editSize">True</cito:booleanparameter>
                                            <cito:integerparameter name="width"/>
                                            <cito:integerparameter name="height"/>
                                        </cito:parameterSet>
                                    </cito:parameters>
                                </cito:InlineElement> en choice 2 <cito:InlineElement id="I2ebb2bdb-2ab2-4919-8d65-b6f218999a1c" layoutTemplateSourceName="CTE.InlineChoiceLayoutTemplate" inlineFO="IEF" xmlns:cito="http://www.cito.nl/citotester">
                                    <cito:parameters>
                                        <cito:parameterSet id="entireItem">
                                            <cito:plaintextparameter name="inlineChoiceId">I2ebb2bdb-2ab2-4919-8d65-b6f218999a1c</cito:plaintextparameter>
                                            <cito:plaintextparameter name="inlineChoiceLabel">IC2</cito:plaintextparameter>
                                            <cito:inlineChoiceScoringparameter name="inlineChoiceScoring" label="IC2" ControllerId="inlineChoiceController" findingOverride="IEF" minChoices="0" max-choices="1">
                                                <cito:subparameterset id="A">
                                                    <cito:xhtmlparameter name="icOption">
                                                        <p id="c1-id-11" xmlns="http://www.w3.org/1999/xhtml">A.A</p>
                                                    </cito:xhtmlparameter>
                                                </cito:subparameterset>
                                                <cito:subparameterset id="B">
                                                    <cito:xhtmlparameter name="icOption">
                                                        <p id="c1-id-11" xmlns="http://www.w3.org/1999/xhtml">B.B</p>
                                                    </cito:xhtmlparameter>
                                                </cito:subparameterset>
                                                <cito:subparameterset id="C">
                                                    <cito:xhtmlparameter name="icOption">
                                                        <p id="c1-id-11" xmlns="http://www.w3.org/1999/xhtml">C.C</p>
                                                    </cito:xhtmlparameter>
                                                </cito:subparameterset>
                                                <cito:subparameterset id="D">
                                                    <cito:xhtmlparameter name="icOption">
                                                        <p id="c1-id-11" xmlns="http://www.w3.org/1999/xhtml">D.D</p>
                                                    </cito:xhtmlparameter>
                                                </cito:subparameterset>
                                                <cito:definition id="">
                                                    <cito:xhtmlparameter name="icOption"/>
                                                </cito:definition>
                                            </cito:inlineChoiceScoringparameter>
                                            <cito:booleanparameter name="editSize">True</cito:booleanparameter>
                                            <cito:integerparameter name="width"/>
                                            <cito:integerparameter name="height"/>
                                        </cito:parameterSet>
                                    </cito:parameters>
                                </cito:InlineElement> en choice 3 <cito:InlineElement id="Ibf865980-8621-4005-a520-3ad82bff7aae" layoutTemplateSourceName="CTE.InlineChoiceLayoutTemplate" inlineFO="IEF" xmlns:cito="http://www.cito.nl/citotester">
                                    <cito:parameters>
                                        <cito:parameterSet id="entireItem">
                                            <cito:plaintextparameter name="inlineChoiceId">Ibf865980-8621-4005-a520-3ad82bff7aae</cito:plaintextparameter>
                                            <cito:plaintextparameter name="inlineChoiceLabel">IC3</cito:plaintextparameter>
                                            <cito:inlineChoiceScoringparameter name="inlineChoiceScoring" label="IC3" ControllerId="inlineChoiceController" findingOverride="IEF" minChoices="0" max-choices="1">
                                                <cito:subparameterset id="A">
                                                    <cito:xhtmlparameter name="icOption">
                                                        <p id="c1-id-11" xmlns="http://www.w3.org/1999/xhtml">A.A.A</p>
                                                    </cito:xhtmlparameter>
                                                </cito:subparameterset>
                                                <cito:subparameterset id="B">
                                                    <cito:xhtmlparameter name="icOption">
                                                        <p id="c1-id-11" xmlns="http://www.w3.org/1999/xhtml">B.B.B</p>
                                                    </cito:xhtmlparameter>
                                                </cito:subparameterset>
                                                <cito:subparameterset id="C">
                                                    <cito:xhtmlparameter name="icOption">
                                                        <p id="c1-id-11" xmlns="http://www.w3.org/1999/xhtml">C.C.C</p>
                                                    </cito:xhtmlparameter>
                                                </cito:subparameterset>
                                                <cito:definition id="">
                                                    <cito:xhtmlparameter name="icOption"/>
                                                </cito:definition>
                                            </cito:inlineChoiceScoringparameter>
                                            <cito:booleanparameter name="editSize">True</cito:booleanparameter>
                                            <cito:integerparameter name="width"/>
                                            <cito:integerparameter name="height"/>
                                        </cito:parameterSet>
                                    </cito:parameters>
                                </cito:InlineElement></p>
                            <p id="c1-id-22" xmlns="http://www.w3.org/1999/xhtml">Meerkeuze 1<br id="c1-id-23"/><cito:InlineElement id="I5e9be57a-1cc0-4501-804b-f89bbb666852" layoutTemplateSourceName="CTE.InlineMultipleChoiceLayoutTemplate" inlineFO="IEF" xmlns:cito="http://www.cito.nl/citotester">
                                    <cito:parameters>
                                        <cito:parameterSet id="entireItem">
                                            <cito:listedparameter name="controlType">mc</cito:listedparameter>
                                            <cito:plaintextparameter name="controlId">I5e9be57a-1cc0-4501-804b-f89bbb666852</cito:plaintextparameter>
                                            <cito:plaintextparameter name="controlLabel">MC1</cito:plaintextparameter>
                                            <cito:plaintextparameter name="controllerId">I5e9be57a-1cc0-4501-804b-f89bbb666852</cito:plaintextparameter>
                                            <cito:listedparameter name="nrAlternativesPerLine">1</cito:listedparameter>
                                            <cito:booleanparameter name="horizontallyCenteredAlternatives">False</cito:booleanparameter>
                                            <cito:integerparameter name="expectedAnswers">1</cito:integerparameter>
                                            <cito:integerparameter name="multiChoiceType">1</cito:integerparameter>
                                            <cito:multichoicescoringparameter name="inlineMCScoring" label="MC1" ControllerId="inlineMC" findingOverride="IEF" minChoices="1" max-choices="1" multiChoice="Radio">
                                                <cito:subparameterset id="A">
                                                    <cito:xhtmlparameter name="mcOption">
                                                        <p id="c1-id-11" xmlns="http://www.w3.org/1999/xhtml">A</p>
                                                    </cito:xhtmlparameter>
                                                </cito:subparameterset>
                                                <cito:subparameterset id="B">
                                                    <cito:xhtmlparameter name="mcOption">
                                                        <p id="c1-id-11" xmlns="http://www.w3.org/1999/xhtml">B</p>
                                                    </cito:xhtmlparameter>
                                                </cito:subparameterset>
                                                <cito:subparameterset id="C">
                                                    <cito:xhtmlparameter name="mcOption">
                                                        <p id="c1-id-11" xmlns="http://www.w3.org/1999/xhtml">C</p>
                                                    </cito:xhtmlparameter>
                                                </cito:subparameterset>
                                                <cito:definition id="">
                                                    <cito:xhtmlparameter name="mcOption"/>
                                                </cito:definition>
                                            </cito:multichoicescoringparameter>
                                        </cito:parameterSet>
                                    </cito:parameters>
                                </cito:InlineElement></p>
                            <p id="c1-id-25" xmlns="http://www.w3.org/1999/xhtml">Meerkeuze 2<br id="c1-id-26"/><cito:InlineElement id="Ib072620d-58c8-4695-b219-6591412548ff" layoutTemplateSourceName="CTE.InlineMultipleChoiceLayoutTemplate" inlineFO="IEF" xmlns:cito="http://www.cito.nl/citotester">
                                    <cito:parameters>
                                        <cito:parameterSet id="entireItem">
                                            <cito:listedparameter name="controlType">mc</cito:listedparameter>
                                            <cito:plaintextparameter name="controlId">Ib072620d-58c8-4695-b219-6591412548ff</cito:plaintextparameter>
                                            <cito:plaintextparameter name="controlLabel">MC2</cito:plaintextparameter>
                                            <cito:plaintextparameter name="controllerId">Ib072620d-58c8-4695-b219-6591412548ff</cito:plaintextparameter>
                                            <cito:listedparameter name="nrAlternativesPerLine">1</cito:listedparameter>
                                            <cito:booleanparameter name="horizontallyCenteredAlternatives">False</cito:booleanparameter>
                                            <cito:integerparameter name="expectedAnswers">1</cito:integerparameter>
                                            <cito:integerparameter name="multiChoiceType">1</cito:integerparameter>
                                            <cito:multichoicescoringparameter name="inlineMCScoring" label="MC2" ControllerId="inlineMC" findingOverride="IEF" minChoices="1" max-choices="1" multiChoice="Radio">
                                                <cito:subparameterset id="A">
                                                    <cito:xhtmlparameter name="mcOption">
                                                        <p id="c1-id-11" xmlns="http://www.w3.org/1999/xhtml">A.A</p>
                                                    </cito:xhtmlparameter>
                                                </cito:subparameterset>
                                                <cito:subparameterset id="B">
                                                    <cito:xhtmlparameter name="mcOption">
                                                        <p id="c1-id-11" xmlns="http://www.w3.org/1999/xhtml">B.B</p>
                                                    </cito:xhtmlparameter>
                                                </cito:subparameterset>
                                                <cito:subparameterset id="C">
                                                    <cito:xhtmlparameter name="mcOption">
                                                        <p id="c1-id-11" xmlns="http://www.w3.org/1999/xhtml">C.C</p>
                                                    </cito:xhtmlparameter>
                                                </cito:subparameterset>
                                                <cito:subparameterset id="D">
                                                    <cito:xhtmlparameter name="mcOption">
                                                        <p id="c1-id-11" xmlns="http://www.w3.org/1999/xhtml">D.D</p>
                                                    </cito:xhtmlparameter>
                                                </cito:subparameterset>
                                                <cito:definition id="">
                                                    <cito:xhtmlparameter name="mcOption"/>
                                                </cito:definition>
                                            </cito:multichoicescoringparameter>
                                        </cito:parameterSet>
                                    </cito:parameters>
                                </cito:InlineElement></p>
                            <p id="c1-id-30" xmlns="http://www.w3.org/1999/xhtml">Multiple Response<br id="c1-id-32"/><cito:InlineElement id="Id8d3499a-99fa-446d-a588-6216b3ae6109" layoutTemplateSourceName="CTE.InlineMultipleResponseLayoutTemplate" inlineFO="IEF" xmlns:cito="http://www.cito.nl/citotester">
                                    <cito:parameters>
                                        <cito:parameterSet id="entireItem">
                                            <cito:listedparameter name="controlType">mr</cito:listedparameter>
                                            <cito:plaintextparameter name="controlId">Id8d3499a-99fa-446d-a588-6216b3ae6109</cito:plaintextparameter>
                                            <cito:plaintextparameter name="controlLabel">MR</cito:plaintextparameter>
                                            <cito:plaintextparameter name="controllerId">Id8d3499a-99fa-446d-a588-6216b3ae6109</cito:plaintextparameter>
                                            <cito:listedparameter name="nrAlternativesPerLine">1</cito:listedparameter>
                                            <cito:booleanparameter name="horizontallyCenteredAlternatives">False</cito:booleanparameter>
                                            <cito:integerparameter name="expectedAnswers">0</cito:integerparameter>
                                            <cito:integerparameter name="multiChoiceType">1</cito:integerparameter>
                                            <cito:multichoicescoringparameter name="inlineMCScoring" label="MR" ControllerId="inlineMC" findingOverride="IEF" minChoices="0" max-choices="0" multiChoice="Radio">
                                                <cito:subparameterset id="A">
                                                    <cito:xhtmlparameter name="mcOption">
                                                        <p id="c1-id-11" xmlns="http://www.w3.org/1999/xhtml">a</p>
                                                    </cito:xhtmlparameter>
                                                </cito:subparameterset>
                                                <cito:subparameterset id="B">
                                                    <cito:xhtmlparameter name="mcOption">
                                                        <p id="c1-id-11" xmlns="http://www.w3.org/1999/xhtml">b</p>
                                                    </cito:xhtmlparameter>
                                                </cito:subparameterset>
                                                <cito:subparameterset id="C">
                                                    <cito:xhtmlparameter name="mcOption">
                                                        <p id="c1-id-11" xmlns="http://www.w3.org/1999/xhtml">c</p>
                                                    </cito:xhtmlparameter>
                                                </cito:subparameterset>
                                                <cito:subparameterset id="D">
                                                    <cito:xhtmlparameter name="mcOption">
                                                        <p id="c1-id-11" xmlns="http://www.w3.org/1999/xhtml">d</p>
                                                    </cito:xhtmlparameter>
                                                </cito:subparameterset>
                                                <cito:subparameterset id="E">
                                                    <cito:xhtmlparameter name="mcOption">
                                                        <p id="c1-id-11" xmlns="http://www.w3.org/1999/xhtml">e</p>
                                                    </cito:xhtmlparameter>
                                                </cito:subparameterset>
                                                <cito:definition id="">
                                                    <cito:xhtmlparameter name="mcOption"/>
                                                </cito:definition>
                                            </cito:multichoicescoringparameter>
                                        </cito:parameterSet>
                                    </cito:parameters>
                                </cito:InlineElement></p>
                        </xhtmlparameter>
                        <xhtmlparameter name="itemGeneral"/>
                    </parameterSet>
                </parameters>
            </assessmentItem>

        Private _itemBody1 As XElement =
           <wrapper>
               <stylesheet href="resource://package:1/itemstyle.css" type="text/css"/>
               <stylesheet href="resource://package:1/userstyle.css" type="text/css"/>
               <qti-item-body class="defaultBody">
                   <div class="content">
                       <styles xmlns="http://www.w3.org/1999/xhtml">
                           <stylecollection>

                           </stylecollection>
                       </styles>
                       <qtiextension xmlns="http://www.w3.org/1999/xhtml">
                           <dep:mathElements xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:dep="http://www.imsglobal.org/xsd/dep/depv1p0/imsdep_qtiv2p1">
                               <dep:mathElement identifier="If8544370-1046-4080-9dda-3cf3077d9384">
                                   <dep:enabledControl>fraction</dep:enabledControl>
                                   <dep:enabledControl>power</dep:enabledControl>
                                   <dep:enabledControl>squareroot</dep:enabledControl>
                                   <dep:enabledControl>cuberoot</dep:enabledControl>
                                   <dep:enabledControl>add</dep:enabledControl>
                                   <dep:enabledControl>subtract</dep:enabledControl>
                                   <dep:enabledControl>multiplication</dep:enabledControl>
                                   <dep:enabledControl>division</dep:enabledControl>
                                   <dep:enabledControl>dot</dep:enabledControl>
                                   <dep:enabledControl>isequal</dep:enabledControl>
                                   <dep:enabledControl>isnotequal</dep:enabledControl>
                                   <dep:enabledControl>approximatelyequal</dep:enabledControl>
                                   <dep:enabledControl>greaterequal</dep:enabledControl>
                                   <dep:enabledControl>lessequal</dep:enabledControl>
                                   <dep:enabledControl>lessthan</dep:enabledControl>
                                   <dep:enabledControl>greaterthan</dep:enabledControl>
                                   <dep:enabledControl>parentheses</dep:enabledControl>
                                   <dep:enabledControl>pi</dep:enabledControl>
                                   <dep:enabledControl>enter</dep:enabledControl>
                               </dep:mathElement>
                               <dep:mathElement identifier="Ifd8a5530-6ab4-43f7-a25c-372d1dd4570f">
                                   <dep:enabledControl>fraction</dep:enabledControl>
                                   <dep:enabledControl>power</dep:enabledControl>
                                   <dep:enabledControl>squareroot</dep:enabledControl>
                                   <dep:enabledControl>cuberoot</dep:enabledControl>
                                   <dep:enabledControl>add</dep:enabledControl>
                                   <dep:enabledControl>subtract</dep:enabledControl>
                                   <dep:enabledControl>multiplication</dep:enabledControl>
                                   <dep:enabledControl>division</dep:enabledControl>
                                   <dep:enabledControl>dot</dep:enabledControl>
                                   <dep:enabledControl>isequal</dep:enabledControl>
                                   <dep:enabledControl>isnotequal</dep:enabledControl>
                                   <dep:enabledControl>approximatelyequal</dep:enabledControl>
                                   <dep:enabledControl>greaterequal</dep:enabledControl>
                                   <dep:enabledControl>lessequal</dep:enabledControl>
                                   <dep:enabledControl>lessthan</dep:enabledControl>
                                   <dep:enabledControl>greaterthan</dep:enabledControl>
                                   <dep:enabledControl>parentheses</dep:enabledControl>
                                   <dep:enabledControl>pi</dep:enabledControl>
                                   <dep:enabledControl>enter</dep:enabledControl>
                               </dep:mathElement>
                           </dep:mathElements>
                           <dep:inlineChoiceRichTextInteraction xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:dep="http://www.imsglobal.org/xsd/imsqti_ext_v2p1" identifier="Ibe763973-0fb7-42da-aadb-32950e644bfd">
                               <dep:inlineChoiceRichText identifier="A">
                                   <imsqti:p class="noParagraphMargin" xmlns:imsqti="http://www.imsglobal.org/xsd/imsqti_v2p1">A</imsqti:p>
                               </dep:inlineChoiceRichText>
                               <dep:inlineChoiceRichText identifier="B">
                                   <imsqti:p class="noParagraphMargin" xmlns:imsqti="http://www.imsglobal.org/xsd/imsqti_v2p1">B</imsqti:p>
                               </dep:inlineChoiceRichText>
                               <dep:inlineChoiceRichText identifier="C">
                                   <imsqti:p class="noParagraphMargin" xmlns:imsqti="http://www.imsglobal.org/xsd/imsqti_v2p1">C</imsqti:p>
                               </dep:inlineChoiceRichText>
                               <dep:inlineChoiceRichText identifier="D">
                                   <imsqti:p class="noParagraphMargin" xmlns:imsqti="http://www.imsglobal.org/xsd/imsqti_v2p1">D</imsqti:p>
                               </dep:inlineChoiceRichText>
                           </dep:inlineChoiceRichTextInteraction>
                           <dep:inlineChoiceRichTextInteraction xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:dep="http://www.imsglobal.org/xsd/imsqti_ext_v2p1" identifier="I2ebb2bdb-2ab2-4919-8d65-b6f218999a1c">
                               <dep:inlineChoiceRichText identifier="A">
                                   <imsqti:p class="noParagraphMargin" xmlns:imsqti="http://www.imsglobal.org/xsd/imsqti_v2p1">A.A</imsqti:p>
                               </dep:inlineChoiceRichText>
                               <dep:inlineChoiceRichText identifier="B">
                                   <imsqti:p class="noParagraphMargin" xmlns:imsqti="http://www.imsglobal.org/xsd/imsqti_v2p1">B.B</imsqti:p>
                               </dep:inlineChoiceRichText>
                               <dep:inlineChoiceRichText identifier="C">
                                   <imsqti:p class="noParagraphMargin" xmlns:imsqti="http://www.imsglobal.org/xsd/imsqti_v2p1">C.C</imsqti:p>
                               </dep:inlineChoiceRichText>
                               <dep:inlineChoiceRichText identifier="D">
                                   <imsqti:p class="noParagraphMargin" xmlns:imsqti="http://www.imsglobal.org/xsd/imsqti_v2p1">D.D</imsqti:p>
                               </dep:inlineChoiceRichText>
                           </dep:inlineChoiceRichTextInteraction>
                           <dep:inlineChoiceRichTextInteraction xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:dep="http://www.imsglobal.org/xsd/imsqti_ext_v2p1" identifier="Ibf865980-8621-4005-a520-3ad82bff7aae">
                               <dep:inlineChoiceRichText identifier="A">
                                   <imsqti:p class="noParagraphMargin" xmlns:imsqti="http://www.imsglobal.org/xsd/imsqti_v2p1">A.A.A</imsqti:p>
                               </dep:inlineChoiceRichText>
                               <dep:inlineChoiceRichText identifier="B">
                                   <imsqti:p class="noParagraphMargin" xmlns:imsqti="http://www.imsglobal.org/xsd/imsqti_v2p1">B.B.B</imsqti:p>
                               </dep:inlineChoiceRichText>
                               <dep:inlineChoiceRichText identifier="C">
                                   <imsqti:p class="noParagraphMargin" xmlns:imsqti="http://www.imsglobal.org/xsd/imsqti_v2p1">C.C.C</imsqti:p>
                               </dep:inlineChoiceRichText>
                           </dep:inlineChoiceRichTextInteraction>
                       </qtiextension>
                       <div xmlns="http://www.w3.org/1999/xhtml">
                           <div id="itemquestion">
                               <p id="c1-id-11">Tekst <qti-text-entry-interaction pattern-mask="^.{0,10}$" response-identifier="I48c06d9f-edb6-40f3-a759-df23c4d95e2f" expected-length="10"/>
                               </p>
                               <p id="c1-id-12">Geheel getal <qti-text-entry-interaction pattern-mask="^-?([0-9]{1,5})?$" response-identifier="Ie4b22c84-5b60-4e02-a4bf-df6dd36cc504" expected-length="6"/>
                               </p>
                               <p id="c1-id-13">Decimaal <qti-text-entry-interaction pattern-mask="^-?([0-9]{1,6})?(([\,])([0-9]{0,2}))?$" response-identifier="I7063ab42-3f7e-432f-a6bc-fa3f3867e617" expected-length="9"/>
                               </p>
                               <p id="c1-id-14">Bedrag <qti-text-entry-interaction pattern-mask="^-?([0-9]{1,5})?(([\,])([0-9]{0,2}))?$" response-identifier="I05fc1135-87b1-4070-9f1b-44396624a85c" expected-length="8"/>
                               </p>
                               <p id="c1-id-15">Datum <span>
                                       <qti-text-entry-interaction pattern-mask="^(?![0])(([0-2]?[0-9])|([3][0-1]))$" response-identifier="Ib946bb76-51f2-4a58-b865-c5aef235a439" expected-length="2" date-sub-type="dutch"/>
								    
								        -
								    
							  
								    <qti-text-entry-interaction pattern-mask="^(?![0])(([0-9])|([1][0-2]))$" response-identifier="Ib946bb76-51f2-4a58-b865-c5aef235a439" expected-length="2" date-sub-type="dutch"/>
								    
								        -
								    
							  
								    <qti-text-entry-interaction pattern-mask="^([0-9]{1,4})$" response-identifier="Ib946bb76-51f2-4a58-b865-c5aef235a439" expected-length="4" date-sub-type="dutch"/>
                                   </span>
                               </p>
                               <p id="c1-id-16">Tijd (hh:mm:ss) <span>
                                       <qti-text-entry-interaction pattern-mask="^(([0-1]?[0-9])|([2][0-3]))$" response-identifier="Ie2c5a24d-8187-4ab3-bb89-cb31d1a250da" expected-length="2" time-sub-type="hhmmss"/>
							  
								    :
								    
								    <qti-text-entry-interaction pattern-mask="^([0-5]?[0-9])$" response-identifier="Ie2c5a24d-8187-4ab3-bb89-cb31d1a250da" expected-length="2" time-sub-type="hhmmss"/>
							  
								    :
								    
								    <qti-text-entry-interaction pattern-mask="^([0-5]?[0-9])$" response-identifier="Ie2c5a24d-8187-4ab3-bb89-cb31d1a250da" expected-length="2" time-sub-type="hhmmss"/>
                                   </span>
                               </p>
                               <p id="c1-id-17">Formule <qti-text-entry-interaction id="If8544370-1046-4080-9dda-3cf3077d9384" response-identifier="If8544370-1046-4080-9dda-3cf3077d9384"/>
                               </p>
                               <p id="c1-id-18">Formule (meerregelig) <qti-extended-text-interaction id="Ifd8a5530-6ab4-43f7-a25c-372d1dd4570f" response-identifier="Ifd8a5530-6ab4-43f7-a25c-372d1dd4570f" isFormulaEditor="True"/>
                               </p>
                               <p id="c1-id-20">Choice <qti-inline-choice-interaction id="Ibe763973-0fb7-42da-aadb-32950e644bfd" response-identifier="Ibe763973-0fb7-42da-aadb-32950e644bfd" shuffle="false" required="true">
                                       <qti-inline-choice identifier="A"/>
                                       <qti-inline-choice identifier="B"/>
                                       <qti-inline-choice identifier="C"/>
                                       <qti-inline-choice identifier="D"/>
                                   </qti-inline-choice-interaction> en choice 2 <qti-inline-choice-interaction id="I2ebb2bdb-2ab2-4919-8d65-b6f218999a1c" response-identifier="I2ebb2bdb-2ab2-4919-8d65-b6f218999a1c" shuffle="false" required="true">
                                       <qti-inline-choice identifier="A"/>
                                       <qti-inline-choice identifier="B"/>
                                       <qti-inline-choice identifier="C"/>
                                       <qti-inline-choice identifier="D"/>
                                   </qti-inline-choice-interaction> en choice 3 <qti-inline-choice-interaction id="Ibf865980-8621-4005-a520-3ad82bff7aae" response-identifier="Ibf865980-8621-4005-a520-3ad82bff7aae" shuffle="false" required="true">
                                       <qti-inline-choice identifier="A"/>
                                       <qti-inline-choice identifier="B"/>
                                       <qti-inline-choice identifier="C"/>
                                   </qti-inline-choice-interaction>
                               </p>
                               <p id="c1-id-22">Meerkeuze 1<br id="c1-id-23"/>
                                   <qti-choice-interaction id="I5e9be57a-1cc0-4501-804b-f89bbb666852" class="" shuffle="false" response-identifier="I5e9be57a-1cc0-4501-804b-f89bbb666852" max-choices="1">
                                       <qti-simple-choice identifier="I5e9be57a-1cc0-4501-804b-f89bbb666852A">
                                           <p id="c1-id-11">A</p>
                                       </qti-simple-choice>
                                       <qti-simple-choice identifier="I5e9be57a-1cc0-4501-804b-f89bbb666852B">
                                           <p id="c1-id-11">B</p>
                                       </qti-simple-choice>
                                       <qti-simple-choice identifier="I5e9be57a-1cc0-4501-804b-f89bbb666852C">
                                           <p id="c1-id-11">C</p>
                                       </qti-simple-choice>
                                   </qti-choice-interaction>
                               </p>
                               <p id="c1-id-25">Meerkeuze 2<br id="c1-id-26"/>
                                   <qti-choice-interaction id="Ib072620d-58c8-4695-b219-6591412548ff" class="" shuffle="false" response-identifier="Ib072620d-58c8-4695-b219-6591412548ff" max-choices="1">
                                       <qti-simple-choice identifier="Ib072620d-58c8-4695-b219-6591412548ffA">
                                           <p id="c1-id-11">A.A</p>
                                       </qti-simple-choice>
                                       <qti-simple-choice identifier="Ib072620d-58c8-4695-b219-6591412548ffB">
                                           <p id="c1-id-11">B.B</p>
                                       </qti-simple-choice>
                                       <qti-simple-choice identifier="Ib072620d-58c8-4695-b219-6591412548ffC">
                                           <p id="c1-id-11">C.C</p>
                                       </qti-simple-choice>
                                       <qti-simple-choice identifier="Ib072620d-58c8-4695-b219-6591412548ffD">
                                           <p id="c1-id-11">D.D</p>
                                       </qti-simple-choice>
                                   </qti-choice-interaction>
                               </p>
                               <p id="c1-id-30">Multiple Response<br id="c1-id-32"/>
                                   <qti-choice-interaction id="Id8d3499a-99fa-446d-a588-6216b3ae6109" class="" shuffle="false" response-identifier="Id8d3499a-99fa-446d-a588-6216b3ae6109" max-choices="0">
                                       <qti-simple-choice identifier="Id8d3499a-99fa-446d-a588-6216b3ae6109A">
                                           <p id="c1-id-11">a</p>
                                       </qti-simple-choice>
                                       <qti-simple-choice identifier="Id8d3499a-99fa-446d-a588-6216b3ae6109B">
                                           <p id="c1-id-11">b</p>
                                       </qti-simple-choice>
                                       <qti-simple-choice identifier="Id8d3499a-99fa-446d-a588-6216b3ae6109C">
                                           <p id="c1-id-11">c</p>
                                       </qti-simple-choice>
                                       <qti-simple-choice identifier="Id8d3499a-99fa-446d-a588-6216b3ae6109D">
                                           <p id="c1-id-11">d</p>
                                       </qti-simple-choice>
                                       <qti-simple-choice identifier="Id8d3499a-99fa-446d-a588-6216b3ae6109E">
                                           <p id="c1-id-11">e</p>
                                       </qti-simple-choice>
                                   </qti-choice-interaction>
                               </p>
                           </div>
                           <div id="answer">
                           </div>
                       </div>
                   </div>
               </qti-item-body>
           </wrapper>

        Private _itemBody2 As XElement =
            <wrapper>
                <qti-item-body class="defaultBody">
                    <div class="content">
                        <styles xmlns="http://www.w3.org/1999/xhtml">
                            <stylecollection>

                            </stylecollection>
                        </styles>
                        <div class="div_left" xmlns="http://www.w3.org/1999/xhtml">
                            <!-- div_left_inner is needed to create the 50px margin between the left and right columns in dual column mode -->
                            <div class="div_left_inner">
                                <div id="leftbody">
                                    <p id="c1-id-13">
                                        <b id="c1-id-12">Gieter</b>
                                    </p>
                                    <p id="c1-id-15">
                                        <span id="c1-id-14">In een gieter...</span>
                                    </p>
                                    <p id="c1-id-18">
                                        <span id="c1-id-16">Het giet ...</span>
                                    </p>
                                    <p id="c1-id-19">
                                        <span id="c1-id-20">
                                            <img id="IMG_306be47c-1da8-4455-af58-a8d03fa41cc8" src="resource://package/nsk1-16cbtBB-A2_gieterij.jpg" width="345" height="230" alt="" isinlineelement="true"/> </span>
                                    </p>
                                </div>
                            </div>
                        </div>
                        <div class="div_right" xmlns="http://www.w3.org/1999/xhtml">
                            <!-- div_right_inner is needed to create the 50px margin between the left and right columns in dual column mode -->
                            <div class="div_right_inner">
                                <div id="itemquestion">
                                    <p id="c1-id-11">Tijdens het ...</p>
                                    <p id="c1-id-12">Over het ...</p>
                                    <p id="c1-id-13">
                                        <b id="c1-id-14">Maak ...</b>
                                    </p>
                                    <p id="c1-id-15">Hadawa<qti-inline-choice-interaction response-identifier="I86634265-270f-46ea-8c73-4f6510e16068" shuffle="false" required="true">
                                            <qti-inline-choice identifier="A">optie A</qti-inline-choice>
                                            <qti-inline-choice identifier="B">optie B</qti-inline-choice>
                                            <qti-inline-choice identifier="C">optie C</qti-inline-choice>
                                        </qti-inline-choice-interaction>wanneer</p>
                                    <p id="c1-id-16">Bada<qti-inline-choice-interaction response-identifier="I4c1a66d2-86aa-4755-be91-4cf664e059ed" shuffle="false" required="true">
                                            <qti-inline-choice identifier="A">A</qti-inline-choice>
                                            <qti-inline-choice identifier="B">B</qti-inline-choice>
                                            <qti-inline-choice identifier="C">C</qti-inline-choice>
                                        </qti-inline-choice-interaction>wanneer</p>
                                </div>
                                <div id="answer">

                                </div>
                            </div>
                        </div>
                    </div>
                </qti-item-body>
            </wrapper>

        Private _itemBody3 As XElement =
            <wrapper>
                <qti-item-body class="defaultBody">
                    <div class="content">
                        <div xmlns="http://www.w3.org/1999/xhtml">
                            <div id="itemquestion">
                                <p id="c1-id-11">Denkend aan Holland</p>
                                <p id="c1-id-12">
                                    <strong id="c1-id-13">zie ik brede rivieren.</strong>
                                </p>
                                <table style="BORDER-COLLAPSE: collapse; " id="c1-id-14">
                                    <colgroup id="c1-id-15">
                                        <col id="c1-id-16"/>
                                        <col id="c1-id-17"/>
                                    </colgroup>
                                    <tbody id="c1-id-18">
                                        <tr style="BORDER-BOTTOM: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid" class="UserSRTitelGroot" id="c1-id-19">
                                            <td style="BORDER-BOTTOM: black 1px solid; BORDER-LEFT: black 1px solid; BACKGROUND-COLOR: #8080ff; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid" valign="middle" colspan="2" id="c1-id-20">
                                                <p id="c1-id-21">Kaatsheuvel</p>
                                            </td>
                                        </tr>
                                        <tr style="BORDER-BOTTOM: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid" id="c1-id-22">
                                            <td style="border-left:Black 1px Solid;PADDING-BOTTOM: 3px;PADDING-LEFT: 3px;PADDING-RIGHT: 3px;border-top:Black 1px Solid;PADDING-TOP: 3px;background-color:#C0C0FF;" id="c1-id-23">
                                                <p id="c1-id-24">Chumps</p>
                                            </td>
                                            <td style="PADDING-BOTTOM: 3px;background-color:#E7E7FF;PADDING-LEFT: 3px;PADDING-RIGHT: 3px;border-right:Black 1px Solid;PADDING-TOP: 3px;border-left:Black 1px Solid;border-top:Black 1px Solid;" id="c1-id-25">
                                                <p id="c1-id-26">20-05-0000 / € 20,00 / per bus</p>
                                            </td>
                                        </tr>
                                        <tr style="BORDER-BOTTOM: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid" id="c1-id-27">
                                            <td valign="top" style="border-left:Black 1px Solid;PADDING-BOTTOM: 3px;PADDING-LEFT: 3px;PADDING-RIGHT: 3px;border-top:Black 1px Solid;PADDING-TOP: 3px;background-color:#C0C0FF;" id="c1-id-28">
                                                <p id="c1-id-29">Opmerking</p>
                                            </td>
                                            <td style="PADDING-BOTTOM: 3px;PADDING-LEFT: 3px;PADDING-RIGHT: 3px;border-right:Black 1px Solid;PADDING-TOP: 3px;border-left:Black 1px Solid;border-top:Black 1px Solid;background-color:#E7E7FF;" id="c1-id-30">
                                                <p id="c1-id-31">Foo.</p>
                                            </td>
                                        </tr>
                                        <tr style="BORDER-BOTTOM: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid" id="c1-id-32">
                                            <td style="BORDER-BOTTOM: black 1px solid; BORDER-LEFT: black 1px solid; PADDING-BOTTOM: 3px; BACKGROUND-COLOR: #f7a85b; PADDING-LEFT: 3px; PADDING-RIGHT: 3px; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid; PADDING-TOP: 3px" valign="top" colspan="2" id="c1-id-33">
                                                <p id="c1-id-34">
                                                    <strong id="c1-id-35">Bar</strong>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr style="BORDER-BOTTOM: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid" id="c1-id-36">
                                            <td style="border-left:Black 1px Solid;PADDING-BOTTOM: 3px;background-color:#FFE0C0;PADDING-LEFT: 3px;PADDING-RIGHT: 3px;border-right:Black 1px Solid;PADDING-TOP: 3px;border-top:Black 1px Solid;" valign="top" id="c1-id-37">
                                                <p id="c1-id-38">Ik zei.. </p>
                                            </td>
                                            <td style="BORDER-BOTTOM: black 1px solid;PADDING-BOTTOM: 3px;PADDING-LEFT: 3px;PADDING-RIGHT: 3px;BORDER-RIGHT: black 1px solid;PADDING-TOP: 3px;" id="c1-id-39">
                                                <p id="c1-id-40">
                                                    <qti-text-entry-interaction pattern-mask="^.{0,2}$" response-identifier="I6be3d96d-b21a-403d-ad73-6e8f35d816ef" expected-length="2"/> meegaat op schoolreis en ga akkoord met de kosten. Deze zal ik per bank overmaken.</p>
                                            </td>
                                        </tr>
                                        <tr style="BORDER-BOTTOM: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid" id="c1-id-41">
                                            <td valign="top" style="border-left:Black 1px Solid;border-top:Black 1px Solid;border-right:Black 1px Solid;border-bottom:Black 1px Solid;background-color:#FFE0C0;" id="c1-id-42">
                                                <p id="c1-id-43">In... <br id="c1-id-44"/>Anna</p>
                                            </td>
                                            <td style="BORDER-BOTTOM: black 1px solid;PADDING-BOTTOM: 3px;PADDING-LEFT: 3px;PADDING-RIGHT: 3px;BORDER-RIGHT: black 1px solid;PADDING-TOP: 3px;" id="c1-id-45">
                                                <p id="c1-id-46">
                                                    <qti-text-entry-interaction pattern-mask="^.{0,30}$" response-identifier="I9d223733-c393-4c55-9f86-7b6e9d13d8a0" expected-length="30"/>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr style="BORDER-BOTTOM: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid" id="c1-id-47">
                                            <td style="BORDER-LEFT: black 1px solid;PADDING-BOTTOM: 3px;BACKGROUND-COLOR: #ffe0c0;PADDING-LEFT: 3px;PADDING-RIGHT: 3px;BORDER-RIGHT: black 1px solid;PADDING-TOP: 3px;" valign="top" id="c1-id-48">
                                                <p id="c1-id-49">Bing</p>
                                            </td>
                                            <td style="BORDER-BOTTOM: black 1px solid; PADDING-BOTTOM: 3px; PADDING-LEFT: 3px; PADDING-RIGHT: 3px; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid; PADDING-TOP: 3px" id="c1-id-50">
                                                <p id="c1-id-51">
                                                    <qti-text-entry-interaction pattern-mask="^.{0,10}$" response-identifier="Ib0f9a278-9f9f-43fd-be24-1120f39ab250" expected-length="10"/> <span>
                                                        <qti-text-entry-interaction pattern-mask="^(([0-1]?[0-9])|([2][0-3]))$" response-identifier="I6f1d0321-bc36-411e-a323-22aa9c07f17e" expected-length="2" time-sub-type="hhmmss"/>
								
									:
									
									<qti-text-entry-interaction pattern-mask="^([0-5]?[0-9])$" response-identifier="I6f1d0321-bc36-411e-a323-22aa9c07f17e" expected-length="2" time-sub-type="hhmmss"/>
								
									:
									
									<qti-text-entry-interaction pattern-mask="^([0-5]?[0-9])$" response-identifier="I6f1d0321-bc36-411e-a323-22aa9c07f17e" expected-length="2" time-sub-type="hhmmss"/>
                                                    </span>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr style="BORDER-BOTTOM: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid" id="c1-id-52">
                                            <td style="BORDER-BOTTOM: black 1px solid; BORDER-LEFT: black 1px solid; PADDING-BOTTOM: 3px; BACKGROUND-COLOR: #ffe0c0; PADDING-LEFT: 3px; PADDING-RIGHT: 3px; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid; PADDING-TOP: 3px" valign="top" id="c1-id-53">
                                                <p id="c1-id-54">Bananastand</p>
                                            </td>
                                            <td style="BORDER-BOTTOM: black 1px solid; PADDING-BOTTOM: 3px; PADDING-LEFT: 3px; PADDING-RIGHT: 3px; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid; PADDING-TOP: 3px" id="c1-id-55">
                                                <p id="c1-id-56">
                                                    <span>
                                                        <qti-text-entry-interaction pattern-mask="^(?![0])(([0-2]?[0-9])|([3][0-1]))$" response-identifier="Idfde9b07-cbd9-49ac-b790-0bfdb2fbe8f6" expected-length="2" date-sub-type="dutch"/>
									
									-
									
								
									<qti-text-entry-interaction pattern-mask="^(?![0])(([0-9])|([1][0-2]))$" response-identifier="Idfde9b07-cbd9-49ac-b790-0bfdb2fbe8f6" expected-length="2" date-sub-type="dutch"/>
									
									-
									
								
									<qti-text-entry-interaction pattern-mask="^([0-9]{1,4})$" response-identifier="Idfde9b07-cbd9-49ac-b790-0bfdb2fbe8f6" expected-length="4" date-sub-type="dutch"/>
                                                    </span>    <qti-extended-text-interaction response-identifier="If6d80e16-db5d-4be6-89e9-3044c7cf36a9" expected-lines="5" expected-length="0"/>
                                                </p>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <p id="c1-id-57">
                                    <br id="c1-id-58"/>
                                    <strong id="c1-id-59">Wat maakt dit.. </strong>
                                </p>
                                <p id="c1-id-60">
                                    <em id="c1-id-61">Surprise.</em>
                                </p>
                                <p id="c1-id-62">
                                    <em id="c1-id-63">
                                        <em id="c1-id-64">
                                            <em id="c1-id-65">
                                                <em id="c1-id-66">
                                                    <qti-extended-text-interaction response-identifier="I1a0e068a-5825-4566-baf6-cc3662e2f7d5" expected-lines="1" expected-length="0"/>
                                                </em>
                                            </em>
                                        </em>
                                    </em>
                                </p>
                                <p id="c1-id-67">
                                    <em id="c1-id-68">
                                        <em id="c1-id-69">
                                            <em id="c1-id-70">
                                                <em id="c1-id-71"> </em>
                                            </em>
                                        </em>
                                    </em>
                                </p>
                                <p id="c1-id-72"> </p>
                                <p id="c1-id-73">
                                    <em id="c1-id-74">
                                        <em id="c1-id-75">
                                            <em id="c1-id-76">
                                                <em id="c1-id-77">Inlinecontrol-item: type </em>
                                            </em>
                                        </em>
                                    </em>
                                    <em id="c1-id-78">
                                        <em id="c1-id-79">
                                            <em id="c1-id-80">ok, </em>
                                        </em>
                                    </em>
                                    <em id="c1-id-81">een kolom.</em>
                                </p>
                            </div>
                            <div id="answer">

                            </div>
                        </div>
                    </div>
                </qti-item-body>
            </wrapper>

        Private _itemBody4 As XElement =
            <wrapper>
                <qti-item-body class="defaultBody">
                    <div class="content">
                        <div>
                            <div id="itemquestion">
                                <qti-gap-match-interaction response-identifier="gapMatchController" shuffle="false">
                                    <qti-gap-text identifier="A" matchMax="3">jou</qti-gap-text>
                                    <qti-gap-text identifier="B" matchMax="3">jouw</qti-gap-text>
                                    <p id="c1-id-11">
                                        <strong id="c1-id-12">juist..</strong>
                                    </p>
                                    <p id="c1-id-13">Het .. <span>
                                            <qti-gap identifier="Icdc09f4f-2ce5-44fa-8964-aa27e0a589bf" required="true"/>
                                        </span> terug... </p>
                                    <p id="c1-id-14">Ik geef <span>
                                            <qti-gap identifier="I4c0faece-4936-4dc0-bcef-728acc334870" required="true"/>
                                        </span>snel .. <span>
                                            <qti-gap identifier="I301ac896-7857-4190-9186-a60406f986a6" required="true"/>
                                        </span> terug.</p>
                                    <p id="c1-id-15"> </p>
                                    <p id="c1-id-16">
                                        <em id="c1-id-17">
                                            <em id="c1-id-18">
                                                <em id="c1-id-19"> </em>
                                            </em>
                                        </em>
                                    </p>
                                    <p id="c1-id-20">
                                        <em id="c1-id-21">
                                            <em id="c1-id-22">
                                                <em id="c1-id-23">Inlinecontrol-item: type </em>
                                            </em>
                                        </em>
                                        <em id="c1-id-24">
                                            <em id="c1-id-25">gap</em>
                                        </em>
                                        <em id="c1-id-26">match (Facet), een kolom. <br id="c1-id-27"/>
                                            <sup id="c1-id-28">(Wat iz deze)</sup>
                                        </em>
                                    </p>
                                </qti-gap-match-interaction>
                            </div>
                            <div id="answer">

                            </div>
                        </div>
                    </div>
                </qti-item-body>
            </wrapper>



        Private _solution1 As XElement =
            <solution>
                <keyFindings>
                    <keyFinding id="IEF" scoringMethod="Dichotomous">
                        <keyFact id="A-Id8d3499a-99fa-446d-a588-6216b3ae6109" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="A-Id8d3499a-99fa-446d-a588-6216b3ae6109" occur="1">
                                <booleanValue>
                                    <typedValue>false</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="B-Id8d3499a-99fa-446d-a588-6216b3ae6109" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="B-Id8d3499a-99fa-446d-a588-6216b3ae6109" occur="1">
                                <booleanValue>
                                    <typedValue>false</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="C-Id8d3499a-99fa-446d-a588-6216b3ae6109" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="C-Id8d3499a-99fa-446d-a588-6216b3ae6109" occur="1">
                                <booleanValue>
                                    <typedValue>false</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="D-Id8d3499a-99fa-446d-a588-6216b3ae6109" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="D-Id8d3499a-99fa-446d-a588-6216b3ae6109" occur="1">
                                <booleanValue>
                                    <typedValue>false</typedValue>
                                </booleanValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="E-Id8d3499a-99fa-446d-a588-6216b3ae6109" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="E-Id8d3499a-99fa-446d-a588-6216b3ae6109" occur="1">
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

        Private _solution6 As XElement =
                    <solution>
                        <keyFindings>
                            <keyFinding id="inlineChoiceController" scoringMethod="Polytomous">
                                <keyFact id="I4c1a66d2-86aa-4755-be91-4cf664e059ed" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                    <keyValue domain="I4c1a66d2-86aa-4755-be91-4cf664e059ed" occur="1">
                                        <stringValue>
                                            <typedValue>B</typedValue>
                                        </stringValue>
                                    </keyValue>
                                </keyFact>
                                <keyFact id="I86634265-270f-46ea-8c73-4f6510e16068" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                    <keyValue domain="I86634265-270f-46ea-8c73-4f6510e16068" occur="1">
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
                            <ItemScoreTranslationTableEntry rawScore="2" translatedScore="2"/>
                        </ItemScoreTranslationTable>
                    </solution>

        Private _solution7 As XElement =
            <solution>
                <keyFindings/>
                <aspectReferences>
                    <aspectReferenceSet id="extendedTextEntryController">
                        <aspectReference maxScore="5" src="Inhoud"></aspectReference>
                    </aspectReferenceSet>
                </aspectReferences>
                <ItemScoreTranslationTable>
                    <ItemScoreTranslationTableEntry rawScore="0" translatedScore="0"/>
                    <ItemScoreTranslationTableEntry rawScore="1" translatedScore="1"/>
                    <ItemScoreTranslationTableEntry rawScore="2" translatedScore="2"/>
                    <ItemScoreTranslationTableEntry rawScore="3" translatedScore="3"/>
                    <ItemScoreTranslationTableEntry rawScore="4" translatedScore="4"/>
                    <ItemScoreTranslationTableEntry rawScore="5" translatedScore="5"/>
                </ItemScoreTranslationTable>
            </solution>

        Private _solution8 As XElement =
            <solution>
                <keyFindings>
                    <keyFinding id="gapMatchController" scoringMethod="Dichotomous">
                        <keyFact id="I301ac896-7857-4190-9186-a60406f986a6" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I301ac896-7857-4190-9186-a60406f986a6" occur="1">
                                <stringValue>
                                    <typedValue>A</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="I4c0faece-4936-4dc0-bcef-728acc334870" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="I4c0faece-4936-4dc0-bcef-728acc334870" occur="1">
                                <stringValue>
                                    <typedValue>B</typedValue>
                                </stringValue>
                            </keyValue>
                        </keyFact>
                        <keyFact id="Icdc09f4f-2ce5-44fa-8964-aa27e0a589bf" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                            <keyValue domain="Icdc09f4f-2ce5-44fa-8964-aa27e0a589bf" occur="1">
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



        Private _result1 As XElement =
    <root>
        <qti-response-declaration identifier="RESPONSE" cardinality="single" base-type="string"/>
        <qti-response-declaration identifier="RESPONSE2" cardinality="single" base-type="string"/>
        <qti-response-declaration identifier="RESPONSE3" cardinality="single" base-type="string"/>
        <qti-response-declaration identifier="RESPONSE4" cardinality="single" base-type="string"/>
        <qti-response-declaration identifier="RESPONSE5" cardinality="single" base-type="string"/>
        <qti-response-declaration identifier="RESPONSE6" cardinality="single" base-type="string"/>
        <qti-response-declaration identifier="RESPONSE7" cardinality="single" base-type="string"/>
        <qti-response-declaration identifier="RESPONSE8" cardinality="single" base-type="string"/>
        <qti-response-declaration identifier="RESPONSE9" cardinality="single" base-type="string"/>
        <qti-response-declaration identifier="RESPONSE10" cardinality="single" base-type="string"/>
        <qti-response-declaration identifier="RESPONSE11" cardinality="single" base-type="string"/>
        <qti-response-declaration identifier="RESPONSE12" cardinality="ordered" base-type="string"/>
        <qti-response-declaration identifier="RESPONSE13" cardinality="single" base-type="string"/>
        <qti-response-declaration identifier="RESPONSE14" cardinality="single" base-type="string"/>
        <qti-response-declaration identifier="RESPONSE15" cardinality="single" base-type="string"/>
        <qti-response-declaration identifier="RESPONSE16" cardinality="single" base-type="string"/>
        <qti-response-declaration identifier="RESPONSE17" cardinality="single" base-type="string"/>
        <qti-response-declaration identifier="RESPONSE18" cardinality="multiple" base-type="string"/>
    </root>

        Private _result6 As XElement =
    <root>
        <qti-response-declaration identifier="RESPONSE" cardinality="single" base-type="identifier">
            <qti-correct-response interpretation="A">
                <qti-value>A</qti-value>
            </qti-correct-response>
        </qti-response-declaration>
        <qti-response-declaration identifier="RESPONSE2" cardinality="single" base-type="identifier">
            <qti-correct-response interpretation="B">
                <qti-value>B</qti-value>
            </qti-correct-response>
        </qti-response-declaration>
    </root>

        Private _result7 As XElement =
    <root>
        <qti-response-declaration identifier="RESPONSE" cardinality="single" base-type="string"/>
        <qti-response-declaration identifier="RESPONSE2" cardinality="single" base-type="string"/>
        <qti-response-declaration identifier="RESPONSE3" cardinality="single" base-type="string"/>
        <qti-response-declaration identifier="RESPONSE4" cardinality="single" base-type="string"/>
        <qti-response-declaration identifier="RESPONSE4" cardinality="single" base-type="string"/>
        <qti-response-declaration identifier="RESPONSE4" cardinality="single" base-type="string"/>
        <qti-response-declaration identifier="RESPONSE7" cardinality="single" base-type="string"/>
        <qti-response-declaration identifier="RESPONSE7" cardinality="single" base-type="string"/>
        <qti-response-declaration identifier="RESPONSE7" cardinality="single" base-type="string"/>
        <qti-response-declaration identifier="RESPONSE10" cardinality="single" base-type="string"/>
        <qti-response-declaration identifier="RESPONSE11" cardinality="single" base-type="string"/>
    </root>

        Private _result8 As XElement =
    <root>
        <qti-response-declaration identifier="RESPONSE" cardinality="multiple" base-type="directedPair">
            <qti-correct-response interpretation="A Icdc09f4f-2ce5-44fa-8964-aa27e0a589bf&amp;B I4c0faece-4936-4dc0-bcef-728acc334870&amp;A I301ac896-7857-4190-9186-a60406f986a6">
                <qti-value>A Icdc09f4f-2ce5-44fa-8964-aa27e0a589bf</qti-value>
                <qti-value>B I4c0faece-4936-4dc0-bcef-728acc334870</qti-value>
                <qti-value>A I301ac896-7857-4190-9186-a60406f986a6</qti-value>
            </qti-correct-response>
        </qti-response-declaration>
    </root>


    End Class

End Namespace
