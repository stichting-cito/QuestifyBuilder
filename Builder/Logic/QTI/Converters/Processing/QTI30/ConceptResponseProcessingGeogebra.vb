Imports System.Xml.Linq
Imports Cito.Tester.ContentModel

Namespace QTI.Converters.Processing.QTI30

    Public Class ConceptResponseProcessingGeogebra
        Inherits ConceptResponseProcessingInput

        Public Sub New(responseIndex As Integer, owner As ResponseProcessingCustomOperators, scoringParam As ScoringParameter, Optional responseSubIndex As Integer = 0)
            MyBase.New(responseIndex, owner, scoringParam, responseSubIndex)
        End Sub

        Protected Overrides Function GetProcessingForStringValue(value As BaseValue, responseIndex As Integer, keyValue As KeyValue) As XElement
            Return Owner.GetGeogebraCustomOperator(responseIndex, CType(value, StringValue).Value, ResponseSubIndex)
        End Function

    End Class

End Namespace