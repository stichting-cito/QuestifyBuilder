
Namespace PluginExtensibility.Html.Converters
    ''' <summary>
    ''' A structure to allow converting of Html
    ''' </summary>
    Public Interface IHtmlConverter : Inherits IDisposable

        ''' <summary>
        ''' Method for converting aspects of the HTML
        ''' </summary>
        ''' <param name="html">html.</param>
        ''' <returns>Converted Html</returns>
        Function ConvertHtml(html As String) As String
        ''' <summary>
        ''' Gets or sets the next converter.
        ''' </summary>
        ''' <value>
        ''' The next converter.
        ''' </value>
        Property NextConverter As IHtmlConverter
        ''' <summary>
        ''' Gets the last converter.
        ''' </summary>
        ReadOnly Property LastConverter As IHtmlConverter

    End Interface
End Namespace