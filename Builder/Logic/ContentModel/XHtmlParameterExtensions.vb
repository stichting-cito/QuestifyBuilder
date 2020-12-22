Imports Cito.Tester.ContentModel
Imports System.Runtime.CompilerServices
Imports System.Xml

Namespace ContentModel

    Public Module XHtmlParameterExtensions

        Private _namespaceManager As XmlNamespaceManager

        <Extension()>
        Public Function GetResources(parameter As XHtmlParameter) As HashSet(Of String)
            Return New XHtmlResourceExtractor(parameter).ExtractResources()
        End Function

        <Extension()>
        Public Function ReplaceInlineImages(parameter As XHtmlParameter, bankId As Integer, bankName As String, itemCode As String) As HashSet(Of String)
            Return New XHtmlResourceExtractor(parameter).ReplaceInlineImages(bankId, bankName, itemCode)
        End Function

        <Extension()>
        Public Function ReplaceInlineElement(parameter As XHtmlParameter, identifier As String, toReplaceWith As InlineElement) As Boolean
            Return New XHtmlInlineElementsManipulator(parameter).ReplaceInlineElement(identifier, toReplaceWith)
        End Function

        <Extension()>
        Public Function RemoveInlineElement(parameter As XHtmlParameter, identifier As String) As Boolean
            Return New XHtmlInlineElementsManipulator(parameter).RemoveInlineElement(identifier)
        End Function

        Public Function GetNamespaceManager() As XmlNamespaceManager
            If _namespaceManager Is Nothing Then
                _namespaceManager = New XmlNamespaceManager(New NameTable())
                _namespaceManager.AddNamespace("def", "http://www.w3.org/1999/xhtml")
                _namespaceManager.AddNamespace("cito", "http://www.cito.nl/citotester")
            End If

            Return _namespaceManager
        End Function

    End Module

End Namespace

