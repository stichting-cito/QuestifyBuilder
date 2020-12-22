Imports System.Linq
Imports System.Xml
Imports Questify.Builder.Logic.QTI.Helpers.QTI_Base.QTICompliantHelper.Interfaces

Namespace QTI.Helpers.QTI_Base.QTICompliantHelper

    Public Class FixIds
        Implements IModifyItemDocument, IModifyExtensionDocument



        Public Sub Modify(ByRef xmlDoc As XmlDocument, docHelper As DocumentHelper) Implements IModifyDocument.Modify
            Dim attributes = xmlDoc.SelectNodes("//*[@id]")
            Dim dictionaryOfIds As New Dictionary(Of String, List(Of XmlNode))
            For Each node As XmlNode In attributes
                If node.Attributes("id") IsNot Nothing Then
                    Dim id As String = node.Attributes("id").Value
                    Dim nodeList As List(Of XmlNode)
                    If Not dictionaryOfIds.ContainsKey(id) Then
                        dictionaryOfIds.Add(id, Nothing)
                        nodeList = New List(Of XmlNode)()
                    Else
                        nodeList = dictionaryOfIds(id)
                    End If
                    nodeList.Add(node)
                    dictionaryOfIds(id) = nodeList
                End If
            Next
            For Each doubleIdNode In dictionaryOfIds.Values.Where(Function(valueList) valueList.Count > 1)
                Dim index As Integer = 0
                For Each xmlNodeToChangeId In doubleIdNode
                    Dim oldValue = xmlNodeToChangeId.Attributes("id").Value
                    xmlNodeToChangeId.Attributes("id").Value = $"{oldValue}_{index.ToString}"
                    index += 1
                Next
            Next
        End Sub

    End Class
End NameSpace