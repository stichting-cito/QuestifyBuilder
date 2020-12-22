Imports System.Collections.Concurrent
Imports System.Linq
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.QTI.PackageCreators.QTI_Base
Imports Questify.Builder.Logic.QTI.PackageCreators.QTI22
Imports Questify.Builder.Logic.QTI.Xsd.QTI22_Final

Namespace QTI.Helpers.QTI22.QtiModelHelpers

    Public Class TestHelper

        Protected _test As GeneralAssessmentTest
        Protected _packageCreator As QTI22PackageCreator

        Public Sub New(test As GeneralAssessmentTest, packageCreator As QTI22PackageCreator)
            _test = test
            _packageCreator = packageCreator
        End Sub

        Public Overridable Function CreateAssessmentTest() As AssessmentTestType
            Dim AssessmentTestType = GetAssessmentTestType()
            AssessmentTestType.identifier = ChainHandlerHelper.GetIdentifierFromResourceId(_test.Identifier, PackageCreatorConstants.TypeOfResource.test)
            AssessmentTestType.title = _test.Title
            If TypeOf _test Is QTIAssessmentTestBase Then
                Dim timeLimits = TimeLimitsHelper.GetTimeLimitsType(DirectCast(_test, QTIAssessmentTestBase).TimeLimits)
                If timeLimits IsNot Nothing Then
                    AssessmentTestType.timeLimits = timeLimits
                End If
            End If

            Dim outcomeDeclarations As New List(Of OutcomeDeclarationType)
            outcomeDeclarations.Add(GetOutComeDeclaration())
            AssessmentTestType.outcomeDeclaration = outcomeDeclarations
            AssessmentTestType.outcomeProcessing = GetOutcomeProcessing(GetOutcomeProcessingItems())
            Return AssessmentTestType
        End Function

        Public Overridable Sub AddMetadata(resourceType As Global.Questify.Builder.Logic.QTI.Xsd.QTI22_Final.ResourceType, testMetaDataCollection As MetaDataCollection)
        End Sub

        Public Overridable Sub AddAttachmentResources(ByVal isPreview As Boolean, ByVal resourceHelper As ResourceHelper, ByVal resources As ConcurrentDictionary(Of String, Dictionary(Of Global.Questify.Builder.Logic.QTI.Xsd.QTI22_Final.ResourceType, List(Of String))), ByVal resourceMimeTypeDictionary As ConcurrentDictionary(Of String, String))
        End Sub

        Public Overridable Sub AddThemeResources(ByVal isPreview As Boolean, ByVal resourceHelper As ResourceHelper, ByVal resources As ConcurrentDictionary(Of String, Dictionary(Of Global.Questify.Builder.Logic.QTI.Xsd.QTI22_Final.ResourceType, List(Of String))), ByVal resourceMimeTypeDictionary As ConcurrentDictionary(Of String, String))
        End Sub

        Protected Overridable Function GetAssessmentTestType() As AssessmentTestType
            Return New AssessmentTestType
        End Function

        Protected Function GetOutComeDeclaration() As OutcomeDeclarationType
            Dim returnValue As New OutcomeDeclarationType With {
                    .baseType = OutcomeDeclarationTypeBaseType.float,
                    .baseTypeSpecified = True,
                    .identifier = PackageCreatorConstants.SCORE,
                    .cardinality = OutcomeDeclarationTypeCardinality.single,
                    .defaultValue = New DefaultValueType
                    }

            Dim valueList As New List(Of ValueType)
            Dim value As New ValueType With {
                    .Value = "0"
                    }
            valueList.Add(value)
            returnValue.defaultValue.value = valueList
            Return returnValue
        End Function

        Protected Overridable Function GetOutcomeProcessingItems() As IEnumerable(Of (ItemsChoiceType8, Object))
            Dim testvariables = New TestVariablesType With {
                .variableIdentifier = PackageCreatorConstants.SCORE,
                .weightIdentifier = PackageCreatorConstants.WEIGHT
            }

            Return {(ItemsChoiceType8.testVariables, testvariables)}
        End Function

        Protected Function GetOutcomeProcessing(items As IEnumerable(Of (ItemsChoiceType8, Object))) As OutcomeProcessingType
            Dim setOutcomeValue = New SetValueType() With {
                .ItemElementName = ItemChoiceType7.sum,
                .identifier = PackageCreatorConstants.SCORE,
                .Item = New NumericLogic1toManyType() With {
                    .ItemsElementName = items.Select(Function(i) i.Item1).ToArray(),
                    .Items = items.Select(Function(i) i.Item2).ToArray()
                }
            }

            Return New OutcomeProcessingType() With {
                .Items = New List(Of Object) From {setOutcomeValue}
            }
        End Function

    End Class
End Namespace