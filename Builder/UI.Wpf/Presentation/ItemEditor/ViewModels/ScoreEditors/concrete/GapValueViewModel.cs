using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using Cinch;
using Questify.Builder.Logic.ContentModel.Scoring;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concrete
{
    class GapValueViewModel<TValue> : ValidatingViewModelBase
    {
        static PropertyChangedEventArgs ValueArgs = ObservableHelper.CreateArgs<GapValueViewModel<TValue>>(x => x.Value); static PropertyChangedEventArgs Value2Args = ObservableHelper.CreateArgs<GapValueViewModel<TValue>>(x => x.Value2); static PropertyChangedEventArgs AvailableComparisonTypesArgs = ObservableHelper.CreateArgs<GapValueViewModel<TValue>>(x => x.AvailableComparisonTypes); static PropertyChangedEventArgs ComparisonTypeArgs = ObservableHelper.CreateArgs<GapValueViewModel<TValue>>(x => x.ComparisonType);
        GapValue<TValue> _gapValue;
        readonly string _key;
        readonly int _index;

        public GapValueViewModel(string key, TValue value, int index) : this(key, new GapValue<TValue>(value), index)
        {
        }

        public GapValueViewModel(string key, GapValue<TValue> gapValue, int index)
        {
            _key = key;
            _index = index;
            _gapValue = gapValue;

            RemoveItem = new SimpleCommand<object, object>(e =>
{
    if (DoRemove != null)
        DoRemove(this);
});

            var app = System.Windows.Application.Current;
            AvailableComparisonTypes = new Dictionary<GapComparisonType, string>();
            AvailableComparisonTypes.Add(GapComparisonType.Equals, (string)app.TryFindResource("ItemEditor.ScoreEditor.Equals"));
            AvailableComparisonTypes.Add(GapComparisonType.Range, (string)app.TryFindResource("ItemEditor.ScoreEditor.Range"));
            AvailableComparisonTypes.Add(GapComparisonType.GreaterThan, (string)app.TryFindResource("ItemEditor.ScoreEditor.GreaterThan"));
            AvailableComparisonTypes.Add(GapComparisonType.SmallerThan, (string)app.TryFindResource("ItemEditor.ScoreEditor.SmallerThan"));
            AvailableComparisonTypes.Add(GapComparisonType.GreaterThanEquals, (string)app.TryFindResource("ItemEditor.ScoreEditor.GreaterThanEquals"));
            AvailableComparisonTypes.Add(GapComparisonType.SmallerThanEquals, (string)app.TryFindResource("ItemEditor.ScoreEditor.SmallerThanEquals"));
        }

        public Dictionary<GapComparisonType, String> AvailableComparisonTypes { protected set; get; }

        public string Mask { get; set; }

        public CultureInfo Culture { get; set; }

        public string Key
        {
            get { return _key; }
        }

        public int Index
        {
            get { return _index; }
        }

        public GapValue<TValue> GapValue
        {
            get { return _gapValue; }
        }

        public TValue Value
        {
            get { return _gapValue.Value; }
            set
            {
                _gapValue.Value = value;

                if (DoUpdate != null)
                    DoUpdate(this);

                NotifyChanged(ValueArgs.PropertyName);

                if (ComparisonType == GapComparisonType.Range)
                {
                    NotifyChanged(Value2Args.PropertyName);
                }
            }
        }

        public TValue Value2
        {
            get { return _gapValue.Value2; }
            set
            {
                _gapValue.Value2 = value;

                if (DoUpdate != null)
                    DoUpdate(this);

                NotifyChanged(Value2Args.PropertyName);

                if (ComparisonType == GapComparisonType.Range)
                {
                    NotifyChanged(ValueArgs.PropertyName);
                }
            }
        }

        public GapComparisonType ComparisonType
        {
            get { return _gapValue.Comparison; }
            set
            {
                _gapValue.Comparison = value;

                if (DoUpdate != null)
                    DoUpdate(this);

                NotifyChanged(ComparisonTypeArgs.PropertyName);

                if (ComparisonType == GapComparisonType.Range)
                {
                    NotifyChanged(ValueArgs.PropertyName);
                    NotifyChanged(Value2Args.PropertyName);
                }
            }
        }

        public SimpleCommand<object, object> RemoveItem { private set; get; }

        public Action<GapValueViewModel<TValue>> DoRemove { get; set; }

        public Action<GapValueViewModel<TValue>> DoUpdate { get; set; }
    }
}
