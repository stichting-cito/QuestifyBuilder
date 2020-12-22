using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using Cinch;
using Cito.Tester.ContentModel;
using MEFedMVVM.Common;
using Questify.Builder.Logic.ContentModel.Scoring;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concrete
{
    internal abstract class BaseGapViewModel<T, SP> : ScoringViewModel<SP>
        where SP : ScoringParameter
    {
        static PropertyChangedEventArgs ScorableItemsArgs = ObservableHelper.CreateArgs<BaseGapViewModel<T, SP>>(x => x.ScorableItems); static PropertyChangedEventArgs CultureArgs = ObservableHelper.CreateArgs<BaseGapViewModel<T, SP>>(x => x.Culture);
        private IViewAwareStatus _viewAwareStatusService;
        protected IGapScoringManipulator<T> _scoringManipulator;

        public SimpleCommand<object, string> AddItem { private set; get; }
        public SortedDictionary<string, List<GapValueViewModel<T>>> ScorableItems { protected set; get; }
        public string Mask { protected set; get; }
        public CultureInfo Culture { protected set; get; }

        public BaseGapViewModel(IViewAwareStatus viewAwareStatusService)
        {
            _viewAwareStatusService = viewAwareStatusService;
            _viewAwareStatusService.ViewLoaded += viewAwareStatusService_ViewLoaded;
            AddItem = new SimpleCommand<object, string>(key => AddEmptyItem(key));
            Culture = CultureInfo.CurrentCulture;
        }

        protected abstract GapValue<T> GetEmptyItem();

        protected abstract override void CreateScoringManipulator(SP scoreParam, Solution solution);

        protected abstract IEnumerable<Rule> GetRules();

        protected virtual void SpecificInit()
        {
        }

        protected virtual void AfterAddEmpty(string key)
        {
        }

        protected virtual void AfterRemove(string key)
        {
        }


        protected virtual IEnumerable<GapValueViewModel<T>> toIndex(string key, IEnumerable<GapValue<T>> enumerable)
        {
            int i = 0;
            foreach (var e in enumerable)
            {
                var gap = GetGap(key, i, e);
                foreach (var rule in GetRules())
                {
                    gap.AddRule(rule);
                }
                gap.DoRemove = x => DoRemove(x.Key, x.GapValue, x.Index);
                gap.DoUpdate = x => DoUpdate(x.Key, x.GapValue, x.Index);
                i++;
                yield return gap;
            }
        }

        protected virtual GapValueViewModel<T> GetGap(string key, int i, GapValue<T> e)
        {
            var valueValidForMask = ValidateMask(e);
            return new GapValueViewModel<T>(key, valueValidForMask ? e : GetEmptyItem(), i) { Mask = this.Mask, Culture = this.Culture };
        }

        private bool ValidateMask(GapValue<T> e)
        {
            var valueValidForMask = true;

            if (!string.IsNullOrEmpty(Mask))
            {
                MaskedTextProvider maskProvider = new MaskedTextProvider(Mask, Culture);
                var valueToStringMethodInfo = e.Value.GetType().GetMethod("ToString", new Type[] { typeof(string), typeof(IFormatProvider) });
                var text = e.Value.ToString();
                if (valueToStringMethodInfo != null)
                {

                    var formatSpecifier = Mask.Replace("9", "0").Replace("#", "0")
                        .Replace(".", Culture.NumberFormat.NumberDecimalSeparator)
                        .Replace(",", Culture.NumberFormat.NumberGroupSeparator)
                        .Replace("$", Culture.NumberFormat.CurrencySymbol);
                    text = (string)valueToStringMethodInfo.Invoke(e.Value, new object[] { formatSpecifier, Culture });

                    if (text.Length > Mask.Length && Mask.StartsWith("#")) text = text.Remove(1, 1);
                }
                valueValidForMask = maskProvider.VerifyString(text);
            }
            return valueValidForMask;
        }

        private void AddEmptyItem(string key)
        {
            var newKeys = new List<GapValue<T>>(ScorableItems[key].Select(e => e.GapValue));
            newKeys.Add(GetEmptyItem());

            _scoringManipulator.RemoveKey(key); _scoringManipulator.SetKeys(key, newKeys);

            ScorableItems = new SortedDictionary<string, List<GapValueViewModel<T>>>(StringComparer.CurrentCultureIgnoreCase);
            foreach (var kvp in _scoringManipulator.GetKeyStatus())
            {
                ScorableItems.Add(kvp.Key, new List<GapValueViewModel<T>>(toIndex(kvp.Key, kvp.Value)));
            }
            NotifyPropertyChanged(ScorableItemsArgs.PropertyName); AfterAddEmpty(key);
        }

        protected bool DoRemove(string key, GapValue<T> value, int index)
        {
            if (ScorableItems.ContainsKey(key))
            {
                ScorableItems[key].RemoveAt(index);
                _scoringManipulator.RemoveKey(key);
                _scoringManipulator.SetKeys(key, ScorableItems[key].Select(gvm => gvm.GapValue));

                ScorableItems = new SortedDictionary<string, List<GapValueViewModel<T>>>(StringComparer.CurrentCultureIgnoreCase);
                foreach (var kvp in _scoringManipulator.GetKeyStatus())
                {
                    ScorableItems.Add(kvp.Key, new List<GapValueViewModel<T>>(toIndex(kvp.Key, kvp.Value)));
                }
                AfterRemove(key);
                NotifyPropertyChanged(ScorableItemsArgs.PropertyName); return true;
            }
            else
                return false;
        }

        protected void DoUpdate(string key, GapValue<T> value, int index)
        {
            _scoringManipulator.ReplaceKeyValueAt(key, value, index);
        }

        private void viewAwareStatusService_ViewLoaded()
        {
            if (!Designer.IsInDesignMode)
            {
                var view = _viewAwareStatusService.View;
                var workspaceData = (IWorkSpaceAware)view;
                var data = (Tuple<ScoringParameter, Solution>)workspaceData.WorkSpaceContextualData.DataValue;

                Init(scorePrm: data.Item1, solution: data.Item2);

                ScorableItems = new SortedDictionary<string, List<GapValueViewModel<T>>>(StringComparer.CurrentCultureIgnoreCase);
                foreach (var kvp in _scoringManipulator.GetKeyStatus())
                {
                    ScorableItems.Add(kvp.Key, new List<GapValueViewModel<T>>(toIndex(kvp.Key, kvp.Value)));
                }
                NotifyPropertyChanged(ScorableItemsArgs.PropertyName);
                SpecificInit();
            }
        }
    }
}
