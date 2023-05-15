
namespace Borigines.Models.Entities
{
#nullable disable 

    public class Article
    {
        /// <summary>
        /// ctor for new Article 
        /// </summary>
        public Article()
        {
            ContentFR = new();
            ContentEN = new();
            ContentNL = new();
        }

        /// <summary>
        /// ctor for Article from Db 
        /// </summary>
        /// <param name="id">Article id</param>
        /// <param name="categoryArticle">category Article : i have name and id inside </param>
        /// <param name="date">date of Article </param>
        /// <param name="contents">List of content : always 3 Fr + En + Nl from Db</param>
        /// <param name="user">user article </param>
        /// <param name="picturs">list of </param>
        public Article(int id, Category categoryArticle, Content contentFr , Content contentEn, Content contentNl, DateTime date, User user)
        {
            Id = id;
            CategoryArticle = categoryArticle;
            Date = date;
            User = user;
            ContentFR = contentFr;
            ContentEN = contentEn;
            ContentNL = contentNl;
        }

        public int Id { get; set; }

        public Category CategoryArticle { get; set; }

        public DateTime Date { get; set; }

        public User User { get; set; }

        public Content ContentFR { get; set; }

        public Content ContentEN { get; set; }

        public Content ContentNL { get; set; }

      


    }//end class 
}//end name space 
