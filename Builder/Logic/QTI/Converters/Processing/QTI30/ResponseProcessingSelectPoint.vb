Imports System.Xml.Linq
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.QTI.Helpers.QTI_Base

Namespace QTI.Converters.Processing.QTI30

    Public Class ResponseProcessingSelectPoint
        Inherits ResponseProcessingPerTypeBase

        Dim _shouldBeTranslated As Boolean

        Public Sub New(responseIndex As Integer, Optional responseSubIndex As Integer = 0)
            MyBase.New(responseIndex, responseSubIndex)
        End Sub

        Protected Overrides Function GetProcessingForFact(fact As KeyFact, Optional addNotMemberOfElement As Boolean = False, Optional outcomeIdentifier As String = "") As XElement
            Dim responseId = QTIScoringHelper.GetResponseId(ResponseIndex)
            Dim outcome As XElement = <qti-set-outcome-value identifier=<%= outcomeIdentifier %>>
                                          <qti-sum>
                                              <qti-map-response-point identifier=<%= responseId %>/>
                                          </qti-sum>
                                      </qti-set-outcome-value>

            Return outcome
        End Function

    End Class

End Namespace
