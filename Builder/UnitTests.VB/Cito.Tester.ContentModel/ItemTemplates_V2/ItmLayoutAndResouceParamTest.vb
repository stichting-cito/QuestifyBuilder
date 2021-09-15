
Option Infer On
Imports System.Text
Imports System.Xml.Linq
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports FakeItEasy

<TestClass()>
Public Class ItmLayoutAndResouceParamTest

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub LoadSimple_ILT_WithSimpleControlTemplateAndXhtmlParameter_ExpectsIltLoadedAndXhtmlParamFilled()
        'Arrange

        Dim control = <?xml version="1.0" encoding="utf-8"?>
                      <Template xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" definitionVersion="2">
                          <Description></Description>
                          <Targets>
                              <Target xsi:type="ControlTemplateTarget" enabled="true" name="ces">
                                  <Template><![CDATA[	]]></Template>
                              </Target>
                          </Targets>
                          <SharedFunctions/>
                          <SharedParameterSet>
                              <xhtmlresourceparameter name="itemBody">
                                  <designersetting key="label">Informatie</designersetting>
                                  <designersetting key="required">false</designersetting>
                              </xhtmlresourceparameter>
                          </SharedParameterSet>
                      </Template>

        Dim handler As EventHandler(Of ResourceNeededEventArgs) = a.Fake(Of EventHandler(Of ResourceNeededEventArgs))()
        A.CallTo(Sub() handler.Invoke(A(Of Object).Ignored, A(Of ResourceNeededEventArgs).Ignored)).
            Invokes(Sub(i)
                        'This is the Resource Needed Handler.
                        Dim e = i.GetArgument(Of ResourceNeededEventArgs)(1)
                        If e.ResourceName = "SecretDoc.txt" Then
                            e.BinaryResource = New BinaryResource(e.ResourceName, Nothing, Encoding.UTF8.GetBytes(txt.ToString()), Nothing)
                        Else
                            e.BinaryResource = New BinaryResource(e.ResourceName, Nothing, If(e.ResourceName = "ItemLayout", ilt.ToString(), control.ToString()), Nothing)
                        End If
                    End Sub)


        'Act if merging has taken place. Normally when an item is opened an empty parameter-set is retrieved from Ilt, which enumerates all ControlTemplates.
        Dim paramsSet = New ParameterSetCollection()
        Dim params = New ParameterCollection() With {.Id = "Test"} 'See ILT
        params.InnerParameters.Add(New XhtmlResourceParameter() With {.Name = "itemBody", .Value = "SecretDoc.txt"})
        paramsSet.Add(params)

        'Act
        Dim itmlayout As New ItemLayoutAdapter("ItemLayout", paramsSet, handler)
        itmlayout.ValidateSolution(New KeyFindingCollection())

        'Assert
        A.CallTo(handler).MustHaveHappened(Repeated.Exactly.Times(3))

        Dim p As XhtmlResourceParameter = DirectCast(params.InnerParameters(0), XhtmlResourceParameter)
        Assert.IsNotNull(p.Content)
        Assert.IsTrue(p.Content.StartsWith(txt_metRef.ToString().Substring(0, 5))) 'Check if param is filled.
    End Sub


    <TestMethod()> <TestCategory("ContentModel")> <WorkItem(10139)>
    Public Sub LoadSimple_ILT_WithSimpleControlTemplate_ExpectsOnly_OnlyILT_Loaded()
        'Arrange
        Dim control = <?xml version="1.0" encoding="utf-8"?>
                      <Template xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" definitionVersion="2">
                          <Description></Description>
                          <Targets>
                              <Target xsi:type="ControlTemplateTarget" enabled="true" name="ces">
                                  <Template><![CDATA[	]]></Template>
                              </Target>
                          </Targets>
                          <SharedFunctions/>
                          <SharedParameterSet>
                              <resourceparameter name="itemBody">
                                  <designersetting key="label">Informatie</designersetting>
                                  <designersetting key="required">false</designersetting>
                              </resourceparameter>
                          </SharedParameterSet>
                      </Template>

        Dim handler As EventHandler(Of ResourceNeededEventArgs) = a.Fake(Of EventHandler(Of ResourceNeededEventArgs))()
        A.CallTo(Sub() handler.Invoke(A(Of Object).Ignored, A(Of ResourceNeededEventArgs).Ignored)).
            Invokes(Sub(i)
                        'This is the Resource Needed Handler.
                        Dim e = i.GetArgument(Of ResourceNeededEventArgs)(1)
                        If e.ResourceName = "SecretDoc.avi" Then
                            Assert.Fail() 'Should not occur!
                        Else
                            e.BinaryResource = New BinaryResource(e.ResourceName, Nothing, If(e.ResourceName = "ItemLayout", ilt.ToString(), control.ToString()), Nothing)
                        End If
                    End Sub)

        'Act if merging has taken place. Normally when an item is opened an empty parameter-set is retrieved from Ilt, which enumerates all ControlTemplates.
        Dim paramsSet = New ParameterSetCollection()
        Dim params = New ParameterCollection() With {.Id = "Test"} 'See ILT
        params.InnerParameters.Add(New ResourceParameter() With {.Name = "itemBody", .Value = "SecretDoc.avi"})
        paramsSet.Add(params)

        'Act
        Dim itmlayout As New ItemLayoutAdapter("ItemLayout", paramsSet, handler)
        itmlayout.ValidateSolution(New KeyFindingCollection())

        'Assert
        A.CallTo(handler).MustHaveHappened(Repeated.Exactly.Times(2))
        Dim p As ResourceParameter = DirectCast(params.InnerParameters(0), ResourceParameter)
    End Sub

