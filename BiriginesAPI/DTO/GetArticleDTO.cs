using Borigines.Models.Entities;

namespace BiriginesAPI.DTO
{
    public class GetArticleDTO
    {
        public GetArticleDTO(Article articleInfos, IEnumerable<Picture> pictures)
        {
            ArticleInfos = articleInfos;
            Pictures = pictures;
        }

        public Article ArticleInfos { get; init; }

        public IEnumerable<Picture> Pictures { get; init; }


    }//end class 
}//end name space 
