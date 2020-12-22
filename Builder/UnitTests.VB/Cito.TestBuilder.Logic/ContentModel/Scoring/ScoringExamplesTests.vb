
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ContentModel
Imports System.Xml.Serialization
Imports System.IO
Imports System.Diagnostics
Imports System.Xml.Linq
Imports Questify.Builder.UnitTests.Framework

<TestClass>
Public Class ScoringExamplesTests

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub Example_InlineId()
        Dim solution As Solution = New Solution()
        Dim param = New MultiChoiceScoringParameter() With {.BluePrint = New ParameterCollection(),
                                                            .Value = New ParameterSetCollection(),
                                                            .InlineId = "XYZ", .MaxChoices = 1}
        param.Value.Add(New ParameterCollection() With {.Id = "A"})
        param.Value.Add(New ParameterCollection() With {.Id = "B"})
        param.Value.Add(New ParameterCollection() With {.Id = "C"})

        Dim manipulator = param.GetScoreManipulator(solution)
        manipulator.SetKey("B")

        Write(Of MultiChoiceScoringParameter)("param", param)
        Write(Of Solution)("solution", solution)

        Dim expect = Data_Inline.ToString()
        Dim result = DoSerialize(solution).ToString()

        Assert.IsTrue(UnitTestHelper.AreSame(expect, result))
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub Example_FindingOverride()
        Dim solution As New Solution()
        Dim param = New MultiChoiceScoringParameter() With {.BluePrint = New ParameterCollection(),
                                                            .Value = New ParameterSetCollection(),
                                                            .FindingOverride = "ABC", .MaxChoices = 1}
        param.Value.Add(New ParameterCollection() With {.Id = "A"})
        param.Value.Add(New ParameterCollection() With {.Id = "B"})
        param.Value.Add(New ParameterCollection() With {.Id = "C"})

        Dim manipulator = param.GetScoreManipulator(solution)
        manipulator.SetKey("B")

        Write(Of MultiChoiceScoringParameter)("param", param)
        Write(Of Solution)("solution", solution)

        Dim expect = Date_FindingOverride.ToString()
        Dim result = DoSerialize(solution).ToString()

        Assert.IsTrue(UnitTestHelper.AreSame(expect, result))
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub Example_Combo_Finding_FindingOverride()
        Dim solution As New Solution()
        Dim param = New MultiChoiceScoringParameter() With {.BluePrint = New ParameterCollection(),
                                                            .Value = New ParameterSetCollection(),
                                                            .InlineId = "INLINE", .FindingOverride = "OVERRIDE", .MaxChoices = 1
                                                           }
        param.Value.Add(New ParameterCollection() With {.Id = "A"})
        param.Value.Add(New ParameterCollection() With {.Id = "B"})
        param.Value.Add(New ParameterCollection() With {.Id = "C"})

        Dim manipulator = param.GetScoreManipulator(solution)
        manipulator.SetKey("B")

        Write(Of MultiChoiceScoringParameter)("param", param)
        Write(Of Solution)("solution", solution)

        Dim expect = Data_InlineAndFinding.ToString()
        Dim result = DoSerialize(solution).ToString()

        Assert.IsTrue(UnitTestHelper.AreSame(expect, result))
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub Example_ControllerId()
        Dim solution As Solution = New Solution()
        Dim param = New MultiChoiceScoringParameter() With {.BluePrint = New ParameterCollection(),
                                                            .Value = New ParameterSetCollection(),
                                                            .ControllerId = "Controller", .MaxChoices = 1
                                                           }
        param.Value.Add(New ParameterCollection() With {.Id = "A"})
        param.Value.Add(New ParameterCollection() With {.Id = "B"})
        param.Value.Add(New ParameterCollection() With {.Id = "C"})

        Dim manipulator = param.GetScoreManipulator(solution)
        manipulator.SetKey("B")

        Write(Of MultiChoiceScoringParameter)("param", param)
        Write(Of Solution)("solution", solution)

        Dim expect = Data_ControllerId.ToString()
        Dim result = DoSerialize(solution).ToString()

        Assert.IsTrue(UnitTestHelper.AreSame(expect, result))
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub Example_ControllerId_And_Finding()
        Dim solution = New Solution()
        Dim param = New MultiChoiceScoringParameter() With {.BluePrint = New ParameterCollection(),
                                                    .Value = New ParameterSetCollection(),
                                                    .ControllerId = "Controller",
                                                    .FindingOverride = "OVERRIDE", .MaxChoices = 1
                                                   }
        param.Value.Add(New ParameterCollection() With {.Id = "A"})
        param.Value.Add(New ParameterCollection() With {.Id = "B"})
        param.Value.Add(New ParameterCollection() With {.Id = "C"})

        Dim manipulator = param.GetScoreManipulator(solution)
        manipulator.SetKey("B")

        Write(Of MultiChoiceScoringParameter)("param", param)
        Write(Of Solution)("solution", solution)

        Dim expect = Data_ControllerIDAndFinding.ToString()
        Dim result = DoSerialize(solution).ToString()

        Assert.IsTrue(UnitTestHelper.AreSame(expect, result))
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub Example_ControllerId_And_Inline()
        Dim solution = New Solution()
        Dim param = New MultiChoiceScoringParameter() With {.BluePrint = New ParameterCollection(),
                                                    .Value = New ParameterSetCollection(),
                                                    .ControllerId = "Controller",
                                                    .InlineId = "Inline", .MaxChoices = 1
                                                   }
        param.Value.Add(New ParameterCollection() With {.Id = "A"})
        param.Value.Add(New ParameterCollection() With {.Id = "B"})
        param.Value.Add(New ParameterCollection() With {.Id = "C"})

        Dim manipulator = param.GetScoreManipulator(solution)
        manipulator.SetKey("B")

        Write(Of MultiChoiceScoringParameter)("param", param)
        Write(Of Solution)("solution", solution)

        Dim expect = Data_ControllerIDAndInlineId.ToString()
        Dim result = DoSerialize(solution).ToString()

        Assert.IsTrue(UnitTestHelper.AreSame(expect, result))
    End Sub



    Private Data_Inline As XElement = <solution>
                                          <keyFindings>
                                              <keyFinding id="XYZ" scoringMethod="None">
                                                  <keyFact id="B-XYZ" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                      <keyValue domain="XYZ" occur="1">
                                                          <stringValue>
                                                              <typedValue>B</typedValue>
                                                          </stringValue>
                                                      </keyValue>
                                                  </keyFact>
                                              </keyFinding>
                                          </keyFindings>
                                          <aspectReferences/>
                                      </solution>

    Private Date_FindingOverride As XElement = <solution>
                                                   <keyFindings>
                                                       <keyFinding id="ABC" scoringMethod="None">
                                                           <keyFact id="B" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                               <keyValue occur="1">
                                                                   <stringValue>
                                                                       <typedValue>B</typedValue>
                                                                   </stringValue>
                                                               </keyValue>
                                                           </keyFact>
                                                       </keyFinding>
                                                   </keyFindings>
                                                   <aspectReferences/>
                                               </solution>

    Private Data_InlineAndFinding As XElement = <solution>
                                                    <keyFindings>
                                                        <keyFinding id="OVERRIDE" scoringMethod="None">
                                                            <keyFact id="B-INLINE" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                <keyValue domain="INLINE" occur="1">
                                                                    <stringValue>
                                                                        <typedValue>B</typedValue>
                                                                    </stringValue>
                                                                </keyValue>
                                                            </keyFact>
                                                        </keyFinding>
                                                    </keyFindings>
                                                    <aspectReferences/>
                                                </solution>

    Private Data_ControllerId As XElement = <solution>
                                                <keyFindings>
                                                    <keyFinding id="Controller" scoringMethod="None">
                                                        <keyFact id="B-Controller" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                            <keyValue domain="Controller" occur="1">
                                                                <stringValue>
                                                                    <typedValue>B</typedValue>
                                                                </stringValue>
                                                            </keyValue>
                                                        </keyFact>
                                                    </keyFinding>
                                                </keyFindings>
                                                <aspectReferences/>
                                            </solution>

    Private Data_ControllerIDAndFinding As XElement = <solution>
                                                          <keyFindings>
                                                              <keyFinding id="OVERRIDE" scoringMethod="None">
                                                                  <keyFact id="B-Controller" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                      <keyValue domain="Controller" occur="1">
                                                                          <stringValue>
                                                                              <typedValue>B</typedValue>
                                                                          </stringValue>
                                                                      </keyValue>
                                                                  </keyFact>
                                                              </keyFinding>
                                                          </keyFindings>
                                                          <aspectReferences/>
                                                      </solution>

    Private Data_ControllerIDAndInlineId As XElement = <solution>
                                                           <keyFindings>
                                                               <keyFinding id="Inline" scoringMethod="None">
                                                                   <keyFact id="B-Inline" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                       <keyValue domain="Inline" occur="1">
                                                                           <stringValue>
                                                                               <typedValue>B</typedValue>
                                                                           </stringValue>
                                                                       </keyValue>
                                                                   </keyFact>
                                                               </keyFinding>
                                                           </keyFindings>
                                                           <aspectReferences/>
                                                       </solution>



    Function DoSerialize(Of T)(obj As T) As XElement
        Dim s = New XmlSerializer(GetType(T))
        Dim ns = New XmlSerializerNamespaces : ns.Add(String.Empty, String.Empty)

        Dim ret As XElement = Nothing
        Using m As New StringWriter()
            s.Serialize(m, obj, ns)
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

    Sub Write(Of T)(stateName As String, obj As T)
        Dim a As New XmlSerializer(GetType(T))
        Debug.WriteLine(String.Empty)
        Debug.WriteLine(String.Format("WriteSolution for State [{0}]", stateName))
        Using stream = New StringWriter()
            a.Serialize(stream, obj)
            Debug.WriteLine(stream.ToString())
        End Using
    End Sub

End Class