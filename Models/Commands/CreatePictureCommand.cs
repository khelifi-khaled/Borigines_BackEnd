using System.Data;
using Tools.CQRS.Commands;
using Tools.DataBase;

namespace Models.Commands
{
    public class CreatePictureCommand : ICommand
    {
        public CreatePictureCommand(int idArticle, string pictureName)
        {
            FK_Article = idArticle;
            Name_picture = pictureName;
        }

        public int FK_Article { get; init; }

        public string Name_picture { get; init; }

    }//end CreatePictureCommand

    public class CreatePictureCommandHandler : ICommandHandler<CreatePictureCommand>
    {
        private readonly IDbConnection _dbconnection;

        public CreatePictureCommandHandler(IDbConnection dbConnection)
        {
            _dbconnection = dbConnection;
        }

        public IResult Execute(CreatePictureCommand command)
        {
            try
            {
                string sql = "INSERT INTO Picture (Name_picture) OUTPUT INSERTED.Id VALUES (@Name_picture)";
                int? FK_Picture = _dbconnection.ExecuteScalar(sql, parameters: new
                {
                    command.Name_picture
                }) as int?;

                sql = "INSERT INTO Article_Picture (FK_Article , FK_Picture) VALUES (@FK_Article , @FK_Picture)";

                _dbconnection.ExecuteNonQuery(sql, parameters: new { FK_Picture, command.FK_Article });

                return Result.Success();
            }
            catch (Exception ex)
            {

                return Result.Failure(ex.Message);
            }

        }
    }


}//end name space 
