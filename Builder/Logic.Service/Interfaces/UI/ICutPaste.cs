namespace Questify.Builder.Logic.Service.Interfaces.UI
{
    public interface ICutPaste : IViewCommands
    {

        bool CanCut { get; }
        bool CanCopy { get; }

        bool CanPaste { get; }
        void Cut();
        void Copy();
        void PasteAsText();

        void PasteSpecial();
    }
}
