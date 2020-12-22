Imports System.Text
Imports System.Xml

Friend NotInheritable Class ParameterHelper

    Public Shared Function CreateInnerText(nodes() As XmlNode) As String
        Dim output As New StringBuilder

        If nodes Is Nothing Then
            output.Append("")
        Else
            For Each node As XmlNode In nodes
                If Not (node.Name.Equals("DesignerSetting", StringComparison.InvariantCultureIgnoreCase) OrElse
                    node.Name.Equals("cito:DesignerSetting", StringComparison.InvariantCultureIgnoreCase)
                        ) Then
                    output.Append(node.OuterXml)
                End If
            Next
        End If
        Return output.ToString
    End Function

    Private Sub New()
    End Sub

End Class
