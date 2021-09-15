
Imports Cito.Tester.ContentModel
Imports System.Xml.Linq
Imports Questify.Builder.Logic.PluginExtensibility.Html.EditBehavior
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.UnitTests.Framework.FakeAppTemplate

<TestClass>
Public Class XHtmlEditBehavior_InlineBug

    <TestInitialize()>
    Public Sub Init()
        FakeDal.Init()
    End Sub

    <TestCleanup()>
    Public Sub DeInit()
        FakeDal.Deinit()
    End Sub
    
    <TestMethod(), TestCategory("UILogic"), TestCategory("EditBehavior")>
    Public Sub ReorderingTagsOfInlineIdImgShouldNotMatter()
        'Arrange
        Dim xhtmlparam As New XHtmlParameter() With {.Value = dataWith2InlinePrms.ToString()}
        FakeDal.AddInline() 'Make Inine possible
        Dim _itemResource As New ItemResourceEntity()
        Dim _behavior As New XHtmlParameterBehavior(_itemResource, FakeDal.GetFakeResourceManager(), 123, xhtmlparam)

        'Act
        _behavior.GetHtml()
        _behavior.SetHtml(htmlWithReorganizedInlineElements_has2Elements.ToString())
        _behavior.GetHtml()

        'Assert
        Assert.AreEqual(2, _behavior.InlineElements.Count)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("EditBehavior")>
    Public Sub MoveInlineElement()
        'Arrange
        Dim xhtmlparam As New XHtmlParameter() With {.Value = dataWith2InlinePrms.ToString()}
        FakeDal.AddInline() 'Make Inine possible
        Dim _itemResource As New ItemResourceEntity()
        Dim _behavior As New XHtmlParameterBehavior(_itemResource, FakeDal.GetFakeResourceManager(), 123, xhtmlparam)

        'Act
        _behavior.GetHtml()
        _behavior.SetHtml(htmlMovingStep1.ToString())
        _behavior.GetHtml()
        _behavior.SetHtml(htmlMovingStep2.ToString())

        'Assert
        Assert.AreEqual(2, _behavior.InlineElements.Count)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("EditBehavior")>
    Public Sub NumberOfInlineElementsWillRemain()
        'Arrange
        Dim xhtmlparam As New XHtmlParameter() With {.Value = dataWith2InlinePrms.ToString()}
        FakeDal.AddInline() 'Make Inine possible
        Dim _itemResource As New ItemResourceEntity()
        Dim _behavior As New XHtmlParameterBehavior(_itemResource, FakeDal.GetFakeResourceManager(), 123, xhtmlparam)

        'Act 
        _behavior.SetHtml(_behavior.GetHtml())
        _behavior.GetHtml()

        'Assert
        Assert.AreEqual(2, _behavior.InlineElements.Count)
    End Sub

