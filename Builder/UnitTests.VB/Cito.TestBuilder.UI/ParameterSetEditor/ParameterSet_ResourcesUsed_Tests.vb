
Imports System.Xml.Linq
Imports Cito.Tester.Common
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.UI
Imports Questify.Builder.UnitTests.Framework.FakeAppTemplate
Imports Questify.Builder.Logic

<TestClass()>
Public Class ParameterSet_ResourcesUsed_Tests

    <TestInitialize()>
    Public Sub Init()
        FakeDal.Init()
    End Sub

    <TestCleanup()>
    Public Sub DeInit()
        FakeDal.Deinit()
    End Sub

    <TestMethod(), TestCategory("UILogic")>
    Public Sub AssertThatNoResourcesAreLost()
        FakeDal.AddInline() : FakeDal.AddTransparentPix()
        Dim parametersetEdtr As New ParameterSetsEditor(Nothing, Nothing, False) With
            {.ResourceManager = FakeDal.GetFakeResourceManager()}
        Dim _itemResource As New ItemResourceEntity()
        _itemResource.Bank = New BankEntity()

        Dim extractedParameterSets As ParameterSetCollection = Nothing
        Dim adapter As New ItemLayoutAdapter("ItemTemplate", Nothing, AddressOf GenericHandler_ResourceNeeded)
        extractedParameterSets = adapter.CreateParameterSetsFromItemTemplate()
        InjectParameterSetWithValue(extractedParameterSets)
        parametersetEdtr.ResourceEntity = _itemResource
        parametersetEdtr.ItemLayoutAdapterForItem = adapter

        AddResource(_itemResource, "TransparentPix.png")
        AddResource(_itemResource, "InlineImageLayoutTemplate")

        parametersetEdtr.ParameterSets = extractedParameterSets

        Dim allUsed As Boolean = _itemResource.DependentResourceCollection.Count = 2

        Assert.IsTrue(allUsed)
    End Sub

    Sub AddResource(item As ItemResourceEntity, name As String)
        If Not String.IsNullOrEmpty(name) Then
            Dim referencedResource As ResourceEntity = ResourceFactory.Instance.GetResourceByNameWithOption(Nothing, name, New ResourceRequestDTO())
            DependencyManagement.AddDependentResourceToResource(item, referencedResource)
        End If
    End Sub

    Sub InjectParameterSetWithValue(extractedParameterSets As ParameterSetCollection)
        Dim val2 As String = "<p xmlns=""http://www.w3.org/1999/xhtml"" id=""c1-id-12"">" +
                                "<cito:InlineElement xmlns:cito=""http://www.cito.nl/citotester"" id=""e476357a-0e97-499d-aa51-510e91717cba"" layoutTemplateSourceName=""InlineImageLayoutTemplate"">" +
                                    "<cito:parameters>" +
                                        "<cito:parameterSet id=""entireItem"">" +
                                            "<cito:resourceparameter name=""source"">TransparentPix.png</cito:resourceparameter>" +
                                            "<cito:integerparameter name=""width"">400</cito:integerparameter>" +
                                            "<cito:integerparameter name=""height"">400</cito:integerparameter>" +
                                            "<cito:booleanparameter name=""showPopup"">False</cito:booleanparameter>" +
                                            "<cito:resourceparameter name=""largeImage"" />" +
                                            "<cito:integerparameter name=""largeWidth"" />" +
                                            "<cito:integerparameter name=""largeHeight"" />" +
                                        "</cito:parameterSet>" +
                                    "</cito:parameters>" +
                                "</cito:InlineElement>" +
                            "</p>"

        extractedParameterSets(0).InnerParameters(0).SetValue(val2)
    End Sub

    Protected Sub GenericHandler_ResourceNeeded(ByVal sender As Object, ByVal e As ResourceNeededEventArgs)
        If e.ResourceName = "ControlTemplate" Then
            e.BinaryResource = New BinaryResource(e.ResourceName, Nothing, ControlTemplate.ToString(), Nothing)
        ElseIf e.ResourceName = "ItemTemplate" Then
            e.BinaryResource = New BinaryResource(e.ResourceName, Nothing, ItemLayoutTemplate.ToString(), Nothing)
        Else
            Throw New Exception("Should NOT occur")
        End If
    End Sub

    Private ControlTemplate As XElement = <Template xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" definitionVersion="1">
                                              <Description>Controltemplate voor weergave standaard item parameters</Description>
                                              <Targets>
                                                  <Target xsi:type="ControlTemplateTarget" enabled="true" name="testPlayer">
                                                      <Description>Testplayer 1.x / 2.x</Description>
                                                      <Template></Template>
                                                  </Target>
                                              </Targets>
                                              <SharedParameterSet id="">
                                                  <xhtmlparameter name="itemBody">
                                                      <designersetting key="label">Informatie</designersetting>
                                                      <designersetting key="description">Schrijf hier de informatie die u wilt tonen.</designersetting>
                                                      <designersetting key="group">2 Informatie</designersetting>
                                                      <designersetting key="required">false</designersetting>
                                                  </xhtmlparameter>
                                              </SharedParameterSet>
                                          </Template>

    Private ItemLayoutTemplate As XElement = <html xmlns="http://www.w3.org/1999/xhtml" xmlns:cito="http://www.cito.nl/citotester">
                                                 <head>
                                                     <title></title>
                                                     <link rel="Stylesheet" href="" type="text/css"/>
                                                 </head>
                                                 <body onselectstart="javascript:return false;">
                                                     <cito:control id="entireItem" type="ControlTemplate"/>
                                                 </body>
                                             </html>

End Class
