
using System.Data;
using Tools.CQRS.Commands;
using Tools.DataBase;

namespace Models.Commands
{
    public class UpdateUserCommand : ICommand
    {
        public UpdateUserCommand(int id, string first_name, string last_name, string login, string password)
        {
            Id = id;
            First_name = first_name;
            Last_name = last_name;
            Login = login;
            Password = password;
        }

        public int Id { get; init; }

        public string First_name { get; init; }
    
        public string Last_name { get; init; }
    
        public string Login { get; init; }
     
        public string Password { get; init; }

    }//end UpdateUserCommand


    public class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand>
    {
        private readonly IDbConnection _dbConnection;

        public UpdateUserCommandHandler (IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public IResult Execute(UpdateUserCommand command)
        {
            try
            {
                string sql = "UpdateUser";
                _dbConnection.ExecuteNonQuery(sql, true, parameters:
                    new
                    {
                        id = command.Id,
                        first_name = command.First_name,
                        last_name = command.Last_name,
                        login = command.Login,
                        passwd = command.Password
                    });

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);

            }//end try catch 

        }//end Execute

    }//end UpdateUserCommandHandler


}//end name space 
