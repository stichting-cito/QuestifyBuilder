Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Xml.Linq
Imports System.Xml.Schema

Namespace Util

    Public Class XmlTools
        Public Shared Function Normalize(ByVal source As XDocument, ByVal schema As XmlSchemaSet) As XDocument
            Dim havePSVI As Boolean = False

            If schema IsNot Nothing Then
                source.Validate(schema, CType(Nothing, ValidationEventHandler), True)
                havePSVI = True
            End If

            Return New XDocument(source.Declaration, New Object(0) {source.Nodes().Select(CType((Function(n)
                                                                                                     If TypeOf n Is XComment OrElse TypeOf n Is XProcessingInstruction OrElse TypeOf n Is XText Then Return CType(Nothing, XNode)
                                                                                                     Dim element As XElement = TryCast(n, XElement)
                                                                                                     If element IsNot Nothing Then Return CType(XmlTools.NormalizeElement(element, havePSVI), XNode)
                                                                                                     Return n
                                                                                                 End Function), Func(Of XNode, XNode)))})
        End Function

        Public Shared Function DeepEqualsWithNormalization(ByVal doc1 As XDocument, ByVal doc2 As XDocument, ByVal schemaSet As XmlSchemaSet) As Boolean
            Return XNode.DeepEquals(CType(Normalize(doc1, schemaSet), XNode), CType(Normalize(doc2, schemaSet), XNode))
        End Function

        Private Shared Function NormalizeAttributes(ByVal element As XElement, ByVal havePSVI As Boolean) As IEnumerable(Of XAttribute)
            Return element.Attributes().OfType(Of XAttribute).Where(CType((Function(a)
                                                                               If Not a.IsNamespaceDeclaration AndAlso a.Name <> Xsi.schemaLocation Then Return a.Name <> Xsi.noNamespaceSchemaLocation
                                                                               Return False
                                                                           End Function),
                                                                          Func(Of XAttribute, Boolean))).OrderBy(Function(a) a.Name.NamespaceName).ThenBy(Function(a) a.Name.LocalName).Select(Function(a)

                                                                                                                                                                                                   If havePSVI Then

                                                                                                                                                                                                       Select Case a.GetSchemaInfo().SchemaType.TypeCode
                                                                                                                                                                                                           Case XmlTypeCode.Boolean
                                                                                                                                                                                                               Return New XAttribute(a.Name, CObj(CBool(a)))
                                                                                                                                                                                                           Case XmlTypeCode.Decimal
                                                                                                                                                                                                               Return New XAttribute(a.Name, CObj(CType(a, Decimal)))
                                                                                                                                                                                                           Case XmlTypeCode.Float
                                                                                                                                                                                                               Return New XAttribute(a.Name, CObj(CSng(a)))
                                                                                                                                                                                                           Case XmlTypeCode.Double
                                                                                                                                                                                                               Return New XAttribute(a.Name, CObj(CDbl(a)))
                                                                                                                                                                                                           Case XmlTypeCode.DateTime
                                                                                                                                                                                                               Return New XAttribute(a.Name, CObj(CType(a, DateTime)))
                                                                                                                                                                                                           Case XmlTypeCode.HexBinary, XmlTypeCode.Language
                                                                                                                                                                                                               Return New XAttribute(a.Name, CObj((CStr(a)).ToLower()))
                                                                                                                                                                                                       End Select
                                                                                                                                                                                                   End If

                                                                                                                                                                                                   Return a
                                                                                                                                                                                               End Function)
        End Function

        Private Shared Function NormalizeNode(ByVal node As XNode, ByVal havePSVI As Boolean) As XNode
            If TypeOf node Is XComment OrElse TypeOf node Is XProcessingInstruction Then Return CType(Nothing, XNode)
            Dim element As XElement = TryCast(node, XElement)
            If element IsNot Nothing Then Return CType(XmlTools.NormalizeElement(element, havePSVI), XNode)
            Return node
        End Function

        Private Shared Function NormalizeElement(ByVal element As XElement, ByVal havePSVI As Boolean) As XElement
            If havePSVI Then

                Select Case element.GetSchemaInfo().SchemaType.TypeCode
                    Case XmlTypeCode.Boolean
                        Return New XElement(element.Name, New Object(1) {CObj(XmlTools.NormalizeAttributes(element, havePSVI)), CObj(CBool(element))})
                    Case XmlTypeCode.Decimal
                        Return New XElement(element.Name, New Object(1) {CObj(XmlTools.NormalizeAttributes(element, havePSVI)), CObj(CDec(element))})
                    Case XmlTypeCode.Float
                        Return New XElement(element.Name, New Object(1) {CObj(XmlTools.NormalizeAttributes(element, havePSVI)), CObj(CSng(element))})
                    Case XmlTypeCode.Double
                        Return New XElement(element.Name, New Object(1) {CObj(XmlTools.NormalizeAttributes(element, havePSVI)), CObj(CDbl(element))})
                    Case XmlTypeCode.DateTime
                        Return New XElement(element.Name, New Object(1) {CObj(XmlTools.NormalizeAttributes(element, havePSVI)), CObj(CDate(element))})
                    Case XmlTypeCode.HexBinary, XmlTypeCode.Language
                        Return New XElement(element.Name, New Object(1) {CObj(XmlTools.NormalizeAttributes(element, havePSVI)), CObj((CStr(element)).ToLower())})
                    Case Else
                        Return New XElement(element.Name, New Object(1) {CObj(XmlTools.NormalizeAttributes(element, havePSVI)), CObj(element.Nodes().[Select](CType((Function(n) XmlTools.NormalizeNode(n, havePSVI)), Func(Of XNode, XNode))))})
                End Select
            Else
                Return New XElement(element.Name, New Object(1) {CObj(XmlTools.NormalizeAttributes(element, havePSVI)), CObj(element.Nodes().[Select](CType((Function(n) XmlTools.NormalizeNode(n, havePSVI)), Func(Of XNode, XNode))))})
            End If
        End Function

    End Class
End NameSpace