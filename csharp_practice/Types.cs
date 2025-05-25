using System.Collections.ObjectModel;

namespace csharp_practice
{
    struct User
    {
        public static event Action<int>? UpdateEvent;

        public int id = 0;
        private string email = "";
        public string Email
        {
            get { return email; }
            set
            {
                email = value.Substring(0, 255);
            }
        }

        public User()
        {
        }

        public User(int id, string email)
        {
            this.id = id;
            Email = email;
        }

        public void SetEmail(string email)
        {
            Email = email;
            UpdateEvent?.Invoke(id);
        }
    }



    class Picture
    {
        public int id;
        public string name;
        public string location;

        public Picture()
        {
            id = 0;
            name = "";
            location = "";
        }

        public Picture(int id, string name, string loc)
        {
            this.id = id; // 'this' necessary because name unambiguous
            this.name = name;
            location = loc;
        }

        public override string ToString()
        {
            return "This is a picture called " + name + ", taken in " + location + ", ID " + id;
        }
    }



    class Animal
    {
        public int legs
        {
            get;
            protected set;
        }
        public string name
        {
            get;
            set;
        }

        public Animal()
        {
            legs = 0;
            name = "";
        }

        public Animal(string name)
        {
            legs = 0;
            this.name = name;
        }

        public string GetSound()
        {
            return "Sound";
        }
    }



    class Dog : Animal
    {
        public Dog(string name)
        {
            legs = 4;
            this.name = name;
        }
    }



    class Bird : Animal
    {
        public Bird(string name)
        {
            legs = 2;
            this.name = name;
        }
    }



    class UserUI
    {
        public string identifier_for_debug;

        public UserUI(string identifier_for_debug)
        {
            this.identifier_for_debug = identifier_for_debug;
        }

        public void RefreshUI(int id)
        {
            //
        }
    }



    class PictureCollection : KeyedCollection<int, Picture>
    {
        protected override int GetKeyForItem(Picture item)
        {
            return item.id;
        }
    }



    class ThreadGenerateUsers
    {
        protected int count = 10000000;
        public List<User> users;

        public ThreadGenerateUsers()
        {
            users = new List<User>(count);
        }

        public void GenerateManyManyUsers()
        {
            for (int i = 0; i < count; i++)
            {
                users.Add(new User(i + 1, App.RandStr(16)));
            }
        }
    }



    enum HttpStatus
    {
        OK = 200,
        BadRequest = 400,
        Unauthorized = 401,
        Forbidden = 403,
        FoundNot = 404,
        ServerError = 500
    }
}
