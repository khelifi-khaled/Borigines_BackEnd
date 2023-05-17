namespace BiriginesAPI.DTO
{
#nullable disable 

    public class GetArticlesByCategoryDTO
    {
        public GetArticlesByCategoryDTO(int id, int idUser, string userLastName, string userFirstName, DateTime date, int idCategory, string title, string content)
        {
            Id = id;
            IdUser = idUser;
            UserLastName = userLastName;
            UserFirstName = userFirstName;
            Date = date;
            IdCategory = idCategory;
            Title = title;
            Content = content;
        }

        public int Id { get; set; }

        public int IdUser { get; set; }

        public string UserLastName { get; set; }

        public string UserFirstName { get; set; }

        public DateTime Date { get; set; }

        public int IdCategory { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }
    }
}
