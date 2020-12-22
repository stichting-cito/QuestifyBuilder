using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Cinch;
using Cito.Tester.ContentModel;
using Questify.Builder.Logic.ContentModel.Scoring;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concrete;
using Questify.Builder.UI.Wpf.Presentation.Services;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concreteAdv
{
    class StringBlockRowViewModel : GapBlockRowViewModelBase<string, StringScoringParameter, IGapScoringManipulator<string>>
    {
        static readonly PropertyChangedEventArgs ShowPopupArgs = ObservableHelper.CreateArgs<StringBlockRowViewModel>(x => x.ShowPopup); static readonly PropertyChangedEventArgs ShowAdornerArgs = ObservableHelper.CreateArgs<StringBlockRowViewModel>(x => x.ShowAdorner);
        public StringBlockRowViewModel(StringScoringParameter scoringParameter, IGapScoringManipulator<string> scoreManipulator, string scoreKey, int index) : base(scoringParameter, scoreManipulator, scoreKey, index)
        {
            ExpectedLength = scoringParameter.ExpectedLength;
            PatternMask = scoringParameter.PatternMask;
            EditPreProcessing = new SimpleCommand<object, object>(ExecuteEditPreProcessingCommand);
            PreprocessorPerAssembly = new List<IResponseKeyValuePreprocessor>();
            PreProcessingRules = new List<CheckableValueViewModel>();
            ShowPopup = new DataWrapper<bool>(this, ShowPopupArgs);
            ShowAdorner = new DataWrapper<bool>(this, ShowAdornerArgs);

            LoadPreprocessorPerAssembly(scoringParameter);
            LoadPreProcessingRules();

        }

        public int ExpectedLength { get; set; }
        public string PatternMask { get; set; }
        public SimpleCommand<object, object> EditPreProcessing { get; private set; }
        public List<IResponseKeyValuePreprocessor> PreprocessorPerAssembly { get; private set; }
        public List<CheckableValueViewModel> PreProcessingRules { get; private set; }
        public DataWrapper<bool> ShowPopup { get; private set; }
        public DataWrapper<bool> ShowAdorner { get; private set; }

        private void LoadPreprocessorPerAssembly(StringScoringParameter scoringParameter)
        {
            if (!string.IsNullOrWhiteSpace(scoringParameter.PreprocessRules))
            {
                var context = Factory.GetExport<ICurrentItemEditorContext>();

                var types = PreProcessingHelper.UnCollapseStrToList(scoringParameter.PreprocessRules); PreprocessorPerAssembly = PreProcessingHelper.CreateUsablePreprocessorRules(types);
            }
        }

        private void LoadPreProcessingRules()
        {
            var activeRules = ScoreManipulator.GetPreProcessingMethods(ScoreKey);
            ShowAdorner.DataValue = activeRules.Any();

            foreach (var rule in PreprocessorPerAssembly)
            {
                PreProcessingRules.Add(new CheckableValueViewModel()
                {
                    DisplayValue = rule.DisplayName,
                    Tag = new Tuple<string, IResponseKeyValuePreprocessor>(ScoreKey, rule),
                    Checked = activeRules.Any(e => IsIntendedRule(e, rule)),
                    DoCheckedChanged = a => DoCheck(a)
                });
            }
        }

        private void DoCheck(CheckableValueViewModel a)
        {
            var tag = (Tuple<string, IResponseKeyValuePreprocessor>)a.Tag;
            var key = tag.Item1;
            var activeRules = ScoreManipulator.GetPreProcessingMethods(key).ToList();
            var ruleToModify = Enum.GetName(typeof(PreProcessingRuleId), tag.Item2.Id);

            if (a.Checked)
            {
                activeRules.Add(ruleToModify);
            }
            else
            {
                activeRules.Remove(ruleToModify);
            }

            ShowAdorner.DataValue = activeRules.Any();

            ScoreManipulator.SetPreProcessingMethods(key, activeRules);

            if (a.Checked)
            {
                PreProcessValues(a);
            }
        }

        private bool IsIntendedRule(string activeRule, IResponseKeyValuePreprocessor rule)
        {
            PreProcessingRuleId ruleId;
            if (Enum.TryParse(activeRule, out ruleId) && ruleId == rule.Id)
            {
                return true;
            }
            var ruleTypeName = rule.GetType().ToString();
            return (ruleTypeName == activeRule) || ruleTypeName.EndsWith(activeRule);
        }

        private void PreProcessValues(CheckableValueViewModel checkableValueViewModel)
        {
            var itemTag = (Tuple<string, IResponseKeyValuePreprocessor>)checkableValueViewModel.Tag;
            var key = itemTag.Item1;
            var preProcessor = itemTag.Item2;

            Value.DataValue = preProcessor.PreprocessValue(new StringValue(Value.DataValue)).ToString();

        }

        private void ExecuteEditPreProcessingCommand(object obj)
        {
            ShowPopup.DataValue = !ShowPopup.DataValue;
        }

        protected override void UpdateScore(string value)
        {
            System.Diagnostics.Debug.Assert(ComparisonType.DataValue != GapComparisonType.Range, "GapComparisonType Range not expected by StringBlowRowViewModel");

            base.UpdateScore(value);
        }
    }
}
