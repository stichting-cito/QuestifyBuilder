using Cinch;
using Cito.Tester.ContentModel;
using Questify.Builder.UI.Wpf.Presentation.Types;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors
{
    abstract class ScoringViewModel<T> : ValidatingViewModelBase, IScoringViewModel where T : ScoringParameter
    {
        internal const string CNoKeyRule = "NoKeyRule";
        internal const string CMinKeyRule = "MinKeyRule";
        internal const string CMaxKeyRule = "MaxKeyRule";

        private T _scorePrm;
        private Solution _solution;


        protected virtual void Init(ScoringParameter scorePrm, Solution solution)
        {
            _scorePrm = (T)scorePrm;
            _solution = solution;

            GetStrings();
            InitDefaultScoring();
        }

        private void GetStrings()
        {
            var prefix = "ItemEditor.ScoreEditor.";
            Msg_NoKey = ApplicationExtensions.GetResource(prefix + "NoKeysSet", CNoKeyRule);
            Msg_TooLittleKey = ApplicationExtensions.GetResource(prefix + "TooLittleKeySet", CMinKeyRule);
            Msg_TooManyKeys = ApplicationExtensions.GetResource(prefix + "TooManyKeysSet", CMaxKeyRule);
        }

        private void InitDefaultScoring()
        {
            CreateScoringManipulator(_scorePrm, _solution);
        }



        public string Msg_NoKey { get; internal set; }

        public string Msg_TooLittleKey { get; internal set; }

        public string Msg_TooManyKeys { get; internal set; }

        public string ControllerName
        { get { return _scorePrm.Name; } }


        protected internal virtual T ScoreParameter
        {
            get { return _scorePrm; }
        }

        protected virtual Solution Solution
        {
            get { return _solution; }
        }




        protected abstract void CreateScoringManipulator(T scoreParam, Solution solution);

    }
}
