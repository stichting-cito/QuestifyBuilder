Imports System.Xml

Namespace PluginExtensibility.Html.Handlers.Logic

    Public MustInherit Class BaseHtmlSplitter : Implements IInlineTextSplitter

        Private ReadOnly _selectedNode As XmlNode
        Private ReadOnly _endNode As XmlNode
        Private ReadOnly _startOffset As Integer
        Private ReadOnly _endOffset As Integer

        Public Sub New(selectedNode As XmlNode, startOffset As Integer, endNode As XmlNode, endOffset As Integer)
            _selectedNode = selectedNode
            _endNode = endNode
            _startOffset = startOffset
            _endOffset = endOffset

            Debug.Assert(selectedNode IsNot Nothing)
            Debug.Assert(endNode IsNot Nothing)

            Debug.Assert(TypeOf selectedNode Is XmlText)
            If (Not (TypeOf selectedNode Is XmlText)) Then Throw New ArgumentException("selectedNode is not XmlText Node")

            Debug.Assert(TypeOf endNode Is XmlText)
            If (Not (TypeOf endNode Is XmlText)) Then Throw New ArgumentException("endNode is not XmlText Node")

        End Sub


        Public MustOverride Function Split() As IEnumerable(Of System.Xml.XmlNode) Implements IInlineTextSplitter.Split


        Public ReadOnly Property SelectedNode As XmlNode
            Get
                Return _selectedNode
            End Get
        End Property

        Public ReadOnly Property EndNode As XmlNode
            Get
                Return _endNode
            End Get
        End Property

        Public ReadOnly Property StartOffset As Integer
            Get
                Return _startOffset
            End Get
        End Property

        Public ReadOnly Property EndOffset As Integer
            Get
                Return _endOffset
            End Get
        End Property


    End Class
End Namespace