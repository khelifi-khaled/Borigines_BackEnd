using Borigines.Models.Entities;
using Models.Mappers;
using System.Data;
using Tools.CQRS.Commands;
using Tools.DataBase;

namespace Models.Commands
{
    public class DeleteUserCommand : ICommand
    {
        public DeleteUserCommand(int id)
        {
            Id = id;
        }

        public int Id { get; init; }


    }//end DeleteUserCommand

    public class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand>
    {
        private readonly IDbConnection _dbConnection;

        public DeleteUserCommandHandler(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }


        public IResult Execute(DeleteUserCommand command)
        {
            try
            {
                string sql = @"SELECT U.Id , 
                                      U.First_name , 
                                      U.Last_name , 
                                      U.Login,
                                      U.Is_Admin FROM Users U join Articles A ON 
                                      U.Id = A.FK_id_user
                                      WHERE U.Id = @Id ;";

                User? u = _dbConnection.ExecuteReader(sql,dr => dr.ToUser(),parameters : command).FirstOrDefault();

                if(u is null)
                {
                    sql = "DELETE FROM Users WHERE Id = @Id ;";
                    _dbConnection.ExecuteNonQuery(sql , parameters: command);

                }
                else
                {
                    if(u.IsAdmin)
                    {
                        return Result.Failure("Vous ne pouvez suprimer le compte d'Admin");
                    }
                    sql = "UpdateUserStatus";
                    //seting user status false 
                    _dbConnection.ExecuteNonQuery(sql, true, new { command.Id, status = 0 });
                }


                return Result.Success();

            }
            catch(Exception ex)
            {
                return Result.Failure(ex.Message);
            }

        }//end Execute

    }//end DeleteUserCommandHandler

}
