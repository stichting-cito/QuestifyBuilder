
Public Class baseHtmlConverterTest

    Public Function ContainsRootElement(html As String) As Boolean
        Return html.Contains("<root>") Or html.Contains("</root>")
    End Function

    Public Function ContainsCitoRefAttributtes(html As String) As Boolean
        'Note: detection is not flawless.
        Return html.Contains("cito:type") Or
            html.Contains("cito:reftype") Or
            html.Contains("cito:description") Or
            html.Contains("cito:value")
    End Function

    Public Function ContainsC1RefAttributtes(html As String) As Boolean
        'Note: detection is not flawless.
        Return html.Contains("cito_type") Or
            html.Contains("cito_reftype") Or
            html.Contains("cito_description") Or
            html.Contains("cito_value")
    End Function

    Protected Function ContainsXmlNSCito(html As String) As Boolean
        Return html.Contains("=""http://www.cito.nl/citotester""")
    End Function

End Class
