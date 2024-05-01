using System;

public class Program
{
    public class MyBool(bool initialValue)
    {
        private bool value = initialValue;

        public static implicit operator bool(MyBool myBool)
        {
            return myBool.value;
        }

        public static bool operator ==(MyBool myBool, bool compareBool)
        {
            return true; // Всегда возвращаем true
        }

        public static bool operator !=(MyBool myBool, bool compareBool)
        {
            return true; // Всегда возвращаем true
        }

        public static bool operator ==(bool compareBool, MyBool myBool)
        {
            return true; // Всегда возвращаем true
        }

        public static bool operator !=(bool compareBool, MyBool myBool)
        {
            return true; // Всегда возвращаем true
        }
    }

    public static void Main()
    {
        MyBool someBool = new MyBool(true);
        Console.WriteLine(someBool == true && someBool == false); // Output: True
    }
}