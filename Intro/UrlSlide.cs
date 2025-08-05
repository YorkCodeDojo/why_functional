using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.Media;
namespace Intro;

public class UrlSlide(string url, int fontSize = 24) : Control, ISlide
{
    private Grid? _layout;

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);

        if (_layout != null)
            return;

        _layout = new Grid();

        var textBlock = new TextBlock
        {
            Text = url,
            Foreground = Brushes.Blue,
            TextDecorations = TextDecorations.Underline,
            Cursor = new Cursor(StandardCursorType.Hand),
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            FontSize = fontSize
        };

        textBlock.PointerPressed += OpenUrl;

        _layout.Children.Add(textBlock);
        LogicalChildren.Add(_layout);
        VisualChildren.Add(_layout);
    }
    
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

        // var formattedText = new FormattedText(
        //     textToDisplay, 
        //     CultureInfo.CurrentUICulture,
        //     FlowDirection.LeftToRight, 
        //     new Typeface("Segoe UI"), 
        //     fontSize, 
        //     Brushes.White);
        //
        // var center = new Point(Bounds.Width / 2, Bounds.Height / 2);
        // var origin = new Point(center.X - formattedText.Width / 2, 
        //                        center.Y - formattedText.Height / 2);
        //
        // context.DrawText(formattedText, origin);
    }
    
    private void OpenUrl(object? sender, PointerPressedEventArgs args)
    {
        try
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                Process.Start("open", url);
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                Process.Start("xdg-open", url);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Failed to open URL: {ex.Message}");
        }
    }
}