Imports System.Linq
Imports System.Xml

Namespace QTI.Helpers.QTI_Base

    Public Class TextToSpeechHelper

        Private Const XPATH_CLASS_QUERY As String = "//*[contains(@class, '{0}')]"
        Private Const CLASS_ATTRIBUTE As String = "class"

        Private Const MUTE_CLASS As String = "TTSMute"
        Private Const ALTERNATIVE_CLASS As String = "TTSAlternative"
        Private Const ALIAS_CLASS As String = "TTSAlias"
        Private Const PAUSE_CLASS As String = "TTSPause"
        Private Const PAUSEDURATION_PREFIX = "PauseDuration_"
        Private Const PAUSEDURATION_UNIT As String = "ms"

        Public Shared Sub ConvertToSsml(ByRef itemDocument As XmlDocument, ssmlNamepsace As String)
            Dim xmlNamespaceManager As New XmlNamespaceManager(itemDocument.NameTable)
            xmlNamespaceManager.AddNamespace("qti", itemDocument.DocumentElement.NamespaceURI)
            xmlNamespaceManager.AddNamespace("html", "http://www.w3.org/1999/xhtml")
            xmlNamespaceManager.AddNamespace("ssml", ssmlNamepsace)

            If itemDocument Is Nothing OrElse itemDocument.DocumentElement Is Nothing Then Return

            ConvertSsmlMuteTags(itemDocument, xmlNamespaceManager, ssmlNamepsace)
            ConvertSsmlAlternativeTags(itemDocument, xmlNamespaceManager, ssmlNamepsace)
            ConvertSsmlPauseTags(itemDocument, xmlNamespaceManager, ssmlNamepsace)
        End Sub

        Private Shared Sub ConvertSsmlPauseTags(itemDocument As XmlDocument, xmlNamespaceManager As XmlNamespaceManager, ssmlNamepsace As String)
            Dim ssmlPauseNodes = itemDocument.DocumentElement.SelectNodes(String.Format(XPATH_CLASS_QUERY, PAUSE_CLASS), xmlNamespaceManager)

            For Each ssmlPauseNode As XmlNode In ssmlPauseNodes
                Dim newNode As XmlNode = itemDocument.CreateElement("ssml:break", ssmlNamepsace)
                Dim timeAttribute = itemDocument.CreateAttribute("time")
                Dim durationClass = ssmlPauseNode.Attributes(CLASS_ATTRIBUTE).Value.Split(" "c).FirstOrDefault(Function(a) a.StartsWith(PAUSEDURATION_PREFIX))
                Dim durationValue = durationClass.Replace(PAUSEDURATION_PREFIX, String.Empty)
                Dim duration As Integer
                If Not String.IsNullOrEmpty(durationValue) AndAlso Integer.TryParse(durationValue, duration) Then
                    timeAttribute.Value = String.Format("{0}" + PAUSEDURATION_UNIT, durationValue)
                Else
                    Continue For
                End If
                newNode.Attributes.Append(timeAttribute)

                ClearTTSMarkup(newNode, ssmlPauseNode, durationClass)
                ClearTTSMarkup(newNode, ssmlPauseNode, PAUSE_CLASS)
                ssmlPauseNode.ParentNode.ReplaceChild(newNode, ssmlPauseNode)
            Next
        End Sub

        Private Shared Sub ConvertSsmlMuteTags(ByRef itemDocument As XmlDocument, xmlNamespaceManager As XmlNamespaceManager, ssmlNamepsace As String)
            Dim ssmlMuteNodes = itemDocument.DocumentElement.SelectNodes(String.Format(XPATH_CLASS_QUERY, MUTE_CLASS), xmlNamespaceManager)

            For Each ssmlMuteNode As XmlNode In ssmlMuteNodes
                Dim newNode As XmlNode = itemDocument.CreateElement("ssml:prosody", ssmlNamepsace)
                Dim volumeAttribute = itemDocument.CreateAttribute("volume")
                volumeAttribute.Value = "silent"
                newNode.Attributes.Append(volumeAttribute)

                ClearTTSMarkup(newNode, ssmlMuteNode, MUTE_CLASS)
                CheckForNotAllowedTags(newNode, xmlNamespaceManager, ssmlNamepsace)
                ssmlMuteNode.ParentNode.ReplaceChild(newNode, ssmlMuteNode)
            Next
        End Sub

        Private Shared Sub ConvertSsmlAlternativeTags(itemDocument As XmlDocument, xmlNamespaceManager As XmlNamespaceManager, ssmlNamepsace As String)
            Dim ssmlAlternativeNodes = itemDocument.DocumentElement.SelectNodes(String.Format(XPATH_CLASS_QUERY, ALTERNATIVE_CLASS), xmlNamespaceManager)

            For Each ssmlAlternativeNode As XmlNode In ssmlAlternativeNodes
                Dim aliasNode = ssmlAlternativeNode.SelectSingleNode(String.Format(XPATH_CLASS_QUERY, ALIAS_CLASS), xmlNamespaceManager)
                Dim newNode As XmlNode = itemDocument.CreateElement("ssml:sub", ssmlNamepsace)
                Dim aliasAttribute = itemDocument.CreateAttribute("alias")

                If Not String.IsNullOrEmpty(aliasNode.Attributes("data-alias")?.Value) Then
                    aliasAttribute.Value = aliasNode.Attributes("data-alias").Value
                Else
                    aliasAttribute.Value = aliasNode.InnerText
                End If

                newNode.Attributes.Append(aliasAttribute)
                ClearTTSMarkup(newNode, ssmlAlternativeNode, ALTERNATIVE_CLASS)

                Dim newAliasNode = newNode.SelectSingleNode(String.Format(XPATH_CLASS_QUERY, ALIAS_CLASS), xmlNamespaceManager)
                If newAliasNode IsNot Nothing AndAlso newAliasNode.LocalName = "span" Then
                    newAliasNode.ParentNode.RemoveChild(newAliasNode)
                Else
                    Dim classAttribute = newAliasNode.Attributes.GetNamedItem(CLASS_ATTRIBUTE)
                    If (classAttribute IsNot Nothing) Then
                        classAttribute.Value = classAttribute.Value.Replace(ALIAS_CLASS, String.Empty).Trim()
                    End If
                    newNode.AppendChild(newAliasNode.Clone())
                End If

                ssmlAlternativeNode.ParentNode.ReplaceChild(newNode, ssmlAlternativeNode)

            Next
        End Sub

        Private Shared Sub ClearTTSMarkup(newNode As XmlNode, ssmlNode As XmlNode, [class] As String)

            Dim classAttribute = ssmlNode.Attributes.GetNamedItem(CLASS_ATTRIBUTE)
            Dim className = classAttribute?.Value.Replace([class], String.Empty).Trim()

            If (String.IsNullOrEmpty(className) AndAlso ssmlNode.LocalName = "span") Then
                newNode.InnerXml = ssmlNode.InnerXml
            Else
                If (classAttribute IsNot Nothing) Then
                    classAttribute.Value = classAttribute.Value.Replace([class], String.Empty).Trim()
                End If

                newNode.AppendChild(ssmlNode.Clone())
            End If
        End Sub

        Private Shared Sub CheckForNotAllowedTags(ByRef newNode As XmlNode, xmlNamespaceMgr As XmlNamespaceManager, ssmlNamepsace As String)
            Dim childNodes As XmlNodeList = newNode.SelectNodes("/*", xmlNamespaceMgr)
            If (childNodes.Count > 0) Then
                Dim n = newNode.OwnerDocument.CreateElement("ssml:s", ssmlNamepsace)
                n.InnerXml = newNode.InnerXml
                newNode.InnerXml = n.OuterXml
            End If
        End Sub
    End Class
End Namespace