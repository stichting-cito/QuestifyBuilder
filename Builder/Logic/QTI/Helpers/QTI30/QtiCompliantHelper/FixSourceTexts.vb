Imports System.Xml
Imports Questify.Builder.Logic.QTI.Helpers.QTI_Base
Imports Questify.Builder.Logic.QTI.Helpers.QTI_Base.QTICompliantHelper.Interfaces

Namespace QTI.Helpers.QTI30.QTICompliantHelper

    Public Class FixSourceTexts
        Implements IModifyItemDocument

        Public Sub Modify(ByRef xmlDoc As XmlDocument, docHelper As DocumentHelper) Implements IModifyDocument.Modify
            Dim sharedStimuli = xmlDoc.SelectNodes($"//div[contains(@class, 'qti-shared-stimulus') and @data-stimulus-idref]")
            If sharedStimuli.Count <= 0 Then
                Return
            End If

            Dim leftBody = xmlDoc.SelectSingleNode("//div[@class='qti-layout-col6']")
            If leftBody Is Nothing Then
                Return
            End If

            For Each stimulus As XmlNode In sharedStimuli
                leftBody.AppendChild(stimulus)
            Next
        End Sub
    End Class

End Namespace
