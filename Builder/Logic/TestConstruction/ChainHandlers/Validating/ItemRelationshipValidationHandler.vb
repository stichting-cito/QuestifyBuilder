Imports Questify.Builder.Logic.Chain
Imports Cito.Tester.Common
Imports System.Linq
Imports Questify.Builder.Logic.ResourceManager
Imports Cito.Tester.ContentModel
Imports Cito.Tester.ContentModel.Datasources
Imports Questify.Builder.Logic.TestConstruction.Requests

Namespace TestConstruction.ChainHandlers.Validating
    Public Class ItemRelationshipValidationHandler
        Inherits ChainHandlerBase(Of TestConstructionRequest)


        Private ReadOnly _comparer As IEqualityComparer(Of ResourceRef) = New ResourceRefIdentityEqualityComparer
        Private ReadOnly _relatedItems As New Dictionary(Of DataSourceSettings, IEnumerable(Of ResourceRef))



        Public ReadOnly Property RelatedItems() As Dictionary(Of DataSourceSettings, IEnumerable(Of ResourceRef))
            Get
                Return _relatedItems
            End Get
        End Property


        Public Sub New(ByVal resourceManager As DataBaseResourceManager, ParamArray dataSourceBehaviours() As String)
            If resourceManager Is Nothing Then
                Throw New ArgumentNullException("resourceManager")
            End If

            _relatedItems = ItemHelpers.GetItemsPerGroup(resourceManager, dataSourceBehaviours)
        End Sub

        Public Sub New(ByVal itemLists As Dictionary(Of String, List(Of ResourceRef)), ByVal behaviour As DataSourceBehaviourEnum)
            GenerateRelatedItemsBasedOnItemList(itemLists, behaviour)
        End Sub



        Public Overrides Function ProcessRequest(ByVal requestData As TestConstructionRequest) As ChainHandlerResult
            Dim ItemsUnderConstruction As IEnumerable(Of ResourceRef) = requestData.Items

            For Each Key As Datasources.DataSourceSettings In Me.RelatedItems.Keys
                Dim relatedItems As IEnumerable(Of ResourceRef) = Me.RelatedItems(Key)

                If SetOperations.ContainsAny(ItemsUnderConstruction, relatedItems, _comparer) Then
                    If ValidateItemRelationship(requestData, Key) = ChainHandlerResult.RequestNotHandled Then
                        Throw New ChainHandlerException()
                    End If
                End If
            Next

            Return ChainHandlerResult.RequestHandled
        End Function

        Private Sub GenerateRelatedItemsBasedOnItemList(ByVal itemLists As IDictionary(Of String, List(Of ResourceRef)), ByVal behaviour As DataSourceBehaviourEnum)
            For Each listValuePair As KeyValuePair(Of String, List(Of ResourceRef)) In itemLists
                Dim newSettings As New DataSourceSettings()
                newSettings.Identifier = listValuePair.Key
                newSettings.Behaviour = behaviour

                _relatedItems.Add(newSettings, listValuePair.Value)
            Next
        End Sub

        Private Function ValidateItemRelationship(ByVal requestData As TestConstructionRequest, ByVal selection As Datasources.DataSourceSettings) As ChainHandlerResult
            Dim relatedItems As IList(Of ResourceRef) = New List(Of ResourceRef)(_relatedItems(selection))

            Dim purposedItemContext As IEnumerable(Of ResourceRef) = requestData.GetPurposedItemContext
            Dim conflictingResourceRefs As IList(Of ResourceRef) = Nothing
            Dim conflictingCausedByResourceRefs As IList(Of ResourceRef) = Nothing

            Select Case requestData.RequestType

                Case TestConstructionRequest.RequestTypeEnum.Add, TestConstructionRequest.RequestTypeEnum.Remove
                    Dim hasRelationshipConflict As Boolean = False
                    Dim numberOfItemConflicts As Integer = 0

                    Select Case selection.Behaviour
                        Case DataSourceBehaviourEnum.Inclusion
                            conflictingResourceRefs = CType(SetOperations.Difference(relatedItems, purposedItemContext, _comparer), IList(Of ResourceRef))

                            conflictingCausedByResourceRefs = SetOperations.Intersect(purposedItemContext, relatedItems, _comparer)

                            hasRelationshipConflict = (conflictingResourceRefs.Count >= 1) _
                                                    AndAlso (conflictingResourceRefs.Count < relatedItems.Count)

                            If hasRelationshipConflict Then
                                numberOfItemConflicts = conflictingResourceRefs.Count
                            Else
                                numberOfItemConflicts = 0
                            End If


                        Case DataSourceBehaviourEnum.Exclusion

                            Dim innerExclusionGroups As New Dictionary(Of String, IList(Of ResourceRef))

                            innerExclusionGroups.Add("Remaining", New List(Of ResourceRef))

                            For Each item As ResourceRef In relatedItems
                                Dim itemsInGroup As IList(Of ResourceRef)
                                If item.Properties.ContainsKey("NestedGroup") Then
                                    Dim groupName As String = $"_{item.Properties("NestedGroup")}"
                                    If Not innerExclusionGroups.ContainsKey(groupName) Then
                                        innerExclusionGroups.Add(groupName, New List(Of ResourceRef))
                                    End If

                                    itemsInGroup = innerExclusionGroups(groupName)
                                Else
                                    itemsInGroup = innerExclusionGroups("Remaining")
                                End If

                                itemsInGroup.Add(item)
                            Next

                            If innerExclusionGroups.Count = 1 Then
                                Dim conflictingInPurposedItemContext As IList(Of ResourceRef) = DirectCast(SetOperations.Intersect(relatedItems, purposedItemContext, _comparer), IList(Of ResourceRef))
                                If conflictingInPurposedItemContext.Count > 1 Then
                                    conflictingResourceRefs = DirectCast(SetOperations.Intersect(requestData.Items, conflictingInPurposedItemContext, _comparer), IList(Of ResourceRef))
                                    hasRelationshipConflict = True
                                    numberOfItemConflicts = conflictingResourceRefs.Count
                                End If

                            ElseIf innerExclusionGroups.Count > 1 Then
                                For Each innerExclusionGroup As KeyValuePair(Of String, IList(Of ResourceRef)) In innerExclusionGroups
                                    Dim foundItemsInExclusionGroupFromItemsToAdd As IList(Of ResourceRef) = SetOperations.Intersect(requestData.Items, DirectCast(innerExclusionGroup.Value, IEnumerable(Of ResourceRef)), _comparer)

                                    If foundItemsInExclusionGroupFromItemsToAdd.Count > 0 Then
                                        For Each otherInnerExclusionGroup As KeyValuePair(Of String, IList(Of ResourceRef)) In innerExclusionGroups
                                            If Not otherInnerExclusionGroup.Equals(innerExclusionGroup) Then
                                                Dim foundItemsExclusionGroupFromItemContext As IList(Of ResourceRef) = SetOperations.Intersect(requestData.ItemContext, DirectCast(otherInnerExclusionGroup.Value, IEnumerable(Of ResourceRef)), _comparer)

                                                If foundItemsExclusionGroupFromItemContext.Count > 0 Then
                                                    conflictingResourceRefs = foundItemsInExclusionGroupFromItemsToAdd
                                                    hasRelationshipConflict = (conflictingResourceRefs.Count > 1)
                                                    numberOfItemConflicts = conflictingResourceRefs.Count
                                                    Exit For
                                                End If
                                            End If
                                        Next
                                    End If
                                Next
                            Else
                            End If
                        Case Else
                            hasRelationshipConflict = True
                            numberOfItemConflicts = -1
                    End Select

                    If hasRelationshipConflict Then
                        Dim relId As String = selection.Identifier
                        Dim relType As String = selection.Behaviour.ToString

                        Dim message As String = String.Format(My.Resources.Message_ValidateItemRelationShip_Conflicts, relId, relType, numberOfItemConflicts)
                        if conflictingResourceRefs isnot nothing andalso conflictingResourceRefs.Count > 0
                            message &=
                                $" Items: { _
                                    string.Join(", ", conflictingResourceRefs.Select(function(r) r.Identifier).ToArray)}"
                        End If
                        Dim validationException As New ItemRelationshipException(message, relId, selection.Behaviour, conflictingResourceRefs, conflictingCausedByResourceRefs)
                        Throw validationException
                    End If

                Case Else
                    Throw New NotSupportedException(String.Format(My.Resources.Message_ValidateItemRelationShip_UnknownRequestType, requestData.RequestType.ToString))
            End Select

            Return ChainHandlerResult.RequestHandled
        End Function



    End Class
End Namespace