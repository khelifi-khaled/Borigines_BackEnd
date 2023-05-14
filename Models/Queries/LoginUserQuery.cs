using Borigines.Provider.Sql.Models;
using Models.Mappers;
using System.Data;
using Tools.CQRS.Queries;
using Tools.DataBase;

namespace Models.Queries
{
    public  class LoginUserQuery : IQuery<User>
    {
        public LoginUserQuery(string login, string password)
        {
            Login = login;
            Password = password;
        }

        public string Login { get; init; }

        public string Password { get; init; }

    }//end LoginUserQuery

    public class LoginUserQueryHandler : IQueryHandler<LoginUserQuery, User>
    {
        private readonly IDbConnection _dbConnection;

        public LoginUserQueryHandler(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public User? Execute(LoginUserQuery query)
        {
            string sql = "LoginUser";
            return _dbConnection.ExecuteReader(sql,dr => dr.ToUser(),true, new { login = query.Login, pwd = query.Password}).FirstOrDefault();
        }
    }


}//end name space 
