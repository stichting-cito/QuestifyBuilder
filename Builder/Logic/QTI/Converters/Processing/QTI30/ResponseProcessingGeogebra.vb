Imports System.Xml.Linq
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.QTI.Helpers.QTI_Base
Imports Questify.Builder.Logic.QTI.Interfaces

Namespace QTI.Converters.Processing.QTI30

    Public Class ResponseProcessingGeogebra
        Inherits ResponseProcessingInput

        Public Sub New(responseIndex As Integer, gapType As CombinedScoringHelper.EnumGapType, owner As IResponseProcessingCustomOperators, Optional responseSubIndex As Integer = 0)
            MyBase.New(responseIndex, gapType, owner, responseSubIndex)
        End Sub

        Protected Overrides Function GetProcessingForStringValue(value As BaseValue, responseIndex As Integer, keyValue As KeyValue) As XElement
            Return Owner.GetGeogebraCustomOperator(responseIndex, CType(value, StringValue).Value, ResponseSubIndex)
        End Function

    End Class

End Namespace