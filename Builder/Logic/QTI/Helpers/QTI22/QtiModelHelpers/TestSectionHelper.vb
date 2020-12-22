Imports System.Linq
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.QTI.PackageCreators.QTI_Base
Imports Questify.Builder.Logic.QTI.Xsd.QTI22_Final

Namespace QTI.Helpers.QTI22.QtiModelHelpers

    Public Class TestSectionHelper

        Protected _testSection As GeneralTestSection

        Public Sub New(testSection As GeneralTestSection)
            _testSection = testSection
        End Sub


        Public Function CreateSection() As AssessmentSectionType
            Dim sectionType As New AssessmentSectionType
            sectionType.identifier = ChainHandlerHelper.GetIdentifierFromResourceId(_testSection.Identifier, PackageCreatorConstants.TypeOfResource.resource)
            sectionType.title = _testSection.Title
            If TypeOf _testSection Is QTITestSectionBase Then
                Dim qtiSection = DirectCast(_testSection, QTITestSectionBase)
                sectionType.visible = qtiSection.Visible
                sectionType.keepTogether = qtiSection.KeepTogether
                If qtiSection.SectionPart IsNot Nothing Then
                    If TypeOf _testSection Is QTITestSectionBase Then
                        Dim timeLimits = TimeLimitsHelper.GetTimeLimitsType(qtiSection.SectionPart.TimeLimits)
                        If timeLimits IsNot Nothing Then
                            sectionType.timeLimits = timeLimits
                        End If
                    End If
                End If
            End If

            Return sectionType
        End Function

        Public Sub AddTestSectionToAssessmentTestType(ByRef assessmentTestType As AssessmentTestType, testSectionPart As AssessmentSectionType, parentId As String)
            Dim id As String = ChainHandlerHelper.GetIdentifierFromResourceId(parentId, PackageCreatorConstants.TypeOfResource.resource)
            If assessmentTestType.testPart IsNot Nothing AndAlso Not assessmentTestType.testPart.Count = 0 Then
                Dim referencedTestParts = From component In assessmentTestType.testPart
                                          Where component.identifier = id
                If referencedTestParts.Count = 1 Then
                    Dim testPartRef As TestPartType = referencedTestParts(0)
                    If testPartRef.assessmentSection IsNot Nothing AndAlso testPartRef.assessmentSection.Count > 0 Then
                        Dim section As List(Of AssessmentSectionType) = testPartRef.assessmentSection.OfType(Of AssessmentSectionType).ToList
                        section.Add(testSectionPart)
                        testPartRef.assessmentSection = section.ToArray
                    Else
                        testPartRef.assessmentSection = New AssessmentSectionType() {testSectionPart}
                    End If
                Else
                    Dim referencedTestSection As AssessmentSectionType = Nothing
                    assessmentTestType.testPart.ToList.ForEach(Sub(tp)
                                                                   If referencedTestSection Is Nothing AndAlso tp.assessmentSection IsNot Nothing Then
                                                                       referencedTestSection = FindParentSectionById(tp.assessmentSection.OfType(Of AssessmentSectionType).ToArray(), id)
                                                                   End If
                                                               End Sub)
                    If referencedTestSection IsNot Nothing Then
                        If referencedTestSection.testComponents IsNot Nothing AndAlso Not referencedTestSection.testComponents.Count = 0 Then
                            Dim sections As List(Of sectionGroup) = referencedTestSection.testComponents.OfType(Of sectionGroup).ToList
                            sections.Add(testSectionPart)
                            referencedTestSection.testComponents = sections
                        Else
                            Dim sectionGroups As New List(Of sectionGroup)
                            sectionGroups.Add(testSectionPart)
                            referencedTestSection.testComponents = sectionGroups
                        End If
                    Else
                        Debug.Assert(True, $"Parent: '{parentId}' for TestSection: '{testSectionPart.identifier}' cannot be found")
                    End If
                End If

            Else
                Throw New Exception("Cannot add a section while no testparts are added")
            End If
        End Sub



        Private Function FindParentSectionById(assessmentSectionTypes As AssessmentSectionType(), id As String) As AssessmentSectionType
            Dim referencedTestSection As AssessmentSectionType = Nothing
            assessmentSectionTypes.ToList.ForEach(Sub(st)
                                                      If referencedTestSection Is Nothing Then
                                                          referencedTestSection = FindParentSectionById(st, id)
                                                      End If
                                                  End Sub)
            Return referencedTestSection
        End Function

        Private Function FindParentSectionById(assessmentSectionType As AssessmentSectionType, id As String) As AssessmentSectionType
            Dim referencedTestSection As AssessmentSectionType = Nothing
            If assessmentSectionType.identifier.Equals(id, StringComparison.InvariantCultureIgnoreCase) Then Return assessmentSectionType
            If assessmentSectionType.testComponents IsNot Nothing Then
                assessmentSectionType.testComponents.ToList.ForEach(Sub(tc)
                                                                        If referencedTestSection Is Nothing Then
                                                                            If TypeOf tc Is AssessmentSectionType Then referencedTestSection = FindParentSectionById(DirectCast(tc, AssessmentSectionType), id)
                                                                        End If
                                                                    End Sub)
            End If
            Return referencedTestSection
        End Function




    End Class
End Namespace