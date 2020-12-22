using System.ComponentModel;
using System.Globalization;
using Cinch;
using Cito.Tester.ContentModel;
using Questify.Builder.Logic.ContentModel.Scoring;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concreteAdv
{
    class CurrencyBlockRowViewModel : GapBlockRowViewModelBase<decimal?, DecimalScoringParameter, IGapScoringManipulator<decimal?>>
    {


        static PropertyChangedEventArgs IntegerPartMaxLengthArgs = ObservableHelper.CreateArgs<DecimalBlockRowViewModel>(x => x.IntegerPartMaxLength); static PropertyChangedEventArgs FractionPartMaxLengthArgs = ObservableHelper.CreateArgs<DecimalBlockRowViewModel>(x => x.FractionPartMaxLength);


        public CurrencyBlockRowViewModel(CurrencyScoringParameter scoringParameter, IGapScoringManipulator<decimal?> scoreManipulator, string scoreKey, int index)
            : base(scoringParameter, scoreManipulator, scoreKey, index)
        {
            IntegerPartMaxLength = new DataWrapper<int>(this, IntegerPartMaxLengthArgs);
            FractionPartMaxLength = new DataWrapper<int>(this, FractionPartMaxLengthArgs);

            IntegerPartMaxLength.DataValue = scoringParameter.IntegerPartMaxLength;
            FractionPartMaxLength.DataValue = scoringParameter.FractionPartMaxLength;
            CurrencyCulture = scoringParameter.CurrencyCulture;
        }



        public DataWrapper<int> IntegerPartMaxLength { get; set; }
        public DataWrapper<int> FractionPartMaxLength { get; set; }
        public string CurrencyCulture { get; set; }



        protected override string GetFormattedDisplayValue(decimal? value)
        {
            var ret = string.Empty;
            CultureInfo ci = null;
            if (!string.IsNullOrEmpty(CurrencyCulture))
            {
                try
                {
                    ci = new CultureInfo(CurrencyCulture);
                }
                catch (CultureNotFoundException)
                {
                }
            }
            if (ci == null) ci = CultureInfo.CurrentCulture;

            var formatString = string.Format("{0} {1}.{2}", ci.NumberFormat.CurrencySymbol, "".PadLeft(IntegerPartMaxLength.DataValue, '0'), "".PadLeft(FractionPartMaxLength.DataValue, '0'));

            if (value.HasValue)
            {
                ret = value.Value.ToString(formatString);
            }
            return ret;
        }


    }
}
