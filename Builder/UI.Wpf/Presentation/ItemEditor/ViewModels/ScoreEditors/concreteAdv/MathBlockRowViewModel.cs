using System.ComponentModel;
using System.Windows.Media.Imaging;
using Cinch;
using Cito.Tester.ContentModel;
using MEFedMVVM.Common;
using Questify.Builder.Logic.ContentModel.Scoring;
using Questify.Builder.Logic.HelperClasses;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concreteAdv
{
    internal class MathBlockRowViewModel : GapBlockRowViewModelBase<string, MathScoringParameter, IGapScoringManipulator<string>>
    {
        static PropertyChangedEventArgs MathImageArgs = ObservableHelper.CreateArgs<MathBlockRowViewModel>(x => x.MathImage);
        public MathBlockRowViewModel(MathScoringParameter scoringParameter, IGapScoringManipulator<string> scoreManipulator, string scoreKey, int index)
            : base(scoringParameter, scoreManipulator, scoreKey, index)
        {
            OpenMathEditor = new SimpleCommand<object, object>(o => DoOpenMathEditor());

            ComparisonType.OnChanged(w => w.DataValue).Do(_ => Value.DataValue = string.Empty);

            CreateMathImage();
        }

        public SimpleCommand<object, object> OpenMathEditor { private set; get; }
        public BitmapImage MathImage { get; set; }

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

        protected override void UpdateScore(string value)
        {
            System.Diagnostics.Debug.Assert(ComparisonType.DataValue != GapComparisonType.Range, "GapComparisonType Range not expected by StringBlowRowViewModel");
            base.UpdateScore(value);
        }

        private void CreateMathImage()
        {
            MathImage = CreateMathBitmapImage(Value.DataValue, IsAllowedToRenderMathImage());
            if (MathImage != null)
            {
                NotifyPropertyChanged(MathImageArgs.PropertyName);
            }
        }

        private bool IsAllowedToRenderMathImage()
        {
            return (ComparisonType.DataValue != GapComparisonType.Evaluate);
        }
    }
}
