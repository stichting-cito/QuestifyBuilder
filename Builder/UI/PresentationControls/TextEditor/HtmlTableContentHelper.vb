Imports System.Xml

Public Class HtmlTableContentHelper

    Public Function GetMaxColumnCount(ByVal table As XmlElement, ByVal namespaceManager As XmlNamespaceManager) As Integer
        Dim maxColumns As Integer = 0

        For i As Integer = 0 To table.SelectNodes("*/def:tr", namespaceManager).Count - 1
            Dim nrOfColumns As Integer = table.SelectNodes(String.Format("*/def:tr[{0}]/def:td", i + 1), namespaceManager).Count

            If nrOfColumns > maxColumns Then
                maxColumns = nrOfColumns
            End If
        Next

        Return maxColumns
    End Function

    Public Function GetHorizontalAlignmentFromCell(ByVal cellElement As XmlElement) As Web.UI.WebControls.HorizontalAlign
        If cellElement IsNot Nothing AndAlso Not String.IsNullOrEmpty(cellElement.GetAttribute("align")) Then
            Return DirectCast([Enum].Parse(GetType(Web.UI.WebControls.HorizontalAlign), cellElement.GetAttribute("align"), True), Web.UI.WebControls.HorizontalAlign)
        End If

        Return Web.UI.WebControls.HorizontalAlign.NotSet
    End Function

    Public Function GetVerticalAlignmentFromCell(ByVal cellElement As XmlElement) As Web.UI.WebControls.VerticalAlign
        If cellElement IsNot Nothing AndAlso Not String.IsNullOrEmpty(cellElement.GetAttribute("valign")) Then
            Return DirectCast([Enum].Parse(GetType(Web.UI.WebControls.VerticalAlign), cellElement.GetAttribute("valign"), True), Web.UI.WebControls.VerticalAlign)
        End If

        Return Web.UI.WebControls.VerticalAlign.NotSet
    End Function

    Public Function GetWidthOfCell(ByVal cellElement As XmlElement) As Web.UI.WebControls.Unit
        If cellElement IsNot Nothing AndAlso Not String.IsNullOrEmpty(cellElement.GetAttribute("width")) Then
            Return CInt(cellElement.GetAttribute("width"))
        End If

        Return Nothing
    End Function

    Public Function GetHeightOfCell(ByVal cellElement As XmlElement) As Web.UI.WebControls.Unit
        If cellElement IsNot Nothing AndAlso Not String.IsNullOrEmpty(cellElement.GetAttribute("height")) Then
            Return CInt(cellElement.GetAttribute("height"))
        End If

        Return Nothing
    End Function

    Public Function GetStyleFromCell(ByVal cellElement As XmlElement) As String
        If cellElement IsNot Nothing AndAlso Not String.IsNullOrEmpty(cellElement.GetAttribute("style")) Then
            Return cellElement.GetAttribute("style")
        End If

        Return Nothing
    End Function

    Public Function GetTable(ByVal node As XmlNode) As XmlElement
        If node IsNot Nothing AndAlso node.Name.ToLower() <> "body" Then
            If node.Name.ToLower() = "table" Then
                Return DirectCast(node, XmlElement)
            ElseIf node.ParentNode IsNot Nothing Then
                Return GetTable(DirectCast(node.ParentNode, XmlElement))
            End If
        End If

        Return Nothing
    End Function

    Public Function GetCell(ByVal node As XmlNode) As XmlElement
        If node IsNot Nothing AndAlso node.Name.ToLower() <> "body" Then
            If node.Name.ToLower() = "td" Then
                Return DirectCast(node, XmlElement)
            ElseIf node.ParentNode IsNot Nothing Then
                Return GetCell(DirectCast(node.ParentNode, XmlElement))
            End If
        End If

        Return Nothing
    End Function

    Public Function GetRow(ByVal node As XmlNode) As XmlElement
        If node IsNot Nothing AndAlso node.Name.ToLower() <> "body" Then
            If node.Name.ToLower() = "tr" Then
                Return DirectCast(node, XmlElement)
            ElseIf node.ParentNode IsNot Nothing Then
                Return GetRow(DirectCast(node.ParentNode, XmlElement))
            End If
        End If

        Return Nothing
    End Function

    Public Function FindColumnIndex(ByVal cell As XmlElement, ByVal namespaceManager As XmlNamespaceManager) As Integer
        Dim row As XmlElement = GetRow(cell)
        Dim columns As XmlNodeList = row.SelectNodes("def:td", namespaceManager)
        Dim columnIndex As Integer = 0

        For i As Integer = 0 To columns.Count - 1
            If columns(i).Equals(cell) Then
                Return columnIndex
            End If

            columnIndex = i + 1
        Next

        Throw New ArgumentException("Failed to find columnindex")
    End Function

    Public Function FindRowIndex(ByVal cell As XmlElement, ByVal namespaceManager As XmlNamespaceManager) As Integer
        Dim table As XmlElement = GetTable(cell)
        Dim rows As XmlNodeList = table.SelectNodes("*/def:tr", namespaceManager)
        Dim rowIndex As Integer = 0

        For i As Integer = 0 To rows.Count - 1
            Dim columns As XmlNodeList = rows(i).SelectNodes("def:td", namespaceManager)

            For Each column As XmlElement In columns
                If column.Equals(cell) Then
                    Return rowIndex
                End If
            Next

            rowIndex = i + 1
        Next

        Throw New ArgumentException("Failed to find rowindex")
    End Function

    Public Sub SetStyleToTableColumns(ByRef tdNodes As IEnumerable)
        Dim style As String = "BORDER-BOTTOM: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid;"
        Dim addTdStyle As String = "PADDING-TOP: 3px; PADDING-BOTTOM: 3px; PADDING-LEFT: 3px; PADDING-RIGHT: 3px;"
        For Each tdNode As XmlElement In tdNodes
            tdNode.SetAttribute("style", If(tdNode.Name = "td", style + addTdStyle, style))
        Next

    End Sub

End Class
