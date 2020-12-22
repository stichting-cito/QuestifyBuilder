Imports Cito.Tester.Common
Imports Questify.Builder.Logic.Chain
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Logic.ResourceManager
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.TestConstruction.Helpers
Imports Questify.Builder.Logic.TestConstruction.ChainHandlers.Processing
Imports Questify.Builder.Logic.TestConstruction.ChainHandlers.Validating

Namespace TestConstruction
    Public NotInheritable Class TestConstructionOp


        Public Shared Function AddItemsToTest(ByVal assessmentTest As AssessmentTest2,
                                      ByVal testEntity As AssessmentTestResourceEntity,
                                      ByVal resourceManager As DataBaseResourceManager,
                                      ByVal itemsToAdd As IEnumerable(Of Datasources.ResourceRef),
                                      ByVal addToSection As TestSection2, ByVal insertAtPosition As Integer,
                                      ByVal facade As TestConstructionFacade) As Boolean
            If addToSection Is Nothing Then
                Throw New ArgumentNullException("addToSection")
            End If

            If insertAtPosition < 0 Then
                Throw New ArgumentOutOfRangeException("insertAtPosition")
            End If

            Dim itemContext As List(Of Datasources.ResourceRef) = ProcessingHelpers.GetItemResourceRefList(assessmentTest)
            SetupFacadeForValidation(facade, resourceManager, assessmentTest, addToSection, False)
            SetupFacadeForContextEditing(facade, assessmentTest, testEntity, resourceManager, addToSection, insertAtPosition)

            Return facade.AddItems(itemsToAdd, itemContext) = ChainHandlerResult.RequestHandled
        End Function


        Public Shared Function DeleteTestComponents(ByVal componentsToDelete As List(Of AssessmentTestNode), ByVal test As AssessmentTest2, ByVal assessmentTestEntity As AssessmentTestResourceEntity, ByVal facade As TestConstructionFacade) As List(Of String)

            Dim removedDependencies As New List(Of String)
            Dim itemRefList As IList(Of ItemReference2) = GetAllInvolvedItemReferences(componentsToDelete)
            Dim containerList As IList(Of AssessmentTestNode)

            If itemRefList.Count > 0 Then
                RemoveItemsFromTest(itemRefList, test, assessmentTestEntity, facade)
            End If

            containerList = GetAllInvolvedContainers(componentsToDelete)
            For Each container As AssessmentTestNode In containerList
                If TypeOf container Is TestPart2 Then
                    Dim testPart As TestPart2 = CType(container, TestPart2)

                    If testPart.Sections.Count = 0 Then
                        test.TestParts.Remove(testPart)
                    End If
                ElseIf TypeOf container Is TestSection2 Then
                    Dim testSection As TestSection2 = CType(container, TestSection2)
                    If testSection.Components.Count = 0 Then
                        If TypeOf testSection.Parent Is TestPart2 Then
                            Dim parent As TestPart2 = CType(testSection.Parent, TestPart2)

                            parent.Sections.Remove(testSection)
                        ElseIf TypeOf container Is TestSection2 Then
                            Dim parent As TestSection2 = CType(testSection.Parent, TestSection2)

                            parent.Components.Remove(testSection)
                        End If

                        removedDependencies = DeleteTestComponentDependencies(test, testSection)
                    End If
                End If
            Next

            Return removedDependencies
        End Function

        Public Shared Function ValidateAddItemsToTest(ByVal assessmentTest As AssessmentTest2,
                                              ByVal resourceManager As DataBaseResourceManager,
                                              ByVal itemsToAdd As IList(Of Datasources.ResourceRef),
                                              ByVal addToSection As TestSection2,
                                              ByVal insertAtPosition As Integer,
                                              ByVal facade As TestConstructionFacade) As Boolean
            Dim resultToReturn As Boolean = False

            If addToSection Is Nothing Then
                Throw New ArgumentNullException("addToSection")
            End If

            If insertAtPosition < 0 Then
                Throw New ArgumentOutOfRangeException("insertAtPosition")
            End If

            Dim itemContext As List(Of Datasources.ResourceRef) = ProcessingHelpers.GetItemResourceRefList(assessmentTest)

            SetupFacadeForValidation(facade, resourceManager, assessmentTest, addToSection, True)

            If facade.AddItems(itemsToAdd, itemContext) = ChainHandlerResult.RequestHandled Then
                resultToReturn = True
            Else
                resultToReturn = False
            End If

            Return resultToReturn
        End Function

        Private Shared Function DeleteTestComponentDependencies(ByVal assessmentTest As AssessmentTest2, ByVal testSection As TestSection2) As List(Of String)
            Dim returnValue As New List(Of String)

            For Each supportedViewType As String In assessmentTest.IncludedViews
                Dim tempViewSection As TestSectionViewBase = AssessmentTestv2Factory.CreateTemporaryTestSectionView(testSection, supportedViewType)

                Dim depResourcesToRemove As New ResourceEntryCollection()
                tempViewSection.GetDependencyResourcesForThisTestSection(depResourcesToRemove, False)

                For Each depResource As ResourceEntry In depResourcesToRemove

                    If Not returnValue.Contains(depResource.Name) Then
                        returnValue.Add(depResource.Name)
                    End If
                Next
            Next

            Return returnValue
        End Function


        Private Shared Function GetAllInvolvedContainers(ByVal components As IList(Of AssessmentTestNode)) As IList(Of AssessmentTestNode)
            Dim componentList As New List(Of AssessmentTestNode)

            For Each component As AssessmentTestNode In components
                If TypeOf component Is TestPart2 Then
                    Dim testPart As TestPart2 = CType(component, TestPart2)

                    Dim involvedSubContainers As New List(Of AssessmentTestNode)
                    For Each subContainer As AssessmentTestNode In testPart.Sections
                        involvedSubContainers.Add(subContainer)
                    Next

                    componentList.AddRange(GetAllInvolvedContainers(involvedSubContainers))
                    componentList.Add(component)

                ElseIf TypeOf component Is TestSection2 Then
                    Dim testSection As TestSection2 = CType(component, TestSection2)
                    Dim subNodes As IList(Of AssessmentTestNode) = New List(Of AssessmentTestNode)
                    For Each comp As TestComponent2 In testSection.Components
                        subNodes.Add(comp)
                    Next

                    componentList.AddRange(GetAllInvolvedContainers(subNodes))
                    componentList.Add(component)

                ElseIf TypeOf component Is ItemReference2 Then

                ElseIf TypeOf component Is AssessmentTest2 Then
                Else
                    Throw New Exception("unknown type")
                End If
            Next

            Return componentList
        End Function


        Private Shared Function GetAllInvolvedItemReferences(ByVal components As List(Of AssessmentTestNode)) As IList(Of ItemReference2)
            Dim itemReferenceList As New List(Of ItemReference2)

            For Each component As AssessmentTestNode In components
                If TypeOf component Is TestPart2 Then
                    itemReferenceList.AddRange(CType(component, TestPart2).GetAllItemReferencesInTestPart(True))

                ElseIf TypeOf component Is TestSection2 Then
                    itemReferenceList.AddRange(CType(component, TestSection2).GetAllItemReferencesInSection(True))

                ElseIf TypeOf component Is ItemReference2 Then
                    itemReferenceList.Add(CType(component, ItemReference2))
                ElseIf TypeOf component Is AssessmentTest2 Then
                Else
                    Throw New Exception("unknown type")
                End If
            Next

            Return itemReferenceList
        End Function

        Private Shared Function RemoveItemsFromTest(ByVal itemsToRemove As IEnumerable(Of ItemReference2), ByVal test As AssessmentTest2, ByVal assessmentTestEntity As AssessmentTestResourceEntity, ByVal facade As TestConstructionFacade) As Boolean
            Dim resultToReturn As Boolean = False

            If itemsToRemove Is Nothing Then
                Throw New ArgumentNullException("itemReferences")
            End If

            Dim itemContext As List(Of Datasources.ResourceRef) = ProcessingHelpers.GetItemResourceRefList(test)
            Dim items As List(Of Datasources.ResourceRef) = ProcessingHelpers.GetItemResourceRefList(itemsToRemove)

            With facade
                .ResetFacade()
                .EditContextHandlers.Add(New RemoveFromAssessmentTestHandler(test))
                .EditContextHandlers.Add(New RemoveDependenciesFromAssessmentTest(assessmentTestEntity))

                resultToReturn = (.RemoveItems(items, itemContext) = ChainHandlerResult.RequestHandled)
            End With

            Return resultToReturn
        End Function

        Private Shared Sub SetupFacadeForContextEditing(ByVal facade As TestConstructionFacade,
                                                ByVal assessmentTest As AssessmentTest2,
                                                ByVal testEntity As AssessmentTestResourceEntity,
                                                ByVal resourceManager As DataBaseResourceManager,
                                                ByVal addToSection As TestSection2,
                                                ByVal insertAtPosition As Integer)
            With facade
                .EditContextHandlers.Clear()
                .EditContextHandlers.Add(New AddNonExistingSectionsToAssessment(assessmentTest, addToSection))
                .EditContextHandlers.Add(New AddToAssessmentTestHandler(resourceManager.BankId, assessmentTest, addToSection, insertAtPosition))
                .EditContextHandlers.Add(New AddDependenciesToAssessmentTest(testEntity))
            End With
        End Sub

        Private Shared Sub SetupFacadeForValidation(ByVal facade As TestConstructionFacade, ByVal resourceManager As DataBaseResourceManager,
                                            ByVal assessmentTest As AssessmentTest2,
                                            ByVal targetSection As TestSection2,
                                            ByVal removeEditContextHandlers As Boolean)
            With facade
                .ValidationHandlers.Add(New ItemRelationshipValidationHandler(resourceManager, "inclusion"))
                .ValidationHandlers.Add(New ItemRelationshipValidationHandler(resourceManager, "exclusion"))
                .ValidationHandlers.Add(New ItemContainsSupportedTestViewsValidationHandler(resourceManager, assessmentTest))
                .ValidationHandlers.Add(New ItemInDatasourceTargetValidation(resourceManager, targetSection, assessmentTest))

                If Not removeEditContextHandlers Then
                    .ValidationHandlers.Add(New PreferDatabindingValidation(resourceManager, targetSection))
                End If

                .ValidationHandlers.Add(New RequestContainsItemsAlreadyInContextValidationHandler())

                If removeEditContextHandlers Then
                    .EditContextHandlers.Clear()

                    .EditContextHandlers.Add(New NOOP(ChainHandlerResult.RequestHandled))
                End If

            End With
        End Sub


    End Class
End Namespace