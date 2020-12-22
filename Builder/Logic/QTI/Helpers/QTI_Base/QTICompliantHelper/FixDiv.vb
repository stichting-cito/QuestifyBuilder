Imports Questify.Builder.Logic.Publication
Imports Questify.Builder.Logic.QTI.Helpers.QTI_Base.QTICompliantHelper.Interfaces

Namespace QTI.Helpers.QTI_Base.QTICompliantHelper

    Public Class FixDiv
        Implements IModifyItemDocument

        Public Sub Modify(ByRef xmlDoc As Xml.XmlDocument, docHelper As DocumentHelper) Implements IModifyDocument.Modify
            Modify(xmlDoc, Guid.NewGuid.ToString, docHelper)
        End Sub

        Public Sub Modify(ByRef xmlDoc As Xml.XmlDocument, fixedId As String, docHelper As DocumentHelper)
            Dim noMoreFixes As Boolean = False
            Dim index As Integer = 0
            While Not noMoreFixes
                Dim isFixed As Boolean = False
                isFixed = xmlDoc.BringElementOutSide("div", "span", fixedId) OrElse isFixed
                isFixed = xmlDoc.BringElementOutSide("div", "p", False, False, True, fixedId) OrElse isFixed
                isFixed = xmlDoc.BringElementOutSide("div", "strong", fixedId) OrElse isFixed
                isFixed = xmlDoc.BringElementOutSide("div", "b", fixedId) OrElse isFixed
                isFixed = xmlDoc.BringElementOutSide("div", "em", fixedId) OrElse isFixed
                isFixed = xmlDoc.BringElementOutSide("div", "i", fixedId) OrElse isFixed
                index += 1
                Debug.Assert(Not index = 20, "infinit loop!")
                noMoreFixes = Not isFixed OrElse index = 20
            End While

        End Sub

    End Class
End Namespace