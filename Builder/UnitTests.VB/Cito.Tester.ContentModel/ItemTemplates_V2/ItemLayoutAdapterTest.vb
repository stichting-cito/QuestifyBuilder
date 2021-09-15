
Option Infer On

Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports FakeItEasy

<TestClass()>
Public Class ItemLayoutAdapterTest

    <TestMethod()> <TestCategory("ContentModel")> <ExpectedException(GetType(InteractionControlException))>
    Public Sub RetrieveUnloadableItemLayOutTemplate_ExpectException()
        'Arrange
        Dim handler As EventHandler(Of ResourceNeededEventArgs) = Sub()
                                                                      'Do nothing
                                                                  End Sub
       
        'Act
        Dim itmlayout As New ItemLayoutAdapter("ItemLayout", Nothing, handler)

        'Assert
        'Expected Exception
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub LoadSimpleTemplate_LoadItemTmplByName()
        'Arrange
        Dim ilt = <?xml version="1.0" encoding="utf-8"?>
                  <Template xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" definitionVersion="2">
                      <Description></Description>
                      <Targets>
                          <Target xsi:type="ItemLayoutTemplateTarget" enabled="true" name="ces">
                              <Template>
                                  <![CDATA[ ]]>
                              </Template>
                          </Target>
                      </Targets>
                  </Template>

        Dim handler As EventHandler(Of ResourceNeededEventArgs) = a.Fake(Of EventHandler(Of ResourceNeededEventArgs))()
        A.CallTo(Sub() handler.Invoke(A(Of Object).Ignored, A(Of ResourceNeededEventArgs).That.Matches(Function(e As ResourceNeededEventArgs) e.ResourceName = "ItemLayout"))).
            Invokes(Sub(i)
                        'This is the Resource Needed Handler.
                        Dim e = i.GetArgument(Of ResourceNeededEventArgs)(1)
                        e.BinaryResource = New BinaryResource(e.ResourceName, Nothing, ilt.ToString(), Nothing)
                    End Sub)

        'Act
        Dim itmlayout As New ItemLayoutAdapter("ItemLayout", Nothing, handler)

        'Assert
        A.CallTo(handler).MustHaveHappened(Repeated.Exactly.Once)
    End Sub

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub LoadSimple_ILT_WithSimpleControlTemplate_ExpectsOnly_OnlyILT_Loaded()
        'Arrange
        Dim ilt = <?xml version="1.0" encoding="utf-8"?>
                  <Template xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" definitionVersion="2">
                      <Description></Description>
                      <Targets>
                          <Target xsi:type="ItemLayoutTemplateTarget" enabled="true" name="ces">
                              <Template>
                                  <![CDATA[ <cito:control xmlns:cito="http://www.cito.nl/citotester" id="Test" type="Test.Control" /> ]]>
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
                          <SharedParameterSet/>
                      </Template>

        Dim handler As EventHandler(Of ResourceNeededEventArgs) = A.Fake(Of EventHandler(Of ResourceNeededEventArgs))()
        A.CallTo(Sub() handler.Invoke(A(Of Object).Ignored, A(Of ResourceNeededEventArgs).Ignored)).
            Invokes(Sub(i)
                        'This is the Resource Needed Handler.
                        Dim e = i.GetArgument(Of ResourceNeededEventArgs)(1)
                        e.BinaryResource = New BinaryResource(e.ResourceName, Nothing, ilt.ToString(), Nothing)
                    End Sub)

        'Act
        Dim itmlayout As New ItemLayoutAdapter("ItemLayout", Nothing, handler)

        'Assert
        A.CallTo(handler).MustHaveHappened(Repeated.Exactly.Once)
    End Sub

End Class
