using System;
using System.Windows.Forms;
using System.Xml.Linq;
using Questify.Builder.Logic;
using Questify.Builder.Logic.HelperClasses;

namespace Questify.Builder.Plugins.MathML.Default
{
    public partial class MathMlEditor : UserControl, IMathMlEditorControl
    {
        public MathMlEditor()
        {
            InitializeComponent();
        }

        public void EditMathMl(string mathMl)
        {
            txtbox.Text = mathMl;
        }

        public string GetMathMl()
        {
            string input = txtbox.Text;
            if (MathMLHelper.IsValidMathMlExpression(ref input))
            {
                return input;
            }
            return string.Empty;
        }
    }
}
