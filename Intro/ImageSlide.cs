using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Imaging;

namespace Intro;

public class ImageSlide(string filename, int? width = null, int? height = null): Control, ISlide
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

        var image = new Bitmap(filename);
        context.DrawImage(image, new Rect(
            (Bounds.Width - (width ?? image.Size.Width)) / 2,
            (Bounds.Height - (height ??  image.Size.Height)) / 2,
            width ?? image.Size.Width,
            height ?? image.Size.Height) );
    }
}