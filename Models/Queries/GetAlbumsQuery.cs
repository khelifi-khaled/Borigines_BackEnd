

using Borigines.Models.Entities;
using Models.Mappers;
using System.Data;
using Tools.CQRS.Queries;
using Tools.DataBase;

namespace Models.Queries
{
    public class GetAlbumsQuery : IQuery<IEnumerable<Album>>
    {
        public GetAlbumsQuery()
        {

        }



    }//end class 

    public class GetAlbumsQueryHandler : IQueryHandler<GetAlbumsQuery, IEnumerable<Album>>
    {
        private readonly IDbConnection _dbConnection;

        public GetAlbumsQueryHandler(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public IEnumerable<Album>? Execute(GetAlbumsQuery query)
        {
            string sql = @"SELECT 
                                a.Id, 
                                a.Title, 
                                a.Date_Album, 
                                u.Id as uId , 
                                u.First_Name, 
                                u.Last_Name, 
                                u.Login, 
                                u.Is_Admin 
                                FROM Albums a JOIN Users u  
                                ON a.Id_user = u.id ; ";

            IEnumerable<Album>? albums = _dbConnection.ExecuteReader(sql, dr => dr.ToAlbum());
            return albums;
        }//end Execute
    }//end GetAlbumsQueryHandler
}//end name space 
