﻿using Borigines.Models.Entities;
using Models.Mappers;
using System.Data;
using Tools.CQRS.Queries;
using Tools.DataBase;

namespace Models.Queries
{
    public class GetArticlesByCategoryQuery : IQuery<IEnumerable<Article>>
    {
        public GetArticlesByCategoryQuery(int categoryId, string language)
        {
            CategoryId = categoryId;
            Language = language.ToUpper();
        }

        public int CategoryId { get; init; }

        public string Language { get; init; }


	}//end GetArticlesQuery

    public class GetArticlesByCategoryQueryHandler : IQueryHandler<GetArticlesByCategoryQuery, IEnumerable<Article>>
    {
        private readonly IDbConnection _dbConnection;


        public GetArticlesByCategoryQueryHandler(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;

        }

        public IEnumerable<Article>? Execute(GetArticlesByCategoryQuery query)
        {
            string sql = query.Language switch
            {
                "FR" => @"SELECT	art.id as artId ,
									art.Date_Article as  artDate_Article ,
									art.Fk_category_id as artFk_category_id,
									art.FK_id_user as artFK_id_user,
									art.FK_content_fr as artFK_content_fr ,
									cat.id as catId , 
									cat.Name_Category as catName_Category , 
									fr.id  as Id, 
									fr.Title as Title,
									fr.Title as Title,
									fr.Content as Text,
									
									u.id as uId , 
									u.First_name as uFirst_name , 
									u.Last_name as uLast_name ,
									u.[Login] as uLogin ,
									u.Is_Admin  as uIs_Admin

									FROM Articles art JOIN Users u  
									ON art.FK_id_user = u.Id JOIN Categorys  cat 
									ON  cat.Id = art.Fk_category_id JOIN Content_fr fr 
									ON fr.Id = art.FK_content_fr  WHERE cat.id  = @CategoryId ;",
                "NL" => @"SELECT	art.id as artId ,
									art.Date_Article as  artDate_Article ,
									art.Fk_category_id as artFk_category_id,
									art.FK_id_user as artFK_id_user,
									art.FK_content_fr as artFK_content_fr ,
									cat.id as catId , 
									cat.Name_Category as catName_Category , 
									nl.id  as Id, 
									nl.Title as Title,
									nl.Content as Text,
									
									u.id as uId , 
									u.First_name as uFirst_name , 
									u.Last_name as uLast_name ,
									u.[Login] as uLogin ,
									u.Is_Admin  as uIs_Admin

									FROM Articles art JOIN Users u  
									ON art.FK_id_user = u.Id JOIN Categorys  cat 
									ON  cat.Id = art.Fk_category_id JOIN Content_nl nl 
									ON nl.Id = art.FK_content_fr  WHERE cat.id  = @CategoryId ;",
                _ => @"SELECT	art.id as artId ,
									art.Date_Article as  artDate_Article ,
									art.Fk_category_id as artFk_category_id,
									art.FK_id_user as artFK_id_user,
									art.FK_content_fr as artFK_content_fr ,
									cat.id as catId , 
									cat.Name_Category as catName_Category , 
									en.id  as Id, 
									en.Title as Title,
									en.Content as Text,
									
									u.id as uId , 
									u.First_name as uFirst_name , 
									u.Last_name as uLast_name ,
									u.[Login] as uLogin ,
									u.Is_Admin  as uIs_Admin

									FROM Articles art JOIN Users u  
									ON art.FK_id_user = u.Id JOIN Categorys  cat 
									ON  cat.Id = art.Fk_category_id JOIN Content_en en 
									ON en.Id = art.FK_content_fr  WHERE cat.id  = @CategoryId ;",
            };
            return _dbConnection.ExecuteReader(sql, dr => dr.ToArticle(),parameters : new { query.CategoryId });

        }//end Execute
    }


}//end name space 
