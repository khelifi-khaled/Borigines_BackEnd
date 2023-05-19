using Borigines.Models.Entities;
using Microsoft.AspNetCore.Http;
using Models.Mappers;
using System.Data;
using Tools.CQRS.Queries;
using Tools.DataBase;

namespace Models.Queries
{
    public class GetArticlePicturesQuery : IQuery<IEnumerable<Picture>>
    {

        public GetArticlePicturesQuery(int id) 
        { 
            Id = id;
        }

        public int Id { get; init; }


    }//end GetArticlePicturesQuery

    public class GetArticlePicturesQueryHandler : IQueryHandler<GetArticlePicturesQuery, IEnumerable<Picture>>
    {
        private readonly IDbConnection _dbConnection;
        private readonly IHttpContextAccessor _contex;

        public GetArticlePicturesQueryHandler(IDbConnection dbConnection, IHttpContextAccessor contex)
        {
            _dbConnection = dbConnection;
            _contex = contex;
        }

        public IEnumerable<Picture>? Execute(GetArticlePicturesQuery query)
        {
            string sql = @"SELECT FK_Article , 
                                  FK_Picture ,
                                  Id , 
                                  Name_picture
                                  FROM Article_Picture JOIN Picture 
                                  ON FK_Picture = Id WHERE FK_Article = @Id ;";

            IEnumerable<Picture> pics = _dbConnection.ExecuteReader(sql, dr => dr.ToPicture(), parameters: query ).ToList();

            foreach(Picture item in pics)
            {
                item.Name_picture = _contex.HttpContext!.Request.Scheme + "://" + _contex.HttpContext!.Request.Host.Value + "/Images/" + item.Name_picture;
            }

            return pics;

        }//end Execute

    }// end GetArticlePicturesQueryHandler

}//end name space 
