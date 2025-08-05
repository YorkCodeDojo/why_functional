using System;
using Avalonia.Controls;
using Avalonia.Input;

namespace Intro;

public partial class MainWindow : Window
{
    private readonly ISlide[] _slides =
    [
        //http://avalonedit.net/documentation/
        new CodeSlide(["Higher Order Functions"]),
        new ImageSlide("Images/logo.jpg", 1200),
        new ListSlide(["Networking and Food", "Explain Problem", "Work In Pairs", "Present", "Pub"]),
        new ImageSlide("Images/ethos.jpg"),
        new UrlSlide("https://github.com/YorkCodeDojo/why_functional", 80),
    ];

    private int _slideNumber = 0;
   
    public MainWindow()
    {
        InitializeComponent();
        this.WindowState = WindowState.Maximized;
        Switcher.Content = _slides[_slideNumber];
        _slides[_slideNumber].Display(true);
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
        if (e.Key == Key.P)
        {
            // P for previous slide
            _slideNumber = Math.Max(0, _slideNumber - 1);
            var previousPage = _slides[_slideNumber];
            Switcher.Content = previousPage;
            previousPage.Display(true);
        }
        
        else if (e.Key == Key.Space)
        {
            // Space bar to either build this slide, or advance to the following slide
            if (_slides[_slideNumber].Display(false) == DisplayResult.Completed)
            {
                // Page is complete, display the next page
                _slideNumber = (_slideNumber + 1) % _slides.Length;
                var nextSlide = _slides[_slideNumber];
                Switcher.Content = nextSlide;
                nextSlide.Display(true);
            }
        }
    }
}