
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Logic
Imports Cito.Tester.ContentModel
Imports System.Linq
Imports Cito.Tester.Common
Imports Questify.Builder.Logic.ContentModel
Imports Questify.Builder.Logic.ResourceManager
Imports Questify.Builder.UnitTests.Framework
Imports Cito.Tester.ContentModel.Datasources
Imports Questify.Builder.UnitTests.Framework.FakeAppTemplate

<TestClass>
Public Class ProposalCreatorTest

    <TestInitialize>
    Public Sub Initialize()
        FakeDal.Init()
        FakeDal.CanSaveResources()
    End Sub

    <TestCleanup>
    Public Sub CleanUp()
        FakeDal.Deinit()
    End Sub

    <TestMethod(), TestCategory("ItemProcessing")>
    Public Sub ItemCount_10()
        Dim proposalCreator = New ProposalCreator
        Dim assessmentTestResource = CreateRegularTest()
        Dim rm As New DataBaseResourceManager(1)
        Dim sectionsWithDatasources As New Dictionary(Of TestSection2, ItemDataSource)
        sectionsWithDatasources.Add(assessmentTestResource.GetAssessmentTest.TestParts(0).Sections(0), New UnitTestSeedingConfig(Nothing, New List(Of Integer)({1, 2, 3}.ToList)))

        Dim proposals = proposalCreator.CreateProposalsFromDataSourceList(assessmentTestResource, sectionsWithDatasources, 6, rm)

        Assert.AreEqual(6, proposals.Count)
        Assert.AreEqual(11, proposals(0).GetAssessmentTest.GetAllItemReferencesInTest.Count)
        Assert.AreEqual(12, proposals(1).GetAssessmentTest.GetAllItemReferencesInTest.Count)
        Assert.AreEqual(13, proposals(2).GetAssessmentTest.GetAllItemReferencesInTest.Count)
        Assert.AreEqual(12, proposals(3).GetAssessmentTest.GetAllItemReferencesInTest.Count)
        Assert.AreEqual(13, proposals(4).GetAssessmentTest.GetAllItemReferencesInTest.Count)
        Assert.AreEqual(13, proposals(5).GetAssessmentTest.GetAllItemReferencesInTest.Count)
    End Sub

    <TestMethod(), TestCategory("ItemProcessing")>
    Public Sub ItemDividedEqual_10()
        Dim proposalCreator = New ProposalCreator
        Dim assessmentTestResource = CreateRegularTest()
        Dim rm As New DataBaseResourceManager(1)
        Dim sectionsWithDatasources As New Dictionary(Of TestSection2, ItemDataSource)
        sectionsWithDatasources.Add(assessmentTestResource.GetAssessmentTest.TestParts(0).Sections(0), New UnitTestSeedingConfig(Nothing, New List(Of Integer)({1, 2, 3}.ToList)))

        Dim proposals = proposalCreator.CreateProposalsFromDataSourceList(assessmentTestResource, sectionsWithDatasources, 6, rm)

        Assert.AreEqual(6, proposals.Count)

        Assert.AreEqual("Group1_Item1", proposals(0).GetAssessmentTest.GetAllItemReferencesInTest(5).SourceName)

        Assert.AreEqual("Group2_Item1", proposals(1).GetAssessmentTest.GetAllItemReferencesInTest(3).SourceName)
        Assert.AreEqual("Group2_Item2", proposals(1).GetAssessmentTest.GetAllItemReferencesInTest(8).SourceName)

        Assert.AreEqual("Group3_Item1", proposals(2).GetAssessmentTest.GetAllItemReferencesInTest(2).SourceName)
        Assert.AreEqual("Group3_Item2", proposals(2).GetAssessmentTest.GetAllItemReferencesInTest(6).SourceName)
        Assert.AreEqual("Group3_Item3", proposals(2).GetAssessmentTest.GetAllItemReferencesInTest(10).SourceName)

        Assert.AreEqual("Group2_Item2", proposals(3).GetAssessmentTest.GetAllItemReferencesInTest(3).SourceName)
        Assert.AreEqual("Group2_Item1", proposals(3).GetAssessmentTest.GetAllItemReferencesInTest(8).SourceName)

        Assert.AreEqual("Group3_Item2", proposals(4).GetAssessmentTest.GetAllItemReferencesInTest(2).SourceName)
        Assert.AreEqual("Group3_Item3", proposals(4).GetAssessmentTest.GetAllItemReferencesInTest(6).SourceName)
        Assert.AreEqual("Group3_Item1", proposals(4).GetAssessmentTest.GetAllItemReferencesInTest(10).SourceName)

        Assert.AreEqual("Group3_Item3", proposals(5).GetAssessmentTest.GetAllItemReferencesInTest(2).SourceName)
        Assert.AreEqual("Group3_Item1", proposals(5).GetAssessmentTest.GetAllItemReferencesInTest(6).SourceName)
        Assert.AreEqual("Group3_Item2", proposals(5).GetAssessmentTest.GetAllItemReferencesInTest(10).SourceName)
    End Sub

    <TestMethod(), TestCategory("ItemProcessing")>
    Public Sub ItemDividedEqualItemFunctionalType_10()
        Dim proposalCreator = New ProposalCreator
        Dim assessmentTestResource = CreateRegularTest()
        Dim rm As New DataBaseResourceManager(1)
        Dim sectionsWithDatasources As New Dictionary(Of TestSection2, ItemDataSource)
        sectionsWithDatasources.Add(assessmentTestResource.GetAssessmentTest.TestParts(0).Sections(0), New UnitTestSeedingConfig(Nothing, New List(Of Integer)({1, 2, 3}.ToList)))

        Dim proposals = proposalCreator.CreateProposalsFromDataSourceList(assessmentTestResource, sectionsWithDatasources, 6, rm)

        Assert.AreEqual(6, proposals.Count)
        Assert.AreEqual(ItemFunctionalType.Seeding, proposals(1).GetAssessmentTest.GetAllItemReferencesInTest(3).ItemFunctionalType)
        Assert.AreEqual(ItemFunctionalType.Seeding, proposals(4).GetAssessmentTest.GetAllItemReferencesInTest(6).ItemFunctionalType)
    End Sub

    <TestMethod(), TestCategory("ItemProcessing")>
    Public Sub ItemWeight_0()
        Dim proposalCreator = New ProposalCreator
        Dim assessmentTestResource = CreateRegularTest(0)
        Dim rm As New DataBaseResourceManager(1)
        Dim sectionsWithDatasources As New Dictionary(Of TestSection2, ItemDataSource)
        sectionsWithDatasources.Add(assessmentTestResource.GetAssessmentTest.TestParts(0).Sections(0), New UnitTestSeedingConfig(Nothing, New List(Of Integer)({1, 2, 3}.ToList)))

        Dim proposals = proposalCreator.CreateProposalsFromDataSourceList(assessmentTestResource, sectionsWithDatasources, 1, rm)

        Assert.AreEqual(1, CInt(proposals(0).GetAssessmentTest.GetAllItemReferencesInTest(0).Weight))
        Assert.AreEqual(1, CInt(proposals(0).GetAssessmentTest.GetAllItemReferencesInTest(1).Weight))
        Assert.AreEqual(1, CInt(proposals(0).GetAssessmentTest.GetAllItemReferencesInTest(2).Weight))
        Assert.AreEqual(1, CInt(proposals(0).GetAssessmentTest.GetAllItemReferencesInTest(3).Weight))
        Assert.AreEqual(1, CInt(proposals(0).GetAssessmentTest.GetAllItemReferencesInTest(4).Weight))
        Assert.AreEqual(0, CInt(proposals(0).GetAssessmentTest.GetAllItemReferencesInTest(5).Weight))
        Assert.AreEqual(1, CInt(proposals(0).GetAssessmentTest.GetAllItemReferencesInTest(6).Weight))
        Assert.AreEqual(1, CInt(proposals(0).GetAssessmentTest.GetAllItemReferencesInTest(7).Weight))
        Assert.AreEqual(1, CInt(proposals(0).GetAssessmentTest.GetAllItemReferencesInTest(8).Weight))
        Assert.AreEqual(1, CInt(proposals(0).GetAssessmentTest.GetAllItemReferencesInTest(9).Weight))
        Assert.AreEqual(2, CInt(proposals(0).GetAssessmentTest.GetAllItemReferencesInTest(10).Weight))
    End Sub

    <TestMethod(), TestCategory("ItemProcessing")>
    Public Sub ItemWeight_1()
        Dim proposalCreator = New ProposalCreator
        Dim assessmentTestResource = CreateRegularTest(1)
        Dim rm As New DataBaseResourceManager(1)
        Dim sectionsWithDatasources As New Dictionary(Of TestSection2, ItemDataSource)
        sectionsWithDatasources.Add(assessmentTestResource.GetAssessmentTest.TestParts(0).Sections(0), New UnitTestSeedingConfig(Nothing, New List(Of Integer)({1, 2, 3}.ToList)))

        Dim proposals = proposalCreator.CreateProposalsFromDataSourceList(assessmentTestResource, sectionsWithDatasources, 1, rm)

        Assert.AreEqual(1, CInt(proposals(0).GetAssessmentTest.GetAllItemReferencesInTest(0).Weight))
        Assert.AreEqual(1, CInt(proposals(0).GetAssessmentTest.GetAllItemReferencesInTest(1).Weight))
        Assert.AreEqual(1, CInt(proposals(0).GetAssessmentTest.GetAllItemReferencesInTest(2).Weight))
        Assert.AreEqual(1, CInt(proposals(0).GetAssessmentTest.GetAllItemReferencesInTest(3).Weight))
        Assert.AreEqual(1, CInt(proposals(0).GetAssessmentTest.GetAllItemReferencesInTest(4).Weight))
        Assert.AreEqual(1, CInt(proposals(0).GetAssessmentTest.GetAllItemReferencesInTest(5).Weight))
        Assert.AreEqual(1, CInt(proposals(0).GetAssessmentTest.GetAllItemReferencesInTest(6).Weight))
        Assert.AreEqual(1, CInt(proposals(0).GetAssessmentTest.GetAllItemReferencesInTest(7).Weight))
        Assert.AreEqual(1, CInt(proposals(0).GetAssessmentTest.GetAllItemReferencesInTest(8).Weight))
        Assert.AreEqual(1, CInt(proposals(0).GetAssessmentTest.GetAllItemReferencesInTest(9).Weight))
        Assert.AreEqual(2, CInt(proposals(0).GetAssessmentTest.GetAllItemReferencesInTest(10).Weight))
    End Sub

    <TestMethod(), TestCategory("ItemProcessing")>
    Public Sub AssessmentTestTitle()
        Dim proposalCreator = New ProposalCreator
        Dim assessmentTestResource = CreateRegularTest()
        Dim rm As New DataBaseResourceManager(1)
        Dim sectionsWithDatasources As New Dictionary(Of TestSection2, ItemDataSource)
        sectionsWithDatasources.Add(assessmentTestResource.GetAssessmentTest.TestParts(0).Sections(0), New UnitTestSeedingConfig(Nothing, New List(Of Integer)({1, 2, 3}.ToList)))

        Dim proposals = proposalCreator.CreateProposalsFromDataSourceList(assessmentTestResource, sectionsWithDatasources, 4, rm)

        For i = 0 To 3
            Assert.IsTrue(proposals(i).Title = proposals(i).GetAssessmentTest.Title)
        Next
        Assert.IsTrue(proposals(0).GetAssessmentTest.Title.Contains("_1_"))
        Assert.IsTrue(proposals(1).GetAssessmentTest.Title.Contains("_2_"))
        Assert.IsTrue(proposals(1).GetAssessmentTest.Title.Contains("_1"))
        Assert.IsTrue(proposals(2).GetAssessmentTest.Title.Contains("_3_"))
        Assert.IsTrue(proposals(2).GetAssessmentTest.Title.Contains("_1"))
        Assert.IsTrue(proposals(3).GetAssessmentTest.Title.Contains("_2_"))
        Assert.IsTrue(proposals(3).GetAssessmentTest.Title.Contains("_2"))

    End Sub

    <TestMethod(), TestCategory("ItemProcessing")>
    Public Sub ItemDividedEqualWithSections_10()
        Dim proposalCreator = New ProposalCreator
        Dim assessmentTestResource = CreateTestWithSubSection()
        Dim rm As New DataBaseResourceManager(1)
        Dim sectionsWithDatasources As New Dictionary(Of TestSection2, ItemDataSource)
        sectionsWithDatasources.Add(assessmentTestResource.GetAssessmentTest.TestParts(1).Sections(0), New UnitTestSeedingConfig(Nothing, New List(Of Integer)({1, 2, 3}.ToList)))

        Dim proposals = proposalCreator.CreateProposalsFromDataSourceList(assessmentTestResource, sectionsWithDatasources, 6, rm)

        Assert.AreEqual(6, proposals.Count)

        Assert.AreEqual("Group1_Item1", proposals(0).GetAssessmentTest.TestParts(1).Sections(0).Components(5).Title)

        Assert.AreEqual("Group2_Item1", proposals(1).GetAssessmentTest.TestParts(1).Sections(0).Components(3).Title)
        Assert.AreEqual("Group2_Item2", proposals(1).GetAssessmentTest.TestParts(1).Sections(0).Components(8).Title)

        Assert.AreEqual("Group3_Item1", proposals(2).GetAssessmentTest.TestParts(1).Sections(0).Components(2).Title)
        Assert.AreEqual("Group3_Item2", proposals(2).GetAssessmentTest.TestParts(1).Sections(0).Components(6).Title)
        Assert.AreEqual("Group3_Item3", proposals(2).GetAssessmentTest.TestParts(1).Sections(0).Components(10).Title)

        Assert.AreEqual("Group2_Item2", proposals(3).GetAssessmentTest.TestParts(1).Sections(0).Components(3).Title)
        Assert.AreEqual("Group2_Item1", proposals(3).GetAssessmentTest.TestParts(1).Sections(0).Components(8).Title)

        Assert.AreEqual("Group3_Item2", proposals(4).GetAssessmentTest.TestParts(1).Sections(0).Components(2).Title)
        Assert.AreEqual("Group3_Item3", proposals(4).GetAssessmentTest.TestParts(1).Sections(0).Components(6).Title)
        Assert.AreEqual("Group3_Item1", proposals(4).GetAssessmentTest.TestParts(1).Sections(0).Components(10).Title)

        Assert.AreEqual("Group3_Item3", proposals(5).GetAssessmentTest.TestParts(1).Sections(0).Components(2).Title)
        Assert.AreEqual("Group3_Item1", proposals(5).GetAssessmentTest.TestParts(1).Sections(0).Components(6).Title)
        Assert.AreEqual("Group3_Item2", proposals(5).GetAssessmentTest.TestParts(1).Sections(0).Components(10).Title)
    End Sub


    <TestMethod(), TestCategory("ItemProcessing")>
    Public Sub ItemDividedEqualWithSections_FunctionalType_10()
        Dim proposalCreator = New ProposalCreator
        Dim assessmentTestResource = CreateTestWithSubSection()
        Dim rm As New DataBaseResourceManager(1)
        Dim sectionsWithDatasources As New Dictionary(Of TestSection2, ItemDataSource)
        sectionsWithDatasources.Add(assessmentTestResource.GetAssessmentTest.TestParts(1).Sections(0), New UnitTestSeedingConfig(Nothing, New List(Of Integer)({1, 2, 3}.ToList)))

        Dim proposals = proposalCreator.CreateProposalsFromDataSourceList(assessmentTestResource, sectionsWithDatasources, 6, rm)

        Assert.AreEqual(6, proposals.Count)
        Assert.AreEqual(ItemFunctionalType.Seeding, CType(proposals(0).GetAssessmentTest.TestParts(1).Sections(0).Components(5), ItemReference2).ItemFunctionalType)
        Assert.AreEqual(ItemFunctionalType.Seeding, CType(proposals(5).GetAssessmentTest.TestParts(1).Sections(0).Components(6), ItemReference2).ItemFunctionalType)

    End Sub


    Private Function CreateRegularTest(Optional weight As Double? = Nothing) As AssessmentTestResourceEntity
        Dim assessmentTestResource = New AssessmentTestResourceEntity With {
           .ResourceId = New Guid, .Name = "MYTEST", .Title = "my test", .BankId = 1}
        Dim assessmentTest = New AssessmentTest2 With {.Identifier = "MYTEST", .Title = "my test"}
        Dim testPart1 = New TestPart2 With {.Identifier = "TestPart1"}
        Dim section1 = New TestSection2 With {.Identifier = "Section1"}
        If weight.HasValue Then section1.ItemWeightForVariantTests = weight.Value
        section1.ItemDataSource = "SeedingGroup"
        Dim regularItem1 = New ItemReference2 With {.Identifier = Guid.NewGuid.ToString, .SourceName = "regularItem1", .Title = "regularItem 1", .ItemFunctionalType = ItemFunctionalType.Regular, .Weight = 1}
        Dim regularItem2 = New ItemReference2 With {.Identifier = Guid.NewGuid.ToString, .SourceName = "regularItem2", .Title = "regularItem 2", .ItemFunctionalType = ItemFunctionalType.Regular, .Weight = 1}
        Dim regularItem3 = New ItemReference2 With {.Identifier = Guid.NewGuid.ToString, .SourceName = "regularItem3", .Title = "regularItem 3", .ItemFunctionalType = ItemFunctionalType.Regular, .Weight = 1}
        Dim regularItem4 = New ItemReference2 With {.Identifier = Guid.NewGuid.ToString, .SourceName = "regularItem4", .Title = "regularItem 4", .ItemFunctionalType = ItemFunctionalType.Regular, .Weight = 1}
        Dim regularItem5 = New ItemReference2 With {.Identifier = Guid.NewGuid.ToString, .SourceName = "regularItem5", .Title = "regularItem 5", .ItemFunctionalType = ItemFunctionalType.Regular, .Weight = 1}
        Dim regularItem6 = New ItemReference2 With {.Identifier = Guid.NewGuid.ToString, .SourceName = "regularItem6", .Title = "regularItem 6", .ItemFunctionalType = ItemFunctionalType.Regular, .Weight = 1}
        Dim regularItem7 = New ItemReference2 With {.Identifier = Guid.NewGuid.ToString, .SourceName = "regularItem7", .Title = "regularItem 7", .ItemFunctionalType = ItemFunctionalType.Regular, .Weight = 1}
        Dim regularItem8 = New ItemReference2 With {.Identifier = Guid.NewGuid.ToString, .SourceName = "regularItem8", .Title = "regularItem 8", .ItemFunctionalType = ItemFunctionalType.Regular, .Weight = 1}
        Dim regularItem9 = New ItemReference2 With {.Identifier = Guid.NewGuid.ToString, .SourceName = "regularItem9", .Title = "regularItem 9", .ItemFunctionalType = ItemFunctionalType.Regular, .Weight = 1}
        Dim regularItem10 = New ItemReference2 With {.Identifier = Guid.NewGuid.ToString, .SourceName = "regularItem10", .Title = "regularItem 10", .ItemFunctionalType = ItemFunctionalType.Regular, .Weight = 2}

        section1.Components.AddRange({regularItem1, regularItem2, regularItem3, regularItem4, regularItem5, regularItem6, regularItem7, regularItem8, regularItem9, regularItem10})
        testPart1.Sections.Add(section1)
        assessmentTest.TestParts.Add(testPart1)

        assessmentTestResource.SetAssessmentToAssessmentResource(assessmentTest)
        Return assessmentTestResource
    End Function

    Private Function CreateTestWithSubSection(Optional weight As Double? = Nothing) As AssessmentTestResourceEntity
        Dim assessmentTestResource = New AssessmentTestResourceEntity With {
            .ResourceId = New Guid, .Name = "MYTEST", .Title = "my test", .BankId = 1}
        Dim assessmentTest = New AssessmentTest2 With {.Identifier = "MYTEST", .Title = "my test"}
        Dim testPart1 = New TestPart2 With {.Identifier = "TestPart1"}
        Dim testPart2 = New TestPart2 With {.Identifier = "TestPart2"}
        Dim section1 = New TestSection2 With {.Identifier = "Section1"}
        Dim section2 = New TestSection2 With {.Identifier = "Section2"}
        section2.ItemDataSource = "SeedingGroup"
        If weight.HasValue Then section2.ItemWeightForVariantTests = weight.Value
        Dim inclusionSection1 = New TestSection2 With {.Identifier = "SectionInclusion1"}
        Dim inclusionSection2 = New TestSection2 With {.Identifier = "SectionInclusion2"}

        Dim infoItem1 = New ItemReference2 With {.Identifier = Guid.NewGuid.ToString, .SourceName = "Info 1", .ItemFunctionalType = ItemFunctionalType.Informational}
        Dim infoItem2 = New ItemReference2 With {.Identifier = Guid.NewGuid.ToString, .SourceName = "Info 2", .ItemFunctionalType = ItemFunctionalType.Informational}

        Dim regularItem1 = New ItemReference2 With {.Identifier = Guid.NewGuid.ToString, .SourceName = "regularItem1", .Title = "regularItem 1", .ItemFunctionalType = ItemFunctionalType.Regular}
        Dim regularItem2 = New ItemReference2 With {.Identifier = Guid.NewGuid.ToString, .SourceName = "regularItem2", .Title = "regularItem 2", .ItemFunctionalType = ItemFunctionalType.Regular}
        Dim regularItem3 = New ItemReference2 With {.Identifier = Guid.NewGuid.ToString, .SourceName = "regularItem3", .Title = "regularItem 3", .ItemFunctionalType = ItemFunctionalType.Regular}
        Dim regularItem4 = New ItemReference2 With {.Identifier = Guid.NewGuid.ToString, .SourceName = "regularItem4", .Title = "regularItem 4", .ItemFunctionalType = ItemFunctionalType.Regular}
        Dim regularItem5 = New ItemReference2 With {.Identifier = Guid.NewGuid.ToString, .SourceName = "regularItem5", .Title = "regularItem 5", .ItemFunctionalType = ItemFunctionalType.Regular}
        Dim regularItem6 = New ItemReference2 With {.Identifier = Guid.NewGuid.ToString, .SourceName = "regularItem6", .Title = "regularItem 6", .ItemFunctionalType = ItemFunctionalType.Regular}
        Dim regularItem7 = New ItemReference2 With {.Identifier = Guid.NewGuid.ToString, .SourceName = "regularItem7", .Title = "regularItem 7", .ItemFunctionalType = ItemFunctionalType.Regular}
        Dim regularItem8 = New ItemReference2 With {.Identifier = Guid.NewGuid.ToString, .SourceName = "regularItem8", .Title = "regularItem 8", .ItemFunctionalType = ItemFunctionalType.Regular}

        Dim itemOfInclusion1_1 = New ItemReference2 With {.Identifier = Guid.NewGuid.ToString, .SourceName = "itemOfInclusion1_1", .Title = "itemOfInclusion 1.1", .ItemFunctionalType = ItemFunctionalType.Regular}
        Dim itemOfInclusion1_2 = New ItemReference2 With {.Identifier = Guid.NewGuid.ToString, .SourceName = "itemOfInclusion1_2", .Title = "itemOfInclusion 1.2", .ItemFunctionalType = ItemFunctionalType.Regular}
        Dim itemOfInclusion1_3 = New ItemReference2 With {.Identifier = Guid.NewGuid.ToString, .SourceName = "itemOfInclusion1_3", .Title = "itemOfInclusion 1.3", .ItemFunctionalType = ItemFunctionalType.Regular}

        Dim itemOfInclusion2_1 = New ItemReference2 With {.Identifier = Guid.NewGuid.ToString, .SourceName = "itemOfInclusion2_1", .Title = "itemOfInclusion 2.1", .ItemFunctionalType = ItemFunctionalType.Regular}
        Dim itemOfInclusion2_2 = New ItemReference2 With {.Identifier = Guid.NewGuid.ToString, .SourceName = "itemOfInclusion2_2", .Title = "itemOfInclusion 2.2", .ItemFunctionalType = ItemFunctionalType.Regular}
        Dim itemOfInclusion2_3 = New ItemReference2 With {.Identifier = Guid.NewGuid.ToString, .SourceName = "itemOfInclusion2_3", .Title = "itemOfInclusion 2.3", .ItemFunctionalType = ItemFunctionalType.Regular}

        section1.Components.AddRange({infoItem1, infoItem2})
        testPart1.Sections.Add(section1)
        inclusionSection1.Components.AddRange({itemOfInclusion1_1, itemOfInclusion1_2, itemOfInclusion1_3})
        inclusionSection2.Components.AddRange({itemOfInclusion2_1, itemOfInclusion2_2, itemOfInclusion2_3})
        section2.Components.AddRange({regularItem1, regularItem2, inclusionSection1, regularItem3, regularItem4, regularItem5, inclusionSection2, regularItem6, regularItem7, regularItem8})
        testPart2.Sections.Add(section2)
        assessmentTest.TestParts.Add(testPart1)
        assessmentTest.TestParts.Add(testPart2)

        assessmentTestResource.SetAssessmentToAssessmentResource(assessmentTest)
        Return assessmentTestResource
    End Function

    Public Class UnitTestSeedingConfig
        Inherits ItemDataSourceManyOutput

        Private ReadOnly _groups As IEnumerable(Of Integer)

        Public Sub New(settings As ItemDataSourceConfig, groups As IEnumerable(Of Integer))
            MyBase.New(settings)
            _groups = groups
        End Sub

        Public Overrides Function GetMany(resourceManager As ResourceManagerBase, numberOfRequests As Integer) As IList(Of IEnumerable(Of ResourceRef))
            Dim returnValue As New List(Of IEnumerable(Of ResourceRef))
            Dim groupDictionary As New Dictionary(Of Integer, Integer)
            Dim groupIndex As Integer = 0
            For proposalIndex = 0 To numberOfRequests - 1
                Dim itemIndex As Integer = GetNextItemFromGroup(groupDictionary, groupIndex)
                Dim canPickFormCurrentGroup = _groups.Count > groupIndex AndAlso _groups(groupIndex) >= itemIndex
                While Not canPickFormCurrentGroup
                    groupIndex = CType(IIf(_groups.Count <= (1 + groupIndex), 0, groupIndex + 1), Integer)
                    itemIndex = GetNextItemFromGroup(groupDictionary, groupIndex)
                    canPickFormCurrentGroup = _groups(groupIndex) > itemIndex
                End While
                If Not groupDictionary.ContainsKey(groupIndex) Then groupDictionary.Add(groupIndex, 0)
                groupDictionary(groupIndex) = itemIndex
                Dim proposalResult As New List(Of ResourceRef)
                Dim firstItem As String = String.Empty
                For index = 0 To _groups(groupIndex) - 1
                    If itemIndex > _groups(groupIndex) - 1 Then itemIndex = 0
                    Dim resourceName = String.Format("Group{0}_Item{1}", (groupIndex + 1).ToString, (itemIndex + 1).ToString)
                    If index = 0 Then firstItem = (itemIndex + 1).ToString
                    Dim resourceRef = New ResourceRef With {
                        .Identifier = resourceName,
                        .Properties = New SerializableGenericDictionary(Of String, String) From {
                        {"seeding", "true"}, {"equallyDivided", "true"},
                        {"seeding_group", (groupIndex + 1).ToString}, {"seeding_group_first_item", firstItem}}
                    }
                    Dim item = New ItemResourceEntity With {.ResourceId = Guid.NewGuid, .Name = resourceName, .Title = resourceName}

                    FakeDal.FakeServices.FakeResourceService.UpdateItemResource(item)
                    proposalResult.Add(resourceRef)

                    itemIndex += 1
                Next
                returnValue.Add(proposalResult)
                groupIndex += 1
            Next
            Return returnValue
        End Function

        Public Overrides ReadOnly Property ClearSectionWhenProposing As Boolean
            Get
                Return False
            End Get
        End Property

        Private Function GetNextItemFromGroup(groupDictionary As Dictionary(Of Integer, Integer), groupIndex As Integer) As Integer
            Dim index As Integer = 0
            If groupDictionary.ContainsKey(groupIndex) Then
                index = groupDictionary(groupIndex) + 1
            End If
            Return index
        End Function
    End Class


End Class
