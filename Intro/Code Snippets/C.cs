namespace Intro.Code_Snippets;

public class C
{
    int Add3(int x) => x + 3;

    int Double(int x) => x * 2;

    int AddAndDouble(int x) => Double(Add3(x));
    
    public void Example()
    {
    }
}