Imports System.Xml.Linq
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.QTI.Helpers.QTI_Base
Imports Questify.Builder.Logic.QTI.Interfaces

Namespace QTI.Converters.Processing.QTI22

    Public Class ResponseProcessingPerTypeBase
        Implements IResponseProcessing


        Protected ResponseIndex As Integer = 0
        Protected ResponseSubIndex As Integer = 0
        Protected ScoringParameter As ScoringParameter



        Public Sub New(responseIndex As Integer, Optional responseSubIndex As Integer = 0, Optional scoringParameter As ScoringParameter = Nothing)
            Me.ResponseIndex = responseIndex
            Me.ResponseSubIndex = responseSubIndex
            Me.ScoringParameter = scoringParameter
        End Sub


        Protected Overridable Function GetProcessingForFact(fact As KeyFact, Optional addNotMemberOfElement As Boolean = False, Optional outcomeIdentifier As String = "") As XElement Implements IResponseProcessing.GetProcessingForFact
            Return Nothing
        End Function

        Protected Overridable Function GetProcessingForVariable() As XElement
            Return GetProcessingForVariable(ResponseIndex)
        End Function

        Protected Overridable Function GetProcessingForVariable(responseIndex As Integer) As XElement
            Dim stringVariable = <variable identifier=<%= QTIScoringHelper.GetResponseId(responseIndex) %>/>
            Dim subIndex = <index n="{0}">{1}</index>
            If ResponseSubIndex > 0 Then
                Return XElement.Parse(String.Format(subIndex.ToString, ResponseSubIndex, stringVariable))
            Else
                Return stringVariable
            End If
        End Function

    End Class
End Namespace