
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ContentModel.Scoring
Imports System.Xml.Serialization
Imports System.Diagnostics
Imports System.IO

<TestClass>
Public MustInherit Class BaseFindingToFindingManipulatorTests(Of TFinding As {KeyFinding})

#Region "Empty Finding"

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub CopyEmptyFinding()
        'Arrange
        Dim solution As New Solution()
        Dim keyFinding1 = GetKeyFindingOrMakeIt("Manipulator_1", solution)
        Dim manipulator_1 = New KeyManipulator(keyFinding1)
        Dim keyFinding2 = GetFindingOrMakeIt("Manipulator_2", solution)
        Dim manipulator_2 = New KeyManipulator(keyFinding2)
        Dim copier As New FindingToFindingManipulator(manipulator_1, manipulator_2)

        WriteSolution("Arrange", solution)

        'Act
        copier.Execute()
        'Assert
        WriteSolution("Assert", solution)

        Assert.AreEqual(0, keyFinding1.Facts.Count)
        Assert.AreEqual(0, keyFinding2.Facts.Count)
    End Sub

#End Region

#Region "Other finding has no data"

#Region "Single fact, single value "

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub CopyFindingWith1FactAnd1Value_DestinationShouldHave1KeyFact()
        'Arrange
        Dim solution As New Solution()
        Dim scoreParam = GetMCScoreParam(Of StringScoringParameter)("A", "B")
        Dim keyFinding1 = GetKeyFindingOrMakeIt("Manipulator_1", solution)
        Dim manipulator_1 = New KeyManipulator(keyFinding1)
        Dim specialized = New StringScoringManipulator(manipulator_1, scoreParam)
        Dim keyFinding2 = GetFindingOrMakeIt("Manipulator_2", solution)
        Dim manipulator_2 = New KeyManipulator(keyFinding2)
        Dim copier As New FindingToFindingManipulator(manipulator_1, manipulator_2)

        specialized.SetKey("A", "1")
        WriteSolution("Arrange", solution)

        'Act
        copier.Execute()
        
        'Assert
        WriteSolution("Assert", solution)

        Assert.AreEqual(1, keyFinding1.Facts.Count)
        Assert.AreEqual(1, keyFinding2.Facts.Count)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub CopyFindingWith1FactAnd1Value_DestinationShouldHaveValue()
        'Arrange
        Dim solution As New Solution()
        Dim scoreParam = GetMCScoreParam(Of StringScoringParameter)("A", "B")
        Dim keyFinding1 = GetKeyFindingOrMakeIt("Manipulator_1", solution)
        Dim manipulator_1 = New KeyManipulator(keyFinding1)
        Dim specialized = New StringScoringManipulator(manipulator_1, scoreParam)
        Dim keyFinding2 = GetFindingOrMakeIt("Manipulator_2", solution)
        Dim manipulator_2 = New KeyManipulator(keyFinding2)
        Dim copier As New FindingToFindingManipulator(manipulator_1, manipulator_2)

        specialized.SetKey("A", "1")
        WriteSolution("Arrange", solution)

        'Act
        copier.Execute()
        
        'Assert
        WriteSolution("Assert", solution)

        Assert.AreEqual(1, DirectCast(keyFinding2.Facts(0), KeyFact).Values.Count, "Precisely 1 KeyValue was expected")
        Assert.AreEqual(1, DirectCast(DirectCast(keyFinding2.Facts(0), KeyFact).Values(0), KeyValue).Values.Count, "Precisely 1 Value was expected")
        Assert.AreEqual("1", DirectCast(DirectCast(DirectCast(keyFinding2.Facts(0), KeyFact).Values(0), KeyValue).Values(0), StringValue).Value)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub CopyFindingWith1FactAnd1Value_DomainShouldBeEqual()
        'Arrange
        Dim solution As New Solution()
        Dim scoreParam = GetMCScoreParam(Of StringScoringParameter)("A", "B")
        Dim keyFinding1 = GetKeyFindingOrMakeIt("Manipulator_1", solution)
        Dim manipulator_1 = New KeyManipulator(keyFinding1)
        Dim specialized = New StringScoringManipulator(manipulator_1, scoreParam)
        Dim keyFinding2 = GetFindingOrMakeIt("Manipulator_2", solution)
        Dim manipulator_2 = New KeyManipulator(keyFinding2)
        Dim copier As New FindingToFindingManipulator(manipulator_1, manipulator_2)

        specialized.SetKey("A", "1")
        WriteSolution("Arrange", solution)

        'Act
        copier.Execute()
        'Assert
        WriteSolution("Assert", solution)

        Assert.AreEqual(1, DirectCast(keyFinding2.Facts(0), KeyFact).Values.Count, "Precisely 1 KeyValue was expected")
        Assert.AreEqual(DirectCast(DirectCast(keyFinding1.Facts(0), KeyFact).Values(0), KeyValue).Domain,
                        DirectCast(DirectCast(keyFinding2.Facts(0), KeyFact).Values(0), KeyValue).Domain, "Domain Should match original domain")

    End Sub

