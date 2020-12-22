
Option Strict On
Option Explicit On


Namespace My.Resources

    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0"), _
Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), _
Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute(), _
Global.Microsoft.VisualBasic.HideModuleNameAttribute()> _
    Friend Module Resources

        Private resourceMan As Global.System.Resources.ResourceManager

        Private resourceCulture As Global.System.Globalization.CultureInfo

        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)> _
        Friend ReadOnly Property ResourceManager() As Global.System.Resources.ResourceManager
            Get
                If Object.ReferenceEquals(resourceMan, Nothing) Then
                    Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("Questify.Builder.UnitTests.Publication.Resources", GetType(Resources).Assembly)
                    resourceMan = temp
                End If
                Return resourceMan
            End Get
        End Property

        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)> _
        Friend Property Culture() As Global.System.Globalization.CultureInfo
            Get
                Return resourceCulture
            End Get
            Set
                resourceCulture = value
            End Set
        End Property

        Friend ReadOnly Property ChoiceInline() As String
            Get
                Return ResourceManager.GetString("ChoiceInline", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ChoiceInline_Itembody() As String
            Get
                Return ResourceManager.GetString("ChoiceInline_Itembody", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ChoiceInline_ResponseDeclarations() As String
            Get
                Return ResourceManager.GetString("ChoiceInline_ResponseDeclarations", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ChoiceInline_ResponseProcessing_QTI22() As String
            Get
                Return ResourceManager.GetString("ChoiceInline_ResponseProcessing_QTI22", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ContentWithResources() As String
            Get
                Return ResourceManager.GetString("ContentWithResources", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property GapDateSC() As String
            Get
                Return ResourceManager.GetString("GapDateSC", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property GapDateSC_Itembody() As String
            Get
                Return ResourceManager.GetString("GapDateSC_Itembody", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property GapDateSC_ResponseProcessing_QTI22() As String
            Get
                Return ResourceManager.GetString("GapDateSC_ResponseProcessing_QTI22", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property GapInlineSC() As String
            Get
                Return ResourceManager.GetString("GapInlineSC", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property GapInlineSC_ItemBody() As String
            Get
                Return ResourceManager.GetString("GapInlineSC_ItemBody", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property GapInlineSC_ResponseDeclarations() As String
            Get
                Return ResourceManager.GetString("GapInlineSC_ResponseDeclarations", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property GapInlineSC_ResponseProcessing_QTI22() As String
            Get
                Return ResourceManager.GetString("GapInlineSC_ResponseProcessing_QTI22", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property GapInlineSC2() As String
            Get
                Return ResourceManager.GetString("GapInlineSC2", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property GapInlineSC2_Itembody() As String
            Get
                Return ResourceManager.GetString("GapInlineSC2_Itembody", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property GapInlineSC2_ResponseDeclarations() As String
            Get
                Return ResourceManager.GetString("GapInlineSC2_ResponseDeclarations", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property GapInlineSC2_ResponseProcessing_QTI22() As String
            Get
                Return ResourceManager.GetString("GapInlineSC2_ResponseProcessing_QTI22", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property GraphicGapMatch() As String
            Get
                Return ResourceManager.GetString("GraphicGapMatch", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property GraphicGapMatch_Itembody() As String
            Get
                Return ResourceManager.GetString("GraphicGapMatch_Itembody", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property GraphicGapMatch_ResponseDeclarations() As String
            Get
                Return ResourceManager.GetString("GraphicGapMatch_ResponseDeclarations", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property GraphicGapMatch_ResponseProcessing_QTI22() As String
            Get
                Return ResourceManager.GetString("GraphicGapMatch_ResponseProcessing_QTI22", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property McWithConcepts() As String
            Get
                Return ResourceManager.GetString("McWithConcepts", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property McWithConcepts_Itembody() As String
            Get
                Return ResourceManager.GetString("McWithConcepts_Itembody", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property McWithConcepts_OutcomeDeclarations() As String
            Get
                Return ResourceManager.GetString("McWithConcepts_OutcomeDeclarations", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property McWithConcepts_ResponseProcessing_QTI22() As String
            Get
                Return ResourceManager.GetString("McWithConcepts_ResponseProcessing_QTI22", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property PreprocessorWithConcepts() As String
            Get
                Return ResourceManager.GetString("PreprocessorWithConcepts", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property PreprocessorWithConcepts_Itembody() As String
            Get
                Return ResourceManager.GetString("PreprocessorWithConcepts_Itembody", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property PreprocessorWithConcepts_OutcomeDeclarations() As String
            Get
                Return ResourceManager.GetString("PreprocessorWithConcepts_OutcomeDeclarations", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property PreprocessorWithConcepts_ResponseProcessing_QTI22() As String
            Get
                Return ResourceManager.GetString("PreprocessorWithConcepts_ResponseProcessing_QTI22", resourceCulture)
            End Get
        End Property
    End Module
End Namespace
