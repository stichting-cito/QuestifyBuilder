using System.Collections.Generic;

namespace Questify.Builder.Logic.Service.Interfaces.UI
{
    public interface IRichText
    {
        event Handlers.IsButtonCheckedChangedEventHandler IsButtonCheckedChanged;

        bool IsButtonChecked(Button btn);

        string CurrentStyle { get; }
        event Handlers.CurrentStyleChangedEventHandler CurrentStyleChanged;

        string CurrentLanguage { get; }

        event Handlers.CurrentLanguageChangedEventHandler CurrentLanguageChanged;

        bool CanBold { get; }
        bool CanItalic { get; }
        bool CanUnderlined { get; }
        bool CanSuperScript { get; }
        bool CanSubScript { get; }

        bool CanStrikethrough { get; }
        bool CanAlignLeft { get; }
        bool CanAlignMiddle { get; }

        bool CanAlignRight { get; }
        bool CanIndent { get; }

        bool CanUnIndent { get; }
        bool CanMakeNumbered { get; }
        bool CanMakeRomanNumbered { get; }

        bool CanMakeBulleted { get; }
        bool CanClearStyling { get; }

        bool CanLock { get; }
        bool CanAddTable { get; }
        bool CanAddTableByRowsColums { get; }
        bool CanAddFormula { get; }
        bool CanAddSpecialSymbol { get; }
        bool CanAddInlineControl { get; }
        bool CanAddReference { get; }
        bool CanAddPopup { get; }
        bool CanAddCustomInteraction { get; }

        bool ShowAddCustomInteraction { get; }
        bool CanSetTextToSpeechOptions { get; }
        bool CanRemoveTTS { get; }
        IList<string> UserStyles { get; }
        IList<string> Languages { get; }
        bool CanSetFormatting { get; }

        event Handlers.SelectionChangedEventHandler SelectionChanged;
        void MakeBold();
        void MakeItalic();
        void MakeUnderlined();
        void MakeSuperScript();
        void MakeSubScript();

        void MakeStrikethrough();
        void AlignLeft();
        void AlignMiddle();

        void AlignRight();
        void DoIndent();

        void DoUnIndent();
        void MakeNumbered();
        void MakeRomanNumbered();

        void MakeBulleted();
        void ClearStyling();

        void Lock();
        void AddTable();
        void AddTableByRowsColums(int columns, int row);
        void AddFormula();
        void AddSpecialSymbol(char symbol);
        void AddInlineControl();
        void AddReference(string referenceId);
        void AddCI();

        void AddPopup();
        void ApplyStyle(string styleName);

        void ApplyLanguage(string languageName);
        void InsertElementReference();
        void InsertSymbolReference();
        void InsertHighlightReference();
        void RemoveReference();
        void OverviewReferences();
        void RemoveCursor();

        void SetCursor();
        void ResetCurrentSelection();
        void SetFocusVisibility(bool setFocus);

        void MuteTextToSpeech();
        void AlternativeTextToSpeech();
        void PauseTextToSpeech(int? pause);
        void RemoveTextToSpeech();
    }
}
