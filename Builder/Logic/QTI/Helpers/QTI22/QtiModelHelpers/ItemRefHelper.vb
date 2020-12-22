
Imports System.Linq
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.QTI.Helpers.QTI_Base
Imports Questify.Builder.Logic.QTI.PackageCreators.QTI_Base
Imports Questify.Builder.Logic.QTI.PackageCreators.QTI22
Imports Questify.Builder.Logic.QTI.Xsd.QTI22_Final

Namespace QTI.Helpers.QTI22.QtiModelHelpers

    Public Class ItemRefHelper

        Protected _itemRef As GeneralItemReference
        Protected _packageCreator As QTI22PackageCreator


        Public Sub New(itemRef As GeneralItemReference, packageCreator As QTI22PackageCreator)
            _itemRef = itemRef
            _packageCreator = packageCreator
        End Sub



        Public Function CreateItemRef(ByRef test As AssessmentTestType) As AssessmentItemRefType
            Dim itemId = ChainHandlerHelper.GetIdentifierFromResourceId(_itemRef.SourceName, PackageCreatorConstants.TypeOfResource.item)
            Dim itemRef As New AssessmentItemRefType
            Dim maxItemDuration As Integer = _packageCreator.GetMaxItemDurationFromItemParameters(_itemRef.SourceName)
            itemRef.identifier = itemId
            itemRef.href = $"{_itemRef.SourceName}.xml"
            itemRef.childIndex = GetChildIndex(ChainHandlerHelper.RemovePrefixFromResourceIdentifier(test.identifier, PackageCreatorConstants.TypeOfResource.test))
            Dim weight As New WeightType
            weight.identifier = PackageCreatorConstants.WEIGHT
            weight.value = GetWeigth()
            itemRef.weight = New List(Of WeightType)
            itemRef.weight.Add(weight)

            If TypeOf _itemRef Is QTIItemReferenceBase AndAlso DirectCast(_itemRef, QTIItemReferenceBase).SectionPart IsNot Nothing Then
                If maxItemDuration > 0 Then
                    Dim newTimeListType As New TimeLimitsType
                    newTimeListType.maxTime = maxItemDuration
                    newTimeListType.maxTimeSpecified = (maxItemDuration > 0)
                    newTimeListType.minTimeSpecified = False
                    itemRef.timeLimits = newTimeListType
                Else
                    Dim timeLimits = TimeLimitsHelper.GetTimeLimitsType(DirectCast(_itemRef, QTIItemReferenceBase).SectionPart.TimeLimits)
                    If timeLimits IsNot Nothing Then
                        itemRef.timeLimits = timeLimits
                    End If
                End If
            End If
            Return itemRef
        End Function

        Protected Function GetChildIndex(testIdentifier As String) As Integer
            Dim result As Integer = 0
            If _packageCreator IsNot Nothing AndAlso _packageCreator.ListOfItems IsNot Nothing AndAlso _packageCreator.ListOfItems.ContainsKey(_itemRef.SourceName) Then
                result = _packageCreator.ListOfItems(_itemRef.SourceName).FirstOrDefault(Function(t) TestIdentifierToCompare(t.Key).Equals(TestIdentifierToCompare(testIdentifier), StringComparison.InvariantCultureIgnoreCase)).Value.Item2
            End If
            Return result
        End Function

        Public Overridable Sub AddItemRefToSection(test As AssessmentTestType, itemRef As AssessmentItemRefType)
            Dim parentId = ChainHandlerHelper.GetIdentifierFromResourceId(_itemRef.Parent.Identifier, PackageCreatorConstants.TypeOfResource.resource)
            Dim section = GetSectionReference(test, parentId)
            If section.testComponents IsNot Nothing AndAlso section.testComponents.Count <> 0 Then
                Dim currentlist = section.testComponents.ToList
                currentlist.Insert(itemRef.childIndex, itemRef)
                section.testComponents = currentlist
            Else
                section.testComponents = New List(Of sectionGroup)
                section.testComponents.Add(itemRef)
            End If
        End Sub

        Protected Overridable Function GetWeigth() As Double
            Return If(_itemRef.ItemFunctionalType = ItemFunctionalType.Informational, 0, _itemRef.Weight)
        End Function



        Protected Overridable Function GetNamespaceHelper() As NamespaceHelper
            Return New QTI22NamespaceHelper
        End Function

        Private Function GetSectionReference(test As AssessmentTestType, parentId As String) As AssessmentSectionType
            Return (From section In GetAllSections(test)
                    Where section.identifier = parentId).FirstOrDefault
        End Function

        Private Function GetAllSections(test As AssessmentTestType) As IList(Of AssessmentSectionType)
            Dim returnValue As New List(Of AssessmentSectionType)
            test.testPart.ToList.ForEach(Sub(part)
                                             part.assessmentSection.OfType(Of AssessmentSectionType).ToList.ForEach(Sub(section)
                                                                                                                        returnValue.Add(section)
                                                                                                                        GetSectionsFromSection(section, returnValue)
                                                                                                                    End Sub)
                                         End Sub)
            Return returnValue
        End Function

        Private Sub GetSectionsFromSection(section As AssessmentSectionType, sections As List(Of AssessmentSectionType))
            If section.testComponents IsNot Nothing Then
                Dim newSections = From comp In section.testComponents
                                  Where TypeOf comp Is AssessmentSectionType
                                  Select DirectCast(comp, AssessmentSectionType)
                If newSections IsNot Nothing Then
                    sections.AddRange(newSections.ToList)
                    newSections.ToList.ForEach(Sub(childSection)
                                                   GetSectionsFromSection(childSection, sections)
                                               End Sub)
                End If
            End If
        End Sub

        Private Function TestIdentifierToCompare(testIdentifierInput As String) As String
            Return testIdentifierInput.Replace(Chr(32), "_"c).Replace(".", "_")
        End Function


    End Class
End Namespace