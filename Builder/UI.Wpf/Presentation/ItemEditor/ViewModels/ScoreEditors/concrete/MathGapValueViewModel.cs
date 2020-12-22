using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;
using Cinch;
using Questify.Builder.Logic;
using Questify.Builder.Logic.ContentModel.Scoring;
using Questify.Builder.Logic.HelperClasses;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concrete
{
    class MathGapValueViewModel : GapValueViewModel<string>
    {
        public BitmapImage MathImage { get; set; }
        public SimpleCommand<object, object> OpenMathEditor { private set; get; }
        private IMathMlEditorPlugin _mathMlEditorPlugin;

        static PropertyChangedEventArgs MathImageArgs = ObservableHelper.CreateArgs<MathGapValueViewModel>(x => x.MathImage);
        public MathGapValueViewModel(string key, GapValue<string> gapvalue, int index)
            : base(key, gapvalue, index)
        {
            OpenMathEditor = new SimpleCommand<object, object>(o => DoOpenMathEditor());

            if (!string.IsNullOrEmpty(gapvalue.Value))
            {
                var imageBytes = GetMathMlPlugin().RenderPng(gapvalue.Value);
                imageBytes = MathMLHelper.SetMathMLMetaDataInImage(imageBytes, gapvalue.Value);
                BytesToMathImage(imageBytes);
            }
            else
            {
                MathImage = Application.Current.Resources["ItemEditor.ScoreEditor.ScoreEditor_NewFormula"] as BitmapImage;
            }

            var app = System.Windows.Application.Current;
            AvailableComparisonTypes = new Dictionary<GapComparisonType, string>();
            AvailableComparisonTypes.Add(GapComparisonType.Equals, (string)app.TryFindResource("ItemEditor.ScoreEditor.Equals"));
            AvailableComparisonTypes.Add(GapComparisonType.Equivalent, (string)app.TryFindResource("ItemEditor.ScoreEditor.Equivalent"));
            AvailableComparisonTypes.Add(GapComparisonType.NotEquals, (string)app.TryFindResource("ItemEditor.ScoreEditor.NotEquals"));
        }

        public MathGapValueViewModel(string key, string value, int index)
            : this(key, new GapValue<string>(value), index)
        {
        }

        private void DoOpenMathEditor()
        {
            byte[] mathMlImage = null;
            if (!string.IsNullOrEmpty(Value))
            {
                mathMlImage = new byte[MathImage.StreamSource.Length];
                MathImage.StreamSource.Position = 0;
                MathImage.StreamSource.Read(mathMlImage, 0, mathMlImage.Length);
            }
            using (var mathFormulaDialog = new EditMathFormulaDialog(mathMlImage, "temp_formula.png", true))
            {
                mathFormulaDialog.EditFormula += (sender, args) =>
                {
                    BytesToMathImage(args.Image);
                    Value = MathMLHelper.GetMetaDataFromPngImage(args.Image);
                };
                mathFormulaDialog.ShowDialog();
            }
        }

        private void BytesToMathImage(byte[] imageBytes)
        {
            MathImage = new BitmapImage();
            MathImage.BeginInit();
            MathImage.StreamSource = new MemoryStream(imageBytes);
            MathImage.EndInit();
            MathImage.Freeze();
            NotifyPropertyChanged(MathImageArgs.PropertyName);
        }

        private IMathMlEditorPlugin GetMathMlPlugin()
        {
            return _mathMlEditorPlugin ?? (_mathMlEditorPlugin = PluginHelper.MathMlPlugin);
        }

    }
}