#Region "Privates"

    'Default Item Layout template
    Private ilt As XElement = <Template xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" definitionVersion="2">
                                  <Description></Description>
                                  <Targets>
                                      <Target xsi:type="ItemLayoutTemplateTarget" enabled="true" name="ces">
                                          <Template>
                                              <![CDATA[<html><body><cito:control xmlns:cito="http://www.cito.nl/citotester" id="Test" type="Test.Control" /></body></html> ]]>
                                          </Template>
                                      </Target>
                                  </Targets>
                              </Template>


    Private txt_metRef As XElement = <html xmlns="http://www.w3.org/1999/xhtml">
                                         <head>
                                             <title>Document Title 2</title>
                                             <link href="resource://package:1/SomeStylesheet.css" rel="StyleSheet" type="text/css" media="screen"/>
                                             <style type="text/css"> a[popuppar] {border-color: blue; border-style: dotted; border-width: 1px;} </style>
                                             <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
                                         </head>
                                         <body style="padding: 2px; margin : 0px; width: 100%;">
                                             <p class="UserSRTitelGroot">De buitengesloten piloot </p>
                                             <p>Kies de 
                                <span id="refbbc82192-bc3f-4f1e-96d1-e76702ba73e5"
                                    contenteditable="false"
                                    cito:type="reference"
                                    cito:reftype="Highlight"
                                    cito:description="juiste"
                                    cito:value="1"
                                    xmlns:cito="http://www.cito.nl/citotester">juiste</span> koers. </p>
                                             <p></p>
                                         </body>
                                     </html>

    Private txt As XElement = <html xmlns="http://www.w3.org/1999/xhtml">
                                  <head>
                                      <title>Document Title 2</title>
                                      <link href="resource://package:1/SomeStylesheet.css" rel="StyleSheet" type="text/css" media="screen"/>
                                      <style type="text/css"> a[popuppar] {border-color: blue; border-style: dotted; border-width: 1px;} </style>
                                      <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
                                  </head>
                                  <body style="padding: 2px; margin : 0px; width: 100%;">
                                      <p class="UserSRTitelGroot">De buitengesloten piloot </p>
                                      <p>Kies de juiste koers. </p>
                                  </body>
                              </html>

#End Region

End Class
