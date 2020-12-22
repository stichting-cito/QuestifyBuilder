using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using Cinch;
using Cito.Tester.ContentModel;
using MEFedMVVM.Common;
using MEFedMVVM.ViewModelLocator;
using Questify.Builder.Logic.ContentModel;
using Questify.Builder.Logic.ContentModel.Scoring;
using Questify.Builder.UI.Wpf.Presentation.Services;
using Questify.Builder.UI.Wpf.Presentation.Types;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concrete
{
    [ExportViewModel("ItemEditor.ScoreEditors.StringScoringVM")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    class StringGapViewModel : BaseGapViewModel<string, StringScoringParameter>
    {
        static PropertyChangedEventArgs SpecificScorableItemsArgs = ObservableHelper.CreateArgs<StringGapViewModel>(x => x.SpecificScorableItems);
        private readonly ICurrentItemEditorContext _context;

        [ImportingConstructor]
        public StringGapViewModel(IViewAwareStatus viewAwareStatusService, ICurrentItemEditorContext context)
            : base(viewAwareStatusService)
        {
            _context = context;

            if (Designer.IsInDesignMode)
            {
                SpecificScorableItems = new ObservableSortedDictionary<string, GapsAndPreprocessing>(KeyComparer.ForInvariantStrings());
                SpecificScorableItems.Add("Finding A", new GapsAndPreprocessing()
                {
                    Gaps = new List<GapValueViewModel<string>>(toIndex("Finding A", new[] { new GapValue<string>("option 1"), new GapValue<string>(""), new GapValue<string>("option 3") })),
                    Preprocessing = new List<CheckableValueViewModel>(new[]
                    {
                        new CheckableValueViewModel() { DisplayName = "Preprocessing 1", Checked = true},
                        new CheckableValueViewModel() { DisplayName = "Preprocessing 2", Checked = false }
                    })
                });

            }
        }


        protected override void CreateScoringManipulator(StringScoringParameter scoreParam, Solution solution)
        {
            _scoringManipulator = scoreParam.GetScoreManipulator(solution);
        }

        protected override GapValue<string> GetEmptyItem()
        {
            return new GapValue<string>("");
        }

        protected override IEnumerable<Rule> GetRules()
        {
            return new Rule[] { GapValidation_StringNoValueRule(Msg_NoKey) };
        }

        private SimpleRule GapValidation_StringNoValueRule(string msg)
        {
            return new SimpleRule("Value", msg, vm =>
            {
                var sgVM = (GapValueViewModel<string>)vm;

                return string.IsNullOrEmpty(sgVM.Value) || string.IsNullOrWhiteSpace(sgVM.Value);
            });
        }


        public List<IResponseKeyValuePreprocessor> Preprocessors { get; private set; }

        public ObservableSortedDictionary<string, GapsAndPreprocessing> SpecificScorableItems { get; private set; }

        protected override void SpecificInit()
        {
            Preprocessors = new List<IResponseKeyValuePreprocessor>();
            if (!string.IsNullOrWhiteSpace(ScoreParameter.PreprocessRules))
            {
                var types = PreProcessingHelper.UnCollapseStrToList(ScoreParameter.PreprocessRules); Preprocessors = PreProcessingHelper.CreateUsablePreprocessorRules(types);
            }

            SpecificScorableItems = new ObservableSortedDictionary<string, GapsAndPreprocessing>(KeyComparer.ForInvariantStrings());

            foreach (var key in ScorableItems.Keys)
            {
                var toAdd = new GapsAndPreprocessing();

                toAdd.Gaps = ScorableItems[key]; toAdd.Preprocessing = new List<CheckableValueViewModel>();

                var activeRules = _scoringManipulator.GetPreProcessingMethods(key);
                foreach (var rule in Preprocessors)
                {
                    toAdd.Preprocessing.Add(new CheckableValueViewModel()
                    {
                        DisplayValue = rule.DisplayName,
                        Tag = new Tuple<string, IResponseKeyValuePreprocessor>(key, rule),
                        Checked = activeRules.Any(e => IsIntendedTypeType(e, rule.GetType())),
                        DoCheckedChanged = a => DoCheck(a)
                    });
                }
                SpecificScorableItems.Add(key, toAdd);

            }
            NotifyPropertyChanged(SpecificScorableItemsArgs);
        }

        protected override void AfterAddEmpty(string key)
        {
            SpecificScorableItems[key].Gaps = ScorableItems[key];
            NotifyPropertyChanged(SpecificScorableItemsArgs);
        }

        protected override void AfterRemove(string key)
        {
            if (ScorableItems.ContainsKey(key))
            {
                SpecificScorableItems[key].Gaps = ScorableItems[key];
            }
            else
            {
                SpecificScorableItems.Remove(key);
            }
        }

        private void DoCheck(CheckableValueViewModel a)
        {
            var tag = (Tuple<string, IResponseKeyValuePreprocessor>)a.Tag;
            var key = tag.Item1;
            var activeRules = _scoringManipulator.GetPreProcessingMethods(key).ToList();
            var ruleToModify = Enum.GetName(typeof(PreProcessingRuleId), tag.Item2.Id);

            if (a.Checked)
            {
                activeRules.Add(ruleToModify);
            }
            else
            {
                activeRules.Remove(ruleToModify);
            }

            _scoringManipulator.SetPreProcessingMethods(key, activeRules);

            if (a.Checked)
            {
                PreProcessValues(a);
            }
        }

        private void PreProcessValues(CheckableValueViewModel checkableValueViewModel)
        {
            var itemTag = (Tuple<string, IResponseKeyValuePreprocessor>)checkableValueViewModel.Tag;
            var key = itemTag.Item1;
            var preProcessor = itemTag.Item2;

            foreach (var gap in SpecificScorableItems[key].Gaps)
            {
                gap.Value = preProcessor.PreprocessValue(new StringValue(gap.Value)).ToString();
            }

        }

        private bool IsIntendedTypeType(string typeName, Type ruleType)
        {
            var ruleTypeName = ruleType.ToString();
            return (ruleTypeName == typeName) || ruleTypeName.EndsWith(typeName);
        }


    }
}
