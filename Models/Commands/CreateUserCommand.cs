
using System.Data;
using Tools.CQRS.Commands;
using Tools.DataBase;

namespace Models.Commands
{
    public class CreateUserCommand : ICommand
    {
        public CreateUserCommand(string first_name, string last_name, string login, string password)
        {
            First_name = first_name;
            Last_name = last_name;
            Login = login;
            Password = password;
        }

        public string First_name { get; init; }
       
        public string Last_name { get; init; }
      
        public string Login { get; init; }
        
        public string Password { get; init; }


    }//end CreateUserCommand

    public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
    {
        private readonly IDbConnection _dbConnection;

        public CreateUserCommandHandler(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public IResult Execute(CreateUserCommand command)
        {
            try
            {
                string sql = "RegisterUser";
                _dbConnection.ExecuteNonQuery(sql, true, parameters: 
                    new {
                        first_name = command.First_name , 
                        last_name = command.Last_name,
                        login = command.Login,
                        passwd = command.Password
                    });
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }
    }

}//end name space 
