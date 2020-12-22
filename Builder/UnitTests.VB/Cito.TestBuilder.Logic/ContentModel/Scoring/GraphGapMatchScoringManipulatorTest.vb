
Imports Cito.Tester.ContentModel
Imports System.Xml.Linq
Imports Cito.Tester.Common
Imports Questify.Builder.Logic.ContentModel
Imports Questify.Builder.Logic.ContentModel.Scoring
Imports System.Diagnostics
Imports System.Xml.Serialization
Imports System.IO
Imports System.Linq
Imports Questify.Builder.UnitTests.Framework

<TestClass()>
Public Class GraphGapMatchScoringManipulatorTest

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub GraphGapMatch_GetKey_A_NoSolution()
        Dim param = CreateScoreParameter()

        Dim sol = GetSolution(GraphGapMatchKeyFinding)
        Dim manipulator As IChoiceArrayScoringManipulator = param.GetScoreManipulator(sol)

        Dim res = manipulator.GetKeyStatus()

        Assert.IsTrue(res.ContainsKey("A"))
        Assert.AreEqual("IMAGE_A", res("A").ToString)
        Assert.AreEqual(2, param.Value.Count)
        Assert.AreEqual(4, param.Gaps.Count)
        Assert.AreEqual(2, res.Keys.Count)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub GetDisplayValue_version1()
        Dim param = CreateScoreParameter()
        Dim sol = GetSolution(GraphGapMatchKeyFinding)
        Dim manipulator As IChoiceArrayScoringManipulator = param.GetScoreManipulator(sol)

        Dim result = manipulator.GetDisplayValueForKey("A")

        Assert.AreEqual("choice IMAGE_A", result)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub GetDisplayValueForExistingItem()
        Dim item = ItemFromTestTool.To(Of AssessmentItem)()

        Dim param = DirectCast(item.Parameters.DeepFetchScoringParameters().First(Function(prm) TypeOf prm Is GraphGapMatchScoringParameter), GraphGapMatchScoringParameter)
        param = param.Transform()

        Dim sol = item.Solution
        Dim manipulator As IChoiceArrayScoringManipulator = ScoringParameterFactory.GetConceptScoreManipulator(Of IValidatingChoiceArrayScoringManipulator(Of String))(param, sol)

        Dim result = manipulator.GetDisplayValueForKey("A")

        Assert.AreEqual("Plaatje C", result)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub GraphGapMatch2_GetKey_A_NoSolution()
        Dim param = CreateScoreParameter(transform:=False)

        Dim sol = GetSolution(GraphGapMatchVar2KeyFinding)
        Dim manipulator As IChoiceArrayScoringManipulator = param.GetScoreManipulator(sol)

        Dim res = manipulator.GetKeyStatus()

        Assert.IsTrue(res.ContainsKey("IMAGE_A"))
        Assert.AreEqual(4, param.Value.Count)
        Assert.AreEqual(0, param.Gaps.Count)
        Assert.AreEqual("A", res("IMAGE_A").ToString)
        Assert.AreEqual("B", res("IMAGE_B").ToString)
        Assert.AreEqual("B", res("IMAGE_C").ToString)
        Assert.AreEqual(4, res.Keys.Count)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub GraphGapMatch_Remove_Key_FromSolution()
        Dim param = CreateScoreParameter()

        Dim sol = GetSolution(GraphGapMatchKeyFinding)
        Dim manipulator As IChoiceArrayScoringManipulator = param.GetScoreManipulator(sol)

        manipulator.Clear()
        WriteSolution("after clear", sol)

        manipulator.SetKey("B", "IMAGE_B")
        WriteSolution("after set key to interaction 2", sol)

        manipulator.RemoveKey("B")
        WriteSolution("after removal of interaction 2", sol)

        Dim res = manipulator.GetKeyStatus()

        Assert.AreEqual(0, res("A").Value.Count)
        Assert.AreEqual(0, res("B").Value.Count)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub GraphGapMatch_Set_Key_FromSolution()
        Dim param = CreateScoreParameter()

        Dim sol = GetSolution(GraphGapMatchKeyFinding)
        Dim manipulator As IChoiceArrayScoringManipulator = param.GetScoreManipulator(sol)

        manipulator.Clear()
        WriteSolution("after clear", sol)

        manipulator.SetKey("B", "IMAGE_B")
        WriteSolution("after set key to interaction 2", sol)

        Dim res = manipulator.GetKeyStatus()

        Assert.AreEqual("IMAGE_B", res("B").ToString)
        Assert.AreEqual(String.Empty, res("A").ToString)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub GraphGapMatch_ClearKey_FromSolution()
        Dim param = CreateScoreParameter()

        Dim sol = GetSolution(GraphGapMatchKeyFinding)
        Dim manipulator As IChoiceArrayScoringManipulator = param.GetScoreManipulator(sol)

        manipulator.Clear()
        WriteSolution("after clear", sol)
        Dim res = manipulator.GetKeyStatus()

        Assert.AreEqual(2, res.Keys.Count)
        Assert.IsTrue(res.ContainsKey("A"))
        Assert.IsTrue(res.ContainsKey("B"))

        Assert.AreEqual(0, res("A").Value.Count)
        Assert.AreEqual(0, res("B").Value.Count)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub GraphGapMatch_GetKey_B_NoSolution()
        Dim param = CreateScoreParameter()

        Dim key = CreateGapMatchKeyFinding("IMAGE_A", "B")

        Dim manipulator As IChoiceArrayScoringManipulator = New GraphGapMatchScoringManipulator(GetKeyManipulator(key), param)
        manipulator.Clear()

        manipulator.SetKey("A", "IMAGE_A")
        manipulator.SetKey("B", "IMAGE_B")

        Dim res = manipulator.GetKeyStatus()

        Assert.AreEqual("IMAGE_A", res("A").ToString)
        Assert.AreEqual("IMAGE_B", res("B").ToString)
        Assert.AreEqual(2, res.Keys.Count)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub GraphGapMatch_CreateInvalidFinding_Test()
        Dim param = CreateScoreParameter()

        Dim sol = GetSolution(GraphGapMatchKeyFinding)
        Dim manipulator As IValidatingChoiceArrayScoringManipulator(Of String) = param.GetScoreManipulator(sol)

        manipulator.SetKey(param.Value(1).Id, "IMAGE_A")

        manipulator.GetKeyStatus()

        Dim isValidFinding = manipulator.IsValid("IMAGE_A")

        Assert.IsFalse(isValidFinding)
    End Sub

    Friend Overridable Function GetKeyManipulator(key As KeyFinding) As FindingManipulatorBase
        Return New KeyManipulator(key)
    End Function

    Protected Function CreateGapMatchKeyFinding(id As String, ParamArray keys As String()) As KeyFinding
        Dim ret As New KeyFinding()

        For Each k In keys
            Dim fact = New KeyFact(id) With {.Score = 1}
            Dim keyVal = New KeyValue(id, 1)
            fact.Values.Add(keyVal)
            keyVal.Values.Add(New StringValue(k))

            ret.Facts.Add(fact)
        Next

        Return ret
    End Function

    Private Function CreateScoreParameter(Optional transform As Boolean = True) As GraphGapMatchScoringParameter
        Dim param = New GraphGapMatchScoringParameter() With {.ControllerId = "ggmCtlrId", .FindingOverride = "ggm1"}
        param.Value = New ParameterSetCollection
        param.Value.Add(New ParameterCollection() With {.Id = "IMAGE_A"})
        param.Value(0).InnerParameters.Add(New GapImageParameter() With {.Name = "choice", .MatchMax = 1, .Value = "1.png"})
        param.Value.Add(New ParameterCollection() With {.Id = "IMAGE_B"})
        param.Value(1).InnerParameters.Add(New GapImageParameter() With {.Name = "choice", .MatchMax = 1, .Value = "2.png"})
        param.Value.Add(New ParameterCollection() With {.Id = "IMAGE_C"})
        param.Value(2).InnerParameters.Add(New GapImageParameter() With {.Name = "choice", .MatchMax = 1, .Value = "3.png"})
        param.Value.Add(New ParameterCollection() With {.Id = "IMAGE_D"})
        param.Value(3).InnerParameters.Add(New GapImageParameter() With {.Name = "choice", .MatchMax = 1, .Value = "4.png"})

        Dim areaParam = Deserialize(Of AreaParameter)(_areaParamElement)
        param.Area = areaParam

        If transform Then param = param.Transform()

        Return param
    End Function

    Private Function GetSolution(xElement As Xml.Linq.XElement) As Solution
        Return DirectCast(SerializeHelper.XmlDeserializeFromString(xElement.ToString(), GetType(Solution)), Solution)
    End Function

    Private ReadOnly _areaParamElement As XElement = <AreaParameter xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                                         <subparameterset id="A">
                                                             <resourceparameter name="clickableImage">clickableImage.jpg</resourceparameter>
                                                         </subparameterset>
                                                         <definition id="">
                                                             <resourceparameter name="clickableImage"/>
                                                         </definition>
                                                         <Shapes>
                                                             <Rectangle id="A" label="A">
                                                                 <TopLeft>
                                                                     <X>33</X>
                                                                     <Y>46</Y>
                                                                 </TopLeft>
                                                                 <BottomRight>
                                                                     <X>155</X>
                                                                     <Y>85</Y>
                                                                 </BottomRight>
                                                             </Rectangle>
                                                             <Rectangle id="B" label="B">
                                                                 <TopLeft>
                                                                     <X>352</X>
                                                                     <Y>47</Y>
                                                                 </TopLeft>
                                                                 <BottomRight>
                                                                     <X>473</X>
                                                                     <Y>85</Y>
                                                                 </BottomRight>
                                                             </Rectangle>
                                                         </Shapes>
                                                     </AreaParameter>

    Private Shared ReadOnly GraphGapMatchKeyFinding As XElement = <solution>
                                                                      <keyFindings>
                                                                          <keyFinding id="ggm1" scoringMethod="Dichotomous">
                                                                              <keyFact id="A-ggmCtlrId" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                  <keyValue domain="A-ggmCtlrId" occur="1">
                                                                                      <stringValue>
                                                                                          <typedValue>IMAGE_A</typedValue>
                                                                                      </stringValue>
                                                                                  </keyValue>
                                                                              </keyFact>
                                                                              <keyFact id="B-ggmCtlrId" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                  <keyValue domain="B-ggmCtlrId" occur="1">
                                                                                      <stringValue>
                                                                                          <typedValue>IMAGE_B</typedValue>
                                                                                      </stringValue>
                                                                                  </keyValue>
                                                                              </keyFact>
                                                                              <keyFact id="C-ggmCtlrId" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                  <keyValue domain="C-ggmCtlrId" occur="1">
                                                                                      <stringValue>
                                                                                          <typedValue>IMAGE_B</typedValue>
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

    Private Shared ReadOnly GraphGapMatchVar2KeyFinding As XElement = <solution>
                                                                          <keyFindings>
                                                                              <keyFinding id="ggm1" scoringMethod="Dichotomous">
                                                                                  <keyFact id="IMAGE_A-ggmCtlrId" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                      <keyValue domain="IMAGE_A-ggmCtlrId" occur="1">
                                                                                          <stringValue>
                                                                                              <typedValue>A</typedValue>
                                                                                          </stringValue>
                                                                                      </keyValue>
                                                                                  </keyFact>
                                                                                  <keyFact id="IMAGE_B-ggmCtlrId" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                      <keyValue domain="IMAGE_B-ggmCtlrId" occur="1">
                                                                                          <stringValue>
                                                                                              <typedValue>B</typedValue>
                                                                                          </stringValue>
                                                                                      </keyValue>
                                                                                  </keyFact>
                                                                                  <keyFact id="IMAGE_C-ggmCtlrId" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                      <keyValue domain="IMAGE_C-ggmCtlrId" occur="1">
                                                                                          <stringValue>
                                                                                              <typedValue>B</typedValue>
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


    Private Shared ReadOnly ItemFromTestTool As XElement = <assessmentItem xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" identifier="TI_GapMatch" title="TI_GrpahicGapMatch" layoutTemplateSrc="ilt.graphicgapmatch1">
                                                               <solution>
                                                                   <keyFindings>
                                                                       <keyFinding id="gapMatchController" scoringMethod="Polytomous">
                                                                           <keyFact id="A" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                               <keyValue domain="A" occur="1">
                                                                                   <stringValue>
                                                                                       <typedValue>C</typedValue>
                                                                                   </stringValue>
                                                                               </keyValue>
                                                                           </keyFact>
                                                                           <keyFact id="B" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                               <keyValue domain="B" occur="1">
                                                                                   <stringValue>
                                                                                       <typedValue>A</typedValue>
                                                                                   </stringValue>
                                                                               </keyValue>
                                                                           </keyFact>
                                                                       </keyFinding>
                                                                   </keyFindings>
                                                                   <conceptFindings>
                                                                       <conceptFinding id="gapMatchController" scoringMethod="None">
                                                                           <conceptFact id="A" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                               <conceptValue domain="A" occur="1">
                                                                                   <stringValue>
                                                                                       <typedValue>C</typedValue>
                                                                                   </stringValue>
                                                                               </conceptValue>
                                                                               <concepts/>
                                                                           </conceptFact>
                                                                           <conceptFact id="B" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                               <conceptValue domain="B" occur="1">
                                                                                   <stringValue>
                                                                                       <typedValue>A</typedValue>
                                                                                   </stringValue>
                                                                               </conceptValue>
                                                                               <concepts/>
                                                                           </conceptFact>
                                                                           <conceptFact id="A[*]" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                               <conceptValue domain="A[*]" occur="1">
                                                                                   <catchAllValue/>
                                                                               </conceptValue>
                                                                               <concepts/>
                                                                           </conceptFact>
                                                                           <conceptFact id="B[*]" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                               <conceptValue domain="B[*]" occur="1">
                                                                                   <catchAllValue/>
                                                                               </conceptValue>
                                                                               <concepts/>
                                                                           </conceptFact>
                                                                       </conceptFinding>
                                                                   </conceptFindings>
                                                                   <aspectReferences/>
                                                                   <ItemScoreTranslationTable>
                                                                       <ItemScoreTranslationTableEntry rawScore="0" translatedScore="0"/>
                                                                       <ItemScoreTranslationTableEntry rawScore="1" translatedScore="1"/>
                                                                       <ItemScoreTranslationTableEntry rawScore="2" translatedScore="2"/>
                                                                   </ItemScoreTranslationTable>
                                                               </solution>
                                                               <parameters>
                                                                   <parameterSet id="invoer">
                                                                       <graphGapMatchScoringParameter name="domainx" findingOverride="gapMatchController">
                                                                           <subparameterset id="A">
                                                                               <gapImageParameter name="Plaatje" matchMax="1" width="0" height="0" contentType="Image" enteredText="">1.png</gapImageParameter>
                                                                           </subparameterset>
                                                                           <subparameterset id="B">
                                                                               <gapImageParameter name="Plaatje" matchMax="1" width="0" height="0" contentType="Image" enteredText="">2.png</gapImageParameter>
                                                                           </subparameterset>
                                                                           <subparameterset id="C">
                                                                               <gapImageParameter name="Plaatje" matchMax="1" width="0" height="0" contentType="Image" enteredText="">3.png</gapImageParameter>
                                                                           </subparameterset>
                                                                           <subparameterset id="D">
                                                                               <gapImageParameter name="Plaatje" matchMax="1" width="0" height="0" contentType="Image" enteredText="">4.png</gapImageParameter>
                                                                           </subparameterset>
                                                                           <definition id="">
                                                                               <gapImageParameter name="Plaatje" matchMax="1" width="0" height="0" contentType="Image"/>
                                                                           </definition>
                                                                           <areaparameter name="itemQuestionArea">
                                                                               <subparameterset id="A">
                                                                                   <resourceparameter name="clickableImage">2.jpg</resourceparameter>
                                                                               </subparameterset>
                                                                               <definition id="">
                                                                                   <resourceparameter name="clickableImage"/>
                                                                               </definition>
                                                                               <Shapes>
                                                                                   <Rectangle id="A" label="A">
                                                                                       <TopLeft>
                                                                                           <X>33</X>
                                                                                           <Y>46</Y>
                                                                                       </TopLeft>
                                                                                       <BottomRight>
                                                                                           <X>155</X>
                                                                                           <Y>85</Y>
                                                                                       </BottomRight>
                                                                                   </Rectangle>
                                                                                   <Rectangle id="B" label="B">
                                                                                       <TopLeft>
                                                                                           <X>352</X>
                                                                                           <Y>47</Y>
                                                                                       </TopLeft>
                                                                                       <BottomRight>
                                                                                           <X>473</X>
                                                                                           <Y>85</Y>
                                                                                       </BottomRight>
                                                                                   </Rectangle>
                                                                               </Shapes>
                                                                           </areaparameter>
                                                                       </graphGapMatchScoringParameter>
                                                                   </parameterSet>
                                                               </parameters>
                                                           </assessmentItem>

    Function DoSerialize(Of T)(obj As T) As XElement
        Dim s = New XmlSerializer(GetType(T))
        Dim ret As XElement = Nothing
        Using m As New StringWriter()
            s.Serialize(m, obj)
            ret = XElement.Parse(m.ToString())
        End Using
        Return ret
    End Function

    Protected Function Deserialize(Of T)(input As XElement) As T
        Dim ret As T
        Dim s = New XmlSerializer(GetType(T))

        Using m As New StringReader(input.ToString())
            ret = DirectCast(s.Deserialize(m), T)
        End Using

        Return ret
    End Function

    Sub WriteSolution(stateName As String, s As Solution)
        Dim a As New XmlSerializer(GetType(Solution))
        Debug.WriteLine(String.Empty)
        Debug.WriteLine(String.Format("WriteSolution for State [{0}]", stateName))
        Using stream = New StringWriter()
            a.Serialize(stream, s)
            Debug.WriteLine(stream.ToString())
        End Using
    End Sub
End Class
