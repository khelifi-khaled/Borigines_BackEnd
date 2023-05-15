
namespace Borigines.Models.Entities { 

#nullable disable

    public class User
    {
        // empty ctor 
        public User(){ }

        public User(int id, string first_name, string last_name, string login, bool isAdmin)
        {
            Id = id;
            First_name = first_name;
            Last_name = last_name;
            Login = login;
            IsAdmin = isAdmin;
        }



        public int Id { get; init; }

        public string First_name { get; init; }

        public string Last_name { get; init; }

        public string Login { get; init; }

        public bool IsAdmin { get; init; }
        

    }//end class
}//end name space 
