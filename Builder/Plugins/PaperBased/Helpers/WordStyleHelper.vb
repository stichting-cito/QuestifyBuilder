Imports System
Imports DocumentFormat.OpenXml.Packaging

Imports DocumentFormat.OpenXml.Wordprocessing
Imports NotesFor.HtmlToOpenXml
Imports Questify.Builder.Logic.Service.HelperFunctions

Friend Class WordStyleHelper
    Friend ReadOnly Property StylesList() As Dictionary(Of String, TableCellProperties)

    Public Event StylesChanged()

    Sub New()
        StylesList = New Dictionary(Of String, TableCellProperties)
    End Sub

    Friend Sub ExtractStyles(ByRef html As String)
        Dim xDoc = XDocument.Parse($"<root>{html}</root>", LoadOptions.PreserveWhitespace)

        Dim tcElements = xDoc.Descendants(XName.Get("td", "http://www.w3.org/1999/xhtml"))

        For Each tcElement In tcElements
            tcElement.SetAttributeValue("id", If(tcElement.Attribute("id")?.Value, Guid.NewGuid().ToString()))

            Dim key As String = $"TCB_{tcElement.Attribute("id").Value}"
            If Not StylesList.ContainsKey(key) Then
                tcElement.SetAttributeValue("class", $"{tcElement.Attribute("class")?.Value} {key}".Trim())
                StylesList.Add(key, New TableCellProperties())
            End If

            StylesList(key).TableCellBorders = TableCellBorderExtractor.Extract(tcElement)
            StylesList(key).TableCellWidth = WidthExtractor.Extract(tcElement)
        Next

        html = xDoc.Root.InnerXml()
    End Sub

    Friend Sub HtmlStyleMissing(sender As Object, e As StyleEventArgs)
        If e Is Nothing Then
            Return
        End If

        ApplyStyleToTableCellIfExists(e.Name, e.StyleDefinitionsPart)
    End Sub

    Friend Sub ApplyStyleToTableCellIfExists(name As String, styleDefinitionsPart As StyleDefinitionsPart)
        If Not StylesList.ContainsKey(name) Then
            Return
        End If

        If (styleDefinitionsPart Is Nothing) Then
            Return
        End If

        If styleDefinitionsPart.Styles Is Nothing Then
            styleDefinitionsPart.Styles = New Styles()
        End If

        Dim styles = styleDefinitionsPart.Styles
        If styles.Descendants(Of Style).Any(Function(d) String.Equals(d.StyleName?.Val, name, StringComparison.InvariantCultureIgnoreCase)) Then
            Return
        End If

        Dim styleId = $"style{styles.Count() + 1}"
        Dim style As New Style With {.Type = StyleValues.Table, .StyleId = styleId, .CustomStyle = True}
        style.Append(New StyleName() With {.Val = name})

        StylesList(styleId) = StylesList(name)

        styles.Append(style)
        styles.Save()

        RaiseEvent StylesChanged()
    End Sub
End Class