
Option Strict On
Option Explicit On

Imports System

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
                    Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("Questify.Builder.Plugins.Html.ComponentOne.Resources", GetType(Resources).Assembly)
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

        Friend ReadOnly Property add_icon_16() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("add_icon_16", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Friend ReadOnly Property ConvertToRoman_small() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("ConvertToRoman_small", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Friend ReadOnly Property customInteraction_16() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("customInteraction_16", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Friend ReadOnly Property DesignMode() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("DesignMode", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Friend ReadOnly Property FitToContent() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("FitToContent", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Friend ReadOnly Property insert_audio() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("insert-audio", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Friend ReadOnly Property insert_video() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("insert-video", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Friend ReadOnly Property InsertFormula_small() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("InsertFormula_small", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Friend ReadOnly Property InsertImage_small() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("InsertImage_small", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Friend ReadOnly Property InsertSymbol_small() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("InsertSymbol_small", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Friend ReadOnly Property InsertTable_small() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("InsertTable_small", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Friend ReadOnly Property ItemLayoutTemplateInlineMultimediaMissing() As String
            Get
                Return ResourceManager.GetString("ItemLayoutTemplateInlineMultimediaMissing", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property LockedForEditImage() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("LockedForEditImage", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Friend ReadOnly Property Mute_16() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("Mute_16", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Friend ReadOnly Property PastFromWord_small() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("PastFromWord_small", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Friend ReadOnly Property pause_16() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("pause_16", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Friend ReadOnly Property Q32_32trans() As System.Drawing.Icon
            Get
                Dim obj As Object = ResourceManager.GetObject("Q32_32trans", resourceCulture)
                Return CType(obj, System.Drawing.Icon)
            End Get
        End Property

        Friend ReadOnly Property TTSAlternative_16() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("TTSAlternative_16", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Friend ReadOnly Property TTSMuteCouldNotBeApplied() As String
            Get
                Return ResourceManager.GetString("TTSMuteCouldNotBeApplied", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property TTSRemove_16() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("TTSRemove_16", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Friend ReadOnly Property TTSRemovesMarkupConfirmationMessage() As String
            Get
                Return ResourceManager.GetString("TTSRemovesMarkupConfirmationMessage", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property TTSRemovesMarkupConfirmationTitle() As String
            Get
                Return ResourceManager.GetString("TTSRemovesMarkupConfirmationTitle", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property XhtmlEditor_MergeCellError() As String
            Get
                Return ResourceManager.GetString("XhtmlEditor_MergeCellError", resourceCulture)
            End Get
        End Property
    End Module
End Namespace
