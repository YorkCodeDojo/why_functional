namespace Intro;

public enum DisplayResult
{
    Completed,
    MoreToDisplay
}

public interface ISlide
{
    /// <summary>
    /// Returns Completed if the completed page is already displayed, and the next page needs to be displayed.
    /// </summary>
    /// <returns></returns>
    DisplayResult Display(bool reset);
}