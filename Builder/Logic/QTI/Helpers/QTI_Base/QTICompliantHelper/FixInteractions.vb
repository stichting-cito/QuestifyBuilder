Imports System.Xml
Imports Questify.Builder.Logic.Publication
Imports Questify.Builder.Logic.QTI.Helpers.QTI_Base.QTICompliantHelper.Interfaces

Namespace QTI.Helpers.QTI_Base.QTICompliantHelper

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
                isFixed = xmlDoc.BringElementOutSide("mediaInteraction", "p", "span", True, True, True, fixedId) OrElse isFixed
                isFixed = xmlDoc.BringElementOutSide("mediaInteraction", "span", fixedId) OrElse isFixed
                isFixed = xmlDoc.BringElementOutSide("mediaInteraction", "i", fixedId) OrElse isFixed
                isFixed = xmlDoc.BringElementOutSide("mediaInteraction", "em", fixedId) OrElse isFixed
                isFixed = xmlDoc.BringElementOutSide("mediaInteraction", "b", fixedId) OrElse isFixed
                isFixed = xmlDoc.BringElementOutSide("customInteraction", "p", "span", True, addToDiv, False, fixedId) OrElse isFixed
                isFixed = xmlDoc.BringElementOutSide("customInteraction", "i", fixedId) OrElse isFixed
                isFixed = xmlDoc.BringElementOutSide("customInteraction", "em", fixedId) OrElse isFixed
                isFixed = xmlDoc.BringElementOutSide("customInteraction", "b", fixedId) OrElse isFixed
                isFixed = xmlDoc.BringElementOutSide("extendedTextInteraction", "p", "span", True, addToDiv, True, fixedId) OrElse isFixed
                isFixed = xmlDoc.BringElementOutSide("extendedTextInteraction", "i", fixedId) OrElse isFixed
                isFixed = xmlDoc.BringElementOutSide("extendedTextInteraction", "em", fixedId) OrElse isFixed
                isFixed = xmlDoc.BringElementOutSide("extendedTextInteraction", "b", fixedId) OrElse isFixed
                index += 1
                Debug.Assert(Not index = 20, "infinit loop!")
                noMoreFixes = Not isFixed OrElse index = 20
            End While

        End Sub

    End Class
End Namespace