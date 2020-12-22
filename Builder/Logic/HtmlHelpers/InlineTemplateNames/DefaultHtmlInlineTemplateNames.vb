Namespace HtmlHelpers

    Public Class DefaultHtmlInlineTemplateNames : Implements IHtmlInlineTemplateNames

        Public Overridable ReadOnly Property ForAudio As String Implements IHtmlInlineTemplateNames.forAudio
            Get
                Return Nothing
            End Get
        End Property

        Public Overridable ReadOnly Property ForImage As String Implements IHtmlInlineTemplateNames.forImage
            Get
                Return InlineMediaTemplateHelper.EMBEDDED_IMAGE_PARAMETERSET_NAME
            End Get
        End Property

        Public Overridable ReadOnly Property ForVideo As String Implements IHtmlInlineTemplateNames.forVideo
            Get
                Return Nothing
            End Get
        End Property

        Public Overridable ReadOnly Property NrOneOptionForAudio As Boolean Implements IHtmlInlineTemplateNames.nrOneOptionForAudio
            Get
                Return False
            End Get
        End Property

        Public Overridable ReadOnly Property NrOneOptionForImage As Boolean Implements IHtmlInlineTemplateNames.nrOneOptionForImage
            Get
                Return False
            End Get
        End Property

        Public Overridable ReadOnly Property NrOneOptionForVideo As Boolean Implements IHtmlInlineTemplateNames.nrOneOptionForVideo
            Get
                Return False
            End Get
        End Property
    End Class

End Namespace