#End Region

#Region "Single fact, multiple value"

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub CopyFindingWith1FactAnd_Multiple_Value_DestinationShouldHave1KeyFact()
        'Arrange
        Dim solution As New Solution()
        Dim scoreParam = GetMCScoreParam(Of StringScoringParameter)("A", "B")
        Dim keyFinding1 = GetKeyFindingOrMakeIt("Manipulator_1", solution)
        Dim manipulator_1 = New KeyManipulator(keyFinding1)
        Dim specialized = New StringScoringManipulator(manipulator_1, scoreParam)
        Dim keyFinding2 = GetFindingOrMakeIt("Manipulator_2", solution)
        Dim manipulator_2 = New KeyManipulator(keyFinding2)
        Dim copier As New FindingToFindingManipulator(manipulator_1, manipulator_2)

        specialized.SetKey("A", "1", "The Story of", "Bla bla")
        WriteSolution("Arrange", solution)

        'Act
        copier.Execute()
        
        'Assert
        WriteSolution("Assert", solution)

        Assert.AreEqual(1, keyFinding1.Facts.Count)
        Assert.AreEqual(1, keyFinding2.Facts.Count)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub CopyFindingWith1FactAnd_Multiple_Value_DestinationShouldHaveValue()
        'Arrange
        Dim solution As New Solution()
        Dim scoreParam = GetMCScoreParam(Of StringScoringParameter)("A", "B")
        Dim keyFinding1 = GetKeyFindingOrMakeIt("Manipulator_1", solution)
        Dim manipulator_1 = New KeyManipulator(keyFinding1)
        Dim specialized = New StringScoringManipulator(manipulator_1, scoreParam)
        Dim keyFinding2 = GetFindingOrMakeIt("Manipulator_2", solution)
        Dim manipulator_2 = New KeyManipulator(keyFinding2)
        Dim copier As New FindingToFindingManipulator(manipulator_1, manipulator_2)

        specialized.SetKey("A", "1", "The Story of", "Bla bla")
        WriteSolution("Arrange", solution)

        'Act
        copier.Execute()
        
        'Assert
        WriteSolution("Assert", solution)

        Assert.AreEqual(1, DirectCast(keyFinding2.Facts(0), KeyFact).Values.Count, "Precisely 1 KeyValue was expected")
        Assert.AreEqual(3, DirectCast(DirectCast(keyFinding2.Facts(0), KeyFact).Values(0), KeyValue).Values.Count, "Precisely 1 Value was expected")
        Assert.AreEqual("1", DirectCast(DirectCast(DirectCast(keyFinding2.Facts(0), KeyFact).Values(0), KeyValue).Values(0), StringValue).Value)
        Assert.AreEqual("The Story of", DirectCast(DirectCast(DirectCast(keyFinding2.Facts(0), KeyFact).Values(0), KeyValue).Values(1), StringValue).Value)
        Assert.AreEqual("Bla bla", DirectCast(DirectCast(DirectCast(keyFinding2.Facts(0), KeyFact).Values(0), KeyValue).Values(2), StringValue).Value)
    End Sub

