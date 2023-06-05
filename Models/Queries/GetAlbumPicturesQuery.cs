

using Borigines.Models.Entities;
using Microsoft.AspNetCore.Http;
using Models.Mappers;
using System.Data;
using Tools.CQRS.Queries;
using Tools.DataBase;

namespace Models.Queries
{
    public class GetAlbumPicturesQuery : IQuery<IEnumerable<Picture>>
    {
        public GetAlbumPicturesQuery(int albumId)
        {
            AlbumId = albumId;
        }

        public int AlbumId { get; init; }

    }//end GetAlbumPicturesQuery

    public class GetAlbumPicturesQueryHandler : IQueryHandler<GetAlbumPicturesQuery, IEnumerable<Picture>>
    {
        private readonly IDbConnection _dbConnection;
        private readonly IHttpContextAccessor _httpContext;

        public GetAlbumPicturesQueryHandler(IDbConnection dbConnection, IHttpContextAccessor httpContext)
        {
            _dbConnection = dbConnection;
            _httpContext = httpContext;
        }

        public IEnumerable<Picture>? Execute(GetAlbumPicturesQuery query)
        {
            string sql = @"SELECT P.Id , p.Name_picture 
                           FROM Picture p  JOIN Album_Picture ap 
                           ON p.Id = ap.FK_Picture WHERE ap.FK_Album = @AlbumId ;";
            IEnumerable<Picture>? pictures = _dbConnection.
                ExecuteReader(sql,dr => dr.ToPicture(),parameters : query).ToList();
            foreach (Picture item  in pictures)
            {
                item.Name_picture = _httpContext.HttpContext!.Request.Scheme + "://" + _httpContext.HttpContext!.Request.Host.Value + "/Images/" + item.Name_picture;
            }
            return pictures;
        }
    }

}//end name space 
