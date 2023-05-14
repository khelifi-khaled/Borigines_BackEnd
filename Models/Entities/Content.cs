﻿
namespace Borigines.Provider.Sql.Models
{
#nullable disable 

    public class Content
    {
        /// <summary>
        /// ctor for new content
        /// </summary>
        public Content()
        {
           
        }


        /// <summary>
        /// ctor for Content from db
        /// </summary>
        /// <param name="id">content Id </param>
        /// <param name="titel">content titel </param>
        /// <param name="text">the text of the content </param>
        public Content(int id, string titel, string text)
        {
            Id = id;
            Titel = titel;
            Text = text;
        }


        public int Id { get; set; }

        public string Titel { get; set; }

        public string Text { get; set; }


    }//end class
}//end name space 
