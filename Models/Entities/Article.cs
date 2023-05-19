
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
             Content = new();

        }

        /// <summary>
        /// ctor for Article from Db with Id and language 
        /// </summary>
        /// <param name="id">Article id</param>
        /// <param name="categoryArticle">category Article : i have name and id inside </param>
        /// <param name="date">date of Article </param>
        /// <param name="user">user article </param>
        /// <param name="pictures">list of pictures </param>
        public Article(int id, Category categoryArticle, Content content , DateTime date, User user)
        {
            Id = id;
            CategoryArticle = categoryArticle;
            Date = date;
            User = user;
            Content = content;

        }

        /// <summary>
        /// ctor for Article from Db with Id so i need to send the 3 Article content to my client (Edite) 
        /// </summary>
        /// <param name="id">Article id</param>
        /// <param name="categoryArticle">category Article : i have name and id inside </param>
        /// <param name="date">date of Article </param>
        /// <param name="content"> fr content  </param>
        /// <param name="contentEn"> En content  </param>
        /// <param name="contentNl"> Nl content  </param>
        /// <param name="user">user article </param>
        /// <param name="pictures">list of pictures </param>
        public Article(int id, Category categoryArticle, Content content , Content contentEn , Content contentNl, DateTime date, User user)
        {
            Id = id;
            CategoryArticle = categoryArticle;
            Date = date;
            User = user;
            Content = content;
            ContentEn= contentEn;
            ContentNl= contentNl;

        }

        public int Id { get; set; }

        public Category CategoryArticle { get; set; }

        public DateTime Date { get; set; }

        public User User { get; set; }

        /// <summary>
        /// if the client get the article with id and laguage, this prop will be in En or Fr or Nl , else this prop will be the Fr content  
        /// </summary>
        public Content Content { get; set; }

        public Content ContentEn { get; set; }

        public Content ContentNl { get; set; }

    }//end class 
}//end name space 
