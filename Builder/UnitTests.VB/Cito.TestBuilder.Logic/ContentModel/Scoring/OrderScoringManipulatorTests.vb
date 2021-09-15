
Imports System.Linq
Imports System.Diagnostics
Imports Questify.Builder.Logic.ContentModel.Scoring
Imports Cito.Tester.ContentModel
Imports System.Xml.Linq
Imports System.Xml.Serialization
Imports System.IO
Imports Questify.Builder.Logic.ContentModel

<TestClass>
Public Class OrderScoringManipulatorTests

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub GetKey_A_NoSolution()
        'Arrange
        Dim param = CreateOrderScoringParameter("A")
        Dim key = OrderKeyFinding("OC", False)
        Dim manipulator As IOrderScoringManipulator = New OrderScoringManipulator(GetKeyManipulator(key), param)
        
        'Act
        Dim res = manipulator.GetKeyStatus()
        
        'Assert
        Write("Assert", key)
        Assert.IsTrue(res.ContainsKey("A"))
        Assert.AreEqual(0, res("A"))
        Assert.AreEqual(1, res.Keys.Count)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub IdOfFactIsSetToIDOfParam()
        'Arrange
        Dim param = CreateOrderScoringParameter("A", "B")
        Dim key = OrderKeyFinding("OC", False)
        Dim manipulator As IOrderScoringManipulator = New OrderScoringManipulator(GetKeyManipulator(key), param)
        
        'Act
        manipulator.SetKey("B", 1)
        Dim result = DirectCast(key.Facts.First(), KeyFact)
        
        'Assert
        Write("Assert", key)
        Assert.AreEqual("B-OC", result.Id)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub DomainOfValueIsSetToIDOfParam()
        'Arrange
        Dim param = CreateOrderScoringParameter("A", "B")
        Dim key = OrderKeyFinding("OC", False)
        Dim manipulator As IOrderScoringManipulator = New OrderScoringManipulator(GetKeyManipulator(key), param)
        
        'Act
        manipulator.SetKey("B", 2)
        Dim result = DirectCast(DirectCast(key.Facts.First(), KeyFact).Values(0), KeyValue)
        
        'Assert
        Assert.AreEqual("OC", result.Domain)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub ClearDoesClearFinding()
        'Arrange
        Dim param = CreateOrderScoringParameter("A", "B")
        Dim key = OrderKeyFinding("OC", False)
        Dim manipulator As IOrderScoringManipulator = New OrderScoringManipulator(GetKeyManipulator(key), param)
        
        'Act
        manipulator.Clear()
        
        'Assert
        Assert.IsTrue(key.Facts.Count = 0 And key.KeyFactsets.Count = 0)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub CreateInvalidFinding_Test()
        'Arrange
        Dim param = CreateOrderScoringParameter("A", "B")

        Dim key = OrderKeyFinding(param.Value.First().Id, False, "A")

        Dim manipulator As IOrderScoringManipulator = New OrderScoringManipulator(GetKeyManipulator(key), param)
        manipulator.SetKey(param.Value(0).Id, 1)
        manipulator.SetKey(param.Value(1).Id, 1)

        'Act
        Dim isValidFinding = manipulator.IsValid(1)

        'Assert
        Assert.IsFalse(isValidFinding)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub CreateValidFinding_Test()
        'Arrange
        Dim param = CreateOrderScoringParameter("A", "B")

        Dim key = OrderKeyFinding(param.Value.First().Id, False, "A", "B")

        Dim manipulator As IOrderScoringManipulator = New OrderScoringManipulator(GetKeyManipulator(key), param)
        manipulator.SetKey(param.Value(1).Id, 1)

        'Act
        Dim isValidFinding = manipulator.IsValid(1)

        'Assert
        Assert.IsTrue(isValidFinding)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub IsConceptIdDeletableShouldReturnFalseForTheSecondConceptIdForThisSolution()
        'Arrange
        Dim sol As Solution = Deserialize(Of Solution)(_solutionWith2FindingFactsSetsAnd3ConceptFactSets, Nothing)
        Dim osp = Deserialize(Of OrderScoringParameter)(_orderScoreParam, New XmlRootAttribute("orderScoringParameter"))
        Dim scoreParamList As New List(Of ScoringParameter) From {osp}
        Dim combinedScoringMapKeys = New ScoringMap(scoreParamList, sol).GetMap().ToList()
        Dim combinedScoringMapKey As CombinedScoringMapKey = combinedScoringMapKeys.FirstOrDefault()
        Dim cm As IConceptScoreManipulator = combinedScoringMapKey.GetConceptManipulator(sol)

        Dim currentConceptIds = cm.GetConceptIds().ToList()
        Dim cid1 = String.Empty, cid2 = String.Empty, cid3 As String = String.Empty

        Dim idEnumerator As IEnumerator(Of String) = currentConceptIds.GetEnumerator()
        If idEnumerator.MoveNext Then cid1 = idEnumerator.Current
        If idEnumerator.MoveNext Then cid2 = idEnumerator.Current
        If idEnumerator.MoveNext Then cid3 = idEnumerator.Current

        'Act
        Dim CanDelete1 As Boolean = cm.IsConceptIdDeletable(cid1)
        Dim CanDelete2 As Boolean = cm.IsConceptIdDeletable(cid2)
        Dim CanDelete3 As Boolean = cm.IsConceptIdDeletable(cid3)

        'Assert
        Assert.AreEqual(False, CanDelete1)
        Assert.AreEqual(False, CanDelete2)
        Assert.AreEqual(True, CanDelete3)
    End Sub

    Private Function CreateOrderScoringParameter(ParamArray subIds As String()) As OrderScoringParameter
        Dim param = New OrderScoringParameter() With {.ControllerId = "OC"}
        param.Value = New ParameterSetCollection

        For Each subId As String In subIds
            Dim pc As New ParameterCollection() With {.Id = subId}

            pc.InnerParameters.Add(New PlainTextParameter() With {.Value = String.Format("Lable {0}", subId)})
            param.Value.Add(pc)
        Next

        Return param
    End Function

    Protected Function OrderKeyFinding(controller As String, useFactSet As Boolean, ParamArray keys As String()) As KeyFinding
        Dim ret As New KeyFinding()

        For Each k In keys
            Dim fact = New KeyFact(String.Format("{0}-{1}", k, controller)) With {.Score = 1}
            Dim keyVal = New KeyValue("OC", 1)
            fact.Values.Add(keyVal)
            keyVal.Values.Add(New IntegerValue(0))

            If useFactSet Then
                If ret.KeyFactsets.Count = 0 Then
                    ret.KeyFactsets.Add(New KeyFactSet())
                End If

                ret.KeyFactsets(0).Facts.Add(fact)
            Else
                ret.Facts.Add(fact)
            End If
        Next

        Return ret
    End Function

    Protected Sub Write(stateName As String, s As KeyFinding)
        Dim a As New XmlSerializer(GetType(KeyFinding))
        Debug.WriteLine(String.Empty)
        Debug.WriteLine(String.Format("Write KeyFinding for State [{0}]", stateName))
        Using stream = New StringWriter()
            a.Serialize(stream, s)

            Debug.WriteLine(stream.ToString())
        End Using
    End Sub

   Friend Overridable Function GetKeyManipulator(key As KeyFinding) As FindingManipulatorBase
        Return New KeyManipulator(key)
    End Function

    Private Function Deserialize(Of T)(input As XElement, rootattr As XmlRootAttribute) As T
        Dim ret As T
        Dim s As XmlSerializer
        If rootattr IsNot Nothing Then
            s = New XmlSerializer(GetType(T), rootattr)
        Else
            s = New XmlSerializer(GetType(T))
        End If

        Using m As New StringReader(input.ToString())
            ret = DirectCast(s.Deserialize(m), T)
        End Using

        Return ret
    End Function

    Private _solutionWith2FindingFactsSetsAnd3ConceptFactSets As XElement = <solution>
                                                                                <keyFindings>
                                                                                    <keyFinding id="orderFO" scoringMethod="Dichotomous">
                                                                                        <keyFactSet>
                                                                                            <keyFact id="A-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                                <keyValue domain="OC" occur="1">
                                                                                                    <integerValue>
                                                                                                        <typedValue>2</typedValue>
                                                                                                    </integerValue>
                                                                                                </keyValue>
                                                                                            </keyFact>
                                                                                            <keyFact id="B-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                                <keyValue domain="OC" occur="1">
                                                                                                    <integerValue>
                                                                                                        <typedValue>1</typedValue>
                                                                                                    </integerValue>
                                                                                                </keyValue>
                                                                                            </keyFact>
                                                                                            <keyFact id="C-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                                <keyValue domain="OC" occur="1">
                                                                                                    <integerValue>
                                                                                                        <typedValue>3</typedValue>
                                                                                                    </integerValue>
                                                                                                </keyValue>
                                                                                            </keyFact>
                                                                                        </keyFactSet>
                                                                                        <keyFactSet>
                                                                                            <keyFact id="A-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                                <keyValue domain="OC" occur="1">
                                                                                                    <integerValue>
                                                                                                        <typedValue>3</typedValue>
                                                                                                    </integerValue>
                                                                                                </keyValue>
                                                                                            </keyFact>
                                                                                            <keyFact id="B-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                                <keyValue domain="OC" occur="1">
                                                                                                    <integerValue>
                                                                                                        <typedValue>2</typedValue>
                                                                                                    </integerValue>
                                                                                                </keyValue>
                                                                                            </keyFact>
                                                                                            <keyFact id="C-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                                <keyValue domain="OC" occur="1">
                                                                                                    <integerValue>
                                                                                                        <typedValue>1</typedValue>
                                                                                                    </integerValue>
                                                                                                </keyValue>
                                                                                            </keyFact>
                                                                                        </keyFactSet>
                                                                                    </keyFinding>
                                                                                </keyFindings>
                                                                                <conceptFindings>
                                                                                    <conceptFinding id="orderFO" scoringMethod="None">
                                                                                        <conceptFactSet>
                                                                                            <conceptFact id="A-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                                <conceptValue domain="A-OC" occur="1">
                                                                                                    <integerValue>
                                                                                                        <typedValue>2</typedValue>
                                                                                                    </integerValue>
                                                                                                </conceptValue>
                                                                                            </conceptFact>
                                                                                            <conceptFact id="B-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                                <conceptValue domain="B-OC" occur="1">
                                                                                                    <integerValue>
                                                                                                        <typedValue>1</typedValue>
                                                                                                    </integerValue>
                                                                                                </conceptValue>
                                                                                            </conceptFact>
                                                                                            <conceptFact id="C-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                                <conceptValue domain="C-OC" occur="1">
                                                                                                    <integerValue>
                                                                                                        <typedValue>3</typedValue>
                                                                                                    </integerValue>
                                                                                                </conceptValue>
                                                                                            </conceptFact>
                                                                                            <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                                <concept value="1" code="1"/>
                                                                                            </concepts>
                                                                                        </conceptFactSet>
                                                                                        <conceptFactSet>
                                                                                            <conceptFact id="A[*]-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                                <conceptValue domain="A[*]-OC" occur="1">
                                                                                                    <catchAllValue/>
                                                                                                </conceptValue>
                                                                                            </conceptFact>
                                                                                            <conceptFact id="B[*]-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                                <conceptValue domain="B[*]-OC" occur="1">
                                                                                                    <catchAllValue/>
                                                                                                </conceptValue>
                                                                                            </conceptFact>
                                                                                            <conceptFact id="C[*]-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                                <conceptValue domain="C[*]-OC" occur="1">
                                                                                                    <catchAllValue/>
                                                                                                </conceptValue>
                                                                                            </conceptFact>
                                                                                            <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                                <concept value="3" code="1"/>
                                                                                            </concepts>
                                                                                        </conceptFactSet>
                                                                                        <conceptFactSet>
                                                                                            <conceptFact id="A-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                                <conceptValue domain="A-OC" occur="1">
                                                                                                    <integerValue>
                                                                                                        <typedValue>3</typedValue>
                                                                                                    </integerValue>
                                                                                                </conceptValue>
                                                                                            </conceptFact>
                                                                                            <conceptFact id="B-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                                <conceptValue domain="B-OC" occur="1">
                                                                                                    <integerValue>
                                                                                                        <typedValue>2</typedValue>
                                                                                                    </integerValue>
                                                                                                </conceptValue>
                                                                                            </conceptFact>
                                                                                            <conceptFact id="C-OC" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                                <conceptValue domain="C-OC" occur="1">
                                                                                                    <integerValue>
                                                                                                        <typedValue>1</typedValue>
                                                                                                    </integerValue>
                                                                                                </conceptValue>
                                                                                            </conceptFact>
                                                                                            <concepts xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                                <concept value="0" code="1"/>
                                                                                            </concepts>
                                                                                        </conceptFactSet>
                                                                                    </conceptFinding>
                                                                                </conceptFindings>
                                                                                <aspectReferences/>
                                                                                <ItemScoreTranslationTable>
                                                                                    <ItemScoreTranslationTableEntry rawScore="0" translatedScore="0"/>
                                                                                    <ItemScoreTranslationTableEntry rawScore="1" translatedScore="1"/>
                                                                                </ItemScoreTranslationTable>
                                                                            </solution>

    Dim _orderScoreParam As XElement = <orderScoringParameter name="order" ControllerId="OC" findingOverride="orderFO" minChoices="0" maxChoices="0">
                                           <subparameterset id="A">
                                               <plaintextparameter name="elementLabel">alinea 3</plaintextparameter>
                                               <xhtmlparameter name="movableElement">
                                                   <p id="c1-id-9">Moet op pos 3</p>
                                               </xhtmlparameter>
                                           </subparameterset>
                                           <subparameterset id="B">
                                               <plaintextparameter name="elementLabel">alinea 1</plaintextparameter>
                                               <xhtmlparameter name="movableElement">
                                                   <p id="c1-id-11">Moet op  pos 1</p>
                                               </xhtmlparameter>
                                           </subparameterset>
                                           <subparameterset id="C">
                                               <plaintextparameter name="elementLabel">alinea 2</plaintextparameter>
                                               <xhtmlparameter name="movableElement">
                                                   <p id="c1-id-13">Moet op pos 2</p>
                                               </xhtmlparameter>
                                           </subparameterset>
                                           <subparameterset id="D">
                                               <plaintextparameter name="elementLabel">alinea 4</plaintextparameter>
                                               <xhtmlparameter name="movableElement">
                                                   <p id="c1-id-15">Moet op pos 4</p>
                                               </xhtmlparameter>
                                           </subparameterset>
                                           <subparameterset id="E">
                                               <plaintextparameter name="elementLabel">alinea 5</plaintextparameter>
                                               <xhtmlparameter name="movableElement">
                                                   <p id="c1-id-16">Moet op pos 5</p>
                                               </xhtmlparameter>
                                           </subparameterset>
                                           <definition id="">
                                               <plaintextparameter name="elementLabel"/>
                                               <xhtmlparameter name="movableElement"/>
                                           </definition>
                                       </orderScoringParameter>

End Class
