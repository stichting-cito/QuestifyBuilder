
Option Strict On
Option Explicit On

Imports System

Namespace My.Resources

    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0"), _
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

        Friend ReadOnly Property ChoiceInline_Itembody_QTI22() As String
            Get
                Return ResourceManager.GetString("ChoiceInline_Itembody_QTI22", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ChoiceInline_Itembody_QTI30() As String
            Get
                Return ResourceManager.GetString("ChoiceInline_Itembody_QTI30", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ChoiceInline_ResponseDeclarations_QTI22() As String
            Get
                Return ResourceManager.GetString("ChoiceInline_ResponseDeclarations_QTI22", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ChoiceInline_ResponseDeclarations_QTI30() As String
            Get
                Return ResourceManager.GetString("ChoiceInline_ResponseDeclarations_QTI30", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ChoiceInline_ResponseProcessing_QTI22() As String
            Get
                Return ResourceManager.GetString("ChoiceInline_ResponseProcessing_QTI22", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ChoiceInline_ResponseProcessing_QTI30() As String
            Get
                Return ResourceManager.GetString("ChoiceInline_ResponseProcessing_QTI30", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ContentWithResources_QTI22() As String
            Get
                Return ResourceManager.GetString("ContentWithResources_QTI22", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ContentWithResources_QTI30() As String
            Get
                Return ResourceManager.GetString("ContentWithResources_QTI30", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property GapDateSC() As String
            Get
                Return ResourceManager.GetString("GapDateSC", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property GapDateSC_Itembody_QTI22() As String
            Get
                Return ResourceManager.GetString("GapDateSC_Itembody_QTI22", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property GapDateSC_Itembody_QTI30() As String
            Get
                Return ResourceManager.GetString("GapDateSC_Itembody_QTI30", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property GapDateSC_ResponseProcessing_QTI22() As String
            Get
                Return ResourceManager.GetString("GapDateSC_ResponseProcessing_QTI22", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property GapDateSC_ResponseProcessing_QTI30() As String
            Get
                Return ResourceManager.GetString("GapDateSC_ResponseProcessing_QTI30", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property GapInlineSC() As String
            Get
                Return ResourceManager.GetString("GapInlineSC", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property GapInlineSC_ItemBody_QTI22() As String
            Get
                Return ResourceManager.GetString("GapInlineSC_ItemBody_QTI22", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property GapInlineSC_ItemBody_QTI30() As String
            Get
                Return ResourceManager.GetString("GapInlineSC_ItemBody_QTI30", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property GapInlineSC_ItemBody1_QTI22() As String
            Get
                Return ResourceManager.GetString("GapInlineSC_ItemBody1_QTI22", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property GapInlineSC_ResponseDeclarations_QTI22() As String
            Get
                Return ResourceManager.GetString("GapInlineSC_ResponseDeclarations_QTI22", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property GapInlineSC_ResponseDeclarations_QTI30() As String
            Get
                Return ResourceManager.GetString("GapInlineSC_ResponseDeclarations_QTI30", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property GapInlineSC_ResponseProcessing_QTI22() As String
            Get
                Return ResourceManager.GetString("GapInlineSC_ResponseProcessing_QTI22", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property GapInlineSC_ResponseProcessing_QTI30() As String
            Get
                Return ResourceManager.GetString("GapInlineSC_ResponseProcessing_QTI30", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property GapInlineSC2() As String
            Get
                Return ResourceManager.GetString("GapInlineSC2", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property GapInlineSC2_Itembody_QTI22() As String
            Get
                Return ResourceManager.GetString("GapInlineSC2_Itembody_QTI22", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property GapInlineSC2_Itembody_QTI30() As String
            Get
                Return ResourceManager.GetString("GapInlineSC2_Itembody_QTI30", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property GapInlineSC2_ResponseDeclarations_QTI22() As String
            Get
                Return ResourceManager.GetString("GapInlineSC2_ResponseDeclarations_QTI22", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property GapInlineSC2_ResponseDeclarations_QTI30() As String
            Get
                Return ResourceManager.GetString("GapInlineSC2_ResponseDeclarations_QTI30", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property GapInlineSC2_ResponseProcessing_QTI22() As String
            Get
                Return ResourceManager.GetString("GapInlineSC2_ResponseProcessing_QTI22", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property GapInlineSC2_ResponseProcessing_QTI30() As String
            Get
                Return ResourceManager.GetString("GapInlineSC2_ResponseProcessing_QTI30", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property GraphicGapMatch() As String
            Get
                Return ResourceManager.GetString("GraphicGapMatch", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property GraphicGapMatch_Itembody_QTI22() As String
            Get
                Return ResourceManager.GetString("GraphicGapMatch_Itembody_QTI22", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property GraphicGapMatch_Itembody_QTI30() As String
            Get
                Return ResourceManager.GetString("GraphicGapMatch_Itembody_QTI30", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property GraphicGapMatch_ResponseDeclarations_QTI22() As String
            Get
                Return ResourceManager.GetString("GraphicGapMatch_ResponseDeclarations_QTI22", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property GraphicGapMatch_ResponseDeclarations_QTI30() As String
            Get
                Return ResourceManager.GetString("GraphicGapMatch_ResponseDeclarations_QTI30", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property GraphicGapMatch_ResponseProcessing_QTI22() As String
            Get
                Return ResourceManager.GetString("GraphicGapMatch_ResponseProcessing_QTI22", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property GraphicGapMatch_ResponseProcessing_QTI30() As String
            Get
                Return ResourceManager.GetString("GraphicGapMatch_ResponseProcessing_QTI30", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property McWithConcepts() As String
            Get
                Return ResourceManager.GetString("McWithConcepts", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property McWithConcepts_Itembody_QTI22() As String
            Get
                Return ResourceManager.GetString("McWithConcepts_Itembody_QTI22", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property McWithConcepts_Itembody_QTI30() As String
            Get
                Return ResourceManager.GetString("McWithConcepts_Itembody_QTI30", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property McWithConcepts_OutcomeDeclarations_QTI22() As String
            Get
                Return ResourceManager.GetString("McWithConcepts_OutcomeDeclarations_QTI22", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property McWithConcepts_OutcomeDeclarations_QTI30() As String
            Get
                Return ResourceManager.GetString("McWithConcepts_OutcomeDeclarations_QTI30", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property McWithConcepts_ResponseProcessing_QTI22() As String
            Get
                Return ResourceManager.GetString("McWithConcepts_ResponseProcessing_QTI22", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property McWithConcepts_ResponseProcessing_QTI30() As String
            Get
                Return ResourceManager.GetString("McWithConcepts_ResponseProcessing_QTI30", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property PreprocessorWithConcepts() As String
            Get
                Return ResourceManager.GetString("PreprocessorWithConcepts", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property PreprocessorWithConcepts_Itembody_QTI22() As String
            Get
                Return ResourceManager.GetString("PreprocessorWithConcepts_Itembody_QTI22", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property PreprocessorWithConcepts_Itembody_QTI30() As String
            Get
                Return ResourceManager.GetString("PreprocessorWithConcepts_Itembody_QTI30", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property PreprocessorWithConcepts_OutcomeDeclarations_QTI22() As String
            Get
                Return ResourceManager.GetString("PreprocessorWithConcepts_OutcomeDeclarations_QTI22", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property PreprocessorWithConcepts_OutcomeDeclarations_QTI30() As String
            Get
                Return ResourceManager.GetString("PreprocessorWithConcepts_OutcomeDeclarations_QTI30", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property PreprocessorWithConcepts_ResponseProcessing1() As String
            Get
                Return ResourceManager.GetString("PreprocessorWithConcepts_ResponseProcessing1", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property PreprocessorWithConcepts_ResponseProcessing11() As String
            Get
                Return ResourceManager.GetString("PreprocessorWithConcepts_ResponseProcessing11", resourceCulture)
            End Get
        End Property
    End Module
End Namespace
