
Option Strict On
Option Explicit On

Imports System

Namespace My.Resources

    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0"), _
Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), _
Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute(), _
Global.Microsoft.VisualBasic.HideModuleNameAttribute()> _
    Public Module Resources

        Private resourceMan As Global.System.Resources.ResourceManager

        Private resourceCulture As Global.System.Globalization.CultureInfo

        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)> _
        Public ReadOnly Property ResourceManager() As Global.System.Resources.ResourceManager
            Get
                If Object.ReferenceEquals(resourceMan, Nothing) Then
                    Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("Resources", GetType(Resources).Assembly)
                    resourceMan = temp
                End If
                Return resourceMan
            End Get
        End Property

        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)> _
        Public Property Culture() As Global.System.Globalization.CultureInfo
            Get
                Return resourceCulture
            End Get
            Set
                resourceCulture = value
            End Set
        End Property

        Public ReadOnly Property ACustomPropertyWithTheSameCodeAlreadyExists() As String
            Get
                Return ResourceManager.GetString("ACustomPropertyWithTheSameCodeAlreadyExists", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property AResourceWithTheSameCodeAlreadyExists() As String
            Get
                Return ResourceManager.GetString("AResourceWithTheSameCodeAlreadyExists", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property AuthenticationType_Default() As String
            Get
                Return ResourceManager.GetString("AuthenticationType_Default", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property CannotDelete() As String
            Get
                Return ResourceManager.GetString("CannotDelete", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Category_ConceptStructure() As String
            Get
                Return ResourceManager.GetString("Category_ConceptStructure", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Category_CustomBankProperties() As String
            Get
                Return ResourceManager.GetString("Category_CustomBankProperties", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Category_DependentResources() As String
            Get
                Return ResourceManager.GetString("Category_DependentResources", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Category_PropertyEntity() As String
            Get
                Return ResourceManager.GetString("Category_PropertyEntity", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property CodeHasIllegalChars() As String
            Get
                Return ResourceManager.GetString("CodeHasIllegalChars", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property CodeHasMultiplePeriodsInARow() As String
            Get
                Return ResourceManager.GetString("CodeHasMultiplePeriodsInARow", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property CodeHasOnlySpaces() As String
            Get
                Return ResourceManager.GetString("CodeHasOnlySpaces", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property CodeIsRequired() As String
            Get
                Return ResourceManager.GetString("CodeIsRequired", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ConceptStructure() As String
            Get
                Return ResourceManager.GetString("ConceptStructure", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ConceptType_Attribute() As String
            Get
                Return ResourceManager.GetString("ConceptType_Attribute", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ConceptType_Bottleneck() As String
            Get
                Return ResourceManager.GetString("ConceptType_Bottleneck", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ConceptType_Domain() As String
            Get
                Return ResourceManager.GetString("ConceptType_Domain", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ConceptType_Part() As String
            Get
                Return ResourceManager.GetString("ConceptType_Part", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ConceptType_ResponseStrategy() As String
            Get
                Return ResourceManager.GetString("ConceptType_ResponseStrategy", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ConceptType_SubAttribute() As String
            Get
                Return ResourceManager.GetString("ConceptType_SubAttribute", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ConceptType_SubDomain() As String
            Get
                Return ResourceManager.GetString("ConceptType_SubDomain", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ConceptType_SubSubAttribute() As String
            Get
                Return ResourceManager.GetString("ConceptType_SubSubAttribute", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property CustomBankPropertyType_Concept() As String
            Get
                Return ResourceManager.GetString("CustomBankPropertyType_Concept", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property CustomBankPropertyType_FreeValue() As String
            Get
                Return ResourceManager.GetString("CustomBankPropertyType_FreeValue", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property CustomBankPropertyType_FreeValueRichText() As String
            Get
                Return ResourceManager.GetString("CustomBankPropertyType_FreeValueRichText", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property CustomBankPropertyType_ListMultipleSelect() As String
            Get
                Return ResourceManager.GetString("CustomBankPropertyType_ListMultipleSelect", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property CustomBankPropertyType_ListSingleSelect() As String
            Get
                Return ResourceManager.GetString("CustomBankPropertyType_ListSingleSelect", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property CustomBankPropertyType_Tree() As String
            Get
                Return ResourceManager.GetString("CustomBankPropertyType_Tree", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property DeliveryResourceEntityType() As String
            Get
                Return ResourceManager.GetString("DeliveryResourceEntityType", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property FreeValue() As String
            Get
                Return ResourceManager.GetString("FreeValue", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property FreeValueRichText() As String
            Get
                Return ResourceManager.GetString("FreeValueRichText", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ItemTypeEnum_Choice() As String
            Get
                Return ResourceManager.GetString("ItemTypeEnum_Choice", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ItemTypeEnum_Composite() As String
            Get
                Return ResourceManager.GetString("ItemTypeEnum_Composite", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ItemTypeEnum_Error() As String
            Get
                Return ResourceManager.GetString("ItemTypeEnum_Error", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ItemTypeEnum_Essay() As String
            Get
                Return ResourceManager.GetString("ItemTypeEnum_Essay", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ItemTypeEnum_Hotspot() As String
            Get
                Return ResourceManager.GetString("ItemTypeEnum_Hotspot", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ItemTypeEnum_Informational() As String
            Get
                Return ResourceManager.GetString("ItemTypeEnum_Informational", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ItemTypeEnum_Inline() As String
            Get
                Return ResourceManager.GetString("ItemTypeEnum_Inline", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ItemTypeEnum_Likert() As String
            Get
                Return ResourceManager.GetString("ItemTypeEnum_Likert", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ItemTypeEnum_Order() As String
            Get
                Return ResourceManager.GetString("ItemTypeEnum_Order", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ItemTypeEnum_Pause() As String
            Get
                Return ResourceManager.GetString("ItemTypeEnum_Pause", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ItemTypeEnum_ShortAnswer() As String
            Get
                Return ResourceManager.GetString("ItemTypeEnum_ShortAnswer", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ItemTypeEnum_System() As String
            Get
                Return ResourceManager.GetString("ItemTypeEnum_System", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ListMultiple() As String
            Get
                Return ResourceManager.GetString("ListMultiple", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ListSingle() As String
            Get
                Return ResourceManager.GetString("ListSingle", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property MeijerinkLevelType_Item1F() As String
            Get
                Return ResourceManager.GetString("MeijerinkLevelType_Item1F", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property MeijerinkLevelType_Item1S() As String
            Get
                Return ResourceManager.GetString("MeijerinkLevelType_Item1S", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property MeijerinkLevelType_Item2F() As String
            Get
                Return ResourceManager.GetString("MeijerinkLevelType_Item2F", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property MeijerinkLevelType_Item2S() As String
            Get
                Return ResourceManager.GetString("MeijerinkLevelType_Item2S", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property MeijerinkLevelType_Item3F() As String
            Get
                Return ResourceManager.GetString("MeijerinkLevelType_Item3F", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property MeijerinkLevelType_Item3S() As String
            Get
                Return ResourceManager.GetString("MeijerinkLevelType_Item3S", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property MeijerinkLevelType_Item4F() As String
            Get
                Return ResourceManager.GetString("MeijerinkLevelType_Item4F", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property MeijerinkLevelType_Item4S() As String
            Get
                Return ResourceManager.GetString("MeijerinkLevelType_Item4S", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property NameIsRequired() As String
            Get
                Return ResourceManager.GetString("NameIsRequired", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property PasswordIsRequired() As String
            Get
                Return ResourceManager.GetString("PasswordIsRequired", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property PrintFormType_CorrectionBooklet() As String
            Get
                Return ResourceManager.GetString("PrintFormType_CorrectionBooklet", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property PrintFormType_MultiMediaInstructionBooklet() As String
            Get
                Return ResourceManager.GetString("PrintFormType_MultiMediaInstructionBooklet", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property PrintFormType_QuestionBooklet() As String
            Get
                Return ResourceManager.GetString("PrintFormType_QuestionBooklet", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property PrintFormType_SourceBooklet() As String
            Get
                Return ResourceManager.GetString("PrintFormType_SourceBooklet", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Property_Description() As String
            Get
                Return ResourceManager.GetString("Property_Description", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Property_ModifiedBy() As String
            Get
                Return ResourceManager.GetString("Property_ModifiedBy", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Property_Name() As String
            Get
                Return ResourceManager.GetString("Property_Name", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Property_StateName() As String
            Get
                Return ResourceManager.GetString("Property_StateName", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Property_Title() As String
            Get
                Return ResourceManager.GetString("Property_Title", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Property_Username() As String
            Get
                Return ResourceManager.GetString("Property_Username", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property PublicationFormWizard_PasswordRules() As String
            Get
                Return ResourceManager.GetString("PublicationFormWizard_PasswordRules", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ResourceType_ControlTemplate() As String
            Get
                Return ResourceManager.GetString("ResourceType_ControlTemplate", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ResourceType_ItemLayoutTemplate() As String
            Get
                Return ResourceManager.GetString("ResourceType_ItemLayoutTemplate", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ResourceTypeEnum_Items() As String
            Get
                Return ResourceManager.GetString("ResourceTypeEnum_Items", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ResourceTypeEnum_Media() As String
            Get
                Return ResourceManager.GetString("ResourceTypeEnum_Media", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ResourceTypeEnum_Tests() As String
            Get
                Return ResourceManager.GetString("ResourceTypeEnum_Tests", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Test() As String
            Get
                Return ResourceManager.GetString("Test", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TestTemplate() As String
            Get
                Return ResourceManager.GetString("TestTemplate", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TitleIsRequired() As String
            Get
                Return ResourceManager.GetString("TitleIsRequired", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TreeStructure() As String
            Get
                Return ResourceManager.GetString("TreeStructure", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property User_Not_Unique() As String
            Get
                Return ResourceManager.GetString("User_Not_Unique", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property UsernameIsRequired() As String
            Get
                Return ResourceManager.GetString("UsernameIsRequired", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property VariantDistributionType_Automatic() As String
            Get
                Return ResourceManager.GetString("VariantDistributionType_Automatic", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property VariantDistributionType_Manual() As String
            Get
                Return ResourceManager.GetString("VariantDistributionType_Manual", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Version() As String
            Get
                Return ResourceManager.GetString("Version", resourceCulture)
            End Get
        End Property
    End Module
End Namespace
