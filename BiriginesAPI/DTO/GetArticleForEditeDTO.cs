using Borigines.Models.Entities;

namespace BiriginesAPI.DTO
{
#nullable disable 

    public class GetArticleForEditeDTO
    {
        public GetArticleForEditeDTO(int id, int idUser, string userLastName, string userFirstName, DateTime date, int idCategory, string titleFr, string contentFr, string titleEn, string contentEn, string titleNl, string contentNl, IEnumerable<Picture> pictures)
        {
            Id = id;
            IdUser = idUser;
            UserLastName = userLastName;
            UserFirstName = userFirstName;
            Date = date;
            IdCategory = idCategory;
            TitleFr = titleFr;
            ContentFr = contentFr;
            TitleEn = titleEn;
            ContentEn = contentEn;
            TitleNl = titleNl;
            ContentNl = contentNl;
            Pictures = pictures;
        }

        public int Id { get; init; }

        public int IdUser { get; init; }

        public string UserLastName { get; init; }

        public string UserFirstName { get; init; }

        public DateTime Date { get; init; }

        public int IdCategory { get; init; }

        public string TitleFr { get; init; }

        public string ContentFr { get; init; }

        public string TitleEn { get; init; }

        public string ContentEn { get; init; }

        public string TitleNl { get; init; }

        public string ContentNl { get; init; }

        public IEnumerable<Picture> Pictures { get; init; }

    }//end class 
}//end  name space 
