
Imports System.Xml.Linq

''' <summary>
''' These test exist to discover old behavior.
''' </summary>
<TestClass>
Public Class NewOnlySolutionScoringInfoTests
    Inherits ScoringTestBase

#Region "New MR tests"

    'Since MR is saving correct and incorrect answers, we want to validate certain fields. 
    'Please note that where false is the correct answer, the score = 0.

    <TestMethod, TestCategory("ResponseAndScoringModel")>
    Public Sub GetMaxRawScoreFromExample_Expects_2()
        'Arrange

        'Act
        Dim solution = toSolution(NewMRSituation_AB_combined_C_D_loose)

        'Assert
        Assert.AreEqual(2, solution.MaxSolutionRawScore)
    End Sub

#End Region

#Region "Data"
    ' A = true              A = false
    '               or
    ' B = false             B = true      
    ' -------------------------------------------------
    ' C = true
    ' -------------------------------------------------
    ' D = false
    ReadOnly NewMRSituation_AB_combined_C_D_loose As XElement = <solution xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                                                                    <keyFindings>
                                                                        <keyFinding id="mc" scoringMethod="Polytomous">

                                                                            <keyFact id="C-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                <keyValue domain="C-mc" occur="1">
                                                                                    <booleanValue>
                                                                                        <typedValue>true</typedValue>
                                                                                    </booleanValue>
                                                                                </keyValue>
                                                                            </keyFact>

                                                                            <keyFact id="D-mc" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                <keyValue domain="D-mc" occur="1">
                                                                                    <booleanValue>
                                                                                        <typedValue>false</typedValue>
                                                                                    </booleanValue>
                                                                                </keyValue>
                                                                            </keyFact>

                                                                            <keyFactSet>
                                                                                <keyFact id="A-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                    <keyValue domain="A-mc" occur="1">
                                                                                        <booleanValue>
                                                                                            <typedValue>true</typedValue>
                                                                                        </booleanValue>
                                                                                    </keyValue>
                                                                                </keyFact>
                                                                                <keyFact id="B-mc" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                    <keyValue domain="B-mc" occur="1">
                                                                                        <booleanValue>
                                                                                            <typedValue>false</typedValue>
                                                                                        </booleanValue>
                                                                                    </keyValue>
                                                                                </keyFact>
                                                                            </keyFactSet>

                                                                            <keyFactSet>
                                                                                <keyFact id="A-mc" score="0" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                    <keyValue domain="A-mc" occur="1">
                                                                                        <booleanValue>
                                                                                            <typedValue>false</typedValue>
                                                                                        </booleanValue>
                                                                                    </keyValue>
                                                                                </keyFact>
                                                                                <keyFact id="B-mc" score="1" xmlns="http://Cito.Tester.Server/xml/serialization">
                                                                                    <keyValue domain="B-mc" occur="1">
                                                                                        <booleanValue>
                                                                                            <typedValue>true</typedValue>
                                                                                        </booleanValue>
                                                                                    </keyValue>
                                                                                </keyFact>
                                                                            </keyFactSet>

                                                                        </keyFinding>
                                                                    </keyFindings>
                                                                    <aspectReferences/>
                                                                </solution>

#End Region

End Class
