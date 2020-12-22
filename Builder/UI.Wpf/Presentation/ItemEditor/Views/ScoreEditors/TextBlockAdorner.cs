using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.Views.ScoreEditors
{
    public class TextBlockAdorner : Adorner
    {
        public TextBlockAdorner(UIElement adornedElement)
    : base(adornedElement)
        {
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            Rect adornedElementRect = new Rect(this.AdornedElement.RenderSize);

            SolidColorBrush renderBrush = new SolidColorBrush(Colors.Green);
            renderBrush.Opacity = 0.4;

            var start = new Point(adornedElementRect.TopRight.X - 12, adornedElementRect.TopRight.Y);

            var segments = new[]
            {
                new LineSegment(adornedElementRect.TopRight, true),
                new LineSegment(new Point(adornedElementRect.TopRight.X, adornedElementRect.TopRight.Y + 12), true)
            };

            var figure = new PathFigure(start, segments, true);
            var geo = new PathGeometry(new[] { figure });
            drawingContext.DrawGeometry(renderBrush, null, geo);

        }
    }
}
