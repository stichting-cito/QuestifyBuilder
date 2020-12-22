Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.QTI.Xsd.QTI22_Final

Namespace QTI.Converters.Declaration.QTI22

    Public Class ResponseDeclarationChoice
        Inherits ResponseDeclarationMultipleResponse

        Protected Overrides Function GetCorrectResponse(fact As KeyFact, baseValue As BaseValue) As ValueType
            Return GetCorrectResponseValueType(fact, baseValue)
        End Function

    End Class

End Namespace