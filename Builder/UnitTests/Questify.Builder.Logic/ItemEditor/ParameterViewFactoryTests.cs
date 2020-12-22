
using System.Linq;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.Logic.ContentModel;
using Questify.Builder.Logic.ItemEditor;

namespace Questify.Builder.UnitTests.Questify.Builder.Logic.ItemEditor
{
    [TestClass]
    public class ParameterViewFactoryTests
    {
        [TestMethod, TestCategory("ParameterEditor")]
        public void CountNumberOfGroups()
        {
            var parameterSetCollection = ParameterSetCollectionCloner.DeSerializerFromString(MCTemplate.ToString()); var factory = new ParameterViewFactory(parameterSetCollection);
            var groups = factory.GetGroups().ToList();
            Assert.AreEqual(8, groups.Count());
        }



        private readonly XElement MCTemplate = XElement.Parse(@"<?xml version=""1.0"" encoding=""utf-8""?>
<ArrayOfParameterCollection xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <ParameterCollection id=""entireItem"">
    <booleanparameter name=""isScoredItem"">
      <designersetting key=""visible"">False<listvalues /></designersetting>
      <designersetting key=""defaultvalue"">True<listvalues /></designersetting>True</booleanparameter>
    <booleanparameter name=""dualColumnLayout"">
      <designersetting key=""visible"">False<listvalues /></designersetting>
      <designersetting key=""defaultvalue"">False<listvalues /></designersetting>False</booleanparameter>
    <booleanparameter name=""showCalculatorButton"">
      <designersetting key=""label"">Rekenmachine<listvalues /></designersetting> 
      <designersetting key=""description"">Geef aan of tijdens het beantwoorden van het item de kandidaat gebruik mag maken van de ingebouwde calculator.<listvalues /></designersetting>
      <designersetting key=""required"">False<listvalues /></designersetting>
      <designersetting key=""sortkey"">1<listvalues /></designersetting>
      <designersetting key=""group"">1 Hulpmiddelen<listvalues /></designersetting>
      <designersetting key=""visible"">True<listvalues /></designersetting>
    </booleanparameter>
    <booleanparameter name=""displayVerklankingOnTheRight"">
      <designersetting key=""label"">Toon verklanking rechts<listvalues /></designersetting>
      <designersetting key=""visible"">False<listvalues /></designersetting>
      <designersetting key=""defaultvalue"">True<listvalues /></designersetting>
      <designersetting key=""group"">2 Verklanking<listvalues /></designersetting>
      <designersetting key=""sortkey"">1<listvalues /></designersetting>True</booleanparameter>
    <collectionparameter name=""numberOfAudioContentItems"">
      <designersetting key=""label"">
        <listvalues />
      </designersetting>
      <designersetting key=""visible"">False<listvalues /></designersetting>
      <designersetting key=""itemcountlabel"">Aantal audio bestanden<listvalues /></designersetting>
      <designersetting key=""description"">
        <listvalues />
      </designersetting>
      <designersetting key=""minimumLength"">0<listvalues /></designersetting>
      <designersetting key=""maximumLength"">5<listvalues /></designersetting>
      <designersetting key=""defaultvalue"">0<listvalues /></designersetting>
      <designersetting key=""subsetidentifiers"">Numeric<listvalues /></designersetting>
      <designersetting key=""group"">2 Verklanking<listvalues /></designersetting>
      <designersetting key=""required"">False<listvalues /></designersetting>
      <designersetting key=""sortkey"">2<listvalues /></designersetting>
      <definition id="""">
        <resourceparameter name=""audiocontent"">
          <designersetting key=""label"">Audiobestand<listvalues /></designersetting>
          <designersetting key=""description"">
            <listvalues />
          </designersetting>
          <designersetting key=""required"">False<listvalues /></designersetting>
          <designersetting key=""filter"">audio/mpeg<listvalues /></designersetting>
          <designersetting key=""editbuttonVisible"">false<listvalues /></designersetting>
          <designersetting key=""deletebuttonVisible"">false<listvalues /></designersetting>
          <designersetting key=""group"">
            <listvalues />
          </designersetting>
        </resourceparameter>
        <xhtmlparameter name=""description"">
          <designersetting key=""label"">Beschrijving<listvalues /></designersetting>
          <designersetting key=""group"">
            <listvalues />
          </designersetting>
          <designersetting key=""required"">False<listvalues /></designersetting>
          <designersetting key=""description"">
            <listvalues />
          </designersetting>
        </xhtmlparameter>
      </definition>
    </collectionparameter>
    <texttospeechparameter name=""verklankingLinks"">
      <designersetting key=""visible"">True<listvalues /></designersetting>
      <designersetting key=""label"">Verklanking<listvalues /></designersetting>
      <designersetting key=""group"">2 Verklanking<listvalues /></designersetting>
      <designersetting key=""sortkey"">1<listvalues /></designersetting>
      <designersetting key=""required"">False<listvalues /></designersetting>
      <designersetting key=""filter"">audio/mpeg|audio/mp3<listvalues /></designersetting>
      <designersetting key=""itemPart"">Both<listvalues /></designersetting>
    </texttospeechparameter>
    <texttospeechparameter name=""verklankingRechts"">
      <designersetting key=""visible"">False<listvalues /></designersetting>
      <designersetting key=""label"">Verklanking rechts<listvalues /></designersetting>
      <designersetting key=""group"">2 Verklanking<listvalues /></designersetting>
      <designersetting key=""sortkey"">2<listvalues /></designersetting>
      <designersetting key=""required"">False<listvalues /></designersetting>
      <designersetting key=""filter"">audio/mpeg|audio/mp3<listvalues /></designersetting>
      <designersetting key=""itemPart"">Right<listvalues /></designersetting>
    </texttospeechparameter>
    <xhtmlparameter name=""leftBody"">
      <designersetting key=""label"">Body links<listvalues /></designersetting>
      <designersetting key=""description"">Geef hier de tekst en/of de afbeelding in zoals die in het linkerdeel weergegeven dient te worden.<listvalues /></designersetting>
      <designersetting key=""group"">3 Linkerkolom<listvalues /></designersetting>
      <designersetting key=""required"">False<listvalues /></designersetting>
      <designersetting key=""visible"">False<listvalues /></designersetting>
      <designersetting key=""sortkey"">1<listvalues /></designersetting>
    </xhtmlparameter>
    <xhtmlresourceparameter name=""leftSource"">
      <designersetting key=""label"">CBT brontekst<listvalues /></designersetting>
      <designersetting key=""description"">Selecteer de brontekst uit de bank.<listvalues /></designersetting>
      <designersetting key=""group"">3 Linkerkolom<listvalues /></designersetting>
      <designersetting key=""required"">False<listvalues /></designersetting>
      <designersetting key=""visible"">False<listvalues /></designersetting>
      <designersetting key=""filter"">text/plain|text/html|application/xhtml+xml<listvalues /></designersetting>
      <designersetting key=""sortkey"">2<listvalues /></designersetting>
    </xhtmlresourceparameter>
    <resourceparameter name=""wordSourceText"">
      <designersetting key=""label"">PBT brontekst<listvalues /></designersetting>
      <designersetting key=""description"">Selecteer de brontekst uit de bank.<listvalues /></designersetting>
      <designersetting key=""required"">False<listvalues /></designersetting>
      <designersetting key=""filter"">application/vnd.openxmlformats-officedocument.wordprocessingml.document<listvalues /></designersetting>
      <designersetting key=""editbuttonVisible"">false<listvalues /></designersetting>
      <designersetting key=""group"">3 Linkerkolom<listvalues /></designersetting>
      <designersetting key=""visible"">False<listvalues /></designersetting>
    </resourceparameter>
    <integerparameter name=""sourceHeight"">
      <designersetting key=""label"">Hoogte<listvalues /></designersetting>
      <designersetting key=""description"">De hoogte van de brontekst.<listvalues /></designersetting>
      <designersetting key=""group"">3 Linkerkolom<listvalues /></designersetting>
      <designersetting key=""visible"">False<listvalues /></designersetting>
      <designersetting key=""defaultvalue"">200<listvalues /></designersetting>
      <designersetting key=""sortkey"">3<listvalues /></designersetting>200</integerparameter>
    <integerparameter name=""sourcePositionTop"">
      <designersetting key=""label"">Facet: Aantal pixels onder linker bovenhoek<listvalues /></designersetting>
      <designersetting key=""description"">Brontekst wordt links in de bovenhoek getoond. Als daar tekst staat zal de brontekst naar beneden geplaatst moeten worden.<listvalues /></designersetting>
      <designersetting key=""group"">3 Linkerkolom<listvalues /></designersetting>
      <designersetting key=""visible"">False<listvalues /></designersetting>
      <designersetting key=""defaultvalue"">0<listvalues /></designersetting>
      <designersetting key=""sortkey"">4<listvalues /></designersetting>0</integerparameter>
    <xhtmlparameter name=""itemBody"">
      <designersetting key=""label"">Body<listvalues /></designersetting>
      <designersetting key=""description"">Geef hier de tekst en/of de afbeelding(en) in zoals die boven de vraag weergegeven dienen te worden.<listvalues /></designersetting>
      <designersetting key=""group"">4 Stam<listvalues /></designersetting>
      <designersetting key=""required"">false<listvalues /></designersetting>
      <designersetting key=""sortkey"">1<listvalues /></designersetting>
    </xhtmlparameter>
    <xhtmlparameter name=""itemQuestion"">
      <designersetting key=""label"">Vraag<listvalues /></designersetting>
      <designersetting key=""description"">Geef hier de tekst en/of de afbeelding in zoals die in het rechterdeel boven de alternatieven weergegeven dient te worden.<listvalues /></designersetting>
      <designersetting key=""group"">4 Stam<listvalues /></designersetting>
      <designersetting key=""required"">True<listvalues /></designersetting>
      <designersetting key=""sortkey"">2<listvalues /></designersetting>
    </xhtmlparameter>
    <listedparameter name=""expectedAnswers"">
      <designersetting key=""label"">Max. sleutels<listvalues /></designersetting>
      <designersetting key=""description"">Geef aan uit hoeveel sleutels het antwoord maximaal bestaat.<listvalues /></designersetting>
      <designersetting key=""group"">6 Alternatieven<listvalues /></designersetting>
      <designersetting key=""visible"">False<listvalues /></designersetting>
      <designersetting key=""defaultvalue"">1<listvalues /></designersetting>
      <designersetting key=""sortkey"">1<listvalues /></designersetting>
      <designersetting key=""list"">
        <listvalues>
          <listvalue key=""0"">Onbeperkt</listvalue>
          <listvalue key=""1"">1</listvalue>
          <listvalue key=""2"">2</listvalue>
          <listvalue key=""3"">3</listvalue>
          <listvalue key=""4"">4</listvalue>
          <listvalue key=""5"">5</listvalue>
          <listvalue key=""6"">6</listvalue>
          <listvalue key=""7"">7</listvalue>
          <listvalue key=""8"">8</listvalue>
          <listvalue key=""9"">9</listvalue>
          <listvalue key=""10"">10</listvalue>
          <listvalue key=""11"">11</listvalue>
          <listvalue key=""12"">12</listvalue>
        </listvalues>
      </designersetting>1</listedparameter>
    <integerparameter name=""multiChoiceType"">
      <designersetting key=""label"">Type: mc/mr<listvalues /></designersetting>
      <designersetting key=""description"">0: check, 1: radio, 2: unknown<listvalues /></designersetting>
      <designersetting key=""group"">6 Alternatieven<listvalues /></designersetting>
      <designersetting key=""visible"">False<listvalues /></designersetting>
      <designersetting key=""defaultvalue"">1<listvalues /></designersetting>1</integerparameter>
    <booleanparameter name=""showGroup"">
      <designersetting key=""groupConditionalEnabled"">True<listvalues /></designersetting>
      <designersetting key=""groupConditionalEnabledSwitch"">5.1 Lay-outalternatieven<listvalues /></designersetting>
      <designersetting key=""groupConditionalEnabledWhenValue"">True<listvalues /></designersetting>
      <designersetting key=""label"">Instellingen voor lay-out van de alternatieven tonen.<listvalues /></designersetting>
      <designersetting key=""description"">De hulpmiddelen voor Facet kunnen worden ingevuld.<listvalues /></designersetting>
      <designersetting key=""group"">5 Lay-outalternatieven<listvalues /></designersetting>
      <designersetting key=""sortkey"">0<listvalues /></designersetting>
      <designersetting key=""defaultvalue"">False<listvalues /></designersetting>
      <designersetting key=""visible"">true<listvalues /></designersetting>
      <designersetting key=""required"">true<listvalues /></designersetting>False</booleanparameter>
    <integerparameter name=""fixedHeightAlternatives"">
      <designersetting key=""conditionalEnabled"">False<listvalues /></designersetting>
      <designersetting key=""label"">Vaste hoogte in pixels<listvalues /></designersetting>
      <designersetting key=""description"">Geef hier de vaste hoogte op voor de alternatieven.<listvalues /></designersetting>
      <designersetting key=""group"">5.1 Lay-outalternatieven<listvalues /></designersetting>
      <designersetting key=""sortkey"">1<listvalues /></designersetting>
      <designersetting key=""defaultvalue"">0<listvalues /></designersetting>
      <designersetting key=""visible"">True<listvalues /></designersetting>0</integerparameter>
    <listedparameter name=""nrAlternativesPerLine"">
      <designersetting key=""label"">Aantal alternatieven per regel<listvalues /></designersetting>
      <designersetting key=""description"">Geef het aantal alternatieven dat op 1 regel in het item weergegeven dienen te worden.<listvalues /></designersetting>
      <designersetting key=""group"">5.1 Lay-outalternatieven<listvalues /></designersetting>
      <designersetting key=""sortkey"">2<listvalues /></designersetting>
      <designersetting key=""defaultvalue"">1<listvalues /></designersetting>
      <designersetting key=""visible"">True<listvalues /></designersetting>
      <designersetting key=""list"">
        <listvalues>
          <listvalue key=""1"">1</listvalue>
          <listvalue key=""2"">2</listvalue>
          <listvalue key=""3"">3</listvalue>
          <listvalue key=""4"">4</listvalue>
          <listvalue key=""5"">5</listvalue>
          <listvalue key=""6"">6</listvalue>
          <listvalue key=""7"">7</listvalue>
          <listvalue key=""8"">8</listvalue>
          <listvalue key=""9"">9</listvalue>
          <listvalue key=""10"">10</listvalue>
          <listvalue key=""11"">11</listvalue>
          <listvalue key=""12"">12</listvalue>
        </listvalues>
      </designersetting>1</listedparameter>
    <booleanparameter name=""horizontallyCenteredAlternatives"">
      <designersetting key=""label"">Horizontaal gecentreerd<listvalues /></designersetting>
      <designersetting key=""description"">Geef aan of de alternatieven horizontaal gecentreerd dienen te worden.<listvalues /></designersetting>
      <designersetting key=""group"">5.1 Lay-outalternatieven<listvalues /></designersetting>
      <designersetting key=""sortkey"">3<listvalues /></designersetting>
      <designersetting key=""defaultvalue"">False<listvalues /></designersetting>
      <designersetting key=""visible"">True<listvalues /></designersetting>False</booleanparameter>
    <booleanparameter name=""hideRadiobuttons"">
      <designersetting key=""label"">Keuzerondjes/-vinkjes verbergen<listvalues /></designersetting>
      <designersetting key=""description"">Geef aan of de keuzerondjes/-vinkjes verborgen moeten worden.<listvalues /></designersetting>
      <designersetting key=""group"">5.1 Lay-outalternatieven<listvalues /></designersetting>
      <designersetting key=""sortkey"">4<listvalues /></designersetting>
      <designersetting key=""defaultvalue"">False<listvalues /></designersetting>
      <designersetting key=""visible"">True<listvalues /></designersetting>False</booleanparameter>
    <xhtmlparameter name=""kolomtekst"">
      <designersetting key=""label"">Kolomtekst kolom 1<listvalues /></designersetting>
      <designersetting key=""description"">
        <listvalues />
      </designersetting>
      <designersetting key=""group"">6 Alternatieven<listvalues /></designersetting>
      <designersetting key=""required"">False<listvalues /></designersetting>
      <designersetting key=""visible"">False<listvalues /></designersetting>
      <designersetting key=""sortkey"">2<listvalues /></designersetting>
    </xhtmlparameter>
    <xhtmlparameter name=""kolomtekst2"">
      <designersetting key=""label"">Kolomtekst kolom 2<listvalues /></designersetting>
      <designersetting key=""description"">
        <listvalues />
      </designersetting>
      <designersetting key=""group"">6 Alternatieven<listvalues /></designersetting>
      <designersetting key=""required"">False<listvalues /></designersetting>
      <designersetting key=""visible"">False<listvalues /></designersetting>
      <designersetting key=""sortkey"">3<listvalues /></designersetting>
    </xhtmlparameter>
    <collectionparameter name=""multiChoice"">
      <designersetting key=""label"">
        <listvalues />
      </designersetting>
      <designersetting key=""itemcountlabel"">Aantal alternatieven<listvalues /></designersetting>
      <designersetting key=""description"">
        <listvalues />
      </designersetting>
      <designersetting key=""visible"">False<listvalues /></designersetting>
      <designersetting key=""group"">6 Alternatieven<listvalues /></designersetting>
      <designersetting key=""minimumLength"">2<listvalues /></designersetting>
      <designersetting key=""maximumLength"">12<listvalues /></designersetting>
      <designersetting key=""subsetidentifiers"">Alphabetic<listvalues /></designersetting>
      <designersetting key=""sortkey"">4<listvalues /></designersetting>
      <definition id="""">
        <xhtmlparameter name=""choice"">
          <designersetting key=""label"">Keuze<listvalues /></designersetting>
          <designersetting key=""description"">
            <listvalues />
          </designersetting>
          <designersetting key=""required"">false<listvalues /></designersetting>
        </xhtmlparameter>
        <xhtmlparameter name=""choice2"">
          <designersetting key=""label"">Keuze kolom 2<listvalues /></designersetting>
          <designersetting key=""description"">
            <listvalues />
          </designersetting>
          <designersetting key=""required"">False<listvalues /></designersetting>
          <designersetting key=""visible"">False<listvalues /></designersetting>
        </xhtmlparameter>
      </definition>
    </collectionparameter>
    <multichoicescoringparameter name=""multiChoiceScoring"" ControllerId=""mc"" findingOverride=""mc"" minChoices=""1"" maxChoices=""1"" multiChoice=""Radio"">
      <designersetting key=""label"">
        <listvalues />
      </designersetting>
      <designersetting key=""itemcountlabel"">Aantal alternatieven<listvalues /></designersetting>
      <designersetting key=""description"">
        <listvalues />
      </designersetting>
      <designersetting key=""visible"">true<listvalues /></designersetting>
      <designersetting key=""group"">6 Alternatieven<listvalues /></designersetting>
      <designersetting key=""sortkey"">4<listvalues /></designersetting>
      <designersetting key=""minimumLength"">2<listvalues /></designersetting>
      <designersetting key=""maximumLength"">12<listvalues /></designersetting>
      <designersetting key=""subsetidentifiers"">Alphabetic<listvalues /></designersetting>
      <attributereference name=""minChoices"" whattocopy=""Value"">expectedAnswers</attributereference>
      <attributereference name=""maxChoices"" whattocopy=""Value"">expectedAnswers</attributereference>
      <attributereference name=""multiChoice"" whattocopy=""Value"">multiChoiceType</attributereference>
      <definition id="""">
        <xhtmlparameter name=""mcOption"">
          <designersetting key=""label"">Keuze<listvalues /></designersetting>
          <designersetting key=""description"">
            <listvalues />
          </designersetting>
          <designersetting key=""required"">true<listvalues /></designersetting>
          <designersetting key=""visible"">true<listvalues /></designersetting>
        </xhtmlparameter>
        <xhtmlparameter name=""mcOption2"">
          <designersetting key=""label"">Keuze kolom 2<listvalues /></designersetting>
          <designersetting key=""description"">
            <listvalues />
          </designersetting>
          <designersetting key=""required"">False<listvalues /></designersetting>
          <designersetting key=""visible"">False<listvalues /></designersetting>
        </xhtmlparameter>
      </definition>
    </multichoicescoringparameter>
    <xhtmlparameter name=""itemGeneral"">
      <designersetting key=""label"">Algemeen tekstveld<listvalues /></designersetting>
      <designersetting key=""description"">Geef hier de tekst en/of de afbeelding in zoals die in het rechterdeel onder de antwoorden weergegeven dient te worden.<listvalues /></designersetting>
      <designersetting key=""group"">7 Algemeen tekstveld<listvalues /></designersetting>
      <designersetting key=""required"">False<listvalues /></designersetting>
      <designersetting key=""sortkey"">1<listvalues /></designersetting>
    </xhtmlparameter>
    <booleanparameter name=""boldedMcLettersForWord"">
      <designersetting key=""conditionalEnabled"">False<listvalues /></designersetting>
      <designersetting key=""label"">Antwoordmogelijkheid in Word vetgedrukt weergeven<listvalues /></designersetting>
      <designersetting key=""description"">Geef aan of de alternatieven vetgedrukt weergegeven dienen te worden.<listvalues /></designersetting>
      <designersetting key=""group"">5.1 Lay-outalternatieven<listvalues /></designersetting>
      <designersetting key=""defaultvalue"">True<listvalues /></designersetting>
      <designersetting key=""visible"">True<listvalues /></designersetting>
      <designersetting key=""sortkey"">5<listvalues /></designersetting>True</booleanparameter>
  </ParameterCollection>
  <ParameterCollection id=""kenmerken"">
    <booleanparameter name=""showCalculatorButton"">
      <designersetting key=""label"">Rekenmachine<listvalues /></designersetting>
      <designersetting key=""description"">Geef aan of tijdens het beantwoorden van het item de kandidaat gebruik mag maken van de ingebouwde calculator.<listvalues /></designersetting>
      <designersetting key=""required"">False<listvalues /></designersetting>
      <designersetting key=""sortkey"">1<listvalues /></designersetting>
      <designersetting key=""group"">1 Hulpmiddelen<listvalues /></designersetting>
      <designersetting key=""visible"">False<listvalues /></designersetting>
      <designersetting key=""redirectEnabled"">True</designersetting>
      <designersetting key=""redirectToTargetControlId"">entireItem</designersetting>
      <designersetting key=""redirectToTargetParameterId"">showCalculatorButton</designersetting>
    </booleanparameter>
    <integerparameter name=""hightOfScrollText"">
      <designersetting key=""label"">CT - Hoogte van scrolltekst<listvalues /></designersetting>
      <designersetting key=""description"">Geef hier (voor CT) de hoogte van de scrolltekst op.<listvalues /></designersetting>
      <designersetting key=""required"">False<listvalues /></designersetting>
      <designersetting key=""sortkey"">30<listvalues /></designersetting>
      <designersetting key=""group"">1 Hulpmiddelen<listvalues /></designersetting>
      <designersetting key=""visible"">False<listvalues /></designersetting>
    </integerparameter>
    <integerparameter name=""fixedWidthMatrixColumn"">
      <designersetting key=""label"">CT - Kolombreedte<listvalues /></designersetting>
      <designersetting key=""description"">De breedte van de kolommen met alternatieven (in pixels) - alleen voor ExamenTester.<listvalues /></designersetting>
      <designersetting key=""defaultvalue"">100<listvalues /></designersetting>
      <designersetting key=""visible"">false<listvalues /></designersetting>
      <designersetting key=""group"">6 Matrix<listvalues /></designersetting>
      <designersetting key=""sortkey"">2<listvalues /></designersetting>100</integerparameter>
    <booleanparameter name=""showChoicesBottomLayout"">
      <designersetting key=""label"">Selectiebolletje onder uitlijnen<listvalues /></designersetting>
      <designersetting key=""description"">Selectiebolletje onder uitlijnen<listvalues /></designersetting>
      <designersetting key=""required"">False<listvalues /></designersetting>
      <designersetting key=""sortkey"">1<listvalues /></designersetting>
      <designersetting key=""group"">5 Layout<listvalues /></designersetting>
      <designersetting key=""visible"">False<listvalues /></designersetting>
      <designersetting key=""redirectEnabled"">True</designersetting>
      <designersetting key=""redirectToTargetControlId"">entireItem</designersetting>
      <designersetting key=""redirectToTargetParameterId"">horizontallyCenteredAlternatives</designersetting>
    </booleanparameter>
    <integerparameter name=""fixedHeightAlternatives"">
      <designersetting key=""label"">Vaste hoogte in pixels<listvalues /></designersetting>
      <designersetting key=""description"">Geef hier de vaste hoogte op voor de alternatieven.<listvalues /></designersetting>
      <designersetting key=""group"">5.1 Lay-outalternatieven<listvalues /></designersetting>
      <designersetting key=""defaultvalue"">0<listvalues /></designersetting>
      <designersetting key=""visible"">False<listvalues /></designersetting>
      <designersetting key=""sortkey"">35<listvalues /></designersetting>
      <designersetting key=""redirectEnabled"">True</designersetting>
      <designersetting key=""redirectToTargetControlId"">entireItem</designersetting>
      <designersetting key=""redirectToTargetParameterId"">fixedHeightAlternatives</designersetting>0</integerparameter>
    <booleanparameter name=""showGroup"">
      <designersetting key=""groupConditionalEnabled"">True<listvalues /></designersetting>
      <designersetting key=""groupConditionalEnabledSwitch"">1 Hulpmiddelen<listvalues /></designersetting>
      <designersetting key=""groupConditionalEnabledWhenValue"">True<listvalues /></designersetting>
      <designersetting key=""label"">Hulpmiddelen tonen<listvalues /></designersetting>
      <designersetting key=""description"">De hulpmiddelen kunnen worden ingevuld.<listvalues /></designersetting>
      <designersetting key=""group"">0 Hulpmiddelen<listvalues /></designersetting>
      <designersetting key=""defaultvalue"">false<listvalues /></designersetting>
      <designersetting key=""visible"">true<listvalues /></designersetting>
      <designersetting key=""required"">true<listvalues /></designersetting>
      <designersetting key=""sortkey"">0<listvalues /></designersetting>False</booleanparameter>
    <plaintextparameter name=""calculatorDescription"">
      <designersetting key=""conditionalEnabled"">True<listvalues /></designersetting>
      <designersetting key=""conditionalEnabledSwitchParameter"">showCalculatorButton<listvalues /></designersetting>
      <designersetting key=""conditionalEnabledWhenValue"">True<listvalues /></designersetting>
      <designersetting key=""label"">Beschrijving van de rekenmachine<listvalues /></designersetting>
      <designersetting key=""description"">Geef hier de beschrijving van de rekenmachine.<listvalues /></designersetting>
      <designersetting key=""group"">1 Hulpmiddelen<listvalues /></designersetting>
      <designersetting key=""defaultvalue"">
        <listvalues />
      </designersetting>
      <designersetting key=""visible"">False<listvalues /></designersetting>
      <designersetting key=""required"">False<listvalues /></designersetting>
      <designersetting key=""sortkey"">2<listvalues /></designersetting>
    </plaintextparameter>
    <listedparameter name=""calculatorMode"">
      <designersetting key=""conditionalEnabled"">True<listvalues /></designersetting>
      <designersetting key=""conditionalEnabledSwitchParameter"">showCalculatorButton<listvalues /></designersetting>
      <designersetting key=""conditionalEnabledWhenValue"">True<listvalues /></designersetting>
      <designersetting key=""label"">Mode van de rekenmachine<listvalues /></designersetting>
      <designersetting key=""description"">Geef aan in welke mode de rekenmachine getoond dient te worden.<listvalues /></designersetting>
      <designersetting key=""group"">1 Hulpmiddelen<listvalues /></designersetting>
      <designersetting key=""defaultvalue"">basic<listvalues /></designersetting>
      <designersetting key=""visible"">True<listvalues /></designersetting>
      <designersetting key=""required"">False<listvalues /></designersetting>
      <designersetting key=""sortkey"">3<listvalues /></designersetting>
      <designersetting key=""list"">
        <listvalues>
          <listvalue key=""minimal"">minimaal</listvalue>
          <listvalue key=""basic"">basis</listvalue>
          <listvalue key=""scientific"">wetenschappelijk</listvalue>
        </listvalues>
      </designersetting>basic</listedparameter>
    <booleanparameter name=""showReset"">
      <designersetting key=""conditionalEnabled"">False<listvalues /></designersetting>
      <designersetting key=""label"">Reset<listvalues /></designersetting>
      <designersetting key=""description"">Geef hier aan of het item gereset mag worden.<listvalues /></designersetting>
      <designersetting key=""group"">1 Hulpmiddelen<listvalues /></designersetting>
      <designersetting key=""defaultvalue"">
        <listvalues />
      </designersetting>
      <designersetting key=""visible"">True<listvalues /></designersetting>
      <designersetting key=""required"">False<listvalues /></designersetting>
      <designersetting key=""sortkey"">4<listvalues /></designersetting>
    </booleanparameter>
    <booleanparameter name=""showNotepad"">
      <designersetting key=""conditionalEnabled"">False<listvalues /></designersetting>
      <designersetting key=""label"">Notitieblok<listvalues /></designersetting>
      <designersetting key=""description"">Geef hier aan of het notitieblok gebruikt mag worden.<listvalues /></designersetting>
      <designersetting key=""group"">1 Hulpmiddelen<listvalues /></designersetting>
      <designersetting key=""defaultvalue"">
        <listvalues />
      </designersetting>
      <designersetting key=""visible"">True<listvalues /></designersetting>
      <designersetting key=""required"">False<listvalues /></designersetting>
      <designersetting key=""sortkey"">5<listvalues /></designersetting>
    </booleanparameter>
    <plaintextparameter name=""notepadDescription"">
      <designersetting key=""conditionalEnabled"">True<listvalues /></designersetting>
      <designersetting key=""conditionalEnabledSwitchParameter"">showNotepad<listvalues /></designersetting>
      <designersetting key=""conditionalEnabledWhenValue"">True<listvalues /></designersetting>
      <designersetting key=""label"">Beschrijving van de kladblok<listvalues /></designersetting>
      <designersetting key=""description"">Geef hier de beschrijving van het kladblok.<listvalues /></designersetting>
      <designersetting key=""group"">1 Hulpmiddelen<listvalues /></designersetting>
      <designersetting key=""defaultvalue"">
        <listvalues />
      </designersetting>
      <designersetting key=""visible"">False<listvalues /></designersetting>
      <designersetting key=""required"">False<listvalues /></designersetting>
      <designersetting key=""sortkey"">6<listvalues /></designersetting>
    </plaintextparameter>
    <booleanparameter name=""showSymbolPicker"">
      <designersetting key=""conditionalEnabled"">False<listvalues /></designersetting>
      <designersetting key=""label"">Symboolkiezer<listvalues /></designersetting>
      <designersetting key=""description"">Geef hier aan of de symboolkiezer gebruikt mag worden.<listvalues /></designersetting>
      <designersetting key=""group"">1 Hulpmiddelen<listvalues /></designersetting>
      <designersetting key=""defaultvalue"">
        <listvalues />
      </designersetting>
      <designersetting key=""visible"">True<listvalues /></designersetting>
      <designersetting key=""required"">False<listvalues /></designersetting>
      <designersetting key=""sortkey"">7<listvalues /></designersetting>
    </booleanparameter>
    <plaintextparameter name=""symbolPickerDescription"">
      <designersetting key=""conditionalEnabled"">True<listvalues /></designersetting>
      <designersetting key=""conditionalEnabledSwitchParameter"">showSymbolPicker<listvalues /></designersetting>
      <designersetting key=""conditionalEnabledWhenValue"">True<listvalues /></designersetting>
      <designersetting key=""label"">Beschrijving van de symboolkiezer<listvalues /></designersetting>
      <designersetting key=""description"">Geef hier de beschrijving van de symboolkiezer.<listvalues /></designersetting>
      <designersetting key=""group"">1 Hulpmiddelen<listvalues /></designersetting>
      <designersetting key=""defaultvalue"">
        <listvalues />
      </designersetting>
      <designersetting key=""visible"">False<listvalues /></designersetting>
      <designersetting key=""required"">False<listvalues /></designersetting>
      <designersetting key=""sortkey"">8<listvalues /></designersetting>
    </plaintextparameter>
    <plaintextparameter name=""symbols"">
      <designersetting key=""conditionalEnabled"">True<listvalues /></designersetting>
      <designersetting key=""conditionalEnabledSwitchParameter"">showSymbolPicker<listvalues /></designersetting>
      <designersetting key=""conditionalEnabledWhenValue"">True<listvalues /></designersetting>
      <designersetting key=""label"">Symbolen<listvalues /></designersetting>
      <designersetting key=""description"">Geef hier de symbolen aan waaruit gekozen kan worden.<listvalues /></designersetting>
      <designersetting key=""group"">1 Hulpmiddelen<listvalues /></designersetting>
      <designersetting key=""defaultvalue"">
        <listvalues />
      </designersetting>
      <designersetting key=""visible"">True<listvalues /></designersetting>
      <designersetting key=""required"">False<listvalues /></designersetting>
      <designersetting key=""sortkey"">9<listvalues /></designersetting>
    </plaintextparameter>
    <booleanparameter name=""showRuler"">
      <designersetting key=""conditionalEnabled"">False<listvalues /></designersetting>
      <designersetting key=""label"">Meetlat<listvalues /></designersetting>
      <designersetting key=""description"">Geef hier aan of de meetlat gebruikt mag worden.<listvalues /></designersetting>
      <designersetting key=""group"">1 Hulpmiddelen<listvalues /></designersetting>
      <designersetting key=""defaultvalue"">
        <listvalues />
      </designersetting>
      <designersetting key=""visible"">True<listvalues /></designersetting>
      <designersetting key=""required"">False<listvalues /></designersetting>
      <designersetting key=""sortkey"">10<listvalues /></designersetting>
    </booleanparameter>
    <plaintextparameter name=""rulerDescription"">
      <designersetting key=""conditionalEnabled"">True<listvalues /></designersetting>
      <designersetting key=""conditionalEnabledSwitchParameter"">showRuler<listvalues /></designersetting>
      <designersetting key=""conditionalEnabledWhenValue"">True<listvalues /></designersetting>
      <designersetting key=""label"">Beschrijving van de meetlat<listvalues /></designersetting>
      <designersetting key=""description"">Geef hier de beschrijving van de meetlat.<listvalues /></designersetting>
      <designersetting key=""group"">1 Hulpmiddelen<listvalues /></designersetting>
      <designersetting key=""defaultvalue"">
        <listvalues />
      </designersetting>
      <designersetting key=""visible"">False<listvalues /></designersetting>
      <designersetting key=""required"">False<listvalues /></designersetting>
      <designersetting key=""sortkey"">11<listvalues /></designersetting>
    </plaintextparameter>
    <plaintextparameter name=""rulerStartValue"">
      <designersetting key=""conditionalEnabled"">True<listvalues /></designersetting>
      <designersetting key=""conditionalEnabledSwitchParameter"">showRuler<listvalues /></designersetting>
      <designersetting key=""conditionalEnabledWhenValue"">True<listvalues /></designersetting>
      <designersetting key=""label"">Startwaarde van de meetlat<listvalues /></designersetting>
      <designersetting key=""description"">Geef hier de startwaarde aan van de meetlat.<listvalues /></designersetting>
      <designersetting key=""validationRegEx"">^([\d]{1,5}([\.\,][\d]{1,3})?|\s*)$<listvalues /></designersetting>
      <designersetting key=""validationRegExMessage"">Dit dient een numerieke waarde te zijn (formaat 00000.000) !<listvalues /></designersetting>
      <designersetting key=""group"">1 Hulpmiddelen<listvalues /></designersetting>
      <designersetting key=""defaultvalue"">
        <listvalues />
      </designersetting>
      <designersetting key=""visible"">True<listvalues /></designersetting>
      <designersetting key=""required"">False<listvalues /></designersetting>
      <designersetting key=""sortkey"">12<listvalues /></designersetting>
    </plaintextparameter>
    <plaintextparameter name=""rulerEndValue"">
      <designersetting key=""conditionalEnabled"">True<listvalues /></designersetting>
      <designersetting key=""conditionalEnabledSwitchParameter"">showRuler<listvalues /></designersetting>
      <designersetting key=""conditionalEnabledWhenValue"">True<listvalues /></designersetting>
      <designersetting key=""label"">Eindwaarde van de meetlat<listvalues /></designersetting>
      <designersetting key=""description"">Geef hier de eindwaarde aan van de meetlat.<listvalues /></designersetting>
      <designersetting key=""validationRegEx"">^([\d]{1,5}([\.\,][\d]{1,3})?|\s*)$<listvalues /></designersetting>
      <designersetting key=""validationRegExMessage"">Dit dient een numerieke waarde te zijn (formaat 00000.000) !<listvalues /></designersetting>
      <designersetting key=""group"">1 Hulpmiddelen<listvalues /></designersetting>
      <designersetting key=""defaultvalue"">
        <listvalues />
      </designersetting>
      <designersetting key=""visible"">True<listvalues /></designersetting>
      <designersetting key=""required"">False<listvalues /></designersetting>
      <designersetting key=""sortkey"">13<listvalues /></designersetting>
    </plaintextparameter>
    <plaintextparameter name=""rulerStepValue"">
      <designersetting key=""conditionalEnabled"">True<listvalues /></designersetting>
      <designersetting key=""conditionalEnabledSwitchParameter"">showRuler<listvalues /></designersetting>
      <designersetting key=""conditionalEnabledWhenValue"">True<listvalues /></designersetting>
      <designersetting key=""label"">Stapgrootte<listvalues /></designersetting>
      <designersetting key=""description"">Geef hier de stapgrootte aan van de meetlat.<listvalues /></designersetting>
      <designersetting key=""validationRegEx"">^([\d]{1,5}([\.\,][\d]{1,3})?|\s*)$<listvalues /></designersetting>
      <designersetting key=""validationRegExMessage"">Dit dient een numerieke waarde te zijn (formaat 00000.000) !<listvalues /></designersetting>
      <designersetting key=""group"">1 Hulpmiddelen<listvalues /></designersetting>
      <designersetting key=""defaultvalue"">
        <listvalues />
      </designersetting>
      <designersetting key=""visible"">True<listvalues /></designersetting>
      <designersetting key=""required"">False<listvalues /></designersetting>
      <designersetting key=""sortkey"">14<listvalues /></designersetting>
    </plaintextparameter>
    <integerparameter name=""rulerStart"">
      <designersetting key=""conditionalEnabled"">True<listvalues /></designersetting>
      <designersetting key=""conditionalEnabledSwitchParameter"">showRuler<listvalues /></designersetting>
      <designersetting key=""conditionalEnabledWhenValue"">True<listvalues /></designersetting>
      <designersetting key=""label"">Startwaarde van de meetlat<listvalues /></designersetting>
      <designersetting key=""description"">Geef hier de startwaarde aan van de meetlat.<listvalues /></designersetting>
      <designersetting key=""group"">1 Hulpmiddelen<listvalues /></designersetting>
      <designersetting key=""defaultvalue"">
        <listvalues />
      </designersetting>
      <designersetting key=""visible"">False<listvalues /></designersetting>
      <designersetting key=""required"">False<listvalues /></designersetting>
      <designersetting key=""sortkey"">15<listvalues /></designersetting>
    </integerparameter>
    <integerparameter name=""rulerEnd"">
      <designersetting key=""conditionalEnabled"">True<listvalues /></designersetting>
      <designersetting key=""conditionalEnabledSwitchParameter"">showRuler<listvalues /></designersetting>
      <designersetting key=""conditionalEnabledWhenValue"">True<listvalues /></designersetting>
      <designersetting key=""label"">Eindwaarde van de meetlat<listvalues /></designersetting>
      <designersetting key=""description"">Geef hier de eindwaarde aan van de meetlat.<listvalues /></designersetting>
      <designersetting key=""group"">1 Hulpmiddelen<listvalues /></designersetting>
      <designersetting key=""defaultvalue"">
        <listvalues />
      </designersetting>
      <designersetting key=""visible"">False<listvalues /></designersetting>
      <designersetting key=""required"">False<listvalues /></designersetting>
      <designersetting key=""sortkey"">16<listvalues /></designersetting>
    </integerparameter>
    <integerparameter name=""rulerStep"">
      <designersetting key=""conditionalEnabled"">True<listvalues /></designersetting>
      <designersetting key=""conditionalEnabledSwitchParameter"">showRuler<listvalues /></designersetting>
      <designersetting key=""conditionalEnabledWhenValue"">True<listvalues /></designersetting>
      <designersetting key=""label"">Stapgrootte<listvalues /></designersetting>
      <designersetting key=""description"">Geef hier de stapgrootte aan van de meetlat.<listvalues /></designersetting>
      <designersetting key=""group"">1 Hulpmiddelen<listvalues /></designersetting>
      <designersetting key=""defaultvalue"">
        <listvalues />
      </designersetting>
      <designersetting key=""visible"">False<listvalues /></designersetting>
      <designersetting key=""required"">False<listvalues /></designersetting>
      <designersetting key=""sortkey"">17<listvalues /></designersetting>
    </integerparameter>
    <integerparameter name=""rulerStepSize"">
      <designersetting key=""conditionalEnabled"">True<listvalues /></designersetting>
      <designersetting key=""conditionalEnabledSwitchParameter"">showRuler<listvalues /></designersetting>
      <designersetting key=""conditionalEnabledWhenValue"">True<listvalues /></designersetting>
      <designersetting key=""label"">Benodigde ruimte per stap (in px)<listvalues /></designersetting>
      <designersetting key=""description"">Geef hier de benodigde ruimte per stap aan (in pixels).<listvalues /></designersetting>
      <designersetting key=""group"">1 Hulpmiddelen<listvalues /></designersetting>
      <designersetting key=""defaultvalue"">
        <listvalues />
      </designersetting>
      <designersetting key=""visible"">True<listvalues /></designersetting>
      <designersetting key=""required"">False<listvalues /></designersetting>
      <designersetting key=""sortkey"">18<listvalues /></designersetting>
    </integerparameter>
    <listedparameter name=""rulerLengthUnit"">
      <designersetting key=""conditionalEnabled"">True<listvalues /></designersetting>
      <designersetting key=""conditionalEnabledSwitchParameter"">showRuler<listvalues /></designersetting>
      <designersetting key=""conditionalEnabledWhenValue"">True<listvalues /></designersetting>
      <designersetting key=""label"">Lengte-eenheid<listvalues /></designersetting>
      <designersetting key=""description"">Geef de eenheid van lengte aan.<listvalues /></designersetting>
      <designersetting key=""group"">1 Hulpmiddelen<listvalues /></designersetting>
      <designersetting key=""defaultvalue"">centimeter<listvalues /></designersetting>
      <designersetting key=""visible"">True<listvalues /></designersetting>
      <designersetting key=""required"">False<listvalues /></designersetting>
      <designersetting key=""sortkey"">19<listvalues /></designersetting>
      <designersetting key=""list"">
        <listvalues>
          <listvalue key=""millimeter"">millimeter</listvalue>
          <listvalue key=""centimeter"">centimeter</listvalue>
          <listvalue key=""meter"">meter</listvalue>
        </listvalues>
      </designersetting>centimeter</listedparameter>
    <booleanparameter name=""showProtractor"">
      <designersetting key=""conditionalEnabled"">False<listvalues /></designersetting>
      <designersetting key=""label"">Kompasroos<listvalues /></designersetting>
      <designersetting key=""description"">Geef hier aan of de kompasroos gebruikt mag worden.<listvalues /></designersetting>
      <designersetting key=""group"">1 Hulpmiddelen<listvalues /></designersetting>
      <designersetting key=""defaultvalue"">
        <listvalues />
      </designersetting>
      <designersetting key=""visible"">True<listvalues /></designersetting>
      <designersetting key=""required"">False<listvalues /></designersetting>
      <designersetting key=""sortkey"">20<listvalues /></designersetting>
    </booleanparameter>
    <plaintextparameter name=""protractorPickerDescription"">
      <designersetting key=""conditionalEnabled"">True<listvalues /></designersetting>
      <designersetting key=""conditionalEnabledSwitchParameter"">showProtractor<listvalues /></designersetting>
      <designersetting key=""conditionalEnabledWhenValue"">True<listvalues /></designersetting>
      <designersetting key=""label"">Beschrijving van de kompasroos<listvalues /></designersetting>
      <designersetting key=""description"">Geef hier de beschrijving van de kompasroos.<listvalues /></designersetting>
      <designersetting key=""group"">1 Hulpmiddelen<listvalues /></designersetting>
      <designersetting key=""defaultvalue"">
        <listvalues />
      </designersetting>
      <designersetting key=""visible"">False<listvalues /></designersetting>
      <designersetting key=""required"">False<listvalues /></designersetting>
      <designersetting key=""sortkey"">21<listvalues /></designersetting>
    </plaintextparameter>
    <booleanparameter name=""protractorEnableRotation"">
      <designersetting key=""conditionalEnabled"">True<listvalues /></designersetting>
      <designersetting key=""conditionalEnabledSwitchParameter"">showProtractor<listvalues /></designersetting>
      <designersetting key=""conditionalEnabledWhenValue"">True<listvalues /></designersetting>
      <designersetting key=""label"">Kompasroos roteerbaar<listvalues /></designersetting>
      <designersetting key=""description"">Geef hier aan of de kompasroos roteerbaar moet zijn.<listvalues /></designersetting>
      <designersetting key=""group"">1 Hulpmiddelen<listvalues /></designersetting>
      <designersetting key=""defaultvalue"">
        <listvalues />
      </designersetting>
      <designersetting key=""visible"">True<listvalues /></designersetting>
      <designersetting key=""required"">False<listvalues /></designersetting>
      <designersetting key=""sortkey"">22<listvalues /></designersetting>
    </booleanparameter>
    <booleanparameter name=""showTriangle"">
      <designersetting key=""conditionalEnabled"">False<listvalues /></designersetting>
      <designersetting key=""label"">GEOdriehoek<listvalues /></designersetting>
      <designersetting key=""description"">Geef hier aan of de GEOdriehoek gebruikt mag worden.<listvalues /></designersetting>
      <designersetting key=""group"">1 Hulpmiddelen<listvalues /></designersetting>
      <designersetting key=""defaultvalue"">
        <listvalues />
      </designersetting>
      <designersetting key=""visible"">True<listvalues /></designersetting>
      <designersetting key=""required"">False<listvalues /></designersetting>
      <designersetting key=""sortkey"">24<listvalues /></designersetting>
    </booleanparameter>
    <plaintextparameter name=""trianglePickerDescription"">
      <designersetting key=""conditionalEnabled"">True<listvalues /></designersetting>
      <designersetting key=""conditionalEnabledSwitchParameter"">showTriangle<listvalues /></designersetting>
      <designersetting key=""conditionalEnabledWhenValue"">True<listvalues /></designersetting>
      <designersetting key=""label"">Beschrijving van de GEOdriehoek<listvalues /></designersetting>
      <designersetting key=""description"">Geef hier de beschrijving van de GEOdriehoek.<listvalues /></designersetting>
      <designersetting key=""group"">1 Hulpmiddelen<listvalues /></designersetting>
      <designersetting key=""defaultvalue"">
        <listvalues />
      </designersetting>
      <designersetting key=""visible"">False<listvalues /></designersetting>
      <designersetting key=""required"">False<listvalues /></designersetting>
      <designersetting key=""sortkey"">25<listvalues /></designersetting>
    </plaintextparameter>
    <integerparameter name=""triangleMinDegrees"">
      <designersetting key=""conditionalEnabled"">True<listvalues /></designersetting>
      <designersetting key=""conditionalEnabledSwitchParameter"">showTriangle<listvalues /></designersetting>
      <designersetting key=""conditionalEnabledWhenValue"">True<listvalues /></designersetting>
      <designersetting key=""label"">Minimaal aantal graden<listvalues /></designersetting>
      <designersetting key=""description"">Geef hier het minimaal aantal graden aan van de GEOdriehoek.<listvalues /></designersetting>
      <designersetting key=""group"">1 Hulpmiddelen<listvalues /></designersetting>
      <designersetting key=""defaultvalue"">
        <listvalues />
      </designersetting>
      <designersetting key=""rangeFrom"">0<listvalues /></designersetting>
      <designersetting key=""rangeTo"">180<listvalues /></designersetting>
      <designersetting key=""visible"">False<listvalues /></designersetting>
      <designersetting key=""required"">False<listvalues /></designersetting>
      <designersetting key=""sortkey"">26<listvalues /></designersetting>
    </integerparameter>
    <integerparameter name=""triangleMaxDegrees"">
      <designersetting key=""conditionalEnabled"">True<listvalues /></designersetting>
      <designersetting key=""conditionalEnabledSwitchParameter"">showTriangle<listvalues /></designersetting>
      <designersetting key=""conditionalEnabledWhenValue"">True<listvalues /></designersetting>
      <designersetting key=""label"">Maximaal aantal graden<listvalues /></designersetting>
      <designersetting key=""description"">Geef hier het maximaal aantal graden aan van de GEOdriehoek.<listvalues /></designersetting>
      <designersetting key=""group"">1 Hulpmiddelen<listvalues /></designersetting>
      <designersetting key=""defaultvalue"">
        <listvalues />
      </designersetting>
      <designersetting key=""rangeFrom"">0<listvalues /></designersetting>
      <designersetting key=""rangeTo"">180<listvalues /></designersetting>
      <designersetting key=""visible"">False<listvalues /></designersetting>
      <designersetting key=""required"">False<listvalues /></designersetting>
      <designersetting key=""sortkey"">27<listvalues /></designersetting>
    </integerparameter>
    <booleanparameter name=""showSpellCheck"">
      <designersetting key=""conditionalEnabled"">False<listvalues /></designersetting>
      <designersetting key=""label"">Spellingscontrole<listvalues /></designersetting>
      <designersetting key=""description"">Geef hier aan of de spellingscontrole aan moet staan.<listvalues /></designersetting>
      <designersetting key=""group"">1 Hulpmiddelen<listvalues /></designersetting>
      <designersetting key=""defaultvalue"">False<listvalues /></designersetting>
      <designersetting key=""visible"">True<listvalues /></designersetting>
      <designersetting key=""required"">False<listvalues /></designersetting>
      <designersetting key=""sortkey"">28<listvalues /></designersetting>False</booleanparameter>
    <listedparameter name=""spellCheckCulture"">
      <designersetting key=""conditionalEnabled"">True<listvalues /></designersetting>
      <designersetting key=""conditionalEnabledSwitchParameter"">showSpellCheck<listvalues /></designersetting>
      <designersetting key=""conditionalEnabledWhenValue"">True<listvalues /></designersetting>
      <designersetting key=""label"">Taal<listvalues /></designersetting>
      <designersetting key=""description"">Geef aan wat de invoertaal is.<listvalues /></designersetting>
      <designersetting key=""group"">1 Hulpmiddelen<listvalues /></designersetting>
      <designersetting key=""defaultvalue"">nl-NL<listvalues /></designersetting>
      <designersetting key=""visible"">False<listvalues /></designersetting>
      <designersetting key=""required"">False<listvalues /></designersetting>
      <designersetting key=""sortkey"">29<listvalues /></designersetting>
      <designersetting key=""list"">
        <listvalues>
          <listvalue key=""nl-NL"">Nederlands</listvalue>
          <listvalue key=""en-US"">Engels (Verenigde Staten)</listvalue>
          <listvalue key=""en-UK"">Engels (Verenigd Koninkrijk)</listvalue>
          <listvalue key=""fr-FR"">Frans</listvalue>
        </listvalues>
      </designersetting>nl-NL</listedparameter>
    <booleanparameter name=""showFormulaList"">
      <designersetting key=""conditionalEnabled"">False<listvalues /></designersetting>
      <designersetting key=""label"">Toon formulekaart<listvalues /></designersetting>
      <designersetting key=""description"">Geef hier aan of de formulekaart beschikbaar moet zijn.<listvalues /></designersetting>
      <designersetting key=""group"">1 Hulpmiddelen<listvalues /></designersetting>
      <designersetting key=""defaultvalue"">False<listvalues /></designersetting>
      <designersetting key=""visible"">True<listvalues /></designersetting>
      <designersetting key=""required"">False<listvalues /></designersetting>
      <designersetting key=""sortkey"">30<listvalues /></designersetting>False</booleanparameter>
    <plaintextparameter name=""formulaListDescription"">
      <designersetting key=""conditionalEnabled"">True<listvalues /></designersetting>
      <designersetting key=""conditionalEnabledSwitchParameter"">showFormulaList<listvalues /></designersetting>
      <designersetting key=""conditionalEnabledWhenValue"">True<listvalues /></designersetting>
      <designersetting key=""label"">Beschrijving van de formulekaart<listvalues /></designersetting>
      <designersetting key=""description"">Geef hier de beschrijving van de formulekaart.<listvalues /></designersetting>
      <designersetting key=""group"">1 Hulpmiddelen<listvalues /></designersetting>
      <designersetting key=""defaultvalue"">
        <listvalues />
      </designersetting>
      <designersetting key=""visible"">False<listvalues /></designersetting>
      <designersetting key=""required"">False<listvalues /></designersetting>
      <designersetting key=""sortkey"">31<listvalues /></designersetting>
    </plaintextparameter>
    <listedparameter name=""formulaType"">
      <designersetting key=""conditionalEnabled"">True<listvalues /></designersetting>
      <designersetting key=""conditionalEnabledSwitchParameter"">showFormulaList<listvalues /></designersetting>
      <designersetting key=""conditionalEnabledWhenValue"">True<listvalues /></designersetting>
      <designersetting key=""label"">Formule<listvalues /></designersetting>
      <designersetting key=""description"">
        <listvalues />
      </designersetting>
      <designersetting key=""required"">True<listvalues /></designersetting>
      <designersetting key=""defaultvalue"">default<listvalues /></designersetting>
      <designersetting key=""group"">1 Hulpmiddelen<listvalues /></designersetting>
      <designersetting key=""sortkey"">32<listvalues /></designersetting>
      <designersetting key=""list"">
        <listvalues>
          <listvalue key=""default"">Standaard</listvalue>
          <listvalue key=""nask"">NASK</listvalue>
        </listvalues>
      </designersetting>default</listedparameter>
    <booleanparameter name=""showTextMarker"">
      <designersetting key=""conditionalEnabled"">False<listvalues /></designersetting>
      <designersetting key=""label"">Markeerstift<listvalues /></designersetting>
      <designersetting key=""description"">Geef hier aan of de markeerstift beschikbaar moet zijn.<listvalues /></designersetting>
      <designersetting key=""group"">1 Hulpmiddelen<listvalues /></designersetting>
      <designersetting key=""defaultvalue"">False<listvalues /></designersetting>
      <designersetting key=""visible"">True<listvalues /></designersetting>
      <designersetting key=""required"">False<listvalues /></designersetting>
      <designersetting key=""sortkey"">33<listvalues /></designersetting>False</booleanparameter>
    <plaintextparameter name=""textMarkerDescription"">
      <designersetting key=""conditionalEnabled"">True<listvalues /></designersetting>
      <designersetting key=""conditionalEnabledSwitchParameter"">showTextMarker<listvalues /></designersetting>
      <designersetting key=""conditionalEnabledWhenValue"">True<listvalues /></designersetting>
      <designersetting key=""label"">Omschrijving markeerstift<listvalues /></designersetting>
      <designersetting key=""description"">Omschrijving van de markeerstift<listvalues /></designersetting>
      <designersetting key=""visible"">False<listvalues /></designersetting>
      <designersetting key=""required"">False<listvalues /></designersetting>
      <designersetting key=""validationRegEx"">
        <listvalues />
      </designersetting>
      <designersetting key=""validationRegExMessage"">
        <listvalues />
      </designersetting>
      <designersetting key=""group"">1 Hulpmiddelen<listvalues /></designersetting>
      <designersetting key=""sortkey"">34<listvalues /></designersetting>
    </plaintextparameter>
    <listedparameter name=""foreignLanguage"">
      <designersetting key=""conditionalEnabled"">False<listvalues /></designersetting>
      <designersetting key=""label"">CT - Buitenlandse taal<listvalues /></designersetting>
      <designersetting key=""description"">Geef aan welke taal gebruikt moet worden voor de verklanking van buitenlandse teksten.<listvalues /></designersetting>
      <designersetting key=""group"">2 Verklanking<listvalues /></designersetting>
      <designersetting key=""defaultvalue"">0<listvalues /></designersetting>
      <designersetting key=""visible"">true<listvalues /></designersetting>
      <designersetting key=""sortkey"">99<listvalues /></designersetting>
      <designersetting key=""list"">
        <listvalues>
          <listvalue key=""0"">Nederlands</listvalue>
          <listvalue key=""1"">Duits</listvalue>
          <listvalue key=""2"">Engels</listvalue>
          <listvalue key=""3"">Frans</listvalue>
          <listvalue key=""4"">Italiaans</listvalue>
          <listvalue key=""5"">Turks</listvalue>
          <listvalue key=""6"">Engels (Brits)</listvalue>
        </listvalues>
      </designersetting>0</listedparameter>
    <listedparameter name=""specialItemLayout"">
      <designersetting key=""conditionalEnabled"">False<listvalues /></designersetting>
      <designersetting key=""label"">CT - Speciale opmaak van item<listvalues /></designersetting>
      <designersetting key=""description"">Geef aan welke speciale opmaak gebruikt moet worden.<listvalues /></designersetting>
      <designersetting key=""group"">1 Hulpmiddelen<listvalues /></designersetting>
      <designersetting key=""defaultvalue"">0<listvalues /></designersetting>
      <designersetting key=""visible"">true<listvalues /></designersetting>
      <designersetting key=""sortkey"">36<listvalues /></designersetting>
      <designersetting key=""list"">
        <listvalues>
          <listvalue key=""normal"">Normale opmaak</listvalue>
          <listvalue key=""instruction"">Instructie</listvalue>
          <listvalue key=""screen-end"">Laatste scherm</listvalue>
          <listvalue key=""screen-example"">Voorbeeldscherm</listvalue>
          <listvalue key=""screen-splash"">Splash-scherm</listvalue>
          <listvalue key=""screen-start"">Startscherm</listvalue>
          <listvalue key=""scrolltxt"">Scrollbare brontekst</listvalue>
        </listvalues>
      </designersetting>0</listedparameter>
    <integerparameter name=""fixedWidthAlternatives"">
      <designersetting key=""conditionalEnabled"">False<listvalues /></designersetting>
      <designersetting key=""label"">CT - Breedte van invoervakken<listvalues /></designersetting>
      <designersetting key=""description"">Geef hier de breedte op voor de invoervakken.<listvalues /></designersetting>
      <designersetting key=""group"">1 Hulpmiddelen<listvalues /></designersetting>
      <designersetting key=""defaultvalue"">100<listvalues /></designersetting>
      <designersetting key=""visible"">false<listvalues /></designersetting>
      <designersetting key=""sortkey"">37<listvalues /></designersetting>100</integerparameter>
    <listedparameter name=""inputFilter"">
      <designersetting key=""conditionalEnabled"">False<listvalues /></designersetting>
      <designersetting key=""label"">CT - Input voor invulvakje (tekst en nummer)<listvalues /></designersetting>
      <designersetting key=""description"">Geef aan welk masker voor het invoeren van gegevens gebruikt moet worden.<listvalues /></designersetting>
      <designersetting key=""group"">1 Hulpmiddelen<listvalues /></designersetting>
      <designersetting key=""defaultvalue"">
        <listvalues />
      </designersetting>
      <designersetting key=""visible"">false<listvalues /></designersetting>
      <designersetting key=""sortkey"">38<listvalues /></designersetting>
      <designersetting key=""list"">
        <listvalues>
          <listvalue key="""" />
          <listvalue key=""numeric-fraction"">Numeriek gebroken getal</listvalue>
          <listvalue key=""numeric-fraction-positive"">Numeriek positief gebroken getal</listvalue>
          <listvalue key=""numeric-integer"">Numeriek geheel getal</listvalue>
          <listvalue key=""text"">Tekst</listvalue>
          <listvalue key=""text-lowercase"">Tekst kleine letters</listvalue>
          <listvalue key=""text-lowercase-autochange"">Tekst kleine letters (hoofdletters automatisch omgezet)</listvalue>
          <listvalue key=""text-uppercase"">Tekst hoofdletters</listvalue>
          <listvalue key=""text-uppercase-autochange"">Tekst hoofdletters (kleine letters automatisch omgezet)</listvalue>
        </listvalues>
      </designersetting>
    </listedparameter>
  </ParameterCollection>
</ArrayOfParameterCollection>");

    }
}
