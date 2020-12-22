Imports Cito.Tester.Common
Imports Questify.Builder.Logic.Chain
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Logic.ResourceManager
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.TestPackageConstruction.ChainHandlers.Processing
Imports Questify.Builder.Logic.TestPackageConstruction.ChainHandlers.Validating

Namespace TestPackageConstruction

    Public NotInheritable Class TestPackageConstruction




        Public Shared Function AddTestToTestPackage(ByVal testPackage As TestPackage, ByVal testPackageEntity As TestPackageResourceEntity, ByVal resourceManager As DataBaseResourceManager, ByVal testsToAdd As IList(Of Datasources.ResourceRef), ByVal addToTestset As TestSet, ByVal insertAtPosition As Integer, ByVal facade As TestPackageConstructionFacade) As Boolean
            Dim resultToReturn As Boolean = False

            If addToTestset Is Nothing Then
                Throw New ArgumentNullException("addToTestset")
            End If

            If insertAtPosition < 0 Then
                Throw New ArgumentOutOfRangeException("insertAtPosition")
            End If
            Dim testContext As List(Of Datasources.ResourceRef) = TestPackageProcessingHelpers.GetTestResourceRefList(testPackage)

            SetupFacadeForValidation(facade, testPackage, False)
            SetupFacadeForContextEditing(facade, testPackage, testPackageEntity, resourceManager, addToTestset, insertAtPosition)

            If facade.AddTests(testsToAdd, testContext) = ChainHandlerResult.RequestHandled Then
                resultToReturn = True
            Else
                resultToReturn = False
            End If

            Return resultToReturn
        End Function

        Public Shared Function DeleteTestPackageComponents(
    ByVal componentsToDelete As List(Of TestPackageNode),
    ByVal testPackage As TestPackage,
    ByVal testPackageEntity As TestPackageResourceEntity,
    ByVal facade As TestPackageConstructionFacade) As List(Of String)


            Dim removedDependencies As New List(Of String)
            Dim testRefList As IList(Of TestReference) = GetAllInvolvedTestReferences(componentsToDelete)
            Dim containerList As IList(Of TestPackageNode)
            SetupFacadeForValidation(facade, testPackage, False)
            Dim successfullyRemoved As Boolean = True
            If testRefList.Count > 0 Then
                successfullyRemoved = RemoveTestsFromTestPackage(testRefList, testPackage, testPackageEntity, facade)
            End If
            If successfullyRemoved Then
                containerList = GetAllInvolvedContainers(componentsToDelete)
                For Each container As TestPackageNode In containerList
                    If TypeOf container Is TestSet Then
                        Dim testSet As TestSet = CType(container, TestSet)
                        If testSet.Components.Count = 0 Then
                            testPackage.TestSets.Remove(testSet)


                            removedDependencies = DeleteTestPackageComponentDependencies(testPackage, testSet)
                        End If
                    End If
                Next

            End If
            Return removedDependencies
        End Function



        Private Shared Function DeleteTestPackageComponentDependencies(ByVal testPackage As TestPackage, ByVal testSet As TestSet) As List(Of String)
            Dim returnValue As New List(Of String)

            For Each supportedViewType In testPackage.IncludedViews
                Dim tempViewTestSet As TestSetViewBase = TestPackageFactory.CreateTemporaryTestSetView(testSet, supportedViewType)

                Dim depResourcesToRemove As New ResourceEntryCollection()
                tempViewTestSet.GetDependencyResourcesForThisTestset(depResourcesToRemove, False)

                For Each depResource As ResourceEntry In depResourcesToRemove

                    If Not returnValue.Contains(depResource.Name) Then
                        returnValue.Add(depResource.Name)
                    End If
                Next
            Next

            Return returnValue
        End Function


        Private Shared Function GetAllInvolvedContainers(ByVal components As IList(Of TestPackageNode)) As IList(Of TestPackageNode)
            Dim componentList As New List(Of TestPackageNode)

            For Each component As TestPackageNode In components
                If TypeOf component Is TestSet Then
                    Dim testSet As TestSet = CType(component, TestSet)
                    Dim subNodes As IList(Of TestPackageNode) = New List(Of TestPackageNode)
                    For Each comp As TestPackageComponent In testSet.Components
                        subNodes.Add(comp)
                    Next

                    componentList.AddRange(GetAllInvolvedContainers(subNodes))
                    componentList.Add(component)

                ElseIf TypeOf component Is TestReference Then
                Else
                    Throw New Exception("unknown type")
                End If
            Next

            Return componentList
        End Function


        Private Shared Function GetAllInvolvedTestReferences(ByVal components As List(Of TestPackageNode)) As IList(Of TestReference)
            Dim testReferenceList As New List(Of TestReference)

            For Each component As TestPackageNode In components
                If TypeOf component Is TestSet Then
                    testReferenceList.AddRange(CType(component, TestSet).GetAllTestReferencesInTestSet())
                ElseIf TypeOf component Is TestReference Then
                    testReferenceList.Add(CType(component, TestReference))
                ElseIf TypeOf component Is TestPackage Then
                Else
                    Throw New Exception("unknown type")
                End If
            Next

            Return testReferenceList
        End Function



        Private Shared Function RemoveTestsFromTestPackage(ByVal testsToRemove As IList(Of TestReference), ByVal testPackage As TestPackage, ByVal testPackageEntity As TestPackageResourceEntity, ByVal facade As TestPackageConstructionFacade) As Boolean
            Dim resultToReturn As Boolean = False

            If testsToRemove Is Nothing Then
                Throw New ArgumentNullException("testReferences")
            End If

            Dim testContext As List(Of Datasources.ResourceRef) = TestPackageProcessingHelpers.GetTestResourceRefList(testPackage)
            Dim tests As List(Of Datasources.ResourceRef) = TestPackageProcessingHelpers.GetTestResourceRefList(testsToRemove)

            With facade
                .EditContextHandlers.Clear()
                .EditContextHandlers.Add(New RemoveFromTestPackageHandler(testPackage))
                .EditContextHandlers.Add(New RemoveDependenciesFromTestPackage(testPackageEntity))

                If .RemoveTests(tests, testContext) = ChainHandlerResult.RequestHandled Then
                    resultToReturn = True
                Else
                    resultToReturn = False
                End If
            End With

            Return resultToReturn
        End Function


        Private Shared Sub SetupFacadeForContextEditing(
    ByVal facade As TestPackageConstructionFacade,
    ByVal testPackage As TestPackage,
    ByVal testPackageEntity As TestPackageResourceEntity,
    ByVal resourceManager As DataBaseResourceManager,
    ByVal addToTestset As TestSet,
    ByVal insertAtPosition As Integer)

            With facade
                .EditContextHandlers.Clear()
                .EditContextHandlers.Add(New AddToTestPackageHandler(resourceManager.BankId, testPackage, addToTestset, insertAtPosition))
                .EditContextHandlers.Add(New AddDependenciesToTestPackage(testPackageEntity))
            End With
        End Sub

        Private Shared Sub SetupFacadeForValidation(
    ByVal facade As TestPackageConstructionFacade,
    ByVal testPackage As TestPackage,
    ByVal removeEditContextHandlers As Boolean)

            With facade
                .ValidationHandlers.Add(New RequestContainsTestsAlreadyInContextValidationHandler())
                .ValidationHandlers.Add(New RequestContainsTestThatAreReferencedAsRetryTestValidationHandler(testPackage))
                If removeEditContextHandlers Then
                    .EditContextHandlers.Clear()

                    .EditContextHandlers.Add(New NOOPTestPackage(ChainHandlerResult.RequestHandled))
                End If

            End With
        End Sub


    End Class

End Namespace