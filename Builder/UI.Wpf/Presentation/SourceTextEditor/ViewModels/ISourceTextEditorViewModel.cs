using System;
using System.Collections.Generic;
using Cinch;
using Cito.Tester.Common;
using Questify.Builder.Logic.Service.Interfaces;
using Questify.Builder.UI.Wpf.Presentation.Types;

namespace Questify.Builder.UI.Wpf.Presentation.SourceTextEditor.ViewModels
{
    internal interface ISourceTextEditorViewModel
    {
        SimpleCommand<object, object> AddAudio { get; }
        SimpleCommand<object, object> AddFormula { get; }
        SimpleCommand<object, object> AddInlineControl { get; }
        SimpleCommand<object, object> AddPicture { get; }
        SimpleCommand<object, object> AddReference { get; }
        SimpleCommand<object, object> OpenSymbolDialog { get; }
        SimpleCommand<object, object> AddTable { get; }
        SimpleCommand<object, object> AddVideo { get; }
        SimpleCommand<object, object> AlignLeft { get; }
        SimpleCommand<object, object> AlignMiddle { get; }
        SimpleCommand<object, object> AlignRight { get; }
        SimpleCommand<object, object> Bold { get; }
        SimpleCommand<object, object> BulletedList { get; }
        SimpleCommand<object, object> ClearFormatting { get; }
        SimpleCommand<object, object> Copy { get; }
        SimpleCommand<object, object> Cut { get; }
        SimpleCommand<object, object> DeIndent { get; }
        SimpleCommand<object, object> Indent { get; }
        SimpleCommand<object, object> Italic { get; }
        SimpleCommand<object, object> Lock { get; }
        SimpleCommand<object, object> NumberedList { get; }
        SimpleCommand<object, object> PasteAsText { get; }
        SimpleCommand<object, object> PasteSpecial { get; }
        SimpleCommand<object, object> RomanNumberedList { get; }
        SimpleCommand<object, object> Save { get; }
        SimpleCommand<object, object> SaveAndClose { get; }
        SimpleCommand<object, object> SaveAs { get; }
        SimpleCommand<object, string> SetStyleCommand { get; }
        SimpleCommand<object, string> SetLanguageCommand { get; }
        SimpleCommand<object, object> StrikeThrough { get; }
        SimpleCommand<object, object> SubScript { get; }
        SimpleCommand<object, object> SuperScript { get; }
        SimpleCommand<object, object> ToggleRibbonMinimize { get; }
        SimpleCommand<object, object> Underline { get; }
        SimpleCommand<object, EventToCommandArgs> WindowClosing { get; }

        DataWrapper<System.Collections.Generic.IList<string>> AvailableStyles { get; }
        DataWrapper<System.Collections.Generic.IList<string>> AvailableLanguages { get; }
        DataWrapper<int> BankId { get; }
        DataWrapper<int?> ContextIdentifier { get; }
        DataWrapper<bool> HasError { get; }
        DataWrapper<bool> IsRibbonMinimized { get; }
        DataWrapper<bool> IsWorking { get; }
        DataWrapper<Guid> SourceTextId { get; }
        DataWrapper<Questify.Builder.Model.ContentModel.Interfaces.IPropertyEntity> GenericResourceEntity { get; }
        DataWrapper<Cinch.WorkspaceData> MetadataWorkspace { get; }
        DataWrapper<ResourceManagerBase> ResourceManager { get; }
        DataWrapper<bool> SaveNeeded { get; }
        DataWrapper<int> SelectedTab { get; }
        DataWrapper<string> WindowTitle { get; }

        event EventHandler<StringEventArgs> Updated;

        void EditorChange(object editor);
        void DoSave();
        bool NeedSave();
        void StylesheetsCollectionChanged();

        bool IsLoading { get; }
        ISourceTextEditorObjectFactory SourceTextEditorObjectFactory { get; }
        IEnumerable<SD.LLBLGen.Pro.ORMSupportClasses.EntityBase2> CustomBankProperties { get; }
        bool CustomBankPropertiesRetrieved { get; }
    }
}
