

using Borigines.Models.Entities;
using Models.Mappers;
using System.Data;
using System.Data.SqlTypes;
using Tools.CQRS.Queries;
using Tools.DataBase;

namespace Models.Queries
{
    public class CheckEmailQuery : IQuery<bool>
    {
        public CheckEmailQuery(string emailToCheck)
        {
            EmailToCheck = emailToCheck;
        }

        public string EmailToCheck { get; init; }


    }//end CheckEmailQuery

    public class CheckEmailQueryHandler : IQueryHandler<CheckEmailQuery, bool>
    {
        private readonly IDbConnection _dbConnection;

        public CheckEmailQueryHandler(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public bool Execute(CheckEmailQuery query)
        {
            string sql = @"SELECT Id , First_name , Last_name , [Login] , Passwd , Is_Admin 
                           FROM Users WHERE [Login] = @EmailToCheck ; ";
            IEnumerable<User>? users = _dbConnection.ExecuteReader(sql, dr => dr.ToUser(), parameters: new { query.EmailToCheck });
            return users.Any();
        }
    }

}//end name space 
