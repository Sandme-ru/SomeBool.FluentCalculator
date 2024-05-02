namespace StatusK.TestTask.SomeBool;

public class Program
{
    private static bool _value;
    private static bool SomeBool
    {
        get
        {
            _value = !_value;
            return _value;
        }
    }

    public static void Main()
    {
        Console.WriteLine(SomeBool == true && SomeBool == false);
    }
}