namespace StatusK.TestTask.FluentCalculator;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            var calc = new Models.FluentCalculators.FluentCalculator();

            var result1 = calc.One.Plus.Two * 10;
            Console.WriteLine(result1);

            var result2 = calc.Zero.DividedBy.Ten.Plus.Two.Times.Nine.Minus.One.Plus.One / 3 / 3;
            Console.WriteLine(result2);

            var result3 = calc.Two.Plus.Ten.Plus;
            Console.WriteLine(result3);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}