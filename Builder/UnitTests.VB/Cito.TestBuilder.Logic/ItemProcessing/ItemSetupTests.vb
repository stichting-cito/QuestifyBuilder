
Imports System.Xml.Linq
Imports Questify.Builder.Logic
Imports Questify.Builder.UnitTests.Framework
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ItemProcessing
Imports Questify.Builder.UnitTests.Framework.FakeAppTemplate

<TestClass>
Public Class ItemSetupTests

    <TestInitialize()>
    Public Sub Init()
        FakeDal.Init()
    End Sub

    <TestCleanup()>
    Public Sub DeInit()
        FakeDal.Deinit()
    End Sub

    <TestMethod(), TestCategory("ItemProcessing")>
    Public Sub Create2NewItems_ParameterSetAreNotRefEqual()
        Dim ilt As ItemLayoutTemplateResourceEntity = Nothing
        Dim newItem_A As New ItemResourceEntity(Guid.NewGuid()) With {.ResourceData = New ResourceDataEntity()}
        Dim newItem_B As New ItemResourceEntity(Guid.NewGuid()) With {.ResourceData = New ResourceDataEntity()}

        FakeDal.Add.ControlTemplate("min.html", Sub(i) FillWithControlTemplate(i)).
            IsUsedBy.ItemTemplate("ilt.html", Sub(i)
                                                  FillWithTemplate(i)
                                                  ilt = i
                                              End Sub)

        Dim warnErr As New WarningsAndErrors
        Dim cache = New SimpleCache
        Dim ResourceManager = FakeDal.GetFakeResourceManager()
        Dim a = New AssessmentItemHelper(ResourceManager, ilt.Name, newItem_A, cache)
        Dim b = New AssessmentItemHelper(ResourceManager, ilt.Name, newItem_B, cache)

        Dim result1 = a.CreateNewAssessmentItem(newItem_A, ilt, warnErr)
        Dim result2 = b.CreateNewAssessmentItem(newItem_B, ilt, warnErr)

        Assert.AreNotSame(result1.Parameters(0), result2.Parameters(0))
    End Sub

    <TestMethod(), TestCategory("ItemProcessing")>
    Public Sub LoadExistingItem()
        Dim itm As ItemResourceEntity = Nothing
        Dim ilt As ItemLayoutTemplateResourceEntity = Nothing

        FakeDal.Add.Item("Some_Item", Sub(i)
                                          FillWithAssessment(i)
                                          itm = i
                                      End Sub).DependsOn.ItemTemplate("ilt.html", Sub(i)
                                                                                      FillWithTemplate(i)
                                                                                      ilt = i
                                                                                  End Sub).DependsOn.ControlTemplate("min.html", Sub(i) FillWithControlTemplate(i))

        Dim warnErr As New WarningsAndErrors
        Dim cache = New SimpleCache
        Dim ResourceManager = FakeDal.GetFakeResourceManager()
        Dim _itemSetupHelper = New AssessmentItemHelper(ResourceManager, ilt.Name, itm, cache)

        Dim _assessmentItem = _itemSetupHelper.GetExistingAssessmentItem()
        _itemSetupHelper.MergeParameters(_assessmentItem, warnErr)
        _itemSetupHelper.ReFillParameterSet(_assessmentItem)

        Dim xhtml = DirectCast(_assessmentItem.Parameters(0).InnerParameters(0), XHtmlParameter)
        Assert.IsTrue(xhtml.Value.Contains("Dit is tekst."))
    End Sub

    <TestMethod(), TestCategory("ItemProcessing")> <WorkItem(11029)>
    Public Sub LoadExistingItem_ValidateThatDesignerSettings_AreAvailable()
        Dim itm As ItemResourceEntity = Nothing
        Dim ilt As ItemLayoutTemplateResourceEntity = Nothing

        FakeDal.Add.Item("Some_Item", Sub(i)
                                          FillWithAssessment(i)
                                          itm = i
                                      End Sub).DependsOn.ItemTemplate("ilt.html", Sub(i)
                                                                                      FillWithTemplate(i)
                                                                                      ilt = i
                                                                                  End Sub).DependsOn.ControlTemplate("min.html", Sub(i) FillWithControlTemplate(i))

        Dim warnErr As New WarningsAndErrors
        Dim cache = New SimpleCache
        Dim ResourceManager = FakeDal.GetFakeResourceManager()
        Dim _itemSetupHelper = New AssessmentItemHelper(ResourceManager, ilt.Name, itm, cache)

        Dim _assessmentItem = _itemSetupHelper.GetExistingAssessmentItem()
        _itemSetupHelper.MergeParameters(_assessmentItem, warnErr)
        _itemSetupHelper.ReFillParameterSet(_assessmentItem)

        Dim xhtml = DirectCast(_assessmentItem.Parameters(0).InnerParameters(0), XHtmlParameter)
        Assert.AreEqual(4, xhtml.DesignerSettings.Count)
    End Sub

    <TestMethod(), TestCategory("ItemProcessing")>
    Public Sub SortTemplateDesignerSetting_IsAppliedToParameterSet_TemplateHasSortedDesignerSetting_ExpectTrue()
        Dim ilt As ItemLayoutTemplateResourceEntity = Nothing
        Dim itemA As New ItemResourceEntity(Guid.NewGuid()) With {.ResourceData = New ResourceDataEntity()}

        FakeDal.Add.ControlTemplate("min.html", Sub(i) FillWithControlTemplate(i)).
            IsUsedBy.ItemTemplate("ilt.SortTest", Sub(i)
                                                      FillWith_Sorted_TestTemplate(i)
                                                      ilt = i
                                                  End Sub)

        Dim cache = New SimpleCache
        Dim resourceManager = FakeDal.GetFakeResourceManager()
        Dim assessmentHelper = New AssessmentItemHelper(resourceManager, ilt.Name, itemA, cache)

        Dim result = assessmentHelper.GetExtractedParameters()

        Assert.IsTrue(result.ShouldSort)
    End Sub

    <TestMethod(), TestCategory("ItemProcessing")>
    Public Sub SortTemplateDesignerSetting_IsAppliedToParameterSet_Template_HasNO_SortedDesignerSetting_ExpectFalse()
        Dim ilt As ItemLayoutTemplateResourceEntity = Nothing
        Dim itemA As New ItemResourceEntity(Guid.NewGuid()) With {.ResourceData = New ResourceDataEntity()}


        FakeDal.Add.ControlTemplate("min.html", Sub(i) FillWithControlTemplate(i)).
            IsUsedBy.ItemTemplate("ilt.html", Sub(i)
                                                  FillWithTemplate(i)
                                                  ilt = i
                                              End Sub)

        Dim cache = New SimpleCache
        Dim resourceManager = FakeDal.GetFakeResourceManager()
        Dim assessmentHelper = New AssessmentItemHelper(resourceManager, ilt.Name, itemA, cache)

        Dim result = assessmentHelper.GetExtractedParameters()

        Assert.IsFalse(result.ShouldSort)
    End Sub

    Private Sub FillWith_Sorted_TestTemplate(i As ItemLayoutTemplateResourceEntity)
        i.SetXmlAsBinData(<Template xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" definitionVersion="1">
                              <Description></Description>
                              <Settings>
                                  <DesignerSetting key="sort">True</DesignerSetting>
                              </Settings>
                              <Targets>
                                  <Target xsi:type="ItemLayoutTemplateTarget" enabled="true" name="word">
                                      <Description>Alleen voor word 2010+</Description>
                                      <Template><![CDATA[<html><body></body></html>]]></Template>
                                  </Target>
                                  <Target xsi:type="ItemLayoutTemplateTarget" enabled="true" name="ces">
                                      <Description>CES</Description>
                                      <Template><![CDATA[<html><body></body></html>]]></Template>
                                  </Target>
                              </Targets>
                          </Template>)
    End Sub

    Private Sub FillWithAssessment(i As ItemResourceEntity, data As XElement)
        i.SetXmlAsBinData(data)
    End Sub

    Private Sub FillWithAssessment(i As ItemResourceEntity)
        i.SetXmlAsBinData(<assessmentItem xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" identifier="code" title="title" layoutTemplateSrc="ilt.html">
                              <solution>
                                  <keyFindings/>
                                  <aspectReferences/>
                              </solution>
                              <parameters>
                                  <parameterSet id="invoer">
                                      <xhtmlparameter name="itemGeneral">
                                          <p id="c1-id-8" xmlns="http://www.w3.org/1999/xhtml">Dit is tekst.</p>
                                      </xhtmlparameter>
                                  </parameterSet>
                              </parameters>
                          </assessmentItem>)
    End Sub

    Private Sub FillWithTemplate(i As ItemLayoutTemplateResourceEntity)
        i.SetXmlAsBinData(<Template xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" definitionVersion="1">
                              <Description></Description>
                              <Settings>
                              </Settings>
                              <Targets>
                                  <Target xsi:type="ItemLayoutTemplateTarget" enabled="true" name="word">
                                      <Description>Alleen voor word 2010+</Description>
                                      <Template>
                                          <![CDATA[
				                            <html>
					                            <body>
						                            <cito:control xmlns:cito="http://www.cito.nl/citotester" id="invoer" type="min.html" />
					                            </body>
				                            </html>
			                            ]]>
                                      </Template>
                                  </Target>
                                  <Target xsi:type="ItemLayoutTemplateTarget" enabled="true" name="ces">
                                      <Description>CES</Description>
                                      <Template>
                                          <![CDATA[
				                            <html>
					                            <body>
						                            <cito:control xmlns:cito="http://www.cito.nl/citotester" id="invoer" type="min.html" />
					                            </body>
				                            </html>
			                            ]]>
                                      </Template>
                                  </Target>
                              </Targets>
                          </Template>)
    End Sub

    Private Sub FillWithControlTemplate(i As ControlTemplateResourceEntity)
        i.SetXmlAsBinData(<Template xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" definitionVersion="1">
                              <Description>Controltemplate voor GAP items</Description>
                              <Targets>
                                  <Target xsi:type="ControlTemplateTarget" enabled="true" name="Word">
                                      <Description>Word</Description>
                                      <Template><![CDATA[<html><body></body></html>]]></Template>
                                  </Target>
                                  <Target xsi:type="ControlTemplateTarget" enabled="true" name="ces">
                                      <Description>CES</Description>
                                      <Template><![CDATA[<html><body></body></html>]]></Template>
                                  </Target>
                              </Targets>
                              <SharedFunctions>
                              </SharedFunctions>
                              <SharedParameterSet id="parameters">
                                  <xhtmlparameter name="itemGeneral">
                                      <designersetting key="label">Algemeen tekstveld</designersetting>
                                      <designersetting key="description">Geef hier tekst en/of afbeelding in zoals die in het rechter deel onder de antwoorden weergegeven dient te worden.</designersetting>
                                      <designersetting key="group">1 Algemeen tekstveld</designersetting>
                                      <designersetting key="required">True</designersetting>
                                  </xhtmlparameter>
                              </SharedParameterSet>
                          </Template>)
    End Sub

    Class SimpleCache : Implements IITemSetupCacheHelper


        Private _cachedParameterSetCollection As Dictionary(Of String, ParameterSetCollection)
        Private _cachedIsTransformed As Dictionary(Of String, Boolean)



        Sub New()
            _cachedParameterSetCollection = New Dictionary(Of String, ParameterSetCollection)()
            _cachedIsTransformed = New Dictionary(Of String, Boolean)
        End Sub


        Public Function GetCachedExtractedParameters(iltName As String) As ParameterSetCollection Implements IITemSetupCacheHelper.GetCachedExtractedParameters
            If (_cachedParameterSetCollection.ContainsKey(iltName)) Then
                Return _cachedParameterSetCollection(iltName)
            End If
            Return Nothing
        End Function

        Public Function GetCachedIsTransformed(iltName As String) As Boolean? Implements IITemSetupCacheHelper.GetCachedIsTransformed
            If (_cachedIsTransformed.ContainsKey(iltName)) Then
                Return _cachedIsTransformed(iltName)
            End If
            Return False
        End Function

        Public Sub ReadyForCaching(iltName As String, ExtractedParameters As ParameterSetCollection, isTransformed As Boolean) Implements IITemSetupCacheHelper.ReadyForCaching
            If (Not _cachedParameterSetCollection.ContainsKey(iltName)) Then
                _cachedParameterSetCollection.Add(iltName, ExtractedParameters)
            End If
            If (Not _cachedIsTransformed.ContainsKey(iltName)) Then
                _cachedIsTransformed.Add(iltName, isTransformed)
            End If
        End Sub

    End Class

End Class
