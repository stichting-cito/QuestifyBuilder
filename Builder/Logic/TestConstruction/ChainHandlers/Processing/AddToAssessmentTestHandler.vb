Imports System.Linq
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Cito.Tester.ContentModel.Datasources
Imports Enums
Imports Questify.Builder.Logic.Chain
Imports Questify.Builder.Logic.TestConstruction.Requests

Namespace TestConstruction.ChainHandlers.Processing
    Class AddToAssessmentTestHandler
        Inherits ChainHandlerBase(Of TestConstructionRequest)


        Private ReadOnly _assessmentTest As AssessmentTest2
        Private ReadOnly _bankId As Integer
        Private ReadOnly _currentPosition As Integer
        Private ReadOnly _currentSectionContext As TestSection2




        Public Sub New(ByVal bankId As Integer, ByVal test As AssessmentTest2, ByVal section As TestSection2, ByVal insertIndex As Integer)
            If test Is Nothing Then
                Throw New ArgumentNullException("test")
            End If

            If section Is Nothing Then
                Throw New ArgumentNullException("section")
            End If

            If insertIndex < 0 Then
                Throw New ArgumentOutOfRangeException("insertIndex")
            End If

            _bankId = bankId
            _assessmentTest = test
            _currentSectionContext = section
            _currentPosition = insertIndex
        End Sub





        Public Overrides Function ProcessRequest(ByVal requestData As TestConstructionRequest) As ChainHandlerResult
            Dim result As ChainHandlerResult

            If requestData.RequestType = TestConstructionRequest.RequestTypeEnum.Add AndAlso requestData.Items.Count > 0 _
                Then
                result = ExecuteRequest(requestData)
            Else
                result = ChainHandlerResult.RequestNotHandled
            End If

            Return result
        End Function


        Private Sub AddItemReferenceToSection(ByVal newItemReference As ItemReference2, ByVal addToSection As TestSection2,
                                      ByVal insertAtPosition As Integer)
            If newItemReference Is Nothing Then
                Throw New ArgumentNullException("newItemReference")
            End If

            If addToSection Is Nothing Then
                Throw New ArgumentNullException("addToSection")
            End If

            If insertAtPosition < 0 Then
                Throw New ArgumentOutOfRangeException("insertAtPosition should have value >=0")
            End If

            insertAtPosition = Math.Min(insertAtPosition, addToSection.Components.Count)

            addToSection.Components.Insert(insertAtPosition, newItemReference)
        End Sub


        Private Function CreateNewItemReference(ByVal itemEntity As ItemResourceEntity, isSeedingItem As Boolean, Optional weight? As Double = Nothing) As ItemReference2
            Dim createdItem As CreatedTestNodeAndViews(Of ItemReference2, ItemReferenceViewBase) =
        AssessmentTestv2Factory.CreateItemReferenceAndViews(_assessmentTest.IncludedViews)

            createdItem.TestNode.Identifier = itemEntity.ResourceId.ToString()
            createdItem.TestNode.Title = itemEntity.Name
            createdItem.TestNode.SourceName = itemEntity.Name

            Dim itemrefWeight = 1.0
            Dim itemTypeFromItemLayoutTemplate = ItemFunctionalType.Regular
            If isSeedingItem Then
                itemrefWeight = 0
                itemTypeFromItemLayoutTemplate = ItemFunctionalType.Seeding
            End If
            If weight.HasValue Then itemrefWeight = weight.Value
            If itemEntity.ItemTypeFromItemLayoutTemplate = ItemTypeEnum.Informational Then
                itemrefWeight = 0
                itemTypeFromItemLayoutTemplate = ItemFunctionalType.Informational
            End If
            createdItem.TestNode.LockedForEdit = isSeedingItem
            createdItem.TestNode.Active = Not (itemEntity.ItemTypeFromItemLayoutTemplate = ItemTypeEnum.Informational)
            createdItem.TestNode.ItemFunctionalType = itemTypeFromItemLayoutTemplate
            createdItem.TestNode.Weight = itemrefWeight
            Return createdItem.TestNode
        End Function


        Private Function ExecuteRequest(ByVal requestData As TestConstructionRequest) As ChainHandlerResult
            Dim itemResources As IDictionary(Of String, ItemResourceEntity) = ProcessingHelpers.GetItemResources(requestData,
                                                                                                     _bankId)
            Dim listEquallyDivided = requestData.Items.Where(Function(res) res.IsSeedingItem)
            Dim listNotEquallyDivide = requestData.Items.Where(Function(res) Not listEquallyDivided.Select(Function(r) r.Identifier).Contains(res.Identifier))
            AddItemsToTest(listNotEquallyDivide, itemResources, requestData.OverridenTarget, False)
            AddItemsToTest(listEquallyDivided.Reverse, itemResources, requestData.OverridenTarget, True)
            If listEquallyDivided.Any Then
                _assessmentTest.Title = String.Format(My.Resources.FormatTitle, _assessmentTest.Title, listEquallyDivided(0).GetSeedingGroup, listEquallyDivided(0).GetFirstItem)
            End If
            Return ChainHandlerResult.RequestHandled
        End Function


        Private Sub AddItemsToTest(listOfItemIds As IEnumerable(Of ResourceRef), itemResources As IDictionary(Of String, ItemResourceEntity), overrideTarget As IDictionary(Of ResourceRef, TestSection2), seeding As Boolean)
            Dim indexToInsertItem As Integer = _currentPosition
            Dim orgItemCount = _currentSectionContext.Components.Count
            Dim itemChangedIndex As Integer = 0
            For Each res In listOfItemIds
                If itemResources.ContainsKey(res.Identifier) Then
                    Dim resInternal As ResourceRef = res
                    Dim itemEntity As ItemResourceEntity = itemResources(res.Identifier)
                    Dim indexItem = indexToInsertItem
                    If seeding Then
                        indexItem = CType(Math.Round(orgItemCount / (listOfItemIds.Count + 1) * (listOfItemIds.Count - itemChangedIndex)), Integer)
                        itemChangedIndex += 1
                    End If
                    Dim newItemRef As ItemReference2 = CreateNewItemReference(itemEntity, seeding, _currentSectionContext.ItemWeightForVariantTests)
                    AddItemReferenceToSection(newItemRef,
                          IIf(overrideTarget.ContainsKey(res),
                              Function() overrideTarget(resInternal),
                              Function() _currentSectionContext),
                          indexItem)

                    indexToInsertItem += 1
                Else
                End If
            Next
        End Sub


        Private Function IIf(Of T)(ByVal expression As Boolean,
                                    truePart As Func(Of T), falsePart As Func(Of T)) As T
            If expression Then
                Return truePart()
            Else
                Return falsePart()
            End If
        End Function

    End Class
End Namespace