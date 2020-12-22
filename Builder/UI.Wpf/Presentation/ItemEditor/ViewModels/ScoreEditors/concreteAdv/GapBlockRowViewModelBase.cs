using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;
using Cito.Tester.ContentModel;
using Questify.Builder.Logic;
using Questify.Builder.Logic.ContentModel.Scoring;
using Questify.Builder.Logic.HelperClasses;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concreteAdv
{
    abstract class GapBlockRowViewModelBase<TGapType, TScoreParameter, TGapScoreManipulator> : BlockRowViewModelBase<TGapType, TScoreParameter, TGapScoreManipulator>
    where TScoreParameter : ScoringParameter
    where TGapScoreManipulator : IGapScoringManipulator<TGapType>
    {
        public GapBlockRowViewModelBase(ScoringParameter scoreParameter, TGapScoreManipulator scoreManipulator, string scoreKey, int index)
    : base(scoreParameter, scoreManipulator, scoreKey, index)
        { }

        private IMathMlEditorPlugin _mathMlEditorPlugin;

        protected override GapValue<TGapType> GetValue()
        {
            return ScoreManipulator.GetValue(ScoreKey, Index);
        }

        protected override void UpdateScore(TGapType value)
        {
            bool isValid = false;
            GapValue<TGapType> newValue = null;
            if (ComparisonType.DataValue == GapComparisonType.Range)
            {
                newValue = new GapValue<TGapType>(Value.DataValue, Value2.DataValue, ComparisonType.DataValue);
                isValid = Value.IsValid && Value2.IsValid;
            }
            else
            {
                newValue = new GapValue<TGapType>(Value.DataValue, ComparisonType.DataValue);
                isValid = Value.IsValid;
            }
            if (isValid)
            {
                ScoreManipulator.ReplaceKeyValueAt(ScoreKey, newValue, Index);
            }
        }

        protected override void DoRemoveValueFromScore()
        {
            var keysAndValues = ScoreManipulator.GetKeyStatus();

            foreach (string key in keysAndValues.Keys)
            {
                if (key.Equals(ScoreKey))
                {
                    if (keysAndValues[key].Count() > Index)
                    {
                        var rules = ScoreManipulator.GetPreProcessingMethods(key);
                        ScoreManipulator.RemoveKey(key);

                        if (keysAndValues[key].Count() > 1)
                        {
                            List<GapValue<TGapType>> newValues = new List<GapValue<TGapType>>();

                            int i = 0;
                            foreach (var gapValue in keysAndValues[key])
                            {
                                if (i != Index)
                                {
                                    newValues.Add(gapValue);
                                }

                                i++;
                            }

                            ScoreManipulator.SetKeys(key, newValues);
                            ScoreManipulator.SetPreProcessingMethods(key, rules);
                        }

                        break;
                    }
                }
            }
        }

        protected override string GetName()
        {


            if (ScoringParameter.Value.Count > 1)
            {
                return string.Format("{0}.{1}", base.GetName(), ScoreKey);
            }
            else
            {
                return base.GetName();
            }
        }

        protected EditMathFormulaDialog CreateMathFormulaDialog(string value, BitmapImage image)
        {
            byte[] mathMlImage = null;

            if (!string.IsNullOrEmpty(value))
            {
                mathMlImage = new byte[image.StreamSource.Length];
                image.StreamSource.Position = 0;
                image.StreamSource.Read(mathMlImage, 0, mathMlImage.Length);
            }

            var mathFormulaDialog = new EditMathFormulaDialog(mathMlImage, "temp_formula.png", true);
            return mathFormulaDialog;
        }

        protected BitmapImage BytesToMathImage(byte[] imageBytes)
        {
            var mathImage = new BitmapImage();
            mathImage.BeginInit();
            mathImage.StreamSource = new MemoryStream(imageBytes);
            mathImage.EndInit();
            mathImage.Freeze();
            return mathImage;
        }

        protected BitmapImage CreateMathBitmapImage(string value, bool allowedToRenderMathImage)
        {
            BitmapImage result = new BitmapImage();
            if (allowedToRenderMathImage)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    var imageBytes = GetMathMlPlugin().RenderPng(value);
                    if (imageBytes != null && imageBytes.Length > 0)
                    {
                        imageBytes = MathMLHelper.SetMathMLMetaDataInImage(imageBytes, value);
                        result = BytesToMathImage(imageBytes);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            return result;
        }

        protected IMathMlEditorPlugin GetMathMlPlugin()
        {
            return _mathMlEditorPlugin ?? (_mathMlEditorPlugin = PluginHelper.MathMlPlugin);
        }
    }
}
