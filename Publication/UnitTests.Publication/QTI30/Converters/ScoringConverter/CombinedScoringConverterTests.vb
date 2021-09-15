Imports System.Xml
Imports Cito.Tester.ContentModel
Imports FakeItEasy
Imports FluentAssertions
Imports Questify.Builder.Logic
Imports Questify.Builder.Logic.QTI.Converters.ScoringConverter.QTI30
Imports Questify.Builder.Logic.QTI.Xsd.QTI30

Namespace QTI30

    <TestClass>
    Public Class CombinedScoringConverterTests
        Private Const ASPECTMAXSCORE As Integer = 5

        <TestMethod>
        Public Sub AspectWithoutScoringTranslationTable_ShouldNotHaveInterpolationTable()
            Dim aspect = GetAspectWithoutTranslationTable()

            Dim table = GetInterpolationTable(aspect)

            table.Should.BeNull()
        End Sub

        <TestMethod()>
        Public Sub AspectWithUntranslatedScoringTranslationTable_ShouldNotHaveInterpolationTable()
            Dim aspect = GetAspectWithUntranslatedTranslationTable()

            Dim table = GetInterpolationTable(aspect)

            table.Should.BeNull()
        End Sub

        <TestMethod()>
        Public Sub AspectWithScoringTranslationTable_ShouldHaveInterpolationTable()
            Dim aspect = GetAspectWithTranslationTable()

            Dim table = GetInterpolationTable(aspect)

            table.Should.NotBeNull()
        End Sub

        <TestMethod()>
        Public Sub AspectWithScoringTranslationTable_ShouldHaveInterpolationTableWithMaxScorePlusOneEntries()
            Dim aspect = GetAspectWithTranslationTable()

            Dim table = GetInterpolationTable(aspect)

            table.qtiinterpolationtableentry.Should().HaveCount(ASPECTMAXSCORE + 1)
        End Sub

        <TestMethod()>
        Public Sub AspectWithScoringTranslationTable_ShouldHaveInterpolationTableWithTranslatedEntries()
            Dim aspect = GetAspectWithTranslationTable()

            Dim table = GetInterpolationTable(aspect)

            NumberOfTranslatedEntriesWithValue(0, table).Should().Be(ASPECTMAXSCORE)
            NumberOfTranslatedEntriesWithValue(ASPECTMAXSCORE, table).Should().Be(1)
        End Sub

        Private Function GetInterpolationTable(aspect As Aspect) As InterpolationTableType
            Dim converter = New CombinedScoringConverter()
            Dim solution = GetSolution()
            Dim packageCreator = A.Fake(Of IPackageCreator)()
            Dim responseIdentifierAttributeList = GetResponseIdentifierAttributeList()
            A.CallTo(Function() packageCreator.GetAspectByCode(A(Of String).Ignored)).Returns(aspect)

            Dim outcomeDeclarations = converter.GetOutcomeDeclarations(solution, responseIdentifierAttributeList, Nothing, packageCreator)

            Dim aspectOutcome = outcomeDeclarations.First(Function(o) o.identifier.Equals("qtiAspectInhoudOutcomeDeclaration"))
            Return TryCast(aspectOutcome.Item, InterpolationTableType)
        End Function

        Private Shared Function GetSolution() As Solution
            Dim solution = New Solution()
            solution.AutoScoring = False

            Dim aspectRefColl = New AspectReferenceCollection()
            aspectRefColl.Id = "gapController"
            aspectRefColl.Items.Add(New AspectReference() With {
                                .SourceName = "Inhoud",
                                .MaxScore = ASPECTMAXSCORE,
                                .Description = "<p>Hi!</p>"
            })

            solution.AspectReferenceSetCollection.Add(aspectRefColl)

            Return solution
        End Function

        Private Shared Function GetResponseIdentifierAttributeList() As XmlNodeList
            Dim fakeDocument = New XmlDocument()
            fakeDocument.LoadXml("<fake></fake>")
            Return fakeDocument.DocumentElement.ChildNodes
        End Function

        Private Function GetAspectWithoutTranslationTable() As Aspect
            Return New Aspect() With {.MaxScore = ASPECTMAXSCORE}
        End Function

        Private Function GetAspectWithUntranslatedTranslationTable() As Aspect
            Dim aspect = New Aspect With {.MaxScore = ASPECTMAXSCORE}
            For i As Integer = 0 To aspect.MaxScore
                aspect.AspectScoreTranslationTable.Add(New AspectScoreTranslationTableEntry(i, i))
            Next
            Return aspect
        End Function

        Private Function GetAspectWithTranslationTable() As Aspect
            Dim aspect = New Aspect With {.MaxScore = ASPECTMAXSCORE}
            For i As Integer = 0 To aspect.MaxScore
                aspect.AspectScoreTranslationTable.Add(New AspectScoreTranslationTableEntry(i, If(i < ASPECTMAXSCORE, 0, ASPECTMAXSCORE)))
            Next
            Return aspect
        End Function

        Private Function NumberOfTranslatedEntriesWithValue(translatedScore As Integer, table As InterpolationTableType) As Integer
            Return table.qtiinterpolationtableentry.Where(Function(i As InterpolationTableEntryType) i.targetvalue = translatedScore.ToString()).Count
        End Function

    End Class
End Namespace