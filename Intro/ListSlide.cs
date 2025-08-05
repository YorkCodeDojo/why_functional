using System.Globalization;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace Intro;

public class ListSlide(string[] textToDisplay, int fontSize = 75) : Control, ISlide
{
    private int _state = 0;
    
    public DisplayResult Display(bool reset)
    {
        if (reset)
        {
            _state = 1;
            return DisplayResult.MoreToDisplay;
        }

        if (_state < textToDisplay.Length )
        {
            _state++;
            InvalidateVisual();
            return DisplayResult.MoreToDisplay;
        }

       
        return DisplayResult.Completed;
    }
    
    public override void Render(DrawingContext context)
    {
        base.Render(context);

        for (var i= 0;i  <_state; i++)
        {
            var formattedText = new FormattedText(
                textToDisplay[i], 
                CultureInfo.CurrentUICulture,
                FlowDirection.LeftToRight, 
                new Typeface("Segoe UI"), 
                fontSize, 
                Brushes.White);

            var center = new Point(Bounds.Width / 2, Bounds.Height / 2);
            var origin = new Point(center.X - formattedText.Width / 2, 
                150 + ( i * 150));

            context.DrawText(formattedText, origin);
        }
        

    }
}