#End Region

#Region "Multiple facts, single value"

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub CopyFindingWith3Facts_And1Value_DestinationShouldHave3KeyFact()
        'Arrange
        Dim solution As New Solution()
        Dim scoreParam = GetMCScoreParam(Of StringScoringParameter)("A", "B")
        Dim keyFinding1 = GetKeyFindingOrMakeIt("Manipulator_1", solution)
        Dim manipulator_1 = New KeyManipulator(keyFinding1)
        Dim specialized = New StringScoringManipulator(manipulator_1, scoreParam)
        Dim keyFinding2 = GetFindingOrMakeIt("Manipulator_2", solution)
        Dim manipulator_2 = New KeyManipulator(keyFinding2)
        Dim copier As New FindingToFindingManipulator(manipulator_1, manipulator_2)

        specialized.SetKey("A", "3")
        specialized.SetKey("B", "1")
        specialized.SetKey("C", "4")
        WriteSolution("Arrange", solution)

        'Act
        copier.Execute()
        
        'Assert
        WriteSolution("Assert", solution)

        Assert.AreEqual(3, keyFinding1.Facts.Count)
        Assert.AreEqual(3, keyFinding2.Facts.Count)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub CopyFindingWith3Facts_And1Value_DestinationShouldHaveValue()
        'Arrange
        Dim solution As New Solution()
        Dim scoreParam = GetMCScoreParam(Of StringScoringParameter)("A", "B")
        Dim keyFinding1 = GetKeyFindingOrMakeIt("Manipulator_1", solution)
        Dim manipulator_1 = New KeyManipulator(keyFinding1)
        Dim specialized = New StringScoringManipulator(manipulator_1, scoreParam)
        Dim keyFinding2 = GetFindingOrMakeIt("Manipulator_2", solution)
        Dim manipulator_2 = New KeyManipulator(keyFinding2)
        Dim copier As New FindingToFindingManipulator(manipulator_1, manipulator_2)

        specialized.SetKey("A", "1")
        specialized.SetKey("B", "2")
        specialized.SetKey("C", "3")

        WriteSolution("Arrange", solution)

        'Act
        copier.Execute()
        
        'Assert
        WriteSolution("Assert", solution)

        Dim factNr As Integer
        For factNr = 0 To 2
            Assert.AreEqual(1, DirectCast(keyFinding2.Facts(factNr), KeyFact).Values.Count, "Precisely 1 KeyValue was expected")
            Assert.AreEqual(1, DirectCast(DirectCast(keyFinding2.Facts(factNr), KeyFact).Values(0), KeyValue).Values.Count, "Precisely 1 Value was expected")
            Assert.AreEqual((factNr + 1).ToString(), DirectCast(DirectCast(DirectCast(keyFinding2.Facts(factNr), KeyFact).Values(0), KeyValue).Values(0), StringValue).Value)
        Next
    End Sub

#End Region

