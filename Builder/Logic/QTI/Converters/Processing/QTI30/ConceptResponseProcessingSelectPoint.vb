Imports System.Xml.Linq
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.QTI.Helpers.QTI_Base

Namespace QTI.Converters.Processing.QTI30

    Public Class ConceptResponseProcessingSelectPoint
        Inherits ResponseProcessingPerTypeBase

        Public Sub New(responseIndex As Integer, Optional responseSubIndex As Integer = 0)
            MyBase.New(responseIndex, responseSubIndex)
        End Sub

        Protected Overrides Function GetProcessingForFact(fact As KeyFact, Optional addNotMemberOfElement As Boolean = False, Optional outcomeIdentifier As String = "") As XElement
            Dim responseId = QTIScoringHelper.GetResponseId(ResponseIndex)
            Dim result As XElement = <qti-gte>
                                         <qti-sum>
                                             <qti-map-response-point identifier=<%= responseId %>/>
                                         </qti-sum>
                                         <qti-base-value base-type="integer">1</qti-base-value>
                                     </qti-gte>

            Return result
        End Function

    End Class

End Namespace