
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.Chain
Imports Questify.Builder.Logic.QTI.Helpers.QTI30
Imports Questify.Builder.Logic.QTI.Helpers.QTI30.QtiModelHelpers
Imports Questify.Builder.Logic.QTI.Model.QTI30
Imports Questify.Builder.Logic.QTI.PackageCreators.QTI30
Imports Questify.Builder.Logic.QTI.Requests.QTI30
Imports Questify.Builder.Logic.QTI.Xsd.QTI30

Namespace QTI.ChainHandlers.Processing.QTI30

    Public Class AssessmentItemHandler
        Inherits ChainHandlerBase

        Private ReadOnly _itemRef As GeneralItemReference
        Private ReadOnly _itemCode As String
        Private ReadOnly _testIdentifier As String
        Protected _assessmentItem As AssessmentItem = Nothing

        Public Sub New(ByVal itemReference As GeneralItemReference, testIdentifier As String, packageCreator As PackageCreator)
            Me.New(itemReference, itemReference.SourceName, testIdentifier, packageCreator)
        End Sub

        Public Sub New(assessmentItem As AssessmentItem, packageCreator As PackageCreator)
            Me.New(Nothing, assessmentItem.Identifier, String.Empty, packageCreator)

            _assessmentItem = SerializeHelper.XmlSerializableClone(Of AssessmentItem)(assessmentItem)
        End Sub

        Private Sub New(ByVal itemReference As GeneralItemReference, itemCode As String, testIdentifier As String, packageCreator As PackageCreator)
            MyBase.New(packageCreator)
            _itemRef = itemReference
            _testIdentifier = testIdentifier
            _itemCode = itemCode

            LastHandledObject = $"item {itemCode}"

            If _assessmentItem Is Nothing Then
                _assessmentItem = Me.PackageCreator.GetItemByCode(_itemCode)
            End If
        End Sub

        Public Overrides Function ProcessRequest(ByVal requestData As PublicationRequest) As ChainHandlerResult
            Dim chainHandlerRequest As ChainHandlerResult = ExecuteRequest(requestData)
            Return chainHandlerRequest
        End Function

        Protected Overridable Sub AddItemToSection(itemRefHelper As ItemRefHelper, testDocumentSet As TestDocumentSet)
            Dim itemRef As AssessmentItemRefType = itemRefHelper.CreateItemRef(testDocumentSet.AssessmentTestType)
            itemRefHelper.AddItemRefToSection(testDocumentSet.AssessmentTestType, itemRef)
        End Sub

        Protected Overridable Function GetItemRefHelper(itemRef As GeneralItemReference) As ItemRefHelper
            Return New ItemRefHelper(itemRef, PackageCreator)
        End Function

        Protected Overridable Sub ExecuteRequestActions(requestData As PublicationRequest)
            Dim testDocumentSet As TestDocumentSet = requestData.Tests(_testIdentifier)

            Dim itemRefHelper = GetItemRefHelper(_itemRef)
            AddItemToSection(itemRefHelper, testDocumentSet)
            testDocumentSet.WeightMaxScore += CType(ScoringHelper.GetItemMaxScore(_assessmentItem.Solution.GetMaxSolutionTranslatedScore, _itemRef.Weight), Decimal)
        End Sub

        Private Function ExecuteRequest(requestData As PublicationRequest) As ChainHandlerResult
            ExecuteRequestActions(requestData)
            _assessmentItem = Nothing
            Return ChainHandlerResult.RequestHandled
        End Function

    End Class
End Namespace