#Region "Multiple facts, Multiple value"

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub CopyFindingWith3Facts_And4Value_DestinationShouldHave3KeyFact()
        'Arrange
        Dim solution As New Solution()
        Dim scoreParam = GetMCScoreParam(Of StringScoringParameter)("A", "B")
        Dim keyFinding1 = GetKeyFindingOrMakeIt("Manipulator_1", solution)
        Dim manipulator_1 = New KeyManipulator(keyFinding1)
        Dim specialized = New StringScoringManipulator(manipulator_1, scoreParam)
        Dim keyFinding2 = GetFindingOrMakeIt("Manipulator_2", solution)
        Dim manipulator_2 = New KeyManipulator(keyFinding2)
        Dim copier As New FindingToFindingManipulator(manipulator_1, manipulator_2)

        specialized.SetKey("A", "1", "10", "100", "1000")
        specialized.SetKey("B", "2", "20", "200", "2000")
        specialized.SetKey("C", "3", "30", "300", "3000")
        WriteSolution("Arrange", solution)

        'Act
        copier.Execute()
        
        'Assert
        WriteSolution("Assert", solution)

        Assert.AreEqual(3, keyFinding1.Facts.Count)
        Assert.AreEqual(3, keyFinding2.Facts.Count)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub CopyFindingWith3Facts_And4Value_DestinationShouldHaveValue()
        'Arrange
        Dim solution As New Solution()
        Dim scoreParam = GetMCScoreParam(Of StringScoringParameter)("A", "B")
        Dim keyFinding1 = GetKeyFindingOrMakeIt("Manipulator_1", solution)
        Dim manipulator_1 = New KeyManipulator(keyFinding1)
        Dim specialized = New StringScoringManipulator(manipulator_1, scoreParam)
        Dim keyFinding2 = GetFindingOrMakeIt("Manipulator_2", solution)
        Dim manipulator_2 = New KeyManipulator(keyFinding2)
        Dim copier As New FindingToFindingManipulator(manipulator_1, manipulator_2)

        specialized.SetKey("A", "1", "10", "100", "1000")
        specialized.SetKey("B", "2", "20", "200", "2000")
        specialized.SetKey("C", "3", "30", "300", "3000")

        WriteSolution("Arrange", solution)

        'Act
        copier.Execute()
        
        'Assert
        WriteSolution("Assert", solution)

        Dim factNr As Integer
        For factNr = 0 To 2
            Assert.AreEqual(1, DirectCast(keyFinding2.Facts(factNr), KeyFact).Values.Count, "Precisely 1 KeyValue was expected")
            Assert.AreEqual(4, DirectCast(DirectCast(keyFinding2.Facts(factNr), KeyFact).Values(0), KeyValue).Values.Count, "Precisely 1 Value was expected")
            Dim valueNr As Integer
            For valueNr = 0 To 3

                Assert.AreEqual(((factNr + 1) * Math.Pow(10, valueNr)).ToString(), DirectCast(DirectCast(DirectCast(keyFinding2.Facts(factNr), KeyFact).Values(0), KeyValue).Values(valueNr), StringValue).Value)

            Next
        Next
    End Sub

#End Region

#Region "Fact Sets"

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub CopyFindingWithFactSet()
        'Arrange
        Dim solution As New Solution()
        Dim scoreParam = GetMCScoreParam(Of StringScoringParameter)("A", "B")

        Dim keyFinding1 = GetKeyFindingOrMakeIt("Manipulator_1", solution)
        keyFinding1.KeyFactsets.Add(New KeyFactSet())
        Dim specialized = New StringScoringManipulator(New KeyManipulator(keyFinding1), scoreParam)
        specialized.SetFactSetTarget(0)

        Dim keyFinding2 = GetFindingOrMakeIt("Manipulator_2", solution)

        Dim manipulator_2 = New KeyManipulator(keyFinding2)
        Dim copier As New FindingToFindingManipulator(New KeyManipulator(keyFinding1), manipulator_2)

        specialized.SetKey("A", "1")
        WriteSolution("Arrange", solution)

        'Act
        copier.Execute()
        
        'Assert
        WriteSolution("Assert", solution)

        Assert.AreEqual(1, keyFinding1.KeyFactsets.Count)
        Assert.AreEqual(1, keyFinding2.KeyFactsets.Count)
    End Sub

#End Region

#End Region

#Region "Other finding HAS data"

