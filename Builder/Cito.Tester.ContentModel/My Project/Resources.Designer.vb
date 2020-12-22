
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
                    Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("Cito.Tester.ContentModel.Resources", GetType(Resources).Assembly)
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

        Friend ReadOnly Property AssessmentTestViewBase_FriendlyEntityName() As String
            Get
                Return ResourceManager.GetString("AssessmentTestViewBase_FriendlyEntityName", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property AssessmentTestViewType_CES() As String
            Get
                Return ResourceManager.GetString("AssessmentTestViewType_CES", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property AssessmentTestViewType_GenericTest() As String
            Get
                Return ResourceManager.GetString("AssessmentTestViewType_GenericTest", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property AssessmentTestViewType_Word() As String
            Get
                Return ResourceManager.GetString("AssessmentTestViewType_Word", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property CesItemUsageType_default() As String
            Get
                Return ResourceManager.GetString("CesItemUsageType_default", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property CesItemUsageType_informational() As String
            Get
                Return ResourceManager.GetString("CesItemUsageType_informational", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property CesItemUsageType_seeding() As String
            Get
                Return ResourceManager.GetString("CesItemUsageType_seeding", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property CesTestPartValidationRuleUnit_Number() As String
            Get
                Return ResourceManager.GetString("CesTestPartValidationRuleUnit_Number", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property CesTestPartValidationRuleUnit_Percentage() As String
            Get
                Return ResourceManager.GetString("CesTestPartValidationRuleUnit_Percentage", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ConvertToLower_Description() As String
            Get
                Return ResourceManager.GetString("ConvertToLower_Description", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ConvertYToIJ_Description() As String
            Get
                Return ResourceManager.GetString("ConvertYToIJ_Description", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property DataSourceBehaviourEnum_Exclusion() As String
            Get
                Return ResourceManager.GetString("DataSourceBehaviourEnum_Exclusion", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property DataSourceBehaviourEnum_Inclusion() As String
            Get
                Return ResourceManager.GetString("DataSourceBehaviourEnum_Inclusion", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property DataSourceBehaviourEnum_Normal() As String
            Get
                Return ResourceManager.GetString("DataSourceBehaviourEnum_Normal", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property DataSourceBehaviourEnum_Seeding() As String
            Get
                Return ResourceManager.GetString("DataSourceBehaviourEnum_Seeding", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property EnumScoringMethod_Dichotomous() As String
            Get
                Return ResourceManager.GetString("EnumScoringMethod_Dichotomous", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property EnumScoringMethod_None() As String
            Get
                Return ResourceManager.GetString("EnumScoringMethod_None", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property EnumScoringMethod_Polytomous() As String
            Get
                Return ResourceManager.GetString("EnumScoringMethod_Polytomous", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property enumSectionType_BumperGrp() As String
            Get
                Return ResourceManager.GetString("enumSectionType_BumperGrp", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property enumSectionType_categoryGrp() As String
            Get
                Return ResourceManager.GetString("enumSectionType_categoryGrp", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property enumSectionType_insertionCol() As String
            Get
                Return ResourceManager.GetString("enumSectionType_insertionCol", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property enumSectionType_insertionGrp() As String
            Get
                Return ResourceManager.GetString("enumSectionType_insertionGrp", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property enumSectionType_normal() As String
            Get
                Return ResourceManager.GetString("enumSectionType_normal", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_AttributeTransaction_ParametersNotSet() As String
            Get
                Return ResourceManager.GetString("Error_AttributeTransaction_ParametersNotSet", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_ChoiceCollectionParameter_Value_SetValueNotAllowed() As String
            Get
                Return ResourceManager.GetString("Error_ChoiceCollectionParameter_Value_SetValueNotAllowed", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_CompileEngine_CallMethod_CannotFindEntryPoint() As String
            Get
                Return ResourceManager.GetString("Error_CompileEngine_CallMethod_CannotFindEntryPoint", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_CompileEngine_CallMethod_ErrorWhileExecutingControlTemplateScript() As String
            Get
                Return ResourceManager.GetString("Error_CompileEngine_CallMethod_ErrorWhileExecutingControlTemplateScript", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_CompileEngine_CallMethod_ExceptionOccurred() As String
            Get
                Return ResourceManager.GetString("Error_CompileEngine_CallMethod_ExceptionOccurred", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_CompileEngine_CallMethod_NumberOfParametersDoesNotMatch() As String
            Get
                Return ResourceManager.GetString("Error_CompileEngine_CallMethod_NumberOfParametersDoesNotMatch", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_CompileEngine_CallMethod_OutputDoesNotReturnStringObject() As String
            Get
                Return ResourceManager.GetString("Error_CompileEngine_CallMethod_OutputDoesNotReturnStringObject", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_CompileEngine_CreateAssembly_CannotLoadReference() As String
            Get
                Return ResourceManager.GetString("Error_CompileEngine_CreateAssembly_CannotLoadReference", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_CompileEngine_CreateAssembly_LanguageNotSupported() As String
            Get
                Return ResourceManager.GetString("Error_CompileEngine_CreateAssembly_LanguageNotSupported", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_CompileEngine_CreateAssembly_NoScriptCode() As String
            Get
                Return ResourceManager.GetString("Error_CompileEngine_CreateAssembly_NoScriptCode", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_ControlAdapter_GetAttributeValue_AttributeNotFound() As String
            Get
                Return ResourceManager.GetString("Error_ControlAdapter_GetAttributeValue_AttributeNotFound", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_ControlAdapter_GetControlAdapterTemplate_CannotGetControlTemplate() As String
            Get
                Return ResourceManager.GetString("Error_ControlAdapter_GetControlAdapterTemplate_CannotGetControlTemplate", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_ControlAdapter_ParseControl_CompilingFailed() As String
            Get
                Return ResourceManager.GetString("Error_ControlAdapter_ParseControl_CompilingFailed", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_ControlAdapter_ParseControl_ErrorExecuting() As String
            Get
                Return ResourceManager.GetString("Error_ControlAdapter_ParseControl_ErrorExecuting", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_DictionaryTransaction_ParametersNotSet() As String
            Get
                Return ResourceManager.GetString("Error_DictionaryTransaction_ParametersNotSet", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_ItemAdapter_GetAttributeValue_AttributeNotFound() As String
            Get
                Return ResourceManager.GetString("Error_ItemAdapter_GetAttributeValue_AttributeNotFound", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_ItemAdapter_ParseTemplate_CannotProcessOutput() As String
            Get
                Return ResourceManager.GetString("Error_ItemAdapter_ParseTemplate_CannotProcessOutput", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_ItemAdapter_ProcessControlTemplateOutput_NoValidTemplate() As String
            Get
                Return ResourceManager.GetString("Error_ItemAdapter_ProcessControlTemplateOutput_NoValidTemplate", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_ItemReference_Constructor_ParametersNotSet() As String
            Get
                Return ResourceManager.GetString("Error_ItemReference_Constructor_ParametersNotSet", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_MethodTransaction_ParametersNotSet() As String
            Get
                Return ResourceManager.GetString("Error_MethodTransaction_ParametersNotSet", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_ResourceParameter_Value_1node() As String
            Get
                Return ResourceManager.GetString("Error_ResourceParameter_Value_1node", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_StringParameter_Value_1node() As String
            Get
                Return ResourceManager.GetString("Error_StringParameter_Value_1node", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_TestReference_Constructor_ParametersNotSet1() As String
            Get
                Return ResourceManager.GetString("Error_TestReference_Constructor_ParametersNotSet1", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Error_TransactionBase_Constructor() As String
            Get
                Return ResourceManager.GetString("Error_TransactionBase_Constructor", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property IdentifierIsARequiredField() As String
            Get
                Return ResourceManager.GetString("IdentifierIsARequiredField", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ItemFunctionalType_Informational() As String
            Get
                Return ResourceManager.GetString("ItemFunctionalType_Informational", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ItemFunctionalType_Regular() As String
            Get
                Return ResourceManager.GetString("ItemFunctionalType_Regular", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ItemFunctionalType_Seeding() As String
            Get
                Return ResourceManager.GetString("ItemFunctionalType_Seeding", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ItemFunctionalType_System() As String
            Get
                Return ResourceManager.GetString("ItemFunctionalType_System", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ItemFunctionalTypeIsARequiredField() As String
            Get
                Return ResourceManager.GetString("ItemFunctionalTypeIsARequiredField", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ItemReferenceViewBase_FriendlyEntityName() As String
            Get
                Return ResourceManager.GetString("ItemReferenceViewBase_FriendlyEntityName", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ItemSelectorIsARequiredField() As String
            Get
                Return ResourceManager.GetString("ItemSelectorIsARequiredField", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property ItemSelectorIsNotAllowedForThisSectionType() As String
            Get
                Return ResourceManager.GetString("ItemSelectorIsNotAllowedForThisSectionType", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property MappedColumnNotVisible() As String
            Get
                Return ResourceManager.GetString("MappedColumnNotVisible", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property MappedColumnVisible() As String
            Get
                Return ResourceManager.GetString("MappedColumnVisible", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property PasswordIsARequiredField() As String
            Get
                Return ResourceManager.GetString("PasswordIsARequiredField", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property PasswordRules() As String
            Get
                Return ResourceManager.GetString("PasswordRules", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property PeriodIndicationIsARequiredField() As String
            Get
                Return ResourceManager.GetString("PeriodIndicationIsARequiredField", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Qti21NavigationMode_automatic() As String
            Get
                Return ResourceManager.GetString("Qti21NavigationMode_automatic", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Qti21NavigationMode_linear() As String
            Get
                Return ResourceManager.GetString("Qti21NavigationMode_linear", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Qti21NavigationMode_nonlinear() As String
            Get
                Return ResourceManager.GetString("Qti21NavigationMode_nonlinear", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property RemoveAllSpaces_Description() As String
            Get
                Return ResourceManager.GetString("RemoveAllSpaces_Description", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property RemoveApostrophs_Description() As String
            Get
                Return ResourceManager.GetString("RemoveApostrophs_Description", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property RemoveDiacritics_Description() As String
            Get
                Return ResourceManager.GetString("RemoveDiacritics_Description", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property RemoveHyphens_Description() As String
            Get
                Return ResourceManager.GetString("RemoveHyphens_Description", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property RemoveLeadingTrailingSpaces_Description() As String
            Get
                Return ResourceManager.GetString("RemoveLeadingTrailingSpaces_Description", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property SelectionTemplateNotConfiguredCorrectly() As String
            Get
                Return ResourceManager.GetString("SelectionTemplateNotConfiguredCorrectly", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property SourceNameIsARequiredField() As String
            Get
                Return ResourceManager.GetString("SourceNameIsARequiredField", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property SubmissionMode_individual() As String
            Get
                Return ResourceManager.GetString("SubmissionMode_individual", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property SubmissionMode_simultaneous() As String
            Get
                Return ResourceManager.GetString("SubmissionMode_simultaneous", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property TestOutcomeValidator_FriendlyName() As String
            Get
                Return ResourceManager.GetString("TestOutcomeValidator_FriendlyName", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property TestOutcomeValidator_Outcome() As String
            Get
                Return ResourceManager.GetString("TestOutcomeValidator_Outcome", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property TestOutcomeValidator_Score() As String
            Get
                Return ResourceManager.GetString("TestOutcomeValidator_Score", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property TestPackageViewBase_FriendlyEntityName() As String
            Get
                Return ResourceManager.GetString("TestPackageViewBase_FriendlyEntityName", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property TestPartViewBase_FriendlyEntityName() As String
            Get
                Return ResourceManager.GetString("TestPartViewBase_FriendlyEntityName", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property TestReferenceViewBase_FriendlyEntityName() As String
            Get
                Return ResourceManager.GetString("TestReferenceViewBase_FriendlyEntityName", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property TestSectionViewBase_FriendlyEntityName() As String
            Get
                Return ResourceManager.GetString("TestSectionViewBase_FriendlyEntityName", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property TestSetViewBase_FriendlyEntityName() As String
            Get
                Return ResourceManager.GetString("TestSetViewBase_FriendlyEntityName", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property TitleIsARequiredField() As String
            Get
                Return ResourceManager.GetString("TitleIsARequiredField", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Trace_AspV3StyleParser_GetParsedResult_ConstructCode() As String
            Get
                Return ResourceManager.GetString("Trace_AspV3StyleParser_GetParsedResult_ConstructCode", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Trace_ControlAdapter_ParseControl_CompileControltemplate() As String
            Get
                Return ResourceManager.GetString("Trace_ControlAdapter_ParseControl_CompileControltemplate", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Trace_ItemAdapter_ProcessControlTemplateOutput_AddScriptOutput() As String
            Get
                Return ResourceManager.GetString("Trace_ItemAdapter_ProcessControlTemplateOutput_AddScriptOutput", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property TypeIsARequiredField() As String
            Get
                Return ResourceManager.GetString("TypeIsARequiredField", resourceCulture)
            End Get
        End Property
    End Module
End Namespace
