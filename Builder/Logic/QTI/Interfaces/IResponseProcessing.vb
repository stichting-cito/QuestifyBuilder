Imports System.Xml.Linq
Imports Cito.Tester.ContentModel

Namespace QTI.Interfaces

    Public Interface IResponseProcessing

        Function GetProcessingForFact(ByVal fact As KeyFact, Optional addNotMemberOfElement As Boolean = False, Optional outcomeIdentifier As String = "") As XElement

    End Interface
End Namespace