#Region "Non_overlapping data"

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub CopyFindingWith1FactAnd1Value_Destination_HasNonOverlappingKey_DestinationShouldHave2KeyFacts()
        'Arrange
        Dim solution As New Solution()
        Dim keyFinding1 = GetKeyFindingOrMakeIt("Manipulator_1", solution)
        Dim manipulator_1 = New KeyManipulator(keyFinding1)
        Dim keyFinding2 = GetFindingOrMakeIt("Manipulator_2", solution)
        Dim manipulator_2 = New KeyManipulator(keyFinding2)
        Dim copier As New FindingToFindingManipulator(manipulator_1, manipulator_2)

        manipulator_1.SetKeyWithOptionals("A", "1")
        manipulator_2.SetKeyWithOptionals("B", "1")

        WriteSolution("Arrange", solution)
        
        'Act
        copier.Execute()

        'Assert
        WriteSolution("Assert", solution)

        Assert.AreEqual(1, keyFinding1.Facts.Count)
        Assert.AreEqual(2, keyFinding2.Facts.Count)
    End Sub


    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub CopyFindingWith1FactAnd1Value_DestinationHasData_DataIsIntact()
        'Arrange
        Dim solution As New Solution()
        Dim keyFinding1 = GetKeyFindingOrMakeIt("Manipulator_1", solution)
        Dim manipulator_1 = New KeyManipulator(keyFinding1)
        Dim keyFinding2 = GetFindingOrMakeIt("Manipulator_2", solution)
        Dim manipulator_2 = New KeyManipulator(keyFinding2)
        Dim copier As New FindingToFindingManipulator(manipulator_1, manipulator_2)

        manipulator_1.SetKeyWithOptionals("A", "1")
        'Already existing
        manipulator_2.SetKeyWithOptionals("B", "X")

        WriteSolution("Arrange", solution)
        
        'Act
        copier.Execute()

        'Assert
        WriteSolution("Assert", solution)

        Assert.AreEqual(2, keyFinding2.Facts.Count, "Precisely 2 Facts are expected")

        'The fact is added, thus the existing fact is first
        Assert.AreEqual(1, DirectCast(DirectCast(keyFinding2.Facts(0), KeyFact).Values(0), KeyValue).Values.Count, "Precisely 1 Value was expected")
        Assert.AreEqual("B", DirectCast(keyFinding2.Facts(0), KeyFact).Id)
        Assert.AreEqual(1, DirectCast(DirectCast(keyFinding2.Facts(0), KeyFact).Values(0), KeyValue).Values.Count, "Should still have 1 value")
        Assert.AreEqual("X", DirectCast(DirectCast(DirectCast(keyFinding2.Facts(0), KeyFact).Values(0), KeyValue).Values(0), StringValue).Value)

        'The seccond fact:
        Assert.AreEqual(1, DirectCast(DirectCast(keyFinding2.Facts(1), KeyFact).Values(0), KeyValue).Values.Count, "Precisely 1 Value was expected")
        Assert.AreEqual("A", DirectCast(keyFinding2.Facts(1), KeyFact).Id)
        Assert.AreEqual(1, DirectCast(DirectCast(keyFinding2.Facts(1), KeyFact).Values(0), KeyValue).Values.Count, "Should have 1 value")
        Assert.AreEqual("1", DirectCast(DirectCast(DirectCast(keyFinding2.Facts(1), KeyFact).Values(0), KeyValue).Values(0), StringValue).Value)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub CopyFindingWith1FactAndMultipleValue_DestinationHasData_DataIsIntact()
        'Arrange
        Dim solution As New Solution()
        Dim keyFinding1 = GetKeyFindingOrMakeIt("Manipulator_1", solution)
        Dim manipulator_1 = New KeyManipulator(keyFinding1)
        Dim keyFinding2 = GetFindingOrMakeIt("Manipulator_2", solution)
        Dim manipulator_2 = New KeyManipulator(keyFinding2)
        Dim copier As New FindingToFindingManipulator(manipulator_1, manipulator_2)

        manipulator_1.SetKeyWithOptionals("A", "1", "2")
        'Already existing
        manipulator_2.SetKeyWithOptionals("B", "X", "Y")

        WriteSolution("Arrange", solution)
        
        'Act
        copier.Execute()

        'Assert
        WriteSolution("Assert", solution)

        Assert.AreEqual(2, keyFinding2.Facts.Count, "Precisely 2 Facts are expected")

        'The fact is added, thus the existing fact is first
        Assert.AreEqual(2, DirectCast(DirectCast(keyFinding2.Facts(0), KeyFact).Values(0), KeyValue).Values.Count, "Precisely 2 Value was expected")
        Assert.AreEqual("B", DirectCast(keyFinding2.Facts(0), KeyFact).Id)
        Assert.AreEqual(2, DirectCast(DirectCast(keyFinding2.Facts(0), KeyFact).Values(0), KeyValue).Values.Count, "Should still have 1 value")
        Assert.AreEqual("X", DirectCast(DirectCast(DirectCast(keyFinding2.Facts(0), KeyFact).Values(0), KeyValue).Values(0), StringValue).Value)
        Assert.AreEqual("Y", DirectCast(DirectCast(DirectCast(keyFinding2.Facts(0), KeyFact).Values(0), KeyValue).Values(1), StringValue).Value)

        'The seccond fact:
        Assert.AreEqual(2, DirectCast(DirectCast(keyFinding2.Facts(1), KeyFact).Values(0), KeyValue).Values.Count, "Precisely 2 Value was expected")
        Assert.AreEqual("A", DirectCast(keyFinding2.Facts(1), KeyFact).Id)
        Assert.AreEqual(2, DirectCast(DirectCast(keyFinding2.Facts(1), KeyFact).Values(0), KeyValue).Values.Count, "Should have 1 value")
        Assert.AreEqual("1", DirectCast(DirectCast(DirectCast(keyFinding2.Facts(1), KeyFact).Values(0), KeyValue).Values(0), StringValue).Value)
        Assert.AreEqual("2", DirectCast(DirectCast(DirectCast(keyFinding2.Facts(1), KeyFact).Values(0), KeyValue).Values(1), StringValue).Value)
    End Sub

