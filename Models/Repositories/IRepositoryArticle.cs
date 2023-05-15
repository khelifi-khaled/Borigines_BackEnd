
using Borigines.Models.Entities;

namespace Models.Repositories
{
    public interface IRepositoryArticle
    {
        void GetArticlePictures(IEnumerable<Article> articles);

    }//end interface
}//end name space 
