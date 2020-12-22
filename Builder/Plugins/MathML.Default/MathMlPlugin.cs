using System;
using System.Collections.Generic;
using Questify.Builder.Logic;
using Questify.Builder.Logic.Service;

namespace Questify.Builder.Plugins.MathML.Default
{
    public class MathMlPlugin : IMathMlEditorPlugin
    {
        public IMathMlEditorControl GetMathMlEditorControl(bool inContentMode)
        {
            return new MathMlEditor();
        }

        public byte[] RenderPng(string mathMl)
        {
            MathClient mathClient = new MathClient();
            return Convert.FromBase64String(mathClient.GetMathAsBase64Image(mathMl));
        }

        public byte[] RenderPng(string mathMl, Dictionary<string, string> imageOptions)
        {
            return RenderPng(mathMl);
        }

        public byte[] RenderPng(string mathMl, Dictionary<string, string> imageOptions, ref double verticalAlignValue)
        {
            return RenderPng(mathMl);
        }
    }
}
