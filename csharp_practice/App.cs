#pragma warning disable CS0219

using System.Collections;

namespace csharp_practice
{
    class App
    {
        static Random rand = new Random();

        static async Task Main(string[] args)
        {
            Practice.Run();

            Task async = AsyncStuff();
            NonAsyncStuff();
            await async;
            Console.WriteLine("End of main");
        }





        static void NonAsyncStuff()
        {
            // Primitives
            {
                bool v_bool = true;
                int v_int = 5;
                Int32 v_int32 = 5;
                long v_long = 500000000;
                float v_float = 5.0505f; // 6-9 digits; 4 bytes
                double v_double = 5.0505; // No suffix, d, D; 15-17 digits; 8 bytes
                decimal v_decimal = 5.0505m; // 28-29 digits; 16 bytes
                char v_char = 'a';
                string v_str = "Milos"; // 2 bytes per char
                v_str = "Paunovic";
                string? v_nullable_str = null;
                v_nullable_str = "Milos";
            }


            // Non primitives
            {
                // Tuples
                (int id, string email, int) v_tuple = (1, "milos@example.test", 55);
                //v_tuple.id;
                //v_tuple.Item3;

                // Structs
                User user = new User();
                //user.id;

                // Classes
                Picture pic = new Picture();
                Picture pic2 = pic;
                pic.location = "Copenhagen";
                pic2.location = "Edinburgh";
                SetPictureLocation(pic, "Paris"); // Will be Paris because you're passing a pointer
                Picture? pic_nullable = null;

                // Properties
                Bird bird = new Bird("Mockingbird");
                //bird.legs = 200; // Not allowed because setter is protected, even though the property is public
            }


            // Arrays
            {
                // Static arrays
                int[] arr = new int[5];
                int[] arr_initialized = [5, 10, 15, 20, 25];
                int[,] arr_2d = new int[2, 5]; // 2x5 matrix
                int[,] arr_2d_initialized =
                {
                    { 5, 10, 15, 20, 25 },
                    { 50, 100, 150, 200, 250 }
                };

                // Dynamic arrays
                List<int> v_list = new List<int>();
                v_list.Add(5);
                v_list.Add(10);
                v_list.Add(15);
                var success = v_list.Remove(10); // Remove value; var is auto from c++
                v_list.RemoveAt(v_list.Count - 1); // Remove last index

                List<Picture> pictures = new List<Picture> {
                    new Picture(1, "Cat", "Hometown"),
                    new Picture(2, "Dog", "Hometown")
                };

                SetLocation(pictures); /* Works as expected, however, mem addresses of 'pictures'
                and 'pics' are different, so the pointer is copied just like in c++ */
                var pic = pictures[0];
                pic.location = "Moscow";
            }


            // Maps
            {
                // Dictionary
                Dictionary<int, Picture> map_pictures = new Dictionary<int, Picture>();
                var pic = new Picture(1, "Cat", "Hometown");
                map_pictures.Add(pic.id, pic); // Can throw
                //map_pictures[temp.id].location;
                map_pictures.ContainsKey(1);
                Picture? pic_out;
                var success = map_pictures.TryGetValue(1, out pic_out);

                // Hashtable
                Hashtable foods = new Hashtable();
                foods.Add(1, "Bananas");
                foods.Add("two", "Pizza");
                if (foods.ContainsKey("two"))
                {
                    var food = foods["two"];
                }

                // KeyedCollection
                PictureCollection pics_keyed = new PictureCollection
                {
                    new Picture(1, "Cat", "Hometown"),
                    new Picture(2, "Dog", "Hometown")
                };
                pics_keyed.Add(new Picture(3, "Pretty church", "Edinburgh"));
                //pics_keyed[3];
            }

            // Reflection
            {
                string v_str = "Milos";
                if (v_str.GetType() == typeof(string))
                {
                    // Do something
                }
                Animal animal = new Bird("Mockingbird");
                var animal_type = animal.GetType();
                System.Reflection.MethodInfo? method = animal_type.GetMethod("GetSound");
                var sound = method?.Invoke(animal, []);
            }


            // Delegates, events
            {
                // Both UIs will refresh when the event is called
                User user = new User(1, "milos@example.test");
                UserUI ui1 = new UserUI("First UI");
                UserUI ui2 = new UserUI("Second UI");
                User.UpdateEvent += ui1.RefreshUI;
                User.UpdateEvent += ui2.RefreshUI;
                user.Email = "milooos@example.test";
                //User.updated_event = null;        // Cannot be done from outside the class
            }


            // in and out keywords
            {
                var pic = new Picture(1, "Camel", "Egypt");
                CopyPicture(pic.id, pic.name, pic.location, out Picture pic_out);
            }



            //Multithreading();


            // Lambdas
            {
                Func<int, int> square = x => x * x;
                var val = square(5);
            }

            // Extensions
            {
                string v_str = "Milos";
                v_str = v_str.AddMonkeys(10);
            }

            // Datetime
            {
                DateTime time = DateTime.UtcNow;
                DateTime time2 = new DateTime(2025, 5, 1, 21, 30, 15);
                TimeSpan diff = time - time2;

                DateTime time3 = DateTime.UtcNow.AddSeconds(-1.0);
                bool result = time > time3;
                time3 = new DateTime(time.Ticks);
                result = time == time3;
            }
        }

        static async Task AsyncStuff()
        {
            await Task.Delay(3000);
            Console.WriteLine("Long task done");
        }

        static void SetPictureLocation(Picture pic, string val)
        {
            pic.location = val;
        }

        static void SetLocation(List<Picture> pics)
        {
            for (int i = 0; i < pics.Count; i++)
            {
                pics[i].location = "Moon";
            }
        }

        public static string RandStr(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            string result = "";

            for (int i = 0; i < length; i++)
            {
                result += chars[rand.Next(0, chars.Length)];
            }

            return result;
        }

        static void CopyPicture(in int pic_id, in string name, in string location, out Picture pic_out)
        {
            //pic_id = 55;      // Not allowed because of in
            pic_out = new Picture(rand.Next(55555), name, location);
        }

        static void Multithreading()
        {
            ThreadGenerateUsers tgu = new();
            Thread? t = new Thread(new ThreadStart(tgu.GenerateManyManyUsers));
            t.Start();
            Console.WriteLine("Thread started");

            bool app_running = true;
            int frames = 0;
            // Main loop
            while (app_running)
            {
                if (t != null && t.IsAlive == false)
                {
                    Console.WriteLine("Thread dead");
                    t = null;
                    var first_user = tgu.users[0];
                    /* Tried to clear memory here but to no avail
                     * 
                    tgu.users.Clear();
                    tgu.users.Capacity = 0;
                    tgu = null;
                    GC.Collect();
                    GC.WaitForPendingFinalizers(); */
                }
                Thread.Sleep(8);
                frames++;
                if (frames == 1000)
                {
                    app_running = false;
                }
            }
        }
    }

    static class StringExtensions
    {
        public static string AddMonkeys(this string str, int count)
        {
            string result = str;
            for (int i = 0; i < count; i++)
            {
                result += '@';
            }
            return result;
        }
    }
}
