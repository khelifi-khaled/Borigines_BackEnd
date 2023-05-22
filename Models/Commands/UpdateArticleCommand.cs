using System.Data;
using Tools.CQRS.Commands;
using Tools.DataBase;

namespace Models.Commands
{
    public class UpdateArticleCommand : ICommand 
    {
        public UpdateArticleCommand(int articleId, int categoryId, string titelFr, string titelEn, string titelNl, string contentFr, string contentEn, string contentNl)
        {
            ArticleId = articleId;
            CategoryId = categoryId;
            TitelFr = titelFr;
            TitelEn = titelEn;
            TitelNl = titelNl;
            ContentFr = contentFr;
            ContentEn = contentEn;
            ContentNl = contentNl;
        }

        public int ArticleId { get; init; }

        public int CategoryId { get; init; }

        public string TitelFr { get; init; }
        
        public string TitelEn { get; init; }

        public string TitelNl { get; init; }

        public string ContentFr { get; init; }

        public string ContentEn { get; init; }

        public string ContentNl { get; init; }


    }//end UpdateArticleCommand

    public class UpdateArticleCommandHandler : ICommandHandler<UpdateArticleCommand>
    {
        private readonly IDbConnection _dbConnection;

        public UpdateArticleCommandHandler(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public IResult Execute(UpdateArticleCommand command)
        {
            try
            {
                string sql = @"UPDATE Content_fr  
                              SET Content_fr.Title = @TitelFr , 
                                  Content_fr.Content = @ContentFr 
                              FROM Content_fr JOIN Articles 
                              ON Articles.FK_content_fr = Content_fr.id 
                              WHERE Articles.Id = @ArticleId ; 
                              
                              UPDATE Content_en  
                              SET Content_en.Title = @TitelEn , 
                                  Content_en.Content = @ContentEn 
                              FROM Content_en JOIN Articles 
                              ON Articles.FK_content_en = Content_en.id 
                              WHERE Articles.Id = @ArticleId ;
                               
                              UPDATE Content_nl  
                              SET Content_nl.Title = @TitelNl , 
                                  Content_nl.Content = @ContentNl 
                              FROM Content_nl JOIN Articles 
                              ON Articles.FK_content_nl = Content_nl.id 
                              WHERE Articles.Id = @ArticleId 
                              
                              UPDATE Articles 
                              SET Fk_category_id = @CategoryId 
                              WHERE Id = @ArticleId; ";
                _dbConnection.ExecuteNonQuery(sql, parameters: new 
                    { 
                       command.TitelFr,
                       command.ContentFr,

                       command.TitelNl,
                       command.ContentNl,

                       command.TitelEn,
                       command.ContentEn,
                       
                       command.ArticleId,
                       command.CategoryId,
                      
                       
                    });
                return Result.Success();
            }
            catch (Exception ex )
            {
                return Result.Failure(ex.Message);

            }//end try catch 

        }//end Execute

    }//end class  UpdateArticleCommandHandler 

}//end name space 
