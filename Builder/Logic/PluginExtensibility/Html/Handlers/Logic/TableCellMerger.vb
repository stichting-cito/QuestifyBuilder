Imports System.Xml
Imports System.Linq

Namespace PluginExtensibility.Html.Handlers.Logic
    Public Class TableCellMerger
        Inherits BaseMerger

        Public Sub New(table As Table)
            MyBase.New(table)
        End Sub

        Public Overrides Sub Merge(mergeTo As TableCell, other As TableCell)
            Dim frst As Boolean = True
            Dim lst = GetNodes(mergeTo.Node.ChildNodes)
            Dim dest As XmlNode = lst.Where(Function(e) e.Name = "p").LastOrDefault()
            If dest Is Nothing Then
                dest = mergeTo.Node
                frst = False
            End If

            For Each sourceNode As XmlNode In GetNodes(other.Node.ChildNodes)
                If (sourceNode.Name = "p" AndAlso frst) Then
                    For Each n As XmlNode In GetNodes(sourceNode.ChildNodes)
                        dest.AppendChild(MakeNewXmlTextNode(ChrW(&HA0)))
                        dest.AppendChild(n)
                    Next
                    frst = False : dest = mergeTo.Node
                Else
                    dest.AppendChild(sourceNode)
                End If
            Next
        End Sub

        Private Function MakeNewXmlTextNode(c As Char) As XmlNode
            Return Table.Node.OwnerDocument.CreateTextNode(c)
        End Function

        Private Function GetNodes(lst As XmlNodeList) As IList(Of XmlNode)
            Dim ret As New List(Of XmlNode)
            For Each n As XmlNode In lst
                ret.Add(n)
            Next
            Return ret
        End Function
    End Class

End Namespace