Imports System.Xml

Namespace PluginExtensibility.Html.Converters

    Friend MustInherit Class HtmlConverterBase
        Implements IHtmlConverter

        Private _next As IHtmlConverter

        Protected MustOverride Function DoConvert(html As String) As String

        Public Function ConvertHtml(html As String) As String Implements IHtmlConverter.ConvertHtml
            Return If(_next Is Nothing, DoConvert(html), _next.ConvertHtml(DoConvert(html)))
        End Function

        Public ReadOnly Property LastConverter As IHtmlConverter Implements IHtmlConverter.LastConverter
            Get
                If (_next Is Nothing) Then Return Me
                Return _next.LastConverter
            End Get
        End Property

        Public Property NextConverter As IHtmlConverter Implements IHtmlConverter.NextConverter
            Get
                Return _next
            End Get
            Set(value As IHtmlConverter)
                Debug.Assert(_next Is Nothing AndAlso value IsNot Nothing, "You are breaking the chain!")
                _next = value
            End Set
        End Property

        Protected Function GetNamespaceMng() As XmlNamespaceManager
            Dim _defaultNamespaceManager As New XmlNamespaceManager(New System.Xml.NameTable())
            _defaultNamespaceManager.AddNamespace("def", "http://www.w3.org/1999/xhtml")
            _defaultNamespaceManager.AddNamespace("cito", "http://www.cito.nl/citotester")
            Return _defaultNamespaceManager
        End Function


        Private _disposedValue As Boolean

        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not _disposedValue Then
                If disposing Then
                End If


            End If
            _disposedValue = True
        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub


    End Class
End Namespace