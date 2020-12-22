Imports Questify.Builder.Logic.Publication
Imports Questify.Builder.Logic.QTI.Helpers.QTI_Base.QTICompliantHelper.Interfaces

Namespace QTI.Helpers.QTI_Base.QTICompliantHelper

    Public Class FixTable
        Implements IModifyItemDocument

        Public Sub Modify(ByRef xmlDoc As Xml.XmlDocument, docHelper As DocumentHelper) Implements IModifyDocument.Modify
            Dim noMoreFixes As Boolean = False
            Dim index As Integer = 0
            While Not noMoreFixes
                Dim isFixed As Boolean = False
                isFixed = xmlDoc.BringElementOutSide("table", "p")
                index += 1
                Debug.Assert(Not index = 20, "infinit loop!")
                noMoreFixes = Not isFixed OrElse index = 20
            End While

        End Sub

    End Class
End NameSpace