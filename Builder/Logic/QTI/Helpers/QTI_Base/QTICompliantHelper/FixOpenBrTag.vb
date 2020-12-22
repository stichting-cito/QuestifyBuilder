Imports System.Text.RegularExpressions
Imports System.Xml
Imports Questify.Builder.Logic.QTI.Helpers.QTI_Base.QTICompliantHelper.Interfaces

Namespace QTI.Helpers.QTI_Base.QTICompliantHelper

    Public Class FixOpenBrTag
        Implements IModifyItemDocument

        Public Sub Modify(ByRef xmlDoc As Xml.XmlDocument, docHelper As DocumentHelper) Implements IModifyDocument.Modify
            ConvertOpenBrTagToClosedBrTag(xmlDoc)
        End Sub

        Private Sub ConvertOpenBrTagToClosedBrTag(ByRef xmlDoc As XmlDocument)
            Dim regEx As New Regex("<br\b[^/>]*>.*?</br>", RegexOptions.IgnoreCase)
            xmlDoc.DocumentElement.InnerXml = regEx.Replace(xmlDoc.DocumentElement.InnerXml, "<br />")
        End Sub

    End Class
End NameSpace