#End Region

#Region "Non_overlapping data"

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub CopyFindingWith1FactAnd1Value_Destination_HasOverlappingKey_DestinationShouldHave2KeyFacts()
        'Arrange
        Dim solution As New Solution()
        Dim keyFinding1 = GetKeyFindingOrMakeIt("Manipulator_1", solution)
        Dim manipulator_1 = New KeyManipulator(keyFinding1)
        Dim keyFinding2 = GetFindingOrMakeIt("Manipulator_2", solution)
        Dim manipulator_2 = New KeyManipulator(keyFinding2)
        Dim copier As New FindingToFindingManipulator(manipulator_1, manipulator_2)

        manipulator_1.SetKeyWithOptionals("A", "1")
        manipulator_2.SetKeyWithOptionals("A", "1")

        WriteSolution("Arrange", solution)
        
        'Act
        copier.Execute()

        'Assert
        WriteSolution("Assert", solution)

        Assert.AreEqual(1, keyFinding1.Facts.Count)
        Assert.AreEqual(1, keyFinding2.Facts.Count)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub CopyFindingWith1FactAnd1Value_Destination_HasOverlappingKey_DataIsIntact()
        'Arrange
        Dim solution As New Solution()
        Dim keyFinding1 = GetKeyFindingOrMakeIt("Manipulator_1", solution)
        Dim manipulator_1 = New KeyManipulator(keyFinding1)
        Dim keyFinding2 = GetFindingOrMakeIt("Manipulator_2", solution)
        Dim manipulator_2 = New KeyManipulator(keyFinding2)
        Dim copier As New FindingToFindingManipulator(manipulator_1, manipulator_2)

        manipulator_1.SetKeyWithOptionals("A", "1")
        'Already existing
        manipulator_2.SetKeyWithOptionals("A", "X")

        WriteSolution("Arrange", solution)
        
        'Act
        copier.Execute()

        'Assert
        WriteSolution("Assert", solution)

        Assert.AreEqual(1, keyFinding2.Facts.Count, "Precisely 1 Fact is expected")

        'The fact is added, thus the existing fact is first
        Assert.AreEqual("A", DirectCast(keyFinding2.Facts(0), KeyFact).Id)
        Assert.AreEqual(1, DirectCast(DirectCast(keyFinding2.Facts(0), KeyFact).Values(0), KeyValue).Values.Count, "Precisely 1 Value was expected")
        Assert.AreEqual(1, DirectCast(DirectCast(keyFinding2.Facts(0), KeyFact).Values(0), KeyValue).Values.Count, "Other value should have been overwritten")
        Assert.AreEqual("1", DirectCast(DirectCast(DirectCast(keyFinding2.Facts(0), KeyFact).Values(0), KeyValue).Values(0), StringValue).Value)
    End Sub

    <TestMethod(), TestCategory("Logic"), TestCategory("Scoring")>
    Public Sub CopyFindingWith1FactAndMultipleValue_Destination_HasOverlappingKey_DataIsIntact()
        'Arrange
        Dim solution As New Solution()
        Dim keyFinding1 = GetKeyFindingOrMakeIt("Manipulator_1", solution)
        Dim manipulator_1 = New KeyManipulator(keyFinding1)
        Dim keyFinding2 = GetFindingOrMakeIt("Manipulator_2", solution)
        Dim manipulator_2 = New KeyManipulator(keyFinding2)
        Dim copier As New FindingToFindingManipulator(manipulator_1, manipulator_2)

        manipulator_1.SetKeyWithOptionals("A", "1", "2")
        'Already existing
        manipulator_2.SetKeyWithOptionals("A", "X", "Y")

        WriteSolution("Arrange", solution)
        
        'Act
        copier.Execute()

        'Assert
        WriteSolution("Assert", solution)

        Assert.AreEqual(1, keyFinding2.Facts.Count, "Precisely 1 Fact is expected")

        'The fact is added, thus the existing fact is first
        Assert.AreEqual("A", DirectCast(keyFinding2.Facts(0), KeyFact).Id)
        Assert.AreEqual(2, DirectCast(DirectCast(keyFinding2.Facts(0), KeyFact).Values(0), KeyValue).Values.Count, "Precisely 2 Value was expected")
        Assert.AreEqual("1", DirectCast(DirectCast(DirectCast(keyFinding2.Facts(0), KeyFact).Values(0), KeyValue).Values(0), StringValue).Value)
        Assert.AreEqual("2", DirectCast(DirectCast(DirectCast(keyFinding2.Facts(0), KeyFact).Values(0), KeyValue).Values(1), StringValue).Value)
    End Sub

#End Region

#End Region

    Private Function GetMCScoreParam(Of T As {ScoringParameter, New})(id As String, ParamArray ids As String()) As T
        Dim param = New T() With {.ControllerId = id}
        param.Value = New ParameterSetCollection

        For Each id In ids
            param.Value.Add(New ParameterCollection() With {.Id = id})
        Next

        Return param
    End Function

    Private Function GetKeyFindingOrMakeIt(id As String, solution As Solution) As KeyFinding
        For Each kf In solution.Findings
            If (kf.Id = id) Then
                Return kf
            End If
        Next
        'Not found 
        Dim ret = New KeyFinding(id)
        solution.Findings.Add(ret)
        Return ret
    End Function

    Private Function GetFindingOrMakeIt(id As String, solution As Solution) As TFinding
        For Each kf In solution.Findings
            If (kf.Id = id) Then
                Return DirectCast(kf, TFinding)
            End If
        Next
        'Not found 
        Dim ret = CreateFinding(id)
        solution.Findings.Add(ret)
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

    Protected MustOverride Function CreateFinding(id As String) As TFinding

End Class

