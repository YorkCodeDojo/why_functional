using System.Globalization;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace Intro;

public class TextSlide(string textToDisplay, int fontSize = 500) : Control, ISlide
{
    public DisplayResult Display(bool reset)
    {
        if (reset)
        {
            return DisplayResult.MoreToDisplay;
        }
        return DisplayResult.Completed;
    }
    
    public override void Render(DrawingContext context)
    {
        base.Render(context);

        var formattedText = new FormattedText(
            textToDisplay, 
            CultureInfo.CurrentUICulture,
            FlowDirection.LeftToRight, 
            new Typeface("Segoe UI"), 
            fontSize, 
            Brushes.White);

        var center = new Point(Bounds.Width / 2, Bounds.Height / 2);
        var origin = new Point(center.X - formattedText.Width / 2, 
                               center.Y - formattedText.Height / 2);

        context.DrawText(formattedText, origin);
    }
}