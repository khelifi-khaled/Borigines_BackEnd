using Borigines.Provider.Sql.Models;
using Models.Mappers;
using System.Data;
using Tools.CQRS.Queries;
using Tools.DataBase;

namespace Models.Queries
{
    public class GetUsersQuery : IQuery<IEnumerable<User>>
    {
    }

    public class GetUsersQueryHandler : IQueryHandler<GetUsersQuery, IEnumerable<User>>
    {
        private readonly IDbConnection _dbConnection;

        public GetUsersQueryHandler(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public IEnumerable<User>Execute(GetUsersQuery query)
        {
            string sql = "SELECT Id, First_name, Last_name, [Login] , Is_Admin  FROM Users WHERE Is_Active = 1 ;";

            return _dbConnection.ExecuteReader(sql,dr => dr.ToUser());
        }
    }
}
