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
    class IntegerBlockRowViewModel : GapBlockRowViewModelBase<MultiType, IntegerScoringParameter, IGapScoringManipulator<MultiType>>
    {
        static PropertyChangedEventArgs MathImageArgs = ObservableHelper.CreateArgs<IntegerBlockRowViewModel>(x => x.MathImage);
        public IntegerBlockRowViewModel(IntegerScoringParameter scoringParameter, IGapScoringManipulator<Questify.Builder.Logic.MultiType> scoreManipulator, string scoreKey, int index)
            : base(scoringParameter, scoreManipulator, scoreKey, index)
        {
            MaxLength = scoringParameter.MaxLength;

            SetValidationRules();

            ComparisonType.OnChanged(w => w.DataValue).Do(_ => Value.DataValue = string.Empty);
            ComparisonType.OnChanged(w => w.DataValue).Do(_ => Value2.DataValue = string.Empty);

            OpenMathEditor = new SimpleCommand<object, object>(o => DoOpenMathEditor());
            CreateMathImage();
        }

        public int MaxLength { get; set; }
        public SimpleCommand<object, object> OpenMathEditor { private set; get; }
        public BitmapImage MathImage { get; set; }

        private void SetValidationRules()
        {
            if (!IsAllowedToRenderMathImage())
            {
                Value.AddRule(GapValidation_ValidRangeRule("Value", "Ongeldig"));
                Value.AddRule(GapValidation_NotIntegerMinValue("Value", "Ongeldig"));
                Value2.AddRule(GapValidation_NotIntegerMinValue("Value2", "Ongeldig"));
            }
        }

        internal SimpleRule GapValidation_ValidRangeRule(string property, string msg)
        {
            return new SimpleRule(property, msg, vm =>
            {
                Cinch.DataWrapper<MultiType> wrapper = (Cinch.DataWrapper<MultiType>)vm;
                var sgVM = (IntegerBlockRowViewModel)wrapper.ParentViewModel;

                return sgVM.ComparisonType.DataValue == GapComparisonType.Range && sgVM.Value.DataValue > sgVM.Value2.DataValue;
            });
        }

        internal SimpleRule GapValidation_NotIntegerMinValue(string property, string msg)
        {
            return new SimpleRule(property, msg, vm =>
            {
                if (IsAllowedToRenderMathImage()) return false; Cinch.DataWrapper<MultiType> wrapper = (Cinch.DataWrapper<MultiType>)vm;
                return wrapper.DataValue == int.MinValue;
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
