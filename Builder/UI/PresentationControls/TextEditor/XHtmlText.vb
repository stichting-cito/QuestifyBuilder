
Imports System.Linq
Imports System.Xml
Imports NLog
Imports Questify.Builder.Logic

Public Class XhtmlText
    Public Shared Sub AddNodeAfterCurrentNode(editor As IXhtmlEditor, newNode As XmlNode, isNew As Boolean)
        Dim p = editor.Selection.Start

        If p Is Nothing OrElse
           newNode Is Nothing OrElse
           editor.Document Is Nothing OrElse
           editor.Selection Is Nothing Then
            Return
        End If

        Debug.Assert(p.ParentNode IsNot Nothing)

        Try
            editor.BeginTransaction()
            Dim toInsNode As XmlNode = editor.Document.ImportNode(newNode, True)
            editor.CommitTransaction()
            If isNew Then
                Dim range As ITextRange = editor.CreateRange(editor.Selection.StartIndex, editor.Selection.Length)
                range.SetXmlElement(DirectCast(toInsNode, XmlElement))
            Else
                Dim selectedNode As XmlNode = editor.Selection.Node
                If selectedNode IsNot Nothing AndAlso selectedNode.ParentNode IsNot Nothing Then
                    selectedNode.ParentNode.ReplaceChild(toInsNode, selectedNode)
                End If
            End If
        Catch ex As NullReferenceException
            Dim _logger = LogManager.GetCurrentClassLogger()
            _logger.Log(LogLevel.Error, String.Format("IxHtmlEditor - AddNodeAfterCurrentNode - Node to add: {0}", newNode.InnerText), ex)
        End Try

    End Sub

End Class
