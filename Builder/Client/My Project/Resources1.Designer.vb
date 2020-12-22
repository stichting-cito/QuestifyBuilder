
Option Strict On
Option Explicit On

Imports System

Namespace My.Resources

    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0"), _
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
                    Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("Questify.Builder.Client.Resources", GetType(Resources).Assembly)
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

        Public ReadOnly Property AboutDialog_About() As String
            Get
                Return ResourceManager.GetString("AboutDialog_About", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property AboutDialog_Version() As String
            Get
                Return ResourceManager.GetString("AboutDialog_Version", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property AboutToClearAndDeleteResourcesInTheBankAndSubbanks() As String
            Get
                Return ResourceManager.GetString("AboutToClearAndDeleteResourcesInTheBankAndSubbanks", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property AboutToolStripMenuItem_Image() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("AboutToolStripMenuItem_Image", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property AdaptiveSectionValidation() As String
            Get
                Return ResourceManager.GetString("AdaptiveSectionValidation", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Add() As String
            Get
                Return ResourceManager.GetString("Add", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property add_favorite_16x16() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("add_favorite_16x16", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property AddAControlTemplate() As String
            Get
                Return ResourceManager.GetString("AddAControlTemplate", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property AddAItemLayoutTemplate() As String
            Get
                Return ResourceManager.GetString("AddAItemLayoutTemplate", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property AddASelectionTemplate() As String
            Get
                Return ResourceManager.GetString("AddASelectionTemplate", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property AddBank_EnterValidInfo() As String
            Get
                Return ResourceManager.GetString("AddBank_EnterValidInfo", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property AddingControlTemplate() As String
            Get
                Return ResourceManager.GetString("AddingControlTemplate", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property AddingCustomProperties() As String
            Get
                Return ResourceManager.GetString("AddingCustomProperties", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property AddingCustomPropertiesToItem() As String
            Get
                Return ResourceManager.GetString("AddingCustomPropertiesToItem", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property AddingItemLayoutTemplate() As String
            Get
                Return ResourceManager.GetString("AddingItemLayoutTemplate", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property AddingMediaFile() As String
            Get
                Return ResourceManager.GetString("AddingMediaFile", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property AddItem16x16_Image() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("AddItem16x16_Image", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property AddItem24x24_Image() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("AddItem24x24_Image", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property AllEditors_CannotUpdateBecauseOfState() As String
            Get
                Return ResourceManager.GetString("AllEditors_CannotUpdateBecauseOfState", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property AllEditors_SaveChangesForResourceWithStateWarning() As String
            Get
                Return ResourceManager.GetString("AllEditors_SaveChangesForResourceWithStateWarning", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property AlterOrDeleteSelectedFiles() As String
            Get
                Return ResourceManager.GetString("AlterOrDeleteSelectedFiles", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property AnErrorOccurred() As String
            Get
                Return ResourceManager.GetString("AnErrorOccurred", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property AnErrorOccurredWhileValidatingTheSolution() As String
            Get
                Return ResourceManager.GetString("AnErrorOccurredWhileValidatingTheSolution", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property apiConnectionError() As String
            Get
                Return ResourceManager.GetString("apiConnectionError", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Appearance1_Image() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("Appearance1_Image", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property Appearance10_Image() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("Appearance10_Image", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property Appearance11_Image() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("Appearance11_Image", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property Appearance2_Image() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("Appearance2_Image", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property Appearance3_Image() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("Appearance3_Image", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property Appearance4_Image() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("Appearance4_Image", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property Appearance5_Image() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("Appearance5_Image", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property Appearance7_Image() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("Appearance7_Image", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property Appearance8_Image() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("Appearance8_Image", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property Appearance9_Image() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("Appearance9_Image", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property AspectEditor_PleaseEnterNewItemCode() As String
            Get
                Return ResourceManager.GetString("AspectEditor_PleaseEnterNewItemCode", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property AspectEditor_Save_SavedStatusbarMessage() As String
            Get
                Return ResourceManager.GetString("AspectEditor_Save_SavedStatusbarMessage", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property AspectEditor_Save_SaveFailedStatusbarMessage() As String
            Get
                Return ResourceManager.GetString("AspectEditor_Save_SaveFailedStatusbarMessage", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property AspectEditor_TitleBar() As String
            Get
                Return ResourceManager.GetString("AspectEditor_TitleBar", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property AspectEditor_ValidateAspect_ValidationErrors() As String
            Get
                Return ResourceManager.GetString("AspectEditor_ValidateAspect_ValidationErrors", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property AspectsToolStripMenuItem_Image() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("AspectsToolStripMenuItem_Image", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property AssessementTestCodeFieldDiffers() As String
            Get
                Return ResourceManager.GetString("AssessementTestCodeFieldDiffers", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property AssessementTestTitleFieldDiffers() As String
            Get
                Return ResourceManager.GetString("AssessementTestTitleFieldDiffers", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Authorization_Error_Saving() As String
            Get
                Return ResourceManager.GetString("Authorization_Error_Saving", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property AutoHide() As String
            Get
                Return ResourceManager.GetString("AutoHide", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property back() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("back", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property BankCodeCannotContainMoreThanThreeCharactersForBAS() As String
            Get
                Return ResourceManager.GetString("BankCodeCannotContainMoreThanThreeCharactersForBAS", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property BankCodeIsNotFilledMakeSureBankExternalKeyIsFilledThisIsOnlyAllowedByBankmanager() As String
            Get
                Return ResourceManager.GetString("BankCodeIsNotFilledMakeSureBankExternalKeyIsFilledThisIsOnlyAllowedByBankmanager", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property BankDeleted() As String
            Get
                Return ResourceManager.GetString("BankDeleted", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property BankNameMaxLenghtExceeded() As String
            Get
                Return ResourceManager.GetString("BankNameMaxLenghtExceeded", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property BankPropertiesFor() As String
            Get
                Return ResourceManager.GetString("BankPropertiesFor", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property BrowseForResource() As String
            Get
                Return ResourceManager.GetString("BrowseForResource", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Cancelled() As String
            Get
                Return ResourceManager.GetString("Cancelled", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property CannotStartPublication() As String
            Get
                Return ResourceManager.GetString("CannotStartPublication", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property CanTFindOtdForSection() As String
            Get
                Return ResourceManager.GetString("CanTFindOtdForSection", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property CanTFindOtdForThisSection() As String
            Get
                Return ResourceManager.GetString("CanTFindOtdForThisSection", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property CesLocationFilter() As String
            Get
                Return ResourceManager.GetString("CesLocationFilter", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ChangeCodeDialog_EmptyCodeName() As String
            Get
                Return ResourceManager.GetString("ChangeCodeDialog_EmptyCodeName", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ChangeDataSourceDialogHeaderTooMuchReferences() As String
            Get
                Return ResourceManager.GetString("ChangeDataSourceDialogHeaderTooMuchReferences", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ChangePasswordDialogTitle() As String
            Get
                Return ResourceManager.GetString("ChangePasswordDialogTitle", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ChooseAExportMethod() As String
            Get
                Return ResourceManager.GetString("ChooseAExportMethod", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ChooseAImportMethod() As String
            Get
                Return ResourceManager.GetString("ChooseAImportMethod", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ChooseAPublicationMethod() As String
            Get
                Return ResourceManager.GetString("ChooseAPublicationMethod", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ClickHereToOpen() As String
            Get
                Return ResourceManager.GetString("ClickHereToOpen", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Close() As String
            Get
                Return ResourceManager.GetString("Close", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property close_16x16() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("close_16x16", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property CodeIsARequiredField() As String
            Get
                Return ResourceManager.GetString("CodeIsARequiredField", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property CodeMaxLenghtExceeded() As String
            Get
                Return ResourceManager.GetString("CodeMaxLenghtExceeded", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Completed() As String
            Get
                Return ResourceManager.GetString("Completed", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property CompleteTheWizard() As String
            Get
                Return ResourceManager.GetString("CompleteTheWizard", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Components() As Byte()
            Get
                Dim obj As Object = ResourceManager.GetObject("Components", resourceCulture)
                Return CType(obj, Byte())
            End Get
        End Property

        Public ReadOnly Property ConceptEditor_SaveChanges() As String
            Get
                Return ResourceManager.GetString("ConceptEditor_SaveChanges", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ConnectDelivery() As String
            Get
                Return ResourceManager.GetString("ConnectDelivery", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ControlTemplate() As String
            Get
                Return ResourceManager.GetString("ControlTemplate", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ControlTemplateHeader() As String
            Get
                Return ResourceManager.GetString("ControlTemplateHeader", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ControlTemplateHeaderV2() As String
            Get
                Return ResourceManager.GetString("ControlTemplateHeaderV2", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ControlTemplateS() As String
            Get
                Return ResourceManager.GetString("ControlTemplateS", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ControlTemplateToBeAdded() As String
            Get
                Return ResourceManager.GetString("ControlTemplateToBeAdded", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ControlTemplateToolStripMenuItem_Image() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("ControlTemplateToolStripMenuItem_Image", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property CopyOf0() As String
            Get
                Return ResourceManager.GetString("CopyOf0", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property CopyToolStripMenuItem_Image() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("CopyToolStripMenuItem_Image", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property CouldNotSaveResourceUnsupportedType() As String
            Get
                Return ResourceManager.GetString("CouldNotSaveResourceUnsupportedType", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property CreateNewBankAsChild() As String
            Get
                Return ResourceManager.GetString("CreateNewBankAsChild", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property CreateNewBankOnRootlevel() As String
            Get
                Return ResourceManager.GetString("CreateNewBankOnRootlevel", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property CustomBankPropertiesAdded() As String
            Get
                Return ResourceManager.GetString("CustomBankPropertiesAdded", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property CustomBankPropertiesAlreadyExisted() As String
            Get
                Return ResourceManager.GetString("CustomBankPropertiesAlreadyExisted", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property CustomBankPropertiesConflicts() As String
            Get
                Return ResourceManager.GetString("CustomBankPropertiesConflicts", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property CustomBankPropertiesRemovedFromItem() As String
            Get
                Return ResourceManager.GetString("CustomBankPropertiesRemovedFromItem", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property CustomPropertiesNewConceptStructure() As String
            Get
                Return ResourceManager.GetString("CustomPropertiesNewConceptStructure", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property CustomPropertiesNewFree() As String
            Get
                Return ResourceManager.GetString("CustomPropertiesNewFree", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property CustomPropertiesNewFreeRichText() As String
            Get
                Return ResourceManager.GetString("CustomPropertiesNewFreeRichText", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property CustomPropertiesNewMulti() As String
            Get
                Return ResourceManager.GetString("CustomPropertiesNewMulti", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property CustomPropertiesNewSingle() As String
            Get
                Return ResourceManager.GetString("CustomPropertiesNewSingle", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property CustomPropertiesNewTreeStructure() As String
            Get
                Return ResourceManager.GetString("CustomPropertiesNewTreeStructure", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property CustomPropertyAlreadyExistInThisBankHierarchy() As String
            Get
                Return ResourceManager.GetString("CustomPropertyAlreadyExistInThisBankHierarchy", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property CustomPropertyEditor_NotAllowed() As String
            Get
                Return ResourceManager.GetString("CustomPropertyEditor_NotAllowed", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property CustomPropertyEditor_TitleBar() As String
            Get
                Return ResourceManager.GetString("CustomPropertyEditor_TitleBar", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property CustomPropertyEditor_ValidationErrors() As String
            Get
                Return ResourceManager.GetString("CustomPropertyEditor_ValidationErrors", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property CustomPropertyEditor_ValidationErrorsOnExit() As String
            Get
                Return ResourceManager.GetString("CustomPropertyEditor_ValidationErrorsOnExit", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property database_16x16() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("database_16x16", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property DataSource() As String
            Get
                Return ResourceManager.GetString("DataSource", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property DataSourceEditor_DialogTitle() As String
            Get
                Return ResourceManager.GetString("DataSourceEditor_DialogTitle", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property DataSourceEditor_ValidationErrorsOnExit() As String
            Get
                Return ResourceManager.GetString("DataSourceEditor_ValidationErrorsOnExit", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property delete_16x() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("delete_16x", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property delete_favorite_16x16() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("delete_favorite_16x16", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property DeletedItemsAfterPaste() As String
            Get
                Return ResourceManager.GetString("DeletedItemsAfterPaste", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property DeleteFailureMessage() As String
            Get
                Return ResourceManager.GetString("DeleteFailureMessage", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property DeleteItem16x16() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("DeleteItem16x16", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property DeleteItems24x24_Image() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("DeleteItems24x24_Image", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property DeleteResources() As String
            Get
                Return ResourceManager.GetString("DeleteResources", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property DeleteThisUser() As String
            Get
                Return ResourceManager.GetString("DeleteThisUser", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property DeleteToolStripButton_Image() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("DeleteToolStripButton_Image", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property DeleteToolStripMenuItem_Image() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("DeleteToolStripMenuItem_Image", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property DeleteUserError() As String
            Get
                Return ResourceManager.GetString("DeleteUserError", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property DeliveryEditor_DeliveryCodeNameAlreadyExistsInBankHierarchy() As String
            Get
                Return ResourceManager.GetString("DeliveryEditor_DeliveryCodeNameAlreadyExistsInBankHierarchy", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property DeliveryEditor_Load_DeliveryDefinitionLoaded() As String
            Get
                Return ResourceManager.GetString("DeliveryEditor_Load_DeliveryDefinitionLoaded", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property DeliveryEditor_NoSufficientRightsToPerformThisAction() As String
            Get
                Return ResourceManager.GetString("DeliveryEditor_NoSufficientRightsToPerformThisAction", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property DeliveryEditor_OnlyPossibleToChangeTheDeliveryCodeWhenNoOtherChanges() As String
            Get
                Return ResourceManager.GetString("DeliveryEditor_OnlyPossibleToChangeTheDeliveryCodeWhenNoOtherChanges", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property DeliveryEditor_SaveDelivery_DeliverySavedStatusbarMessage() As String
            Get
                Return ResourceManager.GetString("DeliveryEditor_SaveDelivery_DeliverySavedStatusbarMessage", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property DeliveryEditor_SaveDelivery_SaveFailedStatusbarMessage() As String
            Get
                Return ResourceManager.GetString("DeliveryEditor_SaveDelivery_SaveFailedStatusbarMessage", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property DeliveryEditor_TitleBar() As String
            Get
                Return ResourceManager.GetString("DeliveryEditor_TitleBar", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property DeliveryEditor_UnexpectedError() As String
            Get
                Return ResourceManager.GetString("DeliveryEditor_UnexpectedError", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property DeliveryEditor_ValidateDelivery_ValidationErrors() As String
            Get
                Return ResourceManager.GetString("DeliveryEditor_ValidateDelivery_ValidationErrors", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property DirectoryDoesNotExist() As String
            Get
                Return ResourceManager.GetString("DirectoryDoesNotExist", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property DocKable() As String
            Get
                Return ResourceManager.GetString("DocKable", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property DockedPane() As String
            Get
                Return ResourceManager.GetString("DockedPane", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property DocX2Html() As String
            Get
                Return ResourceManager.GetString("DocX2Html", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Done() As String
            Get
                Return ResourceManager.GetString("Done", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property down_16x16() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("down_16x16", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property DoYouReallyWantToClearAllTheResourcesInTheBank() As String
            Get
                Return ResourceManager.GetString("DoYouReallyWantToClearAllTheResourcesInTheBank", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property EditFileToolStripMenuItem_Image() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("EditFileToolStripMenuItem_Image", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property Editor_SaveChangesQuestionMessage() As String
            Get
                Return ResourceManager.GetString("Editor_SaveChangesQuestionMessage", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property EditorFor01() As String
            Get
                Return ResourceManager.GetString("EditorFor01", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property EditToolStripButton_Image() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("EditToolStripButton_Image", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property EnabledConditionRefersToNonExistingParameter() As String
            Get
                Return ResourceManager.GetString("EnabledConditionRefersToNonExistingParameter", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Error_EmtpyPassword() As String
            Get
                Return ResourceManager.GetString("Error_EmtpyPassword", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Error_PublicationType_Not_Available() As String
            Get
                Return ResourceManager.GetString("Error_PublicationType_Not_Available", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Error_WrongPassword() As String
            Get
                Return ResourceManager.GetString("Error_WrongPassword", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ErrorCaption() As String
            Get
                Return ResourceManager.GetString("ErrorCaption", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ErrorDeletingEntity() As String
            Get
                Return ResourceManager.GetString("ErrorDeletingEntity", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ErrorImporting() As String
            Get
                Return ResourceManager.GetString("ErrorImporting", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ErrorPublicationServiceUnableToPreview() As String
            Get
                Return ResourceManager.GetString("ErrorPublicationServiceUnableToPreview", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Errors() As String
            Get
                Return ResourceManager.GetString("Errors", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ErrorSavingBankInformation() As String
            Get
                Return ResourceManager.GetString("ErrorSavingBankInformation", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ErrorsFoundInCollectionparameter() As String
            Get
                Return ResourceManager.GetString("ErrorsFoundInCollectionparameter", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ErrorThrown() As String
            Get
                Return ResourceManager.GetString("ErrorThrown", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ErrorUnableToCreatePreview() As String
            Get
                Return ResourceManager.GetString("ErrorUnableToCreatePreview", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ErrorWhileImportingItemMissingIlt() As String
            Get
                Return ResourceManager.GetString("ErrorWhileImportingItemMissingIlt", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ErrorWhileParsingTemplate() As String
            Get
                Return ResourceManager.GetString("ErrorWhileParsingTemplate", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ExportAlreadyExistsSelectOtherExportLocation() As String
            Get
                Return ResourceManager.GetString("ExportAlreadyExistsSelectOtherExportLocation", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ExportFormWizard_BusyConstructingListOfDepencies() As String
            Get
                Return ResourceManager.GetString("ExportFormWizard_BusyConstructingListOfDepencies", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ExportingResourcesToSpecifiedLocation() As String
            Get
                Return ResourceManager.GetString("ExportingResourcesToSpecifiedLocation", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ExportLocationDoesNotExist() As String
            Get
                Return ResourceManager.GetString("ExportLocationDoesNotExist", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ExportOfResources() As String
            Get
                Return ResourceManager.GetString("ExportOfResources", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ExportResultDescriptionOperationCancelled() As String
            Get
                Return ResourceManager.GetString("ExportResultDescriptionOperationCancelled", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ExportTaskDescriptionOperationCancelled() As String
            Get
                Return ResourceManager.GetString("ExportTaskDescriptionOperationCancelled", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ExportTaskOperationCancelled() As String
            Get
                Return ResourceManager.GetString("ExportTaskOperationCancelled", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ExportTaskOperationFailed() As String
            Get
                Return ResourceManager.GetString("ExportTaskOperationFailed", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ExportTitle() As String
            Get
                Return ResourceManager.GetString("ExportTitle", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ExportToABasFile() As String
            Get
                Return ResourceManager.GetString("ExportToABasFile", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ExportToAPackageFile() As String
            Get
                Return ResourceManager.GetString("ExportToAPackageFile", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ExportToAPOKFile() As String
            Get
                Return ResourceManager.GetString("ExportToAPOKFile", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ExportToExcel() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("ExportToExcel", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property ExportToolStripButton_Image() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("ExportToolStripButton_Image", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property ExportToolStripMenuItem_Image() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("ExportToolStripMenuItem_Image", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property favorites() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("favorites", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property Features() As String
            Get
                Return ResourceManager.GetString("Features", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property FieldExportSource() As String
            Get
                Return ResourceManager.GetString("FieldExportSource", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property FieldImportSource() As String
            Get
                Return ResourceManager.GetString("FieldImportSource", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property FieldMustBeFilled() As String
            Get
                Return ResourceManager.GetString("FieldMustBeFilled", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property FileAlreadyExist() As String
            Get
                Return ResourceManager.GetString("FileAlreadyExist", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property FileDoesNotExist() As String
            Get
                Return ResourceManager.GetString("FileDoesNotExist", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property FillInPassword() As String
            Get
                Return ResourceManager.GetString("FillInPassword", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property FillInPasswordDesc() As String
            Get
                Return ResourceManager.GetString("FillInPasswordDesc", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property FillTheExportMethodOptions() As String
            Get
                Return ResourceManager.GetString("FillTheExportMethodOptions", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property FillTheImportMethodOptions() As String
            Get
                Return ResourceManager.GetString("FillTheImportMethodOptions", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property FillThePublicationMethodOptions() As String
            Get
                Return ResourceManager.GetString("FillThePublicationMethodOptions", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property FillTheSecurityOptions() As String
            Get
                Return ResourceManager.GetString("FillTheSecurityOptions", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property FixErrorsFirst() As String
            Get
                Return ResourceManager.GetString("FixErrorsFirst", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Floating() As String
            Get
                Return ResourceManager.GetString("Floating", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property FollowingFilesAreExported() As String
            Get
                Return ResourceManager.GetString("FollowingFilesAreExported", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property FollowingResourcesPreventDeleting() As String
            Get
                Return ResourceManager.GetString("FollowingResourcesPreventDeleting", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property FollowingResourcesResultInAWarning() As String
            Get
                Return ResourceManager.GetString("FollowingResourcesResultInAWarning", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property GenerateProposals_NrOfGeneratedTests() As String
            Get
                Return ResourceManager.GetString("GenerateProposals_NrOfGeneratedTests", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property GenerateProposals_TestIsDirty() As String
            Get
                Return ResourceManager.GetString("GenerateProposals_TestIsDirty", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property GenerateReport() As String
            Get
                Return ResourceManager.GetString("GenerateReport", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property GenerationProgress() As String
            Get
                Return ResourceManager.GetString("GenerationProgress", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property GenerationProgressDescription() As String
            Get
                Return ResourceManager.GetString("GenerationProgressDescription", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property GenericTest_SupportedView() As String
            Get
                Return ResourceManager.GetString("GenericTest_SupportedView", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property HandleImportConflictCustomPropertyDialogWindowTitle() As String
            Get
                Return ResourceManager.GetString("HandleImportConflictCustomPropertyDialogWindowTitle", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property HandleImportConflictResourceDialogWindowTitle() As String
            Get
                Return ResourceManager.GetString("HandleImportConflictResourceDialogWindowTitle", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Harmonization() As String
            Get
                Return ResourceManager.GetString("Harmonization", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property HarmonizationCancelledMessage() As String
            Get
                Return ResourceManager.GetString("HarmonizationCancelledMessage", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property HarmonizationDialogTitle() As String
            Get
                Return ResourceManager.GetString("HarmonizationDialogTitle", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Hide() As String
            Get
                Return ResourceManager.GetString("Hide", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property IdentifierAndCodeFieldDiffer() As String
            Get
                Return ResourceManager.GetString("IdentifierAndCodeFieldDiffer", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Import() As String
            Get
                Return ResourceManager.GetString("Import", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Import_Package() As String
            Get
                Return ResourceManager.GetString("Import_Package", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Import_Package_StartImport() As String
            Get
                Return ResourceManager.GetString("Import_Package_StartImport", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Import_PackageSet() As String
            Get
                Return ResourceManager.GetString("Import_PackageSet", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Import_PackageSet_StartImport() As String
            Get
                Return ResourceManager.GetString("Import_PackageSet_StartImport", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ImportFailure() As String
            Get
                Return ResourceManager.GetString("ImportFailure", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ImportFailureGeneral() As String
            Get
                Return ResourceManager.GetString("ImportFailureGeneral", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ImportFailureTitle() As String
            Get
                Return ResourceManager.GetString("ImportFailureTitle", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ImportFromAExcelFile() As String
            Get
                Return ResourceManager.GetString("ImportFromAExcelFile", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ImportFromATestBuilderPackageFile() As String
            Get
                Return ResourceManager.GetString("ImportFromATestBuilderPackageFile", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ImportHarmonisationDetermineIltsToHarmonize() As String
            Get
                Return ResourceManager.GetString("ImportHarmonisationDetermineIltsToHarmonize", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ImportHarmonisationDetermineParameterSetForILT() As String
            Get
                Return ResourceManager.GetString("ImportHarmonisationDetermineParameterSetForILT", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ImportingResourcesToSpecifiedLocation() As String
            Get
                Return ResourceManager.GetString("ImportingResourcesToSpecifiedLocation", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ImportOfResources() As String
            Get
                Return ResourceManager.GetString("ImportOfResources", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ImportSettingCustomPropertyDisplayValue() As String
            Get
                Return ResourceManager.GetString("ImportSettingCustomPropertyDisplayValue", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ImportToolStripButton_Image() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("ImportToolStripButton_Image", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property ImportToolStripMenuItem_Image() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("ImportToolStripMenuItem_Image", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property InitialiseProgressDescription() As String
            Get
                Return ResourceManager.GetString("InitialiseProgressDescription", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property InitialiseProgressTask() As String
            Get
                Return ResourceManager.GetString("InitialiseProgressTask", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property InitializingHarmonization() As String
            Get
                Return ResourceManager.GetString("InitializingHarmonization", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property InstanceAlreadyOpened() As String
            Get
                Return ResourceManager.GetString("InstanceAlreadyOpened", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property InvalidTargetMessage() As String
            Get
                Return ResourceManager.GetString("InvalidTargetMessage", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property InvisibleParameterHasAChangedValue() As String
            Get
                Return ResourceManager.GetString("InvisibleParameterHasAChangedValue", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Item() As String
            Get
                Return ResourceManager.GetString("Item", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Item0InAdaptiveSectionIsMissingInOtd1() As String
            Get
                Return ResourceManager.GetString("Item0InAdaptiveSectionIsMissingInOtd1", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Item0InOtdIsMissingInAdaptiveSection1() As String
            Get
                Return ResourceManager.GetString("Item0InOtdIsMissingInAdaptiveSection1", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ItemEditor_AnErrorOccurredWhileUpdatingTheCodeNameInTests() As String
            Get
                Return ResourceManager.GetString("ItemEditor_AnErrorOccurredWhileUpdatingTheCodeNameInTests", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ItemEditor_ItemCodeNameAlreadyExistsInBankHierarchy() As String
            Get
                Return ResourceManager.GetString("ItemEditor_ItemCodeNameAlreadyExistsInBankHierarchy", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ItemEditor_ItemCodeRenamingIsSucceeded() As String
            Get
                Return ResourceManager.GetString("ItemEditor_ItemCodeRenamingIsSucceeded", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ItemEditor_Load_ItemDefinitionLoaded() As String
            Get
                Return ResourceManager.GetString("ItemEditor_Load_ItemDefinitionLoaded", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ItemEditor_NoSufficientRightsToPerformThisAction() As String
            Get
                Return ResourceManager.GetString("ItemEditor_NoSufficientRightsToPerformThisAction", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ItemEditor_OldItemLayoutTemplateOpenedStatusBarMessage() As String
            Get
                Return ResourceManager.GetString("ItemEditor_OldItemLayoutTemplateOpenedStatusBarMessage", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ItemEditor_OnlyPossibleToChangeTheResourceCodeWhenNoOtherChanges() As String
            Get
                Return ResourceManager.GetString("ItemEditor_OnlyPossibleToChangeTheResourceCodeWhenNoOtherChanges", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ItemEditor_PleaseEnterNewItemCode() As String
            Get
                Return ResourceManager.GetString("ItemEditor_PleaseEnterNewItemCode", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ItemEditor_SaveAsCancelled() As String
            Get
                Return ResourceManager.GetString("ItemEditor_SaveAsCancelled", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ItemEditor_SaveAsText() As String
            Get
                Return ResourceManager.GetString("ItemEditor_SaveAsText", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ItemEditor_SaveItem_ItemSavedStatusbarMessage() As String
            Get
                Return ResourceManager.GetString("ItemEditor_SaveItem_ItemSavedStatusbarMessage", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ItemEditor_SaveItem_SaveFailedStatusbarMessage() As String
            Get
                Return ResourceManager.GetString("ItemEditor_SaveItem_SaveFailedStatusbarMessage", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ItemEditor_TitleBar() As String
            Get
                Return ResourceManager.GetString("ItemEditor_TitleBar", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ItemEditor_UnexpectedError() As String
            Get
                Return ResourceManager.GetString("ItemEditor_UnexpectedError", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ItemEditor_ValidateItem_ValidationErrors() As String
            Get
                Return ResourceManager.GetString("ItemEditor_ValidateItem_ValidationErrors", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ItemEditorNaviateNextNotPossible() As String
            Get
                Return ResourceManager.GetString("ItemEditorNaviateNextNotPossible", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ItemEditorNaviatePreviousNotPossible() As String
            Get
                Return ResourceManager.GetString("ItemEditorNaviatePreviousNotPossible", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ItemForm_DialogTitle() As String
            Get
                Return ResourceManager.GetString("ItemForm_DialogTitle", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ItemForm_IsDirtyWhenCancelPressed() As String
            Get
                Return ResourceManager.GetString("ItemForm_IsDirtyWhenCancelPressed", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ItemForm_ValidationFailedWhenSaved() As String
            Get
                Return ResourceManager.GetString("ItemForm_ValidationFailedWhenSaved", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ItemLayoutTemplate() As String
            Get
                Return ResourceManager.GetString("ItemLayoutTemplate", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property itemlayoutTemplateHeader() As String
            Get
                Return ResourceManager.GetString("itemlayoutTemplateHeader", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ItemLayoutTemplateHeaderV2() As String
            Get
                Return ResourceManager.GetString("ItemLayoutTemplateHeaderV2", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ItemLayoutTemplateS() As String
            Get
                Return ResourceManager.GetString("ItemLayoutTemplateS", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ItemLayoutTemplateToBeAdded() As String
            Get
                Return ResourceManager.GetString("ItemLayoutTemplateToBeAdded", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ItemLayoutTemplateToolStripMenuItem_Image() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("ItemLayoutTemplateToolStripMenuItem_Image", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property ItemrefenceDoesnTContainAnyItems() As String
            Get
                Return ResourceManager.GetString("ItemrefenceDoesnTContainAnyItems", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ItemrefenceInSectionIsEqualToTheItemsInTheOtd() As String
            Get
                Return ResourceManager.GetString("ItemrefenceInSectionIsEqualToTheItemsInTheOtd", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ItemsInExcel() As String
            Get
                Return ResourceManager.GetString("ItemsInExcel", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ItemsUpdatedWithCustomProperties() As String
            Get
                Return ResourceManager.GetString("ItemsUpdatedWithCustomProperties", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ItemToolStripMenuItem_Image() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("ItemToolStripMenuItem_Image", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property LastPublicationStatusMessage() As String
            Get
                Return ResourceManager.GetString("LastPublicationStatusMessage", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Login_LoginFailed_Text() As String
            Get
                Return ResourceManager.GetString("Login_LoginFailed_Text", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Login_LoginFailed_Title() As String
            Get
                Return ResourceManager.GetString("Login_LoginFailed_Title", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Login_LoginFailedBcOldVersion_Text() As String
            Get
                Return ResourceManager.GetString("Login_LoginFailedBcOldVersion_Text", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property MainForm_BankDoesNotExist() As String
            Get
                Return ResourceManager.GetString("MainForm_BankDoesNotExist", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property MainForm_BankDoesNotExistTitle() As String
            Get
                Return ResourceManager.GetString("MainForm_BankDoesNotExistTitle", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property MainForm_CopyToClipboard() As String
            Get
                Return ResourceManager.GetString("MainForm_CopyToClipboard", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property MainForm_CustomPropertyIsConnectedToItem() As String
            Get
                Return ResourceManager.GetString("MainForm_CustomPropertyIsConnectedToItem", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property MainForm_DeleteCustomProperty() As String
            Get
                Return ResourceManager.GetString("MainForm_DeleteCustomProperty", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property MainForm_DoYouReallyWantToDeleteBankAndAllOfItsResources() As String
            Get
                Return ResourceManager.GetString("MainForm_DoYouReallyWantToDeleteBankAndAllOfItsResources", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property MainForm_EntityDoesNotExist() As String
            Get
                Return ResourceManager.GetString("MainForm_EntityDoesNotExist", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property MainForm_EntityDoesNotExistTitle() As String
            Get
                Return ResourceManager.GetString("MainForm_EntityDoesNotExistTitle", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property MainForm_FastSearchTimeOut() As String
            Get
                Return ResourceManager.GetString("MainForm_FastSearchTimeOut", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property MainForm_ItemAlreadyOpen() As String
            Get
                Return ResourceManager.GetString("MainForm_ItemAlreadyOpen", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property MainForm_ItemAlreadyOpenTitle() As String
            Get
                Return ResourceManager.GetString("MainForm_ItemAlreadyOpenTitle", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property MainForm_LoginNotPermittedMessage() As String
            Get
                Return ResourceManager.GetString("MainForm_LoginNotPermittedMessage", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property MainForm_MaintenanceWindowMessage() As String
            Get
                Return ResourceManager.GetString("MainForm_MaintenanceWindowMessage", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property MainForm_MaxOpenItemsReached() As String
            Get
                Return ResourceManager.GetString("MainForm_MaxOpenItemsReached", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property MainForm_MaxOpenItemsReachedNewItem() As String
            Get
                Return ResourceManager.GetString("MainForm_MaxOpenItemsReachedNewItem", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property MainForm_MaxOpenItemsReachedTitle() As String
            Get
                Return ResourceManager.GetString("MainForm_MaxOpenItemsReachedTitle", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property MainForm_NoOpenIItems() As String
            Get
                Return ResourceManager.GetString("MainForm_NoOpenIItems", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property MainForm_NoOpenIItemsTitle() As String
            Get
                Return ResourceManager.GetString("MainForm_NoOpenIItemsTitle", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property MainForm_NoPreviewerAvailable() As String
            Get
                Return ResourceManager.GetString("MainForm_NoPreviewerAvailable", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property MainForm_NoResourcesSelected() As String
            Get
                Return ResourceManager.GetString("MainForm_NoResourcesSelected", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property MainForm_SelectResourcesToExport() As String
            Get
                Return ResourceManager.GetString("MainForm_SelectResourcesToExport", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property MainForm_SelectTestPackageToPublish() As String
            Get
                Return ResourceManager.GetString("MainForm_SelectTestPackageToPublish", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property MainForm_SelectTestToPublish() As String
            Get
                Return ResourceManager.GetString("MainForm_SelectTestToPublish", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property MainForm_SyntaxErrorSearchString() As String
            Get
                Return ResourceManager.GetString("MainForm_SyntaxErrorSearchString", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property MainForm_TestCannotBePreviewed() As String
            Get
                Return ResourceManager.GetString("MainForm_TestCannotBePreviewed", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property MainForm_TestCannotBePreviewed_Title() As String
            Get
                Return ResourceManager.GetString("MainForm_TestCannotBePreviewed_Title", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property MainForm_TestPackageCannotBePreviewed() As String
            Get
                Return ResourceManager.GetString("MainForm_TestPackageCannotBePreviewed", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property MainForm_TestTemplateGrid_GenerateResources_DialogTitle() As String
            Get
                Return ResourceManager.GetString("MainForm_TestTemplateGrid_GenerateResources_DialogTitle", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property MainForm_TestTemplateGrid_GenerateResources_NoDataSourceSections() As String
            Get
                Return ResourceManager.GetString("MainForm_TestTemplateGrid_GenerateResources_NoDataSourceSections", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property MainForm_TestTemplateGrid_GenerateResources_OldTestModelMessage() As String
            Get
                Return ResourceManager.GetString("MainForm_TestTemplateGrid_GenerateResources_OldTestModelMessage", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property MakeSureBankExternalKeyHasAValue() As String
            Get
                Return ResourceManager.GetString("MakeSureBankExternalKeyHasAValue", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Maximize() As String
            Get
                Return ResourceManager.GetString("Maximize", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property MediaEditorForResource() As String
            Get
                Return ResourceManager.GetString("MediaEditorForResource", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property MediaFile() As String
            Get
                Return ResourceManager.GetString("MediaFile", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property MediaFileS() As String
            Get
                Return ResourceManager.GetString("MediaFileS", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property MediaFileToBeAdded() As String
            Get
                Return ResourceManager.GetString("MediaFileToBeAdded", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property MediaToolStripMenuItem_Image() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("MediaToolStripMenuItem_Image", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property Menu() As String
            Get
                Return ResourceManager.GetString("Menu", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Message_NoSectionSelected() As String
            Get
                Return ResourceManager.GetString("Message_NoSectionSelected", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Message_NoSectionSelectedTitle() As String
            Get
                Return ResourceManager.GetString("Message_NoSectionSelectedTitle", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property MetaDataDescription() As String
            Get
                Return ResourceManager.GetString("MetaDataDescription", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property MetaDataStatus() As String
            Get
                Return ResourceManager.GetString("MetaDataStatus", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Minimize() As String
            Get
                Return ResourceManager.GetString("Minimize", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property MultiSelectItemEditWizardChanging() As String
            Get
                Return ResourceManager.GetString("MultiSelectItemEditWizardChanging", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property MultiSelectItemEditWizardCompleted() As String
            Get
                Return ResourceManager.GetString("MultiSelectItemEditWizardCompleted", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property MultiSelectItemEditWizardEditDescription() As String
            Get
                Return ResourceManager.GetString("MultiSelectItemEditWizardEditDescription", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property MultiSelectItemEditWizardEditPreprocessingRule() As String
            Get
                Return ResourceManager.GetString("MultiSelectItemEditWizardEditPreprocessingRule", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property MultiSelectItemEditWizardEditTitle() As String
            Get
                Return ResourceManager.GetString("MultiSelectItemEditWizardEditTitle", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property MultiSelectItemEditWizardMultipleBanksSelected() As String
            Get
                Return ResourceManager.GetString("MultiSelectItemEditWizardMultipleBanksSelected", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property MultiSelectItemEditWizardOverwriteCheckboxText() As String
            Get
                Return ResourceManager.GetString("MultiSelectItemEditWizardOverwriteCheckboxText", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property MultiSelectItemEditWizardOverwriteDescription() As String
            Get
                Return ResourceManager.GetString("MultiSelectItemEditWizardOverwriteDescription", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property MultiSelectItemEditWizardOverwriteTitle() As String
            Get
                Return ResourceManager.GetString("MultiSelectItemEditWizardOverwriteTitle", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property MultiSelectItemEditWizardResultDescription() As String
            Get
                Return ResourceManager.GetString("MultiSelectItemEditWizardResultDescription", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property MultiSelectItemEditWizardSelectEditDescription() As String
            Get
                Return ResourceManager.GetString("MultiSelectItemEditWizardSelectEditDescription", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property MultiSelectItemEditWizardSelectEditTitle() As String
            Get
                Return ResourceManager.GetString("MultiSelectItemEditWizardSelectEditTitle", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property MultiSelectItemEditWizardSkippedItemsBecauseMissingKey() As String
            Get
                Return ResourceManager.GetString("MultiSelectItemEditWizardSkippedItemsBecauseMissingKey", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property MultiSelectItemEditWizardValidationErrors() As String
            Get
                Return ResourceManager.GetString("MultiSelectItemEditWizardValidationErrors", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property MultiSelectItemEditWizardWelcomeDescription() As String
            Get
                Return ResourceManager.GetString("MultiSelectItemEditWizardWelcomeDescription", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property MultiSelectItemEditWizardWelcomeTitle() As String
            Get
                Return ResourceManager.GetString("MultiSelectItemEditWizardWelcomeTitle", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Network_Download_icon() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("Network-Download-icon", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property new_document_16x16() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("new_document_16x16", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property NewAspectToolStripMenuItem_Image() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("NewAspectToolStripMenuItem_Image", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property NewControlTemplateToolStripMenuItem_Image() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("NewControlTemplateToolStripMenuItem_Image", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property NewDataSourceTemplate() As String
            Get
                Return ResourceManager.GetString("NewDataSourceTemplate", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property NewItemLayoutTemplateToolStripMenuItem_Image() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("NewItemLayoutTemplateToolStripMenuItem_Image", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property NewItemToolStripMenuItem_Image() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("NewItemToolStripMenuItem_Image", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property NewMediaToolStripMenuItem_Image() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("NewMediaToolStripMenuItem_Image", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property NewPassword() As String
            Get
                Return ResourceManager.GetString("NewPassword", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property NewPasswordIntro() As String
            Get
                Return ResourceManager.GetString("NewPasswordIntro", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property NewSection() As String
            Get
                Return ResourceManager.GetString("NewSection", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property newString() As String
            Get
                Return ResourceManager.GetString("newString", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property NewTestDefaultName() As String
            Get
                Return ResourceManager.GetString("NewTestDefaultName", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property NewTestPackage() As String
            Get
                Return ResourceManager.GetString("NewTestPackage", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property NewTestPackageToolStripMenuItem_Image() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("NewTestPackageToolStripMenuItem_Image", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property NewTestpart() As String
            Get
                Return ResourceManager.GetString("NewTestpart", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property NewTestset() As String
            Get
                Return ResourceManager.GetString("NewTestset", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property NewTestTemplateDefaultName() As String
            Get
                Return ResourceManager.GetString("NewTestTemplateDefaultName", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property NewTestTemplateToolStripMenuItem_Image() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("NewTestTemplateToolStripMenuItem_Image", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property NewTestToolStripMenuItem_Image() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("NewTestToolStripMenuItem_Image", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property NewToolStripMenuItem_Image() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("NewToolStripMenuItem_Image", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property NewToolStripSplitButton_Image() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("NewToolStripSplitButton_Image", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property next_page() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("next-page", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property No() As String
            Get
                Return ResourceManager.GetString("No", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property NoAction() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("NoAction", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property NoApplications() As String
            Get
                Return ResourceManager.GetString("NoApplications", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property NoExportHandlersAvailable() As String
            Get
                Return ResourceManager.GetString("NoExportHandlersAvailable", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property NoFieldValidation() As String
            Get
                Return ResourceManager.GetString("NoFieldValidation", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property NoPrintFormsAvailable() As String
            Get
                Return ResourceManager.GetString("NoPrintFormsAvailable", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property NoPublicationHandlers() As String
            Get
                Return ResourceManager.GetString("NoPublicationHandlers", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property NoReportsAvailable() As String
            Get
                Return ResourceManager.GetString("NoReportsAvailable", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property NotAbleToDelete() As String
            Get
                Return ResourceManager.GetString("NotAbleToDelete", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property NotAllRequiredTemplateFieldsAreFilled() As String
            Get
                Return ResourceManager.GetString("NotAllRequiredTemplateFieldsAreFilled", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property NothingIsPreventingYouToPublishThisPackage() As String
            Get
                Return ResourceManager.GetString("NothingIsPreventingYouToPublishThisPackage", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property NotValid() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("NotValid", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property NotValidated() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("NotValidated", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property NotValidFiles0() As String
            Get
                Return ResourceManager.GetString("NotValidFiles0", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property OneOrMoreInstancesRunning() As String
            Get
                Return ResourceManager.GetString("OneOrMoreInstancesRunning", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property OneOrMoreInstancesRunningTitle() As String
            Get
                Return ResourceManager.GetString("OneOrMoreInstancesRunningTitle", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property online_16x16() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("online_16x16", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property OnlyAllowedWhenResourceIsNotNew() As String
            Get
                Return ResourceManager.GetString("OnlyAllowedWhenResourceIsNotNew", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property open_report() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("open_report", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property OperationCancelled() As String
            Get
                Return ResourceManager.GetString("OperationCancelled", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property OperationCancelling() As String
            Get
                Return ResourceManager.GetString("OperationCancelling", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property OperationSuccesfull() As String
            Get
                Return ResourceManager.GetString("OperationSuccesfull", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property OperationUnSuccesfull() As String
            Get
                Return ResourceManager.GetString("OperationUnSuccesfull", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property options_16x16() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("options_16x16", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property OptionsForm_RestartLanguage_Text() As String
            Get
                Return ResourceManager.GetString("OptionsForm_RestartLanguage_Text", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property OptionsForm_RestartLanguage_Title() As String
            Get
                Return ResourceManager.GetString("OptionsForm_RestartLanguage_Title", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property OptionsForm_Text() As String
            Get
                Return ResourceManager.GetString("OptionsForm_Text", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property OptionsForm_Title() As String
            Get
                Return ResourceManager.GetString("OptionsForm_Title", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property OtdAndAdaptiveSectionDoesnTContainAnyItems() As String
            Get
                Return ResourceManager.GetString("OtdAndAdaptiveSectionDoesnTContainAnyItems", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property OtdDoesnTContainAnyItems() As String
            Get
                Return ResourceManager.GetString("OtdDoesnTContainAnyItems", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property PackageExportHandler_CreateExportFile() As String
            Get
                Return ResourceManager.GetString("PackageExportHandler_CreateExportFile", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property PackageExportHandler_ProcessingCustomBankProperties() As String
            Get
                Return ResourceManager.GetString("PackageExportHandler_ProcessingCustomBankProperties", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ParameterCannotBeRedirectedMultiTimes() As String
            Get
                Return ResourceManager.GetString("ParameterCannotBeRedirectedMultiTimes", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ParameterNamesMustBeUniqueMessage() As String
            Get
                Return ResourceManager.GetString("ParameterNamesMustBeUniqueMessage", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ParameterRedirectionInfoIsMissing() As String
            Get
                Return ResourceManager.GetString("ParameterRedirectionInfoIsMissing", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ParametersCannotReferToItself() As String
            Get
                Return ResourceManager.GetString("ParametersCannotReferToItself", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ParametersMustHaveSameType() As String
            Get
                Return ResourceManager.GetString("ParametersMustHaveSameType", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property PasswordCannotBeEmpty() As String
            Get
                Return ResourceManager.GetString("PasswordCannotBeEmpty", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property PasswordsDoNotMatch() As String
            Get
                Return ResourceManager.GetString("PasswordsDoNotMatch", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Passwordvalidate() As String
            Get
                Return ResourceManager.GetString("Passwordvalidate", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property PasteToolStripMenuItem_Image() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("PasteToolStripMenuItem_Image", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property PerformingOperation() As String
            Get
                Return ResourceManager.GetString("PerformingOperation", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property PermissionTarget() As String
            Get
                Return ResourceManager.GetString("PermissionTarget", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property PleaseChooseThePublicationLocation() As String
            Get
                Return ResourceManager.GetString("PleaseChooseThePublicationLocation", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property PleaseCloseAllItemEditorsFirst() As String
            Get
                Return ResourceManager.GetString("PleaseCloseAllItemEditorsFirst", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property PleaseEnterACompleteAndValidPath() As String
            Get
                Return ResourceManager.GetString("PleaseEnterACompleteAndValidPath", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property PleaseEnterAValidExportFilename() As String
            Get
                Return ResourceManager.GetString("PleaseEnterAValidExportFilename", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property PleaseSelectAPrintForm() As String
            Get
                Return ResourceManager.GetString("PleaseSelectAPrintForm", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property PleaseSelectAReport() As String
            Get
                Return ResourceManager.GetString("PleaseSelectAReport", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property PleaseSelectDocx() As String
            Get
                Return ResourceManager.GetString("PleaseSelectDocx", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property PreviewTestProgressCaption() As String
            Get
                Return ResourceManager.GetString("PreviewTestProgressCaption", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property PreviewToolStripButton_Image() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("PreviewToolStripButton_Image", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property PreviewToolStripMenuItem_Image() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("PreviewToolStripMenuItem_Image", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property ProcessingResource() As String
            Get
                Return ResourceManager.GetString("ProcessingResource", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ProgressHandler_UpdatingBankAndServerCache() As String
            Get
                Return ResourceManager.GetString("ProgressHandler_UpdatingBankAndServerCache", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ProperiesToolStripMenuItem_Image() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("ProperiesToolStripMenuItem_Image", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property property_blue_icon() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("property-blue-icon", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property PropertyToolStripButton_Image() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("PropertyToolStripButton_Image", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property Publication() As String
            Get
                Return ResourceManager.GetString("Publication", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property PublicationFormWizard_PasswordRules() As String
            Get
                Return ResourceManager.GetString("PublicationFormWizard_PasswordRules", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property PublicationFormWizard_PasswordsDontMatch() As String
            Get
                Return ResourceManager.GetString("PublicationFormWizard_PasswordsDontMatch", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property PublicationLocationText() As String
            Get
                Return ResourceManager.GetString("PublicationLocationText", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property PublicationName() As String
            Get
                Return ResourceManager.GetString("PublicationName", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property PublicationOfTheBank() As String
            Get
                Return ResourceManager.GetString("PublicationOfTheBank", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property PublicationOfTheTest() As String
            Get
                Return ResourceManager.GetString("PublicationOfTheTest", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property PublicationOfTheTestPackage() As String
            Get
                Return ResourceManager.GetString("PublicationOfTheTestPackage", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property PublicationResultText() As String
            Get
                Return ResourceManager.GetString("PublicationResultText", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property PublishToolStripButton_Image() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("PublishToolStripButton_Image", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property Questify() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("Questify", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property ReadExcelAdditionalExceptionMessage() As String
            Get
                Return ResourceManager.GetString("ReadExcelAdditionalExceptionMessage", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ReadyImporting() As String
            Get
                Return ResourceManager.GetString("ReadyImporting", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Reference() As String
            Get
                Return ResourceManager.GetString("Reference", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property RefreshItemTemplate() As String
            Get
                Return ResourceManager.GetString("RefreshItemTemplate", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property RefreshToolStripButton_Image() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("RefreshToolStripButton_Image", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property RefreshToolStripMenuItem_Image() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("RefreshToolStripMenuItem_Image", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property Remove() As String
            Get
                Return ResourceManager.GetString("Remove", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property RepeatPassword() As String
            Get
                Return ResourceManager.GetString("RepeatPassword", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ReportsToolStripButton_Image() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("ReportsToolStripButton_Image", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property ReportsToolStripMenuItem_Image() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("ReportsToolStripMenuItem_Image", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property Resource() As String
            Get
                Return ResourceManager.GetString("Resource", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ResourceNotFound() As String
            Get
                Return ResourceManager.GetString("ResourceNotFound", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ResourceParameterContainsValueNotInBank() As String
            Get
                Return ResourceManager.GetString("ResourceParameterContainsValueNotInBank", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ResourcePropertiesFor() As String
            Get
                Return ResourceManager.GetString("ResourcePropertiesFor", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ResourcePropertyDialog_BrowseForRawDataButton_Click_AspectResourceFilter() As String
            Get
                Return ResourceManager.GetString("ResourcePropertyDialog_BrowseForRawDataButton_Click_AspectResourceFilter", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ResourcePropertyDialog_BrowseForRawDataButton_Click_ControlTemplateResourceFilter() As String
            Get
                Return ResourceManager.GetString("ResourcePropertyDialog_BrowseForRawDataButton_Click_ControlTemplateResourceFilter" & _
                        "", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ResourcePropertyDialog_BrowseForRawDataButton_Click_DeliveryResourceFilter() As String
            Get
                Return ResourceManager.GetString("ResourcePropertyDialog_BrowseForRawDataButton_Click_DeliveryResourceFilter", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ResourcePropertyDialog_BrowseForRawDataButton_Click_GenericResourceFilter() As String
            Get
                Return ResourceManager.GetString("ResourcePropertyDialog_BrowseForRawDataButton_Click_GenericResourceFilter", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ResourcePropertyDialog_BrowseForRawDataButton_Click_ItemLayoutResourceFilter() As String
            Get
                Return ResourceManager.GetString("ResourcePropertyDialog_BrowseForRawDataButton_Click_ItemLayoutResourceFilter", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ResourcePropertyDialog_BrowseForRawDataButton_Click_ItemResourceFilter() As String
            Get
                Return ResourceManager.GetString("ResourcePropertyDialog_BrowseForRawDataButton_Click_ItemResourceFilter", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ResourcePropertyDialog_BrowseForRawDataButton_Click_TestPackageResourceFilter() As String
            Get
                Return ResourceManager.GetString("ResourcePropertyDialog_BrowseForRawDataButton_Click_TestPackageResourceFilter", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ResourcePropertyDialog_BrowseForRawDataButton_Click_TestResourceFilter() As String
            Get
                Return ResourceManager.GetString("ResourcePropertyDialog_BrowseForRawDataButton_Click_TestResourceFilter", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ResourcePropertyDialog_ExportButton_Click_DialogTitle() As String
            Get
                Return ResourceManager.GetString("ResourcePropertyDialog_ExportButton_Click_DialogTitle", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ResourcePropertyDialog_ExportButton_Click_ResourceExported() As String
            Get
                Return ResourceManager.GetString("ResourcePropertyDialog_ExportButton_Click_ResourceExported", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ResourcesToMoveMustBeInSameBank() As String
            Get
                Return ResourceManager.GetString("ResourcesToMoveMustBeInSameBank", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ResourceToImportDestinationCurrentBank() As String
            Get
                Return ResourceManager.GetString("ResourceToImportDestinationCurrentBank", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ResourceToImportDestinationCurrentBankExcel() As String
            Get
                Return ResourceManager.GetString("ResourceToImportDestinationCurrentBankExcel", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ResourceToImportPsychometricData() As String
            Get
                Return ResourceManager.GetString("ResourceToImportPsychometricData", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ResourceToPublishDestinationPublicationMethod() As String
            Get
                Return ResourceManager.GetString("ResourceToPublishDestinationPublicationMethod", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Restore() As String
            Get
                Return ResourceManager.GetString("Restore", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property RolePropertyTitle() As String
            Get
                Return ResourceManager.GetString("RolePropertyTitle", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Save() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("Save", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property Save16() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("Save16", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property SaveAs16() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("SaveAs16", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property SaveClose16() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("SaveClose16", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property Saving0312() As String
            Get
                Return ResourceManager.GetString("Saving0312", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property SearchToolStripButton_Image() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("SearchToolStripButton_Image", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property SearchToolStripMenuItem_Image() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("SearchToolStripMenuItem_Image", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property Section0() As String
            Get
                Return ResourceManager.GetString("Section0", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property SelectAFileToAdd() As String
            Get
                Return ResourceManager.GetString("SelectAFileToAdd", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property SelectAnUserBeforeContinuing() As String
            Get
                Return ResourceManager.GetString("SelectAnUserBeforeContinuing", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property SelectCesPreview() As String
            Get
                Return ResourceManager.GetString("SelectCesPreview", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property SelectDependentResourceDialog_NoResourceSelectedMessage() As String
            Get
                Return ResourceManager.GetString("SelectDependentResourceDialog_NoResourceSelectedMessage", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property SelectedBank() As String
            Get
                Return ResourceManager.GetString("SelectedBank", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property SelectedFolderDoesNotExist() As String
            Get
                Return ResourceManager.GetString("SelectedFolderDoesNotExist", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property SelectedMultipleInValidFiles() As String
            Get
                Return ResourceManager.GetString("SelectedMultipleInValidFiles", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property SelectedMultipleValidFiles() As String
            Get
                Return ResourceManager.GetString("SelectedMultipleValidFiles", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property SelectedSingleInValidFile() As String
            Get
                Return ResourceManager.GetString("SelectedSingleInValidFile", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property SelectedSingleValidFile() As String
            Get
                Return ResourceManager.GetString("SelectedSingleValidFile", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property SelectFilesToAdd() As String
            Get
                Return ResourceManager.GetString("SelectFilesToAdd", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property SelectReport() As String
            Get
                Return ResourceManager.GetString("SelectReport", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property SelectReportDescription() As String
            Get
                Return ResourceManager.GetString("SelectReportDescription", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property SelectReportLocation() As String
            Get
                Return ResourceManager.GetString("SelectReportLocation", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property SelectReportLocationDescription() As String
            Get
                Return ResourceManager.GetString("SelectReportLocationDescription", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property SelectResourceDialog_CannotSelectBecauseOfStatus() As String
            Get
                Return ResourceManager.GetString("SelectResourceDialog_CannotSelectBecauseOfStatus", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property SelectSupportedViewsForAssessmentTestDialog_ContentLabel_AddRemoveViewTypes() As String
            Get
                Return ResourceManager.GetString("SelectSupportedViewsForAssessmentTestDialog_ContentLabel_AddRemoveViewTypes", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property SelectSupportedViewsForAssessmentTestDialog_ContentLabel_NewTest() As String
            Get
                Return ResourceManager.GetString("SelectSupportedViewsForAssessmentTestDialog_ContentLabel_NewTest", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property SelectSupportedViewsForAssessmentTestDialog_PleaseSelectViews_Message() As String
            Get
                Return ResourceManager.GetString("SelectSupportedViewsForAssessmentTestDialog_PleaseSelectViews_Message", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property SelectSupportedViewsForAssessmentTestDialog_PleaseSelectViews_Title() As String
            Get
                Return ResourceManager.GetString("SelectSupportedViewsForAssessmentTestDialog_PleaseSelectViews_Title", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property SelectSupportedViewsForTestPackageDialog_ContentLabel_AddRemoveViewTypes() As String
            Get
                Return ResourceManager.GetString("SelectSupportedViewsForTestPackageDialog_ContentLabel_AddRemoveViewTypes", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property SelectSupportedViewsForTestPackageDialog_ContentLabel_NewTestPackage() As String
            Get
                Return ResourceManager.GetString("SelectSupportedViewsForTestPackageDialog_ContentLabel_NewTestPackage", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property SelectTargetsForTestPackages() As String
            Get
                Return ResourceManager.GetString("SelectTargetsForTestPackages", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property SelectTargetsForTests() As String
            Get
                Return ResourceManager.GetString("SelectTargetsForTests", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property SelectTemplateDialog_NoTemplateSelected_Text() As String
            Get
                Return ResourceManager.GetString("SelectTemplateDialog_NoTemplateSelected_Text", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property SelectTemplateDialog_NoTemplateSelected_Title() As String
            Get
                Return ResourceManager.GetString("SelectTemplateDialog_NoTemplateSelected_Title", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ServiceNotAvailable() As String
            Get
                Return ResourceManager.GetString("ServiceNotAvailable", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property SetExportSettings() As String
            Get
                Return ResourceManager.GetString("SetExportSettings", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ShowPassword() As String
            Get
                Return ResourceManager.GetString("ShowPassword", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ShowPasswordImg() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("ShowPasswordImg", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property ShowReport() As String
            Get
                Return ResourceManager.GetString("ShowReport", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property sinc_16x16() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("sinc_16x16", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property SkipConflictingBankPropertiesAndContinueWithImport() As String
            Get
                Return ResourceManager.GetString("SkipConflictingBankPropertiesAndContinueWithImport", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property SolutionWillBeDeletedNoMatchPossible() As String
            Get
                Return ResourceManager.GetString("SolutionWillBeDeletedNoMatchPossible", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property SortKeyMustBeNumeric() As String
            Get
                Return ResourceManager.GetString("SortKeyMustBeNumeric", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property SourceEditor_SaveItem_ItemSavedStatusMessage() As String
            Get
                Return ResourceManager.GetString("SourceEditor_SaveItem_ItemSavedStatusMessage", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property SourceEditor_SaveItem_SaveFailedStatusbarMessage() As String
            Get
                Return ResourceManager.GetString("SourceEditor_SaveItem_SaveFailedStatusbarMessage", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property SourceEditorControlTemplateTitle() As String
            Get
                Return ResourceManager.GetString("SourceEditorControlTemplateTitle", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property SourceEditorDataSourceTitle() As String
            Get
                Return ResourceManager.GetString("SourceEditorDataSourceTitle", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property SourceEditorItemLayoutTemplateTitle() As String
            Get
                Return ResourceManager.GetString("SourceEditorItemLayoutTemplateTitle", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property SourceFileNotFoundErrorMessage() As String
            Get
                Return ResourceManager.GetString("SourceFileNotFoundErrorMessage", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Splashscreen_Questify_Builder() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("Splashscreen_Questify_Builder", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property StartExport() As String
            Get
                Return ResourceManager.GetString("StartExport", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property StartGenerationReport() As String
            Get
                Return ResourceManager.GetString("StartGenerationReport", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property StartImport() As String
            Get
                Return ResourceManager.GetString("StartImport", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property StartPublication() As String
            Get
                Return ResourceManager.GetString("StartPublication", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property StartPublishingTest() As String
            Get
                Return ResourceManager.GetString("StartPublishingTest", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property StartUpWarningCaption() As String
            Get
                Return ResourceManager.GetString("StartUpWarningCaption", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property StartUpWarningText() As String
            Get
                Return ResourceManager.GetString("StartUpWarningText", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Status_GetData() As String
            Get
                Return ResourceManager.GetString("Status_GetData", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Status_SavingItem() As String
            Get
                Return ResourceManager.GetString("Status_SavingItem", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property StatusBar_ItemsFound() As String
            Get
                Return ResourceManager.GetString("StatusBar_ItemsFound", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property StatusOfResourcesUsedInTest() As String
            Get
                Return ResourceManager.GetString("StatusOfResourcesUsedInTest", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Succesfull() As String
            Get
                Return ResourceManager.GetString("Succesfull", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property SuccesfulyExported() As String
            Get
                Return ResourceManager.GetString("SuccesfulyExported", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property SuccesfulyExportedResourcesTo() As String
            Get
                Return ResourceManager.GetString("SuccesfulyExportedResourcesTo", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property SuccesfulyImportedToCurrentBank() As String
            Get
                Return ResourceManager.GetString("SuccesfulyImportedToCurrentBank", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property SureToDelete() As String
            Get
                Return ResourceManager.GetString("SureToDelete", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TargetSpecifiedMoreThanOnceMessage() As String
            Get
                Return ResourceManager.GetString("TargetSpecifiedMoreThanOnceMessage", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TemplateSwitchWarningContinue() As String
            Get
                Return ResourceManager.GetString("TemplateSwitchWarningContinue", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Test() As String
            Get
                Return ResourceManager.GetString("Test", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TestDoesNotContainAnyItemsPleaseAddItemsToTheTestBeforePublishing() As String
            Get
                Return ResourceManager.GetString("TestDoesNotContainAnyItemsPleaseAddItemsToTheTestBeforePublishing", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TestEditor_ChangeViewTypes_InCompatibleItemsInTestMessage() As String
            Get
                Return ResourceManager.GetString("TestEditor_ChangeViewTypes_InCompatibleItemsInTestMessage", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TestEditor_ChangeViewTypes_InCompatibleItemsInTestTitle() As String
            Get
                Return ResourceManager.GetString("TestEditor_ChangeViewTypes_InCompatibleItemsInTestTitle", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TestEditor_DeleteComponentsQuestionMessage() As String
            Get
                Return ResourceManager.GetString("TestEditor_DeleteComponentsQuestionMessage", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TestEditor_ItemNotFoundMessage() As String
            Get
                Return ResourceManager.GetString("TestEditor_ItemNotFoundMessage", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TestEditor_ItemNotFoundTitle() As String
            Get
                Return ResourceManager.GetString("TestEditor_ItemNotFoundTitle", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TestEditor_ItemNotInBank() As String
            Get
                Return ResourceManager.GetString("TestEditor_ItemNotInBank", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TestEditor_Load_TestDefinitionLoaded() As String
            Get
                Return ResourceManager.GetString("TestEditor_Load_TestDefinitionLoaded", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TestEditor_MultipleItemsAlreadyExistsInTestMessage() As String
            Get
                Return ResourceManager.GetString("TestEditor_MultipleItemsAlreadyExistsInTestMessage", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TestEditor_MultipleItemsAlreadyInTestAndBecauseOfWorkFlowStatusMessage() As String
            Get
                Return ResourceManager.GetString("TestEditor_MultipleItemsAlreadyInTestAndBecauseOfWorkFlowStatusMessage", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TestEditor_MultipleItemsNotAddedInTest_AlreadyInTestAndIncompatibleViewTypesMessage() As String
            Get
                Return ResourceManager.GetString("TestEditor_MultipleItemsNotAddedInTest_AlreadyInTestAndIncompatibleViewTypesMessa" & _
                        "ge", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TestEditor_MultipleItemsNotAddedInTest_AlreadyInTestAndWorkflowStatusAndIncompatibleViewTypesMessage() As String
            Get
                Return ResourceManager.GetString("TestEditor_MultipleItemsNotAddedInTest_AlreadyInTestAndWorkflowStatusAndIncompati" & _
                        "bleViewTypesMessage", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TestEditor_MultipleItemsNotAddedInTest_IncompatibleViewTypesMessage() As String
            Get
                Return ResourceManager.GetString("TestEditor_MultipleItemsNotAddedInTest_IncompatibleViewTypesMessage", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TestEditor_MultipleItemsNotAddedInTest_WorkflowStatusAndIncompatibleViewTypesMessage() As String
            Get
                Return ResourceManager.GetString("TestEditor_MultipleItemsNotAddedInTest_WorkflowStatusAndIncompatibleViewTypesMess" & _
                        "age", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TestEditor_MultipleItemsNotAddedToTestBecauseOfWorkFlowStatusMessage() As String
            Get
                Return ResourceManager.GetString("TestEditor_MultipleItemsNotAddedToTestBecauseOfWorkFlowStatusMessage", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TestEditor_MultipleItemsNotInBank() As String
            Get
                Return ResourceManager.GetString("TestEditor_MultipleItemsNotInBank", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TestEditor_RemoveViewTypeMessageBox() As String
            Get
                Return ResourceManager.GetString("TestEditor_RemoveViewTypeMessageBox", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TestEditor_RemoveViewTypeMessageBoxTitle() As String
            Get
                Return ResourceManager.GetString("TestEditor_RemoveViewTypeMessageBoxTitle", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TestEditor_SaveTest_SaveFailedStatusbarMessage() As String
            Get
                Return ResourceManager.GetString("TestEditor_SaveTest_SaveFailedStatusbarMessage", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TestEditor_SaveTest_TestSavedStatusbarMessage() As String
            Get
                Return ResourceManager.GetString("TestEditor_SaveTest_TestSavedStatusbarMessage", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TestEditor_TestCodeNameAlreadyExistsInBankHierarchy() As String
            Get
                Return ResourceManager.GetString("TestEditor_TestCodeNameAlreadyExistsInBankHierarchy", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TestEditor_TestCodeRenamingIsSucceeded() As String
            Get
                Return ResourceManager.GetString("TestEditor_TestCodeRenamingIsSucceeded", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TestEditor_TestPreview_IsDirty() As String
            Get
                Return ResourceManager.GetString("TestEditor_TestPreview_IsDirty", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TestEditor_ValidateItem_ValidationErrors() As String
            Get
                Return ResourceManager.GetString("TestEditor_ValidateItem_ValidationErrors", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TestEditorv2_CannotAddItemIncompatibleViewTypes() As String
            Get
                Return ResourceManager.GetString("TestEditorv2_CannotAddItemIncompatibleViewTypes", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TestEditorv2_OldModelOpenedStatusBarMessage() As String
            Get
                Return ResourceManager.GetString("TestEditorv2_OldModelOpenedStatusBarMessage", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TestPackage() As String
            Get
                Return ResourceManager.GetString("TestPackage", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TestPackageDoesNotContainAnytestPleaseAddTetsToTheTestPackagebeforePublishing() As String
            Get
                Return ResourceManager.GetString("TestPackageDoesNotContainAnytestPleaseAddTetsToTheTestPackagebeforePublishing", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TestPackageEditor_CannotAddTestIncompatibleViewTypes() As String
            Get
                Return ResourceManager.GetString("TestPackageEditor_CannotAddTestIncompatibleViewTypes", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TestPackageEditor_ChangeViewTypes_InCompatibleTestsInTestPackageMessage() As String
            Get
                Return ResourceManager.GetString("TestPackageEditor_ChangeViewTypes_InCompatibleTestsInTestPackageMessage", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TestPackageEditor_ChangeViewTypes_InCompatibleTestsInTestPackageTitle() As String
            Get
                Return ResourceManager.GetString("TestPackageEditor_ChangeViewTypes_InCompatibleTestsInTestPackageTitle", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TestPackageEditor_DeleteComponentsQuestionMessage() As String
            Get
                Return ResourceManager.GetString("TestPackageEditor_DeleteComponentsQuestionMessage", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TestPackageEditor_Load_TestPackageDefinitionLoaded() As String
            Get
                Return ResourceManager.GetString("TestPackageEditor_Load_TestPackageDefinitionLoaded", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TestPackageEditor_MultipleItemsNotInBank() As String
            Get
                Return ResourceManager.GetString("TestPackageEditor_MultipleItemsNotInBank", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TestPackageEditor_MultipleTestNotAddedToTestPackageBecauseOfWorkFlowStatusMessage() As String
            Get
                Return ResourceManager.GetString("TestPackageEditor_MultipleTestNotAddedToTestPackageBecauseOfWorkFlowStatusMessage" & _
                        "", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TestPackageEditor_MultipleTestsAlreadyExistsInTestPackageMessage() As String
            Get
                Return ResourceManager.GetString("TestPackageEditor_MultipleTestsAlreadyExistsInTestPackageMessage", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TestPackageEditor_MultipleTestsAlreadyInTestPackageAndBecauseOfWorkFlowStatusMessage() As String
            Get
                Return ResourceManager.GetString("TestPackageEditor_MultipleTestsAlreadyInTestPackageAndBecauseOfWorkFlowStatusMess" & _
                        "age", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TestPackageEditor_MultipleTestsNotAddedInTestPackage_AlreadyInTestAndWorkflowStatusAndIncompatibleViewTypesMessage() As String
            Get
                Return ResourceManager.GetString("TestPackageEditor_MultipleTestsNotAddedInTestPackage_AlreadyInTestAndWorkflowStat" & _
                        "usAndIncompatibleViewTypesMessage", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TestPackageEditor_MultipleTestsNotAddedInTestPackage_AlreadyInTestPackageAndIncompatibleViewTypesMessage() As String
            Get
                Return ResourceManager.GetString("TestPackageEditor_MultipleTestsNotAddedInTestPackage_AlreadyInTestPackageAndIncom" & _
                        "patibleViewTypesMessage", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TestPackageEditor_MultipleTestsNotAddedInTestPackage_IncompatibleViewTypesMessage() As String
            Get
                Return ResourceManager.GetString("TestPackageEditor_MultipleTestsNotAddedInTestPackage_IncompatibleViewTypesMessage" & _
                        "", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TestPackageEditor_MultipleTestsNotAddedInTestPackage_WorkflowStatusAndIncompatibleViewTypesMessage() As String
            Get
                Return ResourceManager.GetString("TestPackageEditor_MultipleTestsNotAddedInTestPackage_WorkflowStatusAndIncompatibl" & _
                        "eViewTypesMessage", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TestPackageEditor_RemoveViewTypeMessageBox() As String
            Get
                Return ResourceManager.GetString("TestPackageEditor_RemoveViewTypeMessageBox", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TestPackageEditor_RemoveViewTypeMessageBoxTitle() As String
            Get
                Return ResourceManager.GetString("TestPackageEditor_RemoveViewTypeMessageBoxTitle", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TestPackageEditor_SaveTestPackage_SaveFailedStatusbarMessage() As String
            Get
                Return ResourceManager.GetString("TestPackageEditor_SaveTestPackage_SaveFailedStatusbarMessage", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TestPackageEditor_SaveTestPackage_TestPackageSavedStatusbarMessage() As String
            Get
                Return ResourceManager.GetString("TestPackageEditor_SaveTestPackage_TestPackageSavedStatusbarMessage", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TestPackageEditor_TestCodeNameAlreadyExistsInBankHierarchy1() As String
            Get
                Return ResourceManager.GetString("TestPackageEditor_TestCodeNameAlreadyExistsInBankHierarchy1", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TestPackageEditor_TestCodeRenamingIsSucceeded() As String
            Get
                Return ResourceManager.GetString("TestPackageEditor_TestCodeRenamingIsSucceeded", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TestPackageEditor_TestNotFoundMessage() As String
            Get
                Return ResourceManager.GetString("TestPackageEditor_TestNotFoundMessage", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TestPackageEditor_TestNotFoundTitle() As String
            Get
                Return ResourceManager.GetString("TestPackageEditor_TestNotFoundTitle", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TestPackageEditor_TestNotInBank() As String
            Get
                Return ResourceManager.GetString("TestPackageEditor_TestNotInBank", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TestPackageEditor_TestPreview_IsDirty() As String
            Get
                Return ResourceManager.GetString("TestPackageEditor_TestPreview_IsDirty", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TestPackageEditor_ValidateItem_ValidationErrors() As String
            Get
                Return ResourceManager.GetString("TestPackageEditor_ValidateItem_ValidationErrors", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TestPackageNoCaps() As String
            Get
                Return ResourceManager.GetString("TestPackageNoCaps", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TestPackageToolStripMenuItem_Image() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("TestPackageToolStripMenuItem_Image", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property TestPackageToPublishPublicationMethod() As String
            Get
                Return ResourceManager.GetString("TestPackageToPublishPublicationMethod", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TestPartValidationRuleRemoved() As String
            Get
                Return ResourceManager.GetString("TestPartValidationRuleRemoved", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TestPreviewToTestPlayer_v1x() As String
            Get
                Return ResourceManager.GetString("TestPreviewToTestPlayer_v1x", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TestPreviewToTestPlayer_v2x() As String
            Get
                Return ResourceManager.GetString("TestPreviewToTestPlayer_v2x", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TestTemplate() As String
            Get
                Return ResourceManager.GetString("TestTemplate", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TestTemplateToolStripMenuItem_Image() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("TestTemplateToolStripMenuItem_Image", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property TestToolStripMenuItem_Image() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("TestToolStripMenuItem_Image", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property TestToPublishPublicationMethod() As String
            Get
                Return ResourceManager.GetString("TestToPublishPublicationMethod", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TheBankCouldnTBeCleared() As String
            Get
                Return ResourceManager.GetString("TheBankCouldnTBeCleared", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TheCodeIsNotFilledForAllItemLayoutTemplates() As String
            Get
                Return ResourceManager.GetString("TheCodeIsNotFilledForAllItemLayoutTemplates", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TheFilenameCannotContainIllegalCharacters() As String
            Get
                Return ResourceManager.GetString("TheFilenameCannotContainIllegalCharacters", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TheFollowingResourcesPreventYouFromPublishingThisPackage() As String
            Get
                Return ResourceManager.GetString("TheFollowingResourcesPreventYouFromPublishingThisPackage", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TheFollowingResourcesResultInAWarning() As String
            Get
                Return ResourceManager.GetString("TheFollowingResourcesResultInAWarning", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TheFollowingResourcesWereSkippedBecauseTheyAlreadyExistedWithinDestinationBankHierarchy() As String
            Get
                Return ResourceManager.GetString("TheFollowingResourcesWereSkippedBecauseTheyAlreadyExistedWithinDestinationBankHie" & _
                        "rarchy", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TheFollowingTestAreSkipped0() As String
            Get
                Return ResourceManager.GetString("TheFollowingTestAreSkipped0", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TheFollowingTestHaveBeenSuccessfullyPublished0() As String
            Get
                Return ResourceManager.GetString("TheFollowingTestHaveBeenSuccessfullyPublished0", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TheFollowingTestPackagesHaveBeenSuccessfullyPublished0() As String
            Get
                Return ResourceManager.GetString("TheFollowingTestPackagesHaveBeenSuccessfullyPublished0", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TheLocationOfTheTestPreviewPlayerIsnTSpecifiedInTheConfigurationFile() As String
            Get
                Return ResourceManager.GetString("TheLocationOfTheTestPreviewPlayerIsnTSpecifiedInTheConfigurationFile", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TheOperationIsSuccesfulyCompleted() As String
            Get
                Return ResourceManager.GetString("TheOperationIsSuccesfulyCompleted", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ThereAre0ItemsThatAreMissingInTheAdaptiveSection1() As String
            Get
                Return ResourceManager.GetString("ThereAre0ItemsThatAreMissingInTheAdaptiveSection1", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ThereAre0ItemsThatAreMissingInTheOtd1() As String
            Get
                Return ResourceManager.GetString("ThereAre0ItemsThatAreMissingInTheOtd1", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ThereIsNoApplicationAssociatedToThisFileExcelIsProbablyNotInstalled() As String
            Get
                Return ResourceManager.GetString("ThereIsNoApplicationAssociatedToThisFileExcelIsProbablyNotInstalled", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TheTestHaveBeenSuccesfulyPublished() As String
            Get
                Return ResourceManager.GetString("TheTestHaveBeenSuccesfulyPublished", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TheTestPackageContainTheFollowingTests() As String
            Get
                Return ResourceManager.GetString("TheTestPackageContainTheFollowingTests", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ThisWizardChecksIfTheOtdAndTheAdativeSectionContainTheSameItems() As String
            Get
                Return ResourceManager.GetString("ThisWizardChecksIfTheOtdAndTheAdativeSectionContainTheSameItems", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ThisWizardHelpsYouToExportTheSelectedResources() As String
            Get
                Return ResourceManager.GetString("ThisWizardHelpsYouToExportTheSelectedResources", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ThisWizardHelpsYouToGenerateAReport() As String
            Get
                Return ResourceManager.GetString("ThisWizardHelpsYouToGenerateAReport", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ThisWizardHelpsYouToImportResources() As String
            Get
                Return ResourceManager.GetString("ThisWizardHelpsYouToImportResources", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ThisWizardHelpsYouToPublicateTheSelectedBank() As String
            Get
                Return ResourceManager.GetString("ThisWizardHelpsYouToPublicateTheSelectedBank", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ThisWizardHelpsYouToPublicateTheSelectedTest() As String
            Get
                Return ResourceManager.GetString("ThisWizardHelpsYouToPublicateTheSelectedTest", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TitleIsARequiredField() As String
            Get
                Return ResourceManager.GetString("TitleIsARequiredField", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property TypeIsARequiredField() As String
            Get
                Return ResourceManager.GetString("TypeIsARequiredField", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property UnknownHostname() As String
            Get
                Return ResourceManager.GetString("UnknownHostname", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property up_16x16() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("up_16x16", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property UpdateAvailableWillBeInstalled_Message() As String
            Get
                Return ResourceManager.GetString("UpdateAvailableWillBeInstalled_Message", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property UpdateAvailbleWillBeInstalled_Caption() As String
            Get
                Return ResourceManager.GetString("UpdateAvailbleWillBeInstalled_Caption", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property user__16x16() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("user__16x16", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property UserAccountToolStripMenuItem_Image() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("UserAccountToolStripMenuItem_Image", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property UserPropertyTitle() As String
            Get
                Return ResourceManager.GetString("UserPropertyTitle", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Users() As String
            Get
                Return ResourceManager.GetString("Users", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Valid() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("Valid", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property ValidateParametersCollectionParameterWillShrink() As String
            Get
                Return ResourceManager.GetString("ValidateParametersCollectionParameterWillShrink", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ValidateParametersCollectionWillBeDeleted() As String
            Get
                Return ResourceManager.GetString("ValidateParametersCollectionWillBeDeleted", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ValidateParametersParameterHasADifferentType() As String
            Get
                Return ResourceManager.GetString("ValidateParametersParameterHasADifferentType", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ValidateParametersParameterWillBeDeleted() As String
            Get
                Return ResourceManager.GetString("ValidateParametersParameterWillBeDeleted", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ValidateParametersWhenOpeningEditorThisEditorCannotBeOpened() As String
            Get
                Return ResourceManager.GetString("ValidateParametersWhenOpeningEditorThisEditorCannotBeOpened", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ValidateParametersWhenOpeningItemThisItemCannotBeOpenedPossibleLossOfData() As String
            Get
                Return ResourceManager.GetString("ValidateParametersWhenOpeningItemThisItemCannotBeOpenedPossibleLossOfData", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ValidatingResource() As String
            Get
                Return ResourceManager.GetString("ValidatingResource", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Validation() As String
            Get
                Return ResourceManager.GetString("Validation", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ValidationResult() As String
            Get
                Return ResourceManager.GetString("ValidationResult", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ValidationsSucceeded() As String
            Get
                Return ResourceManager.GetString("ValidationsSucceeded", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ValidationTask() As String
            Get
                Return ResourceManager.GetString("ValidationTask", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ValidationTaskDesciption() As String
            Get
                Return ResourceManager.GetString("ValidationTaskDesciption", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property ValidFilename() As String
            Get
                Return ResourceManager.GetString("ValidFilename", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property VerifiesThatNoResourcesPreventYouFromPublishing() As String
            Get
                Return ResourceManager.GetString("VerifiesThatNoResourcesPreventYouFromPublishing", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property VerifyTheChoicesMadeInTheWizard() As String
            Get
                Return ResourceManager.GetString("VerifyTheChoicesMadeInTheWizard", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Wait() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("Wait", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property Warning() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("Warning", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property WarningDeleteCustomProperties() As String
            Get
                Return ResourceManager.GetString("WarningDeleteCustomProperties", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property WarningDeleteCustomPropertiesTitle() As String
            Get
                Return ResourceManager.GetString("WarningDeleteCustomPropertiesTitle", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Warnings() As String
            Get
                Return ResourceManager.GetString("Warnings", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property WarningsFoundInCollectionparameter() As String
            Get
                Return ResourceManager.GetString("WarningsFoundInCollectionparameter", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property web_16x16() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("web_16x16", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Public ReadOnly Property WizardFileSelectionTabTaskDescription() As String
            Get
                Return ResourceManager.GetString("WizardFileSelectionTabTaskDescription", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property WizardFileSelectionTabTaskDescription2() As String
            Get
                Return ResourceManager.GetString("WizardFileSelectionTabTaskDescription2", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property WizardForm_CodeFieldCannotContainIllegalChars() As String
            Get
                Return ResourceManager.GetString("WizardForm_CodeFieldCannotContainIllegalChars", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property WizardForm_CodeNotFilled() As String
            Get
                Return ResourceManager.GetString("WizardForm_CodeNotFilled", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property WizardForm_FileNameCannotContainIllegalChars() As String
            Get
                Return ResourceManager.GetString("WizardForm_FileNameCannotContainIllegalChars", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property WizardForm_SelectAFileMessage() As String
            Get
                Return ResourceManager.GetString("WizardForm_SelectAFileMessage", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property WizardForm_SelectAFileTitle() As String
            Get
                Return ResourceManager.GetString("WizardForm_SelectAFileTitle", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property WizardForm_SelectAnObject() As String
            Get
                Return ResourceManager.GetString("WizardForm_SelectAnObject", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property WizardForm_TitleNotFilled() As String
            Get
                Return ResourceManager.GetString("WizardForm_TitleNotFilled", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property WizardForm_TypeNotFilled() As String
            Get
                Return ResourceManager.GetString("WizardForm_TypeNotFilled", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property WizardResultTabTaskDescription() As String
            Get
                Return ResourceManager.GetString("WizardResultTabTaskDescription", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property WizardSelectedFilesTabTaskDescription() As String
            Get
                Return ResourceManager.GetString("WizardSelectedFilesTabTaskDescription", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property WizardSelectMethodTask() As String
            Get
                Return ResourceManager.GetString("WizardSelectMethodTask", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property WizardSelectTemplateTask() As String
            Get
                Return ResourceManager.GetString("WizardSelectTemplateTask", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property WizardSelectTemplateTaskDescription() As String
            Get
                Return ResourceManager.GetString("WizardSelectTemplateTaskDescription", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property WizardWelcomeTabDescription() As String
            Get
                Return ResourceManager.GetString("WizardWelcomeTabDescription", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property WordPublication_NoBookletsFound_Caption() As String
            Get
                Return ResourceManager.GetString("WordPublication_NoBookletsFound_Caption", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property WordPublication_NoBookletsFound_Message() As String
            Get
                Return ResourceManager.GetString("WordPublication_NoBookletsFound_Message", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property WouldYouLikeToOpenTheFileNow() As String
            Get
                Return ResourceManager.GetString("WouldYouLikeToOpenTheFileNow", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property Yes() As String
            Get
                Return ResourceManager.GetString("Yes", resourceCulture)
            End Get
        End Property

        Public ReadOnly Property YouHaveSelected0ValidFiles1ValidFiles2() As String
            Get
                Return ResourceManager.GetString("YouHaveSelected0ValidFiles1ValidFiles2", resourceCulture)
            End Get
        End Property
    End Module
End Namespace
