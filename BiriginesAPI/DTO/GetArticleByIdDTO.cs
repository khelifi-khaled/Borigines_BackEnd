using Borigines.Models.Entities;

namespace BiriginesAPI.DTO
{
#nullable disable 

    public class GetArticleByIdDTO
    {
        public GetArticleByIdDTO(int id, int idUser, string userLastName, string userFirstName, DateTime date, int idCategory, string title, string content , IEnumerable<Picture> pictures)
        {
            Id = id;
            IdUser = idUser;
            UserLastName = userLastName;
            UserFirstName = userFirstName;
            Date = date;
            IdCategory = idCategory;
            Title = title;
            Content = content;
            Pictures = pictures;
        }

        public int Id { get; init; }

        public int IdUser { get; init; }

        public string UserLastName { get; init; }

        public string UserFirstName { get; init; }

        public DateTime Date { get; init; }

        public int IdCategory { get; init; }

        public string Title { get; init; }

        public string Content { get; init; }

        public IEnumerable<Picture> Pictures { get; init; }


    }//end class 
}//end name space 
