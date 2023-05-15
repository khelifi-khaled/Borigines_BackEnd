

namespace Borigines.Models.Entities
{

#nullable disable

    public class Category
    {
        /// <summary>
        /// ctor new Cat
        /// </summary>
        public Category()
        {

        }

        /// <summary>
        /// ctor cat from db
        /// </summary>
        /// <param name="id">Cat Id</param>
        /// <param name="name_Category">Cat name</param>
        public Category(int id, string name_Category)
        {
            Id = id;
            Name_Category = name_Category;
        }

        public int  Id { get; set; }

        public string Name_Category { get; set; }



    }//end class
}//end name space 
