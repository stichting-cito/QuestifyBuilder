
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
                    Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("Cito.Tester.Common.Resources", GetType(Resources).Assembly)
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

        Friend ReadOnly Property Cancel() As String
            Get
                Return ResourceManager.GetString("Cancel", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Canvas_RemoveShapeConfirmation() As String
            Get
                Return ResourceManager.GetString("Canvas_RemoveShapeConfirmation", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_ContentModelException_UserFriendlyErrorMessage_Message() As String
            Get
                Return ResourceManager.GetString("Error_ContentModelException_UserFriendlyErrorMessage_Message", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_ControlTemplateException_UserFriendlyErrorMessage_Message() As String
            Get
                Return ResourceManager.GetString("Error_ControlTemplateException_UserFriendlyErrorMessage_Message", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_ControlTemplateScriptException_Constructor_CannotCreate() As String
            Get
                Return ResourceManager.GetString("Error_ControlTemplateScriptException_Constructor_CannotCreate", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_ControlTemplateScriptException_Constructor_Obsolete() As String
            Get
                Return ResourceManager.GetString("Error_ControlTemplateScriptException_Constructor_Obsolete", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_ControlTemplateScriptException_GetObjectData_CannotGetData() As String
            Get
                Return ResourceManager.GetString("Error_ControlTemplateScriptException_GetObjectData_CannotGetData", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_ControlTemplateScriptException_UserFriendlyErrorMessage_Message() As String
            Get
                Return ResourceManager.GetString("Error_ControlTemplateScriptException_UserFriendlyErrorMessage_Message", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_ExceptionDialog_Load_TitleBarText() As String
            Get
                Return ResourceManager.GetString("Error_ExceptionDialog_Load_TitleBarText", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_ExceptionHandler_HandleException_NotHandled() As String
            Get
                Return ResourceManager.GetString("Error_ExceptionHandler_HandleException_NotHandled", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_ExceptionListenerConfigSection_GetListeners_ConfigurationException() As String
            Get
                Return ResourceManager.GetString("Error_ExceptionListenerConfigSection_GetListeners_ConfigurationException", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_FileHelper_MakeFileFromStream_ParameterFileNameIsNothing() As String
            Get
                Return ResourceManager.GetString("Error_FileHelper_MakeFileFromStream_ParameterFileNameIsNothing", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_FileHelper_MakeFileFromStream_ParameterFileStreamIsNothing() As String
            Get
                Return ResourceManager.GetString("Error_FileHelper_MakeFileFromStream_ParameterFileStreamIsNothing", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_InteractionControlException_UserFriendlyErrorMessage_Message() As String
            Get
                Return ResourceManager.GetString("Error_InteractionControlException_UserFriendlyErrorMessage_Message", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_InteractionControllerException_UserFriendlyErrorMessage_Message() As String
            Get
                Return ResourceManager.GetString("Error_InteractionControllerException_UserFriendlyErrorMessage_Message", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_ItemLogicControllerException_UserFriendlyErrorMessage_Message() As String
            Get
                Return ResourceManager.GetString("Error_ItemLogicControllerException_UserFriendlyErrorMessage_Message", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_ItemTemplateException_UserFriendlyErrorMessage_Message() As String
            Get
                Return ResourceManager.GetString("Error_ItemTemplateException_UserFriendlyErrorMessage_Message", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_LabelControlException_UserFriendlyErrorMessage_Message() As String
            Get
                Return ResourceManager.GetString("Error_LabelControlException_UserFriendlyErrorMessage_Message", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_LogicException_UserFriendlyErrorMessage_Message() As String
            Get
                Return ResourceManager.GetString("Error_LogicException_UserFriendlyErrorMessage_Message", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_PluginHelper_CheckExpectedType_NotExpectedType() As String
            Get
                Return ResourceManager.GetString("Error_PluginHelper_CheckExpectedType_NotExpectedType", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_PluginHelper_CheckExpectedType_ObjectParameterNotSet() As String
            Get
                Return ResourceManager.GetString("Error_PluginHelper_CheckExpectedType_ObjectParameterNotSet", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_PluginHelper_CheckExpectedType_TypeParameterNotSet() As String
            Get
                Return ResourceManager.GetString("Error_PluginHelper_CheckExpectedType_TypeParameterNotSet", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_PluginHelper_CheckIsNotNothing_ObjectIsNothing() As String
            Get
                Return ResourceManager.GetString("Error_PluginHelper_CheckIsNotNothing_ObjectIsNothing", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_PluginHelper_CreateInstance_TypeNameParameterNotKnown() As String
            Get
                Return ResourceManager.GetString("Error_PluginHelper_CreateInstance_TypeNameParameterNotKnown", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_PopUpLinkControlException_UserFriendlyErrorMessage_Message() As String
            Get
                Return ResourceManager.GetString("Error_PopUpLinkControlException_UserFriendlyErrorMessage_Message", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_ResourceEntriesInCacheCollection_itemParNotSet() As String
            Get
                Return ResourceManager.GetString("Error_ResourceEntriesInCacheCollection_itemParNotSet", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_ResourceEntry_Constructor_DependentResources_Empty() As String
            Get
                Return ResourceManager.GetString("Error_ResourceEntry_Constructor_DependentResources_Empty", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_ResourceEntry_Constructor_Name_Empty() As String
            Get
                Return ResourceManager.GetString("Error_ResourceEntry_Constructor_Name_Empty", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_ResourceEntry_Constructor_Type_Empty() As String
            Get
                Return ResourceManager.GetString("Error_ResourceEntry_Constructor_Type_Empty", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_ResourceEntry_Constructor_Uri_Empty() As String
            Get
                Return ResourceManager.GetString("Error_ResourceEntry_Constructor_Uri_Empty", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_ResourceException_UserFriendlyErrorMessage_Message() As String
            Get
                Return ResourceManager.GetString("Error_ResourceException_UserFriendlyErrorMessage_Message", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_Serializable_Constructor() As String
            Get
                Return ResourceManager.GetString("Error_Serializable_Constructor", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_SerializeHelper_XmlSerializeTo_ObjectIsNothing() As String
            Get
                Return ResourceManager.GetString("Error_SerializeHelper_XmlSerializeTo_ObjectIsNothing", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_SqlException_UserFriendlyErrorMessage_Message() As String
            Get
                Return ResourceManager.GetString("Error_SqlException_UserFriendlyErrorMessage_Message", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_StreamConverters_ConvertStreamToByteArray_ErrorOccurred() As String
            Get
                Return ResourceManager.GetString("Error_StreamConverters_ConvertStreamToByteArray_ErrorOccurred", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_StreamConverters_ConvertStreamToString_ErrorOccurred() As String
            Get
                Return ResourceManager.GetString("Error_StreamConverters_ConvertStreamToString_ErrorOccurred", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_TesterException_UserFriendlyErrorMessage_Message() As String
            Get
                Return ResourceManager.GetString("Error_TesterException_UserFriendlyErrorMessage_Message", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_TestStateException_UserFriendlyErrorMessage_Message() As String
            Get
                Return ResourceManager.GetString("Error_TestStateException_UserFriendlyErrorMessage_Message", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_TestViewerException_UserFriendlyErrorMessage_Message() As String
            Get
                Return ResourceManager.GetString("Error_TestViewerException_UserFriendlyErrorMessage_Message", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_UICommandHandlerException_UserFriendlyErrorMessage() As String
            Get
                Return ResourceManager.GetString("Error_UICommandHandlerException_UserFriendlyErrorMessage", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_UserFriendlyErrorOccurredException_UserFriendlyErrorMessage_Message() As String
            Get
                Return ResourceManager.GetString("Error_UserFriendlyErrorOccurredException_UserFriendlyErrorMessage_Message", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_XHtmlDocument_GetEntity_CannotGetEntity() As String
            Get
                Return ResourceManager.GetString("Error_XHtmlDocument_GetEntity_CannotGetEntity", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_XHtmlDocument_LoadXML_CannotLoadXML() As String
            Get
                Return ResourceManager.GetString("Error_XHtmlDocument_LoadXML_CannotLoadXML", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_XHtmlDocument_LoadXML_DocumentIsNothing() As String
            Get
                Return ResourceManager.GetString("Error_XHtmlDocument_LoadXML_DocumentIsNothing", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_XHtmlDocument_ResolveUri_URIisNothing() As String
            Get
                Return ResourceManager.GetString("Error_XHtmlDocument_ResolveUri_URIisNothing", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property mathml3() As String
            Get
                Return ResourceManager.GetString("mathml3", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property mathml3_common() As String
            Get
                Return ResourceManager.GetString("mathml3_common", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property mathml3_content() As String
            Get
                Return ResourceManager.GetString("mathml3_content", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property mathml3_presentation() As String
            Get
                Return ResourceManager.GetString("mathml3_presentation", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property mathml3_strict_content() As String
            Get
                Return ResourceManager.GetString("mathml3_strict_content", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property OK() As String
            Get
                Return ResourceManager.GetString("OK", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property RemoveAllShapesConfirm() As String
            Get
                Return ResourceManager.GetString("RemoveAllShapesConfirm", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property RemoveShapeConfirm() As String
            Get
                Return ResourceManager.GetString("RemoveShapeConfirm", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Trace_XHtmlDocument_GetEntity_NoEntityAvailable() As String
            Get
                Return ResourceManager.GetString("Trace_XHtmlDocument_GetEntity_NoEntityAvailable", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Trace_XHtmlDocument_GetEntity_StreamReturnedSuccesfully() As String
            Get
                Return ResourceManager.GetString("Trace_XHtmlDocument_GetEntity_StreamReturnedSuccesfully", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Trace_XHtmlDocument_ResolveUri_ResolvedSuccessFully() As String
            Get
                Return ResourceManager.GetString("Trace_XHtmlDocument_ResolveUri_ResolvedSuccessFully", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Trace_XHtmlDocument_ResolveUri_Unresolved() As String
            Get
                Return ResourceManager.GetString("Trace_XHtmlDocument_ResolveUri_Unresolved", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ValidationHelper_CodeCannotContainIllegalCharacters() As String
            Get
                Return ResourceManager.GetString("ValidationHelper_CodeCannotContainIllegalCharacters", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ValidationHelper_CodeCannotContainMoreThen255Characters() As String
            Get
                Return ResourceManager.GetString("ValidationHelper_CodeCannotContainMoreThen255Characters", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ValidationHelper_CodeCannotContainMultiplePeriodsInARow() As String
            Get
                Return ResourceManager.GetString("ValidationHelper_CodeCannotContainMultiplePeriodsInARow", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ValidationHelper_CodeIsNotAValidNcName() As String
            Get
                Return ResourceManager.GetString("ValidationHelper_CodeIsNotAValidNcName", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ValidationHelper_CodeNotOnlyWhiteSpaces() As String
            Get
                Return ResourceManager.GetString("ValidationHelper_CodeNotOnlyWhiteSpaces", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ValidationHelper_CodeRequiredField() As String
            Get
                Return ResourceManager.GetString("ValidationHelper_CodeRequiredField", resourceCulture)
            End Get
        End Property
    End Module
End Namespace
