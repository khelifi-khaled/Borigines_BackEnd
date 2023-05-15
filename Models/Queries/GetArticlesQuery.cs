using Borigines.Models.Entities;
using Models.Mappers;
using System.Data;
using Tools.CQRS.Queries;
using Tools.DataBase;

namespace Models.Queries
{
    public class GetArticlesQuery : IQuery<IEnumerable<Article>>
    {
		public GetArticlesQuery ()
		{

		}

    }//end GetArticlesQuery

    public class GetArticlesQueryHandler : IQueryHandler<GetArticlesQuery,IEnumerable<Article>>
    {
        private readonly IDbConnection _dbConnection;


        public GetArticlesQueryHandler(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;

        }

        public IEnumerable<Article>? Execute(GetArticlesQuery query)
        {
            string sql = @"SELECT	art.id as artId ,
									art.Date_Article as  artDate_Article ,
									art.Fk_category_id as artFk_category_id,
									art.FK_id_user as artFK_id_user,
									art.FK_content_fr as artFK_content_fr,
									art.FK_content_en as artFK_content_en,
									art.FK_content_nl as artFK_content_nl,
									cat.id as catId , 
									cat.Name_Category as catName_Category , 
									fr.id  as frId, 
									fr.Titel as frTitel,
									fr.Content as frContent,
									en.id as enId , 
									en.Titel as enTitel,
									en.Content as enContent,
									nl.id as nlId , 
									nl.Titel as nlTitel,
									nl.Content as nlContent ,
									u.id as uId , 
									u.First_name as uFirst_name , 
									u.Last_name as uLast_name ,
									u.[Login] as uLogin ,
									u.Is_Admin  as uIs_Admin
									FROM Articles art JOIN Users u  
									ON art.FK_id_user = u.Id JOIN Categorys  cat 
									ON  cat.Id = art.Fk_category_id JOIN Content_fr fr 
									ON fr.Id = art.FK_content_fr JOIN Content_en en 
									ON en.Id = art.FK_content_en JOIN Content_nl nl 
									ON nl.id = art.FK_content_nl ;";

            return _dbConnection.ExecuteReader(sql, dr => dr.ToArticle());

        }//end Execute
    }


}//end name space 
