
Imports System.Linq
Imports Questify.Builder.Logic.CustomInteractions
Imports System.Xml.Linq

<TestClass()>
Public Class PackageValidatorTest

    <TestMethod(), TestCategory("customInteractionProcessing")>
    Public Sub ReadCustomInteractionCoordinateAllMetadataTest()
        'Arrange
        Dim errorMessage As New List(Of String)
        
        'Act
        Dim metadata = CiPackageValidator.ReadCustomInteractionMetadata(Coordinatescomplete, errorMessage)
        
        'Assert
        Assert.AreEqual(0, errorMessage.Count)
        Assert.AreEqual("customInteraction coordinates", metadata.Title)
        Assert.AreEqual(200, metadata.Height)
        Dim scoringTypes = metadata.GetScoring
        Assert.AreEqual(2, scoringTypes.Count)
        Assert.AreEqual(2, scoringTypes.OfType(Of CoordinateScoring).Count)
        Assert.AreEqual("coordinate 1", scoringTypes.OfType(Of CoordinateScoring).FirstOrDefault.Label)
    End Sub

    <TestMethod(), TestCategory("customInteractionProcessing")>
    Public Sub ReadCustomInteractionCoordinateBasicMetadataTest()
        'Arrange
        Dim errorMessage As New List(Of String)
       
        'Act
        Dim metadata = CiPackageValidator.ReadCustomInteractionMetadata(CoordinateBasic, errorMessage)
        
        'Assert
        Assert.AreEqual(errorMessage.Count, 0)
        Dim scoringTypes = metadata.GetScoring
        Assert.AreEqual(scoringTypes.Count, 2)
        Assert.AreEqual(scoringTypes.OfType(Of CoordinateScoring).Count, 2)
        Assert.AreEqual(scoringTypes.OfType(Of CoordinateScoring).FirstOrDefault.Label, "coordinate 1")
    End Sub

    <TestMethod(), TestCategory("customInteractionProcessing")>
    Public Sub ReadCustomInteractionDecimalAllMetadataTest()
        'Arrange
        Dim errorMessage As New List(Of String)
        
        'Act
        Dim metadata = CiPackageValidator.ReadCustomInteractionMetadata(DecimalComplete, errorMessage)
        
        'Assert
        Assert.AreEqual(errorMessage.Count, 0)
        Assert.AreEqual("customInteraction coordinates", metadata.Title)
        Assert.AreEqual(200, metadata.Height)
        Dim scoringTypes = metadata.GetScoring
        Assert.AreEqual(4, scoringTypes.Count)
        Assert.AreEqual(4, scoringTypes.OfType(Of DecimalScoring).Count)
        Assert.AreEqual(2, scoringTypes.OfType(Of DecimalScoring).FirstOrDefault.MaxFactionPart)
    End Sub

    <TestMethod(), TestCategory("customInteractionProcessing")>
    Public Sub ReadCustomInteractionDecimalBasicMetadataTest()
        'Arrange
        Dim errorMessage As New List(Of String)
        
        'Act
        Dim metadata = CiPackageValidator.ReadCustomInteractionMetadata(DecimalMinimum, errorMessage)
       
        'Assert
        Assert.AreEqual(0, errorMessage.Count)
        Dim scoringTypes = metadata.GetScoring
        Assert.AreEqual(4, scoringTypes.Count)
        Assert.AreEqual(4, scoringTypes.OfType(Of DecimalScoring).Count)
    End Sub

    <TestMethod(), TestCategory("customInteractionProcessing")>
    Public Sub ReadCustomInteractionIntegerAllMetadataTest()
        'Arrange
        Dim errorMessage As New List(Of String)
       
        'Act
        Dim metadata = CiPackageValidator.ReadCustomInteractionMetadata(IntegerComplete, errorMessage)
      
        'Assert
        Assert.AreEqual(0, errorMessage.Count)
        Assert.AreEqual("customInteraction coordinates", metadata.Title)
        Assert.AreEqual(200, metadata.Height)
        Dim scoringTypes = metadata.GetScoring
        Assert.AreEqual(4, scoringTypes.Count)
        Assert.AreEqual(4, scoringTypes.OfType(Of IntegerScoring).Count)
        Assert.AreEqual(2, scoringTypes.OfType(Of IntegerScoring).FirstOrDefault.MaxLength)
    End Sub

    <TestMethod(), TestCategory("customInteractionProcessing")>
    Public Sub ReadCustomInteractionIntegerBasicMetadataTest()
        'Arrange
        Dim errorMessage As New List(Of String)
       
        'Act
        Dim metadata = CiPackageValidator.ReadCustomInteractionMetadata(IntegerMinimum, errorMessage)
       
        'Assert
        Assert.AreEqual(0, errorMessage.Count)
        Dim scoringTypes = metadata.GetScoring
        Assert.AreEqual(4, scoringTypes.Count)
        Assert.AreEqual(4, scoringTypes.OfType(Of IntegerScoring).Count)
    End Sub

    <TestMethod(), TestCategory("customInteractionProcessing")>
    Public Sub ReadCustomInteractionMathMlAllMetadataTest()
        'Arrange
        Dim errorMessage As New List(Of String)
       
        'Act
        Dim metadata = CiPackageValidator.ReadCustomInteractionMetadata(MathMlComplete, errorMessage)
       
        'Assert
        Assert.AreEqual(0, errorMessage.Count)
        Assert.AreEqual("customInteraction formula", metadata.Title)
        Assert.AreEqual(200, metadata.Height)
        Dim scoringTypes = metadata.GetScoring
        Assert.AreEqual(2, scoringTypes.Count)
        Assert.AreEqual(2, scoringTypes.OfType(Of MathMlScoring).Count)
        Assert.AreEqual("formule 1", scoringTypes.OfType(Of MathMlScoring).FirstOrDefault.Label)
    End Sub

    <TestMethod(), TestCategory("customInteractionProcessing")>
    Public Sub ReadCustomInteractionMathMlBasicMetadataTest()
        'Arrange
        Dim errorMessage As New List(Of String)
       
        'Act
        Dim metadata = CiPackageValidator.ReadCustomInteractionMetadata(MathMlMinimum, errorMessage)
      
        'Assert
        Assert.AreEqual(0, errorMessage.Count)
        Dim scoringTypes = metadata.GetScoring
        Assert.AreEqual(2, scoringTypes.Count)
        Assert.AreEqual(2, scoringTypes.OfType(Of MathMlScoring).Count)
    End Sub

    <TestMethod(), TestCategory("customInteractionProcessing")>
    Public Sub ReadCustomInteractionChoiceAllMetadataTest()
        'Arrange
        Dim errorMessage As New List(Of String)
      
        'Act
        Dim metadata = CiPackageValidator.ReadCustomInteractionMetadata(ChoiceComplete, errorMessage)
      
        'Assert
        Assert.AreEqual(0, errorMessage.Count)
        Assert.AreEqual("test customInteraction", metadata.Title)
        Assert.AreEqual(200, metadata.Height)
        Dim scoringTypes = metadata.GetScoring
        Assert.AreEqual(2, scoringTypes.Count)
        Assert.AreEqual(2, scoringTypes.OfType(Of ChoiceScoring).Count)
        Assert.AreEqual(4, scoringTypes.OfType(Of ChoiceScoring).ToList.Item(0).Choices.Count)
        Assert.AreEqual(4, scoringTypes.OfType(Of ChoiceScoring).ToList.Item(0).MaxChoices)
        Assert.AreEqual(5, scoringTypes.OfType(Of ChoiceScoring).ToList.Item(1).Choices.Count)
        Assert.AreEqual(5, scoringTypes.OfType(Of ChoiceScoring).ToList.Item(1).MaxChoices)
    End Sub

    <TestMethod(), TestCategory("customInteractionProcessing")>
    Public Sub ReadCustomInteractionCombinedMetadataTest()
        'Arrange
        Dim errorMessage As New List(Of String)
     
        'Act
        Dim metadata = CiPackageValidator.ReadCustomInteractionMetadata(Combined, errorMessage)
     
        'Assert
        Assert.AreEqual(0, errorMessage.Count)
        Assert.AreEqual("test customInteraction", metadata.Title)
        Assert.AreEqual(200, metadata.Height)
        Dim scoringTypes = metadata.GetScoring
        Assert.AreEqual(3, scoringTypes.Count)
        Assert.AreEqual(1, scoringTypes.OfType(Of ChoiceScoring).Count)
        Assert.AreEqual(2, scoringTypes.OfType(Of IntegerScoring).Count)
        Assert.AreEqual(4, scoringTypes.OfType(Of ChoiceScoring).ToList.Item(0).Choices.Count)
        Assert.AreEqual(4, scoringTypes.OfType(Of ChoiceScoring).ToList.Item(0).MaxChoices)
        Assert.AreEqual(2, scoringTypes.OfType(Of IntegerScoring).ToList.Item(0).MaxLength)
        Assert.AreEqual("coordinate 1 - x", scoringTypes.OfType(Of IntegerScoring).ToList.Item(0).Label)
        Assert.AreEqual("coordinate 1 - y", scoringTypes.OfType(Of IntegerScoring).ToList.Item(1).Label)
    End Sub

    <TestMethod(), TestCategory("customInteractionProcessing")>
    Public Sub MaxChoiceGreaterThanChoiceCollectionTest()
        'Arrange
        Dim errorMessage As New List(Of String)
     
        'Act
        CiPackageValidator.ReadCustomInteractionMetadata(MaxChoiceToHigh, errorMessage)
     
        'Assert
        Assert.AreEqual(1, errorMessage.Count)
    End Sub

    <TestMethod(), TestCategory("customInteractionProcessing")>
    Public Sub MinChoiceGreaterThanChoiceCollectionTest()
        'Arrange
        Dim errorMessage As New List(Of String)
     
        'Act
        CiPackageValidator.ReadCustomInteractionMetadata(MinChoiceToHigh, errorMessage)
     
        'Assert
        Assert.AreEqual(2, errorMessage.Count)
    End Sub

    <TestMethod(), TestCategory("customInteractionProcessing")>
    Public Sub SolutionNotInChoiceCollectionTest()
        'Arrange
        Dim errorMessage As New List(Of String)
      
        'Act
        CiPackageValidator.ReadCustomInteractionMetadata(CorrectResponseNotInChoices, errorMessage)
     
        'Assert
        Assert.AreEqual(1, errorMessage.Count)
    End Sub

    <TestMethod(), TestCategory("customInteractionProcessing")>
    Public Sub NoScoreTest()
        'Arrange
        Dim errorMessage As New List(Of String)
      
        'Act
        CiPackageValidator.ReadCustomInteractionMetadata(NoScore, errorMessage)
      
        'Assert
        Assert.AreEqual(1, errorMessage.Count)
    End Sub

    <TestMethod(), TestCategory("customInteractionProcessing")>
    Public Sub CoordinateWrongCultureTest()
        'Arrange
        Dim errorMessage As New List(Of String)
      
        'Act
        CiPackageValidator.ReadCustomInteractionMetadata(CoordinateWrongCultureCorrectResponse, errorMessage)
     
        'Assert 
        Assert.AreEqual(2, errorMessage.Count)
    End Sub

    <TestMethod(), TestCategory("customInteractionProcessing")>
    Public Sub DecimalCultureWrongTest()
        'Arrange
        Dim errorMessage As New List(Of String)
     
        'Act
        CiPackageValidator.ReadCustomInteractionMetadata(DecimalCultureWrong, errorMessage)
     
        'Assert
        Assert.AreEqual(1, errorMessage.Count)
    End Sub

    <TestMethod(), TestCategory("customInteractionProcessing")>
    Public Sub IntegerCultureWrongTest()
        'Arrange
        Dim errorMessage As New List(Of String)
      
        'Act
        CiPackageValidator.ReadCustomInteractionMetadata(IntegerCorrectAnswerWrong, errorMessage)
     
        'Assert
        Assert.AreEqual(1, errorMessage.Count)
    End Sub

    <TestMethod(), TestCategory("customInteractionProcessing")>
    Public Sub ReadGeogebraAllMetadataTest()
        'Arrange
        Dim errorMessage As New List(Of String)
     
        'Act
        Dim metadata = GeogebraPackageValidator.ReadCustomInteractionMetadata(GetGeogebraNodeList(), errorMessage)
     
        'Assert
        Assert.AreEqual(0, errorMessage.Count)
        Dim scoringTypes = metadata.GetScoring
        Assert.AreEqual(2, scoringTypes.Count)
        Assert.AreEqual(2, scoringTypes.OfType(Of GeogebraScoring).Count)
        Assert.AreEqual("geoA", scoringTypes.OfType(Of GeogebraScoring).FirstOrDefault.Label)
    End Sub

    Private Const Coordinatescomplete As String = "{""title"":""customInteraction coordinates"",""width"":400,""height"":200,""scalable"":false,""scoring"":[{""coordinate"":{""label"":""coordinate 1"",""correctResponse"":""(x:5.2)(y:5.2)""}},{""coordinate"":{""label"":""coordinate 2"",""correctResponse"":""(x:10.1)(y:10.1)""}}]}"
    Private Const CoordinateBasic As String = "{""scoring"":[{""coordinate"":{""label"":""coordinate 1""}},{""coordinate"":{""label"":""coordinate 2""}}]}"
    Private Const DecimalComplete As String = "{""title"":""customInteraction coordinates"",""width"":400,""height"":200,""scalable"":false,""scoring"":[{""decimal"":{""label"":""coordinate 1 - x"",""maxIntegerPart"":2,""maxFactionPart"":2,""correctResponse"":""5.1""}},{""decimal"":{""label"":""coordinate 1 - y"",""maxIntegerPart"":2,""maxFactionPart"":2,""correctResponse"":""5.1""}},{""decimal"":{""label"":""coordinate 2 - x"",""maxIntegerPart"":2,""maxFactionPart"":2,""correctResponse"":""8.5""}},{""decimal"":{""label"":""coordinate 2 - y"",""maxIntegerPart"":2,""maxFactionPart"":2,""correctResponse"":""10.5""}}]}"
    Private Const DecimalMinimum As String = "{""scoring"":[{""decimal"":{}},{""decimal"":{}},{""decimal"":{}},{""decimal"":{}}]}"
    Private Const IntegerComplete As String = "{""title"":""customInteraction coordinates"",""width"":400,""height"":200,""scalable"":false,""scoring"":[{""integer"":{""label"":""coordinate 1 - x"",""maxLength"":2,""correctResponse"":""5""}},{""integer"":{""label"":""coordinate 1 - y"",""maxLength"":2,""correctResponse"":""5""}},{""integer"":{""label"":""coordinate 2 - x"",""maxLength"":2,""correctResponse"":""8""}},{""integer"":{""label"":""coordinate 2 - y"",""maxLength"":2,""correctResponse"":""10""}}]}"
    Private Const IntegerMinimum As String = "{""scoring"":[{""integer"":{}},{""integer"":{}},{""integer"":{}},{""integer"":{}}]}"
    Private Const MathMlComplete As String = "{""title"":""customInteraction formula"",""width"":400,""height"":200,""scalable"":false,""scoring"":[{""mathml"":{""label"":""formule 1"",""correctResponse"":""<math xmlns='http://www.w3.org/1998/Math/MathML'><apply><in/><cn type='complex-cartesian'>17<sep/>29</cn><complexes/></apply></math>""}},{""mathml"":{""label"":""formule 2"",""correctResponse"":""<math xmlns='http://www.w3.org/1998/Math/MathML'><infinity/></math>""}}]}"
    Private Const MathMlMinimum As String = "{""scoring"":[{""mathml"":{}},{""mathml"":{}}]}"
    Private Const ChoiceComplete As String = "{""title"":""test customInteraction"",""width"":400,""height"":200,""scalable"":false,""scoring"":[{""choice"":{""minChoices"":1,""maxChoices"":4,""correctResponse"":""C"",""choices"":[{""id"":""A"",""label"":""Picture 1 selected""},{""id"":""B"",""label"":""Picture 2 selected""},{""id"":""C"",""label"":""Picture 3 selected""},{""id"":""D"",""label"":""Picture 4 selected""}]}},{""choice"":{""minChoices"":1,""maxChoices"":5,""correctResponse"":""A"",""choices"":[{""id"":""A"",""label"":""Picture 1 selected""},{""id"":""B"",""label"":""Picture 2 selected""},{""id"":""C"",""label"":""Picture 3 selected""},{""id"":""D"",""label"":""Picture 4 selected""},{""id"":""E"",""label"":""Picture 5 selected""}]}}]}"
    Private Const Combined As String = "{""title"":""test customInteraction"",""width"":400,""height"":200,""scalable"":false,""scoring"":[{""choice"":{""minChoices"":1,""maxChoices"":4,""choices"":[{""id"":""A""},{""id"":""B""},{""id"":""C""},{""id"":""D""}]}},{""integer"":{""label"":""coordinate 1 - x"",""maxLength"":2,""correctResponse"":""5""}},{""integer"":{""label"":""coordinate 1 - y"",""maxLength"":2,""correctResponse"":""5""}}]}"
    Private Const MaxChoiceToHigh As String = "{""title"":""test customInteraction"",""width"":400,""height"":200,""scalable"":false,""scoring"":[{""choice"":{""minChoices"":1,""maxChoices"":5,""choices"":[{""id"":""A""},{""id"":""B""},{""id"":""C""},{""id"":""D""}]}},{""integer"":{""label"":""coordinate 1 - x"",""maxLength"":2,""correctResponse"":""5""}},{""integer"":{""label"":""coordinate 1 - y"",""maxLength"":2,""correctResponse"":""5""}}]}"
    Private Const MinChoiceToHigh As String = "{""title"":""test customInteraction"",""width"":400,""height"":200,""scalable"":false,""scoring"":[{""choice"":{""minChoices"":5,""maxChoices"":4,""choices"":[{""id"":""A""},{""id"":""B""},{""id"":""C""},{""id"":""D""}]}},{""integer"":{""label"":""coordinate 1 - x"",""maxLength"":2,""correctResponse"":""5""}},{""integer"":{""label"":""coordinate 1 - y"",""maxLength"":2,""correctResponse"":""5""}}]}"
    Private Const CorrectResponseNotInChoices As String = "{""title"":""test customInteraction"",""width"":400,""height"":200,""scalable"":false,""scoring"":[{""choice"":{""minChoices"":1,""maxChoices"":4,""correctResponse"":""E"",""choices"":[{""id"":""A""},{""id"":""B""},{""id"":""C""},{""id"":""D""}]}}]}"
    Private Const NoScore As String = "{""title"":""test customInteraction"",""width"":400,""height"":200,""scalable"":false,""scoring"":[{}]}"
    Private Const CoordinateWrongCultureCorrectResponse As String = "{""title"":""customInteraction coordinates"",""width"":400,""height"":200,""scalable"":false,""scoring"":[{""coordinate"":{""label"":""coordinate 1"",""correctResponse"":""(x:5.000.000,00)(y:6.000.000,00)""}},{""coordinate"":{""label"":""coordinate 2"",""correctResponse"":""(x:19.000.000,00)(y:20.000.000,00)""}}]}"
    Private Const DecimalCultureWrong = "{""scoring"":[{""decimal"":{""correctResponse"":""5.000.000,00""}}]}"
    Private Const IntegerCorrectAnswerWrong = "{""scoring"":[{""integer"":{""correctResponse"":""bla""}}]}"

    Private Function GetGeogebraNodeList() As IEnumerable(Of XElement)
        Return GeogebraElement.Descendants("expression").Where(Function(n) n.Attributes.Any(Function(a) a.Name.ToString.Equals("label") AndAlso a.Value.StartsWith("geo")))
    End Function

    Private GeogebraElement As XElement = <geogebra format="4.2" version="4.2.51.0" id="664cc608-d709-4f58-8112-9384766fd6e3" xsi:noNamespaceSchemaLocation="http://www.geogebra.org/ggb.xsd" xmlns="" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
                                              <gui>
                                                  <window width="1296" height="786"/>
                                                  <perspectives>
                                                      <perspective id="tmp">
                                                          <panes>
                                                              <pane location="" divider="0.10185185185185185" orientation="1"/>
                                                          </panes>
                                                          <views>
                                                              <view id="4097" visible="false" inframe="true" stylebar="true" location="1,1,1" size="400" window="264,65,735,640"/>
                                                              <view id="4" toolbar="0 59 || 2020 , 2021 , 2022 , 66 || 2001 , 2003 , 2002 , 2004 , 2005 || 2040 , 2041 , 2042 , 2044 , 2043" visible="false" inframe="false" stylebar="false" location="1,1" size="300" window="100,100,600,400"/>
                                                              <view id="8" toolbar="1001 | 1002 | 1003  || 1005 | 1004 || 1006 | 1007 | 1010 | 1011 || 1008 1009 || 6" visible="false" inframe="false" stylebar="false" location="1,3" size="300" window="100,100,600,400"/>
                                                              <view id="1" visible="true" inframe="false" stylebar="true" location="1" size="1269" window="100,100,600,400"/>
                                                              <view id="2" visible="false" inframe="false" stylebar="false" location="3" size="132" window="100,100,250,400"/>
                                                              <view id="16" visible="false" inframe="false" stylebar="false" location="1" size="150" window="50,50,500,500"/>
                                                              <view id="32" visible="false" inframe="false" stylebar="true" location="1" size="150" window="50,50,500,500"/>
                                                              <view id="64" visible="false" inframe="true" stylebar="true" location="1" size="150" window="50,50,500,500"/>
                                                              <view id="70" toolbar="0 || 2020 || 2021 || 2022" visible="false" inframe="true" stylebar="true" location="1" size="150" window="50,50,500,500"/>
                                                          </views>
                                                          <toolbar show="true" items="0 39 59 | 1 501 67 , 5 19 , 72 | 2 15 45 , 18 65 , 7 37 | 4 3 8 9 , 13 44 , 58 , 47 | 16 51 64 , 70 | 10 34 53 11 , 24  20 22 , 21 23 | 55 56 57 , 12 | 36 46 , 38 49 50 , 71 | 30 29 54 32 31 33 | 17 26 62 73 , 14 66 68 | 25 52 60 61 | 40 41 42 , 27 28 35 , 6" position="1" help="false"/>
                                                          <input show="false" cmd="true" top="false"/>
                                                          <dockBar show="true" east="true"/>
                                                      </perspective>
                                                  </perspectives>
                                                  <labelingStyle val="3"/>
                                                  <font size="20"/>
                                                  <graphicsSettings javaLatexFonts="false"/>
                                              </gui>
                                              <euclidianView>
                                                  <size width="1269" height="644"/>
                                                  <coordSystem xZero="85.99999999999999" yZero="505.9999999999994" scale="49.999999999999595" yscale="49.99999999999995"/>
                                                  <evSettings axes="true" grid="true" gridIsBold="false" pointCapturing="3" rightAngleStyle="2" checkboxSize="13" gridType="0"/>
                                                  <bgColor r="255" g="255" b="255"/>
                                                  <axesColor r="0" g="0" b="0"/>
                                                  <gridColor r="192" g="192" b="192"/>
                                                  <lineStyle axes="3" grid="0"/>
                                                  <axis id="0" show="true" label="" unitLabel="" tickStyle="1" showNumbers="true"/>
                                                  <axis id="1" show="true" label="" unitLabel="" tickStyle="1" showNumbers="true"/>
                                              </euclidianView>
                                              <kernel>
                                                  <continuous val="false"/>
                                                  <usePathAndRegionParameters val="true"/>
                                                  <decimals val="2"/>
                                                  <angleUnit val="degree"/>
                                                  <algebraStyle val="0"/>
                                                  <coordStyle val="0"/>
                                                  <angleFromInvTrig val="false"/>
                                              </kernel>
                                              <scripting blocked="false" disabled="false"/>
                                              <construction title="M1 WI H03 Schatzoeken" author="" date="26 August 2013">
                                                  <command name="Segment">
                                                      <input a0="A" a1="B"/>
                                                      <output a0="b"/>
                                                  </command>
                                                  <element type="segment" label="b">
                                                      <show object="true" label="false"/>
                                                      <objColor r="153" g="51" b="0" alpha="0.0"/>
                                                      <layer val="0"/>
                                                      <labelMode val="0"/>
                                                      <coords x="-0.52" y="-0.4800000000000004" z="7.16"/>
                                                      <lineStyle thickness="13" type="0" typeHidden="1"/>
                                                      <outlyingIntersections val="false"/>
                                                      <keepTypeOnTransform val="true"/>
                                                  </element>
                                                  <expression label="geoA" exp="Sleutel 1"/>
                                                  <element type="point" label="P">
                                                      <show object="true" label="false"/>
                                                      <condition showObject="Distance[P, M] &lt; 0.2"/>
                                                      <objColor r="0" g="0" b="255" alpha="0.0"/>
                                                      <layer val="0"/>
                                                      <labelMode val="0"/>
                                                      <coords x="10.0" y="4.0" z="1.0"/>
                                                      <pointSize val="7"/>
                                                      <pointStyle val="1"/>
                                                  </element>
                                                  <command name="Point">
                                                      <input a0="a"/>
                                                      <output a0="M"/>
                                                  </command>
                                                  <expression label="geoB" exp="Sleutel 2"/>
                                                  <element type="text" label="Text1_1">
                                                      <show object="true" label="false"/>
                                                      <objColor r="255" g="0" b="0" alpha="0.0"/>
                                                      <layer val="0"/>
                                                      <labelMode val="0"/>
                                                      <fixed val="true"/>
                                                      <font serif="false" sizeM="1.0" size="0" style="1"/>
                                                      <startPoint x="1.0800000000000143" y="-1.0600000000000191" z="1.0"/>
                                                  </element>
                                                  <element type="image" label="afbeelding1">
                                                      <file name="3e82102a20163b92fc66fdbcd871e3cc\wit papier.jpg"/>
                                                      <inBackground val="false"/>
                                                      <startPoint number="0" x="-2.1600000000000135" y="-4.120000000000017" z="1.0"/>
                                                      <show object="true" label="false"/>
                                                      <objColor r="0" g="0" b="0" alpha="1.0"/>
                                                      <layer val="3"/>
                                                      <labelMode val="0"/>
                                                      <fixed val="true"/>
                                                  </element>
                                              </construction>
                                          </geogebra>

End Class
