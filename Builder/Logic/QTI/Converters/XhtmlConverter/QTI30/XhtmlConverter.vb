Imports System.Xml
Imports Cito.Tester.Common
Imports Questify.Builder.Logic.QTI.Converters.XhtmlConverter.QTI_Base

Namespace QTI.Converters.XhtmlConverter.QTI30

    Public Class QTI30XhtmlConverter
        Inherits QTIXhtmlConverter

        Public Sub New()
            MyBase.New()
        End Sub

        Protected Overrides Sub RemoveIllegalParentNodesFromMediaInteraction(ByRef doc As XHtmlDocument)
            For Each mediaInteractionNode As XmlNode In doc.SelectNodes("//qti-media-interaction")
                RemoveIllegalParentNodesOfMediaInteractionControl(mediaInteractionNode.ParentNode)
            Next
        End Sub

        Protected Overrides Sub StripAllUnknownTags(dummyElement As XmlNode, checkRoot As Boolean)
            Dim listOfAllowedRootTags As String = "qti-response-declaration|qti-outcome-declaration|qti-stylesheet|qti-item-body|qti-response-processing"
            Dim listOfAllowedTags As String = "qti-position-object-stage|qti-gap-match-interaction|qti-match-interaction|qti-graphic-gap-match-interaction|qti-hotspot-interaction|qti-graphic-order-interaction|qti-hottext-interaction|qti-portable-custom-interaction|" &
                                              "qti-select-point-interaction|qti-graphic-associate-interaction|qti-slider-interaction|qti-choice-interaction|qti-custom-interaction|qti-media-interaction|qti-order-interaction|qti-extended-text-interaction|" &
                                              "qti-associate-interaction|qti-upload-interaction|qti-companion-materials-info|pre|h2|h3|h1|h6|h4|h5|p|address|dl|ol|hr|qti-rubric-block|blockquote|qti-feedback-block|ul|table|div|xi:include|m:math|&nbsp;"


            If checkRoot Then
                RemoveUnsuppportedElements(dummyElement, listOfAllowedRootTags)

                For Each childNode As XmlNode In dummyElement.ChildNodes
                    If childNode.LocalName.ToString.ToLower = "qti-item-body" Then
                        RemoveUnsuppportedElements(childNode, listOfAllowedTags)

                        For Each node As XmlNode In childNode.ChildNodes
                            CleanupChildNodes(node)
                        Next
                    End If
                Next
            Else
                RemoveUnsuppportedElements(dummyElement, listOfAllowedTags)

                For Each childNode As XmlNode In dummyElement.ChildNodes
                    CleanupChildNodes(childNode)
                Next
            End If
        End Sub

        Protected Overrides Function GetSupportedElements(elementName As String) As String
            Dim returnValue As String = "*"

            Select Case elementName.ToLower
                Case "h1", "h2", "h3", "h4", "h5", "h6", "p", "span"
                    returnValue = "qti-text-entry-interaction|qti-inline-choice-interaction|qti-end-attempt-interaction|qti-hottext|img|br|qti-printed-variable|object|qti-gap|em|a|code|span|sub|acronym|big" &
                                  "|tt|kbd|q|i|dfn|qti-feedback-inline|abbr|strong|sup|var|small|samp|b|cite|xi:include|m:math|qti-hottext"
                Case "ol"
                    returnValue = "li"
                Case "td", "div", "li"
                    returnValue = "pre|h2|h3|h1|h6|h4|h5|p|address|dl|ol|img|br|ul|hr|qti-printed-variable|object|qti-rubric-block|blockquote|qti-feedback-block" &
                                  "|qti-hottext|em|a|code|span|sub|acronym|big|tt|kbd|q|i|dfn|qti-feedback-inline|abbr|strong|sup|var|small|samp|b|cite|table" &
                                  "|div|xi:include|m:math|qti-text-entry-interaction|qti-inline-choice-interaction|qti-end-attempt-interaction|qti-custom-interaction|qti-portable-custom-interaction|qti-gap-match-interaction" &
                                  "|qti-match-interaction|qti-graphic-gap-match-interaction|qti-hotspot-interaction|qti-graphic-order-interaction|qti-select-point-interaction|qti-graphic-associate-interaction" &
                                  "|qti-slider-interaction|qti-choice-interaction|qti-media-interaction|qti-order-interaction|qti-extended-text-interaction|qti-associate-interaction|qti-hottext-interaction|qti-position-object-stage" &
                                  "|qti-upload-interaction|qti-companion-materials-info"
                Case "table"
                    returnValue = "caption|col|colgroup|thead|tfoot|tbody"
                Case "tbody"
                    returnValue = "tr"
                Case "colgroup"
                    returnValue = "col"
                Case "tr"
                    returnValue = "th|td"
            End Select

            Return returnValue
        End Function

        Protected Overrides Function GetSupportedAttributes(elementName As String) As String
            Dim returnValue As String = "*"

            Select Case elementName.ToLower
                Case "h1", "h2", "h3", "h4", "h5", "h6", "p", "span", "div", "pre"
                    returnValue = "class|id|label|xml:base|xml:lang|xsi:type|data-alias|data-dep-dialog~|data-stimulus~"
                Case "ol", "sup", "sub", "tbody", "tr", "ul"
                    returnValue = "class|id|label|xml:base|xml:lang|xsi:type"
                Case "li"
                    returnValue = "class|id|label|xml:base|xml:lang|xsi:type|data-value"
                Case "img"
                    returnValue = "alt|src|height|width|longdesc|class|id|label|xml:base|xml:lang|xsi:type"
                Case "td"
                    returnValue = "abbr|axis|colspan|rowspan|scope|headers|class|id|label|header|xml:lang|xsi:type"
                Case "table"
                    returnValue = "summary|class|id|label|xml:base|xml:lang|xsi:type"
                Case "col", "colgroup"
                    returnValue = "class|id|label|span|xml:lang|xsi:type"
                Case "qti-media-interaction"
                    returnValue = "class|id|label|span|response-identifier|autostart|max-plays|xml:lang|xsi:type"
            End Select

            Return returnValue
        End Function

    End Class
End Namespace