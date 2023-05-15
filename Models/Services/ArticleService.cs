using Borigines.Models.Entities;
using Microsoft.AspNetCore.Http;
using Models.Repositories;
using System.Data;
using System.Data.SqlClient;

namespace Models.Services
{
    public class ArticleService : IRepositoryArticle
    {
        private readonly IDbConnection _dbConnection ;
        private readonly IHttpContextAccessor _contex;

        public ArticleService (IDbConnection dbConnection, IHttpContextAccessor contex)
        {
            _dbConnection = dbConnection;
            _contex = contex;
        }

        public void GetArticlePictures(IEnumerable<Article> articles)
        {
            string sql = @"SELECT FK_Article , 
                                  FK_Picture ,
                                  Id , 
                                  Name_picture
                                  FROM Article_Picture JOIN Picture 
                                  ON FK_Picture = Id";

            SqlCommand cmd = new (sql,(SqlConnection)_dbConnection);

  
            // Execuction of my sql query
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                //geting Pic Base Url
                string basePicUrl =
                    _contex.HttpContext!.Request.Scheme + "://" + _contex.HttpContext!.Request.Host.Value + "/Images/" + reader.GetString("Name_picture");
                Picture p = new (reader.GetInt32("Id"), basePicUrl);
                foreach (Article item in articles)
                {
                    //injection of my Pics in Article
                    if (reader.GetInt32("FK_Article") == item.Id)
                    {
                        //item.Pictures = item.Pictures.Append(p);
                    }
                }
            }

        }
    }//end class
}//end namespace 
