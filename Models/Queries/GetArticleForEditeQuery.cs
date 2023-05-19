using Borigines.Models.Entities ;
using Models.Mappers;
using System.Data;
using Tools.CQRS.Queries ;
using Tools.DataBase;

namespace Models.Queries
{
    public class GetArticleForEditeQuery : IQuery<Article>
    {
        public GetArticleForEditeQuery(int id)
        {
            Id = id;
        }

        public int Id { get; init; }


    }//end GetArticleForEditeQuery

    public class GetArticleForEditeQueryHandler : IQueryHandler<GetArticleForEditeQuery, Article>
    {
        private readonly IDbConnection _dbConnection;

        public GetArticleForEditeQueryHandler(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection ;
        }

        public Article? Execute(GetArticleForEditeQuery query)
        {
            string sql = @"SELECT	art.id as artId ,
									art.Date_Article as  artDate_Article ,
									art.Fk_category_id as artFk_category_id,
									art.FK_id_user as artFK_id_user,
									art.FK_content_fr as artFK_content_fr ,
									cat.id as catId , 
									cat.Name_Category as catName_Category , 

									fr.id  as IdFr, 
									fr.Title as TitleFr,
									fr.Content as TextFr,

									nl.id  as IdNl, 
									nl.Title as TitleNl,
									nl.Content as TextNl,
									
									en.id  as IdEn, 
									en.Title as TitleEn,
									en.Content as TextEn,

									u.id as uId , 
									u.First_name as uFirst_name , 
									u.Last_name as uLast_name ,
									u.[Login] as uLogin ,
									u.Is_Admin  as uIs_Admin

									FROM Articles art JOIN Users u  
									ON art.FK_id_user = u.Id JOIN Categorys  cat 
									ON  cat.Id = art.Fk_category_id JOIN Content_fr fr 
									ON fr.Id = art.FK_content_fr  JOIN Content_nl nl 
									ON nl.Id = art.FK_content_nl JOIN Content_en en 
									ON en.Id =  art.FK_content_nl 
									WHERE art.id  = @Id";

			return _dbConnection.ExecuteReader(sql, dr => dr.ToFullArticle() , parameters : query ).SingleOrDefault();

        }//end Execute

    }//end GetArticleForEditeQueryHandler

}//end name space 
