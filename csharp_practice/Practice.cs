namespace csharp_practice
{
    class Practice
    {
        public static event Action<int>? UserUpdated;

        public static void Run()
        {
            //
        }

        static void CopyInt(in int a, out int b)
        {
            //a = 10;       // Not allowed
            b = a;
        }
    }
}
