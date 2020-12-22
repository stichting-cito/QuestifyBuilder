using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using Cinch;
using Cito.Tester.Common;
using Cito.Tester.ContentModel;
using Questify.Builder.Logic.Service.Classes;
using Questify.Builder.Logic.Service.HelperFunctions.Symbols;
using Questify.Builder.Logic.Service.Interfaces.UI;
using MenuItem = Fluent.MenuItem;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels
{
    class EditorViewModelBase : ViewModelBase, IDisposable
    {
        protected object CurrentEditor = new object();
        protected IAddSymbolDialog AddSymbolDialog;
        protected bool _disposed;

        public DataWrapper<ResourceManagerBase> ResourceManager { get; protected set; }
        public DataWrapper<IList<string>> AvailableStyles { get; protected set; }
        public DataWrapper<IList<string>> AvailableLanguages { get; protected set; }
        public DataWrapper<List<MenuItem>> AvailableDefaultDurations { get; protected set; }
        public DataWrapper<bool> ShowTextToSpeech { get; protected set; }
        public DataWrapper<bool> ShowTextToSpeechGroup { get; protected set; }
        public DataWrapper<int> SelectedTab { get; protected set; }
        public DataWrapper<string> WindowTitle { get; protected set; }
        public DataWrapper<bool> IsRibbonMinimized { get; protected set; }
        public DataWrapper<bool> SaveNeeded { get; protected set; }
        public DataWrapper<bool> HasError { get; protected set; }
        public DataWrapper<int?> ContextIdentifier { get; protected set; }
        public DataWrapper<bool> IsWorking { get; protected set; }

        protected SimpleCommand<object, object> Command<TIViewCommands>(Func<TIViewCommands, bool> inspect, Action<TIViewCommands> cmd)
        {
            return new SimpleCommand<object, object>(o => CurrentEditor != null && Can(CurrentEditor, inspect), o => Execute(CurrentEditor, cmd));
        }

        protected SimpleCommand<object, object> Command<TIViewCommands>(Action<TIViewCommands> cmd)
        {
            return new SimpleCommand<object, object>(o => Execute(CurrentEditor, cmd));
        }

        protected static bool Can<TIViewCommands>(object obj, Func<TIViewCommands, bool> inspect)
        {
            if (obj == null || inspect == null)
            {
                return false;
            }

            if (obj != null && obj is TIViewCommands)
            {
                var command = (TIViewCommands)obj;
                return inspect(command);
            }
            return false;
        }

        protected static void Execute<TIViewCommands>(object obj, Action<TIViewCommands> cmd)
        {
            if (obj == null || cmd == null)
            {
                return;
            }

            if (obj is TIViewCommands)
            {
                cmd((TIViewCommands)obj);
            }
        }

        public SimpleCommand<object, object> Save { get; protected set; }
        public SimpleCommand<object, object> SaveAs { get; protected set; }
        public SimpleCommand<object, object> SaveAndClose { get; protected set; }
        public SimpleCommand<object, EventToCommandArgs> WindowClosing { get; protected set; }
        public SimpleCommand<object, object> ToggleRibbonMinimize { get; protected set; }

        public SimpleCommand<object, object> PasteAsText { get; protected set; }
        public SimpleCommand<object, object> PasteSpecial { get; protected set; }
        public SimpleCommand<object, object> Cut { get; protected set; }
        public SimpleCommand<object, object> Copy { get; protected set; }
        public SimpleCommand<object, object> AddPicture { get; protected set; }
        public SimpleCommand<object, object> AddVideo { get; protected set; }
        public SimpleCommand<object, object> AddAudio { get; protected set; }
        public SimpleCommand<object, object> Bold { get; protected set; }
        public SimpleCommand<object, object> Italic { get; protected set; }
        public SimpleCommand<object, object> Underline { get; protected set; }
        public SimpleCommand<object, object> SuperScript { get; protected set; }
        public SimpleCommand<object, object> SubScript { get; protected set; }
        public SimpleCommand<object, object> StrikeThrough { get; protected set; }
        public SimpleCommand<object, object> AlignLeft { get; protected set; }
        public SimpleCommand<object, object> AlignMiddle { get; protected set; }
        public SimpleCommand<object, object> AlignRight { get; protected set; }
        public SimpleCommand<object, string> SetStyleCommand { get; protected set; }
        public SimpleCommand<object, string> SetLanguageCommand { get; protected set; }
        public SimpleCommand<object, object> ClearFormatting { get; protected set; }
        public SimpleCommand<object, object> Lock { get; protected set; }
        public SimpleCommand<object, object> NumberedList { get; protected set; }
        public SimpleCommand<object, object> RomanNumberedList { get; protected set; }
        public SimpleCommand<object, object> BulletedList { get; protected set; }
        public SimpleCommand<object, object> Indent { get; protected set; }
        public SimpleCommand<object, object> DeIndent { get; protected set; }
        public SimpleCommand<object, object> AddTable { get; protected set; }
        public SimpleCommand<object, object> AddFormula { get; protected set; }
        public SimpleCommand<object, object> AddInlineControl { get; protected set; }
        public SimpleCommand<object, object> AddReference { get; protected set; }
        public SimpleCommand<object, object> OpenSymbolDialog { get; protected set; }
        public SimpleCommand<object, object> MuteTextToSpeech { get; protected set; }
        public SimpleCommand<object, object> AlternativeTextToSpeech { get; protected set; }
        public SimpleCommand<object, int?> PauseTextToSpeech { get; protected set; }
        public SimpleCommand<object, object> RemoveTextToSpeech { get; protected set; }

        protected void InitCopyPasteCommands()
        {
            PasteAsText = Command<ICutPaste>(cmd => cmd.PasteAsText());
            PasteSpecial = Command<ICutPaste>(cmd => cmd.PasteSpecial());
            Cut = Command<ICutPaste>(cmd => cmd.Cut());
            Copy = Command<ICutPaste>(cmd => cmd.Copy());
        }

        protected virtual void InitMediaCommands()
        {
            AddPicture = Command<IMedia>(can => can.CanAddImage, cmd => cmd.AddImage());
            AddVideo = Command<IMedia>(can => can.CanAddVideo, cmd => cmd.AddVideo());
            AddAudio = Command<IMedia>(can => can.CanAddAudio, cmd => cmd.AddAudio());
        }

        protected void InitTextFormatCommands()
        {
            InitTextFormatBasicCommands();
            InitTextFormatAlignCommands();
            InitTextFormatListCommands();
            InitTextFormatIndentCommands();
        }

        private void InitTextFormatBasicCommands()
        {
            Bold = Command<IRichText>(can => can.CanBold, cmd => cmd.MakeBold());
            Italic = Command<IRichText>(can => can.CanItalic, cmd => cmd.MakeItalic());
            Underline = Command<IRichText>(can => can.CanUnderlined, cmd => cmd.MakeUnderlined());
            SuperScript = Command<IRichText>(can => can.CanSuperScript, cmd => cmd.MakeSuperScript());
            SubScript = Command<IRichText>(can => can.CanSubScript, cmd => cmd.MakeSubScript());
            StrikeThrough = Command<IRichText>(can => can.CanStrikethrough, cmd => cmd.MakeStrikethrough());
        }

        private void InitTextFormatAlignCommands()
        {
            AlignLeft = Command<IRichText>(can => can.CanAlignLeft, cmd => cmd.AlignLeft());
            AlignMiddle = Command<IRichText>(can => can.CanAlignMiddle, cmd => cmd.AlignMiddle());
            AlignRight = Command<IRichText>(can => can.CanAlignRight, cmd => cmd.AlignRight());
        }

        private void InitTextFormatListCommands()
        {
            ClearFormatting = Command<IRichText>(can => can.CanClearStyling, cmd => cmd.ClearStyling());
            Lock = Command<IRichText>(can => can.CanLock, cmd => cmd.Lock());
            NumberedList = Command<IRichText>(can => can.CanMakeNumbered, cmd => cmd.MakeNumbered());
            BulletedList = Command<IRichText>(can => can.CanMakeBulleted, cmd => cmd.MakeBulleted());
            RomanNumberedList = Command<IRichText>(can => can.CanMakeRomanNumbered, cmd => cmd.MakeRomanNumbered());
        }

        private void InitTextFormatIndentCommands()
        {
            Indent = Command<IRichText>(can => can.CanIndent, cmd => cmd.DoIndent());
            DeIndent = Command<IRichText>(can => can.CanUnIndent, cmd => cmd.DoUnIndent());
        }

        protected void InitTextToSpeechCommands()
        {
            MuteTextToSpeech = Command<IRichText>(can => can.CanSetTextToSpeechOptions, cmd => cmd.MuteTextToSpeech());
            AlternativeTextToSpeech = Command<IRichText>(can => can.CanSetTextToSpeechOptions, cmd => cmd.AlternativeTextToSpeech());
            PauseTextToSpeech = new SimpleCommand<object, int?>(CanSetTextToSpeechOptions, DoSetPauseTextToSpeech);
            RemoveTextToSpeech = Command<IRichText>(can => can.CanRemoveTTS, cmd => cmd.RemoveTextToSpeech());
        }
        private bool CanSetTextToSpeechOptions(object obj)
        {
            return (CurrentEditor as IRichText)?.CanSetTextToSpeechOptions ?? false;
        }

        protected void SetTextToSpeechOptionsVisibility(IRichText richTextEditor)
        {
            ShowTextToSpeechGroup.DataValue = richTextEditor != null &&
                                              (AvailableLanguages.DataValue.Any() ||
                                               richTextEditor.CanSetTextToSpeechOptions);
        }

        protected void SetTextToSpeechOptionsEnabled(IRichText richTextEditor)
        {
            ShowTextToSpeech.DataValue = richTextEditor != null && richTextEditor.CanSetTextToSpeechOptions;
            var durationsList = new List<MenuItem>();
            foreach (var pd in PauseDuration.FromConfig)
            {
                var menuItem = new MenuItem
                {
                    Header = pd.Name,
                    Command = PauseTextToSpeech,
                    CommandParameter = pd.Duration
                };
                durationsList.Add(menuItem);
            }

            AvailableDefaultDurations.DataValue = durationsList;
        }

        protected void SetStyle(string style)
        {
            if (!string.IsNullOrEmpty(style))
            {
                var e = CurrentEditor as IRichText;
                e?.ApplyStyle(style);
            }
        }

        protected void SetLanguage(string language)
        {
            if (!string.IsNullOrEmpty(language))
            {
                var e = CurrentEditor as IRichText;
                e?.ApplyLanguage(language);
            }
        }

        protected void SetAvailableStyles(IRichText richTextEditor)
        {
            AvailableStyles.DataValue = richTextEditor == null ? new List<string>() : new List<string>(richTextEditor.UserStyles);
        }

        protected void SetAvailableLanguages(IRichText richTextEditor)
        {
            AvailableLanguages.DataValue = richTextEditor == null ? new List<string>() : new List<string>(richTextEditor.Languages);
        }

        protected bool CanAddSpecialSymbol()
        {
            var editor = CurrentEditor as IRichText;
            if (editor != null)
                return editor.CanAddSpecialSymbol;
            return false;
        }

        private void DoSetPauseTextToSpeech(int? obj)
        {
            if (CanSetTextToSpeechOptions(null))
                (CurrentEditor as IRichText)?.PauseTextToSpeech(obj);
        }

        public void DoOpenSymbolDialog()
        {
            if (AddSymbolDialog == null || AddSymbolDialog.IsDisposed())
                AddSymbolDialog = new AddSymbolsDialog();
            var focussedControl = CurrentEditor as Control;
            if (focussedControl != null)
            {
                AddSymbolDialog.Show(focussedControl, focussedControl.Location);

                AddSymbolDialog.SpecialSymbolPicked += (sender, e) => InsertSymbol(e);
                AddSymbolDialog.DialogClosed += (sender, e) =>
                {
                    var symbolDialog = sender as IAddSymbolDialog;
                    symbolDialog?.Dispose();
                };
            }
        }

        private void InsertSymbol(SpecialSymbolEventArgs e)
        {
            var editor = CurrentEditor as IRichText;
            editor?.AddSpecialSymbol(e.UnicodeValue[0]);
        }

        protected void Handle_TestSessionContext_ResourceNeeded(object sender, ResourceNeededEventArgs e)
        {
            if (e.Command == ResourceNeededCommand.Resource)
            {
                var request = new ResourceRequestDTO();
                var r = e.TypedResourceType != null ? ResourceManager.DataValue.GetTypedResource(e.ResourceName, e.TypedResourceType, request) : ResourceManager.DataValue.GetResource(e.ResourceName, e.StreamProcessingDelegate, request);

                e.BinaryResource = r;
            }
            else
            {
                e.BinaryResource = new BinaryResource(new object());
            }

            if (e.Command == ResourceNeededCommand.MetaData)
            {
                var fetchedMetaData = ResourceManager.DataValue.GetResourceMetaData(e.ResourceName);
                e.Metadata.Clear();
                e.Metadata.AddRange(fetchedMetaData);
            }
        }

        public new void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                base.Dispose();
                AddSymbolDialog?.Dispose();
                CleanUp();
            }

            _disposed = true;
        }

        protected virtual void CleanUp()
        {
        }
    }
}
