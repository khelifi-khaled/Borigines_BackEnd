
using Borigines.Models.Entities;
using Models.Mappers;
using System.Data;
using Tools.CQRS.Commands;
using Tools.DataBase;

namespace Models.Commands
{
    public  class DeletePictureCommand : ICommand 
    {
        public DeletePictureCommand(int idArt, string name_Picture)
        {
            IdArt = idArt;
            Name_Picture = name_Picture;
        }

        public int IdArt { get; init; }

        public string Name_Picture { get; init; }

    }//end deletePictureCommand 

    public class DeletePictureCommandHandler : ICommandHandler<DeletePictureCommand>
    {
        private readonly IDbConnection _dbConnection;

        public DeletePictureCommandHandler(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public IResult Execute(DeletePictureCommand command)
        {
            try
            {
                //geting pic Id from db 
                string sql = "SELECT Id , Name_picture from Picture WHERE Name_picture = @Name_Picture ; ";

                Picture? pictureToDelete = _dbConnection.ExecuteReader(sql, dr => dr.ToPicture(), parameters: new { command.Name_Picture }).SingleOrDefault();
                if (pictureToDelete is null)
                {
                    return Result.Failure("Picture to delete not found");
                }
                //delete pic from Article_Picture
                sql = "DELETE FROM Article_Picture WHERE FK_Picture = @id AND FK_Article = @IdArt ; ";
                _dbConnection.ExecuteNonQuery(sql, parameters: new { pictureToDelete.Id, command.IdArt });

                //delete pic from Picture table on DB 
                sql = "DELETE FROM Picture WHERE Id = @Id";
                _dbConnection.ExecuteNonQuery(sql, parameters: new { pictureToDelete.Id });

                

                return Result.Success();
            }
            catch (Exception ex )
            {
                return Result.Failure(ex.Message);
            }

        }//end Execute 

    }//end DeletePictureCommandHandler

}//end name space 
