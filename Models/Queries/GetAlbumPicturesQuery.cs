

using Borigines.Models.Entities;
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

        public GetAlbumPicturesQueryHandler(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public IEnumerable<Picture>? Execute(GetAlbumPicturesQuery query)
        {
            string sql = @"SELECT P.Id , p.Name_picture 
                           FROM Picture p  JOIN Album_Picture ap 
                           ON p.Id = ap.FK_Picture WHERE ap.FK_Album = @AlbumId ;";
            IEnumerable<Picture>? pictures = _dbConnection.
                ExecuteReader(sql,dr => dr.ToPicture(),parameters : query);

            return pictures;
        }
    }

}//end name space 
