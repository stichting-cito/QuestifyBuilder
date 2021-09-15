Imports System.Xml
Imports Questify.Builder.Logic.Publication
Imports Questify.Builder.Logic.QTI.Helpers.QTI_Base
Imports Questify.Builder.Logic.QTI.Helpers.QTI_Base.QTICompliantHelper.Interfaces

Namespace QTI.Helpers.QTI30.QTICompliantHelper

    Public Class FixInteractions
        Implements IModifyItemDocument

        Public Sub Modify(ByRef xmlDoc As XmlDocument, docHelper As DocumentHelper) Implements IModifyDocument.Modify
            Modify(xmlDoc, Guid.NewGuid.ToString, docHelper)
        End Sub

        Public Sub Modify(ByRef xmlDoc As XmlDocument, fixedId As String, docHelper As DocumentHelper)
            Dim noMoreFixes As Boolean = False
            Dim index As Integer = 0
            While Not noMoreFixes
                Dim isFixed = False
                Dim addToDiv As Boolean = CombinedScoringHelper.ItemContainsGapmatchInteraction(xmlDoc)
                isFixed = xmlDoc.BringElementOutSide("qti-media-interaction", "p", "span", True, True, True, fixedId) OrElse isFixed
                isFixed = xmlDoc.BringElementOutSide("qti-media-interaction", "span", fixedId) OrElse isFixed
                isFixed = xmlDoc.BringElementOutSide("qti-media-interaction", "i", fixedId) OrElse isFixed
                isFixed = xmlDoc.BringElementOutSide("qti-media-interaction", "em", fixedId) OrElse isFixed
                isFixed = xmlDoc.BringElementOutSide("qti-media-interaction", "b", fixedId) OrElse isFixed
                isFixed = xmlDoc.BringElementOutSide("qti-custom-interaction", "p", "span", True, addToDiv, False, fixedId) OrElse isFixed
                isFixed = xmlDoc.BringElementOutSide("qti-custom-interaction", "i", fixedId) OrElse isFixed
                isFixed = xmlDoc.BringElementOutSide("qti-custom-interaction", "em", fixedId) OrElse isFixed
                isFixed = xmlDoc.BringElementOutSide("qti-custom-interaction", "b", fixedId) OrElse isFixed
                isFixed = xmlDoc.BringElementOutSide("qti-extended-text-interaction", "p", "span", True, addToDiv, True, fixedId) OrElse isFixed
                isFixed = xmlDoc.BringElementOutSide("qti-extended-text-interaction", "i", fixedId) OrElse isFixed
                isFixed = xmlDoc.BringElementOutSide("qti-extended-text-interaction", "em", fixedId) OrElse isFixed
                isFixed = xmlDoc.BringElementOutSide("qti-extended-text-interaction", "b", fixedId) OrElse isFixed
                index += 1
                Debug.Assert(Not index = 20, "infinit loop!")
                noMoreFixes = Not isFixed OrElse index = 20
            End While

        End Sub

    End Class
End Namespace