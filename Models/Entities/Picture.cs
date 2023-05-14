using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borigines.Provider.Sql.Models
{
#nullable disable
    public class Picture
    {

        public Picture() {}

        public Picture(int id, string name_picture)
        {
            Id = id;
            Name_picture = name_picture;
        }

        public int  Id { get; set; }

        public string Name_picture { get; set; }













    }//end class
}//end name space 
