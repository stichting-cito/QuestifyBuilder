Imports System.IO
Imports Cito.Tester.Common

Namespace HtmlHelpers

    Public Class ResourceManagerWithEmbeddedResources : Inherits ResourceManagerDecorator

        Public Sub New(decoree As ResourceManagerBase)
            MyBase.New(decoree)
        End Sub

        Public Overrides Function GetResource(ByVal name As String) As StreamResource
            Dim result = MyBase.GetResource(name)

            If (result Is Nothing) AndAlso (InlineMediaTemplateHelper.IsEmbeddedResourceInlineMediaTemplate(name)) Then
                Return New StreamResource(New MemoryStream(New Byte() {1}))
            End If

            Return result
        End Function
    End Class
End Namespace