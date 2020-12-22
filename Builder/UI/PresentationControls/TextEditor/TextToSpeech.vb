Imports System.Linq
Imports System.Text.RegularExpressions
Imports System.Xml
Imports Questify.Builder.Logic
Imports Questify.Builder.Logic.Service.Classes

Public Class TextToSpeech

    Const TTS_MUTE_CLASSNAME As String = "TTSMute"
    Const TTS_ALTERNATIVE_CLASSNAME As String = "TTSAlternative"
    Const TTS_ALIAS_CLASSNAME As String = "TTSAlias"
    Const TTS_PAUSE_CLASSNAME As String = "TTSPause"
    Const TTS_LANGUAGE_FRENCH As String = "LangTTSFrans"
    Const TTS_LANGUAGE_ENGLISH As String = "LangTTSEngels"
    Const TTS_LANGUAGE_GERMAN As String = "LangTTSDuits"
    Const TTS_LANGUAGE_DUTCH As String = "LangTTSNederlands"
    Const CLASS_ATTRIBUTE As String = "class"

    Public Shared Sub Mute(editor As IXHtmlEditor)
        Try
            If editor.IsInline Then
                Return
            End If

            Dim selection = editor.Selection
            If selection Is Nothing OrElse selection.IsEmpty Then
                Return
            End If

            If selection.IsClassApplied(TTS_MUTE_CLASSNAME) Then Return

            If Not CheckForAppliedStyles(selection) Then Return

            selection.ApplyTag("span")

            selection.ApplyClass(TTS_MUTE_CLASSNAME, StyleType.Character)
        Catch ex As Exception
            MessageBox.Show(String.Format(My.Resources.TTSMuteCouldNotBeApplied, ex.Message))
        End Try
    End Sub

    Public Shared Sub Alternative(editor As IXHtmlEditor)
        If editor.IsInline Then
            Return
        End If

        Dim selection = editor.Selection
        If selection Is Nothing OrElse selection.IsEmpty Then
            Return
        End If
        If Not CheckForAppliedStyles(selection) Then
            Return
        End If

        If Not selection.IsEmpty Then
            If (selection.IsClassApplied(TTS_ALTERNATIVE_CLASSNAME) OrElse selection.IsClassApplied(TTS_ALIAS_CLASSNAME)) Then
                Return
            End If

            selection.ApplyTag("span")
            selection.ApplyClass(TTS_ALTERNATIVE_CLASSNAME, StyleType.Character)

            Dim innerspan = editor.Document.CreateElement("span")
            Dim innerclass = editor.Document.CreateAttribute(CLASS_ATTRIBUTE)
            innerclass.Value = TTS_ALIAS_CLASSNAME
            innerspan.Attributes.Append(innerclass)
            innerspan.InnerText = "[Uit te spreken tekst]"

            If selection.Node.NodeType = XmlNodeType.Text Then
                selection.Node.ParentNode.PrependChild(innerspan)
            Else
                selection.Node.PrependChild(innerspan)
            End If

            selection.Select()
        Else
            Dim range As ITextRange = editor.CreateRange(0, 0)
            range.XmlText = $"<span class=""{TTS_ALTERNATIVE_CLASSNAME}""><span class=""{TTS_ALIAS_CLASSNAME}"">[Uit te spreken tekst]</span>[Weer te geven tekst]</span>"
            range.Select()
        End If

    End Sub

    Public Shared Sub Pause(editor As IXHtmlEditor, duration As Integer?)
        If editor.IsInline Then
            Return
        End If

        Dim durationFromSelection As Boolean = False
        Dim pauseDuration As PauseDuration

        If Not duration.HasValue OrElse duration.Value <= 0 Then
            pauseDuration = GetDurationFromSelection(editor.Selection)
            durationFromSelection = pauseDuration IsNot Nothing

            If pauseDuration Is Nothing Then pauseDuration = GetDefaultDuration()
        Else
            pauseDuration = CType(duration.Value, PauseDuration)
        End If

        If pauseDuration Is Nothing Then Return

        Dim range As ITextRange
        If Not durationFromSelection Then
            range = editor.CreateRange(editor.Selection.StartIndex, 0)
        Else
            range = editor.CreateRange(editor.Selection.StartIndex, editor.Selection.Length)
        End If

        If (range.IsClassApplied(TTS_PAUSE_CLASSNAME)) Then
        Else
            range.XmlText = String.Format(" <span class=""TTSPause PauseDuration_{0}"">{1}</span> ", pauseDuration.Duration.ToString(), pauseDuration.Name)
        End If

        range = editor.CreateRange(editor.Selection.StartIndex, 0)
        range.Select()

    End Sub

    Public Shared Function CanRemove(selection As ISelection) As Boolean
        If selection Is Nothing Then
            Return False
        End If
        Dim ttsClassnames As String() = New String() {
                                              TTS_ALIAS_CLASSNAME,
                                              TTS_ALTERNATIVE_CLASSNAME,
                                              TTS_LANGUAGE_DUTCH,
                                              TTS_LANGUAGE_ENGLISH,
                                              TTS_LANGUAGE_FRENCH,
                                              TTS_LANGUAGE_GERMAN,
                                              TTS_MUTE_CLASSNAME,
                                              TTS_PAUSE_CLASSNAME}
        Dim stringToCheck = selection.XmlText
        If String.IsNullOrEmpty(stringToCheck) Then
            If selection?.Node?.ParentNode Is Nothing Then
                Return False
            End If
            stringToCheck = selection.Node.ParentNode.OuterXml
        End If
        If String.IsNullOrEmpty(stringToCheck) Then
            If selection?.Node?.ParentNode?.ParentNode Is Nothing Then
                Return False
            End If
            stringToCheck = selection.Node.ParentNode.ParentNode.OuterXml
        End If
        If String.IsNullOrEmpty(stringToCheck) Then
            Return False
        End If
        For Each c In ttsClassnames
            If stringToCheck.Contains(c) Then Return True
        Next
        Return False

    End Function
    Public Shared Sub Remove(selection As ISelection, document As XmlDocument)
        RemovePause(selection)
        RemoveAlternative(selection, document)
        RemoveClass(selection, TTS_MUTE_CLASSNAME, document)
        RemoveClass(selection, TTS_LANGUAGE_DUTCH, document)
        RemoveClass(selection, TTS_LANGUAGE_ENGLISH, document)
        RemoveClass(selection, TTS_LANGUAGE_FRENCH, document)
        RemoveClass(selection, TTS_LANGUAGE_GERMAN, document)
    End Sub

    Private Shared Sub RemoveAlternative(selection As ISelection, document As XmlDocument)
        Dim altnode = FindNode(selection, TTS_ALTERNATIVE_CLASSNAME)
        If altnode IsNot Nothing AndAlso altnode.ChildNodes?.OfType(Of XmlText)?.FirstOrDefault IsNot Nothing Then
            Dim replacement = altnode.ChildNodes.OfType(Of XmlText).First.InnerText
            Dim toBeReplaced = altnode.OuterXml.Replace($" xmlns=""{altnode.NamespaceURI}""", String.Empty)
            Dim bodyNode = document.GetElementsByTagName("body")(0)
            Dim xmlnsStrippedBodyNodeContent = bodyNode.InnerXml.Replace($" xmlns=""{altnode.NamespaceURI}""", String.Empty)
            If bodyNode IsNot Nothing Then
                bodyNode.InnerXml = xmlnsStrippedBodyNodeContent.Replace(toBeReplaced, replacement)
            End If
        End If
    End Sub

    Private Shared Function FindNode(selection As ISelection, ttsClass As String) As XmlNode
        If selection?.Node?.ParentNode IsNot Nothing AndAlso selection.Node.ParentNode.ParentNode IsNot Nothing AndAlso selection.Node.ParentNode.ParentNode.Attributes?("class")?.Value?.Contains(ttsClass) Then
            Return selection.Node.ParentNode.ParentNode
        ElseIf selection?.Node?.ParentNode IsNot Nothing AndAlso selection.Node.ParentNode.Attributes?("class")?.Value?.Contains(ttsClass) Then
            Return selection.Node.ParentNode
        End If
        For Each child As XmlNode In selection?.Node.ParentNode.ChildNodes()
            If child.Attributes?("class")?.Value?.Contains(ttsClass) Then
                Return child
            End If
        Next
        For Each child As XmlNode In selection.Node.ChildNodes
            If child.Attributes?("class")?.Value?.Contains(ttsClass) Then
                Return child
            End If
        Next
        Return Nothing
    End Function

    Private Shared Sub RemoveClass(selection As ISelection, ttsClassname As String, document As XmlDocument)
        Dim foundNode = FindNode(selection, ttsClassname)
        If foundNode IsNot Nothing Then
            If ttsClassname = TTS_ALTERNATIVE_CLASSNAME Then
                document.DocumentElement.ChildNodes(1).InnerXml = document.DocumentElement.ChildNodes(1).InnerXml.Replace(foundNode.OuterXml, foundNode.ChildNodes(1).OuterXml)
            Else
                Dim toBeReplaced = foundNode.OuterXml.Replace($" xmlns=""{foundNode.NamespaceURI}""", String.Empty)
                Dim xmlnsStrippedInnerXml = document.DocumentElement.ChildNodes(1).InnerXml.Replace($" xmlns=""{foundNode.NamespaceURI}""", String.Empty)
                document.DocumentElement.ChildNodes(1).InnerXml = xmlnsStrippedInnerXml.Replace(toBeReplaced, foundNode.InnerXml)
            End If
        End If
    End Sub

    Private Shared Sub RemovePause(selection As ISelection)
        Dim pauseNode As XmlNode = FindNode(selection, TTS_PAUSE_CLASSNAME)
        If pauseNode IsNot Nothing Then
            selection.Node.ParentNode.ParentNode.InnerXml = selection.Node.ParentNode.ParentNode.InnerXml.Replace($" {pauseNode.OuterXml}", String.Empty)
        End If
    End Sub

    Private Shared Function GetDurationFromSelection(selection As ISelection) As PauseDuration
        If selection Is Nothing OrElse selection.IsEmpty Then
            Return Nothing
        End If

        Dim duration As Integer
        If Integer.TryParse(selection.Text.Trim(), duration) Then
            Return CType(duration, PauseDuration)
        End If

        Return Nothing
    End Function


    Private Shared Function GetDefaultDuration() As PauseDuration
        Dim fromConfig As PauseDuration() = PauseDuration.FromConfig
        Dim duration As PauseDuration = fromConfig.FirstOrDefault(Function(pd) pd.Name.Equals("gemiddeld", StringComparison.InvariantCultureIgnoreCase) OrElse pd.Name.Equals("normaal", StringComparison.InvariantCultureIgnoreCase))

        If duration Is Nothing AndAlso fromConfig.Any() Then
            duration = fromConfig.Skip(CType(Math.Floor(fromConfig.Count / 2), Integer)).First()
        End If

        Return duration
    End Function

    Private Shared Function CheckForAppliedStyles(selection As ISelection) As Boolean
        Dim regExpression As New Regex("(<\/?){1}([A-Za-z0-9]+){1}(\s[^<>])*(>|\/>){1}")
        Dim permissionAsked As Boolean = False
        If String.IsNullOrEmpty(selection.XmlText) Then Return True

        Dim paragraphStrippedXml As String = StripParagraphTag(selection.XmlText)

        While regExpression.IsMatch(paragraphStrippedXml)
            If Not permissionAsked Then
                If MessageBox.Show(My.Resources.TTSRemovesMarkupConfirmationMessage, My.Resources.TTSRemovesMarkupConfirmationTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = DialogResult.Yes Then
                    permissionAsked = True
                Else
                    Return False
                End If
            End If

            Dim tagName As String = regExpression.Match(paragraphStrippedXml).Groups(2).Value
            selection.RemoveTag(tagName)
            paragraphStrippedXml = StripParagraphTag(selection.XmlText)
        End While

        Return True
    End Function

    Private Shared Function StripParagraphTag(input As String) As String
        Dim result As String = input.Replace("<p>", String.Empty)
        result = result.Replace("</p>", String.Empty)
        Return result
    End Function
End Class
