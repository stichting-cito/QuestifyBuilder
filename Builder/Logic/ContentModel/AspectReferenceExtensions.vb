Imports System.Runtime.CompilerServices
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.HelperClasses

Namespace ContentModel
    Public Module AspectReferenceExtensions
        <Extension>
        Public Function GetResources(aspectReference As AspectReference) As HashSet(Of String)
            Dim xHtml As New XHtmlParameter()
            xHtml.Value = aspectReference.Description
            Dim xHtmlResourceExtractor As New XHtmlResourceExtractor(xHtml)

            Dim result = xHtmlResourceExtractor.ExtractResources()

            If Not String.IsNullOrEmpty(aspectReference.SourceName) AndAlso Not result.Contains(aspectReference.SourceName) AndAlso Not AspectHelper.IsDefaultResourceAspect(aspectReference.SourceName) Then
                result.Add(aspectReference.SourceName)
            End If

            Return result
        End Function
    End Module

End Namespace
