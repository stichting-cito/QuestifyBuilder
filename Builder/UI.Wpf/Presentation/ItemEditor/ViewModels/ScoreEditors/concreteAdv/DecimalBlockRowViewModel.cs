using System.ComponentModel;
using System.Windows.Media.Imaging;
using Cinch;
using Cito.Tester.ContentModel;
using MEFedMVVM.Common;
using Questify.Builder.Logic;
using Questify.Builder.Logic.ContentModel.Scoring;
using Questify.Builder.Logic.HelperClasses;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concreteAdv
{
    class DecimalBlockRowViewModel : GapBlockRowViewModelBase<MultiType, DecimalScoringParameter, IGapScoringManipulator<MultiType>>
    {

        static PropertyChangedEventArgs IntegerPartMaxLengthArgs = ObservableHelper.CreateArgs<DecimalBlockRowViewModel>(x => x.IntegerPartMaxLength); static PropertyChangedEventArgs FractionPartMaxLengthArgs = ObservableHelper.CreateArgs<DecimalBlockRowViewModel>(x => x.FractionPartMaxLength); static PropertyChangedEventArgs MathImageArgs = ObservableHelper.CreateArgs<IntegerBlockRowViewModel>(x => x.MathImage);


        public DecimalBlockRowViewModel(DecimalScoringParameter scoringParameter, IGapScoringManipulator<MultiType> scoreManipulator, string scoreKey, int index)
            : base(scoringParameter, scoreManipulator, scoreKey, index)
        {
            IntegerPartMaxLength = new DataWrapper<int>(this, IntegerPartMaxLengthArgs);
            FractionPartMaxLength = new DataWrapper<int>(this, FractionPartMaxLengthArgs);

            IntegerPartMaxLength.DataValue = scoringParameter.IntegerPartMaxLength;
            FractionPartMaxLength.DataValue = scoringParameter.FractionPartMaxLength;

            SetValidationRules();

            ComparisonType.OnChanged(w => w.DataValue).Do(ComparisonTypeChanged);

            OpenMathEditor = new SimpleCommand<object, object>(o => DoOpenMathEditor());
            CreateMathImage();
        }

        private void ComparisonTypeChanged(DataWrapper<GapComparisonType> obj)
        {
            Value.DataValue = string.Empty;
            Value2.DataValue = string.Empty;
        }



        public DataWrapper<int> IntegerPartMaxLength { get; set; }
        public DataWrapper<int> FractionPartMaxLength { get; set; }
        public SimpleCommand<object, object> OpenMathEditor { private set; get; }
        public BitmapImage MathImage { get; set; }



        private void SetValidationRules()
        {
            if (!IsAllowedToRenderMathImage())
            {
                Value.AddRule(GapValidation_NotDecimalMinValue("Value", "Ongeldig"));
            }
        }

        internal SimpleRule GapValidation_NotDecimalMinValue(string property, string msg)
        {
            return new SimpleRule(property, msg, vm =>
            {
                DataWrapper<MultiType> wrapper = (DataWrapper<MultiType>)vm;

                if (IsAllowedToRenderMathImage())
                {
                    return false;
                }

                var result = wrapper.DataValue == decimal.MinValue;
                return result;
            });
        }



        private void DoOpenMathEditor()
        {
            var mathFormulaDialog = CreateMathFormulaDialog(Value.DataValue, MathImage);
            mathFormulaDialog.EditFormula += (sender, args) =>
            {
                SetMathImage(args.Image);
                Value.DataValue = MathMLHelper.GetMetaDataFromPngImage(args.Image);
            };
            mathFormulaDialog.ShowDialog();
        }


        private void SetMathImage(byte[] imageBytes)
        {
            MathImage = BytesToMathImage(imageBytes);
            NotifyPropertyChanged(MathImageArgs.PropertyName);
        }

        private void CreateMathImage()
        {
            MathImage = CreateMathBitmapImage(Value.DataValue, IsAllowedToRenderMathImage());
            NotifyPropertyChanged(MathImageArgs.PropertyName);
        }

        private bool IsAllowedToRenderMathImage()
        {
            return (ComparisonType.DataValue == GapComparisonType.Dependency);
        }

    }
}
