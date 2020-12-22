Imports System.Xml.Linq
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.QTI.Helpers.QTI_Base

Namespace QTI.Converters.Processing.QTI22

    Public Class ConceptResponseProcessingSelectPoint
        Inherits ResponseProcessingPerTypeBase

        Public Sub New(responseIndex As Integer, Optional responseSubIndex As Integer = 0)
            MyBase.New(responseIndex, responseSubIndex)
        End Sub

        Protected Overrides Function GetProcessingForFact(fact As KeyFact, Optional addNotMemberOfElement As Boolean = False, Optional outcomeIdentifier As String = "") As XElement
            Dim responseId = QTIScoringHelper.GetResponseId(ResponseIndex)
            Dim result As XElement = <gte>
                                         <sum>
                                             <mapResponsePoint identifier=<%= responseId %>/>
                                         </sum>
                                         <baseValue baseType="integer">1</baseValue>
                                     </gte>

            Return result
        End Function

    End Class

End Namespace