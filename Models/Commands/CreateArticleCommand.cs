using System.Data;
using Tools.CQRS.Commands;
using Tools.DataBase;

namespace Models.Commands
{
    public class CreateArticleCommand : ICommand
    {
        public CreateArticleCommand(int categoryId, int userId, string titelFr, string titelEn, string titelNl, string contentFr, string contentEn, string contentNl)
        {
            Date_Article = DateTime.Now;
            CategoryId = categoryId;
            UserId = userId;
            TitelFr = titelFr;
            TitelEn = titelEn;
            TitelNl = titelNl;
            ContentFr = contentFr;
            ContentEn = contentEn;
            ContentNl = contentNl;
        }

        public DateTime Date_Article { get; init; }

        public int CategoryId { get; init; }

        public int UserId { get; init; }

        public string TitelFr { get; init; }

        public string TitelEn { get; init; }

        public string TitelNl { get; init; }

        public string ContentFr { get; init; }

        public string ContentEn { get; init; }

        public string ContentNl { get; init; }


    }//end CreateArticleCommand

    public class CreateArticleCommandHandler : ICommandHandler<CreateArticleCommand>
    {
        private readonly IDbConnection _dbconnection;

        public CreateArticleCommandHandler(IDbConnection dbconnection)
        {
            _dbconnection = dbconnection;
        }

        public IResult Execute(CreateArticleCommand command)
        {
            try
            {
                string sql = @"INSERT INTO Content_fr  (Title, Content)  OUTPUT INSERTED.Id
                                                VALUES (@TitelFr , @ContentFr)";
                int? IdContentFr = _dbconnection.ExecuteScalar(sql, parameters: new
                {
                    command.TitelFr,
                    command.ContentFr,
                }) as int?;

                sql = @"INSERT INTO Content_nl  (Title, Content) OUTPUT INSERTED.Id
                                         VALUES (@TitelNl , @ContentNl)";
                int? IdContentNl = _dbconnection.ExecuteScalar(sql, parameters: new
                {
                    command.TitelNl,
                    command.ContentNl,
                }) as int?;

                sql = @"INSERT INTO Content_en (Title, Content) OUTPUT INSERTED.Id
                                        VALUES (@TitelEn , @ContentEn)";
                int? IdContentEn = _dbconnection.ExecuteScalar(sql, parameters: new
                {
                    command.TitelEn,
                    command.ContentEn,
                }) as int?;

                sql = @"INSERT INTO Articles
                        (Date_Article,FK_id_user,FK_content_fr,FK_content_en,FK_content_nl,Fk_category_id) 
                        OUTPUT INSERTED.Id 
                 VALUES (@Date_Article,@FK_id_user,@FK_content_fr,@FK_content_en,@FK_content_nl,@Fk_category_id)";

                int? IdArticle = _dbconnection.ExecuteScalar(sql, parameters: new
                {
                    command.Date_Article,
                    FK_id_user = command.UserId,
                    FK_content_fr = IdContentFr,
                    FK_content_en = IdContentEn,
                    FK_content_nl = IdContentNl,
                    Fk_category_id = command.CategoryId
                }) as int?;

                return Result.Success(IdArticle.ToString());
            }
            catch (Exception ex)
            {
               return Result.Failure(ex.Message);
            }

           
        }//end Execute


    }//end CreateArticleCommandHandler
}//end name space 