#Region "Data"
    ReadOnly htmlWithReorganizedInlineElements_has2Elements As XElement = <html xmlns="http://www.w3.org/1999/xhtml" id="c1-id-1">
                                                                              <head id="c1-id-2">
                                                                                  <title id="c1-id-3">Document Title 3</title>
                                                                                  <style type="text/css" id="c1-id-4"> a[popuppar] {border-color: blue; border-style: dotted; border-width: 1px;} </style>
                                                                                  <style type="text/css" id="c1-id-5"/>
                                                                                  <meta http-equiv="Content-Type" content="text/html; charset=utf-8" id="c1-id-6"/>
                                                                              </head>
                                                                              <body style="padding: 2px; margin: 0; box-sizing: border-box; height: auto; width: 100%;" id="c1-id-7">
                                                                                  <p id="c1-id-8">Het getal 
		                                                                            <img id="I16746288-1b56-4c53-880d-2d54d060fba8" style="VERTICAL-ALIGN: middle" alt="" isinlinecontrol="true" isinlineelement="true"/>  is in 
		                                                                            <img isinlinecontrol="true" isinlineelement="true" style="VERTICAL-ALIGN: middle" alt="" id="Ibb37a53e-43d7-49e6-ab4a-fb967ecba6cc"/>het romijnse systeem :
                                                                                  </p>
                                                                              </body>
                                                                          </html>

    ReadOnly htmlMovingStep1 As XElement = <html xmlns="http://www.w3.org/1999/xhtml" id="c1-id-1">
                                               <head id="c1-id-2">
                                                   <title id="c1-id-3">Document Title 3</title>
                                                   <style type="text/css" id="c1-id-4"> a[popuppar] {border-color: blue; border-style: dotted; border-width: 1px;} </style>
                                                   <style type="text/css" id="c1-id-5"/>
                                                   <meta http-equiv="Content-Type" content="text/html; charset=utf-8" id="c1-id-6"/>
                                               </head>
                                               <body style="padding: 2px; margin: 0; box-sizing: border-box; height: auto; width: 100%;" id="c1-id-7">
                                                   <p id="c1-id-8">Het getal 
		<img id="I16746288-1b56-4c53-880d-2d54d060fba8" style="VERTICAL-ALIGN: middle" alt="" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAEQAAAAVCAIAAABwo9+3AAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsEAAA7BAbiRa+0AAAHMSURBVFhH3Zc5bgIxGEaREA0FZyAlVyACKiSKMByBpaHiCHSIilNAzQloUApKEDAsYQkKYosghHRBSCHPM6MZiOYAdl5h2f4pvif/tgbP9R8hZC6XSyqVCoVCwWDwQSkITGzCo2DJPGlarVYTampSrVaTmsZEyORyOcbz+Xw4HD6UgsDEthWETDqdZqT2pSDEthWETCaTYTwej58KQmxbwZFBkYK0iK66x953kdnv9+aPJOR0OnE3vm9gSY9xZ4jtLkNNQpDZbDa6rr/dMBgMVqsVJ/NXJpvNMu52u3cpIXGr1YpEIolEImnAJBqNNhoN7gyxbQVHZjabvUoJ59Dr9QqFgs/n8xh4vd58Pt9utykR20VmOBy+SMl4PF4ul/RbpVIJBAJ+v79UKnFitBklYrvLUJOT0Wi0WCy22225XC4Wi+v1mjNhk5K7TL/fpyAt3Pj5fE5TTadTeo+luU9sF5lut8uLITPkpusmkwkTa0vXiX0nY37YdDod7pn8kN6aGRDbVhAy5iPN82fVlYLYtoJzMrRjs9l8VgoCm0+zczL8uanX60wUhfAoMBEyfOTE4/FYLBYOhx+VgsDEJjwKlozJj4G1UIS7zNfrLxjDuZGfGRlKAAAAAElFTkSuQmCC" isinlinecontrol="true" isinlineelement="true"/>  is in 
		<img id="c1-id-9" style="VERTICAL-ALIGN: middle" alt="" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAEQAAAAVCAIAAABwo9+3AAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsEAAA7BAbiRa+0AAAHMSURBVFhH3Zc5bgIxGEaREA0FZyAlVyACKiSKMByBpaHiCHSIilNAzQloUApKEDAsYQkKYosghHRBSCHPM6MZiOYAdl5h2f4pvif/tgbP9R8hZC6XSyqVCoVCwWDwQSkITGzCo2DJPGlarVYTampSrVaTmsZEyORyOcbz+Xw4HD6UgsDEthWETDqdZqT2pSDEthWETCaTYTwej58KQmxbwZFBkYK0iK66x953kdnv9+aPJOR0OnE3vm9gSY9xZ4jtLkNNQpDZbDa6rr/dMBgMVqsVJ/NXJpvNMu52u3cpIXGr1YpEIolEImnAJBqNNhoN7gyxbQVHZjabvUoJ59Dr9QqFgs/n8xh4vd58Pt9utykR20VmOBy+SMl4PF4ul/RbpVIJBAJ+v79UKnFitBklYrvLUJOT0Wi0WCy22225XC4Wi+v1mjNhk5K7TL/fpyAt3Pj5fE5TTadTeo+luU9sF5lut8uLITPkpusmkwkTa0vXiX0nY37YdDod7pn8kN6aGRDbVhAy5iPN82fVlYLYtoJzMrRjs9l8VgoCm0+zczL8uanX60wUhfAoMBEyfOTE4/FYLBYOhx+VgsDEJjwKlozJj4G1UIS7zNfrLxjDuZGfGRlKAAAAAElFTkSuQmCC" isinlinecontrol="true" isinlineelement="true"/>het romijnse systeem : 
		<img id="Ibb37a53e-43d7-49e6-ab4a-fb967ecba6cc" style="VERTICAL-ALIGN: middle" alt="" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAEQAAAAVCAIAAABwo9+3AAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsEAAA7BAbiRa+0AAAHMSURBVFhH3Zc5bgIxGEaREA0FZyAlVyACKiSKMByBpaHiCHSIilNAzQloUApKEDAsYQkKYosghHRBSCHPM6MZiOYAdl5h2f4pvif/tgbP9R8hZC6XSyqVCoVCwWDwQSkITGzCo2DJPGlarVYTampSrVaTmsZEyORyOcbz+Xw4HD6UgsDEthWETDqdZqT2pSDEthWETCaTYTwej58KQmxbwZFBkYK0iK66x953kdnv9+aPJOR0OnE3vm9gSY9xZ4jtLkNNQpDZbDa6rr/dMBgMVqsVJ/NXJpvNMu52u3cpIXGr1YpEIolEImnAJBqNNhoN7gyxbQVHZjabvUoJ59Dr9QqFgs/n8xh4vd58Pt9utykR20VmOBy+SMl4PF4ul/RbpVIJBAJ+v79UKnFitBklYrvLUJOT0Wi0WCy22225XC4Wi+v1mjNhk5K7TL/fpyAt3Pj5fE5TTadTeo+luU9sF5lut8uLITPkpusmkwkTa0vXiX0nY37YdDod7pn8kN6aGRDbVhAy5iPN82fVlYLYtoJzMrRjs9l8VgoCm0+zczL8uanX60wUhfAoMBEyfOTE4/FYLBYOhx+VgsDEJjwKlozJj4G1UIS7zNfrLxjDuZGfGRlKAAAAAElFTkSuQmCC" isinlinecontrol="true" isinlineelement="true"/>
                                                   </p>
                                               </body>
                                           </html>

    ReadOnly htmlMovingStep2 As XElement = <html xmlns="http://www.w3.org/1999/xhtml" id="c1-id-1">
                                               <head id="c1-id-2">
                                                   <title id="c1-id-3">Document Title 3</title>
                                                   <style type="text/css" id="c1-id-4"> a[popuppar] {border-color: blue; border-style: dotted; border-width: 1px;} </style>
                                                   <style type="text/css" id="c1-id-5"/>
                                                   <meta http-equiv="Content-Type" content="text/html; charset=utf-8" id="c1-id-6"/>
                                               </head>
                                               <body style="padding: 2px; margin: 0; box-sizing: border-box; height: auto; width: 100%;" id="c1-id-7">
                                                   <p id="c1-id-8">Het getal 
		<img id="I16746288-1b56-4c53-880d-2d54d060fba8" style="VERTICAL-ALIGN: middle" alt="" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAEQAAAAVCAIAAABwo9+3AAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsEAAA7BAbiRa+0AAAHMSURBVFhH3Zc5bgIxGEaREA0FZyAlVyACKiSKMByBpaHiCHSIilNAzQloUApKEDAsYQkKYosghHRBSCHPM6MZiOYAdl5h2f4pvif/tgbP9R8hZC6XSyqVCoVCwWDwQSkITGzCo2DJPGlarVYTampSrVaTmsZEyORyOcbz+Xw4HD6UgsDEthWETDqdZqT2pSDEthWETCaTYTwej58KQmxbwZFBkYK0iK66x953kdnv9+aPJOR0OnE3vm9gSY9xZ4jtLkNNQpDZbDa6rr/dMBgMVqsVJ/NXJpvNMu52u3cpIXGr1YpEIolEImnAJBqNNhoN7gyxbQVHZjabvUoJ59Dr9QqFgs/n8xh4vd58Pt9utykR20VmOBy+SMl4PF4ul/RbpVIJBAJ+v79UKnFitBklYrvLUJOT0Wi0WCy22225XC4Wi+v1mjNhk5K7TL/fpyAt3Pj5fE5TTadTeo+luU9sF5lut8uLITPkpusmkwkTa0vXiX0nY37YdDod7pn8kN6aGRDbVhAy5iPN82fVlYLYtoJzMrRjs9l8VgoCm0+zczL8uanX60wUhfAoMBEyfOTE4/FYLBYOhx+VgsDEJjwKlozJj4G1UIS7zNfrLxjDuZGfGRlKAAAAAElFTkSuQmCC" isinlinecontrol="true" isinlineelement="true"/>  is in 
		<img id="c1-id-9" style="VERTICAL-ALIGN: middle" alt="" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAEQAAAAVCAIAAABwo9+3AAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsEAAA7BAbiRa+0AAAHMSURBVFhH3Zc5bgIxGEaREA0FZyAlVyACKiSKMByBpaHiCHSIilNAzQloUApKEDAsYQkKYosghHRBSCHPM6MZiOYAdl5h2f4pvif/tgbP9R8hZC6XSyqVCoVCwWDwQSkITGzCo2DJPGlarVYTampSrVaTmsZEyORyOcbz+Xw4HD6UgsDEthWETDqdZqT2pSDEthWETCaTYTwej58KQmxbwZFBkYK0iK66x953kdnv9+aPJOR0OnE3vm9gSY9xZ4jtLkNNQpDZbDa6rr/dMBgMVqsVJ/NXJpvNMu52u3cpIXGr1YpEIolEImnAJBqNNhoN7gyxbQVHZjabvUoJ59Dr9QqFgs/n8xh4vd58Pt9utykR20VmOBy+SMl4PF4ul/RbpVIJBAJ+v79UKnFitBklYrvLUJOT0Wi0WCy22225XC4Wi+v1mjNhk5K7TL/fpyAt3Pj5fE5TTadTeo+luU9sF5lut8uLITPkpusmkwkTa0vXiX0nY37YdDod7pn8kN6aGRDbVhAy5iPN82fVlYLYtoJzMrRjs9l8VgoCm0+zczL8uanX60wUhfAoMBEyfOTE4/FYLBYOhx+VgsDEJjwKlozJj4G1UIS7zNfrLxjDuZGfGRlKAAAAAElFTkSuQmCC" isinlinecontrol="true" isinlineelement="true"/>het romijnse systeem : 		
                                              </p>
                                               </body>
                                           </html>

    ReadOnly dataWith2InlinePrms As XElement = <p id="c1-id-8" xmlns="http://www.w3.org/1999/xhtml">
          Het getal <cito:InlineElement id="I16746288-1b56-4c53-880d-2d54d060fba8" layoutTemplateSourceName="tmp.inline.choice" xmlns:cito="http://www.cito.nl/citotester">
                                                       <cito:parameters>
                                                           <cito:parameterSet id="entireItem">
                                                               <cito:plaintextparameter name="controlType">choice</cito:plaintextparameter>
                                                               <cito:plaintextparameter name="inlineChoiceId">I16746288-1b56-4c53-880d-2d54d060fba8</cito:plaintextparameter>
                                                               <cito:plaintextparameter name="inlineChoiceLabel">Getal</cito:plaintextparameter>
                                                               <cito:inlineChoiceScoringparameter name="inlineChoiceScoring" label="Getal" ControllerId="inlineChoiceController" findingOverride="Opgave" minChoices="0" maxChoices="1">
                                                                   <cito:subparameterset id="A">
                                                                       <cito:plaintextparameter name="icOption">100</cito:plaintextparameter>
                                                                   </cito:subparameterset>
                                                                   <cito:subparameterset id="B">
                                                                       <cito:plaintextparameter name="icOption">200</cito:plaintextparameter>
                                                                   </cito:subparameterset>
                                                                   <cito:subparameterset id="C">
                                                                       <cito:plaintextparameter name="icOption">300</cito:plaintextparameter>
                                                                   </cito:subparameterset>
                                                                   <cito:definition id="">
                                                                       <cito:plaintextparameter name="icOption"/>
                                                                   </cito:definition>
                                                               </cito:inlineChoiceScoringparameter>
                                                           </cito:parameterSet>
                                                       </cito:parameters>
                                                   </cito:InlineElement>  is in het romijnse systeem : <cito:InlineElement id="Ibb37a53e-43d7-49e6-ab4a-fb967ecba6cc" layoutTemplateSourceName="tmp.inline.choice" xmlns:cito="http://www.cito.nl/citotester">
                                                       <cito:parameters>
                                                           <cito:parameterSet id="entireItem">
                                                               <cito:plaintextparameter name="controlType">choice</cito:plaintextparameter>
                                                               <cito:plaintextparameter name="inlineChoiceId">Ibb37a53e-43d7-49e6-ab4a-fb967ecba6cc</cito:plaintextparameter>
                                                               <cito:plaintextparameter name="inlineChoiceLabel">Roman</cito:plaintextparameter>
                                                               <cito:inlineChoiceScoringparameter name="inlineChoiceScoring" label="Roman" ControllerId="inlineChoiceController" findingOverride="Opgave" minChoices="0" maxChoices="1">
                                                                   <cito:subparameterset id="A">
                                                                       <cito:plaintextparameter name="icOption">C</cito:plaintextparameter>
                                                                   </cito:subparameterset>
                                                                   <cito:subparameterset id="B">
                                                                       <cito:plaintextparameter name="icOption">D</cito:plaintextparameter>
                                                                   </cito:subparameterset>
                                                                   <cito:subparameterset id="C">
                                                                       <cito:plaintextparameter name="icOption">CC</cito:plaintextparameter>
                                                                   </cito:subparameterset>
                                                                   <cito:subparameterset id="D">
                                                                       <cito:plaintextparameter name="icOption">DD</cito:plaintextparameter>
                                                                   </cito:subparameterset>
                                                                   <cito:definition id="">
                                                                       <cito:plaintextparameter name="icOption"/>
                                                                   </cito:definition>
                                                               </cito:inlineChoiceScoringparameter>
                                                           </cito:parameterSet>
                                                       </cito:parameters>
                                                   </cito:InlineElement>
                                               </p>

#End Region

End Class
