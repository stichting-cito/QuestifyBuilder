
Option Infer On

Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports FakeItEasy


<TestClass()>
Public Class ItemLayoutAndControlAdapterTests

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub LoadSimple_ILT_WithSimpleControlTemplate_ExpectsOnly_OnlyILT_Loaded()
        Dim extractedParameterSets As ParameterSetCollection
        Dim ilt = <?xml version="1.0" encoding="utf-8"?>
                  <Template xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" definitionVersion="2">
                      <Description></Description>
                      <Targets>
                          <Target xsi:type="ItemLayoutTemplateTarget" enabled="true" name="ces">
                              <Template>
                                  <![CDATA[<html><body><cito:control xmlns:cito="http://www.cito.nl/citotester" id="Test" type="Test.Control" /></body></html> ]]>
                              </Template>
                          </Target>
                      </Targets>
                  </Template>

        Dim control = <?xml version="1.0" encoding="utf-8"?>
                      <Template xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" definitionVersion="2">
                          <Description></Description>
                          <Targets>
                              <Target xsi:type="ControlTemplateTarget" enabled="true" name="ces">
                                  <Template>
                                      <![CDATA[	]]>
                                  </Template>
                              </Target>
                          </Targets>
                          <SharedFunctions/>
                          <SharedParameterSet>
                              <booleanparameter name="bool1"/>
                          </SharedParameterSet>
                      </Template>

        Dim handler As EventHandler(Of ResourceNeededEventArgs) = a.Fake(Of EventHandler(Of ResourceNeededEventArgs))()
        A.CallTo(Sub() handler.Invoke(A(Of Object).Ignored, A(Of ResourceNeededEventArgs).Ignored)).
            Invokes(Sub(i)
                        Dim e = i.GetArgument(Of ResourceNeededEventArgs)(1)
                        e.BinaryResource = New BinaryResource(e.ResourceName, Nothing, If(e.ResourceName = "ItemLayout", ilt.ToString(), control.ToString()), Nothing)
                    End Sub)

        Dim itmlayout As New ItemLayoutAdapter("ItemLayout", Nothing, handler)

        extractedParameterSets = itmlayout.CreateParameterSetsFromItemTemplate()

        A.CallTo(handler).MustHaveHappened(Repeated.Exactly.Twice)
        Assert.AreEqual(1, extractedParameterSets.Count)

    End Sub

End Class
