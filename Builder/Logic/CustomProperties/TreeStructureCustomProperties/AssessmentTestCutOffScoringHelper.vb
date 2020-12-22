Imports System.Linq
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Model.ContentModel.Interfaces
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ContentModel
Imports Questify.Builder.Logic.Service.Factories

Namespace CustomProperties
    Public Class AssessmentTestCutOffScoringHelper
        Public Shared Sub UpdateCutOffScoreConditionsInTests(bankId As Integer, dirtyTreeStructureParts As List(Of TreeStructurePartCustomBankPropertyEntity), removedTreeStructureParts As List(Of Guid))
            If (dirtyTreeStructureParts IsNot Nothing AndAlso dirtyTreeStructureParts.Count > 0) OrElse (removedTreeStructureParts IsNot Nothing AndAlso removedTreeStructureParts.Count > 0) Then
                Dim testResourceEntities = ResourceFactory.Instance.GetAssessmentTestsForBank(bankId, False, True, False)
                If testResourceEntities.Count > 0 Then

                    Dim updatedTreeStructureParts = GetDictionaryOfUpdatedTreeStructureParts(dirtyTreeStructureParts, removedTreeStructureParts)

                    For Each testResource As AssessmentTestResourceEntity In testResourceEntities
                        Dim isV2Model As Boolean
                        Dim assessmentTest = GetAssessmentTest(testResource, isV2Model)

                        If UpdateCutOffScoreConditions(assessmentTest, updatedTreeStructureParts) Then
                            SetAssessmentTest(testResource, assessmentTest, isV2Model)
                            ResourceFactory.Instance.UpdateAssessmentTestResource(testResource)
                        End If
                    Next
                End If
            End If
        End Sub

        Public Shared Sub CheckUsageOfTreeStructurePartsInTests(bankId As Integer, treeStructurePart As TreeStructurePartCustomBankPropertyEntity, ByRef referencedEntities As List(Of IPropertyEntity))
            CheckUsageOfTreeStructurePartsInTests(bankId, New List(Of Guid) From {treeStructurePart.Code}, referencedEntities)
        End Sub

        Public Shared Sub CheckUsageOfTreeStructurePartsInTests(bankId As Integer, treeStructureParts As List(Of TreeStructurePartCustomBankPropertyEntity), ByRef referencedEntities As List(Of IPropertyEntity))
            CheckUsageOfTreeStructurePartsInTests(bankId, treeStructureParts.Select(Function(tp) tp.Code).ToList(), referencedEntities)
        End Sub

        Private Shared Sub CheckUsageOfTreeStructurePartsInTests(bankId As Integer, treeStructureParts As List(Of Guid), ByRef referencedEntities As List(Of IPropertyEntity), Optional maxNrOfTestsAsResult As Integer = 0)
            For Each testResource As AssessmentTestResourceEntity In ResourceFactory.Instance.GetAssessmentTestsForBank(bankId, False, True, False)
                Dim assessmentTest = testResource.GetAssessmentTest()
                If assessmentTest.CutOffScoreConditions.Any(Function(co) treeStructureParts.Contains(co.LevelId)) Then
                    referencedEntities.Add(testResource)
                End If

                If maxNrOfTestsAsResult > 0 AndAlso referencedEntities.Count >= maxNrOfTestsAsResult Then
                    Exit For
                End If
            Next
        End Sub

        Private Shared Function GetAssessmentTest(test As AssessmentTestResourceEntity, ByRef isV2Model As Boolean) As AssessmentTest2
            If test.ResourceData Is Nothing OrElse test.ResourceData.BinData Is Nothing Then
                test.ResourceData = ResourceFactory.Instance.GetResourceData(test)
            End If

            Dim factoryResult As ReturnedAssessmentTestModelInfo = AssessmentTestv2Factory.ReturnAssessmentTestv2ModelFromByteArray(test.ResourceData.BinData, True)
            Return factoryResult.AssessmentTestv2
        End Function

        Private Shared Sub SetAssessmentTest(ByVal testResource As AssessmentTestResourceEntity, ByVal assessmentTest As AssessmentTest2, ByVal isV2Model As Boolean)
            Dim serializedAssessmentTestModel As Byte() = Nothing
            serializedAssessmentTestModel = SerializeHelper.XmlSerializeToByteArray(assessmentTest)

            testResource.ResourceData.BinData = serializedAssessmentTestModel
        End Sub

        Private Shared Function UpdateCutOffScoreConditions(ByVal assessmentTest As AssessmentTest2, ByVal updatedTreeStructureParts As Dictionary(Of Guid, String)) As Boolean
            Dim isModified As Boolean = False
            Dim cutOffScoreConditionsToRemove As New List(Of CutOffScoreCondition)


            If assessmentTest.CutOffScoreConditions.Any(Function(co) updatedTreeStructureParts.Keys.Contains(co.LevelId)) Then
                assessmentTest.CutOffScoreConditions.ForEach(Sub(co)
                                                                 If updatedTreeStructureParts.ContainsKey(co.LevelId) AndAlso Not co.LevelName.Equals(updatedTreeStructureParts(co.LevelId), StringComparison.InvariantCulture) Then
                                                                     If Not String.IsNullOrEmpty(updatedTreeStructureParts(co.LevelId)) Then
                                                                         co.LevelName = updatedTreeStructureParts(co.LevelId)
                                                                         isModified = True
                                                                     Else
                                                                         cutOffScoreConditionsToRemove.Add(co)
                                                                     End If
                                                                 End If
                                                             End Sub)
            End If

            For Each co In cutOffScoreConditionsToRemove
                assessmentTest.CutOffScoreConditions.Remove(co)
                isModified = True
            Next

            Return isModified
        End Function

        Private Shared Function GetDictionaryOfUpdatedTreeStructureParts(dirtyTreeStructureParts As List(Of TreeStructurePartCustomBankPropertyEntity), removedTreeStructureParts As List(Of Guid)) As Dictionary(Of Guid, String)
            Dim updatedTreeStructureParts As New Dictionary(Of Guid, String)

            If dirtyTreeStructureParts IsNot Nothing Then dirtyTreeStructureParts.ForEach(Sub(e)
                                                                                              updatedTreeStructureParts.Add(CType(e, TreeStructurePartCustomBankPropertyEntity).Code, CType(e, TreeStructurePartCustomBankPropertyEntity).Name)
                                                                                          End Sub)

            If removedTreeStructureParts IsNot Nothing Then removedTreeStructureParts.ForEach(Sub(e)
                                                                                                  updatedTreeStructureParts.Add(e, String.Empty)
                                                                                              End Sub)

            Return updatedTreeStructureParts
        End Function
    End Class

End Namespace