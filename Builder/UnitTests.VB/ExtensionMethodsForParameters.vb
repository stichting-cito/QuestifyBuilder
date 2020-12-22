Imports System.Runtime.CompilerServices
Imports Cito.Tester.ContentModel
Imports Cito.Tester.Common
Imports System.Xml.Serialization
Imports System.Text

Module ExtensionMethodsForParameters

    Public Const GapIdKey As String = "inlineGapMatchId"
    Public Const GapLabelKey As String = "inlineGapMatchLabel"

    <Extension()>
    Public Sub AddInlineGapMatch(ByVal xhtmlParameter As XHtmlParameter, inlineGapMatchId As String, gapLabel As String)
        Dim ns = New XmlSerializerNamespaces()
        ns.Add("cito", "http://www.cito.nl/citotester")
        Dim inlineElement = New InlineElement() With {.Identifier = Guid.NewGuid().ToString()}


        Dim paramCollection = New ParameterCollection()
        paramCollection.InnerParameters.Add(New PlainTextParameter() With {.Name = GapIdKey, .Value = inlineGapMatchId})
        paramCollection.InnerParameters.Add(New PlainTextParameter() With {.Name = GapLabelKey, .Value = gapLabel})

        inlineElement.Parameters.Add(paramCollection)

        Dim toAdd = SerializeHelper.XmlSerializeToString(inlineElement, False, ns, True, Encoding.UTF8).Trim()
        xhtmlParameter.Value += String.Format("<p>{0}</p>", toAdd)
    End Sub

End Module
