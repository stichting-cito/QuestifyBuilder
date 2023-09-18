Imports System.Collections.Concurrent
Imports System.Linq
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.QTI.PackageCreators.QTI_Base
Imports Questify.Builder.Logic.QTI.PackageCreators.QTI30
Imports Questify.Builder.Logic.QTI.Xsd.QTI30
Imports ResourceType = Questify.Builder.Logic.QTI.Xsd.QTI30.ResourceType

Namespace QTI.Helpers.QTI30.QtiModelHelpers

    Public Class TestHelper

        Protected _test As GeneralAssessmentTest
        Protected _packageCreator As PackageCreator

        Public Sub New(test As GeneralAssessmentTest, packageCreator As PackageCreator)
            _test = test
            _packageCreator = packageCreator
        End Sub

        Public Overridable Function CreateAssessmentTest() As AssessmentTestType
            Dim assessmentTestType = GetAssessmentTestType()
            assessmentTestType.identifier = ChainHandlerHelper.GetIdentifierFromResourceId(_test.Identifier, PackageCreatorConstants.TypeOfResource.test)
            assessmentTestType.title = _test.Title
            If TypeOf _test Is QTIAssessmentTestBase Then
                Dim timeLimits = TimeLimitsHelper.GetTimeLimitsType(DirectCast(_test, QTIAssessmentTestBase).TimeLimits)
                If timeLimits IsNot Nothing Then
                    assessmentTestType.qtitimelimits = timeLimits
                End If
            End If

            assessmentTestType.qtioutcomedeclaration = {GetOutComeDeclaration()}
            assessmentTestType.qtioutcomeprocessing = GetOutcomeProcessing(GetOutcomeProcessingItems())
            Return assessmentTestType
        End Function

        Public Overridable Sub AddMetadata(resourceType As ResourceType, testMetaDataCollection As MetaDataCollection)
        End Sub

        Public Overridable Sub AddAttachmentResources(ByVal isPreview As Boolean, ByVal resourceHelper As ResourceHelper, ByVal resources As ConcurrentDictionary(Of String, Dictionary(Of ResourceType, List(Of String))), ByVal resourceMimeTypeDictionary As ConcurrentDictionary(Of String, String))
        End Sub

        Public Overridable Sub AddThemeResources(ByVal isPreview As Boolean, ByVal resourceHelper As ResourceHelper, ByVal resources As ConcurrentDictionary(Of String, Dictionary(Of ResourceType, List(Of String))), ByVal resourceMimeTypeDictionary As ConcurrentDictionary(Of String, String))
        End Sub

        Protected Overridable Function GetAssessmentTestType() As AssessmentTestType
            Return New AssessmentTestType
        End Function

        Protected Function GetOutComeDeclaration() As OutcomeDeclarationType
            Dim returnValue As New OutcomeDeclarationType With {
                    .basetype = OutcomeDeclarationTypeBasetype.float,
                    .basetypeSpecified = True,
                    .identifier = PackageCreatorConstants.SCORE,
                    .cardinality = OutcomeDeclarationTypeCardinality.single,
                    .qtidefaultvalue = New DefaultValueType
                    }

            Dim value As New ValueType With {
                    .Value = "0"
                    }
            returnValue.qtidefaultvalue.qtivalue = {value}
            Return returnValue
        End Function

        Protected Overridable Function GetOutcomeProcessingItems() As IEnumerable(Of (ItemsChoiceType8, Object))
            Dim testvariables = New TestVariablesType With {
                .variableidentifier = PackageCreatorConstants.SCORE,
                .weightidentifier = PackageCreatorConstants.WEIGHT
            }

            Return {(ItemsChoiceType8.qtitestvariables, testvariables)}
        End Function

        Protected Function GetOutcomeProcessing(items As IEnumerable(Of (ItemsChoiceType8, Object))) As OutcomeProcessingType
            Dim setOutcomeValue = New SetValueType() With {
                .ItemElementName = ItemChoiceType7.qtisum,
                .identifier = PackageCreatorConstants.SCORE,
                .Item = New NumericLogic1toManyType() With {
                    .ItemsElementName = items.Select(Function(i) i.Item1).ToArray(),
                    .Items = items.Select(Function(i) i.Item2).ToArray()
                }
            }

            Return New OutcomeProcessingType() With {
                .Items = {setOutcomeValue}
            }
        End Function

    End Class
End Namespace