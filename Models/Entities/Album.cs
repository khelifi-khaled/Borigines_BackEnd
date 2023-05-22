

namespace Borigines.Models.Entities
{
#nullable disable 

    public class Album
    {
        /// <summary>
        /// ctor for my new album, Date = DateTime.Now
        /// </summary>
        public Album()
        {
            Date = DateTime.Now;
        }


        /// <summary>
        /// ctor for album from DB 
        /// </summary>
        /// <param name="id">id of album</param>
        /// <param name="titel">album titel</param>
        /// <param name="date">titel date </param>
        /// <param name="userAlbum">user album </param>
        public Album(int id, string titel, DateTime date, User userAlbum)
        {
            Id = id;
            Titel = titel;
            Date = date;
            UserAlbum = userAlbum;
        }



        public int Id { get; set; }

        public string Titel { get; set; }

        public DateTime Date { get; set; }

        public User UserAlbum { get; set; }


    }//end class
}//end name space 
