
Imports Questify.Builder.Logic.QTI.Helpers.QTI22
Imports Questify.Builder.Logic.QTI.Xsd.QTI22_Final

Namespace QTI22Publication.Helpers.QTI22
    <TestClass>
    Public Class ResponseDeclarationHelperTests
        <TestMethod>
        Public Sub CreateEmptyResponseDeclaration_CardinalitySingle_ReturnsCorrectCardinality
            Dim cardinality = ResponseDeclarationTypeCardinality.single

            Dim result = ResponseDeclarationHelper.CreateEmptyResponseDeclaration(cardinality, String.Empty)

            Assert.IsNotNull(result)
            Assert.AreEqual(cardinality, result.cardinality)
        End Sub

        <TestMethod>
        Public Sub CreateEmptyResponseDeclaration_Identifier_ReturnsCorrectIdentifier
            Dim identifier = "abcdefg"

            Dim result = ResponseDeclarationHelper.CreateEmptyResponseDeclaration(ResponseDeclarationTypeCardinality.single, identifier)

            Assert.IsNotNull(result)
            Assert.AreEqual(identifier, result.identifier)
        End Sub
    End Class
End NameSpace