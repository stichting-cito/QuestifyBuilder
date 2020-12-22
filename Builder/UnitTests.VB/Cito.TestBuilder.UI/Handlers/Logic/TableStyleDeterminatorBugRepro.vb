
Imports System.Xml.Linq
Imports Questify.Builder.Logic.PluginExtensibility.Html.Handlers.Logic

<TestClass>
Public Class TableStyleDeterminatorBugRepro
    Inherits baseFakedTable

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table"), WorkItem(10223)>
    Public Sub ReadTableStyle_WithLineStyleNotDefined()
        Dim t = New Table(_table1.ToXmlNode())
        Dim determinator As New TableStyleDeterminator(t, New TableBounds(3, 5))

        Dim result = determinator.DetermineStyle()

        Assert.IsTrue(True)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table")>
    Public Sub DetermineCollapsedStyleOnActualTable()
        Dim t = New Table(_table1.ToXmlNode())

        Dim result = t.IsCollapsedStyle()

        Assert.IsTrue(result)
    End Sub

    <TestMethod(), TestCategory("UILogic"), TestCategory("Table"), WorkItem(10288)>
    Public Sub DetermineBorderBug()
        Dim t = New Table(_table2.ToXmlNode())
        Dim determinator As New TableStyleDeterminator(t, New TableBounds(0, 0))
        Dim dto = determinator.DetermineStyle()

        Dim rersult = dto.BoxAndInnerSameStyle()

        Assert.IsFalse(rersult)
    End Sub


    Private _table1 As XElement = <table style="FONT-SIZE: 13px; FONT-FAMILY: Microsoft Sans Serif; BORDER-COLLAPSE: collapse; WIDTH: 900px" width="900" xmlns="http://www.w3.org/1999/xhtml">
                                      <colgroup>
                                          <col width="150"/>
                                          <col width="50"/>
                                          <col width="300"/>
                                          <col width="150"/>
                                          <col width="150"/>
                                      </colgroup>
                                      <tbody>
                                          <tr style="BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid; BORDER-BOTTOM: black 1px solid; BORDER-LEFT: black 1px solid" valign="top">
                                              <td style="border-top:Black 1px Solid;border-right:Black 1px Solid;PADDING-BOTTOM: 1px;PADDING-TOP: 1px;PADDING-LEFT: 1px;border-left:Black 1px Solid;PADDING-RIGHT: 1px;border-bottom:Black 0px None;background-color:#E5E5E5;" valign="top" colspan="5">
                                                  <p>Plaats / gedraging / overtreding: (de voor het openbaar vervoer openstaande weg)</p>
                                              </td>
                                          </tr>
                                          <tr style="BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid; BORDER-BOTTOM: black 1px solid; BORDER-LEFT: black 1px solid" valign="top">
                                              <td style="PADDING-BOTTOM: 1px;PADDING-TOP: 1px;PADDING-LEFT: 1px;border-left:Black 1px Solid;PADDING-RIGHT: 1px;border-top:Black 0px None;border-right:Black 0px None;border-bottom:Black 0px None;background-color:#E5E5E5;" valign="top" colspan="3">
                                                  <p><img id="220dbc35-7c71-4d61-83bc-ef8dec80bf09" src="resource://package:1/Inlineessay.png" isinlineelement="true" style="vertical-align: middle;" alt=""/></p>
                                              </td>
                                              <td style="PADDING-BOTTOM: 1px;PADDING-TOP: 1px;PADDING-LEFT: 1px;PADDING-RIGHT: 1px;border-left:Black 0px None;border-top:Black 0px None;border-right:Black 0px None;border-bottom:Black 0px None;background-color:#E5E5E5;" valign="top" width="150">
                                                  <p>Wegcode<br/><img id="41e6baa1-db8f-45e8-97bb-9f1602303a8a" src="resource://package:1/Inlineinput.png" isinlineelement="true" style="vertical-align: middle;" alt=""/></p>
                                              </td>
                                              <td style="border-right:Black 1px Solid;PADDING-BOTTOM: 1px;PADDING-TOP: 1px;PADDING-LEFT: 1px;PADDING-RIGHT: 1px;border-left:Black 0px None;border-top:Black 0px None;border-bottom:Black 0px None;background-color:#E5E5E5;" valign="top" width="150">
                                                  <p>HM-Paal<br/><img id="7c27aea9-6808-4bd7-81a4-c9896a288de4" src="resource://package:1/Inlineinput.png" isinlineelement="true" style="vertical-align: middle;" alt=""/></p>
                                              </td>
                                          </tr>
                                          <tr style="BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid; BORDER-BOTTOM: black 1px solid; BORDER-LEFT: black 1px solid" valign="top">
                                              <td style="border-top:Black 0px None;border-right:Black 0px None;PADDING-BOTTOM: 1px;PADDING-TOP: 1px;PADDING-LEFT: 1px;border-left:Black 1px Solid;PADDING-RIGHT: 1px;border-bottom:Black 0px None;background-color:#E5E5E5;" valign="top" colspan="3">
                                                  <p>Te<br/><img id="28be2c78-40a7-4df1-a576-88852105f65d" src="resource://package:1/Inlineinput.png" isinlineelement="true" style="vertical-align: middle;" alt=""/></p>
                                              </td>
                                              <td style="border-top:Black 0px None;border-right:Black 1px Solid;border-bottom:Black 1px Solid;PADDING-BOTTOM: 3px;PADDING-TOP: 3px;PADDING-LEFT: 3px;border-left:Black 0px None;PADDING-RIGHT: 3px;background-color:#E5E5E5;" valign="top" width="150" colspan="2">
                                                  <p>Gemeente<br/><img id="31c2d156-3b6e-4678-952a-d861f3fb0d84" src="resource://package:1/Inlineinput.png" isinlineelement="true" style="vertical-align: middle;" alt=""/></p>
                                              </td>
                                          </tr>
                                          <tr style="BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid; BORDER-BOTTOM: black 1px solid; BORDER-LEFT: black 1px solid" valign="top">
                                              <td style="border-top:Black 1px Solid;border-right:Black 1px Solid;PADDING-BOTTOM: 1px;PADDING-TOP: 1px;PADDING-LEFT: 1px;border-left:Black 1px Solid;PADDING-RIGHT: 1px;border-bottom:Black 0px None;background-color:#E5E5E5;" valign="top" width="150">
                                                  <p>Soort voer-vaartuig<br/><img id="fe27aa7d-d17a-4308-998f-a2ef26b49b7f" src="resource://package:1/Inlineinput.png" isinlineelement="true" style="vertical-align: middle;" alt=""/></p>
                                              </td>
                                              <td style="border-top:Black 1px Solid;border-right:Black 1px Solid;border-bottom:Black 1px Solid;PADDING-BOTTOM: 3px;PADDING-TOP: 3px;PADDING-LEFT: 3px;border-left:Black 1px Solid;PADDING-RIGHT: 3px;background-color:#E5E5E5;" valign="top" width="50">
                                                  <p>Land<br/><img id="2a722e3f-b54b-4d3c-940c-104c6891848e" src="resource://package:1/Inlineinput.png" isinlineelement="true" style="vertical-align: middle;" alt=""/></p>
                                              </td>
                                              <td style="border-top:Black 1px Solid;border-right:Black 1px Solid;border-bottom:Black 1px Solid;PADDING-BOTTOM: 3px;PADDING-TOP: 3px;PADDING-LEFT: 3px;border-left:Black 1px Solid;PADDING-RIGHT: 3px;background-color:#E5E5E5;" valign="top" colspan="2">
                                                  <p>Kenteken/registratieteken<br/><img id="9dbfa222-1349-4a1d-9aeb-10fa50c3b82c" src="resource://package:1/Inlineinput.png" isinlineelement="true" style="vertical-align: middle;" alt=""/></p>
                                              </td>
                                              <td style="border-top:Black 1px Solid;border-right:Black 1px Solid;border-bottom:Black 1px Solid;PADDING-BOTTOM: 3px;PADDING-TOP: 3px;PADDING-LEFT: 3px;border-left:Black 1px Solid;PADDING-RIGHT: 3px;background-color:#E5E5E5;" valign="top">
                                                  <p>Dupl. code<br/><img id="13c9465b-cfd9-45eb-92d4-60b04e2d21e6" src="resource://package:1/Inlineinput.png" isinlineelement="true" style="vertical-align: middle;" alt=""/></p>
                                              </td>
                                          </tr>
                                          <tr style="BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid; BORDER-BOTTOM: black 1px solid; BORDER-LEFT: black 1px solid" valign="top">
                                              <td style="border-top:Black 1px Solid;border-right:Black 1px Solid;PADDING-BOTTOM: 1px;PADDING-TOP: 1px;PADDING-LEFT: 1px;border-left:Black 1px Solid;PADDING-RIGHT: 1px;border-bottom:Black 0px None;background-color:#E0E0E0;" valign="top" colspan="2">
                                                  <p>Merk<br/><img id="981dc6e0-5f26-415f-aa0c-6c0a4210fb1a" src="resource://package:1/Inlineinput.png" isinlineelement="true" style="vertical-align: middle;" alt=""/></p>
                                              </td>
                                              <td style="BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid; BORDER-BOTTOM: black 1px solid; PADDING-BOTTOM: 3px; PADDING-TOP: 3px; PADDING-LEFT: 3px; BORDER-LEFT: black 1px solid; PADDING-RIGHT: 3px" valign="top" width="300">
                                                  <p>Type<br/><img id="fc72ecc8-5e32-42bd-87f5-14bf18ba92de" src="resource://package:1/Inlineinput.png" isinlineelement="true" style="vertical-align: middle;" alt=""/></p>
                                              </td>
                                              <td style="BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid; BORDER-BOTTOM: black 1px solid; PADDING-BOTTOM: 3px; PADDING-TOP: 3px; PADDING-LEFT: 3px; BORDER-LEFT: black 1px solid; PADDING-RIGHT: 3px" valign="top" width="300" colspan="2">
                                                  <p>Kleur<br/><img id="11d06472-54e3-4eb2-b13e-ca1757a25fe8" src="resource://package:1/Inlineinput.png" isinlineelement="true" style="vertical-align: middle;" alt=""/></p>
                                              </td>
                                          </tr>
                                          <tr style="BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid; BORDER-BOTTOM: black 1px solid; BORDER-LEFT: black 1px solid" valign="top">
                                              <td style="border-top:Black 1px Solid;border-right:Black 0px ;PADDING-BOTTOM: 1px;PADDING-TOP: 1px;PADDING-LEFT: 1px;border-left:Black 1px Solid;PADDING-RIGHT: 1px;border-bottom:Black 0px ;background-color:#E5E5E5;" valign="top" colspan="3">
                                                  <p>Ik, ambtenaar, zag/hoorde dat op genoemde datum, tijdstip en plaats door<br/>betrokkene/verdachte-met het omschreven voertuig-/vaartuig-de volgende <br/>gedragingen/overtredingen werd verricht:</p>
                                              </td>
                                              <td style="border-top:Black 1px Solid;border-right:Black 0px ;border-bottom:Black 0px ;PADDING-BOTTOM: 3px;PADDING-TOP: 3px;PADDING-LEFT: 3px;PADDING-RIGHT: 3px;border-left:Black 0px ;background-color:#E5E5E5;" valign="top" width="150">
                                                  <p>Categorie<br/><img id="8f99dd4d-8924-49a6-8b86-332d5801a884" src="resource://package:1/Inlineinput.png" isinlineelement="true" style="vertical-align: middle;" alt=""/></p>
                                              </td>
                                              <td style="border-top:Black 1px Solid;border-right:Black 1px Solid;border-bottom:Black 0px ;PADDING-BOTTOM: 3px;PADDING-TOP: 3px;PADDING-LEFT: 3px;border-left:Black 0px ;PADDING-RIGHT: 3px;background-color:#E5E5E5;" valign="top" width="150">
                                                  <p>Nummer<br/><img id="bef3f21c-30c6-44d7-a151-eacee1562bb0" src="resource://package:1/Inlineinput.png" isinlineelement="true" style="vertical-align: middle;" alt=""/></p>
                                              </td>
                                          </tr>
                                          <tr style="BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid; BORDER-BOTTOM: black 1px solid; BORDER-LEFT: black 1px solid" valign="top">
                                              <td style="border-right:Black 1px Solid;PADDING-BOTTOM: 1px;PADDING-TOP: 1px;PADDING-LEFT: 1px;border-left:Black 1px Solid;PADDING-RIGHT: 1px;border-top:Black 0px None;border-bottom:Black 0px None;background-color:#E5E5E5;" valign="top" colspan="5">
                                                  <p><img id="059b5a7e-6972-40f4-b7d8-03f7ee3788ff" src="resource://package:1/Inlineessay.png" isinlineelement="true" style="vertical-align: middle;" alt=""/></p>
                                              </td>
                                          </tr>
                                      </tbody>
                                  </table>


    Private _table2 As XElement = <table style="BORDER-COLLAPSE: collapse; FONT-FAMILY: Microsoft Sans Serif; FONT-SIZE: 13" width="100%" xmlns="http://www.w3.org/1999/xhtml">
                                      <colgroup>
                                          <col/>
                                          <col/>
                                      </colgroup>
                                      <tbody>
                                          <tr>
                                              <td style="border-bottom:Black 1px Solid;border-left:Black 1px Solid;border-top:Black 1px Solid;background-color:#FF8080;">
                                                  <p></p>
                                              </td>
                                              <td style="border-bottom:Black 1px Solid;border-left:Black 5px Solid;border-top:Black 1px Solid;border-right:Black 1px Solid;background-color:#FF8080;">
                                                  <p></p>
                                              </td>
                                          </tr>
                                      </tbody>
                                  </table>


End Class
