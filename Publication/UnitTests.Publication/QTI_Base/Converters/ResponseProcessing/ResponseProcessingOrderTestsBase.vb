Imports Cito.Tester.ContentModel

Namespace QTI_Base

    Public MustInherit Class ResponseProcessingOrderTestsBase

        Protected Function GetOrderScoringParams() As HashSet(Of ScoringParameter)
            Dim scoreParams As New HashSet(Of ScoringParameter)
            scoreParams.Add(New OrderScoringParameter() With {.ControllerId = "orderController", .FindingOverride = "orderController"}.AddSubParameters("A", "B", "C"))
            Return scoreParams
        End Function

        Protected _finding1 As XElement =
            <keyFinding id="orderController" scoringMethod="Dichotomous">
                <keyFactSet>
                    <keyFact id="A-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="orderController" occur="1">
                            <integerValue>
                                <typedValue>1</typedValue>
                            </integerValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="B-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="orderController" occur="1">
                            <integerValue>
                                <typedValue>2</typedValue>
                            </integerValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="C-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="orderController" occur="1">
                            <integerValue>
                                <typedValue>3</typedValue>
                            </integerValue>
                        </keyValue>
                    </keyFact>
                </keyFactSet>
                <keyFactSet>
                    <keyFact id="A-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="orderController" occur="1">
                            <integerValue>
                                <typedValue>3</typedValue>
                            </integerValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="B-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="orderController" occur="1">
                            <integerValue>
                                <typedValue>2</typedValue>
                            </integerValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="C-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="orderController" occur="1">
                            <integerValue>
                                <typedValue>1</typedValue>
                            </integerValue>
                        </keyValue>
                    </keyFact>
                </keyFactSet>
            </keyFinding>

        Protected _finding2 As XElement =
            <keyFinding id="orderController" scoringMethod="Dichotomous">
                <keyFact id="A-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="orderController" occur="1">
                        <integerValue>
                            <typedValue>3</typedValue>
                        </integerValue>
                    </keyValue>
                </keyFact>
                <keyFact id="B-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="orderController" occur="1">
                        <integerValue>
                            <typedValue>2</typedValue>
                        </integerValue>
                    </keyValue>
                </keyFact>
                <keyFact id="C-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="orderController" occur="1">
                        <integerValue>
                            <typedValue>1</typedValue>
                        </integerValue>
                    </keyValue>
                </keyFact>
            </keyFinding>

        Protected _finding3 As XElement =
            <keyFinding id="orderController" scoringMethod="Polytomous">
                <keyFactSet>
                    <keyFact id="A-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="orderController" occur="1">
                            <integerValue>
                                <typedValue>1</typedValue>
                            </integerValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="B-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="orderController" occur="1">
                            <integerValue>
                                <typedValue>2</typedValue>
                            </integerValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="C-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="orderController" occur="1">
                            <integerValue>
                                <typedValue>3</typedValue>
                            </integerValue>
                        </keyValue>
                    </keyFact>
                </keyFactSet>
                <keyFactSet>
                    <keyFact id="A-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="orderController" occur="1">
                            <integerValue>
                                <typedValue>3</typedValue>
                            </integerValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="B-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="orderController" occur="1">
                            <integerValue>
                                <typedValue>2</typedValue>
                            </integerValue>
                        </keyValue>
                    </keyFact>
                    <keyFact id="C-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                        <keyValue domain="orderController" occur="1">
                            <integerValue>
                                <typedValue>1</typedValue>
                            </integerValue>
                        </keyValue>
                    </keyFact>
                </keyFactSet>
            </keyFinding>

        Protected _finding4 As XElement =
           <keyFinding id="orderController" scoringMethod="Polytomous">
               <keyFact id="A-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                   <keyValue domain="orderController" occur="1">
                       <integerValue>
                           <typedValue>3</typedValue>
                       </integerValue>
                   </keyValue>
               </keyFact>
               <keyFact id="B-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                   <keyValue domain="orderController" occur="1">
                       <integerValue>
                           <typedValue>2</typedValue>
                       </integerValue>
                   </keyValue>
               </keyFact>
               <keyFact id="C-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                   <keyValue domain="orderController" occur="1">
                       <integerValue>
                           <typedValue>1</typedValue>
                       </integerValue>
                   </keyValue>
               </keyFact>
           </keyFinding>

        Protected _finding5 As XElement =
            <keyFinding id="orderController" scoringMethod="Dichotomous">
                <keyFact id="C-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="orderController" occur="1">
                        <integerValue>
                            <typedValue>1</typedValue>
                        </integerValue>
                    </keyValue>
                </keyFact>
                <keyFact id="B-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="orderController" occur="1">
                        <integerValue>
                            <typedValue>2</typedValue>
                        </integerValue>
                    </keyValue>
                </keyFact>
                <keyFact id="A-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                    <keyValue domain="orderController" occur="1">
                        <integerValue>
                            <typedValue>3</typedValue>
                        </integerValue>
                    </keyValue>
                </keyFact>
            </keyFinding>

        Protected _finding6 As XElement =
           <keyFinding id="orderController" scoringMethod="Polytomous">
               <keyFact id="C-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                   <keyValue domain="orderController" occur="1">
                       <integerValue>
                           <typedValue>1</typedValue>
                       </integerValue>
                   </keyValue>
               </keyFact>
               <keyFact id="B-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                   <keyValue domain="orderController" occur="1">
                       <integerValue>
                           <typedValue>2</typedValue>
                       </integerValue>
                   </keyValue>
               </keyFact>
               <keyFact id="A-orderController" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                   <keyValue domain="orderController" occur="1">
                       <integerValue>
                           <typedValue>3</typedValue>
                       </integerValue>
                   </keyValue>
               </keyFact>
           </keyFinding>

    End Class

End Namespace