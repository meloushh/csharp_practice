using System.Collections.Generic;

namespace csharp_practice
{
    class Practice
    {
        public static event Action<int>? UserUpdated;

        public static void Run()
        {
            var func1 = (int a) =>
            {
                Console.WriteLine("Func 1");
            };

            var func2 = (int a) =>
            {
                Console.WriteLine("Func 2");
            };

            UserUpdated += func1;
            UserUpdated += func2;

            UserUpdated?.Invoke(5);
            int a = 6;
            int b;
            CopyInt(a, out b);
        }

        static void CopyInt(in int a, out int b)
        {
            //a = 10;       // Not allowed
            b = a;
        }
    }
}
